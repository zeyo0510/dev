/*
 * Copyright (C) 2010-2022 Talend Inc. - www.talend.com
 *
 * This source code is available under agreement available at
 * https://www.talend.com/legal-terms/us-eula
 *
 * You should have received a copy of the agreement
 * along with this program; if not, write to Talend SA
 * 5 rue Salomon de Rothschild - 92150 Suresnes
 *
 */

import _ from 'lodash';

const { crypto } = window;

const CRYPTO_ALPHABET = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.-~'; // DON'T ADD CHARACTERS THAT GET URL-ENCODED IN THE ALPHABET. WOULD BREAK REGEXES
const CRYPTO_ALPHABET_SIZE = _.size(CRYPTO_ALPHABET);

const generateCryptoSecureRandomString = (size) => {
  const array = crypto.getRandomValues(new Uint32Array(size));
  return _(array)
    .map((number) => CRYPTO_ALPHABET.charAt(number % CRYPTO_ALPHABET_SIZE))
    .join('');
};

const extractHashParameters = (fragment) => (_(fragment.substring(1).split('&'))
  .map((parameter) => [ parameter.split('=')[ 0 ], parameter.split('=')[ 1 ] ])
  .fromPairs()
  .value());

const logout = (url) => {
  const options = {
    method: 'GET',
    credentials: 'include',
  };

  return fetch(url, options)
    .then((response) => {
      if (response.ok) {
        return;
      }

      throw Error('Logout failed');
    });
};

window.APP.authService = {
  generateCryptoSecureRandomString,
  extractHashParameters,
  logout,
};
