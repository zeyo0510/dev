
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

define('text!SpecialChars.json',[],function () { return '[\n    "&#33",\n    "&#35",\n    "&#36",\n    "&#37",\n    "&#39",\n    "&#40",\n    "&#41",\n    "&#42",\n    "&#43",\n    "&#44",\n    "&#45",\n    "&#46",\n    "&#58",\n    "&#59",\n    "&#61",\n    "&#63",\n    "&#64",\n    "&#91",\n    "&#92",\n    "&#93",\n    "&#94",\n    "&#95",\n    "&#96",\n    "&#123",\n    "&#124",\n    "&#125",\n    "&#126",\n    "&#8226",\n    "&#9679",\n    \n    "&aacute",\n    "&Aacute",\n    "&acirc",\n    "&Acirc",\n    "&acute",\n    "&aelig",\n    "&AElig",\n    "&agrave",\n    "&Agrave",\n    "&alefsym",\n    "&alpha",\n    "&Alpha",\n    "&amp",\n    "&and",\n    "&ang",\n    "&aring",\n    "&Aring",\n    "&asymp",\n    "&atilde",\n    "&Atilde",\n    "&auml",\n    "&Auml",\n    "&bdquo",\n    "&beta",\n    "&Beta",\n    "&brvbar",\n    "&bull",\n    "&cap",\n    "&ccedil",\n    "&Ccedil",\n    "&cedil",\n    "&cent",\n    "&chi",\n    "&Chi",\n    "&circ",\n    "&clubs",\n    "&cong",\n    "&copy",\n    "&crarr",\n    "&cup",\n    "&curren",\n    "&dagger",\n    "&Dagger",\n    "&darr",\n    "&dArr",\n    "&deg",\n    "&delta",\n    "&Delta",\n    "&diams",\n    "&die",\n    "&divide",\n    "&eacute",\n    "&Eacute",\n    "&ecirc",\n    "&Ecirc",\n    "&egrave",\n    "&Egrave",\n    "&empty",\n    "&emsp",\n    "&ensp",\n    "&epsilon",\n    "&Epsilon",\n    "&equiv",\n    "&eta",\n    "&Eta",\n    "&eth",\n    "&ETH",\n    "&euml",\n    "&Euml",\n    "&euro",\n    "&exist",\n    "&forall",\n    "&frac12",\n    "&frac14",\n    "&frac34",\n    "&frasl",\n    "&gamma",\n    "&Gamma",\n    "&ge",\n    "&gt",\n    "&harr",\n    "&hArr",\n    "&hearts",\n    "&hellip",\n    "&iacute",\n    "&Iacute",\n    "&icirc",\n    "&Icirc",\n    "&iexcl",\n    "&igrave",\n    "&Igrave",\n    "&image",\n    "&infin",\n    "&int",\n    "&iota",\n    "&Iota",\n    "&iquest",\n    "&isin",\n    "&iuml",\n    "&Iuml",\n    "&kappa",\n    "&Kappa",\n    "&lambda",\n    "&Lambda",\n    "&lang",\n    "&laquo",\n    "&larr",\n    "&lArr",\n    "&lceil",\n    "&ldquo",\n    "&le",\n    "&lfloor",\n    "&lowast",\n    "&loz",\n    "&lrm",\n    "&lsaquo",\n    "&lsquo",\n    "&lt",\n    "&macr",\n    "&mdash",\n    "&micro",\n    "&middot",\n    "&minus",\n    "&mu",\n    "&Mu",\n    "&nabla",\n    "&nbsp",\n    "&ndash",\n    "&ne",\n    "&ni",\n    "&not",\n    "&notin",\n    "&nsub",\n    "&ntilde",\n    "&Ntilde",\n    "&nu",\n    "&Nu",\n    "&oacute",\n    "&Oacute",\n    "&ocirc",\n    "&Ocirc",\n    "&ograve",\n    "&Ograve",\n    "&oline",\n    "&omega",\n    "&Omega",\n    "&omicron",\n    "&Omicron",\n    "&oplus",\n    "&or",\n    "&ordf",\n    "&ordm",\n    "&oslash",\n    "&Oslash",\n    "&otilde",\n    "&Otilde",\n    "&otimes",\n    "&ouml",\n    "&Ouml",\n    "&para",\n    "&part",\n    "&permil",\n    "&perp",\n    "&phi",\n    "&Phi",\n    "&pi",\n    "&Pi",\n    "&piv",\n    "&plusmn",\n    "&pound",\n    "&prime",\n    "&Prime",\n    "&prod",\n    "&prop",\n    "&psi",\n    "&Psi",\n    "&quot",\n    "&radic",\n    "&rang",\n    "&raquo",\n    "&rarr",\n    "&rArr",\n    "&rceil",\n    "&rdquo",\n    "&real",\n    "&reg",\n    "&rfloor",\n    "&rho",\n    "&Rho",\n    "&rlm",\n    "&rsaquo",\n    "&rsquo",\n    "&sbquo",\n    "&Scaron",\n    "&scaron",\n    "&sdot",\n    "&sect",\n    "&shy",\n    "&sigma",\n    "&Sigma",\n    "&sim",\n    "&spades",\n    "&sub",\n    "&sube",\n    "&sum",\n    "&sup",\n    "&supe",\n    "&sup1",\n    "&sup2",\n    "&sup3",\n    "&szlig",\n    "&tau",\n    "&Tau",\n    "&there4",\n    "&theta",\n    "&Theta",\n    "&thetasym",\n    "&thinsp",\n    "&thorn",\n    "&THORN",\n    "&times",\n    "&tilde",\n    "&trade",\n    "&uacute",\n    "&Uacute",\n    "&uarr",\n    "&uArr",\n    "&ucirc",\n    "&Ucirc",\n    "&ugrave",\n    "&Ugrave",\n    "&uml",\n    "&upsih",\n    "&upsilon",\n    "&Upsilon",\n    "&uuml",\n    "&Uuml",\n    "&weierp",\n    "&xi",\n    "&Xi",\n    "&yacute",\n    "&Yacute",\n    "&yen",\n    "&yuml",\n    "&zeta",\n    "&Zeta",\n    "&zwnj"\n]';});

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

define('main',['require','exports','module','text!SpecialChars.json'],function (require, exports, module) {
    

    // Load dependent modules
    var AppInit             = brackets.getModule("utils/AppInit"),
        CodeHintManager     = brackets.getModule("editor/CodeHintManager"),
        ExtensionUtils      = brackets.getModule("utils/ExtensionUtils"),
        HTMLUtils           = brackets.getModule("language/HTMLUtils"),
        HtmlSpecialChars    = require("text!SpecialChars.json"),
        specialChars;

    /**
     * Encodes the special Char value given. 
     * 
     * @param {string} value
     * The value to encode
     *
     * @return {string}
     * The encoded string
     */
    function _encodeValue(value) {
        return value.replace("&", "&amp;").replace("#", "&#35;");
    }
    
    /**
     * Decodes the special Char value given. 
     * 
     * @param {string} value
     * The value to decode
     *
     * @return {string}
     * The decoded string
     */
    function _decodeValue(value) {
        return value.replace("&amp;", "&").replace("&#35;", "#");
    }
    
    /**
     * @constructor
     */
    function SpecialCharHints() {
        this.primaryTriggerKeys = "&ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#0123456789";
        this.currentQuery = "";
    }
    
    /**
     * Determines whether HtmlSpecialChar hints are available in the current editor
     * context.
     * 
     * @param {Editor} editor 
     * A non-null editor object for the active window.
     *
     * @param {string} implicitChar
     * Either null, if the hinting request was explicit, or a single character
     * that represents the last insertion and that indicates an implicit
     * hinting request.
     *
     * @return {boolean}
     * Determines whether the current provider is able to provide hints for
     * the given editor context and, in case implicitChar is non- null,
     * whether it is appropriate to do so.
     */
    SpecialCharHints.prototype.hasHints = function (editor, implicitChar) {
        this.editor = editor;

        return this._getQuery() !== null;
    };
       
    /**
     * Returns a list of avaliable HtmlSpecialChar hints if possible for the current
     * editor context. 
     * 
     * @param {string} implicitChar
     * Either null, if the hinting request was explicit, or a single character
     * that represents the last insertion and that indicates an implicit
     * hinting request.
     *
     * @return {{hints: Array.<(string|jQuery.Obj)>, match: string, selectInitial: boolean}}
     * Null if the provider wishes to end the hinting session. Otherwise, a
     * response object that provides 1. a sorted array hints that consists 
     * of strings; 2. a string match that is used by the manager to emphasize
     * matching substrings when rendering the hint list; and 3. a boolean that
     * indicates whether the first result, if one exists, should be selected
     * by default in the hint list window.
     */
    SpecialCharHints.prototype.getHints = function (implicitChar) {
        var query,
            result;

        if (implicitChar === null || this.primaryTriggerKeys.indexOf(implicitChar) !== -1) {
            this.currentQuery = query = this._getQuery();
            result = $.map(specialChars, function (value, index) {
                if (value.indexOf(query) === 0) {
                    var shownValue = _encodeValue(value);
                    return shownValue  + "; <span class='entity-display-character'>" + value + ";</span>";
                }
            }).sort(this._internalSort);
            
            if (query !== null) {
                query = _encodeValue(query);
            }
            
            return {
                hints: result,
                match: query,
                selectInitial: true
            };
        }
        
        return null;
    };
    
    /**
     * Sort function used internally when sorting the Hints
     * 
     * @param {string} value
     * The value to decode
     *
     * @return {string}
     * The decoded string
     */
    SpecialCharHints.prototype._internalSort = function (a, b) {
        a = _decodeValue(a.slice(0, a.indexOf(" "))).toLowerCase();
        b = _decodeValue(b.slice(0, b.indexOf(" "))).toLowerCase();
        
        if (a.indexOf("#") !== -1 && b.indexOf("#") !== -1) {
            var num1 = parseInt(a.slice(a.indexOf("#") + 1, a.length - 1), 10),
                num2 = parseInt(b.slice(b.indexOf("#") + 1, b.length - 1), 10);
                    
            return (num1 - num2);
        }
                
        return a.localeCompare(b);
    };
    
    /**
     * Returns a query for the Hints
     * 
     * @return {string}
     * The Query for which to search
     */
    SpecialCharHints.prototype._getQuery = function () {
        var query,
            lineContentBeforeCursor,
            startChar,
            endChar,
            cursor = this.editor.getCursorPos();
        
        if (HTMLUtils.getTagInfo(this.editor, cursor).tagName !== "") {
            return null;
        }
                
        lineContentBeforeCursor = this.editor.document.getRange({
            line: cursor.line,
            ch: 0
        }, cursor);
        
        startChar = lineContentBeforeCursor.lastIndexOf("&");
        endChar = lineContentBeforeCursor.lastIndexOf(";");
        
        // If no startChar was found or the endChar is greater than the startChar then it is no entity
        if (startChar === -1 || endChar > startChar) {
            return null;
        }
        
        query = this.editor.document.getRange({
            line: cursor.line,
            ch: startChar
        }, cursor);
        
        return query;
    };
    
    /**
     * Inserts a given HtmlSpecialChar hint into the current editor context. 
     * 
     * @param {string} completition
     * The hint to be inserted into the editor context.
     * 
     * @return {boolean}
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    SpecialCharHints.prototype.insertHint = function (completion) {
        var start = {line: -1, ch: -1},
            end = {line: -1, ch: -1},
            cursor = this.editor.getCursorPos(),
            match,
            matchSemicolonPos;

        end.line = start.line = cursor.line;
        start.ch = cursor.ch - this.currentQuery.length;
        match = this.editor.document.getLine(cursor.line).slice(cursor.ch);
        matchSemicolonPos = match.indexOf(";");
        end.ch = start.ch + this.currentQuery.length;
        
        if (matchSemicolonPos !== -1 && /^(#*[0-9]+)|([a-zA-Z]+)$/.test(match.slice(0, matchSemicolonPos))) {
            end.ch = this.editor.document.getLine(cursor.line).indexOf(";", start.ch) + 1;
        }
        
        completion = completion.slice(0, completion.indexOf(" "));
        completion = _decodeValue(completion);
        if (start.ch !== end.ch) {
            this.editor.document.replaceRange(completion, start, end);
        } else {
            this.editor.document.replaceRange(completion, start);
        }
        
        return false;
    };

    AppInit.appReady(function () {
        ExtensionUtils.loadStyleSheet(module, "styles.css");
        // Parse JSON files
        specialChars = JSON.parse(HtmlSpecialChars);
        
        // Register code hint providers
        var specialCharHints = new SpecialCharHints();
        
        CodeHintManager.registerHintProvider(specialCharHints, ["html"], 1);
    });
    
    //Export Hints for Unit Tests
    exports.SpecialCharHints = SpecialCharHints;
});