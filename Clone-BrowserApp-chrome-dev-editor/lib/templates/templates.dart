// Copyright (c) 2014, the Dart project authors.  Please see the AUTHORS file
// for details. All rights reserved. Use of this source code is governed by a
// BSD-style license that can be found in the LICENSE file.

library spark.templates;

import 'dart:async';
import 'dart:convert' show JSON;
import 'dart:core' hide Resource;
import 'dart:html' hide File;

import 'package:chrome/chrome_app.dart' as chrome;
import 'package:stagehand/stagehand.dart' as stagehand;

import '../package_mgmt/bower_properties.dart';
import '../utils.dart' as utils;
import '../workspace.dart';

part 'chrome/chrome_app_polymer_js/template.dart';
part 'polymer/template.dart';
part 'polymer/polymer_element_dart/template.dart';
part 'polymer/polymer_element_js/template.dart';
part 'polymer/spark_widget/template.dart';
part 'stagehand/template.dart';

/**
 * Specifies a variable-to-value substitution in a template file text.
 */
class TemplateVar {
  final String name;
  String value;

  TemplateVar(this.name, this.value);

  String interpolate(String text) => text.replaceAll('``$name``', value);
}

/**
 * A class to create a sample project given a project name and a list of
 * template IDs to use as building blocks.
 *
 * Directories corresponding to the template IDs will be copied on top of
 * one another in random order; conflicts will not be detected nor resolved.
 */
class ProjectBuilder {
  DirectoryEntry _destRoot;
  List<ProjectTemplate> _templates = [];
  // NOTE: Currently unused, but may be useful in future.
  utils.Notifier _notifier;

  ProjectBuilder(this._destRoot, this._templates, this._notifier);

  /**
   * Build the sample project and complete the Future when finished.
   */
  Future build() {
    return Future.forEach(_templates, (ProjectTemplate template) {
      return template.instantiate(_destRoot);
    });
  }

  /**
   * Return the 'main' file for the given project. This is generally the first
   * file we should show to the user after a project is created.
   */
  static Resource getMainResourceFor(Project project) {
    // Look for `manifest.json` or `index.html`.
    Function fileMatch = (Iterable<Resource> resources) {
      return resources.firstWhere((r) => r.name == 'manifest.json',
          orElse: () => resources.firstWhere((r) => r.name == 'index.html',
          orElse: () => null));
    };

    Folder web = project.getChild('web');
    if (web != null) {
      Resource match = fileMatch(web.getChildren());
      if (match != null) return match;
    }

    Folder app = project.getChild('app');
    if (app != null) {
      Resource match = fileMatch(app.getChildren());
      if (match != null) return match;
    }

    Resource match = fileMatch(project.getChildren());
    if (match != null) return match;

    File file = project.getChild('pubspec.yaml');
    if (file != null) return file;

    return project;
  }
}

/**
 * A project template that knows how to instantiate itself within a given
 * destination directory.
 *
 * Instantiation consists of copying over files and directories listed in
 * the template's setup.json, with renaming the targets as specified.
 *
 * In addition, each source file's contents is pre-interpolated using the
 * provided global and local template variables.
 */
abstract class ProjectTemplate {
  factory ProjectTemplate(
      String id,
      [List<TemplateVar> globalVars = const [],
       List<TemplateVar> localVars = const []]) {
    const String stagehandPrefix = "stagehand/";

    if (id.startsWith(stagehandPrefix)) {
      return new StagehandProjectTemplate(
          id.substring(stagehandPrefix.length), globalVars, localVars);
    }

    switch (id) {
      case 'chrome/chrome_app_polymer_js':
        return new ChromeAppWithPolymerJSTemplate(id, globalVars, localVars);
      case 'polymer/polymer_element_js':
        return new PolymerJSTemplate(id, globalVars, localVars);
      case 'polymer/polymer_element_dart':
        return new PolymerDartTemplate(id, globalVars, localVars);
      case 'polymer/spark_widget':
        return new SparkWidgetTemplate(id, globalVars, localVars);
      default:
        return new SparkProjectTemplate._(id, globalVars, localVars);
    }
  }

  Future instantiate(DirectoryEntry destRoot);

  Future showIntro(Project finalProject, utils.Notifier notifier);
}

class SparkProjectTemplate implements ProjectTemplate {
  String _sourceUri;
  Map<String, TemplateVar> _vars = {};

  /**
   * Subclasses can add or redefine any of the variables in the resulting _vars.
   */
  SparkProjectTemplate._(
      String id,
      List<TemplateVar> globalVars,
      List<TemplateVar> localVars) {
    _sourceUri = 'lib/templates/$id';
    _addOrReplaceVars(globalVars);
    _addOrReplaceVars(localVars);
    _addOrReplaceVars([
      // For copyrights etc.
      new TemplateVar('year', new DateTime.now().year.toString())
    ]);
  }

  Future instantiate(DirectoryEntry destRoot) {
    DirectoryEntry sourceRoot;

    return utils.getPackageDirectoryEntry().then((root) {
      return root.getDirectory(_sourceUri);
    }).then((dir) {
      sourceRoot = dir;
      return utils.getAppContents("$_sourceUri/setup.json");
    }).then((String contents) {
      contents = _interpolateTemplateVars(contents);
      final Map m = JSON.decode(contents);
      return _traverseElement(destRoot, sourceRoot, _sourceUri, m);
    });
  }

  Future showIntro(Project finalProject, utils.Notifier notifier) {
    return new Future.value();
  }

  void _addOrReplaceVars(List<TemplateVar> vars) {
    for (var v in vars) {
      _vars[v.name] = v;
    }
  }

  String _interpolateTemplateVars(String text) {
    _vars.values.forEach((v) {
      text = v.interpolate(text);
    });
    return text;
  }

  Future _traverseElement(
      DirectoryEntry destRoot,
      DirectoryEntry sourceRoot,
      String sourceUri,
      Map<String, dynamic> element) {
    return _handleDirectories(destRoot, sourceRoot, sourceUri,
        element['directories']).then((_) =>
            _handleFiles(destRoot, sourceRoot, sourceUri, element['files']));
  }

  Future _handleDirectories(
      DirectoryEntry destRoot,
      DirectoryEntry sourceRoot,
      String sourceUri,
      Map<String, dynamic> directories) {
    if (directories == null || directories.isEmpty) return new Future.value();

    return Future.forEach(directories.keys, (String directoryName) {
      DirectoryEntry destDirectoryRoot;
      return destRoot.createDirectory(directoryName).then((DirectoryEntry entry) {
        destDirectoryRoot = entry;
        return sourceRoot.getDirectory(directoryName);
      }).then((DirectoryEntry sourceDirectoryRoot) {
        return _traverseElement(destDirectoryRoot, sourceDirectoryRoot,
            "$sourceUri/$directoryName", directories[directoryName]);
      });
    });
  }

  Future _handleFiles(
      DirectoryEntry destRoot,
      DirectoryEntry sourceRoot,
      String sourceUri,
      List<Map<String, String>> files) {
    if (files == null || files.isEmpty) return new Future.value();

    return Future.forEach(files, (fileElement) {
      String source = fileElement['source'];
      String dest = _interpolateTemplateVars(fileElement['dest']);
      chrome.ChromeFileEntry fileEntry;

      return destRoot.createFile(dest).then((chrome.ChromeFileEntry entry) {
        fileEntry = entry;
        if (dest.endsWith(".png")) {
          return utils.getAppContentsBinary("$sourceUri/$source").then(
              (List<int> data) {
            return fileEntry.writeBytes(new chrome.ArrayBuffer.fromBytes(data));
          });
        } else {
          return utils.getAppContents("$sourceUri/$source").then((String data) {
            return fileEntry.writeText(_interpolateTemplateVars(data));
          });
        }
      });
    });
  }
}
