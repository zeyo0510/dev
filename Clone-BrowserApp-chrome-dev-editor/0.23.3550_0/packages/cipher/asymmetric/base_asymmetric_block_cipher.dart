// Copyright (c) 2013-present, Iván Zaera Avellón - izaera@gmail.com

// This library is dually licensed under LGPL 3 and MPL 2.0. See file LICENSE for more information.

// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of
// the MPL was not distributed with this file, you can obtain one at http://mozilla.org/MPL/2.0/.

library cipher.asymmetric.base_asymmetric_block_cipher;

import "dart:typed_data";

import "package:cipher/api.dart";

/// Base implementation of [AsymmetricBlockCipher] which provides shared methods.
abstract class BaseAsymmetricBlockCipher implements AsymmetricBlockCipher {

  Uint8List process(Uint8List data) {
    var out = new Uint8List(outputBlockSize);
    var len = processBlock(data, 0, data.length, out, 0);
    return out.sublist(0, len);
  }

}