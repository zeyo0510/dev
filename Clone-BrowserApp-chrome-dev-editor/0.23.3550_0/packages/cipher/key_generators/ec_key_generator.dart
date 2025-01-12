// Copyright (c) 2013-present, Iván Zaera Avellón - izaera@gmail.com

// This library is dually licensed under LGPL 3 and MPL 2.0. See file LICENSE for more information.

// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of
// the MPL was not distributed with this file, you can obtain one at http://mozilla.org/MPL/2.0/.

library cipher.key_generators.ec_key_generator;

import "package:bignum/bignum.dart";

import "package:cipher/api.dart";
import "package:cipher/api/ecc.dart";
import "package:cipher/params/parameters_with_random.dart";
import "package:cipher/params/key_generators/ec_key_generator_parameters.dart";

class ECKeyGenerator implements KeyGenerator {

  ECDomainParameters _params;
  SecureRandom _random;

  String get algorithmName => "EC";

  void init(CipherParameters params) {
    ECKeyGeneratorParameters ecparams;

    if( params is ParametersWithRandom ) {
      _random = params.random;
      ecparams = params.parameters;
    } else {
      _random = new SecureRandom();
      ecparams = params;
    }

    _params = ecparams.domainParameters;
  }

  AsymmetricKeyPair generateKeyPair() {
    var n = _params.n;
    var nBitLength = n.bitLength();
    var d;

    do {
      d = _random.nextBigInteger(nBitLength);
    } while( d==BigInteger.ZERO || (d>=n) );

    var Q = _params.G*d;

    return new AsymmetricKeyPair(
      new ECPublicKey(Q, _params),
      new ECPrivateKey(d, _params)
    );
  }

}
