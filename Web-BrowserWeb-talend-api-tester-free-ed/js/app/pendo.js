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

import merge from 'lodash/merge';

/* eslint-disable */
//@formatter:off
// directly inspired from https://github.com/pendo-io/pendo-on-chrome-extension
// /!\ especially two things have been done:
//     + fixed their implementation of "appendChild" and "insertBefore" methods (cf below)
//     + compute the right visitor/account data used when initializing pendo (search "userData" below)

const pendoAgentScriptsBaseUrl = chrome.runtime.getURL(`remote-scripts/pendo/agent`);

const initializePendo = (apiKey, userData) => { // [Talend] replaced the visitor/account parameters with the right json "userData" parameter
  let manifestVersion = chrome.runtime.getManifest().manifest_version;

  // URL Redirects for manifest V3 to load local resources
  if (manifestVersion === 3) {
    let urlRedirects = {
      // In App Designer
      "https://app.pendo.io/in-app-designer/latest/plugin.js": `${pendoAgentScriptsBaseUrl}/plugin.js`,
      "https://app.pendo.io/in-app-designer/latest/preloader.js": `${pendoAgentScriptsBaseUrl}/preloader.js`,
      // Preview Mode
      "https://cdn.pendo.io/agent/releases/2.143.1/pendo.preview.min.js": `${pendoAgentScriptsBaseUrl}/pendo.preview.min.js`,
    };

    // [Talend] Take care, the original implementations do not respect the contract of appendChild and insertBefore methods.
    // [Talend] They should return the inserted/added node which was not done originally.
    // [Talend] Ticket created https://github.com/pendo-io/pendo-on-chrome-extension/issues/1
    // Overwrite appendChild method
    (function (appendChild) {
      Element.prototype.appendChild = newAppend;

      function newAppend() {
        // Check for appended iframe so appendChild can be overwritten there as well
        // Required for preview mode which is called in new iframe
        if (arguments[0]?.tagName === "IFRAME") {
          let pendoCallback = arguments[0].onload;
          arguments[0].onload = function () {
            if (this.contentDocument) {
              this.contentDocument.body.appendChild = newAppend;
              pendoCallback();
            }
          };
        }

        // Apply url directs if applicable
        if (urlRedirects[arguments[0].src]) {
          console.log(
            `Redirecting ${arguments[0].src} to ${
              urlRedirects[arguments[0].src]
            }`
          );
          arguments[0].src = urlRedirects[arguments[0].src];
        }

        // Call original method
        return appendChild.apply(this, arguments);
      }
    })(Element.prototype.appendChild);

    // Overwrite insertBefore method
    (function (insertBefore) {
      Node.prototype.insertBefore = function () {
        // Apply url directs if applicable
        if (urlRedirects[arguments[0].src]) {
          console.log(
            `Redirecting ${arguments[0].src} to ${
              urlRedirects[arguments[0].src]
            }`
          );
          arguments[0].src = urlRedirects[arguments[0].src];
        }

        // Call original method
        return insertBefore.apply(this, arguments);
      };
    })(Node.prototype.insertBefore);
  }

  // Snippet to install Pendo
  // Note, call to get agentType is only for illustrative purposes to show how JSONP fails in manifest V3
  // In Manifest V2, the agent is loaded from the CDN as is normal
  // In Manifest V3, the agent must be self-hosted and is loaded from inside the chrome extension
  chrome.storage.sync.get(["agentType"], (data) => {
    (function (apiKey) {
      window._apiKey = apiKey;
      (function (p, e, n, d, o) {
        var v, w, x, y, z;
        o = p[d] = p[d] || {};
        o._q = o._q || [];
        v = ["initialize", "identify", "updateOptions", "pageLoad", "track"];
        for (w = 0, x = v.length; w < x; ++w)
          (function (m) {
            o[m] =
              o[m] ||
              function () {
                o._q[m === v[0] ? "unshift" : "push"](
                  [m].concat([].slice.call(arguments, 0))
                );
              };
          })(v[w]);
        y = e.createElement(n);
        y.async = !0;
        y.src =
          manifestVersion === 2
            ? `https://cdn.pendo.io/agent/static/${apiKey}/pendo.js`
            : `${pendoAgentScriptsBaseUrl}/pendo.${data.agentType}.js`;

        z = e.getElementsByTagName(n)[0];
        z.parentNode.insertBefore(y, z);
      })(window, document, "script", "pendo");

      pendo.initialize(userData); // [Talend] replaced the original json payload by our own specific payload
    })(apiKey);
  });
}

// [Talend] start Talend code
/* eslint-enable */
function getPendoData (talendPortalUrl, accessToken) {
  return fetch(talendPortalUrl, { headers: { Authorization: `Bearer ${accessToken}` } })
    .then((response) => response.json()); // Parse the Response instance into a useable format using `.json()`
}

function getClientPendoDataOverwrite (PENDO_API_KEY) {
  return {
    apiKey: PENDO_API_KEY,
    visitor: {
      browserResolution: `${window.screen.width}x${window.screen.height}`,
    },
  };
}

const initPendo = (accessToken, PENDO_API_KEY, talendPortalUrl) => {

  if (!accessToken) {
    return Promise.resolve();
  }

  return getPendoData(talendPortalUrl, accessToken)
    .then((data) => {
      const pendoDataStructure = { // default structure to send to Pendo in case back-end data is malformed
        visitor: {},
        account: {},
      };
      const clientPendoDataOverwrite = getClientPendoDataOverwrite(PENDO_API_KEY);
      const pendoData = merge(
        pendoDataStructure,
        data,
        clientPendoDataOverwrite,
      );

      return initializePendo(PENDO_API_KEY, pendoData);
    });
};

window.APP.pendoService = {
  initPendo,
};
// [Talend] end Talend code
