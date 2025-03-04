(function () {
  (function () {
    /*!! Simple JavaScript Inheritance
     * By John Resig http://ejohn.org/
     * MIT Licensed.
     * http://ejohn.org/blog/simple-javascript-inheritance
     *
     */
    var c = false,
      e = /xyz/.test(function () {
        xyz;
      })
        ? /\b_super\b/
        : /.*/;
    var b = function () {};
    b.extend = function (l) {
      var k = this.prototype;
      c = true;
      var j = new this();
      c = false;
      for (var i in l) {
        j[i] =
          typeof l[i] == "function" && typeof k[i] == "function" && e.test(l[i])
            ? (function (m, n) {
                return function () {
                  var p = this._super;
                  this._super = k[m];
                  var o = n.apply(this, arguments);
                  this._super = p;
                  return o;
                };
              })(i, l[i])
            : l[i];
      }
      function h() {
        if (!(this instanceof h)) {
          return new h(arguments);
        }
        if (!c && this.init) {
          this.init.apply(this, arguments);
        }
      }
      h.prototype = j;
      h.prototype.constructor = h;
      h.extend = arguments.callee;
      return h;
    };
    var d = b.extend({
      init: function () {},
      setAccount: function () {},
      trackEvent: function () {},
      trackPageView: function () {},
      logEvents: function () {},
      setLabel: function () {},
      runFuncIfAnalyticsExists: function () {},
    });
    var g = d.extend({
      _loaded: false,
      _trackerID: "",
      _logEvents: false,
      _logger: null,
      _activeLabel: null,
      _optPageName: null,
      _customVariableIndices: null,
      _canLog: function () {
        return this._logger !== null && this._logEvents === true;
      },
      init: function (k, m, j, i) {
        var l,
          n = document.createElement("script"),
          h = this;
        this._optPageName = m;
        if (i === undefined) {
          i = false;
        }
        n.type = "text/javascript";
        n.async = true;
        n.src = "thirdparty/google-analytics-bundle.js"
        l = document.getElementsByTagName("script")[0];
        l.parentNode.insertBefore(n, l);
        this._trackerID = k;
        n.onreadystatechange = function () {
          if (this.readyState === "complete") {
            h._scriptLoaded();
            if (j) {
              j();
            }
          }
        };
        n.onload = function () {
          h._scriptLoaded();
          if (j) {
            j();
          }
        };
        h._customVariableIndices = { product: 1, license: 2, numUsers: 3 };
      },
      loaded: function () {
        return this._loaded;
      },
      setAccount: function (h) {
        this._push(["_setAccount", h]);
      },
      setLabel: function (h) {
        this._activeLabel = h;
      },
      setCustomVariables: function (k, j, h) {
        var i = 3;
        if (k) {
          this._push([
            "_setCustomVar",
            this._customVariableIndices.product,
            "product",
            k,
            i,
          ]);
        }
        this._push([
          "_setCustomVar",
          this._customVariableIndices.license,
          "license",
          j,
          i,
        ]);
        this._push([
          "_setCustomVar",
          this._customVariableIndices.numUsers,
          "numUsers",
          h,
          i,
        ]);
      },
      trackEvent: function (j, k, l, h, i) {
        l = l === undefined ? null : l;
        h = h === undefined ? null : h;
        i === undefined ? null : i;
        if (l === null && this._activeLabel !== null) {
          l = this._activeLabel;
        }
        this._push(["_trackEvent", j, k, l, h, i]);
        if (j === "lzxclient") {
          this._push(["_trackPageview", "/lzxEvent/" + k]);
        }
      },
      trackPageView: function () {
        if (this._optPageName) {
          this._push(["_trackPageview", this._optPageName]);
        } else {
          this._push(["_trackPageview"]);
        }
      },
      logEvents: function (h, i) {
        this._logEvents = h;
        this._logger = i;
      },
      _scriptLoaded: function () {
        this._loaded = true;
        this._push(["_setDomainName", "none"]);
        this.setAccount(this._trackerID);
        this.trackPageView();
      },
      _push: function (h) {
        if (typeof _gaq != "undefined") {
          _gaq.push(h);
          if (this._canLog()) {
            this._logger.log("ANALYTICS: " + h.join(", "));
          }
        }
      },
      runFuncIfAnalyticsExists: function (h) {
        if (h) {
          h();
        }
      },
    });
    var a = b.extend({
      _analytics: null,
      _queuedEvents: null,
      init: function () {
        var h = this;
        h._queuedEvents = [];
        jq = window.jQuery || window["$"];
        jq(window).bind("init.gliffyAnalytics", function (j, k) {
          var i = jq.extend(
            {},
            {
              accountType: "none",
              accountID: "UA-248648-8",
              debug: false,
              optPageName: null,
            },
            k
          );
          if (!h._analytics) {
            switch (i.accountType) {
              case "google":
                h._analytics = new g(
                  i.accountID,
                  i.optPageName,
                  function () {
                    var l;
                    while ((l = h._queuedEvents.shift())) {
                      $(window).trigger(l[0] + ".gliffyAnalytics", l[1]);
                    }
                  },
                  i.debug
                );
                break;
            }
          }
        });
        jq(window).bind("trackEvent.gliffyAnalytics", function (j, k) {
          var i = jq.extend(
            {},
            {
              category: "",
              action: "",
              label: null,
              value: null,
              non_interaction: null,
            },
            k
          );
          if (h._analytics && h._analytics.loaded()) {
            h._analytics.trackEvent(
              i.category,
              i.action,
              i.label,
              i.value,
              i.non_interaction
            );
          } else {
            h._queuedEvents.push(["trackEvent", k]);
          }
        });
        jq(window).bind("setLabel.gliffyAnalytics", function (j, k) {
          var i = jq.extend({}, { label: null }, k);
          if (h._analytics && h._analytics.loaded()) {
            h._analytics.setLabel(i.label);
          } else {
            h._queuedEvents.push(["setLabel", k]);
          }
        });
        jq(window).bind("logEvents.gliffyAnalytics", function (j, k) {
          var i = jq.extend({}, { logEvents: false, logger: null }, k);
          if (h._analytics && h._analytics.loaded()) {
            h._analytics.logEvents(i.logEvents, i.logger);
          } else {
            h._queuedEvents.push(["logEvents", k]);
          }
        });
        jq(window).bind("setCustomVariables.gliffyAnalytics", function (j, k) {
          var i = jq.extend(
            {},
            { product: null, license: "none", numUsers: 0 },
            k
          );
          if (h._analytics && h._analytics.loaded()) {
            h._analytics.setCustomVariables(i.product, i.license, i.numUsers);
          } else {
            h._queuedEvents.push(["setCustomVariables", k]);
          }
        });
        jq(window).bind("runFuncIfAnalyticsExists.gliffyAnalytics", function (
          j,
          k
        ) {
          var i = jq.extend({}, { func: null }, k);
          if (h._analytics) {
            h._analytics.runFuncIfAnalyticsExists(i.func);
          }
        });
      },
    });
    var f = new a();
  })();
})();
