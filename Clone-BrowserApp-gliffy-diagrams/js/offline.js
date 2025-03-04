(function () {
  /*!
   * Copyright (c) 2010 Chris O'Hara <cohara87@gmail.com>
   *
   * Permission is hereby granted, free of charge, to any person obtaining
   * a copy of this software and associated documentation files (the
   * "Software"), to deal in the Software without restriction, including
   * without limitation the rights to use, copy, modify, merge, publish,
   * distribute, sublicense, and/or sell copies of the Software, and to
   * permit persons to whom the Software is furnished to do so, subject to
   * the following conditions:
   *
   * The above copyright notice and this permission notice shall be
   * included in all copies or substantial portions of the Software.
   *
   * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
   * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
   * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
   * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
   * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
   * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
   * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
   */
  (function (E) {
    var b = {
      "&nbsp;": " ",
      "&iexcl;": "¡",
      "&cent;": "¢",
      "&pound;": "£",
      "&curren;": "€",
      "&yen;": "¥",
      "&brvbar;": "Š",
      "&sect;": "§",
      "&uml;": "š",
      "&copy;": "©",
      "&ordf;": "ª",
      "&laquo;": "«",
      "&not;": "¬",
      "&shy;": "",
      "&reg;": "®",
      "&macr;": "¯",
      "&deg;": "°",
      "&plusmn;": "±",
      "&sup2;": "²",
      "&sup3;": "³",
      "&acute;": "Ž",
      "&micro;": "µ",
      "&para;": "¶",
      "&middot;": "·",
      "&cedil;": "ž",
      "&sup1;": "¹",
      "&ordm;": "º",
      "&raquo;": "»",
      "&frac14;": "Œ",
      "&frac12;": "œ",
      "&frac34;": "Ÿ",
      "&iquest;": "¿",
      "&Agrave;": "À",
      "&Aacute;": "Á",
      "&Acirc;": "Â",
      "&Atilde;": "Ã",
      "&Auml;": "Ä",
      "&Aring;": "Å",
      "&AElig;": "Æ",
      "&Ccedil;": "Ç",
      "&Egrave;": "È",
      "&Eacute;": "É",
      "&Ecirc;": "Ê",
      "&Euml;": "Ë",
      "&Igrave;": "Ì",
      "&Iacute;": "Í",
      "&Icirc;": "Î",
      "&Iuml;": "Ï",
      "&ETH;": "Ð",
      "&Ntilde;": "Ñ",
      "&Ograve;": "Ò",
      "&Oacute;": "Ó",
      "&Ocirc;": "Ô",
      "&Otilde;": "Õ",
      "&Ouml;": "Ö",
      "&times;": "×",
      "&Oslash;": "Ø",
      "&Ugrave;": "Ù",
      "&Uacute;": "Ú",
      "&Ucirc;": "Û",
      "&Uuml;": "Ü",
      "&Yacute;": "Ý",
      "&THORN;": "Þ",
      "&szlig;": "ß",
      "&agrave;": "à",
      "&aacute;": "á",
      "&acirc;": "â",
      "&atilde;": "ã",
      "&auml;": "ä",
      "&aring;": "å",
      "&aelig;": "æ",
      "&ccedil;": "ç",
      "&egrave;": "è",
      "&eacute;": "é",
      "&ecirc;": "ê",
      "&euml;": "ë",
      "&igrave;": "ì",
      "&iacute;": "í",
      "&icirc;": "î",
      "&iuml;": "ï",
      "&eth;": "ð",
      "&ntilde;": "ñ",
      "&ograve;": "ò",
      "&oacute;": "ó",
      "&ocirc;": "ô",
      "&otilde;": "õ",
      "&ouml;": "ö",
      "&divide;": "÷",
      "&oslash;": "ø",
      "&ugrave;": "ù",
      "&uacute;": "ú",
      "&ucirc;": "û",
      "&uuml;": "ü",
      "&yacute;": "ý",
      "&thorn;": "þ",
      "&yuml;": "ÿ",
      "&quot;": '"',
      "&lt;": "<",
      "&gt;": ">",
      "&apos;": "'",
      "&minus;": "−",
      "&circ;": "ˆ",
      "&tilde;": "˜",
      "&Scaron;": "Š",
      "&lsaquo;": "‹",
      "&OElig;": "Œ",
      "&lsquo;": "‘",
      "&rsquo;": "’",
      "&ldquo;": "“",
      "&rdquo;": "”",
      "&bull;": "•",
      "&ndash;": "–",
      "&mdash;": "—",
      "&trade;": "™",
      "&scaron;": "š",
      "&rsaquo;": "›",
      "&oelig;": "œ",
      "&Yuml;": "Ÿ",
      "&fnof;": "ƒ",
      "&Alpha;": "Α",
      "&Beta;": "Β",
      "&Gamma;": "Γ",
      "&Delta;": "Δ",
      "&Epsilon;": "Ε",
      "&Zeta;": "Ζ",
      "&Eta;": "Η",
      "&Theta;": "Θ",
      "&Iota;": "Ι",
      "&Kappa;": "Κ",
      "&Lambda;": "Λ",
      "&Mu;": "Μ",
      "&Nu;": "Ν",
      "&Xi;": "Ξ",
      "&Omicron;": "Ο",
      "&Pi;": "Π",
      "&Rho;": "Ρ",
      "&Sigma;": "Σ",
      "&Tau;": "Τ",
      "&Upsilon;": "Υ",
      "&Phi;": "Φ",
      "&Chi;": "Χ",
      "&Psi;": "Ψ",
      "&Omega;": "Ω",
      "&alpha;": "α",
      "&beta;": "β",
      "&gamma;": "γ",
      "&delta;": "δ",
      "&epsilon;": "ε",
      "&zeta;": "ζ",
      "&eta;": "η",
      "&theta;": "θ",
      "&iota;": "ι",
      "&kappa;": "κ",
      "&lambda;": "λ",
      "&mu;": "μ",
      "&nu;": "ν",
      "&xi;": "ξ",
      "&omicron;": "ο",
      "&pi;": "π",
      "&rho;": "ρ",
      "&sigmaf;": "ς",
      "&sigma;": "σ",
      "&tau;": "τ",
      "&upsilon;": "υ",
      "&phi;": "φ",
      "&chi;": "χ",
      "&psi;": "ψ",
      "&omega;": "ω",
      "&thetasym;": "ϑ",
      "&upsih;": "ϒ",
      "&piv;": "ϖ",
      "&ensp;": " ",
      "&emsp;": " ",
      "&thinsp;": " ",
      "&zwnj;": "",
      "&zwj;": "",
      "&lrm;": "",
      "&rlm;": "",
      "&sbquo;": "‚",
      "&bdquo;": "„",
      "&dagger;": "†",
      "&Dagger;": "‡",
      "&hellip;": "…",
      "&permil;": "‰",
      "&prime;": "′",
      "&Prime;": "″",
      "&oline;": "‾",
      "&frasl;": "⁄",
      "&euro;": "€",
      "&image;": "ℑ",
      "&weierp;": "℘",
      "&real;": "ℜ",
      "&alefsym;": "ℵ",
      "&larr;": "←",
      "&uarr;": "↑",
      "&rarr;": "→",
      "&darr;": "↓",
      "&harr;": "↔",
      "&crarr;": "↵",
      "&lArr;": "⇐",
      "&uArr;": "⇑",
      "&rArr;": "⇒",
      "&dArr;": "⇓",
      "&hArr;": "⇔",
      "&forall;": "∀",
      "&part;": "∂",
      "&exist;": "∃",
      "&empty;": "∅",
      "&nabla;": "∇",
      "&isin;": "∈",
      "&notin;": "∉",
      "&ni;": "∋",
      "&prod;": "∏",
      "&sum;": "∑",
      "&lowast;": "∗",
      "&radic;": "√",
      "&prop;": "∝",
      "&infin;": "∞",
      "&ang;": "∠",
      "&and;": "∧",
      "&or;": "∨",
      "&cap;": "∩",
      "&cup;": "∪",
      "&int;": "∫",
      "&there4;": "∴",
      "&sim;": "∼",
      "&cong;": "≅",
      "&asymp;": "≈",
      "&ne;": "≠",
      "&equiv;": "≡",
      "&le;": "≤",
      "&ge;": "≥",
      "&sub;": "⊂",
      "&sup;": "⊃",
      "&nsub;": "⊄",
      "&sube;": "⊆",
      "&supe;": "⊇",
      "&oplus;": "⊕",
      "&otimes;": "⊗",
      "&perp;": "⊥",
      "&sdot;": "⋅",
      "&lceil;": "⌈",
      "&rceil;": "⌉",
      "&lfloor;": "⌊",
      "&rfloor;": "⌋",
      "&lang;": "〈",
      "&rang;": "〉",
      "&loz;": "◊",
      "&spades;": "♠",
      "&clubs;": "♣",
      "&hearts;": "♥",
      "&diams;": "♦",
    };
    var z = function (c) {
      if (!~c.indexOf("&")) {
        return c;
      }
      for (var d in b) {
        c = c.replace(new RegExp(d, "g"), b[d]);
      }
      c = c.replace(/&#x(0*[0-9a-f]{2,5});?/gi, function (e, f) {
        return String.fromCharCode(parseInt(+f, 16));
      });
      c = c.replace(/&#([0-9]{2,4});?/gi, function (e, f) {
        return String.fromCharCode(+f);
      });
      c = c.replace(/&amp;/g, "&");
      return c;
    };
    var w = function (c) {
      c = c.replace(/&/g, "&amp;");
      c = c.replace(/'/g, "&#39;");
      for (var d in b) {
        c = c.replace(new RegExp(b[d], "g"), d);
      }
      return c;
    };
    E.entities = { encode: w, decode: z };
    var F = {
      "document.cookie": "",
      "document.write": "",
      ".parentNode": "",
      ".innerHTML": "",
      "window.location": "",
      "-moz-binding": "",
      "<!--": "&lt;!--",
      "-->": "--&gt;",
      "<![CDATA[": "&lt;![CDATA[",
    };
    var k = {
      "javascript\\s*:": "",
      "expression\\s*(\\(|&\\#40;)": "",
      "vbscript\\s*:": "",
      "Redirect\\s+302": "",
    };
    var C = [
      /%0[0-8bcef]/g,
      /%1[0-9a-f]/g,
      /[\x00-\x08]/g,
      /\x0b/g,
      /\x0c/g,
      /[\x0e-\x1f]/g,
    ];
    var j = [
      "javascript",
      "expression",
      "vbscript",
      "script",
      "applet",
      "alert",
      "document",
      "write",
      "cookie",
      "window",
    ];
    E.xssClean = function (o, s) {
      if (typeof o === "object") {
        for (var n in o) {
          o[n] = E.xssClean(o[n]);
        }
        return o;
      }
      o = B(o);
      o = o.replace(/\&([a-z\_0-9]+)\=([a-z\_0-9]+)/i, D() + "$1=$2");
      o = o.replace(/(&\#?[0-9a-z]{2,})([\x00-\x20])*;?/i, "$1;$2");
      o = o.replace(/(&\#x?)([0-9A-F]+);?/i, "$1;$2");
      o = o.replace(D(), "&");
      try {
        o = decodeURIComponent(o);
      } catch (h) {}
      o = o.replace(/[a-z]+=([\'\"]).*?\1/gi, function (e, f) {
        return e.replace(f, g(f));
      });
      o = B(o);
      o = o.replace("	", " ");
      var d = o;
      for (var n in F) {
        o = o.replace(n, F[n]);
      }
      for (var n in k) {
        o = o.replace(new RegExp(n, "i"), k[n]);
      }
      for (var n in j) {
        var p = j[n].split("").join("\\s*") + "\\s*";
        o = o.replace(new RegExp("(" + p + ")(\\W)", "ig"), function (f, i, l) {
          return i.replace(/\s+/g, "") + l;
        });
      }
      do {
        var c = o;
        if (o.match(/<a/i)) {
          o = o.replace(/<a\s+([^>]*?)(>|$)/gi, function (f, i, l) {
            i = x(i.replace("<", "").replace(">", ""));
            return f.replace(
              i,
              i.replace(
                /href=.*?(alert\(|alert&\#40;|javascript\:|charset\=|window\.|document\.|\.cookie|<script|<xss|base64\s*,)/gi,
                ""
              )
            );
          });
        }
        if (o.match(/<img/i)) {
          o = o.replace(/<img\s+([^>]*?)(\s?\/?>|$)/gi, function (f, i, l) {
            i = x(i.replace("<", "").replace(">", ""));
            return f.replace(
              i,
              i.replace(
                /src=.*?(alert\(|alert&\#40;|javascript\:|charset\=|window\.|document\.|\.cookie|<script|<xss|base64\s*,)/gi,
                ""
              )
            );
          });
        }
        if (o.match(/script/i) || o.match(/xss/i)) {
          o = o.replace(/<(\/*)(script|xss)(.*?)\>/gi, "");
        }
      } while (c != o);
      event_handlers = ["[^a-z_-]on\\w*"];
      if (!s) {
        event_handlers.push("xmlns");
      }
      o = o.replace(
        new RegExp(
          "<([^><]+?)(" +
            event_handlers.join("|") +
            ")(\\s*=\\s*[^><]*)([><]*)",
          "i"
        ),
        "<$1$4"
      );
      naughty =
        "alert|applet|audio|basefont|base|behavior|bgsound|blink|body|embed|expression|form|frameset|frame|head|html|ilayer|iframe|input|isindex|layer|link|meta|object|plaintext|style|script|textarea|title|video|xml|xss";
      o = o.replace(
        new RegExp("<(/*\\s*)(" + naughty + ")([^><]*)([><]*)", "gi"),
        function (l, u, G, f, m) {
          return (
            "&lt;" + u + G + f + m.replace(">", "&gt;").replace("<", "&lt;")
          );
        }
      );
      o = o.replace(
        /(alert|cmd|passthru|eval|exec|expression|system|fopen|fsockopen|file|file_get_contents|readfile|unlink)(\s*)\((.*?)\)/gi,
        "$1$2&#40;$3&#41;"
      );
      for (var n in F) {
        o = o.replace(n, F[n]);
      }
      for (var n in k) {
        o = o.replace(new RegExp(n, "i"), k[n]);
      }
      if (s && o !== d) {
        throw new Error("Image may contain XSS");
      }
      return o;
    };

    function B(c) {
      for (var d in C) {
        c = c.replace(C[d], "");
      }
      return c;
    }

    function D() {
      return "!*$^#(@*#&";
    }

    function g(c) {
      return c.replace(">", "&gt;").replace("<", "&lt;").replace("\\", "\\\\");
    }

    function x(c) {
      var d = /\/\*.*?\*\//g;
      return c
        .replace(/\s*[a-z-]+\s*=\s*'[^']*'/gi, function (e) {
          return e.replace(d, "");
        })
        .replace(/\s*[a-z-]+\s*=\s*"[^"]*"/gi, function (e) {
          return e.replace(d, "");
        })
        .replace(/\s*[a-z-]+\s*=\s*[^\s]+/gi, function (e) {
          return e.replace(d, "");
        });
    }

    var v = (E.Validator = function () {});
    v.prototype.check = function (c, d) {
      this.str =
        typeof c === "undefined" ||
        c === null ||
        (isNaN(c) && c.length === undefined)
          ? ""
          : c + "";
      this.msg = d;
      this._errors = this._errors || [];
      return this;
    };
    v.prototype.validate = v.prototype.check;
    v.prototype.assert = v.prototype.check;
    v.prototype.error = function (c) {
      throw new Error(c);
    };

    function y(c) {
      if (c instanceof Date) {
        return c;
      }
      var d = Date.parse(c);
      if (isNaN(d)) {
        return null;
      }
      return new Date(d);
    }

    v.prototype.isAfter = function (c) {
      c = c || new Date();
      var d = y(this.str),
        f = y(c);
      if (!(d && f && d >= f)) {
        return this.error(this.msg || "Invalid date");
      }
      return this;
    };
    v.prototype.isBefore = function (c) {
      c = c || new Date();
      var d = y(this.str),
        f = y(c);
      if (!(d && f && d <= f)) {
        return this.error(this.msg || "Invalid date");
      }
      return this;
    };
    v.prototype.isEmail = function () {
      if (
        !this.str.match(
          /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/
        )
      ) {
        return this.error(this.msg || "Invalid email");
      }
      return this;
    };
    v.prototype.isCreditCard = function () {
      this.str = this.str.replace(/[^0-9]+/g, "");
      if (
        !this.str.match(
          /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/
        )
      ) {
        return this.error(this.msg || "Invalid credit card");
      }
      var d = 0;
      var h;
      var l;
      var c = false;
      for (var f = this.length - 1; f >= 0; f--) {
        h = this.substring(f, f + 1);
        l = parseInt(h, 10);
        if (c) {
          l *= 2;
          if (l >= 10) {
            d += (l % 10) + 1;
          } else {
            d += l;
          }
        } else {
          d += l;
        }
        if (c) {
          c = false;
        } else {
          c = true;
        }
      }
      if (d % 10 !== 0) {
        return this.error(this.msg || "Invalid credit card");
      }
      return this;
    };
    v.prototype.isUrl = function () {
      if (
        !this.str.match(
          /^(?!mailto:)(?:(?:https?|ftp):\/\/)?(?:\S+(?::\S*)?@)?(?:(?:(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))|localhost)(?::\d{2,5})?(?:\/[^\s]*)?$/i
        ) ||
        this.str.length > 2083
      ) {
        return this.error(this.msg || "Invalid URL");
      }
      return this;
    };
    v.prototype.isIP = function () {
      if (
        !this.str.match(
          /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/
        )
      ) {
        return this.error(this.msg || "Invalid IP");
      }
      return this;
    };
    v.prototype.isAlpha = function () {
      if (!this.str.match(/^[a-zA-Z]+$/)) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.isAlphanumeric = function () {
      if (!this.str.match(/^[a-zA-Z0-9]+$/)) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.isNumeric = function () {
      if (!this.str.match(/^-?[0-9]+$/)) {
        return this.error(this.msg || "Invalid number");
      }
      return this;
    };
    v.prototype.isLowercase = function () {
      if (!this.str.match(/^[a-z0-9]+$/)) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.isUppercase = function () {
      if (!this.str.match(/^[A-Z0-9]+$/)) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.isInt = function () {
      if (!this.str.match(/^(?:-?(?:0|[1-9][0-9]*))$/)) {
        return this.error(this.msg || "Invalid integer");
      }
      return this;
    };
    v.prototype.isDecimal = function () {
      if (!this.str.match(/^(?:-?(?:0|[1-9][0-9]*))?(?:\.[0-9]*)?$/)) {
        return this.error(this.msg || "Invalid decimal");
      }
      return this;
    };
    v.prototype.isFloat = function () {
      return this.isDecimal();
    };
    v.prototype.notNull = function () {
      if (this.str === "") {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.isNull = function () {
      if (this.str !== "") {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.notEmpty = function () {
      if (this.str.match(/^[\s\t\r\n]*$/)) {
        return this.error(this.msg || "String is whitespace");
      }
      return this;
    };
    v.prototype.equals = function (c) {
      if (this.str != c) {
        return this.error(this.msg || "Not equal");
      }
      return this;
    };
    v.prototype.contains = function (c) {
      if (this.str.indexOf(c) === -1) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.notContains = function (c) {
      if (this.str.indexOf(c) >= 0) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.regex = v.prototype.is = function (c, d) {
      if (Object.prototype.toString.call(c).slice(8, -1) !== "RegExp") {
        c = new RegExp(c, d);
      }
      if (!this.str.match(c)) {
        return this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.notRegex = v.prototype.not = function (c, d) {
      if (Object.prototype.toString.call(c).slice(8, -1) !== "RegExp") {
        c = new RegExp(c, d);
      }
      if (this.str.match(c)) {
        this.error(this.msg || "Invalid characters");
      }
      return this;
    };
    v.prototype.len = function (c, d) {
      if (this.str.length < c) {
        return this.error(this.msg || "String is too small");
      }
      if (typeof d !== undefined && this.str.length > d) {
        return this.error(this.msg || "String is too large");
      }
      return this;
    };
    v.prototype.isUUID = function (c) {
      var d;
      if (c == 3 || c == "v3") {
        d = /[0-9A-F]{8}-[0-9A-F]{4}-3[0-9A-F]{3}-[0-9A-F]{4}-[0-9A-F]{12}$/i;
      } else {
        if (c == 4 || c == "v4") {
          d = /[0-9A-F]{8}-[0-9A-F]{4}-4[0-9A-F]{3}-[89AB][0-9A-F]{3}-[0-9A-F]{12}$/i;
        } else {
          d = /[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}$/i;
        }
      }
      if (!this.str.match(d)) {
        return this.error(this.msg || "Not a UUID");
      }
      return this;
    };
    v.prototype.isDate = function () {
      var c = Date.parse(this.str);
      if (isNaN(c)) {
        return this.error(this.msg || "Not a date");
      }
      return this;
    };
    v.prototype.isIn = function (c) {
      if (c && typeof c.indexOf === "function") {
        if (!~c.indexOf(this.str)) {
          return this.error(this.msg || "Unexpected value");
        }
        return this;
      } else {
        return this.error(this.msg || "Invalid in() argument");
      }
    };
    v.prototype.notIn = function (c) {
      if (c && typeof c.indexOf === "function") {
        if (c.indexOf(this.str) !== -1) {
          return this.error(this.msg || "Unexpected value");
        }
        return this;
      } else {
        return this.error(this.msg || "Invalid notIn() argument");
      }
    };
    v.prototype.min = function (c) {
      var d = parseFloat(this.str);
      if (!isNaN(d) && d < c) {
        return this.error(this.msg || "Invalid number");
      }
      return this;
    };
    v.prototype.max = function (c) {
      var d = parseFloat(this.str);
      if (!isNaN(d) && d > c) {
        return this.error(this.msg || "Invalid number");
      }
      return this;
    };
    v.prototype.isArray = function () {
      if (!Array.isArray(this.str)) {
        return this.error(this.msg || "Not an array");
      }
      return this;
    };
    var q = (E.Filter = function () {});
    var A = "\\r\\n\\t\\s";
    q.prototype.modify = function (c) {
      this.str = c;
    };
    q.prototype.convert = q.prototype.sanitize = function (c) {
      this.str = c == null ? "" : c + "";
      return this;
    };
    q.prototype.xss = function (c) {
      this.modify(E.xssClean(this.str, c));
      return this.str;
    };
    q.prototype.entityDecode = function () {
      this.modify(z(this.str));
      return this.str;
    };
    q.prototype.entityEncode = function () {
      this.modify(w(this.str));
      return this.str;
    };
    q.prototype.escape = function () {
      this.modify(
        this.str
          .replace(/&/g, "&amp;")
          .replace(/"/g, "&quot;")
          .replace(/</g, "&lt;")
          .replace(/>/g, "&gt;")
      );
      return this.str;
    };
    q.prototype.ltrim = function (c) {
      c = c || A;
      this.modify(this.str.replace(new RegExp("^[" + c + "]+", "g"), ""));
      return this.str;
    };
    q.prototype.rtrim = function (c) {
      c = c || A;
      this.modify(this.str.replace(new RegExp("[" + c + "]+$", "g"), ""));
      return this.str;
    };
    q.prototype.trim = function (c) {
      c = c || A;
      this.modify(
        this.str.replace(new RegExp("^[" + c + "]+|[" + c + "]+$", "g"), "")
      );
      return this.str;
    };
    q.prototype.ifNull = function (c) {
      if (!this.str || this.str === "") {
        this.modify(c);
      }
      return this.str;
    };
    q.prototype.toFloat = function () {
      this.modify(parseFloat(this.str));
      return this.str;
    };
    q.prototype.toInt = function (c) {
      c = c || 10;
      this.modify(parseInt(this.str, c));
      return this.str;
    };
    q.prototype.toBoolean = function () {
      if (
        !this.str ||
        this.str == "0" ||
        this.str == "false" ||
        this.str == ""
      ) {
        this.modify(false);
      } else {
        this.modify(true);
      }
      return this.str;
    };
    q.prototype.toBooleanStrict = function () {
      if (this.str == "1" || this.str == "true") {
        this.modify(true);
      } else {
        this.modify(false);
      }
      return this.str;
    };
    E.sanitize = E.convert = function (c) {
      var d = new E.Filter();
      return d.sanitize(c);
    };
    E.check = E.validate = E.assert = function (d, f) {
      var c = new E.Validator();
      return c.check(d, f);
    };
  })(typeof exports === "undefined" ? window : exports);
  (function () {
    if (!window.GLIFFY) {
      window.GLIFFY = {};
    }
    if (!window.GLIFFY.ENV) {
      window.GLIFFY.ENV = {};
    }
    if (!window.GLIFFY.FEATURES) {
      window.GLIFFY.FEATURES = {};
    }
    GLIFFY.FEATURES.browserTabs = true;
    window.GLIFFY.ENV.app = "";
    window.GLIFFY.ENV.appName = "";
    window.GLIFFY.ENV.welcomeController = "demoController";
  })();
  (function () {
    window.GLIFFY.onlineUtils = {
      predecorate: function (b, c, e) {
        var d = b.prototype[c];
        b.prototype[c] = function () {
          e.apply(this, arguments);
          return d.apply(this, arguments);
        };
      },
      postdecorate: function (b, c, d) {
        var e = b.prototype[c];
        b.prototype[c] = function () {
          var f = [e.apply(this, arguments)].concat(
            Array.prototype.slice.call(arguments, 0)
          );
          return d.apply(this, f);
        };
      },
      override: function (c, d, b) {
        c.prototype[d] = b;
      },
      appendTemplates: function (b, d) {
        var c;
        for (c = 0; c < b.length; c++) {
          window.GLIFFY.onlineUtils.appendTemplate(b[c], d);
        }
      },
      appendTemplate: function (b, c) {
        c = c || "#gliffy";
        setTimeout(function () {
          Ember.View.create({ templateName: b }).appendTo(c);
        }, 1);
      },
      renderTemplate: function (c, b) {
        setTimeout(function () {
          $(b).html("");
          Ember.View.create({ templateName: c }).appendTo(b);
        }, 1);
      },
      isValidEmail: function (c) {
        var e = new RegExp(
          "^([\\w+!#$%&'*+-/=?^_`{|}~.])+@(([a-z\\d-]+)|([a-z\\d-]+\\.[a-z\\d-]+))+\\.[a-z]+$",
          "gi"
        );
        var b = e.exec(c);
        if (b !== null) {
          var d = b[1];
          if (d.indexOf(".") !== d.length - 1 && d.indexOf("..") === -1) {
            return true;
          }
        }
        return false;
      },
    };
  })();
  (function () {
    GliffyApp.DocumentModel = GliffyApp.DocumentModel.extend({
      fullPath: null,
      lastDraftModifiedDate: null,
      populate: function (b) {
        if (b.name !== undefined) {
          this.set("fullPath", b.fullPath);
          this.set("title", b.name);
        }
      },
      displayTitle: Ember.computed(function () {
        var b = this.get("title");
        if (
          !$.isNumeric(this.get("currentRevision")) ||
          this.get("currentRevision") < 1
        ) {
          b = $.i18n._("DOCUMENT_NAME_UNTITLED");
        }
        return (this.get("unsavedChanges") === true ? "*" : "") + b;
      }).property("title", "unsavedChanges", "currentRevision"),
      update: function (b) {
        this._super(b);
        if (b.getUrls().modificationDate) {
          this.set(
            "lastDraftModifiedDate",
            new Date(b.getUrls().modificationDate).toString(
              "MMMM d, yyyy HH:mm:ss"
            )
          );
        }
      },
    });
  })();
  (function () {
    GliffyApp.toolbarController = GliffyApp.toolbarController.extend({
      doNew: function () {
        this._super();
      },
      doOpen: function () {
        this._super();
      },
      doSave: function () {
        this._super();
      },
      doClose: function () {
        this.__editor.__actions.close(
          this.__editor.getDocumentManager().getActiveDocument().__toObject()
        );
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.SaveDialogController = GliffyApp.SaveDialogController.extend({
      show: function () {
        var c = this,
          d = this.__editor.getDocumentManager().getActiveDocument().getUrls()
            .tempId;
        b.send(
          "promptSaveAs",
          null,
          function (e) {
            if (e.cancelled) {
              c.cancelSave();
            } else {
              e.urls.tempId = d;
              c.__editor
                .getDocumentManager()
                .getActiveDocument()
                .refreshFromObject(e);
              c.applySave();
            }
          },
          function (e, f) {
            this.showErrorMessage(f);
          }
        );
      },
      showErrorMessage: function (c) {
        GLIFFY.Dialog.showAlertDialog({ content: c, title: "Oops!" });
      },
    });
  })();
  (function () {
    var c = "hideTips",
      b = new GLIFFY.Message();
    GliffyApp.TipsController = GliffyApp.TipsController.extend({
      handleNoTipsChecked: function (e) {
        var d = {};
        d[c] = e ? true : false;
        b.send("setLocalStorage", d);
      },
      doNotShowTips: function () {
        return GliffyApp.demoController.__hideTips;
      },
    });
    GliffyApp.demoController = Ember.Object.create({
      __hideTips: false,
      init: function () {},
      show: function () {
        var d = this;
        b.send("getLocalStorage", [c, "gliffyDesktop"], function (f) {
          d.__hideTips = f[c] || false;
          var e = f.gliffyDesktop || {};
          if (e.registered) {
            d.__showTips();
          } else {
            d.__showRegister(e);
          }
        });
      },
      __sendDesktopData: function (d) {
        b.send("updateDesktopData", { gliffyDesktop: d });
      },
      __showRegister: function (f) {
        var d = this,
          e;
        e = $(
          "<div id='gliffy-desktop-register' class='gliffy-popup'><div class='modal'><div class='modal-header'>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_HEADER") +
            "</div><div class='modal-body'><div id='gliffy-desktop-register-errors' class='alert alert-error' style='display: none;'></div><div>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_INSTR") +
            "</div><form id='desktop-register-form'><input class='name' name='name' placeholder=\"" +
            $.i18n._("DESKTOP_REGISTER_MODAL_PLACEHOLDER_NAME") +
            "\" type='text'/><input class='email' name='email' placeholder=\"" +
            $.i18n._("DESKTOP_REGISTER_MODAL_PLACEHOLDER_EMAIL") +
            "\" type='text'/><input class='company' name='company' placeholder=\"" +
            $.i18n._("DESKTOP_REGISTER_MODAL_PLACEHOLDER_COMPANY") +
            "\" type='text'/><div><label class='checkbox'><input name='registered' type='checkbox'/>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_TERMS1") +
            " <a href='#' id='gliffy-desktop-eula-view'>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_TERMS2") +
            "</a></label></div><div><label class='checkbox'><input class='emailOptIn' name='emailOptIn' type='checkbox' checked='on'/>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_OPTIN") +
            "</label></div></form><div class='toolbar-bottom'><button id='gliffy-register-ok' class='btn gliffy-btn-primary'>" +
            $.i18n._("DESKTOP_REGISTER_MODAL_REGISTER") +
            "</button></div></div></div></div>"
        );
        $("#gliffy").append(e);
        $("#gliffy-desktop-register-errors").hide();
        if (f) {
          $.each(["name", "email", "company", "emailOptIn"], function (g, h) {
            if (f[h]) {
              if (h != "name" || (h == "name" && f[h] != "Trial User")) {
                $("#desktop-register-form ." + h).val(f[h]);
              }
            }
          });
        }
        $("#gliffy-desktop-eula-view")
          .click(function (g) {
            b.send("eula");
            g.preventDefault();
            return false;
          })
          .mousedown(function (g) {
            g.preventDefault();
            return false;
          });
        $("#gliffy-register-ok").click(function () {
          var g = {},
            h = [];
          $.each($("#desktop-register-form").serializeArray(), function (i, j) {
            g[j.name] = j.value;
          });
          if (!GLIFFY.onlineUtils.isValidEmail(g.email)) {
            h.push($.i18n._("DESKTOP_REGISTER_MODAL_ERROR_INVALID_EMAIL"));
          }
          if (g.name.length === 0) {
            h.push($.i18n._("DESKTOP_REGISTER_MODAL_ERROR_BLANK_NAME"));
          }
          if (!g.registered) {
            h.push($.i18n._("DESKTOP_REGISTER_MODAL_ERROR_ACCEPT_EULA"));
          } else {
            g.registered = true;
          }
          if (g.emailOptIn) {
            g.emailOptIn = true;
          }
          if (h.length > 0) {
            $("#gliffy-desktop-register-errors")
              .show()
              .html(
                "<h3>" +
                  $.i18n._("DESKTOP_REGISTER_MODAL_FIX_ERRORS") +
                  ":</h3><ul><li>" +
                  h.join("</li><li>") +
                  "</li></ul>"
              );
          } else {
            d.__sendDesktopData(g);
            e.modal("hide");
            d.__showTips();
          }
        });
        e.modal({ show: true, backdrop: "static", keyboard: false });
        $("#gliffy").append(e);
      },
      __showTips: function () {
        if (this.__hideTips !== true) {
          GliffyApp.tipsController.open();
        }
      },
      hide: function () {
        GliffyApp.tipsController.close();
      },
    });
  })();
  (function () {
    GliffyApp.bannerController = Ember.Object.create({
      integrations: null,
      init: function () {},
    });
  })();
  (function () {
    GliffyApp.ImageBrowserController = Ember.Object.extend({
      init: function () {},
      updateCounter: function () {},
      updateTotalImagesDisplay: function () {},
      buildImageShapes: function (b) {
        $("#gliffy-sidebar-com-gliffy-library-images").html(
          "<div>" + $.i18n._("DESKTOP_IMAGES_ADD_INSTR") + "</div>"
        );
      },
      buildImageShapesFromData: function (b) {},
      bindDragEvents: function (b) {},
      handlePreviousClick: function () {},
      handleNextClick: function () {},
    });
  })();
  (function () {
    GliffyApp.rateAppController = Ember.Object.create({
      message: Ember.computed(function () {
        if (this.get("messages") !== null) {
          if (this.launchCount <= 3) {
            return this.get("messages")[0];
          } else {
            if (this.launchCount <= 10) {
              return this.get("messages")[1];
            } else {
              if (this.launchCount <= 25) {
                return this.get("messages")[2];
              } else {
                if (this.launchCount <= 50) {
                  return this.get("messages")[3];
                } else {
                  return this.get("messages")[4];
                }
              }
            }
          }
        }
      }).property("launchCount"),
      launchCount: 0,
      userRated: false,
      messages: null,
      init: function () {},
      show: function (c, b) {
        this.messages = Ember.A([
          $.i18n._("DESKTOP_RATE_APP_MSG1"),
          $.i18n._("DESKTOP_RATE_APP_MSG2"),
          $.i18n._("DESKTOP_RATE_APP_MSG3"),
          $.i18n._("DESKTOP_RATE_APP_MSG4"),
          $.i18n._("DESKTOP_RATE_APP_MSG5"),
        ]);
        if (
          b ||
          (!this.userRated &&
            (this.launchCount === 3 ||
              this.launchCount === 10 ||
              this.launchCount % 25 === 0))
        ) {
          $("#gliffy-rate-app-modal")
            .unbind("hide")
            .on("hide", function () {
              c();
            })
            .modal("show");
        } else {
          c();
        }
        if (GLIFFY.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "rateApp",
            action: "show",
          });
        }
      },
      hide: function () {
        $("#gliffy-rate-app-modal").modal("hide");
      },
    });
  })();
  (function () {
    GliffyApp.gdriveInstallController = Ember.Object.create({
      init: function () {},
      show: function (c, b) {
        $("#gliffy-gdrive-install-modal").modal("show");
        if (GLIFFY.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "gdriveInstallModal",
            action: "show",
          });
        }
      },
      hide: function () {
        $("#gliffy-gdrive-install-modal").modal("hide");
      },
      install: function () {
        var b = window.open(
          "https://go.gliffy.com/go/gdrive/install?cid=" + GLIFFY.ENV.cid,
          "_blank"
        );
        if (window.focus) {
          b.focus();
        }
        $(".gliffy-nav-menus .dropdown-toggle").dropdown("toggle");
        if (GLIFFY.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktop",
            action: "driveInstall",
          });
        }
        return false;
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.feedbackController = Ember.Object.create({
      init: function () {},
      show: function () {
        $("#gliffy-feedback-modal").modal("show");
        $("#gliffy-feedback-modal input[name='fullname']").val(
          GLIFFY.ENV.userName
        );
        $("#gliffy-feedback-modal input[name='email']").val(GLIFFY.ENV.email);
        $("#gliffy-feedback-modal textarea").val("");
        $("#gliffy-feedback-modal input[name='performance']").attr(
          "checked",
          false
        );
        $("#gliffy-feedback-modal input[name='design']").attr("checked", false);
        setTimeout(function () {
          $("#gliffy-feedback-modal input[name='fullname']").focus();
        }, 100);
      },
      hide: function () {
        $("#gliffy-feedback-modal").modal("hide");
      },
      send: function () {
        var g,
          i,
          e,
          j,
          c,
          d = $("#gliffy-feedback-modal input[name='fullname']").val(),
          f = $("#gliffy-feedback-modal input[name='email']").val(),
          h;
        e = $("#gliffy-feedback-modal textarea").val();
        if (d === "") {
          d = "N/A";
        }
        if (f === "") {
          f = "N/A";
        }
        g = e.substring(0, 50);
        j = $("#gliffy-feedback-modal input[name='performance']:checked").val();
        c = $("#gliffy-feedback-modal input[name='design']:checked").val();
        i =
          "*Feedback:*\n" +
          i +
          "\n\n*" +
          $(".gliffy-feedback-performance-label").html() +
          ":* " +
          $("#gliffy-feedback-modal input[name='performance']:checked").val() +
          "\n\n*" +
          $(".gliffy-feedback-design-label").html() +
          ":* " +
          $("#gliffy-feedback-modal input[name='design']:checked").val() +
          "\n\n*CID:* " +
          GLIFFY.ENV.cid;
        h =
          "*Location*: " +
          window.location.href +
          " *App Version*: " +
          GLIFFY.ENV.productVersion +
          "\n *User-Agent*: " +
          window.navigator.userAgent +
          " *Screen Resolution*: " +
          screen.width +
          " X " +
          screen.height;
        b.send("feedback", {
          fullname: d,
          email: f,
          summary: g,
          description: i,
          webInfo: h,
          feedback: e,
          performanceRating: j,
          designRating: c,
        });
        this.hide();
        GLIFFY.Utils.notify(
          "success",
          $.i18n._("FEEDBACK_THANKS"),
          $.i18n._("FEEDBACK_APPRECIATION"),
          4000,
          $(window).width() / 2 - 125,
          null
        );
        GliffyApp.accountManagerController.updateAccountInfo(d, f);
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.accountManagerController = Ember.Object.create({
      init: function () {},
      showAccountModal: function () {
        $("#gliffy-account-modal").modal("show");
        $("#gliffy-account-modal input[name='fullname']").val(
          GLIFFY.ENV.userName
        );
        $("#gliffy-account-modal input[name='email']").val(GLIFFY.ENV.email);
      },
      closeAccountModal: function () {
        $("#gliffy-account-modal").modal("hide");
      },
      save: function (f) {
        var d = $("#gliffy-account-modal #account-fullname-input").val(),
          c = $("#gliffy-account-modal #account-email-input").val();
        this.updateAccountInfo(d, c);
        this.closeAccountModal();
      },
      updateAccountInfo: function (e, d) {
        var c = { name: GLIFFY.ENV.userName, email: GLIFFY.ENV.email };
        e = e.trim();
        d = d.trim();
        if (GLIFFY.ENV.userName !== e) {
          if (e == "") {
            e = "N/A";
          }
          Ember.set(GLIFFY.ENV, "userName", e);
          c.name = e;
        }
        if (GLIFFY.ENV.email !== d) {
          if (d == "") {
            d = "N/A";
          }
          Ember.set(GLIFFY.ENV, "email", d);
          c.email = d;
        }
        b.send("updateAccount", c);
      },
      isEmail: function (c) {
        var d = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return d.test(c);
      },
    });
  })();
  (function () {
    GliffyApp.HeaderView = Ember.View.extend({
      appName: Ember.computed(function () {
        return GLIFFY.ENV.appName || "Gliffy Demo App";
      }).property(),
      userNameBinding: Ember.Binding.oneWay("GLIFFY.ENV.userName"),
      accountNameBinding: Ember.Binding.oneWay("GLIFFY.ENV.accountName"),
      driveInstall: function () {
        GLIFFY.googleDriveManager.install();
        if (GLIFFY.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "menuClick",
            action: "installGoogleDrive",
          });
        }
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.BannerView = Ember.View.extend({
      productTypeBinding: Ember.Binding.oneWay("GLIFFY.ENV.productType"),
      expirationDateBinding: Ember.Binding.oneWay("GLIFFY.ENV.trialExpiration"),
      integrationsBinding: Ember.Binding.oneWay(
        "GliffyApp.bannerController.integrations"
      ),
      daysLeft: Ember.computed(function () {
        var e = this.get("expirationDate"),
          c = new Date(),
          d,
          f = 0;
        if (e) {
          e = new Date(e);
          d = e.getTime() - c.getTime();
          f = Math.floor(d / (1000 * 60 * 60 * 24));
        }
        if (f < 0) {
          f = 0;
        }
        if (f === 1) {
          return f + " day";
        } else {
          return f + " days";
        }
      }).property("expirationDate"),
      isAnonymousAccount: Ember.computed(function () {
        return false;
      }).property("productType"),
      isFreeAccount: Ember.computed(function () {
        return !GLIFFY.ENV.isTrial;
      }).property("productType"),
      isStandardAccount: Ember.computed(function () {
        return false;
      }).property("productType"),
      isTrialAccount: Ember.computed(function () {
        return GLIFFY.ENV.isTrial;
      }).property("productType"),
      didInsertElement: function (d) {
        var c = this;
        GLIFFY.ENV.productType = GLIFFY.ENV.productType | "";
        GLIFFY.ENV.trialExpiration =
          GLIFFY.ENV.trialExpiration | new Date().getTime();
        $(this.element).show();
        if (GLIFFY.editor) {
          GLIFFY.editor.updateContainerSize();
          this.addObserver("productType", function () {
            c.rerender();
            GLIFFY.editor.updateContainerSize();
          });
        } else {
          $("html").addClass("gliffy-banner");
          this.addObserver("productType", function () {
            if (
              GliffyApp.authController.isPremiumAccount() ||
              GliffyApp.authController.isAnonymousAccount()
            ) {
              $("html").removeClass("gliffy-banner");
            } else {
              $("html").addClass("gliffy-banner");
            }
          });
        }
      },
      handleCloseClick: function (c) {
        c.preventDefault();
        if (GLIFFY.editor) {
          this.$(".standard-account").hide();
          GLIFFY.editor.updateContainerSize();
        } else {
          $("html").removeClass("gliffy-banner");
        }
        GLIFFY.Utils.analyticsEvent("trackEvent", {
          category: "bannerAd",
          action: "close",
        });
        return false;
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.RateAppView = Ember.View.extend({
      messageBinding: Ember.Binding.oneWay(
        "GliffyApp.rateAppController.message"
      ),
      didInsertElement: function () {
        var c = this;
        this.$(".rate-app").click(function () {
          b.send("updateDesktopData", { gliffyDesktop: { userRated: true } });
          c.$(".modal").modal("hide");
          if (GLIFFY.editor.analyticsEnabled) {
            GLIFFY.Utils.analyticsEvent("trackEvent", {
              category: "rateApp",
              action: "clickToRate",
            });
          }
        });
      },
    });
  })();
  (function () {
    var b = new GLIFFY.Message();
    GliffyApp.GDriveInstallView = Ember.View.extend({
      install: function (c) {
        c.preventDefault();
        GliffyApp.gdriveInstallController.install();
      },
    });
  })();
  (function () {
    GliffyApp.FileMenuView = GliffyApp.FileMenuView.extend({
      menuSaveAs: function (b) {
        if (!b.isTrigger) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "menuClick",
            action: "saveAs",
          });
        }
        this.onMenuClick(b, "gliffy-toolbar-menu-file", false);
        GLIFFY.editor
          .getDocumentManager()
          .getActiveDocument()
          .setCurrentRevision(0);
        GLIFFY.editor.save();
      },
    });
  })();
  (function () {
    GliffyApp.FeedbackModalView = Ember.View.extend({
      didInsertElement: function () {},
      send: function () {
        GliffyApp.feedbackController.send();
      },
    });
  })();
  (function () {
    GliffyApp.AccountMenuView = Ember.View.extend({
      updateAccount: function (b) {
        GliffyApp.accountManagerController.showAccountModal();
        GLIFFY.Utils.analyticsEvent("trackEvent", {
          category: "menuClick",
          action: "updateAccount",
        });
      },
    });
  })();
  (function () {
    GliffyApp.AccountModalView = Ember.View.extend({
      save: function (b) {
        GliffyApp.accountManagerController.save(b);
        GLIFFY.Utils.analyticsEvent("trackEvent", {
          category: "accountDetails",
          action: "save",
        });
      },
    });
  })();
  (function () {
    var b = true,
      g = true,
      e = new GLIFFY.Message(),
      f = [
        "com.gliffy.stencil.marker_start.v1",
        "com.gliffy.stencil.marker_end.v1",
        "com.gliffy.stencil.marker_start.v2",
        "com.gliffy.stencil.marker_end.v2",
        "com.gliffy.stencil.marker_start.v3",
        "com.gliffy.stencil.marker_end.v3",
        "com.gliffy.stencil.marker_start.v4",
        "com.gliffy.stencil.marker_end.v4",
        "com.gliffy.stencil.marker_start.v5",
        "com.gliffy.stencil.marker_end.v5",
        "com.gliffy.stencil.marker_start.v6",
        "com.gliffy.stencil.marker_end.v6",
        "com.gliffy.stencil.marker_start.v7",
        "com.gliffy.stencil.marker_end.v7",
        "com.gliffy.stencil.marker_start.v8",
        "com.gliffy.stencil.marker_end.v8",
        "com.gliffy.stencil.marker_start.v9",
        "com.gliffy.stencil.marker_end.v9",
        "com.gliffy.stencil.marker_start.v10",
        "com.gliffy.stencil.marker_end.v10",
        "com.gliffy.stencil.marker_start.v11",
        "com.gliffy.stencil.marker_end.v11",
        "com.gliffy.stencil.marker_start.v12",
        "com.gliffy.stencil.marker_end.v12",
        "com.gliffy.stencil.marker_start.v13",
        "com.gliffy.stencil.marker_end.v13",
        "com.gliffy.stencil.marker_start.v14",
        "com.gliffy.stencil.marker_end.v14",
        "com.gliffy.stencil.marker_start.v15",
        "com.gliffy.stencil.marker_end.v15",
        "com.gliffy.stencil.marker_start.v16",
        "com.gliffy.stencil.marker_end.v16",
        "com.gliffy.stencil.marker_start.v17",
        "com.gliffy.stencil.marker_end.v17",
      ],
      d = { RGBColor: GLIFFY.RGBColor, candash: GLIFFY.candash },
      c = [
        "com.gliffy.shape.basic.basic_v1.default.group",
        "com.gliffy.shape.basic.basic_v1.default.selection",
      ];
    window.GLIFFY.shapeLibrary.loadSvgStencil = function (h, i, j) {};
    window.GLIFFY.shapeLibrary.loadLibraries = function (i) {
      var h = JSON.parse(
        '[{"uid":"com.gliffy.libraries","categories":[{"uid":"com.gliffy.libraries.basic","shapeSets":["com.gliffy.library.test.basic.basic_v1.default","com.gliffy.library.test.basic.basic_v2.default"]}],"shapeSets":[{"uid":"com.gliffy.libraries.basic.basic_v1.default","shapes":["com.gliffy.shape.basic.basic_v1.default.triangle","com.gliffy.shape.basic.basic_v1.default.ellipse","com.gliffy.shape.basic.basic_v1.default.rectangle","com.gliffy.shape.basic.basic_v1.default.circle","com.gliffy.shape.basic.basic_v1.default.square","com.gliffy.shape.basic.basic_v1.default.pentagon","com.gliffy.shape.basic.basic_v1.default.hexagon","com.gliffy.shape.basic.basic_v1.default.octagon","com.gliffy.shape.basic.basic_v1.default.right_triangle","com.gliffy.shape.basic.basic_v1.default.star","com.gliffy.shape.basic.basic_v1.default.round_rectangle","com.gliffy.shape.basic.basic_v1.default.cross","com.gliffy.shape.basic.basic_v1.default.left_arrow","com.gliffy.shape.basic.basic_v1.default.right_arrow","com.gliffy.shape.basic.basic_v1.default.double_arrow","com.gliffy.shape.basic.basic_v1.default.cylinder"]},{"uid":"com.gliffy.libraries.basic.basic_v2.default","shapes":["com.gliffy.shape.basic.basic_v2.default.triangle","com.gliffy.shape.basic.basic_v2.default.ellipse","com.gliffy.shape.basic.basic_v2.default.rectangle","com.gliffy.shape.basic.basic_v2.default.circle","com.gliffy.shape.basic.basic_v2.default.square","com.gliffy.shape.basic.basic_v2.default.pentagon","com.gliffy.shape.basic.basic_v2.default.hexagon","com.gliffy.shape.basic.basic_v2.default.octagon","com.gliffy.shape.basic.basic_v2.default.right_triangle","com.gliffy.shape.basic.basic_v2.default.star","com.gliffy.shape.basic.basic_v2.default.round_rectangle","com.gliffy.shape.basic.basic_v2.default.cross","com.gliffy.shape.basic.basic_v2.default.left_arrow","com.gliffy.shape.basic.basic_v2.default.right_arrow","com.gliffy.shape.basic.basic_v2.default.double_arrow","com.gliffy.shape.basic.basic_v2.default.cylinder"]},{"uid":"com.gliffy.libraries.flowchart.flowchart_v1.default","shapes":["com.gliffy.shape.flowchart.flowchart_v1.default.process","com.gliffy.shape.flowchart.flowchart_v1.default.decision","com.gliffy.shape.flowchart.flowchart_v1.default.document","com.gliffy.shape.flowchart.flowchart_v1.default.subroutine","com.gliffy.shape.flowchart.flowchart_v1.default.input_output","com.gliffy.shape.flowchart.flowchart_v1.default.paper_tape","com.gliffy.shape.flowchart.flowchart_v1.default.database","com.gliffy.shape.flowchart.flowchart_v1.default.data_storage","com.gliffy.shape.flowchart.flowchart_v1.default.merge","com.gliffy.shape.flowchart.flowchart_v1.default.multiple_documents","com.gliffy.shape.flowchart.flowchart_v1.default.manual_input","com.gliffy.shape.flowchart.flowchart_v1.default.manual_operation","com.gliffy.shape.flowchart.flowchart_v1.default.internal_storage","com.gliffy.shape.flowchart.flowchart_v1.default.tape_data","com.gliffy.shape.flowchart.flowchart_v1.default.loop_limit","com.gliffy.shape.flowchart.flowchart_v1.default.preparation","com.gliffy.shape.flowchart.flowchart_v1.default.card","com.gliffy.shape.flowchart.flowchart_v1.default.display","com.gliffy.shape.flowchart.flowchart_v1.default.delay","com.gliffy.shape.flowchart.flowchart_v1.default.start_end","com.gliffy.shape.flowchart.flowchart_v1.default.connector","com.gliffy.shape.flowchart.flowchart_v1.default.off_page_connector","com.gliffy.shape.flowchart.flowchart_v1.default.left_arrow","com.gliffy.shape.flowchart.flowchart_v1.default.right_arrow","com.gliffy.shape.flowchart.flowchart_v1.default.double_arrow"]},{"uid":"com.gliffy.libraries.flowchart.flowchart_v2.default","shapes":["com.gliffy.shape.flowchart.flowchart_v2.default.process","com.gliffy.shape.flowchart.flowchart_v2.default.decision","com.gliffy.shape.flowchart.flowchart_v2.default.document","com.gliffy.shape.flowchart.flowchart_v2.default.subroutine","com.gliffy.shape.flowchart.flowchart_v2.default.input_output","com.gliffy.shape.flowchart.flowchart_v2.default.paper_tape","com.gliffy.shape.flowchart.flowchart_v2.default.database","com.gliffy.shape.flowchart.flowchart_v2.default.data_storage","com.gliffy.shape.flowchart.flowchart_v2.default.merge","com.gliffy.shape.flowchart.flowchart_v2.default.multiple_documents","com.gliffy.shape.flowchart.flowchart_v2.default.manual_input","com.gliffy.shape.flowchart.flowchart_v2.default.manual_operation","com.gliffy.shape.flowchart.flowchart_v2.default.internal_storage","com.gliffy.shape.flowchart.flowchart_v2.default.tape_data","com.gliffy.shape.flowchart.flowchart_v2.default.loop_limit","com.gliffy.shape.flowchart.flowchart_v2.default.preparation","com.gliffy.shape.flowchart.flowchart_v2.default.card","com.gliffy.shape.flowchart.flowchart_v2.default.display","com.gliffy.shape.flowchart.flowchart_v2.default.delay","com.gliffy.shape.flowchart.flowchart_v2.default.start_end","com.gliffy.shape.flowchart.flowchart_v2.default.connector","com.gliffy.shape.flowchart.flowchart_v2.default.off_page_connector","com.gliffy.shape.flowchart.flowchart_v2.default.left_arrow","com.gliffy.shape.flowchart.flowchart_v2.default.right_arrow","com.gliffy.shape.flowchart.flowchart_v2.default.double_arrow"]},{"uid":"com.gliffy.libraries.network.network_v3.home","shapes":["com.gliffy.shape.network.network_v3.home.firewall","com.gliffy.shape.network.network_v3.home.cloud","com.gliffy.shape.network.network_v3.home.comm_link","com.gliffy.shape.network.network_v3.home.server","com.gliffy.shape.network.network_v3.home.computer","com.gliffy.shape.network.network_v3.home.laptop","com.gliffy.shape.network.network_v3.home.monitor_crt","com.gliffy.shape.network.network_v3.home.monitor_lcd","com.gliffy.shape.network.network_v3.home.cellphone","com.gliffy.shape.network.network_v3.home.printer","com.gliffy.shape.network.network_v3.home.scanner","com.gliffy.shape.network.network_v3.home.fax","com.gliffy.shape.network.network_v3.home.external_storage","com.gliffy.shape.network.network_v3.home.pda","com.gliffy.shape.network.network_v3.home.telephone","com.gliffy.shape.network.network_v3.home.hub","com.gliffy.shape.network.network_v3.home.iphone","com.gliffy.shape.network.network_v3.home.modem","com.gliffy.shape.network.network_v3.home.wireless_modem","com.gliffy.shape.network.network_v3.home.router","com.gliffy.shape.network.network_v3.home.game_system","com.gliffy.shape.network.network_v3.home.outlet","com.gliffy.shape.network.network_v3.home.stereo","com.gliffy.shape.network.network_v3.home.speakers","com.gliffy.shape.network.network_v3.home.subwoofer","com.gliffy.shape.network.network_v3.home.tv","com.gliffy.shape.network.network_v3.home.tv_flatscreen"]},{"uid":"com.gliffy.libraries.network.network_v3.business","shapes":["com.gliffy.shape.network.network_v3.business.firewall","com.gliffy.shape.network.network_v3.business.cloud","com.gliffy.shape.network.network_v3.business.comm_link","com.gliffy.shape.network.network_v3.business.user","com.gliffy.shape.network.network_v3.business.user_female","com.gliffy.shape.network.network_v3.business.user_male","com.gliffy.shape.network.network_v3.business.user_group","com.gliffy.shape.network.network_v3.business.server","com.gliffy.shape.network.network_v3.business.database_server","com.gliffy.shape.network.network_v3.business.mail_server","com.gliffy.shape.network.network_v3.business.proxy_server","com.gliffy.shape.network.network_v3.business.web_server","com.gliffy.shape.network.network_v3.business.database","com.gliffy.shape.network.network_v3.business.hub","com.gliffy.shape.network.network_v3.business.switch","com.gliffy.shape.network.network_v3.business.router","com.gliffy.shape.network.network_v3.business.workstation_crt","com.gliffy.shape.network.network_v3.business.workstation_lcd","com.gliffy.shape.network.network_v3.business.laptop","com.gliffy.shape.network.network_v3.business.printer","com.gliffy.shape.network.network_v3.business.copier","com.gliffy.shape.network.network_v3.business.mainframe","com.gliffy.shape.network.network_v3.business.rack_server_1u","com.gliffy.shape.network.network_v3.business.multi_u_server","com.gliffy.shape.network.network_v3.business.rack","com.gliffy.shape.network.network_v3.business.telephone","com.gliffy.shape.network.network_v3.business.flash_drive","com.gliffy.shape.network.network_v3.business.tape_backup","com.gliffy.shape.network.network_v3.business.video_projector","com.gliffy.shape.network.network_v3.business.video_screen","com.gliffy.shape.network.network_v3.business.satellite","com.gliffy.shape.network.network_v3.business.satellite_antenna"]},{"uid":"com.gliffy.libraries.network.network_v3.rack","shapes":["com.gliffy.shape.network.network_v3.rack.rack_server_1u","com.gliffy.shape.network.network_v3.rack.multi_u_server","com.gliffy.shape.network.network_v3.rack.hub_switch_1u","com.gliffy.shape.network.network_v3.rack.kvm_switch_1u","com.gliffy.shape.network.network_v3.rack.power_strip_1u","com.gliffy.shape.network.network_v3.rack.spacer_1u","com.gliffy.shape.network.network_v3.rack.tray_1u","com.gliffy.shape.network.network_v3.rack.hub_switch_2u","com.gliffy.shape.network.network_v3.rack.lcd_7u","com.gliffy.shape.network.network_v3.rack.rack"]},{"uid":"com.gliffy.libraries.sitemap.sitemap_v1.default","shapes":["com.gliffy.shape.sitemap.sitemap_v1.default.page","com.gliffy.shape.sitemap.sitemap_v1.default.home_page","com.gliffy.shape.sitemap.sitemap_v1.default.form","com.gliffy.shape.sitemap.sitemap_v1.default.login","com.gliffy.shape.sitemap.sitemap_v1.default.search","com.gliffy.shape.sitemap.sitemap_v1.default.site_map","com.gliffy.shape.sitemap.sitemap_v1.default.script","com.gliffy.shape.sitemap.sitemap_v1.default.flash","com.gliffy.shape.sitemap.sitemap_v1.default.video","com.gliffy.shape.sitemap.sitemap_v1.default.game","com.gliffy.shape.sitemap.sitemap_v1.default.document","com.gliffy.shape.sitemap.sitemap_v1.default.pdf","com.gliffy.shape.sitemap.sitemap_v1.default.word_document","com.gliffy.shape.sitemap.sitemap_v1.default.excel_document","com.gliffy.shape.sitemap.sitemap_v1.default.powerpoint_document","com.gliffy.shape.sitemap.sitemap_v1.default.chat","com.gliffy.shape.sitemap.sitemap_v1.default.gliffy","com.gliffy.shape.sitemap.sitemap_v1.default.print","com.gliffy.shape.sitemap.sitemap_v1.default.shopping_cart","com.gliffy.shape.sitemap.sitemap_v1.default.download","com.gliffy.shape.sitemap.sitemap_v1.default.link","com.gliffy.shape.sitemap.sitemap_v1.default.share","com.gliffy.shape.sitemap.sitemap_v1.default.database","com.gliffy.shape.sitemap.sitemap_v1.default.secure_https","com.gliffy.shape.sitemap.sitemap_v1.default.email"]},{"uid":"com.gliffy.libraries.bpmn.bpmn_v1.events","shapes":["com.gliffy.shape.bpmn.bpmn_v1.events.general_start","com.gliffy.shape.bpmn.bpmn_v1.events.general_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.general_end","com.gliffy.shape.bpmn.bpmn_v1.events.message_start","com.gliffy.shape.bpmn.bpmn_v1.events.message_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.message_end","com.gliffy.shape.bpmn.bpmn_v1.events.timer_start","com.gliffy.shape.bpmn.bpmn_v1.events.timer_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.cancel_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.cancel_end","com.gliffy.shape.bpmn.bpmn_v1.events.terminate","com.gliffy.shape.bpmn.bpmn_v1.events.link_start","com.gliffy.shape.bpmn.bpmn_v1.events.link_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.link_end","com.gliffy.shape.bpmn.bpmn_v1.events.error_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.error_end","com.gliffy.shape.bpmn.bpmn_v1.events.compensation_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.compensation_end","com.gliffy.shape.bpmn.bpmn_v1.events.rule_start","com.gliffy.shape.bpmn.bpmn_v1.events.rule_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.multiple_start","com.gliffy.shape.bpmn.bpmn_v1.events.multiple_intermediate","com.gliffy.shape.bpmn.bpmn_v1.events.multiple_end"]},{"uid":"com.gliffy.libraries.bpmn.bpmn_v1.activities","shapes":["com.gliffy.shape.bpmn.bpmn_v1.activities.task","com.gliffy.shape.bpmn.bpmn_v1.activities.collapsed_sub_process","com.gliffy.shape.bpmn.bpmn_v1.activities.expanded_sub_process","com.gliffy.shape.bpmn.bpmn_v1.activities.transaction","com.gliffy.shape.bpmn.bpmn_v1.activities.process","com.gliffy.shape.bpmn.bpmn_v1.activities.looping","com.gliffy.shape.bpmn.bpmn_v1.activities.ad_hoc","com.gliffy.shape.bpmn.bpmn_v1.activities.compensation","com.gliffy.shape.bpmn.bpmn_v1.activities.multiple_instances"]},{"uid":"com.gliffy.libraries.bpmn.bpmn_v1.data_artifacts","shapes":["com.gliffy.shape.bpmn.bpmn_v1.data_artifacts.annotation","com.gliffy.shape.bpmn.bpmn_v1.data_artifacts.data_object","com.gliffy.shape.bpmn.bpmn_v1.data_artifacts.group"]},{"uid":"com.gliffy.libraries.bpmn.bpmn_v1.gateways","shapes":["com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway","com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway_xor_data","com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway_and","com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway_xor_event","com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway_complex","com.gliffy.shape.bpmn.bpmn_v1.gateways.gateway_or"]},{"uid":"com.gliffy.libraries.bpmn.bpmn_v1.connectors","shapes":["com.gliffy.shape.bpmn.bpmn_v1.connectors.normal_sequence_flow","com.gliffy.shape.bpmn.bpmn_v1.connectors.message_flow","com.gliffy.shape.bpmn.bpmn_v1.connectors.default_sequence_flow","com.gliffy.shape.bpmn.bpmn_v1.connectors.conditional_sequence_flow","com.gliffy.shape.bpmn.bpmn_v1.connectors.exception_flow","com.gliffy.shape.bpmn.bpmn_v1.connectors.association","com.gliffy.shape.bpmn.bpmn_v1.connectors.association_with_arrow"]},{"uid":"com.gliffy.libraries.venn.radial.default","shapes":["com.gliffy.shape.venn.radial.default.99CC99","com.gliffy.shape.venn.radial.default.99CCFF","com.gliffy.shape.venn.radial.default.FFFF99","com.gliffy.shape.venn.radial.default.999999","com.gliffy.shape.venn.radial.default.9999FF","com.gliffy.shape.venn.radial.default.99CC33","com.gliffy.shape.venn.radial.default.FF9900","com.gliffy.shape.venn.radial.default.666666","com.gliffy.shape.venn.radial.default.999900","com.gliffy.shape.venn.radial.default.FF0000","com.gliffy.shape.venn.radial.default.0066CC","com.gliffy.shape.venn.radial.default.FFFF00"]},{"uid":"com.gliffy.libraries.venn.flat.default","shapes":["com.gliffy.shape.venn.flat.default.99CC99","com.gliffy.shape.venn.flat.default.99CCFF","com.gliffy.shape.venn.flat.default.FFFF99","com.gliffy.shape.venn.flat.default.999999","com.gliffy.shape.venn.flat.default.9999FF","com.gliffy.shape.venn.flat.default.99CC33","com.gliffy.shape.venn.flat.default.FF9900","com.gliffy.shape.venn.flat.default.666666","com.gliffy.shape.venn.flat.default.999900","com.gliffy.shape.venn.flat.default.FF0000","com.gliffy.shape.venn.flat.default.0066CC","com.gliffy.shape.venn.flat.default.FFFF00"]},{"uid":"com.gliffy.libraries.venn.outline.default","shapes":["com.gliffy.shape.venn.outline.default.99CC99","com.gliffy.shape.venn.outline.default.99CCFF","com.gliffy.shape.venn.outline.default.FFFF99","com.gliffy.shape.venn.outline.default.999999","com.gliffy.shape.venn.outline.default.9999FF","com.gliffy.shape.venn.outline.default.99CC33","com.gliffy.shape.venn.outline.default.FF9900","com.gliffy.shape.venn.outline.default.666666","com.gliffy.shape.venn.outline.default.999900","com.gliffy.shape.venn.outline.default.FF0000","com.gliffy.shape.venn.outline.default.0066CC","com.gliffy.shape.venn.outline.default.FFFF00"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.structure","shapes":["com.gliffy.shape.floorplan.floorplan_v2.structure.door1","com.gliffy.shape.floorplan.floorplan_v2.structure.door2","com.gliffy.shape.floorplan.floorplan_v2.structure.wall_horizontal","com.gliffy.shape.floorplan.floorplan_v2.structure.wall_vertical","com.gliffy.shape.floorplan.floorplan_v2.structure.window_single","com.gliffy.shape.floorplan.floorplan_v2.structure.dimension","com.gliffy.shape.floorplan.floorplan_v2.structure.stairs1","com.gliffy.shape.floorplan.floorplan_v2.structure.stairs2","com.gliffy.shape.floorplan.floorplan_v2.structure.elevator","com.gliffy.shape.floorplan.floorplan_v2.structure.fireplace","com.gliffy.shape.floorplan.floorplan_v2.structure.tile_floor","com.gliffy.shape.floorplan.floorplan_v2.structure.wood_floor","com.gliffy.shape.floorplan.floorplan_v2.structure.light_wood_floor"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.bedroom","shapes":["com.gliffy.shape.floorplan.floorplan_v2.bedroom.bed_double","com.gliffy.shape.floorplan.floorplan_v2.bedroom.bed_queen","com.gliffy.shape.floorplan.floorplan_v2.bedroom.bed_single","com.gliffy.shape.floorplan.floorplan_v2.bedroom.dresser","com.gliffy.shape.floorplan.floorplan_v2.bedroom.armoire","com.gliffy.shape.floorplan.floorplan_v2.bedroom.lamp"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.living_room","shapes":["com.gliffy.shape.floorplan.floorplan_v2.living_room.coffee_table","com.gliffy.shape.floorplan.floorplan_v2.living_room.bookcase","com.gliffy.shape.floorplan.floorplan_v2.living_room.floor_lamp","com.gliffy.shape.floorplan.floorplan_v2.living_room.lamp","com.gliffy.shape.floorplan.floorplan_v2.living_room.side_table","com.gliffy.shape.floorplan.floorplan_v2.living_room.couch","com.gliffy.shape.floorplan.floorplan_v2.living_room.plant","com.gliffy.shape.floorplan.floorplan_v2.living_room.flat_tv","com.gliffy.shape.floorplan.floorplan_v2.living_room.tv","com.gliffy.shape.floorplan.floorplan_v2.living_room.loveseat","com.gliffy.shape.floorplan.floorplan_v2.living_room.small_loveseat","com.gliffy.shape.floorplan.floorplan_v2.living_room.ottoman","com.gliffy.shape.floorplan.floorplan_v2.living_room.circular_rug","com.gliffy.shape.floorplan.floorplan_v2.living_room.rectangular_rug"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.dining_room","shapes":["com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_wood","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_glass","com.gliffy.shape.floorplan.floorplan_v2.dining_room.chair","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_4_chairs","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_4_chairs_square","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_6_chairs_wood","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_6_chairs","com.gliffy.shape.floorplan.floorplan_v2.dining_room.table_circular"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.kitchen","shapes":["com.gliffy.shape.floorplan.floorplan_v2.kitchen.stool","com.gliffy.shape.floorplan.floorplan_v2.kitchen.range","com.gliffy.shape.floorplan.floorplan_v2.kitchen.refrigerator","com.gliffy.shape.floorplan.floorplan_v2.kitchen.sink_single1","com.gliffy.shape.floorplan.floorplan_v2.kitchen.sink_single2","com.gliffy.shape.floorplan.floorplan_v2.kitchen.sink_double_2d"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.bathroom","shapes":["com.gliffy.shape.floorplan.floorplan_v2.bathroom.bathtub","com.gliffy.shape.floorplan.floorplan_v2.bathroom.toilet","com.gliffy.shape.floorplan.floorplan_v2.bathroom.shower","com.gliffy.shape.floorplan.floorplan_v2.bathroom.sink_single1","com.gliffy.shape.floorplan.floorplan_v2.bathroom.sink_single2"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.office","shapes":["com.gliffy.shape.floorplan.floorplan_v2.office.cubicle","com.gliffy.shape.floorplan.floorplan_v2.office.wall_horizontal","com.gliffy.shape.floorplan.floorplan_v2.office.wall_vertical","com.gliffy.shape.floorplan.floorplan_v2.office.office_chair","com.gliffy.shape.floorplan.floorplan_v2.office.computer_crt","com.gliffy.shape.floorplan.floorplan_v2.office.desk_l","com.gliffy.shape.floorplan.floorplan_v2.office.desk_office","com.gliffy.shape.floorplan.floorplan_v2.office.desk_metal","com.gliffy.shape.floorplan.floorplan_v2.office.desk_metal_l","com.gliffy.shape.floorplan.floorplan_v2.office.computer_lcd","com.gliffy.shape.floorplan.floorplan_v2.office.copier","com.gliffy.shape.floorplan.floorplan_v2.office.desk_lamp","com.gliffy.shape.floorplan.floorplan_v2.office.laptop","com.gliffy.shape.floorplan.floorplan_v2.office.telephone","com.gliffy.shape.floorplan.floorplan_v2.office.printer","com.gliffy.shape.floorplan.floorplan_v2.office.scanner","com.gliffy.shape.floorplan.floorplan_v2.office.table_3person","com.gliffy.shape.floorplan.floorplan_v2.office.table_6person","com.gliffy.shape.floorplan.floorplan_v2.office.table_10person","com.gliffy.shape.floorplan.floorplan_v2.office.vending_machine","com.gliffy.shape.floorplan.floorplan_v2.office.video_projector","com.gliffy.shape.floorplan.floorplan_v2.office.video_screen","com.gliffy.shape.floorplan.floorplan_v2.office.water_cooler","com.gliffy.shape.floorplan.floorplan_v2.office.plant"]},{"uid":"com.gliffy.libraries.floorplan.floorplan_v2.miscellaneous","shapes":["com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.washing_machine","com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.dryer","com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.piano","com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.pool_table","com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.drumset","com.gliffy.shape.floorplan.floorplan_v2.miscellaneous.gas_furnace"]},{"uid":"com.gliffy.libraries.uml.uml_v1.default","shapes":["com.gliffy.shape.uml.uml_v1.default.package","com.gliffy.shape.uml.uml_v1.default.class","com.gliffy.shape.uml.uml_v1.default.simple_class","com.gliffy.shape.uml.uml_v1.default.note","com.gliffy.shape.uml.uml_v1.default.object","com.gliffy.shape.uml.uml_v1.default.interface","com.gliffy.shape.uml.uml_v1.default.node","com.gliffy.shape.uml.uml_v1.default.component","com.gliffy.shape.uml.uml_v1.default.generalization","com.gliffy.shape.uml.uml_v1.default.implements","com.gliffy.shape.uml.uml_v1.default.association","com.gliffy.shape.uml.uml_v1.default.aggregation","com.gliffy.shape.uml.uml_v1.default.composition","com.gliffy.shape.uml.uml_v1.default.dependency","com.gliffy.shape.uml.uml_v1.default.object_timeline","com.gliffy.shape.uml.uml_v1.default.activation","com.gliffy.shape.uml.uml_v1.default.lifeline","com.gliffy.shape.uml.uml_v1.default.message","com.gliffy.shape.uml.uml_v1.default.self_message","com.gliffy.shape.uml.uml_v1.default.actor","com.gliffy.shape.uml.uml_v1.default.use_case"]},{"uid":"com.gliffy.libraries.erd.erd_v1.default","shapes":["com.gliffy.shape.erd.erd_v1.default.entity_with_attributes","com.gliffy.shape.erd.erd_v1.default.entity_with_multiple_attributes","com.gliffy.shape.erd.erd_v1.default.attribute","com.gliffy.shape.erd.erd_v1.default.entity","com.gliffy.shape.erd.erd_v1.default.relation","com.gliffy.shape.erd.erd_v1.default.zero_many_optional","com.gliffy.shape.erd.erd_v1.default.one_many","com.gliffy.shape.erd.erd_v1.default.one_mandatory","com.gliffy.shape.erd.erd_v1.default.one_one","com.gliffy.shape.erd.erd_v1.default.one","com.gliffy.shape.erd.erd_v1.default.zero_one","com.gliffy.shape.erd.erd_v1.default.many","com.gliffy.shape.erd.erd_v1.default.many_many","com.gliffy.shape.erd.erd_v1.default.one_optional_many_optional","com.gliffy.shape.erd.erd_v1.default.one_mandatory_many_optional","com.gliffy.shape.erd.erd_v1.default.one_mandatory_many_mandatory","com.gliffy.shape.erd.erd_v1.default.one_optional_many_mandatory","com.gliffy.shape.erd.erd_v1.default.many_mandatory_many_mandatory","com.gliffy.shape.erd.erd_v1.default.many_optional_many_mandatory"]},{"uid":"com.gliffy.libraries.ui.ui_v2.forms_components","shapes":["com.gliffy.shape.ui.ui_v2.forms_components.window","com.gliffy.shape.ui.ui_v2.forms_components.dialog_box","com.gliffy.shape.ui.ui_v2.forms_components.listbox","com.gliffy.shape.ui.ui_v2.forms_components.scrollbar_vertical","com.gliffy.shape.ui.ui_v2.forms_components.scrollbar_horizontal","com.gliffy.shape.ui.ui_v2.forms_components.text_area","com.gliffy.shape.ui.ui_v2.forms_components.slider_vertical","com.gliffy.shape.ui.ui_v2.forms_components.slider_horizontal","com.gliffy.shape.ui.ui_v2.forms_components.button","com.gliffy.shape.ui.ui_v2.forms_components.radio_button","com.gliffy.shape.ui.ui_v2.forms_components.checkbox","com.gliffy.shape.ui.ui_v2.forms_components.combobox","com.gliffy.shape.ui.ui_v2.forms_components.datepicker","com.gliffy.shape.ui.ui_v2.forms_components.colorpicker","com.gliffy.shape.ui.ui_v2.forms_components.textbox"]},{"uid":"com.gliffy.libraries.ui.ui_v2.content","shapes":["com.gliffy.shape.ui.ui_v2.content.area_box","com.gliffy.shape.ui.ui_v2.content.plain_box","com.gliffy.shape.ui.ui_v2.content.image","com.gliffy.shape.ui.ui_v2.content.street_map","com.gliffy.shape.ui.ui_v2.content.video_player","com.gliffy.shape.ui.ui_v2.content.progress_bar","com.gliffy.shape.ui.ui_v2.content.bar_chart","com.gliffy.shape.ui.ui_v2.content.pie_chart","com.gliffy.shape.ui.ui_v2.content.column_chart","com.gliffy.shape.ui.ui_v2.content.line_chart"]},{"uid":"com.gliffy.libraries.ui.ui_v2.miscellaneous","shapes":["com.gliffy.shape.ui.ui_v2.miscellaneous.note","com.gliffy.shape.ui.ui_v2.miscellaneous.notes_reference","com.gliffy.shape.ui.ui_v2.miscellaneous.rule_horizontal","com.gliffy.shape.ui.ui_v2.miscellaneous.rule_vertical","com.gliffy.shape.ui.ui_v2.miscellaneous.rule_horizontal_dotted","com.gliffy.shape.ui.ui_v2.miscellaneous.rule_vertical_dotted","com.gliffy.shape.ui.ui_v2.miscellaneous.left_arrow","com.gliffy.shape.ui.ui_v2.miscellaneous.right_arrow","com.gliffy.shape.ui.ui_v2.miscellaneous.page_previous","com.gliffy.shape.ui.ui_v2.miscellaneous.page_next"]},{"uid":"com.gliffy.libraries.swimlanes.swimlanes_v1.default","shapes":["com.gliffy.shape.swimlanes.swimlanes_v1.default.vertical_single_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.horizontal_single_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.vertical_double_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.horizontal_double_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.vertical_triple_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.horizontal_triple_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.vertical_four_lane_pool","com.gliffy.shape.swimlanes.swimlanes_v1.default.horizontal_four_lane_pool"]}]}]'
      );
      h.done = function (j) {
        j(h);
      };
      return h;
    };
    window.GLIFFY.shapeLibrary.loadStencils = function (j, h) {
      var m = "desktop",
        p,
        l = this,
        n = 0,
        o,
        k = [];
      j = j || [];
      if (!this.markersLoaded) {
        this.markersLoaded = true;
        j = j.concat(f);
      }
      for (n = 0; n < j.length; n++) {
        o = j[n];
        if (!l.templates[o] && $.inArray(o, k) === -1) {
          k.push(o);
        }
      }
      if (k.length > 0) {
        p = $.Deferred(function (i) {
          e.send("getStencils", k, i.resolve, i.reject);
        }).promise();
        p.then(
          function (i, r, q) {
            GLIFFY.Utils.tryCatch(
              function () {
                var t, s;
                for (t = 0; t < i.length; t++) {
                  s = i[t];
                  l.__addStencil(s);
                }
              },
              function (s) {
                GLIFFY.handleError("com.gliffy.error.stencil_load", [m], s, h);
              }
            );
          },
          function (r, q, i) {
            GLIFFY.handleError("com.gliffy.error.stencil_load", [m], i, h);
          }
        );
      } else {
        p = $.Deferred(function (i) {
          i.resolve();
        }).promise();
      }
      return p;
    };
    window.GLIFFY.shapeLibrary.loadShapes = function (r, n, l) {
      var m,
        o,
        j = "desktop",
        p = this,
        k = [],
        h = [],
        q;
      r = r || [];
      for (m = 0; m < r.length; m++) {
        o = r[m];
        if (!p.shapes[o] && $.inArray(o, k) === -1 && $.inArray(o, c) === -1) {
          k.push(o);
        }
      }
      if (k.length > 0) {
        q = $.Deferred(function (i) {
          e.send("getShapes", k, i.resolve, i.fail);
        }).promise();
        q.then(
          function (i, t, s) {
            GLIFFY.Utils.tryCatch(
              function () {
                var v, u;
                for (v = 0; v < i.length; v++) {
                  u = i[v];
                  p.shapes[u.uid] = u;
                }
              },
              function (u) {
                GLIFFY.handleError("com.gliffy.error.shape_load", [j], u, n);
              }
            );
            return h;
          },
          function (t, s, i) {
            GLIFFY.handleError("com.gliffy.error.shape_load", [j], i, n);
          }
        );
      } else {
        q = $.Deferred(function (i) {
          i.resolve();
        }).promise();
      }
      return q;
    };
  })();
  (function () {
    var b = new GLIFFY.Message();

    function c(d) {
      var e = JSON.stringify(d);
      b.send("setLocalStorage", { clipboard: e });
    }

    GLIFFY.onlineUtils.postdecorate(GLIFFY.CopyCommand, "execute", function (
      e,
      d
    ) {
      c(this.nodeObject);
    });
    GLIFFY.onlineUtils.postdecorate(GLIFFY.CutCommand, "execute", function (
      e,
      d
    ) {
      c(this.nodeObject);
    });
    GLIFFY.onlineUtils.override(GLIFFY.PasteCommand, "getNode", function (
      d,
      e
    ) {
      b.send("getLocalStorage", "clipboard", function (f) {
        var h = new GLIFFY.Node();
        var g = JSON.parse(f.clipboard);
        h.fromObject(g);
        e(h);
      });
    });
  })();
  (function () {
    GLIFFY.onlineUtils.predecorate(GLIFFY.Text, "fromObject", function (b) {
      var c = b.html;
      try {
        c = sanitize(c).xss();
      } catch (d) {
        c = '<p><span style="color:#ff0000;">Text was removed.</span></p>';
      }
      b.html = c;
    });
  })();
  var a = {};
  document.__defineSetter__("cookie", function (b) {
    if (b.indexOf(";") < 0) {
      return;
    }
    var c = b.substring(0, b.indexOf("="));
    var d = b.substring(c.length + 1, b.indexOf(";"));
    a[c] = d;
  });
  document.__defineGetter__("cookie", function () {
    var b = [];
    for (var c in a) {
      b.push(c + "=" + a[c]);
    }
    return b.join("; ");
  });
  history.__defineGetter__("length", function () {
    return 0;
  });
  (function () {
    var b = new GLIFFY.Message();
    window.GLIFFY.googleDriveManager = {
      ready: function () {
        window.addEventListener(
          "online",
          function (c) {
            $("#menu-drive-install").removeClass("disabled");
            $("#menu-drive-view").removeClass("disabled");
          },
          false
        );
        window.addEventListener(
          "offline",
          function (c) {
            $("#menu-drive-install").addClass("disabled");
            $("#menu-drive-view").addClass("disabled");
          },
          false
        );
        if (navigator.onLine == true) {
          $("#menu-drive-install").removeClass("disabled");
          $("#menu-drive-view").removeClass("disabled");
        }
      },
      install: function () {
        GliffyApp.gdriveInstallController.show();
        return false;
      },
    };
  })();
  $(function () {
    var g,
      e,
      m,
      i,
      h,
      l,
      o = new GLIFFY.Message();
    var c = "en",
      k = navigator.language.substring(0, 2);
    if ($.inArray(k, ["fr", "de", "ru"]) != -1) {
      c = k;
    }
    if (c == "en") {
      $.i18n.setDictionary(window.GLIFFY.DICTIONARY.en);
    } else {
      $.i18n.setDictionary(
        $.extend({}, window.GLIFFY.DICTIONARY.en, window.GLIFFY.DICTIONARY[c])
      );
    }
    GLIFFY.Renderer.StageSingleCanvas.prototype.boundingBoxFit = true;
    GLIFFY.setErrorHandler({
      report: function (q, p, r) {
        GLIFFY.Dialog.showAlertDialog({ content: q, title: "Error!" });
        if (p) {
          throw p;
        }
      },
    });
    o.registerReceiveActions({
      identity: function (p, r, q) {
        f(p);
        r();
      },
      version: function (p) {
        GLIFFY.ENV.productVersion = p;
      },
    });

    function j() {
      var r, q, p;
      if (h.getDocumentManager().getActiveDocument().getUrls().tempId) {
        $("#gliffy-desktop-draft-notice").show();
      } else {
        $("#gliffy-desktop-draft-notice").hide();
      }
      p = GliffyApp.documentModel.get("unsavedChanges") === true ? "*" : "";
      if (
        h.getDocumentManager().getActiveDocument().getCurrentRevision() !== 0
      ) {
        r = GliffyApp.documentModel.get("title");
      } else {
        if (!b(h.getDocumentManager().getActiveDocument())) {
          r = $.i18n._("DOCUMENT_NAME_UNTITLED");
        } else {
          r = GliffyApp.documentModel.get("title");
        }
        p = b(h.getDocumentManager().getActiveDocument()) ? "*" : p;
      }
      $("#gliffy-file-name")
        .html("")
        .text(GliffyApp.documentModel.get("displayTitle"));
      o.send("setTitle", r);
    }

    g = GLIFFY.IntegrationHandler.extend({
      closeLastTab: function (p) {
        o.send("close", document);
      },
      checkDraft: function (q, p, r) {
        p([]);
      },
      saveDraft: function (q, p, r) {
        console.log("auto saving...");
        if (!q.urls.tempId) {
          q.urls.tempId = new Date().getTime();
        }
        h.getDocumentManager().getActiveDocument().refreshFromObject(q);
        o.send(
          "saveTemp",
          q,
          function () {
            console.log("auto saved!");
            p({
              id: q.title,
              createDate: new Date().getTime(),
              friendlyDate: new Date().toString("HH:mm:ss"),
              location: q.title,
            });
          },
          r
        );
      },
      getDraft: function (r, p, q) {
        q();
      },
      deleteDraft: function (q, p, r) {
        p();
      },
    });
    e = {
      init: function (p) {
        this.editor = p;
        GliffyApp.set("documentModel", GliffyApp.DocumentModel.create());
      },
      export: function (s, p, q) {
        var r = s.data || null;
        if (r) {
          o.send("export", {
            name: GLIFFY.editor
              .getDocumentManager()
              .getActiveDocument()
              .getTitle(),
            ext: s.extension,
            contentType: s.contentType,
            imageBase64: r,
          });
        } else {
        }
      },
      clickAddTab: function () {},
      clickFeedback: function () {
        GliffyApp.feedbackController.show();
      },
      showFileOpenBrowser: function () {
        if (GLIFFY.FEATURES.useFileBrowser) {
          GliffyApp.fileOpenBrowserController.show();
        } else {
          o.send(
            "open",
            null,
            function (p) {
              if (p !== null) {
                if (
                  h.getDocumentManager().getActiveDocument().isNewNotDirty()
                ) {
                  h.open({
                    url: p,
                    scale: 1,
                    callback: function () {
                      GliffyApp.documentModel.update(
                        h.getDocumentManager().getActiveDocument()
                      );
                    },
                  });
                } else {
                  o.send("newWindow", p);
                }
              }
            },
            function (p, q) {
              GLIFFY.Dialog.showAlertDialog({ content: q, title: "Oops!" });
            }
          );
        }
      },
      documentContainerList: function (r, p, q) {
        p([
          { name: "ROOT", url: "http://localhost:8090/gon/" },
          {
            name: "basic",
            url: "http://localhost:8090/gon/basic/",
          },
          { name: "flowchart", url: "http://localhost:8090/gon/flowchart/" },
        ]);
      },
      documentList: function (r, p, q) {
        p([
          {
            name: "bob",
            url: "bob",
            lastModified: "today",
            image: "images/rate-app.png",
          },
        ]);
      },
      handleFileNew: function () {
        o.send("newWindow", null);
      },
      create: function (q, p, r) {
        GliffyApp.set("documentModel", GliffyApp.DocumentModel.create());
        if (this.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktopDocument",
            action: "create",
          });
        }
      },
      save: function (q, p, r) {
        o.send("save", q, p, r);
        if (this.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktopDocument",
            action: "save",
          });
        }
      },
      saveAs: function (q, p, r) {
        o.send(
          "promptSaveAs",
          q,
          function () {
            p();
          },
          r
        );
        if (this.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktopDocument",
            action: "saveAs",
          });
        }
      },
      close: function (q, p, r) {
        var s = this.editor;
        if (GliffyApp.documentModel.get("unsavedChanges")) {
          GLIFFY.Dialog.showYesNoDialog({
            title: $.i18n._("TABDIALOG_CONFIRM_NAVIGATION"),
            content: $.i18n._("TABDIALOG_UNSAVED_CHANGES", [q.title]),
            cancelCallback: null,
            buttons: [
              {
                text: $.i18n._("TABDIALOG_CONTINUE_WORKING"),
                callback: null,
                primary: false,
                dismiss: true,
              },
              {
                text: $.i18n._("TABDIALOG_CLOSE_TAB"),
                callback: function (t) {
                  if (s.analyticsEnabled) {
                    GLIFFY.Utils.analyticsEvent("trackEvent", {
                      category: "desktopDocument",
                      action: "close",
                    });
                  }
                  o.send("close", q);
                },
                primary: true,
                dismiss: true,
              },
            ],
          });
        } else {
          if (this.editor.analyticsEnabled) {
            GLIFFY.Utils.analyticsEvent("trackEvent", {
              category: "desktopDocument",
              action: "close",
            });
          }
          o.send("close", q);
        }
      },
      closeLastTab: function (r, p, q) {
        this.editor.performAction("close");
      },
      open: function (r, p, q) {
        p(r);
        if (this.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktopDocument",
            action: "open",
          });
        }
      },
      registrationPopup: function () {
        o.send("register");
      },
    };
    m = function () {
      $(window).trigger("gliffy-render-complete");
    };
    i = [
      {
        title: $.i18n._("TIP_TITLE0"),
        content: $.i18n._("TIP_CONTENT0"),
        image: "images/gif/tips/drag-and-drop.gif",
      },
      {
        title: $.i18n._("TIP_TITLE1"),
        content: $.i18n._("TIP_CONTENT1"),
        image: "images/gif/tips/connectors.gif",
      },
      {
        title: $.i18n._("TIP_TITLE2"),
        content: $.i18n._("TIP_CONTENT2"),
        image: "images/gif/tips/text.gif",
      },
      {
        title: $.i18n._("TIP_TITLE3"),
        content: $.i18n._("TIP_CONTENT3"),
        image: "images/gif/tips/style.gif",
      },
      {
        title: $.i18n._("TIP_TITLE4"),
        content: $.i18n._("TIP_CONTENT4"),
        image: "images/gif/tips/connector-style.gif",
      },
      {
        title: $.i18n._("TIP_TITLE5"),
        content: $.i18n._("TIP_CONTENT5"),
        image: "images/gif/tips/dragndrop_images.gif",
      },
      {
        title: $.i18n._("TIP_TITLE6"),
        content: $.i18n._("TIP_CONTENT6"),
        image: "images/gif/tips/drawing-guides.gif",
      },
    ];

    function d(p) {
      if (GLIFFY.editor.analyticsEnabled && p.launchCount === 1) {
        GLIFFY.Utils.analyticsEvent("trackEvent", {
          category: "desktop",
          action: "install",
        });
      }
      setTimeout(function () {
        GliffyApp.rateAppController.set("launchCount", p.launchCount);
        GliffyApp.rateAppController.set("userRated", p.userRated);
        GliffyApp.rateAppController.show(function () {
          if (
            GLIFFY.ENV.welcomeController &&
            GliffyApp[GLIFFY.ENV.welcomeController] &&
            GliffyApp[GLIFFY.ENV.welcomeController].show
          ) {
            GliffyApp[GLIFFY.ENV.welcomeController].show();
          }
        });
      }, 100);
      GliffyApp.documentModel.addObserver("title", j);
      GliffyApp.documentModel.addObserver("unsavedChanges", j);
      GliffyApp.documentModel.addObserver("currentRevision", j);
    }

    addTopLevelItem = function (s, q, r, p) {
      $("div.navbar-fixed-top ul.pull-right").prepend(
        '<li class="dropdown"><a class="dropdown-top-menu dropdown-toggle" href="#" id="' +
          r +
          '"><span id="gliffy-menu-label-' +
          r +
          '">' +
          q +
          "</span></a></li>"
      );
      $("#" + r).click(function (t) {
        t.preventDefault();
        p();
      });
    };
    addTopLevelMenu = function (p, q) {
      $("div.navbar-fixed-top ul.pull-right").prepend(
        '<li class="dropdown" id="' +
          q +
          '"><a class="dropdown-toggle" data-toggle="dropdown" href="#' +
          q +
          '"><span id="gliffy-menu-label-' +
          q +
          '">' +
          p +
          '</span> <b class="caret"> </b></a><ul class="dropdown-menu" id="sub-' +
          q +
          '"></ul> </li>'
      );
    };
    addTopLevelMenuItem = function (s, q, r, p) {
      $("#sub-" + s).append(
        '<li><a href="#" id="' +
          r +
          '"><span id="gliffy-menu-label-' +
          r +
          '">' +
          q +
          "</span></a></li>"
      );
      $("#" + r).click(function (t) {
        t.preventDefault();
        p();
      });
    };
    hideToolbarItems = function () {
      $(".gliffy-toolbar-btns .gliffy-logo").parent().hide();
    };
    rewireNavbarItems = function () {
      $("#gliffy-anchor-feedback")
        .parent()
        .insertAfter($("#gliffy .navbar ul.nav li#menu1"));
      $("#gliffy .navbar ul.nav li#menu-account").insertAfter(
        $("#gliffy .navbar ul.nav li#menu1")
      );
    //   GLIFFY.onlineUtils.appendTemplates([
    //     "gliffy-feedback-function d(p) {modal-template",
    //     "gliffy-account-modal-template",
    //   ]);
      $("#gliffy").delegate(".gliffy-modal", "show", function () {
        $(".gliffy-sidebar.scrollable").css("overflow-y", "hidden");
      });
      $("#gliffy").delegate(".gliffy-modal", "hide", function () {
        $(".gliffy-sidebar.scrollable").css("overflow-y", "auto");
      });
      $(".gliffy-filemenu-new").show();
      $(".gliffy-filemenu-saveAs").show();
      $(".gliffy-file-menu-export:not(.divider)").hide();
      $(".gliffy-file-menu-export > .gliffy-filemenu-exportAsJPG")
        .parent()
        .show();
      $(".gliffy-file-menu-export > .gliffy-filemenu-exportAsPNG")
        .parent()
        .show();
      $(".gliffy-filemenu-saveAndClose").hide();
      $(".gliffy-buttons-right").hide();
      $(
        ".gliffy-nav-menus li:nth-child(3) .dropdown-menu li:nth-child(4)"
      ).hide();
      $(
        ".gliffy-nav-menus li:nth-child(3) .dropdown-menu li:nth-child(5)"
      ).hide();
      $(".gliffy-help-menu-link-eula a").click(function () {
        o.send("eula");
        $(".gliffy-nav-menus li:nth-child(3) .dropdown-toggle").dropdown(
          "toggle"
        );
        return false;
      });
      $(".gliffy-navbar-helpmenu-tips")
        .parent()
        .after(
          "<li><a href='#' id='rateApp' onclick='GliffyApp.rateAppController.show(function(){}, true);'><span id='gliffy-menu-label-rateApp'>" +
            $.i18n._("DESKTOP_RATE_APP_RATE_OUR_APP") +
            "</span></a></li>"
        );
      $(
        ".gliffy-nav-menus li:nth-child(3) .dropdown-menu > li:nth-child(2) a"
      ).click(function () {
        o.send("manual");
        $(".gliffy-nav-menus li:nth-child(3) .dropdown-toggle").dropdown(
          "toggle"
        );
        return false;
      });
      $(".gliffy-navbar-helpmenu-productVersion").hide();
      $(".gliffy-file-menu-export > .gliffy-filemenu-exportAsJPG").html(
        $.i18n._("DESKTOP_SAVE_AS_JPG")
      );
      $(".gliffy-file-menu-export > .gliffy-filemenu-exportAsPNG").html(
        $.i18n._("DESKTOP_SAVE_AS_PNG")
      );
      $("#gliffy-desktop-draft-notice .discard-btn").click(function (p) {
        o.send("getLocalStorage", "windowSettings", function (q) {
          if (q.windowSettings && q.windowSettings.count === 1) {
            o.send("newWindow", null, function () {
              o.send(
                "close",
                GLIFFY.editor
                  .getDocumentManager()
                  .getActiveDocument()
                  .toObject()
              );
            });
          } else {
            o.send(
              "close",
              GLIFFY.editor.getDocumentManager().getActiveDocument().toObject()
            );
          }
        });
      });
      $("#gliffy-desktop-draft-notice .continue-btn").click(function (p) {
        $("#gliffy-desktop-draft-notice").hide();
      });
      if (!GLIFFY.ENV.installDriveMenu) {
        $("#menu-username").hide();
      }
    };
    h = new window.GLIFFY.Editor({
      integration: new g(),
      target: $("#container"),
      rootServiceUrl: "desktop",
      rootResourceUrl: "/legal/terms-of-use/#",
      actions: e,
      tips: i,
      productVersion: "Desktop-" + GLIFFY.ENV.productVersion,
      analyticsEnabled: true,
      analyticsAccount: "UA-248648-10",
      analyticsLabel: "Desktop",
      analyticsProduct: "Online",
      analyticsLicense: GLIFFY.ENV.productType,
      analyticsNumUsers: 1,
      logEvents: false,
      language: c,
      draftInterval: 5,
      draftsEnabled: true,
      readyCallback: function (p) {
        e.init(p);
        hideToolbarItems();
        rewireNavbarItems();
        GLIFFY.toolbarController.toggleThemes();
        if (this.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("init", {
            accountType: "google",
            accountID: this.analyticsAccount,
            debug: false,
          });
          if (this.logEvents) {
            GLIFFY.Utils.analyticsEvent("logEvents", {
              logEvents: this.logEvents,
              logger: GLIFFY.Logger,
            });
          }
          if (this.analyticsLabel) {
            GLIFFY.Utils.analyticsEvent("setLabel", {
              label: this.analyticsLabel,
            });
          }
          GLIFFY.Utils.analyticsEvent("setCustomVariables", {
            product: this.analyticsProduct,
            license: this.analyticsLicense,
            numUsers: this.analyticsNumUsers + " Users",
          });
        }
        o.send(
          "getLocalStorage",
          ["statistics", "gliffyDesktop"],
          function (q) {
            var r = q.statistics || {};
            r.userRated = q.gliffyDesktop ? q.gliffyDesktop.userRated : false;
            o.send(
              "ready",
              null,
              function (s) {
                if (s) {
                  GLIFFY.ENV.welcomeController = null;
                  d(r);
                  p.open({
                    url: s,
                    scale: 1,
                    callback: function () {
                      var t = p.getDocumentManager().getActiveDocument();
                      var u = b(t);
                      p.getDocumentManager()
                        .getActiveStage()
                        .setUnsavedChanges(u);
                      GliffyApp.documentModel.update(t);
                      o.send("hideSplash");
                    },
                  });
                } else {
                  d(r);
                  j();
                  o.send("hideSplash");
                }
              },
              function (s, t) {
                d(r);
                GLIFFY.handleError(t);
              }
            );
          },
          function (q, r) {
            d({});
            GLIFFY.handleError(r);
          }
        );
        if (this.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktopDocument",
            action: "newFromLaunch",
          });
        }
      },
    });
    GLIFFY.editor = h;
    l = $("<div id='gliffy-bottom-banner'></div>");
    h.dockToBottomPanel(l);
    // setTimeout(function () {
    //   Ember.View.create({ templateName: "gliffy-banner-template" }).appendTo(l);
    // }, 1);
    GLIFFY.onlineUtils.appendTemplates([
      "gliffy-rate-app",
      "gliffy-gdrive-install-template",
    ]);

    function b(p) {
      return p.getUrls().tempId !== null && p.getUrls().tempId !== undefined;
    }

    function f(p) {
      if (p.lcb == true) {
        $("#alertDialog .close").hide();
        $("#alertDialog .modal-footer").hide();
        if (GLIFFY.editor.analyticsEnabled) {
          GLIFFY.Utils.analyticsEvent("trackEvent", {
            category: "desktop",
            action: "licenseInvalid",
          });
        }
        GLIFFY.Dialog.showAlertDialog({
          content: $.i18n._("DESKTOP_APP_LICENSE_INVALID"),
          title: $.i18n._("DESKTOP_ERROR") + "!",
          buttons: [],
          backdropStatic: true,
        });
      }
      Ember.set(GLIFFY.ENV, "userName", p.name);
      Ember.set(GLIFFY.ENV, "email", p.email);
      Ember.set(GLIFFY.ENV, "productType", p.productType);
      Ember.set(GLIFFY.ENV, "isTrial", p.isTrial);
      Ember.set(GLIFFY.ENV, "trialExpiration", p.trialExpiration);
      Ember.set(GLIFFY.ENV, "accountName", p.name);
      Ember.set(GLIFFY.ENV, "rated", p.rated);
      Ember.set(GLIFFY.ENV, "driveEnabled", p.driveEnabled);
      Ember.set(GLIFFY.ENV, "cid", p.cid);
      GLIFFY.googleDriveManager.ready();
    }

    GLIFFY.Components.appendTemplate("gliffy-file-open-browser-template");
    GLIFFY.Components.appendTemplate("gliffy-folder-create-modal-template");
    GLIFFY.Components.appendTemplate("gliffy-desktop-draft-notice-template");
  });
})();
