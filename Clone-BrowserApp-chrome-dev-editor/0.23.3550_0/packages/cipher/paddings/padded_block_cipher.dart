// Copyright (c) 2013-present, Iván Zaera Avellón - izaera@gmail.com

// This library is dually licensed under LGPL 3 and MPL 2.0. See file LICENSE for more information.

// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of
// the MPL was not distributed with this file, you can obtain one at http://mozilla.org/MPL/2.0/.

library cipher.paddings.padded_block_cipher;

import "dart:typed_data";

import "package:cipher/api.dart";
import "package:cipher/params/padded_block_cipher_parameters.dart";

/// The standard implementation of [PaddedBlockCipher].
class PaddedBlockCipherImpl implements PaddedBlockCipher {

  final Padding padding;
  final BlockCipher cipher;

  bool _encrypting;

  PaddedBlockCipherImpl(this.padding,this.cipher);

  String get algorithmName => cipher.algorithmName+"/"+padding.algorithmName;

  int get blockSize => cipher.blockSize;

  void reset() {
    _encrypting = null;
    cipher.reset();
  }

  void init( bool forEncryption, PaddedBlockCipherParameters params ) {
    _encrypting = forEncryption;
    cipher.init( forEncryption, params.underlyingCipherParameters );
    padding.init( params.paddingCipherParameters );
  }

  Uint8List process(Uint8List data) {
    var blocks = (data.length ~/ blockSize) + 1;

    var out = new Uint8List(blocks * blockSize);
    for (var i = 0; i < (blocks - 1); i++) {
      var offset = (i * blockSize);
      processBlock(data, offset, out, offset);
    }

    var remainder = (data.length % blockSize);
    if (remainder == 0) {
      remainder = blockSize;
    }
    var offset = ((blocks - 1) * blockSize);
    doFinal(data, offset, out, offset);

    return out;
  }

  int processBlock(Uint8List inp, int inpOff, Uint8List out, int outOff) {
    return cipher.processBlock(inp, inpOff, out, outOff);
  }

  int doFinal(Uint8List inp, int inpOff, Uint8List out, int outOff) {
    if( _encrypting ) {
      Uint8List tmp = new Uint8List(blockSize)
        ..setAll( 0, inp.sublist(inpOff) );
      var padCount = inp.length-inpOff;
      padding.addPadding( tmp, padCount );
      var processed = processBlock(tmp, 0, out, outOff);
      return processed - padCount;
    } else {
      var processed = processBlock(inp, inpOff, out, outOff);
      var padCount = padding.padCount(out.sublist(outOff));
      return processed - padCount;
    }
  }

}