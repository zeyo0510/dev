
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

define('text!HtmlTags.json',[],function () { return '{     \n    "a":          { "attributes": ["href", "hreflang", "media", "rel", "target", "type"] },\n    "abbr":       { "attributes": [] },\n    "address":    { "attributes": [] },\n    "area":       { "attributes": ["alt", "coords", "href", "hreflang", "media", "rel", "shape", "target", "type"] },\n    "article":    { "attributes": [] },\n    "aside":      { "attributes": [] },\n    "audio":      { "attributes": ["autoplay", "controls", "loop", "mediagroup", "muted", "preload", "src"] },\n    "b":          { "attributes": [] },\n    "base":       { "attributes": ["href", "target"] },\n    "bdi":        { "attributes": [] },\n    "bdo":        { "attributes": [] },\n    "big":        { "attributes": [] },\n    "blockquote": { "attributes": ["cite"] },\n    "body":       { "attributes": ["onafterprint", "onbeforeprint", "onbeforeunload", "onhashchange", "onmessage", "onoffline", "ononline",\n                                   "onpagehide", "onpageshow", "onpopstate", "onredo", "onresize", "onstorage", "onundo", "onunload"] },\n    "br":         { "attributes": [] },\n    "button":     { "attributes": ["autofocus", "disabled", "form", "formaction", "formenctype", "formmethod", "formnovalidate", "formtarget", \n                                   "name", "type", "value"] },\n    "canvas":     { "attributes": ["height", "width"] },\n    "caption":    { "attributes": [] },\n    "cite":       { "attributes": [] },\n    "code":       { "attributes": [] },\n    "col":        { "attributes": ["span"] },\n    "colgroup":   { "attributes": ["span"] },\n    "command":    { "attributes": ["checked", "disabled", "icon", "label", "radiogroup", "type"] },\n    "datalist":   { "attributes": [] },\n    "dd":         { "attributes": [] },\n    "del":        { "attributes": ["cite", "datetime"] },\n    "details":    { "attributes": ["open"] },\n    "dfn":        { "attributes": [] },\n    "div":        { "attributes": [] },\n    "dl":         { "attributes": [] },\n    "dt":         { "attributes": [] },\n    "em":         { "attributes": [] },\n    "embed":      { "attributes": ["height", "src", "type", "width"] },\n    "fieldset":   { "attributes": ["disabled", "form", "name"] },\n    "figcaption": { "attributes": [] },\n    "figure":     { "attributes": [] },\n    "footer":     { "attributes": [] },\n    "form":       { "attributes": ["accept-charset", "action", "autocomplete", "enctype", "method", "name", "novalidate", "target"] },\n    "h1":         { "attributes": [] },\n    "h2":         { "attributes": [] },\n    "h3":         { "attributes": [] },\n    "h4":         { "attributes": [] },\n    "h5":         { "attributes": [] },\n    "h6":         { "attributes": [] },\n    "head":       { "attributes": [] },\n    "header":     { "attributes": [] },\n    "hgroup":     { "attributes": [] },\n    "hr":         { "attributes": [] },\n    "html":       { "attributes": ["manifest", "xml:lang", "xmlns"] },\n    "i":          { "attributes": [] },\n    "iframe":     { "attributes": ["height", "name", "sandbox", "seamless", "src", "srcdoc", "width"] },\n    "ilayer":     { "attributes": [] },\n    "img":        { "attributes": ["alt", "height", "ismap", "longdesc", "src", "usemap", "width"] },\n    "input":      { "attributes": ["accept", "alt", "autocomplete", "autofocus", "checked", "dirname", "disabled", "form", "formaction", "formenctype", "formmethod", \n                                   "formnovalidate", "formtarget", "height", "list", "max", "maxlength", "min", "multiple", "name", "pattern", "placeholder", "readonly", \n                                   "required", "size", "src", "step", "type", "value", "width"] },\n    "ins":        { "attributes": ["cite", "datetime"] },\n    "kbd":        { "attributes": [] },\n    "keygen":     { "attributes": ["autofocus", "challenge", "disabled", "form", "keytype", "name"] },\n    "label":      { "attributes": ["for", "form"] },\n    "legend":     { "attributes": [] },\n    "li":         { "attributes": ["value"] },\n    "link":       { "attributes": ["disabled", "href", "hreflang", "media", "rel", "sizes", "type"] },\n    "map":        { "attributes": ["name"] },\n    "mark":       { "attributes": [] },\n    "marquee":    { "attributes": ["align", "behavior", "bgcolor", "direction", "height", "hspace", "loop", "scrollamount", "scrolldelay", "truespeed", "vspace", "width"] },\n    "menu":       { "attributes": ["label", "type"] },\n    "meta":       { "attributes": ["charset", "content", "http-equiv", "name"] },\n    "meter":      { "attributes": ["form", "high", "low", "max", "min", "optimum", "value"] },\n    "nav":        { "attributes": [] },\n    "noscript":   { "attributes": [] },\n    "object":     { "attributes": ["archive", "codebase", "codetype", "data", "declare", "form", "height", "name", "standby", "type", "usemap", "width"] },\n    "ol":         { "attributes": ["reversed", "start", "type"] },\n    "optgroup":   { "attributes": ["disabled", "label"] },\n    "option":     { "attributes": ["disabled", "label", "selected", "value"] },\n    "output":     { "attributes": ["for", "form", "name"] },\n    "p":          { "attributes": [] },\n    "param":      { "attributes": ["name", "value"] },\n    "pre":        { "attributes": [] },\n    "progress":   { "attributes": ["form", "max", "value"] },\n    "q":          { "attributes": ["cite"] },\n    "rp":         { "attributes": [] },\n    "rt":         { "attributes": [] },\n    "ruby":       { "attributes": [] },\n    "samp":       { "attributes": [] },\n    "script":     { "attributes": ["async", "charset", "defer", "src", "type"] },\n    "section":    { "attributes": [] },\n    "select":     { "attributes": ["autofocus", "disabled", "form", "multiple", "name", "required", "size"] },\n    "small":      { "attributes": [] },\n    "source":     { "attributes": ["media", "src", "type"] },\n    "span":       { "attributes": [] },\n    "strong":     { "attributes": [] },\n    "style":      { "attributes": ["disabled", "media", "scoped", "type"] },\n    "sub":        { "attributes": [] },\n    "summary":    { "attributes": [] },\n    "sup":        { "attributes": [] },\n    "table":      { "attributes": ["border"] },\n    "tbody":      { "attributes": [] },\n    "td":         { "attributes": ["colspan", "headers", "rowspan"] },\n    "textarea":   { "attributes": ["autofocus", "cols", "dirname", "disabled", "form", "label", "maxlength", "name", "placeholder", "readonly", "required", "rows", "wrap"] },\n    "tfoot":      { "attributes": [] },\n    "th":         { "attributes": ["colspan", "headers", "rowspan", "scope"] },\n    "thead":      { "attributes": [] },\n    "time":       { "attributes": ["datetime", "pubdate"] },\n    "title":      { "attributes": [] },\n    "tr":         { "attributes": [] },\n    "track":      { "attributes": ["default", "kind", "label", "src", "srclang"] },\n    "tt":         { "attributes": [] },\n    "ul":         { "attributes": [] },\n    "var":        { "attributes": [] },\n    "video":      { "attributes": ["autoplay", "controls", "height", "loop", "mediagroup", "muted", "poster", "preload", "src", "width"] },\n    "wbr":        { "attributes": [] }\n}';});

define('text!HtmlAttributes.json',[],function () { return '{\n    "accesskey":          { "attribOption": [], "global": "true" },\n    "class":              { "attribOption": [], "global": "true", "type": "cssStyle" },\n    "contenteditable":    { "attribOption": [], "global": "true", "type": "boolean" },\n    "contextmenu":        { "attribOption": [], "global": "true" },\n    "dir":                { "attribOption": ["ltr", "rtl"], "global": "true"},\n    "draggable":          { "attribOption": ["auto", "false", "true"], "global": "true" },\n    "dropzone":           { "attribOption": ["copy", "move", "link"], "global": "true" },\n    "hidden":             { "attribOption": ["hidden"], "global": "true" },\n    "id":                 { "attribOption": [], "global": "true", "type": "cssId" },\n    "lang":               { "attribOption": ["ab", "aa", "af", "sq", "am", "ar", "an", "hy", "as", "ay", "az", "ba", "eu", "bn", "dz", "bh", "bi", "br", \n                                             "bg", "my", "be", "km", "ca", "zh", "co", "hr", "cs", "da", "nl", "en", "eo", "et", "fo", "fa", "fi", "fr", \n                                             "fy", "gl", "gd", "gv", "ka", "de", "el", "kl", "gn", "gu", "ht", "ha", "he", "hi", "hu", "is", "io", "id", \n                                             "ia", "ie", "iu", "ik", "ga", "it", "ja", "jv", "kn", "ks", "kk", "rw", "ky", "rn", "ko", "ku", "lo", "la", \n                                             "lv", "li", "ln", "lt", "mk", "mg", "ms", "ml", "mt", "mi", "mr", "mo", "mn", "na", "ne", "no", "oc", "or", \n                                             "om", "ps", "pl", "pt", "pa", "qu", "rm", "ro", "ru", "sz", "sm", "sg", "sa", "sr", "sh", "st", "tn", "sn", \n                                             "ii", "sd", "si", "ss", "sk", "sl", "so", "es", "su", "sw", "sv", "tl", "tg", "ta", "tt", "te", "th", "bo", \n                                             "ti", "to", "ts", "tr", "tk", "tw", "ug", "uk", "ur", "uz", "vi", "vo", "wa", "cy", "wo", "xh", "yi", "yo", \n                                             "zu"], \n                            "global": "true" },\n    "role":               { "attribOption": ["alert", "alertdialog", "article", "application", "banner", "button", "checkbox", "columnheader", "combobox", \n                                             "complementary", "contentinfo", "definition", "directory", "dialog", "document", "form", "grid", "gridcell", \n                                             "group", "heading", "img", "link", "list", "listbox", "listitem", "log", "main", "marquee", "math", "menu", \n                                             "menubar", "menuitem", "menuitemcheckbox", "menuitemradio", "navigation", "note", "option", "presentation", \n                                             "progressbar", "radio", "radiogroup", "region", "row", "rowgroup", "rowheader", "scrollbar", "search", \n                                             "separator", "slider", "spinbutton", "status", "tab", "tablist", "tabpanel", "textbox", "timer", "toolbar", \n                                             "tooltip", "tree", "treegrid", "treeitem"], \n                            "global": "true" },\n    "spellcheck":         { "attribOption": [], "global": "true", "type": "boolean" },\n    "style":              { "attribOption": [], "global": "true", "type": "style" },\n    "tabindex":           { "attribOption": [], "global": "true" },\n    "title":              { "attribOption": [], "global": "true" },\n  \n    "onabort":            { "attribOption": [], "global": "true" },\n    "onblur":             { "attribOption": [], "global": "true" },\n    "oncanplay":          { "attribOption": [], "global": "true" },\n    "oncanplaythrough":   { "attribOption": [], "global": "true" },\n    "onchange":           { "attribOption": [], "global": "true" },\n    "onclick":            { "attribOption": [], "global": "true" },\n    "oncontextmenu":      { "attribOption": [], "global": "true" },\n    "oncuechange":        { "attribOption": [], "global": "true" },\n    "ondblclick":         { "attribOption": [], "global": "true" },\n    "ondrag":             { "attribOption": [], "global": "true" },\n    "ondragend":          { "attribOption": [], "global": "true" },\n    "ondragenter":        { "attribOption": [], "global": "true" },\n    "ondragleave":        { "attribOption": [], "global": "true" },\n    "ondragover":         { "attribOption": [], "global": "true" },\n    "ondragstart":        { "attribOption": [], "global": "true" },\n    "ondrop":             { "attribOption": [], "global": "true" },\n    "ondurationchange":   { "attribOption": [], "global": "true" },\n    "onemptied":          { "attribOption": [], "global": "true" },\n    "onended":            { "attribOption": [], "global": "true" },\n    "onerror":            { "attribOption": [], "global": "true" },\n    "onfocus":            { "attribOption": [], "global": "true" },\n    "oninput":            { "attribOption": [], "global": "true" },\n    "oninvalid":          { "attribOption": [], "global": "true" },\n    "onkeydown":          { "attribOption": [], "global": "true" },\n    "onkeypress":         { "attribOption": [], "global": "true" },\n    "onkeyup":            { "attribOption": [], "global": "true" },\n    "onload":             { "attribOption": [], "global": "true" },\n    "onloadeddata":       { "attribOption": [], "global": "true" },\n    "onloadedmetadata":   { "attribOption": [], "global": "true" },\n    "onloadstart":        { "attribOption": [], "global": "true" },\n    "onmousedown":        { "attribOption": [], "global": "true" },\n    "onmousemove":        { "attribOption": [], "global": "true" },\n    "onmouseout":         { "attribOption": [], "global": "true" },\n    "onmouseover":        { "attribOption": [], "global": "true" },\n    "onmouseup":          { "attribOption": [], "global": "true" },\n    "onmousewheel":       { "attribOption": [], "global": "true" },\n    "onpause":            { "attribOption": [], "global": "true" },\n    "onplay":             { "attribOption": [], "global": "true" },\n    "onplaying":          { "attribOption": [], "global": "true" },\n    "onprogress":         { "attribOption": [], "global": "true" },\n    "onratechange":       { "attribOption": [], "global": "true" },\n    "onreadystatechange": { "attribOption": [], "global": "true" },\n    "onreset":            { "attribOption": [], "global": "true" },\n    "onscroll":           { "attribOption": [], "global": "true" },\n    "onseeked":           { "attribOption": [], "global": "true" },\n    "onseeking":          { "attribOption": [], "global": "true" },\n    "onselect":           { "attribOption": [], "global": "true" },\n    "onshow":             { "attribOption": [], "global": "true" },\n    "onstalled":          { "attribOption": [], "global": "true" },\n    "onsubmit":           { "attribOption": [], "global": "true" },\n    "onsuspend":          { "attribOption": [], "global": "true" },\n    "ontimeupdate":       { "attribOption": [], "global": "true" },\n    "onvolumechange":     { "attribOption": [], "global": "true" },\n    "onwaiting":          { "attribOption": [], "global": "true" },\n      \n    "accept":             { "attribOption": ["text/html", "text/plain", "application/msword", "application/msexcel", "application/postscript",\n                                             "application/x-zip-compressed", "application/pdf", "application/rtf", "video/x-msvideo", "video/quicktime",\n                                             "video/x-mpeg2", "audio/x-pn/realaudio", "audio/x-mpeg", "audio/x-waw", "audio/x-aiff", "audio/basic",\n                                             "image/tiff", "image/jpeg", "image/gif", "image/x-png", "image/x-photo-cd", "image/x-MS-bmp", "image/x-rgb",\n                                             "image/x-portable-pixmap", "image/x-portable-greymap", "image/x-portablebitmap"] },\n    "accept-charset":     { "attribOption": [] },\n    "action":             { "attribOption": [] },\n    "align":              { "attribOption": [] },\n    "alt":                { "attribOption": [] },\n    "archive":            { "attribOption": [] },\n    "async":              { "attribOption": [], "type": "flag" },\n    "autocomplete":       { "attribOption": ["off", "on"] },\n    "autofocus":          { "attribOption": [], "type": "flag" },\n    "autoplay":           { "attribOption": [], "type": "flag" },\n    "behavior":           { "attribOption": ["scroll", "slide", "alternate"] },\n    "bgcolor":            { "attribOption": [], "type": "color" },\n    "border":             { "attribOption": [] },\n    "challenge":          { "attribOption": [] },\n    "charset":            { "attribOption": ["iso-8859-1", "utf-8", "shift_jis", "euc-jp", "big5", "gb2312", "euc-kr", "din_66003-kr", "ns_4551-1-kr", \n                                             "sen_850200_b", "csISO2022jp", "hz-gb-2312", "ibm852", "ibm866", "irv", "iso-2022-kr", "iso-8859-2", \n                                             "iso-8859-3", "iso-8859-4", "iso-8859-5", "iso-8859-6", "iso-8859-7", "iso-8859-8", "iso-8859-9", "koi8-r", \n                                             "ks_c_5601", "windows-1250", "windows-1251", "windows-1252", "windows-1253", "windows-1254", "windows-1255", \n                                             "windows-1256", "windows-1257", "windows-1258", "windows-874", "x-euc", "asmo-708", "dos-720", "dos-862", \n                                             "dos-874", "cp866", "cp1256"] },\n    "checked":            { "attribOption": [], "type": "flag" },\n    "cite":               { "attribOption": [] },\n    "codebase":           { "attribOption": [] },\n    "codetype":           { "attribOption": [] },\n    "cols":               { "attribOption": [] },\n    "colspan":            { "attribOption": [] },\n    "content":            { "attribOption": [] },\n    "controls":           { "attribOption": [], "type": "flag" },\n    "coords":             { "attribOption": [] },\n    "data":               { "attribOption": [] },\n    "datetime":           { "attribOption": [] },\n    "declare":            { "attribOption": [], "type": "flag" },\n    "default":            { "attribOption": [], "type": "flag" },\n    "defer":              { "attribOption": [], "type": "flag" },\n    "direction":          { "attribOption": ["left", "right", "up", "down"] },\n    "dirname":            { "attribOption": [] },\n    "disabled":           { "attribOption": [], "type": "flag" },\n    "enctype":            { "attribOption": ["application/x-www-form-urlencoded", "multipart/form-data", "text/plain"] },\n    "for":                { "attribOption": [] },\n    "form":               { "attribOption": [] },\n    "formaction":         { "attribOption": [] },\n    "formenctype":        { "attribOption": ["application/x-www-form-urlencoded", "multipart/form-data", "text/plain"] },\n    "formmethod":         { "attribOption": ["get", "post"] },\n    "formnovalidate":     { "attribOption": [], "type": "flag" },\n    "formtarget":         { "attribOption": ["_blank", "_parent", "_self", "_top"] },\n    "headers":            { "attribOption": [] },\n    "height":             { "attribOption": [] },\n    "high":               { "attribOption": [] },\n    "href":               { "attribOption": [] },\n    "hreflang":           { "attribOption": [] },\n    "hspace":             { "attribOption": [] },\n    "http-equiv":         { "attribOption": ["content-type", "default-style", "refresh"] },\n    "icon":               { "attribOption": [] },\n    "ismap":              { "attribOption": [], "type": "flag" },\n    "keytype":            { "attribOption": ["dsa", "ec", "rsa"] },\n    "kind":               { "attribOption": ["captions", "chapters", "descriptions", "metadata", "subtitles"] },\n    "label":              { "attribOption": [] },\n    "list":               { "attribOption": [] },\n    "longdesc":           { "attribOption": [] },\n    "loop":               { "attribOption": [], "type": "flag" },\n    "low":                { "attribOption": [] },\n    "manifest":           { "attribOption": [] },\n    "max":                { "attribOption": [] },\n    "maxlength":          { "attribOption": [] },\n    "media":              { "attribOption": ["screen", "tty", "tv", "projection", "handheld", "print", "aural", "braille", "embossed", "speech", "all", "width",\n                                             "min-width", "max-width", "height", "min-height", "max-height", "device-width", "min-device-width", "max-device-width",\n                                             "device-height", "min-device-height", "max-device-height", "orientation", "aspect-ratio", "min-aspect-ratio",\n                                             "max-aspect-ratio", "device-aspect-ratio", "min-device-aspect-ratio", "max-device-aspect-ratio", "color",\n                                             "min-color", "max-color", "color-index", "min-color-index", "max-color-index", "monochrome", "min-monochrome",\n                                             "max-monochrome", "resolution", "min-resolution", "max-resolution", "scan", "grid"],\n                            "allowMultipleValues": "true" },\n    "mediagroup":         { "attribOption": [] },\n    "method":             { "attribOption": ["get", "post"] },\n    "min":                { "attribOption": [] },\n    "multiple":           { "attribOption": [], "type": "flag" },\n    "muted":              { "attribOption": [], "type": "flag" },\n    "name":               { "attribOption": [] },\n    "meta/name":          { "attribOption": ["application-name", "author", "description", "generator", "Keywords"] },\n    "novalidate":         { "attribOption": [], "type": "flag" },\n    "open":               { "attribOption": [], "type": "flag" },\n    "optimum":            { "attribOption": [] },\n    "pattern":            { "attribOption": [] },\n    "placeholder":        { "attribOption": [] },\n    "poster":             { "attribOption": [] },\n    "preload":            { "attribOption": ["auto", "metadata", "none"] },\n    "pubdate":            { "attribOption": [] },\n    "radiogroup":         { "attribOption": [] },\n    "rel":                { "attribOption": ["alternate", "author", "bookmark", "help", "license", "next", "nofollow", "noreferrer", "prefetch", \n                                             "prev", "search", "sidebar", "tag", "external"] },\n    "link/rel":           { "attribOption": ["alternate", "author", "help", "icon", "license", "next", "pingback", "prefetch", "prev", "search", \n                                             "sidebar", "stylesheet", "tag"] },\n    "readonly":           { "attribOption": [], "type": "flag" },\n    "required":           { "attribOption": [], "type": "flag" },\n    "reversed":           { "attribOption": [], "type": "flag" },\n    "rows":               { "attribOption": [] },\n    "rowspan":            { "attribOption": [] },\n    "sandbox":            { "attribOption": ["allow-forms", "allow-same-origin", "allow-scripts", "allow-top-navigation"] },\n    "seamless":           { "attribOption": [], "type": "flag" },\n    "selected":           { "attribOption": [], "type": "flag" },\n    "scope":              { "attribOption": ["col", "colgroup", "row", "rowgroup"] },\n    "scoped":             { "attribOption": [], "type": "boolean" },\n    "scrollamount":       { "attribOption": [] },\n    "scrolldelay":        { "attribOption": [] },\n    "shape":              { "attribOption": ["circle", "default", "poly","rect"] },\n    "size":               { "attribOption": [] },\n    "sizes":              { "attribOption": ["any"] },\n    "span":               { "attribOption": [] },\n    "src":                { "attribOption": [] },\n    "srcdoc":             { "attribOption": [] },\n    "srclang":            { "attribOption": [] },\n    "standby":            { "attribOption": [] },\n    "start":              { "attribOption": [] },\n    "step":               { "attribOption": [] },\n    "target":             { "attribOption": ["_blank", "_parent", "_self", "_top"] },\n    "truespeed":          { "attribOption": [], "type": "flag" },\n    "type":               { "attribOption": [] },\n    "button/type":        { "attribOption": ["button", "reset", "submit"] },\n    "command/type":       { "attribOption": ["command", "checkbox", "radio"] },\n    "link/type":          { "attribOption": ["text/css"] },\n    "menu/type":          { "attribOption": ["context", "list", "toolbar"] },\n    "ol/type":            { "attribOption": ["1", "a", "A", "i", "I"] },\n    "script/type":        { "attribOption": ["text/javascript", "text/ecmascript", "text/jscript", "text/livescript", "text/tcl", "text/x-javascript", "text/x-ecmascript", \n                                             "application/x-javascript", "application/x-ecmascript", "application/javascript", "application/ecmascript"] },\n    "style/type":         { "attribOption": ["text/css"] },\n    "input/type":         { "attribOption": ["button", "checkbox", "color", "date", "datetime", "datetime-local", "email", "file", "hidden", "image", "month",\n                                             "number", "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"] },\n    "usemap":             { "attribOption": [] },\n    "value":              { "attribOption": [] },\n    "vspace":             { "attribOption": [] },\n    "width":              { "attribOption": [] },\n    "wrap":               { "attribOption": ["hard", "soft"] },\n    "xml:lang":           { "attribOption": [] },\n    "xmlns":              { "attribOption": [] }\n}';});

/*
 * Copyright (c) 2012 Adobe Systems Incorporated. All rights reserved.
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

define('main',['require','exports','module','text!HtmlTags.json','text!HtmlAttributes.json'],function (require, exports, module) {
    

    // Load dependent modules
    var AppInit             = brackets.getModule("utils/AppInit"),
        CodeHintManager     = brackets.getModule("editor/CodeHintManager"),
        HTMLUtils           = brackets.getModule("language/HTMLUtils"),
        HTMLTags            = require("text!HtmlTags.json"),
        HTMLAttributes      = require("text!HtmlAttributes.json"),
        tags,
        attributes;

    /**
     * @constructor
     */
    function TagHints() {
        this.exclusion = null;
    }
    
    /**
     * Check whether the exclusion is still the same as text after the cursor. 
     * If not, reset it to null.
     */
    TagHints.prototype.updateExclusion = function () {
        var textAfterCursor;
        if (this.exclusion && this.tagInfo) {
            textAfterCursor = this.tagInfo.tagName.substr(this.tagInfo.position.offset);
            if (!CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                this.exclusion = null;
            }
        }
    };
    
    /**
     * Determines whether HTML tag hints are available in the current editor
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
    TagHints.prototype.hasHints = function (editor, implicitChar) {
        var pos = editor.getCursorPos();
        
        this.tagInfo = HTMLUtils.getTagInfo(editor, pos);
        this.editor = editor;
        if (implicitChar === null) {
            if (this.tagInfo.position.tokenType === HTMLUtils.TAG_NAME) {
                if (this.tagInfo.position.offset >= 0) {
                    if (this.tagInfo.position.offset === 0) {
                        this.exclusion = this.tagInfo.tagName;
                    } else {
                        this.updateExclusion();
                    }
                    return true;
                }
            }
            return false;
        } else {
            if (implicitChar === "<") {
                this.exclusion = this.tagInfo.tagName;
                return true;
            }
            return false;
        }
    };
       
    /**
     * Returns a list of availble HTML tag hints if possible for the current
     * editor context. 
     *
     * @return {{hints: Array.<string|jQueryObject>, match: string, 
     *      selectInitial: boolean}}
     * Null if the provider wishes to end the hinting session. Otherwise, a
     * response object that provides 1. a sorted array hints that consists 
     * of strings; 2. a string match that is used by the manager to emphasize
     * matching substrings when rendering the hint list; and 3. a boolean that
     * indicates whether the first result, if one exists, should be selected
     * by default in the hint list window.
     */
    TagHints.prototype.getHints = function (implicitChar) {
        var query,
            result;

        this.tagInfo = HTMLUtils.getTagInfo(this.editor, this.editor.getCursorPos());
        if (this.tagInfo.position.tokenType === HTMLUtils.TAG_NAME) {
            if (this.tagInfo.position.offset >= 0) {
                this.updateExclusion();
                query = this.tagInfo.tagName.slice(0, this.tagInfo.position.offset);
                result = $.map(tags, function (value, key) {
                    if (key.indexOf(query) === 0) {
                        return key;
                    }
                }).sort();
                
                return {
                    hints: result,
                    match: query,
                    selectInitial: true
                };
            }
        }
        
        return null;
    };
    
    /**
     * Inserts a given HTML tag hint into the current editor context. 
     * 
     * @param {string} hint 
     * The hint to be inserted into the editor context.
     *
     * @return {boolean} 
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    TagHints.prototype.insertHint = function (completion) {
        var start = {line: -1, ch: -1},
            end = {line: -1, ch: -1},
            cursor = this.editor.getCursorPos(),
            charCount = 0;

        if (this.tagInfo.position.tokenType === HTMLUtils.TAG_NAME) {
            var textAfterCursor = this.tagInfo.tagName.substr(this.tagInfo.position.offset);
            if (CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                charCount = this.tagInfo.position.offset;
            } else {
                charCount = this.tagInfo.tagName.length;
            }
        }

        end.line = start.line = cursor.line;
        start.ch = cursor.ch - this.tagInfo.position.offset;
        end.ch = start.ch + charCount;

        if (this.exclusion || completion !== this.tagInfo.tagName) {
            if (start.ch !== end.ch) {
                this.editor.document.replaceRange(completion, start, end);
            } else {
                this.editor.document.replaceRange(completion, start);
            }
            this.exclusion = null;
        }
        
        return false;
    };

    /**
     * @constructor
     */
    function AttrHints() {
        this.globalAttributes = this.readGlobalAttrHints();
        this.cachedHints = null;
        this.exclusion = "";
    }

    /**
     * @private
     * Parse the code hints from JSON data and extract all hints from property names.
     * @return {!Array.<string>} An array of code hints read from the JSON data source.
     */
    AttrHints.prototype.readGlobalAttrHints = function () {
        return $.map(attributes, function (value, key) {
            if (value.global === "true") {
                return key;
            }
        });
    };

    /**
     * Helper function that determines the possible value hints for a given html tag/attribute name pair
     * 
     * @param {{queryStr: string}} query
     * The current query
     *
     * @param {string} tagName 
     * HTML tag name
     *
     * @param {string} attrName 
     * HTML attribute name
     *
     * @return {{hints: Array.<string>|$.Deferred, sortFunc: ?Function}} 
     * The (possibly deferred) hints and the sort function to use on thise hints.
     */
    AttrHints.prototype._getValueHintsForAttr = function (query, tagName, attrName) {
        // We look up attribute values with tagName plus a slash and attrName first.  
        // If the lookup fails, then we fall back to look up with attrName only. Most 
        // of the attributes in JSON are using attribute name only as their properties, 
        // but in some cases like "type" attribute, we have different properties like 
        // "script/type", "link/type" and "button/type".
        var hints = [],
            sortFunc = null;
        
        var tagPlusAttr = tagName + "/" + attrName,
            attrInfo = attributes[tagPlusAttr] || attributes[attrName];
        
        if (attrInfo) {
            if (attrInfo.type === "boolean") {
                hints = ["false", "true"];
            } else if (attrInfo.attribOption) {
                hints = attrInfo.attribOption;
            }
        }
        
        return { hints: hints, sortFunc: sortFunc };
    };
    
    /**
     * Check whether the exclusion is still the same as text after the cursor. 
     * If not, reset it to null.
     *
     * @param {boolean} attrNameOnly
     * true to indicate that we update the exclusion only if the cursor is inside an attribute name context.
     * Otherwise, we also update exclusion for attribute value context.
     */
    AttrHints.prototype.updateExclusion = function (attrNameOnly) {
        if (this.exclusion && this.tagInfo) {
            var tokenType = this.tagInfo.position.tokenType,
                offset = this.tagInfo.position.offset,
                textAfterCursor;
            
            if (tokenType === HTMLUtils.ATTR_NAME) {
                textAfterCursor = this.tagInfo.attr.name.substr(offset);
            } else if (!attrNameOnly && tokenType === HTMLUtils.ATTR_VALUE) {
                textAfterCursor = this.tagInfo.attr.value.substr(offset);
            }
            if (!CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                this.exclusion = null;
            }
        }
    };
    
    /**
     * Determines whether HTML attribute hints are available in the current 
     * editor context.
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
    AttrHints.prototype.hasHints = function (editor, implicitChar) {
        var pos = editor.getCursorPos(),
            tokenType,
            offset,
            query,
            textAfterCursor;
        
        this.editor = editor;
        this.tagInfo = HTMLUtils.getTagInfo(editor, pos);
        tokenType = this.tagInfo.position.tokenType;
        offset = this.tagInfo.position.offset;
        if (implicitChar === null) {
            query = null;
             
            if (tokenType === HTMLUtils.ATTR_NAME) {
                if (offset >= 0) {
                    query = this.tagInfo.attr.name.slice(0, offset);
                }
            } else if (tokenType === HTMLUtils.ATTR_VALUE) {
                if (this.tagInfo.position.offset >= 0) {
                    query = this.tagInfo.attr.value.slice(0, offset);
                } else {
                    // We get negative offset for a quoted attribute value with some leading whitespaces 
                    // as in <a rel= "rtl" where the cursor is just to the right of the "=".
                    // So just set the queryStr to an empty string. 
                    query = "";
                }
                
                // If we're at an attribute value, check if it's an attribute name that has hintable values.
                if (this.tagInfo.attr.name) {
                    var hintsAndSortFunc = this._getValueHintsForAttr({queryStr: query},
                                                                      this.tagInfo.tagName,
                                                                      this.tagInfo.attr.name);
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

            if (offset >= 0) {
                if (tokenType === HTMLUtils.ATTR_NAME && offset === 0) {
                    this.exclusion = this.tagInfo.attr.name;
                } else {
                    this.updateExclusion(false);
                }
            }
            
            return query !== null;
        } else {
            if (implicitChar === " " || implicitChar === "'" ||
                    implicitChar === "\"" || implicitChar === "=") {
                if (tokenType === HTMLUtils.ATTR_NAME) {
                    this.exclusion = this.tagInfo.attr.name;
                }
                return true;
            }
            return false;
        }
    };
    
    /**
     * Returns a list of availble HTML attribute hints if possible for the 
     * current editor context. 
     *
     * @return {{hints: Array.<string|jQueryObject>, match: string, 
     *      selectInitial: boolean}}
     * Null if the provider wishes to end the hinting session. Otherwise, a
     * response object that provides 1. a sorted array hints that consists 
     * of strings; 2. a string match that is used by the manager to emphasize
     * matching substrings when rendering the hint list; and 3. a boolean that
     * indicates whether the first result, if one exists, should be selected
     * by default in the hint list window.
     */
    AttrHints.prototype.getHints = function (implicitChar) {
        var cursor = this.editor.getCursorPos(),
            query = {queryStr: null},
            tokenType,
            offset,
            result = [],
            textAfterCursor;
 
        this.tagInfo = HTMLUtils.getTagInfo(this.editor, cursor);
        tokenType = this.tagInfo.position.tokenType;
        offset = this.tagInfo.position.offset;
        if (tokenType === HTMLUtils.ATTR_NAME || tokenType === HTMLUtils.ATTR_VALUE) {
            query.tag = this.tagInfo.tagName;
            
            if (offset >= 0) {
                if (tokenType === HTMLUtils.ATTR_NAME) {
                    query.queryStr = this.tagInfo.attr.name.slice(0, offset);
                } else {
                    query.queryStr = this.tagInfo.attr.value.slice(0, offset);
                    query.attrName = this.tagInfo.attr.name;
                }
                this.updateExclusion(false);
            } else if (tokenType === HTMLUtils.ATTR_VALUE) {
                // We get negative offset for a quoted attribute value with some leading whitespaces 
                // as in <a rel= "rtl" where the cursor is just to the right of the "=".
                // So just set the queryStr to an empty string. 
                query.queryStr = "";
                query.attrName = this.tagInfo.attr.name;
            }

            query.usedAttr = HTMLUtils.getTagAttributes(this.editor, cursor);
        }

        if (query.tag && query.queryStr !== null) {
            var tagName = query.tag,
                attrName = query.attrName,
                filter = query.queryStr,
                unfiltered = [],
                hints = [],
                sortFunc = null;

            if (attrName) {
                var hintsAndSortFunc = this._getValueHintsForAttr(query, tagName, attrName);
                hints = hintsAndSortFunc.hints;
                sortFunc = hintsAndSortFunc.sortFunc;
                
            } else if (tags && tags[tagName] && tags[tagName].attributes) {
                unfiltered = tags[tagName].attributes.concat(this.globalAttributes);
                hints = $.grep(unfiltered, function (attr, i) {
                    return $.inArray(attr, query.usedAttr) < 0;
                });
            }
            
            if (hints instanceof Array && hints.length) {
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
            } else if (hints instanceof Object && hints.hasOwnProperty("done")) { // Deferred hints
                var deferred = $.Deferred();
                hints.done(function (asyncHints) {
                    deferred.resolveWith(this, [{ hints : asyncHints, match: query.queryStr, selectInitial: true }]);
                });
                return deferred;
            } else {
                return null;
            }
        }

        
    };
    
    /**
     * Inserts a given HTML attribute hint into the current editor context.
     * 
     * @param {string} hint 
     * The hint to be inserted into the editor context.
     * 
     * @return {boolean} 
     * Indicates whether the manager should follow hint insertion with an
     * additional explicit hint request.
     */
    AttrHints.prototype.insertHint = function (completion) {
        var cursor = this.editor.getCursorPos(),
            start = {line: -1, ch: -1},
            end = {line: -1, ch: -1},
            tokenType = this.tagInfo.position.tokenType,
            offset = this.tagInfo.position.offset,
            charCount = 0,
            insertedName = false,
            replaceExistingOne = this.tagInfo.attr.valueAssigned,
            endQuote = "",
            shouldReplace = true,
            textAfterCursor;

        if (tokenType === HTMLUtils.ATTR_NAME) {
            textAfterCursor = this.tagInfo.attr.name.substr(offset);
            if (CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                charCount = offset;
                replaceExistingOne = false;
            } else {
                charCount = this.tagInfo.attr.name.length;
            }
            // Append an equal sign and two double quotes if the current attr is not an empty attr
            // and then adjust cursor location before the last quote that we just inserted.
            if (!replaceExistingOne && attributes && attributes[completion] &&
                    attributes[completion].type !== "flag") {
                completion += "=\"\"";
                insertedName = true;
            } else if (completion === this.tagInfo.attr.name) {
                shouldReplace = false;
            }
        } else if (tokenType === HTMLUtils.ATTR_VALUE) {
            textAfterCursor = this.tagInfo.attr.value.substr(offset);
            if (CodeHintManager.hasValidExclusion(this.exclusion, textAfterCursor)) {
                charCount = offset;
                // Set exclusion to null only after attribute value insertion,
                // not after attribute name insertion since we need to keep it 
                // for attribute value insertion.
                this.exclusion = null;
            } else {
                charCount = this.tagInfo.attr.value.length;
            }
            
            if (!this.tagInfo.attr.hasEndQuote) {
                endQuote = this.tagInfo.attr.quoteChar;
                if (endQuote) {
                    completion += endQuote;
                } else if (offset === 0) {
                    completion = "\"" + completion + "\"";
                }
            } else if (completion === this.tagInfo.attr.value) {
                shouldReplace = false;
            }
        }

        end.line = start.line = cursor.line;
        start.ch = cursor.ch - offset;
        end.ch = start.ch + charCount;

        if (shouldReplace) {
            if (start.ch !== end.ch) {
                this.editor.document.replaceRange(completion, start, end);
            } else {
                this.editor.document.replaceRange(completion, start);
            }
        }

        if (insertedName) {
            this.editor.setCursorPos(start.line, start.ch + completion.length - 1);

            // Since we're now inside the double-quotes we just inserted,
            // immediately pop up the attribute value hint.
            return true;
        } else if (tokenType === HTMLUtils.ATTR_VALUE && this.tagInfo.attr.hasEndQuote) {
            // Move the cursor to the right of the existing end quote after value insertion.
            this.editor.setCursorPos(start.line, start.ch + completion.length + 1);
        }
        
        return false;
    };

    AppInit.appReady(function () {
        // Parse JSON files
        tags = JSON.parse(HTMLTags);
        attributes = JSON.parse(HTMLAttributes);
        
        // Register code hint providers
        var tagHints = new TagHints();
        var attrHints = new AttrHints();
        CodeHintManager.registerHintProvider(tagHints, ["html"], 0);
        CodeHintManager.registerHintProvider(attrHints, ["html"], 0);
    
        // For unit testing
        exports.tagHintProvider = tagHints;
        exports.attrHintProvider = attrHints;
    });
});