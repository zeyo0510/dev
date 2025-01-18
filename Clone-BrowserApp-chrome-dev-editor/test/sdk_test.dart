// Copyright (c) 2013, Google Inc. Please see the AUTHORS file for details.
// All rights reserved. Use of this source code is governed by a BSD-style
// license that can be found in the LICENSE file.

library spark.sdk_test;

import 'package:unittest/unittest.dart';

import '../lib/dart/sdk.dart';
import '../lib/dart/sdk_app.dart';

DartSdk sdk;

defineTests() {
  group('sdk', () {
    setUp(() {
      if (sdk == null) {
        return createSdk().then((result) {
          sdk = result;
        });
      }
    });

    test('exists', () {
      expect(sdk.available, true);
    });

    test('has version', () {
      expect(sdk.version.length, greaterThan(0));
    });

    test('sdk parent is null', () {
      expect(sdk.parent, isNull);
    });

    test('list directory entries', () {
      expect(sdk.getChildren().length, 1);
    });

    test('lib directory parent is sdk', () {
      expect(sdk.libDirectory.parent, sdk);
    });

    test('list lib directory entries', () {
      expect(sdk.libDirectory.getChildren().length, greaterThan(10));
    });
  });
}
