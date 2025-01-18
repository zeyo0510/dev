// Copyright (c) 2013-present, Iván Zaera Avellón - izaera@gmail.com

// This library is dually licensed under LGPL 3 and MPL 2.0. See file LICENSE for more information.

// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of
// the MPL was not distributed with this file, you can obtain one at http://mozilla.org/MPL/2.0/.

library cipher.params.parameters_with_random;

import "package:cipher/api.dart";

class ParametersWithRandom<UnderlyingParameters extends CipherParameters> implements CipherParameters {

  final UnderlyingParameters parameters;
  final SecureRandom random;

  ParametersWithRandom(this.parameters,this.random);

}
