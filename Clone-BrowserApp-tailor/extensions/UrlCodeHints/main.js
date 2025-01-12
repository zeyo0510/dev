
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

define('text!data.json',[],function () { return '{\n    "htmlAttrs": {\n        "href":         { "attribOption": [] },\n        "src":          { "attribOption": [] }\n    }\n}\n';});

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
 */

/*jslint vars: true, plusplus: true, devel: true, nomen: true,  regexp: true, indent: 4, maxerr: 50 */
/*global define, brackets, $, window */


define('main',['require','exports','module','text!data.json'],function (require, exports, module) {
    
    
    // Brackets modules
    var AppInit             = brackets.getModule("utils/AppInit"),
        CodeHintManager     = brackets.getModule("editor/CodeHintManager"),
        CSSUtils            = brackets.getModule("language/CSSUtils"),
        DocumentManager     = brackets.getModule("document/DocumentManager"),
        EditorManager       = brackets.getModule("editor/EditorManager"),
        HTMLUtils           = brackets.getModule("language/HTMLUtils"),
        NativeFileSystem    = brackets.getModule("file/NativeFileSystem").NativeFileSystem,
        ProjectManager      = brackets.getModule("project/ProjectManager"),
        StringUtils         = brackets.getModule("utils/StringUtils"),

        Data                = require("text!data.json"),

        data,
        htmlAttrs;
    
    /**
     * @constructor
     */
    function UrlCodeHints() {}

    /**
     * Helper function to create a list of urls to existing files based on the query.
     * @param {{queryStr: string}} query -- a query object, used to filter the code hints
     *
     * @return {Array.<string>|$.Deferred} The (possibly deferred) hints.
     */
    UrlCodeHints.prototype._getUrlList = function (query) {
        var doc,
            result = [];
        
        // site-root relative links are not yet supported, so filter them out
        if (query.queryStr.length > 0 && query.queryStr[0] === "/") {
            return result;
        }

        // get path to current document
        doc = DocumentManager.getCurrentDocument();
        if (!doc || !doc.file) {
            return result;
        }

        var docUrl = window.PathUtils.parseUrl(doc.file.fullPath);
        if (!docUrl) {
            return result;
        }

        var docDir = docUrl.domain + docUrl.directory;

        // get relative path from query string
        // TODO: handle site-root relative
        var queryDir = "";
        var queryUrl = window.PathUtils.parseUrl(query.queryStr);
        if (queryUrl) {
            queryDir = queryUrl.directory;
        }

        // build target folder path
        var targetDir = docDir + decodeURI(queryDir);

        // get list of files from target folder
        var unfiltered = [];

        // Getting the file/folder info is an asynch operation, so it works like this:
        //
        // The initial pass initiates the asynchronous retrieval of data and returns an
        // empty list, so no code hints are displayed. In the async callback, the code
        // hints and the original query are stored in a cache, and then the process to
        // show code hints is re-initiated.
        //
        // During the next pass, there should now be code hints cached from the initial
        // pass, but user may have typed while file/folder info was being retrieved from
        // disk, so we need to make sure code hints still apply to current query. If so,
        // display them, otherwise, clear cache and start over.
        //
        // As user types within a folder, the same unfiltered file/folder list is still
        // valid and re-used from cache. Filtering based on user input is done outside
        // of this method. When user moves to a new folder, then the cache is deleted,
        // and file/folder info for new folder is then retrieved.

        if (this.cachedHints) {
            // url hints have been cached, so determine if they're stale
            if (!this.cachedHints.query ||
                    this.cachedHints.query.tag !== query.tag ||
                    this.cachedHints.query.attrName !== query.attrName ||
                    this.cachedHints.queryDir !== queryDir ||
                    this.cachedHints.docDir !== docDir) {

                // delete stale cache
                this.cachedHints = null;
            }
        }

        if (this.cachedHints) {
            // use cached hints
            unfiltered = this.cachedHints.unfiltered;

        } else {
            var self = this;

            if (self.cachedHints && self.cachedHints.deferred) {
                self.cachedHints.deferred.reject();
            }
            // create empty object so we can detect "waiting" state
            self.cachedHints = {};
            self.cachedHints.deferred = $.Deferred();
            self.cachedHints.unfiltered = [];

            NativeFileSystem.requestNativeFileSystem(targetDir, function (fs) {
                fs.root.createReader().readEntries(function (entries) {

                    entries.forEach(function (entry) {
                        if (ProjectManager.shouldShow(entry)) {
                            // convert to doc relative path
                            var entryStr = entry.fullPath.replace(docDir, "");

                            // code hints show the same strings that are inserted into text,
                            // so strings in list will be encoded. wysiwyg, baby!
                            unfiltered.push(encodeURI(entryStr));
                        }
                    });

                    self.cachedHints.unfiltered = unfiltered;
                    self.cachedHints.query      = query;
                    self.cachedHints.queryDir   = queryDir;
                    self.cachedHints.docDir     = docDir;
                    
                    if (self.cachedHints.deferred.state() !== "rejected") {
                        var currentDeferred = self.cachedHints.deferred;
                        // Since we've cached the results, the next call to _getUrlList should be synchronous.
                        // If it isn't, we've got a problem and should reject both the current deferred
                        // and any new deferred that got created on the call.
                        var syncResults = self._getUrlList(query);
                        if (syncResults instanceof Array) {
                            currentDeferred.resolveWith(self, [syncResults]);
                        } else {
                            if (currentDeferred && currentDeferred.state() === "pending") {
                                currentDeferred.reject();
                            }
                            
                            if (self.cachedHints.deferred &&
                                    self.cachedHints.deferred.state() === "pending") {
                                self.cachedHints.deferred.reject();
                                self.cachedHints.deferred = null;
                            }
                        }
                    }
                });
            });

            return self.cachedHints.deferred;
        }

        // build list

        // without these entries, typing "../" will not display entries for containing folder
        if (queryUrl.filename === ".") {
            result.push(queryDir + ".");
        } else if (queryUrl.filename === "..") {
            result.push(queryDir + "..");
        }

        // add file/folder entries
        unfiltered.forEach(function (item) {
            result.push(item);
        });

        // TODO: filter by desired file type based on tag, type attr, etc.

        // TODO: add list item to top of list to popup modal File Finder dialog
        // New string: "Browse..." or "Choose a File..."
        // Command: Commands.FILE_OPEN

        return result;
    };

    /**
     * Helper function that determines the possible value hints for a given html tag/attribute name pair
     * 
     * @param {{queryStr: string}} query
     * The current query
     *
     * @return {{hints: (Array.<string>|$.Deferred), sortFunc: ?function(string, string): number}}
     * The (possibly deferred) hints and the sort function to use on thise hints.
     */
    UrlCodeHints.prototype._getUrlHints = function (query) {
        var hints = [],
            sortFunc = null;

        // Do not show hints after "?" in url
        if (query.queryStr.indexOf("?") === -1) {
            
            // Default behavior for url hints is do not close on select.
            this.closeOnSelect = false;
            hints = this._getUrlList(query);
            sortFunc = StringUtils.urlSort;
        }
        
        return { hints: hints, sortFunc: sortFunc };
    };
    
    /**
     * Determines whether font hints are available in the current editor
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
     * the given editor context and, in case implicitChar is non-null,
     * whether it is appropriate to do so.
     */
    UrlCodeHints.prototype.hasHints = function (editor, implicitChar) {
        var mode = editor.getModeForSelection();
        if (mode === "html") {
            return this.hasHtmlHints(editor, implicitChar);
        } else if (mode === "css") {
            return this.hasCssHints(editor, implicitChar);
        }

        return false;
    };

    /**
     * Helper function for hasHints() for CSS.
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
     * the given editor context and, in case implicitChar is non-null,
     * whether it is appropriate to do so.
     */
    UrlCodeHints.prototype.hasCssHints = function (editor, implicitChar) {
        this.editor = editor;
        var cursor = this.editor.getCursorPos();

        this.info = CSSUtils.getInfoAtPos(editor, cursor);

        if (this.info.context !== CSSUtils.PROP_VALUE && this.info.context !== CSSUtils.IMPORT_URL) {
            return false;
        }

        // collect existing value
        var i,
            val = "";

        for (i = 0; i <= this.info.index && i < this.info.values.length; i++) {
            if (i < this.info.index) {
                val += this.info.values[i];
            } else {
                val += this.info.values[i].substring(0, this.info.offset);
            }
        }
        
        // starts with "url(" ?
        if (val.match(/^\s*url\(/i)) {
            return true;
        }

        return false;
    };

    /**
     * Helper function for hasHints() for HTML.
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
     * the given editor context and, in case implicitChar is non-null,
     * whether it is appropriate to do so.
     */
    UrlCodeHints.prototype.hasHtmlHints = function (editor, implicitChar) {
        var tagInfo,
            query,
            tokenType;

        this.editor = editor;
        if (implicitChar === null) {
            tagInfo = HTMLUtils.getTagInfo(editor, editor.getCursorPos());
            query = null;
            tokenType = tagInfo.position.tokenType;
             
            if (tokenType === HTMLUtils.ATTR_VALUE) {
                
                // Verify that attribute name has hintable values
                if (htmlAttrs[tagInfo.attr.name]) {
                
                    if (tagInfo.position.offset >= 0) {
                        query = tagInfo.attr.value.slice(0, tagInfo.position.offset);
                    } else {
                        // We get negative offset for a quoted attribute value with some leading whitespaces 
                        // as in <a rel= "rtl" where the cursor is just to the right of the "=".
                        // So just set the queryStr to an empty string. 
                        query = "";
                    }
                
                    var hintsAndSortFunc = this._getUrlHints({queryStr: query});
                    var hints = hintsAndSortFunc.hints;
                    if (hints instanceof Array) {
                        // If we got synchronous hints, check if we have something we'll actually use
                        var i, foundPrefix = false;
                        for (i = 0; i < hints.length; i++) {
                            if (hints[i].indexOf(query) === 0) {
                                foundPrefix = true;
                                break;
                            }
                        }
                        if (!foundPrefix) {
                            query = null;
                        }
                    }
                }
            }

            return (query !== null);
        }
    };

    /**
     * Returns a list of availble font hints, if possible, for the current
     * editor context.
     *
     * @return {{hints: Array.<jQuery.Object>, match: string, selectInitial: boolean}}
     *
     * Null if the provider wishes to end the hinting session. Otherwise, a
     * response object that provides:
     * 1. a sorted array formatted hints;
     * 2. a null string match to indicate that the hints are already formatted;
     * 3. a boolean that indicates whether the first result, if one exists,
     *    should be selected by default in the hint list window.
     */
    UrlCodeHints.prototype.getHints = function (key) {
        var mode = this.editor.getModeForSelection(),
            cursor = this.editor.getCursorPos(),
            filter = "",
            unfiltered = [],
            hints = [],
            sortFunc = null,
            query = { queryStr: "" },
            result = [];

        if (mode === "html") {
            var tagInfo = HTMLUtils.getTagInfo(this.editor, cursor),
                tokenType = tagInfo.position.tokenType;

            if (tokenType !== HTMLUtils.ATTR_VALUE || !htmlAttrs[tagInfo.attr.name]) {
                return null;
            }
            
            if (tagInfo.position.offset >= 0) {
                query.queryStr = tagInfo.attr.value.slice(0, tagInfo.position.offset);
            }
            this.info = tagInfo;

        } else if (mode === "css") {
            this.info = CSSUtils.getInfoAtPos(this.editor, cursor);

            var context = this.info.context;
            if (context !== CSSUtils.PROP_VALUE && context !== CSSUtils.IMPORT_URL) {
                return null;
            }

            // Cursor is in an existing property value or partially typed value
            if (this.info.index !== -1) {

                // Collect value up to (item) index/(char) offset
                var i, val = "";
                for (i = 0; i < this.info.index; i++) {
                    val += this.info.values[i];
                }
                // index may exceed length of array for multiple-value case
                if (this.info.index < this.info.values.length) {
                    val += this.info.values[this.info.index].substr(0, this.info.offset);
                }

                // Strip "url("
                val = val.replace(/^\s*url\(/i, "");

                // Keep track of leading whitespace and strip it
                var matchWhitespace = val.match(/^\s*/);
                if (matchWhitespace) {
                    this.info.leadingWhitespace = matchWhitespace[0];
                    val = val.substring(matchWhitespace[0].length);
                } else {
                    this.info.leadingWhitespace = null;
                }
                
                // Keep track of opening quote and strip it
                if (val.match(/^["']/)) {
                    this.info.openingQuote = val[0];
                    val = val.substring(1);
                } else {
                    this.info.openingQuote = null;
                }

                query.queryStr = val;
            }

        } else {
            return null;
        }

        if (query.queryStr !== null) {
            filter = query.queryStr;
            var hintsAndSortFunc = this._getUrlHints(query);
            hints = hintsAndSortFunc.hints;
            sortFunc = hintsAndSortFunc.sortFunc;
        }
        this.info.filter = filter;

        if (hints instanceof Array && hints.length) {
            // Array was returned
            console.assert(!result.length);
            result = $.map(hints, function (item) {
                if (item.indexOf(filter) === 0) {
                    return item;
                }
            }).sort(sortFunc);

            return {
                hints: result,
                match: query.queryStr,
                selectInitial: true
            };

        } else if (hints instanceof Object && hints.hasOwnProperty("done")) {
            // Deferred hints were returned
            var deferred = $.Deferred();
            hints.done(function (asyncHints) {
                deferred.resolveWith(this, [{
                    hints: asyncHints,
                    match: query.queryStr,
                    selectInitial: true
                }]);
            });

            return deferred;
        }

        return null;
    };

    /**
     * Inserts a given url hint into the current editor context.
     *
     * @param {jQuery.Object} completion
     * The hint to be inserted into the editor context.
     *
     * @return {boolean}
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    UrlCodeHints.prototype.insertHint = function (completion) {
        var mode = this.editor.getModeForSelection();
        if (mode === "html") {
            return this.insertHtmlHint(completion);
        } else if (mode === "css") {
            return this.insertCssHint(completion);
        }

        return false;
    };

    /**
     * Get distance between 2 positions.
     * 
     * Assumption: pos2 >= pos1
     * 
     * Note that this function is designed to work on CSSUtils info.values array,
     * so this could be made a method if that is converted to an object.
     *
     * @param {Array.<string>}  array  - strings to be searched
     * @param {{index: number, offset: number}} pos1 - starting index/offset in index string
     * @param {{index: number, offset: number}} pos2 - ending index/offset in index string
     *
     * @return {number}
     * Number of characters between 2 positions
     */
    UrlCodeHints.prototype.getCharOffset = function (array, pos1, pos2) {
        var i, count = 0;
        
        if (pos1.index === pos2.index) {
            return (pos2.offset >= pos1.offset) ? (pos2.offset - pos1.offset) : 0;
        } else if (pos1.index < pos2.index) {
            if (pos1.index < 0 || pos1.index >= array.length || pos2.index < 0 || pos2.index >= array.length) {
                return 0;
            }
            
            for (i = pos1.index; i <= pos2.index; i++) {
                if (i === pos1.index) {
                    count += (array[i].length - pos1.offset);
                } else if (i === pos2.index) {
                    count += pos2.offset;
                } else {
                    count += array[i].length;
                }
            }
        }
        
        return count;
    };

    /**
     * Finds next position in array of specified char.
     * 
     * Note that this function is designed to work on CSSUtils info.values array,
     * so this could be made a method if that is converted to an object.
     *
     * @param {Array}  array - strings to be searched
     * @param {string} ch    - char to search for
     * @param {{index: number, offset: number}} pos - starting index/offset in index string
     *
     * @return {{index: number, offset: number}}
     * Index of array, and offset in string where char found.
     */
    UrlCodeHints.prototype.findNextPosInArray = function (array, ch, pos) {
        var i, o, searchOffset;
        for (i = pos.index; i < array.length; i++) {
            // Only use offset on index, then offset of 0 after that
            searchOffset = (i === pos.index) ? pos.offset : 0;
            o = array[i].indexOf(ch, searchOffset);
            
            if (o !== -1) {
                return { index: i, offset: o };
            }
        }
        return { index: -1, offset: -1 };
    };

    /**
     * Inserts a given css url hint into the current editor context.
     *
     * @param {jQuery.Object} completion
     * The hint to be inserted into the editor context.
     *
     * @return {boolean}
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    UrlCodeHints.prototype.insertCssHint = function (completion) {
        var cursor = this.editor.getCursorPos(),
            start  = { line: cursor.line, ch: cursor.ch },
            end    = { line: cursor.line, ch: cursor.ch };

        var hasClosingQuote = false,
            hasClosingParen = false,
            insertText      = completion,
            moveLen         = 0,
            closingPos      = { index: -1, offset: -1 },
            searchResult    = { index: -1, offset: -1 };

        if (this.info.context !== CSSUtils.PROP_VALUE && this.info.context !== CSSUtils.IMPORT_URL) {
            return false;
        }

        // Special handling for URL hinting -- if the completion is a file name
        // and not a folder, then close the code hint list.
        if (!this.closeOnSelect && completion.match(/\/$/) === null) {
            this.closeOnSelect = true;
        }

        // Look for optional closing quote
        if (this.info.openingQuote) {
            closingPos = this.findNextPosInArray(this.info.values, this.info.openingQuote, this.info);
            hasClosingQuote = (closingPos.index !== -1);
        }

        // Look for closing paren
        if (hasClosingQuote) {
            searchResult = this.findNextPosInArray(this.info.values, ")", closingPos);
            hasClosingParen = (searchResult.index !== -1);
        } else {
            // index may exceed length of array for multiple-value case
            closingPos = this.findNextPosInArray(this.info.values, ")", this.info);
            hasClosingParen = (closingPos.index !== -1);
        }

        // Adjust insert char positions to replace existing value, if there is a closing paren
        if (closingPos.index !== -1) {
            end.ch += this.getCharOffset(this.info.values, this.info, closingPos);
        }
        if (this.info.filter.length > 0) {
            start.ch -= this.info.filter.length;
        }

        // Append matching quote, whitespace, paren
        if (this.info.openingQuote && !hasClosingQuote) {
            insertText += this.info.openingQuote;
        }
        if (!hasClosingParen) {
            // Add trailing whitespace to match leading whitespace
            if (this.info.leadingWhitespace) {
                insertText += this.info.leadingWhitespace;
            }
            insertText += ")";
        }

        // HACK (tracking adobe/brackets#1688): We talk to the private CodeMirror instance
        // directly to replace the range instead of using the Document, as we should. The
        // reason is due to a flaw in our current document synchronization architecture when
        // inline editors are open.
        this.editor._codeMirror.replaceRange(insertText, start, end);

        // Adjust cursor position
        if (this.closeOnSelect) {
            // If there is existing closing quote and/or paren, move the cursor past them
            moveLen = (hasClosingQuote ? 1 : 0) + (hasClosingParen ? 1 : 0);
            if (moveLen > 0) {
                this.editor.setCursorPos(start.line, start.ch + completion.length + moveLen);
            }
            return false;

        } else {
            // If closing quote and/or paren are added, move the cursor to where it would have been
            moveLen = ((this.info.openingQuote && !hasClosingQuote) ? 1 : 0) + (!hasClosingParen ? 1 : 0);
            if (moveLen > 0) {
                this.editor.setCursorPos(start.line, start.ch + completion.length);
            }
        }

        return true;
    };

    /**
     * Inserts a given html url hint into the current editor context.
     *
     * @param {jQuery.Object} completion
     * The hint to be inserted into the editor context.
     *
     * @return {boolean}
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    UrlCodeHints.prototype.insertHtmlHint = function (completion) {
        var cursor = this.editor.getCursorPos(),
            start = {line: -1, ch: -1},
            end = {line: -1, ch: -1},
            tagInfo = HTMLUtils.getTagInfo(this.editor, cursor),
            tokenType = tagInfo.position.tokenType,
            charCount = 0,
            replaceExistingOne = tagInfo.attr.valueAssigned,
            endQuote = "",
            shouldReplace = true;

        if (tokenType === HTMLUtils.ATTR_VALUE) {
            charCount = tagInfo.attr.value.length;
            
            // Special handling for URL hinting -- if the completion is a file name
            // and not a folder, then close the code hint list.
            if (!this.closeOnSelect && completion.match(/\/$/) === null) {
                this.closeOnSelect = true;
            }
            
            if (!tagInfo.attr.hasEndQuote) {
                endQuote = tagInfo.attr.quoteChar;
                if (endQuote) {
                    completion += endQuote;
                } else if (tagInfo.position.offset === 0) {
                    completion = "\"" + completion + "\"";
                }
            } else if (completion === tagInfo.attr.value) {
                shouldReplace = false;
            }
        }

        end.line = start.line = cursor.line;
        start.ch = cursor.ch - tagInfo.position.offset;
        end.ch = start.ch + charCount;

        if (shouldReplace) {
            if (start.ch !== end.ch) {
                this.editor.document.replaceRange(completion, start, end);
            } else {
                this.editor.document.replaceRange(completion, start);
            }
        }

        if (!this.closeOnSelect) {
            // If we append the missing quote, then we need to adjust the cursor postion
            // to keep the code hint list open.
            if (tokenType === HTMLUtils.ATTR_VALUE && !tagInfo.attr.hasEndQuote) {
                this.editor.setCursorPos(start.line, start.ch + completion.length - 1);
            }
            return true;
        }
        
        if (tokenType === HTMLUtils.ATTR_VALUE && tagInfo.attr.hasEndQuote) {
            // Move the cursor to the right of the existing end quote after value insertion.
            this.editor.setCursorPos(start.line, start.ch + completion.length + 1);
        }
        
        return false;
    };

    AppInit.appReady(function () {
        data            = JSON.parse(Data);
        htmlAttrs       = data.htmlAttrs;

        var urlHints = new UrlCodeHints();
        CodeHintManager.registerHintProvider(urlHints, ["css", "html"], 5);

        // For unit testing
        exports.hintProvider = urlHints;
    });
});
