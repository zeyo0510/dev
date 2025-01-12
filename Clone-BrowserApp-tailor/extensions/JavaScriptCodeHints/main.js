
// Acorn is a tiny, fast JavaScript parser written in JavaScript.
//
// Acorn was written by Marijn Haverbeke and released under an MIT
// license. The Unicode regexps (for identifiers and whitespace) were
// taken from [Esprima](http://esprima.org) by Ariya Hidayat.
//
// Git repositories for Acorn are available at
//
//     http://marijnhaverbeke.nl/git/acorn
//     https://github.com/marijnh/acorn.git
//
// Please use the [github bug tracker][ghbt] to report issues.
//
// [ghbt]: https://github.com/marijnh/acorn/issues
//
// This file defines the main parser interface. The library also comes
// with a [error-tolerant parser][dammit] and an
// [abstract syntax tree walker][walk], defined in other files.
//
// [dammit]: acorn_loose.js
// [walk]: util/walk.js

(function(mod) {
  if (typeof exports == "object" && typeof module == "object") return mod(exports); // CommonJS
  if (typeof define == "function" && define.amd) return define('thirdparty/acorn/acorn',["exports"], mod); // AMD
  mod(this.acorn || (this.acorn = {})); // Plain browser env
})(function(exports) {
  

  exports.version = "0.3.2";

  // The main exported interface (under `self.acorn` when in the
  // browser) is a `parse` function that takes a code string and
  // returns an abstract syntax tree as specified by [Mozilla parser
  // API][api], with the caveat that the SpiderMonkey-specific syntax
  // (`let`, `yield`, inline XML, etc) is not recognized.
  //
  // [api]: https://developer.mozilla.org/en-US/docs/SpiderMonkey/Parser_API

  var options, input, inputLen, sourceFile;

  exports.parse = function(inpt, opts) {
    input = String(inpt); inputLen = input.length;
    setOptions(opts);
    initTokenState();
    return parseTopLevel(options.program);
  };

  // A second optional argument can be given to further configure
  // the parser process. These options are recognized:

  var defaultOptions = exports.defaultOptions = {
    // `ecmaVersion` indicates the ECMAScript version to parse. Must
    // be either 3 or 5. This
    // influences support for strict mode, the set of reserved words, and
    // support for getters and setter.
    ecmaVersion: 5,
    // Turn on `strictSemicolons` to prevent the parser from doing
    // automatic semicolon insertion.
    strictSemicolons: false,
    // When `allowTrailingCommas` is false, the parser will not allow
    // trailing commas in array and object literals.
    allowTrailingCommas: true,
    // By default, reserved words are not enforced. Enable
    // `forbidReserved` to enforce them.
    forbidReserved: false,
    // When `locations` is on, `loc` properties holding objects with
    // `start` and `end` properties in `{line, column}` form (with
    // line being 1-based and column 0-based) will be attached to the
    // nodes.
    locations: false,
    // A function can be passed as `onComment` option, which will
    // cause Acorn to call that function with `(block, text, start,
    // end)` parameters whenever a comment is skipped. `block` is a
    // boolean indicating whether this is a block (`/* */`) comment,
    // `text` is the content of the comment, and `start` and `end` are
    // character offsets that denote the start and end of the comment.
    // When the `locations` option is on, two more parameters are
    // passed, the full `{line, column}` locations of the start and
    // end of the comments.
    onComment: null,
    // Nodes have their start and end characters offsets recorded in
    // `start` and `end` properties (directly on the node, rather than
    // the `loc` object, which holds line/column data. To also add a
    // [semi-standardized][range] `range` property holding a `[start,
    // end]` array with the same numbers, set the `ranges` option to
    // `true`.
    //
    // [range]: https://bugzilla.mozilla.org/show_bug.cgi?id=745678
    ranges: false,
    // It is possible to parse multiple files into a single AST by
    // passing the tree produced by parsing the first file as
    // `program` option in subsequent parses. This will add the
    // toplevel forms of the parsed file to the `Program` (top) node
    // of an existing parse tree.
    program: null,
    // When `location` is on, you can pass this to record the source
    // file in every node's `loc` object.
    sourceFile: null
  };

  function setOptions(opts) {
    options = opts || {};
    for (var opt in defaultOptions) if (!Object.prototype.hasOwnProperty.call(options, opt))
      options[opt] = defaultOptions[opt];
    sourceFile = options.sourceFile || null;
  }

  // The `getLineInfo` function is mostly useful when the
  // `locations` option is off (for performance reasons) and you
  // want to find the line/column position for a given character
  // offset. `input` should be the code string that the offset refers
  // into.

  var getLineInfo = exports.getLineInfo = function(input, offset) {
    for (var line = 1, cur = 0;;) {
      lineBreak.lastIndex = cur;
      var match = lineBreak.exec(input);
      if (match && match.index < offset) {
        ++line;
        cur = match.index + match[0].length;
      } else break;
    }
    return {line: line, column: offset - cur};
  };

  // Acorn is organized as a tokenizer and a recursive-descent parser.
  // The `tokenize` export provides an interface to the tokenizer.
  // Because the tokenizer is optimized for being efficiently used by
  // the Acorn parser itself, this interface is somewhat crude and not
  // very modular. Performing another parse or call to `tokenize` will
  // reset the internal state, and invalidate existing tokenizers.

  exports.tokenize = function(inpt, opts) {
    input = String(inpt); inputLen = input.length;
    setOptions(opts);
    initTokenState();

    var t = {};
    function getToken(forceRegexp) {
      readToken(forceRegexp);
      t.start = tokStart; t.end = tokEnd;
      t.startLoc = tokStartLoc; t.endLoc = tokEndLoc;
      t.type = tokType; t.value = tokVal;
      return t;
    }
    getToken.jumpTo = function(pos, reAllowed) {
      tokPos = pos;
      if (options.locations) {
        tokCurLine = tokLineStart = lineBreak.lastIndex = 0;
        var match;
        while ((match = lineBreak.exec(input)) && match.index < pos) {
          ++tokCurLine;
          tokLineStart = match.index + match[0].length;
        }
      }
      var ch = input.charAt(pos - 1);
      tokRegexpAllowed = reAllowed;
      skipSpace();
    };
    return getToken;
  };

  // State is kept in (closure-)global variables. We already saw the
  // `options`, `input`, and `inputLen` variables above.

  // The current position of the tokenizer in the input.

  var tokPos;

  // The start and end offsets of the current token.

  var tokStart, tokEnd;

  // When `options.locations` is true, these hold objects
  // containing the tokens start and end line/column pairs.

  var tokStartLoc, tokEndLoc;

  // The type and value of the current token. Token types are objects,
  // named by variables against which they can be compared, and
  // holding properties that describe them (indicating, for example,
  // the precedence of an infix operator, and the original name of a
  // keyword token). The kind of value that's held in `tokVal` depends
  // on the type of the token. For literals, it is the literal value,
  // for operators, the operator name, and so on.

  var tokType, tokVal;

  // Interal state for the tokenizer. To distinguish between division
  // operators and regular expressions, it remembers whether the last
  // token was one that is allowed to be followed by an expression.
  // (If it is, a slash is probably a regexp, if it isn't it's a
  // division operator. See the `parseStatement` function for a
  // caveat.)

  var tokRegexpAllowed;

  // When `options.locations` is true, these are used to keep
  // track of the current line, and know when a new line has been
  // entered.

  var tokCurLine, tokLineStart;

  // These store the position of the previous token, which is useful
  // when finishing a node and assigning its `end` position.

  var lastStart, lastEnd, lastEndLoc;

  // This is the parser's state. `inFunction` is used to reject
  // `return` statements outside of functions, `labels` to verify that
  // `break` and `continue` have somewhere to jump to, and `strict`
  // indicates whether strict mode is on.

  var inFunction, labels, strict;

  // This function is used to raise exceptions on parse errors. It
  // takes an offset integer (into the current `input`) to indicate
  // the location of the error, attaches the position to the end
  // of the error message, and then raises a `SyntaxError` with that
  // message.

  function raise(pos, message) {
    var loc = getLineInfo(input, pos);
    message += " (" + loc.line + ":" + loc.column + ")";
    var err = new SyntaxError(message);
    err.pos = pos; err.loc = loc; err.raisedAt = tokPos;
    throw err;
  }

  // ## Token types

  // The assignment of fine-grained, information-carrying type objects
  // allows the tokenizer to store the information it has about a
  // token in a way that is very cheap for the parser to look up.

  // All token type variables start with an underscore, to make them
  // easy to recognize.

  // These are the general types. The `type` property is only used to
  // make them recognizeable when debugging.

  var _num = {type: "num"}, _regexp = {type: "regexp"}, _string = {type: "string"};
  var _name = {type: "name"}, _eof = {type: "eof"};

  // Keyword tokens. The `keyword` property (also used in keyword-like
  // operators) indicates that the token originated from an
  // identifier-like word, which is used when parsing property names.
  //
  // The `beforeExpr` property is used to disambiguate between regular
  // expressions and divisions. It is set on all token types that can
  // be followed by an expression (thus, a slash after them would be a
  // regular expression).
  //
  // `isLoop` marks a keyword as starting a loop, which is important
  // to know when parsing a label, in order to allow or disallow
  // continue jumps to that label.

  var _break = {keyword: "break"}, _case = {keyword: "case", beforeExpr: true}, _catch = {keyword: "catch"};
  var _continue = {keyword: "continue"}, _debugger = {keyword: "debugger"}, _default = {keyword: "default"};
  var _do = {keyword: "do", isLoop: true}, _else = {keyword: "else", beforeExpr: true};
  var _finally = {keyword: "finally"}, _for = {keyword: "for", isLoop: true}, _function = {keyword: "function"};
  var _if = {keyword: "if"}, _return = {keyword: "return", beforeExpr: true}, _switch = {keyword: "switch"};
  var _throw = {keyword: "throw", beforeExpr: true}, _try = {keyword: "try"}, _var = {keyword: "var"};
  var _while = {keyword: "while", isLoop: true}, _with = {keyword: "with"}, _new = {keyword: "new", beforeExpr: true};
  var _this = {keyword: "this"};

  // The keywords that denote values.

  var _null = {keyword: "null", atomValue: null}, _true = {keyword: "true", atomValue: true};
  var _false = {keyword: "false", atomValue: false};

  // Some keywords are treated as regular operators. `in` sometimes
  // (when parsing `for`) needs to be tested against specifically, so
  // we assign a variable name to it for quick comparing.

  var _in = {keyword: "in", binop: 7, beforeExpr: true};

  // Map keyword names to token types.

  var keywordTypes = {"break": _break, "case": _case, "catch": _catch,
                      "continue": _continue, "debugger": _debugger, "default": _default,
                      "do": _do, "else": _else, "finally": _finally, "for": _for,
                      "function": _function, "if": _if, "return": _return, "switch": _switch,
                      "throw": _throw, "try": _try, "var": _var, "while": _while, "with": _with,
                      "null": _null, "true": _true, "false": _false, "new": _new, "in": _in,
                      "instanceof": {keyword: "instanceof", binop: 7, beforeExpr: true}, "this": _this,
                      "typeof": {keyword: "typeof", prefix: true, beforeExpr: true},
                      "void": {keyword: "void", prefix: true, beforeExpr: true},
                      "delete": {keyword: "delete", prefix: true, beforeExpr: true}};

  // Punctuation token types. Again, the `type` property is purely for debugging.

  var _bracketL = {type: "[", beforeExpr: true}, _bracketR = {type: "]"}, _braceL = {type: "{", beforeExpr: true};
  var _braceR = {type: "}"}, _parenL = {type: "(", beforeExpr: true}, _parenR = {type: ")"};
  var _comma = {type: ",", beforeExpr: true}, _semi = {type: ";", beforeExpr: true};
  var _colon = {type: ":", beforeExpr: true}, _dot = {type: "."}, _question = {type: "?", beforeExpr: true};

  // Operators. These carry several kinds of properties to help the
  // parser use them properly (the presence of these properties is
  // what categorizes them as operators).
  //
  // `binop`, when present, specifies that this operator is a binary
  // operator, and will refer to its precedence.
  //
  // `prefix` and `postfix` mark the operator as a prefix or postfix
  // unary operator. `isUpdate` specifies that the node produced by
  // the operator should be of type UpdateExpression rather than
  // simply UnaryExpression (`++` and `--`).
  //
  // `isAssign` marks all of `=`, `+=`, `-=` etcetera, which act as
  // binary operators with a very low precedence, that should result
  // in AssignmentExpression nodes.

  var _slash = {binop: 10, beforeExpr: true}, _eq = {isAssign: true, beforeExpr: true};
  var _assign = {isAssign: true, beforeExpr: true}, _plusmin = {binop: 9, prefix: true, beforeExpr: true};
  var _incdec = {postfix: true, prefix: true, isUpdate: true}, _prefix = {prefix: true, beforeExpr: true};
  var _bin1 = {binop: 1, beforeExpr: true}, _bin2 = {binop: 2, beforeExpr: true};
  var _bin3 = {binop: 3, beforeExpr: true}, _bin4 = {binop: 4, beforeExpr: true};
  var _bin5 = {binop: 5, beforeExpr: true}, _bin6 = {binop: 6, beforeExpr: true};
  var _bin7 = {binop: 7, beforeExpr: true}, _bin8 = {binop: 8, beforeExpr: true};
  var _bin10 = {binop: 10, beforeExpr: true};

  // Provide access to the token types for external users of the
  // tokenizer.

  exports.tokTypes = {bracketL: _bracketL, bracketR: _bracketR, braceL: _braceL, braceR: _braceR,
                      parenL: _parenL, parenR: _parenR, comma: _comma, semi: _semi, colon: _colon,
                      dot: _dot, question: _question, slash: _slash, eq: _eq, name: _name, eof: _eof,
                      num: _num, regexp: _regexp, string: _string};
  for (var kw in keywordTypes) exports.tokTypes["_" + kw] = keywordTypes[kw];

  // This is a trick taken from Esprima. It turns out that, on
  // non-Chrome browsers, to check whether a string is in a set, a
  // predicate containing a big ugly `switch` statement is faster than
  // a regular expression, and on Chrome the two are about on par.
  // This function uses `eval` (non-lexical) to produce such a
  // predicate from a space-separated string of words.
  //
  // It starts by sorting the words by length.
  // function makePredicate(words) {
  //   words = words.split(" ");
  //   var f = "", cats = [];
  //   out: for (var i = 0; i < words.length; ++i) {
  //     for (var j = 0; j < cats.length; ++j)
  //       if (cats[j][0].length == words[i].length) {
  //         cats[j].push(words[i]);
  //         continue out;
  //       }
  //     cats.push([words[i]]);
  //   }
  //   function compareTo(arr) {
  //     if (arr.length == 1) return f += "return str === " + JSON.stringify(arr[0]) + ";";
  //     f += "switch(str){";
  //     for (var i = 0; i < arr.length; ++i) f += "case " + JSON.stringify(arr[i]) + ":";
  //     f += "return true}return false;";
  //   }

  //   // When there are more than three length categories, an outer
  //   // switch first dispatches on the lengths, to save on comparisons.

  //   if (cats.length > 3) {
  //     cats.sort(function(a, b) {return b.length - a.length;});
  //     f += "switch(str.length){";
  //     for (var i = 0; i < cats.length; ++i) {
  //       var cat = cats[i];
  //       f += "case " + cat[0].length + ":";
  //       compareTo(cat);
  //     }
  //     f += "}";

  //   // Otherwise, simply generate a flat `switch` statement.

  //   } else {
  //     compareTo(words);
  //   }
  //   return new Function("str", f);
  // }
  var makePredicate = function(words){
    var words = words.split(" ");
    var regexString = "^" + words[0] + "$";
    for (var i = 1; i < words.length; i++){
      regexString += ("|^" + words[i] + "$");
    }
    var regex = new RegExp(regexString);

    return function(str){
      return regex.test(str);
    }
  }

  // The ECMAScript 3 reserved word list.

  var isReservedWord3 = makePredicate("abstract boolean byte char class double enum export extends final float goto implements import int interface long native package private protected public short static super synchronized throws transient volatile");

  // ECMAScript 5 reserved words.

  var isReservedWord5 = makePredicate("class enum extends super const export import");

  // The additional reserved words in strict mode.

  var isStrictReservedWord = makePredicate("implements interface let package private protected public static yield");

  // The forbidden variable names in strict mode.

  var isStrictBadIdWord = makePredicate("eval arguments");

  // And the keywords.

  var isKeyword = makePredicate("break case catch continue debugger default do else finally for function if return switch throw try var while with null true false instanceof typeof void delete new in this");

  // ## Character categories

  // Big ugly regular expressions that match characters in the
  // whitespace, identifier, and identifier-start categories. These
  // are only applied when a character is found to actually have a
  // code point above 128.

  var nonASCIIwhitespace = /[\u1680\u180e\u2000-\u200a\u2028\u2029\u202f\u205f\u3000\ufeff]/;
  var nonASCIIidentifierStartChars = "\xaa\xb5\xba\xc0-\xd6\xd8-\xf6\xf8-\u02c1\u02c6-\u02d1\u02e0-\u02e4\u02ec\u02ee\u0370-\u0374\u0376\u0377\u037a-\u037d\u0386\u0388-\u038a\u038c\u038e-\u03a1\u03a3-\u03f5\u03f7-\u0481\u048a-\u0527\u0531-\u0556\u0559\u0561-\u0587\u05d0-\u05ea\u05f0-\u05f2\u0620-\u064a\u066e\u066f\u0671-\u06d3\u06d5\u06e5\u06e6\u06ee\u06ef\u06fa-\u06fc\u06ff\u0710\u0712-\u072f\u074d-\u07a5\u07b1\u07ca-\u07ea\u07f4\u07f5\u07fa\u0800-\u0815\u081a\u0824\u0828\u0840-\u0858\u08a0\u08a2-\u08ac\u0904-\u0939\u093d\u0950\u0958-\u0961\u0971-\u0977\u0979-\u097f\u0985-\u098c\u098f\u0990\u0993-\u09a8\u09aa-\u09b0\u09b2\u09b6-\u09b9\u09bd\u09ce\u09dc\u09dd\u09df-\u09e1\u09f0\u09f1\u0a05-\u0a0a\u0a0f\u0a10\u0a13-\u0a28\u0a2a-\u0a30\u0a32\u0a33\u0a35\u0a36\u0a38\u0a39\u0a59-\u0a5c\u0a5e\u0a72-\u0a74\u0a85-\u0a8d\u0a8f-\u0a91\u0a93-\u0aa8\u0aaa-\u0ab0\u0ab2\u0ab3\u0ab5-\u0ab9\u0abd\u0ad0\u0ae0\u0ae1\u0b05-\u0b0c\u0b0f\u0b10\u0b13-\u0b28\u0b2a-\u0b30\u0b32\u0b33\u0b35-\u0b39\u0b3d\u0b5c\u0b5d\u0b5f-\u0b61\u0b71\u0b83\u0b85-\u0b8a\u0b8e-\u0b90\u0b92-\u0b95\u0b99\u0b9a\u0b9c\u0b9e\u0b9f\u0ba3\u0ba4\u0ba8-\u0baa\u0bae-\u0bb9\u0bd0\u0c05-\u0c0c\u0c0e-\u0c10\u0c12-\u0c28\u0c2a-\u0c33\u0c35-\u0c39\u0c3d\u0c58\u0c59\u0c60\u0c61\u0c85-\u0c8c\u0c8e-\u0c90\u0c92-\u0ca8\u0caa-\u0cb3\u0cb5-\u0cb9\u0cbd\u0cde\u0ce0\u0ce1\u0cf1\u0cf2\u0d05-\u0d0c\u0d0e-\u0d10\u0d12-\u0d3a\u0d3d\u0d4e\u0d60\u0d61\u0d7a-\u0d7f\u0d85-\u0d96\u0d9a-\u0db1\u0db3-\u0dbb\u0dbd\u0dc0-\u0dc6\u0e01-\u0e30\u0e32\u0e33\u0e40-\u0e46\u0e81\u0e82\u0e84\u0e87\u0e88\u0e8a\u0e8d\u0e94-\u0e97\u0e99-\u0e9f\u0ea1-\u0ea3\u0ea5\u0ea7\u0eaa\u0eab\u0ead-\u0eb0\u0eb2\u0eb3\u0ebd\u0ec0-\u0ec4\u0ec6\u0edc-\u0edf\u0f00\u0f40-\u0f47\u0f49-\u0f6c\u0f88-\u0f8c\u1000-\u102a\u103f\u1050-\u1055\u105a-\u105d\u1061\u1065\u1066\u106e-\u1070\u1075-\u1081\u108e\u10a0-\u10c5\u10c7\u10cd\u10d0-\u10fa\u10fc-\u1248\u124a-\u124d\u1250-\u1256\u1258\u125a-\u125d\u1260-\u1288\u128a-\u128d\u1290-\u12b0\u12b2-\u12b5\u12b8-\u12be\u12c0\u12c2-\u12c5\u12c8-\u12d6\u12d8-\u1310\u1312-\u1315\u1318-\u135a\u1380-\u138f\u13a0-\u13f4\u1401-\u166c\u166f-\u167f\u1681-\u169a\u16a0-\u16ea\u16ee-\u16f0\u1700-\u170c\u170e-\u1711\u1720-\u1731\u1740-\u1751\u1760-\u176c\u176e-\u1770\u1780-\u17b3\u17d7\u17dc\u1820-\u1877\u1880-\u18a8\u18aa\u18b0-\u18f5\u1900-\u191c\u1950-\u196d\u1970-\u1974\u1980-\u19ab\u19c1-\u19c7\u1a00-\u1a16\u1a20-\u1a54\u1aa7\u1b05-\u1b33\u1b45-\u1b4b\u1b83-\u1ba0\u1bae\u1baf\u1bba-\u1be5\u1c00-\u1c23\u1c4d-\u1c4f\u1c5a-\u1c7d\u1ce9-\u1cec\u1cee-\u1cf1\u1cf5\u1cf6\u1d00-\u1dbf\u1e00-\u1f15\u1f18-\u1f1d\u1f20-\u1f45\u1f48-\u1f4d\u1f50-\u1f57\u1f59\u1f5b\u1f5d\u1f5f-\u1f7d\u1f80-\u1fb4\u1fb6-\u1fbc\u1fbe\u1fc2-\u1fc4\u1fc6-\u1fcc\u1fd0-\u1fd3\u1fd6-\u1fdb\u1fe0-\u1fec\u1ff2-\u1ff4\u1ff6-\u1ffc\u2071\u207f\u2090-\u209c\u2102\u2107\u210a-\u2113\u2115\u2119-\u211d\u2124\u2126\u2128\u212a-\u212d\u212f-\u2139\u213c-\u213f\u2145-\u2149\u214e\u2160-\u2188\u2c00-\u2c2e\u2c30-\u2c5e\u2c60-\u2ce4\u2ceb-\u2cee\u2cf2\u2cf3\u2d00-\u2d25\u2d27\u2d2d\u2d30-\u2d67\u2d6f\u2d80-\u2d96\u2da0-\u2da6\u2da8-\u2dae\u2db0-\u2db6\u2db8-\u2dbe\u2dc0-\u2dc6\u2dc8-\u2dce\u2dd0-\u2dd6\u2dd8-\u2dde\u2e2f\u3005-\u3007\u3021-\u3029\u3031-\u3035\u3038-\u303c\u3041-\u3096\u309d-\u309f\u30a1-\u30fa\u30fc-\u30ff\u3105-\u312d\u3131-\u318e\u31a0-\u31ba\u31f0-\u31ff\u3400-\u4db5\u4e00-\u9fcc\ua000-\ua48c\ua4d0-\ua4fd\ua500-\ua60c\ua610-\ua61f\ua62a\ua62b\ua640-\ua66e\ua67f-\ua697\ua6a0-\ua6ef\ua717-\ua71f\ua722-\ua788\ua78b-\ua78e\ua790-\ua793\ua7a0-\ua7aa\ua7f8-\ua801\ua803-\ua805\ua807-\ua80a\ua80c-\ua822\ua840-\ua873\ua882-\ua8b3\ua8f2-\ua8f7\ua8fb\ua90a-\ua925\ua930-\ua946\ua960-\ua97c\ua984-\ua9b2\ua9cf\uaa00-\uaa28\uaa40-\uaa42\uaa44-\uaa4b\uaa60-\uaa76\uaa7a\uaa80-\uaaaf\uaab1\uaab5\uaab6\uaab9-\uaabd\uaac0\uaac2\uaadb-\uaadd\uaae0-\uaaea\uaaf2-\uaaf4\uab01-\uab06\uab09-\uab0e\uab11-\uab16\uab20-\uab26\uab28-\uab2e\uabc0-\uabe2\uac00-\ud7a3\ud7b0-\ud7c6\ud7cb-\ud7fb\uf900-\ufa6d\ufa70-\ufad9\ufb00-\ufb06\ufb13-\ufb17\ufb1d\ufb1f-\ufb28\ufb2a-\ufb36\ufb38-\ufb3c\ufb3e\ufb40\ufb41\ufb43\ufb44\ufb46-\ufbb1\ufbd3-\ufd3d\ufd50-\ufd8f\ufd92-\ufdc7\ufdf0-\ufdfb\ufe70-\ufe74\ufe76-\ufefc\uff21-\uff3a\uff41-\uff5a\uff66-\uffbe\uffc2-\uffc7\uffca-\uffcf\uffd2-\uffd7\uffda-\uffdc";
  var nonASCIIidentifierChars = "\u0300-\u036f\u0483-\u0487\u0591-\u05bd\u05bf\u05c1\u05c2\u05c4\u05c5\u05c7\u0610-\u061a\u0620-\u0649\u0672-\u06d3\u06e7-\u06e8\u06fb-\u06fc\u0730-\u074a\u0800-\u0814\u081b-\u0823\u0825-\u0827\u0829-\u082d\u0840-\u0857\u08e4-\u08fe\u0900-\u0903\u093a-\u093c\u093e-\u094f\u0951-\u0957\u0962-\u0963\u0966-\u096f\u0981-\u0983\u09bc\u09be-\u09c4\u09c7\u09c8\u09d7\u09df-\u09e0\u0a01-\u0a03\u0a3c\u0a3e-\u0a42\u0a47\u0a48\u0a4b-\u0a4d\u0a51\u0a66-\u0a71\u0a75\u0a81-\u0a83\u0abc\u0abe-\u0ac5\u0ac7-\u0ac9\u0acb-\u0acd\u0ae2-\u0ae3\u0ae6-\u0aef\u0b01-\u0b03\u0b3c\u0b3e-\u0b44\u0b47\u0b48\u0b4b-\u0b4d\u0b56\u0b57\u0b5f-\u0b60\u0b66-\u0b6f\u0b82\u0bbe-\u0bc2\u0bc6-\u0bc8\u0bca-\u0bcd\u0bd7\u0be6-\u0bef\u0c01-\u0c03\u0c46-\u0c48\u0c4a-\u0c4d\u0c55\u0c56\u0c62-\u0c63\u0c66-\u0c6f\u0c82\u0c83\u0cbc\u0cbe-\u0cc4\u0cc6-\u0cc8\u0cca-\u0ccd\u0cd5\u0cd6\u0ce2-\u0ce3\u0ce6-\u0cef\u0d02\u0d03\u0d46-\u0d48\u0d57\u0d62-\u0d63\u0d66-\u0d6f\u0d82\u0d83\u0dca\u0dcf-\u0dd4\u0dd6\u0dd8-\u0ddf\u0df2\u0df3\u0e34-\u0e3a\u0e40-\u0e45\u0e50-\u0e59\u0eb4-\u0eb9\u0ec8-\u0ecd\u0ed0-\u0ed9\u0f18\u0f19\u0f20-\u0f29\u0f35\u0f37\u0f39\u0f41-\u0f47\u0f71-\u0f84\u0f86-\u0f87\u0f8d-\u0f97\u0f99-\u0fbc\u0fc6\u1000-\u1029\u1040-\u1049\u1067-\u106d\u1071-\u1074\u1082-\u108d\u108f-\u109d\u135d-\u135f\u170e-\u1710\u1720-\u1730\u1740-\u1750\u1772\u1773\u1780-\u17b2\u17dd\u17e0-\u17e9\u180b-\u180d\u1810-\u1819\u1920-\u192b\u1930-\u193b\u1951-\u196d\u19b0-\u19c0\u19c8-\u19c9\u19d0-\u19d9\u1a00-\u1a15\u1a20-\u1a53\u1a60-\u1a7c\u1a7f-\u1a89\u1a90-\u1a99\u1b46-\u1b4b\u1b50-\u1b59\u1b6b-\u1b73\u1bb0-\u1bb9\u1be6-\u1bf3\u1c00-\u1c22\u1c40-\u1c49\u1c5b-\u1c7d\u1cd0-\u1cd2\u1d00-\u1dbe\u1e01-\u1f15\u200c\u200d\u203f\u2040\u2054\u20d0-\u20dc\u20e1\u20e5-\u20f0\u2d81-\u2d96\u2de0-\u2dff\u3021-\u3028\u3099\u309a\ua640-\ua66d\ua674-\ua67d\ua69f\ua6f0-\ua6f1\ua7f8-\ua800\ua806\ua80b\ua823-\ua827\ua880-\ua881\ua8b4-\ua8c4\ua8d0-\ua8d9\ua8f3-\ua8f7\ua900-\ua909\ua926-\ua92d\ua930-\ua945\ua980-\ua983\ua9b3-\ua9c0\uaa00-\uaa27\uaa40-\uaa41\uaa4c-\uaa4d\uaa50-\uaa59\uaa7b\uaae0-\uaae9\uaaf2-\uaaf3\uabc0-\uabe1\uabec\uabed\uabf0-\uabf9\ufb20-\ufb28\ufe00-\ufe0f\ufe20-\ufe26\ufe33\ufe34\ufe4d-\ufe4f\uff10-\uff19\uff3f";
  var nonASCIIidentifierStart = new RegExp("[" + nonASCIIidentifierStartChars + "]");
  var nonASCIIidentifier = new RegExp("[" + nonASCIIidentifierStartChars + nonASCIIidentifierChars + "]");

  // Whether a single character denotes a newline.

  var newline = /[\n\r\u2028\u2029]/;

  // Matches a whole line break (where CRLF is considered a single
  // line break). Used to count lines.

  var lineBreak = /\r\n|[\n\r\u2028\u2029]/g;

  // Test whether a given character code starts an identifier.

  var isIdentifierStart = exports.isIdentifierStart = function(code) {
    if (code < 65) return code === 36;
    if (code < 91) return true;
    if (code < 97) return code === 95;
    if (code < 123)return true;
    return code >= 0xaa && nonASCIIidentifierStart.test(String.fromCharCode(code));
  };

  // Test whether a given character is part of an identifier.

  var isIdentifierChar = exports.isIdentifierChar = function(code) {
    if (code < 48) return code === 36;
    if (code < 58) return true;
    if (code < 65) return false;
    if (code < 91) return true;
    if (code < 97) return code === 95;
    if (code < 123)return true;
    return code >= 0xaa && nonASCIIidentifier.test(String.fromCharCode(code));
  };

  // ## Tokenizer

  // These are used when `options.locations` is on, for the
  // `tokStartLoc` and `tokEndLoc` properties.

  function line_loc_t() {
    this.line = tokCurLine;
    this.column = tokPos - tokLineStart;
  }

  // Reset the token state. Used at the start of a parse.

  function initTokenState() {
    tokCurLine = 1;
    tokPos = tokLineStart = 0;
    tokRegexpAllowed = true;
    skipSpace();
  }

  // Called at the end of every token. Sets `tokEnd`, `tokVal`, and
  // `tokRegexpAllowed`, and skips the space after the token, so that
  // the next one's `tokStart` will point at the right position.

  function finishToken(type, val) {
    tokEnd = tokPos;
    if (options.locations) tokEndLoc = new line_loc_t;
    tokType = type;
    skipSpace();
    tokVal = val;
    tokRegexpAllowed = type.beforeExpr;
  }

  function skipBlockComment() {
    var startLoc = options.onComment && options.locations && new line_loc_t;
    var start = tokPos, end = input.indexOf("*/", tokPos += 2);
    if (end === -1) raise(tokPos - 2, "Unterminated comment");
    tokPos = end + 2;
    if (options.locations) {
      lineBreak.lastIndex = start;
      var match;
      while ((match = lineBreak.exec(input)) && match.index < tokPos) {
        ++tokCurLine;
        tokLineStart = match.index + match[0].length;
      }
    }
    if (options.onComment)
      options.onComment(true, input.slice(start + 2, end), start, tokPos,
                        startLoc, options.locations && new line_loc_t);
  }

  function skipLineComment() {
    var start = tokPos;
    var startLoc = options.onComment && options.locations && new line_loc_t;
    var ch = input.charCodeAt(tokPos+=2);
    while (tokPos < inputLen && ch !== 10 && ch !== 13 && ch !== 8232 && ch !== 8329) {
      ++tokPos;
      ch = input.charCodeAt(tokPos);
    }
    if (options.onComment)
      options.onComment(false, input.slice(start + 2, tokPos), start, tokPos,
                        startLoc, options.locations && new line_loc_t);
  }

  // Called at the start of the parse and after every token. Skips
  // whitespace and comments, and.

  function skipSpace() {
    while (tokPos < inputLen) {
      var ch = input.charCodeAt(tokPos);
      if (ch === 32) { // ' '
        ++tokPos;
      } else if(ch === 13) {
        ++tokPos;
        var next = input.charCodeAt(tokPos);
        if(next === 10) {
          ++tokPos;
        }
        if(options.locations) {
          ++tokCurLine;
          tokLineStart = tokPos;
        }
      } else if (ch === 10) {
        ++tokPos;
        ++tokCurLine;
        tokLineStart = tokPos;
      } else if(ch < 14 && ch > 8) {
        ++tokPos;
      } else if (ch === 47) { // '/'
        var next = input.charCodeAt(tokPos+1);
        if (next === 42) { // '*'
          skipBlockComment();
        } else if (next === 47) { // '/'
          skipLineComment();
        } else break;
      } else if ((ch < 14 && ch > 8) || ch === 32 || ch === 160) { // ' ', '\xa0'
        ++tokPos;
      } else if (ch >= 5760 && nonASCIIwhitespace.test(String.fromCharCode(ch))) {
        ++tokPos;
      } else {
        break;
      }
    }
  }

  // ### Token reading

  // This is the function that is called to fetch the next token. It
  // is somewhat obscure, because it works in character codes rather
  // than characters, and because operator parsing has been inlined
  // into it.
  //
  // All in the name of speed.
  //
  // The `forceRegexp` parameter is used in the one case where the
  // `tokRegexpAllowed` trick does not work. See `parseStatement`.

  function readToken_dot() {
    var next = input.charCodeAt(tokPos+1);
    if (next >= 48 && next <= 57) return readNumber(true);
    ++tokPos;
    return finishToken(_dot);
  }

  function readToken_slash() { // '/'
    var next = input.charCodeAt(tokPos+1);
    if (tokRegexpAllowed) {++tokPos; return readRegexp();}
    if (next === 61) return finishOp(_assign, 2);
    return finishOp(_slash, 1);
  }

  function readToken_mult_modulo() { // '%*'
    var next = input.charCodeAt(tokPos+1);
    if (next === 61) return finishOp(_assign, 2);
    return finishOp(_bin10, 1);
  }

  function readToken_pipe_amp(code) { // '|&'
    var next = input.charCodeAt(tokPos+1);
    if (next === code) return finishOp(code === 124 ? _bin1 : _bin2, 2);
    if (next === 61) return finishOp(_assign, 2);
    return finishOp(code === 124 ? _bin3 : _bin5, 1);
  }

  function readToken_caret() { // '^'
    var next = input.charCodeAt(tokPos+1);
    if (next === 61) return finishOp(_assign, 2);
    return finishOp(_bin4, 1);
  }

  function readToken_plus_min(code) { // '+-'
    var next = input.charCodeAt(tokPos+1);
    if (next === code) return finishOp(_incdec, 2);
    if (next === 61) return finishOp(_assign, 2);
    return finishOp(_plusmin, 1);
  }

  function readToken_lt_gt(code) { // '<>'
    var next = input.charCodeAt(tokPos+1);
    var size = 1;
    if (next === code) {
      size = code === 62 && input.charCodeAt(tokPos+2) === 62 ? 3 : 2;
      if (input.charCodeAt(tokPos + size) === 61) return finishOp(_assign, size + 1);
      return finishOp(_bin8, size);
    }
    if (next === 61)
      size = input.charCodeAt(tokPos+2) === 61 ? 3 : 2;
    return finishOp(_bin7, size);
  }

  function readToken_eq_excl(code) { // '=!'
    var next = input.charCodeAt(tokPos+1);
    if (next === 61) return finishOp(_bin6, input.charCodeAt(tokPos+2) === 61 ? 3 : 2);
    return finishOp(code === 61 ? _eq : _prefix, 1);
  }

  function getTokenFromCode(code) {
    switch(code) {
      // The interpretation of a dot depends on whether it is followed
      // by a digit.
    case 46: // '.'
      return readToken_dot();

      // Punctuation tokens.
    case 40: ++tokPos; return finishToken(_parenL);
    case 41: ++tokPos; return finishToken(_parenR);
    case 59: ++tokPos; return finishToken(_semi);
    case 44: ++tokPos; return finishToken(_comma);
    case 91: ++tokPos; return finishToken(_bracketL);
    case 93: ++tokPos; return finishToken(_bracketR);
    case 123: ++tokPos; return finishToken(_braceL);
    case 125: ++tokPos; return finishToken(_braceR);
    case 58: ++tokPos; return finishToken(_colon);
    case 63: ++tokPos; return finishToken(_question);

      // '0x' is a hexadecimal number.
    case 48: // '0'
      var next = input.charCodeAt(tokPos+1);
      if (next === 120 || next === 88) return readHexNumber();
      // Anything else beginning with a digit is an integer, octal
      // number, or float.
    case 49: case 50: case 51: case 52: case 53: case 54: case 55: case 56: case 57: // 1-9
      return readNumber(false);

      // Quotes produce strings.
    case 34: case 39: // '"', "'"
      return readString(code);

    // Operators are parsed inline in tiny state machines. '=' (61) is
    // often referred to. `finishOp` simply skips the amount of
    // characters it is given as second argument, and returns a token
    // of the type given by its first argument.

    case 47: // '/'
      return readToken_slash(code);

    case 37: case 42: // '%*'
      return readToken_mult_modulo();

    case 124: case 38: // '|&'
      return readToken_pipe_amp(code);

    case 94: // '^'
      return readToken_caret();

    case 43: case 45: // '+-'
      return readToken_plus_min(code);

    case 60: case 62: // '<>'
      return readToken_lt_gt(code);

    case 61: case 33: // '=!'
      return readToken_eq_excl(code);

    case 126: // '~'
      return finishOp(_prefix, 1);
    }

    return false;
  }

  function readToken(forceRegexp) {
    if (!forceRegexp) tokStart = tokPos;
    else tokPos = tokStart + 1;
    if (options.locations) tokStartLoc = new line_loc_t;
    if (forceRegexp) return readRegexp();
    if (tokPos >= inputLen) return finishToken(_eof);

    var code = input.charCodeAt(tokPos);
    // Identifier or keyword. '\uXXXX' sequences are allowed in
    // identifiers, so '\' also dispatches to that.
    if (isIdentifierStart(code) || code === 92 /* '\' */) return readWord();

    var tok = getTokenFromCode(code);

    if (tok === false) {
      // If we are here, we either found a non-ASCII identifier
      // character, or something that's entirely disallowed.
      var ch = String.fromCharCode(code);
      if (ch === "\\" || nonASCIIidentifierStart.test(ch)) return readWord();
      raise(tokPos, "Unexpected character '" + ch + "'");
    }
    return tok;
  }

  function finishOp(type, size) {
    var str = input.slice(tokPos, tokPos + size);
    tokPos += size;
    finishToken(type, str);
  }

  // Parse a regular expression. Some context-awareness is necessary,
  // since a '/' inside a '[]' set does not end the expression.

  function readRegexp() {
    var content = "", escaped, inClass, start = tokPos;
    for (;;) {
      if (tokPos >= inputLen) raise(start, "Unterminated regular expression");
      var ch = input.charAt(tokPos);
      if (newline.test(ch)) raise(start, "Unterminated regular expression");
      if (!escaped) {
        if (ch === "[") inClass = true;
        else if (ch === "]" && inClass) inClass = false;
        else if (ch === "/" && !inClass) break;
        escaped = ch === "\\";
      } else escaped = false;
      ++tokPos;
    }
    var content = input.slice(start, tokPos);
    ++tokPos;
    // Need to use `readWord1` because '\uXXXX' sequences are allowed
    // here (don't ask).
    var mods = readWord1();
    if (mods && !/^[gmsiy]*$/.test(mods)) raise(start, "Invalid regexp flag");
    return finishToken(_regexp, new RegExp(content, mods));
  }

  // Read an integer in the given radix. Return null if zero digits
  // were read, the integer value otherwise. When `len` is given, this
  // will return `null` unless the integer has exactly `len` digits.

  function readInt(radix, len) {
    var start = tokPos, total = 0;
    for (var i = 0, e = len == null ? Infinity : len; i < e; ++i) {
      var code = input.charCodeAt(tokPos), val;
      if (code >= 97) val = code - 97 + 10; // a
      else if (code >= 65) val = code - 65 + 10; // A
      else if (code >= 48 && code <= 57) val = code - 48; // 0-9
      else val = Infinity;
      if (val >= radix) break;
      ++tokPos;
      total = total * radix + val;
    }
    if (tokPos === start || len != null && tokPos - start !== len) return null;

    return total;
  }

  function readHexNumber() {
    tokPos += 2; // 0x
    var val = readInt(16);
    if (val == null) raise(tokStart + 2, "Expected hexadecimal number");
    if (isIdentifierStart(input.charCodeAt(tokPos))) raise(tokPos, "Identifier directly after number");
    return finishToken(_num, val);
  }

  // Read an integer, octal integer, or floating-point number.

  function readNumber(startsWithDot) {
    var start = tokPos, isFloat = false, octal = input.charCodeAt(tokPos) === 48;
    if (!startsWithDot && readInt(10) === null) raise(start, "Invalid number");
    if (input.charCodeAt(tokPos) === 46) {
      ++tokPos;
      readInt(10);
      isFloat = true;
    }
    var next = input.charCodeAt(tokPos);
    if (next === 69 || next === 101) { // 'eE'
      next = input.charCodeAt(++tokPos);
      if (next === 43 || next === 45) ++tokPos; // '+-'
      if (readInt(10) === null) raise(start, "Invalid number");
      isFloat = true;
    }
    if (isIdentifierStart(input.charCodeAt(tokPos))) raise(tokPos, "Identifier directly after number");

    var str = input.slice(start, tokPos), val;
    if (isFloat) val = parseFloat(str);
    else if (!octal || str.length === 1) val = parseInt(str, 10);
    else if (/[89]/.test(str) || strict) raise(start, "Invalid number");
    else val = parseInt(str, 8);
    return finishToken(_num, val);
  }

  // Read a string value, interpreting backslash-escapes.

  function readString(quote) {
    tokPos++;
    var out = "";
    for (;;) {
      if (tokPos >= inputLen) raise(tokStart, "Unterminated string constant");
      var ch = input.charCodeAt(tokPos);
      if (ch === quote) {
        ++tokPos;
        return finishToken(_string, out);
      }
      if (ch === 92) { // '\'
        ch = input.charCodeAt(++tokPos);
        var octal = /^[0-7]+/.exec(input.slice(tokPos, tokPos + 3));
        if (octal) octal = octal[0];
        while (octal && parseInt(octal, 8) > 255) octal = octal.slice(0, octal.length - 1);
        if (octal === "0") octal = null;
        ++tokPos;
        if (octal) {
          if (strict) raise(tokPos - 2, "Octal literal in strict mode");
          out += String.fromCharCode(parseInt(octal, 8));
          tokPos += octal.length - 1;
        } else {
          switch (ch) {
          case 110: out += "\n"; break; // 'n' -> '\n'
          case 114: out += "\r"; break; // 'r' -> '\r'
          case 120: out += String.fromCharCode(readHexChar(2)); break; // 'x'
          case 117: out += String.fromCharCode(readHexChar(4)); break; // 'u'
          case 85: out += String.fromCharCode(readHexChar(8)); break; // 'U'
          case 116: out += "\t"; break; // 't' -> '\t'
          case 98: out += "\b"; break; // 'b' -> '\b'
          case 118: out += "\u000b"; break; // 'v' -> '\u000b'
          case 102: out += "\f"; break; // 'f' -> '\f'
          case 48: out += "\0"; break; // 0 -> '\0'
          case 13: if (input.charCodeAt(tokPos) === 10) ++tokPos; // '\r\n'
          case 10: // ' \n'
            if (options.locations) { tokLineStart = tokPos; ++tokCurLine; }
            break;
          default: out += String.fromCharCode(ch); break;
          }
        }
      } else {
        if (ch === 13 || ch === 10 || ch === 8232 || ch === 8329) raise(tokStart, "Unterminated string constant");
        out += String.fromCharCode(ch); // '\'
        ++tokPos;
      }
    }
  }

  // Used to read character escape sequences ('\x', '\u', '\U').

  function readHexChar(len) {
    var n = readInt(16, len);
    if (n === null) raise(tokStart, "Bad character escape sequence");
    return n;
  }

  // Used to signal to callers of `readWord1` whether the word
  // contained any escape sequences. This is needed because words with
  // escape sequences must not be interpreted as keywords.

  var containsEsc;

  // Read an identifier, and return it as a string. Sets `containsEsc`
  // to whether the word contained a '\u' escape.
  //
  // Only builds up the word character-by-character when it actually
  // containeds an escape, as a micro-optimization.

  function readWord1() {
    containsEsc = false;
    var word, first = true, start = tokPos;
    for (;;) {
      var ch = input.charCodeAt(tokPos);
      if (isIdentifierChar(ch)) {
        if (containsEsc) word += input.charAt(tokPos);
        ++tokPos;
      } else if (ch === 92) { // "\"
        if (!containsEsc) word = input.slice(start, tokPos);
        containsEsc = true;
        if (input.charCodeAt(++tokPos) != 117) // "u"
          raise(tokPos, "Expecting Unicode escape sequence \\uXXXX");
        ++tokPos;
        var esc = readHexChar(4);
        var escStr = String.fromCharCode(esc);
        if (!escStr) raise(tokPos - 1, "Invalid Unicode escape");
        if (!(first ? isIdentifierStart(esc) : isIdentifierChar(esc)))
          raise(tokPos - 4, "Invalid Unicode escape");
        word += escStr;
      } else {
        break;
      }
      first = false;
    }
    return containsEsc ? word : input.slice(start, tokPos);
  }

  // Read an identifier or keyword token. Will check for reserved
  // words when necessary.

  function readWord() {
    var word = readWord1();
    var type = _name;
    if (!containsEsc) {
      if (isKeyword(word)) type = keywordTypes[word];
      else if (options.forbidReserved &&
               (options.ecmaVersion === 3 ? isReservedWord3 : isReservedWord5)(word) ||
               strict && isStrictReservedWord(word))
        raise(tokStart, "The keyword '" + word + "' is reserved");
    }
    return finishToken(type, word);
  }

  // ## Parser

  // A recursive descent parser operates by defining functions for all
  // syntactic elements, and recursively calling those, each function
  // advancing the input stream and returning an AST node. Precedence
  // of constructs (for example, the fact that `!x[1]` means `!(x[1])`
  // instead of `(!x)[1]` is handled by the fact that the parser
  // function that parses unary prefix operators is called first, and
  // in turn calls the function that parses `[]` subscripts — that
  // way, it'll receive the node for `x[1]` already parsed, and wraps
  // *that* in the unary operator node.
  //
  // Acorn uses an [operator precedence parser][opp] to handle binary
  // operator precedence, because it is much more compact than using
  // the technique outlined above, which uses different, nesting
  // functions to specify precedence, for all of the ten binary
  // precedence levels that JavaScript defines.
  //
  // [opp]: http://en.wikipedia.org/wiki/Operator-precedence_parser

  // ### Parser utilities

  // Continue to the next token.

  function next() {
    lastStart = tokStart;
    lastEnd = tokEnd;
    lastEndLoc = tokEndLoc;
    readToken();
  }

  // Enter strict mode. Re-reads the next token to please pedantic
  // tests ( 010; -- should fail).

  function setStrict(strct) {
    strict = strct;
    tokPos = lastEnd;
    while (tokPos < tokLineStart) {
      tokLineStart = input.lastIndexOf("\n", tokLineStart - 2) + 1;
      --tokCurLine;
    }
    skipSpace();
    readToken();
  }

  // Start an AST node, attaching a start offset.

  function node_t() {
    this.type = null;
    this.start = tokStart;
    this.end = null;
  }

  function node_loc_t() {
    this.start = tokStartLoc;
    this.end = null;
    if (sourceFile !== null) this.source = sourceFile;
  }

  function startNode() {
    var node = new node_t();
    if (options.locations)
      node.loc = new node_loc_t();
    if (options.ranges)
      node.range = [tokStart, 0];
    return node;
  }

  // Start a node whose start offset information should be based on
  // the start of another node. For example, a binary operator node is
  // only started after its left-hand side has already been parsed.

  function startNodeFrom(other) {
    var node = new node_t();
    node.start = other.start;
    if (options.locations) {
      node.loc = new node_loc_t();
      node.loc.start = other.loc.start;
    }
    if (options.ranges)
      node.range = [other.range[0], 0];

    return node;
  }

  // Finish an AST node, adding `type` and `end` properties.

  function finishNode(node, type) {
    node.type = type;
    node.end = lastEnd;
    if (options.locations)
      node.loc.end = lastEndLoc;
    if (options.ranges)
      node.range[1] = lastEnd;
    return node;
  }

  // Test whether a statement node is the string literal `"use strict"`.

  function isUseStrict(stmt) {
    return options.ecmaVersion >= 5 && stmt.type === "ExpressionStatement" &&
      stmt.expression.type === "Literal" && "use strict" === stmt.expression.value;
  }

  // Predicate that tests whether the next token is of the given
  // type, and if yes, consumes it as a side effect.

  function eat(type) {
    if (tokType === type) {
      next();
      return true;
    }
  }

  // Test whether a semicolon can be inserted at the current position.

  function canInsertSemicolon() {
    return !options.strictSemicolons &&
      (tokType === _eof || tokType === _braceR || newline.test(input.slice(lastEnd, tokStart)));
  }

  // Consume a semicolon, or, failing that, see if we are allowed to
  // pretend that there is a semicolon at this position.

  function semicolon() {
    if (!eat(_semi) && !canInsertSemicolon()) unexpected();
  }

  // Expect a token of a given type. If found, consume it, otherwise,
  // raise an unexpected token error.

  function expect(type) {
    if (tokType === type) next();
    else unexpected();
  }

  // Raise an unexpected token error.

  function unexpected() {
    raise(tokStart, "Unexpected token");
  }

  // Verify that a node is an lval — something that can be assigned
  // to.

  function checkLVal(expr) {
    if (expr.type !== "Identifier" && expr.type !== "MemberExpression")
      raise(expr.start, "Assigning to rvalue");
    if (strict && expr.type === "Identifier" && isStrictBadIdWord(expr.name))
      raise(expr.start, "Assigning to " + expr.name + " in strict mode");
  }

  // ### Statement parsing

  // Parse a program. Initializes the parser, reads any number of
  // statements, and wraps them in a Program node.  Optionally takes a
  // `program` argument.  If present, the statements will be appended
  // to its body instead of creating a new node.

  function parseTopLevel(program) {
    lastStart = lastEnd = tokPos;
    if (options.locations) lastEndLoc = new line_loc_t;
    inFunction = strict = null;
    labels = [];
    readToken();

    var node = program || startNode(), first = true;
    if (!program) node.body = [];
    while (tokType !== _eof) {
      var stmt = parseStatement();
      node.body.push(stmt);
      if (first && isUseStrict(stmt)) setStrict(true);
      first = false;
    }
    return finishNode(node, "Program");
  }

  var loopLabel = {kind: "loop"}, switchLabel = {kind: "switch"};

  // Parse a single statement.
  //
  // If expecting a statement and finding a slash operator, parse a
  // regular expression literal. This is to handle cases like
  // `if (foo) /blah/.exec(foo);`, where looking at the previous token
  // does not help.

  function parseStatement() {
    if (tokType === _slash)
      readToken(true);

    var starttype = tokType, node = startNode();

    // Most types of statements are recognized by the keyword they
    // start with. Many are trivial to parse, some require a bit of
    // complexity.

    switch (starttype) {
    case _break: case _continue:
      next();
      var isBreak = starttype === _break;
      if (eat(_semi) || canInsertSemicolon()) node.label = null;
      else if (tokType !== _name) unexpected();
      else {
        node.label = parseIdent();
        semicolon();
      }

      // Verify that there is an actual destination to break or
      // continue to.
      for (var i = 0; i < labels.length; ++i) {
        var lab = labels[i];
        if (node.label == null || lab.name === node.label.name) {
          if (lab.kind != null && (isBreak || lab.kind === "loop")) break;
          if (node.label && isBreak) break;
        }
      }
      if (i === labels.length) raise(node.start, "Unsyntactic " + starttype.keyword);
      return finishNode(node, isBreak ? "BreakStatement" : "ContinueStatement");

    case _debugger:
      next();
      semicolon();
      return finishNode(node, "DebuggerStatement");

    case _do:
      next();
      labels.push(loopLabel);
      node.body = parseStatement();
      labels.pop();
      expect(_while);
      node.test = parseParenExpression();
      semicolon();
      return finishNode(node, "DoWhileStatement");

      // Disambiguating between a `for` and a `for`/`in` loop is
      // non-trivial. Basically, we have to parse the init `var`
      // statement or expression, disallowing the `in` operator (see
      // the second parameter to `parseExpression`), and then check
      // whether the next token is `in`. When there is no init part
      // (semicolon immediately after the opening parenthesis), it is
      // a regular `for` loop.

    case _for:
      next();
      labels.push(loopLabel);
      expect(_parenL);
      if (tokType === _semi) return parseFor(node, null);
      if (tokType === _var) {
        var init = startNode();
        next();
        parseVar(init, true);
        if (init.declarations.length === 1 && eat(_in))
          return parseForIn(node, init);
        return parseFor(node, init);
      }
      var init = parseExpression(false, true);
      if (eat(_in)) {checkLVal(init); return parseForIn(node, init);}
      return parseFor(node, init);

    case _function:
      next();
      return parseFunction(node, true);

    case _if:
      next();
      node.test = parseParenExpression();
      node.consequent = parseStatement();
      node.alternate = eat(_else) ? parseStatement() : null;
      return finishNode(node, "IfStatement");

    case _return:
      if (!inFunction) raise(tokStart, "'return' outside of function");
      next();

      // In `return` (and `break`/`continue`), the keywords with
      // optional arguments, we eagerly look for a semicolon or the
      // possibility to insert one.

      if (eat(_semi) || canInsertSemicolon()) node.argument = null;
      else { node.argument = parseExpression(); semicolon(); }
      return finishNode(node, "ReturnStatement");

    case _switch:
      next();
      node.discriminant = parseParenExpression();
      node.cases = [];
      expect(_braceL);
      labels.push(switchLabel);

      // Statements under must be grouped (by label) in SwitchCase
      // nodes. `cur` is used to keep the node that we are currently
      // adding statements to.

      for (var cur, sawDefault; tokType != _braceR;) {
        if (tokType === _case || tokType === _default) {
          var isCase = tokType === _case;
          if (cur) finishNode(cur, "SwitchCase");
          node.cases.push(cur = startNode());
          cur.consequent = [];
          next();
          if (isCase) cur.test = parseExpression();
          else {
            if (sawDefault) raise(lastStart, "Multiple default clauses"); sawDefault = true;
            cur.test = null;
          }
          expect(_colon);
        } else {
          if (!cur) unexpected();
          cur.consequent.push(parseStatement());
        }
      }
      if (cur) finishNode(cur, "SwitchCase");
      next(); // Closing brace
      labels.pop();
      return finishNode(node, "SwitchStatement");

    case _throw:
      next();
      if (newline.test(input.slice(lastEnd, tokStart)))
        raise(lastEnd, "Illegal newline after throw");
      node.argument = parseExpression();
      semicolon();
      return finishNode(node, "ThrowStatement");

    case _try:
      next();
      node.block = parseBlock();
      node.handler = null;
      if (tokType === _catch) {
        var clause = startNode();
        next();
        expect(_parenL);
        clause.param = parseIdent();
        if (strict && isStrictBadIdWord(clause.param.name))
          raise(clause.param.start, "Binding " + clause.param.name + " in strict mode");
        expect(_parenR);
        clause.guard = null;
        clause.body = parseBlock();
        node.handler = finishNode(clause, "CatchClause");
      }
      node.finalizer = eat(_finally) ? parseBlock() : null;
      if (!node.handler && !node.finalizer)
        raise(node.start, "Missing catch or finally clause");
      return finishNode(node, "TryStatement");

    case _var:
      next();
      node = parseVar(node);
      semicolon();
      return node;

    case _while:
      next();
      node.test = parseParenExpression();
      labels.push(loopLabel);
      node.body = parseStatement();
      labels.pop();
      return finishNode(node, "WhileStatement");

    case _with:
      if (strict) raise(tokStart, "'with' in strict mode");
      next();
      node.object = parseParenExpression();
      node.body = parseStatement();
      return finishNode(node, "WithStatement");

    case _braceL:
      return parseBlock();

    case _semi:
      next();
      return finishNode(node, "EmptyStatement");

      // If the statement does not start with a statement keyword or a
      // brace, it's an ExpressionStatement or LabeledStatement. We
      // simply start parsing an expression, and afterwards, if the
      // next token is a colon and the expression was a simple
      // Identifier node, we switch to interpreting it as a label.

    default:
      var maybeName = tokVal, expr = parseExpression();
      if (starttype === _name && expr.type === "Identifier" && eat(_colon)) {
        for (var i = 0; i < labels.length; ++i)
          if (labels[i].name === maybeName) raise(expr.start, "Label '" + maybeName + "' is already declared");
        var kind = tokType.isLoop ? "loop" : tokType === _switch ? "switch" : null;
        labels.push({name: maybeName, kind: kind});
        node.body = parseStatement();
        labels.pop();
        node.label = expr;
        return finishNode(node, "LabeledStatement");
      } else {
        node.expression = expr;
        semicolon();
        return finishNode(node, "ExpressionStatement");
      }
    }
  }

  // Used for constructs like `switch` and `if` that insist on
  // parentheses around their expression.

  function parseParenExpression() {
    expect(_parenL);
    var val = parseExpression();
    expect(_parenR);
    return val;
  }

  // Parse a semicolon-enclosed block of statements, handling `"use
  // strict"` declarations when `allowStrict` is true (used for
  // function bodies).

  function parseBlock(allowStrict) {
    var node = startNode(), first = true, strict = false, oldStrict;
    node.body = [];
    expect(_braceL);
    while (!eat(_braceR)) {
      var stmt = parseStatement();
      node.body.push(stmt);
      if (first && isUseStrict(stmt)) {
        oldStrict = strict;
        setStrict(strict = true);
      }
      first = false
    }
    if (strict && !oldStrict) setStrict(false);
    return finishNode(node, "BlockStatement");
  }

  // Parse a regular `for` loop. The disambiguation code in
  // `parseStatement` will already have parsed the init statement or
  // expression.

  function parseFor(node, init) {
    node.init = init;
    expect(_semi);
    node.test = tokType === _semi ? null : parseExpression();
    expect(_semi);
    node.update = tokType === _parenR ? null : parseExpression();
    expect(_parenR);
    node.body = parseStatement();
    labels.pop();
    return finishNode(node, "ForStatement");
  }

  // Parse a `for`/`in` loop.

  function parseForIn(node, init) {
    node.left = init;
    node.right = parseExpression();
    expect(_parenR);
    node.body = parseStatement();
    labels.pop();
    return finishNode(node, "ForInStatement");
  }

  // Parse a list of variable declarations.

  function parseVar(node, noIn) {
    node.declarations = [];
    node.kind = "var";
    for (;;) {
      var decl = startNode();
      decl.id = parseIdent();
      if (strict && isStrictBadIdWord(decl.id.name))
        raise(decl.id.start, "Binding " + decl.id.name + " in strict mode");
      decl.init = eat(_eq) ? parseExpression(true, noIn) : null;
      node.declarations.push(finishNode(decl, "VariableDeclarator"));
      if (!eat(_comma)) break;
    }
    return finishNode(node, "VariableDeclaration");
  }

  // ### Expression parsing

  // These nest, from the most general expression type at the top to
  // 'atomic', nondivisible expression types at the bottom. Most of
  // the functions will simply let the function(s) below them parse,
  // and, *if* the syntactic construct they handle is present, wrap
  // the AST node that the inner parser gave them in another node.

  // Parse a full expression. The arguments are used to forbid comma
  // sequences (in argument lists, array literals, or object literals)
  // or the `in` operator (in for loops initalization expressions).

  function parseExpression(noComma, noIn) {
    var expr = parseMaybeAssign(noIn);
    if (!noComma && tokType === _comma) {
      var node = startNodeFrom(expr);
      node.expressions = [expr];
      while (eat(_comma)) node.expressions.push(parseMaybeAssign(noIn));
      return finishNode(node, "SequenceExpression");
    }
    return expr;
  }

  // Parse an assignment expression. This includes applications of
  // operators like `+=`.

  function parseMaybeAssign(noIn) {
    var left = parseMaybeConditional(noIn);
    if (tokType.isAssign) {
      var node = startNodeFrom(left);
      node.operator = tokVal;
      node.left = left;
      next();
      node.right = parseMaybeAssign(noIn);
      checkLVal(left);
      return finishNode(node, "AssignmentExpression");
    }
    return left;
  }

  // Parse a ternary conditional (`?:`) operator.

  function parseMaybeConditional(noIn) {
    var expr = parseExprOps(noIn);
    if (eat(_question)) {
      var node = startNodeFrom(expr);
      node.test = expr;
      node.consequent = parseExpression(true);
      expect(_colon);
      node.alternate = parseExpression(true, noIn);
      return finishNode(node, "ConditionalExpression");
    }
    return expr;
  }

  // Start the precedence parser.

  function parseExprOps(noIn) {
    return parseExprOp(parseMaybeUnary(noIn), -1, noIn);
  }

  // Parse binary operators with the operator precedence parsing
  // algorithm. `left` is the left-hand side of the operator.
  // `minPrec` provides context that allows the function to stop and
  // defer further parser to one of its callers when it encounters an
  // operator that has a lower precedence than the set it is parsing.

  function parseExprOp(left, minPrec, noIn) {
    var prec = tokType.binop;
    if (prec != null && (!noIn || tokType !== _in)) {
      if (prec > minPrec) {
        var node = startNodeFrom(left);
        node.left = left;
        node.operator = tokVal;
        next();
        node.right = parseExprOp(parseMaybeUnary(noIn), prec, noIn);
        var node = finishNode(node, /&&|\|\|/.test(node.operator) ? "LogicalExpression" : "BinaryExpression");
        return parseExprOp(node, minPrec, noIn);
      }
    }
    return left;
  }

  // Parse unary operators, both prefix and postfix.

  function parseMaybeUnary(noIn) {
    if (tokType.prefix) {
      var node = startNode(), update = tokType.isUpdate;
      node.operator = tokVal;
      node.prefix = true;
      next();
      node.argument = parseMaybeUnary(noIn);
      if (update) checkLVal(node.argument);
      else if (strict && node.operator === "delete" &&
               node.argument.type === "Identifier")
        raise(node.start, "Deleting local variable in strict mode");
      return finishNode(node, update ? "UpdateExpression" : "UnaryExpression");
    }
    var expr = parseExprSubscripts();
    while (tokType.postfix && !canInsertSemicolon()) {
      var node = startNodeFrom(expr);
      node.operator = tokVal;
      node.prefix = false;
      node.argument = expr;
      checkLVal(expr);
      next();
      expr = finishNode(node, "UpdateExpression");
    }
    return expr;
  }

  // Parse call, dot, and `[]`-subscript expressions.

  function parseExprSubscripts() {
    return parseSubscripts(parseExprAtom());
  }

  function parseSubscripts(base, noCalls) {
    if (eat(_dot)) {
      var node = startNodeFrom(base);
      node.object = base;
      node.property = parseIdent(true);
      node.computed = false;
      return parseSubscripts(finishNode(node, "MemberExpression"), noCalls);
    } else if (eat(_bracketL)) {
      var node = startNodeFrom(base);
      node.object = base;
      node.property = parseExpression();
      node.computed = true;
      expect(_bracketR);
      return parseSubscripts(finishNode(node, "MemberExpression"), noCalls);
    } else if (!noCalls && eat(_parenL)) {
      var node = startNodeFrom(base);
      node.callee = base;
      node.arguments = parseExprList(_parenR, false);
      return parseSubscripts(finishNode(node, "CallExpression"), noCalls);
    } else return base;
  }

  // Parse an atomic expression — either a single token that is an
  // expression, an expression started by a keyword like `function` or
  // `new`, or an expression wrapped in punctuation like `()`, `[]`,
  // or `{}`.

  function parseExprAtom() {
    switch (tokType) {
    case _this:
      var node = startNode();
      next();
      return finishNode(node, "ThisExpression");
    case _name:
      return parseIdent();
    case _num: case _string: case _regexp:
      var node = startNode();
      node.value = tokVal;
      node.raw = input.slice(tokStart, tokEnd);
      next();
      return finishNode(node, "Literal");

    case _null: case _true: case _false:
      var node = startNode();
      node.value = tokType.atomValue;
      node.raw = tokType.keyword
      next();
      return finishNode(node, "Literal");

    case _parenL:
      var tokStartLoc1 = tokStartLoc, tokStart1 = tokStart;
      next();
      var val = parseExpression();
      val.start = tokStart1;
      val.end = tokEnd;
      if (options.locations) {
        val.loc.start = tokStartLoc1;
        val.loc.end = tokEndLoc;
      }
      if (options.ranges)
        val.range = [tokStart1, tokEnd];
      expect(_parenR);
      return val;

    case _bracketL:
      var node = startNode();
      next();
      node.elements = parseExprList(_bracketR, true, true);
      return finishNode(node, "ArrayExpression");

    case _braceL:
      return parseObj();

    case _function:
      var node = startNode();
      next();
      return parseFunction(node, false);

    case _new:
      return parseNew();

    default:
      unexpected();
    }
  }

  // New's precedence is slightly tricky. It must allow its argument
  // to be a `[]` or dot subscript expression, but not a call — at
  // least, not without wrapping it in parentheses. Thus, it uses the

  function parseNew() {
    var node = startNode();
    next();
    node.callee = parseSubscripts(parseExprAtom(), true);
    if (eat(_parenL)) node.arguments = parseExprList(_parenR, false);
    else node.arguments = [];
    return finishNode(node, "NewExpression");
  }

  // Parse an object literal.

  function parseObj() {
    var node = startNode(), first = true, sawGetSet = false;
    node.properties = [];
    next();
    while (!eat(_braceR)) {
      if (!first) {
        expect(_comma);
        if (options.allowTrailingCommas && eat(_braceR)) break;
      } else first = false;

      var prop = {key: parsePropertyName()}, isGetSet = false, kind;
      if (eat(_colon)) {
        prop.value = parseExpression(true);
        kind = prop.kind = "init";
      } else if (options.ecmaVersion >= 5 && prop.key.type === "Identifier" &&
                 (prop.key.name === "get" || prop.key.name === "set")) {
        isGetSet = sawGetSet = true;
        kind = prop.kind = prop.key.name;
        prop.key = parsePropertyName();
        if (tokType !== _parenL) unexpected();
        prop.value = parseFunction(startNode(), false);
      } else unexpected();

      // getters and setters are not allowed to clash — either with
      // each other or with an init property — and in strict mode,
      // init properties are also not allowed to be repeated.

      if (prop.key.type === "Identifier" && (strict || sawGetSet)) {
        for (var i = 0; i < node.properties.length; ++i) {
          var other = node.properties[i];
          if (other.key.name === prop.key.name) {
            var conflict = kind == other.kind || isGetSet && other.kind === "init" ||
              kind === "init" && (other.kind === "get" || other.kind === "set");
            if (conflict && !strict && kind === "init" && other.kind === "init") conflict = false;
            if (conflict) raise(prop.key.start, "Redefinition of property");
          }
        }
      }
      node.properties.push(prop);
    }
    return finishNode(node, "ObjectExpression");
  }

  function parsePropertyName() {
    if (tokType === _num || tokType === _string) return parseExprAtom();
    return parseIdent(true);
  }

  // Parse a function declaration or literal (depending on the
  // `isStatement` parameter).

  function parseFunction(node, isStatement) {
    if (tokType === _name) node.id = parseIdent();
    else if (isStatement) unexpected();
    else node.id = null;
    node.params = [];
    var first = true;
    expect(_parenL);
    while (!eat(_parenR)) {
      if (!first) expect(_comma); else first = false;
      node.params.push(parseIdent());
    }

    // Start a new scope with regard to labels and the `inFunction`
    // flag (restore them to their old value afterwards).
    var oldInFunc = inFunction, oldLabels = labels;
    inFunction = true; labels = [];
    node.body = parseBlock(true);
    inFunction = oldInFunc; labels = oldLabels;

    // If this is a strict mode function, verify that argument names
    // are not repeated, and it does not try to bind the words `eval`
    // or `arguments`.
    if (strict || node.body.body.length && isUseStrict(node.body.body[0])) {
      for (var i = node.id ? -1 : 0; i < node.params.length; ++i) {
        var id = i < 0 ? node.id : node.params[i];
        if (isStrictReservedWord(id.name) || isStrictBadIdWord(id.name))
          raise(id.start, "Defining '" + id.name + "' in strict mode");
        if (i >= 0) for (var j = 0; j < i; ++j) if (id.name === node.params[j].name)
          raise(id.start, "Argument name clash in strict mode");
      }
    }

    return finishNode(node, isStatement ? "FunctionDeclaration" : "FunctionExpression");
  }

  // Parses a comma-separated list of expressions, and returns them as
  // an array. `close` is the token type that ends the list, and
  // `allowEmpty` can be turned on to allow subsequent commas with
  // nothing in between them to be parsed as `null` (which is needed
  // for array literals).

  function parseExprList(close, allowTrailingComma, allowEmpty) {
    var elts = [], first = true;
    while (!eat(close)) {
      if (!first) {
        expect(_comma);
        if (allowTrailingComma && options.allowTrailingCommas && eat(close)) break;
      } else first = false;

      if (allowEmpty && tokType === _comma) elts.push(null);
      else elts.push(parseExpression(true));
    }
    return elts;
  }

  // Parse the next token as an identifier. If `liberal` is true (used
  // when parsing properties), it will also convert keywords into
  // identifiers.

  function parseIdent(liberal) {
    var node = startNode();
    node.name = tokType === _name ? tokVal : (liberal && !options.forbidReserved && tokType.keyword) || unexpected();
    next();
    return finishNode(node, "Identifier");
  }

});

/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50, regexp: true */
/*global define */

define('HintUtils',['require','exports','module','thirdparty/acorn/acorn'],function (require, exports, module) {
    

    var acorn                       = require("thirdparty/acorn/acorn");

    var LANGUAGE_ID                 = "javascript",
        HTML_LANGUAGE_ID            = "html",
        SUPPORTED_LANGUAGES         = [LANGUAGE_ID, HTML_LANGUAGE_ID],
        SINGLE_QUOTE                = "'",
        DOUBLE_QUOTE                = "\"";

    /**
     * Create a hint token with name value that occurs at the given list of
     * positions.
     * 
     * @param {string} value - name of the new hint token
     * @param {?Array.<number>=} positions - optional list of positions at which
     *      the token occurs
     * @return {Object} - a new hint token
     */
    function makeToken(value, positions) {
        positions = positions || [];

        return {
            value: value,
            positions: positions
        };
    }

    /**
     * Is the string key perhaps a valid JavaScript identifier?
     *
     * @param {string} key - string to test.
     * @return {boolean} - could key be a valid identifier?
     */
    function maybeIdentifier(key) {
        var result = false,
            i;

        for (i = 0; i < key.length; i++) {
            result = acorn.isIdentifierChar(key.charCodeAt(i));
            if (!result) {
                break;
            }
        }

        return result;
    }

    /**
     * Is the token's class hintable? (A very conservative test.) 
     * 
     * @param {Object} token - the token to test for hintability
     * @return {boolean} - could the token be hintable?
     */
    function hintable(token) {
        switch (token.type) {
        case "comment":
        case "number":
        case "regexp":
        case "string":
        // exclude variable & param decls
        case "def":
            return false;
        default:
            return true;
        }
    }

    /**
     *  Determine if hints should be displayed for the given key.
     *
     * @param {string} key - key entered by the user
     * @return {boolean} true if the hints should be shown for the key,
     * false otherwise.
     */
    function hintableKey(key) {
        return (key === null || key === "." || maybeIdentifier(key));
    }

    /**
     * Divide a path into directory and filename parts
     * 
     * @param {string} path - a URI with directories separated by /
     * @return {{dir: string, file: string}} - a pair of strings that
     *      correspond to the directory and filename of the given path.
     */
    function splitPath(path) {
        var index   = path.lastIndexOf("/"),
            dir     = (index === -1) ? "" : path.substring(0, index),
            file    = path.substring(index + 1, path.length);
        
        return {dir: dir, file: file };
    }
    
    /*
     * Get a JS-hints-specific event name. Used to prevent event namespace
     * pollution.
     * 
     * @param {string} name - the unqualified event name
     * @return {string} - the qualified event name
     */
    function eventName(name) {
        var EVENT_TAG = "brackets-js-hints";
        return name + "." + EVENT_TAG;
    }

    /*
     * Annotate a list of tokens as literals of a particular kind;
     * if string literals, annotate with an appropriate delimiter.
     *
     * @param {Array.<Object>} literals - list of hint tokens
     * @param {string} kind - the kind of literals in the list (e.g., "string")
     * @return {Array.<Object>} - the input array; to each object in the array a
     *      new literal {boolean} property has been added to indicate that it
     *      is a literal hint, and also a new kind {string} property to indicate
     *      the literal kind. For string literals, a delimiter property is also
     *      added to indicate what the default delimiter should be (viz. a
     *      single or double quotation mark).
     */
    function annotateLiterals(literals, kind) {
        return literals.map(function (t) {
            t.literal = true;
            t.kind = kind;
            t.origin = "ecma5";
            if (kind === "string") {
                if (/[\\\\]*[^\\]"/.test(t.value)) {
                    t.delimiter = SINGLE_QUOTE;
                } else {
                    t.delimiter = DOUBLE_QUOTE;
                }
            }
            return t;
        });
    }

    /*
     * Annotate a list of tokens as keywords
     * 
     * @param {Array.<Object>} keyword - list of keyword tokens
     * @return {Array.<Object>} - the input array; to each object in the array a
     *      new keyword {boolean} property has been added to indicate that the
     *      hint is a keyword.
     */
    function annotateKeywords(keywords) {
        return keywords.map(function (t) {
            t.keyword = true;
            t.origin = "ecma5";
            return t;
        });
    }

    function isSupportedLanguage(languageId) {
        return SUPPORTED_LANGUAGES.indexOf(languageId) !== -1;
    }

    var KEYWORD_NAMES   = [
        "break", "case", "catch", "continue", "debugger", "default", "delete",
        "do", "else", "finally", "for", "function", "if", "in", "instanceof",
        "new", "return", "switch", "this", "throw", "try", "typeof", "var",
        "void", "while", "with"
    ],
        KEYWORD_TOKENS  = KEYWORD_NAMES.map(function (t) {
            return makeToken(t, []);
        }),
        KEYWORDS        = annotateKeywords(KEYWORD_TOKENS);
    
    var LITERAL_NAMES   = [
        "true", "false", "null"
    ],
        LITERAL_TOKENS  = LITERAL_NAMES.map(function (t) {
            return makeToken(t, []);
        }),
        LITERALS        = annotateLiterals(LITERAL_TOKENS);

    exports.makeToken                   = makeToken;
    exports.hintable                    = hintable;
    exports.hintableKey                 = hintableKey;
    exports.maybeIdentifier             = maybeIdentifier;
    exports.splitPath                   = splitPath;
    exports.eventName                   = eventName;
    exports.annotateLiterals            = annotateLiterals;
    exports.isSupportedLanguage         = isSupportedLanguage;
    exports.KEYWORDS                    = KEYWORDS;
    exports.LITERALS                    = LITERALS;
    exports.LANGUAGE_ID                 = LANGUAGE_ID;
    exports.SINGLE_QUOTE                = SINGLE_QUOTE;
    exports.DOUBLE_QUOTE                = DOUBLE_QUOTE;
    exports.SUPPORTED_LANGUAGES         = SUPPORTED_LANGUAGES;
});

/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50, regexp: true */
/*global define */

define('MessageIds',['require','exports','module'],function (require, exports, module) {
    

    var TERN_ADD_FILES_MSG          = "AddFiles",
        TERN_UPDATE_FILE_MSG        = "UpdateFile",
        TERN_INIT_MSG               = "Init",
        TERN_JUMPTODEF_MSG          = "JumptoDef",
        TERN_COMPLETIONS_MSG        = "Completions",
        TERN_GET_FILE_MSG           = "GetFile",
        TERN_CALLED_FUNC_TYPE_MSG   = "FunctionType",
        TERN_PRIME_PUMP_MSG         = "PrimePump",
        TERN_GET_GUESSES_MSG        = "GetGuesses",
        TERN_WORKER_READY           = "WorkerReady";

    // Message parameter constants
    var TERN_FILE_INFO_TYPE_PART    = "part",
        TERN_FILE_INFO_TYPE_FULL    = "full",
        TERN_FILE_INFO_TYPE_EMPTY   = "empty";


    exports.TERN_ADD_FILES_MSG          = TERN_ADD_FILES_MSG;
    exports.TERN_JUMPTODEF_MSG          = TERN_JUMPTODEF_MSG;
    exports.TERN_COMPLETIONS_MSG        = TERN_COMPLETIONS_MSG;
    exports.TERN_INIT_MSG               = TERN_INIT_MSG;
    exports.TERN_GET_FILE_MSG           = TERN_GET_FILE_MSG;
    exports.TERN_CALLED_FUNC_TYPE_MSG   = TERN_CALLED_FUNC_TYPE_MSG;
    exports.TERN_PRIME_PUMP_MSG         = TERN_PRIME_PUMP_MSG;
    exports.TERN_GET_GUESSES_MSG        = TERN_GET_GUESSES_MSG;
    exports.TERN_UPDATE_FILE_MSG        = TERN_UPDATE_FILE_MSG;
    exports.TERN_WORKER_READY           = TERN_WORKER_READY;
    exports.TERN_FILE_INFO_TYPE_PART    = TERN_FILE_INFO_TYPE_PART;
    exports.TERN_FILE_INFO_TYPE_FULL    = TERN_FILE_INFO_TYPE_FULL;
    exports.TERN_FILE_INFO_TYPE_EMPTY   = TERN_FILE_INFO_TYPE_EMPTY;
});



/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/**

    This class parses configuration values from a JSON object. The expected
    name of the file is “.jscodehints” but this class does not actually read
    the file, it just provides a constant, FILE_NAME.

    The following properties are supported:

     "excluded-directories" - An array of directory strings that match
     directories that will be excluded from analysis. Directories may be
     excluded if they contain automated tests that aren’t relevant for code hinting.
     The wildcards “*” and “?” are supported in strings.

     "excluded-files" - An array of file strings that match files that will
     be excluded from analysis. Files are typically excluded because
     their API is in a JSON file or they are known to cause problems with either
     stability or performance. The wildcards “*” and “?” are supported in strings.

     "max-file-count" - Limits the total number of files that can be processed for
     hints.

     "max-file-size" - Files larger than this number of bytes will not be parsed.

     The strings in "excluded-directories" or "excluded-files" will be treated as a
     regular expression if the first and last characters of the string are the '/'
     character. Note the '\' character in a regular expression needs to be escaped
     to be valid in a JSON formatted file. For example "/[\d]/" becomes "/[\\d]/".

     Example file:

     {
     "excluded-directories" : ["/ex[\\w]*ed/"],
     "excluded-files" : ["require.js", "jquery*.js", "less*.min.js", "ember*.js", "d2?.js", "d3*"],
     "max-file-count": 100,
     "max-file-size": 524288
     }

 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50, regexp: true */
/*global define, brackets, $ */

define('Preferences',['require','exports','module'],function (require, exports, module) {
    

    var StringUtils      = brackets.getModule("utils/StringUtils");

    /**
     *  Convert an array of strings with optional wildcards, to an equivalent
     *  regular expression.
     *
     * @param {Array.<string|RegExp>} settings
     * @param {RegExp} defaultRegExp - default regular expression to use
     * if none if provided.
     * @return {RegExp} Regular expression that captures the array of string
     * with optional wildcards.
     */
    function settingsToRegExp(settings, defaultRegExp) {
        var regExpString = "";

        if (settings instanceof Array && settings.length > 0) {

            // Append default settings to user settings. The default
            // settings are builtin and cannot be overridden.
            if (defaultRegExp) {
                settings.push("/" + defaultRegExp.source + "/");
            }

            // convert each string, with optional wildcards to an equivalent
            // string in a regular expression.
            settings.forEach(function (value, index) {

                if (typeof value === "string") {
                    var isRegExp = value[0] === '/' && value[value.length - 1] === '/';

                    if (isRegExp) {
                        value = value.substring(1, value.length - 1);
                    } else {
                        value = StringUtils.regexEscape(value);

                        // convert user input wildcard, "*" or "?", to a regular
                        // expression. We can just replace the escaped "*" or "?"
                        // since we know it is a wildcard.
                        value = value.replace("\\?", ".?");
                        value = value.replace("\\*", ".*");

                        // Add "^" and "$" to prevent matching in the middle of strings.
                        value = "^" + value + "$";
                    }

                    if (index > 0) {
                        regExpString += "|";
                    }

                    regExpString = regExpString.concat(value);
                }
            });
        }

        if (!regExpString) {
            return defaultRegExp;
        }

        return new RegExp(regExpString);
    }

    /**
     * Constructor to create a default preference object.
     *
     * @constructor
     * @param {Object=} prefs - preference object
     */
    function Preferences(prefs) {
        var DEFAULT_EXCLUDED_DIRECTORIES = null, /* no exclusions */
            // exclude require and jquery since we have special knowledge of those
            // temporarily exclude less*min.js because it is causing instability in tern.
            // exclude ember*.js as it is currently causing problems
            DEFAULT_EXCLUDED_FILES = /^require.*\.js$|^jquery.*\.js$|^less.*\.min\.js$/,
            DEFAULT_MAX_FILE_COUNT = 100,
            DEFAULT_MAX_FILE_SIZE = 512 * 1024;

        if (prefs) {
            this._excludedDirectories = settingsToRegExp(prefs["excluded-directories"],
                DEFAULT_EXCLUDED_DIRECTORIES);
            this._excludedFiles = settingsToRegExp(prefs["excluded-files"],
                DEFAULT_EXCLUDED_FILES);
            this._maxFileCount = prefs["max-file-count"];
            this._maxFileSize = prefs["max-file-size"];

            // sanity check values
            if (!this._maxFileCount || this._maxFileCount < 0) {
                this._maxFileCount = DEFAULT_MAX_FILE_COUNT;
            }

            if (!this._maxFileSize || this._maxFileSize < 0) {
                this._maxFileSize = DEFAULT_MAX_FILE_SIZE;
            }

        } else {
            this._excludedDirectories = DEFAULT_EXCLUDED_DIRECTORIES;
            this._excludedFiles = DEFAULT_EXCLUDED_FILES;
            this._maxFileCount = DEFAULT_MAX_FILE_COUNT;
            this._maxFileSize = DEFAULT_MAX_FILE_SIZE;
        }
    }

    Preferences.FILE_NAME = ".jscodehints";

    /**
     * Get the regular expression for excluded directories.
     *
     * @returns {?RegExp} Regular expression matching the directories that should
     * be excluded. Returns null if no directories are excluded.
     */
    Preferences.prototype.getExcludedDirectories = function () {
        return this._excludedDirectories;
    };

    /**
     * Get the regular expression for excluded files.
     *
     * @returns {?RegExp} Regular expression matching the files that should
     * be excluded. Returns null if no files are excluded.
     */
    Preferences.prototype.getExcludedFiles = function () {
        return this._excludedFiles;
    };

    /**
     * Get the maximum number of files that will be analyzed.
     *
     * @returns {number}
     */
    Preferences.prototype.getMaxFileCount = function () {
        return this._maxFileCount;
    };

    /**
     * Get the maximum size of a file that will be analyzed. Files that are
     * larger will be ignored.
     *
     * @returns {number}
     */
    Preferences.prototype.getMaxFileSize = function () {
        return this._maxFileSize;
    };

    module.exports = Preferences;

});


/**
 * @license RequireJS text 2.0.6 Copyright (c) 2010-2012, The Dojo Foundation All Rights Reserved.
 * Available via the MIT or new BSD license.
 * see: http://github.com/requirejs/text for details
 */
/*jslint regexp: true */
/*global require, XMLHttpRequest, ActiveXObject,
  define, window, process, Packages,
  java, location, Components, FileUtils */

define('text',['module'], function (module) {
    

    var text, fs, Cc, Ci,
        progIds = ['Msxml2.XMLHTTP', 'Microsoft.XMLHTTP', 'Msxml2.XMLHTTP.4.0'],
        xmlRegExp = /^\s*<\?xml(\s)+version=[\'\"](\d)*.(\d)*[\'\"](\s)*\?>/im,
        bodyRegExp = /<body[^>]*>\s*([\s\S]+)\s*<\/body>/im,
        hasLocation = typeof location !== 'undefined' && location.href,
        defaultProtocol = hasLocation && location.protocol && location.protocol.replace(/\:/, ''),
        defaultHostName = hasLocation && location.hostname,
        defaultPort = hasLocation && (location.port || undefined),
        buildMap = [],
        masterConfig = (module.config && module.config()) || {};

    text = {
        version: '2.0.6',

        strip: function (content) {
            //Strips <?xml ...?> declarations so that external SVG and XML
            //documents can be added to a document without worry. Also, if the string
            //is an HTML document, only the part inside the body tag is returned.
            if (content) {
                content = content.replace(xmlRegExp, "");
                var matches = content.match(bodyRegExp);
                if (matches) {
                    content = matches[1];
                }
            } else {
                content = "";
            }
            return content;
        },

        jsEscape: function (content) {
            return content.replace(/(['\\])/g, '\\$1')
                .replace(/[\f]/g, "\\f")
                .replace(/[\b]/g, "\\b")
                .replace(/[\n]/g, "\\n")
                .replace(/[\t]/g, "\\t")
                .replace(/[\r]/g, "\\r")
                .replace(/[\u2028]/g, "\\u2028")
                .replace(/[\u2029]/g, "\\u2029");
        },

        createXhr: masterConfig.createXhr || function () {
            //Would love to dump the ActiveX crap in here. Need IE 6 to die first.
            var xhr, i, progId;
            if (typeof XMLHttpRequest !== "undefined") {
                return new XMLHttpRequest();
            } else if (typeof ActiveXObject !== "undefined") {
                for (i = 0; i < 3; i += 1) {
                    progId = progIds[i];
                    try {
                        xhr = new ActiveXObject(progId);
                    } catch (e) {}

                    if (xhr) {
                        progIds = [progId];  // so faster next time
                        break;
                    }
                }
            }

            return xhr;
        },

        /**
         * Parses a resource name into its component parts. Resource names
         * look like: module/name.ext!strip, where the !strip part is
         * optional.
         * @param {String} name the resource name
         * @returns {Object} with properties "moduleName", "ext" and "strip"
         * where strip is a boolean.
         */
        parseName: function (name) {
            var modName, ext, temp,
                strip = false,
                index = name.indexOf("."),
                isRelative = name.indexOf('./') === 0 ||
                             name.indexOf('../') === 0;

            if (index !== -1 && (!isRelative || index > 1)) {
                modName = name.substring(0, index);
                ext = name.substring(index + 1, name.length);
            } else {
                modName = name;
            }

            temp = ext || modName;
            index = temp.indexOf("!");
            if (index !== -1) {
                //Pull off the strip arg.
                strip = temp.substring(index + 1) === "strip";
                temp = temp.substring(0, index);
                if (ext) {
                    ext = temp;
                } else {
                    modName = temp;
                }
            }

            return {
                moduleName: modName,
                ext: ext,
                strip: strip
            };
        },

        xdRegExp: /^((\w+)\:)?\/\/([^\/\\]+)/,

        /**
         * Is an URL on another domain. Only works for browser use, returns
         * false in non-browser environments. Only used to know if an
         * optimized .js version of a text resource should be loaded
         * instead.
         * @param {String} url
         * @returns Boolean
         */
        useXhr: function (url, protocol, hostname, port) {
            var uProtocol, uHostName, uPort,
                match = text.xdRegExp.exec(url);
            if (!match) {
                return true;
            }
            uProtocol = match[2];
            uHostName = match[3];

            uHostName = uHostName.split(':');
            uPort = uHostName[1];
            uHostName = uHostName[0];

            return (!uProtocol || uProtocol === protocol) &&
                   (!uHostName || uHostName.toLowerCase() === hostname.toLowerCase()) &&
                   ((!uPort && !uHostName) || uPort === port);
        },

        finishLoad: function (name, strip, content, onLoad) {
            content = strip ? text.strip(content) : content;
            if (masterConfig.isBuild) {
                buildMap[name] = content;
            }
            onLoad(content);
        },

        load: function (name, req, onLoad, config) {
            //Name has format: some.module.filext!strip
            //The strip part is optional.
            //if strip is present, then that means only get the string contents
            //inside a body tag in an HTML string. For XML/SVG content it means
            //removing the <?xml ...?> declarations so the content can be inserted
            //into the current doc without problems.

            // Do not bother with the work if a build and text will
            // not be inlined.
            if (config.isBuild && !config.inlineText) {
                onLoad();
                return;
            }

            masterConfig.isBuild = config.isBuild;

            var parsed = text.parseName(name),
                nonStripName = parsed.moduleName +
                    (parsed.ext ? '.' + parsed.ext : ''),
                url = req.toUrl(nonStripName),
                useXhr = (masterConfig.useXhr) ||
                         text.useXhr;

            //Load the text. Use XHR if possible and in a browser.
            if (!hasLocation || useXhr(url, defaultProtocol, defaultHostName, defaultPort)) {
                text.get(url, function (content) {
                    text.finishLoad(name, parsed.strip, content, onLoad);
                }, function (err) {
                    if (onLoad.error) {
                        onLoad.error(err);
                    }
                });
            } else {
                //Need to fetch the resource across domains. Assume
                //the resource has been optimized into a JS module. Fetch
                //by the module name + extension, but do not include the
                //!strip part to avoid file system issues.
                req([nonStripName], function (content) {
                    text.finishLoad(parsed.moduleName + '.' + parsed.ext,
                                    parsed.strip, content, onLoad);
                });
            }
        },

        write: function (pluginName, moduleName, write, config) {
            if (buildMap.hasOwnProperty(moduleName)) {
                var content = text.jsEscape(buildMap[moduleName]);
                write.asModule(pluginName + "!" + moduleName,
                               "define(function () { return '" +
                                   content +
                               "';});\n");
            }
        },

        writeFile: function (pluginName, moduleName, req, write, config) {
            var parsed = text.parseName(moduleName),
                extPart = parsed.ext ? '.' + parsed.ext : '',
                nonStripName = parsed.moduleName + extPart,
                //Use a '.js' file name so that it indicates it is a
                //script that can be loaded across domains.
                fileName = req.toUrl(parsed.moduleName + extPart) + '.js';

            //Leverage own load() method to load plugin value, but only
            //write out values that do not have the strip argument,
            //to avoid any potential issues with ! in file names.
            text.load(nonStripName, req, function (value) {
                //Use own write() method to construct full module value.
                //But need to create shell that translates writeFile's
                //write() to the right interface.
                var textWrite = function (contents) {
                    return write(fileName, contents);
                };
                textWrite.asModule = function (moduleName, contents) {
                    return write.asModule(moduleName, fileName, contents);
                };

                text.write(pluginName, nonStripName, textWrite, config);
            }, config);
        }
    };

    if (masterConfig.env === 'node' || (!masterConfig.env &&
            typeof process !== "undefined" &&
            process.versions &&
            !!process.versions.node)) {
        //Using special require.nodeRequire, something added by r.js.
        fs = require.nodeRequire('fs');

        text.get = function (url, callback) {
            var file = fs.readFileSync(url, 'utf8');
            //Remove BOM (Byte Mark Order) from utf8 files if it is there.
            if (file.indexOf('\uFEFF') === 0) {
                file = file.substring(1);
            }
            callback(file);
        };
    } else if (masterConfig.env === 'xhr' || (!masterConfig.env &&
            text.createXhr())) {
        text.get = function (url, callback, errback, headers) {
            var xhr = text.createXhr(), header;
            xhr.open('GET', url, true);

            //Allow plugins direct access to xhr headers
            if (headers) {
                for (header in headers) {
                    if (headers.hasOwnProperty(header)) {
                        xhr.setRequestHeader(header.toLowerCase(), headers[header]);
                    }
                }
            }

            //Allow overrides specified in config
            if (masterConfig.onXhr) {
                masterConfig.onXhr(xhr, url);
            }

            xhr.onreadystatechange = function (evt) {
                var status, err;
                //Do not explicitly handle errors, those should be
                //visible via console output in the browser.
                if (xhr.readyState === 4) {
                    status = xhr.status;
                    if (status > 399 && status < 600) {
                        //An http 4xx or 5xx error. Signal an error.
                        err = new Error(url + ' HTTP status: ' + status);
                        err.xhr = xhr;
                        errback(err);
                    } else {
                        callback(xhr.responseText);
                    }

                    if (masterConfig.onXhrComplete) {
                        masterConfig.onXhrComplete(xhr, url);
                    }
                }
            };
            xhr.send(null);
        };
    } else if (masterConfig.env === 'rhino' || (!masterConfig.env &&
            typeof Packages !== 'undefined' && typeof java !== 'undefined')) {
        //Why Java, why is this so awkward?
        text.get = function (url, callback) {
            var stringBuffer, line,
                encoding = "utf-8",
                file = new java.io.File(url),
                lineSeparator = java.lang.System.getProperty("line.separator"),
                input = new java.io.BufferedReader(new java.io.InputStreamReader(new java.io.FileInputStream(file), encoding)),
                content = '';
            try {
                stringBuffer = new java.lang.StringBuffer();
                line = input.readLine();

                // Byte Order Mark (BOM) - The Unicode Standard, version 3.0, page 324
                // http://www.unicode.org/faq/utf_bom.html

                // Note that when we use utf-8, the BOM should appear as "EF BB BF", but it doesn't due to this bug in the JDK:
                // http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4508058
                if (line && line.length() && line.charAt(0) === 0xfeff) {
                    // Eat the BOM, since we've already found the encoding on this file,
                    // and we plan to concatenating this buffer with others; the BOM should
                    // only appear at the top of a file.
                    line = line.substring(1);
                }

                stringBuffer.append(line);

                while ((line = input.readLine()) !== null) {
                    stringBuffer.append(lineSeparator);
                    stringBuffer.append(line);
                }
                //Make sure we return a JavaScript string and not a Java string.
                content = String(stringBuffer.toString()); //String
            } finally {
                input.close();
            }
            callback(content);
        };
    } else if (masterConfig.env === 'xpconnect' || (!masterConfig.env &&
            typeof Components !== 'undefined' && Components.classes &&
            Components.interfaces)) {
        //Avert your gaze!
        Cc = Components.classes,
        Ci = Components.interfaces;
        Components.utils['import']('resource://gre/modules/FileUtils.jsm');

        text.get = function (url, callback) {
            var inStream, convertStream,
                readData = {},
                fileObj = new FileUtils.File(url);

            //XPCOM, you so crazy
            try {
                inStream = Cc['@mozilla.org/network/file-input-stream;1']
                           .createInstance(Ci.nsIFileInputStream);
                inStream.init(fileObj, 1, 0, false);

                convertStream = Cc['@mozilla.org/intl/converter-input-stream;1']
                                .createInstance(Ci.nsIConverterInputStream);
                convertStream.init(inStream, "utf-8", inStream.available(),
                Ci.nsIConverterInputStream.DEFAULT_REPLACEMENT_CHARACTER);

                convertStream.readString(inStream.available(), readData);
                convertStream.close();
                inStream.close();
                callback(readData.value);
            } catch (e) {
                throw new Error((fileObj && fileObj.path || '') + ': ' + e);
            }
        };
    }
    return text;
});

define('text!thirdparty/tern/defs/ecma5.json',[],function () { return '{\n  "!name": "ecma5",\n  "!define": {"Error.prototype": "Error.prototype"},\n  "Infinity": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Infinity",\n    "!doc": "A numeric value representing infinity."\n  },\n  "undefined": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/undefined",\n    "!doc": "The value undefined."\n  },\n  "NaN": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/NaN",\n    "!doc": "A value representing Not-A-Number."\n  },\n  "Object": {\n    "!type": "fn()",\n    "getPrototypeOf": {\n      "!type": "fn(obj: ?) -> ?",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/getPrototypeOf",\n      "!doc": "Returns the prototype (i.e. the internal prototype) of the specified object."\n    },\n    "create": {\n      "!type": "fn(proto: ?) -> !custom:Object_create",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/create",\n      "!doc": "Creates a new object with the specified prototype object and properties."\n    },\n    "defineProperty": {\n      "!type": "fn(obj: ?, prop: string, desc: ?)",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/defineProperty",\n      "!doc": "Defines a new property directly on an object, or modifies an existing property on an object, and returns the object. If you want to see how to use the Object.defineProperty method with a binary-flags-like syntax, see this article."\n    },\n    "defineProperties": {\n      "!type": "fn(obj: ?, props: ?)",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/defineProperty",\n      "!doc": "Defines a new property directly on an object, or modifies an existing property on an object, and returns the object. If you want to see how to use the Object.defineProperty method with a binary-flags-like syntax, see this article."\n    },\n    "getOwnPropertyDescriptor": {\n      "!type": "fn(obj: ?, prop: string) -> ?",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/getOwnPropertyDescriptor",\n      "!doc": "Returns a property descriptor for an own property (that is, one directly present on an object, not present by dint of being along an object\'s prototype chain) of a given object."\n    },\n    "keys": {\n      "!type": "fn(obj: ?) -> [string]",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/keys",\n      "!doc": "Returns an array of a given object\'s own enumerable properties, in the same order as that provided by a for-in loop (the difference being that a for-in loop enumerates properties in the prototype chain as well)."\n    },\n    "getOwnPropertyNames": {\n      "!type": "fn(obj: ?) -> [string]",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/getOwnPropertyNames",\n      "!doc": "Returns an array of all properties (enumerable or not) found directly upon a given object."\n    },\n    "seal": {\n      "!type": "fn(obj: ?)",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/seal",\n      "!doc": "Seals an object, preventing new properties from being added to it and marking all existing properties as non-configurable. Values of present properties can still be changed as long as they are writable."\n    },\n    "isSealed": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/isSealed",\n      "!doc": "Determine if an object is sealed."\n    },\n    "freeze": {\n      "!type": "fn(obj: ?)",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/freeze",\n      "!doc": "Freezes an object: that is, prevents new properties from being added to it; prevents existing properties from being removed; and prevents existing properties, or their enumerability, configurability, or writability, from being changed. In essence the object is made effectively immutable. The method returns the object being frozen."\n    },\n    "isFrozen": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/isFrozen",\n      "!doc": "Determine if an object is frozen."\n    },\n    "prototype": {\n      "!stdProto": "Object",\n      "toString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/toString",\n        "!doc": "Returns a string representing the object."\n      },\n      "toLocaleString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/toLocaleString",\n        "!doc": "Returns a string representing the object. This method is meant to be overriden by derived objects for locale-specific purposes."\n      },\n      "valueOf": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/valueOf",\n        "!doc": "Returns the primitive value of the specified object"\n      },\n      "hasOwnProperty": {\n        "!type": "fn(prop: string) -> bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/hasOwnProperty",\n        "!doc": "Returns a boolean indicating whether the object has the specified property."\n      },\n      "propertyIsEnumerable": {\n        "!type": "fn(prop: string) -> bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object/propertyIsEnumerable",\n        "!doc": "Returns a Boolean indicating whether the specified property is enumerable."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Object",\n    "!doc": "Creates an object wrapper."\n  },\n  "Function": {\n    "!type": "fn(body: string) -> fn()",\n    "prototype": {\n      "!stdProto": "Function",\n      "apply": {\n        "!type": "fn(this: ?, args: [?])",\n        "!effects": [\n          "call and return !this this=!0 !1.<i> !1.<i> !1.<i>"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Function/apply",\n        "!doc": "Calls a function with a given this value and arguments provided as an array (or an array like object)."\n      },\n      "call": {\n        "!type": "fn(this: ?, args?: ?) -> !this.!ret",\n        "!effects": [\n          "call and return !this this=!0 !1 !2 !3 !4"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Function/call",\n        "!doc": "Calls a function with a given this value and arguments provided individually."\n      },\n      "bind": {\n        "!type": "fn(this: ?, args?: ?) -> !custom:Function_bind",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Function/bind",\n        "!doc": "Creates a new function that, when called, has its this keyword set to the provided value, with a given sequence of arguments preceding any provided when the new function was called."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Function",\n    "!doc": "Every function in JavaScript is actually a Function object."\n  },\n  "Array": {\n    "!type": "fn(size: number) -> !custom:Array_ctor",\n    "isArray": {\n      "!type": "fn(value: ?) -> bool",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/isArray",\n      "!doc": "Returns true if an object is an array, false if it is not."\n    },\n    "prototype": {\n      "!stdProto": "Array",\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/length",\n        "!doc": "An unsigned, 32-bit integer that specifies the number of elements in an array."\n      },\n      "concat": {\n        "!type": "fn(other: [?]) -> !this",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/concat",\n        "!doc": "Returns a new array comprised of this array joined with other array(s) and/or value(s)."\n      },\n      "join": {\n        "!type": "fn(separator?: string) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/join",\n        "!doc": "Joins all elements of an array into a string."\n      },\n      "splice": {\n        "!type": "fn(pos: number, amount: number)",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/splice",\n        "!doc": "Changes the content of an array, adding new elements while removing old elements."\n      },\n      "pop": {\n        "!type": "fn() -> !this.<i>",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/pop",\n        "!doc": "Removes the last element from an array and returns that element."\n      },\n      "push": {\n        "!type": "fn(newelt: ?) -> number",\n        "!effects": [\n          "propagate !0 !this.<i>"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/push",\n        "!doc": "Mutates an array by appending the given elements and returning the new length of the array."\n      },\n      "shift": {\n        "!type": "fn() -> !this.<i>",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/shift",\n        "!doc": "Removes the first element from an array and returns that element. This method changes the length of the array."\n      },\n      "unshift": {\n        "!type": "fn(newelt: ?) -> number",\n        "!effects": [\n          "propagate !0 !this.<i>"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/unshift",\n        "!doc": "Adds one or more elements to the beginning of an array and returns the new length of the array."\n      },\n      "slice": {\n        "!type": "fn(from: number, to?: number) -> !this",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/slice",\n        "!doc": "Returns a shallow copy of a portion of an array."\n      },\n      "reverse": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/reverse",\n        "!doc": "Reverses an array in place.  The first array element becomes the last and the last becomes the first."\n      },\n      "sort": {\n        "!type": "fn(compare?: fn(a: ?, b: ?) -> number)",\n        "!effects": [\n          "call !0 !this.<i> !this.<i>"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/sort",\n        "!doc": "Sorts the elements of an array in place and returns the array."\n      },\n      "indexOf": {\n        "!type": "fn(elt: ?, from?: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/indexOf",\n        "!doc": "Returns the first index at which a given element can be found in the array, or -1 if it is not present."\n      },\n      "lastIndexOf": {\n        "!type": "fn(elt: ?, from?: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/lastIndexOf",\n        "!doc": "Returns the last index at which a given element can be found in the array, or -1 if it is not present. The array is searched backwards, starting at fromIndex."\n      },\n      "every": {\n        "!type": "fn(test: fn(elt: ?, i: number) -> bool, context?: ?) -> bool",\n        "!effects": [\n          "call !0 this=!1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/every",\n        "!doc": "Tests whether all elements in the array pass the test implemented by the provided function."\n      },\n      "some": {\n        "!type": "fn(test: fn(elt: ?, i: number) -> bool, context?: ?) -> bool",\n        "!effects": [\n          "call !0 this=!1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/some",\n        "!doc": "Tests whether some element in the array passes the test implemented by the provided function."\n      },\n      "filter": {\n        "!type": "fn(test: fn(elt: ?, i: number) -> bool, context?: ?) -> !this",\n        "!effects": [\n          "call !0 this=!1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/filter",\n        "!doc": "Creates a new array with all elements that pass the test implemented by the provided function."\n      },\n      "forEach": {\n        "!type": "fn(f: fn(elt: ?, i: number), context?: ?)",\n        "!effects": [\n          "call !0 this=!1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/forEach",\n        "!doc": "Executes a provided function once per array element."\n      },\n      "map": {\n        "!type": "fn(f: fn(elt: ?, i: number) -> ?, context?: ?) -> [!0.!ret]",\n        "!effects": [\n          "call !0 this=!1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/map",\n        "!doc": "Creates a new array with the results of calling a provided function on every element in this array."\n      },\n      "reduce": {\n        "!type": "fn(combine: fn(sum: ?, elt: ?, i: number) -> ?, init?: ?) -> !0.!ret",\n        "!effects": [\n          "call !0 !1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/Reduce",\n        "!doc": "Apply a function against an accumulator and each value of the array (from left-to-right) as to reduce it to a single value."\n      },\n      "reduceRight": {\n        "!type": "fn(combine: fn(sum: ?, elt: ?, i: number) -> ?, init?: ?) -> !0.!ret",\n        "!effects": [\n          "call !0 !1 !this.<i> number"\n        ],\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/ReduceRight",\n        "!doc": "Apply a function simultaneously against two values of the array (from right-to-left) as to reduce it to a single value."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array",\n    "!doc": "The JavaScript Array global object is a constructor for arrays, which are high-level, list-like objects."\n  },\n  "String": {\n    "!type": "fn(value: ?) -> string",\n    "fromCharCode": {\n      "!type": "fn(code: number) -> string",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/fromCharCode",\n      "!doc": "Returns a string created by using the specified sequence of Unicode values."\n    },\n    "prototype": {\n      "!stdProto": "String",\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/JavaScript/Reference/Global_Objects/String",\n        "!doc": "The String global object is a constructor for strings, or a sequence of characters."\n      },\n      "<i>": "string",\n      "charAt": {\n        "!type": "fn(i: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/charAt",\n        "!doc": "Returns the specified character from a string."\n      },\n      "charCodeAt": {\n        "!type": "fn(i: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/charCodeAt",\n        "!doc": "Returns the numeric Unicode value of the character at the given index (except for unicode codepoints > 0x10000)."\n      },\n      "indexOf": {\n        "!type": "fn(char: string, from?: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/indexOf",\n        "!doc": "Returns the index within the calling String object of the first occurrence of the specified value, starting the search at fromIndex,\\nreturns -1 if the value is not found."\n      },\n      "lastIndexOf": {\n        "!type": "fn(char: string, from?: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/lastIndexOf",\n        "!doc": "Returns the index within the calling String object of the last occurrence of the specified value, or -1 if not found. The calling string is searched backward, starting at fromIndex."\n      },\n      "substring": {\n        "!type": "fn(from: number, to?: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/substring",\n        "!doc": "Returns a subset of a string between one index and another, or through the end of the string."\n      },\n      "substr": {\n        "!type": "fn(from: number, length?: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/substr",\n        "!doc": "Returns the characters in a string beginning at the specified location through the specified number of characters."\n      },\n      "slice": {\n        "!type": "fn(from: number, to?: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/slice",\n        "!doc": "Extracts a section of a string and returns a new string."\n      },\n      "trim": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/Trim",\n        "!doc": "Removes whitespace from both ends of the string."\n      },\n      "trimLeft": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/TrimLeft",\n        "!doc": "Removes whitespace from the left end of the string."\n      },\n      "trimRight": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/TrimRight",\n        "!doc": "Removes whitespace from the right end of the string."\n      },\n      "toUpperCase": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/toUpperCase",\n        "!doc": "Returns the calling string value converted to uppercase."\n      },\n      "toLowerCase": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/toLowerCase",\n        "!doc": "Returns the calling string value converted to lowercase."\n      },\n      "toLocaleUpperCase": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/toLocaleUpperCase",\n        "!doc": "Returns the calling string value converted to upper case, according to any locale-specific case mappings."\n      },\n      "toLocaleLowerCase": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/toLocaleLowerCase",\n        "!doc": "Returns the calling string value converted to lower case, according to any locale-specific case mappings."\n      },\n      "split": {\n        "!type": "fn(pattern: string) -> [string]",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/split",\n        "!doc": "Splits a String object into an array of strings by separating the string into substrings."\n      },\n      "concat": {\n        "!type": "fn(other: string) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/concat",\n        "!doc": "Combines the text of two or more strings and returns a new string."\n      },\n      "localeCompare": {\n        "!type": "fn(other: string) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/localeCompare",\n        "!doc": "Returns a number indicating whether a reference string comes before or after or is the same as the given string in sort order."\n      },\n      "match": {\n        "!type": "fn(pattern: +RegExp) -> [string]",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/match",\n        "!doc": "Used to retrieve the matches when matching a string against a regular expression."\n      },\n      "replace": {\n        "!type": "fn(pattern: +RegExp, replacement: string) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/replace",\n        "!doc": "Returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match."\n      },\n      "search": {\n        "!type": "fn(pattern: +RegExp) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String/search",\n        "!doc": "Executes the search for a match between a regular expression and this String object."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/String",\n    "!doc": "The String global object is a constructor for strings, or a sequence of characters."\n  },\n  "Number": {\n    "!type": "fn(value: ?) -> number",\n    "MAX_VALUE": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/MAX_VALUE",\n      "!doc": "The maximum numeric value representable in JavaScript."\n    },\n    "MIN_VALUE": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/MIN_VALUE",\n      "!doc": "The smallest positive numeric value representable in JavaScript."\n    },\n    "POSITIVE_INFINITY": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/POSITIVE_INFINITY",\n      "!doc": "A value representing the positive Infinity value."\n    },\n    "NEGATIVE_INFINITY": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/NEGATIVE_INFINITY",\n      "!doc": "A value representing the negative Infinity value."\n    },\n    "prototype": {\n      "!stdProto": "Number",\n      "toString": {\n        "!type": "fn(radix?: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/toString",\n        "!doc": "Returns a string representing the specified Number object"\n      },\n      "toFixed": {\n        "!type": "fn(digits: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/toFixed",\n        "!doc": "Formats a number using fixed-point notation"\n      },\n      "toExponential": {\n        "!type": "fn(digits: number) -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number/toExponential",\n        "!doc": "Returns a string representing the Number object in exponential notation"\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Number",\n    "!doc": "The Number JavaScript object is a wrapper object allowing you to work with numerical values. A Number object is created using the Number() constructor."\n  },\n  "Boolean": {\n    "!type": "fn(value: ?) -> bool",\n    "prototype": {\n      "!stdProto": "Boolean"\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Boolean",\n    "!doc": "The Boolean object is an object wrapper for a boolean value."\n  },\n  "RegExp": {\n    "!type": "fn(source: string, flags?: string)",\n    "prototype": {\n      "!stdProto": "RegExp",\n      "exec": {\n        "!type": "fn(input: string) -> [string]",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp/exec",\n        "!doc": "Executes a search for a match in a specified string. Returns a result array, or null."\n      },\n      "compile": {\n        "!type": "fn(source: string, flags?: string)",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp",\n        "!doc": "Creates a regular expression object for matching text with a pattern."\n      },\n      "test": {\n        "!type": "fn(input: string) -> bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp/test",\n        "!doc": "Executes the search for a match between a regular expression and a specified string. Returns true or false."\n      },\n      "global": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp",\n        "!doc": "Creates a regular expression object for matching text with a pattern."\n      },\n      "ignoreCase": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp",\n        "!doc": "Creates a regular expression object for matching text with a pattern."\n      },\n      "multiline": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp/multiline",\n        "!doc": "Reflects whether or not to search in strings across multiple lines.\\n"\n      },\n      "source": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp/source",\n        "!doc": "A read-only property that contains the text of the pattern, excluding the forward slashes.\\n"\n      },\n      "lastIndex": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp/lastIndex",\n        "!doc": "A read/write integer property that specifies the index at which to start the next match."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RegExp",\n    "!doc": "Creates a regular expression object for matching text with a pattern."\n  },\n  "Date": {\n    "!type": "fn(ms: number)",\n    "parse": {\n      "!type": "fn(source: string) -> +Date",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/parse",\n      "!doc": "Parses a string representation of a date, and returns the number of milliseconds since January 1, 1970, 00:00:00 UTC."\n    },\n    "UTC": {\n      "!type": "fn(year: number, month: number, date: number, hour?: number, min?: number, sec?: number, ms?: number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/UTC",\n      "!doc": "Accepts the same parameters as the longest form of the constructor, and returns the number of milliseconds in a Date object since January 1, 1970, 00:00:00, universal time."\n    },\n    "now": {\n      "!type": "fn() -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/now",\n      "!doc": "Returns the number of milliseconds elapsed since 1 January 1970 00:00:00 UTC."\n    },\n    "prototype": {\n      "toUTCString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toUTCString",\n        "!doc": "Converts a date to a string, using the universal time convention."\n      },\n      "toISOString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toISOString",\n        "!doc": "JavaScript provides a direct way to convert a date object into a string in ISO format, the ISO 8601 Extended Format."\n      },\n      "toDateString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toDateString",\n        "!doc": "Returns the date portion of a Date object in human readable form in American English."\n      },\n      "toTimeString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toTimeString",\n        "!doc": "Returns the time portion of a Date object in human readable form in American English."\n      },\n      "toLocaleDateString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toLocaleDateString",\n        "!doc": "Converts a date to a string, returning the \\"date\\" portion using the operating system\'s locale\'s conventions.\\n"\n      },\n      "toLocaleTimeString": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/toLocaleTimeString",\n        "!doc": "Converts a date to a string, returning the \\"time\\" portion using the current locale\'s conventions."\n      },\n      "getTime": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getTime",\n        "!doc": "Returns the numeric value corresponding to the time for the specified date according to universal time."\n      },\n      "getFullYear": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getFullYear",\n        "!doc": "Returns the year of the specified date according to local time."\n      },\n      "getYear": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getYear",\n        "!doc": "Returns the year in the specified date according to local time."\n      },\n      "getMonth": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getMonth",\n        "!doc": "Returns the month in the specified date according to local time."\n      },\n      "getUTCMonth": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCMonth",\n        "!doc": "Returns the month of the specified date according to universal time.\\n"\n      },\n      "getDate": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getDate",\n        "!doc": "Returns the day of the month for the specified date according to local time."\n      },\n      "getUTCDate": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCDate",\n        "!doc": "Returns the day (date) of the month in the specified date according to universal time.\\n"\n      },\n      "getDay": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getDay",\n        "!doc": "Returns the day of the week for the specified date according to local time."\n      },\n      "getUTCDay": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCDay",\n        "!doc": "Returns the day of the week in the specified date according to universal time.\\n"\n      },\n      "getHours": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getHours",\n        "!doc": "Returns the hour for the specified date according to local time."\n      },\n      "getUTCHours": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCHours",\n        "!doc": "Returns the hours in the specified date according to universal time.\\n"\n      },\n      "getMinutes": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getMinutes",\n        "!doc": "Returns the minutes in the specified date according to local time."\n      },\n      "getUTCMinutes": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date",\n        "!doc": "Creates JavaScript Date instances which let you work with dates and times."\n      },\n      "getSeconds": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getSeconds",\n        "!doc": "Returns the seconds in the specified date according to local time."\n      },\n      "getUTCSeconds": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCSeconds",\n        "!doc": "Returns the seconds in the specified date according to universal time.\\n"\n      },\n      "getMilliseconds": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getMilliseconds",\n        "!doc": "Returns the milliseconds in the specified date according to local time."\n      },\n      "getUTCMilliseconds": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getUTCMilliseconds",\n        "!doc": "Returns the milliseconds in the specified date according to universal time.\\n"\n      },\n      "getTimezoneOffset": {\n        "!type": "fn() -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/getTimezoneOffset",\n        "!doc": "Returns the time-zone offset from UTC, in minutes, for the current locale."\n      },\n      "setTime": {\n        "!type": "fn(date: +Date) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setTime",\n        "!doc": "Sets the Date object to the time represented by a number of milliseconds since January 1, 1970, 00:00:00 UTC.\\n"\n      },\n      "setFullYear": {\n        "!type": "fn(year: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setFullYear",\n        "!doc": "Sets the full year for a specified date according to local time.\\n"\n      },\n      "setUTCFullYear": {\n        "!type": "fn(year: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCFullYear",\n        "!doc": "Sets the full year for a specified date according to universal time.\\n"\n      },\n      "setMonth": {\n        "!type": "fn(month: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setMonth",\n        "!doc": "Set the month for a specified date according to local time."\n      },\n      "setUTCMonth": {\n        "!type": "fn(month: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCMonth",\n        "!doc": "Sets the month for a specified date according to universal time.\\n"\n      },\n      "setDate": {\n        "!type": "fn(day: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setDate",\n        "!doc": "Sets the day of the month for a specified date according to local time."\n      },\n      "setUTCDate": {\n        "!type": "fn(day: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCDate",\n        "!doc": "Sets the day of the month for a specified date according to universal time.\\n"\n      },\n      "setHours": {\n        "!type": "fn(hour: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setHours",\n        "!doc": "Sets the hours for a specified date according to local time, and returns the number of milliseconds since 1 January 1970 00:00:00 UTC until the time represented by the updated Date instance."\n      },\n      "setUTCHours": {\n        "!type": "fn(hour: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCHours",\n        "!doc": "Sets the hour for a specified date according to universal time.\\n"\n      },\n      "setMinutes": {\n        "!type": "fn(min: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setMinutes",\n        "!doc": "Sets the minutes for a specified date according to local time."\n      },\n      "setUTCMinutes": {\n        "!type": "fn(min: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCMinutes",\n        "!doc": "Sets the minutes for a specified date according to universal time.\\n"\n      },\n      "setSeconds": {\n        "!type": "fn(sec: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setSeconds",\n        "!doc": "Sets the seconds for a specified date according to local time."\n      },\n      "setUTCSeconds": {\n        "!type": "fn(sec: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCSeconds",\n        "!doc": "Sets the seconds for a specified date according to universal time.\\n"\n      },\n      "setMilliseconds": {\n        "!type": "fn(ms: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setMilliseconds",\n        "!doc": "Sets the milliseconds for a specified date according to local time.\\n"\n      },\n      "setUTCMilliseconds": {\n        "!type": "fn(ms: number) -> number",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date/setUTCMilliseconds",\n        "!doc": "Sets the milliseconds for a specified date according to universal time.\\n"\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Date",\n    "!doc": "Creates JavaScript Date instances which let you work with dates and times."\n  },\n  "Error": {\n    "!type": "fn(message: string)",\n    "prototype": {\n      "name": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Error/name",\n        "!doc": "A name for the type of error."\n      },\n      "message": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Error/message",\n        "!doc": "A human-readable description of the error."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Error",\n    "!doc": "Creates an error object."\n  },\n  "SyntaxError": {\n    "!type": "fn(message: string)",\n    "prototype": "Error.prototype",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/SyntaxError",\n    "!doc": "Represents an error when trying to interpret syntactically invalid code."\n  },\n  "ReferenceError": {\n    "!type": "fn(message: string)",\n    "prototype": "Error.prototype",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/ReferenceError",\n    "!doc": "Represents an error when a non-existent variable is referenced."\n  },\n  "URIError": {\n    "!type": "fn(message: string)",\n    "prototype": "Error.prototype",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/URIError",\n    "!doc": "Represents an error when a malformed URI is encountered."\n  },\n  "EvalError": {\n    "!type": "fn(message: string)",\n    "prototype": "Error.prototype",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/EvalError",\n    "!doc": "Represents an error regarding the eval function."\n  },\n  "RangeError": {\n    "!type": "fn(message: string)",\n    "prototype": "Error.prototype",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/RangeError",\n    "!doc": "Represents an error when a number is not within the correct range allowed."\n  },\n  "parseInt": {\n    "!type": "fn(string: string, radix?: number) -> number",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/parseInt",\n    "!doc": "Parses a string argument and returns an integer of the specified radix or base."\n  },\n  "parseFloat": {\n    "!type": "fn(string: string) -> number",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/parseFloat",\n    "!doc": "Parses a string argument and returns a floating point number."\n  },\n  "isNaN": {\n    "!type": "fn(value: number) -> bool",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/isNaN",\n    "!doc": "Determines whether a value is NaN or not. Be careful, this function is broken. You may be interested in ECMAScript 6 Number.isNaN."\n  },\n  "eval": {\n    "!type": "fn(code: string) -> ?",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/eval",\n    "!doc": "Evaluates JavaScript code represented as a string."\n  },\n  "encodeURI": {\n    "!type": "fn(uri: string) -> string",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/encodeURI",\n    "!doc": "Encodes a Uniform Resource Identifier (URI) by replacing each instance of certain characters by one, two, three, or four escape sequences representing the UTF-8 encoding of the character (will only be four escape sequences for characters composed of two \\"surrogate\\" characters)."\n  },\n  "encodeURIComponent": {\n    "!type": "fn(uri: string) -> string",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/encodeURIComponent",\n    "!doc": "Encodes a Uniform Resource Identifier (URI) component by replacing each instance of certain characters by one, two, three, or four escape sequences representing the UTF-8 encoding of the character (will only be four escape sequences for characters composed of two \\"surrogate\\" characters)."\n  },\n  "decodeURI": {\n    "!type": "fn(uri: string) -> string",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/decodeURI",\n    "!doc": "Decodes a Uniform Resource Identifier (URI) previously created by encodeURI or by a similar routine."\n  },\n  "decodeURIComponent": {\n    "!type": "fn(uri: string) -> string",\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/decodeURIComponent",\n    "!doc": "Decodes a Uniform Resource Identifier (URI) component previously created by encodeURIComponent or by a similar routine."\n  },\n  "Math": {\n    "E": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/E",\n      "!doc": "The base of natural logarithms, e, approximately 2.718."\n    },\n    "LN2": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/LN2",\n      "!doc": "The natural logarithm of 2, approximately 0.693."\n    },\n    "LN10": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/LN10",\n      "!doc": "The natural logarithm of 10, approximately 2.302."\n    },\n    "LOG2E": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/LOG2E",\n      "!doc": "The base 2 logarithm of E (approximately 1.442)."\n    },\n    "LOG10E": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/LOG10E",\n      "!doc": "The base 10 logarithm of E (approximately 0.434)."\n    },\n    "SQRT1_2": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/SQRT1_2",\n      "!doc": "The square root of 1/2; equivalently, 1 over the square root of 2, approximately 0.707."\n    },\n    "SQRT2": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/SQRT2",\n      "!doc": "The square root of 2, approximately 1.414."\n    },\n    "PI": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/PI",\n      "!doc": "The ratio of the circumference of a circle to its diameter, approximately 3.14159."\n    },\n    "abs": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/abs",\n      "!doc": "Returns the absolute value of a number."\n    },\n    "cos": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/cos",\n      "!doc": "Returns the cosine of a number."\n    },\n    "sin": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/sin",\n      "!doc": "Returns the sine of a number."\n    },\n    "tan": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/tan",\n      "!doc": "Returns the tangent of a number."\n    },\n    "acos": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/acos",\n      "!doc": "Returns the arccosine (in radians) of a number."\n    },\n    "asin": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/asin",\n      "!doc": "Returns the arcsine (in radians) of a number."\n    },\n    "atan": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/atan",\n      "!doc": "Returns the arctangent (in radians) of a number."\n    },\n    "atan2": {\n      "!type": "fn(number, number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/atan2",\n      "!doc": "Returns the arctangent of the quotient of its arguments."\n    },\n    "ceil": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/ceil",\n      "!doc": "Returns the smallest integer greater than or equal to a number."\n    },\n    "floor": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/floor",\n      "!doc": "Returns the largest integer less than or equal to a number."\n    },\n    "round": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/round",\n      "!doc": "Returns the value of a number rounded to the nearest integer."\n    },\n    "exp": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/exp",\n      "!doc": "Returns Ex, where x is the argument, and E is Euler\'s constant, the base of the natural logarithms."\n    },\n    "log": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/log",\n      "!doc": "Returns the natural logarithm (base E) of a number."\n    },\n    "sqrt": {\n      "!type": "fn(number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/sqrt",\n      "!doc": "Returns the square root of a number."\n    },\n    "pow": {\n      "!type": "fn(number, number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/pow",\n      "!doc": "Returns base to the exponent power, that is, baseexponent."\n    },\n    "max": {\n      "!type": "fn(number, number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/max",\n      "!doc": "Returns the largest of zero or more numbers."\n    },\n    "min": {\n      "!type": "fn(number, number) -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/min",\n      "!doc": "Returns the smallest of zero or more numbers."\n    },\n    "random": {\n      "!type": "fn() -> number",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math/random",\n      "!doc": "Returns a floating-point, pseudo-random number in the range [0, 1) that is, from 0 (inclusive) up to but not including 1 (exclusive), which you can then scale to your desired range."\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Math",\n    "!doc": "A built-in object that has properties and methods for mathematical constants and functions."\n  },\n  "JSON": {\n    "parse": {\n      "!type": "fn(json: string) -> ?",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/JSON/parse",\n      "!doc": "Parse a string as JSON, optionally transforming the value produced by parsing."\n    },\n    "stringify": {\n      "!type": "fn(value: ?) -> string",\n      "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/JSON/stringify",\n      "!doc": "Convert a value to JSON, optionally replacing values if a replacer function is specified, or optionally including only the specified properties if a replacer array is specified."\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/JSON",\n    "!doc": "JSON (JavaScript Object Notation) is a data-interchange format.  It closely resembles a subset of JavaScript syntax, although it is not a strict subset. (See JSON in the JavaScript Reference for full details.)  It is useful when writing any kind of JavaScript-based application, including websites and browser extensions.  For example, you might store user information in JSON format in a cookie, or you might store extension preferences in JSON in a string-valued browser preference."\n  }\n}\n';});

define('text!thirdparty/tern/defs/browser.json',[],function () { return '{\n  "!name": "browser",\n  "!define": {\n    "canvas.context2d": {\n      "canvas": "+Element",\n      "width": "number",\n      "height": "number",\n      "commit": "fn()",\n      "save": "fn()",\n      "restore": "fn()",\n      "currentTransform": "?",\n      "scale": "fn(x: number, y: number)",\n      "rotate": "fn(angle: number)",\n      "translate": "fn(x: number, y: number)",\n      "transform": "fn(a: number, b: number, c: number, d: number, e: number, f: number)",\n      "setTransform": "fn(a: number, b: number, c: number, d: number, e: number, f: number)",\n      "resetTransform": "fn()",\n      "globalAlpha": "number",\n      "globalCompositeOperation": "string",\n      "imageSmoothingEnabled": "bool",\n      "strokeStyle": "string",\n      "fillStyle": "string",\n      "createLinearGradient": "fn(x0: number, y0: number, x1: number, y1: number) -> ?",\n      "createPattern": "fn(image: ?, repetition: string) -> ?",\n      "shadowOffsetX": "number",\n      "shadowOffsetY": "number",\n      "shadowBlur": "number",\n      "shadowColor": "string",\n      "clearRect": "fn(x: number, y: number, w: number, h: number)",\n      "fillRect": "fn(x: number, y: number, w: number, h: number)",\n      "strokeRect": "fn(x: number, y: number, w: number, h: number)",\n      "fillRule": "string",\n      "fill": "fn()",\n      "beginPath": "fn()",\n      "stroke": "fn()",\n      "clip": "fn()",\n      "resetClip": "fn()",\n      "measureText": "fn(text: string) -> ?",\n      "drawImage": "fn(image: ?, dx: number, dy: number)",\n      "createImageData": "fn(sw: number, sh: number) -> ?",\n      "getImageData": "fn(sx: number, sy: number, sw: number, sh: number) -> ?",\n      "putImageData": "fn(imagedata: ?, dx: number, dy: number)",\n      "lineWidth": "number",\n      "lineCap": "string",\n      "lineJoin": "string",\n      "miterLimit": "number",\n      "setLineDash": "fn(segments: [number])",\n      "getLineDash": "fn() -> [number]",\n      "lineDashOffset": "number",\n      "font": "string",\n      "textAlign": "string",\n      "textBaseline": "string",\n      "direction": "string",\n      "closePath": "fn()",\n      "moveTo": "fn(x: number, y: number)",\n      "lineTo": "fn(x: number, y: number)",\n      "quadraticCurveTo": "fn(cpx: number, cpy: number, x: number, y: number)",\n      "bezierCurveTo": "fn(cp1x: number, cp1y: number, cp2x: number, cp2y: number, x: number, y: number)",\n      "arcTo": "fn(x1: number, y1: number, x2: number, y2: number, radius: number)",\n      "rect": "fn(x: number, y: number, w: number, h: number)",\n      "arc": "fn(x: number, y: number, radius: number, startAngle: number, endAngle: number, anticlockwise?: bool)",\n      "ellipse": "fn(x: number, y: number, radiusX: number, radiusY: number, rotation: number, startAngle: number, endAngle: number, anticlockwise: bool)"\n    }\n  },\n  "location": {\n    "assign": {\n      "!type": "fn(url: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "Load the document at the provided URL."\n    },\n    "replace": {\n      "!type": "fn(url: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "Replace the current document with the one at the provided URL. The difference from the assign() method is that after using replace() the current page will not be saved in session history, meaning the user won\'t be able to use the Back button to navigate to it."\n    },\n    "reload": {\n      "!type": "fn()",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "Reload the document from the current URL. forceget is a boolean, which, when it is true, causes the page to always be reloaded from the server. If it is false or not specified, the browser may reload the page from its cache."\n    },\n    "origin": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The origin of the URL."\n    },\n    "hash": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The part of the URL that follows the # symbol, including the # symbol."\n    },\n    "search": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The part of the URL that follows the ? symbol, including the ? symbol."\n    },\n    "pathname": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The path (relative to the host)."\n    },\n    "port": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The port number of the URL."\n    },\n    "hostname": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The host name (without the port number or square brackets)."\n    },\n    "host": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The host name and port number."\n    },\n    "protocol": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The protocol of the URL."\n    },\n    "href": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n      "!doc": "The entire URL."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.location",\n    "!doc": "Returns a location object with information about the current location of the document. Assigning to the location property changes the current page to the new address."\n  },\n  "Node": {\n    "!type": "fn()",\n    "prototype": {\n      "parentElement": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.parentElement",\n        "!doc": "Returns the DOM node\'s parent Element, or null if the node either has no parent, or its parent isn\'t a DOM Element."\n      },\n      "textContent": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.textContent",\n        "!doc": "Gets or sets the text content of a node and its descendants."\n      },\n      "baseURI": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.baseURI",\n        "!doc": "The absolute base URI of a node or null if unable to obtain an absolute URI."\n      },\n      "localName": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.localName",\n        "!doc": "Returns the local part of the qualified name of this node."\n      },\n      "prefix": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.prefix",\n        "!doc": "Returns the namespace prefix of the specified node, or null if no prefix is specified. This property is read only."\n      },\n      "namespaceURI": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.namespaceURI",\n        "!doc": "The namespace URI of the node, or null if the node is not in a namespace (read-only). When the node is a document, it returns the XML namespace for the current document."\n      },\n      "ownerDocument": {\n        "!type": "+Document",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.ownerDocument",\n        "!doc": "The ownerDocument property returns the top-level document object for this node."\n      },\n      "attributes": {\n        "!type": "+NamedNodeMap",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.attributes",\n        "!doc": "A collection of all attribute nodes registered to the specified node. It is a NamedNodeMap,not an Array, so it has no Array methods and the Attr nodes\' indexes may differ among browsers."\n      },\n      "nextSibling": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.nextSibling",\n        "!doc": "Returns the node immediately following the specified one in its parent\'s childNodes list, or null if the specified node is the last node in that list."\n      },\n      "previousSibling": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.previousSibling",\n        "!doc": "Returns the node immediately preceding the specified one in its parent\'s childNodes list, null if the specified node is the first in that list."\n      },\n      "lastChild": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.lastChild",\n        "!doc": "Returns the last child of a node."\n      },\n      "firstChild": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.firstChild",\n        "!doc": "Returns the node\'s first child in the tree, or null if the node is childless. If the node is a Document, it returns the first node in the list of its direct children."\n      },\n      "childNodes": {\n        "!type": "+NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.childNodes",\n        "!doc": "Returns a collection of child nodes of the given element."\n      },\n      "parentNode": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.parentNode",\n        "!doc": "Returns the parent of the specified node in the DOM tree."\n      },\n      "nodeType": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.nodeType",\n        "!doc": "Returns an integer code representing the type of the node."\n      },\n      "nodeValue": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.nodeValue",\n        "!doc": "Returns or sets the value of the current node."\n      },\n      "nodeName": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.nodeName",\n        "!doc": "Returns the name of the current node as a string."\n      },\n      "tagName": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.nodeName",\n        "!doc": "Returns the name of the current node as a string."\n      },\n      "insertBefore": {\n        "!type": "fn(newElt: +Element, before: +Element) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.insertBefore",\n        "!doc": "Inserts the specified node before a reference element as a child of the current node."\n      },\n      "replaceChild": {\n        "!type": "fn(newElt: +Element, oldElt: +Element) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.replaceChild",\n        "!doc": "Replaces one child node of the specified element with another."\n      },\n      "removeChild": {\n        "!type": "fn(oldElt: +Element) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.removeChild",\n        "!doc": "Removes a child node from the DOM. Returns removed node."\n      },\n      "appendChild": {\n        "!type": "fn(newElt: +Element) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.appendChild",\n        "!doc": "Adds a node to the end of the list of children of a specified parent node. If the node already exists it is removed from current parent node, then added to new parent node."\n      },\n      "hasChildNodes": {\n        "!type": "fn() -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.hasChildNodes",\n        "!doc": "Returns a Boolean value indicating whether the current Node has child nodes or not."\n      },\n      "cloneNode": {\n        "!type": "fn(deep: bool) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.cloneNode",\n        "!doc": "Returns a duplicate of the node on which this method was called."\n      },\n      "normalize": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.normalize",\n        "!doc": "Puts the specified node and all of its subtree into a \\"normalized\\" form. In a normalized subtree, no text nodes in the subtree are empty and there are no adjacent text nodes."\n      },\n      "isSupported": {\n        "!type": "fn(features: string, version: number) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.isSupported",\n        "!doc": "Tests whether the DOM implementation implements a specific feature and that feature is supported by this node."\n      },\n      "hasAttributes": {\n        "!type": "fn() -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.hasAttributes",\n        "!doc": "Returns a boolean value of true or false, indicating if the current element has any attributes or not."\n      },\n      "lookupPrefix": {\n        "!type": "fn(uri: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.lookupPrefix",\n        "!doc": "Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible, the result is implementation-dependent."\n      },\n      "isDefaultNamespace": {\n        "!type": "fn(uri: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.isDefaultNamespace",\n        "!doc": "Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not."\n      },\n      "lookupNamespaceURI": {\n        "!type": "fn(uri: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.lookupNamespaceURI",\n        "!doc": "Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not). Supplying null for the prefix will return the default namespace."\n      },\n      "addEventListener": {\n        "!type": "fn(type: string, listener: fn(e: +Event), capture: bool)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.addEventListener",\n        "!doc": "Registers a single event listener on a single target. The event target may be a single element in a document, the document itself, a window, or an XMLHttpRequest."\n      },\n      "removeEventListener": {\n        "!type": "fn(type: string, listener: fn(), capture: bool)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.removeEventListener",\n        "!doc": "Allows the removal of event listeners from the event target."\n      },\n      "isSameNode": {\n        "!type": "fn(other: +Node) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.isSameNode",\n        "!doc": "Tests whether two nodes are the same, that is they reference the same object."\n      },\n      "isEqualNode": {\n        "!type": "fn(other: +Node) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.isEqualNode",\n        "!doc": "Tests whether two nodes are equal."\n      },\n      "compareDocumentPosition": {\n        "!type": "fn(other: +Node) -> number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.compareDocumentPosition",\n        "!doc": "Compares the position of the current node against another node in any other document."\n      },\n      "contains": {\n        "!type": "fn(other: +Node) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Node.contains",\n        "!doc": "Indicates whether a node is a descendent of a given node."\n      },\n      "dispatchEvent": {\n        "!type": "fn(event: +Event) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.dispatchEvent",\n        "!doc": "Dispatches an event into the event system. The event is subject to the same capturing and bubbling behavior as directly dispatched events."\n      },\n      "ELEMENT_NODE": "number",\n      "ATTRIBUTE_NODE": "number",\n      "TEXT_NODE": "number",\n      "CDATA_SECTION_NODE": "number",\n      "ENTITY_REFERENCE_NODE": "number",\n      "ENTITY_NODE": "number",\n      "PROCESSING_INSTRUCTION_NODE": "number",\n      "COMMENT_NODE": "number",\n      "DOCUMENT_NODE": "number",\n      "DOCUMENT_TYPE_NODE": "number",\n      "DOCUMENT_FRAGMENT_NODE": "number",\n      "NOTATION_NODE": "number",\n      "DOCUMENT_POSITION_DISCONNECTED": "number",\n      "DOCUMENT_POSITION_PRECEDING": "number",\n      "DOCUMENT_POSITION_FOLLOWING": "number",\n      "DOCUMENT_POSITION_CONTAINS": "number",\n      "DOCUMENT_POSITION_CONTAINED_BY": "number",\n      "DOCUMENT_POSITION_IMPLEMENTATION_SPECIFIC": "number"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Node",\n    "!doc": "A Node is an interface from which a number of DOM types inherit, and allows these various types to be treated (or tested) similarly."\n  },\n  "Element": {\n    "!type": "fn()",\n    "prototype": {\n      "!proto": "Node.prototype",\n      "getAttribute": {\n        "!type": "fn(name: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getAttribute",\n        "!doc": "Returns the value of the named attribute on the specified element. If the named attribute does not exist, the value returned will either be null or \\"\\" (the empty string)."\n      },\n      "setAttribute": {\n        "!type": "fn(name: string, value: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.setAttribute",\n        "!doc": "Adds a new attribute or changes the value of an existing attribute on the specified element."\n      },\n      "removeAttribute": {\n        "!type": "fn(name: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.removeAttribute",\n        "!doc": "Removes an attribute from the specified element."\n      },\n      "getAttributeNode": {\n        "!type": "fn(name: string) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getAttributeNode",\n        "!doc": "Returns the specified attribute of the specified element, as an Attr node."\n      },\n      "getElementsByTagName": {\n        "!type": "fn(tagName: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getElementsByTagName",\n        "!doc": "Returns a list of elements with the given tag name. The subtree underneath the specified element is searched, excluding the element itself. The returned list is live, meaning that it updates itself with the DOM tree automatically. Consequently, there is no need to call several times element.getElementsByTagName with the same element and arguments."\n      },\n      "getElementsByTagNameNS": {\n        "!type": "fn(ns: string, tagName: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getElementsByTagNameNS",\n        "!doc": "Returns a list of elements with the given tag name belonging to the given namespace."\n      },\n      "getAttributeNS": {\n        "!type": "fn(ns: string, name: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getAttributeNS",\n        "!doc": "Returns the string value of the attribute with the specified namespace and name. If the named attribute does not exist, the value returned will either be null or \\"\\" (the empty string)."\n      },\n      "setAttributeNS": {\n        "!type": "fn(ns: string, name: string, value: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.setAttributeNS",\n        "!doc": "Adds a new attribute or changes the value of an attribute with the given namespace and name."\n      },\n      "removeAttributeNS": {\n        "!type": "fn(ns: string, name: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.removeAttributeNS",\n        "!doc": "removeAttributeNS removes the specified attribute from an element."\n      },\n      "getAttributeNodeNS": {\n        "!type": "fn(ns: string, name: string) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getAttributeNodeNS",\n        "!doc": "Returns the Attr node for the attribute with the given namespace and name."\n      },\n      "hasAttribute": {\n        "!type": "fn(name: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.hasAttribute",\n        "!doc": "hasAttribute returns a boolean value indicating whether the specified element has the specified attribute or not."\n      },\n      "hasAttributeNS": {\n        "!type": "fn(ns: string, name: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.hasAttributeNS",\n        "!doc": "hasAttributeNS returns a boolean value indicating whether the current element has the specified attribute."\n      },\n      "focus": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.focus",\n        "!doc": "Sets focus on the specified element, if it can be focused."\n      },\n      "blur": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.blur",\n        "!doc": "The blur method removes keyboard focus from the current element."\n      },\n      "scrollIntoView": {\n        "!type": "fn(top: bool)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.scrollIntoView",\n        "!doc": "The scrollIntoView() method scrolls the element into view."\n      },\n      "scrollByLines": {\n        "!type": "fn(lines: number)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollByLines",\n        "!doc": "Scrolls the document by the given number of lines."\n      },\n      "scrollByPages": {\n        "!type": "fn(pages: number)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollByPages",\n        "!doc": "Scrolls the current document by the specified number of pages."\n      },\n      "getElementsByClassName": {\n        "!type": "fn(name: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getElementsByClassName",\n        "!doc": "Returns a set of elements which have all the given class names. When called on the document object, the complete document is searched, including the root node. You may also call getElementsByClassName on any element; it will return only elements which are descendants of the specified root element with the given class names."\n      },\n      "querySelector": {\n        "!type": "fn(selectors: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.querySelector",\n        "!doc": "Returns the first element that is a descendent of the element on which it is invoked that matches the specified group of selectors."\n      },\n      "querySelectorAll": {\n        "!type": "fn(selectors: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.querySelectorAll",\n        "!doc": "Returns a non-live NodeList of all elements descended from the element on which it is invoked that match the specified group of CSS selectors."\n      },\n      "getClientRects": {\n        "!type": "fn() -> [+ClientRect]",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n        "!doc": "Returns a collection of rectangles that indicate the bounding rectangles for each box in a client."\n      },\n      "getBoundingClientRect": {\n        "!type": "fn() -> +ClientRect",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getBoundingClientRect",\n        "!doc": "Returns a text rectangle object that encloses a group of text rectangles."\n      },\n      "setAttributeNode": {\n        "!type": "fn(attr: +Attr) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.setAttributeNode",\n        "!doc": "Adds a new Attr node to the specified element."\n      },\n      "removeAttributeNode": {\n        "!type": "fn(attr: +Attr) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.removeAttributeNode",\n        "!doc": "Removes the specified attribute from the current element."\n      },\n      "setAttributeNodeNS": {\n        "!type": "fn(attr: +Attr) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.setAttributeNodeNS",\n        "!doc": "Adds a new namespaced attribute node to an element."\n      },\n      "insertAdjacentHTML": {\n        "!type": "fn(position: string, text: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.insertAdjacentHTML",\n        "!doc": "Parses the specified text as HTML or XML and inserts the resulting nodes into the DOM tree at a specified position. It does not reparse the element it is being used on and thus it does not corrupt the existing elements inside the element. This, and avoiding the extra step of serialization make it much faster than direct innerHTML manipulation."\n      },\n      "children": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.children",\n        "!doc": "Returns a collection of child elements of the given element."\n      },\n      "childElementCount": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.childElementCount",\n        "!doc": "Returns the number of child elements of the given element."\n      },\n      "className": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.className",\n        "!doc": "Gets and sets the value of the class attribute of the specified element."\n      },\n      "style": {\n        "cssText": "string",\n        "alignmentBaseline": "string",\n        "background": "string",\n        "backgroundAttachment": "string",\n        "backgroundClip": "string",\n        "backgroundColor": "string",\n        "backgroundImage": "string",\n        "backgroundOrigin": "string",\n        "backgroundPosition": "string",\n        "backgroundPositionX": "string",\n        "backgroundPositionY": "string",\n        "backgroundRepeat": "string",\n        "backgroundRepeatX": "string",\n        "backgroundRepeatY": "string",\n        "backgroundSize": "string",\n        "baselineShift": "string",\n        "border": "string",\n        "borderBottom": "string",\n        "borderBottomColor": "string",\n        "borderBottomLeftRadius": "string",\n        "borderBottomRightRadius": "string",\n        "borderBottomStyle": "string",\n        "borderBottomWidth": "string",\n        "borderCollapse": "string",\n        "borderColor": "string",\n        "borderImage": "string",\n        "borderImageOutset": "string",\n        "borderImageRepeat": "string",\n        "borderImageSlice": "string",\n        "borderImageSource": "string",\n        "borderImageWidth": "string",\n        "borderLeft": "string",\n        "borderLeftColor": "string",\n        "borderLeftStyle": "string",\n        "borderLeftWidth": "string",\n        "borderRadius": "string",\n        "borderRight": "string",\n        "borderRightColor": "string",\n        "borderRightStyle": "string",\n        "borderRightWidth": "string",\n        "borderSpacing": "string",\n        "borderStyle": "string",\n        "borderTop": "string",\n        "borderTopColor": "string",\n        "borderTopLeftRadius": "string",\n        "borderTopRightRadius": "string",\n        "borderTopStyle": "string",\n        "borderTopWidth": "string",\n        "borderWidth": "string",\n        "bottom": "string",\n        "boxShadow": "string",\n        "boxSizing": "string",\n        "captionSide": "string",\n        "clear": "string",\n        "clip": "string",\n        "clipPath": "string",\n        "clipRule": "string",\n        "color": "string",\n        "colorInterpolation": "string",\n        "colorInterpolationFilters": "string",\n        "colorProfile": "string",\n        "colorRendering": "string",\n        "content": "string",\n        "counterIncrement": "string",\n        "counterReset": "string",\n        "cursor": "string",\n        "direction": "string",\n        "display": "string",\n        "dominantBaseline": "string",\n        "emptyCells": "string",\n        "enableBackground": "string",\n        "fill": "string",\n        "fillOpacity": "string",\n        "fillRule": "string",\n        "filter": "string",\n        "float": "string",\n        "floodColor": "string",\n        "floodOpacity": "string",\n        "font": "string",\n        "fontFamily": "string",\n        "fontSize": "string",\n        "fontStretch": "string",\n        "fontStyle": "string",\n        "fontVariant": "string",\n        "fontWeight": "string",\n        "glyphOrientationHorizontal": "string",\n        "glyphOrientationVertical": "string",\n        "height": "string",\n        "imageRendering": "string",\n        "kerning": "string",\n        "left": "string",\n        "letterSpacing": "string",\n        "lightingColor": "string",\n        "lineHeight": "string",\n        "listStyle": "string",\n        "listStyleImage": "string",\n        "listStylePosition": "string",\n        "listStyleType": "string",\n        "margin": "string",\n        "marginBottom": "string",\n        "marginLeft": "string",\n        "marginRight": "string",\n        "marginTop": "string",\n        "marker": "string",\n        "markerEnd": "string",\n        "markerMid": "string",\n        "markerStart": "string",\n        "mask": "string",\n        "maxHeight": "string",\n        "maxWidth": "string",\n        "minHeight": "string",\n        "minWidth": "string",\n        "opacity": "string",\n        "orphans": "string",\n        "outline": "string",\n        "outlineColor": "string",\n        "outlineOffset": "string",\n        "outlineStyle": "string",\n        "outlineWidth": "string",\n        "overflow": "string",\n        "overflowWrap": "string",\n        "overflowX": "string",\n        "overflowY": "string",\n        "padding": "string",\n        "paddingBottom": "string",\n        "paddingLeft": "string",\n        "paddingRight": "string",\n        "paddingTop": "string",\n        "page": "string",\n        "pageBreakAfter": "string",\n        "pageBreakBefore": "string",\n        "pageBreakInside": "string",\n        "pointerEvents": "string",\n        "position": "string",\n        "quotes": "string",\n        "resize": "string",\n        "right": "string",\n        "shapeRendering": "string",\n        "size": "string",\n        "speak": "string",\n        "src": "string",\n        "stopColor": "string",\n        "stopOpacity": "string",\n        "stroke": "string",\n        "strokeDasharray": "string",\n        "strokeDashoffset": "string",\n        "strokeLinecap": "string",\n        "strokeLinejoin": "string",\n        "strokeMiterlimit": "string",\n        "strokeOpacity": "string",\n        "strokeWidth": "string",\n        "tabSize": "string",\n        "tableLayout": "string",\n        "textAlign": "string",\n        "textAnchor": "string",\n        "textDecoration": "string",\n        "textIndent": "string",\n        "textLineThrough": "string",\n        "textLineThroughColor": "string",\n        "textLineThroughMode": "string",\n        "textLineThroughStyle": "string",\n        "textLineThroughWidth": "string",\n        "textOverflow": "string",\n        "textOverline": "string",\n        "textOverlineColor": "string",\n        "textOverlineMode": "string",\n        "textOverlineStyle": "string",\n        "textOverlineWidth": "string",\n        "textRendering": "string",\n        "textShadow": "string",\n        "textTransform": "string",\n        "textUnderline": "string",\n        "textUnderlineColor": "string",\n        "textUnderlineMode": "string",\n        "textUnderlineStyle": "string",\n        "textUnderlineWidth": "string",\n        "top": "string",\n        "unicodeBidi": "string",\n        "unicodeRange": "string",\n        "vectorEffect": "string",\n        "verticalAlign": "string",\n        "visibility": "string",\n        "whiteSpace": "string",\n        "width": "string",\n        "wordBreak": "string",\n        "wordSpacing": "string",\n        "wordWrap": "string",\n        "writingMode": "string",\n        "zIndex": "string",\n        "zoom": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.style",\n        "!doc": "Returns an object that represents the element\'s style attribute."\n      },\n      "classList": {\n        "!type": "+DOMTokenList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.classList",\n        "!doc": "Returns a token list of the class attribute of the element."\n      },\n      "contentEditable": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.contentEditable",\n        "!doc": "Indicates whether or not the element is editable."\n      },\n      "firstElementChild": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.firstElementChild",\n        "!doc": "Returns the element\'s first child element or null if there are no child elements."\n      },\n      "lastElementChild": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.lastElementChild",\n        "!doc": "Returns the element\'s last child element or null if there are no child elements."\n      },\n      "nextElementSibling": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.nextElementSibling",\n        "!doc": "Returns the element immediately following the specified one in its parent\'s children list, or null if the specified element is the last one in the list."\n      },\n      "previousElementSibling": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Element.previousElementSibling",\n        "!doc": "Returns the element immediately prior to the specified one in its parent\'s children list, or null if the specified element is the first one in the list."\n      },\n      "tabIndex": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.tabIndex",\n        "!doc": "Gets/sets the tab order of the current element."\n      },\n      "title": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.title",\n        "!doc": "Establishes the text to be displayed in a \'tool tip\' popup when the mouse is over the displayed node."\n      },\n      "width": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetWidth",\n        "!doc": "Returns the layout width of an element."\n      },\n      "height": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetHeight",\n        "!doc": "Height of an element relative to the element\'s offsetParent."\n      },\n      "getContext": {\n        "!type": "fn(id: string) -> canvas.context2d",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/HTMLCanvasElement",\n        "!doc": "DOM canvas elements expose the HTMLCanvasElement interface, which provides properties and methods for manipulating the layout and presentation of canvas elements. The HTMLCanvasElement interface inherits the properties and methods of the element object interface."\n      },\n      "supportsContext": "fn(id: string) -> bool",\n      "oncopy": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.oncopy",\n        "!doc": "The oncopy property returns the onCopy event handler code on the current element."\n      },\n      "oncut": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.oncut",\n        "!doc": "The oncut property returns the onCut event handler code on the current element."\n      },\n      "onpaste": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onpaste",\n        "!doc": "The onpaste property returns the onPaste event handler code on the current element."\n      },\n      "onbeforeunload": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/HTML/Element/body",\n        "!doc": "The HTML <body> element represents the main content of an HTML document. There is only one <body> element in a document."\n      },\n      "onfocus": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onfocus",\n        "!doc": "The onfocus property returns the onFocus event handler code on the current element."\n      },\n      "onblur": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onblur",\n        "!doc": "The onblur property returns the onBlur event handler code, if any, that exists on the current element."\n      },\n      "onchange": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onchange",\n        "!doc": "The onchange property sets and returns the onChange event handler code for the current element."\n      },\n      "onclick": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onclick",\n        "!doc": "The onclick property returns the onClick event handler code on the current element."\n      },\n      "ondblclick": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.ondblclick",\n        "!doc": "The ondblclick property returns the onDblClick event handler code on the current element."\n      },\n      "onmousedown": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmousedown",\n        "!doc": "The onmousedown property returns the onMouseDown event handler code on the current element."\n      },\n      "onmouseup": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmouseup",\n        "!doc": "The onmouseup property returns the onMouseUp event handler code on the current element."\n      },\n      "onmousewheel": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/wheel",\n        "!doc": "The wheel event is fired when a wheel button of a pointing device (usually a mouse) is rotated. This event deprecates the legacy mousewheel event."\n      },\n      "onmouseover": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmouseover",\n        "!doc": "The onmouseover property returns the onMouseOver event handler code on the current element."\n      },\n      "onmouseout": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmouseout",\n        "!doc": "The onmouseout property returns the onMouseOut event handler code on the current element."\n      },\n      "onmousemove": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmousemove",\n        "!doc": "The onmousemove property returns the mousemove event handler code on the current element."\n      },\n      "oncontextmenu": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/window.oncontextmenu",\n        "!doc": "An event handler property for right-click events on the window. Unless the default behavior is prevented, the browser context menu will activate. Note that this event will occur with any non-disabled right-click event and does not depend on an element possessing the \\"contextmenu\\" attribute."\n      },\n      "onkeydown": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onkeydown",\n        "!doc": "The onkeydown property returns the onKeyDown event handler code on the current element."\n      },\n      "onkeyup": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onkeyup",\n        "!doc": "The onkeyup property returns the onKeyUp event handler code for the current element."\n      },\n      "onkeypress": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onkeypress",\n        "!doc": "The onkeypress property sets and returns the onKeyPress event handler code for the current element."\n      },\n      "onresize": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onresize",\n        "!doc": "onresize returns the element\'s onresize event handler code. It can also be used to set the code to be executed when the resize event occurs."\n      },\n      "onscroll": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.onscroll",\n        "!doc": "The onscroll property returns the onScroll event handler code on the current element."\n      },\n      "ondragstart": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n        "!doc": "The following describes the steps that occur during a drag and drop operation."\n      },\n      "ondragover": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/dragover",\n        "!doc": "The dragover event is fired when an element or text selection is being dragged over a valid drop target (every few hundred milliseconds)."\n      },\n      "ondragleave": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/dragleave",\n        "!doc": "The dragleave event is fired when a dragged element or text selection leaves a valid drop target."\n      },\n      "ondragenter": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/dragenter",\n        "!doc": "The dragenter event is fired when a dragged element or text selection enters a valid drop target."\n      },\n      "ondragend": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/dragend",\n        "!doc": "The dragend event is fired when a drag operation is being ended (by releasing a mouse button or hitting the escape key)."\n      },\n      "ondrag": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Mozilla_event_reference/drag",\n        "!doc": "The drag event is fired when an element or text selection is being dragged (every few hundred milliseconds)."\n      },\n      "offsetTop": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetTop",\n        "!doc": "Returns the distance of the current element relative to the top of the offsetParent node."\n      },\n      "offsetLeft": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetLeft",\n        "!doc": "Returns the number of pixels that the upper left corner of the current element is offset to the left within the offsetParent node."\n      },\n      "offsetHeight": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetHeight",\n        "!doc": "Height of an element relative to the element\'s offsetParent."\n      },\n      "offsetWidth": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.offsetWidth",\n        "!doc": "Returns the layout width of an element."\n      },\n      "scrollTop": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.scrollTop",\n        "!doc": "Gets or sets the number of pixels that the content of an element is scrolled upward."\n      },\n      "scrollLeft": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.scrollLeft",\n        "!doc": "Gets or sets the number of pixels that an element\'s content is scrolled to the left."\n      },\n      "scrollHeight": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.scrollHeight",\n        "!doc": "Height of the scroll view of an element; it includes the element padding but not its margin."\n      },\n      "scrollWidth": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.scrollWidth",\n        "!doc": "Read-only property that returns either the width in pixels of the content of an element or the width of the element itself, whichever is greater."\n      },\n      "clientTop": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.clientTop",\n        "!doc": "The width of the top border of an element in pixels. It does not include the top margin or padding. clientTop is read-only."\n      },\n      "clientLeft": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.clientLeft",\n        "!doc": "The width of the left border of an element in pixels. It includes the width of the vertical scrollbar if the text direction of the element is right-to-left and if there is an overflow causing a left vertical scrollbar to be rendered. clientLeft does not include the left margin or the left padding. clientLeft is read-only."\n      },\n      "clientHeight": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.clientHeight",\n        "!doc": "Returns the inner height of an element in pixels, including padding but not the horizontal scrollbar height, border, or margin."\n      },\n      "clientWidth": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.clientWidth",\n        "!doc": "The inner width of an element in pixels. It includes padding but not the vertical scrollbar (if present, if rendered), border or margin."\n      },\n      "innerHTML": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.innerHTML",\n        "!doc": "Sets or gets the HTML syntax describing the element\'s descendants."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Element",\n    "!doc": "Represents an element in an HTML or XML document."\n  },\n  "Text": {\n    "!type": "fn()",\n    "prototype": {\n      "!proto": "Node.prototype",\n      "wholeText": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Text.wholeText",\n        "!doc": "Returns all text of all Text nodes logically adjacent to the node.  The text is concatenated in document order.  This allows you to specify any text node and obtain all adjacent text as a single string."\n      },\n      "splitText": {\n        "!type": "fn(offset: number) -> +Text",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Text.splitText",\n        "!doc": "Breaks the Text node into two nodes at the specified offset, keeping both nodes in the tree as siblings."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Text",\n    "!doc": "In the DOM, the Text interface represents the textual content of an Element or Attr.  If an element has no markup within its content, it has a single child implementing Text that contains the element\'s text.  However, if the element contains markup, it is parsed into information items and Text nodes that form its children."\n  },\n  "Document": {\n    "!type": "fn()",\n    "prototype": {\n      "!proto": "Node.prototype",\n      "activeElement": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.activeElement",\n        "!doc": "Returns the currently focused element, that is, the element that will get keystroke events if the user types any. This attribute is read only."\n      },\n      "compatMode": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.compatMode",\n        "!doc": "Indicates whether the document is rendered in Quirks mode or Strict mode."\n      },\n      "designMode": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.designMode",\n        "!doc": "Can be used to make any document editable, for example in a <iframe />:"\n      },\n      "dir": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Document.dir",\n        "!doc": "This property should indicate and allow the setting of the directionality of the text of the document, whether left to right (default) or right to left."\n      },\n      "height": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.height",\n        "!doc": "Returns the height of the <body> element of the current document."\n      },\n      "width": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.width",\n        "!doc": "Returns the width of the <body> element of the current document in pixels."\n      },\n      "characterSet": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.characterSet",\n        "!doc": "Returns the character encoding of the current document."\n      },\n      "readyState": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.readyState",\n        "!doc": "Returns \\"loading\\" while the document is loading, \\"interactive\\" once it is finished parsing but still loading sub-resources, and \\"complete\\" once it has loaded."\n      },\n      "location": {\n        "!type": "location",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.location",\n        "!doc": "Returns a Location object, which contains information about the URL of the document and provides methods for changing that URL."\n      },\n      "lastModified": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.lastModified",\n        "!doc": "Returns a string containing the date and time on which the current document was last modified."\n      },\n      "head": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.head",\n        "!doc": "Returns the <head> element of the current document. If there are more than one <head> elements, the first one is returned."\n      },\n      "body": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.body",\n        "!doc": "Returns the <body> or <frameset> node of the current document."\n      },\n      "cookie": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.cookie",\n        "!doc": "Get and set the cookies associated with the current document."\n      },\n      "URL": "string",\n      "domain": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.domain",\n        "!doc": "Gets/sets the domain portion of the origin of the current document, as used by the same origin policy."\n      },\n      "referrer": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.referrer",\n        "!doc": "Returns the URI of the page that linked to this page."\n      },\n      "title": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.title",\n        "!doc": "Gets or sets the title of the document."\n      },\n      "defaultView": {\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.defaultView",\n        "!doc": "In browsers returns the window object associated with the document or null if none available."\n      },\n      "documentURI": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.documentURI",\n        "!doc": "Returns the document location as string. It is read-only per DOM4 specification."\n      },\n      "xmlStandalone": "bool",\n      "xmlVersion": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.xmlVersion",\n        "!doc": "Returns the version number as specified in the XML declaration (e.g., <?xml version=\\"1.0\\"?>) or \\"1.0\\" if the declaration is absent."\n      },\n      "xmlEncoding": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Document.xmlEncoding",\n        "!doc": "Returns the encoding as determined by the XML declaration. Should be null if unspecified or unknown."\n      },\n      "inputEncoding": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.inputEncoding",\n        "!doc": "Returns a string representing the encoding under which the document was parsed (e.g. ISO-8859-1)."\n      },\n      "documentElement": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.documentElement",\n        "!doc": "Read-only"\n      },\n      "implementation": {\n        "hasFeature": "fn(feature: string, version: number) -> bool",\n        "createDocumentType": {\n          "!type": "fn(qualifiedName: string, publicId: string, systemId: string) -> +Node",\n          "!url": "https://developer.mozilla.org/en/docs/DOM/DOMImplementation.createDocumentType",\n          "!doc": "Returns a DocumentType object which can either be used with DOMImplementation.createDocument upon document creation or they can be put into the document via Node.insertBefore() or Node.replaceChild(): http://www.w3.org/TR/DOM-Level-3-Cor...l#ID-B63ED1A31 (less ideal due to features not likely being as accessible: http://www.w3.org/TR/DOM-Level-3-Cor...createDocument ). In any case, entity declarations and notations will not be available: http://www.w3.org/TR/DOM-Level-3-Cor...-createDocType   "\n        },\n        "createHTMLDocument": {\n          "!type": "fn(title: string) -> +Document",\n          "!url": "https://developer.mozilla.org/en/docs/DOM/DOMImplementation.createHTMLDocument",\n          "!doc": "This method (available from document.implementation) creates a new HTML document."\n        },\n        "createDocument": {\n          "!type": "fn(namespaceURI: string, qualifiedName: string, type: +Node) -> +Document",\n          "!url": "https://developer.mozilla.org/en-US/docs/DOM/DOMImplementation.createHTMLDocument",\n          "!doc": "This method creates a new HTML document."\n        },\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.implementation",\n        "!doc": "Returns a DOMImplementation object associated with the current document."\n      },\n      "doctype": {\n        "!type": "+Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.doctype",\n        "!doc": "Returns the Document Type Declaration (DTD) associated with current document. The returned object implements the DocumentType interface. Use DOMImplementation.createDocumentType() to create a DocumentType."\n      },\n      "open": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.open",\n        "!doc": "The document.open() method opens a document for writing."\n      },\n      "close": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.close",\n        "!doc": "The document.close() method finishes writing to a document, opened with document.open()."\n      },\n      "write": {\n        "!type": "fn(html: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.write",\n        "!doc": "Writes a string of text to a document stream opened by document.open()."\n      },\n      "writeln": {\n        "!type": "fn(html: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.writeln",\n        "!doc": "Writes a string of text followed by a newline character to a document."\n      },\n      "clear": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.clear",\n        "!doc": "In recent versions of Mozilla-based applications as well as in Internet Explorer and Netscape 4 this method does nothing."\n      },\n      "hasFocus": {\n        "!type": "fn() -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.hasFocus",\n        "!doc": "Returns a Boolean value indicating whether the document or any element inside the document has focus. This method can be used to determine whether the active element in a document has focus."\n      },\n      "createElement": {\n        "!type": "fn(tagName: string) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createElement",\n        "!doc": "Creates the specified element."\n      },\n      "createElementNS": {\n        "!type": "fn(ns: string, tagName: string) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createElementNS",\n        "!doc": "Creates an element with the specified namespace URI and qualified name."\n      },\n      "createDocumentFragment": {\n        "!type": "fn() -> +DocumentFragment",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createDocumentFragment",\n        "!doc": "Creates a new empty DocumentFragment."\n      },\n      "createTextNode": {\n        "!type": "fn(content: string) -> +Text",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createTextNode",\n        "!doc": "Creates a new Text node."\n      },\n      "createComment": {\n        "!type": "fn(content: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createComment",\n        "!doc": "Creates a new comment node, and returns it."\n      },\n      "createCDATASection": {\n        "!type": "fn(content: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createCDATASection",\n        "!doc": "Creates a new CDATA section node, and returns it. "\n      },\n      "createProcessingInstruction": {\n        "!type": "fn(content: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createProcessingInstruction",\n        "!doc": "Creates a new processing instruction node, and returns it."\n      },\n      "createAttribute": {\n        "!type": "fn(name: string) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createAttribute",\n        "!doc": "Creates a new attribute node, and returns it."\n      },\n      "createAttributeNS": {\n        "!type": "fn(ns: string, name: string) -> +Attr",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Attr",\n        "!doc": "This type represents a DOM element\'s attribute as an object. In most DOM methods, you will probably directly retrieve the attribute as a string (e.g., Element.getAttribute(), but certain functions (e.g., Element.getAttributeNode()) or means of iterating give Attr types."\n      },\n      "importNode": {\n        "!type": "fn(node: +Node, deep: bool) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.importNode",\n        "!doc": "Creates a copy of a node from an external document that can be inserted into the current document."\n      },\n      "getElementById": {\n        "!type": "fn(id: string) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getElementById",\n        "!doc": "Returns a reference to the element by its ID."\n      },\n      "getElementsByTagName": {\n        "!type": "fn(tagName: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getElementsByTagName",\n        "!doc": "Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node. The returned NodeList is live, meaning that it updates itself automatically to stay in sync with the DOM tree without having to call document.getElementsByTagName again."\n      },\n      "getElementsByTagNameNS": {\n        "!type": "fn(ns: string, tagName: string) -> +NodeList",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getElementsByTagNameNS",\n        "!doc": "Returns a list of elements with the given tag name belonging to the given namespace. The complete document is searched, including the root node."\n      },\n      "createEvent": {\n        "!type": "fn(type: string) -> +Event",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createEvent",\n        "!doc": "Creates an event of the type specified. The returned object should be first initialized and can then be passed to element.dispatchEvent."\n      },\n      "createRange": {\n        "!type": "fn() -> +Range",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createRange",\n        "!doc": "Returns a new Range object."\n      },\n      "evaluate": {\n        "!type": "fn(expr: ?) -> +XPathResult",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.evaluate",\n        "!doc": "Returns an XPathResult based on an XPath expression and other given parameters."\n      },\n      "execCommand": {\n        "!type": "fn(cmd: string)",\n        "!url": "https://developer.mozilla.org/en-US/docs/Rich-Text_Editing_in_Mozilla#Executing_Commands",\n        "!doc": "Run command to manipulate the contents of an editable region."\n      },\n      "queryCommandEnabled": {\n        "!type": "fn(cmd: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n        "!doc": "Returns true if the Midas command can be executed on the current range."\n      },\n      "queryCommandIndeterm": {\n        "!type": "fn(cmd: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n        "!doc": "Returns true if the Midas command is in a indeterminate state on the current range."\n      },\n      "queryCommandState": {\n        "!type": "fn(cmd: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n        "!doc": "Returns true if the Midas command has been executed on the current range."\n      },\n      "queryCommandSupported": {\n        "!type": "fn(cmd: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.queryCommandSupported",\n        "!doc": "Reports whether or not the specified editor query command is supported by the browser."\n      },\n      "queryCommandValue": {\n        "!type": "fn(cmd: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n        "!doc": "Returns the current value of the current range for Midas command."\n      },\n      "getElementsByName": {\n        "!type": "fn(name: string) -> +HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getElementsByName",\n        "!doc": "Returns a list of elements with a given name in the HTML document."\n      },\n      "elementFromPoint": {\n        "!type": "fn(x: number, y: number) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.elementFromPoint",\n        "!doc": "Returns the element from the document whose elementFromPoint method is being called which is the topmost element which lies under the given point.  To get an element, specify the point via coordinates, in CSS pixels, relative to the upper-left-most point in the window or frame containing the document."\n      },\n      "getSelection": {\n        "!type": "fn() -> +Selection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.getSelection",\n        "!doc": "The DOM getSelection() method is available on the Window and Document interfaces."\n      },\n      "adoptNode": {\n        "!type": "fn(node: +Node) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.adoptNode",\n        "!doc": "Adopts a node from an external document. The node and its subtree is removed from the document it\'s in (if any), and its ownerDocument is changed to the current document. The node can then be inserted into the current document."\n      },\n      "createTreeWalker": {\n        "!type": "fn(root: +Node, mask: number) -> ?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createTreeWalker",\n        "!doc": "Returns a new TreeWalker object."\n      },\n      "createExpression": {\n        "!type": "fn(text: string) -> ?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createExpression",\n        "!doc": "This method compiles an XPathExpression which can then be used for (repeated) evaluations."\n      },\n      "createNSResolver": {\n        "!type": "fn(node: +Node)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.createNSResolver",\n        "!doc": "Creates an XPathNSResolver which resolves namespaces with respect to the definitions in scope for a specified node."\n      },\n      "scripts": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Document.scripts",\n        "!doc": "Returns a list of the <script> elements in the document. The returned object is an HTMLCollection."\n      },\n      "plugins": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.plugins",\n        "!doc": "Returns an HTMLCollection object containing one or more HTMLEmbedElements or null which represent the <embed> elements in the current document."\n      },\n      "embeds": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.embeds",\n        "!doc": "Returns a list of the embedded OBJECTS within the current document."\n      },\n      "anchors": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.anchors",\n        "!doc": "Returns a list of all of the anchors in the document."\n      },\n      "links": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.links",\n        "!doc": "The links property returns a collection of all AREA elements and anchor elements in a document with a value for the href attribute. "\n      },\n      "forms": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.forms",\n        "!doc": "Returns a collection (an HTMLCollection) of the form elements within the current document."\n      },\n      "styleSheets": {\n        "!type": "+HTMLCollection",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.styleSheets",\n        "!doc": "Returns a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n    "!doc": "Each web page loaded in the browser has its own document object. This object serves as an entry point to the web page\'s content (the DOM tree, including elements such as <body> and <table>) and provides functionality global to the document (such as obtaining the page\'s URL and creating new elements in the document)."\n  },\n  "document": {\n    "!type": "+Document",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/document",\n    "!doc": "Each web page loaded in the browser has its own document object. This object serves as an entry point to the web page\'s content (the DOM tree, including elements such as <body> and <table>) and provides functionality global to the document (such as obtaining the page\'s URL and creating new elements in the document)."\n  },\n  "XMLDocument": {\n    "!type": "fn()",\n    "prototype": "Document.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/Parsing_and_serializing_XML",\n    "!doc": "The Web platform provides the following objects for parsing and serializing XML:"\n  },\n  "Attr": {\n    "!type": "fn()",\n    "prototype": {\n      "isId": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Attr",\n        "!doc": "This type represents a DOM element\'s attribute as an object. In most DOM methods, you will probably directly retrieve the attribute as a string (e.g., Element.getAttribute(), but certain functions (e.g., Element.getAttributeNode()) or means of iterating give Attr types."\n      },\n      "name": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Attr",\n        "!doc": "This type represents a DOM element\'s attribute as an object. In most DOM methods, you will probably directly retrieve the attribute as a string (e.g., Element.getAttribute(), but certain functions (e.g., Element.getAttributeNode()) or means of iterating give Attr types."\n      },\n      "value": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Attr",\n        "!doc": "This type represents a DOM element\'s attribute as an object. In most DOM methods, you will probably directly retrieve the attribute as a string (e.g., Element.getAttribute(), but certain functions (e.g., Element.getAttributeNode()) or means of iterating give Attr types."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Attr",\n    "!doc": "This type represents a DOM element\'s attribute as an object. In most DOM methods, you will probably directly retrieve the attribute as a string (e.g., Element.getAttribute(), but certain functions (e.g., Element.getAttributeNode()) or means of iterating give Attr types."\n  },\n  "NodeList": {\n    "!type": "fn()",\n    "prototype": {\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.length",\n        "!doc": "Returns the number of items in a NodeList."\n      },\n      "item": {\n        "!type": "fn(i: number) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NodeList.item",\n        "!doc": "Returns a node from a NodeList by index."\n      },\n      "<i>": "+Element"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/NodeList",\n    "!doc": "NodeList objects are collections of nodes returned by getElementsByTagName, getElementsByTagNameNS, Node.childNodes, querySelectorAll, getElementsByClassName, etc."\n  },\n  "HTMLCollection": {\n    "!type": "fn()",\n    "prototype": {\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/HTMLCollection",\n        "!doc": "The number of items in the collection."\n      },\n      "item": {\n        "!type": "fn(i: number) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/HTMLCollection",\n        "!doc": "Returns the specific node at the given zero-based index into the list. Returns null if the index is out of range."\n      },\n      "namedItem": {\n        "!type": "fn(name: string) -> +Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/HTMLCollection",\n        "!doc": "Returns the specific node whose ID or, as a fallback, name matches the string specified by name. Matching by name is only done as a last resort, only in HTML, and only if the referenced element supports the name attribute. Returns null if no node exists by the given name."\n      },\n      "<i>": "+Element"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/HTMLCollection",\n    "!doc": "HTMLCollection is an interface representing a generic collection of elements (in document order) and offers methods and properties for traversing the list."\n  },\n  "NamedNodeMap": {\n    "!type": "fn()",\n    "prototype": {\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "The number of items in the map."\n      },\n      "getNamedItem": {\n        "!type": "fn(name: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Gets a node by name."\n      },\n      "setNamedItem": {\n        "!type": "fn(node: +Node) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Adds (or replaces) a node by its nodeName."\n      },\n      "removeNamedItem": {\n        "!type": "fn(name: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Removes a node (or if an attribute, may reveal a default if present)."\n      },\n      "item": {\n        "!type": "fn(i: number) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Returns the item at the given index (or null if the index is higher or equal to the number of nodes)."\n      },\n      "getNamedItemNS": {\n        "!type": "fn(ns: string, name: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Gets a node by namespace and localName."\n      },\n      "setNamedItemNS": {\n        "!type": "fn(node: +Node) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Adds (or replaces) a node by its localName and namespaceURI."\n      },\n      "removeNamedItemNS": {\n        "!type": "fn(ns: string, name: string) -> +Node",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n        "!doc": "Removes a node (or if an attribute, may reveal a default if present)."\n      },\n      "<i>": "+Node"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/NamedNodeMap",\n    "!doc": "A collection of nodes returned by Element.attributes (also potentially for DocumentType.entities, DocumentType.notations). NamedNodeMaps are not in any particular order (unlike NodeList), although they may be accessed by an index as in an array (they may also be accessed with the item() method). A NamedNodeMap object are live and will thus be auto-updated if changes are made to their contents internally or elsewhere."\n  },\n  "DocumentFragment": {\n    "!type": "fn()",\n    "prototype": {\n      "!proto": "Node.prototype"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/document.createDocumentFragment",\n    "!doc": "Creates a new empty DocumentFragment."\n  },\n  "DOMTokenList": {\n    "!type": "fn()",\n    "prototype": {\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "The amount of items in the list."\n      },\n      "item": {\n        "!type": "fn(i: number) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "Returns an item in the list by its index."\n      },\n      "contains": {\n        "!type": "fn(token: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "Return true if the underlying string contains token, otherwise false."\n      },\n      "add": {\n        "!type": "fn(token: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "Adds token to the underlying string."\n      },\n      "remove": {\n        "!type": "fn(token: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "Remove token from the underlying string."\n      },\n      "toggle": {\n        "!type": "fn(token: string) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n        "!doc": "Removes token from string and returns false. If token doesn\'t exist it\'s added and the function returns true."\n      },\n      "<i>": "string"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/DOMTokenList",\n    "!doc": "This type represents a set of space-separated tokens. Commonly returned by HTMLElement.classList, HTMLLinkElement.relList, HTMLAnchorElement.relList or HTMLAreaElement.relList. It is indexed beginning with 0 as with JavaScript arrays. DOMTokenList is always case-sensitive."\n  },\n  "XPathResult": {\n    "!type": "fn()",\n    "prototype": {\n      "boolValue": "bool",\n      "invalidIteratorState": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript",\n        "!doc": "This document describes the interface for using XPath in JavaScript internally, in extensions, and from websites. Mozilla implements a fair amount of the DOM 3 XPath. Which means that XPath expressions can be run against both HTML and XML documents."\n      },\n      "numberValue": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/XPathResult",\n        "!doc": "Refer to nsIDOMXPathResult for more detail."\n      },\n      "resultType": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/document.evaluate",\n        "!doc": "Returns an XPathResult based on an XPath expression and other given parameters."\n      },\n      "singleNodeValue": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript",\n        "!doc": "This document describes the interface for using XPath in JavaScript internally, in extensions, and from websites. Mozilla implements a fair amount of the DOM 3 XPath. Which means that XPath expressions can be run against both HTML and XML documents."\n      },\n      "snapshotLength": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/XPathResult",\n        "!doc": "Refer to nsIDOMXPathResult for more detail."\n      },\n      "stringValue": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript",\n        "!doc": "This document describes the interface for using XPath in JavaScript internally, in extensions, and from websites. Mozilla implements a fair amount of the DOM 3 XPath. Which means that XPath expressions can be run against both HTML and XML documents."\n      },\n      "iterateNext": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/Introduction_to_using_XPath_in_JavaScript",\n        "!doc": "This document describes the interface for using XPath in JavaScript internally, in extensions, and from websites. Mozilla implements a fair amount of the DOM 3 XPath. Which means that XPath expressions can be run against both HTML and XML documents."\n      },\n      "snapshotItem": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en-US/docs/XPathResult#snapshotItem()"\n      },\n      "ANY_TYPE": "number",\n      "NUMBER_TYPE": "number",\n      "STRING_TYPE": "number",\n      "BOOL_TYPE": "number",\n      "UNORDERED_NODE_ITERATOR_TYPE": "number",\n      "ORDERED_NODE_ITERATOR_TYPE": "number",\n      "UNORDERED_NODE_SNAPSHOT_TYPE": "number",\n      "ORDERED_NODE_SNAPSHOT_TYPE": "number",\n      "ANY_UNORDERED_NODE_TYPE": "number",\n      "FIRST_ORDERED_NODE_TYPE": "number"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/XPathResult",\n    "!doc": "Refer to nsIDOMXPathResult for more detail."\n  },\n  "ClientRect": {\n    "!type": "fn()",\n    "prototype": {\n      "top": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n        "!doc": "Top of the box, in pixels, relative to the viewport."\n      },\n      "left": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n        "!doc": "Left of the box, in pixels, relative to the viewport."\n      },\n      "bottom": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n        "!doc": "Bottom of the box, in pixels, relative to the viewport."\n      },\n      "right": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n        "!doc": "Right of the box, in pixels, relative to the viewport."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.getClientRects",\n    "!doc": "Returns a collection of rectangles that indicate the bounding rectangles for each box in a client."\n  },\n  "Event": {\n    "!type": "fn()",\n    "prototype": {\n      "stopPropagation": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.stopPropagation",\n        "!doc": "Prevents further propagation of the current event."\n      },\n      "preventDefault": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.preventDefault",\n        "!doc": "Cancels the event if it is cancelable, without stopping further propagation of the event."\n      },\n      "initEvent": {\n        "!type": "fn(type: string, bubbles: bool, cancelable: bool)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.initEvent",\n        "!doc": "The initEvent method is used to initialize the value of an event created using document.createEvent."\n      },\n      "stopImmediatePropagation": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.stopImmediatePropagation",\n        "!doc": "Prevents other listeners of the same event to be called."\n      },\n      "NONE": "number",\n      "CAPTURING_PHASE": "number",\n      "AT_TARGET": "number",\n      "BUBBLING_PHASE": "number",\n      "MOUSEDOWN": "number",\n      "MOUSEUP": "number",\n      "MOUSEOVER": "number",\n      "MOUSEOUT": "number",\n      "MOUSEMOVE": "number",\n      "MOUSEDRAG": "number",\n      "CLICK": "number",\n      "DBLCLICK": "number",\n      "KEYDOWN": "number",\n      "KEYUP": "number",\n      "KEYPRESS": "number",\n      "DRAGDROP": "number",\n      "FOCUS": "number",\n      "BLUR": "number",\n      "SELECT": "number",\n      "CHANGE": "number",\n      "target": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget",\n        "!doc": "An EventTarget is a DOM interface implemented by objects that can receive DOM events and have listeners for them. The most common EventTargets are DOM elements, although other objects can be EventTargets too, for example document, window, XMLHttpRequest, and others."\n      },\n      "relatedTarget": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.relatedTarget",\n        "!doc": "Identifies a secondary target for the event."\n      },\n      "pageX": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.pageX",\n        "!doc": "Returns the horizontal coordinate of the event relative to whole document."\n      },\n      "pageY": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.pageY",\n        "!doc": "Returns the vertical coordinate of the event relative to the whole document."\n      },\n      "clientX": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.clientX",\n        "!doc": "Returns the horizontal coordinate within the application\'s client area at which the event occurred (as opposed to the coordinates within the page). For example, clicking in the top-left corner of the client area will always result in a mouse event with a clientX value of 0, regardless of whether the page is scrolled horizontally."\n      },\n      "clientY": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.clientY",\n        "!doc": "Returns the vertical coordinate within the application\'s client area at which the event occurred (as opposed to the coordinates within the page). For example, clicking in the top-left corner of the client area will always result in a mouse event with a clientY value of 0, regardless of whether the page is scrolled vertically."\n      },\n      "keyCode": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.keyCode",\n        "!doc": "Returns the Unicode value of a non-character key in a keypress event or any key in any other type of keyboard event."\n      },\n      "charCode": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.charCode",\n        "!doc": "Returns the Unicode value of a character key pressed during a keypress event."\n      },\n      "which": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.which",\n        "!doc": "Returns the numeric keyCode of the key pressed, or the character code (charCode) for an alphanumeric key pressed."\n      },\n      "button": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.button",\n        "!doc": "Indicates which mouse button caused the event."\n      },\n      "shiftKey": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.shiftKey",\n        "!doc": "Indicates whether the SHIFT key was pressed when the event fired."\n      },\n      "ctrlKey": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.ctrlKey",\n        "!doc": "Indicates whether the CTRL key was pressed when the event fired."\n      },\n      "altKey": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.altKey",\n        "!doc": "Indicates whether the ALT key was pressed when the event fired."\n      },\n      "metaKey": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.metaKey",\n        "!doc": "Indicates whether the META key was pressed when the event fired."\n      },\n      "returnValue": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/window.onbeforeunload",\n        "!doc": "An event that fires when a window is about to unload its resources. The document is still visible and the event is still cancelable."\n      },\n      "cancelBubble": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/event.cancelBubble",\n        "!doc": "bool is the boolean value of true or false."\n      },\n      "dataTransfer": {\n        "dropEffect": {\n          "!type": "string",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/DataTransfer",\n          "!doc": "The actual effect that will be used, and should always be one of the possible values of effectAllowed."\n        },\n        "effectAllowed": {\n          "!type": "string",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n          "!doc": "Specifies the effects that are allowed for this drag."\n        },\n        "files": {\n          "!type": "+FileList",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/DataTransfer",\n          "!doc": "Contains a list of all the local files available on the data transfer."\n        },\n        "types": {\n          "!type": "[string]",\n          "!url": "https://developer.mozilla.org/en-US/docs/DragDrop/DataTransfer",\n          "!doc": "Holds a list of the format types of the data that is stored for the first item, in the same order the data was added. An empty list will be returned if no data was added."\n        },\n        "addElement": {\n          "!type": "fn(element: +Element)",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/DataTransfer",\n          "!doc": "Set the drag source."\n        },\n        "clearData": {\n          "!type": "fn(type?: string)",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n          "!doc": "Remove the data associated with a given type."\n        },\n        "getData": {\n          "!type": "fn(type: string) -> string",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n          "!doc": "Retrieves the data for a given type, or an empty string if data for that type does not exist or the data transfer contains no data."\n        },\n        "setData": {\n          "!type": "fn(type: string, data: string)",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n          "!doc": "Set the data for a given type."\n        },\n        "setDragImage": {\n          "!type": "fn(image: +Element)",\n          "!url": "https://developer.mozilla.org/en/docs/DragDrop/Drag_Operations",\n          "!doc": "Set the image to be used for dragging if a custom one is desired."\n        },\n        "!url": "https://developer.mozilla.org/en/docs/DragDrop/DataTransfer",\n        "!doc": "This object is available from the dataTransfer property of all drag events. It cannot be created separately."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/event",\n    "!doc": "The DOM Event interface is accessible from within the handler function, via the event object passed as the first argument."\n  },\n  "TouchEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Touch_events",\n    "!doc": "In order to provide quality support for touch-based user interfaces, touch events offer the ability to interpret finger activity on touch screens or trackpads."\n  },\n  "WheelEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/WheelEvent",\n    "!doc": "The DOM WheelEvent represents events that occur due to the user moving a mouse wheel or similar input device."\n  },\n  "MouseEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/MouseEvent",\n    "!doc": "The DOM MouseEvent represents events that occur due to the user interacting with a pointing device (such as a mouse). It\'s represented by the nsINSDOMMouseEvent interface, which extends the nsIDOMMouseEvent interface."\n  },\n  "KeyboardEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/KeyboardEvent",\n    "!doc": "KeyboardEvent objects describe a user interaction with the keyboard. Each event describes a key; the event type (keydown, keypress, or keyup) identifies what kind of activity was performed."\n  },\n  "HashChangeEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onhashchange",\n    "!doc": "The hashchange event fires when a window\'s hash changes."\n  },\n  "ErrorEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/DOM_event_reference/error",\n    "!doc": "The error event is fired whenever a resource fails to load."\n  },\n  "CustomEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Event/CustomEvent",\n    "!doc": "The DOM CustomEvent are events initialized by an application for any purpose."\n  },\n  "BeforeLoadEvent": {\n    "!type": "fn()",\n    "prototype": "Event.prototype",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window",\n    "!doc": "This section provides a brief reference for all of the methods, properties, and events available through the DOM window object. The window object implements the Window interface, which in turn inherits from the AbstractView interface. Some additional global functions, namespaces objects, and constructors, not typically associated with the window, but available on it, are listed in the JavaScript Reference."\n  },\n  "WebSocket": {\n    "!type": "fn(url: string)",\n    "prototype": {\n      "close": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/CloseEvent",\n        "!doc": "A CloseEvent is sent to clients using WebSockets when the connection is closed. This is delivered to the listener indicated by the WebSocket object\'s onclose attribute."\n      },\n      "send": {\n        "!type": "fn(data: string)",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/WebSocket",\n        "!doc": "The WebSocket object provides the API for creating and managing a WebSocket connection to a server, as well as for sending and receiving data on the connection."\n      },\n      "binaryType": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/WebSocket",\n        "!doc": "The WebSocket object provides the API for creating and managing a WebSocket connection to a server, as well as for sending and receiving data on the connection."\n      },\n      "bufferedAmount": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/Writing_WebSocket_client_applications",\n        "!doc": "WebSockets is a technology that makes it possible to open an interactive communication session between the user\'s browser and a server. Using a WebSocket connection, Web applications can perform real-time communication instead of having to poll for changes back and forth."\n      },\n      "extensions": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/WebSocket",\n        "!doc": "The WebSocket object provides the API for creating and managing a WebSocket connection to a server, as well as for sending and receiving data on the connection."\n      },\n      "onclose": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/CloseEvent",\n        "!doc": "A CloseEvent is sent to clients using WebSockets when the connection is closed. This is delivered to the listener indicated by the WebSocket object\'s onclose attribute."\n      },\n      "onerror": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/Writing_WebSocket_client_applications",\n        "!doc": "WebSockets is a technology that makes it possible to open an interactive communication session between the user\'s browser and a server. Using a WebSocket connection, Web applications can perform real-time communication instead of having to poll for changes back and forth."\n      },\n      "onmessage": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/WebSocket",\n        "!doc": "The WebSocket object provides the API for creating and managing a WebSocket connection to a server, as well as for sending and receiving data on the connection."\n      },\n      "onopen": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/WebSockets_reference/WebSocket",\n        "!doc": "The WebSocket object provides the API for creating and managing a WebSocket connection to a server, as well as for sending and receiving data on the connection."\n      },\n      "protocol": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets",\n        "!doc": "WebSockets is an advanced technology that makes it possible to open an interactive communication session between the user\'s browser and a server. With this API, you can send messages to a server and receive event-driven responses without having to poll the server for a reply."\n      },\n      "url": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/WebSockets/Writing_WebSocket_client_applications",\n        "!doc": "WebSockets is a technology that makes it possible to open an interactive communication session between the user\'s browser and a server. Using a WebSocket connection, Web applications can perform real-time communication instead of having to poll for changes back and forth."\n      },\n      "CONNECTING": "number",\n      "OPEN": "number",\n      "CLOSING": "number",\n      "CLOSED": "number"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/WebSockets",\n    "!doc": "WebSockets is an advanced technology that makes it possible to open an interactive communication session between the user\'s browser and a server. With this API, you can send messages to a server and receive event-driven responses without having to poll the server for a reply."\n  },\n  "Worker": {\n    "!type": "fn(scriptURL: string)",\n    "prototype": {\n      "postMessage": {\n        "!type": "fn(message: ?)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n        "!doc": "Sends a message to the worker\'s inner scope. This accepts a single parameter, which is the data to send to the worker. The data may be any value or JavaScript object handled by the structured clone algorithm, which includes cyclical references."\n      },\n      "terminate": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n        "!doc": "Immediately terminates the worker. This does not offer the worker an opportunity to finish its operations; it is simply stopped at once."\n      },\n      "onmessage": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n        "!doc": "An event listener that is called whenever a MessageEvent with type message bubbles through the worker. The message is stored in the event\'s data member."\n      },\n      "onerror": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n        "!doc": "An event listener that is called whenever an ErrorEvent with type error bubbles through the worker."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n    "!doc": "Workers are background tasks that can be easily created and can send messages back to their creators. Creating a worker is as simple as calling the Worker() constructor, specifying a script to be run in the worker thread."\n  },\n  "localStorage": {\n    "setItem": {\n      "!type": "fn(name: string, value: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n      "!doc": "Store an item in storage."\n    },\n    "getItem": {\n      "!type": "fn(name: string) -> string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n      "!doc": "Retrieve an item from storage."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n    "!doc": "The DOM Storage mechanism is a means through which string key/value pairs can be securely stored and later retrieved for use."\n  },\n  "sessionStorage": {\n    "setItem": {\n      "!type": "fn(name: string, value: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n      "!doc": "Store an item in storage."\n    },\n    "getItem": {\n      "!type": "fn(name: string) -> string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n      "!doc": "Retrieve an item from storage."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Storage",\n    "!doc": "This is a global object (sessionStorage) that maintains a storage area that\'s available for the duration of the page session. A page session lasts for as long as the browser is open and survives over page reloads and restores. Opening a page in a new tab or window will cause a new session to be initiated."\n  },\n  "FileList": {\n    "!type": "fn()",\n    "prototype": {\n      "length": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileList",\n        "!doc": "A read-only value indicating the number of files in the list."\n      },\n      "item": {\n        "!type": "fn(i: number) -> +File",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileList",\n        "!doc": "Returns a File object representing the file at the specified index in the file list."\n      },\n      "<i>": "+File"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/FileList",\n    "!doc": "An object of this type is returned by the files property of the HTML input element; this lets you access the list of files selected with the <input type=\\"file\\"> element. It\'s also used for a list of files dropped into web content when using the drag and drop API."\n  },\n  "File": {\n    "!type": "fn()",\n    "prototype": {\n      "!proto": "Blob.prototype",\n      "fileName": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/File.fileName",\n        "!doc": "Returns the name of the file. For security reasons the path is excluded from this property."\n      },\n      "fileSize": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/File.fileSize",\n        "!doc": "Returns the size of a file in bytes."\n      },\n      "lastModifiedDate": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/File.lastModifiedDate",\n        "!doc": "Returns the last modified date of the file. Files without a known last modified date use the current date instead."\n      },\n      "name": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/File.name",\n        "!doc": "Returns the name of the file. For security reasons, the path is excluded from this property."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/File",\n    "!doc": "The File object provides information about -- and access to the contents of -- files. These are generally retrieved from a FileList object returned as a result of a user selecting files using the input element, or from a drag and drop operation\'s DataTransfer object."\n  },\n  "Blob": {\n    "!type": "fn(parts: [?], properties?: ?)",\n    "prototype": {\n      "size": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Blob",\n        "!doc": "The size, in bytes, of the data contained in the Blob object. Read only."\n      },\n      "type": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Blob",\n        "!doc": "An ASCII-encoded string, in all lower case, indicating the MIME type of the data contained in the Blob. If the type is unknown, this string is empty. Read only."\n      },\n      "slice": {\n        "!type": "fn(start: number, end?: number, type?: string) -> +Blob",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Blob",\n        "!doc": "Returns a new Blob object containing the data in the specified range of bytes of the source Blob."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Blob",\n    "!doc": "A Blob object represents a file-like object of immutable, raw data. Blobs represent data that isn\'t necessarily in a JavaScript-native format. The File interface is based on Blob, inheriting blob functionality and expanding it to support files on the user\'s system."\n  },\n  "FileReader": {\n    "!type": "fn()",\n    "prototype": {\n      "abort": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Aborts the read operation. Upon return, the readyState will be DONE."\n      },\n      "readAsArrayBuffer": {\n        "!type": "fn(blob: +Blob)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Starts reading the contents of the specified Blob, producing an ArrayBuffer."\n      },\n      "readAsBinaryString": {\n        "!type": "fn(blob: +Blob)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Starts reading the contents of the specified Blob, producing raw binary data."\n      },\n      "readAsDataURL": {\n        "!type": "fn(blob: +Blob)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Starts reading the contents of the specified Blob, producing a data: url."\n      },\n      "readAsText": {\n        "!type": "fn(blob: +Blob, encoding?: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Starts reading the contents of the specified Blob, producing a string."\n      },\n      "EMPTY": "number",\n      "LOADING": "number",\n      "DONE": "number",\n      "error": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "The error that occurred while reading the file. Read only."\n      },\n      "readyState": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Indicates the state of the FileReader. This will be one of the State constants. Read only."\n      },\n      "result": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "The file\'s contents. This property is only valid after the read operation is complete, and the format of the data depends on which of the methods was used to initiate the read operation. Read only."\n      },\n      "onabort": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called when the read operation is aborted."\n      },\n      "onerror": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called when an error occurs."\n      },\n      "onload": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called when the read operation is successfully completed."\n      },\n      "onloadend": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called when the read is completed, whether successful or not. This is called after either onload or onerror."\n      },\n      "onloadstart": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called when reading the data is about to begin."\n      },\n      "onprogress": {\n        "!type": "?",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n        "!doc": "Called periodically while the data is being read."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/FileReader",\n    "!doc": "The FileReader object lets web applications asynchronously read the contents of files (or raw data buffers) stored on the user\'s computer, using File or Blob objects to specify the file or data to read. File objects may be obtained from a FileList object returned as a result of a user selecting files using the <input> element, from a drag and drop operation\'s DataTransfer object, or from the mozGetAsFile() API on an HTMLCanvasElement."\n  },\n  "Range": {\n    "!type": "fn()",\n    "prototype": {\n      "collapsed": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.collapsed",\n        "!doc": "Returns a boolean indicating whether the range\'s start and end points are at the same position."\n      },\n      "commonAncestorContainer": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.commonAncestorContainer",\n        "!doc": "Returns the deepest Node that contains the  startContainer and  endContainer Nodes."\n      },\n      "endContainer": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.endContainer",\n        "!doc": "Returns the Node within which the Range ends."\n      },\n      "endOffset": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.endOffset",\n        "!doc": "Returns a number representing where in the  endContainer the Range ends."\n      },\n      "startContainer": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.startContainer",\n        "!doc": "Returns the Node within which the Range starts."\n      },\n      "startOffset": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.startOffset",\n        "!doc": "Returns a number representing where in the startContainer the Range starts."\n      },\n      "setStart": {\n        "!type": "fn(node: +Element, offset: number)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setStart",\n        "!doc": "Sets the start position of a Range."\n      },\n      "setEnd": {\n        "!type": "fn(node: +Element, offset: number)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setEnd",\n        "!doc": "Sets the end position of a Range."\n      },\n      "setStartBefore": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setStartBefore",\n        "!doc": "Sets the start position of a Range relative to another Node."\n      },\n      "setStartAfter": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setStartAfter",\n        "!doc": "Sets the start position of a Range relative to a Node."\n      },\n      "setEndBefore": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setEndBefore",\n        "!doc": "Sets the end position of a Range relative to another Node."\n      },\n      "setEndAfter": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.setEndAfter",\n        "!doc": "Sets the end position of a Range relative to another Node."\n      },\n      "selectNode": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.selectNode",\n        "!doc": "Sets the Range to contain the Node and its contents."\n      },\n      "selectNodeContents": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.selectNodeContents",\n        "!doc": "Sets the Range to contain the contents of a Node."\n      },\n      "collapse": {\n        "!type": "fn(toStart: bool)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.collapse",\n        "!doc": "Collapses the Range to one of its boundary points."\n      },\n      "cloneContents": {\n        "!type": "fn() -> +DocumentFragment",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.cloneContents",\n        "!doc": "Returns a DocumentFragment copying the Nodes of a Range."\n      },\n      "deleteContents": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.deleteContents",\n        "!doc": "Removes the contents of a Range from the Document."\n      },\n      "extractContents": {\n        "!type": "fn() -> +DocumentFragment",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.extractContents",\n        "!doc": "Moves contents of a Range from the document tree into a DocumentFragment."\n      },\n      "insertNode": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.insertNode",\n        "!doc": "Insert a node at the start of a Range."\n      },\n      "surroundContents": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.surroundContents",\n        "!doc": "Moves content of a Range into a new node, placing the new node at the start of the specified range."\n      },\n      "compareBoundaryPoints": {\n        "!type": "fn(how: number, other: +Range) -> number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.compareBoundaryPoints",\n        "!doc": "Compares the boundary points of two Ranges."\n      },\n      "cloneRange": {\n        "!type": "fn() -> +Range",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.cloneRange",\n        "!doc": "Returns a Range object with boundary points identical to the cloned Range."\n      },\n      "detach": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/range.detach",\n        "!doc": "Releases a Range from use to improve performance. This lets the browser choose to release resources associated with this Range. Subsequent attempts to use the detached range will result in a DOMException being thrown with an error code of INVALID_STATE_ERR."\n      },\n      "END_TO_END": "number",\n      "END_TO_START": "number",\n      "START_TO_END": "number",\n      "START_TO_START": "number"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/range.detach",\n    "!doc": "Releases a Range from use to improve performance. This lets the browser choose to release resources associated with this Range. Subsequent attempts to use the detached range will result in a DOMException being thrown with an error code of INVALID_STATE_ERR."\n  },\n  "XMLHttpRequest": {\n    "!type": "fn()",\n    "prototype": {\n      "abort": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Aborts the request if it has already been sent."\n      },\n      "getAllResponseHeaders": {\n        "!type": "fn() -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Returns all the response headers as a string, or null if no response has been received. Note: For multipart requests, this returns the headers from the current part of the request, not from the original channel."\n      },\n      "getResponseHeader": {\n        "!type": "fn(header: string) -> string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Returns the string containing the text of the specified header, or null if either the response has not yet been received or the header doesn\'t exist in the response."\n      },\n      "open": {\n        "!type": "fn(method: string, url: string, async?: bool, user?: string, password?: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Initializes a request."\n      },\n      "overrideMimeType": {\n        "!type": "fn(type: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Overrides the MIME type returned by the server."\n      },\n      "send": {\n        "!type": "fn(data?: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Sends the request. If the request is asynchronous (which is the default), this method returns as soon as the request is sent. If the request is synchronous, this method doesn\'t return until the response has arrived."\n      },\n      "setRequestHeader": {\n        "!type": "fn(header: string, value: string)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Sets the value of an HTTP request header.You must call setRequestHeader() after open(), but before send()."\n      },\n      "onreadystatechange": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "A JavaScript function object that is called whenever the readyState attribute changes."\n      },\n      "readyState": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The state of the request. (0=unsent, 1=opened, 2=headers_received, 3=loading, 4=done)"\n      },\n      "response": {\n        "!type": "+Document",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The response entity body according to responseType, as an ArrayBuffer, Blob, Document, JavaScript object (for \\"json\\"), or string. This is null if the request is not complete or was not successful."\n      },\n      "responseText": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The response to the request as text, or null if the request was unsuccessful or has not yet been sent."\n      },\n      "responseType": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "Can be set to change the response type."\n      },\n      "responseXML": {\n        "!type": "+Document",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The response to the request as a DOM Document object, or null if the request was unsuccessful, has not yet been sent, or cannot be parsed as XML or HTML."\n      },\n      "status": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The status of the response to the request. This is the HTTP result code"\n      },\n      "statusText": {\n        "!type": "string",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n        "!doc": "The response string returned by the HTTP server. Unlike status, this includes the entire text of the response message (\\"200 OK\\", for example)."\n      },\n      "timeout": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest/Synchronous_and_Asynchronous_Requests",\n        "!doc": "The number of milliseconds a request can take before automatically being terminated. A value of 0 (which is the default) means there is no timeout."\n      },\n      "UNSENT": "number",\n      "OPENED": "number",\n      "HEADERS_RECEIVED": "number",\n      "LOADING": "number",\n      "DONE": "number"\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/XMLHttpRequest",\n    "!doc": "XMLHttpRequest is a JavaScript object that was designed by Microsoft and adopted by Mozilla, Apple, and Google. It\'s now being standardized in the W3C. It provides an easy way to retrieve data at a URL. Despite its name, XMLHttpRequest can be used to retrieve any type of data, not just XML, and it supports protocols other than HTTP (including file and ftp)."\n  },\n  "DOMParser": {\n    "!type": "fn()",\n    "prototype": {\n      "parseFromString": {\n        "!type": "fn(data: string, mime: string) -> +Document",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/DOMParser",\n        "!doc": "DOMParser can parse XML or HTML source stored in a string into a DOM Document. DOMParser is specified in DOM Parsing and Serialization."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/DOMParser",\n    "!doc": "DOMParser can parse XML or HTML source stored in a string into a DOM Document. DOMParser is specified in DOM Parsing and Serialization."\n  },\n  "Selection": {\n    "!type": "fn()",\n    "prototype": {\n      "anchorNode": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/anchorNode",\n        "!doc": "Returns the node in which the selection begins."\n      },\n      "anchorOffset": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/anchorOffset",\n        "!doc": "Returns the number of characters that the selection\'s anchor is offset within the anchorNode."\n      },\n      "focusNode": {\n        "!type": "+Element",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/focusNode",\n        "!doc": "Returns the node in which the selection ends."\n      },\n      "focusOffset": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/focusOffset",\n        "!doc": "Returns the number of characters that the selection\'s focus is offset within the focusNode. "\n      },\n      "isCollapsed": {\n        "!type": "bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/isCollapsed",\n        "!doc": "Returns a boolean indicating whether the selection\'s start and end points are at the same position."\n      },\n      "rangeCount": {\n        "!type": "number",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/rangeCount",\n        "!doc": "Returns the number of ranges in the selection."\n      },\n      "getRangeAt": {\n        "!type": "fn(i: number) -> +Range",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/getRangeAt",\n        "!doc": "Returns a range object representing one of the ranges currently selected."\n      },\n      "collapse": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/collapse",\n        "!doc": "Collapses the current selection to a single point. The document is not modified. If the content is focused and editable, the caret will blink there."\n      },\n      "extend": {\n        "!type": "fn(node: +Element, offset: number)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/extend",\n        "!doc": "Moves the focus of the selection to a specified point. The anchor of the selection does not move. The selection will be from the anchor to the new focus regardless of direction."\n      },\n      "collapseToStart": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/collapseToStart",\n        "!doc": "Collapses the selection to the start of the first range in the selection.  If the content of the selection is focused and editable, the caret will blink there."\n      },\n      "collapseToEnd": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/collapseToEnd",\n        "!doc": "Collapses the selection to the end of the last range in the selection.  If the content the selection is in is focused and editable, the caret will blink there."\n      },\n      "selectAllChildren": {\n        "!type": "fn(node: +Element)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/selectAllChildren",\n        "!doc": "Adds all the children of the specified node to the selection. Previous selection is lost."\n      },\n      "addRange": {\n        "!type": "fn(range: +Range)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/addRange",\n        "!doc": "Adds a Range to a Selection."\n      },\n      "removeRange": {\n        "!type": "fn(range: +Range)",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/removeRange",\n        "!doc": "Removes a range from the selection."\n      },\n      "removeAllRanges": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/removeAllRanges",\n        "!doc": "Removes all ranges from the selection, leaving the anchorNode and focusNode properties equal to null and leaving nothing selected. "\n      },\n      "deleteFromDocument": {\n        "!type": "fn()",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/deleteFromDocument",\n        "!doc": "Deletes the actual text being represented by a selection object from the document\'s DOM."\n      },\n      "containsNode": {\n        "!type": "fn(node: +Element) -> bool",\n        "!url": "https://developer.mozilla.org/en/docs/DOM/Selection/containsNode",\n        "!doc": "Indicates if the node is part of the selection."\n      }\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Selection",\n    "!doc": "Selection is the class of the object returned by window.getSelection() and other methods. It represents the text selection in the greater page, possibly spanning multiple elements, when the user drags over static text and other parts of the page. For information about text selection in an individual text editing element."\n  },\n  "console": {\n    "error": {\n      "!type": "fn(text: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/console.error",\n      "!doc": "Outputs an error message to the Web Console."\n    },\n    "info": {\n      "!type": "fn(text: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/console.info",\n      "!doc": "Outputs an informational message to the Web Console."\n    },\n    "log": {\n      "!type": "fn(text: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/console.log",\n      "!doc": "Outputs a message to the Web Console."\n    },\n    "warn": {\n      "!type": "fn(text: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/console.warn",\n      "!doc": "Outputs a warning message to the Web Console."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/console",\n    "!doc": "The console object provides access to the browser\'s debugging console. The specifics of how it works vary from browser to browser, but there is a de facto set of features that are typically provided."\n  },\n  "top": {\n    "!type": "<top>",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.top",\n    "!doc": "Returns a reference to the topmost window in the window hierarchy."\n  },\n  "parent": {\n    "!type": "<top>",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.parent",\n    "!doc": "A reference to the parent of the current window or subframe."\n  },\n  "window": {\n    "!type": "<top>",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window",\n    "!doc": "This section provides a brief reference for all of the methods, properties, and events available through the DOM window object. The window object implements the Window interface, which in turn inherits from the AbstractView interface. Some additional global functions, namespaces objects, and constructors, not typically associated with the window, but available on it, are listed in the JavaScript Reference."\n  },\n  "opener": {\n    "!type": "<top>",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.opener",\n    "!doc": "Returns a reference to the window that opened this current window."\n  },\n  "self": {\n    "!type": "<top>",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.self",\n    "!doc": "Returns an object reference to the window object. "\n  },\n  "devicePixelRatio": "number",\n  "name": {\n    "!type": "string",\n    "!url": "https://developer.mozilla.org/en/docs/JavaScript/Reference/Global_Objects/Function/name",\n    "!doc": "The name of the function."\n  },\n  "closed": {\n    "!type": "bool",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.closed",\n    "!doc": "This property indicates whether the referenced window is closed or not."\n  },\n  "pageYOffset": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollY",\n    "!doc": "Returns the number of pixels that the document has already been scrolled vertically."\n  },\n  "pageXOffset": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollX",\n    "!doc": "Returns the number of pixels that the document has already been scrolled vertically."\n  },\n  "scrollY": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollY",\n    "!doc": "Returns the number of pixels that the document has already been scrolled vertically."\n  },\n  "scrollX": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollX",\n    "!doc": "Returns the number of pixels that the document has already been scrolled vertically."\n  },\n  "screenTop": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.top",\n    "!doc": "Returns the distance in pixels from the top side of the current screen."\n  },\n  "screenLeft": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.left",\n    "!doc": "Returns the distance in pixels from the left side of the main screen to the left side of the current screen."\n  },\n  "screenY": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/event.screenY",\n    "!doc": "Returns the vertical coordinate of the event within the screen as a whole."\n  },\n  "screenX": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/event.screenX",\n    "!doc": "Returns the horizontal coordinate of the event within the screen as a whole."\n  },\n  "innerWidth": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.innerWidth",\n    "!doc": "Width (in pixels) of the browser window viewport including, if rendered, the vertical scrollbar."\n  },\n  "innerHeight": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.innerHeight",\n    "!doc": "Height (in pixels) of the browser window viewport including, if rendered, the horizontal scrollbar."\n  },\n  "outerWidth": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.outerWidth",\n    "!doc": "window.outerWidth gets the width of the outside of the browser window. It represents the width of the whole browser window including sidebar (if expanded), window chrome and window resizing borders/handles."\n  },\n  "outerHeight": {\n    "!type": "number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.outerHeight",\n    "!doc": "window.outerHeight gets the height in pixels of the whole browser window."\n  },\n  "frameElement": {\n    "!type": "+Element",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.frameElement",\n    "!doc": "Returns the element (such as <iframe> or <object>) in which the window is embedded, or null if the window is top-level."\n  },\n  "crypto": {\n    "getRandomValues": {\n      "!type": "fn([number])",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.crypto.getRandomValues",\n      "!doc": "This methods lets you get cryptographically random values."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.crypto.getRandomValues",\n    "!doc": "This methods lets you get cryptographically random values."\n  },\n  "navigator": {\n    "appName": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.appName",\n      "!doc": "Returns the name of the browser. The HTML5 specification also allows any browser to return \\"Netscape\\" here, for compatibility reasons."\n    },\n    "appVersion": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.appVersion",\n      "!doc": "Returns the version of the browser as a string. It may be either a plain version number, like \\"5.0\\", or a version number followed by more detailed information. The HTML5 specification also allows any browser to return \\"4.0\\" here, for compatibility reasons."\n    },\n    "language": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.language",\n      "!doc": "Returns a string representing the language version of the browser."\n    },\n    "platform": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.platform",\n      "!doc": "Returns a string representing the platform of the browser."\n    },\n    "plugins": {\n      "!type": "[?]",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.plugins",\n      "!doc": "Returns a PluginArray object, listing the plugins installed in the application."\n    },\n    "userAgent": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.userAgent",\n      "!doc": "Returns the user agent string for the current browser."\n    },\n    "vendor": {\n      "!type": "string",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.vendor",\n      "!doc": "Returns the name of the browser vendor for the current browser."\n    },\n    "javaEnabled": {\n      "!type": "bool",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator.javaEnabled",\n      "!doc": "This method indicates whether the current browser is Java-enabled or not."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.navigator",\n    "!doc": "Returns a reference to the navigator object, which can be queried for information about the application running the script."\n  },\n  "history": {\n    "state": {\n      "!type": "?",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "length": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "go": {\n      "!type": "fn(delta: number)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.history",\n      "!doc": "Returns a reference to the History object, which provides an interface for manipulating the browser session history (pages visited in the tab or frame that the current page is loaded in)."\n    },\n    "forward": {\n      "!type": "fn()",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "back": {\n      "!type": "fn()",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "pushState": {\n      "!type": "fn(data: ?, title: string, url?: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "replaceState": {\n      "!type": "fn(data: ?, title: string, url?: string)",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n      "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Manipulating_the_browser_history",\n    "!doc": "The DOM window object provides access to the browser\'s history through the history object. It exposes useful methods and properties that let you move back and forth through the user\'s history, as well as -- starting with HTML5 -- manipulate the contents of the history stack."\n  },\n  "screen": {\n    "availWidth": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.availWidth",\n      "!doc": "Returns the amount of horizontal space in pixels available to the window."\n    },\n    "availHeight": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.availHeight",\n      "!doc": "Returns the amount of vertical space available to the window on the screen."\n    },\n    "availTop": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.availTop",\n      "!doc": "Specifies the y-coordinate of the first pixel that is not allocated to permanent or semipermanent user interface features."\n    },\n    "availLeft": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.availLeft",\n      "!doc": "Returns the first available pixel available from the left side of the screen."\n    },\n    "pixelDepth": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.pixelDepth",\n      "!doc": "Returns the bit depth of the screen."\n    },\n    "colorDepth": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.colorDepth",\n      "!doc": "Returns the color depth of the screen."\n    },\n    "width": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.width",\n      "!doc": "Returns the width of the screen."\n    },\n    "height": {\n      "!type": "number",\n      "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen.height",\n      "!doc": "Returns the height of the screen in pixels."\n    },\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.screen",\n    "!doc": "Returns a reference to the screen object associated with the window."\n  },\n  "postMessage": {\n    "!type": "fn(message: string, targetOrigin: string)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.postMessage",\n    "!doc": "window.postMessage, when called, causes a MessageEvent to be dispatched at the target window when any pending script that must be executed completes (e.g. remaining event handlers if window.postMessage is called from an event handler, previously-set pending timeouts, etc.). The MessageEvent has the type message, a data property which is set to the value of the first argument provided to window.postMessage, an origin property corresponding to the origin of the main document in the window calling window.postMessage at the time window.postMessage was called, and a source property which is the window from which window.postMessage is called. (Other standard properties of events are present with their expected values.)"\n  },\n  "close": {\n    "!type": "fn()",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.close",\n    "!doc": "Closes the current window, or a referenced window."\n  },\n  "blur": {\n    "!type": "fn()",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.blur",\n    "!doc": "The blur method removes keyboard focus from the current element."\n  },\n  "focus": {\n    "!type": "fn()",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.focus",\n    "!doc": "Sets focus on the specified element, if it can be focused."\n  },\n  "onload": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onload",\n    "!doc": "An event handler for the load event of a window."\n  },\n  "onunload": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onunload",\n    "!doc": "The unload event is raised when the window is unloading its content and resources. The resources removal is processed after the unload event occurs."\n  },\n  "onscroll": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onscroll",\n    "!doc": "Specifies the function to be called when the window is scrolled."\n  },\n  "onresize": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onresize",\n    "!doc": "An event handler for the resize event on the window."\n  },\n  "ononline": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/document.ononline",\n    "!doc": ",fgh s dgkljgsdfl dfjg sdlgj sdlg sdlfj dlg jkdfkj dfjgdfkglsdfjsdlfkgj hdflkg hdlkfjgh dfkjgh"\n  },\n  "onoffline": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/Online_and_offline_events",\n    "!doc": "Some browsers implement Online/Offline events from the WHATWG Web Applications 1.0 specification."\n  },\n  "onmousewheel": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/DOM_event_reference/mousewheel",\n    "!doc": "The DOM mousewheel event is fired asynchronously when mouse wheel or similar device is operated. It\'s represented by the MouseWheelEvent interface."\n  },\n  "onmouseup": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onmouseup",\n    "!doc": "An event handler for the mouseup event on the window."\n  },\n  "onmouseover": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmouseover",\n    "!doc": "The onmouseover property returns the onMouseOver event handler code on the current element."\n  },\n  "onmouseout": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmouseout",\n    "!doc": "The onmouseout property returns the onMouseOut event handler code on the current element."\n  },\n  "onmousemove": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onmousemove",\n    "!doc": "The onmousemove property returns the mousemove event handler code on the current element."\n  },\n  "onmousedown": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onmousedown",\n    "!doc": "An event handler for the mousedown event on the window."\n  },\n  "onclick": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onclick",\n    "!doc": "The onclick property returns the onClick event handler code on the current element."\n  },\n  "ondblclick": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.ondblclick",\n    "!doc": "The ondblclick property returns the onDblClick event handler code on the current element."\n  },\n  "onmessage": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/Worker",\n    "!doc": "Dedicated Web Workers provide a simple means for web content to run scripts in background threads.  Once created, a worker can send messages to the spawning task by posting messages to an event handler specified by the creator."\n  },\n  "onkeyup": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onkeyup",\n    "!doc": "The onkeyup property returns the onKeyUp event handler code for the current element."\n  },\n  "onkeypress": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onkeypress",\n    "!doc": "The onkeypress property sets and returns the onKeyPress event handler code for the current element."\n  },\n  "onkeydown": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onkeydown",\n    "!doc": "An event handler for the keydown event on the window."\n  },\n  "oninput": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/DOM_event_reference/input",\n    "!doc": "The DOM input event is fired synchronously when the value of an <input> or <textarea> element is changed. Additionally, it\'s also fired on contenteditable editors when its contents are changed. In this case, the event target is the editing host element. If there are two or more elements which have contenteditable as true, \\"editing host\\" is the nearest ancestor element whose parent isn\'t editable. Similarly, it\'s also fired on root element of designMode editors."\n  },\n  "onpopstate": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onpopstate",\n    "!doc": "An event handler for the popstate event on the window."\n  },\n  "onhashchange": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onhashchange",\n    "!doc": "The hashchange event fires when a window\'s hash changes."\n  },\n  "onfocus": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onfocus",\n    "!doc": "The onfocus property returns the onFocus event handler code on the current element."\n  },\n  "onblur": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onblur",\n    "!doc": "The onblur property returns the onBlur event handler code, if any, that exists on the current element."\n  },\n  "onerror": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onerror",\n    "!doc": "An event handler for runtime script errors."\n  },\n  "ondrop": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/drop",\n    "!doc": "The drop event is fired when an element or text selection is dropped on a valid drop target."\n  },\n  "ondragstart": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/dragstart",\n    "!doc": "The dragstart event is fired when the user starts dragging an element or text selection."\n  },\n  "ondragover": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/dragover",\n    "!doc": "The dragover event is fired when an element or text selection is being dragged over a valid drop target (every few hundred milliseconds)."\n  },\n  "ondragleave": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/dragleave",\n    "!doc": "The dragleave event is fired when a dragged element or text selection leaves a valid drop target."\n  },\n  "ondragenter": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/dragenter",\n    "!doc": "The dragenter event is fired when a dragged element or text selection enters a valid drop target."\n  },\n  "ondragend": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/dragend",\n    "!doc": "The dragend event is fired when a drag operation is being ended (by releasing a mouse button or hitting the escape key)."\n  },\n  "ondrag": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en-US/docs/DOM/Mozilla_event_reference/drag",\n    "!doc": "The drag event is fired when an element or text selection is being dragged (every few hundred milliseconds)."\n  },\n  "oncontextmenu": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.oncontextmenu",\n    "!doc": "An event handler property for right-click events on the window. Unless the default behavior is prevented, the browser context menu will activate (though IE8 has a bug with this and will not activate the context menu if a contextmenu event handler is defined). Note that this event will occur with any non-disabled right-click event and does not depend on an element possessing the \\"contextmenu\\" attribute."\n  },\n  "onchange": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/element.onchange",\n    "!doc": "The onchange property sets and returns the onChange event handler code for the current element."\n  },\n  "onbeforeunload": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onbeforeunload",\n    "!doc": "An event that fires when a window is about to unload its resources. The document is still visible and the event is still cancelable."\n  },\n  "onabort": {\n    "!type": "?",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.onabort",\n    "!doc": "An event handler for abort events sent to the window."\n  },\n  "getSelection": {\n    "!type": "fn() -> +Selection",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.getSelection",\n    "!doc": "Returns a selection object representing the range of text selected by the user. "\n  },\n  "alert": {\n    "!type": "fn(message: string)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.alert",\n    "!doc": "Display an alert dialog with the specified content and an OK button."\n  },\n  "confirm": {\n    "!type": "fn(message: string) -> bool",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.confirm",\n    "!doc": "Displays a modal dialog with a message and two buttons, OK and Cancel."\n  },\n  "prompt": {\n    "!type": "fn(message: string, value: string) -> string",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.prompt",\n    "!doc": "Displays a dialog with a message prompting the user to input some text."\n  },\n  "scrollBy": {\n    "!type": "fn(x: number, y: number)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollBy",\n    "!doc": "Scrolls the document in the window by the given amount."\n  },\n  "scrollTo": {\n    "!type": "fn(x: number, y: number)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scrollTo",\n    "!doc": "Scrolls to a particular set of coordinates in the document."\n  },\n  "scroll": {\n    "!type": "fn(x: number, y: number)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.scroll",\n    "!doc": "Scrolls the window to a particular place in the document."\n  },\n  "setTimeout": {\n    "!type": "fn(f: fn(), ms: number) -> number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.setTimeout",\n    "!doc": "Calls a function or executes a code snippet after specified delay."\n  },\n  "clearTimeout": {\n    "!type": "fn(timeout: number)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.clearTimeout",\n    "!doc": "Clears the delay set by window.setTimeout()."\n  },\n  "setInterval": {\n    "!type": "fn(f: fn(), ms: number) -> number",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.setInterval",\n    "!doc": "Calls a function or executes a code snippet repeatedly, with a fixed time delay between each call to that function."\n  },\n  "clearInterval": {\n    "!type": "fn(interval: number)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.clearInterval",\n    "!doc": "Cancels repeated action which was set up using setInterval."\n  },\n  "atob": {\n    "!type": "fn(encoded: string) -> string",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.atob",\n    "!doc": "Decodes a string of data which has been encoded using base-64 encoding."\n  },\n  "btoa": {\n    "!type": "fn(data: string) -> string",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.btoa",\n    "!doc": "Creates a base-64 encoded ASCII string from a string of binary data."\n  },\n  "addEventListener": {\n    "!type": "fn(type: string, listener: fn(e: +Event), capture: bool)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.addEventListener",\n    "!doc": "Registers a single event listener on a single target. The event target may be a single element in a document, the document itself, a window, or an XMLHttpRequest."\n  },\n  "removeEventListener": {\n    "!type": "fn(type: string, listener: fn(), capture: bool)",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.removeEventListener",\n    "!doc": "Allows the removal of event listeners from the event target."\n  },\n  "dispatchEvent": {\n    "!type": "fn(event: +Event) -> bool",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/EventTarget.dispatchEvent",\n    "!doc": "Dispatches an event into the event system. The event is subject to the same capturing and bubbling behavior as directly dispatched events."\n  },\n  "getComputedStyle": {\n    "!type": "fn(node: +Element, pseudo?: string) -> Element.prototype.style",\n    "!url": "https://developer.mozilla.org/en/docs/DOM/window.getComputedStyle",\n    "!doc": "Gives the final used values of all the CSS properties of an element."\n  }\n}\n';});

define('text!thirdparty/tern/defs/jquery.json',[],function () { return '{\n  "!name": "jQuery",\n  "!define": {\n    "offset": {\n      "top": "number",\n      "left": "number"\n    },\n    "keyvalue": {\n      "name": "string",\n      "value": "string"\n    }\n  },\n  "jQuery": {\n    "!type": "fn(selector: string, context: frameElement) -> jQuery.fn",\n    "!url": "http://api.jquery.com/jquery/",\n    "!doc": "Return a collection of matched elements either found in the DOM based on passed argument(s) or created by passing an HTML string.",\n    "fn": {\n      "add": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/add/",\n        "!doc": "Add elements to the set of matched elements."\n      },\n      "addBack": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/addBack/",\n        "!doc": "Add the previous set of elements on the stack to the current set, optionally filtered by a selector."\n      },\n      "addClass": {\n        "!type": "fn(className: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/addClass/",\n        "!doc": "Adds the specified class(es) to each of the set of matched elements."\n      },\n      "after": {\n        "!type": "fn(content: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/after/",\n        "!doc": "Insert content, specified by the parameter, after each element in the set of matched elements."\n      },\n      "ajaxComplete": {\n        "!type": "fn(handler: fn(event: +jQuery.Event, req: +XMLHttpRequest)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxComplete/",\n        "!doc": "Register a handler to be called when Ajax requests complete. This is an AjaxEvent."\n      },\n      "ajaxError": {\n        "!type": "fn(handler: fn(event: +jQuery.Event, req: +XMLHttpRequest)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxError/",\n        "!doc": "Register a handler to be called when Ajax requests complete with an error. This is an Ajax Event."\n      },\n      "ajaxSend": {\n        "!type": "fn(handler: fn(event: +jQuery.Event, req: +XMLHttpRequest)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxSend/",\n        "!doc": "Attach a function to be executed before an Ajax request is sent. This is an Ajax Event."\n      },\n      "ajaxStart": {\n        "!type": "fn(handler: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxStart/",\n        "!doc": "Register a handler to be called when the first Ajax request begins. This is an Ajax Event."\n      },\n      "ajaxStop": {\n        "!type": "fn(handler: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxStop/",\n        "!doc": "Register a handler to be called when all Ajax requests have completed. This is an Ajax Event."\n      },\n      "ajaxSuccess": {\n        "!type": "fn(handler: fn(event: +jQuery.Event, req: +XMLHttpRequest)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ajaxSuccess/",\n        "!doc": ""\n      },\n      "andSelf": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/andSelf/",\n        "!doc": "Attach a function to be executed whenever an Ajax request completes successfully. This is an Ajax Event."\n      },\n      "animate": {\n        "!type": "fn(properties: ?, duration?: number, easing?: string, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/animate/",\n        "!doc": "Perform a custom animation of a set of CSS properties."\n      },\n      "append": {\n        "!type": "fn(content: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/append/",\n        "!doc": "Insert content, specified by the parameter, to the end of each element in the set of matched elements."\n      },\n      "appendTo": {\n        "!type": "fn(target: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/appendTo/",\n        "!doc": "Insert every element in the set of matched elements to the end of the target."\n      },\n      "attr": {\n        "!type": "fn(name: string, value?: string) -> string",\n        "!url": "http://api.jquery.com/attr/",\n        "!doc": "Get the value of an attribute for the first element in the set of matched elements or set one or more attributes for every matched element."\n      },\n      "before": {\n        "!type": "fn(content: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/before/",\n        "!doc": "Insert content, specified by the parameter, before each element in the set of matched elements."\n      },\n      "bind": {\n        "!type": "fn(eventType: string, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/bind/",\n        "!doc": "Attach a handler to an event for the elements."\n      },\n      "blur": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/blur/",\n        "!doc": "Bind an event handler to the \'blur\' JavaScript event, or trigger that event on an element."\n      },\n      "change": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/change/",\n        "!doc": "Bind an event handler to the \'change\' JavaScript event, or trigger that event on an element."\n      },\n      "children": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/children/",\n        "!doc": "Get the children of each element in the set of matched elements, optionally filtered by a selector."\n      },\n      "click": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/click/",\n        "!doc": "Bind an event handler to the \'click\' JavaScript event, or trigger that event on an element."\n      },\n      "clone": {\n        "!type": "fn(dataAndEvents?: bool, deep?: bool) -> jQuery.fn",\n        "!url": "http://api.jquery.com/clone/",\n        "!doc": "Create a deep copy of the set of matched elements."\n      },\n      "closest": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/closest/",\n        "!doc": "For each element in the set, get the first element that matches the selector by testing the element itself and traversing up through its ancestors in the DOM tree."\n      },\n      "contents": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/contents/",\n        "!doc": "Get the children of each element in the set of matched elements, including text and comment nodes."\n      },\n      "context": {\n        "!type": "fn() -> +Element",\n        "!url": "http://api.jquery.com/context/",\n        "!doc": "The DOM node context originally passed to jQuery(); if none was passed then context will likely be the document."\n      },\n      "css": {\n        "!type": "fn(name: string, value?: string) -> string",\n        "!url": "http://api.jquery.com/css/",\n        "!doc": "Get the value of a style property for the first element in the set of matched elements or set one or more CSS properties for every matched element."\n      },\n      "data": {\n        "!type": "fn(key: string, value?: ?) -> !1",\n        "!url": "http://api.jquery.com/data/",\n        "!doc": "Store arbitrary data associated with the matched elements or return the value at the named data store for the first element in the set of matched elements."\n      },\n      "dblclick": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/dblclick/",\n        "!doc": "Bind an event handler to the \'dblclick\' JavaScript event, or trigger that event on an element."\n      },\n      "delay": {\n        "!type": "fn(duration: number, queue?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/delay/",\n        "!doc": "Set a timer to delay execution of subsequent items in the queue."\n      },\n      "delegate": {\n        "!type": "fn(selector: string, eventType: string, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/delegate/",\n        "!doc": "Attach a handler to one or more events for all elements that match the selector, now or in the future, based on a specific set of root elements."\n      },\n      "dequeue": {\n        "!type": "fn(queue?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/dequeue/",\n        "!doc": "Execute the next function on the queue for the matched elements."\n      },\n      "detach": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/detach/",\n        "!doc": "Remove the set of matched elements from the DOM."\n      },\n      "die": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/die/",\n        "!doc": "Remove event handlers previously attached using .live() from the elements."\n      },\n      "each": {\n        "!type": "fn(callback: fn(i: number, element: +Element)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/each/",\n        "!doc": "Iterate over a jQuery object, executing a function for each matched element."\n      },\n      "empty": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/empty/",\n        "!doc": "Remove all child nodes of the set of matched elements from the DOM."\n      },\n      "end": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/end/",\n        "!doc": "End the most recent filtering operation in the current chain and return the set of matched elements to its previous state."\n      },\n      "eq": {\n        "!type": "fn(i: number) -> jQuery.fn",\n        "!url": "http://api.jquery.com/eq/",\n        "!doc": "Reduce the set of matched elements to the one at the specified index."\n      },\n      "error": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/error/",\n        "!doc": "Bind an event handler to the \'error\' JavaScript event."\n      },\n      "fadeIn": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/fadeIn/",\n        "!doc": "Display the matched elements by fading them to opaque."\n      },\n      "fadeOut": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/fadeOut/",\n        "!doc": "Hide the matched elements by fading them to transparent."\n      },\n      "fadeTo": {\n        "!type": "fn(duration: number, opacity: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/fadeTo/",\n        "!doc": "Adjust the opacity of the matched elements."\n      },\n      "fadeToggle": {\n        "!type": "fn(duration?: number, easing?: string, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/fadeToggle/",\n        "!doc": "Display or hide the matched elements by animating their opacity."\n      },\n      "filter": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/filter/",\n        "!doc": "Reduce the set of matched elements to those that match the selector or pass the function\'s test."\n      },\n      "find": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/find/",\n        "!doc": "Get the descendants of each element in the current set of matched elements, filtered by a selector, jQuery object, or element."\n      },\n      "finish": {\n        "!type": "fn(queue?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/finish/",\n        "!doc": "Stop the currently-running animation, remove all queued animations, and complete all animations for the matched elements."\n      },\n      "first": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/first/",\n        "!doc": "Reduce the set of matched elements to the first in the set."\n      },\n      "focusin": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/focusin/",\n        "!doc": "Bind an event handler to the \'focusin\' event."\n      },\n      "focusout": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/focusout/",\n        "!doc": "Bind an event handler to the \'focusout\' JavaScript event."\n      },\n      "get": {\n        "!type": "fn(i: number) -> +Element",\n        "!url": "http://api.jquery.com/get/",\n        "!doc": "Retrieve the DOM elements matched by the jQuery object."\n      },\n      "has": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/has/",\n        "!doc": "Reduce the set of matched elements to those that have a descendant that matches the selector or DOM element."\n      },\n      "hasClass": {\n        "!type": "fn(className: string) -> bool",\n        "!url": "http://api.jquery.com/hasClass/",\n        "!doc": "Determine whether any of the matched elements are assigned the given class."\n      },\n      "height": {\n        "!type": "fn() -> number",\n        "!url": "http://api.jquery.com/height/",\n        "!doc": "Get the current computed height for the first element in the set of matched elements or set the height of every matched element."\n      },\n      "hide": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/hide/",\n        "!doc": "Hide the matched elements."\n      },\n      "hover": {\n        "!type": "fn(fnOver: fn(+jQuery.Event), fnOut?: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/hover/",\n        "!doc": "Bind one or two handlers to the matched elements, to be executed when the mouse pointer enters and leaves the elements."\n      },\n      "html": {\n        "!type": "fn() -> string",\n        "!url": "http://api.jquery.com/html/",\n        "!doc": "Get the HTML contents of the first element in the set of matched elements or set the HTML contents of every matched element."\n      },\n      "index": {\n        "!type": "fn(selector?: string) -> number",\n        "!url": "http://api.jquery.com/index/",\n        "!doc": "Search for a given element from among the matched elements."\n      },\n      "innerHeight": {\n        "!type": "fn() -> number",\n        "!url": "http://api.jquery.com/innerHeight/",\n        "!doc": "Get the current computed height for the first element in the set of matched elements, including padding but not border."\n      },\n      "innerWidth": {\n        "!type": "fn() -> number",\n        "!url": "http://api.jquery.com/innerWidth/",\n        "!doc": "Get the current computed width for the first element in the set of matched elements, including padding but not border."\n      },\n      "insertAfter": {\n        "!type": "fn(target: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/insertAfter/",\n        "!doc": "Insert every element in the set of matched elements after the target."\n      },\n      "insertBefore": {\n        "!type": "fn(target: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/insertBefore/",\n        "!doc": "Insert every element in the set of matched elements before the target."\n      },\n      "is": {\n        "!type": "fn(selector: ?) -> bool",\n        "!url": "http://api.jquery.com/is/",\n        "!doc": "Check the current matched set of elements against a selector, element, or jQuery object and return true if at least one of these elements matches the given arguments."\n      },\n      "jquery": {\n        "!type": "string",\n        "!url": "http://api.jquery.com/jquery-2/",\n        "!doc": "A string containing the jQuery version number."\n      },\n      "keydown": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/keydown/",\n        "!doc": "Bind an event handler to the \'keydown\' JavaScript event, or trigger that event on an element."\n      },\n      "keypress": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/keypress/",\n        "!doc": "Bind an event handler to the \'keypress\' JavaScript event, or trigger that event on an element."\n      },\n      "keyup": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/keyup/",\n        "!doc": "Bind an event handler to the \'keyup\' JavaScript event, or trigger that event on an element."\n      },\n      "last": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/last/",\n        "!doc": "Reduce the set of matched elements to the final one in the set."\n      },\n      "length": {\n        "!type": "number",\n        "!url": "http://api.jquery.com/length/",\n        "!doc": "The number of elements in the jQuery object."\n      },\n      "live": {\n        "!type": "fn(selector: string, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/live/",\n        "!doc": "Attach an event handler for all elements which match the current selector, now and in the future."\n      },\n      "load": {\n        "!type": "fn(handler: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/load/",\n        "!doc": "Load data from the server and place the returned HTML into the matched element."\n      },\n      "map": {\n        "!type": "fn(callback: fn(i: number, element: +Element)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/map/",\n        "!doc": "Pass each element in the current matched set through a function, producing a new jQuery object containing the return values."\n      },\n      "mousedown": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mousedown/",\n        "!doc": "Bind an event handler to the \'mousedown\' JavaScript event, or trigger that event on an element."\n      },\n      "mouseenter": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mouseenter/",\n        "!doc": "Bind an event handler to be fired when the mouse enters an element, or trigger that handler on an element."\n      },\n      "mouseleave": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mouseleave/",\n        "!doc": "Bind an event handler to be fired when the mouse leaves an element, or trigger that handler on an element."\n      },\n      "mousemove": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mousemouve/",\n        "!doc": "Bind an event handler to the \'mousemove\' JavaScript event, or trigger that event on an element."\n      },\n      "mouseout": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mouseout/",\n        "!doc": "Bind an event handler to the \'mouseout\' JavaScript event, or trigger that event on an element."\n      },\n      "mouseover": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mouseover/",\n        "!doc": "Bind an event handler to the \'mouseover\' JavaScript event, or trigger that event on an element."\n      },\n      "mouseup": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/mouseup/",\n        "!doc": "Bind an event handler to the \'mouseup\' JavaScript event, or trigger that event on an element."\n      },\n      "next": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/next/",\n        "!doc": "Get the immediately following sibling of each element in the set of matched elements. If a selector is provided, it retrieves the next sibling only if it matches that selector."\n      },\n      "nextAll": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/nextAll/",\n        "!doc": "Get all following siblings of each element in the set of matched elements, optionally filtered by a selector."\n      },\n      "nextUntil": {\n        "!type": "fn(selector?: string, filter?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/nextUntil/",\n        "!doc": "Get all following siblings of each element up to but not including the element matched by the selector, DOM node, or jQuery object passed."\n      },\n      "not": {\n        "!type": "fn(selector: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/not/",\n        "!doc": "Remove elements from the set of matched elements."\n      },\n      "off": {\n        "!type": "fn(events: string, selector?: string, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/off/",\n        "!doc": "Remove an event handler."\n      },\n      "offset": {\n        "!type": "fn() -> offset",\n        "!url": "http://api.jquery.com/offset/",\n        "!doc": "Get the current coordinates of the first element, or set the coordinates of every element, in the set of matched elements, relative to the document."\n      },\n      "offsetParent": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/offsetParent/",\n        "!doc": "Get the closest ancestor element that is positioned."\n      },\n      "on": {\n        "!type": "fn(events: string, selector?: string, data?: ?, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/on/",\n        "!doc": "Attach an event handler function for one or more events to the selected elements."\n      },\n      "one": {\n        "!type": "fn(events: string, data?: ?, handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/one/",\n        "!doc": "Attach a handler to an event for the elements. The handler is executed at most once per element."\n      },\n      "outerHeight": {\n        "!type": "fn(includeMargin?: bool) -> number",\n        "!url": "http://api.jquery.com/outerHeight/",\n        "!doc": "Get the current computed height for the first element in the set of matched elements, including padding, border, and optionally margin. Returns an integer (without \'px\') representation of the value or null if called on an empty set of elements."\n      },\n      "outerWidth": {\n        "!type": "fn(includeMargin?: bool) -> number",\n        "!url": "http://api.jquery.com/outerWidth/",\n        "!doc": "Get the current computed width for the first element in the set of matched elements, including padding and border."\n      },\n      "parent": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/parent/",\n        "!doc": "Get the parent of each element in the current set of matched elements, optionally filtered by a selector."\n      },\n      "parents": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/parents/",\n        "!doc": "Get the ancestors of each element in the current set of matched elements, optionally filtered by a selector."\n      },\n      "parentsUntil": {\n        "!type": "fn(selector?: string, filter?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/parentsUntil/",\n        "!doc": "Get the ancestors of each element in the current set of matched elements, up to but not including the element matched by the selector, DOM node, or jQuery object."\n      },\n      "position": {\n        "!type": "fn() -> offset",\n        "!url": "http://api.jquery.com/position/",\n        "!doc": "Get the current coordinates of the first element in the set of matched elements, relative to the offset parent."\n      },\n      "prepend": {\n        "!type": "fn(content: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/prepend/",\n        "!doc": "Insert content, specified by the parameter, to the beginning of each element in the set of matched elements."\n      },\n      "prependTo": {\n        "!type": "fn(target: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/prependTo/",\n        "!doc": "Insert every element in the set of matched elements to the beginning of the target."\n      },\n      "prev": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/prev/",\n        "!doc": "Get the immediately preceding sibling of each element in the set of matched elements, optionally filtered by a selector."\n      },\n      "prevAll": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/prevAll/",\n        "!doc": "Get all preceding siblings of each element in the set of matched elements, optionally filtered by a selector."\n      },\n      "prevUntil": {\n        "!type": "fn(selector?: string, filter?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/prevUntil/",\n        "!doc": "Get all preceding siblings of each element up to but not including the element matched by the selector, DOM node, or jQuery object."\n      },\n      "promise": {\n        "!type": "fn(type?: string, target: ?) -> +jQuery.Deferred",\n        "!url": "http://api.jquery.com/promise/",\n        "!doc": "Return a Promise object to observe when all actions of a certain type bound to the collection, queued or not, have finished."\n      },\n      "prop": {\n        "!type": "fn(name: string, value?: string) -> string",\n        "!url": "http://api.jquery.com/prop/",\n        "!doc": "Get the value of a property for the first element in the set of matched elements or set one or more properties for every matched element."\n      },\n      "pushStack": {\n        "!type": "fn(elements: [+Element]) -> jQuery.fn",\n        "!url": "http://api.jquery.com/pushStack/",\n        "!doc": "Add a collection of DOM elements onto the jQuery stack."\n      },\n      "queue": {\n        "!type": "fn(queue?: string) -> [?]",\n        "!url": "http://api.jquery.com/queue/",\n        "!doc": "Show or manipulate the queue of functions to be executed on the matched elements."\n      },\n      "ready": {\n        "!type": "fn(fn: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/ready/",\n        "!doc": "Specify a function to execute when the DOM is fully loaded."\n      },\n      "remove": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/remove/",\n        "!doc": "Remove the set of matched elements from the DOM."\n      },\n      "removeAttr": {\n        "!type": "fn(attrName: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/removeAttr/",\n        "!doc": "Remove an attribute from each element in the set of matched elements."\n      },\n      "removeClass": {\n        "!type": "fn(className?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/removeClass/",\n        "!doc": "Remove a single class, multiple classes, or all classes from each element in the set of matched elements."\n      },\n      "removeData": {\n        "!type": "fn(name?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/removeData/",\n        "!doc": "Remove a previously-stored piece of data."\n      },\n      "removeProp": {\n        "!type": "fn(propName: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/removeProp/",\n        "!doc": "Remove a property for the set of matched elements."\n      },\n      "replaceAll": {\n        "!type": "fn(target: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/replaceAll/",\n        "!doc": "Replace each target element with the set of matched elements."\n      },\n      "replaceWith": {\n        "!type": "fn(newContent: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/replaceWith/",\n        "!doc": "Replace each element in the set of matched elements with the provided new content and return the set of elements that was removed."\n      },\n      "resize": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/resize/",\n        "!doc": "Bind an event handler to the \'resize\' JavaScript event, or trigger that event on an element."\n      },\n      "scroll": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/scroll/",\n        "!doc": "Bind an event handler to the \'scroll\' JavaScript event, or trigger that event on an element."\n      },\n      "scrollLeft": {\n        "!type": "number",\n        "!url": "http://api.jquery.com/scrollLeft/",\n        "!doc": "Get the current horizontal position of the scroll bar for the first element in the set of matched elements or set the horizontal position of the scroll bar for every matched element."\n      },\n      "scrollTop": {\n        "!type": "number",\n        "!url": "http://api.jquery.com/scrollTop/",\n        "!doc": "Get the current vertical position of the scroll bar for the first element in the set of matched elements or set the vertical position of the scroll bar for every matched element."\n      },\n      "select": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/select/",\n        "!doc": "Bind an event handler to the \'select\' JavaScript event, or trigger that event on an element."\n      },\n      "selector": {\n        "!type": "string",\n        "!url": "http://api.jquery.com/selector/",\n        "!doc": "A selector representing selector passed to jQuery(), if any, when creating the original set."\n      },\n      "serialize": {\n        "!type": "fn() -> string",\n        "!url": "http://api.jquery.com/serialize/",\n        "!doc": "Encode a set of form elements as a string for submission."\n      },\n      "serializeArray": {\n        "!type": "fn() -> [keyvalue]",\n        "!url": "http://api.jquery.com/serializeArray/",\n        "!doc": "Encode a set of form elements as an array of names and values."\n      },\n      "show": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/show/",\n        "!doc": "Display the matched elements."\n      },\n      "siblings": {\n        "!type": "fn(selector?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/siblings/",\n        "!doc": "Get the siblings of each element in the set of matched elements, optionally filtered by a selector."\n      },\n      "size": {\n        "!type": "fn() -> number",\n        "!url": "http://api.jquery.com/size/",\n        "!doc": "Return the number of elements in the jQuery object."\n      },\n      "slice": {\n        "!type": "fn(start: number, end?: number) -> jQuery.fn",\n        "!url": "http://api.jquery.com/slice/",\n        "!doc": "Reduce the set of matched elements to a subset specified by a range of indices."\n      },\n      "slideDown": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/slideDown/",\n        "!doc": "Display the matched elements with a sliding motion."\n      },\n      "slideToggle": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/slideToggle/",\n        "!doc": "Display or hide the matched elements with a sliding motion."\n      },\n      "slideUp": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/slideUp/",\n        "!doc": "Hide the matched elements with a sliding motion."\n      },\n      "stop": {\n        "!type": "fn(clearQueue?: bool, jumpToEnd?: bool) -> jQuery.fn",\n        "!url": "http://api.jquery.com/stop/",\n        "!doc": "Stop the currently-running animation on the matched elements."\n      },\n      "submit": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/submit/",\n        "!doc": "Bind an event handler to the \'submit\' JavaScript event, or trigger that event on an element."\n      },\n      "text": {\n        "!type": "fn() -> string",\n        "!url": "http://api.jquery.com/text/",\n        "!doc": "Get the combined text contents of each element in the set of matched elements, including their descendants, or set the text contents of the matched elements."\n      },\n      "toArray": {\n        "!type": "fn() -> [+Element]",\n        "!url": "http://api.jquery.com/toArray/",\n        "!doc": "Retrieve all the DOM elements contained in the jQuery set, as an array."\n      },\n      "toggle": {\n        "!type": "fn(duration?: number, complete?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/toggle/",\n        "!doc": "Display or hide the matched elements."\n      },\n      "toggleClass": {\n        "!type": "fn(className: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/toggleClass/",\n        "!doc": "Add or remove one or more classes from each element in the set of matched elements, depending on either the class\'s presence or the value of the switch argument."\n      },\n      "trigger": {\n        "!type": "fn(eventType: string, params: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/trigger/",\n        "!doc": "Execute all handlers and behaviors attached to the matched elements for the given event type."\n      },\n      "triggerHandler": {\n        "!type": "fn(eventType: string, params: ?) -> ?",\n        "!url": "http://api.jquery.com/triggerHandler/",\n        "!doc": "Execute all handlers attached to an element for an event."\n      },\n      "unbind": {\n        "!type": "fn(eventType?: string, handler?: fn()) -> jQuery.fn",\n        "!url": "http://api.jquery.com/unbind/",\n        "!doc": "Remove a previously-attached event handler from the elements."\n      },\n      "undelegate": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/undelegate/",\n        "!doc": "Remove a handler from the event for all elements which match the current selector, based upon a specific set of root elements."\n      },\n      "unload": {\n        "!type": "fn(handler: fn(+jQuery.Event)) -> jQuery.fn",\n        "!url": "http://api.jquery.com/unload/",\n        "!doc": "Bind an event handler to the \'unload\' JavaScript event."\n      },\n      "unwrap": {\n        "!type": "fn() -> jQuery.fn",\n        "!url": "http://api.jquery.com/unwrap/",\n        "!doc": "Remove the parents of the set of matched elements from the DOM, leaving the matched elements in their place."\n      },\n      "val": {\n        "!type": "fn() -> string",\n        "!url": "http://api.jquery.com/val/",\n        "!doc": "Get the current value of the first element in the set of matched elements or set the value of every matched element."\n      },\n      "width": {\n        "!type": "fn() -> number",\n        "!url": "http://api.jquery.com/width/",\n        "!doc": "Get the current computed width for the first element in the set of matched elements or set the width of every matched element."\n      },\n      "wrap": {\n        "!type": "fn(wrappingElement: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/wrap/",\n        "!doc": "Wrap an HTML structure around each element in the set of matched elements."\n      },\n      "wrapAll": {\n        "!type": "fn(wrappingElement: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/wrapAll/",\n        "!doc": "Wrap an HTML structure around all elements in the set of matched elements."\n      },\n      "wrapInner": {\n        "!type": "fn(wrappingElement: ?) -> jQuery.fn",\n        "!url": "http://api.jquery.com/wrapInner/",\n        "!doc": "Wrap an HTML structure around the content of each element in the set of matched elements."\n      },\n\n      "slice": {\n        "!type": "fn(start: number, end: number) -> jQuery.fn",\n        "!url": "http://api.jquery.com/slice/",\n        "!doc": "Reduce the set of matched elements to a subset specified by a range of indices."\n      },\n      "push": {\n        "!type": "Array.prototype.push",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/push",\n        "!doc": "Mutates an array by appending the given elements and returning the new length of the array."\n      },\n      "sort": {\n        "!type": "Array.prototype.sort",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/sort",\n        "!doc": "Sorts the elements of an array in place and returns the array."\n      },\n      "splice": {\n        "!type": "Array.prototype.splice",\n        "!url": "https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/Array/splice",\n        "!doc": "Changes the content of an array, adding new elements while removing old elements."\n      }\n    },\n    "ajax": {\n      "!type": "fn(url: string, settings: ?) -> +XMLHttpRequest",\n      "!url": "http://api.jquery.com/jquery.ajax/",\n      "!doc": "Perform an asynchronous HTTP (Ajax) request."\n    },\n    "ajaxPrefilter": {\n      "!type": "fn(dataTypes?: string, handler: fn(options: ?, originalOptions: ?, req: +XMLHttpRequest))",\n      "!url": "http://api.jquery.com/jquery.ajaxPrefilter/",\n      "!doc": "Handle custom Ajax options or modify existing options before each request is sent and before they are processed by $.ajax()."\n    },\n    "ajaxSetup": {\n      "!type": "fn(options: ?)",\n      "!url": "http://api.jquery.com/jquery.ajaxSetup/",\n      "!doc": "Set default values for future Ajax requests. Its use is not recommended."\n    },\n    "ajaxTransport": {\n      "!type": "fn(dataType: string, handler: fn(options: ?, originalOptions: ?, req: +XMLHttpRequest))",\n      "!url": "http://api.jquery.com/jquery.ajaxTransport/",\n      "!doc": "Creates an object that handles the actual transmission of Ajax data."\n    },\n    "Callbacks": {\n      "!type": "fn(flags: string) -> +jQuery.Callbacks",\n      "!url": "http://api.jquery.com/jquery.Callbacks/",\n      "!doc": "A multi-purpose callbacks list object that provides a powerful way to manage callback lists.",\n      "prototype": {\n        "add":{\n          "!type": "fn(callbacks: ?) -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.add/",\n          "!doc": "Add a callback or a collection of callbacks to a callback list."\n        },\n        "disable":{\n          "!type": "fn() -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.disable/",\n          "!doc": "Disable a callback list from doing anything more."\n        },\n        "disabled":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/callbacks.disabled/",\n          "!doc": "Determine if the callbacks list has been disabled."\n        },\n        "empty":{\n          "!type": "fn() -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.empty/",\n          "!doc": "Remove all of the callbacks from a list."\n        },\n        "fire":{\n          "!type": "fn(arguments: ?) -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.fire/",\n          "!doc": "Call all of the callbacks with the given arguments"\n        },\n        "fired":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/callbacks.fired/",\n          "!doc": "Determine if the callbacks have already been called at least once."\n        },\n        "fireWith":{\n          "!type": "fn(context?: ?, args?: ?) -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.fireWith/",\n          "!doc": "Call all callbacks in a list with the given context and arguments."\n        },\n        "has":{\n          "!type": "fn(callback: fn()) -> bool",\n          "!url": "http://api.jquery.com/callbacks.has/",\n          "!doc": "Determine whether a supplied callback is in a list."\n        },\n        "lock":{\n          "!type": "fn() -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.lock/",\n          "!doc": "Lock a callback list in its current state."\n        },\n        "locked":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/callbacks.locked/",\n          "!doc": "Determine if the callbacks list has been locked."\n        },\n        "remove":{\n          "!type": "fn(callbacks: ?) -> +jQuery.Callbacks",\n          "!url": "http://api.jquery.com/callbacks.remove/",\n          "!doc": "Remove a callback or a collection of callbacks from a callback list."\n        }\n      }\n    },\n    "contains": {\n      "!type": "fn(container: +Element, contained: +Element) -> bool",\n      "!url": "http://api.jquery.com/jquery.contains/",\n      "!doc": "Check to see if a DOM element is a descendant of another DOM element."\n    },\n    "cssHooks": {\n      "!type": "?",\n      "!url": "http://api.jquery.com/cssHooks/",\n      "!doc": "Hook directly into jQuery to override how particular CSS properties are retrieved or set, normalize CSS property naming, or create custom properties."\n    },\n    "data": {\n      "!type": "fn(element: +Element, key: string, value: ?) -> !2",\n      "!url": "http://api.jquery.com/jquery.data/",\n      "!doc": "Store arbitrary data associated with the specified element and/or return the value that was set."\n    },\n    "Event": {\n      "!type": "fn(type: ?, props?: ?) -> +jQuery.Event",\n      "!url": "http://api.jquery.com/category/events/event-object/",\n      "!doc": "The jQuery.Event constructor is exposed and can be used when calling trigger. The new operator is optional.",\n      "prototype": {\n        "currentTarget":{\n          "!type": "+Element",\n          "!url": "http://api.jquery.com/event.currentTarget/",\n          "!doc": "The current DOM element within the event bubbling phase."\n        },\n        "data":{\n          "!type": "?",\n          "!url": "http://api.jquery.com/event.data/",\n          "!doc": "An optional object of data passed to an event method when the current executing handler is bound."\n        },\n        "delegateTarget":{\n          "!type": "+Element",\n          "!url": "http://api.jquery.com/event.delegateTarget/",\n          "!doc": "The element where the currently-called jQuery event handler was attached."\n        },\n        "isDefaultPrevented":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/event.isDefaultPrevented/",\n          "!doc": "Returns whether event.preventDefault() was ever called on this event object."\n        },\n        "isImmediatePropagationStopped":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/event.isImmediatePropagationStopped/",\n          "!doc": "Returns whether event.stopImmediatePropagation() was ever called on this event object."\n        },\n        "isPropagationStopped":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/event.isPropagationStopped/",\n          "!doc": "Returns whether event.stopPropagation() was ever called on this event object."\n        },\n        "metaKey":{\n          "!type": "bool",\n          "!url": "http://api.jquery.com/event.metaKey/",\n          "!doc": "Indicates whether the META key was pressed when the event fired."\n        },\n        "namespace":{\n          "!type": "string",\n          "!url": "http://api.jquery.com/event.namespace/",\n          "!doc": "The namespace specified when the event was triggered."\n        },\n        "pageX":{\n          "!type": "number",\n          "!url": "http://api.jquery.com/event.pageX/",\n          "!doc": "The mouse position relative to the left edge of the document."\n        },\n        "pageY":{\n          "!type": "number",\n          "!url": "http://api.jquery.com/event.pageY/",\n          "!doc": "The mouse position relative to the top edge of the document."\n        },\n        "preventDefault":{\n          "!type": "fn()",\n          "!url": "http://api.jquery.com/event.preventDefault/",\n          "!doc": "If this method is called, the default action of the event will not be triggered."\n        },\n        "relatedTarget":{\n          "!type": "+Element",\n          "!url": "http://api.jquery.com/event.relatedTarget/",\n          "!doc": "The other DOM element involved in the event, if any."\n        },\n        "result":{\n          "!type": "?",\n          "!url": "http://api.jquery.com/event.result/",\n          "!doc": "The last value returned by an event handler that was triggered by this event, unless the value was undefined."\n        },\n        "stopImmediatePropagation":{\n          "!type": "fn()",\n          "!url": "http://api.jquery.com/event.stopImmediatePropagation/",\n          "!doc": "Keeps the rest of the handlers from being executed and prevents the event from bubbling up the DOM tree."\n        },\n        "stopPropagation":{\n          "!type": "fn()",\n          "!url": "http://api.jquery.com/event.stopPropagation/",\n          "!doc": "Prevents the event from bubbling up the DOM tree, preventing any parent handlers from being notified of the event."\n        },\n        "target":{\n          "!type": "+Element",\n          "!url": "http://api.jquery.com/event.target/",\n          "!doc": "The DOM element that initiated the event."\n        },\n        "timeStamp":{\n          "!type": "number",\n          "!url": "http://api.jquery.com/event.timeStamp/",\n          "!doc": "The difference in milliseconds between the time the browser created the event and January 1, 1970."\n        },\n        "type":{\n          "!type": "string",\n          "!url": "http://api.jquery.com/event.type/",\n          "!doc": "Describes the nature of the event."\n        },\n        "which":{\n          "!type": "number",\n          "!url": "http://api.jquery.com/event.which/",\n          "!doc": "For key or mouse events, this property indicates the specific key or button that was pressed."\n        }\n      }\n    },\n    "Deferred": {\n      "!type": "fn(beforeStart?: fn(deferred: +jQuery.Deferred)) -> +jQuery.Deferred",\n      "!url": "http://api.jquery.com/jQuery.Deferred/",\n      "!doc": "A constructor function that returns a chainable utility object with methods to register multiple callbacks into callback queues, invoke callback queues, and relay the success or failure state of any synchronous or asynchronous function.",\n      "prototype": {\n        "always":{\n          "!type": "fn(callback: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.always/",\n          "!doc": "Add handlers to be called when the Deferred object is either resolved or rejected."\n        },\n        "done":{\n          "!type": "fn(callback: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.done/",\n          "!doc": "Add handlers to be called when the Deferred object is resolved."\n        },\n        "fail":{\n          "!type": "fn(callback: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.fail/",\n          "!doc": "Add handlers to be called when the Deferred object is rejected."\n        },\n        "isRejected":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/deferred.isRejected/",\n          "!doc": "Determine whether a Deferred object has been rejected."\n        },\n        "isResolved":{\n          "!type": "fn() -> bool",\n          "!url": "http://api.jquery.com/deferred.isResolved/",\n          "!doc": "Determine whether a Deferred object has been resolved."\n        },\n        "notify":{\n          "!type": "fn(args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.notify/",\n          "!doc": "Call the progressCallbacks on a Deferred object with the given args."\n        },\n        "notifyWith":{\n          "!type": "fn(context?: ?, args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.notifyWith/",\n          "!doc": "Call the progressCallbacks on a Deferred object with the given context and args."\n        },\n        "pipe":{\n          "!type": "fn(doneFilter?: fn(), failFilter?: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.pipe/",\n          "!doc": "Utility method to filter and/or chain Deferreds."\n        },\n        "progress":{\n          "!type": "fn(callback: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.progress/",\n          "!doc": "Add handlers to be called when the Deferred object generates progress notifications."\n        },\n        "promise":{\n          "!type": "fn(target: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.promise/",\n          "!doc": "Return a Deferred\'s Promise object."\n        },\n        "reject":{\n          "!type": "fn(args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.reject/",\n          "!doc": "Reject a Deferred object and call any failCallbacks with the given args."\n        },\n        "rejectWith":{\n          "!type": "fn(context?: ?, args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.rejectWith/",\n          "!doc": "Reject a Deferred object and call any failCallbacks with the given context and args."\n        },\n        "resolve":{\n          "!type": "fn(args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.resolve/",\n          "!doc": "Resolve a Deferred object and call any doneCallbacks with the given args."\n        },\n        "resolveWith":{\n          "!type": "fn(context?: ?, args?: ?) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.resolveWith/",\n          "!doc": "Resolve a Deferred object and call any doneCallbacks with the given context and args."\n        },\n        "state":{\n          "!type": "fn() -> string",\n          "!url": "http://api.jquery.com/deferred.state/",\n          "!doc": "Determine the current state of a Deferred object."\n        },\n        "then":{\n          "!type": "fn(doneFilter: fn(), failFilter?: fn(), progressFilter?: fn()) -> +jQuery.Deferred",\n          "!url": "http://api.jquery.com/deferred.then/",\n          "!doc": "Add handlers to be called when the Deferred object is resolved, rejected, or still in progress."\n        }\n      }\n    },\n    "Promise": "jQuery.Deferred",\n    "dequeue": {\n        "!type": "fn(queue?: string) -> jQuery.fn",\n        "!url": "http://api.jquery.com/jQuery.dequeue/",\n        "!doc": "Execute the next function on the queue for the matched elements."\n    },\n    "each": {\n      "!type": "fn(collection: ?, callback: fn(i: number, elt: ?)) -> !0",\n      "!effects": ["call !1 number !0.<i>"],\n      "!url": "http://api.jquery.com/jQuery.each/",\n      "!doc": "A generic iterator function, which can be used to seamlessly iterate over both objects and arrays. Arrays and array-like objects with a length property (such as a function\'s arguments object) are iterated by numeric index, from 0 to length-1. Other objects are iterated via their named properties."\n    },\n    "error": "fn(message: string)",\n    "extend": {\n      "!type": "fn(target: ?, source: ?) -> !0",\n      "!effects": ["copy !1 !0"]\n    },\n    "fx": {\n      "!type": "fn(elem: +Element, options: ?, prop: string, end?: number, easing?: bool)",\n      "interval":{\n        "!type": "number",\n        "!url": "http://api.jquery.com/jquery.fx.interval",\n        "!doc": "The rate (in milliseconds) at which animations fire."\n      },\n      "off":{\n        "!type": "bool",\n        "!url": "http://api.jquery.com/jquery.fx.off",\n        "!doc": "Globally disable all animations."\n      },\n      "speeds": {\n        "slow": "number",\n        "fast": "number",\n        "_default": "number"\n      },\n      "stop": "fn()",\n      "tick": "fn()",\n      "start": "fn()"\n    },\n    "get":{\n      "!type": "fn(url: string, data?: ?, success: fn(data: string, textStatus: string, req: +XMLHttpRequest), dataType?: string) -> +XMLHttpRequest",\n      "!url": "http://api.jquery.com/jquery.get/",\n      "!doc": "Load data from the server using a HTTP GET request."\n    },\n    "getJSON": {\n      "!type": "fn(url: string, data?: ?, success: fn(data: ?, textStatus: string, req: +XMLHttpRequest)) -> +XMLHttpRequest",\n      "!url": "http://api.jquery.com/jquery.getJSON/",\n      "!doc": "Load JSON-encoded data from the server using a GET HTTP request."\n    },\n    "getScript": {\n      "!type": "fn(url: string, success?: fn(script: string, textStatus: string, req: +XMLHttpRequest)) -> +XMLHttpRequest",\n      "!url": "http://api.jquery.com/jquery.getScript/",\n      "!doc": "Load a JavaScript file from the server using a GET HTTP request, then execute it."\n    },\n    "globalEval": {\n      "!type": "fn(code: string)",\n      "!url": "http://api.jquery.com/jquery.globalEval/",\n      "!doc": "Execute some JavaScript code globally."\n    },\n    "grep": {\n      "!type": "fn(array: [?], filter: fn(elt: ?, i: number), invert?: bool) -> !0",\n      "!effects": ["call !1 !0.<i> number"],\n      "!url":"http://api.jquery.com/jquery.grep/",\n      "!doc":"Finds the elements of an array which satisfy a filter function. The original array is not affected."\n    },\n    "hasData": {\n      "!type": "fn(element: +Element) -> bool",\n      "!url": "http://api.jquery.com/jquery.hasData/",\n      "!doc": "Determine whether an element has any jQuery data associated with it."\n    },\n    "holdReady": {\n      "!type": "fn(hold: bool)",\n      "!url": "http://api.jquery.com/jquery.holdReady/",\n      "!doc": "Holds or releases the execution of jQuery\'s ready event."\n    },\n    "inArray": {\n      "!type": "fn(value: ?, array: [?], from?: number) -> number",\n      "!url": "http://api.jquery.com/jquery.inArray/",\n      "!doc": "Search for a specified value within an array and return its index (or -1 if not found)."\n    },\n    "isArray": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isArray/",\n      "!doc": "Determine whether the argument is an array."\n    },\n    "isEmptyObject": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isEmptyObject/",\n      "!doc": "Check to see if an object is empty (contains no enumerable properties)."\n    },\n    "isFunction": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isFunction/",\n      "!doc": "Determine if the argument passed is a Javascript function object."\n    },\n    "isNumeric": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isNumeric/",\n      "!doc": "Determines whether its argument is a number."\n    },\n    "isPlainObject": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isPlainObject/",\n      "!doc": "Check to see if an object is a plain object (created using \'{}\' or \'new Object\')."\n    },\n    "isWindow": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isWindow/",\n      "!doc": "Determine whether the argument is a window."\n    },\n    "isXMLDoc": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isXMLDoc/",\n      "!doc": "Check to see if a DOM node is within an XML document (or is an XML document)."\n    },\n    "isFunction": {\n      "!type": "fn(obj: ?) -> bool",\n      "!url": "http://api.jquery.com/jquery.isFunction/",\n      "!doc": ""\n    },\n    "makeArray": {\n      "!type": "fn(obj: ?) -> [!0.<i>]",\n      "!url": "http://api.jquery.com/jquery.makeArray/",\n      "!doc": "Convert an array-like object into a true JavaScript array."\n    },\n    "map": {\n      "!type": "fn(array: [?], callback: fn(element: ?, i: number) -> ?) -> [!1.!ret]",\n      "!effects": ["call !1 !0.<i> number"],\n      "!url": "http://api.jquery.com/jquery.map/",\n      "!doc": "Translate all items in an array or object to new array of items."\n    },\n    "merge": {\n      "!type": "fn(first: [?], second: [?]) -> !0",\n      "!url": "http://api.jquery.com/jquery.merge/",\n      "!doc": "Merge the contents of two arrays together into the first array."\n    },\n    "noConflict": {\n      "!type": "fn(removeAll?: bool) -> jQuery",\n      "!url": "http://api.jquery.com/jquery.noConflict/",\n      "!doc": "Relinquish jQuery\'s control of the $ variable."\n    },\n    "noop": {\n      "!type": "fn()",\n      "!url": "http://api.jquery.com/jquery.noop/",\n      "!doc": "An empty function."\n    },\n    "now": {\n      "!type": "fn() -> number",\n      "!url": "http://api.jquery.com/jquery.now/",\n      "!doc": "Return a number representing the current time."\n    },\n    "param": {\n      "!type": "fn(obj: ?) -> string",\n      "!url": "http://api.jquery.com/jquery.param/",\n      "!doc": "Create a serialized representation of an array or object, suitable for use in a URL query string or Ajax request."\n    },\n    "parseHTML": {\n      "!type": "fn(data: string, context?: +Element, keepScripts?: bool) -> [+Element]",\n      "!url": "http://api.jquery.com/jquery.parseHTML/",\n      "!doc": "Parses a string into an array of DOM nodes."\n    },\n    "parseJSON": {\n      "!type": "fn(json: string) -> ?",\n      "!url": "http://api.jquery.com/jquery.parseJSON/",\n      "!doc": "Takes a well-formed JSON string and returns the resulting JavaScript object."\n    },\n    "parseXML": {\n      "!type": "fn(xml: string) -> +XMLDocument",\n      "!url": "http://api.jquery.com/jquery.parseXML/",\n      "!doc": "Parses a string into an XML document."\n    },\n    "post": {\n      "!type": "fn(url: string, data?: ?, success: fn(data: string, textStatus: string, req: +XMLHttpRequest), dataType?: string) -> +XMLHttpRequest",\n      "!url": "http://api.jquery.com/jquery.post/",\n      "!doc": "Load data from the server using a HTTP POST request."\n    },\n    "proxy": {\n      "!type": "fn(function: fn(), context: ?) -> fn()",\n      "!url": "http://api.jquery.com/jquery.proxy/",\n      "!doc": "Takes a function and returns a new one that will always have a particular context."\n    },\n    "queue": {\n      "!type": "fn(element: +Element, queue?: string) -> [?]",\n      "!url": "http://api.jquery.com/jquery.queue/",\n      "!doc": "Show or manipulate the queue of functions to be executed on the matched element."\n    },\n    "removeData": {\n      "!type": "fn(element: +Element, name?: string)",\n      "!url": "http://api.jquery.com/jquery.removeData/",\n      "!doc": ""\n    },\n    "sub": {\n      "!type": "fn() -> jQuery",\n      "!url": "http://api.jquery.com/jquery.sub/",\n      "!doc": "Remove a previously-stored piece of data."\n    },\n    "support": {\n      "!url": "http://api.jquery.com/jquery.support/",\n      "!doc": "A collection of properties that represent the presence of different browser features or bugs. Primarily intended for jQuery\'s internal use; specific properties may be removed when they are no longer needed internally to improve page startup performance.",\n      "getSetAttribute": "bool",\n      "leadingWhitespace": "bool",\n      "tbody": "bool",\n      "htmlSerialize": "bool",\n      "style": "bool",\n      "hrefNormalized": "bool",\n      "opacity": "bool",\n      "cssFloat": "bool",\n      "checkOn": "bool",\n      "optSelected": "bool",\n      "enctype": "bool",\n      "html5Clone": "bool",\n      "boxModel": "bool",\n      "deleteExpando": "bool",\n      "noCloneEvent": "bool",\n      "inlineBlockNeedsLayout": "bool",\n      "shrinkWrapBlocks": "bool",\n      "reliableMarginRight": "bool",\n      "boxSizingReliable": "bool",\n      "pixelPosition": "bool",\n      "noCloneChecked": "bool",\n      "optDisabled": "bool",\n      "input": "bool",\n      "radioValue": "bool",\n      "appendChecked": "bool",\n      "checkClone": "bool",\n      "clearCloneStyle": "bool",\n      "reliableHiddenOffsets": "bool",\n      "boxSizing": "bool",\n      "doesNotIncludeMarginInBodyOffset": "bool",\n      "cors": "bool",\n      "ajax": "bool"\n    },\n    "trim": {\n      "!type": "fn(str: string) -> string",\n      "!url": "http://api.jquery.com/jquery.trim/",\n      "!doc": "Remove the whitespace from the beginning and end of a string."\n    },\n    "type": {\n      "!type": "fn(obj: ?) -> string",\n      "!url": "http://api.jquery.com/jquery.type/",\n      "!doc": "Determine the internal JavaScript [[Class]] of an object."\n    },\n    "unique": {\n      "!type": "fn(array: [?]) -> !0",\n      "!url": "http://api.jquery.com/jquery.unique/",\n      "!doc": "Sorts an array of DOM elements, in place, with the duplicates removed. Note that this only works on arrays of DOM elements, not strings or numbers."\n    },\n    "when": {\n      "!type": "fn(deferred: +jQuery.Deferred) -> +jQuery.Deferred",\n      "!url": "http://api.jquery.com/jquery.when/",\n      "!doc": "Provides a way to execute callback functions based on one or more objects, usually Deferred objects that represent asynchronous events."\n    }\n  },\n  "$": "jQuery"\n}\n';});

/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*
 * Throughout this file, the term "outer scope" is used to refer to the outer-
 * most/global/root Scope objects for particular file. The term "inner scope"
 * is used to refer to a Scope object that is reachable via the child relation
 * from an outer scope.
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50 */
/*global define, brackets, CodeMirror, $, Worker, setTimeout */

define('ScopeManager',['require','exports','module','HintUtils','MessageIds','Preferences','text!thirdparty/tern/defs/ecma5.json','text!thirdparty/tern/defs/browser.json','text!thirdparty/tern/defs/jquery.json'],function (require, exports, module) {
    

    var DocumentManager     = brackets.getModule("document/DocumentManager"),
        LanguageManager     = brackets.getModule("language/LanguageManager"),
        PlatformFileSystem  = brackets.getModule("file/PlatformFileSystem").PlatformFileSystem,
        ProjectManager      = brackets.getModule("project/ProjectManager"),
        CollectionUtils     = brackets.getModule("utils/CollectionUtils"),
        ExtensionUtils      = brackets.getModule("utils/ExtensionUtils"),
        FileUtils           = brackets.getModule("file/FileUtils"),
        FileIndexManager    = brackets.getModule("project/FileIndexManager"),
        HintUtils           = require("HintUtils"),
        MessageIds          = require("MessageIds"),
        Preferences         = require("Preferences"),
        Ecma5Json           = require("text!thirdparty/tern/defs/ecma5.json"),
        BrowserJson         = require("text!thirdparty/tern/defs/browser.json"),
        JqueryJson          = require("text!thirdparty/tern/defs/jquery.json");
    
    var ternEnvironment     = [],
        pendingTernRequests = {},
        builtinFiles        = ["ecma5.json", "browser.json", "jquery.json"],
        builtinLibraryNames = [],
        isDocumentDirty     = false,
        _hintCount          = 0,
        currentWorker       = null,
        documentChanges     = null,     // bounds of document changes
        preferences         = null,
        deferredPreferences = null;

    var MAX_HINTS           = 30,  // how often to reset the tern server
        LARGE_LINE_CHANGE   = 100,
        LARGE_LINE_COUNT    = 250,
        OFFSET_ZERO         = {line: 0, ch: 0};

    /**
     *  An array of library names that contain JavaScript builtins definitions.
     *
     * @returns {Array.<string>} - array of library  names.
     */
    function getBuiltins() {
        return builtinLibraryNames;
    }

    /**
     * Read in the json files that have type information for the builtins, dom,etc
     */
    function initTernEnv() {
        var defs = [Ecma5Json, BrowserJson, JqueryJson];
        defs.forEach(function(def){
            var library = JSON.parse(def);
            builtinLibraryNames.push(library["!name"]);
            ternEnvironment.push(library);
        });
        // var path = ExtensionUtils.getModulePath(module, "thirdparty/tern/defs/"),
        //     files = builtinFiles,
        //     library;

        // files.forEach(function (i) {
        //     PlatformFileSystem.resolveNativeFileSystemPath(path + i, function (fileEntry) {
        //         FileUtils.readAsText(fileEntry).done(function (text) {
        //             library = JSON.parse(text);
        //             builtinLibraryNames.push(library["!name"]);
        //             ternEnvironment.push(library);
        //         }).fail(function (error) {
        //             console.log("failed to read tern config file " + i);
        //         });
        //     }, function (error) {
        //         console.log("failed to read tern config file " + i);
        //     });
        // });
    }

    initTernEnv();

    /**
     *  Init preferences from a file in the project root or builtin
     *  defaults if no file is found;
     *
     *  @param {string=} projectRootPath - new project root path. Only needed
     *  for unit tests.
     */
    function initPreferences(projectRootPath) {

        // Reject the old preferences if they have not completed.
        if (deferredPreferences && deferredPreferences.state() === "pending") {
            deferredPreferences.reject();
        }

        deferredPreferences = $.Deferred();
        var pr = ProjectManager.getProjectRoot();

        // Open preferences relative to the project root
        // Normally there is a project root, but for unit tests we need to
        // pass in a project root.
        if (pr) {
            projectRootPath = pr.fullPath;
        } else if (!projectRootPath) {
            console.log("initPreferences: projectRootPath has no value");
        }

        var path = projectRootPath + Preferences.FILE_NAME;

        PlatformFileSystem.resolveNativeFileSystemPath(path, function (fileEntry) {
            FileUtils.readAsText(fileEntry).done(function (text) {
                var configObj = null;
                try {
                    configObj = JSON.parse(text);
                } catch (e) {
                    // continue with null configObj which will result in
                    // default settings.
                    console.log("Error parsing preference file: " + path);
                    if (e instanceof SyntaxError) {
                        console.log(e.message);
                    }
                }
                preferences = new Preferences(configObj);
                deferredPreferences.resolve();
            }).fail(function (error) {
                preferences = new Preferences();
                deferredPreferences.resolve();
            });
        }, function (error) {
            preferences = new Preferences();
            deferredPreferences.resolve();
        });
    }

    /**
     * Will initialize preferences only if they do not exist.
     *
     */
    function ensurePreferences() {
        if (!deferredPreferences) {
            initPreferences();
        }
    }

    /**
     * Send a message to the tern worker - if the worker is being initialized,
     * the message will not be posted until initialization is complete
     */
    function postMessage(msg) {
        currentWorker.postMessage(msg);
    }

    /**
     *  For each file in a directory get a callback with the path of the javascript
     *  file or directory.
     *
     *  dotfiles are ignored.
     *
     * @param {string} dir - directory in which to list the files.
     * @param {function()} doneCallback - called after all of the files have
     * been listed.
     * @param {function(string)} fileCallback - callback for javascript files.
     * The function is passed the full path name of the file.
     * @param {!function(string)=} directoryCallback - callback for directory
     * files. The function is passed the full path name of the file (optional).
     * @param {!function(string)=} errorCallback - Callback for errors (optional).
     */
    function forEachFileInDirectory(dir, doneCallback, fileCallback, directoryCallback, errorCallback) {
        var files = [];

        PlatformFileSystem.resolveNativeFileSystemPath(dir, function (dirEntry) {
            var reader = dirEntry.createReader();

            reader.readEntries(function (entries) {
                Array.prototype.slice.call(entries, 0, preferences.getMaxFileCount()).forEach(function (entry) {
                    var path    = entry.fullPath,
                        split   = HintUtils.splitPath(path),
                        file    = split.file;

                    if (fileCallback && entry.isFile) {

                        if (file.indexOf(".") > 0) { // ignore .dotfiles
                            var languageID = LanguageManager.getLanguageForPath(path).getId();
                            if (languageID === HintUtils.LANGUAGE_ID) {
                                fileCallback(path);
                            }
                        }
                    } else if (directoryCallback && entry.isDirectory) {
                        var dirName = HintUtils.splitPath(split.dir).file;
                        if (dirName.indexOf(".") !== 0) { // ignore .dotfiles
                            directoryCallback(entry.fullPath);
                        }
                    }
                });
                doneCallback();
            }, function (err) {
                if (errorCallback) {
                    errorCallback(err);
                }
                console.log("Unable to refresh directory: " + err);
            });
        }, function (err) {
            if (errorCallback) {
                errorCallback(err);
            }
            console.log("Directory \"%s\" does not exist", dir);
        });
    }

    /**
     * Test if the directory should be excluded from analysis.
     *
     * @param {!string} path - full directory path.
     * @returns {boolean} true if excluded, false otherwise.
     */
    function isDirectoryExcluded(path) {
        var excludes = preferences.getExcludedDirectories();

        if (!excludes) {
            return false;
        }

        var testPath = ProjectManager.makeProjectRelativeIfPossible(path);
        testPath = FileUtils.canonicalizeFolderPath(testPath);

        return excludes.test(testPath);
    }

    /**
     * Test if the file should be excluded from analysis.
     *
     * @param {!string} path - full directory path.
     * @returns {boolean} true if excluded, false otherwise.
     */
    function isFileExcluded(path) {
        var excludes = preferences.getExcludedFiles();

        if (!excludes) {
            return false;
        }

        var file = HintUtils.splitPath(path).file;

        return excludes.test(file);
    }

    /**
     *  Get a list of javascript files in a given directory.
     *
     * @param {string} dir - directory to list the files of.
     * @param {function(Array.<string>)} successCallback - callback with
     * array of file path names.
     * @param {function(Array.<string>)} errorCallback - callback for
     * when an error occurs.
     */
    function getFilesInDirectory(dir, successCallback, errorCallback) {
        var files = []; // file names without paths.

        /**
         *  Call the success callback with all of the found files.
         */
        function doneCallback() {
            successCallback(files);
        }

        /**
         *  Add files to global list.
         *
         * @param path - full path of file.
         */
        function fileCallback(path) {
            if (!isFileExcluded(path)) {
                files.push(path);
            }
        }

        forEachFileInDirectory(dir, doneCallback, fileCallback, null, errorCallback);
    }

    /**
     * Add a pending request waiting for the tern-worker to complete.
     *
     * @param {string} file - the name of the file
     * @param {{line: number, ch: number}} offset - the offset into the file the request is for
     * @param {string} type - the type of request
     * @return {jQuery.Promise} - the promise for the request  
     */
    function addPendingRequest(file, offset, type) {
        var requests,
            key = file + "@" + offset.line + "@" + offset.ch,
            $deferredRequest;
        if (CollectionUtils.hasProperty(pendingTernRequests, key)) {
            requests = pendingTernRequests[key];
        } else {
            requests = {};
            pendingTernRequests[key] = requests;
        }

        if (CollectionUtils.hasProperty(requests, type)) {
            $deferredRequest = requests[type];
        } else {
            requests[type] = $deferredRequest = $.Deferred();
        }
        return $deferredRequest.promise();
    }

    /**
     * Get any pending $.Deferred object waiting on the specified file and request type
     * @param {string} file - the file
     * @param {{line: number, ch: number}} offset - the offset into the file the request is for
     * @param {string} type - the type of request
     * @return {jQuery.Deferred} - the $.Deferred for the request     
     */
    function getPendingRequest(file, offset, type) {
        var key = file + "@" + offset.line + "@" + offset.ch;
        if (CollectionUtils.hasProperty(pendingTernRequests, key)) {
            var requests = pendingTernRequests[key],
                requestType = requests[type];

            delete pendingTernRequests[key][type];

            if (!Object.keys(requests).length) {
                delete pendingTernRequests[key];
            }

            return requestType;
        }
    }
    
    /**
     * @param {string} file a relative path
     * @return {string} returns the path we resolved when we tried to parse the file, or undefined
     */
    function getResolvedPath(file) {
        return currentWorker.getResolvedPath(file);
    }

    /**
     * Get a Promise for the definition from TernJS, for the file & offset passed in.
     * @param {{type: string, name: string, offsetLines: number, text: string}} fileInfo
     * - type of update, name of file, and the text of the update.
     * For "full" updates, the whole text of the file is present. For "part" updates,
     * the changed portion of the text. For "empty" updates, the file has not been modified
     * and the text is empty.
     * @param {{line: number, ch: number}} offset - the offset in the file the hints should be calculate at
     * @return {jQuery.Promise} - a promise that will resolve to definition when
     *      it is done
     */
    function getJumptoDef(fileInfo, offset) {
        postMessage({
            type: MessageIds.TERN_JUMPTODEF_MSG,
            fileInfo: fileInfo,
            offset: offset
        });

        return addPendingRequest(fileInfo.name, offset, MessageIds.TERN_JUMPTODEF_MSG);
    }

    /**
     * check to see if the text we are sending to Tern is too long.
     * @param {string} the text to check
     * @return {string} the text, or the empty text if the original was too long
     */
    function filterText(text) {
        var newText = text;
        if (text.length > preferences.getMaxFileSize()) {
            newText = "";
        }
        return newText;
    }
    
    /**
     * Get the text of a document, applying any size restrictions
     * if necessary
     * @param {Document} document - the document to get the text from
     * @return {string} the text, or the empty text if the original was too long
     */
    function getTextFromDocument(document) {
        var text = document.getText();
        text = filterText(text);
        return text;
    }
    
    
    
    /**
     * Request Jump-To-Definition from Tern.
     *
     * @param {session} session - the session
     * @param {Document} document - the document
     * @param {{line: number, ch: number}} offset - the offset into the document
     * @return {jQuery.Promise} - The promise will not complete until tern
     *      has completed.
     */
    function requestJumptoDef(session, document, offset) {
        var path    = document.file.fullPath,
            fileInfo = {type: MessageIds.TERN_FILE_INFO_TYPE_FULL,
                name: path,
                offsetLines: 0,
                text: filterText(session.getJavascriptText())};
        
        var ternPromise = getJumptoDef(fileInfo, offset);
        
        return {promise: ternPromise};
    }

    /**
     * Handle the response from the tern web worker when
     * it responds with the definition
     *
     * @param response - the response from the worker
     */
    function handleJumptoDef(response) {
        
        var file = response.file,
            offset = response.offset;
        
        var $deferredJump = getPendingRequest(file, offset, MessageIds.TERN_JUMPTODEF_MSG);
        
        if ($deferredJump) {
            response.fullPath = getResolvedPath(response.resultFile);
            $deferredJump.resolveWith(null, [response]);
        }
    }

    /**
     * Get a Promise for the completions from TernJS, for the file & offset passed in.
     *
     * @param {{type: string, name: string, offsetLines: number, text: string}} fileInfo
     * - type of update, name of file, and the text of the update.
     * For "full" updates, the whole text of the file is present. For "part" updates,
     * the changed portion of the text. For "empty" updates, the file has not been modified
     * and the text is empty.
     * @param {{line: number, ch: number}} offset - the offset in the file the hints should be calculate at
     * @param {boolean} isProperty - true if getting a property hint,
     * otherwise getting an identifier hint.
     * @return {jQuery.Promise} - a promise that will resolve to an array of completions when
     *      it is done
     */
    function getTernHints(fileInfo, offset, isProperty) {

        /**
         *  If the document is large and we have modified a small portions of it that
         *  we are asking hints for, then send a partial document.
         */
        postMessage({
            type: MessageIds.TERN_COMPLETIONS_MSG,
            fileInfo: fileInfo,
            offset: offset,
            isProperty: isProperty
        });
        
        return addPendingRequest(fileInfo.name, offset, MessageIds.TERN_COMPLETIONS_MSG);
    }

    /**
     * Get a Promise for the function type from TernJS.
     * @param {{type: string, name: string, offsetLines: number, text: string}} fileInfo
     * - type of update, name of file, and the text of the update.
     * For "full" updates, the whole text of the file is present. For "part" updates,
     * the changed portion of the text. For "empty" updates, the file has not been modified
     * and the text is empty.
     * @param {{line:number, ch:number}} offset - the line, column info for what we want the function type of.
     * @return {jQuery.Promise} - a promise that will resolve to the function type of the function being called.
     */
    function getTernFunctionType(fileInfo, offset) {
        postMessage({
            type: MessageIds.TERN_CALLED_FUNC_TYPE_MSG,
            fileInfo: fileInfo,
            offset: offset
        });

        return addPendingRequest(fileInfo.name, offset, MessageIds.TERN_CALLED_FUNC_TYPE_MSG);
    }


    /**
     *  Given a starting and ending position, get a code fragment that is self contained
     *  enough to be compiled.
     *
     * @param {!Session} session - the current session
     * @param {{line: number, ch: number}} start - the starting position of the changes
     * @returns {{type: string, name: string, offsetLines: number, text: string}}
     */
    function getFragmentAround(session, start) {
        var minIndent = null,
            minLine   = null,
            endLine,
            cm        = session.editor._codeMirror,
            tabSize   = cm.getOption("tabSize"),
            document  = session.editor.document,
            p,
            min,
            indent,
            line;

        // expand range backwards
        for (p = start.line - 1, min = Math.max(0, p - 100); p >= min; --p) {
            line = session.getLine(p);
            var fn = line.search(/\bfunction\b/);
            
            if (fn >= 0) {
                indent = CodeMirror.countColumn(line, null, tabSize);
                if (minIndent === null || minIndent > indent) {
                    if (session.getToken({line: p, ch: fn + 1}).type === "keyword") {
                        minIndent = indent;
                        minLine = p;
                    }
                }
            }
        }

        if (minIndent === null) {
            minIndent = 0;
        }

        if (minLine === null) {
            minLine = min;
        }

        var max = Math.min(cm.lastLine(), start.line + 100),
            endCh = 0;

        for (endLine = start.line + 1; endLine < max; ++endLine) {
            line = cm.getLine(endLine);

            if (line.length > 0) {
                indent = CodeMirror.countColumn(line, null, tabSize);
                if (indent <= minIndent) {
                    endCh = line.length;
                    break;
                }
            }
        }

        var from = {line: minLine, ch: 0},
            to   = {line: endLine, ch: endCh};

        return {type: MessageIds.TERN_FILE_INFO_TYPE_PART,
            name: document.file.fullPath,
            offsetLines: from.line,
            text: document.getRange(from, to)};
    }


    /**
     * Get an object that describes what tern needs to know about the updated
     * file to produce a hint. As a side-effect of this calls the document
     * changes are reset.
     *
     * @param {!Session} session - the current session
     * @returns {{type: string, name: {string}, offsetLines: {number}, text: {string}}
     */
    function getFileInfo(session) {
        var start = session.getCursor(),
            end = start,
            document = session.editor.document,
            path = document.file.fullPath,
            isHtmlFile = LanguageManager.getLanguageForPath(path).getId() === "html",
            result;

        if (isHtmlFile) {
            result = {type: MessageIds.TERN_FILE_INFO_TYPE_FULL,
                name: path,
                text: session.getJavascriptText()};
        } else if (!documentChanges) {
            result = {type: MessageIds.TERN_FILE_INFO_TYPE_EMPTY,
                name: path,
                text: ""};
        } else if (session.editor.lineCount() > LARGE_LINE_COUNT &&
                (documentChanges.to - documentChanges.from < LARGE_LINE_CHANGE) &&
                documentChanges.from <= start.line &&
                documentChanges.to > end.line) {
            result = getFragmentAround(session, start);
        } else {
            result = {type: MessageIds.TERN_FILE_INFO_TYPE_FULL,
                name: path,
                text: getTextFromDocument(document)};
        }

        documentChanges = null;
        return result;
    }

    /**
     *  Get the current offset. The offset is adjusted for "part" updates.
     *
     * @param {!Session} session - the current session
     * @param {{type: string, name: string, offsetLines: number, text: string}} fileInfo
     * - type of update, name of file, and the text of the update.
     * For "full" updates, the whole text of the file is present. For "part" updates,
     * the changed portion of the text. For "empty" updates, the file has not been modified
     * and the text is empty.
     * @param {{line: number, ch: number}=} offset - the default offset (optional). Will
     * use the cursor if not provided.
     * @returns {{line: number, ch: number}}
     */
    function getOffset(session, fileInfo, offset) {
        var newOffset = offset || session.getCursor();

        if (fileInfo.type === MessageIds.TERN_FILE_INFO_TYPE_PART) {
            newOffset.line = Math.max(0, newOffset.line - fileInfo.offsetLines);
        }

        return newOffset;
    }
    
    /**
     * Get a Promise for all of the known properties from TernJS, for the directory and file.
     * The properties will be used as guesses in tern.
     * @param {Session} session - the active hinting session
     * @param {Document} document - the document for which scope info is
     *      desired
     * @return {jQuery.Promise} - The promise will not complete until the tern
     *      request has completed.
     */
    function requestGuesses(session, document) {
        var $deferred = $.Deferred(),
            fileInfo = getFileInfo(session),
            offset = getOffset(session, fileInfo);

        postMessage({
            type: MessageIds.TERN_GET_GUESSES_MSG,
            fileInfo: fileInfo,
            offset: offset
        });

        var promise = addPendingRequest(fileInfo.name, offset, MessageIds.TERN_GET_GUESSES_MSG);
        promise.done(function (guesses) {
            session.setGuesses(guesses);
            $deferred.resolve();
        }).fail(function () {
            $deferred.reject();
        });

        return $deferred.promise();
    }

    /**
     * Handle the response from the tern web worker when
     * it responds with the list of completions
     *
     * @param {{file: string, offset: {line: number, ch: number}, completions:Array.<string>,
     *          properties:Array.<string>}} response - the response from the worker
     */
    function handleTernCompletions(response) {

        var file = response.file,
            offset = response.offset,
            completions = response.completions,
            properties = response.properties,
            fnType  = response.fnType,
            type = response.type,
            $deferredHints = getPendingRequest(file, offset, type);
        
        if ($deferredHints) {
            if (completions) {
                $deferredHints.resolveWith(null, [{completions: completions}]);
            } else if (properties) {
                $deferredHints.resolveWith(null, [{properties: properties}]);
            } else if (fnType) {
                $deferredHints.resolveWith(null, [fnType]);
            }
        }
    }

    /**
     * Handle the response from the tern web worker when
     * it responds to the get guesses message.
     *
     * @param {{file: string, type: string, offset: {line: number, ch: number},
     *      properties: Array.<string>}} response -
     *      the response from the worker contains the guesses for a
     *      property lookup.
     */
    function handleGetGuesses(response) {
        var path = response.file,
            type = response.type,
            offset = response.offset,
            $deferredHints = getPendingRequest(path, offset, type);

        if ($deferredHints) {
            $deferredHints.resolveWith(null, [response.properties]);
        }
    }

    /**
     * Handle the response from the tern web worker when
     * it responds to the update file message.
     *
     * @param {{path:string, type: string}} response - the response from the worker
     */
    function handleUpdateFile(response) {

        var path = response.path,
            type = response.type,
            $deferredHints = getPendingRequest(path, OFFSET_ZERO, type);

        if ($deferredHints) {
            $deferredHints.resolve();
        }
    }

    /**
     * Encapsulate all the logic to talk to the worker thread.  This will create
     * a new instance of a TernWorker, which the rest of the hinting code can use to talk
     * to the worker, without worrying about initialization, priming the pump, etc.
     *
     */
    function TernWorker() {
        var ternPromise         = null,
            addFilesPromise     = null,
            rootTernDir         = null,
            projectRoot         = null,
            stopAddingFiles     = false,
            resolvedFiles       = {},       // file -> resolved file
            numInitialFiles     = 0,
            numResolvedFiles    = 0,
            numAddedFiles       = 0,
            _ternWorker         = null;

        /**
         * @param {string} file a relative path
         * @return {string} returns the path we resolved when we tried to parse the file, or undefined
         */
        function getResolvedPath(file) {
            return resolvedFiles[file];
        }
        
        /**
         *  Determine whether the current set of files are using modules to pull in
         *  additional files.
         *
         * @returns {boolean} - true if more files than the current directory have
         * been read in.
         */
        function usingModules() {
            return numInitialFiles !== numResolvedFiles;
        }
        
        /**
         * Send a message to the tern worker - if the worker is being initialized,
         * the message will not be posted until initialization is complete
         */
        function postMessage(msg) {
            addFilesPromise.done(function (ternWorker) {
                ternWorker.postMessage(msg);
            });
        }

        /**
         * Send a message to the tern worker - this is only for messages that
         * need to be sent before and while the addFilesPromise is being resolved.
         */
        function _postMessageByPass(msg) {
            ternPromise.done(function (ternWorker) {
                ternWorker.postMessage(msg);
            });
        }

        /**
         *  Update tern with the new contents of a given file.
         *
         * @param {Document} document - the document to update
         * @return {jQuery.Promise} - the promise for the request
         */
        function updateTernFile(document) {
            var path  = document.file.fullPath;
            
            _postMessageByPass({
                type       : MessageIds.TERN_UPDATE_FILE_MSG,
                path       : path,
                text       : getTextFromDocument(document)
            });

            return addPendingRequest(path, OFFSET_ZERO, MessageIds.TERN_UPDATE_FILE_MSG);
        }

        /**
         * Handle a request from the worker for text of a file
         *
         * @param {{file:string}} request - the request from the worker.  Should be an Object containing the name
         *      of the file tern wants the contents of 
         */
        function handleTernGetFile(request) {
    
            function replyWith(name, txt) {
                _postMessageByPass({
                    type: MessageIds.TERN_GET_FILE_MSG,
                    file: name,
                    text: txt
                });
            }
    
            var name = request.file;
    
            /**
             * Helper function to get the text of a given document and send it to tern.
             * If we successfully get the document from the DocumentManager then the text of 
             * the document will be sent to the tern worker.
             * The Promise for getDocumentForPath is returned so that custom fail functions can be
             * used.
             *
             * @param {string} filePath - the path of the file to get the text of
             * @return {jQuery.Promise} - the Promise returned from DocumentMangaer.getDocumentForPath 
             */
            function getDocText(filePath) {
                return DocumentManager.getDocumentForPath(filePath).done(function (document) {
                    resolvedFiles[name] = filePath;
                    numResolvedFiles++;
                    replyWith(name, getTextFromDocument(document));
                });
            }
            
            /**
             * Helper function to find any files in the project that end with the
             * name we are looking for.  This is so we can find requirejs modules 
             * when the baseUrl is unknown, or when the project root is not the same
             * as the script root (e.g. if you open the 'brackets' dir instead of 'brackets/src' dir).
             */
            function findNameInProject() {
                // check for any files in project that end with the right path.
                var fileName = HintUtils.splitPath(name).file;
                FileIndexManager.getFilenameMatches("all", fileName)
                    .done(function (files) {
                        var file;
                        files = files.filter(function (file) {
                            var pos = file.fullPath.length - name.length;
                            return pos === file.fullPath.lastIndexOf(name);
                        });
                        
                        if (files.length === 1) {
                            file = files[0];
                        }
                        if (file) {
                            getDocText(file.fullPath).fail(function () {
                                replyWith(name, "");
                            });
                        } else {
                            replyWith(name, "");
                        }
                        
                    })
                    .fail(function () {
                        replyWith(name, "");
                    });
            }
    
            getDocText(name).fail(function () {
                getDocText(rootTernDir + name).fail(function () {
                    // check relative to project root
                    getDocText(projectRoot + name)
                        // last look for any files that end with the right path
                        // in the project
                        .fail(findNameInProject);
                });
            });
        }
    
        /**
         *  Prime the pump for a fast first lookup.
         *
         * @param {string} path - full path of file
         * @return {jQuery.Promise} - the promise for the request
         */
        function primePump(path) {
            _postMessageByPass({
                type        : MessageIds.TERN_PRIME_PUMP_MSG,
                path        : path
            });
    
            return addPendingRequest(path, OFFSET_ZERO, MessageIds.TERN_PRIME_PUMP_MSG);
        }

        /**
         * Handle the response from the tern web worker when
         * it responds to the prime pump message.
         *
         * @param {{path: string, type: string}} response - the response from the worker
         */
        function handlePrimePumpCompletion(response) {

            var path = response.path,
                type = response.type,
                $deferredHints = getPendingRequest(path, OFFSET_ZERO, type);

            if ($deferredHints) {
                $deferredHints.resolve();
            }
        }

        /**
         *  Add new files to tern, keeping any previous files.
         *  The tern server must be initialized before making
         *  this call.
         *
         * @param {Array.<string>} files - array of file to add to tern.
         * @return {boolean} - true if more files may be added, false if maximum has been reached.
         */
        function addFilesToTern(files) {
            // limit the number of files added to tern.
            var maxFileCount = preferences.getMaxFileCount();
            if (numResolvedFiles + numAddedFiles < maxFileCount) {
                var available = maxFileCount - numResolvedFiles - numAddedFiles;
    
                if (available < files.length) {
                    files = files.slice(0, available);
                }
    
                numAddedFiles += files.length;
                ternPromise.done(function (worker) {
                    worker.postMessage({
                        type        : MessageIds.TERN_ADD_FILES_MSG,
                        files       : files
                    });
                });
    
            } else {
                stopAddingFiles = true;
            }
    
            return stopAddingFiles;
        }
            
        /**
         *  Add the files in the directory and subdirectories of a given directory
         *  to tern.
         *
         * @param {string} dir - the root directory to add.
         * @param {function ()} doneCallback - called when all files have been
         * added to tern.
         */
        function addAllFilesAndSubdirectories(dir, doneCallback) {
    
            var numDirectoriesLeft = 1;        // number of directories to process
    
            /**
             *  Add the files in the directory and subdirectories of a given directory
             *  to tern, excluding the rootTernDir).
             *
             * @param {string} dir - the root directory to add.
             * @param {function()} successCallback - callback when
             * done processing files.
             */
            function addAllFilesRecursively(dir, successCallback) {
    
                var files = [],
                    dirs = [];
    
                function doneCallback() {
                    numDirectoriesLeft--;
    
                    if (!stopAddingFiles && files.length > 0 &&
                            (dir + "/") !== rootTernDir) {
                        addFilesToTern(files);
                    }
    
                    if (!stopAddingFiles) {
                        dirs.forEach(function (path) {
                            var dir = path;//HintUtils.splitPath(path).dir;
                            if (!stopAddingFiles) {
                                numDirectoriesLeft++;
                                addAllFilesRecursively(dir, successCallback);
                            }
                        });
                    }
    
                    if (numDirectoriesLeft === 0) {
                        successCallback();
                    }
                }
    
                /**
                 *  Add files to global list.
                 *
                 * @param path - full path of file.
                 */
                function fileCallback(path) {
                    if (!isFileExcluded(path)) {
                        files.push(path);
                    }
                }
    
                /**
                 *  For each directory, add all the files in its subdirectory.
                 *
                 * @param path
                 */
                function directoryCallback(path) {
                    if (!isDirectoryExcluded(path) &&
                            path !== rootTernDir) {
                        dirs.push(path);
                    }
                }
    
                dir = FileUtils.canonicalizeFolderPath(dir);
                forEachFileInDirectory(dir, doneCallback, fileCallback, directoryCallback);
            }
    
            addAllFilesRecursively(dir, function () {
                doneCallback();
            });
        }
            
        /**
         * Init the web worker that does all the code hinting work.
         *
         * If a worker already exists, then this will terminate that worker and
         * start a new worker - this helps alleviate leaks that may be ocurring in 
         * the code that the worker runs.  
         */
        function initTernWorker() {
            if (_ternWorker) {
                _ternWorker.terminate();
            }
            var workerDeferred = $.Deferred();
            ternPromise = workerDeferred.promise();
            var path = ExtensionUtils.getModulePath(module, "tern-worker.js");
            _ternWorker = new Worker(path);
    
            _ternWorker.addEventListener("message", function (e) {
                var response = e.data,
                    type = response.type;
    
                if (type === MessageIds.TERN_COMPLETIONS_MSG ||
                        type === MessageIds.TERN_CALLED_FUNC_TYPE_MSG) {
                    // handle any completions the worker calculated
                    handleTernCompletions(response);
                } else if (type === MessageIds.TERN_GET_FILE_MSG) {
                    // handle a request for the contents of a file
                    handleTernGetFile(response);
                } else if (type === MessageIds.TERN_JUMPTODEF_MSG) {
                    handleJumptoDef(response);
                } else if (type === MessageIds.TERN_PRIME_PUMP_MSG) {
                    handlePrimePumpCompletion(response);
                } else if (type === MessageIds.TERN_GET_GUESSES_MSG) {
                    handleGetGuesses(response);
                } else if (type === MessageIds.TERN_UPDATE_FILE_MSG) {
                    handleUpdateFile(response);
                } else if (type === MessageIds.TERN_WORKER_READY) {
                    workerDeferred.resolveWith(null, [_ternWorker]);
                } else {
                    console.log("Worker: " + (response.log || response));
                }
            });
            
        }
        /**
         * Create a new tern server.
         */
        function initTernServer(dir, files) {
            initTernWorker();
            numResolvedFiles = 0;
            numAddedFiles = 0;
            stopAddingFiles = false;
            numInitialFiles = files.length;

            ternPromise.done(function (worker) {
                worker.postMessage({
                    type        : MessageIds.TERN_INIT_MSG,
                    dir         : dir,
                    files       : files,
                    env         : ternEnvironment
                });
            });
            rootTernDir = dir + "/";
        }
    
        /**
         *  We can skip tern initialization if we are opening a file that has
         *  already been added to tern.
         *
         * @param {string} newFile - full path of new file being opened in the editor.
         * @returns {boolean} - true if tern initialization should be skipped,
         * false otherwise.
         */
        function canSkipTernInitialization(newFile) {
            return resolvedFiles[newFile] !== undefined;
        }
    
    
        /**
         *  Do the work to initialize a code hinting session.
         *
         * @param {Session} session - the active hinting session
         * @param {Document} document - the document the editor has changed to
         * @param {Document} previousDocument - the document the editor has changed from
         */
        function doEditorChange(session, document, previousDocument) {
            var path        = document.file.fullPath,
                split       = HintUtils.splitPath(path),
                dir         = split.dir,
                files       = [],
                file        = split.file,
                pr;
    
            var addFilesDeferred = $.Deferred();
    
            documentChanges = null;
            addFilesPromise = addFilesDeferred.promise();
            pr = ProjectManager.getProjectRoot() ? ProjectManager.getProjectRoot().fullPath : null;
    
            // avoid re-initializing tern if possible.
            if (canSkipTernInitialization(path)) {
    
                // update the previous document in tern to prevent stale files.
                if (isDocumentDirty && previousDocument) {
                    var updateFilePromise = updateTernFile(previousDocument);
                    updateFilePromise.done(function () {
                        primePump(path);
                        addFilesDeferred.resolveWith(null, [_ternWorker]);
                    });
                } else {
                    addFilesDeferred.resolveWith(null, [_ternWorker]);
                }
    
                isDocumentDirty = false;
                return;
            }
    
            isDocumentDirty = false;
            resolvedFiles = {};
            projectRoot = pr;

            ensurePreferences();
            deferredPreferences.done(function () {
                getFilesInDirectory(dir, function (files) {
                    initTernServer(dir, files);

                    var hintsPromise = primePump(path);
                    hintsPromise.done(function () {
                        if (!usingModules()) {
                            // Read the subdirectories of the new file's directory.
                            // Read them first in case there are too many files to
                            // read in the project.
                            addAllFilesAndSubdirectories(dir, function () {
                                // If the file is in the project root, then read
                                // all the files under the project root.
                                var currentDir = (dir + "/");
                                if (projectRoot && currentDir !== projectRoot &&
                                        currentDir.indexOf(projectRoot) === 0) {
                                    addAllFilesAndSubdirectories(projectRoot, function () {
                                        // prime the pump again but this time don't wait
                                        // for completion.
                                        primePump(path);

                                        addFilesDeferred.resolveWith(null, [_ternWorker]);
                                    });
                                } else {
                                    addFilesDeferred.resolveWith(null, [_ternWorker]);
                                }
                            });
                        } else {
                            addFilesDeferred.resolveWith(null, [_ternWorker]);
                        }
                    });
                }, function () {
                    addFilesDeferred.resolveWith(null);
                });
            }).fail(function () {});
        }

        /**
         * Called each time a new editor becomes active.
         *
         * @param {Session} session - the active hinting session
         * @param {Document} document - the document of the editor that has changed
         * @param {Document} previousDocument - the document of the editor is changing from
         */
        function handleEditorChange(session, document, previousDocument) {
            if (addFilesPromise === null) {
                doEditorChange(session, document, previousDocument);
            } else {
                addFilesPromise.done(function () {
                    doEditorChange(session, document, previousDocument);
                });
            }
        }

        /**
         * Do some cleanup when a project is closed.
         *
         * We can clean up the web worker we use to calculate hints now, since
         * we know we will need to re-init it in any new project that is opened.  
         */
        function closeWorker() {
            function terminateWorker() {
                var worker = _ternWorker;
                setTimeout(function () {
                    // give pending requests a chance to finish
                    worker.terminate();
                    worker = null;
                }, 1000);
                _ternWorker = null;
                resolvedFiles = {};
            }
            
            if (_ternWorker) {
                if (addFilesPromise) {
                    // If we're in the middle of added files, don't terminate 
                    // until we're done or we might get NPEs
                    addFilesPromise.done(terminateWorker).fail(terminateWorker);
                } else {
                    terminateWorker();
                }
            }
        }

        function whenReady(func) {
            addFilesPromise.done(func);
        }
        
        this.closeWorker = closeWorker;
        this.handleEditorChange = handleEditorChange;
        this.postMessage = postMessage;
        this.getResolvedPath = getResolvedPath;
        this.whenReady = whenReady;
        
        return this;
    }

    var reseting = false;

    /**
     * reset the tern worker thread, if necessary.  
     *
     * To avoid memory leaks in the worker thread we periodically kill
     * the web worker instance, and start a new one.  To avoid a performance
     * hit when we do this we start up a new worker, and don't kill the old
     * one unitl the new one is initialized.
     */
    function maybeReset(session, document) {
        var newWorker;
        // if we're in the middle of a reset, don't have to check
        // the new worker will be online soon
        if (!reseting) {
            if (++_hintCount > MAX_HINTS) {
                reseting = true;
                newWorker = new TernWorker();
                newWorker.handleEditorChange(session, document, null);
                newWorker.whenReady(function () {
                    // tell the old worker to shut down
                    currentWorker.closeWorker();
                    currentWorker = newWorker;
                    // all done reseting
                    reseting = false;
                });
                _hintCount = 0;
            }
        }
    }

    /**
     * Request hints from Tern.
     *
     * Note that successive calls to getScope may return the same objects, so
     * clients that wish to modify those objects (e.g., by annotating them based
     * on some temporary context) should copy them first. See, e.g.,
     * Session.getHints().
     * 
     * @param {Session} session - the active hinting session
     * @param {Document} document - the document for which scope info is 
     *      desired
     * @return {jQuery.Promise} - The promise will not complete until the tern
     *      hints have completed.
     */
    function requestHints(session, document) {
        var $deferredHints = $.Deferred(),
            hintPromise,
            fnTypePromise,
            sessionType = session.getType(),
            fileInfo = getFileInfo(session),
            offset = getOffset(session, fileInfo,
                            sessionType.showFunctionType ? sessionType.functionCallPos : null);

        maybeReset(session, document);

        hintPromise = getTernHints(fileInfo, offset, sessionType.property);

        if (sessionType.showFunctionType) {
            // Show function sig
            fnTypePromise = getTernFunctionType(fileInfo, offset);
        } else {
            var $fnTypeDeferred = $.Deferred();
            fnTypePromise = $fnTypeDeferred.promise();
            $fnTypeDeferred.resolveWith(null);
        }

        $.when(hintPromise, fnTypePromise).done(
            function (completions, fnType) {
                if (completions.completions) {
                    session.setTernHints(completions.completions);
                    session.setGuesses(null);
                } else {
                    session.setTernHints([]);
                    session.setGuesses(completions.properties);
                }

                session.setFnType(fnType);
                $deferredHints.resolveWith(null);
            }
        ).fail(function () {
            $deferredHints.reject();
        });

        return $deferredHints.promise();
    }

    /**
     *  Track the update area of the current document so we can tell if we can send
     *  partial updates to tern or not.
     *
     * @param {{from: {line:number, ch: number}, to: {line:number, ch: number},
     * text: Array<string>}} changeList - the document changes (since last change or cumlative?)
     */
    function trackChange(changeList) {
        var changed = documentChanges;
        if (changed === null) {
            documentChanges = changed = {from: changeList.from.line, to: changeList.from.line};
        }

        var end = changeList.from.line + (changeList.text.length - 1);
        if (changeList.from.line < changed.to) {
            changed.to = changed.to - (changeList.to.line - end);
        }

        if (end >= changed.to) {
            changed.to = end + 1;
        }

        if (changed.from > changeList.from.line) {
            changed.from = changeList.from.line;
        }
    }

    /*
     * Called each time the file associated with the active editor changes.
     * Marks the file as being dirty.
     *
     * @param {from: {line:number, ch: number}, to: {line:number, ch: number}}
     */
    function handleFileChange(changeList) {
        isDocumentDirty = true;
        trackChange(changeList);
    }

    /**
     * Called each time a new editor becomes active.
     *
     * @param {Session} session - the active hinting session
     * @param {Document} document - the document of the editor that has changed
     * @param {Document} previousDocument - the document of the editor is changing from
     */
    function handleEditorChange(session, document, previousDocument) {

        if (!currentWorker) {
            currentWorker = new TernWorker();
        }
        return currentWorker.handleEditorChange(session, document, previousDocument);
    }

    /**
     * Do some cleanup when a project is closed.
     *
     * We can clean up the web worker we use to calculate hints now, since
     * we know we will need to re-init it in any new project that is opened.  
     */
    function handleProjectClose() {
        if (currentWorker) {
            currentWorker.closeWorker();
            currentWorker = null;
        }
    }

    /**
     *  Read in project preferences when a new project is opened.
     *  Look in the project root directory for a preference file.
     *
     *  @param {string=} projectRootPath - new project root path(optional).
     *  Only needed for unit tests.
     */
    function handleProjectOpen(projectRootPath) {
        initPreferences(projectRootPath);
    }

    exports.getBuiltins = getBuiltins;
    exports.getResolvedPath = getResolvedPath;
    exports.getTernHints = getTernHints;
    exports.handleEditorChange = handleEditorChange;
    exports.requestGuesses = requestGuesses;
    exports.handleFileChange = handleFileChange;
    exports.requestHints = requestHints;
    exports.requestJumptoDef = requestJumptoDef;
    exports.handleProjectClose = handleProjectClose;
    exports.handleProjectOpen = handleProjectOpen;

});

/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50, regexp: true */
/*global define, brackets, $ */

define('Session',['require','exports','module','HintUtils','ScopeManager'],function (require, exports, module) {
    

    var StringMatch     = brackets.getModule("utils/StringMatch"),
        LanguageManager = brackets.getModule("language/LanguageManager"),
        HTMLUtils       = brackets.getModule("language/HTMLUtils"),
        TokenUtils      = brackets.getModule("utils/TokenUtils"),
        HintUtils       = require("HintUtils"),
        ScopeManager    = require("ScopeManager");

    /**
     * Session objects encapsulate state associated with a hinting session
     * and provide methods for updating and querying the session.
     *
     * @constructor
     * @param {Editor} editor - the editor context for the session
     */
    function Session(editor) {
        this.editor = editor;
        this.path = editor.document.file.fullPath;
        this.ternHints = [];
        this.ternGuesses = null;
        this.fnType = null;
        this.builtins = null;
    }

    /**
     *  Get the builtin libraries tern is using.
     *
     * @returns {Array.<string>} - array of library names.
     * @private
     */
    Session.prototype._getBuiltins = function () {
        if (!this.builtins) {
            this.builtins = ScopeManager.getBuiltins();
            this.builtins.push("requirejs.js");     // consider these globals as well.
        }

        return this.builtins;
    };

    /**
     * Get the name of the file associated with the current session
     * 
     * @return {string} - the full pathname of the file associated with the
     *      current session
     */
    Session.prototype.getPath = function () {
        return this.path;
    };

    /**
     * Get the current cursor position.
     *
     * @return {{line: number, ch: number}} - the current cursor position
     */
    Session.prototype.getCursor = function () {
        return this.editor.getCursorPos();
    };

    /**
     * Get the text of a line.
     *
     * @param {number} line - the line number     
     * @return {string} - the text of the line
     */
    Session.prototype.getLine = function (line) {
        var doc = this.editor.document;
        return doc.getLine(line);
    };
    
    /**
     * Get the offset of the current cursor position
     *
     * @return {number} - the offset into the current document of the current
     *      cursor
     */
    Session.prototype.getOffset = function () {
        var cursor = this.getCursor();
        
        return this.getOffsetFromCursor(cursor);
    };
    
    /**
     * Get the offset of a cursor position
     *
     * @param {{line: number, ch: number}} the line/col info
     * @return {number} - the offset into the current document of the cursor
     */
    Session.prototype.getOffsetFromCursor = function (cursor) {
        return this.editor.indexFromPos(cursor);
    };

    /**
     * Get the token at the given cursor position, or at the current cursor
     * if none is given.
     * 
     * @param {?{line: number, ch: number}} cursor - the cursor position
     *      at which to retrieve a token
     * @return {Object} - the CodeMirror token at the given cursor position
     */
    Session.prototype.getToken = function (cursor) {
        var cm = this.editor._codeMirror;

        if (cursor) {
            return cm.getTokenAt(cursor, true);
        } else {
            return cm.getTokenAt(this.getCursor(), true);
        }
    };

    /**
     * Get the token after the one at the given cursor position
     *
     * @param {{line: number, ch: number}} cursor - cursor position before
     *      which a token should be retrieved
     * @return {Object} - the CodeMirror token after the one at the given
     *      cursor position
     */
    Session.prototype.getNextTokenOnLine = function (cursor) {
        cursor = this.getNextCursorOnLine(cursor);
        if (cursor) {
            return this.getToken(cursor);
        }

        return null;
    };

    /**
     * Get the next cursor position on the line, or null if there isn't one.
     * 
     * @return {?{line: number, ch: number}} - the cursor position
     *      immediately following the current cursor position, or null if
     *      none exists.
     */
    Session.prototype.getNextCursorOnLine = function (cursor) {
        var doc     = this.editor.document,
            line    = doc.getLine(cursor.line);

        if (cursor.ch < line.length) {
            return {
                ch  : cursor.ch + 1,
                line: cursor.line
            };
        } else {
            return null;
        }
    };
    
    /**
     * Get the token before the one at the given cursor position
     * 
     * @param {{line: number, ch: number}} cursor - cursor position after
     *      which a token should be retrieved
     * @return {Object} - the CodeMirror token before the one at the given
     *      cursor position
     */
    Session.prototype._getPreviousToken = function (cursor) {
        var token   = this.getToken(cursor),
            prev    = token,
            doc     = this.editor.document;

        do {
            if (prev.start < cursor.ch) {
                cursor.ch = prev.start;
            } else if (prev.start > 0) {
                cursor.ch = prev.start - 1;
            } else if (cursor.line > 0) {
                cursor.ch = doc.getLine(cursor.line - 1).length;
                cursor.line--;
            } else {
                break;
            }
            prev = this.getToken(cursor);
        } while (prev.string.trim() === "");
        
        return prev;
    };

    /**
     * Get the token after the one at the given cursor position
     * 
     * @param {{line: number, ch: number}} cursor - cursor position after
     *      which a token should be retrieved
     * @param {boolean} skipWhitespace - true if this should skip over whitespace tokens 
     * @return {Object} - the CodeMirror token after the one at the given
     *      cursor position
     */
    Session.prototype.getNextToken = function (cursor, skipWhitespace) {
        var token   = this.getToken(cursor),
            next    = token,
            doc     = this.editor.document;

        do {
            if (next.end > cursor.ch) {
                cursor.ch = next.end;
            } else if (next.end < doc.getLine(cursor.line).length) {
                cursor.ch = next.end + 1;
            } else if (doc.getLine(cursor.line + 1)) {
                cursor.ch = 0;
                cursor.line++;
            } else {
                next = null;
                break;
            }
            next = this.getToken(cursor);
        } while (skipWhitespace && next.string.trim() === "");
        
        return next;
    };
    
    /**
     * Calculate a query string relative to the current cursor position
     * and token. E.g., from a state "identi<cursor>er", the query string is
     * "identi".
     * 
     * @return {string} - the query string for the current cursor position
     */
    Session.prototype.getQuery = function () {
        var cursor  = this.getCursor(),
            token   = this.getToken(cursor),
            query   = "",
            start   = cursor.ch,
            end     = start;

        if (token) {
            var line = this.getLine(cursor.line);
            while (start > 0) {
                if (HintUtils.maybeIdentifier(line[start - 1])) {
                    start--;
                } else {
                    break;
                }
            }

            query = line.substring(start, end);
        }

        return query;
    };

    /**
     * Find the context of a property lookup. For example, for a lookup 
     * foo(bar, baz(quux)).prop, foo is the context.
     * 
     * @param {{line: number, ch: number}} cursor - the cursor position
     *      at which context information is to be retrieved
     * @param {number} depth - the current depth of the parenthesis stack, or
     *      undefined if the depth is 0.
     * @return {string} - the context for the property that was looked up
     */
    Session.prototype.getContext = function (cursor, depth) {
        var token = this.getToken(cursor);

        if (depth === undefined) {
            depth = 0;
        }

        if (token.string === ")") {
            this._getPreviousToken(cursor);
            return this.getContext(cursor, ++depth);
        } else if (token.string === "(") {
            this._getPreviousToken(cursor);
            return this.getContext(cursor, --depth);
        } else {
            if (depth > 0 || token.string === ".") {
                this._getPreviousToken(cursor);
                return this.getContext(cursor, depth);
            } else {
                return token.string;
            }
        }
    };

    /**
     * @return {{line:number, ch:number}} - the line, col info for where the previous "."
     *      in a property lookup occurred, or undefined if no previous "." was found.
     */
    Session.prototype.findPreviousDot = function () {
        var cursor = this.getCursor(),
            token = this.getToken(cursor);
        
        // If the cursor is right after the dot, then the current token will be "."
        if (token && token.string === ".") {
            return cursor;
        } else {
            // If something has been typed like 'foo.b' then we have to look back 2 tokens
            // to get past the 'b' token
            token = this._getPreviousToken(cursor);
            if (token && token.string === ".") {
                return cursor;
            }
        }
        return undefined;
    };
    
    /**
     * Get the type of the current session, i.e., whether it is a property
     * lookup and, if so, what the context of the lookup is.
     * 
     * @return {{property: boolean, 
                 showFunctionType:boolean, 
                 context: string,
                 functionCallPos: {line:number, ch:number}}} - an Object consisting
     *      of a {boolean} "property" that indicates whether or not the type of
     *      the session is a property lookup, and a {string} "context" that
     *      indicates the object context (as described in getContext above) of
     *      the property lookup, or null if there is none. The context is
     *      always null for non-property lookups.
     *      a {boolean} "showFunctionType" indicating if the function type should
     *      be displayed instead of normal hints.  If "showFunctionType" is true, then
     *      then "functionCallPos" will be an object with line & col information of the
     *      function being called     
     */
    Session.prototype.getType = function () {
        function getLexicalState(token) {
            if (token.state.lexical) {
                // in a javascript file this is just in the state field
                return token.state.lexical;
            } else if (token.state.localState && token.state.localState.lexical) {
                // inline javascript in an html file will have this in 
                // the localState field
                return token.state.localState.lexical;
            }
        }
        var propertyLookup   = false,
            inFunctionCall   = false,
            showFunctionType = false,
            context          = null,
            cursor           = this.getCursor(),
            functionCallPos,
            token            = this.getToken(cursor),
            lexical;

        if (token) {
            // if this token is part of a function call, then the tokens lexical info
            // will be annotated with "call"
            lexical = getLexicalState(token);
            if (lexical.info === "call") {
                inFunctionCall = true;
                if (this.getQuery().length > 0) {
                    inFunctionCall = false;
                    showFunctionType = false;
                } else {
                    showFunctionType = true;
                    // we need to find the location of the called function so that we can request the functions type.
                    // the token's lexical info will contain the column where the open "(" for the
                    // function call occurrs, but for whatever reason it does not have the line, so 
                    // we have to walk back and try to find the correct location.  We do this by walking
                    // up the lines starting with the line the token is on, and seeing if any of the lines
                    // have "(" at the column indicated by the tokens lexical state.  
                    // We walk back 9 lines, as that should be far enough to find most function calls,
                    // and it will prevent us from walking back thousands of lines if something went wrong.
                    // there is nothing magical about 9 lines, and it can be adjusted if it doesn't seem to be
                    // working well
                    var col = lexical.column,
                        line,
                        e,
                        found;
                    for (line = this.getCursor().line, e = Math.max(0, line - 9), found = false; line >= e; --line) {
                        if (this.getLine(line).charAt(col) === "(") {
                            found = true;
                            break;
                        }
                    }
                    if (found) {
                        functionCallPos = {line: line, ch: col};
                    }
                }
            }
            if (token.type === "property") {
                propertyLookup = true;
            }

            cursor = this.findPreviousDot();
            if (cursor) {
                propertyLookup = true;
                context = this.getContext(cursor);
            }
            if (propertyLookup) { showFunctionType = false; }
        }
        
        return {
            property: propertyLookup,
            inFunctionCall: inFunctionCall,
            showFunctionType: showFunctionType,
            functionCallPos: functionCallPos,
            context: context
        };
    };
    
    // Comparison function used for sorting that does a case-insensitive string
    // comparison on the "value" field of both objects. Unlike a normal string
    // comparison, however, this sorts leading "_" to the bottom, given that a
    // leading "_" usually denotes a private value.
    function penalizeUnderscoreValueCompare(a, b) {
        var aName = a.value.toLowerCase(), bName = b.value.toLowerCase();
        // this sort function will cause _ to sort lower than lower case
        // alphabetical letters
        if (aName[0] === "_" && bName[0] !== "_") {
            return 1;
        } else if (bName[0] === "_" && aName[0] !== "_") {
            return -1;
        }
        if (aName < bName) {
            return -1;
        } else if (aName > bName) {
            return 1;
        }
        return 0;
    }
    
    /**
     * Get a list of hints for the current session using the current scope
     * information. 
     *
     * @param {string} query - the query prefix
     * @param {StringMatcher} matcher - the class to find query matches and sort the results
     * @returns {hints: Array.<string>, needGuesses: boolean} - array of
     * matching hints. If needGuesses is true, then the caller needs to
     * request guesses and call getHints again.
     */
    Session.prototype.getHints = function (query, matcher) {

        if (query === undefined) {
            query = "";
        }

        var MAX_DISPLAYED_HINTS = 500,
            type                = this.getType(),
            builtins            = this._getBuiltins(),
            needGuesses         = false,
            hints;

        /**
         *  Is the origin one of the builtin files.
         *
         * @param {string} origin
         */
        function isBuiltin(origin) {
            return builtins.indexOf(origin) !== -1;
        }

        /**
         *  Filter an array hints using a given query and matcher.
         *  The hints are returned in the format of the matcher.
         *  The matcher returns the value in the "label" property,
         *  the match score in "matchGoodness" property.
         *
         * @param {Array} hints - array of hints
         * @param {StringMatcher} matcher
         * @returns {Array} - array of matching hints.
         */
        function filterWithQueryAndMatcher(hints, matcher) {
            var matchResults = $.map(hints, function (hint) {
                var searchResult = matcher.match(hint.value, query);
                if (searchResult) {
                    searchResult.value = hint.value;
                    searchResult.guess = hint.guess;

                    if (hint.keyword !== undefined) {
                        searchResult.keyword = hint.keyword;
                    }

                    if (hint.literal !== undefined) {
                        searchResult.literal = hint.literal;
                    }

                    if (hint.depth !== undefined) {
                        searchResult.depth = hint.depth;
                    }

                    if (!type.property && !type.showFunctionType && hint.origin &&
                            isBuiltin(hint.origin)) {
                        searchResult.builtin = 1;
                    } else {
                        searchResult.builtin = 0;
                    }
                }

                return searchResult;
            });

            return matchResults;
        }

        if (type.property) {
            hints = this.ternHints || [];
            hints = filterWithQueryAndMatcher(hints, matcher);

            // If there are no hints then switch over to guesses.
            if (hints.length === 0) {
                if (this.ternGuesses) {
                    hints = filterWithQueryAndMatcher(this.ternGuesses, matcher);
                } else {
                    needGuesses = true;
                }
            }

            StringMatch.multiFieldSort(hints, [ "matchGoodness", penalizeUnderscoreValueCompare ]);
        } else if (type.showFunctionType) {
            hints = this.getFunctionTypeHint();
        } else {     // identifiers, literals, and keywords
            hints = this.ternHints || [];
            hints = hints.concat(HintUtils.LITERALS);
            hints = hints.concat(HintUtils.KEYWORDS);
            hints = filterWithQueryAndMatcher(hints, matcher);
            StringMatch.multiFieldSort(hints, [ "matchGoodness", "depth", "builtin", penalizeUnderscoreValueCompare ]);
        }

        if (hints.length > MAX_DISPLAYED_HINTS) {
            hints = hints.slice(0, MAX_DISPLAYED_HINTS);
        }

        return {hints: hints, needGuesses: needGuesses};
    };
    
    Session.prototype.setTernHints = function (newHints) {
        this.ternHints = newHints;
    };

    Session.prototype.setGuesses = function (newGuesses) {
        this.ternGuesses = newGuesses;
    };

    Session.prototype.setFnType = function (newFnType) {
        this.fnType = newFnType;
    };
    
    /**
     * Get the function type hint.  This will format the hint so
     * that it has the called variable name instead of just "fn()".
     */
    Session.prototype.getFunctionTypeHint = function () {
        var fnHint = this.fnType,
            hints = [];
        
        if (fnHint && (fnHint.substring(0, 3) === "fn(")) {
            var sessionType = this.getType(),
                cursor = sessionType.functionCallPos,
                token = cursor ? this.getToken(cursor) : undefined,
                varName;
            if (token &&
                    // only change the 'fn' when the token looks like a function
                    // name, and isn't some other kind of expression
                    (token.type === "variable" ||
                     token.type === "variable-2" ||
                     token.type === "property")) {
                varName = token.string;
                if (varName) {
                    fnHint = varName + fnHint.substr(2);
                }
            }
            hints[0] = {value: fnHint, positions: []};
        }
        
        hints.handleWideResults = true;
        return hints;
    };

    /**
     * Get the javascript text of the file open in the editor for this Session.
     * For a javascript file, this is just the text of the file.  For an HTML file,
     * this will be only the text in the <script> tags.  This is so that we can pass
     * just the javascript text to tern, and avoid confusing it with HTML tags, since it
     * only knows how to parse javascript.
     * @return {string} - the "javascript" text that can be sent to Tern.
     */
    Session.prototype.getJavascriptText = function () {
        if (LanguageManager.getLanguageForPath(this.editor.document.file.fullPath).getId() === "html") {
            // HTML file - need to send back only the bodies of the
            // <script> tags
            var text = "",
                offset = this.getOffset(),
                cursor = this.getCursor(),
                editor = this.editor,
                scriptBlocks = HTMLUtils.findBlocks(editor, "javascript");
            
            // Add all the javascript text
            // For non-javascript blocks we replace everything except for newlines
            // with whitespace.  This is so that the offset and cursor positions
            // we get from the document still work.  
            // Alternatively we could strip the non-javascript text, and modify the offset,
            // and/or cursor, but then we have to remember how to reverse the translation
            // to support jump-to-definition
            var htmlStart = {line: 0, ch: 0};
            scriptBlocks.forEach(function (scriptBlock) {
                var start = scriptBlock.start,
                    end = scriptBlock.end;
                
                // get the preceding html text, and replace it with whitespace
                var htmlText = editor.document.getRange(htmlStart, start);
                htmlText = htmlText.replace(/./g, " ");

                htmlStart = end;
                text += htmlText + scriptBlock.text;
            });
            
            return text;
        } else {
            // Javascript file, just return the text
            return this.editor.document.getText();
        }
    };
    
    /**
     * Deterimine if the cursor is located in the name of a function declaration.
     * This is so we can suppress hints when in a funtion name, as we do for variable and
     * parameter declarations, but we can tell those from the token itself rather than having
     * to look at previous tokens.
     * 
     * @return {boolean} - true if the current cursor position is in the name of a function decl.
     */
    Session.prototype.isFunctionName = function () {
        var cursor = this.getCursor(),
            token  = this.getToken(cursor),
            prevToken = this._getPreviousToken(cursor);
        
        return prevToken.string === "function";
    };
    
    module.exports = Session;
});

/*
 * Copyright (c) 2013 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50 */
/*global define, brackets, $ */

define('main',['require','exports','module','HintUtils','ScopeManager','Session','thirdparty/acorn/acorn'],function (require, exports, module) {
    

    var CodeHintManager = brackets.getModule("editor/CodeHintManager"),
        EditorManager   = brackets.getModule("editor/EditorManager"),
        DocumentManager = brackets.getModule("document/DocumentManager"),
        Commands        = brackets.getModule("command/Commands"),
        CommandManager  = brackets.getModule("command/CommandManager"),
        Menus           = brackets.getModule("command/Menus"),
        Strings         = brackets.getModule("strings"),
        AppInit         = brackets.getModule("utils/AppInit"),
        ExtensionUtils  = brackets.getModule("utils/ExtensionUtils"),
        PerfUtils       = brackets.getModule("utils/PerfUtils"),
        StringUtils     = brackets.getModule("utils/StringUtils"),
        StringMatch     = brackets.getModule("utils/StringMatch"),
        LanguageManager = brackets.getModule("language/LanguageManager"),
        ProjectManager  = brackets.getModule("project/ProjectManager"),
        HintUtils       = require("HintUtils"),
        ScopeManager    = require("ScopeManager"),
        Session         = require("Session"),
        Acorn           = require("thirdparty/acorn/acorn");

    var session      = null,  // object that encapsulates the current session state
        cachedCursor = null,  // last cursor of the current hinting session
        cachedHints  = null,  // sorted hints for the current hinting session
        cachedType   = null,  // describes the lookup type and the object context
        cachedToken  = null,  // the token used in the current hinting session
        matcher      = null,  // string matcher for hints
        ignoreChange;         // can ignore next "change" event if true;

    /**
     *  Get the value of current session.
     *  Used for unit testing.
     * @returns {Session} - the current session.
     */
    function getSession() {
        return session;
    }

    /**
     * Creates a hint response object. Filters the hint list using the query
     * string, formats the hints for display, and returns a hint response
     * object according to the CodeHintManager's API for code hint providers.
     *
     * @param {Array.<Object>} hints - hints to be included in the response
     * @param {string} query - querystring with which to filter the hint list
     * @param {Object} type - the type of query, property vs. identifier
     * @return {Object} - hint response as defined by the CodeHintManager API 
     */
    function getHintResponse(hints, query, type) {

        var trimmedQuery,
            formattedHints;

        /*
         * Returns a formatted list of hints with the query substring
         * highlighted.
         * 
         * @param {Array.<Object>} hints - the list of hints to format
         * @param {string} query - querystring used for highlighting matched
         *      poritions of each hint
         * @return {Array.<jQuery.Object>} - array of hints formatted as jQuery
         *      objects
         */
        function formatHints(hints, query) {
            return hints.map(function (token) {
                var $hintObj    = $("<span>").addClass("brackets-js-hints");

                // level indicates either variable scope or property confidence
                if (!type.property && !token.builtin && token.depth !== undefined) {
                    switch (token.depth) {
                    case 0:
                        $hintObj.addClass("priority-high");
                        break;
                    case 1:
                        $hintObj.addClass("priority-medium");
                        break;
                    case 2:
                        $hintObj.addClass("priority-low");
                        break;
                    default:
                        $hintObj.addClass("priority-lowest");
                        break;
                    }
                }

                if (token.guess) {
                    $hintObj.addClass("guess-hint");
                }

                // is the token a keyword?
                if (token.keyword) {
                    $hintObj.addClass("keyword-hint");
                }

                if (token.literal) {
                    $hintObj.addClass("literal-hint");
                }
             
                // highlight the matched portion of each hint
                if (token.stringRanges) {
                    token.stringRanges.forEach(function (item) {
                        if (item.matched) {
                            $hintObj.append($("<span>")
                                .append(StringUtils.htmlEscape(item.text))
                                .addClass("matched-hint"));
                        } else {
                            $hintObj.append(StringUtils.htmlEscape(item.text));
                        }
                    });
                } else {
                    $hintObj.text(token.value);
                }

                $hintObj.data("token", token);
                
                return $hintObj;
            });
        }

        // trim leading and trailing string literal delimiters from the query
        if (query.indexOf(HintUtils.SINGLE_QUOTE) === 0 ||
                query.indexOf(HintUtils.DOUBLE_QUOTE) === 0) {
            trimmedQuery = query.substring(1);
            if (trimmedQuery.lastIndexOf(HintUtils.DOUBLE_QUOTE) === trimmedQuery.length - 1 ||
                    trimmedQuery.lastIndexOf(HintUtils.SINGLE_QUOTE) === trimmedQuery.length - 1) {
                trimmedQuery = trimmedQuery.substring(0, trimmedQuery.length - 1);
            }
        } else {
            trimmedQuery = query;
        }

        if (hints) {
            formattedHints = formatHints(hints, trimmedQuery);
        } else {
            formattedHints = [];
        }
        
        return {
            hints: formattedHints,
            match: null, // the CodeHintManager should not format the results
            selectInitial: true,
            handleWideResults: hints.handleWideResults
        };
    }

    /**
     * @constructor
     */
    function JSHints() {
    }

    /**
     * determine if the cached hint information should be invalidated and re-calculated
     *
     * @param {Session} session - the active hinting session
     * @return {boolean} - true if the hints should be recalculated
     */
    JSHints.prototype.needNewHints = function (session) {
        var cursor  = session.getCursor(),
            type    = session.getType();

        return !cachedHints || !cachedCursor || !cachedType ||
                cachedCursor.line !== cursor.line ||
                type.property !== cachedType.property ||
                type.context !== cachedType.context ||
                type.showFunctionType !== cachedType.showFunctionType ||
                (type.functionCallPos && cachedType.functionCallPos &&
                    type.functionCallPos.ch !== cachedType.functionCallPos.ch);
    };

    /**
     *  Cache the hints and the hint's context.
     *
     *  @param {Array.<string>} hints - array of hints
     *  @param {{line:number, ch:number}} cursor - the location where the hints
     *  were created.
     * @param {{property: boolean,
                showFunctionType:boolean,
                context: string,
                functionCallPos: {line:number, ch:number}}} type -
     *  type information about the hints
     *  @param {Object} token - CodeMirror token
     */
    function setCachedHintContext(hints, cursor, type, token) {
        cachedHints = hints;
        cachedCursor = cursor;
        cachedType = type;
        cachedToken = token;
    }

    /**
     *  Reset cached hint context.
     */
    function resetCachedHintContext() {
        cachedHints = null;
        cachedCursor = null;
        cachedType = null;
        cachedToken =  null;
    }

    /**
     *  Have conditions have changed enough to justify closing the hints popup?
     *
     * @param {Session} session - the active hinting session
     * @return {boolean} - true if the hints popup should be closed.
     */
    JSHints.prototype.shouldCloseHints = function (session) {

        // close if the token className has changed then close the hints.
        var cursor = session.getCursor(),
            token = session.getToken(cursor),
            lastToken = cachedToken;

        // if the line has changed, then close the hints
        if (!cachedCursor || cursor.line !== cachedCursor.line) {
            return true;
        }

        if (token.type === null) {
            token = session.getNextTokenOnLine(cursor);
        }

        if (lastToken && lastToken.type === null) {
            lastToken = session.getNextTokenOnLine(cachedCursor);
        }

        // Both of the tokens should never be null (happens when token is off
        // the end of the line), so one is null then close the hints.
        if (!lastToken || !token ||
                token.type !== lastToken.type) {
            return true;
        }

        // Test if one token string is a prefix of the other.
        // If one is a prefix of the other then consider it the
        // same token and don't close the hints.
        if (token.string.length >= lastToken.string.length) {
            return token.string.indexOf(lastToken.string) !== 0;
        } else {
            return lastToken.string.indexOf(token.string) !== 0;
        }
    };

    /**
     * @return {boolean} - true if the document is a html file
     */
    function isHTMLFile(document) {
        var languageID = LanguageManager.getLanguageForPath(document.file.fullPath).getId();
        return languageID === "html";
    }
    
    function isInlineScript(editor) {
        return editor.getModeForSelection() === "javascript";
    }

    /**
     *  Create a new StringMatcher instance, if needed.
     *
     * @returns {StringMatcher} - a StringMatcher instance.
     */
    function getStringMatcher() {
        if (!matcher) {
            matcher = new StringMatch.StringMatcher({
                preferPrefixMatches: true
            });
        }

        return matcher;
    }

    /**
     *  Check if a hint response is pending.
     *
     * @param {jQuery.Deferred} deferredHints - deferred hint response
     * @returns {boolean} - true if deferred hints are pending, false otherwise.
     */
    function hintsArePending(deferredHints) {
        return (deferredHints && !deferredHints.hasOwnProperty("hints") &&
            deferredHints.state() === "pending");
    }

    /**
     *  Common code to get the session hints. Will get guesses if there were
     *  no completions for the query.
     *
     * @param {string} query - user text to search hints with
     *  @param {{line:number, ch:number}} cursor - the location where the hints
     *  were created.
     * @param {{property: boolean,
                 showFunctionType:boolean,
                 context: string,
                 functionCallPos: {line:number, ch:number}}} type -
     *  type information about the hints
     *  @param {Object} token - CodeMirror token
     * @param {jQuery.Deferred=} $deferredHints - existing Deferred we need to
     * resolve (optional). If not supplied a new Deferred will be created if
     * needed.
     * @return {Object + jQuery.Deferred} - hint response (immediate or
     *     deferred) as defined by the CodeHintManager API
     */
    function getSessionHints(query, cursor, type, token, $deferredHints) {

        var hintResults = session.getHints(query, getStringMatcher());
        if (hintResults.needGuesses) {
            var guessesResponse = ScopeManager.requestGuesses(session,
                session.editor.document);

            if (!$deferredHints) {
                $deferredHints = $.Deferred();
            }

            guessesResponse.done(function () {
                if (hintsArePending($deferredHints)) {
                    hintResults = session.getHints(query, getStringMatcher());
                    setCachedHintContext(hintResults.hints, cursor, type, token);
                    var hintResponse = getHintResponse(cachedHints, query, type);
                    $deferredHints.resolveWith(null, [hintResponse]);
                }
            }).fail(function () {
                if (hintsArePending($deferredHints)) {
                    $deferredHints.reject();
                }
            });

            return $deferredHints;
        } else if (hintsArePending($deferredHints)) {
            setCachedHintContext(hintResults.hints, cursor, type, token);
            var hintResponse    = getHintResponse(cachedHints, query, type);
            $deferredHints.resolveWith(null, [hintResponse]);
            return null;
        } else {
            setCachedHintContext(hintResults.hints, cursor, type, token);
            return getHintResponse(cachedHints, query, type);
        }
    }

    /**
     * Determine whether hints are available for a given editor context
     * 
     * @param {Editor} editor - the current editor context
     * @param {string} key - charCode of the last pressed key
     * @return {boolean} - can the provider provide hints for this session?
     */
    JSHints.prototype.hasHints = function (editor, key) {
        if (session && HintUtils.hintableKey(key)) {
            
            if (isHTMLFile(session.editor.document)) {
                if (!isInlineScript(session.editor)) {
                    return false;
                }
            }
            var cursor  = session.getCursor(),
                token   = session.getToken(cursor);

            // don't autocomplete within strings or comments, etc.
            if (token && HintUtils.hintable(token)) {
                if (session.isFunctionName()) {
                    return false;
                }
                var offset = session.getOffset(),
                    type    = session.getType(),
                    query   = session.getQuery();

                if (this.needNewHints(session)) {
                    resetCachedHintContext();
                    matcher = null;
                }
                return true;
            }
        }
        return false;
    };

    /**
      * Return a list of hints, possibly deferred, for the current editor 
      * context
      * 
      * @param {string} key - charCode of the last pressed key
      * @return {Object + jQuery.Deferred} - hint response (immediate or
      *     deferred) as defined by the CodeHintManager API
      */
    JSHints.prototype.getHints = function (key) {
        var cursor = session.getCursor(),
            token = session.getToken(cursor);

        if (token && HintUtils.hintableKey(key) && HintUtils.hintable(token)) {
            var type    = session.getType(),
                query   = session.getQuery();

            // If the hint context is changed and the hints are open, then
            // close the hints by returning null;
            if (CodeHintManager.isOpen() && this.shouldCloseHints(session)) {
                return null;
            }

            // Compute fresh hints if none exist, or if the session
            // type has changed since the last hint computation
            if (this.needNewHints(session)) {
                if (key) {
                    ScopeManager.handleFileChange({from: cursor, to: cursor, text: [key]});
                    ignoreChange = true;
                }

                var scopeResponse   = ScopeManager.requestHints(session, session.editor.document),
                    $deferredHints = $.Deferred();

                scopeResponse.done(function () {
                    if (hintsArePending($deferredHints)) {
                        getSessionHints(query, cursor, type, token, $deferredHints);
                    }
                }).fail(function () {
                    if (hintsArePending($deferredHints)) {
                        $deferredHints.reject();
                    }
                });

                return $deferredHints;
            }

            if (cachedHints) {
                return getSessionHints(query, cursor, type, token);
            }
        }

        return null;
    };

    /**
     * Inserts the hint selected by the user into the current editor.
     * 
     * @param {jQuery.Object} $hintObj - hint object to insert into current editor
     * @return {boolean} - should a new hinting session be requested 
     *      immediately after insertion?
     */
    JSHints.prototype.insertHint = function ($hintObj) {
        var hint        = $hintObj.data("token"),
            completion  = hint.value,
            cursor      = session.getCursor(),
            token       = session.getToken(cursor),
            query       = session.getQuery(),
            start       = {line: cursor.line, ch: cursor.ch - query.length},
            end         = {line: cursor.line, ch: cursor.ch},
            delimiter;

        if (session.getType().showFunctionType) {
            // function types show up as hints, so don't insert anything
            // if we were displaying a function type            
            return false;
        }
        
        if (session.getType().property) {
            // if we're inserting a property name, we need to make sure the 
            // hint is a valid property name.  
            // to check this, run the hint through Acorns tokenizer
            // it should result in one token, and that token should either be 
            // a 'name' or a 'keyword', as javascript allows keywords as property names
            var tokenizer = Acorn.tokenize(completion);
            var currentToken = tokenizer(),
                invalidPropertyName = false;
            
            // the name is invalid if the hint is not a 'name' or 'keyword' token
            if (currentToken.type !== Acorn.tokTypes.name && !currentToken.type.keyword) {
                invalidPropertyName = true;
            } else {
                // check for a second token - if there is one (other than 'eof')
                // then the hint isn't a valid property name either
                currentToken = tokenizer();
                if (currentToken.type !== Acorn.tokTypes.eof) {
                    invalidPropertyName = true;
                }
            }
            
            if (invalidPropertyName) {
                // need to walk back to the '.' and replace
                // with '["<hint>"]
                var dotCursor = session.findPreviousDot();
                if (dotCursor) {
                    completion = "[\"" + completion + "\"]";
                    start.line = dotCursor.line;
                    start.ch = dotCursor.ch - 1;
                }
            }
        }
        // Replace the current token with the completion
        // HACK (tracking adobe/brackets#1688): We talk to the private CodeMirror instance
        // directly to replace the range instead of using the Document, as we should. The
        // reason is due to a flaw in our current document synchronization architecture when
        // inline editors are open.
        session.editor._codeMirror.replaceRange(completion, start, end);

        // Return false to indicate that another hinting session is not needed
        return false;
    };

     // load the extension
    AppInit.appReady(function () {

        /*
         * When the editor is changed, reset the hinting session and cached 
         * information, and reject any pending deferred requests.
         * 
         * @param {Editor} editor - editor context to be initialized.
         * @param {Editor} previousEditor - the previous editor.
         */
        function initializeSession(editor, previousEditor) {
            session = new Session(editor);
            ScopeManager.handleEditorChange(session, editor.document,
                previousEditor ? previousEditor.document : null);
            cachedHints = null;
        }

        /*
         * Install editor change listeners
         * 
         * @param {Editor} editor - editor context on which to listen for
         *      changes
         * @param {Editor} previousEditor - the previous editor
         */
        function installEditorListeners(editor, previousEditor) {
            // always clean up cached scope and hint info
            resetCachedHintContext();

            if (editor && HintUtils.isSupportedLanguage(LanguageManager.getLanguageForPath(editor.document.file.fullPath).getId())) {
                initializeSession(editor, previousEditor);
                $(editor)
                    .on(HintUtils.eventName("change"), function (event, editor, changeList) {
                        if (!ignoreChange) {
                            ScopeManager.handleFileChange(changeList);
                        }
                        ignoreChange = false;
                    });
            } else {
                session = null;
            }
        }

        /*
         * Uninstall editor change listeners
         * 
         * @param {Editor} editor - editor context on which to stop listening
         *      for changes
         */
        function uninstallEditorListeners(editor) {
            $(editor)
                .off(HintUtils.eventName("change"));
        }

        /*
         * Handle the activeEditorChange event fired by EditorManager.
         * Uninstalls the change listener on the previous editor
         * and installs a change listener on the new editor.
         * 
         * @param {Event} event - editor change event (ignored)
         * @param {Editor} current - the new current editor context
         * @param {Editor} previous - the previous editor context
         */
        function handleActiveEditorChange(event, current, previous) {
            uninstallEditorListeners(previous);
            installEditorListeners(current, previous);
        }
        
        /*
         * Handle JumptoDefiniton menu/keyboard command.
         */
        function handleJumpToDefinition() {
            var offset,
                handleJumpResponse;

                        
            // Only provide jump-to-definition results when cursor is in JavaScript content
            if (session.editor.getModeForSelection() !== "javascript") {
                return null;
            }

            var result = new $.Deferred();

            /**
             * Make a jump-to-def request based on the session and offset passed in.
             * @param {Session} session - the session
             * @param {number} offset - the offset of where to jump from
             */
            function requestJumpToDef(session, offset) {
                var response = ScopeManager.requestJumptoDef(session, session.editor.document, offset);
    
                if (response.hasOwnProperty("promise")) {
                    response.promise.done(handleJumpResponse).fail(function () {
                        result.reject();
                    });
                }
            }
            

            /**
             * Sets the selection to move the cursor to the result position.
             * Assumes that the editor has already changed files, if necessary.
             *
             * Additionally, this will check to see if the selection looks like an
             * assignment to a member expression - if it is, and the type is a function,
             * then we will attempt to jump to the RHS of the expression.
             *
             * 'exports.foo = foo'
             *
             * if the selection is 'foo' in 'exports.foo', then we will attempt to jump to def
             * on the rhs of the assignment.
             *
             * @param {number} start - the start of the selection
             * @param {number} end - the end of the selection
             * @param {boolean} isFunction - true if we are jumping to the source of a function def
             */
            function setJumpSelection(start, end, isFunction) {
                
                /**
                 * helper function to decide if the tokens on the RHS of an assignment
                 * look like an identifier, or member expr.
                 */
                function validIdOrProp(token) {
                    if (!token) {
                        return false;
                    }
                    if (token.string === ".") {
                        return true;
                    }
                    var type = token.type;
                    if (type === "variable-2" || type === "variable" || type === "property") {
                        return true;
                    }
                    
                    return false;
                }
                
                var madeNewRequest = false;
                
                if (isFunction) {
                    // When jumping to function defs, follow the chain back
                    // to get to the original function def
                    var cursor = {line: end.line, ch: end.ch},
                        prev = session._getPreviousToken(cursor),
                        next,
                        token,
                        offset;
    
                    // see if the selection is preceded by a '.', indicating we're in a member expr
                    if (prev.string === ".") {
                        cursor = {line: end.line, ch: end.ch};
                        next = session.getNextToken(cursor, true);
                        // check if the next token indicates an assignment
                        if (next && next.string === "=") {
                            next = session.getNextToken(cursor, true);
                            // find the last token of the identifier, or member expr
                            while (validIdOrProp(next)) {
                                offset = session.getOffsetFromCursor({line: cursor.line, ch: next.end});
                                next = session.getNextToken(cursor, false);
                            }
                            if (offset) {
                                // trigger another jump to def based on the offset of the RHS
                                requestJumpToDef(session, offset);
                                madeNewRequest = true;
                            }
                        }
                    }
                }
                // We didn't make a new jump-to-def request, so we can resolve the promise
                // and set the selection
                if (!madeNewRequest) {
                    // set the selection
                    session.editor.setSelection(start, end, true);
                    result.resolve(true);
                }
            }

            /**
             * handle processing of the completed jump-to-def request.              
             * will open the appropriate file, and set the selection based
             * on the response.
             */
            handleJumpResponse = function (jumpResp) {

                if (jumpResp.resultFile) {
                    if (jumpResp.resultFile !== jumpResp.file) {
                        var resolvedPath = ScopeManager.getResolvedPath(jumpResp.resultFile);
                        if (resolvedPath) {
                            CommandManager.execute(Commands.FILE_OPEN, {fullPath: resolvedPath})
                                .done(function () {
                                    setJumpSelection(jumpResp.start, jumpResp.end, jumpResp.isFunction);
                                });
                        }
                    } else {
                        setJumpSelection(jumpResp.start, jumpResp.end, jumpResp.isFunction);
                    }
                } else {
                    result.reject();
                }
            };
            
            offset = session.getOffset();
            // request a jump-to-def
            requestJumpToDef(session, offset);
            
            return result.promise();
        }

        /*
         * Helper for QuickEdit jump-to-definition request.
         */
        function quickEditHelper() {
            var offset     = session.getCursor(),
                response   = ScopeManager.requestJumptoDef(session, session.editor.document, offset);

            return response;
        }

        // Register quickEditHelper.
        brackets._jsCodeHintsHelper = quickEditHelper;
  
        ExtensionUtils.loadStyleSheet(module, "styles/brackets-js-hints.css");
        
        // uninstall/install change listener as the active editor changes
        $(EditorManager)
            .on(HintUtils.eventName("activeEditorChange"),
                handleActiveEditorChange);
        
        $(ProjectManager).on("beforeProjectClose", function () {
            ScopeManager.handleProjectClose();
        });

        $(ProjectManager).on("projectOpen", function () {
            ScopeManager.handleProjectOpen();
        });

        // immediately install the current editor
        installEditorListeners(EditorManager.getActiveEditor());

        // init
        EditorManager.registerJumpToDefProvider(handleJumpToDefinition);

        var jsHints = new JSHints();
        CodeHintManager.registerHintProvider(jsHints, HintUtils.SUPPORTED_LANGUAGES, 0);

        // for unit testing
        exports.getSession = getSession;
        exports.jsHintProvider = jsHints;
        exports.initializeSession = initializeSession;
        exports.handleJumpToDefinition = handleJumpToDefinition;
    });
});
