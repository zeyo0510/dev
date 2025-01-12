// Copyright (c) 2014, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

library stagehand.webapp;

import '../stagehand.dart';
import '../src/common.dart';
import 'webapp_data.dart';

/**
 * A generator for a minimal web application.
 */
class WebAppGenerator extends DefaultGenerator {
  WebAppGenerator() : super(
      'webapp',
      'Web Application',
      "A mobile-friendly web app with routing, responsive CSS, and "
      "(optional) Sass support.",
      categories: const ['dart', 'web']) {

    for (TemplateFile file in decodeConcanenatedData(data)) {
      addTemplateFile(file);
    }

    setEntrypoint(getFile('web/index.html'));
  }

  String getInstallInstructions() =>
      "${super.getInstallInstructions()}\n"
      "sass is required if you want to modify the sass styles (sass-lang.com/install)\n"
      "to run your app, use 'pub serve'\n";
}
