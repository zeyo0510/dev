// Copyright (c) 2013, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

/**
 * This library contains [BuilderManager] and the abstract [Builder] class.
 * These classes are used to batch process resource change events.
 */
library spark.builder;

import 'dart:async';
import 'dart:core' hide Resource;

import 'package:intl/intl.dart';
import 'package:logging/logging.dart';

import 'jobs.dart';
import 'package_mgmt/package_utils.dart';
import 'spark_flags.dart';
import 'workspace.dart';

final Logger _logger = new Logger('spark.builder');
final NumberFormat _nf = new NumberFormat.decimalPattern();

/**
 * A [BuilderManager] listens for changes to a [Workspace], batches up those
 * changes, and feeds them into [Builder]s to be processed. A build - a
 * sequential execution of [Builder]s - can be a long running process. The build
 * is run in a [Job] in order to give good indication of progress to the user.
 */
class BuilderManager {
  final Workspace workspace;
  final JobManager jobManager;

  final List<Builder> builders = [];

  final List<ResourceChangeEvent> _events = [];

  Timer _timer;
  bool _buildRunning = false;

  BuilderManager(this.workspace, this.jobManager) {
    workspace.onResourceChange.listen(_handleChange);
  }

  bool get isRunning => _buildRunning;

  final List<Completer> _completers = [];

  /**
   * Returns a [Future] that will complete when all current builds are finished.
   */
  Future waitForAllBuilds() {
    if (_events.isNotEmpty || isRunning) {
      Completer completer = new Completer();
      _completers.add(completer);
      return completer.future;
    } else {
      return new Future.value();
    }
  }

  void _handleChange(ResourceChangeEvent event) {
    _events.add(event);

    if (!_buildRunning) {
      _startTimer();
    }
  }

  void _startTimer() {
    if (_timer != null) {
      _timer.cancel();
    }

    // Bundle up changes for ~50ms.
    _timer = new Timer(new Duration(milliseconds: 50), _runBuild);
  }

  void _runBuild() {
    ResourceChangeEvent event = _combineEvents(_events);
    _events.clear();

    if (event.isEmpty) return;

    if (SparkFlags.developerMode) {
      String changeStr = '${event.changes}';
      if (changeStr.length > 200) {
        changeStr = changeStr.substring(0, 200) + "...";
      }
      _logger.info('starting build for ${changeStr}');
    } else {
      _logger.info('starting build');
    }

    Stopwatch timer = new Stopwatch()..start();

    _buildRunning = true;

    Completer completer = new Completer();
    _BuildJob job = new _BuildJob(event, builders.toList(), completer);
    jobManager.schedule(job);

    completer.future.then((_) {
      _logger.info('build finished in ${_nf.format(timer.elapsedMilliseconds)}ms');

      _buildRunning = false;

      if (_events.isNotEmpty) {
        _startTimer();
      } else {
        _completers.forEach((c) => c.complete());
        _completers.clear();
      }
    });
  }
}

/**
 * An abstract class that is given batched up resources changes to process.
 * Builders can be long running, and are executed in [Job]s.
 *
 * See also [BuilderManager].
 */
abstract class Builder {
  /**
   * Process a set of resource changes and complete the [Future] when finished.
   */
  Future build(ResourceChangeEvent event, ProgressMonitor monitor);

  /**
   * Return only those changes that did not occur in a packages directory.
   */
  Iterable<ChangeDelta> filterPackageChanges(Iterable<ChangeDelta> changes) {
    return changes.where(
        (ChangeDelta delta) => !isInPackagesFolder(delta.resource));
  }
}

class _BuildJob extends Job {
  final ResourceChangeEvent event;
  final List<Builder> builders;

  _BuildJob(this.event, this.builders, Completer completer)
      : super('Building…', completer);

  Future<SparkJobStatus> run(ProgressMonitor monitor) {
    return Future.forEach(builders, (Builder builder) {
      Future f = builder.build(event, monitor);
      assert(f != null);
      assert(f is Future);
      return f;
    }).catchError((e, st) {
      _logger.severe('Exception from build manager', e, st);
    }).whenComplete(() {
      done(new SparkJobStatus(
          code: SparkStatusCodes.SPARK_JOB_BUILD_SUCCESS));
    });
  }
}

// TODO: combine events better -

ResourceChangeEvent _combineEvents(List<ResourceChangeEvent> events) {
  List<ChangeDelta> deltas = [];
  events.forEach((e) => deltas.addAll(e.changes.where((ChangeDelta change) {
    Resource resource = change.resource;
    return resource.project != null && !resource.isDerived();
  })));
  return new ResourceChangeEvent.fromList(deltas, filterRename: true);
}
