// Copyright (c) 2014, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

library spark_widgets.tree_view;

import 'package:polymer/polymer.dart';

import 'spark_tree_view_model.dart';
import 'src/spark_tree_view_node.dart';

@CustomTag('spark-tree-view')
class SparkTreeView extends SparkTreeViewNode {
  @published SparkTreeViewModel model;

  /// Constructor.
  SparkTreeView.created() : super.created();

  @override
  void attached() {
    super.attached();
  }
}
