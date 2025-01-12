
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

define('text!QuickViewTemplate.html',[],function () { return '<div id="quick-view-container">\n    <div class="preview-content">\n    </div>\n</div>';});

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

/*jslint vars: true, plusplus: true, devel: true, nomen: true,  regexp: true, indent: 4, maxerr: 50 */
/*global define, brackets, $, window, PathUtils, CodeMirror */

define('main',['require','exports','module','text!QuickViewTemplate.html'],function (require, exports, module) {
    
    
    // Brackets modules
    var AppInit             = brackets.getModule("utils/AppInit"),
        CommandManager      = brackets.getModule("command/CommandManager"),
        DocumentManager     = brackets.getModule("document/DocumentManager"),
        EditorManager       = brackets.getModule("editor/EditorManager"),
        ExtensionUtils      = brackets.getModule("utils/ExtensionUtils"),
        Menus               = brackets.getModule("command/Menus"),
        PreferencesManager  = brackets.getModule("preferences/PreferencesManager"),
        Strings             = brackets.getModule("strings"),
        ColorUtils          = brackets.getModule("utils/ColorUtils");
   
    var previewContainerHTML       = require("text!QuickViewTemplate.html");
    
    var defaultPrefs               = { enabled: true },
        enabled,                             // Only show preview if true
        prefs                      = null,   // Preferences
        $previewContainer,                   // Preview container
        $previewContent,                     // Preview content holder
        lastPos;                             // Last line/ch pos processed by handleMouseMove
    
    // Constants
    var CMD_ENABLE_QUICK_VIEW       = "view.enableQuickView",
        HOVER_DELAY                 = 350,  // Time (ms) mouse must remain over a provider's matched text before popover appears
        POSITION_OFFSET             = 38,   // Distance between the bottom of the line and the bottom of the preview container
        POINTER_LEFT_OFFSET         = 17,   // Half of the pointer width, used to find the center of the pointer
        POINTER_TOP_OFFSET          =  7,   // Pointer height, used to shift popover above pointer
        POSITION_BELOW_OFFSET       = 16,   // Amount to adjust to top position when the preview bubble is below the text
        POPOVER_HORZ_MARGIN         =  5;   // Horizontal margin
    
    /**
     * There are three states for this var:
     * 1. If null, there is no provider result for the given mouse position.
     * 2. If non-null, and visible==true, there is a popover currently showing.
     * 3. If non-null, but visible==false, there is a provider result but it has not been shown yet because
     * we're waiting for HOVER_DELAY, which is tracked by hoverTimer. The state changes to visible==true as
     * soon as hoverTimer fires. If the mouse moves before then, the popover will never become visible.
     * 
     * @type {{
     *      visible: boolean,
     *      editor: !Editor,
     *      hoverTimer: number,             - setTimeout() token
     *      start: !{line, ch},             - start of matched text range
     *      end: !{line, ch},               - end of matched text range
     *      content: !string,               - HTML content to display in popover
     *      onShow: ?function():void,       - called once popover content added to the DOM (may never be called)
     *      xpos: number,                   - x of center of popover
     *      ytop: number,                   - y of top of matched text (when popover placed above text, normally)
     *      ybot: number,                   - y of bottom of matched text (when popover moved below text, avoiding window top)
     *      marker: ?CodeMirror.TextMarker  - only set once visible==true
     * }}
     */
    var popoverState = null;
    
    
    
    // Popover widget management ----------------------------------------------
    
    /**
     * Cancels whatever popoverState was currently pending and sets it back to null. If the popover was visible,
     * hides it; if the popover was invisible and still pending, cancels hoverTimer so it will never be shown.
     */
    function hidePreview() {
        if (!popoverState) {
            return;
        }
        
        if (popoverState.visible) {
            popoverState.marker.clear();
            
            $previewContent.empty();
            $previewContainer.hide();
            
        } else {
            window.clearTimeout(popoverState.hoverTimer);
        }
        
        popoverState = null;
    }
    
    function positionPreview(xpos, ypos, ybot) {
        var previewWidth  = $previewContainer.width(),
            top           = ypos - $previewContainer.height() - POSITION_OFFSET,
            left          = xpos - previewWidth / 2 - POINTER_LEFT_OFFSET,
            $editorHolder = $("#editor-holder"),
            editorLeft    = $editorHolder.offset().left;

        left = Math.max(left, editorLeft + POPOVER_HORZ_MARGIN);
        left = Math.min(left, editorLeft + $editorHolder.width() - previewWidth - POPOVER_HORZ_MARGIN);
        
        if (top < 0) {
            $previewContainer.removeClass("preview-bubble-above");
            $previewContainer.addClass("preview-bubble-below");
            top = ybot + POSITION_BELOW_OFFSET;
            $previewContainer.offset({
                left: left,
                top: top
            });
        } else {
            $previewContainer.removeClass("preview-bubble-below");
            $previewContainer.addClass("preview-bubble-above");
            $previewContainer.offset({
                left: left,
                top: top - POINTER_TOP_OFFSET
            });
        }
    }
    
    /**
     * Changes the current hidden popoverState to visible, showing it in the UI and highlighting
     * its matching text in the editor.
     */
    function showPreview() {
        
        var cm = popoverState.editor._codeMirror;
        popoverState.marker = cm.markText(
            popoverState.start,
            popoverState.end,
            {className: "quick-view-highlight"}
        );
        
        $previewContent.append(popoverState.content);
        $previewContainer.show();
        
        popoverState.visible = true;
        
        if (popoverState.onShow) {
            popoverState.onShow();
        }
        
        positionPreview(popoverState.xpos, popoverState.ytop, popoverState.ybot);
    }
    
    function divContainsMouse($div, event) {
        var offset = $div.offset();
        
        return (event.clientX >= offset.left &&
                event.clientX <= offset.left + $div.width() &&
                event.clientY >= offset.top &&
                event.clientY <= offset.top + $div.height());
    }
    
    
    // Color & gradient preview provider --------------------------------------

    function colorAndGradientPreviewProvider(editor, pos, token, line) {

        // Check for gradient. -webkit-gradient() can have parens in parameters
        // nested 2 levels. Other gradients can only nest 1 level.
        var gradientRegEx = /-webkit-gradient\((?:[^\(]*?(?:\((?:[^\(]*?(?:\([^\)]*?\))*?)*?\))*?)*?\)|(?:(?:-moz-|-ms-|-o-|-webkit-|\s)((repeating-)?linear-gradient)|(?:-moz-|-ms-|-o-|-webkit-|\s)((repeating-)?radial-gradient))(\((?:[^\)]*?(?:\([^\)]*?\))*?)*?\))/gi,
            colorRegEx = new RegExp(ColorUtils.COLOR_REGEX);

        function execGradientMatch(line) {
            var gradientMatch = gradientRegEx.exec(line),
                prefix = "",
                colorValue;
            
            if (gradientMatch) {
                if (gradientMatch[0].indexOf("@") !== -1) {
                    // If the gradient match has "@" in it, it is most likely a less or
                    // sass variable. Ignore it since it won't be displayed correctly.
                    gradientMatch = null;
    
                } else {
                    // If it was a linear-gradient or radial-gradient variant with a vendor prefix 
                    // add "-webkit-" so it shows up correctly in Brackets.
                    if (gradientMatch[0].match(/-o-|-moz-|-ms-|-webkit-/i)) {
                        prefix = "-webkit-";
                    }
                    
                    // For prefixed gradients, use the non-prefixed value as the color value.
                    // "-webkit-" will be added before this value later
                    if (gradientMatch[1]) {
                        colorValue = gradientMatch[1] + gradientMatch[5];    // linear gradiant
                    } else if (gradientMatch[3]) {
                        colorValue = gradientMatch[3] + gradientMatch[5];    // radial gradiant
                    } else if (gradientMatch[0]) {
                        colorValue = gradientMatch[0];                       // -webkit-gradient
                        prefix = "";                                         // do not prefix
                    }
                }
            }

            return {
                match:      gradientMatch,
                prefix:     prefix,
                colorValue: colorValue
            };
        }

        function execColorMatch(line) {
            var colorMatch;

            function hyphenOnMatchBoundary(match, line) {
                var beforeIndex, afterIndex;
                if (match) {
                    beforeIndex = match.index - 1;
                    if (beforeIndex >= 0 && line[beforeIndex] === "-") {
                        return true;
                    } else {
                        afterIndex = match.index + match[0].length;
                        if (afterIndex < line.length && line[afterIndex] === "-") {
                            return true;
                        }
                    }
                }
                
                return false;
            }

            // Hyphens do not count as a regex word boundary (\b), so check for those here
            do {
                colorMatch = colorRegEx.exec(line);
            } while (colorMatch && hyphenOnMatchBoundary(colorMatch, line));

            return colorMatch;
        }
        
        // simple css property splitter (used to find color stop arguments in gradients)
        function splitStyleProperty(property) {
            var token = /((?:[^"']|".*?"|'.*?')*?)([(,)]|$)/g;
            var recurse = function () {
                var array = [];
                for (;;) {
                    var result = token.exec(property);
                    if (result[2] === "(") {
                        var str = result[1].trim() + "(" + recurse().join(",") + ")";
                        result = token.exec(property);
                        str += result[1];
                        array.push(str);
                    } else {
                        array.push(result[1].trim());
                    }
                    if (result[2] !== ",") {
                        return array;
                    }
                }
            };
            return (recurse());
        }
        
        // color stop helpers
        function isGradientColorStop(args) {
            return (args.length > 0 && args[0].match(colorRegEx) !== null);
        }
        
        function hasLengthInPixels(args) {
            return (args.length > 1 && args[1].indexOf("px") > 0);
        }
        
        // Normalizes px color stops to % 
        function normalizeGradientExpressionForQuickview(expression) {
            if (expression.indexOf("px") > 0) {
                var paramStart = expression.indexOf("(") + 1,
                    paramEnd = expression.lastIndexOf(")"),
                    parameters = expression.substring(paramStart, paramEnd),
                    params = splitStyleProperty(parameters),
                    lowerBound = 0,
                    upperBound = $previewContainer.width(),
                    args,
                    thisSize,
                    i;


                // find lower bound                
                for (i = 0; i < params.length; i++) {
                    args = params[i].split(" ");
                    
                    if (hasLengthInPixels(args)) {
                        thisSize = parseFloat(args[1]);

                        upperBound = Math.max(upperBound, thisSize);
                        // we really only care about converting negative
                        //  pixel values -- so take the smallest negative pixel 
                        //  value and use that as baseline for display purposes
                        if (thisSize < 0) {
                            lowerBound = Math.min(lowerBound, thisSize);
                        }
                    }
                }
                
                // convert negative lower bound to positive and adjust all pixel values
                //  so that -20px is now 0px and 100px is now 120px 
                lowerBound = Math.abs(lowerBound);
                
                // Offset the upperbound by the lowerBound to give us a corrected context
                upperBound += lowerBound;
                
                // convert to %
                for (i = 0; i < params.length; i++) {
                    args = params[i].split(" ");
                    if (isGradientColorStop(args) && hasLengthInPixels(args)) {
                        thisSize = ((parseFloat(args[1]) + lowerBound) / upperBound) * 100;
                        args[1] = thisSize + "%";
                    }
                    params[i] = args.join(" ");
                }

                // put it back together.                
                expression = expression.substring(0, paramStart) + params.join(", ") + expression.substring(paramEnd);
            }
            return expression;
        }

        var gradientMatch = execGradientMatch(line),
            match = gradientMatch.match || execColorMatch(line),
            cm = editor._codeMirror;

        while (match) {
            if (pos.ch >= match.index && pos.ch <= match.index + match[0].length) {
                // build the css for previewing the gradient from the regex result
                var previewCSS = gradientMatch.prefix + (gradientMatch.colorValue || match[0]);
                
                // normalize the arguments to something that we can display to the user
                // NOTE: we need both the div and the popover's _previewCSS member 
                //          (used by unit tests) to match so normalize the css for both
                previewCSS = normalizeGradientExpressionForQuickview(previewCSS);
                    
                var preview = "<div class='color-swatch' style='background:" + previewCSS + "'>" +
                              "</div>";
                var startPos = {line: pos.line, ch: match.index},
                    endPos = {line: pos.line, ch: match.index + match[0].length},
                    startCoords = cm.charCoords(startPos),
                    xPos;
                
                xPos = (cm.charCoords(endPos).left - startCoords.left) / 2 + startCoords.left;
                
                return {
                    start: startPos,
                    end: endPos,
                    content: preview,
                    xpos: xPos,
                    ytop: startCoords.top,
                    ybot: startCoords.bottom,
                    _previewCSS: previewCSS
                };
            }

            // Get next match
            if (gradientMatch.match) {
                gradientMatch = execGradientMatch(line);
            }
            match = gradientMatch.match || execColorMatch(line);
        }
        
        return null;
    }
    
    
    // Image preview provider -------------------------------------------------
    
    function imagePreviewProvider(editor, pos, token, line) {
        var cm = editor._codeMirror;
        
        // Check for image name
        var urlRegEx = /url\(([^\)]*)\)/gi,
            tokenString,
            urlMatch;

        if (token.type === "string") {
            tokenString = token.string;
        } else {
            urlMatch = urlRegEx.exec(line);
            while (urlMatch) {
                if (pos.ch >= urlMatch.index && pos.ch <= urlMatch.index + urlMatch[0].length) {
                    tokenString = urlMatch[1];
                    break;
                }
                urlMatch = urlRegEx.exec(line);
            }
        }
        
        if (tokenString) {
            // Strip quotes, if present
            var quotesRegEx = /(\'|\")?([^(\'|\")]*)(\'|\")?/;
            tokenString = tokenString.replace(quotesRegEx, "$2");
            
            if (/^(data\:image)|(\.gif|\.png|\.jpg|\.jpeg|\.svg)$/i.test(tokenString)) {
                var sPos, ePos;
                var docPath = editor.document.file.fullPath;
                var imgPath;
                
                if (PathUtils.isAbsoluteUrl(tokenString)) {
                    imgPath = tokenString;
                } else {
                    imgPath = "file:///" + docPath.substr(0, docPath.lastIndexOf("/") + 1) + tokenString;
                }
                
                if (urlMatch) {
                    sPos = {line: pos.line, ch: urlMatch.index};
                    ePos = {line: pos.line, ch: urlMatch.index + urlMatch[0].length};
                } else {
                    sPos = {line: pos.line, ch: token.start};
                    ePos = {line: pos.line, ch: token.end};
                }
                
                if (imgPath) {
                    var imgPreview = "<div class='image-preview'>"          +
                                     "    <img src=\"" + imgPath + "\">"    +
                                     "</div>";
                    var coord = cm.charCoords(sPos);
                    var xpos = (cm.charCoords(ePos).left - coord.left) / 2 + coord.left;
                    
                    var showHandler = function () {
                        // Hide the preview container until the image is loaded.
                        $previewContainer.hide();
                        
                        $previewContainer.find(".image-preview > img").on("load", function () {
                            $previewContent
                                .append("<div class='img-size'>"                                            +
                                            this.naturalWidth + " x " + this.naturalHeight + " " + Strings.UNIT_PIXELS +
                                        "</div>"
                                    );
                            $previewContainer.show();
                            positionPreview(popoverState.xpos, popoverState.ytop, popoverState.ybot);
                        });
                    };
                    
                    return {
                        start: sPos,
                        end: ePos,
                        content: imgPreview,
                        onShow: showHandler,
                        xpos: xpos,
                        ytop: coord.top,
                        ybot: coord.bottom,
                        _imgPath: imgPath
                    };
                }
            }
        }
        
        return null;
    }
    

    // Preview hide/show logic ------------------------------------------------
    
    /**
     * Returns a 'ready for use' popover state object:
     * { visible: false, editor, start, end, content, ?onShow, xpos, ytop, ybot }
     * Lacks only hoverTimer (supplied by handleMouseMove()) and marker (supplied by showPreview()).
     */
    function queryPreviewProviders(editor, pos, token) {
        
        var line = editor.document.getLine(pos.line);
        
        // FUTURE: Support plugin providers. For now we just hard-code...
        var popover = colorAndGradientPreviewProvider(editor, pos, token, line) ||
                      imagePreviewProvider(editor, pos, token, line);
        
        if (popover) {
            // Providers return just { start, end, content, ?onShow, xpos, ytop, ybot }
            $.extend(popover, { visible: false, editor: editor });
            
            return popover;
        }
        return null;
    }
    
    
    function handleMouseMove(event) {
        if (!enabled) {
            return;
        }
        
        if (event.which) {
            // Button is down - don't show popovers while dragging
            hidePreview();
            return;
        }
        
        // Figure out which editor we are over
        var fullEditor = EditorManager.getCurrentFullEditor();
        
        if (!fullEditor) {
            hidePreview();
            return;
        }
        
        // Check for inline Editor instances first
        var inlines = fullEditor.getInlineWidgets(),
            i,
            editor;
        
        for (i = 0; i < inlines.length; i++) {
            var $inlineDiv = inlines[i].$editorsDiv,  // see MultiRangeInlineEditor
                $otherDiv  = inlines[i].$htmlContent;
            
            if ($inlineDiv && divContainsMouse($inlineDiv, event)) {
                editor = inlines[i].editors[0];
                break;
            } else if ($otherDiv && divContainsMouse($otherDiv, event)) {
                // Mouse inside unsupported inline editor like Quick Docs or Color Editor
                hidePreview();
                return;
            }
        }
        
        // Check main editor
        if (!editor) {
            if (divContainsMouse($(fullEditor.getRootElement()), event)) {
                editor = fullEditor;
            }
        }
        
        if (editor && editor._codeMirror) {
            // Find char mouse is over
            var cm = editor._codeMirror;
            var pos = cm.coordsChar({left: event.clientX, top: event.clientY});
            
            if (lastPos && lastPos.line === pos.line && lastPos.ch === pos.ch) {
                return;  // bail if mouse is on same char as last event
            }
            lastPos = pos;
            
            var showImmediately = false;
            
            // Is there a popover already visible or pending?
            if (popoverState) {
                if (editor.posWithinRange(pos, popoverState.start, popoverState.end)) {
                    // That one's still relevant - nothing more to do
                    return;
                } else {
                    // That one doesn't cover this pos - hide it and query providers anew
                    showImmediately = popoverState.visible;
                    hidePreview();
                }
            }
            
            // Query providers for a new popoverState
            var token = cm.getTokenAt(pos, true);
            popoverState = queryPreviewProviders(editor, pos, token);
            
            if (popoverState) {
                // We have a popover available - wait until we're ready to show it
                if (showImmediately) {
                    showPreview();
                } else {
                    popoverState.hoverTimer = window.setTimeout(function () {
                        // Ready to show now (we'll never get here if mouse movement rendered this popover
                        // inapplicable first - hidePopover() cancels hoverTimer)
                        showPreview();
                    }, HOVER_DELAY);
                }
            }
            
        } else {
            // Mouse not over any Editor - immediately hide popover
            hidePreview();
        }
    }
    
    function onActiveEditorChange(event, current, previous) {
        // Hide preview when editor changes
        hidePreview();

        if (previous && previous.document) {
            $(previous.document).off("change", hidePreview);
        }

        if (current && current.document) {
            $(current.document).on("change", hidePreview);
        }
    }

    // Menu command handlers
    function updateMenuItemCheckmark() {
        CommandManager.get(CMD_ENABLE_QUICK_VIEW).setChecked(enabled);
    }

    function setEnabled(_enabled) {
        if (enabled !== _enabled) {
            enabled = _enabled;
            var editorHolder = $("#editor-holder")[0];
            if (enabled) {
                // Note: listening to "scroll" also catches text edits, which bubble a scroll event up from the hidden text area. This means
                // we auto-hide on text edit, which is probably actually a good thing.
                editorHolder.addEventListener("mousemove", handleMouseMove, true);
                editorHolder.addEventListener("scroll", hidePreview, true);
                editorHolder.addEventListener("mouseout", hidePreview, true);

                // Setup doc "change" listener
                onActiveEditorChange(null, EditorManager.getActiveEditor(), null);
                $(EditorManager).on("activeEditorChange", onActiveEditorChange);

            } else {
                editorHolder.removeEventListener("mousemove", handleMouseMove, true);
                editorHolder.removeEventListener("scroll", hidePreview, true);
                editorHolder.removeEventListener("mouseout", hidePreview, true);

                // Cleanup doc "change" listener
                onActiveEditorChange(null, null, EditorManager.getActiveEditor());
                $(EditorManager).off("activeEditorChange", onActiveEditorChange);

                hidePreview();
            }
            prefs.setValue("enabled", enabled);
        }
        // Always update the checkmark, even if the enabled flag hasn't changed.
        updateMenuItemCheckmark();
    }
    
    function toggleEnableQuickView() {
        setEnabled(!enabled);
    }
        
    // Create the preview container
    $previewContainer = $(previewContainerHTML).appendTo($("body"));
    $previewContent = $previewContainer.find(".preview-content");
    
    // Load our stylesheet
    ExtensionUtils.loadStyleSheet(module, "QuickView.css");
    
    // Register command
    CommandManager.register(Strings.CMD_ENABLE_QUICK_VIEW, CMD_ENABLE_QUICK_VIEW, toggleEnableQuickView);
    Menus.getMenu(Menus.AppMenuBar.VIEW_MENU).addMenuItem(CMD_ENABLE_QUICK_VIEW);
    
    // Init PreferenceStorage
    prefs = PreferencesManager.getPreferenceStorage(module, defaultPrefs);

    // Setup initial UI state
    setEnabled(prefs.getValue("enabled"));
    
    // For unit testing
    exports._queryPreviewProviders  = queryPreviewProviders;
    exports._forceShow              = function (popover) {
        hidePreview();
        popoverState = popover;
        showPreview();
    };
});
