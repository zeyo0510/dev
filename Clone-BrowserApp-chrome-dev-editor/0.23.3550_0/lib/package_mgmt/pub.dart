// Copyright (c) 2014, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

/**
 * Pub services.
 */
library spark.package_mgmt.pub;

import 'dart:async';
import 'dart:core' hide Resource;

import 'package:logging/logging.dart';
import 'package:tavern/tavern.dart' as tavern;
import 'package:yaml/yaml.dart' as yaml;

import 'package_manager.dart';
import '../decorators.dart';
import '../exception.dart';
import '../jobs.dart';
import '../platform_info.dart';
import '../workspace.dart';

Logger _logger = new Logger('spark.pub');

// TODO(ussuri): Make package-private once no longer used outside.
final PubProperties pubProperties = new PubProperties();

class PubProperties extends PackageServiceProperties {
  final List _managedFolders = const [
      'benchmark', 'bin', 'example', 'packages', 'test', 'tool', 'web'
  ];

  PubProperties();
  //
  // PackageServiceProperties virtual interface:
  //

  String get packageServiceName => 'pub';
  String get configFileName => null;
  String get packageSpecFileName => 'pubspec.yaml';
  String getPackagesDirName(Resource resource) => 'packages';
  String get libDirName => 'lib';
  String get packageRefPrefix => 'package:';
  // This will get both the "package:foo/bar.dart" variant when used directly
  // in Dart and the "baz/packages/foo/bar.dart" variant when served over HTTP.
  RegExp get packageRefPrefixRegexp =>
    new RegExp(r'^(package:|.*/packages/|packages/)(.*)$');

  void setSelfReference(Project project, String selfReference) =>
    project.setMetadata('${packageServiceName}SelfReference', selfReference);

  String getSelfReference(Project project) =>
    project.getMetadata('${packageServiceName}SelfReference');

  bool isManagedFolder(String name) => _managedFolders.contains(name);
}

File findPubspec(Container container) {
  while (container.parent != null && container is! Workspace) {
    Resource child = container.getChild(pubProperties.packageSpecFileName);
    if (child != null) {
      return child;
    }
    container = container.parent;
  }
  return null;
}

class PubManager extends PackageManager {
  final StreamController<Project> _controller = new StreamController.broadcast();

  /**
   * Create a new [PubManager] instance. This is a heavy-weight object; it
   * creates a new [Builder].
   */
  PubManager(Workspace workspace) : super(workspace);

  PackageServiceProperties get properties => pubProperties;

  void setSelfReference(Project project, String selfReference) {
    properties.setSelfReference(project, selfReference);
    _controller.add(project);
  }

  Stream<Project> get onSelfReferenceChange => _controller.stream;

  PackageBuilder getBuilderFor(Workspace workspace) => new _PubBuilder(this);

  PackageResolver getResolverFor(Project project) =>
      new _PubResolver._(this, project);

  bool canRunPub(Folder project) => pubProperties.isFolderWithPackages(project);

  Future installPackages(Folder container, ProgressMonitor monitor) =>
      _installUpgradePackages(container, 'get', false, monitor);

  Future upgradePackages(Folder container, ProgressMonitor monitor) =>
      _installUpgradePackages(container, 'upgrade', true, monitor);

  Future<dynamic> arePackagesInstalled(Folder container) {
    File pubspecFile = findPubspec(container);
    if (pubspecFile is File) {
      container = pubspecFile.parent;
      return pubspecFile.getContents().then((String str) {
        try {
          _PubSpecInfo info = new _PubSpecInfo.parse(str);
          for (String dep in info.getDependencies()) {
            Resource dependency = container.getChildPath(
                '${properties.getPackagesDirName(container)}/${dep}');
            if (dependency is! Folder) {
              return dep;
            }
          }
        } on Exception catch (e) {
          _logger.info('Error parsing pubspec file', e);
        }
      });
    }
    return new Future.value();
  }

  Future _installUpgradePackages(
      Folder container,
      String commandName,
      bool isUpgrade,
      ProgressMonitor monitor) {
    // Don't run pub on Windows (#2743).
    if (PlatformInfo.isWin) {
      throw new SparkException(
          SparkErrorMessages.PUB_ON_WINDOWS_MSG,
          errorCode: SparkErrorConstants.PUB_ON_WINDOWS_NOT_SUPPORTED);
    }

    // Fake the total amount of work, since we don't know it. When an update
    // comes from Tavern, just refresh the generic message w/o showing progress.
    monitor.start(
        "Getting Pub packages…", maxWork: 0, format: ProgressFormat.NONE);

    void handleLog(String line, String level) {
      _logger.info(line.trim());
      // Also fake the amount of work done for the same reason.
      monitor.worked(1);
    }

    return tavern.getDependencies(container.entry, handleLog, isUpgrade).
        whenComplete(() {
      return container.refresh();
    }).catchError((e, st) {
      _logger.severe('Error running Pub $commandName', e, st);
      if (isSymlinkError(e)) {
        return new Future.error(new SparkException(
          SparkErrorMessages.SYMLINKS_ERROR_MSG,
          errorCode: SparkErrorConstants.SYMLINKS_OPERATION_NOT_SUPPORTED), st);
      }
      return new Future.error(e, st);
    });
  }
}

/**
 * A decorator to add text decorations to the `pubspec.yaml` file.
 */
class PubDecorator extends Decorator {
  final PubManager _manager;
  final StreamController _controller = new StreamController.broadcast();

  PubDecorator(this._manager) {
    _manager.onSelfReferenceChange.listen((_) => _controller.add(null));
  }

  bool canDecorate(Object object) {
    if (object is! Resource) return false;

    Resource r = object;
    return r.isFile && r.name == _manager.properties.packageSpecFileName;
  }

  String getTextDecoration(Object object) {
    Resource resource = object;
    Project project = resource.project;
    if (project == null) return null;
    String name = _manager.properties.getSelfReference(project);
    return name == null ? null : ' - ${name}';
  }

  Stream get onChanged => _controller.stream;
}

/**
 * A class to help resolve pub `package:` references.
 */
class _PubResolver extends PackageResolver {
  final PubManager manager;
  final Project project;

  _PubResolver._(this.manager, this.project) {
    // We calculate the pubspec.yaml self-reference name as each project is
    // initially touched / opened. We do this as a workaround for the workspace
    // meta-data not persisting (#1578).
    _calcSelfReference();
  }

  PackageServiceProperties get properties => pubProperties;

  /**
   * Resolve a `package:` reference to a file in this project. This will
   * correctly handle self-references, and resolve them to the `lib/` directory.
   * Other references will resolve to the `packages/` directory. If a reference
   * does not resolve to an existing file, this method will return `null`.
   */
  File resolveRefToFile(String url) {
    Match match = properties.packageRefPrefixRegexp.matchAsPrefix(url);
    if (match == null) return null;

    String ref = match.group(2);
    String selfRefName = properties.getSelfReference(project);
    Folder packageDir = project.getChild(properties.getPackagesDirName(project));

    if (selfRefName != null && ref.startsWith(selfRefName + '/')) {
      // `foo/bar.dart` becomes `bar.dart` in the lib/ directory.
      ref = ref.substring(selfRefName.length + 1);
      packageDir = project.getChild(properties.libDirName);
    }

    if (packageDir == null) return null;

    Resource resource = packageDir.getChildPath(ref);
    return resource is File ? resource : null;
  }

  /**
   * Given a [File], return the best pub `package:` reference for it. This will
   * correctly return package self-references for files in the `lib/` folder. If
   * there is no valid `package:` reference to the file, then this methods will
   * return `null`.
   */
  String getReferenceFor(File file) {
    if (file.project != project) return null;

    List resources = [];
    resources.add(file);

    Container parent = file.parent;
    while (parent is! Project) {
      resources.insert(0, parent);
      parent = parent.parent;
    }

    if (resources[0].name == properties.getPackagesDirName(file)) {
      resources.removeAt(0);
      return properties.packageRefPrefix + resources.map((r) => r.name).join('/');
    } else if (resources[0].name == properties.libDirName) {
      String selfRefName = properties.getSelfReference(project);

      if (selfRefName != null) {
        resources.removeAt(0);
        return 'package:${selfRefName}/' +
               resources.map((r) => r.name).join('/');
      } else {
        return null;
      }
    } else {
      return null;
    }
  }

  Future _calcSelfReference() {
    Resource file = project.getChild(properties.packageSpecFileName);

    if (file is! File) return new Future.value();

    return (file as File).getContents().then((String str) {
      try {
        _PubSpecInfo info = new _PubSpecInfo.parse(str);
        manager.setSelfReference(file.project, info.name);
      } catch (e) { }
    });
  }

  String toString() => 'Pub resolver for ${project}';
}

/**
 * A [Builder] implementation which watches for changes to `pubspec.yaml` files
 * and updates the project pub metadata. Specifically, it parses and stores
 * information about the project's self-reference name, for later use in
 * resolving `package:` references.
 */
class _PubBuilder extends PackageBuilder {
  final PubManager _pubManager;

  _PubBuilder(this._pubManager);

  PackageServiceProperties get properties => pubProperties;

  Future build(ResourceChangeEvent event, ProgressMonitor monitor) {
    Project project = event.changes.first.resource.project;

    // If we're building a top-level file, return.
    if (project == null) return new Future.value();

    File pubspecFile = project.getChild(properties.packageSpecFileName);

    if (pubspecFile is! File) {
      if (properties.getSelfReference(project) != null) {
        _pubManager.setSelfReference(project, null);
      }
    } else {
      bool analyzePubspec = false;

      for (ChangeDelta delta in event.changes) {
        Resource r = delta.resource;

        if (r == pubspecFile) {
          if (delta.isDelete) {
            analyzePubspec = false;
            break;
          } else {
            analyzePubspec = true;
          }
        } else if (properties.isInPackagesFolder(r)) {
          analyzePubspec = true;
        }
      }

      if (analyzePubspec) {
        return _analyzePubspec(pubspecFile);
      }
    }

    return new Future.value();
  }

  Future _analyzePubspec(File file) {
    file.clearMarkers(properties.packageServiceName);

    return file.getContents().then((String str) {
      final String packageServiceName = properties.packageServiceName;
      final String packagesDirName = properties.getPackagesDirName(file);
      try {
        _PubSpecInfo info = new _PubSpecInfo.parse(str);
        _pubManager.setSelfReference(file.project, info.name);
        for (String dep in info.getDependencies()) {
          Resource dependency =
              file.project.getChildPath('${packagesDirName}/${dep}');
          if (dependency is! Folder) {
            // TODO(devoncarew): We should place these markers on the correct line.
            file.createMarker(packageServiceName,
                Marker.SEVERITY_WARNING,
                "'${dep}' does not exist in the packages directory. "
                "Do you need to run 'pub get'?",
                1);
          }
        }
      } on Exception catch (e) {
        file.createMarker(packageServiceName, Marker.SEVERITY_ERROR, '${e}', 1);
      }
    });
  }
}

class _PubSpecInfo {
  Map _map;

  /**
   * This method can throw exceptions on parse errors.
   */
  _PubSpecInfo.parse(String str) {
    _map = yaml.loadYaml(str);
  }

  String get name => _map['name'];

  List<String> getDependencies() => _getDeps('dependencies');

  List<String> getDevDependencies() => _getDeps('dev_dependencies');

  List<String> _getDeps(String name) {
    var deps = _map[name];
    if (deps is Map) {
      return _map.containsKey(name) ? _map[name].keys.toList() : [];
    } else {
      return [];
    }
  }
}
