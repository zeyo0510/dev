
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

define('text!CSSProperties.json',[],function () { return '{\n    "align-content":               {"values": ["center", "flex-end", "flex-start", "space-around", "space-between", "stretch"]},\n    "align-items":                 {"values": ["baseline", "center", "flex-end", "flex-start", "stretch"]},\n    "align-self":                  {"values": ["auto", "baseline", "center", "flex-end", "flex-start", "stretch"]},\n    "animation":                   {"values": []},\n    "animation-delay":             {"values": []},\n    "animation-direction":         {"values": ["alternate", "alternate-reverse", "normal", "reverse"]},\n    "animation-duration":          {"values": []},\n    "animation-fill-mode":         {"values": ["backwards", "both", "forwards", "none"]},\n    "animation-iteration-count":   {"values": ["infinite"]},\n    "animation-name":              {"values": ["none"]},\n    "animation-play-state":        {"values": ["paused", "running"]},\n    "animation-timing-function":   {"values": ["cubic-bezier()", "ease", "ease-in", "ease-in-out", "ease-out", "linear", "step-end", "step-start", "steps()"]},\n    "backface-visibility":         {"values": ["hidden", "visible"]},\n    "background":                  {"values": []},\n    "background-attachment":       {"values": ["fixed", "local", "scroll", "inherit"]},\n    "background-clip":             {"values": ["border-box", "content-box", "padding-box", "inherit"]},\n    "background-color":            {"values": ["currentColor", "transparent", "inherit"]},\n    "background-image":            {"values": ["image()", "linear-gradient()", "radial-gradient()", "repeating-linear-gradient()", "repeating-radial-gradient()", "url()"]},\n    "background-origin":           {"values": ["border-box", "content-box", "padding-box", "inherit"]},\n    "background-position":         {"values": ["left", "center", "right", "bottom", "top"]},\n    "background-repeat":           {"values": ["no-repeat", "repeat", "repeat-x", "repeat-y", "round", "space"]},\n    "background-size":             {"values": ["auto", "contain", "cover"]},\n    "border":                      {"values": []},\n    "border-collapse":             {"values": ["collapse", "separate", "inherit"]},\n    "border-color":                {"values": ["currentColor", "transparent", "inherit"]},\n    "border-spacing":              {"values": ["inherit"]},\n    "border-style":                {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "border-bottom":               {"values": []},\n    "border-bottom-color":         {"values": ["currentColor", "transparent", "inherit"]},\n    "border-bottom-left-radius":   {"values": []},\n    "border-bottom-right-radius":  {"values": []},\n    "border-bottom-style":         {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "border-bottom-width":         {"values": ["medium", "thin", "thick", "inherit"]},\n    "border-image":                {"values": [ "url()" ]},\n    "border-image-outset":         {"values": []},\n    "border-image-slice":          {"values": []},\n    "border-image-source":         {"values": []},\n    "border-image-repeat":         {"values": ["repeat", "round", "space", "stretch"]},\n    "border-image-width":          {"values": ["auto"]},    \n    "border-left":                 {"values": []},\n    "border-left-color":           {"values": ["currentColor", "transparent", "inherit"]},\n    "border-left-style":           {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "border-left-width":           {"values": ["medium", "thin", "thick", "inherit"]},\n    "border-radius":               {"values": []},\n    "border-right":                {"values": []},\n    "border-right-color":          {"values": ["currentColor", "transparent", "inherit"]},\n    "border-right-style":          {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "border-right-width":          {"values": ["medium", "thin", "thick", "inherit"]},\n    "border-top":                  {"values": []},\n    "border-top-color":            {"values": ["currentColor", "transparent", "inherit"]},\n    "border-top-left-radius":      {"values": []},\n    "border-top-right-radius":     {"values": []},\n    "border-top-style":            {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "border-top-width":            {"values": ["medium", "thin", "thick", "inherit"]},\n    "box-decoration-break":        {"values": []},\n    "box-shadow":                  {"values": []},\n    "box-sizing":                  {"values": ["border-box", "content-box", "padding-box", "inherit"]},\n    "bottom":                      {"values": ["auto", "inherit"]},\n    "break-after":                 {"values": ["always", "auto", "avoid", "avoid-column", "avoid-page", "column", "left", "page", "right"]},\n    "break-before":                {"values": ["always", "auto", "avoid", "avoid-column", "avoid-page", "column", "left", "page", "right"]},\n    "break-inside":                {"values": ["auto", "avoid", "avoid-column", "avoid-page"]},\n    "caption-side":                {"values": ["bottom", "top", "inherit"]},\n    "clear":                       {"values": ["both", "left", "none", "right", "inherit"]},\n    "clip":                        {"values": ["auto", "inherit"]},\n    "color":                       {"values": ["inherit"]},\n    "columns":                     {"values": []},\n    "column-count":                {"values": []},\n    "column-fill":                 {"values": ["auto", "balance"]},\n    "column-gap":                  {"values": ["normal"]},\n    "column-rule":                 {"values": []},\n    "column-rule-color":           {"values": ["currentColor"]},\n    "column-rule-style":           {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "column-rule-width":           {"values": ["medium", "thin", "thick", "inherit"]},\n    "column-span":                 {"values": ["all", "none"]},\n    "column-width":                {"values": ["auto", "inherit"]},\n    "content":                     {"values": ["attr()", "close-quote", "no-close-quote", "no-open-quote", "normal", "none", "open-quote", "inherit"]},\n    "counter-increment":           {"values": ["none", "inherit"]},\n    "counter-reset":               {"values": ["none", "inherit"]},\n    "cursor":                      {"values": ["auto", "crosshair", "e-resize", "default", "help", "move", "n-resize", "ne-resize", "nw-resize", "pointer", "progress", "s-resize", "se-resize", "sw-resize", "text", "w-resize", "wait", "inherit"]},\n    "direction":                   {"values": ["ltr", "rtl", "inherit"]},\n    "display":                     {"values": ["block", "flex", "inline", "inline-block", "inline-flex", "inline-table", "list-item", "none", "table", "table-caption", "table-cell", "table-column", "table-column-group", "table-footer-group", "table-header-group", "table-row", "table-row-group", "inherit"]},\n    "empty-cells":                 {"values": ["hide", "show", "inherit"]},    \n    "flex":                        {"values": ["none"]},\n    "flex-basis":                  {"values": []},\n    "flex-direction":              {"values": ["column", "column-reverse", "row", "row-reverse"]},\n    "flex-flow":                   {"values": ["column", "column-reverse", "nowrap", "row", "row-reverse", "wrap", "wrap-reverse"]},\n    "flex-grow":                   {"values": []},\n    "flex-shrink":                 {"values": []},\n    "flex-wrap":                   {"values": ["nowrap", "wrap", "wrap-reverse"]},\n    "float":                       {"values": ["left", "right", "none", "inherit"]},\n    "font":                        {"values": []},\n    "font-family":                 {"values": ["cursive", "fantasy", "inherit", "monospace", "sans-serif", "serif"]},\n    "font-feature-settings":       {"values": ["normal"]},\n    "font-kerning":                {"values": ["auto", "none", "normal"]},\n    "font-language-override":      {"values": ["normal"]},\n    "font-size":                   {"values": []},\n    "font-size-adjust":            {"values": ["auto", "none"]},\n    "font-stretch":                {"values": ["condensed", "expanded", "extra-condensed", "extra-expanded", "normal", "semi-condensed", "semi-expanded", "ultra-condensed", "ultra-expanded"]},\n    "font-style":                  {"values": ["italic", "normal", "oblique"]},\n    "font-synthesis":              {"values": ["none", "style", "weight"]},\n    "font-variant":                {"values": ["normal", "none"]},\n    "font-variant-alternates":     {"values": ["normal"]},\n    "font-variant-caps":           {"values": ["normal", "small-caps", "all-small-caps", "petite-caps", "all-petite-caps", "unicase", "titling-caps"]},\n    "font-variant-east-asian":     {"values": ["normal"]},\n    "font-variant-ligatures":      {"values": ["normal", "none"]},\n    "font-variant-numeric":        {"values": ["normal"]},\n    "font-variant-position":       {"values": ["normal", "sub", "super"]},\n    "font-weight":                 {"values": ["bold", "bolder", "lighter", "normal", "100", "200", "300", "400", "500", "600", "700", "800", "900", "inherit"]},\n    "height":                      {"values": ["auto", "inherit"]},\n    "hyphens":                     {"values": ["auto", "manual", "none"]},\n    "image-orientation":           {"values": []},\n    "image-resolution":            {"values": ["from-image", "snap"]},\n    "justify-content":             {"values": ["center", "flex-end", "flex-start", "space-around", "space-between"]},\n    "left":                        {"values": ["auto", "inherit"]},\n    "letter-spacing":              {"values": ["normal", "inherit"]},\n    "line-height":                 {"values": ["normal", "inherit"]},\n    "list-style":                  {"values": []},\n    "list-style-image":            {"values": [ "url()" ]},\n    "list-style-position":         {"values": ["inside", "outside", "inherit"]},\n    "list-style-type":             {"values": ["armenian", "circle", "decimal", "decimal-leading-zero", "disc", "georgian", "lower-alpha", "lower-greek", "lower-latin", "lower-roman", "none", "square", "upper-alpha", "upper-latin", "upper-roman", "inherit"]},\n    "margin":                      {"values": ["inherit"]},\n    "margin-bottom":               {"values": []},\n    "margin-left":                 {"values": []},\n    "margin-right":                {"values": []},\n    "margin-top":                  {"values": []},\n    "max-height":                  {"values": ["none", "inherit"]},\n    "max-width":                   {"values": ["none", "inherit"]},\n    "min-height":                  {"values": ["inherit"]},\n    "min-width":                   {"values": ["inherit"]},\n    "object-fit":                  {"values": ["contain", "cover", "fill", "none", "scale-down"]},\n    "object-position":             {"values": ["left", "center", "right", "bottom", "top"]},\n    "opacity":                     {"values": ["inherit"]},\n    "order":                       {"values": []},\n    "orphans":                     {"values": ["inherit"]},\n    "outline":                     {"values": ["inherit"]},\n    "outline-color":               {"values": ["invert", "inherit"]},\n    "outline-offset":              {"values": ["inherit"]},\n    "outline-style":               {"values": ["dashed", "dotted", "double", "groove", "hidden", "inset", "none", "outset", "ridge", "solid", "inherit"]},\n    "outline-width":               {"values": ["medium", "thin", "thick", "inherit"]},\n    "overflow":                    {"values": ["auto", "hidden", "scroll", "visible", "inherit"]},\n    "overflow-x":                  {"values": ["auto", "hidden", "scroll", "visible", "inherit"]},\n    "overflow-y":                  {"values": ["auto", "hidden", "scroll", "visible", "inherit"]},\n    "padding":                     {"values": ["inherit"]},\n    "padding-bottom":              {"values": []},\n    "padding-left":                {"values": []},\n    "padding-right":               {"values": []},\n    "padding-top":                 {"values": []},\n    "page-break-after":            {"values": ["always", "auto", "avoid", "left", "right", "inherit"]},\n    "page-break-before":           {"values": ["always", "auto", "avoid", "left", "right", "inherit"]},\n    "page-break-inside":           {"values": ["auto", "avoid", "inherit"]},\n    "perspective":                 {"values": ["none"]},\n    "perspective-origin":          {"values": ["bottom", "center", "left", "right", "top"]},\n    "position":                    {"values": ["absolute", "fixed", "relative", "static", "sticky", "inherit"]},\n    "quotes":                      {"values": ["none", "inherit"]},\n    "resize":                      {"values": ["both", "horizontal", "none", "vertical", "inherit"]},\n    "right":                       {"values": ["auto", "inherit"]},\n    "src":                         {"values": [ "url()"]},\n    "table-layout":                {"values": ["auto", "fixed", "inherit"]},\n    "text-align":                  {"values": ["center", "left", "justify", "right", "inherit"]},\n    "text-align-last":             {"values": ["center", "left", "justify", "right", "inherit"]},\n    "text-decoration":             {"values": ["line-through", "none", "overline", "underline", "inherit"]},\n    "text-decoration-color":       {"values": ["currentColor"]},\n    "text-decoration-line":        {"values": ["line-through", "none", "overline", "underline"]},\n    "text-decoration-skip":        {"values": ["edges", "ink", "none", "objects", "spaces"]},\n    "text-decoration-style":       {"values": ["dashed", "dotted", "double", "solid", "wavy"]},\n    "text-emphasis":               {"values": []},\n    "text-emphasis-color":         {"values": ["currentColor"]},\n    "text-emphasis-position":      {"values": ["above", "below", "left", "right"]},\n    "text-emphasis-style":         {"values": ["circle", "dot", "double-circle", "filled", "none", "open", "sesame", "triangle"]},\n    "text-indent":                 {"values": ["inherit"]},\n    "text-overflow":               {"values": ["clip", "ellipsis", "inherit"]},\n    "text-shadow":                 {"values": []},\n    "text-transform":              {"values": ["capitalize", "full-width", "lowercase", "none", "uppercase", "inherit"]},\n    "text-underline-position":     {"values": ["alphabetic", "auto", "below", "left", "right"]}, \n    "top":                         {"values": ["auto", "inherit"]},\n    "transform":                   {"values": ["matrix()", "matrix3d()", "perspective()", "rotate()", "rotate3d()", "rotateX()", "rotateY()", "rotateZ()", "scale()", "scale3d()", "scaleX()", "scaleY()", "scaleZ()", "skewX()", "skewY()", "translate()", "translate3d()", "translateX()", "translateY()", "translateZ()"]},\n    "transform-origin":            {"values": ["bottom", "center", "left", "right", "top"]},\n    "transform-style":             {"values": ["flat", "preserve-3d"]},\n    "transition":                  {"values": []},\n    "transition-delay":            {"values": []},\n    "transition-duration":         {"values": []},\n    "transition-property":         {"values": ["all", "none"]},\n    "transition-timing-function":  {"values": ["cubic-bezier()", "ease", "ease-in", "ease-in-out", "ease-out", "linear", "step-end", "step-start", "steps()"]},\n    "unicode-bidi":                {"values": ["bidi-override", "embed", "normal", "inherit"]},\n    "unicode-range":               {"values": []},\n    "vertical-align":              {"values": ["baseline", "bottom", "middle", "sub", "super", "text-bottom", "text-top", "top", "inherit"]},\n    "visibility":                  {"values": ["collapse", "hidden", "visible", "inherit"]},\n    "white-space":                 {"values": ["normal", "nowrap", "pre", "pre-line", "pre-wrap", "inherit"]},\n    "widows":                      {"values": ["inherit"]},\n    "width":                       {"values": ["auto", "inherit"]},\n    "word-spacing":                {"values": ["normal", "inherit"]},\n    "word-wrap":                   {"values": ["break-word", "normal"]},\n    "z-index":                     {"values": ["auto", "inherit"]}\n}';});

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
/*global define, brackets, $, window */

define('main',['require','exports','module','text!CSSProperties.json'],function (require, exports, module) {
    

    var AppInit             = brackets.getModule("utils/AppInit"),
        CodeHintManager     = brackets.getModule("editor/CodeHintManager"),
        CSSUtils            = brackets.getModule("language/CSSUtils"),
        TokenUtils          = brackets.getModule("utils/TokenUtils"),
        CSSProperties       = require("text!CSSProperties.json"),
        properties          = JSON.parse(CSSProperties);
    
    // Context of the last request for hints: either CSSUtils.PROP_NAME,
    // CSSUtils.PROP_VALUE or null.
    var lastContext;
    
    /**
     * @constructor
     */
    function CssPropHints() {
        this.primaryTriggerKeys = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-()";
        this.secondaryTriggerKeys = ":";
        this.exclusion = null;
    }

    /**
     * Check whether the exclusion is still the same as text after the cursor. 
     * If not, reset it to null.
     *
     * @param {boolean} propNameOnly
     * true to indicate that we update the exclusion only if the cursor is inside property name context.
     * Otherwise, we also update exclusion for property value context.
     */
    CssPropHints.prototype.updateExclusion = function (propNameOnly) {
        var textAfterCursor;
        if (this.exclusion && this.info) {
            if (this.info.context === CSSUtils.PROP_NAME) {
                textAfterCursor = this.info.name.substr(this.info.offset);
            } else if (!propNameOnly && this.info.context === CSSUtils.PROP_VALUE) {
                textAfterCursor = this.info.value.substr(this.info.offset);
            }
            if (!CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                this.exclusion = null;
            }
        }
    };
    
    /**
     * Determines whether CSS propertyname or -name hints are available in the current editor
     * context.
     * 
     * @param {Editor} editor 
     * A non-null editor object for the active window.
     *
     * @param {String} implicitChar 
     * Either null, if the hinting request was explicit, or a single character
     * that represents the last insertion and that indicates an implicit
     * hinting request.
     *
     * @return {Boolean} 
     * Determines whether the current provider is able to provide hints for
     * the given editor context and, in case implicitChar is non- null,
     * whether it is appropriate to do so.
     */
    CssPropHints.prototype.hasHints = function (editor, implicitChar) {
        this.editor = editor;
        var cursor = this.editor.getCursorPos(),
            textAfterCursor;

        lastContext = null;
        this.info = CSSUtils.getInfoAtPos(editor, cursor);
        
        if (this.info.context !== CSSUtils.PROP_NAME && this.info.context !== CSSUtils.PROP_VALUE) {
            return false;
        }
        
        if (implicitChar) {
            this.updateExclusion(false);
            if (this.info.context === CSSUtils.PROP_NAME) {
                // Check if implicitChar is the first character typed before an existing property name.
                if (!this.exclusion && this.info.offset === 1 && implicitChar === this.info.name[0]) {
                    this.exclusion = this.info.name.substr(this.info.offset);
                }
            }
            
            return (this.primaryTriggerKeys.indexOf(implicitChar) !== -1) ||
                   (this.secondaryTriggerKeys.indexOf(implicitChar) !== -1);
        } else if (this.info.context === CSSUtils.PROP_NAME) {
            if (this.info.offset === 0) {
                this.exclusion = this.info.name;
            } else {
                this.updateExclusion(true);
            }
        }
        
        return true;
    };
       
    /**
     * Returns a list of availble CSS protertyname or -value hints if possible for the current
     * editor context. 
     * 
     * @param {Editor} implicitChar 
     * Either null, if the hinting request was explicit, or a single character
     * that represents the last insertion and that indicates an implicit
     * hinting request.
     *
     * @return {{hints: Array.<string|jQueryObject>, match: string, 
     *      selectInitial: boolean}}
     * Null if the provider wishes to end the hinting session. Otherwise, a
     * response object that provides 
     * 1. a sorted array hints that consists of strings
     * 2. a string match that is used by the manager to emphasize matching 
     *    substrings when rendering the hint list 
     * 3. a boolean that indicates whether the first result, if one exists, should be 
     *    selected by default in the hint list window.
     */
    CssPropHints.prototype.getHints = function (implicitChar) {
        this.info = CSSUtils.getInfoAtPos(this.editor, this.editor.getCursorPos());

        var needle = this.info.name,
            valueNeedle = "",
            context = this.info.context,
            result,
            selectInitial = false;
            
        
        if (this.primaryTriggerKeys.indexOf(implicitChar) !== -1) {
            selectInitial = true;
        }
        
        // Clear the exclusion if the user moves the cursor with left/right arrow key.
        this.updateExclusion(true);

        if (context === CSSUtils.PROP_VALUE) {
            // When switching from a NAME to a VALUE context, restart the session
            // to give other more specialized providers a chance to intervene.
            if (lastContext === CSSUtils.PROP_NAME) {
                return true;
            } else {
                lastContext = CSSUtils.PROP_VALUE;
            }
            
            if (!properties[needle]) {
                return null;
            }
            
            // Cursor is in an existing property value or partially typed value
            if (!this.info.isNewItem && this.info.index !== -1) {
                valueNeedle = this.info.values[this.info.index].trim();
                valueNeedle = valueNeedle.substr(0, this.info.offset);
            }
            
            result = $.map(properties[needle].values, function (pvalue, pindex) {
                if (pvalue.indexOf(valueNeedle) === 0) {
                    return pvalue;
                }
            }).sort();
            
            return {
                hints: result,
                match: valueNeedle,
                selectInitial: selectInitial
            };
        } else if (context === CSSUtils.PROP_NAME) {
            lastContext = CSSUtils.PROP_NAME;
            needle = needle.substr(0, this.info.offset);
            result = $.map(properties, function (pvalues, pname) {
                if (pname.indexOf(needle) === 0) {
                    return pname;
                }
            }).sort();
            
            return {
                hints: result,
                match: needle,
                selectInitial: selectInitial
            };
        }
        return null;
    };
    
    /**
     * Inserts a given CSS protertyname or -value hint into the current editor context. 
     * 
     * @param {String} hint 
     * The hint to be inserted into the editor context.
     * 
     * @return {Boolean} 
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    CssPropHints.prototype.insertHint = function (hint) {
        var offset = this.info.offset,
            cursor = this.editor.getCursorPos(),
            start = {line: -1, ch: -1},
            end = {line: -1, ch: -1},
            keepHints = false,
            adjustCursor = false,
            newCursor,
            ctx;
        
        if (this.info.context !== CSSUtils.PROP_NAME && this.info.context !== CSSUtils.PROP_VALUE) {
            return false;
        }
        
        start.line = end.line = cursor.line;
        start.ch = cursor.ch - offset;

        if (this.info.context === CSSUtils.PROP_NAME) {
            keepHints = true;
            var textAfterCursor = this.info.name.substr(this.info.offset);
            if (this.info.name.length === 0 || CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                // It's a new insertion, so append a colon and set keepHints
                // to show property value hints.
                hint += ":";
                end.ch = start.ch;
                end.ch += offset;
                    
                if (this.exclusion) {
                    // Append a space to the end of hint to insert and then adjust
                    // the cursor before that space.
                    hint += " ";
                    adjustCursor = true;
                    newCursor = { line: cursor.line,
                                  ch: start.ch + hint.length - 1 };
                    this.exclusion = null;
                }
            } else {
                // It's a replacement of an existing one or just typed in property.
                // So we need to check whether there is an existing colon following 
                // the current property name. If a colon already exists, then we also 
                // adjust the cursor position and show code hints for property values.
                end.ch = start.ch + this.info.name.length;
                ctx = TokenUtils.getInitialContext(this.editor._codeMirror, cursor);
                if (ctx.token.string.length > 0 && !ctx.token.string.match(/\S/)) {
                    // We're at the very beginning of a property name. So skip it 
                    // before we locate the colon following it.
                    TokenUtils.moveNextToken(ctx);
                }
                if (TokenUtils.moveSkippingWhitespace(TokenUtils.moveNextToken, ctx) && ctx.token.string === ":") {
                    adjustCursor = true;
                    newCursor = { line: cursor.line,
                                  ch: cursor.ch + (hint.length - this.info.name.length) };
                } else {
                    hint += ":";
                }
            }
        } else {
            if (!this.info.isNewItem && this.info.index !== -1) {
                // Replacing an existing property value or partially typed value
                end.ch = start.ch + this.info.values[this.info.index].length;
            } else {
                // Inserting a new property value
                end.ch = start.ch;
            }

            var parenMatch = hint.match(/url\([\w\W]*?\)/i);
            if (parenMatch) {
                // value has url(...), so place cursor inside opening paren
                // and keep hints open
                adjustCursor = true;
                newCursor = { line: cursor.line,
                              ch: cursor.ch + 4 - this.info.offset };
                keepHints = true;
            }
        }
        
        // HACK (tracking adobe/brackets#1688): We talk to the private CodeMirror instance
        // directly to replace the range instead of using the Document, as we should. The
        // reason is due to a flaw in our current document synchronization architecture when
        // inline editors are open.
        this.editor._codeMirror.replaceRange(hint, start, end);
        
        if (adjustCursor) {
            this.editor.setCursorPos(newCursor);
        }
        
        return keepHints;
    };
    
    AppInit.appReady(function () {
        var cssPropHints = new CssPropHints();
        CodeHintManager.registerHintProvider(cssPropHints, ["css"], 0);
        
        // For unit testing
        exports.cssPropHintProvider = cssPropHints;
    });
});