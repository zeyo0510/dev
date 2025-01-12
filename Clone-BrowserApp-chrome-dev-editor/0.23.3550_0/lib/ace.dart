// Copyright (c) 2013, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

/**
 * An entrypoint to the [ace.dart](https://github.com/rmsmith/ace.dart) package.
 */
library spark.ace;

import 'dart:async';
import 'dart:html' as html;
import 'dart:js' as js;
import 'dart:math' as math;

import 'package:ace/ace.dart' as ace;
import 'package:ace/proxy.dart';
//import 'package:crypto/crypto.dart' as crypto;
import 'package:crc32/crc32.dart' as crc;

import 'css/cssbeautify.dart';
import 'dependency.dart';
import 'editors.dart';
import 'markdown.dart';
import 'navigation.dart';
import 'package_mgmt/bower_properties.dart';
import 'package_mgmt/pub.dart';
import 'platform_info.dart';
import 'preferences.dart';
import 'utils.dart' as utils;
import 'workspace.dart' as workspace;
import 'workspace_utils.dart';
import 'services.dart' as svc;
import 'spark_flags.dart';
import 'state.dart';
import 'outline.dart';
import 'ui/goto_line_view/goto_line_view.dart';
import 'utils.dart';

export 'package:ace/ace.dart' show EditSession;

dynamic get _spark => html.querySelector('spark-polymer-ui');
dynamic get _toggleOutlineButton => _spark.$['toggle-outline'];
dynamic get _polymerDesignerButton => _spark.$['openPolymerDesignerButton'];

class TextEditor extends Editor {
  static final RegExp _WHITESPACE_REGEX = new RegExp(r'[\t ]+$', multiLine: false);
  static const int _LARGE_FILE_SIZE = 500000;

  final AceManager aceManager;
  final workspace.File file;

  StreamSubscription _aceSubscription;
  final StreamController _dirtyController = new StreamController.broadcast();
  final StreamController _modificationController = new StreamController.broadcast();
  final Completer<Editor> _whenReadyCompleter = new Completer();

  final SparkPreferences _prefs;
  ace.EditSession _session;

  int _lastSavedHash;

  bool _dirty = false;

  StateManager get state => Dependencies.dependency[StateManager];

  factory TextEditor(AceManager aceManager, workspace.File file,
      SparkPreferences prefs) {
    if (DartEditor.isDartFile(file)) {
      return new DartEditor._create(aceManager, file, prefs);
    }
    if (CssEditor.isCssFile(file)) {
      return new CssEditor._create(aceManager, file, prefs);
    }
    if (MarkdownEditor.isMarkdownFile(file)) {
      return new MarkdownEditor._create(aceManager, file, prefs);
    }
    if (HtmlEditor.isHtmlFile(file)) {
      return new HtmlEditor._create(aceManager, file, prefs);
    }
    if (YamlEditor.isYamlFile(file)) {
      return new YamlEditor._create(aceManager, file, prefs);
    }
    if (GoEditor.isGoFile(file)) {
      return new GoEditor._create(aceManager, file, prefs);
    }
    if (JsonEditor.isJsonFile(file)) {
      return new JsonEditor._create(aceManager, file, prefs);
    }
    return new TextEditor._create(aceManager, file, prefs);
  }

  TextEditor._create(this.aceManager, this.file, this._prefs);

  Future<Editor> get whenReady => _whenReadyCompleter.future;

  bool get dirty => _dirty;

  Stream get onDirtyChange => _dirtyController.stream;
  Stream get onModification => _modificationController.stream;

  set dirty(bool value) {
    if (value != _dirty) {
      _dirty = value;
      _dirtyController.add(value);
    }
    _modificationController.add(dirty);
  }

  html.Element get element => aceManager.containerElement;

  void activate() {
    _outline.visible = supportsOutline;
    // TODO(devoncarew): Instead of ace.dart knowing about the toggle outline
    // button, the button should instead listen to the enablement of the
    // 'toggle-outline' command (#3478).
    _toggleOutlineButton.disabled = !supportsOutline;
    aceManager._aceEditor.readOnly = readOnly;
  }

  void setSession(ace.EditSession value) {
    _session = value;

    customizeSession(_session);

    restoreState();

    if (_aceSubscription != null) _aceSubscription.cancel();
    _aceSubscription = _session.onChange.listen((_) => dirty = true);
    if (!_whenReadyCompleter.isCompleted) _whenReadyCompleter.complete(this);

    _invokeReconcile();
  }

  /**
   * Called a short delay after the file's content has been altered.
   */
  void reconcile() { }

  void deactivate() {
    if (supportsOutline && _outline.visible) {
      _outline.visible = false;
    }

    _toggleOutlineButton.disabled = true;

    saveState();
  }

  void resize() => aceManager.resize();

  void focus() => aceManager.focus();

  void select(Span selection) {
    // Check if we're the current editor.
    if (file != aceManager.currentFile) return;

    ace.Point startSelection = _session.document.indexToPosition(selection.offset);
    ace.Point endSelection = _session.document.indexToPosition(
        selection.offset + selection.length);

    aceManager._aceEditor.gotoLine(startSelection.row);
    aceManager._aceEditor.scrollToLine(startSelection.row, center: true);

    ace.Selection aceSel = aceManager._aceEditor.selection;
    aceSel.setSelectionAnchor(startSelection.row, startSelection.column);
    aceSel.selectTo(endSelection.row, endSelection.column);
  }

  bool get supportsOutline => false;

  bool get supportsFormat => false;

  bool get readOnly {
    return
        !SparkFlags.packageFilesAreEditable && (
            pubProperties.isInPackagesFolder(file) ||
            bowerProperties.isInPackagesFolder(file));
  }

  void format() { }

  /**
   * Jump to the declaration of the symbol currently under the cursor.
   */
  Future<svc.Declaration> navigateToDeclaration([Duration timeLimit]) =>
      new Future.value(svc.Declaration.EMPTY_DECLARATION);

  void saveState() {
    if (_session == null) return;

    html.Point p = aceManager.cursorPosition;
    state.setState(file.uuid, {
      'scrollTop': _session.scrollTop,
      'column': p.x,
      'row': p.y
    });
  }

  void restoreState() {
    Map m = state.getState(file.uuid);

    if (m != null) {
      _session.scrollTop = m['scrollTop'];

      // TODO(devoncarew): Consider saving a SHA for the file, so that we don't
      // select text that is wildly different from the (possible changed)
      // current version of the file.
      aceManager.cursorPosition = new html.Point(m['x'], m['y']);
    }
  }

  void fileContentsChanged() {
    if (_session != null) {
      // Check that we didn't cause this change event.
      file.getContents().then((String text) {
        int fileContentsHash = crc.CRC32.compute(text);

        if (fileContentsHash != _lastSavedHash) {
          _lastSavedHash = fileContentsHash;
          _replaceContents(text);
        }
      });
    }
  }

  bool fileIsLarge(String text) {
    return (text.length > _LARGE_FILE_SIZE);
  }

  Future save() {
    // We store a hash of the contents when saving. When we get a change
    // notification (in fileContentsChanged()), we compare the last write to the
    // contents on disk.
    if (_dirty) {
      String text = _session.value;

      // Remove the trailing whitespace if asked to do so.
      if (_prefs.stripWhitespaceOnSave.value) {
        if (!fileIsLarge(text)) {
          text = text.replaceAll(_WHITESPACE_REGEX, '');
        }
      }

      _lastSavedHash = crc.CRC32.compute(text);

      return file.setContents(text).then((_) {
        dirty = false;
        _invokeReconcile();
      });
    } else {
      return new Future.value();
    }
  }

  int getCursorOffset() => _session.document.positionToIndex(
      aceManager._aceEditor.cursorPosition);

  void customizeSession(ace.EditSession session) {
    // By default, all file types use 2-space soft tabs for indentation.
    session.tabSize = 2;
    session.useSoftTabs = true;
    // Select the range of an undo or redo operation.
    session.undoSelect = true;
  }

  /**
   * Replace the editor's contents with the given text.
   */
  void replaceContents(String newContents) =>
      _replaceContents(newContents, fireEvent: true);

  /**
   * Replace the editor's contents with the given text - internal version.
   * Suppress firing Ace event by default: internal callers don't need it.
   */
  void _replaceContents(String newContents, {fireEvent: false}) {
    if (!fireEvent) {
      _aceSubscription.cancel();
    }

    try {
      // Restore the cursor position and scroll location.
      num scrollTop = _session.scrollTop;
      html.Point cursorPos = (aceManager.currentFile == file) ?
          aceManager.cursorPosition : null;

      // Use `session.doc.setValue()` here to preserve the undo stack (we don't
      // use `session.value = newContents` here as it resets the undo stack).
      _session.document.value = newContents;

      _session.scrollTop = scrollTop;
      if (cursorPos != null) aceManager.cursorPosition = cursorPos;

      _invokeReconcile();
    } finally {
      if (!fireEvent) {
        _aceSubscription = _session.onChange.listen((_) => dirty = true);
      }
    }
  }

  void _invokeReconcile() {
    reconcile();
  }

  Outline get _outline => aceManager.outline;

  /**
   * Handle navigating to file references in strings. So, things like:
   *
   *     @import url("packages/bootstrap/bootstrap.min.css");
   */
  Future<svc.Declaration> _simpleNavigateToDeclaration([Duration timeLimit]) {
    if (file.parent == null) {
      return new Future.value(svc.Declaration.EMPTY_DECLARATION);
    }

    String path = _getQuotedString(_session.value, getCursorOffset());
    if (path == null) return new Future.value(svc.Declaration.EMPTY_DECLARATION);

    workspace.File targetFile = resolvePath(file, path);

    if (targetFile != null) {
      aceManager.delegate.openEditor(targetFile);
      return new Future.value(new svc.FileDeclaration(targetFile));
    } else {
      return new Future.value();
    }
  }
}

class DartEditor extends TextEditor {
  static bool isDartFile(workspace.File file) => file.name.endsWith('.dart');

  OffsetRange outlineScrollPosition = new OffsetRange();

  DartEditor._create(AceManager aceManager, workspace.File file, SparkPreferences prefs) :
      super._create(aceManager, file, prefs);

  void customizeSession(ace.EditSession session) {
    // Dart files use 2-space soft tabs for indentation.
    session.tabSize = 2;
    session.useSoftTabs = true;
  }

  bool get supportsOutline => true;

  @override
  void activate() {
    super.activate();

    // Outline will be built in reconcile().
    //_outline.build(file.name, _session.value);

    _outline.scrollPosition = outlineScrollPosition;

    if (file.project != null) {
      aceManager._analysisService.getCreateProjectAnalyzer(file.project);
    }
  }

  @override
  void deactivate() {
    super.deactivate();

    outlineScrollPosition = _outline.scrollPosition;
  }

  void reconcile() {
    OffsetRange pos = _outline.scrollPosition;
    _outline.build(file.name, _session.value).then((_) {
      _outline.scrollPosition = pos;
    });
  }

  Future<svc.Declaration> navigateToDeclaration([Duration timeLimit]) {
    int offset = getCursorOffset();

    Future declarationFuture = aceManager._analysisService.getDeclarationFor(
        file, offset);

    if (timeLimit != null) {
      declarationFuture = declarationFuture.timeout(timeLimit, onTimeout: () {
        throw new TimeoutException("navigateToDeclaration timed out");
      });
    }

    return declarationFuture.then((svc.Declaration declaration) {
      if (declaration != null) {
        if (declaration is svc.SourceDeclaration) {
          workspace.File targetFile = declaration.getFile(file.project);

          // Open targetFile and select the range of text.
          if (targetFile != null) {
            Span selection = new Span(declaration.offset, declaration.length);
            aceManager.delegate.openEditor(targetFile, selection: selection);
          }
        } else if (declaration is svc.DocDeclaration) {
          html.window.open(declaration.url, "spark_doc");
        }
      }
      return declaration;
    });
  }
}

class CssEditor extends TextEditor {
  static bool isCssFile(workspace.File file) => file.name.endsWith('.css');

  CssEditor._create(AceManager aceManager, workspace.File file, SparkPreferences prefs) :
      super._create(aceManager, file, prefs);

  bool get supportsFormat => true;

  void format() {
    String oldValue = _session.value;
    String newValue = new CssBeautify().format(oldValue);
    if (newValue != oldValue) {
      _replaceContents(newValue);
      dirty = true;
    }
  }

  Future<svc.Declaration> navigateToDeclaration([Duration timeLimit]) =>
      _simpleNavigateToDeclaration(timeLimit);
}

class MarkdownEditor extends TextEditor {
  static bool isMarkdownFile(workspace.File file) =>
      file.name.toLowerCase().endsWith('.md');

  Markdown _markdown;

  MarkdownEditor._create(AceManager aceManager, workspace.File file, SparkPreferences prefs) :
      super._create(aceManager, file, prefs) {
    // Parent this at the tab container level.
    _markdown = new Markdown(element.parent.parent, file);
  }

  @override
  void activate() {
    super.activate();
    _markdown.activate();
  }

  @override
  void deactivate() {
    super.deactivate();
    _markdown.deactivate();
  }

  @override
  void reconcile() {
    _markdown.renderHtml();
  }
}

class HtmlEditor extends TextEditor {
  static bool isHtmlFile(workspace.File file) => isHtmlFilename(file.name);

  HtmlEditor._create(AceManager aceManager, workspace.File file, SparkPreferences prefs) :
      super._create(aceManager, file, prefs);

  void activate() {
    _polymerDesignerButton.classes.toggle('hidden', false);
    super.activate();
  }

  void deactivate() {
    _polymerDesignerButton.classes.toggle('hidden', true);
    super.deactivate();
  }

  Future<svc.Declaration> navigateToDeclaration([Duration timeLimit]) =>
      _simpleNavigateToDeclaration(timeLimit);
}

class JsonEditor extends TextEditor {
  static bool isJsonFile(workspace.File file) => file.name.endsWith('.json');

  JsonEditor._create(AceManager aceManager, workspace.File file, SparkPreferences prefs) :
      super._create(aceManager, file, prefs);

  Future<svc.Declaration> navigateToDeclaration([Duration timeLimit]) =>
      _simpleNavigateToDeclaration(timeLimit);
}

/**
 * An editor for `.go` files. Go's convention is to use hard tabs for
 * indentation.
 */
class GoEditor extends TextEditor {
  static bool isGoFile(workspace.File file) => file.name.endsWith('.go');

  GoEditor._create(AceManager aceManager, workspace.File file,
      SparkPreferences prefs) : super._create(aceManager, file, prefs);

  void customizeSession(ace.EditSession session) {
    super.customizeSession(session);

    // Go files use hard tabs for indentation.
    session.useSoftTabs = false;

    // The number of spaces to use is not specified by Go.
    session.tabSize = 4;
  }
}

/**
 * An editor for `.yaml` files. The yaml format does not accept tabs.
 */
class YamlEditor extends TextEditor {
  static bool isYamlFile(workspace.File file) => file.name.endsWith('.yaml');

  YamlEditor._create(AceManager aceManager, workspace.File file,
      SparkPreferences prefs) : super._create(aceManager, file, prefs);

  void customizeSession(ace.EditSession session) {
    // Yaml files use 2-space soft tabs for indentation.
    session.tabSize = 2;

    // Hard tabs are not supported.
    session.useSoftTabs = true;
  }
}

/**
 * A wrapper around an Ace editor instance.
 */
class AceManager {
  static final KEY_BINDINGS = ace.KeyboardHandler.BINDINGS;

  final AceManagerDelegate delegate;
  final SparkPreferences _prefs;

  /**
   * The container for the Ace editor.
   */
  html.DivElement containerElement;

  Outline outline;

  final StreamController _onGotoDeclarationController = new StreamController();
  Stream get onGotoDeclaration => _onGotoDeclarationController.stream;
  GotoLineView gotoLineView;

  ace.Editor _aceEditor;
  ace.EditSession _currentSession;

  workspace.Marker _currentMarker;

  StreamSubscription<ace.FoldChangeEvent> _foldListenerSubscription;

  static bool get available => js.context['ace'] != null;

  StreamSubscription _markerSubscription;
  workspace.File currentFile;
  svc.AnalyzerService _analysisService;

  ace.EditSession _markerSession = null;
  int _linkingMarkerId;

  AceManager(this.delegate, svc.Services services, this._prefs) {
    ace.implementation = ACE_PROXY_IMPLEMENTATION;
    // Enable code completion.
    ace.require('ace/ext/language_tools');
    // Enable hyper-linking.
    ace.require('ace/ext/linking');

    _analysisService =  services.getService("analyzer");

    // Add some additional file extension editors.
    ace.Mode.extensionMap['classpath'] = ace.Mode.XML;
    ace.Mode.extensionMap['gyp'] = ace.Mode.PYTHON;
    ace.Mode.extensionMap['gypi'] = ace.Mode.PYTHON;
    ace.Mode.extensionMap['idl'] = ace.Mode.C_CPP;
    ace.Mode.extensionMap['lock'] = ace.Mode.YAML;
    ace.Mode.extensionMap['nmf'] = ace.Mode.JSON;
    ace.Mode.extensionMap['project'] = ace.Mode.XML;
    ace.Mode.extensionMap['webapp'] = ace.Mode.JSON;
    ace.Mode.extensionMap['gsp'] = ace.Mode.HTML;
    ace.Mode.extensionMap['jsp'] = ace.Mode.HTML;
    ace.Mode.extensionMap['sql'] = ace.Mode.SQL;
    ace.Mode.extensionMap['sqlite'] = ace.Mode.SQL;
    // The extensions used in Spark's own internal templates.
    ace.Mode.extensionMap['html_'] = ace.Mode.HTML;
    ace.Mode.extensionMap['css_'] = ace.Mode.CSS;
    ace.Mode.extensionMap['js_'] = ace.Mode.JAVASCRIPT;
    ace.Mode.extensionMap['dart_'] = ace.Mode.DART;
    ace.Mode.extensionMap['json_'] = ace.Mode.JSON;
    ace.Mode.extensionMap['yaml_'] = ace.Mode.YAML;
    // The extension that "Refactor for CSP" feature assigns to originals of
    // refactored HTMLs.
    ace.Mode.extensionMap['pre_csp'] = ace.Mode.HTML;

    // NOTE: The id isn't really needed, but is useful for DOM debugging.
    containerElement = html.document.createElement('div')..id = "aceContainer";

    List<ace.Command> customCommands = [];
    // Override Ace's `gotoline` command.
    customCommands.add(new ace.Command(
        'gotoline',
        const ace.BindKey(mac: 'Command-L', win: 'Ctrl-L'),
        _showGotoLineView)
    );
    if (PlatformInfo.isMac) {
      customCommands.add(new ace.Command(
          'scrolltobeginningofdocument',
          const ace.BindKey(mac: 'Home'),
          _scrollToBeginningOfDocument)
      );
      customCommands.add(new ace.Command(
          'scrolltoendofdocument',
          const ace.BindKey(mac: 'End'),
          _scrollToEndOfDocument)
      );
    }

    _aceEditor = ace.edit(containerElement);
    _aceEditor.renderer.fixedWidthGutter = true;
    _aceEditor
        ..highlightActiveLine = true
        ..printMarginColumn = 80
        ..readOnly = true;
    _aceEditor.setOptions({
        'enableBasicAutocompletion': true,
        // TODO(devoncarew): Disabled to workaround #2442.
        //..setOption('enableSnippets', true)
        'enableLinking': true,
        'enableMultiselect': SparkFlags.enableMultiSelect
    });
    _aceEditor.commands
        ..addCommands(customCommands)
        // Remove Ace's `ctrl-,` binding, which opens a popup with the settings menu.
        ..removeCommand('showSettingsMenu');

    // Underine links on hover.
    _aceEditor.onLinkHover.listen(_decorateLink);
    // Force removal of the sometimes stuck link underline.
    containerElement.onKeyUp.listen(_undecorateLink);

    // Setup ACCEL + clicking on declaration
    var node = containerElement.getElementsByClassName("ace_content")[0];
    node.onClick.listen((e) {
      if (e.altKey && (PlatformInfo.isMac ? e.metaKey : e.ctrlKey)) {
        _onGotoDeclarationController.add(null);
      }
    });

    _setupGotoLine();
  }

  void _decorateLink(ace.LinkEvent event) {
    if (currentFile == null || !DartEditor.isDartFile(currentFile)) return;

    ace.Token token = event.token;

    if (token != null && token.type == "identifier") {
      int startColumn = event.token.start;
      ace.Point startPosition =
          new ace.Point(event.position.row, startColumn);
      int endColumn = startColumn + event.token.value.length;
      ace.Point endPosition = new ace.Point(event.position.row, endColumn);
      ace.Range markerRange =
          new ace.Range.fromPoints(startPosition, endPosition);
      _setLinkingMarker(markerRange);
    } else {
      _setLinkingMarker(null);
    }
  }

  void _undecorateLink(html.KeyboardEvent event) {
    if ((PlatformInfo.isMac && event.keyCode == html.KeyCode.META) ||
        (!PlatformInfo.isMac && event.keyCode == html.KeyCode.CTRL)) {
      _setLinkingMarker(null);
    }
  }

  void setupOutline(html.Element outlineContainer) {
    outline = new Outline(_analysisService, outlineContainer, _prefs.prefsStore);
    outline.visible = false;
    outline.onChildSelected.listen((OutlineItem item) {
      ace.Point startPoint =
          currentSession.document.indexToPosition(item.nameStartOffset);
      ace.Point endPoint =
          currentSession.document.indexToPosition(item.nameEndOffset);

      ace.Selection selection = _aceEditor.selection;
      _aceEditor.gotoLine(startPoint.row);
      selection.setSelectionAnchor(startPoint.row, startPoint.column);
      selection.selectTo(endPoint.row, endPoint.column);
      _aceEditor.focus();
    });

    ace.Point lastCursorPosition =  new ace.Point(-1, -1);
    _aceEditor.onChangeSelection.listen((_) {
      ace.Point currentPosition = _aceEditor.cursorPosition;
      // Cancel the last outline selection update.
      if (lastCursorPosition != currentPosition) {
        outline.selectItemAtOffset(
            currentSession.document.positionToIndex(currentPosition));
        lastCursorPosition = currentPosition;
      }
    });
  }

  // Set up the goto line dialog.
  void _setupGotoLine() {
    gotoLineView = new GotoLineView();
    if (gotoLineView is! GotoLineView) {
      html.querySelector('#splashScreen').style.backgroundColor = 'red';
    }
    gotoLineView.style.zIndex = '101';
    containerElement.children.add(gotoLineView);
    gotoLineView.onTriggered.listen(_handleGotoLineViewEvent);
    gotoLineView.onClosed.listen(_handleGotoLineViewClosed);
    containerElement.onKeyDown
        .where((e) => e.keyCode == html.KeyCode.ESC)
        .listen((_) => gotoLineView.hide());
  }

  void _setLinkingMarker(ace.Range markerRange) {
    // Always remove a previous hover
    if (_linkingMarkerId != null) {
      _markerSession.removeMarker(_linkingMarkerId);
      _linkingMarkerId = null;
    }

    // html.DivElement contentElement =
    //     _aceEditor.renderer.containerElement.querySelector(".ace_content");

    if (markerRange != null) {
      _markerSession = currentSession;
      _linkingMarkerId = _markerSession.addMarker(markerRange,
          "ace_link_marker", type: ace.Marker.TEXT);
    }
  }

  bool isFileExtensionEditable(String extension) {
    if (extension.startsWith('.')) {
      extension = extension.substring(1);
    }
    return ace.Mode.extensionMap[extension] != null;
  }

  html.Element get _minimapElement {
    List element = containerElement.getElementsByClassName("minimap");
    return element.length == 1 ? element.first : null;
  }

  String _formatAnnotationItemText(String text, [String type]) {
    if (type == null) return text;

    return "<img class='ace-tooltip' src='images/${type}_icon.png'> ${text}";
  }

  void setMarkers(List<workspace.Marker> markers) {
    List<ace.Annotation> annotations = [];
    int numberLines = currentSession.screenLength;

    _recreateMiniMap();
    Map<int, ace.Annotation> annotationByRow = new Map<int, ace.Annotation>();

    html.Element minimap = _minimapElement;

    // Sort by line, then by severity.
    markers.sort((m1, m2) {
      if (m1.severity == m2.severity) {
        return m1.lineNum.compareTo(m2.lineNum);
      } else {
        return m2.severity.compareTo(m1.severity);
      }
    });

    int numberMarkers = markers.length.clamp(0, 100);

    var isScrolling = (_aceEditor.lastVisibleRow -
        _aceEditor.firstVisibleRow + 1) < currentSession.document.length;

    num documentHeight;
    if (!isScrolling) {
      var lineElements = containerElement.getElementsByClassName("ace_line");
      documentHeight = (lineElements.last.offsetTo(containerElement).y -
          lineElements.first.offsetTo(containerElement).y);
    }

    for (int markerIndex = 0; markerIndex < numberMarkers; markerIndex++) {
      workspace.Marker marker = markers[markerIndex];
      String annotationType = _convertMarkerSeverity(marker.severity);

      // Style the marker with the annotation type.
      String markerHtml = _formatAnnotationItemText(marker.message,
          annotationType);

      ace.Point charPoint = currentSession.document.indexToPosition(
          marker.charStart);
      // Ace uses 0-based lines.
      int aceRow = marker.lineNum - 1;
      int aceColumn = charPoint.column;

      // If there is an existing annotation, delete it and combine into one.
      var existingAnnotation = annotationByRow[aceRow];
      if (existingAnnotation != null) {
        markerHtml = '${existingAnnotation.html}'
            '<div class="ace-tooltip-divider"></div>${markerHtml}';
        annotations.remove(existingAnnotation);
      }

      ace.Annotation annotation = new ace.Annotation(
          html: markerHtml,
          row: aceRow,
          type: existingAnnotation != null ? existingAnnotation.type : annotationType);
      annotations.add(annotation);
      annotationByRow[aceRow] = annotation;

      double verticalPercentage = currentSession.documentToScreenRow(
          marker.lineNum, aceColumn) / numberLines;

      String markerPos;

      if (!isScrolling) {
        markerPos = '${verticalPercentage * documentHeight}px';
      } else {
        markerPos = (verticalPercentage * 100.0).toStringAsFixed(2) + "%";
      }

      // TODO(ericarnold): This should also be based upon annotations so ace's
      //     immediate handling of deleting / adding lines gets used.

      // Only add errors and warnings to the mini-map.
      if (marker.severity >= workspace.Marker.SEVERITY_WARNING) {
        html.Element minimapMarker = new html.Element.div();
        minimapMarker.classes.addAll(['minimap-marker', marker.severityDescription]);
        minimapMarker.style.top = markerPos;
        minimapMarker.onClick.listen((e) => _miniMapMarkerClicked(e, marker));

        minimap.append(minimapMarker);
      }
    }

    currentSession.setAnnotations(annotations);
  }

  void selectNextMarker() {
    _selectMarkerFromCurrent(1);
  }

  void selectPrevMarker() {
    _selectMarkerFromCurrent(-1);
  }

  void _selectMarkerFromCurrent(int offset) {
    // TODO(ericarnold): This should be based upon the current cursor position.
    List<workspace.Marker> markers = currentFile.getMarkers();
    if (markers != null && markers.length > 0) {
      if (_currentMarker == null) {
        _selectMarker(markers[0]);
      } else {
        int markerIndex = markers.indexOf(_currentMarker);
        markerIndex += offset;
        if (markerIndex < 0) {
          markerIndex = markers.length -1;
        } else if (markerIndex >= markers.length) {
          markerIndex = 0;
        }
        _selectMarker(markers[markerIndex]);
      }
    }
  }

  void _miniMapMarkerClicked(html.MouseEvent event, workspace.Marker marker) {
    event.stopPropagation();
    event.preventDefault();
    _selectMarker(marker);
  }

  void _selectMarker(workspace.Marker marker) {
    _aceEditor.gotoLine(marker.lineNum);
    ace.Selection selection = _aceEditor.selection;
    ace.Range range = selection.getLineRange(marker.lineNum - 1);
    selection.setSelectionAnchor(range.end.row, range.end.column);
    selection.selectTo(range.start.row, range.start.column);
    _aceEditor.focus();
    _currentMarker = marker;
  }

  void _recreateMiniMap() {
    if (containerElement == null) {
      return;
    }

    html.Element miniMap = new html.Element.div();
    miniMap.classes.add("minimap");

    if (_minimapElement != null) {
      _minimapElement.replaceWith(miniMap);
    } else {
      containerElement.append(miniMap);
    }
  }

  void clearMarkers() => currentSession.clearAnnotations();

  Future<String> getKeyBinding() {
    var handler = _aceEditor.keyBinding.keyboardHandler;
    return handler.onLoad.then((_) {
      return KEY_BINDINGS.contains(handler.name) ? handler.name : null;
    });
  }

  void setKeyBinding(String name) {
    var handler = new ace.KeyboardHandler.named(name);
    handler.onLoad.then((_) => _aceEditor.keyBinding.keyboardHandler = handler);
  }

  num getFontSize() => _aceEditor.fontSize;

  void setFontSize(num size) {
    _aceEditor.fontSize = size;
    outline.setFontSize(size);
  }

  void focus() => _aceEditor.focus();

  void resize() => _aceEditor.resize(false);

  html.Point get cursorPosition {
    ace.Point cursorPosition = _aceEditor.cursorPosition;
    return new html.Point(cursorPosition.column, cursorPosition.row);
  }

  void set cursorPosition(html.Point position) {
    _aceEditor.navigateTo(position.y, position.x);
  }

  ace.EditSession createEditSession(String text, String fileName) {
    ace.EditSession session = ace.createEditSession(text,
        new ace.Mode.forFile(fileName));
    // Disable Ace's analysis (this shows up in JavaScript files).
    session.useWorker = false;
    return session;
  }

  ace.EditSession get currentSession => _currentSession;

  void switchTo(ace.EditSession session, [workspace.File file]) {
    if (_foldListenerSubscription != null) {
      _foldListenerSubscription.cancel();
      _foldListenerSubscription = null;
    }

    if (session == null) {
      _currentSession = ace.createEditSession('', new ace.Mode('ace/mode/text'));
      _aceEditor.session = _currentSession;
      currentFile = null;
    } else {
      _currentSession = session;
      _aceEditor.session = _currentSession;

      _foldListenerSubscription = currentSession.onChangeFold.listen((_) {
        setMarkers(file.getMarkers());
      });
    }

    // Setup the code completion options for the current file type.
    if (file != null) {
      currentFile = file;
      // For now, we turn on lexical code completion for Dart files. We'll want
      // to switch this over to semantic code completion soonest.
      //_aceEditor.setOption(
      //    'enableBasicAutocompletion', path.extension(file.name) != '.dart');

      if (_markerSubscription == null) {
        _markerSubscription = file.workspace.onMarkerChange.listen(
            _handleMarkerChange);
      }

      setMarkers(file.getMarkers());
      session.onChangeScrollTop.listen((_) => Timer.run(() {
        if (outline.showing) {
          int firstCursorOffset = currentSession.document.positionToIndex(
              new ace.Point(_aceEditor.firstVisibleRow, 0));
          int lastCursorOffset = currentSession.document.positionToIndex(
              new ace.Point(_aceEditor.lastVisibleRow, 0));

          outline.scrollOffsetRangeIntoView(
              new OffsetRange(firstCursorOffset, lastCursorOffset));
        }
      }));
    }
  }

  void _handleMarkerChange(workspace.MarkerChangeEvent event) {
    if (event.hasChangesFor(currentFile)) {
      setMarkers(currentFile.getMarkers());
    }
  }

  String _convertMarkerSeverity(int markerSeverity) {
    switch (markerSeverity) {
      case workspace.Marker.SEVERITY_ERROR:
        return ace.Annotation.ERROR;
      case workspace.Marker.SEVERITY_WARNING:
        return ace.Annotation.WARNING;
      default:
        return ace.Annotation.INFO;
    }
  }

  void _showGotoLineView(_) {
    html.Element searchElement = containerElement.querySelector('.ace_search');
    if (searchElement != null) searchElement.style.display = 'none';
    gotoLineView.show();
  }

  void _handleGotoLineViewEvent(_) {
    _aceEditor.gotoLine(gotoLineView.lineNumber);
    gotoLineView.hide();
  }

  void _handleGotoLineViewClosed(_) => focus();

  void _scrollToBeginningOfDocument(_) {
    _currentSession.scrollTop = 0;
  }

  void _scrollToEndOfDocument(_) {
    int lineHeight = html.querySelector('.ace_gutter-cell').clientHeight;
    _currentSession.scrollTop = _currentSession.document.length * lineHeight;
  }

  NavigationLocation get navigationLocation {
    if (currentFile == null) return null;
    ace.Range range = _aceEditor.selection.range;
    int offsetStart = _currentSession.document.positionToIndex(range.start);
    int offsetEnd = _currentSession.document.positionToIndex(range.end);
    Span span = new Span(offsetStart, offsetEnd - offsetStart);
    return new NavigationLocation(currentFile, span);
  }
}

class ThemeManager {
  static final _THEMES = [
      // Dark bg color themes:
      'ambiance',
      'chaos',
      'clouds_midnight',
      'cobalt',
      'idle_fingers',
      'kr_theme',
      'merbivore',
      'merbivore_soft',
      'mono_industrial',
      'monokai',
      'pastel_on_dark',
      'solarized_dark',
      'terminal',
      'tomorrow_night',
      'tomorrow_night_blue',
      'tomorrow_night_bright',
      'tomorrow_night_eighties',
      'twilight',
      'vibrant_ink',
      // White bg color themes:
      'chrome',
      'clouds',
      'crimson_editor',
      'dreamweaver',
      'eclipse',
      // This one uses bold font for keywords: doesn't work well with Monaco.
      // 'github',
      'textmate',
      'tomorrow',
      'xcode',
      // Non-white bg color themes:
      // This one has the same bg color as CDE: looks bad, esp. with the tab bar.
      // 'dawn',
      'katzenmilch',
      'kuroir',
      'solarized_light',
  ];

  ace.Editor _aceEditor;
  SparkPreferences _prefs;
  html.Element _label;

  ThemeManager(AceManager aceManager, this._prefs, this._label) :
      _aceEditor = aceManager._aceEditor {

    String theme = _prefs.editorTheme.value;
    if (theme == null || theme.isEmpty || !_THEMES.contains(theme)) {
      theme = _THEMES[0];
    }
    _updateTheme(theme);
  }

  void nextTheme(html.Event e) {
    e.stopPropagation();
    _changeTheme(1);
  }

  void prevTheme(html.Event e) {
    e.stopPropagation();
    _changeTheme(-1);
  }

  void _changeTheme(int direction) {
    int index = _THEMES.indexOf(_aceEditor.theme.name);
    index = (index + direction) % _THEMES.length;
    String newTheme = _THEMES[index];
    _updateTheme(newTheme);
  }

  void _updateTheme(String theme) {
    _prefs.editorTheme.value = theme;
    _aceEditor.theme = new ace.Theme.named(theme);
    if (_label != null) {
      _label.text = utils.toTitleCase(theme.replaceAll('_', ' '));
    }
  }
}

class KeyBindingManager {
  AceManager aceManager;
  SparkPreferences prefs;
  html.Element _label;

  KeyBindingManager(this.aceManager, this.prefs, this._label) {
    String value = prefs.keyBindings.value;
    if (value != null) {
      aceManager.setKeyBinding(value);
    }
    _updateName(value);
  }

  void inc(html.Event e) {
    e.stopPropagation();
    _changeBinding(1);
  }

  void dec(html.Event e) {
    e.stopPropagation();
    _changeBinding(-1);
  }

  void _changeBinding(int direction) {
    aceManager.getKeyBinding().then((String name) {
      int index = math.max(AceManager.KEY_BINDINGS.indexOf(name), 0);
      index = (index + direction) % AceManager.KEY_BINDINGS.length;
      String newBinding = AceManager.KEY_BINDINGS[index];
      prefs.keyBindings.value = newBinding;
      _updateName(newBinding);
      aceManager.setKeyBinding(newBinding);
    });
  }

  void _updateName(String name) {
    _label.text = (name == null ? 'Default' : utils.capitalize(name));
  }
}

class AceFontManager {
  AceManager aceManager;
  SparkPreferences prefs;
  html.Element _label;
  num _value;

  AceFontManager(this.aceManager, this.prefs, this._label) {
    _value = aceManager.getFontSize();
    _updateLabel(_value);

    try {
      _value = prefs.editorFontSize.value;
      aceManager.setFontSize(_value);
      _updateLabel(_value);
    } catch (e) {

    }
  }

  void dec() => _adjustSize(_value - 1);

  void inc() => _adjustSize(_value + 1);

  void _adjustSize(num newValue) {
    // Clamp to between 6pt and 36pt.
    _value = newValue.clamp(6, 36);
    aceManager.setFontSize(_value);
    _updateLabel(_value);
    prefs.editorFontSize.value = _value;
  }

  void _updateLabel(num size) {
    _label.text = '${size}pt';
  }
}

abstract class AceManagerDelegate {
  /**
   * Mark the files with the given file extension as editable in text format.
   */
  void setShowFileAsText(String extension, bool enabled);

  /**
   * Returns true if the file with the given filename can be edited as text.
   */
  bool canShowFileAsText(String filename);

  void openEditor(workspace.File file, {Span selection});
}

// String _calcMD5(String text) {
//   crypto.MD5 md5 = new crypto.MD5();
//   md5.add(text.codeUnits);
//   return crypto.CryptoUtils.bytesToHex(md5.close());
// }

/**
 * Given some arbitrary text and an offset into it, attempt to return the
 * parts of the offset surrounded by quotes.
 */
String _getQuotedString(String text, int offset) {
  if (text.isEmpty) return null;

  offset = offset.clamp(0, math.max(0, text.length - 1));
  int leftSide = offset;

  while (leftSide >= 0) {
    String c = text[leftSide];
    if (c == '\n' || leftSide == 0) return null;
    if (c == "'" || c == '"') break;
    leftSide--;
  }

  leftSide++;
  int rightSide = offset;

  while ((rightSide + 1) < text.length) {
    String c = text[rightSide];
    if (c == "'" || c == '"') {
      rightSide--;
      break;
    }
    if (c == '\n') break;
    rightSide++;
  }

  return text.substring(leftSide, rightSide + 1);
}
