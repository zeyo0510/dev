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

/* eslint consistent-return: "off" */
/* eslint no-empty: ["error", { "allowEmptyCatch": true }] */
/* eslint import/prefer-default-export: "off" */

// https://github.com/auth0/angular-jwt/blob/master/src/angularJwt/services/jwt.js
function urlBase64Decode (str) {
  let output = str.replace(/-/g, '+')
    .replace(/_/g, '/');
  switch (output.length % 4) {
    case 0:
      break;
    case 2:
      output += '==';
      break;
    case 3:
      output += '=';
      break;
    default:
      return;
  }

  try {
    // If the token is completely wrong this can throws so let's be failsafe
    return window.decodeURIComponent(window.escape(window.atob(output)));
  } catch (e) {
    console && console.debug(e);
  }
}

export const decodeJWTStringToJWTObject = (tokenToDecode) => {
  if (!tokenToDecode || tokenToDecode === 'undefined' || tokenToDecode === 'null') {
    return;
  }

  const parts = tokenToDecode.split('.');

  if (parts.length !== 3) {
    return;
  }

  const decodedToken = urlBase64Decode(parts[ 1 ]);
  return decodedToken ? JSON.parse(decodedToken) : undefined;
};

window.APP.jwtUtils = {
  decodeJWTStringToJWTObject,
};
