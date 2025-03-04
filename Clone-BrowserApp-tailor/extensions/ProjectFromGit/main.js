
(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        //Allow using this built library as an AMD module
        //in another project. That other project will only
        //see this AMD call, not the internal modules in
        //the closure below.
        define('thirdparty/gitlite.js/api-built',factory);
    } else {
        //Browser globals case. Just assign the
        //result to a property on the global.
        root.GitLite = factory();
    }
}(this, function () {
/**
 * almond 0.2.5 Copyright (c) 2011-2012, The Dojo Foundation All Rights Reserved.
 * Available via the MIT or new BSD license.
 * see: http://github.com/jrburke/almond for details
 */
//Going sloppy to avoid 'use strict' string cost, but strict practices should
//be followed.
/*jslint sloppy: true */
/*global setTimeout: false */

var requirejs, require, define;
(function (undef) {
    var main, req, makeMap, handlers,
        defined = {},
        waiting = {},
        config = {},
        defining = {},
        hasOwn = Object.prototype.hasOwnProperty,
        aps = [].slice;

    function hasProp(obj, prop) {
        return hasOwn.call(obj, prop);
    }

    /**
     * Given a relative module name, like ./something, normalize it to
     * a real name that can be mapped to a path.
     * @param {String} name the relative name
     * @param {String} baseName a real name that the name arg is relative
     * to.
     * @returns {String} normalized name
     */
    function normalize(name, baseName) {
        var nameParts, nameSegment, mapValue, foundMap,
            foundI, foundStarMap, starI, i, j, part,
            baseParts = baseName && baseName.split("/"),
            map = config.map,
            starMap = (map && map['*']) || {};

        //Adjust any relative paths.
        if (name && name.charAt(0) === ".") {
            //If have a base name, try to normalize against it,
            //otherwise, assume it is a top-level require that will
            //be relative to baseUrl in the end.
            if (baseName) {
                //Convert baseName to array, and lop off the last part,
                //so that . matches that "directory" and not name of the baseName's
                //module. For instance, baseName of "one/two/three", maps to
                //"one/two/three.js", but we want the directory, "one/two" for
                //this normalization.
                baseParts = baseParts.slice(0, baseParts.length - 1);

                name = baseParts.concat(name.split("/"));

                //start trimDots
                for (i = 0; i < name.length; i += 1) {
                    part = name[i];
                    if (part === ".") {
                        name.splice(i, 1);
                        i -= 1;
                    } else if (part === "..") {
                        if (i === 1 && (name[2] === '..' || name[0] === '..')) {
                            //End of the line. Keep at least one non-dot
                            //path segment at the front so it can be mapped
                            //correctly to disk. Otherwise, there is likely
                            //no path mapping for a path starting with '..'.
                            //This can still fail, but catches the most reasonable
                            //uses of ..
                            break;
                        } else if (i > 0) {
                            name.splice(i - 1, 2);
                            i -= 2;
                        }
                    }
                }
                //end trimDots

                name = name.join("/");
            } else if (name.indexOf('./') === 0) {
                // No baseName, so this is ID is resolved relative
                // to baseUrl, pull off the leading dot.
                name = name.substring(2);
            }
        }

        //Apply map config if available.
        if ((baseParts || starMap) && map) {
            nameParts = name.split('/');

            for (i = nameParts.length; i > 0; i -= 1) {
                nameSegment = nameParts.slice(0, i).join("/");

                if (baseParts) {
                    //Find the longest baseName segment match in the config.
                    //So, do joins on the biggest to smallest lengths of baseParts.
                    for (j = baseParts.length; j > 0; j -= 1) {
                        mapValue = map[baseParts.slice(0, j).join('/')];

                        //baseName segment has  config, find if it has one for
                        //this name.
                        if (mapValue) {
                            mapValue = mapValue[nameSegment];
                            if (mapValue) {
                                //Match, update name to the new value.
                                foundMap = mapValue;
                                foundI = i;
                                break;
                            }
                        }
                    }
                }

                if (foundMap) {
                    break;
                }

                //Check for a star map match, but just hold on to it,
                //if there is a shorter segment match later in a matching
                //config, then favor over this star map.
                if (!foundStarMap && starMap && starMap[nameSegment]) {
                    foundStarMap = starMap[nameSegment];
                    starI = i;
                }
            }

            if (!foundMap && foundStarMap) {
                foundMap = foundStarMap;
                foundI = starI;
            }

            if (foundMap) {
                nameParts.splice(0, foundI, foundMap);
                name = nameParts.join('/');
            }
        }

        return name;
    }

    function makeRequire(relName, forceSync) {
        return function () {
            //A version of a require function that passes a moduleName
            //value for items that may need to
            //look up paths relative to the moduleName
            return req.apply(undef, aps.call(arguments, 0).concat([relName, forceSync]));
        };
    }

    function makeNormalize(relName) {
        return function (name) {
            return normalize(name, relName);
        };
    }

    function makeLoad(depName) {
        return function (value) {
            defined[depName] = value;
        };
    }

    function callDep(name) {
        if (hasProp(waiting, name)) {
            var args = waiting[name];
            delete waiting[name];
            defining[name] = true;
            main.apply(undef, args);
        }

        if (!hasProp(defined, name) && !hasProp(defining, name)) {
            throw new Error('No ' + name);
        }
        return defined[name];
    }

    //Turns a plugin!resource to [plugin, resource]
    //with the plugin being undefined if the name
    //did not have a plugin prefix.
    function splitPrefix(name) {
        var prefix,
            index = name ? name.indexOf('!') : -1;
        if (index > -1) {
            prefix = name.substring(0, index);
            name = name.substring(index + 1, name.length);
        }
        return [prefix, name];
    }

    /**
     * Makes a name map, normalizing the name, and using a plugin
     * for normalization if necessary. Grabs a ref to plugin
     * too, as an optimization.
     */
    makeMap = function (name, relName) {
        var plugin,
            parts = splitPrefix(name),
            prefix = parts[0];

        name = parts[1];

        if (prefix) {
            prefix = normalize(prefix, relName);
            plugin = callDep(prefix);
        }

        //Normalize according
        if (prefix) {
            if (plugin && plugin.normalize) {
                name = plugin.normalize(name, makeNormalize(relName));
            } else {
                name = normalize(name, relName);
            }
        } else {
            name = normalize(name, relName);
            parts = splitPrefix(name);
            prefix = parts[0];
            name = parts[1];
            if (prefix) {
                plugin = callDep(prefix);
            }
        }

        //Using ridiculous property names for space reasons
        return {
            f: prefix ? prefix + '!' + name : name, //fullName
            n: name,
            pr: prefix,
            p: plugin
        };
    };

    function makeConfig(name) {
        return function () {
            return (config && config.config && config.config[name]) || {};
        };
    }

    handlers = {
        require: function (name) {
            return makeRequire(name);
        },
        exports: function (name) {
            var e = defined[name];
            if (typeof e !== 'undefined') {
                return e;
            } else {
                return (defined[name] = {});
            }
        },
        module: function (name) {
            return {
                id: name,
                uri: '',
                exports: defined[name],
                config: makeConfig(name)
            };
        }
    };

    main = function (name, deps, callback, relName) {
        var cjsModule, depName, ret, map, i,
            args = [],
            usingExports;

        //Use name if no relName
        relName = relName || name;

        //Call the callback to define the module, if necessary.
        if (typeof callback === 'function') {

            //Pull out the defined dependencies and pass the ordered
            //values to the callback.
            //Default to [require, exports, module] if no deps
            deps = !deps.length && callback.length ? ['require', 'exports', 'module'] : deps;
            for (i = 0; i < deps.length; i += 1) {
                map = makeMap(deps[i], relName);
                depName = map.f;

                //Fast path CommonJS standard dependencies.
                if (depName === "require") {
                    args[i] = handlers.require(name);
                } else if (depName === "exports") {
                    //CommonJS module spec 1.1
                    args[i] = handlers.exports(name);
                    usingExports = true;
                } else if (depName === "module") {
                    //CommonJS module spec 1.1
                    cjsModule = args[i] = handlers.module(name);
                } else if (hasProp(defined, depName) ||
                           hasProp(waiting, depName) ||
                           hasProp(defining, depName)) {
                    args[i] = callDep(depName);
                } else if (map.p) {
                    map.p.load(map.n, makeRequire(relName, true), makeLoad(depName), {});
                    args[i] = defined[depName];
                } else {
                    throw new Error(name + ' missing ' + depName);
                }
            }

            ret = callback.apply(defined[name], args);

            if (name) {
                //If setting exports via "module" is in play,
                //favor that over return value and exports. After that,
                //favor a non-undefined return value over exports use.
                if (cjsModule && cjsModule.exports !== undef &&
                        cjsModule.exports !== defined[name]) {
                    defined[name] = cjsModule.exports;
                } else if (ret !== undef || !usingExports) {
                    //Use the return value from the function.
                    defined[name] = ret;
                }
            }
        } else if (name) {
            //May just be an object definition for the module. Only
            //worry about defining if have a module name.
            defined[name] = callback;
        }
    };

    requirejs = require = req = function (deps, callback, relName, forceSync, alt) {
        if (typeof deps === "string") {
            if (handlers[deps]) {
                //callback in this case is really relName
                return handlers[deps](callback);
            }
            //Just return the module wanted. In this scenario, the
            //deps arg is the module name, and second arg (if passed)
            //is just the relName.
            //Normalize module name, if it contains . or ..
            return callDep(makeMap(deps, callback).f);
        } else if (!deps.splice) {
            //deps is a config object, not an array.
            config = deps;
            if (callback.splice) {
                //callback is an array, which means it is a dependency list.
                //Adjust args if there are dependencies
                deps = callback;
                callback = relName;
                relName = null;
            } else {
                deps = undef;
            }
        }

        //Support require(['a'])
        callback = callback || function () {};

        //If relName is a function, it is an errback handler,
        //so remove it.
        if (typeof relName === 'function') {
            relName = forceSync;
            forceSync = alt;
        }

        //Simulate async callback;
        if (forceSync) {
            main(undef, deps, callback, relName);
        } else {
            //Using a non-zero value because of concern for what old browsers
            //do, and latest browsers "upgrade" to 4 if lower value is used:
            //http://www.whatwg.org/specs/web-apps/current-work/multipage/timers.html#dom-windowtimers-settimeout:
            //If want a value immediately, use require('id') instead -- something
            //that works in almond on the global level, but not guaranteed and
            //unlikely to work in other AMD implementations.
            setTimeout(function () {
                main(undef, deps, callback, relName);
            }, 4);
        }

        return req;
    };

    /**
     * Just drops the config on the floor, but returns req in case
     * the config return value is used.
     */
    req.config = function (cfg) {
        config = cfg;
        if (config.deps) {
            req(config.deps, config.callback);
        }
        return req;
    };

    define = function (name, deps, callback) {

        //This module may not have dependencies
        if (!deps.splice) {
            //deps is not an array, so probably means
            //an object literal or factory function for
            //the value. Adjust args.
            callback = deps;
            deps = [];
        }

        if (!hasProp(defined, name) && !hasProp(waiting, name)) {
            waiting[name] = [name, deps, callback];
        }
    };

    define.amd = {
        jQuery: true
    };
}());

define("thirdparty/almond", function(){});

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

define('text!workers/api-worker-built.js',[],function () { return 'var window = self;\n(function (root, factory) {\n    \n    var apiWorker = factory();\n    apiWorker();\n    \n}(this, function () {\n/**\n * almond 0.2.5 Copyright (c) 2011-2012, The Dojo Foundation All Rights Reserved.\n * Available via the MIT or new BSD license.\n * see: http://github.com/jrburke/almond for details\n */\n//Going sloppy to avoid \'use strict\' string cost, but strict practices should\n//be followed.\n/*jslint sloppy: true */\n/*global setTimeout: false */\n\nvar requirejs, require, define;\n(function (undef) {\n    var main, req, makeMap, handlers,\n        defined = {},\n        waiting = {},\n        config = {},\n        defining = {},\n        hasOwn = Object.prototype.hasOwnProperty,\n        aps = [].slice;\n\n    function hasProp(obj, prop) {\n        return hasOwn.call(obj, prop);\n    }\n\n    /**\n     * Given a relative module name, like ./something, normalize it to\n     * a real name that can be mapped to a path.\n     * @param {String} name the relative name\n     * @param {String} baseName a real name that the name arg is relative\n     * to.\n     * @returns {String} normalized name\n     */\n    function normalize(name, baseName) {\n        var nameParts, nameSegment, mapValue, foundMap,\n            foundI, foundStarMap, starI, i, j, part,\n            baseParts = baseName && baseName.split("/"),\n            map = config.map,\n            starMap = (map && map[\'*\']) || {};\n\n        //Adjust any relative paths.\n        if (name && name.charAt(0) === ".") {\n            //If have a base name, try to normalize against it,\n            //otherwise, assume it is a top-level require that will\n            //be relative to baseUrl in the end.\n            if (baseName) {\n                //Convert baseName to array, and lop off the last part,\n                //so that . matches that "directory" and not name of the baseName\'s\n                //module. For instance, baseName of "one/two/three", maps to\n                //"one/two/three.js", but we want the directory, "one/two" for\n                //this normalization.\n                baseParts = baseParts.slice(0, baseParts.length - 1);\n\n                name = baseParts.concat(name.split("/"));\n\n                //start trimDots\n                for (i = 0; i < name.length; i += 1) {\n                    part = name[i];\n                    if (part === ".") {\n                        name.splice(i, 1);\n                        i -= 1;\n                    } else if (part === "..") {\n                        if (i === 1 && (name[2] === \'..\' || name[0] === \'..\')) {\n                            //End of the line. Keep at least one non-dot\n                            //path segment at the front so it can be mapped\n                            //correctly to disk. Otherwise, there is likely\n                            //no path mapping for a path starting with \'..\'.\n                            //This can still fail, but catches the most reasonable\n                            //uses of ..\n                            break;\n                        } else if (i > 0) {\n                            name.splice(i - 1, 2);\n                            i -= 2;\n                        }\n                    }\n                }\n                //end trimDots\n\n                name = name.join("/");\n            } else if (name.indexOf(\'./\') === 0) {\n                // No baseName, so this is ID is resolved relative\n                // to baseUrl, pull off the leading dot.\n                name = name.substring(2);\n            }\n        }\n\n        //Apply map config if available.\n        if ((baseParts || starMap) && map) {\n            nameParts = name.split(\'/\');\n\n            for (i = nameParts.length; i > 0; i -= 1) {\n                nameSegment = nameParts.slice(0, i).join("/");\n\n                if (baseParts) {\n                    //Find the longest baseName segment match in the config.\n                    //So, do joins on the biggest to smallest lengths of baseParts.\n                    for (j = baseParts.length; j > 0; j -= 1) {\n                        mapValue = map[baseParts.slice(0, j).join(\'/\')];\n\n                        //baseName segment has  config, find if it has one for\n                        //this name.\n                        if (mapValue) {\n                            mapValue = mapValue[nameSegment];\n                            if (mapValue) {\n                                //Match, update name to the new value.\n                                foundMap = mapValue;\n                                foundI = i;\n                                break;\n                            }\n                        }\n                    }\n                }\n\n                if (foundMap) {\n                    break;\n                }\n\n                //Check for a star map match, but just hold on to it,\n                //if there is a shorter segment match later in a matching\n                //config, then favor over this star map.\n                if (!foundStarMap && starMap && starMap[nameSegment]) {\n                    foundStarMap = starMap[nameSegment];\n                    starI = i;\n                }\n            }\n\n            if (!foundMap && foundStarMap) {\n                foundMap = foundStarMap;\n                foundI = starI;\n            }\n\n            if (foundMap) {\n                nameParts.splice(0, foundI, foundMap);\n                name = nameParts.join(\'/\');\n            }\n        }\n\n        return name;\n    }\n\n    function makeRequire(relName, forceSync) {\n        return function () {\n            //A version of a require function that passes a moduleName\n            //value for items that may need to\n            //look up paths relative to the moduleName\n            return req.apply(undef, aps.call(arguments, 0).concat([relName, forceSync]));\n        };\n    }\n\n    function makeNormalize(relName) {\n        return function (name) {\n            return normalize(name, relName);\n        };\n    }\n\n    function makeLoad(depName) {\n        return function (value) {\n            defined[depName] = value;\n        };\n    }\n\n    function callDep(name) {\n        if (hasProp(waiting, name)) {\n            var args = waiting[name];\n            delete waiting[name];\n            defining[name] = true;\n            main.apply(undef, args);\n        }\n\n        if (!hasProp(defined, name) && !hasProp(defining, name)) {\n            throw new Error(\'No \' + name);\n        }\n        return defined[name];\n    }\n\n    //Turns a plugin!resource to [plugin, resource]\n    //with the plugin being undefined if the name\n    //did not have a plugin prefix.\n    function splitPrefix(name) {\n        var prefix,\n            index = name ? name.indexOf(\'!\') : -1;\n        if (index > -1) {\n            prefix = name.substring(0, index);\n            name = name.substring(index + 1, name.length);\n        }\n        return [prefix, name];\n    }\n\n    /**\n     * Makes a name map, normalizing the name, and using a plugin\n     * for normalization if necessary. Grabs a ref to plugin\n     * too, as an optimization.\n     */\n    makeMap = function (name, relName) {\n        var plugin,\n            parts = splitPrefix(name),\n            prefix = parts[0];\n\n        name = parts[1];\n\n        if (prefix) {\n            prefix = normalize(prefix, relName);\n            plugin = callDep(prefix);\n        }\n\n        //Normalize according\n        if (prefix) {\n            if (plugin && plugin.normalize) {\n                name = plugin.normalize(name, makeNormalize(relName));\n            } else {\n                name = normalize(name, relName);\n            }\n        } else {\n            name = normalize(name, relName);\n            parts = splitPrefix(name);\n            prefix = parts[0];\n            name = parts[1];\n            if (prefix) {\n                plugin = callDep(prefix);\n            }\n        }\n\n        //Using ridiculous property names for space reasons\n        return {\n            f: prefix ? prefix + \'!\' + name : name, //fullName\n            n: name,\n            pr: prefix,\n            p: plugin\n        };\n    };\n\n    function makeConfig(name) {\n        return function () {\n            return (config && config.config && config.config[name]) || {};\n        };\n    }\n\n    handlers = {\n        require: function (name) {\n            return makeRequire(name);\n        },\n        exports: function (name) {\n            var e = defined[name];\n            if (typeof e !== \'undefined\') {\n                return e;\n            } else {\n                return (defined[name] = {});\n            }\n        },\n        module: function (name) {\n            return {\n                id: name,\n                uri: \'\',\n                exports: defined[name],\n                config: makeConfig(name)\n            };\n        }\n    };\n\n    main = function (name, deps, callback, relName) {\n        var cjsModule, depName, ret, map, i,\n            args = [],\n            usingExports;\n\n        //Use name if no relName\n        relName = relName || name;\n\n        //Call the callback to define the module, if necessary.\n        if (typeof callback === \'function\') {\n\n            //Pull out the defined dependencies and pass the ordered\n            //values to the callback.\n            //Default to [require, exports, module] if no deps\n            deps = !deps.length && callback.length ? [\'require\', \'exports\', \'module\'] : deps;\n            for (i = 0; i < deps.length; i += 1) {\n                map = makeMap(deps[i], relName);\n                depName = map.f;\n\n                //Fast path CommonJS standard dependencies.\n                if (depName === "require") {\n                    args[i] = handlers.require(name);\n                } else if (depName === "exports") {\n                    //CommonJS module spec 1.1\n                    args[i] = handlers.exports(name);\n                    usingExports = true;\n                } else if (depName === "module") {\n                    //CommonJS module spec 1.1\n                    cjsModule = args[i] = handlers.module(name);\n                } else if (hasProp(defined, depName) ||\n                           hasProp(waiting, depName) ||\n                           hasProp(defining, depName)) {\n                    args[i] = callDep(depName);\n                } else if (map.p) {\n                    map.p.load(map.n, makeRequire(relName, true), makeLoad(depName), {});\n                    args[i] = defined[depName];\n                } else {\n                    throw new Error(name + \' missing \' + depName);\n                }\n            }\n\n            ret = callback.apply(defined[name], args);\n\n            if (name) {\n                //If setting exports via "module" is in play,\n                //favor that over return value and exports. After that,\n                //favor a non-undefined return value over exports use.\n                if (cjsModule && cjsModule.exports !== undef &&\n                        cjsModule.exports !== defined[name]) {\n                    defined[name] = cjsModule.exports;\n                } else if (ret !== undef || !usingExports) {\n                    //Use the return value from the function.\n                    defined[name] = ret;\n                }\n            }\n        } else if (name) {\n            //May just be an object definition for the module. Only\n            //worry about defining if have a module name.\n            defined[name] = callback;\n        }\n    };\n\n    requirejs = require = req = function (deps, callback, relName, forceSync, alt) {\n        if (typeof deps === "string") {\n            if (handlers[deps]) {\n                //callback in this case is really relName\n                return handlers[deps](callback);\n            }\n            //Just return the module wanted. In this scenario, the\n            //deps arg is the module name, and second arg (if passed)\n            //is just the relName.\n            //Normalize module name, if it contains . or ..\n            return callDep(makeMap(deps, callback).f);\n        } else if (!deps.splice) {\n            //deps is a config object, not an array.\n            config = deps;\n            if (callback.splice) {\n                //callback is an array, which means it is a dependency list.\n                //Adjust args if there are dependencies\n                deps = callback;\n                callback = relName;\n                relName = null;\n            } else {\n                deps = undef;\n            }\n        }\n\n        //Support require([\'a\'])\n        callback = callback || function () {};\n\n        //If relName is a function, it is an errback handler,\n        //so remove it.\n        if (typeof relName === \'function\') {\n            relName = forceSync;\n            forceSync = alt;\n        }\n\n        //Simulate async callback;\n        if (forceSync) {\n            main(undef, deps, callback, relName);\n        } else {\n            //Using a non-zero value because of concern for what old browsers\n            //do, and latest browsers "upgrade" to 4 if lower value is used:\n            //http://www.whatwg.org/specs/web-apps/current-work/multipage/timers.html#dom-windowtimers-settimeout:\n            //If want a value immediately, use require(\'id\') instead -- something\n            //that works in almond on the global level, but not guaranteed and\n            //unlikely to work in other AMD implementations.\n            setTimeout(function () {\n                main(undef, deps, callback, relName);\n            }, 4);\n        }\n\n        return req;\n    };\n\n    /**\n     * Just drops the config on the floor, but returns req in case\n     * the config return value is used.\n     */\n    req.config = function (cfg) {\n        config = cfg;\n        if (config.deps) {\n            req(config.deps, config.callback);\n        }\n        return req;\n    };\n\n    define = function (name, deps, callback) {\n\n        //This module may not have dependencies\n        if (!deps.splice) {\n            //deps is not an array, so probably means\n            //an object literal or factory function for\n            //the value. Adjust args.\n            callback = deps;\n            deps = [];\n        }\n\n        if (!hasProp(defined, name) && !hasProp(waiting, name)) {\n            waiting[name] = [name, deps, callback];\n        }\n    };\n\n    define.amd = {\n        jQuery: true\n    };\n}());\n\ndefine("thirdparty/almond", function(){});\n\n/** @license zlib.js 2012 - imaya [ https://github.com/imaya/zlib.js ] The MIT License */\n(function() {var COMPILED = !0, goog = goog || {};\ngoog.global = this;\ngoog.DEBUG = !1;\ngoog.LOCALE = "en";\ngoog.provide = function(a) {\n  if(!COMPILED) {\n    if(goog.isProvided_(a)) {\n      throw Error(\'Namespace "\' + a + \'" already declared.\');\n    }\n    delete goog.implicitNamespaces_[a];\n    for(var b = a;(b = b.substring(0, b.lastIndexOf("."))) && !goog.getObjectByName(b);) {\n      goog.implicitNamespaces_[b] = !0\n    }\n  }\n  goog.exportPath_(a)\n};\ngoog.setTestOnly = function(a) {\n  if(COMPILED && !goog.DEBUG) {\n    throw a = a || "", Error("Importing test-only code into non-debug environment" + a ? ": " + a : ".");\n  }\n};\nCOMPILED || (goog.isProvided_ = function(a) {\n  return!goog.implicitNamespaces_[a] && !!goog.getObjectByName(a)\n}, goog.implicitNamespaces_ = {});\ngoog.exportPath_ = function(a, b, c) {\n  a = a.split(".");\n  c = c || goog.global;\n  !(a[0] in c) && c.execScript && c.execScript("var " + a[0]);\n  for(var d;a.length && (d = a.shift());) {\n    !a.length && goog.isDef(b) ? c[d] = b : c = c[d] ? c[d] : c[d] = {}\n  }\n};\ngoog.getObjectByName = function(a, b) {\n  for(var c = a.split("."), d = b || goog.global, e;e = c.shift();) {\n    if(goog.isDefAndNotNull(d[e])) {\n      d = d[e]\n    }else {\n      return null\n    }\n  }\n  return d\n};\ngoog.globalize = function(a, b) {\n  var c = b || goog.global, d;\n  for(d in a) {\n    c[d] = a[d]\n  }\n};\ngoog.addDependency = function(a, b, c) {\n  if(!COMPILED) {\n    for(var d, a = a.replace(/\\\\/g, "/"), e = goog.dependencies_, f = 0;d = b[f];f++) {\n      e.nameToPath[d] = a, a in e.pathToNames || (e.pathToNames[a] = {}), e.pathToNames[a][d] = !0\n    }\n    for(d = 0;b = c[d];d++) {\n      a in e.requires || (e.requires[a] = {}), e.requires[a][b] = !0\n    }\n  }\n};\ngoog.ENABLE_DEBUG_LOADER = !0;\ngoog.require = function(a) {\n  if(!COMPILED && !goog.isProvided_(a)) {\n    if(goog.ENABLE_DEBUG_LOADER) {\n      var b = goog.getPathFromDeps_(a);\n      if(b) {\n        goog.included_[b] = !0;\n        goog.writeScripts_();\n        return\n      }\n    }\n    a = "goog.require could not find: " + a;\n    goog.global.console && goog.global.console.error(a);\n    throw Error(a);\n  }\n};\ngoog.basePath = "";\ngoog.nullFunction = function() {\n};\ngoog.identityFunction = function(a) {\n  return a\n};\ngoog.abstractMethod = function() {\n  throw Error("unimplemented abstract method");\n};\ngoog.addSingletonGetter = function(a) {\n  a.getInstance = function() {\n    if(a.instance_) {\n      return a.instance_\n    }\n    goog.DEBUG && (goog.instantiatedSingletons_[goog.instantiatedSingletons_.length] = a);\n    return a.instance_ = new a\n  }\n};\ngoog.instantiatedSingletons_ = [];\n!COMPILED && goog.ENABLE_DEBUG_LOADER && (goog.included_ = {}, goog.dependencies_ = {pathToNames:{}, nameToPath:{}, requires:{}, visited:{}, written:{}}, goog.inHtmlDocument_ = function() {\n  var a = goog.global.document;\n  return"undefined" != typeof a && "write" in a\n}, goog.findBasePath_ = function() {\n  if(goog.global.CLOSURE_BASE_PATH) {\n    goog.basePath = goog.global.CLOSURE_BASE_PATH\n  }else {\n    if(goog.inHtmlDocument_()) {\n      for(var a = goog.global.document.getElementsByTagName("script"), b = a.length - 1;0 <= b;--b) {\n        var c = a[b].src, d = c.lastIndexOf("?"), d = -1 == d ? c.length : d;\n        if("base.js" == c.substr(d - 7, 7)) {\n          goog.basePath = c.substr(0, d - 7);\n          break\n        }\n      }\n    }\n  }\n}, goog.importScript_ = function(a) {\n  var b = goog.global.CLOSURE_IMPORT_SCRIPT || goog.writeScriptTag_;\n  !goog.dependencies_.written[a] && b(a) && (goog.dependencies_.written[a] = !0)\n}, goog.writeScriptTag_ = function(a) {\n  return goog.inHtmlDocument_() ? (goog.global.document.write(\'<script type="text/javascript" src="\' + a + \'"><\\/script>\'), !0) : !1\n}, goog.writeScripts_ = function() {\n  function a(e) {\n    if(!(e in d.written)) {\n      if(!(e in d.visited) && (d.visited[e] = !0, e in d.requires)) {\n        for(var g in d.requires[e]) {\n          if(!goog.isProvided_(g)) {\n            if(g in d.nameToPath) {\n              a(d.nameToPath[g])\n            }else {\n              throw Error("Undefined nameToPath for " + g);\n            }\n          }\n        }\n      }\n      e in c || (c[e] = !0, b.push(e))\n    }\n  }\n  var b = [], c = {}, d = goog.dependencies_, e;\n  for(e in goog.included_) {\n    d.written[e] || a(e)\n  }\n  for(e = 0;e < b.length;e++) {\n    if(b[e]) {\n      goog.importScript_(goog.basePath + b[e])\n    }else {\n      throw Error("Undefined script input");\n    }\n  }\n}, goog.getPathFromDeps_ = function(a) {\n  return a in goog.dependencies_.nameToPath ? goog.dependencies_.nameToPath[a] : null\n}, goog.findBasePath_(), goog.global.CLOSURE_NO_DEPS || goog.importScript_(goog.basePath + "deps.js"));\ngoog.typeOf = function(a) {\n  var b = typeof a;\n  if("object" == b) {\n    if(a) {\n      if(a instanceof Array) {\n        return"array"\n      }\n      if(a instanceof Object) {\n        return b\n      }\n      var c = Object.prototype.toString.call(a);\n      if("[object Window]" == c) {\n        return"object"\n      }\n      if("[object Array]" == c || "number" == typeof a.length && "undefined" != typeof a.splice && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("splice")) {\n        return"array"\n      }\n      if("[object Function]" == c || "undefined" != typeof a.call && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("call")) {\n        return"function"\n      }\n    }else {\n      return"null"\n    }\n  }else {\n    if("function" == b && "undefined" == typeof a.call) {\n      return"object"\n    }\n  }\n  return b\n};\ngoog.isDef = function(a) {\n  return void 0 !== a\n};\ngoog.isNull = function(a) {\n  return null === a\n};\ngoog.isDefAndNotNull = function(a) {\n  return null != a\n};\ngoog.isArray = function(a) {\n  return"array" == goog.typeOf(a)\n};\ngoog.isArrayLike = function(a) {\n  var b = goog.typeOf(a);\n  return"array" == b || "object" == b && "number" == typeof a.length\n};\ngoog.isDateLike = function(a) {\n  return goog.isObject(a) && "function" == typeof a.getFullYear\n};\ngoog.isString = function(a) {\n  return"string" == typeof a\n};\ngoog.isBoolean = function(a) {\n  return"boolean" == typeof a\n};\ngoog.isNumber = function(a) {\n  return"number" == typeof a\n};\ngoog.isFunction = function(a) {\n  return"function" == goog.typeOf(a)\n};\ngoog.isObject = function(a) {\n  var b = typeof a;\n  return"object" == b && null != a || "function" == b\n};\ngoog.getUid = function(a) {\n  return a[goog.UID_PROPERTY_] || (a[goog.UID_PROPERTY_] = ++goog.uidCounter_)\n};\ngoog.removeUid = function(a) {\n  "removeAttribute" in a && a.removeAttribute(goog.UID_PROPERTY_);\n  try {\n    delete a[goog.UID_PROPERTY_]\n  }catch(b) {\n  }\n};\ngoog.UID_PROPERTY_ = "closure_uid_" + Math.floor(2147483648 * Math.random()).toString(36);\ngoog.uidCounter_ = 0;\ngoog.getHashCode = goog.getUid;\ngoog.removeHashCode = goog.removeUid;\ngoog.cloneObject = function(a) {\n  var b = goog.typeOf(a);\n  if("object" == b || "array" == b) {\n    if(a.clone) {\n      return a.clone()\n    }\n    var b = "array" == b ? [] : {}, c;\n    for(c in a) {\n      b[c] = goog.cloneObject(a[c])\n    }\n    return b\n  }\n  return a\n};\ngoog.bindNative_ = function(a, b, c) {\n  return a.call.apply(a.bind, arguments)\n};\ngoog.bindJs_ = function(a, b, c) {\n  if(!a) {\n    throw Error();\n  }\n  if(2 < arguments.length) {\n    var d = Array.prototype.slice.call(arguments, 2);\n    return function() {\n      var c = Array.prototype.slice.call(arguments);\n      Array.prototype.unshift.apply(c, d);\n      return a.apply(b, c)\n    }\n  }\n  return function() {\n    return a.apply(b, arguments)\n  }\n};\ngoog.bind = function(a, b, c) {\n  goog.bind = Function.prototype.bind && -1 != Function.prototype.bind.toString().indexOf("native code") ? goog.bindNative_ : goog.bindJs_;\n  return goog.bind.apply(null, arguments)\n};\ngoog.partial = function(a, b) {\n  var c = Array.prototype.slice.call(arguments, 1);\n  return function() {\n    var b = Array.prototype.slice.call(arguments);\n    b.unshift.apply(b, c);\n    return a.apply(this, b)\n  }\n};\ngoog.mixin = function(a, b) {\n  for(var c in b) {\n    a[c] = b[c]\n  }\n};\ngoog.now = Date.now || function() {\n  return+new Date\n};\ngoog.globalEval = function(a) {\n  if(goog.global.execScript) {\n    goog.global.execScript(a, "JavaScript")\n  }else {\n    if(goog.global.eval) {\n      if(null == goog.evalWorksForGlobals_ && (goog.global.eval("var _et_ = 1;"), "undefined" != typeof goog.global._et_ ? (delete goog.global._et_, goog.evalWorksForGlobals_ = !0) : goog.evalWorksForGlobals_ = !1), goog.evalWorksForGlobals_) {\n        goog.global.eval(a)\n      }else {\n        var b = goog.global.document, c = b.createElement("script");\n        c.type = "text/javascript";\n        c.defer = !1;\n        c.appendChild(b.createTextNode(a));\n        b.body.appendChild(c);\n        b.body.removeChild(c)\n      }\n    }else {\n      throw Error("goog.globalEval not available");\n    }\n  }\n};\ngoog.evalWorksForGlobals_ = null;\ngoog.getCssName = function(a, b) {\n  var c = function(a) {\n    return goog.cssNameMapping_[a] || a\n  }, d = function(a) {\n    for(var a = a.split("-"), b = [], d = 0;d < a.length;d++) {\n      b.push(c(a[d]))\n    }\n    return b.join("-")\n  }, d = goog.cssNameMapping_ ? "BY_WHOLE" == goog.cssNameMappingStyle_ ? c : d : function(a) {\n    return a\n  };\n  return b ? a + "-" + d(b) : d(a)\n};\ngoog.setCssNameMapping = function(a, b) {\n  goog.cssNameMapping_ = a;\n  goog.cssNameMappingStyle_ = b\n};\n!COMPILED && goog.global.CLOSURE_CSS_NAME_MAPPING && (goog.cssNameMapping_ = goog.global.CLOSURE_CSS_NAME_MAPPING);\ngoog.getMsg = function(a, b) {\n  var c = b || {}, d;\n  for(d in c) {\n    var e = ("" + c[d]).replace(/\\$/g, "$$$$"), a = a.replace(RegExp("\\\\{\\\\$" + d + "\\\\}", "gi"), e)\n  }\n  return a\n};\ngoog.exportSymbol = function(a, b, c) {\n  goog.exportPath_(a, b, c)\n};\ngoog.exportProperty = function(a, b, c) {\n  a[b] = c\n};\ngoog.inherits = function(a, b) {\n  function c() {\n  }\n  c.prototype = b.prototype;\n  a.superClass_ = b.prototype;\n  a.prototype = new c;\n  a.prototype.constructor = a\n};\ngoog.base = function(a, b, c) {\n  var d = arguments.callee.caller;\n  if(d.superClass_) {\n    return d.superClass_.constructor.apply(a, Array.prototype.slice.call(arguments, 1))\n  }\n  for(var e = Array.prototype.slice.call(arguments, 2), f = !1, g = a.constructor;g;g = g.superClass_ && g.superClass_.constructor) {\n    if(g.prototype[b] === d) {\n      f = !0\n    }else {\n      if(f) {\n        return g.prototype[b].apply(a, e)\n      }\n    }\n  }\n  if(a[b] === d) {\n    return a.constructor.prototype[b].apply(a, e)\n  }\n  throw Error("goog.base called from a method of one name to a method of a different name");\n};\ngoog.scope = function(a) {\n  a.call(goog.global)\n};\nvar USE_TYPEDARRAY = "undefined" !== typeof Uint8Array && "undefined" !== typeof Uint16Array && "undefined" !== typeof Uint32Array;\nvar Zlib = {BitStream:function(a, b) {\n  this.index = "number" === typeof b ? b : 0;\n  this.bitindex = 0;\n  this.buffer = a instanceof (USE_TYPEDARRAY ? Uint8Array : Array) ? a : new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.BitStream.DefaultBlockSize);\n  if(2 * this.buffer.length <= this.index) {\n    throw Error("invalid index");\n  }\n  this.buffer.length <= this.index && this.expandBuffer()\n}};\nZlib.BitStream.DefaultBlockSize = 32768;\nZlib.BitStream.prototype.expandBuffer = function() {\n  var a = this.buffer, b, c = a.length, d = new (USE_TYPEDARRAY ? Uint8Array : Array)(c << 1);\n  if(USE_TYPEDARRAY) {\n    d.set(a)\n  }else {\n    for(b = 0;b < c;++b) {\n      d[b] = a[b]\n    }\n  }\n  return this.buffer = d\n};\nZlib.BitStream.prototype.writeBits = function(a, b, c) {\n  var d = this.buffer, e = this.index, f = this.bitindex, g = d[e];\n  c && 1 < b && (a = 8 < b ? (Zlib.BitStream.ReverseTable[a & 255] << 24 | Zlib.BitStream.ReverseTable[a >>> 8 & 255] << 16 | Zlib.BitStream.ReverseTable[a >>> 16 & 255] << 8 | Zlib.BitStream.ReverseTable[a >>> 24 & 255]) >> 32 - b : Zlib.BitStream.ReverseTable[a] >> 8 - b);\n  if(8 > b + f) {\n    g = g << b | a, f += b\n  }else {\n    for(c = 0;c < b;++c) {\n      g = g << 1 | a >> b - c - 1 & 1, 8 === ++f && (f = 0, d[e++] = Zlib.BitStream.ReverseTable[g], g = 0, e === d.length && (d = this.expandBuffer()))\n    }\n  }\n  d[e] = g;\n  this.buffer = d;\n  this.bitindex = f;\n  this.index = e\n};\nZlib.BitStream.prototype.finish = function() {\n  var a = this.buffer, b = this.index;\n  0 < this.bitindex && (a[b] <<= 8 - this.bitindex, a[b] = Zlib.BitStream.ReverseTable[a[b]], b++);\n  USE_TYPEDARRAY ? a = a.subarray(0, b) : a.length = b;\n  return a\n};\nZlib.BitStream.ReverseTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(256), b;\n  for(b = 0;256 > b;++b) {\n    for(var c = a, d = b, e = b, f = e, g = 7, e = e >>> 1;e;e >>>= 1) {\n      f <<= 1, f |= e & 1, --g\n    }\n    c[d] = (f << g & 255) >>> 0\n  }\n  return a\n}());\nZlib.CRC32 = {};\nZlib.CRC32.calc = function(a, b, c) {\n  return Zlib.CRC32.update(a, 0, b, c)\n};\nZlib.CRC32.update = function(a, b, c, d) {\n  for(var e = Zlib.CRC32.Table, f = "number" === typeof c ? c : c = 0, d = "number" === typeof d ? d : a.length, b = b ^ 4294967295, f = d & 7;f--;++c) {\n    b = b >>> 8 ^ e[(b ^ a[c]) & 255]\n  }\n  for(f = d >> 3;f--;c += 8) {\n    b = b >>> 8 ^ e[(b ^ a[c]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 1]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 2]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 3]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 4]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 5]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 6]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 7]) & 255]\n  }\n  return(b ^ 4294967295) >>> 0\n};\nZlib.CRC32.Table = function(a) {\n  return USE_TYPEDARRAY ? new Uint32Array(a) : a\n}([0, 1996959894, 3993919788, 2567524794, 124634137, 1886057615, 3915621685, 2657392035, 249268274, 2044508324, 3772115230, 2547177864, 162941995, 2125561021, 3887607047, 2428444049, 498536548, 1789927666, 4089016648, 2227061214, 450548861, 1843258603, 4107580753, 2211677639, 325883990, 1684777152, 4251122042, 2321926636, 335633487, 1661365465, 4195302755, 2366115317, 997073096, 1281953886, 3579855332, 2724688242, 1006888145, 1258607687, 3524101629, 2768942443, 901097722, 1119000684, 3686517206, \n2898065728, 853044451, 1172266101, 3705015759, 2882616665, 651767980, 1373503546, 3369554304, 3218104598, 565507253, 1454621731, 3485111705, 3099436303, 671266974, 1594198024, 3322730930, 2970347812, 795835527, 1483230225, 3244367275, 3060149565, 1994146192, 31158534, 2563907772, 4023717930, 1907459465, 112637215, 2680153253, 3904427059, 2013776290, 251722036, 2517215374, 3775830040, 2137656763, 141376813, 2439277719, 3865271297, 1802195444, 476864866, 2238001368, 4066508878, 1812370925, 453092731, \n2181625025, 4111451223, 1706088902, 314042704, 2344532202, 4240017532, 1658658271, 366619977, 2362670323, 4224994405, 1303535960, 984961486, 2747007092, 3569037538, 1256170817, 1037604311, 2765210733, 3554079995, 1131014506, 879679996, 2909243462, 3663771856, 1141124467, 855842277, 2852801631, 3708648649, 1342533948, 654459306, 3188396048, 3373015174, 1466479909, 544179635, 3110523913, 3462522015, 1591671054, 702138776, 2966460450, 3352799412, 1504918807, 783551873, 3082640443, 3233442989, 3988292384, \n2596254646, 62317068, 1957810842, 3939845945, 2647816111, 81470997, 1943803523, 3814918930, 2489596804, 225274430, 2053790376, 3826175755, 2466906013, 167816743, 2097651377, 4027552580, 2265490386, 503444072, 1762050814, 4150417245, 2154129355, 426522225, 1852507879, 4275313526, 2312317920, 282753626, 1742555852, 4189708143, 2394877945, 397917763, 1622183637, 3604390888, 2714866558, 953729732, 1340076626, 3518719985, 2797360999, 1068828381, 1219638859, 3624741850, 2936675148, 906185462, 1090812512, \n3747672003, 2825379669, 829329135, 1181335161, 3412177804, 3160834842, 628085408, 1382605366, 3423369109, 3138078467, 570562233, 1426400815, 3317316542, 2998733608, 733239954, 1555261956, 3268935591, 3050360625, 752459403, 1541320221, 2607071920, 3965973030, 1969922972, 40735498, 2617837225, 3943577151, 1913087877, 83908371, 2512341634, 3803740692, 2075208622, 213261112, 2463272603, 3855990285, 2094854071, 198958881, 2262029012, 4057260610, 1759359992, 534414190, 2176718541, 4139329115, 1873836001, \n414664567, 2282248934, 4279200368, 1711684554, 285281116, 2405801727, 4167216745, 1634467795, 376229701, 2685067896, 3608007406, 1308918612, 956543938, 2808555105, 3495958263, 1231636301, 1047427035, 2932959818, 3654703836, 1088359270, 936918E3, 2847714899, 3736837829, 1202900863, 817233897, 3183342108, 3401237130, 1404277552, 615818150, 3134207493, 3453421203, 1423857449, 601450431, 3009837614, 3294710456, 1567103746, 711928724, 3020668471, 3272380065, 1510334235, 755167117]);\nZlib.exportObject = function(a, b) {\n  var c, d, e, f;\n  if(Object.keys) {\n    c = Object.keys(b)\n  }else {\n    for(d in c = [], e = 0, b) {\n      c[e++] = d\n    }\n  }\n  e = 0;\n  for(f = c.length;e < f;++e) {\n    d = c[e], goog.exportSymbol(a + "." + d, b[d])\n  }\n};\nZlib.GunzipMember = function() {\n};\nZlib.GunzipMember.prototype.getName = function() {\n  return this.name\n};\nZlib.GunzipMember.prototype.getData = function() {\n  return this.data\n};\nZlib.GunzipMember.prototype.getMtime = function() {\n  return this.mtime\n};\nZlib.Heap = function(a) {\n  this.buffer = new (USE_TYPEDARRAY ? Uint16Array : Array)(2 * a);\n  this.length = 0\n};\nZlib.Heap.prototype.getParent = function(a) {\n  return 2 * ((a - 2) / 4 | 0)\n};\nZlib.Heap.prototype.getChild = function(a) {\n  return 2 * a + 2\n};\nZlib.Heap.prototype.push = function(a, b) {\n  var c, d, e = this.buffer, f;\n  c = this.length;\n  e[this.length++] = b;\n  for(e[this.length++] = a;0 < c;) {\n    if(d = this.getParent(c), e[c] > e[d]) {\n      f = e[c], e[c] = e[d], e[d] = f, f = e[c + 1], e[c + 1] = e[d + 1], e[d + 1] = f, c = d\n    }else {\n      break\n    }\n  }\n  return this.length\n};\nZlib.Heap.prototype.pop = function() {\n  var a, b, c = this.buffer, d, e, f;\n  b = c[0];\n  a = c[1];\n  this.length -= 2;\n  c[0] = c[this.length];\n  c[1] = c[this.length + 1];\n  for(f = 0;;) {\n    e = this.getChild(f);\n    if(e >= this.length) {\n      break\n    }\n    e + 2 < this.length && c[e + 2] > c[e] && (e += 2);\n    if(c[e] > c[f]) {\n      d = c[f], c[f] = c[e], c[e] = d, d = c[f + 1], c[f + 1] = c[e + 1], c[e + 1] = d\n    }else {\n      break\n    }\n    f = e\n  }\n  return{index:a, value:b, length:this.length}\n};\nZlib.Huffman = {};\nZlib.Huffman.buildHuffmanTable = function(a) {\n  var b = a.length, c = 0, d = Number.POSITIVE_INFINITY, e, f, g, h, i, j, l, m, k;\n  for(m = 0;m < b;++m) {\n    a[m] > c && (c = a[m]), a[m] < d && (d = a[m])\n  }\n  e = 1 << c;\n  f = new (USE_TYPEDARRAY ? Uint32Array : Array)(e);\n  g = 1;\n  h = 0;\n  for(i = 2;g <= c;) {\n    for(m = 0;m < b;++m) {\n      if(a[m] === g) {\n        j = 0;\n        l = h;\n        for(k = 0;k < g;++k) {\n          j = j << 1 | l & 1, l >>= 1\n        }\n        for(k = j;k < e;k += i) {\n          f[k] = g << 16 | m\n        }\n        ++h\n      }\n    }\n    ++g;\n    h <<= 1;\n    i <<= 1\n  }\n  return[f, c, d]\n};\nZlib.RawDeflate = function(a, b) {\n  this.compressionType = Zlib.RawDeflate.CompressionType.DYNAMIC;\n  this.lazy = 0;\n  this.input = a;\n  this.op = 0;\n  b && (b.lazy && (this.lazy = b.lazy), "number" === typeof b.compressionType && (this.compressionType = b.compressionType), b.outputBuffer && (this.output = USE_TYPEDARRAY && b.outputBuffer instanceof Array ? new Uint8Array(b.outputBuffer) : b.outputBuffer), "number" === typeof b.outputIndex && (this.op = b.outputIndex));\n  this.output || (this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(32768))\n};\nZlib.RawDeflate.CompressionType = {NONE:0, FIXED:1, DYNAMIC:2, RESERVED:3};\nZlib.RawDeflate.Lz77MinLength = 3;\nZlib.RawDeflate.Lz77MaxLength = 258;\nZlib.RawDeflate.WindowSize = 32768;\nZlib.RawDeflate.MaxCodeLength = 16;\nZlib.RawDeflate.HUFMAX = 286;\nZlib.RawDeflate.FixedHuffmanTable = function() {\n  var a = [], b;\n  for(b = 0;288 > b;b++) {\n    switch(!0) {\n      case 143 >= b:\n        a.push([b + 48, 8]);\n        break;\n      case 255 >= b:\n        a.push([b - 144 + 400, 9]);\n        break;\n      case 279 >= b:\n        a.push([b - 256 + 0, 7]);\n        break;\n      case 287 >= b:\n        a.push([b - 280 + 192, 8]);\n        break;\n      default:\n        throw"invalid literal: " + b;\n    }\n  }\n  return a\n}();\nZlib.RawDeflate.prototype.compress = function() {\n  var a, b, c, d = this.input;\n  switch(this.compressionType) {\n    case Zlib.RawDeflate.CompressionType.NONE:\n      b = 0;\n      for(c = d.length;b < c;) {\n        a = USE_TYPEDARRAY ? d.subarray(b, b + 65535) : d.slice(b, b + 65535), b += a.length, this.makeNocompressBlock(a, b === c)\n      }\n      break;\n    case Zlib.RawDeflate.CompressionType.FIXED:\n      this.output = this.makeFixedHuffmanBlock(d, !0);\n      this.op = this.output.length;\n      break;\n    case Zlib.RawDeflate.CompressionType.DYNAMIC:\n      this.output = this.makeDynamicHuffmanBlock(d, !0);\n      this.op = this.output.length;\n      break;\n    default:\n      throw"invalid compression type";\n  }\n  return this.output\n};\nZlib.RawDeflate.prototype.makeNocompressBlock = function(a, b) {\n  var c, d, e = this.output, f = this.op;\n  if(USE_TYPEDARRAY) {\n    for(e = new Uint8Array(this.output.buffer);e.length <= f + a.length + 5;) {\n      e = new Uint8Array(e.length << 1)\n    }\n    e.set(this.output)\n  }\n  c = Zlib.RawDeflate.CompressionType.NONE;\n  e[f++] = (b ? 1 : 0) | c << 1;\n  c = a.length;\n  d = ~c + 65536 & 65535;\n  e[f++] = c & 255;\n  e[f++] = c >>> 8 & 255;\n  e[f++] = d & 255;\n  e[f++] = d >>> 8 & 255;\n  if(USE_TYPEDARRAY) {\n    e.set(a, f), f += a.length, e = e.subarray(0, f)\n  }else {\n    c = 0;\n    for(d = a.length;c < d;++c) {\n      e[f++] = a[c]\n    }\n    e.length = f\n  }\n  this.op = f;\n  return this.output = e\n};\nZlib.RawDeflate.prototype.makeFixedHuffmanBlock = function(a, b) {\n  var c = new Zlib.BitStream(new Uint8Array(this.output.buffer), this.op), d;\n  d = Zlib.RawDeflate.CompressionType.FIXED;\n  c.writeBits(b ? 1 : 0, 1, !0);\n  c.writeBits(d, 2, !0);\n  d = this.lz77(a);\n  this.fixedHuffman(d, c);\n  return c.finish()\n};\nZlib.RawDeflate.prototype.makeDynamicHuffmanBlock = function(a, b) {\n  var c = new Zlib.BitStream(new Uint8Array(this.output), this.op), d, e, f, g, h = [16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15], i, j, l, m, k, n, q = Array(19), p;\n  d = Zlib.RawDeflate.CompressionType.DYNAMIC;\n  c.writeBits(b ? 1 : 0, 1, !0);\n  c.writeBits(d, 2, !0);\n  d = this.lz77(a);\n  i = this.getLengths_(this.freqsLitLen, 15);\n  j = this.getCodesFromLengths_(i);\n  l = this.getLengths_(this.freqsDist, 7);\n  m = this.getCodesFromLengths_(l);\n  for(e = 286;257 < e && 0 === i[e - 1];e--) {\n  }\n  for(f = 30;1 < f && 0 === l[f - 1];f--) {\n  }\n  k = this.getTreeSymbols_(e, i, f, l);\n  n = this.getLengths_(k.freqs, 7);\n  for(p = 0;19 > p;p++) {\n    q[p] = n[h[p]]\n  }\n  for(g = 19;4 < g && 0 === q[g - 1];g--) {\n  }\n  h = this.getCodesFromLengths_(n);\n  c.writeBits(e - 257, 5, !0);\n  c.writeBits(f - 1, 5, !0);\n  c.writeBits(g - 4, 4, !0);\n  for(p = 0;p < g;p++) {\n    c.writeBits(q[p], 3, !0)\n  }\n  p = 0;\n  for(q = k.codes.length;p < q;p++) {\n    if(e = k.codes[p], c.writeBits(h[e], n[e], !0), 16 <= e) {\n      p++;\n      switch(e) {\n        case 16:\n          e = 2;\n          break;\n        case 17:\n          e = 3;\n          break;\n        case 18:\n          e = 7;\n          break;\n        default:\n          throw"invalid code: " + e;\n      }\n      c.writeBits(k.codes[p], e, !0)\n    }\n  }\n  this.dynamicHuffman(d, [j, i], [m, l], c);\n  return c.finish()\n};\nZlib.RawDeflate.prototype.dynamicHuffman = function(a, b, c, d) {\n  var e, f, g, h, i;\n  g = b[0];\n  b = b[1];\n  h = c[0];\n  i = c[1];\n  c = 0;\n  for(e = a.length;c < e;++c) {\n    if(f = a[c], d.writeBits(g[f], b[f], !0), 256 < f) {\n      d.writeBits(a[++c], a[++c], !0), f = a[++c], d.writeBits(h[f], i[f], !0), d.writeBits(a[++c], a[++c], !0)\n    }else {\n      if(256 === f) {\n        break\n      }\n    }\n  }\n  return d\n};\nZlib.RawDeflate.prototype.fixedHuffman = function(a, b) {\n  var c, d, e;\n  c = 0;\n  for(d = a.length;c < d;c++) {\n    if(e = a[c], Zlib.BitStream.prototype.writeBits.apply(b, Zlib.RawDeflate.FixedHuffmanTable[e]), 256 < e) {\n      b.writeBits(a[++c], a[++c], !0), b.writeBits(a[++c], 5), b.writeBits(a[++c], a[++c], !0)\n    }else {\n      if(256 === e) {\n        break\n      }\n    }\n  }\n  return b\n};\nZlib.RawDeflate.Lz77Match = function(a, b) {\n  this.length = a;\n  this.backwardDistance = b\n};\nZlib.RawDeflate.Lz77Match.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint32Array(a) : a\n}(function() {\n  function a(a) {\n    switch(!0) {\n      case 3 === a:\n        return[257, a - 3, 0];\n      case 4 === a:\n        return[258, a - 4, 0];\n      case 5 === a:\n        return[259, a - 5, 0];\n      case 6 === a:\n        return[260, a - 6, 0];\n      case 7 === a:\n        return[261, a - 7, 0];\n      case 8 === a:\n        return[262, a - 8, 0];\n      case 9 === a:\n        return[263, a - 9, 0];\n      case 10 === a:\n        return[264, a - 10, 0];\n      case 12 >= a:\n        return[265, a - 11, 1];\n      case 14 >= a:\n        return[266, a - 13, 1];\n      case 16 >= a:\n        return[267, a - 15, 1];\n      case 18 >= a:\n        return[268, a - 17, 1];\n      case 22 >= a:\n        return[269, a - 19, 2];\n      case 26 >= a:\n        return[270, a - 23, 2];\n      case 30 >= a:\n        return[271, a - 27, 2];\n      case 34 >= a:\n        return[272, a - 31, 2];\n      case 42 >= a:\n        return[273, a - 35, 3];\n      case 50 >= a:\n        return[274, a - 43, 3];\n      case 58 >= a:\n        return[275, a - 51, 3];\n      case 66 >= a:\n        return[276, a - 59, 3];\n      case 82 >= a:\n        return[277, a - 67, 4];\n      case 98 >= a:\n        return[278, a - 83, 4];\n      case 114 >= a:\n        return[279, a - 99, 4];\n      case 130 >= a:\n        return[280, a - 115, 4];\n      case 162 >= a:\n        return[281, a - 131, 5];\n      case 194 >= a:\n        return[282, a - 163, 5];\n      case 226 >= a:\n        return[283, a - 195, 5];\n      case 257 >= a:\n        return[284, a - 227, 5];\n      case 258 === a:\n        return[285, a - 258, 0];\n      default:\n        throw"invalid length: " + a;\n    }\n  }\n  var b = [], c, d;\n  for(c = 3;258 >= c;c++) {\n    d = a(c), b[c] = d[2] << 24 | d[1] << 16 | d[0]\n  }\n  return b\n}());\nZlib.RawDeflate.Lz77Match.prototype.getDistanceCode_ = function(a) {\n  switch(!0) {\n    case 1 === a:\n      a = [0, a - 1, 0];\n      break;\n    case 2 === a:\n      a = [1, a - 2, 0];\n      break;\n    case 3 === a:\n      a = [2, a - 3, 0];\n      break;\n    case 4 === a:\n      a = [3, a - 4, 0];\n      break;\n    case 6 >= a:\n      a = [4, a - 5, 1];\n      break;\n    case 8 >= a:\n      a = [5, a - 7, 1];\n      break;\n    case 12 >= a:\n      a = [6, a - 9, 2];\n      break;\n    case 16 >= a:\n      a = [7, a - 13, 2];\n      break;\n    case 24 >= a:\n      a = [8, a - 17, 3];\n      break;\n    case 32 >= a:\n      a = [9, a - 25, 3];\n      break;\n    case 48 >= a:\n      a = [10, a - 33, 4];\n      break;\n    case 64 >= a:\n      a = [11, a - 49, 4];\n      break;\n    case 96 >= a:\n      a = [12, a - 65, 5];\n      break;\n    case 128 >= a:\n      a = [13, a - 97, 5];\n      break;\n    case 192 >= a:\n      a = [14, a - 129, 6];\n      break;\n    case 256 >= a:\n      a = [15, a - 193, 6];\n      break;\n    case 384 >= a:\n      a = [16, a - 257, 7];\n      break;\n    case 512 >= a:\n      a = [17, a - 385, 7];\n      break;\n    case 768 >= a:\n      a = [18, a - 513, 8];\n      break;\n    case 1024 >= a:\n      a = [19, a - 769, 8];\n      break;\n    case 1536 >= a:\n      a = [20, a - 1025, 9];\n      break;\n    case 2048 >= a:\n      a = [21, a - 1537, 9];\n      break;\n    case 3072 >= a:\n      a = [22, a - 2049, 10];\n      break;\n    case 4096 >= a:\n      a = [23, a - 3073, 10];\n      break;\n    case 6144 >= a:\n      a = [24, a - 4097, 11];\n      break;\n    case 8192 >= a:\n      a = [25, a - 6145, 11];\n      break;\n    case 12288 >= a:\n      a = [26, a - 8193, 12];\n      break;\n    case 16384 >= a:\n      a = [27, a - 12289, 12];\n      break;\n    case 24576 >= a:\n      a = [28, a - 16385, 13];\n      break;\n    case 32768 >= a:\n      a = [29, a - 24577, 13];\n      break;\n    default:\n      throw"invalid distance";\n  }\n  return a\n};\nZlib.RawDeflate.Lz77Match.prototype.toLz77Array = function() {\n  var a = this.backwardDistance, b = [], c = 0, d;\n  d = Zlib.RawDeflate.Lz77Match.LengthCodeTable[this.length];\n  b[c++] = d & 65535;\n  b[c++] = d >> 16 & 255;\n  b[c++] = d >> 24;\n  d = this.getDistanceCode_(a);\n  b[c++] = d[0];\n  b[c++] = d[1];\n  b[c++] = d[2];\n  return b\n};\nZlib.RawDeflate.prototype.lz77 = function(a) {\n  function b(a, b) {\n    var c = a.toLz77Array(), d, e;\n    d = 0;\n    for(e = c.length;d < e;++d) {\n      l[m++] = c[d]\n    }\n    n[c[0]]++;\n    q[c[3]]++;\n    k = a.length + b - 1;\n    j = null\n  }\n  var c, d, e, f, g, h = {}, i = Zlib.RawDeflate.WindowSize, j, l = USE_TYPEDARRAY ? new Uint16Array(2 * a.length) : [], m = 0, k = 0, n = new (USE_TYPEDARRAY ? Uint32Array : Array)(286), q = new (USE_TYPEDARRAY ? Uint32Array : Array)(30), p = this.lazy;\n  if(!USE_TYPEDARRAY) {\n    for(e = 0;285 >= e;) {\n      n[e++] = 0\n    }\n    for(e = 0;29 >= e;) {\n      q[e++] = 0\n    }\n  }\n  n[256] = 1;\n  c = 0;\n  for(d = a.length;c < d;++c) {\n    e = g = 0;\n    for(f = Zlib.RawDeflate.Lz77MinLength;e < f && c + e !== d;++e) {\n      g = g << 8 | a[c + e]\n    }\n    void 0 === h[g] && (h[g] = []);\n    e = h[g];\n    if(!(0 < k--)) {\n      for(;0 < e.length && c - e[0] > i;) {\n        e.shift()\n      }\n      if(c + Zlib.RawDeflate.Lz77MinLength >= d) {\n        j && b(j, -1);\n        e = 0;\n        for(f = d - c;e < f;++e) {\n          g = a[c + e], l[m++] = g, ++n[g]\n        }\n        break\n      }\n      0 < e.length ? (f = this.searchLongestMatch_(a, c, e), j ? j.length < f.length ? (g = a[c - 1], l[m++] = g, ++n[g], b(f, 0)) : b(j, -1) : f.length < p ? j = f : b(f, 0)) : j ? b(j, -1) : (g = a[c], l[m++] = g, ++n[g])\n    }\n    e.push(c)\n  }\n  l[m++] = 256;\n  n[256]++;\n  this.freqsLitLen = n;\n  this.freqsDist = q;\n  return USE_TYPEDARRAY ? l.subarray(0, m) : l\n};\nZlib.RawDeflate.prototype.searchLongestMatch_ = function(a, b, c) {\n  var d, e, f = 0, g, h, i, j = a.length;\n  h = 0;\n  i = c.length;\n  a:for(;h < i;h++) {\n    d = c[i - h - 1];\n    g = Zlib.RawDeflate.Lz77MinLength;\n    if(f > Zlib.RawDeflate.Lz77MinLength) {\n      for(g = f;g > Zlib.RawDeflate.Lz77MinLength;g--) {\n        if(a[d + g - 1] !== a[b + g - 1]) {\n          continue a\n        }\n      }\n      g = f\n    }\n    for(;g < Zlib.RawDeflate.Lz77MaxLength && b + g < j && a[d + g] === a[b + g];) {\n      ++g\n    }\n    g > f && (e = d, f = g);\n    if(g === Zlib.RawDeflate.Lz77MaxLength) {\n      break\n    }\n  }\n  return new Zlib.RawDeflate.Lz77Match(f, b - e)\n};\nZlib.RawDeflate.prototype.getTreeSymbols_ = function(a, b, c, d) {\n  var e = new (USE_TYPEDARRAY ? Uint32Array : Array)(a + c), f, g, h = new (USE_TYPEDARRAY ? Uint32Array : Array)(316), i = new (USE_TYPEDARRAY ? Uint8Array : Array)(19);\n  for(f = g = 0;f < a;f++) {\n    e[g++] = b[f]\n  }\n  for(f = 0;f < c;f++) {\n    e[g++] = d[f]\n  }\n  if(!USE_TYPEDARRAY) {\n    f = 0;\n    for(b = i.length;f < b;++f) {\n      i[f] = 0\n    }\n  }\n  f = c = 0;\n  for(b = e.length;f < b;f += g) {\n    for(g = 1;f + g < b && e[f + g] === e[f];++g) {\n    }\n    a = g;\n    if(0 === e[f]) {\n      if(3 > a) {\n        for(;0 < a--;) {\n          h[c++] = 0, i[0]++\n        }\n      }else {\n        for(;0 < a;) {\n          d = 138 > a ? a : 138, d > a - 3 && d < a && (d = a - 3), 10 >= d ? (h[c++] = 17, h[c++] = d - 3, i[17]++) : (h[c++] = 18, h[c++] = d - 11, i[18]++), a -= d\n        }\n      }\n    }else {\n      if(h[c++] = e[f], i[e[f]]++, a--, 3 > a) {\n        for(;0 < a--;) {\n          h[c++] = e[f], i[e[f]]++\n        }\n      }else {\n        for(;0 < a;) {\n          d = 6 > a ? a : 6, d > a - 3 && d < a && (d = a - 3), h[c++] = 16, h[c++] = d - 3, i[16]++, a -= d\n        }\n      }\n    }\n  }\n  return{codes:USE_TYPEDARRAY ? h.subarray(0, c) : h.slice(0, c), freqs:i}\n};\nZlib.RawDeflate.prototype.getLengths_ = function(a, b) {\n  var c = a.length, d = new Zlib.Heap(2 * Zlib.RawDeflate.HUFMAX), e = new (USE_TYPEDARRAY ? Uint8Array : Array)(c), f, g, h;\n  if(!USE_TYPEDARRAY) {\n    for(g = 0;g < c;g++) {\n      e[g] = 0\n    }\n  }\n  for(g = 0;g < c;++g) {\n    0 < a[g] && d.push(g, a[g])\n  }\n  c = Array(d.length / 2);\n  f = new (USE_TYPEDARRAY ? Uint32Array : Array)(d.length / 2);\n  if(1 === c.length) {\n    return e[d.pop().index] = 1, e\n  }\n  g = 0;\n  for(h = d.length / 2;g < h;++g) {\n    c[g] = d.pop(), f[g] = c[g].value\n  }\n  d = this.reversePackageMerge_(f, f.length, b);\n  g = 0;\n  for(h = c.length;g < h;++g) {\n    e[c[g].index] = d[g]\n  }\n  return e\n};\nZlib.RawDeflate.prototype.reversePackageMerge_ = function(a, b, c) {\n  function d(a) {\n    var c = i[a][j[a]];\n    c === b ? (d(a + 1), d(a + 1)) : --g[c];\n    ++j[a]\n  }\n  var e = new (USE_TYPEDARRAY ? Uint16Array : Array)(c), f = new (USE_TYPEDARRAY ? Uint8Array : Array)(c), g = new (USE_TYPEDARRAY ? Uint8Array : Array)(b), h = Array(c), i = Array(c), j = Array(c), l = (1 << c) - b, m = 1 << c - 1, k, n;\n  e[c - 1] = b;\n  for(k = 0;k < c;++k) {\n    l < m ? f[k] = 0 : (f[k] = 1, l -= m), l <<= 1, e[c - 2 - k] = (e[c - 1 - k] / 2 | 0) + b\n  }\n  e[0] = f[0];\n  h[0] = Array(e[0]);\n  i[0] = Array(e[0]);\n  for(k = 1;k < c;++k) {\n    e[k] > 2 * e[k - 1] + f[k] && (e[k] = 2 * e[k - 1] + f[k]), h[k] = Array(e[k]), i[k] = Array(e[k])\n  }\n  for(l = 0;l < b;++l) {\n    g[l] = c\n  }\n  for(m = 0;m < e[c - 1];++m) {\n    h[c - 1][m] = a[m], i[c - 1][m] = m\n  }\n  for(l = 0;l < c;++l) {\n    j[l] = 0\n  }\n  1 === f[c - 1] && (--g[0], ++j[c - 1]);\n  for(k = c - 2;0 <= k;--k) {\n    c = l = 0;\n    n = j[k + 1];\n    for(m = 0;m < e[k];m++) {\n      c = h[k + 1][n] + h[k + 1][n + 1], c > a[l] ? (h[k][m] = c, i[k][m] = b, n += 2) : (h[k][m] = a[l], i[k][m] = l, ++l)\n    }\n    j[k] = 0;\n    1 === f[k] && d(k)\n  }\n  return g\n};\nZlib.RawDeflate.prototype.getCodesFromLengths_ = function(a) {\n  var b = new (USE_TYPEDARRAY ? Uint16Array : Array)(a.length), c = [], d = [], e = 0, f, g, h;\n  f = 0;\n  for(g = a.length;f < g;f++) {\n    c[a[f]] = (c[a[f]] | 0) + 1\n  }\n  f = 1;\n  for(g = Zlib.RawDeflate.MaxCodeLength;f <= g;f++) {\n    d[f] = e, e += c[f] | 0, e <<= 1\n  }\n  f = 0;\n  for(g = a.length;f < g;f++) {\n    e = d[a[f]];\n    d[a[f]] += 1;\n    c = b[f] = 0;\n    for(h = a[f];c < h;c++) {\n      b[f] = b[f] << 1 | e & 1, e >>>= 1\n    }\n  }\n  return b\n};\nZlib.Gzip = function(a, b) {\n  this.input = a;\n  this.op = this.ip = 0;\n  this.flags = {};\n  b && (b.flags && (this.flags = b.flags), "string" === typeof b.filename && (this.filename = b.filename), "string" === typeof b.comment && (this.comment = b.comment), b.deflateOptions && (this.deflateOptions = b.deflateOptions));\n  this.deflateOptions || (this.deflateOptions = {})\n};\nZlib.Gzip.DefaultBufferSize = 32768;\nZlib.Gzip.prototype.compress = function() {\n  var a, b, c, d, e, f = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.Gzip.DefaultBufferSize);\n  c = 0;\n  var g = this.input, h = this.ip;\n  b = this.filename;\n  var i = this.comment;\n  f[c++] = 31;\n  f[c++] = 139;\n  f[c++] = 8;\n  a = 0;\n  this.flags.fname && (a |= Zlib.Gzip.FlagsMask.FNAME);\n  this.flags.fcomment && (a |= Zlib.Gzip.FlagsMask.FCOMMENT);\n  this.flags.fhcrc && (a |= Zlib.Gzip.FlagsMask.FHCRC);\n  f[c++] = a;\n  a = (Date.now ? Date.now() : +new Date) / 1E3 | 0;\n  f[c++] = a & 255;\n  f[c++] = a >>> 8 & 255;\n  f[c++] = a >>> 16 & 255;\n  f[c++] = a >>> 24 & 255;\n  f[c++] = 0;\n  f[c++] = Zlib.Gzip.OperatingSystem.UNKNOWN;\n  if(void 0 !== this.flags.fname) {\n    d = 0;\n    for(e = b.length;d < e;++d) {\n      a = b.charCodeAt(d), 255 < a && (f[c++] = a >>> 8 & 255), f[c++] = a & 255\n    }\n    f[c++] = 0\n  }\n  if(this.flags.comment) {\n    d = 0;\n    for(e = i.length;d < e;++d) {\n      a = i.charCodeAt(d), 255 < a && (f[c++] = a >>> 8 & 255), f[c++] = a & 255\n    }\n    f[c++] = 0\n  }\n  this.flags.fhcrc && (b = Zlib.CRC32.calc(f, 0, c) & 65535, f[c++] = b & 255, f[c++] = b >>> 8 & 255);\n  this.deflateOptions.outputBuffer = f;\n  this.deflateOptions.outputIndex = c;\n  c = new Zlib.RawDeflate(g, this.deflateOptions);\n  f = c.compress();\n  c = c.op;\n  USE_TYPEDARRAY && (c + 8 > f.buffer.byteLength ? (this.output = new Uint8Array(c + 8), this.output.set(new Uint8Array(f.buffer)), f = this.output) : f = new Uint8Array(f.buffer));\n  b = Zlib.CRC32.calc(g);\n  f[c++] = b & 255;\n  f[c++] = b >>> 8 & 255;\n  f[c++] = b >>> 16 & 255;\n  f[c++] = b >>> 24 & 255;\n  e = g.length;\n  f[c++] = e & 255;\n  f[c++] = e >>> 8 & 255;\n  f[c++] = e >>> 16 & 255;\n  f[c++] = e >>> 24 & 255;\n  this.ip = h;\n  USE_TYPEDARRAY && c < f.length && (this.output = f = f.subarray(0, c));\n  return f\n};\nZlib.Gzip.OperatingSystem = {FAT:0, AMIGA:1, VMS:2, UNIX:3, VM_CMS:4, ATARI_TOS:5, HPFS:6, MACINTOSH:7, Z_SYSTEM:8, CP_M:9, TOPS_20:10, NTFS:11, QDOS:12, ACORN_RISCOS:13, UNKNOWN:255};\nZlib.Gzip.FlagsMask = {FTEXT:1, FHCRC:2, FEXTRA:4, FNAME:8, FCOMMENT:16};\nvar ZLIB_RAW_INFLATE_BUFFER_SIZE = 32768;\nZlib.RawInflate = function(a, b) {\n  this.blocks = [];\n  this.bufferSize = ZLIB_RAW_INFLATE_BUFFER_SIZE;\n  this.bitsbuflen = this.bitsbuf = this.ip = this.totalpos = 0;\n  this.input = USE_TYPEDARRAY ? new Uint8Array(a) : a;\n  this.bfinal = !1;\n  this.bufferType = Zlib.RawInflate.BufferType.ADAPTIVE;\n  this.resize = !1;\n  if(b || !(b = {})) {\n    b.index && (this.ip = b.index), b.bufferSize && (this.bufferSize = b.bufferSize), b.bufferType && (this.bufferType = b.bufferType), b.resize && (this.resize = b.resize)\n  }\n  switch(this.bufferType) {\n    case Zlib.RawInflate.BufferType.BLOCK:\n      this.op = Zlib.RawInflate.MaxBackwardLength;\n      this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflate.MaxBackwardLength + this.bufferSize + Zlib.RawInflate.MaxCopyLength);\n      break;\n    case Zlib.RawInflate.BufferType.ADAPTIVE:\n      this.op = 0;\n      this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.bufferSize);\n      this.expandBuffer = this.expandBufferAdaptive;\n      this.concatBuffer = this.concatBufferDynamic;\n      this.decodeHuffman = this.decodeHuffmanAdaptive;\n      break;\n    default:\n      throw Error("invalid inflate mode");\n  }\n};\nZlib.RawInflate.BufferType = {BLOCK:0, ADAPTIVE:1};\nZlib.RawInflate.prototype.decompress = function() {\n  for(;!this.bfinal;) {\n    this.parseBlock()\n  }\n  return this.concatBuffer()\n};\nZlib.RawInflate.MaxBackwardLength = 32768;\nZlib.RawInflate.MaxCopyLength = 258;\nZlib.RawInflate.Order = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15]);\nZlib.RawInflate.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 258, 258]);\nZlib.RawInflate.LengthExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0]);\nZlib.RawInflate.DistCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577]);\nZlib.RawInflate.DistExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13]);\nZlib.RawInflate.FixedLiteralLengthTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(288), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 143 >= b ? 8 : 255 >= b ? 9 : 279 >= b ? 7 : 8\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflate.FixedDistanceTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(30), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 5\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflate.prototype.parseBlock = function() {\n  var a = this.readBits(3);\n  a & 1 && (this.bfinal = !0);\n  a >>>= 1;\n  switch(a) {\n    case 0:\n      this.parseUncompressedBlock();\n      break;\n    case 1:\n      this.parseFixedHuffmanBlock();\n      break;\n    case 2:\n      this.parseDynamicHuffmanBlock();\n      break;\n    default:\n      throw Error("unknown BTYPE: " + a);\n  }\n};\nZlib.RawInflate.prototype.readBits = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f;c < a;) {\n    f = d[e++];\n    if(void 0 === f) {\n      throw Error("input buffer is broken");\n    }\n    b |= f << c;\n    c += 8\n  }\n  f = b & (1 << a) - 1;\n  this.bitsbuf = b >>> a;\n  this.bitsbuflen = c - a;\n  this.ip = e;\n  return f\n};\nZlib.RawInflate.prototype.readCodeByTable = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f = a[0], a = a[1], g;c < a;) {\n    g = d[e++];\n    if(void 0 === g) {\n      throw Error("input buffer is broken");\n    }\n    b |= g << c;\n    c += 8\n  }\n  d = f[b & (1 << a) - 1];\n  f = d >>> 16;\n  this.bitsbuf = b >> f;\n  this.bitsbuflen = c - f;\n  this.ip = e;\n  return d & 65535\n};\nZlib.RawInflate.prototype.parseUncompressedBlock = function() {\n  var a = this.input, b = this.ip, c = this.output, d = this.op, e, f, g, h = c.length;\n  this.bitsbuflen = this.bitsbuf = 0;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: LEN (first byte)");\n  }\n  f = e;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: LEN (second byte)");\n  }\n  f |= e << 8;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: NLEN (first byte)");\n  }\n  g = e;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: NLEN (second byte)");\n  }\n  if(f === ~(g | e << 8)) {\n    throw Error("invalid uncompressed block header: length verify");\n  }\n  if(b + f > a.length) {\n    throw Error("input buffer is broken");\n  }\n  switch(this.bufferType) {\n    case Zlib.RawInflate.BufferType.BLOCK:\n      for(;d + f > c.length;) {\n        e = h - d;\n        f -= e;\n        if(USE_TYPEDARRAY) {\n          c.set(a.subarray(b, b + e), d), d += e, b += e\n        }else {\n          for(;e--;) {\n            c[d++] = a[b++]\n          }\n        }\n        this.op = d;\n        c = this.expandBuffer();\n        d = this.op\n      }\n      break;\n    case Zlib.RawInflate.BufferType.ADAPTIVE:\n      for(;d + f > c.length;) {\n        c = this.expandBuffer({fixRatio:2})\n      }\n      break;\n    default:\n      throw Error("invalid inflate mode");\n  }\n  if(USE_TYPEDARRAY) {\n    c.set(a.subarray(b, b + f), d), d += f, b += f\n  }else {\n    for(;f--;) {\n      c[d++] = a[b++]\n    }\n  }\n  this.ip = b;\n  this.op = d;\n  this.output = c\n};\nZlib.RawInflate.prototype.parseFixedHuffmanBlock = function() {\n  this.decodeHuffman(Zlib.RawInflate.FixedLiteralLengthTable, Zlib.RawInflate.FixedDistanceTable)\n};\nZlib.RawInflate.prototype.parseDynamicHuffmanBlock = function() {\n  function a(a, b, c) {\n    var d, e, f;\n    for(f = 0;f < a;) {\n      switch(d = this.readCodeByTable(b), d) {\n        case 16:\n          for(d = 3 + this.readBits(2);d--;) {\n            c[f++] = e\n          }\n          break;\n        case 17:\n          for(d = 3 + this.readBits(3);d--;) {\n            c[f++] = 0\n          }\n          e = 0;\n          break;\n        case 18:\n          for(d = 11 + this.readBits(7);d--;) {\n            c[f++] = 0\n          }\n          e = 0;\n          break;\n        default:\n          e = c[f++] = d\n      }\n    }\n    return c\n  }\n  var b = this.readBits(5) + 257, c = this.readBits(5) + 1, d = this.readBits(4) + 4, e = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflate.Order.length), f;\n  for(f = 0;f < d;++f) {\n    e[Zlib.RawInflate.Order[f]] = this.readBits(3)\n  }\n  d = (0,Zlib.Huffman.buildHuffmanTable)(e);\n  e = new (USE_TYPEDARRAY ? Uint8Array : Array)(b);\n  f = new (USE_TYPEDARRAY ? Uint8Array : Array)(c);\n  this.decodeHuffman((0,Zlib.Huffman.buildHuffmanTable)(a.call(this, b, d, e)), (0,Zlib.Huffman.buildHuffmanTable)(a.call(this, c, d, f)))\n};\nZlib.RawInflate.prototype.decodeHuffman = function(a, b) {\n  var c = this.output, d = this.op;\n  this.currentLitlenTable = a;\n  for(var e = c.length - Zlib.RawInflate.MaxCopyLength, f, g, h;256 !== (f = this.readCodeByTable(a));) {\n    if(256 > f) {\n      d >= e && (this.op = d, c = this.expandBuffer(), d = this.op), c[d++] = f\n    }else {\n      f -= 257;\n      h = Zlib.RawInflate.LengthCodeTable[f];\n      0 < Zlib.RawInflate.LengthExtraTable[f] && (h += this.readBits(Zlib.RawInflate.LengthExtraTable[f]));\n      f = this.readCodeByTable(b);\n      g = Zlib.RawInflate.DistCodeTable[f];\n      0 < Zlib.RawInflate.DistExtraTable[f] && (g += this.readBits(Zlib.RawInflate.DistExtraTable[f]));\n      d >= e && (this.op = d, c = this.expandBuffer(), d = this.op);\n      for(;h--;) {\n        c[d] = c[d++ - g]\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = d\n};\nZlib.RawInflate.prototype.decodeHuffmanAdaptive = function(a, b) {\n  var c = this.output, d = this.op;\n  this.currentLitlenTable = a;\n  for(var e = c.length, f, g, h;256 !== (f = this.readCodeByTable(a));) {\n    if(256 > f) {\n      d >= e && (c = this.expandBuffer(), e = c.length), c[d++] = f\n    }else {\n      f -= 257;\n      h = Zlib.RawInflate.LengthCodeTable[f];\n      0 < Zlib.RawInflate.LengthExtraTable[f] && (h += this.readBits(Zlib.RawInflate.LengthExtraTable[f]));\n      f = this.readCodeByTable(b);\n      g = Zlib.RawInflate.DistCodeTable[f];\n      0 < Zlib.RawInflate.DistExtraTable[f] && (g += this.readBits(Zlib.RawInflate.DistExtraTable[f]));\n      d + h > e && (c = this.expandBuffer(), e = c.length);\n      for(;h--;) {\n        c[d] = c[d++ - g]\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = d\n};\nZlib.RawInflate.prototype.expandBuffer = function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.op - Zlib.RawInflate.MaxBackwardLength), b = this.op - Zlib.RawInflate.MaxBackwardLength, c, d, e = this.output;\n  if(USE_TYPEDARRAY) {\n    a.set(e.subarray(Zlib.RawInflate.MaxBackwardLength, a.length))\n  }else {\n    c = 0;\n    for(d = a.length;c < d;++c) {\n      a[c] = e[c + Zlib.RawInflate.MaxBackwardLength]\n    }\n  }\n  this.blocks.push(a);\n  this.totalpos += a.length;\n  if(USE_TYPEDARRAY) {\n    e.set(e.subarray(b, b + Zlib.RawInflate.MaxBackwardLength))\n  }else {\n    for(c = 0;c < Zlib.RawInflate.MaxBackwardLength;++c) {\n      e[c] = e[b + c]\n    }\n  }\n  this.op = Zlib.RawInflate.MaxBackwardLength;\n  return e\n};\nZlib.RawInflate.prototype.expandBufferAdaptive = function(a) {\n  var b = this.input.length / this.ip + 1 | 0, c = this.input, d = this.output;\n  a && ("number" === typeof a.fixRatio && (b = a.fixRatio), "number" === typeof a.addRatio && (b += a.addRatio));\n  2 > b ? (a = (c.length - this.ip) / this.currentLitlenTable[2], a = 258 * (a / 2) | 0, a = a < d.length ? d.length + a : d.length << 1) : a = d.length * b;\n  USE_TYPEDARRAY ? (a = new Uint8Array(a), a.set(d)) : a = d;\n  return this.output = a\n};\nZlib.RawInflate.prototype.concatBuffer = function() {\n  var a = 0, b = this.output, c = this.blocks, d, e = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.totalpos + (this.op - Zlib.RawInflate.MaxBackwardLength)), f, g, h, i;\n  if(0 === c.length) {\n    return USE_TYPEDARRAY ? this.output.subarray(Zlib.RawInflate.MaxBackwardLength, this.op) : this.output.slice(Zlib.RawInflate.MaxBackwardLength, this.op)\n  }\n  f = 0;\n  for(g = c.length;f < g;++f) {\n    d = c[f];\n    h = 0;\n    for(i = d.length;h < i;++h) {\n      e[a++] = d[h]\n    }\n  }\n  f = Zlib.RawInflate.MaxBackwardLength;\n  for(g = this.op;f < g;++f) {\n    e[a++] = b[f]\n  }\n  this.blocks = [];\n  return this.buffer = e\n};\nZlib.RawInflate.prototype.concatBufferDynamic = function() {\n  var a, b = this.op;\n  USE_TYPEDARRAY ? this.resize ? (a = new Uint8Array(b), a.set(this.output.subarray(0, b))) : a = this.output.subarray(0, b) : (this.output.length > b && (this.output.length = b), a = this.output);\n  return this.buffer = a\n};\nZlib.Gunzip = function(a) {\n  this.input = a;\n  this.ip = 0;\n  this.member = [];\n  this.decompressed = !1\n};\nZlib.Gunzip.prototype.getMembers = function() {\n  this.decompressed || this.decompress();\n  return this.member.slice()\n};\nZlib.Gunzip.prototype.decompress = function() {\n  for(var a = this.input.length;this.ip < a;) {\n    this.decodeMember()\n  }\n  this.decompressed = !0;\n  return this.concatMember()\n};\nZlib.Gunzip.prototype.decodeMember = function() {\n  var a = new Zlib.GunzipMember, b, c, d, e, f, g = this.input;\n  c = this.ip;\n  a.id1 = g[c++];\n  a.id2 = g[c++];\n  if(31 !== a.id1 || 139 !== a.id2) {\n    throw Error("invalid file signature:" + a.id1 + "," + a.id2);\n  }\n  a.cm = g[c++];\n  switch(a.cm) {\n    case 8:\n      break;\n    default:\n      throw Error("unknown compression method: " + a.cm);\n  }\n  a.flg = g[c++];\n  b = g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24;\n  a.mtime = new Date(1E3 * b);\n  a.xfl = g[c++];\n  a.os = g[c++];\n  0 < (a.flg & Zlib.Gzip.FlagsMask.FEXTRA) && (a.xlen = g[c++] | g[c++] << 8, c = this.decodeSubField(c, a.xlen));\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FNAME)) {\n    f = [];\n    for(e = 0;0 < (b = g[c++]);) {\n      f[e++] = String.fromCharCode(b)\n    }\n    a.name = f.join("")\n  }\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FCOMMENT)) {\n    f = [];\n    for(e = 0;0 < (b = g[c++]);) {\n      f[e++] = String.fromCharCode(b)\n    }\n    a.comment = f.join("")\n  }\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FHCRC) && (a.crc16 = Zlib.CRC32.calc(g, 0, c) & 65535, a.crc16 !== (g[c++] | g[c++] << 8))) {\n    throw Error("invalid header crc16");\n  }\n  b = g[g.length - 4] | g[g.length - 3] << 8 | g[g.length - 2] << 16 | g[g.length - 1] << 24;\n  g.length - c - 4 - 4 < 512 * b && (d = b);\n  c = new Zlib.RawInflate(g, {index:c, bufferSize:d});\n  a.data = d = c.decompress();\n  c = c.ip;\n  a.crc32 = b = (g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24) >>> 0;\n  if(Zlib.CRC32.calc(d) !== b) {\n    throw Error("invalid CRC-32 checksum: 0x" + Zlib.CRC32.calc(d).toString(16) + " / 0x" + b.toString(16));\n  }\n  a.isize = b = (g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24) >>> 0;\n  if((d.length & 4294967295) !== b) {\n    throw Error("invalid input size: " + (d.length & 4294967295) + " / " + b);\n  }\n  this.member.push(a);\n  this.ip = c\n};\nZlib.Gunzip.prototype.decodeSubField = function(a, b) {\n  return a + b\n};\nZlib.Gunzip.prototype.concatMember = function() {\n  var a = this.member, b, c, d = 0, e = 0;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    e += a[b].data.length\n  }\n  if(USE_TYPEDARRAY) {\n    e = new Uint8Array(e);\n    for(b = 0;b < c;++b) {\n      e.set(a[b].data, d), d += a[b].data.length\n    }\n  }else {\n    e = [];\n    for(b = 0;b < c;++b) {\n      e[b] = a[b].data\n    }\n    e = Array.prototype.concat.apply([], e)\n  }\n  return e\n};\nvar ZLIB_STREAM_RAW_INFLATE_BUFFER_SIZE = 32768;\nZlib.RawInflateStream = function(a, b, c) {\n  this.blocks = [];\n  this.bufferSize = c ? c : ZLIB_STREAM_RAW_INFLATE_BUFFER_SIZE;\n  this.totalpos = 0;\n  this.ip = void 0 === b ? 0 : b;\n  this.bitsbuflen = this.bitsbuf = 0;\n  this.input = USE_TYPEDARRAY ? new Uint8Array(a) : a;\n  this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.bufferSize);\n  this.op = 0;\n  this.resize = this.bfinal = !1;\n  this.sp = 0;\n  this.status = Zlib.RawInflateStream.Status.INITIALIZED\n};\nZlib.RawInflateStream.BlockType = {UNCOMPRESSED:0, FIXED:1, DYNAMIC:2};\nZlib.RawInflateStream.Status = {INITIALIZED:0, BLOCK_HEADER_START:1, BLOCK_HEADER_END:2, BLOCK_BODY_START:3, BLOCK_BODY_END:4, DECODE_BLOCK_START:5, DECODE_BLOCK_END:6};\nZlib.RawInflateStream.prototype.decompress = function(a, b) {\n  var c = !1;\n  void 0 !== a && (this.input = a);\n  void 0 !== b && (this.ip = b);\n  for(;!c;) {\n    switch(this.status) {\n      case Zlib.RawInflateStream.Status.INITIALIZED:\n      ;\n      case Zlib.RawInflateStream.Status.BLOCK_HEADER_START:\n        0 > this.readBlockHeader() && (c = !0);\n        break;\n      case Zlib.RawInflateStream.Status.BLOCK_HEADER_END:\n      ;\n      case Zlib.RawInflateStream.Status.BLOCK_BODY_START:\n        switch(this.currentBlockType) {\n          case Zlib.RawInflateStream.BlockType.UNCOMPRESSED:\n            0 > this.readUncompressedBlockHeader() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.FIXED:\n            0 > this.parseFixedHuffmanBlock() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.DYNAMIC:\n            0 > this.parseDynamicHuffmanBlock() && (c = !0)\n        }\n        break;\n      case Zlib.RawInflateStream.Status.BLOCK_BODY_END:\n      ;\n      case Zlib.RawInflateStream.Status.DECODE_BLOCK_START:\n        switch(this.currentBlockType) {\n          case Zlib.RawInflateStream.BlockType.UNCOMPRESSED:\n            0 > this.parseUncompressedBlock() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.FIXED:\n          ;\n          case Zlib.RawInflateStream.BlockType.DYNAMIC:\n            0 > this.decodeHuffman() && (c = !0)\n        }\n        break;\n      case Zlib.RawInflateStream.Status.DECODE_BLOCK_END:\n        this.bfinal ? c = !0 : this.status = Zlib.RawInflateStream.Status.INITIALIZED\n    }\n  }\n  return this.concatBuffer()\n};\nZlib.RawInflateStream.MaxBackwardLength = 32768;\nZlib.RawInflateStream.MaxCopyLength = 258;\nZlib.RawInflateStream.Order = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15]);\nZlib.RawInflateStream.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 258, 258]);\nZlib.RawInflateStream.LengthExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0]);\nZlib.RawInflateStream.DistCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577]);\nZlib.RawInflateStream.DistExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13]);\nZlib.RawInflateStream.FixedLiteralLengthTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(288), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 143 >= b ? 8 : 255 >= b ? 9 : 279 >= b ? 7 : 8\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflateStream.FixedDistanceTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(30), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 5\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflateStream.prototype.readBlockHeader = function() {\n  var a;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_HEADER_START;\n  this.save_();\n  if(0 > (a = this.readBits(3))) {\n    return this.restore_(), -1\n  }\n  a & 1 && (this.bfinal = !0);\n  a >>>= 1;\n  switch(a) {\n    case 0:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.UNCOMPRESSED;\n      break;\n    case 1:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.FIXED;\n      break;\n    case 2:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.DYNAMIC;\n      break;\n    default:\n      throw Error("unknown BTYPE: " + a);\n  }\n  this.status = Zlib.RawInflateStream.Status.BLOCK_HEADER_END\n};\nZlib.RawInflateStream.prototype.readBits = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f;c < a;) {\n    f = d[e++];\n    if(void 0 === f) {\n      return-1\n    }\n    b |= f << c;\n    c += 8\n  }\n  f = b & (1 << a) - 1;\n  this.bitsbuf = b >>> a;\n  this.bitsbuflen = c - a;\n  this.ip = e;\n  return f\n};\nZlib.RawInflateStream.prototype.readCodeByTable = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f = a[0], a = a[1], g;c < a;) {\n    g = d[e++];\n    if(void 0 === g) {\n      return-1\n    }\n    b |= g << c;\n    c += 8\n  }\n  d = f[b & (1 << a) - 1];\n  f = d >>> 16;\n  this.bitsbuf = b >> f;\n  this.bitsbuflen = c - f;\n  this.ip = e;\n  return d & 65535\n};\nZlib.RawInflateStream.prototype.readUncompressedBlockHeader = function() {\n  var a, b, c, d = this.input, e = this.ip;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  b = a;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  b |= a << 8;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  c = a;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  if(b === ~(c | a << 8)) {\n    throw Error("invalid uncompressed block header: length verify");\n  }\n  this.bitsbuflen = this.bitsbuf = 0;\n  this.ip = e;\n  this.blockLength = b;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END\n};\nZlib.RawInflateStream.prototype.parseUncompressedBlock = function() {\n  var a = this.input, b = this.ip, c = this.output, d = this.op, e = this.blockLength;\n  for(this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_START;e--;) {\n    d === c.length && (c = this.expandBuffer());\n    if(void 0 === a[b]) {\n      return this.ip = b, this.op = d, this.blockLength = e + 1, -1\n    }\n    c[d++] = a[b++]\n  }\n  0 > e && (this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_END);\n  this.ip = b;\n  this.op = d;\n  return 0\n};\nZlib.RawInflateStream.prototype.parseFixedHuffmanBlock = function() {\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  this.litlenTable = Zlib.RawInflateStream.FixedLiteralLengthTable;\n  this.distTable = Zlib.RawInflateStream.FixedDistanceTable;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END;\n  return 0\n};\nZlib.RawInflateStream.prototype.save_ = function() {\n  this.ip_ = this.ip;\n  this.bitsbuflen_ = this.bitsbuflen;\n  this.bitsbuf_ = this.bitsbuf\n};\nZlib.RawInflateStream.prototype.restore_ = function() {\n  this.ip = this.ip_;\n  this.bitsbuflen = this.bitsbuflen_;\n  this.bitsbuf = this.bitsbuf_\n};\nZlib.RawInflateStream.prototype.parseDynamicHuffmanBlock = function() {\n  var a, b, c, d = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflateStream.Order.length), e, f, g, h = 0;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  this.save_();\n  a = this.readBits(5) + 257;\n  b = this.readBits(5) + 1;\n  c = this.readBits(4) + 4;\n  if(0 > a || 0 > b || 0 > c) {\n    return this.restore_(), -1\n  }\n  try {\n    for(var i = function(a, b, c) {\n      for(var d, e, f = 0, f = 0;f < a;) {\n        d = this.readCodeByTable(b);\n        if(0 > d) {\n          throw Error("not enough input");\n        }\n        switch(d) {\n          case 16:\n            if(0 > (d = this.readBits(2))) {\n              throw Error("not enough input");\n            }\n            for(d = 3 + d;d--;) {\n              c[f++] = e\n            }\n            break;\n          case 17:\n            if(0 > (d = this.readBits(3))) {\n              throw Error("not enough input");\n            }\n            for(d = 3 + d;d--;) {\n              c[f++] = 0\n            }\n            e = 0;\n            break;\n          case 18:\n            if(0 > (d = this.readBits(7))) {\n              throw Error("not enough input");\n            }\n            for(d = 11 + d;d--;) {\n              c[f++] = 0\n            }\n            e = 0;\n            break;\n          default:\n            e = c[f++] = d\n        }\n      }\n      return c\n    }, j, h = 0;h < c;++h) {\n      if(0 > (j = this.readBits(3))) {\n        throw Error("not enough input");\n      }\n      d[Zlib.RawInflateStream.Order[h]] = j\n    }\n    e = (0,Zlib.Huffman.buildHuffmanTable)(d);\n    f = new (USE_TYPEDARRAY ? Uint8Array : Array)(a);\n    g = new (USE_TYPEDARRAY ? Uint8Array : Array)(b);\n    this.litlenTable = (0,Zlib.Huffman.buildHuffmanTable)(i.call(this, a, e, f));\n    this.distTable = (0,Zlib.Huffman.buildHuffmanTable)(i.call(this, b, e, g))\n  }catch(l) {\n    return this.restore_(), -1\n  }\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END;\n  return 0\n};\nZlib.RawInflateStream.prototype.decodeHuffman = function() {\n  var a = this.output, b = this.op, c, d, e, f = this.litlenTable, g = this.distTable, h = a.length;\n  for(this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_START;;) {\n    this.save_();\n    c = this.readCodeByTable(f);\n    if(0 > c) {\n      return this.op = b, this.restore_(), -1\n    }\n    if(256 === c) {\n      break\n    }\n    if(256 > c) {\n      b === h && (a = this.expandBuffer(), h = a.length), a[b++] = c\n    }else {\n      d = c - 257;\n      e = Zlib.RawInflateStream.LengthCodeTable[d];\n      if(0 < Zlib.RawInflateStream.LengthExtraTable[d]) {\n        c = this.readBits(Zlib.RawInflateStream.LengthExtraTable[d]);\n        if(0 > c) {\n          return this.op = b, this.restore_(), -1\n        }\n        e += c\n      }\n      c = this.readCodeByTable(g);\n      if(0 > c) {\n        return this.op = b, this.restore_(), -1\n      }\n      d = Zlib.RawInflateStream.DistCodeTable[c];\n      if(0 < Zlib.RawInflateStream.DistExtraTable[c]) {\n        c = this.readBits(Zlib.RawInflateStream.DistExtraTable[c]);\n        if(0 > c) {\n          return this.op = b, this.restore_(), -1\n        }\n        d += c\n      }\n      b + e >= h && (a = this.expandBuffer(), h = a.length);\n      for(;e--;) {\n        a[b] = a[b++ - d]\n      }\n      if(this.ip === this.input.length) {\n        return this.op = b, -1\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = b;\n  this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_END\n};\nZlib.RawInflateStream.prototype.expandBuffer = function(a) {\n  var b = this.input.length / this.ip + 1 | 0, c = this.input, d = this.output;\n  a && ("number" === typeof a.fixRatio && (b = a.fixRatio), "number" === typeof a.addRatio && (b += a.addRatio));\n  2 > b ? (a = (c.length - this.ip) / this.litlenTable[2], a = 258 * (a / 2) | 0, a = a < d.length ? d.length + a : d.length << 1) : a = d.length * b;\n  USE_TYPEDARRAY ? (a = new Uint8Array(a), a.set(d)) : a = d;\n  return this.output = a\n};\nZlib.RawInflateStream.prototype.concatBuffer = function() {\n  var a, b = this.op;\n  this.resize ? USE_TYPEDARRAY ? (a = new Uint8Array(b), a.set(this.output.subarray(this.sp, b))) : a = this.output.slice(this.sp, b) : a = USE_TYPEDARRAY ? this.output.subarray(this.sp, b) : this.output.slice(this.sp, b);\n  this.buffer = a;\n  this.sp = b;\n  return this.buffer\n};\nZlib.RawInflateStream.prototype.getBytes = function() {\n  return USE_TYPEDARRAY ? this.output.subarray(0, this.op) : this.output.slice(0, this.op)\n};\nZlib.InflateStream = function(a) {\n  this.input = void 0 === a ? new (USE_TYPEDARRAY ? Uint8Array : Array) : a;\n  this.ip = 0;\n  this.rawinflate = new Zlib.RawInflateStream(this.input, this.ip);\n  this.output = this.rawinflate.output\n};\nZlib.InflateStream.prototype.decompress = function(a) {\n  if(void 0 !== a) {\n    if(USE_TYPEDARRAY) {\n      var b = new Uint8Array(this.input.length + a.length);\n      b.set(this.input, 0);\n      b.set(a, this.input.length);\n      this.input = b\n    }else {\n      this.input = this.input.concat(a)\n    }\n  }\n  if(void 0 === this.method && 0 > this.readHeader()) {\n    return new (USE_TYPEDARRAY ? Uint8Array : Array)\n  }\n  a = this.rawinflate.decompress(this.input, this.ip);\n  this.ip = this.rawinflate.ip;\n  return a\n};\nZlib.InflateStream.prototype.getBytes = function() {\n  return this.rawinflate.getBytes()\n};\nZlib.InflateStream.prototype.readHeader = function() {\n  var a = this.ip, b = this.input, c = b[a++], b = b[a++];\n  if(void 0 === c || void 0 === b) {\n    return-1\n  }\n  switch(c & 15) {\n    case Zlib.CompressionMethod.DEFLATE:\n      this.method = Zlib.CompressionMethod.DEFLATE;\n      break;\n    default:\n      throw Error("unsupported compression method");\n  }\n  if(0 !== ((c << 8) + b) % 31) {\n    throw Error("invalid fcheck flag:" + ((c << 8) + b) % 31);\n  }\n  if(b & 32) {\n    throw Error("fdict flag is not supported");\n  }\n  this.ip = a\n};\nZlib.Util = {};\nZlib.Util.stringToByteArray = function(a) {\n  var a = a.split(""), b, c;\n  b = 0;\n  for(c = a.length;b < c;b++) {\n    a[b] = (a[b].charCodeAt(0) & 255) >>> 0\n  }\n  return a\n};\nZlib.Adler32 = function(a) {\n  "string" === typeof a && (a = Zlib.Util.stringToByteArray(a));\n  return Zlib.Adler32.update(1, a)\n};\nZlib.Adler32.update = function(a, b) {\n  for(var c = a & 65535, d = a >>> 16 & 65535, e = b.length, f, g = 0;0 < e;) {\n    f = e > Zlib.Adler32.OptimizationParameter ? Zlib.Adler32.OptimizationParameter : e;\n    e -= f;\n    do {\n      c += b[g++], d += c\n    }while(--f);\n    c %= 65521;\n    d %= 65521\n  }\n  return(d << 16 | c) >>> 0\n};\nZlib.Adler32.OptimizationParameter = 1024;\nZlib.Deflate = function(a, b) {\n  this.input = a;\n  this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.Deflate.DefaultBufferSize);\n  this.compressionType = Zlib.Deflate.CompressionType.DYNAMIC;\n  var c = {}, d;\n  if((b || !(b = {})) && "number" === typeof b.compressionType) {\n    this.compressionType = b.compressionType\n  }\n  for(d in b) {\n    c[d] = b[d]\n  }\n  c.outputBuffer = this.output;\n  this.rawDeflate = new Zlib.RawDeflate(this.input, c)\n};\nZlib.Deflate.DefaultBufferSize = 32768;\nZlib.Deflate.CompressionType = Zlib.RawDeflate.CompressionType;\nZlib.Deflate.compress = function(a, b) {\n  return(new Zlib.Deflate(a, b)).compress()\n};\nZlib.Deflate.prototype.compress = function() {\n  var a, b, c, d = 0;\n  c = this.output;\n  a = Zlib.CompressionMethod.DEFLATE;\n  switch(a) {\n    case Zlib.CompressionMethod.DEFLATE:\n      b = Math.LOG2E * Math.log(Zlib.RawDeflate.WindowSize) - 8;\n      break;\n    default:\n      throw Error("invalid compression method");\n  }\n  b = b << 4 | a;\n  c[d++] = b;\n  switch(a) {\n    case Zlib.CompressionMethod.DEFLATE:\n      switch(this.compressionType) {\n        case Zlib.Deflate.CompressionType.NONE:\n          a = 0;\n          break;\n        case Zlib.Deflate.CompressionType.FIXED:\n          a = 1;\n          break;\n        case Zlib.Deflate.CompressionType.DYNAMIC:\n          a = 2;\n          break;\n        default:\n          throw Error("unsupported compression type");\n      }\n      break;\n    default:\n      throw Error("invalid compression method");\n  }\n  a = a << 6 | 0;\n  c[d++] = a | 31 - (256 * b + a) % 31;\n  b = Zlib.Adler32(this.input);\n  this.rawDeflate.op = d;\n  c = this.rawDeflate.compress();\n  d = c.length;\n  USE_TYPEDARRAY && (c = new Uint8Array(c.buffer), c.length <= d + 4 && (this.output = new Uint8Array(c.length + 4), this.output.set(c), c = this.output), c = c.subarray(0, d + 4));\n  c[d++] = b >> 24 & 255;\n  c[d++] = b >> 16 & 255;\n  c[d++] = b >> 8 & 255;\n  c[d++] = b & 255;\n  return c\n};\nZlib.Inflate = function(a, b) {\n  var c, d;\n  this.input = a;\n  this.ip = 0;\n  if(b || !(b = {})) {\n    b.index && (this.ip = b.index), b.verify && (this.verify = b.verify)\n  }\n  c = a[this.ip++];\n  d = a[this.ip++];\n  switch(c & 15) {\n    case Zlib.CompressionMethod.DEFLATE:\n      this.method = Zlib.CompressionMethod.DEFLATE;\n      break;\n    default:\n      throw Error("unsupported compression method");\n  }\n  if(0 !== ((c << 8) + d) % 31) {\n    throw Error("invalid fcheck flag:" + ((c << 8) + d) % 31);\n  }\n  if(d & 32) {\n    throw Error("fdict flag is not supported");\n  }\n  this.rawinflate = new Zlib.RawInflate(a, {index:this.ip, bufferSize:b.bufferSize, bufferType:b.bufferType, resize:b.resize})\n};\nZlib.Inflate.BufferType = Zlib.RawInflate.BufferType;\nZlib.Inflate.prototype.decompress = function() {\n  var a = this.input, b;\n  b = this.rawinflate.decompress();\n  this.ip = this.rawinflate.ip;\n  if(this.verify && (a = (a[this.ip++] << 24 | a[this.ip++] << 16 | a[this.ip++] << 8 | a[this.ip++]) >>> 0, a !== Zlib.Adler32(b))) {\n    throw Error("invalid adler-32 checksum");\n  }\n  return b\n};\ngoog.exportSymbol("Zlib.Inflate", Zlib.Inflate);\ngoog.exportSymbol("Zlib.Inflate.prototype.decompress", Zlib.Inflate.prototype.decompress);\nZlib.exportObject("Zlib.Inflate.BufferType", {ADAPTIVE:Zlib.Inflate.BufferType.ADAPTIVE, BLOCK:Zlib.Inflate.BufferType.BLOCK});\nZlib.Zip = function(a) {\n  a = a || {};\n  this.files = [];\n  this.comment = a.comment\n};\nZlib.Zip.prototype.addFile = function(a, b) {\n  var b = b || {}, c, d = a.length, e = 0;\n  USE_TYPEDARRAY && a instanceof Array && (a = new Uint8Array(a));\n  "number" !== typeof b.compressionMethod && (b.compressionMethod = Zlib.Zip.CompressionMethod.DEFLATE);\n  if(b.compress) {\n    switch(b.compressionMethod) {\n      case Zlib.Zip.CompressionMethod.STORE:\n        break;\n      case Zlib.Zip.CompressionMethod.DEFLATE:\n        e = Zlib.CRC32.calc(a);\n        a = this.deflateWithOption(a, b);\n        c = !0;\n        break;\n      default:\n        throw Error("unknown compression method:" + b.compressionMethod);\n    }\n  }\n  this.files.push({buffer:a, option:b, compressed:c, size:d, crc32:e})\n};\nZlib.Zip.CompressionMethod = {STORE:0, DEFLATE:8};\nZlib.Zip.OperatingSystem = {MSDOS:0, UNIX:3, MACINTOSH:7};\nZlib.Zip.prototype.compress = function() {\n  var a = this.files, b, c, d, e, f, g = 0, h = 0, i, j, l, m, k, n;\n  k = 0;\n  for(n = a.length;k < n;++k) {\n    b = a[k];\n    l = b.option.filename ? b.option.filename.length : 0;\n    m = b.option.comment ? b.option.comment.length : 0;\n    if(!b.compressed) {\n      switch(b.crc32 = Zlib.CRC32.calc(b.buffer), b.option.compressionMethod) {\n        case Zlib.Zip.CompressionMethod.STORE:\n          break;\n        case Zlib.Zip.CompressionMethod.DEFLATE:\n          b.buffer = this.deflateWithOption(b.buffer, b.option);\n          b.compressed = !0;\n          break;\n        default:\n          throw Error("unknown compression method:" + b.option.compressionMethod);\n      }\n    }\n    g += 30 + l + b.buffer.length;\n    h += 46 + l + m\n  }\n  c = new (USE_TYPEDARRAY ? Uint8Array : Array)(g + h + (46 + (this.comment ? this.comment.length : 0)));\n  d = 0;\n  e = g;\n  f = e + h;\n  k = 0;\n  for(n = a.length;k < n;++k) {\n    b = a[k];\n    l = b.option.filename ? b.option.filename.length : 0;\n    m = b.option.comment ? b.option.comment.length : 0;\n    i = d;\n    c[d++] = c[e++] = 80;\n    c[d++] = c[e++] = 75;\n    c[d++] = 3;\n    c[d++] = 4;\n    c[e++] = 1;\n    c[e++] = 2;\n    c[e++] = 20;\n    c[e++] = b.option.os || Zlib.Zip.OperatingSystem.MSDOS;\n    c[d++] = c[e++] = 20;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    j = b.option.compressionMethod;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    j = b.option.date || new Date;\n    c[d++] = c[e++] = (j.getMinutes() & 7) << 5 | j.getSeconds() / 2 | 0;\n    c[d++] = c[e++] = j.getHours() << 3 | j.getMinutes() >> 3;\n    c[d++] = c[e++] = (j.getMonth() + 1 & 7) << 5 | j.getDate();\n    c[d++] = c[e++] = (j.getFullYear() - 1980 & 127) << 1 | j.getMonth() + 1 >> 3;\n    j = b.crc32;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    j = b.buffer.length;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    j = b.size;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    c[d++] = c[e++] = l & 255;\n    c[d++] = c[e++] = l >> 8 & 255;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    c[e++] = m & 255;\n    c[e++] = m >> 8 & 255;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = i & 255;\n    c[e++] = i >> 8 & 255;\n    c[e++] = i >> 16 & 255;\n    c[e++] = i >> 24 & 255;\n    if(j = b.option.filename) {\n      if(USE_TYPEDARRAY) {\n        c.set(j, d), c.set(j, e), d += l, e += l\n      }else {\n        for(i = 0;i < l;++i) {\n          c[d++] = c[e++] = j[i]\n        }\n      }\n    }\n    if(l = b.option.extraField) {\n      if(USE_TYPEDARRAY) {\n        c.set(l, d), c.set(l, e), d += 0, e += 0\n      }else {\n        for(i = 0;i < m;++i) {\n          c[d++] = c[e++] = l[i]\n        }\n      }\n    }\n    if(l = b.option.comment) {\n      if(USE_TYPEDARRAY) {\n        c.set(l, e), e += m\n      }else {\n        for(i = 0;i < m;++i) {\n          c[e++] = l[i]\n        }\n      }\n    }\n    if(USE_TYPEDARRAY) {\n      c.set(b.buffer, d), d += b.buffer.length\n    }else {\n      i = 0;\n      for(m = b.buffer.length;i < m;++i) {\n        c[d++] = b.buffer[i]\n      }\n    }\n  }\n  c[f++] = 80;\n  c[f++] = 75;\n  c[f++] = 5;\n  c[f++] = 6;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = n & 255;\n  c[f++] = n >> 8 & 255;\n  c[f++] = n & 255;\n  c[f++] = n >> 8 & 255;\n  c[f++] = h & 255;\n  c[f++] = h >> 8 & 255;\n  c[f++] = h >> 16 & 255;\n  c[f++] = h >> 24 & 255;\n  c[f++] = g & 255;\n  c[f++] = g >> 8 & 255;\n  c[f++] = g >> 16 & 255;\n  c[f++] = g >> 24 & 255;\n  m = this.comment ? this.comment.length : 0;\n  c[f++] = m & 255;\n  c[f++] = m >> 8 & 255;\n  if(this.comment) {\n    if(USE_TYPEDARRAY) {\n      c.set(this.comment, f)\n    }else {\n      for(i = 0;i < m;++i) {\n        c[f++] = this.comment[i]\n      }\n    }\n  }\n  return c\n};\nZlib.Zip.prototype.deflateWithOption = function(a, b) {\n  return(new Zlib.RawDeflate(a, b.deflateOption)).compress()\n};\nZlib.Unzip = function(a, b) {\n  b = b || {};\n  this.input = USE_TYPEDARRAY && a instanceof Array ? new Uint8Array(a) : a;\n  this.ip = 0;\n  this.verify = b.verify || !1\n};\nZlib.Unzip.CompressionMethod = Zlib.Zip.CompressionMethod;\nZlib.Unzip.FileHeader = function(a, b) {\n  this.input = a;\n  this.offset = b\n};\nZlib.Unzip.FileHeader.prototype.parse = function() {\n  var a = this.input, b = this.offset;\n  if(80 !== a[b++] || 75 !== a[b++] || 1 !== a[b++] || 2 !== a[b++]) {\n    throw Error("invalid file header signature");\n  }\n  this.version = a[b++];\n  this.os = a[b++];\n  this.needVersion = a[b++] | a[b++] << 8;\n  this.flags = a[b++] | a[b++] << 8;\n  this.compression = a[b++] | a[b++] << 8;\n  this.time = a[b++] | a[b++] << 8;\n  this.date = a[b++] | a[b++] << 8;\n  this.crc32 = (a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24) >>> 0;\n  this.compressedSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.plainSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.fileNameLength = a[b++] | a[b++] << 8;\n  this.extraFieldLength = a[b++] | a[b++] << 8;\n  this.fileCommentLength = a[b++] | a[b++] << 8;\n  this.diskNumberStart = a[b++] | a[b++] << 8;\n  this.internalFileAttributes = a[b++] | a[b++] << 8;\n  this.externalFileAttributes = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.relativeOffset = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.filename = String.fromCharCode.apply(null, USE_TYPEDARRAY ? a.subarray(b, b += this.fileNameLength) : a.slice(b, b += this.fileNameLength));\n  this.extraField = USE_TYPEDARRAY ? a.subarray(b, b += this.extraFieldLength) : a.slice(b, b += this.extraFieldLength);\n  this.comment = USE_TYPEDARRAY ? a.subarray(b, b + this.fileCommentLength) : a.slice(b, b + this.fileCommentLength);\n  this.length = b - this.offset\n};\nZlib.Unzip.LocalFileHeader = function(a, b) {\n  this.input = a;\n  this.offset = b\n};\nZlib.Unzip.LocalFileHeader.prototype.parse = function() {\n  var a = this.input, b = this.offset;\n  if(80 !== a[b++] || 75 !== a[b++] || 3 !== a[b++] || 4 !== a[b++]) {\n    throw Error("invalid local file header signature");\n  }\n  this.needVersion = a[b++] | a[b++] << 8;\n  this.flags = a[b++] | a[b++] << 8;\n  this.compression = a[b++] | a[b++] << 8;\n  this.time = a[b++] | a[b++] << 8;\n  this.date = a[b++] | a[b++] << 8;\n  this.crc32 = (a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24) >>> 0;\n  this.compressedSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.plainSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.fileNameLength = a[b++] | a[b++] << 8;\n  this.extraFieldLength = a[b++] | a[b++] << 8;\n  this.filename = String.fromCharCode.apply(null, USE_TYPEDARRAY ? a.subarray(b, b += this.fileNameLength) : a.slice(b, b += this.fileNameLength));\n  this.extraField = USE_TYPEDARRAY ? a.subarray(b, b += this.extraFieldLength) : a.slice(b, b += this.extraFieldLength);\n  this.length = b - this.offset\n};\nZlib.Unzip.prototype.searchEndOfCentralDirectoryRecord = function() {\n  var a = this.input, b;\n  for(b = a.length - 12;0 < b;--b) {\n    if(80 === a[b] && 75 === a[b + 1] && 5 === a[b + 2] && 6 === a[b + 3]) {\n      this.eocdrOffset = b;\n      return\n    }\n  }\n  throw Error("End of Central Directory Record not found");\n};\nZlib.Unzip.prototype.parseEndOfCentralDirectoryRecord = function() {\n  var a = this.input, b;\n  this.eocdrOffset || this.searchEndOfCentralDirectoryRecord();\n  b = this.eocdrOffset;\n  if(80 !== a[b++] || 75 !== a[b++] || 5 !== a[b++] || 6 !== a[b++]) {\n    throw Error("invalid signature");\n  }\n  this.numberOfThisDisk = a[b++] | a[b++] << 8;\n  this.startDisk = a[b++] | a[b++] << 8;\n  this.totalEntriesThisDisk = a[b++] | a[b++] << 8;\n  this.totalEntries = a[b++] | a[b++] << 8;\n  this.centralDirectorySize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.centralDirectoryOffset = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.commentLength = a[b++] | a[b++] << 8;\n  this.comment = USE_TYPEDARRAY ? a.subarray(b, b + this.commentLength) : a.slice(b, b + this.commentLength)\n};\nZlib.Unzip.prototype.parseFileHeader = function() {\n  var a = [], b = {}, c, d, e, f;\n  if(!this.fileHeaderList) {\n    void 0 === this.centralDirectoryOffset && this.parseEndOfCentralDirectoryRecord();\n    c = this.centralDirectoryOffset;\n    e = 0;\n    for(f = this.totalEntries;e < f;++e) {\n      d = new Zlib.Unzip.FileHeader(this.input, c), d.parse(), c += d.length, a[e] = d, b[d.filename] = e\n    }\n    if(this.centralDirectorySize < c - this.centralDirectoryOffset) {\n      throw Error("invalid file header size");\n    }\n    this.fileHeaderList = a;\n    this.filenameToIndex = b\n  }\n};\nZlib.Unzip.prototype.getFileData = function(a) {\n  var b = this.fileHeaderList, c;\n  b || this.parseFileHeader();\n  if(void 0 === b[a]) {\n    throw Error("wrong index");\n  }\n  b = b[a].relativeOffset;\n  a = new Zlib.Unzip.LocalFileHeader(this.input, b);\n  a.parse();\n  b += a.length;\n  c = a.compressedSize;\n  switch(a.compression) {\n    case Zlib.Unzip.CompressionMethod.STORE:\n      b = USE_TYPEDARRAY ? this.input.subarray(b, b + c) : this.input.slice(b, b + c);\n      break;\n    case Zlib.Unzip.CompressionMethod.DEFLATE:\n      b = (new Zlib.RawInflate(this.input, {index:b, bufferSize:a.plainSize})).decompress();\n      break;\n    default:\n      throw Error("unknown compression type");\n  }\n  if(this.verify && (c = Zlib.CRC32.calc(b), a.crc32 !== c)) {\n    throw Error("wrong crc: file=0x" + a.crc32.toString(16) + ", data=0x" + c.toString(16));\n  }\n  return b\n};\nZlib.Unzip.prototype.getFilenames = function() {\n  var a = [], b, c, d;\n  this.fileHeaderList || this.parseFileHeader();\n  d = this.fileHeaderList;\n  b = 0;\n  for(c = d.length;b < c;++b) {\n    a[b] = d[b].filename\n  }\n  return a\n};\nZlib.Unzip.prototype.decompress = function(a) {\n  var b;\n  this.filenameToIndex || this.parseFileHeader();\n  b = this.filenameToIndex[a];\n  if(void 0 === b) {\n    throw Error(a + " not found");\n  }\n  return this.getFileData(b)\n};\nZlib.CompressionMethod = {DEFLATE:8, RESERVED:15};\n}).call(this);\n\ndefine("thirdparty/inflate.min", function(){});\n\n/** @license zlib.js 2012 - imaya [ https://github.com/imaya/zlib.js ] The MIT License */\n(function() {var COMPILED = !0, goog = goog || {};\ngoog.global = this;\ngoog.DEBUG = !1;\ngoog.LOCALE = "en";\ngoog.provide = function(a) {\n  if(!COMPILED) {\n    if(goog.isProvided_(a)) {\n      throw Error(\'Namespace "\' + a + \'" already declared.\');\n    }\n    delete goog.implicitNamespaces_[a];\n    for(var b = a;(b = b.substring(0, b.lastIndexOf("."))) && !goog.getObjectByName(b);) {\n      goog.implicitNamespaces_[b] = !0\n    }\n  }\n  goog.exportPath_(a)\n};\ngoog.setTestOnly = function(a) {\n  if(COMPILED && !goog.DEBUG) {\n    throw a = a || "", Error("Importing test-only code into non-debug environment" + a ? ": " + a : ".");\n  }\n};\nCOMPILED || (goog.isProvided_ = function(a) {\n  return!goog.implicitNamespaces_[a] && !!goog.getObjectByName(a)\n}, goog.implicitNamespaces_ = {});\ngoog.exportPath_ = function(a, b, c) {\n  a = a.split(".");\n  c = c || goog.global;\n  !(a[0] in c) && c.execScript && c.execScript("var " + a[0]);\n  for(var d;a.length && (d = a.shift());) {\n    !a.length && goog.isDef(b) ? c[d] = b : c = c[d] ? c[d] : c[d] = {}\n  }\n};\ngoog.getObjectByName = function(a, b) {\n  for(var c = a.split("."), d = b || goog.global, e;e = c.shift();) {\n    if(goog.isDefAndNotNull(d[e])) {\n      d = d[e]\n    }else {\n      return null\n    }\n  }\n  return d\n};\ngoog.globalize = function(a, b) {\n  var c = b || goog.global, d;\n  for(d in a) {\n    c[d] = a[d]\n  }\n};\ngoog.addDependency = function(a, b, c) {\n  if(!COMPILED) {\n    for(var d, a = a.replace(/\\\\/g, "/"), e = goog.dependencies_, f = 0;d = b[f];f++) {\n      e.nameToPath[d] = a, a in e.pathToNames || (e.pathToNames[a] = {}), e.pathToNames[a][d] = !0\n    }\n    for(d = 0;b = c[d];d++) {\n      a in e.requires || (e.requires[a] = {}), e.requires[a][b] = !0\n    }\n  }\n};\ngoog.ENABLE_DEBUG_LOADER = !0;\ngoog.require = function(a) {\n  if(!COMPILED && !goog.isProvided_(a)) {\n    if(goog.ENABLE_DEBUG_LOADER) {\n      var b = goog.getPathFromDeps_(a);\n      if(b) {\n        goog.included_[b] = !0;\n        goog.writeScripts_();\n        return\n      }\n    }\n    a = "goog.require could not find: " + a;\n    goog.global.console && goog.global.console.error(a);\n    throw Error(a);\n  }\n};\ngoog.basePath = "";\ngoog.nullFunction = function() {\n};\ngoog.identityFunction = function(a) {\n  return a\n};\ngoog.abstractMethod = function() {\n  throw Error("unimplemented abstract method");\n};\ngoog.addSingletonGetter = function(a) {\n  a.getInstance = function() {\n    if(a.instance_) {\n      return a.instance_\n    }\n    goog.DEBUG && (goog.instantiatedSingletons_[goog.instantiatedSingletons_.length] = a);\n    return a.instance_ = new a\n  }\n};\ngoog.instantiatedSingletons_ = [];\n!COMPILED && goog.ENABLE_DEBUG_LOADER && (goog.included_ = {}, goog.dependencies_ = {pathToNames:{}, nameToPath:{}, requires:{}, visited:{}, written:{}}, goog.inHtmlDocument_ = function() {\n  var a = goog.global.document;\n  return"undefined" != typeof a && "write" in a\n}, goog.findBasePath_ = function() {\n  if(goog.global.CLOSURE_BASE_PATH) {\n    goog.basePath = goog.global.CLOSURE_BASE_PATH\n  }else {\n    if(goog.inHtmlDocument_()) {\n      for(var a = goog.global.document.getElementsByTagName("script"), b = a.length - 1;0 <= b;--b) {\n        var c = a[b].src, d = c.lastIndexOf("?"), d = -1 == d ? c.length : d;\n        if("base.js" == c.substr(d - 7, 7)) {\n          goog.basePath = c.substr(0, d - 7);\n          break\n        }\n      }\n    }\n  }\n}, goog.importScript_ = function(a) {\n  var b = goog.global.CLOSURE_IMPORT_SCRIPT || goog.writeScriptTag_;\n  !goog.dependencies_.written[a] && b(a) && (goog.dependencies_.written[a] = !0)\n}, goog.writeScriptTag_ = function(a) {\n  return goog.inHtmlDocument_() ? (goog.global.document.write(\'<script type="text/javascript" src="\' + a + \'"><\\/script>\'), !0) : !1\n}, goog.writeScripts_ = function() {\n  function a(e) {\n    if(!(e in d.written)) {\n      if(!(e in d.visited) && (d.visited[e] = !0, e in d.requires)) {\n        for(var g in d.requires[e]) {\n          if(!goog.isProvided_(g)) {\n            if(g in d.nameToPath) {\n              a(d.nameToPath[g])\n            }else {\n              throw Error("Undefined nameToPath for " + g);\n            }\n          }\n        }\n      }\n      e in c || (c[e] = !0, b.push(e))\n    }\n  }\n  var b = [], c = {}, d = goog.dependencies_, e;\n  for(e in goog.included_) {\n    d.written[e] || a(e)\n  }\n  for(e = 0;e < b.length;e++) {\n    if(b[e]) {\n      goog.importScript_(goog.basePath + b[e])\n    }else {\n      throw Error("Undefined script input");\n    }\n  }\n}, goog.getPathFromDeps_ = function(a) {\n  return a in goog.dependencies_.nameToPath ? goog.dependencies_.nameToPath[a] : null\n}, goog.findBasePath_(), goog.global.CLOSURE_NO_DEPS || goog.importScript_(goog.basePath + "deps.js"));\ngoog.typeOf = function(a) {\n  var b = typeof a;\n  if("object" == b) {\n    if(a) {\n      if(a instanceof Array) {\n        return"array"\n      }\n      if(a instanceof Object) {\n        return b\n      }\n      var c = Object.prototype.toString.call(a);\n      if("[object Window]" == c) {\n        return"object"\n      }\n      if("[object Array]" == c || "number" == typeof a.length && "undefined" != typeof a.splice && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("splice")) {\n        return"array"\n      }\n      if("[object Function]" == c || "undefined" != typeof a.call && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("call")) {\n        return"function"\n      }\n    }else {\n      return"null"\n    }\n  }else {\n    if("function" == b && "undefined" == typeof a.call) {\n      return"object"\n    }\n  }\n  return b\n};\ngoog.isDef = function(a) {\n  return void 0 !== a\n};\ngoog.isNull = function(a) {\n  return null === a\n};\ngoog.isDefAndNotNull = function(a) {\n  return null != a\n};\ngoog.isArray = function(a) {\n  return"array" == goog.typeOf(a)\n};\ngoog.isArrayLike = function(a) {\n  var b = goog.typeOf(a);\n  return"array" == b || "object" == b && "number" == typeof a.length\n};\ngoog.isDateLike = function(a) {\n  return goog.isObject(a) && "function" == typeof a.getFullYear\n};\ngoog.isString = function(a) {\n  return"string" == typeof a\n};\ngoog.isBoolean = function(a) {\n  return"boolean" == typeof a\n};\ngoog.isNumber = function(a) {\n  return"number" == typeof a\n};\ngoog.isFunction = function(a) {\n  return"function" == goog.typeOf(a)\n};\ngoog.isObject = function(a) {\n  var b = typeof a;\n  return"object" == b && null != a || "function" == b\n};\ngoog.getUid = function(a) {\n  return a[goog.UID_PROPERTY_] || (a[goog.UID_PROPERTY_] = ++goog.uidCounter_)\n};\ngoog.removeUid = function(a) {\n  "removeAttribute" in a && a.removeAttribute(goog.UID_PROPERTY_);\n  try {\n    delete a[goog.UID_PROPERTY_]\n  }catch(b) {\n  }\n};\ngoog.UID_PROPERTY_ = "closure_uid_" + Math.floor(2147483648 * Math.random()).toString(36);\ngoog.uidCounter_ = 0;\ngoog.getHashCode = goog.getUid;\ngoog.removeHashCode = goog.removeUid;\ngoog.cloneObject = function(a) {\n  var b = goog.typeOf(a);\n  if("object" == b || "array" == b) {\n    if(a.clone) {\n      return a.clone()\n    }\n    var b = "array" == b ? [] : {}, c;\n    for(c in a) {\n      b[c] = goog.cloneObject(a[c])\n    }\n    return b\n  }\n  return a\n};\ngoog.bindNative_ = function(a, b, c) {\n  return a.call.apply(a.bind, arguments)\n};\ngoog.bindJs_ = function(a, b, c) {\n  if(!a) {\n    throw Error();\n  }\n  if(2 < arguments.length) {\n    var d = Array.prototype.slice.call(arguments, 2);\n    return function() {\n      var c = Array.prototype.slice.call(arguments);\n      Array.prototype.unshift.apply(c, d);\n      return a.apply(b, c)\n    }\n  }\n  return function() {\n    return a.apply(b, arguments)\n  }\n};\ngoog.bind = function(a, b, c) {\n  goog.bind = Function.prototype.bind && -1 != Function.prototype.bind.toString().indexOf("native code") ? goog.bindNative_ : goog.bindJs_;\n  return goog.bind.apply(null, arguments)\n};\ngoog.partial = function(a, b) {\n  var c = Array.prototype.slice.call(arguments, 1);\n  return function() {\n    var b = Array.prototype.slice.call(arguments);\n    b.unshift.apply(b, c);\n    return a.apply(this, b)\n  }\n};\ngoog.mixin = function(a, b) {\n  for(var c in b) {\n    a[c] = b[c]\n  }\n};\ngoog.now = Date.now || function() {\n  return+new Date\n};\ngoog.globalEval = function(a) {\n  if(goog.global.execScript) {\n    goog.global.execScript(a, "JavaScript")\n  }else {\n    if(goog.global.eval) {\n      if(null == goog.evalWorksForGlobals_ && (goog.global.eval("var _et_ = 1;"), "undefined" != typeof goog.global._et_ ? (delete goog.global._et_, goog.evalWorksForGlobals_ = !0) : goog.evalWorksForGlobals_ = !1), goog.evalWorksForGlobals_) {\n        goog.global.eval(a)\n      }else {\n        var b = goog.global.document, c = b.createElement("script");\n        c.type = "text/javascript";\n        c.defer = !1;\n        c.appendChild(b.createTextNode(a));\n        b.body.appendChild(c);\n        b.body.removeChild(c)\n      }\n    }else {\n      throw Error("goog.globalEval not available");\n    }\n  }\n};\ngoog.evalWorksForGlobals_ = null;\ngoog.getCssName = function(a, b) {\n  var c = function(a) {\n    return goog.cssNameMapping_[a] || a\n  }, d = function(a) {\n    for(var a = a.split("-"), b = [], d = 0;d < a.length;d++) {\n      b.push(c(a[d]))\n    }\n    return b.join("-")\n  }, d = goog.cssNameMapping_ ? "BY_WHOLE" == goog.cssNameMappingStyle_ ? c : d : function(a) {\n    return a\n  };\n  return b ? a + "-" + d(b) : d(a)\n};\ngoog.setCssNameMapping = function(a, b) {\n  goog.cssNameMapping_ = a;\n  goog.cssNameMappingStyle_ = b\n};\n!COMPILED && goog.global.CLOSURE_CSS_NAME_MAPPING && (goog.cssNameMapping_ = goog.global.CLOSURE_CSS_NAME_MAPPING);\ngoog.getMsg = function(a, b) {\n  var c = b || {}, d;\n  for(d in c) {\n    var e = ("" + c[d]).replace(/\\$/g, "$$$$"), a = a.replace(RegExp("\\\\{\\\\$" + d + "\\\\}", "gi"), e)\n  }\n  return a\n};\ngoog.exportSymbol = function(a, b, c) {\n  goog.exportPath_(a, b, c)\n};\ngoog.exportProperty = function(a, b, c) {\n  a[b] = c\n};\ngoog.inherits = function(a, b) {\n  function c() {\n  }\n  c.prototype = b.prototype;\n  a.superClass_ = b.prototype;\n  a.prototype = new c;\n  a.prototype.constructor = a\n};\ngoog.base = function(a, b, c) {\n  var d = arguments.callee.caller;\n  if(d.superClass_) {\n    return d.superClass_.constructor.apply(a, Array.prototype.slice.call(arguments, 1))\n  }\n  for(var e = Array.prototype.slice.call(arguments, 2), f = !1, g = a.constructor;g;g = g.superClass_ && g.superClass_.constructor) {\n    if(g.prototype[b] === d) {\n      f = !0\n    }else {\n      if(f) {\n        return g.prototype[b].apply(a, e)\n      }\n    }\n  }\n  if(a[b] === d) {\n    return a.constructor.prototype[b].apply(a, e)\n  }\n  throw Error("goog.base called from a method of one name to a method of a different name");\n};\ngoog.scope = function(a) {\n  a.call(goog.global)\n};\nvar USE_TYPEDARRAY = "undefined" !== typeof Uint8Array && "undefined" !== typeof Uint16Array && "undefined" !== typeof Uint32Array;\nvar Zlib = {BitStream:function(a, b) {\n  this.index = "number" === typeof b ? b : 0;\n  this.bitindex = 0;\n  this.buffer = a instanceof (USE_TYPEDARRAY ? Uint8Array : Array) ? a : new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.BitStream.DefaultBlockSize);\n  if(2 * this.buffer.length <= this.index) {\n    throw Error("invalid index");\n  }\n  this.buffer.length <= this.index && this.expandBuffer()\n}};\nZlib.BitStream.DefaultBlockSize = 32768;\nZlib.BitStream.prototype.expandBuffer = function() {\n  var a = this.buffer, b, c = a.length, d = new (USE_TYPEDARRAY ? Uint8Array : Array)(c << 1);\n  if(USE_TYPEDARRAY) {\n    d.set(a)\n  }else {\n    for(b = 0;b < c;++b) {\n      d[b] = a[b]\n    }\n  }\n  return this.buffer = d\n};\nZlib.BitStream.prototype.writeBits = function(a, b, c) {\n  var d = this.buffer, e = this.index, f = this.bitindex, g = d[e];\n  c && 1 < b && (a = 8 < b ? (Zlib.BitStream.ReverseTable[a & 255] << 24 | Zlib.BitStream.ReverseTable[a >>> 8 & 255] << 16 | Zlib.BitStream.ReverseTable[a >>> 16 & 255] << 8 | Zlib.BitStream.ReverseTable[a >>> 24 & 255]) >> 32 - b : Zlib.BitStream.ReverseTable[a] >> 8 - b);\n  if(8 > b + f) {\n    g = g << b | a, f += b\n  }else {\n    for(c = 0;c < b;++c) {\n      g = g << 1 | a >> b - c - 1 & 1, 8 === ++f && (f = 0, d[e++] = Zlib.BitStream.ReverseTable[g], g = 0, e === d.length && (d = this.expandBuffer()))\n    }\n  }\n  d[e] = g;\n  this.buffer = d;\n  this.bitindex = f;\n  this.index = e\n};\nZlib.BitStream.prototype.finish = function() {\n  var a = this.buffer, b = this.index;\n  0 < this.bitindex && (a[b] <<= 8 - this.bitindex, a[b] = Zlib.BitStream.ReverseTable[a[b]], b++);\n  USE_TYPEDARRAY ? a = a.subarray(0, b) : a.length = b;\n  return a\n};\nZlib.BitStream.ReverseTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(256), b;\n  for(b = 0;256 > b;++b) {\n    for(var c = a, d = b, e = b, f = e, g = 7, e = e >>> 1;e;e >>>= 1) {\n      f <<= 1, f |= e & 1, --g\n    }\n    c[d] = (f << g & 255) >>> 0\n  }\n  return a\n}());\nZlib.CRC32 = {};\nZlib.CRC32.calc = function(a, b, c) {\n  return Zlib.CRC32.update(a, 0, b, c)\n};\nZlib.CRC32.update = function(a, b, c, d) {\n  for(var e = Zlib.CRC32.Table, f = "number" === typeof c ? c : c = 0, d = "number" === typeof d ? d : a.length, b = b ^ 4294967295, f = d & 7;f--;++c) {\n    b = b >>> 8 ^ e[(b ^ a[c]) & 255]\n  }\n  for(f = d >> 3;f--;c += 8) {\n    b = b >>> 8 ^ e[(b ^ a[c]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 1]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 2]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 3]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 4]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 5]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 6]) & 255], b = b >>> 8 ^ e[(b ^ a[c + 7]) & 255]\n  }\n  return(b ^ 4294967295) >>> 0\n};\nZlib.CRC32.Table = function(a) {\n  return USE_TYPEDARRAY ? new Uint32Array(a) : a\n}([0, 1996959894, 3993919788, 2567524794, 124634137, 1886057615, 3915621685, 2657392035, 249268274, 2044508324, 3772115230, 2547177864, 162941995, 2125561021, 3887607047, 2428444049, 498536548, 1789927666, 4089016648, 2227061214, 450548861, 1843258603, 4107580753, 2211677639, 325883990, 1684777152, 4251122042, 2321926636, 335633487, 1661365465, 4195302755, 2366115317, 997073096, 1281953886, 3579855332, 2724688242, 1006888145, 1258607687, 3524101629, 2768942443, 901097722, 1119000684, 3686517206, \n2898065728, 853044451, 1172266101, 3705015759, 2882616665, 651767980, 1373503546, 3369554304, 3218104598, 565507253, 1454621731, 3485111705, 3099436303, 671266974, 1594198024, 3322730930, 2970347812, 795835527, 1483230225, 3244367275, 3060149565, 1994146192, 31158534, 2563907772, 4023717930, 1907459465, 112637215, 2680153253, 3904427059, 2013776290, 251722036, 2517215374, 3775830040, 2137656763, 141376813, 2439277719, 3865271297, 1802195444, 476864866, 2238001368, 4066508878, 1812370925, 453092731, \n2181625025, 4111451223, 1706088902, 314042704, 2344532202, 4240017532, 1658658271, 366619977, 2362670323, 4224994405, 1303535960, 984961486, 2747007092, 3569037538, 1256170817, 1037604311, 2765210733, 3554079995, 1131014506, 879679996, 2909243462, 3663771856, 1141124467, 855842277, 2852801631, 3708648649, 1342533948, 654459306, 3188396048, 3373015174, 1466479909, 544179635, 3110523913, 3462522015, 1591671054, 702138776, 2966460450, 3352799412, 1504918807, 783551873, 3082640443, 3233442989, 3988292384, \n2596254646, 62317068, 1957810842, 3939845945, 2647816111, 81470997, 1943803523, 3814918930, 2489596804, 225274430, 2053790376, 3826175755, 2466906013, 167816743, 2097651377, 4027552580, 2265490386, 503444072, 1762050814, 4150417245, 2154129355, 426522225, 1852507879, 4275313526, 2312317920, 282753626, 1742555852, 4189708143, 2394877945, 397917763, 1622183637, 3604390888, 2714866558, 953729732, 1340076626, 3518719985, 2797360999, 1068828381, 1219638859, 3624741850, 2936675148, 906185462, 1090812512, \n3747672003, 2825379669, 829329135, 1181335161, 3412177804, 3160834842, 628085408, 1382605366, 3423369109, 3138078467, 570562233, 1426400815, 3317316542, 2998733608, 733239954, 1555261956, 3268935591, 3050360625, 752459403, 1541320221, 2607071920, 3965973030, 1969922972, 40735498, 2617837225, 3943577151, 1913087877, 83908371, 2512341634, 3803740692, 2075208622, 213261112, 2463272603, 3855990285, 2094854071, 198958881, 2262029012, 4057260610, 1759359992, 534414190, 2176718541, 4139329115, 1873836001, \n414664567, 2282248934, 4279200368, 1711684554, 285281116, 2405801727, 4167216745, 1634467795, 376229701, 2685067896, 3608007406, 1308918612, 956543938, 2808555105, 3495958263, 1231636301, 1047427035, 2932959818, 3654703836, 1088359270, 936918E3, 2847714899, 3736837829, 1202900863, 817233897, 3183342108, 3401237130, 1404277552, 615818150, 3134207493, 3453421203, 1423857449, 601450431, 3009837614, 3294710456, 1567103746, 711928724, 3020668471, 3272380065, 1510334235, 755167117]);\nZlib.exportObject = function(a, b) {\n  var c, d, e, f;\n  if(Object.keys) {\n    c = Object.keys(b)\n  }else {\n    for(d in c = [], e = 0, b) {\n      c[e++] = d\n    }\n  }\n  e = 0;\n  for(f = c.length;e < f;++e) {\n    d = c[e], goog.exportSymbol(a + "." + d, b[d])\n  }\n};\nZlib.GunzipMember = function() {\n};\nZlib.GunzipMember.prototype.getName = function() {\n  return this.name\n};\nZlib.GunzipMember.prototype.getData = function() {\n  return this.data\n};\nZlib.GunzipMember.prototype.getMtime = function() {\n  return this.mtime\n};\nZlib.Heap = function(a) {\n  this.buffer = new (USE_TYPEDARRAY ? Uint16Array : Array)(2 * a);\n  this.length = 0\n};\nZlib.Heap.prototype.getParent = function(a) {\n  return 2 * ((a - 2) / 4 | 0)\n};\nZlib.Heap.prototype.getChild = function(a) {\n  return 2 * a + 2\n};\nZlib.Heap.prototype.push = function(a, b) {\n  var c, d, e = this.buffer, f;\n  c = this.length;\n  e[this.length++] = b;\n  for(e[this.length++] = a;0 < c;) {\n    if(d = this.getParent(c), e[c] > e[d]) {\n      f = e[c], e[c] = e[d], e[d] = f, f = e[c + 1], e[c + 1] = e[d + 1], e[d + 1] = f, c = d\n    }else {\n      break\n    }\n  }\n  return this.length\n};\nZlib.Heap.prototype.pop = function() {\n  var a, b, c = this.buffer, d, e, f;\n  b = c[0];\n  a = c[1];\n  this.length -= 2;\n  c[0] = c[this.length];\n  c[1] = c[this.length + 1];\n  for(f = 0;;) {\n    e = this.getChild(f);\n    if(e >= this.length) {\n      break\n    }\n    e + 2 < this.length && c[e + 2] > c[e] && (e += 2);\n    if(c[e] > c[f]) {\n      d = c[f], c[f] = c[e], c[e] = d, d = c[f + 1], c[f + 1] = c[e + 1], c[e + 1] = d\n    }else {\n      break\n    }\n    f = e\n  }\n  return{index:a, value:b, length:this.length}\n};\nZlib.Huffman = {};\nZlib.Huffman.buildHuffmanTable = function(a) {\n  var b = a.length, c = 0, d = Number.POSITIVE_INFINITY, e, f, g, h, i, j, l, m, k;\n  for(m = 0;m < b;++m) {\n    a[m] > c && (c = a[m]), a[m] < d && (d = a[m])\n  }\n  e = 1 << c;\n  f = new (USE_TYPEDARRAY ? Uint32Array : Array)(e);\n  g = 1;\n  h = 0;\n  for(i = 2;g <= c;) {\n    for(m = 0;m < b;++m) {\n      if(a[m] === g) {\n        j = 0;\n        l = h;\n        for(k = 0;k < g;++k) {\n          j = j << 1 | l & 1, l >>= 1\n        }\n        for(k = j;k < e;k += i) {\n          f[k] = g << 16 | m\n        }\n        ++h\n      }\n    }\n    ++g;\n    h <<= 1;\n    i <<= 1\n  }\n  return[f, c, d]\n};\nZlib.RawDeflate = function(a, b) {\n  this.compressionType = Zlib.RawDeflate.CompressionType.DYNAMIC;\n  this.lazy = 0;\n  this.input = a;\n  this.op = 0;\n  b && (b.lazy && (this.lazy = b.lazy), "number" === typeof b.compressionType && (this.compressionType = b.compressionType), b.outputBuffer && (this.output = USE_TYPEDARRAY && b.outputBuffer instanceof Array ? new Uint8Array(b.outputBuffer) : b.outputBuffer), "number" === typeof b.outputIndex && (this.op = b.outputIndex));\n  this.output || (this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(32768))\n};\nZlib.RawDeflate.CompressionType = {NONE:0, FIXED:1, DYNAMIC:2, RESERVED:3};\nZlib.RawDeflate.Lz77MinLength = 3;\nZlib.RawDeflate.Lz77MaxLength = 258;\nZlib.RawDeflate.WindowSize = 32768;\nZlib.RawDeflate.MaxCodeLength = 16;\nZlib.RawDeflate.HUFMAX = 286;\nZlib.RawDeflate.FixedHuffmanTable = function() {\n  var a = [], b;\n  for(b = 0;288 > b;b++) {\n    switch(!0) {\n      case 143 >= b:\n        a.push([b + 48, 8]);\n        break;\n      case 255 >= b:\n        a.push([b - 144 + 400, 9]);\n        break;\n      case 279 >= b:\n        a.push([b - 256 + 0, 7]);\n        break;\n      case 287 >= b:\n        a.push([b - 280 + 192, 8]);\n        break;\n      default:\n        throw"invalid literal: " + b;\n    }\n  }\n  return a\n}();\nZlib.RawDeflate.prototype.compress = function() {\n  var a, b, c, d = this.input;\n  switch(this.compressionType) {\n    case Zlib.RawDeflate.CompressionType.NONE:\n      b = 0;\n      for(c = d.length;b < c;) {\n        a = USE_TYPEDARRAY ? d.subarray(b, b + 65535) : d.slice(b, b + 65535), b += a.length, this.makeNocompressBlock(a, b === c)\n      }\n      break;\n    case Zlib.RawDeflate.CompressionType.FIXED:\n      this.output = this.makeFixedHuffmanBlock(d, !0);\n      this.op = this.output.length;\n      break;\n    case Zlib.RawDeflate.CompressionType.DYNAMIC:\n      this.output = this.makeDynamicHuffmanBlock(d, !0);\n      this.op = this.output.length;\n      break;\n    default:\n      throw"invalid compression type";\n  }\n  return this.output\n};\nZlib.RawDeflate.prototype.makeNocompressBlock = function(a, b) {\n  var c, d, e = this.output, f = this.op;\n  if(USE_TYPEDARRAY) {\n    for(e = new Uint8Array(this.output.buffer);e.length <= f + a.length + 5;) {\n      e = new Uint8Array(e.length << 1)\n    }\n    e.set(this.output)\n  }\n  c = Zlib.RawDeflate.CompressionType.NONE;\n  e[f++] = (b ? 1 : 0) | c << 1;\n  c = a.length;\n  d = ~c + 65536 & 65535;\n  e[f++] = c & 255;\n  e[f++] = c >>> 8 & 255;\n  e[f++] = d & 255;\n  e[f++] = d >>> 8 & 255;\n  if(USE_TYPEDARRAY) {\n    e.set(a, f), f += a.length, e = e.subarray(0, f)\n  }else {\n    c = 0;\n    for(d = a.length;c < d;++c) {\n      e[f++] = a[c]\n    }\n    e.length = f\n  }\n  this.op = f;\n  return this.output = e\n};\nZlib.RawDeflate.prototype.makeFixedHuffmanBlock = function(a, b) {\n  var c = new Zlib.BitStream(new Uint8Array(this.output.buffer), this.op), d;\n  d = Zlib.RawDeflate.CompressionType.FIXED;\n  c.writeBits(b ? 1 : 0, 1, !0);\n  c.writeBits(d, 2, !0);\n  d = this.lz77(a);\n  this.fixedHuffman(d, c);\n  return c.finish()\n};\nZlib.RawDeflate.prototype.makeDynamicHuffmanBlock = function(a, b) {\n  var c = new Zlib.BitStream(new Uint8Array(this.output), this.op), d, e, f, g, h = [16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15], i, j, l, m, k, n, q = Array(19), p;\n  d = Zlib.RawDeflate.CompressionType.DYNAMIC;\n  c.writeBits(b ? 1 : 0, 1, !0);\n  c.writeBits(d, 2, !0);\n  d = this.lz77(a);\n  i = this.getLengths_(this.freqsLitLen, 15);\n  j = this.getCodesFromLengths_(i);\n  l = this.getLengths_(this.freqsDist, 7);\n  m = this.getCodesFromLengths_(l);\n  for(e = 286;257 < e && 0 === i[e - 1];e--) {\n  }\n  for(f = 30;1 < f && 0 === l[f - 1];f--) {\n  }\n  k = this.getTreeSymbols_(e, i, f, l);\n  n = this.getLengths_(k.freqs, 7);\n  for(p = 0;19 > p;p++) {\n    q[p] = n[h[p]]\n  }\n  for(g = 19;4 < g && 0 === q[g - 1];g--) {\n  }\n  h = this.getCodesFromLengths_(n);\n  c.writeBits(e - 257, 5, !0);\n  c.writeBits(f - 1, 5, !0);\n  c.writeBits(g - 4, 4, !0);\n  for(p = 0;p < g;p++) {\n    c.writeBits(q[p], 3, !0)\n  }\n  p = 0;\n  for(q = k.codes.length;p < q;p++) {\n    if(e = k.codes[p], c.writeBits(h[e], n[e], !0), 16 <= e) {\n      p++;\n      switch(e) {\n        case 16:\n          e = 2;\n          break;\n        case 17:\n          e = 3;\n          break;\n        case 18:\n          e = 7;\n          break;\n        default:\n          throw"invalid code: " + e;\n      }\n      c.writeBits(k.codes[p], e, !0)\n    }\n  }\n  this.dynamicHuffman(d, [j, i], [m, l], c);\n  return c.finish()\n};\nZlib.RawDeflate.prototype.dynamicHuffman = function(a, b, c, d) {\n  var e, f, g, h, i;\n  g = b[0];\n  b = b[1];\n  h = c[0];\n  i = c[1];\n  c = 0;\n  for(e = a.length;c < e;++c) {\n    if(f = a[c], d.writeBits(g[f], b[f], !0), 256 < f) {\n      d.writeBits(a[++c], a[++c], !0), f = a[++c], d.writeBits(h[f], i[f], !0), d.writeBits(a[++c], a[++c], !0)\n    }else {\n      if(256 === f) {\n        break\n      }\n    }\n  }\n  return d\n};\nZlib.RawDeflate.prototype.fixedHuffman = function(a, b) {\n  var c, d, e;\n  c = 0;\n  for(d = a.length;c < d;c++) {\n    if(e = a[c], Zlib.BitStream.prototype.writeBits.apply(b, Zlib.RawDeflate.FixedHuffmanTable[e]), 256 < e) {\n      b.writeBits(a[++c], a[++c], !0), b.writeBits(a[++c], 5), b.writeBits(a[++c], a[++c], !0)\n    }else {\n      if(256 === e) {\n        break\n      }\n    }\n  }\n  return b\n};\nZlib.RawDeflate.Lz77Match = function(a, b) {\n  this.length = a;\n  this.backwardDistance = b\n};\nZlib.RawDeflate.Lz77Match.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint32Array(a) : a\n}(function() {\n  function a(a) {\n    switch(!0) {\n      case 3 === a:\n        return[257, a - 3, 0];\n      case 4 === a:\n        return[258, a - 4, 0];\n      case 5 === a:\n        return[259, a - 5, 0];\n      case 6 === a:\n        return[260, a - 6, 0];\n      case 7 === a:\n        return[261, a - 7, 0];\n      case 8 === a:\n        return[262, a - 8, 0];\n      case 9 === a:\n        return[263, a - 9, 0];\n      case 10 === a:\n        return[264, a - 10, 0];\n      case 12 >= a:\n        return[265, a - 11, 1];\n      case 14 >= a:\n        return[266, a - 13, 1];\n      case 16 >= a:\n        return[267, a - 15, 1];\n      case 18 >= a:\n        return[268, a - 17, 1];\n      case 22 >= a:\n        return[269, a - 19, 2];\n      case 26 >= a:\n        return[270, a - 23, 2];\n      case 30 >= a:\n        return[271, a - 27, 2];\n      case 34 >= a:\n        return[272, a - 31, 2];\n      case 42 >= a:\n        return[273, a - 35, 3];\n      case 50 >= a:\n        return[274, a - 43, 3];\n      case 58 >= a:\n        return[275, a - 51, 3];\n      case 66 >= a:\n        return[276, a - 59, 3];\n      case 82 >= a:\n        return[277, a - 67, 4];\n      case 98 >= a:\n        return[278, a - 83, 4];\n      case 114 >= a:\n        return[279, a - 99, 4];\n      case 130 >= a:\n        return[280, a - 115, 4];\n      case 162 >= a:\n        return[281, a - 131, 5];\n      case 194 >= a:\n        return[282, a - 163, 5];\n      case 226 >= a:\n        return[283, a - 195, 5];\n      case 257 >= a:\n        return[284, a - 227, 5];\n      case 258 === a:\n        return[285, a - 258, 0];\n      default:\n        throw"invalid length: " + a;\n    }\n  }\n  var b = [], c, d;\n  for(c = 3;258 >= c;c++) {\n    d = a(c), b[c] = d[2] << 24 | d[1] << 16 | d[0]\n  }\n  return b\n}());\nZlib.RawDeflate.Lz77Match.prototype.getDistanceCode_ = function(a) {\n  switch(!0) {\n    case 1 === a:\n      a = [0, a - 1, 0];\n      break;\n    case 2 === a:\n      a = [1, a - 2, 0];\n      break;\n    case 3 === a:\n      a = [2, a - 3, 0];\n      break;\n    case 4 === a:\n      a = [3, a - 4, 0];\n      break;\n    case 6 >= a:\n      a = [4, a - 5, 1];\n      break;\n    case 8 >= a:\n      a = [5, a - 7, 1];\n      break;\n    case 12 >= a:\n      a = [6, a - 9, 2];\n      break;\n    case 16 >= a:\n      a = [7, a - 13, 2];\n      break;\n    case 24 >= a:\n      a = [8, a - 17, 3];\n      break;\n    case 32 >= a:\n      a = [9, a - 25, 3];\n      break;\n    case 48 >= a:\n      a = [10, a - 33, 4];\n      break;\n    case 64 >= a:\n      a = [11, a - 49, 4];\n      break;\n    case 96 >= a:\n      a = [12, a - 65, 5];\n      break;\n    case 128 >= a:\n      a = [13, a - 97, 5];\n      break;\n    case 192 >= a:\n      a = [14, a - 129, 6];\n      break;\n    case 256 >= a:\n      a = [15, a - 193, 6];\n      break;\n    case 384 >= a:\n      a = [16, a - 257, 7];\n      break;\n    case 512 >= a:\n      a = [17, a - 385, 7];\n      break;\n    case 768 >= a:\n      a = [18, a - 513, 8];\n      break;\n    case 1024 >= a:\n      a = [19, a - 769, 8];\n      break;\n    case 1536 >= a:\n      a = [20, a - 1025, 9];\n      break;\n    case 2048 >= a:\n      a = [21, a - 1537, 9];\n      break;\n    case 3072 >= a:\n      a = [22, a - 2049, 10];\n      break;\n    case 4096 >= a:\n      a = [23, a - 3073, 10];\n      break;\n    case 6144 >= a:\n      a = [24, a - 4097, 11];\n      break;\n    case 8192 >= a:\n      a = [25, a - 6145, 11];\n      break;\n    case 12288 >= a:\n      a = [26, a - 8193, 12];\n      break;\n    case 16384 >= a:\n      a = [27, a - 12289, 12];\n      break;\n    case 24576 >= a:\n      a = [28, a - 16385, 13];\n      break;\n    case 32768 >= a:\n      a = [29, a - 24577, 13];\n      break;\n    default:\n      throw"invalid distance";\n  }\n  return a\n};\nZlib.RawDeflate.Lz77Match.prototype.toLz77Array = function() {\n  var a = this.backwardDistance, b = [], c = 0, d;\n  d = Zlib.RawDeflate.Lz77Match.LengthCodeTable[this.length];\n  b[c++] = d & 65535;\n  b[c++] = d >> 16 & 255;\n  b[c++] = d >> 24;\n  d = this.getDistanceCode_(a);\n  b[c++] = d[0];\n  b[c++] = d[1];\n  b[c++] = d[2];\n  return b\n};\nZlib.RawDeflate.prototype.lz77 = function(a) {\n  function b(a, b) {\n    var c = a.toLz77Array(), d, e;\n    d = 0;\n    for(e = c.length;d < e;++d) {\n      l[m++] = c[d]\n    }\n    n[c[0]]++;\n    q[c[3]]++;\n    k = a.length + b - 1;\n    j = null\n  }\n  var c, d, e, f, g, h = {}, i = Zlib.RawDeflate.WindowSize, j, l = USE_TYPEDARRAY ? new Uint16Array(2 * a.length) : [], m = 0, k = 0, n = new (USE_TYPEDARRAY ? Uint32Array : Array)(286), q = new (USE_TYPEDARRAY ? Uint32Array : Array)(30), p = this.lazy;\n  if(!USE_TYPEDARRAY) {\n    for(e = 0;285 >= e;) {\n      n[e++] = 0\n    }\n    for(e = 0;29 >= e;) {\n      q[e++] = 0\n    }\n  }\n  n[256] = 1;\n  c = 0;\n  for(d = a.length;c < d;++c) {\n    e = g = 0;\n    for(f = Zlib.RawDeflate.Lz77MinLength;e < f && c + e !== d;++e) {\n      g = g << 8 | a[c + e]\n    }\n    void 0 === h[g] && (h[g] = []);\n    e = h[g];\n    if(!(0 < k--)) {\n      for(;0 < e.length && c - e[0] > i;) {\n        e.shift()\n      }\n      if(c + Zlib.RawDeflate.Lz77MinLength >= d) {\n        j && b(j, -1);\n        e = 0;\n        for(f = d - c;e < f;++e) {\n          g = a[c + e], l[m++] = g, ++n[g]\n        }\n        break\n      }\n      0 < e.length ? (f = this.searchLongestMatch_(a, c, e), j ? j.length < f.length ? (g = a[c - 1], l[m++] = g, ++n[g], b(f, 0)) : b(j, -1) : f.length < p ? j = f : b(f, 0)) : j ? b(j, -1) : (g = a[c], l[m++] = g, ++n[g])\n    }\n    e.push(c)\n  }\n  l[m++] = 256;\n  n[256]++;\n  this.freqsLitLen = n;\n  this.freqsDist = q;\n  return USE_TYPEDARRAY ? l.subarray(0, m) : l\n};\nZlib.RawDeflate.prototype.searchLongestMatch_ = function(a, b, c) {\n  var d, e, f = 0, g, h, i, j = a.length;\n  h = 0;\n  i = c.length;\n  a:for(;h < i;h++) {\n    d = c[i - h - 1];\n    g = Zlib.RawDeflate.Lz77MinLength;\n    if(f > Zlib.RawDeflate.Lz77MinLength) {\n      for(g = f;g > Zlib.RawDeflate.Lz77MinLength;g--) {\n        if(a[d + g - 1] !== a[b + g - 1]) {\n          continue a\n        }\n      }\n      g = f\n    }\n    for(;g < Zlib.RawDeflate.Lz77MaxLength && b + g < j && a[d + g] === a[b + g];) {\n      ++g\n    }\n    g > f && (e = d, f = g);\n    if(g === Zlib.RawDeflate.Lz77MaxLength) {\n      break\n    }\n  }\n  return new Zlib.RawDeflate.Lz77Match(f, b - e)\n};\nZlib.RawDeflate.prototype.getTreeSymbols_ = function(a, b, c, d) {\n  var e = new (USE_TYPEDARRAY ? Uint32Array : Array)(a + c), f, g, h = new (USE_TYPEDARRAY ? Uint32Array : Array)(316), i = new (USE_TYPEDARRAY ? Uint8Array : Array)(19);\n  for(f = g = 0;f < a;f++) {\n    e[g++] = b[f]\n  }\n  for(f = 0;f < c;f++) {\n    e[g++] = d[f]\n  }\n  if(!USE_TYPEDARRAY) {\n    f = 0;\n    for(b = i.length;f < b;++f) {\n      i[f] = 0\n    }\n  }\n  f = c = 0;\n  for(b = e.length;f < b;f += g) {\n    for(g = 1;f + g < b && e[f + g] === e[f];++g) {\n    }\n    a = g;\n    if(0 === e[f]) {\n      if(3 > a) {\n        for(;0 < a--;) {\n          h[c++] = 0, i[0]++\n        }\n      }else {\n        for(;0 < a;) {\n          d = 138 > a ? a : 138, d > a - 3 && d < a && (d = a - 3), 10 >= d ? (h[c++] = 17, h[c++] = d - 3, i[17]++) : (h[c++] = 18, h[c++] = d - 11, i[18]++), a -= d\n        }\n      }\n    }else {\n      if(h[c++] = e[f], i[e[f]]++, a--, 3 > a) {\n        for(;0 < a--;) {\n          h[c++] = e[f], i[e[f]]++\n        }\n      }else {\n        for(;0 < a;) {\n          d = 6 > a ? a : 6, d > a - 3 && d < a && (d = a - 3), h[c++] = 16, h[c++] = d - 3, i[16]++, a -= d\n        }\n      }\n    }\n  }\n  return{codes:USE_TYPEDARRAY ? h.subarray(0, c) : h.slice(0, c), freqs:i}\n};\nZlib.RawDeflate.prototype.getLengths_ = function(a, b) {\n  var c = a.length, d = new Zlib.Heap(2 * Zlib.RawDeflate.HUFMAX), e = new (USE_TYPEDARRAY ? Uint8Array : Array)(c), f, g, h;\n  if(!USE_TYPEDARRAY) {\n    for(g = 0;g < c;g++) {\n      e[g] = 0\n    }\n  }\n  for(g = 0;g < c;++g) {\n    0 < a[g] && d.push(g, a[g])\n  }\n  c = Array(d.length / 2);\n  f = new (USE_TYPEDARRAY ? Uint32Array : Array)(d.length / 2);\n  if(1 === c.length) {\n    return e[d.pop().index] = 1, e\n  }\n  g = 0;\n  for(h = d.length / 2;g < h;++g) {\n    c[g] = d.pop(), f[g] = c[g].value\n  }\n  d = this.reversePackageMerge_(f, f.length, b);\n  g = 0;\n  for(h = c.length;g < h;++g) {\n    e[c[g].index] = d[g]\n  }\n  return e\n};\nZlib.RawDeflate.prototype.reversePackageMerge_ = function(a, b, c) {\n  function d(a) {\n    var c = i[a][j[a]];\n    c === b ? (d(a + 1), d(a + 1)) : --g[c];\n    ++j[a]\n  }\n  var e = new (USE_TYPEDARRAY ? Uint16Array : Array)(c), f = new (USE_TYPEDARRAY ? Uint8Array : Array)(c), g = new (USE_TYPEDARRAY ? Uint8Array : Array)(b), h = Array(c), i = Array(c), j = Array(c), l = (1 << c) - b, m = 1 << c - 1, k, n;\n  e[c - 1] = b;\n  for(k = 0;k < c;++k) {\n    l < m ? f[k] = 0 : (f[k] = 1, l -= m), l <<= 1, e[c - 2 - k] = (e[c - 1 - k] / 2 | 0) + b\n  }\n  e[0] = f[0];\n  h[0] = Array(e[0]);\n  i[0] = Array(e[0]);\n  for(k = 1;k < c;++k) {\n    e[k] > 2 * e[k - 1] + f[k] && (e[k] = 2 * e[k - 1] + f[k]), h[k] = Array(e[k]), i[k] = Array(e[k])\n  }\n  for(l = 0;l < b;++l) {\n    g[l] = c\n  }\n  for(m = 0;m < e[c - 1];++m) {\n    h[c - 1][m] = a[m], i[c - 1][m] = m\n  }\n  for(l = 0;l < c;++l) {\n    j[l] = 0\n  }\n  1 === f[c - 1] && (--g[0], ++j[c - 1]);\n  for(k = c - 2;0 <= k;--k) {\n    c = l = 0;\n    n = j[k + 1];\n    for(m = 0;m < e[k];m++) {\n      c = h[k + 1][n] + h[k + 1][n + 1], c > a[l] ? (h[k][m] = c, i[k][m] = b, n += 2) : (h[k][m] = a[l], i[k][m] = l, ++l)\n    }\n    j[k] = 0;\n    1 === f[k] && d(k)\n  }\n  return g\n};\nZlib.RawDeflate.prototype.getCodesFromLengths_ = function(a) {\n  var b = new (USE_TYPEDARRAY ? Uint16Array : Array)(a.length), c = [], d = [], e = 0, f, g, h;\n  f = 0;\n  for(g = a.length;f < g;f++) {\n    c[a[f]] = (c[a[f]] | 0) + 1\n  }\n  f = 1;\n  for(g = Zlib.RawDeflate.MaxCodeLength;f <= g;f++) {\n    d[f] = e, e += c[f] | 0, e <<= 1\n  }\n  f = 0;\n  for(g = a.length;f < g;f++) {\n    e = d[a[f]];\n    d[a[f]] += 1;\n    c = b[f] = 0;\n    for(h = a[f];c < h;c++) {\n      b[f] = b[f] << 1 | e & 1, e >>>= 1\n    }\n  }\n  return b\n};\nZlib.Gzip = function(a, b) {\n  this.input = a;\n  this.op = this.ip = 0;\n  this.flags = {};\n  b && (b.flags && (this.flags = b.flags), "string" === typeof b.filename && (this.filename = b.filename), "string" === typeof b.comment && (this.comment = b.comment), b.deflateOptions && (this.deflateOptions = b.deflateOptions));\n  this.deflateOptions || (this.deflateOptions = {})\n};\nZlib.Gzip.DefaultBufferSize = 32768;\nZlib.Gzip.prototype.compress = function() {\n  var a, b, c, d, e, f = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.Gzip.DefaultBufferSize);\n  c = 0;\n  var g = this.input, h = this.ip;\n  b = this.filename;\n  var i = this.comment;\n  f[c++] = 31;\n  f[c++] = 139;\n  f[c++] = 8;\n  a = 0;\n  this.flags.fname && (a |= Zlib.Gzip.FlagsMask.FNAME);\n  this.flags.fcomment && (a |= Zlib.Gzip.FlagsMask.FCOMMENT);\n  this.flags.fhcrc && (a |= Zlib.Gzip.FlagsMask.FHCRC);\n  f[c++] = a;\n  a = (Date.now ? Date.now() : +new Date) / 1E3 | 0;\n  f[c++] = a & 255;\n  f[c++] = a >>> 8 & 255;\n  f[c++] = a >>> 16 & 255;\n  f[c++] = a >>> 24 & 255;\n  f[c++] = 0;\n  f[c++] = Zlib.Gzip.OperatingSystem.UNKNOWN;\n  if(void 0 !== this.flags.fname) {\n    d = 0;\n    for(e = b.length;d < e;++d) {\n      a = b.charCodeAt(d), 255 < a && (f[c++] = a >>> 8 & 255), f[c++] = a & 255\n    }\n    f[c++] = 0\n  }\n  if(this.flags.comment) {\n    d = 0;\n    for(e = i.length;d < e;++d) {\n      a = i.charCodeAt(d), 255 < a && (f[c++] = a >>> 8 & 255), f[c++] = a & 255\n    }\n    f[c++] = 0\n  }\n  this.flags.fhcrc && (b = Zlib.CRC32.calc(f, 0, c) & 65535, f[c++] = b & 255, f[c++] = b >>> 8 & 255);\n  this.deflateOptions.outputBuffer = f;\n  this.deflateOptions.outputIndex = c;\n  c = new Zlib.RawDeflate(g, this.deflateOptions);\n  f = c.compress();\n  c = c.op;\n  USE_TYPEDARRAY && (c + 8 > f.buffer.byteLength ? (this.output = new Uint8Array(c + 8), this.output.set(new Uint8Array(f.buffer)), f = this.output) : f = new Uint8Array(f.buffer));\n  b = Zlib.CRC32.calc(g);\n  f[c++] = b & 255;\n  f[c++] = b >>> 8 & 255;\n  f[c++] = b >>> 16 & 255;\n  f[c++] = b >>> 24 & 255;\n  e = g.length;\n  f[c++] = e & 255;\n  f[c++] = e >>> 8 & 255;\n  f[c++] = e >>> 16 & 255;\n  f[c++] = e >>> 24 & 255;\n  this.ip = h;\n  USE_TYPEDARRAY && c < f.length && (this.output = f = f.subarray(0, c));\n  return f\n};\nZlib.Gzip.OperatingSystem = {FAT:0, AMIGA:1, VMS:2, UNIX:3, VM_CMS:4, ATARI_TOS:5, HPFS:6, MACINTOSH:7, Z_SYSTEM:8, CP_M:9, TOPS_20:10, NTFS:11, QDOS:12, ACORN_RISCOS:13, UNKNOWN:255};\nZlib.Gzip.FlagsMask = {FTEXT:1, FHCRC:2, FEXTRA:4, FNAME:8, FCOMMENT:16};\nvar ZLIB_RAW_INFLATE_BUFFER_SIZE = 32768;\nZlib.RawInflate = function(a, b) {\n  this.blocks = [];\n  this.bufferSize = ZLIB_RAW_INFLATE_BUFFER_SIZE;\n  this.bitsbuflen = this.bitsbuf = this.ip = this.totalpos = 0;\n  this.input = USE_TYPEDARRAY ? new Uint8Array(a) : a;\n  this.bfinal = !1;\n  this.bufferType = Zlib.RawInflate.BufferType.ADAPTIVE;\n  this.resize = !1;\n  if(b || !(b = {})) {\n    b.index && (this.ip = b.index), b.bufferSize && (this.bufferSize = b.bufferSize), b.bufferType && (this.bufferType = b.bufferType), b.resize && (this.resize = b.resize)\n  }\n  switch(this.bufferType) {\n    case Zlib.RawInflate.BufferType.BLOCK:\n      this.op = Zlib.RawInflate.MaxBackwardLength;\n      this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflate.MaxBackwardLength + this.bufferSize + Zlib.RawInflate.MaxCopyLength);\n      break;\n    case Zlib.RawInflate.BufferType.ADAPTIVE:\n      this.op = 0;\n      this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.bufferSize);\n      this.expandBuffer = this.expandBufferAdaptive;\n      this.concatBuffer = this.concatBufferDynamic;\n      this.decodeHuffman = this.decodeHuffmanAdaptive;\n      break;\n    default:\n      throw Error("invalid inflate mode");\n  }\n};\nZlib.RawInflate.BufferType = {BLOCK:0, ADAPTIVE:1};\nZlib.RawInflate.prototype.decompress = function() {\n  for(;!this.bfinal;) {\n    this.parseBlock()\n  }\n  return this.concatBuffer()\n};\nZlib.RawInflate.MaxBackwardLength = 32768;\nZlib.RawInflate.MaxCopyLength = 258;\nZlib.RawInflate.Order = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15]);\nZlib.RawInflate.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 258, 258]);\nZlib.RawInflate.LengthExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0]);\nZlib.RawInflate.DistCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577]);\nZlib.RawInflate.DistExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13]);\nZlib.RawInflate.FixedLiteralLengthTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(288), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 143 >= b ? 8 : 255 >= b ? 9 : 279 >= b ? 7 : 8\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflate.FixedDistanceTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(30), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 5\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflate.prototype.parseBlock = function() {\n  var a = this.readBits(3);\n  a & 1 && (this.bfinal = !0);\n  a >>>= 1;\n  switch(a) {\n    case 0:\n      this.parseUncompressedBlock();\n      break;\n    case 1:\n      this.parseFixedHuffmanBlock();\n      break;\n    case 2:\n      this.parseDynamicHuffmanBlock();\n      break;\n    default:\n      throw Error("unknown BTYPE: " + a);\n  }\n};\nZlib.RawInflate.prototype.readBits = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f;c < a;) {\n    f = d[e++];\n    if(void 0 === f) {\n      throw Error("input buffer is broken");\n    }\n    b |= f << c;\n    c += 8\n  }\n  f = b & (1 << a) - 1;\n  this.bitsbuf = b >>> a;\n  this.bitsbuflen = c - a;\n  this.ip = e;\n  return f\n};\nZlib.RawInflate.prototype.readCodeByTable = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f = a[0], a = a[1], g;c < a;) {\n    g = d[e++];\n    if(void 0 === g) {\n      throw Error("input buffer is broken");\n    }\n    b |= g << c;\n    c += 8\n  }\n  d = f[b & (1 << a) - 1];\n  f = d >>> 16;\n  this.bitsbuf = b >> f;\n  this.bitsbuflen = c - f;\n  this.ip = e;\n  return d & 65535\n};\nZlib.RawInflate.prototype.parseUncompressedBlock = function() {\n  var a = this.input, b = this.ip, c = this.output, d = this.op, e, f, g, h = c.length;\n  this.bitsbuflen = this.bitsbuf = 0;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: LEN (first byte)");\n  }\n  f = e;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: LEN (second byte)");\n  }\n  f |= e << 8;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: NLEN (first byte)");\n  }\n  g = e;\n  e = a[b++];\n  if(void 0 === e) {\n    throw Error("invalid uncompressed block header: NLEN (second byte)");\n  }\n  if(f === ~(g | e << 8)) {\n    throw Error("invalid uncompressed block header: length verify");\n  }\n  if(b + f > a.length) {\n    throw Error("input buffer is broken");\n  }\n  switch(this.bufferType) {\n    case Zlib.RawInflate.BufferType.BLOCK:\n      for(;d + f > c.length;) {\n        e = h - d;\n        f -= e;\n        if(USE_TYPEDARRAY) {\n          c.set(a.subarray(b, b + e), d), d += e, b += e\n        }else {\n          for(;e--;) {\n            c[d++] = a[b++]\n          }\n        }\n        this.op = d;\n        c = this.expandBuffer();\n        d = this.op\n      }\n      break;\n    case Zlib.RawInflate.BufferType.ADAPTIVE:\n      for(;d + f > c.length;) {\n        c = this.expandBuffer({fixRatio:2})\n      }\n      break;\n    default:\n      throw Error("invalid inflate mode");\n  }\n  if(USE_TYPEDARRAY) {\n    c.set(a.subarray(b, b + f), d), d += f, b += f\n  }else {\n    for(;f--;) {\n      c[d++] = a[b++]\n    }\n  }\n  this.ip = b;\n  this.op = d;\n  this.output = c\n};\nZlib.RawInflate.prototype.parseFixedHuffmanBlock = function() {\n  this.decodeHuffman(Zlib.RawInflate.FixedLiteralLengthTable, Zlib.RawInflate.FixedDistanceTable)\n};\nZlib.RawInflate.prototype.parseDynamicHuffmanBlock = function() {\n  function a(a, b, c) {\n    var d, e, f;\n    for(f = 0;f < a;) {\n      switch(d = this.readCodeByTable(b), d) {\n        case 16:\n          for(d = 3 + this.readBits(2);d--;) {\n            c[f++] = e\n          }\n          break;\n        case 17:\n          for(d = 3 + this.readBits(3);d--;) {\n            c[f++] = 0\n          }\n          e = 0;\n          break;\n        case 18:\n          for(d = 11 + this.readBits(7);d--;) {\n            c[f++] = 0\n          }\n          e = 0;\n          break;\n        default:\n          e = c[f++] = d\n      }\n    }\n    return c\n  }\n  var b = this.readBits(5) + 257, c = this.readBits(5) + 1, d = this.readBits(4) + 4, e = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflate.Order.length), f;\n  for(f = 0;f < d;++f) {\n    e[Zlib.RawInflate.Order[f]] = this.readBits(3)\n  }\n  d = (0,Zlib.Huffman.buildHuffmanTable)(e);\n  e = new (USE_TYPEDARRAY ? Uint8Array : Array)(b);\n  f = new (USE_TYPEDARRAY ? Uint8Array : Array)(c);\n  this.decodeHuffman((0,Zlib.Huffman.buildHuffmanTable)(a.call(this, b, d, e)), (0,Zlib.Huffman.buildHuffmanTable)(a.call(this, c, d, f)))\n};\nZlib.RawInflate.prototype.decodeHuffman = function(a, b) {\n  var c = this.output, d = this.op;\n  this.currentLitlenTable = a;\n  for(var e = c.length - Zlib.RawInflate.MaxCopyLength, f, g, h;256 !== (f = this.readCodeByTable(a));) {\n    if(256 > f) {\n      d >= e && (this.op = d, c = this.expandBuffer(), d = this.op), c[d++] = f\n    }else {\n      f -= 257;\n      h = Zlib.RawInflate.LengthCodeTable[f];\n      0 < Zlib.RawInflate.LengthExtraTable[f] && (h += this.readBits(Zlib.RawInflate.LengthExtraTable[f]));\n      f = this.readCodeByTable(b);\n      g = Zlib.RawInflate.DistCodeTable[f];\n      0 < Zlib.RawInflate.DistExtraTable[f] && (g += this.readBits(Zlib.RawInflate.DistExtraTable[f]));\n      d >= e && (this.op = d, c = this.expandBuffer(), d = this.op);\n      for(;h--;) {\n        c[d] = c[d++ - g]\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = d\n};\nZlib.RawInflate.prototype.decodeHuffmanAdaptive = function(a, b) {\n  var c = this.output, d = this.op;\n  this.currentLitlenTable = a;\n  for(var e = c.length, f, g, h;256 !== (f = this.readCodeByTable(a));) {\n    if(256 > f) {\n      d >= e && (c = this.expandBuffer(), e = c.length), c[d++] = f\n    }else {\n      f -= 257;\n      h = Zlib.RawInflate.LengthCodeTable[f];\n      0 < Zlib.RawInflate.LengthExtraTable[f] && (h += this.readBits(Zlib.RawInflate.LengthExtraTable[f]));\n      f = this.readCodeByTable(b);\n      g = Zlib.RawInflate.DistCodeTable[f];\n      0 < Zlib.RawInflate.DistExtraTable[f] && (g += this.readBits(Zlib.RawInflate.DistExtraTable[f]));\n      d + h > e && (c = this.expandBuffer(), e = c.length);\n      for(;h--;) {\n        c[d] = c[d++ - g]\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = d\n};\nZlib.RawInflate.prototype.expandBuffer = function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.op - Zlib.RawInflate.MaxBackwardLength), b = this.op - Zlib.RawInflate.MaxBackwardLength, c, d, e = this.output;\n  if(USE_TYPEDARRAY) {\n    a.set(e.subarray(Zlib.RawInflate.MaxBackwardLength, a.length))\n  }else {\n    c = 0;\n    for(d = a.length;c < d;++c) {\n      a[c] = e[c + Zlib.RawInflate.MaxBackwardLength]\n    }\n  }\n  this.blocks.push(a);\n  this.totalpos += a.length;\n  if(USE_TYPEDARRAY) {\n    e.set(e.subarray(b, b + Zlib.RawInflate.MaxBackwardLength))\n  }else {\n    for(c = 0;c < Zlib.RawInflate.MaxBackwardLength;++c) {\n      e[c] = e[b + c]\n    }\n  }\n  this.op = Zlib.RawInflate.MaxBackwardLength;\n  return e\n};\nZlib.RawInflate.prototype.expandBufferAdaptive = function(a) {\n  var b = this.input.length / this.ip + 1 | 0, c = this.input, d = this.output;\n  a && ("number" === typeof a.fixRatio && (b = a.fixRatio), "number" === typeof a.addRatio && (b += a.addRatio));\n  2 > b ? (a = (c.length - this.ip) / this.currentLitlenTable[2], a = 258 * (a / 2) | 0, a = a < d.length ? d.length + a : d.length << 1) : a = d.length * b;\n  USE_TYPEDARRAY ? (a = new Uint8Array(a), a.set(d)) : a = d;\n  return this.output = a\n};\nZlib.RawInflate.prototype.concatBuffer = function() {\n  var a = 0, b = this.output, c = this.blocks, d, e = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.totalpos + (this.op - Zlib.RawInflate.MaxBackwardLength)), f, g, h, i;\n  if(0 === c.length) {\n    return USE_TYPEDARRAY ? this.output.subarray(Zlib.RawInflate.MaxBackwardLength, this.op) : this.output.slice(Zlib.RawInflate.MaxBackwardLength, this.op)\n  }\n  f = 0;\n  for(g = c.length;f < g;++f) {\n    d = c[f];\n    h = 0;\n    for(i = d.length;h < i;++h) {\n      e[a++] = d[h]\n    }\n  }\n  f = Zlib.RawInflate.MaxBackwardLength;\n  for(g = this.op;f < g;++f) {\n    e[a++] = b[f]\n  }\n  this.blocks = [];\n  return this.buffer = e\n};\nZlib.RawInflate.prototype.concatBufferDynamic = function() {\n  var a, b = this.op;\n  USE_TYPEDARRAY ? this.resize ? (a = new Uint8Array(b), a.set(this.output.subarray(0, b))) : a = this.output.subarray(0, b) : (this.output.length > b && (this.output.length = b), a = this.output);\n  return this.buffer = a\n};\nZlib.Gunzip = function(a) {\n  this.input = a;\n  this.ip = 0;\n  this.member = [];\n  this.decompressed = !1\n};\nZlib.Gunzip.prototype.getMembers = function() {\n  this.decompressed || this.decompress();\n  return this.member.slice()\n};\nZlib.Gunzip.prototype.decompress = function() {\n  for(var a = this.input.length;this.ip < a;) {\n    this.decodeMember()\n  }\n  this.decompressed = !0;\n  return this.concatMember()\n};\nZlib.Gunzip.prototype.decodeMember = function() {\n  var a = new Zlib.GunzipMember, b, c, d, e, f, g = this.input;\n  c = this.ip;\n  a.id1 = g[c++];\n  a.id2 = g[c++];\n  if(31 !== a.id1 || 139 !== a.id2) {\n    throw Error("invalid file signature:" + a.id1 + "," + a.id2);\n  }\n  a.cm = g[c++];\n  switch(a.cm) {\n    case 8:\n      break;\n    default:\n      throw Error("unknown compression method: " + a.cm);\n  }\n  a.flg = g[c++];\n  b = g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24;\n  a.mtime = new Date(1E3 * b);\n  a.xfl = g[c++];\n  a.os = g[c++];\n  0 < (a.flg & Zlib.Gzip.FlagsMask.FEXTRA) && (a.xlen = g[c++] | g[c++] << 8, c = this.decodeSubField(c, a.xlen));\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FNAME)) {\n    f = [];\n    for(e = 0;0 < (b = g[c++]);) {\n      f[e++] = String.fromCharCode(b)\n    }\n    a.name = f.join("")\n  }\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FCOMMENT)) {\n    f = [];\n    for(e = 0;0 < (b = g[c++]);) {\n      f[e++] = String.fromCharCode(b)\n    }\n    a.comment = f.join("")\n  }\n  if(0 < (a.flg & Zlib.Gzip.FlagsMask.FHCRC) && (a.crc16 = Zlib.CRC32.calc(g, 0, c) & 65535, a.crc16 !== (g[c++] | g[c++] << 8))) {\n    throw Error("invalid header crc16");\n  }\n  b = g[g.length - 4] | g[g.length - 3] << 8 | g[g.length - 2] << 16 | g[g.length - 1] << 24;\n  g.length - c - 4 - 4 < 512 * b && (d = b);\n  c = new Zlib.RawInflate(g, {index:c, bufferSize:d});\n  a.data = d = c.decompress();\n  c = c.ip;\n  a.crc32 = b = (g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24) >>> 0;\n  if(Zlib.CRC32.calc(d) !== b) {\n    throw Error("invalid CRC-32 checksum: 0x" + Zlib.CRC32.calc(d).toString(16) + " / 0x" + b.toString(16));\n  }\n  a.isize = b = (g[c++] | g[c++] << 8 | g[c++] << 16 | g[c++] << 24) >>> 0;\n  if((d.length & 4294967295) !== b) {\n    throw Error("invalid input size: " + (d.length & 4294967295) + " / " + b);\n  }\n  this.member.push(a);\n  this.ip = c\n};\nZlib.Gunzip.prototype.decodeSubField = function(a, b) {\n  return a + b\n};\nZlib.Gunzip.prototype.concatMember = function() {\n  var a = this.member, b, c, d = 0, e = 0;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    e += a[b].data.length\n  }\n  if(USE_TYPEDARRAY) {\n    e = new Uint8Array(e);\n    for(b = 0;b < c;++b) {\n      e.set(a[b].data, d), d += a[b].data.length\n    }\n  }else {\n    e = [];\n    for(b = 0;b < c;++b) {\n      e[b] = a[b].data\n    }\n    e = Array.prototype.concat.apply([], e)\n  }\n  return e\n};\nvar ZLIB_STREAM_RAW_INFLATE_BUFFER_SIZE = 32768;\nZlib.RawInflateStream = function(a, b, c) {\n  this.blocks = [];\n  this.bufferSize = c ? c : ZLIB_STREAM_RAW_INFLATE_BUFFER_SIZE;\n  this.totalpos = 0;\n  this.ip = void 0 === b ? 0 : b;\n  this.bitsbuflen = this.bitsbuf = 0;\n  this.input = USE_TYPEDARRAY ? new Uint8Array(a) : a;\n  this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(this.bufferSize);\n  this.op = 0;\n  this.resize = this.bfinal = !1;\n  this.sp = 0;\n  this.status = Zlib.RawInflateStream.Status.INITIALIZED\n};\nZlib.RawInflateStream.BlockType = {UNCOMPRESSED:0, FIXED:1, DYNAMIC:2};\nZlib.RawInflateStream.Status = {INITIALIZED:0, BLOCK_HEADER_START:1, BLOCK_HEADER_END:2, BLOCK_BODY_START:3, BLOCK_BODY_END:4, DECODE_BLOCK_START:5, DECODE_BLOCK_END:6};\nZlib.RawInflateStream.prototype.decompress = function(a, b) {\n  var c = !1;\n  void 0 !== a && (this.input = a);\n  void 0 !== b && (this.ip = b);\n  for(;!c;) {\n    switch(this.status) {\n      case Zlib.RawInflateStream.Status.INITIALIZED:\n      ;\n      case Zlib.RawInflateStream.Status.BLOCK_HEADER_START:\n        0 > this.readBlockHeader() && (c = !0);\n        break;\n      case Zlib.RawInflateStream.Status.BLOCK_HEADER_END:\n      ;\n      case Zlib.RawInflateStream.Status.BLOCK_BODY_START:\n        switch(this.currentBlockType) {\n          case Zlib.RawInflateStream.BlockType.UNCOMPRESSED:\n            0 > this.readUncompressedBlockHeader() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.FIXED:\n            0 > this.parseFixedHuffmanBlock() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.DYNAMIC:\n            0 > this.parseDynamicHuffmanBlock() && (c = !0)\n        }\n        break;\n      case Zlib.RawInflateStream.Status.BLOCK_BODY_END:\n      ;\n      case Zlib.RawInflateStream.Status.DECODE_BLOCK_START:\n        switch(this.currentBlockType) {\n          case Zlib.RawInflateStream.BlockType.UNCOMPRESSED:\n            0 > this.parseUncompressedBlock() && (c = !0);\n            break;\n          case Zlib.RawInflateStream.BlockType.FIXED:\n          ;\n          case Zlib.RawInflateStream.BlockType.DYNAMIC:\n            0 > this.decodeHuffman() && (c = !0)\n        }\n        break;\n      case Zlib.RawInflateStream.Status.DECODE_BLOCK_END:\n        this.bfinal ? c = !0 : this.status = Zlib.RawInflateStream.Status.INITIALIZED\n    }\n  }\n  return this.concatBuffer()\n};\nZlib.RawInflateStream.MaxBackwardLength = 32768;\nZlib.RawInflateStream.MaxCopyLength = 258;\nZlib.RawInflateStream.Order = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15]);\nZlib.RawInflateStream.LengthCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 258, 258]);\nZlib.RawInflateStream.LengthExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0]);\nZlib.RawInflateStream.DistCodeTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint16Array(a) : a\n}([1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577]);\nZlib.RawInflateStream.DistExtraTable = function(a) {\n  return USE_TYPEDARRAY ? new Uint8Array(a) : a\n}([0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13]);\nZlib.RawInflateStream.FixedLiteralLengthTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(288), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 143 >= b ? 8 : 255 >= b ? 9 : 279 >= b ? 7 : 8\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflateStream.FixedDistanceTable = function(a) {\n  return a\n}(function() {\n  var a = new (USE_TYPEDARRAY ? Uint8Array : Array)(30), b, c;\n  b = 0;\n  for(c = a.length;b < c;++b) {\n    a[b] = 5\n  }\n  return(0,Zlib.Huffman.buildHuffmanTable)(a)\n}());\nZlib.RawInflateStream.prototype.readBlockHeader = function() {\n  var a;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_HEADER_START;\n  this.save_();\n  if(0 > (a = this.readBits(3))) {\n    return this.restore_(), -1\n  }\n  a & 1 && (this.bfinal = !0);\n  a >>>= 1;\n  switch(a) {\n    case 0:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.UNCOMPRESSED;\n      break;\n    case 1:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.FIXED;\n      break;\n    case 2:\n      this.currentBlockType = Zlib.RawInflateStream.BlockType.DYNAMIC;\n      break;\n    default:\n      throw Error("unknown BTYPE: " + a);\n  }\n  this.status = Zlib.RawInflateStream.Status.BLOCK_HEADER_END\n};\nZlib.RawInflateStream.prototype.readBits = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f;c < a;) {\n    f = d[e++];\n    if(void 0 === f) {\n      return-1\n    }\n    b |= f << c;\n    c += 8\n  }\n  f = b & (1 << a) - 1;\n  this.bitsbuf = b >>> a;\n  this.bitsbuflen = c - a;\n  this.ip = e;\n  return f\n};\nZlib.RawInflateStream.prototype.readCodeByTable = function(a) {\n  for(var b = this.bitsbuf, c = this.bitsbuflen, d = this.input, e = this.ip, f = a[0], a = a[1], g;c < a;) {\n    g = d[e++];\n    if(void 0 === g) {\n      return-1\n    }\n    b |= g << c;\n    c += 8\n  }\n  d = f[b & (1 << a) - 1];\n  f = d >>> 16;\n  this.bitsbuf = b >> f;\n  this.bitsbuflen = c - f;\n  this.ip = e;\n  return d & 65535\n};\nZlib.RawInflateStream.prototype.readUncompressedBlockHeader = function() {\n  var a, b, c, d = this.input, e = this.ip;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  b = a;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  b |= a << 8;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  c = a;\n  a = d[e++];\n  if(void 0 === a) {\n    return-1\n  }\n  if(b === ~(c | a << 8)) {\n    throw Error("invalid uncompressed block header: length verify");\n  }\n  this.bitsbuflen = this.bitsbuf = 0;\n  this.ip = e;\n  this.blockLength = b;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END\n};\nZlib.RawInflateStream.prototype.parseUncompressedBlock = function() {\n  var a = this.input, b = this.ip, c = this.output, d = this.op, e = this.blockLength;\n  for(this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_START;e--;) {\n    d === c.length && (c = this.expandBuffer());\n    if(void 0 === a[b]) {\n      return this.ip = b, this.op = d, this.blockLength = e + 1, -1\n    }\n    c[d++] = a[b++]\n  }\n  0 > e && (this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_END);\n  this.ip = b;\n  this.op = d;\n  return 0\n};\nZlib.RawInflateStream.prototype.parseFixedHuffmanBlock = function() {\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  this.litlenTable = Zlib.RawInflateStream.FixedLiteralLengthTable;\n  this.distTable = Zlib.RawInflateStream.FixedDistanceTable;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END;\n  return 0\n};\nZlib.RawInflateStream.prototype.save_ = function() {\n  this.ip_ = this.ip;\n  this.bitsbuflen_ = this.bitsbuflen;\n  this.bitsbuf_ = this.bitsbuf\n};\nZlib.RawInflateStream.prototype.restore_ = function() {\n  this.ip = this.ip_;\n  this.bitsbuflen = this.bitsbuflen_;\n  this.bitsbuf = this.bitsbuf_\n};\nZlib.RawInflateStream.prototype.parseDynamicHuffmanBlock = function() {\n  var a, b, c, d = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.RawInflateStream.Order.length), e, f, g, h = 0;\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_START;\n  this.save_();\n  a = this.readBits(5) + 257;\n  b = this.readBits(5) + 1;\n  c = this.readBits(4) + 4;\n  if(0 > a || 0 > b || 0 > c) {\n    return this.restore_(), -1\n  }\n  try {\n    for(var i = function(a, b, c) {\n      for(var d, e, f = 0, f = 0;f < a;) {\n        d = this.readCodeByTable(b);\n        if(0 > d) {\n          throw Error("not enough input");\n        }\n        switch(d) {\n          case 16:\n            if(0 > (d = this.readBits(2))) {\n              throw Error("not enough input");\n            }\n            for(d = 3 + d;d--;) {\n              c[f++] = e\n            }\n            break;\n          case 17:\n            if(0 > (d = this.readBits(3))) {\n              throw Error("not enough input");\n            }\n            for(d = 3 + d;d--;) {\n              c[f++] = 0\n            }\n            e = 0;\n            break;\n          case 18:\n            if(0 > (d = this.readBits(7))) {\n              throw Error("not enough input");\n            }\n            for(d = 11 + d;d--;) {\n              c[f++] = 0\n            }\n            e = 0;\n            break;\n          default:\n            e = c[f++] = d\n        }\n      }\n      return c\n    }, j, h = 0;h < c;++h) {\n      if(0 > (j = this.readBits(3))) {\n        throw Error("not enough input");\n      }\n      d[Zlib.RawInflateStream.Order[h]] = j\n    }\n    e = (0,Zlib.Huffman.buildHuffmanTable)(d);\n    f = new (USE_TYPEDARRAY ? Uint8Array : Array)(a);\n    g = new (USE_TYPEDARRAY ? Uint8Array : Array)(b);\n    this.litlenTable = (0,Zlib.Huffman.buildHuffmanTable)(i.call(this, a, e, f));\n    this.distTable = (0,Zlib.Huffman.buildHuffmanTable)(i.call(this, b, e, g))\n  }catch(l) {\n    return this.restore_(), -1\n  }\n  this.status = Zlib.RawInflateStream.Status.BLOCK_BODY_END;\n  return 0\n};\nZlib.RawInflateStream.prototype.decodeHuffman = function() {\n  var a = this.output, b = this.op, c, d, e, f = this.litlenTable, g = this.distTable, h = a.length;\n  for(this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_START;;) {\n    this.save_();\n    c = this.readCodeByTable(f);\n    if(0 > c) {\n      return this.op = b, this.restore_(), -1\n    }\n    if(256 === c) {\n      break\n    }\n    if(256 > c) {\n      b === h && (a = this.expandBuffer(), h = a.length), a[b++] = c\n    }else {\n      d = c - 257;\n      e = Zlib.RawInflateStream.LengthCodeTable[d];\n      if(0 < Zlib.RawInflateStream.LengthExtraTable[d]) {\n        c = this.readBits(Zlib.RawInflateStream.LengthExtraTable[d]);\n        if(0 > c) {\n          return this.op = b, this.restore_(), -1\n        }\n        e += c\n      }\n      c = this.readCodeByTable(g);\n      if(0 > c) {\n        return this.op = b, this.restore_(), -1\n      }\n      d = Zlib.RawInflateStream.DistCodeTable[c];\n      if(0 < Zlib.RawInflateStream.DistExtraTable[c]) {\n        c = this.readBits(Zlib.RawInflateStream.DistExtraTable[c]);\n        if(0 > c) {\n          return this.op = b, this.restore_(), -1\n        }\n        d += c\n      }\n      b + e >= h && (a = this.expandBuffer(), h = a.length);\n      for(;e--;) {\n        a[b] = a[b++ - d]\n      }\n      if(this.ip === this.input.length) {\n        return this.op = b, -1\n      }\n    }\n  }\n  for(;8 <= this.bitsbuflen;) {\n    this.bitsbuflen -= 8, this.ip--\n  }\n  this.op = b;\n  this.status = Zlib.RawInflateStream.Status.DECODE_BLOCK_END\n};\nZlib.RawInflateStream.prototype.expandBuffer = function(a) {\n  var b = this.input.length / this.ip + 1 | 0, c = this.input, d = this.output;\n  a && ("number" === typeof a.fixRatio && (b = a.fixRatio), "number" === typeof a.addRatio && (b += a.addRatio));\n  2 > b ? (a = (c.length - this.ip) / this.litlenTable[2], a = 258 * (a / 2) | 0, a = a < d.length ? d.length + a : d.length << 1) : a = d.length * b;\n  USE_TYPEDARRAY ? (a = new Uint8Array(a), a.set(d)) : a = d;\n  return this.output = a\n};\nZlib.RawInflateStream.prototype.concatBuffer = function() {\n  var a, b = this.op;\n  this.resize ? USE_TYPEDARRAY ? (a = new Uint8Array(b), a.set(this.output.subarray(this.sp, b))) : a = this.output.slice(this.sp, b) : a = USE_TYPEDARRAY ? this.output.subarray(this.sp, b) : this.output.slice(this.sp, b);\n  this.buffer = a;\n  this.sp = b;\n  return this.buffer\n};\nZlib.RawInflateStream.prototype.getBytes = function() {\n  return USE_TYPEDARRAY ? this.output.subarray(0, this.op) : this.output.slice(0, this.op)\n};\nZlib.InflateStream = function(a) {\n  this.input = void 0 === a ? new (USE_TYPEDARRAY ? Uint8Array : Array) : a;\n  this.ip = 0;\n  this.rawinflate = new Zlib.RawInflateStream(this.input, this.ip);\n  this.output = this.rawinflate.output\n};\nZlib.InflateStream.prototype.decompress = function(a) {\n  if(void 0 !== a) {\n    if(USE_TYPEDARRAY) {\n      var b = new Uint8Array(this.input.length + a.length);\n      b.set(this.input, 0);\n      b.set(a, this.input.length);\n      this.input = b\n    }else {\n      this.input = this.input.concat(a)\n    }\n  }\n  if(void 0 === this.method && 0 > this.readHeader()) {\n    return new (USE_TYPEDARRAY ? Uint8Array : Array)\n  }\n  a = this.rawinflate.decompress(this.input, this.ip);\n  this.ip = this.rawinflate.ip;\n  return a\n};\nZlib.InflateStream.prototype.getBytes = function() {\n  return this.rawinflate.getBytes()\n};\nZlib.InflateStream.prototype.readHeader = function() {\n  var a = this.ip, b = this.input, c = b[a++], b = b[a++];\n  if(void 0 === c || void 0 === b) {\n    return-1\n  }\n  switch(c & 15) {\n    case Zlib.CompressionMethod.DEFLATE:\n      this.method = Zlib.CompressionMethod.DEFLATE;\n      break;\n    default:\n      throw Error("unsupported compression method");\n  }\n  if(0 !== ((c << 8) + b) % 31) {\n    throw Error("invalid fcheck flag:" + ((c << 8) + b) % 31);\n  }\n  if(b & 32) {\n    throw Error("fdict flag is not supported");\n  }\n  this.ip = a\n};\nZlib.Util = {};\nZlib.Util.stringToByteArray = function(a) {\n  var a = a.split(""), b, c;\n  b = 0;\n  for(c = a.length;b < c;b++) {\n    a[b] = (a[b].charCodeAt(0) & 255) >>> 0\n  }\n  return a\n};\nZlib.Adler32 = function(a) {\n  "string" === typeof a && (a = Zlib.Util.stringToByteArray(a));\n  return Zlib.Adler32.update(1, a)\n};\nZlib.Adler32.update = function(a, b) {\n  for(var c = a & 65535, d = a >>> 16 & 65535, e = b.length, f, g = 0;0 < e;) {\n    f = e > Zlib.Adler32.OptimizationParameter ? Zlib.Adler32.OptimizationParameter : e;\n    e -= f;\n    do {\n      c += b[g++], d += c\n    }while(--f);\n    c %= 65521;\n    d %= 65521\n  }\n  return(d << 16 | c) >>> 0\n};\nZlib.Adler32.OptimizationParameter = 1024;\nZlib.Deflate = function(a, b) {\n  this.input = a;\n  this.output = new (USE_TYPEDARRAY ? Uint8Array : Array)(Zlib.Deflate.DefaultBufferSize);\n  this.compressionType = Zlib.Deflate.CompressionType.DYNAMIC;\n  var c = {}, d;\n  if((b || !(b = {})) && "number" === typeof b.compressionType) {\n    this.compressionType = b.compressionType\n  }\n  for(d in b) {\n    c[d] = b[d]\n  }\n  c.outputBuffer = this.output;\n  this.rawDeflate = new Zlib.RawDeflate(this.input, c)\n};\nZlib.Deflate.DefaultBufferSize = 32768;\nZlib.Deflate.CompressionType = Zlib.RawDeflate.CompressionType;\nZlib.Deflate.compress = function(a, b) {\n  return(new Zlib.Deflate(a, b)).compress()\n};\nZlib.Deflate.prototype.compress = function() {\n  var a, b, c, d = 0;\n  c = this.output;\n  a = Zlib.CompressionMethod.DEFLATE;\n  switch(a) {\n    case Zlib.CompressionMethod.DEFLATE:\n      b = Math.LOG2E * Math.log(Zlib.RawDeflate.WindowSize) - 8;\n      break;\n    default:\n      throw Error("invalid compression method");\n  }\n  b = b << 4 | a;\n  c[d++] = b;\n  switch(a) {\n    case Zlib.CompressionMethod.DEFLATE:\n      switch(this.compressionType) {\n        case Zlib.Deflate.CompressionType.NONE:\n          a = 0;\n          break;\n        case Zlib.Deflate.CompressionType.FIXED:\n          a = 1;\n          break;\n        case Zlib.Deflate.CompressionType.DYNAMIC:\n          a = 2;\n          break;\n        default:\n          throw Error("unsupported compression type");\n      }\n      break;\n    default:\n      throw Error("invalid compression method");\n  }\n  a = a << 6 | 0;\n  c[d++] = a | 31 - (256 * b + a) % 31;\n  b = Zlib.Adler32(this.input);\n  this.rawDeflate.op = d;\n  c = this.rawDeflate.compress();\n  d = c.length;\n  USE_TYPEDARRAY && (c = new Uint8Array(c.buffer), c.length <= d + 4 && (this.output = new Uint8Array(c.length + 4), this.output.set(c), c = this.output), c = c.subarray(0, d + 4));\n  c[d++] = b >> 24 & 255;\n  c[d++] = b >> 16 & 255;\n  c[d++] = b >> 8 & 255;\n  c[d++] = b & 255;\n  return c\n};\ngoog.exportSymbol("Zlib.Deflate", Zlib.Deflate);\ngoog.exportSymbol("Zlib.Deflate.compress", Zlib.Deflate.compress);\ngoog.exportSymbol("Zlib.Deflate.prototype.compress", Zlib.Deflate.prototype.compress);\nZlib.exportObject("Zlib.Deflate.CompressionType", {NONE:Zlib.Deflate.CompressionType.NONE, FIXED:Zlib.Deflate.CompressionType.FIXED, DYNAMIC:Zlib.Deflate.CompressionType.DYNAMIC});\nZlib.Inflate = function(a, b) {\n  var c, d;\n  this.input = a;\n  this.ip = 0;\n  if(b || !(b = {})) {\n    b.index && (this.ip = b.index), b.verify && (this.verify = b.verify)\n  }\n  c = a[this.ip++];\n  d = a[this.ip++];\n  switch(c & 15) {\n    case Zlib.CompressionMethod.DEFLATE:\n      this.method = Zlib.CompressionMethod.DEFLATE;\n      break;\n    default:\n      throw Error("unsupported compression method");\n  }\n  if(0 !== ((c << 8) + d) % 31) {\n    throw Error("invalid fcheck flag:" + ((c << 8) + d) % 31);\n  }\n  if(d & 32) {\n    throw Error("fdict flag is not supported");\n  }\n  this.rawinflate = new Zlib.RawInflate(a, {index:this.ip, bufferSize:b.bufferSize, bufferType:b.bufferType, resize:b.resize})\n};\nZlib.Inflate.BufferType = Zlib.RawInflate.BufferType;\nZlib.Inflate.prototype.decompress = function() {\n  var a = this.input, b;\n  b = this.rawinflate.decompress();\n  this.ip = this.rawinflate.ip;\n  if(this.verify && (a = (a[this.ip++] << 24 | a[this.ip++] << 16 | a[this.ip++] << 8 | a[this.ip++]) >>> 0, a !== Zlib.Adler32(b))) {\n    throw Error("invalid adler-32 checksum");\n  }\n  return b\n};\nZlib.Zip = function(a) {\n  a = a || {};\n  this.files = [];\n  this.comment = a.comment\n};\nZlib.Zip.prototype.addFile = function(a, b) {\n  var b = b || {}, c, d = a.length, e = 0;\n  USE_TYPEDARRAY && a instanceof Array && (a = new Uint8Array(a));\n  "number" !== typeof b.compressionMethod && (b.compressionMethod = Zlib.Zip.CompressionMethod.DEFLATE);\n  if(b.compress) {\n    switch(b.compressionMethod) {\n      case Zlib.Zip.CompressionMethod.STORE:\n        break;\n      case Zlib.Zip.CompressionMethod.DEFLATE:\n        e = Zlib.CRC32.calc(a);\n        a = this.deflateWithOption(a, b);\n        c = !0;\n        break;\n      default:\n        throw Error("unknown compression method:" + b.compressionMethod);\n    }\n  }\n  this.files.push({buffer:a, option:b, compressed:c, size:d, crc32:e})\n};\nZlib.Zip.CompressionMethod = {STORE:0, DEFLATE:8};\nZlib.Zip.OperatingSystem = {MSDOS:0, UNIX:3, MACINTOSH:7};\nZlib.Zip.prototype.compress = function() {\n  var a = this.files, b, c, d, e, f, g = 0, h = 0, i, j, l, m, k, n;\n  k = 0;\n  for(n = a.length;k < n;++k) {\n    b = a[k];\n    l = b.option.filename ? b.option.filename.length : 0;\n    m = b.option.comment ? b.option.comment.length : 0;\n    if(!b.compressed) {\n      switch(b.crc32 = Zlib.CRC32.calc(b.buffer), b.option.compressionMethod) {\n        case Zlib.Zip.CompressionMethod.STORE:\n          break;\n        case Zlib.Zip.CompressionMethod.DEFLATE:\n          b.buffer = this.deflateWithOption(b.buffer, b.option);\n          b.compressed = !0;\n          break;\n        default:\n          throw Error("unknown compression method:" + b.option.compressionMethod);\n      }\n    }\n    g += 30 + l + b.buffer.length;\n    h += 46 + l + m\n  }\n  c = new (USE_TYPEDARRAY ? Uint8Array : Array)(g + h + (46 + (this.comment ? this.comment.length : 0)));\n  d = 0;\n  e = g;\n  f = e + h;\n  k = 0;\n  for(n = a.length;k < n;++k) {\n    b = a[k];\n    l = b.option.filename ? b.option.filename.length : 0;\n    m = b.option.comment ? b.option.comment.length : 0;\n    i = d;\n    c[d++] = c[e++] = 80;\n    c[d++] = c[e++] = 75;\n    c[d++] = 3;\n    c[d++] = 4;\n    c[e++] = 1;\n    c[e++] = 2;\n    c[e++] = 20;\n    c[e++] = b.option.os || Zlib.Zip.OperatingSystem.MSDOS;\n    c[d++] = c[e++] = 20;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    j = b.option.compressionMethod;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    j = b.option.date || new Date;\n    c[d++] = c[e++] = (j.getMinutes() & 7) << 5 | j.getSeconds() / 2 | 0;\n    c[d++] = c[e++] = j.getHours() << 3 | j.getMinutes() >> 3;\n    c[d++] = c[e++] = (j.getMonth() + 1 & 7) << 5 | j.getDate();\n    c[d++] = c[e++] = (j.getFullYear() - 1980 & 127) << 1 | j.getMonth() + 1 >> 3;\n    j = b.crc32;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    j = b.buffer.length;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    j = b.size;\n    c[d++] = c[e++] = j & 255;\n    c[d++] = c[e++] = j >> 8 & 255;\n    c[d++] = c[e++] = j >> 16 & 255;\n    c[d++] = c[e++] = j >> 24 & 255;\n    c[d++] = c[e++] = l & 255;\n    c[d++] = c[e++] = l >> 8 & 255;\n    c[d++] = c[e++] = 0;\n    c[d++] = c[e++] = 0;\n    c[e++] = m & 255;\n    c[e++] = m >> 8 & 255;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = 0;\n    c[e++] = i & 255;\n    c[e++] = i >> 8 & 255;\n    c[e++] = i >> 16 & 255;\n    c[e++] = i >> 24 & 255;\n    if(j = b.option.filename) {\n      if(USE_TYPEDARRAY) {\n        c.set(j, d), c.set(j, e), d += l, e += l\n      }else {\n        for(i = 0;i < l;++i) {\n          c[d++] = c[e++] = j[i]\n        }\n      }\n    }\n    if(l = b.option.extraField) {\n      if(USE_TYPEDARRAY) {\n        c.set(l, d), c.set(l, e), d += 0, e += 0\n      }else {\n        for(i = 0;i < m;++i) {\n          c[d++] = c[e++] = l[i]\n        }\n      }\n    }\n    if(l = b.option.comment) {\n      if(USE_TYPEDARRAY) {\n        c.set(l, e), e += m\n      }else {\n        for(i = 0;i < m;++i) {\n          c[e++] = l[i]\n        }\n      }\n    }\n    if(USE_TYPEDARRAY) {\n      c.set(b.buffer, d), d += b.buffer.length\n    }else {\n      i = 0;\n      for(m = b.buffer.length;i < m;++i) {\n        c[d++] = b.buffer[i]\n      }\n    }\n  }\n  c[f++] = 80;\n  c[f++] = 75;\n  c[f++] = 5;\n  c[f++] = 6;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = 0;\n  c[f++] = n & 255;\n  c[f++] = n >> 8 & 255;\n  c[f++] = n & 255;\n  c[f++] = n >> 8 & 255;\n  c[f++] = h & 255;\n  c[f++] = h >> 8 & 255;\n  c[f++] = h >> 16 & 255;\n  c[f++] = h >> 24 & 255;\n  c[f++] = g & 255;\n  c[f++] = g >> 8 & 255;\n  c[f++] = g >> 16 & 255;\n  c[f++] = g >> 24 & 255;\n  m = this.comment ? this.comment.length : 0;\n  c[f++] = m & 255;\n  c[f++] = m >> 8 & 255;\n  if(this.comment) {\n    if(USE_TYPEDARRAY) {\n      c.set(this.comment, f)\n    }else {\n      for(i = 0;i < m;++i) {\n        c[f++] = this.comment[i]\n      }\n    }\n  }\n  return c\n};\nZlib.Zip.prototype.deflateWithOption = function(a, b) {\n  return(new Zlib.RawDeflate(a, b.deflateOption)).compress()\n};\nZlib.Unzip = function(a, b) {\n  b = b || {};\n  this.input = USE_TYPEDARRAY && a instanceof Array ? new Uint8Array(a) : a;\n  this.ip = 0;\n  this.verify = b.verify || !1\n};\nZlib.Unzip.CompressionMethod = Zlib.Zip.CompressionMethod;\nZlib.Unzip.FileHeader = function(a, b) {\n  this.input = a;\n  this.offset = b\n};\nZlib.Unzip.FileHeader.prototype.parse = function() {\n  var a = this.input, b = this.offset;\n  if(80 !== a[b++] || 75 !== a[b++] || 1 !== a[b++] || 2 !== a[b++]) {\n    throw Error("invalid file header signature");\n  }\n  this.version = a[b++];\n  this.os = a[b++];\n  this.needVersion = a[b++] | a[b++] << 8;\n  this.flags = a[b++] | a[b++] << 8;\n  this.compression = a[b++] | a[b++] << 8;\n  this.time = a[b++] | a[b++] << 8;\n  this.date = a[b++] | a[b++] << 8;\n  this.crc32 = (a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24) >>> 0;\n  this.compressedSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.plainSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.fileNameLength = a[b++] | a[b++] << 8;\n  this.extraFieldLength = a[b++] | a[b++] << 8;\n  this.fileCommentLength = a[b++] | a[b++] << 8;\n  this.diskNumberStart = a[b++] | a[b++] << 8;\n  this.internalFileAttributes = a[b++] | a[b++] << 8;\n  this.externalFileAttributes = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.relativeOffset = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.filename = String.fromCharCode.apply(null, USE_TYPEDARRAY ? a.subarray(b, b += this.fileNameLength) : a.slice(b, b += this.fileNameLength));\n  this.extraField = USE_TYPEDARRAY ? a.subarray(b, b += this.extraFieldLength) : a.slice(b, b += this.extraFieldLength);\n  this.comment = USE_TYPEDARRAY ? a.subarray(b, b + this.fileCommentLength) : a.slice(b, b + this.fileCommentLength);\n  this.length = b - this.offset\n};\nZlib.Unzip.LocalFileHeader = function(a, b) {\n  this.input = a;\n  this.offset = b\n};\nZlib.Unzip.LocalFileHeader.prototype.parse = function() {\n  var a = this.input, b = this.offset;\n  if(80 !== a[b++] || 75 !== a[b++] || 3 !== a[b++] || 4 !== a[b++]) {\n    throw Error("invalid local file header signature");\n  }\n  this.needVersion = a[b++] | a[b++] << 8;\n  this.flags = a[b++] | a[b++] << 8;\n  this.compression = a[b++] | a[b++] << 8;\n  this.time = a[b++] | a[b++] << 8;\n  this.date = a[b++] | a[b++] << 8;\n  this.crc32 = (a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24) >>> 0;\n  this.compressedSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.plainSize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.fileNameLength = a[b++] | a[b++] << 8;\n  this.extraFieldLength = a[b++] | a[b++] << 8;\n  this.filename = String.fromCharCode.apply(null, USE_TYPEDARRAY ? a.subarray(b, b += this.fileNameLength) : a.slice(b, b += this.fileNameLength));\n  this.extraField = USE_TYPEDARRAY ? a.subarray(b, b += this.extraFieldLength) : a.slice(b, b += this.extraFieldLength);\n  this.length = b - this.offset\n};\nZlib.Unzip.prototype.searchEndOfCentralDirectoryRecord = function() {\n  var a = this.input, b;\n  for(b = a.length - 12;0 < b;--b) {\n    if(80 === a[b] && 75 === a[b + 1] && 5 === a[b + 2] && 6 === a[b + 3]) {\n      this.eocdrOffset = b;\n      return\n    }\n  }\n  throw Error("End of Central Directory Record not found");\n};\nZlib.Unzip.prototype.parseEndOfCentralDirectoryRecord = function() {\n  var a = this.input, b;\n  this.eocdrOffset || this.searchEndOfCentralDirectoryRecord();\n  b = this.eocdrOffset;\n  if(80 !== a[b++] || 75 !== a[b++] || 5 !== a[b++] || 6 !== a[b++]) {\n    throw Error("invalid signature");\n  }\n  this.numberOfThisDisk = a[b++] | a[b++] << 8;\n  this.startDisk = a[b++] | a[b++] << 8;\n  this.totalEntriesThisDisk = a[b++] | a[b++] << 8;\n  this.totalEntries = a[b++] | a[b++] << 8;\n  this.centralDirectorySize = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.centralDirectoryOffset = a[b++] | a[b++] << 8 | a[b++] << 16 | a[b++] << 24;\n  this.commentLength = a[b++] | a[b++] << 8;\n  this.comment = USE_TYPEDARRAY ? a.subarray(b, b + this.commentLength) : a.slice(b, b + this.commentLength)\n};\nZlib.Unzip.prototype.parseFileHeader = function() {\n  var a = [], b = {}, c, d, e, f;\n  if(!this.fileHeaderList) {\n    void 0 === this.centralDirectoryOffset && this.parseEndOfCentralDirectoryRecord();\n    c = this.centralDirectoryOffset;\n    e = 0;\n    for(f = this.totalEntries;e < f;++e) {\n      d = new Zlib.Unzip.FileHeader(this.input, c), d.parse(), c += d.length, a[e] = d, b[d.filename] = e\n    }\n    if(this.centralDirectorySize < c - this.centralDirectoryOffset) {\n      throw Error("invalid file header size");\n    }\n    this.fileHeaderList = a;\n    this.filenameToIndex = b\n  }\n};\nZlib.Unzip.prototype.getFileData = function(a) {\n  var b = this.fileHeaderList, c;\n  b || this.parseFileHeader();\n  if(void 0 === b[a]) {\n    throw Error("wrong index");\n  }\n  b = b[a].relativeOffset;\n  a = new Zlib.Unzip.LocalFileHeader(this.input, b);\n  a.parse();\n  b += a.length;\n  c = a.compressedSize;\n  switch(a.compression) {\n    case Zlib.Unzip.CompressionMethod.STORE:\n      b = USE_TYPEDARRAY ? this.input.subarray(b, b + c) : this.input.slice(b, b + c);\n      break;\n    case Zlib.Unzip.CompressionMethod.DEFLATE:\n      b = (new Zlib.RawInflate(this.input, {index:b, bufferSize:a.plainSize})).decompress();\n      break;\n    default:\n      throw Error("unknown compression type");\n  }\n  if(this.verify && (c = Zlib.CRC32.calc(b), a.crc32 !== c)) {\n    throw Error("wrong crc: file=0x" + a.crc32.toString(16) + ", data=0x" + c.toString(16));\n  }\n  return b\n};\nZlib.Unzip.prototype.getFilenames = function() {\n  var a = [], b, c, d;\n  this.fileHeaderList || this.parseFileHeader();\n  d = this.fileHeaderList;\n  b = 0;\n  for(c = d.length;b < c;++b) {\n    a[b] = d[b].filename\n  }\n  return a\n};\nZlib.Unzip.prototype.decompress = function(a) {\n  var b;\n  this.filenameToIndex || this.parseFileHeader();\n  b = this.filenameToIndex[a];\n  if(void 0 === b) {\n    throw Error(a + " not found");\n  }\n  return this.getFileData(b)\n};\nZlib.CompressionMethod = {DEFLATE:8, RESERVED:15};\n}).call(this);\n\ndefine("thirdparty/deflate.min", function(){});\n\ndefine(\'utils/misc_utils\',[\'thirdparty/inflate.min\', \'thirdparty/deflate.min\'], function(){\n    /* Main object */\n    var utils = {\n      \n      // Print an error either to the console if in node, or to div#jsgit-errors\n      // if in the client.\n      handleError: function(message) {\n        if (jsGitInNode) {\n          console.log(message)\n        }\n        else {\n          $(\'#jsgit-errors\').append(message)\n        }\n      },\n      \n      // Turn an array of bytes into a String\n      bytesToString: function(bytes) {\n        var result = "";\n        var i;\n        for (i = 0; i < bytes.length; i++) {\n          result = result.concat(String.fromCharCode(bytes[i]));\n        }\n        return result;\n      },\n      \n      stringToBytes: function(string) {\n        var bytes = []; \n        var i; \n        for(i = 0; i < string.length; i++) {\n          bytes.push(string.charCodeAt(i) & 0xff);\n        }\n        return bytes;\n      },\n        \n      toBinaryString: function(binary) {\n        if (Array.isArray(binary)) {\n          return Git.bytesToString(binary)\n        }\n        else {\n          return binary\n        }\n      },\n        \n      // returns the next pkt-line\n      nextPktLine: function(data) {\n        var length = parseInt(data.substring(0, 4), 16);\n        return data.substring(4, length);\n      },\n      \n      // zlib files contain a two byte header. (RFC 1950)\n      stripZlibHeader: function(zlib) {\n        return zlib.subarray(2)\n      },\n      \n      escapeHTML: function(s) {\n        return s\n          .replace(/&/g, \'&amp;\')\n          .replace(/</g, \'&lt;\')\n          .replace(/>/g, \'&gt;\');\n      },\n        convertShaToBytes: function(sha){\n            var bytes = new Uint8Array(sha.length/2);\n            for (var i = 0; i < sha.length; i+=2)\n            {\n                bytes[i/2] = parseInt(\'0x\' + sha.substr(i, 2));\n            }\n            return bytes;   \n        },\n        convertBytesToSha : function(bytes){\n            var shaChars = [];\n            for (var i = 0; i < bytes.length; i++){\n                var next = (bytes[i] < 16 ? \'0\' : \'\') + bytes[i].toString(16);\n                shaChars.push(next);\n            }\n            return shaChars.join(\'\');\n        },\n        compareShas : function(sha1, sha2){\n            for (var i = 1; i < 20; i++){\n                if (sha1[i] != sha2[i]){\n                    return sha1[i] - sha2[i];\n                }\n            }\n            return 0;\n        },\n        inflate: function(data, expectedLength){\n            var options;\n            if (expectedLength){\n              options = {bufferSize: expectedLength};\n            } \n            var inflate = new Zlib.Inflate(data, options);\n            inflate.verify = true;\n            var out = inflate.decompress();\n            out.compressedLength = inflate.ip;\n            return out;\n        },\n        deflate: function(data){\n            var deflate = new Zlib.Deflate(data);\n            var out = deflate.compress();\n            return out;\n        },\n        trimBuffer: function(data){\n            var buffer = data.buffer;\n            if (data.byteOffset != 0 || data.byteLength != data.buffer.byteLength){\n                buffer = data.buffer.slice(data.byteOffset, data.byteLength + data.byteOffset);\n            }\n            return buffer;\n        }\n    }\n\n    return utils;\n\n});\n\ndefine(\'utils/file_utils\',[\'utils/misc_utils\'], function(utils){\n\tArray.prototype.asyncEach = function(func, callback){\n\t\tif (this.length == 0){\n\t\t\tcallback();\n\t\t\treturn;\n\t\t}\n\t\tvar list = this,\n\t\t    counter = {x:0, end:list.length};\n\t\t\n\t\tvar finish = function(){\n\t\t\tcounter.x += 1;\n\t\t\tif (counter.x == counter.end){\n\t\t\t\tcallback();\n\t\t\t}\n\t\t}\n\t\t\n\t\tfor (var i = 0; i < list.length; i++){\n\t\t\tfunc.call(list, list[i], finish, i);\n\t\t}\n\t}\n\n\tvar FileUtils = (function(){\n\t\t\n\t\tvar toArray = function(list) {\n\t\t\treturn Array.prototype.slice.call(list || [], 0);\n\t\t}\n\t\t\n\t\tvar makeFile = function(root, filename, contents, callback, error){\n\t\t\troot.getFile(filename, {create:true}, function(fileEntry){\n\t\t\t\tfileEntry.createWriter(function(writer){\n\t\t\t\t\twriter.onwriteend = function(){\n\t\t\t\t\t\t// strange piece of the FileWriter api. Writing to an \n\t\t\t\t\t\t// existing file just overwrites content in place. Still need to truncate\n\t\t\t\t\t\t// which triggers onwritend event...again. o_O\n\t\t\t\t\t\tif (writer.position < writer.length){\n\t\t\t\t\t\t\twriter.truncate(writer.position);\t\n\t\t\t\t\t\t}\n\t\t\t\t\t\telse if (callback)\n\t\t\t\t\t\t\tcallback(fileEntry);\n\n\t\t\t\t\t}\n\t\t\t\t\twriter.onerror = function(e){\n\t\t\t\t\t\tthrow(e);\n\t\t\t\t\t}\n\t\t\t\t\tif (contents instanceof ArrayBuffer){\n\t\t\t\t\t\tcontents = new Uint8Array(contents);\n\t\t\t\t\t}\n\t\t\t\t\twriter.write(new Blob([contents]));\n\t\t\t\t}, error);\n\t\t\t}, error);\n\t\t}\n\t\t\n\t\tvar makeDir = function(root, dirname, callback, error){\n\t\t\troot.getDirectory(dirname, {create:true},callback, error);\n\t\t}\n\t\t\n\t\treturn {\n\t\t\tmkdirs : function(root, dirname, callback, error){\n\t\t\t\tvar pathParts;\n\t\t\t\tif (dirname instanceof Array){\n\t\t\t\t\tpathParts = dirname;\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tpathParts = dirname.split(\'/\');\n\t\t\t\t}\n\t\t\t\t\n\t\t\t\tvar makeDirCallback = function(dir){\n\t\t\t\t\tif (pathParts.length){\n\t\t\t\t\t\tmakeDir(dir, pathParts.shift(), makeDirCallback, error);\n\t\t\t\t\t}\n\t\t\t\t\telse{\n\t\t\t\t\t\tif (callback)\n\t\t\t\t\t\t\tcallback(dir);\n\t\t\t\t\t}\n\t\t\t\t}\n\t\t\t\tmakeDirCallback(root);\n\t\t\t},\n\t\t\trmDir : function (root, dirname, callback){\n\t\t\t\troot.getDirectory(dirname, {create:true}, function(dirEntry){\n\t\t\t\t\tdirEntry.removeRecursively(callback, utils.errorHandler);\n\t\t\t\t});\n\t\t\t},\n\t\t\trmFile : function(root, filename, callback){\n\t\t\t\troot.getFile(filename, {create:true}, function(fileEntry){\n\t\t\t\t\tfileEntry.remove(callback, utils.errorHandler);\n\t\t\t\t});\n\t\t\t},\n\t\t\tmkfile : function(root, filename, contents, callback, error){\n\t\t\t\tif (filename.charAt(0) == \'/\'){\n\t\t\t\t\tfilename = filename.substring(1);\n\t\t\t\t}\n\t\t\t\tvar pathParts = filename.split(\'/\');\n\t\t\t\tif (pathParts.length > 1){\n\t\t\t\t\tFileUtils.mkdirs(root, pathParts.slice(0, pathParts.length - 1), function(dir){\n\t\t\t\t\t\tmakeFile(dir, pathParts[pathParts.length - 1], contents, callback, error);\n\t\t\t\t\t}, error);\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tmakeFile(root, filename, contents, callback, error);\n\t\t\t\t}\n\t\t\t},\n\t\t\tls: function(dir, callback, error){\n\t\t\t\tvar reader = dir.createReader();\n\t\t\t\tvar entries = [];\n\t\t\t\t\n\t\t\t\tvar readEntries = function() {\n\t\t\t\t\treader.readEntries (function(results) {\n\t\t\t\t\t\tif (!results.length) {\n\t\t\t\t\t\t\tcallback(entries);\n\t\t\t\t\t\t} else {\n\t\t\t\t\t\t\tentries = entries.concat(toArray(results));\n\t\t\t\t\t\t\treadEntries();\n\t\t\t\t\t\t}\n\t\t\t\t\t}, error);\n\t\t\t\t}\n\t\t\t\treadEntries();\n\t\t\t\t\n\t\t\t},\n\t\t\treadBlob: function(blob, dataType, callback){\n\t\t\t\tvar reader = new FileReader();\n\t\t\t\treader.onloadend = function(e){\n\t\t\t\t\tcallback(reader.result);\n\t\t\t\t}\n\t\t\t\treader["readAs" + dataType](blob);\n\t\t\t},\n\t\t\treadFileEntry : function(fileEntry, dataType, callback){\n\t\t\t\tfileEntry.file(function(file){\n\t\t\t\t\tFileUtils.readBlob(file, dataType, callback);\n\t\t\t\t});\n\t\t\t},\n\t\t\treadFile : function(root, file, dataType, callback, error) {\n\t\t\t\t\n\t\t\t\troot.getFile(file, {create:false}, function(fileEntry){\n\t\t\t\t\tFileUtils.readFileEntry(fileEntry, dataType, callback, error);\n\t\t\t\t}, error);\n\t\t\t}\n\t\t\n\t\t};\n\t}\n\t)();\n\n\treturn FileUtils; \n});\ndefine(\'commands/object2file\',[\'utils/file_utils\'], function(fileutils){\n\n    var expandBlob = function(dir, store, name, blobSha, callback){\n        var makeFileFactory = function(name){\n            return function(blob){\n                fileutils.mkfile(dir, name, blob.data, callback, function(e){console.log(e)});\n            }\n        }\n        store._retrieveObject(blobSha, "Blob", makeFileFactory(name));\n    }\n\n    var expandTree = function(dir, store, treeSha, callback){\n        \n        store._retrieveObject(treeSha, "Tree", function(tree){\n            var entries = tree.entries;\n            entries.asyncEach(function(entry, done){\n                if (entry.isBlob){\n                    var name = entry.name;\n                    expandBlob(dir, store, name, entry.sha, done);\n                }\n                else{\n                    var sha = entry.sha;\n                    fileutils.mkdirs(dir, entry.name, function(newDir){\n                        expandTree(newDir, store, sha, done);\n                    });\n                }\n            },callback);\n        });\n    }\n\n    return {\n        expandTree : expandTree,\n        expandBlob : expandBlob\n    }\n\n});\ndefine(\'objectstore/delta\',[],function() {\n    var applyDelta = (function() {\n        var matchLength = function(stream) {\n            var data = stream.data\n            var offset = stream.offset\n            var result = 0\n            var currentShift = 0\n            var _byte = 128\n            var maskedByte, shiftedByte\n\n            while ((_byte & 128) != 0) {\n                _byte = data[offset]\n                offset += 1\n                maskedByte = _byte & 0x7f\n                shiftedByte = maskedByte << currentShift\n                result += shiftedByte\n                currentShift += 7\n            }\n            stream.offset = offset\n            return result\n        }\n\n        return function(baseData, delta) {\n            //var baseData = Git.stringToBytes(baseDataString)\n            var stream = {\n                data: delta,\n                offset: 0,\n                length: delta.length\n            }\n            var bb = [];\n            var baseLength = matchLength(stream)\n            if (baseLength != baseData.length) {\n                throw (Error("Delta Error: base length not equal to length of given base data"))\n            }\n\n            var resultLength = matchLength(stream)\n            var resultData = new Uint8Array(resultLength);\n            var resultOffset = 0;\n\n            var copyOffset\n            var copyLength\n            var opcode\n            var copyFromResult\n            while (stream.offset < stream.length) {\n                opcode = stream.data[stream.offset]\n                stream.offset += 1\n                copyOffset = 0\n                copyLength = 0\n                if (opcode == 0) {\n                    throw (Error("Don\'t know what to do with a delta opcode 0"))\n                } else if ((opcode & 0x80) != 0) {\n                    var value\n                    var shift = 0\n                    _(4).times(function() {\n                        if ((opcode & 0x01) != 0) {\n                            value = stream.data[stream.offset]\n                            stream.offset += 1\n                            copyOffset += (value << shift)\n                        }\n                        opcode >>= 1\n                        shift += 8\n                    })\n                    shift = 0\n                    _(2).times(function() {\n                        if ((opcode & 0x01) != 0) {\n                            value = stream.data[stream.offset]\n                            stream.offset += 1\n                            copyLength += (value << shift)\n                        }\n                        opcode >>= 1\n                        shift += 8\n                    })\n                    if (copyLength == 0) {\n                        copyLength = (1 << 16)\n                    }\n\n                    // TODO: check if this is a version 2 packfile and apply copyFromResult if so\n                    copyFromResult = (opcode & 0x01)\n                    var subarray = baseData.subarray(copyOffset, copyOffset + copyLength);\n                    resultData.set(subarray, resultOffset);\n                    resultOffset += subarray.length;\n\n                } else if ((opcode & 0x80) == 0) {\n                    var subarray = stream.data.subarray(stream.offset, stream.offset + opcode);\n                    resultData.set(subarray, resultOffset);\n                    resultOffset += subarray.length;\n                    stream.offset += opcode\n                }\n            }\n            return resultData.buffer;\n            \n        }\n    }());\n\n    return applyDelta;\n});\nif(typeof Crypto=="undefined"||!Crypto.util)(function(){var i=window.Crypto={},l=i.util={rotl:function(a,c){return a<<c|a>>>32-c},rotr:function(a,c){return a<<32-c|a>>>c},endian:function(a){if(a.constructor==Number)return l.rotl(a,8)&16711935|l.rotl(a,24)&4278255360;for(var c=0;c<a.length;c++)a[c]=l.endian(a[c]);return a},randomBytes:function(a){for(var c=[];a>0;a--)c.push(Math.floor(Math.random()*256));return c},bytesToWords:function(a){for(var c=[],b=0,d=0;b<a.length;b++,d+=8)c[d>>>5]|=a[b]<<24-\nd%32;return c},wordsToBytes:function(a){for(var c=[],b=0;b<a.length*32;b+=8)c.push(a[b>>>5]>>>24-b%32&255);return c},bytesToHex:function(a){for(var c=[],b=0;b<a.length;b++){c.push((a[b]>>>4).toString(16));c.push((a[b]&15).toString(16))}return c.join("")},hexToBytes:function(a){for(var c=[],b=0;b<a.length;b+=2)c.push(parseInt(a.substr(b,2),16));return c},bytesToBase64:function(a){if(typeof btoa=="function")return btoa(m.bytesToString(a));for(var c=[],b=0;b<a.length;b+=3)for(var d=a[b]<<16|a[b+1]<<\n8|a[b+2],e=0;e<4;e++)b*8+e*6<=a.length*8?c.push("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".charAt(d>>>6*(3-e)&63)):c.push("=");return c.join("")},base64ToBytes:function(a){if(typeof atob=="function")return m.stringToBytes(atob(a));a=a.replace(/[^A-Z0-9+\\/]/ig,"");for(var c=[],b=0,d=0;b<a.length;d=++b%4)d!=0&&c.push(("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".indexOf(a.charAt(b-1))&Math.pow(2,-2*d+8)-1)<<d*2|"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".indexOf(a.charAt(b))>>>\n6-d*2);return c}};i.mode={};i=i.charenc={};i.UTF8={stringToBytes:function(a){return m.stringToBytes(unescape(encodeURIComponent(a)))},bytesToString:function(a){return decodeURIComponent(escape(m.bytesToString(a)))}};var m=i.Binary={stringToBytes:function(a){for(var c=[],b=0;b<a.length;b++)c.push(a.charCodeAt(b)&255);return c},bytesToString:function(a){for(var c=[],b=0;b<a.length;b++)c.push(String.fromCharCode(a[b]));return c.join("")}}})();\n(function(){var i=Crypto,l=i.util,m=i.charenc,a=m.UTF8,c=m.Binary,b=i.SHA1=function(d,e){var g=l.wordsToBytes(b._sha1(d));return e&&e.asBytes?g:e&&e.asString?c.bytesToString(g):l.bytesToHex(g)};b._sha1=function(d){if(d.constructor==String)d=a.stringToBytes(d);var e=l.bytesToWords(d),g=d.length*8;d=[];var n=1732584193,h=-271733879,j=-1732584194,k=271733878,o=-1009589776;e[g>>5]|=128<<24-g%32;e[(g+64>>>9<<4)+15]=g;for(g=0;g<e.length;g+=16){for(var q=n,r=h,s=j,t=k,u=o,f=0;f<80;f++){if(f<16)d[f]=e[g+\nf];else{var p=d[f-3]^d[f-8]^d[f-14]^d[f-16];d[f]=p<<1|p>>>31}p=(n<<5|n>>>27)+o+(d[f]>>>0)+(f<20?(h&j|~h&k)+1518500249:f<40?(h^j^k)+1859775393:f<60?(h&j|h&k|j&k)-1894007588:(h^j^k)-899497514);o=k;k=j;j=h<<30|h>>>2;h=n;n=p}n+=q;h+=r;j+=s;k+=t;o+=u}return[n,h,j,k,o]};b._blocksize=16;b._digestsize=20})();\n\ndefine("thirdparty/2.2.0-sha1", function(){});\n\ndefine(\'formats/pack\',[\'objectstore/delta\', \'utils/misc_utils\', \'utils/file_utils\', \'thirdparty/2.2.0-sha1\'], function(applyDelta, utils, fileutils) {\n\n    String.prototype.rjust = function(width, padding) {\n        padding = padding || " ";\n        padding = padding.substr(0, 1);\n        if (this.length < width)\n            return padding.repeat(width - this.length) + this;\n        else\n            return this.toString();\n    }\n    String.prototype.repeat = function(num) {\n        for (var i = 0, buf = ""; i < num; i++) buf += this;\n        return buf;\n    }\n\n    var Pack = function(binary, store) {\n        //var binaryString = Git.toBinaryString(binary)\n        var data;\n        if (binary.constructor == String)\n            data = new Uint8Array(utils.stringToBytes(binary)); //new BinaryFile(binaryString)\n        else\n            data = new Uint8Array(binary); //new BinaryFile(binaryString)\n        var offset = 0\n        var objects = null\n\n        //var lastObjectData = null;\n        //var chainCache = {};\n        this.getData = function() {\n            return data;\n        }\n\n        //if (typeof require === "undefined") {\n        var myDebug = function(obj) {\n            console.log(obj)\n        }\n        //}\n        //else {\n        //  var myDebug = require(\'util\').debug\n        //}\n\n        var peek = function(length) {\n            return data.subarray(offset, offset + length)\n        }\n\n        var rest = function() {\n            return data.subarray(offset)\n        }\n\n        var advance = function(length) {\n            offset += length\n        }\n\n        var matchPrefix = function() {\n            if (utils.bytesToString(peek(4)) === "PACK") {\n                advance(4)\n            } else {\n                throw (Error("couldn\'t match PACK"))\n            }\n        }\n\n        var matchVersion = function(expectedVersion) {\n            var actualVersion = peek(4)[3]\n            advance(4)\n            if (actualVersion !== expectedVersion) {\n                throw ("expected packfile version " + expectedVersion + ", but got " + actualVersion)\n            }\n        }\n\n        var matchNumberOfObjects = function() {\n            var num = 0\n            _(peek(4)).each(function(b) {\n                num = num << 8\n                num += b\n            })\n            advance(4);\n            return num;\n        }\n\n        var PackedTypes = {\n            COMMIT: 1,\n            TREE: 2,\n            BLOB: 3,\n            TAG: 4,\n            OFS_DELTA: 6,\n            REF_DELTA: 7\n        }\n        var typeArray = [null, "commit", "tree", "blob", "tag", null, "ofs_delta", "ref_delta"];\n        var getTypeStr = function(type) {\n            return typeArray[type];\n        }\n\n        var matchObjectHeader = function() {\n            var objectStartOffset = offset;\n            var headByte = data[offset++];\n            var type = (0x70 & headByte) >>> 4;\n            var needMore = (0x80 & headByte) > 0;\n\n            var size = headByte & 0xf;\n            var bitsToShift = 4;\n\n            while (needMore) {\n                headByte = data[offset++];\n                needMore = (0x80 & headByte) > 0;\n                size = size | ((headByte & 0x7f) << bitsToShift);\n                bitsToShift += 7;\n            }\n\n            return {\n                size: size,\n                type: type,\n                offset: objectStartOffset\n            }\n           \n        }\n\n        var objectHash = function(type, content) {\n            var contentData = new Uint8Array(content);\n            var data = utils.stringToBytes(getTypeStr(type) + " " + contentData.byteLength + "\\0");\n            var buf = new ArrayBuffer(data.length + contentData.byteLength);\n            var fullContent = new Uint8Array(buf);\n            fullContent.set(data);\n            fullContent.set(contentData, data.length);\n            // return new SHA1(data).hexdigest()\n            return Crypto.SHA1(fullContent, {\n                asBytes: true\n            });\n        }\n\n        var findDeltaBaseOffset = function(header) {\n            var offsetBytes = []\n            var hintAndOffsetBits = peek(1)[0].toString(2).rjust(8, "0")\n            var needMore = (hintAndOffsetBits[0] == "1")\n\n            offsetBytes.push(hintAndOffsetBits.slice(1, 8))\n            advance(1)\n\n            while (needMore) {\n                hintAndOffsetBits = peek(1)[0].toString(2).rjust(8, "0")\n                needMore = (hintAndOffsetBits[0] == "1")\n                offsetBytes.push(hintAndOffsetBits.slice(1, 8))\n                advance(1)\n            }\n\n            var longOffsetString = _(offsetBytes).reduce(function(memo, byteString) {\n                return memo + byteString\n            }, "")\n\n            var offsetDelta = parseInt(longOffsetString, 2)\n            var n = 1\n            _(offsetBytes.length - 1).times(function() {\n                offsetDelta += Math.pow(2, 7 * n)\n                n += 1\n            })\n            var desiredOffset = header.offset - offsetDelta\n            return desiredOffset;\n        }\n\n        \n        var expandDeltifiedObject = function(object, callback) {\n\n            var doExpand = function(baseObject, deltaObject) {\n                deltaObject.type = baseObject.type;\n                deltaObject.data = applyDelta(new Uint8Array(baseObject.data), new Uint8Array(deltaObject.data));\n                deltaObject.sha = objectHash(deltaObject.type, deltaObject.data);\n                return deltaObject;\n            }\n\n            if (object.type == PackedTypes.OFS_DELTA) {\n                var baseObject = matchObjectAtOffset(object.desiredOffset);\n                switch (baseObject.type) {\n                    case PackedTypes.OFS_DELTA:\n                    case PackedTypes.REF_DELTA:\n                        expandDeltifiedObject(baseObject, function(expandedObject) {\n                            var newObject = doExpand(expandedObject, object);\n                            callback(newObject);\n                        });\n                        break;\n                    default:\n                        var newObject = doExpand(baseObject, object);\n                        callback(newObject);\n                }\n\n            } else {\n                store._retrieveRawObject(object.baseSha, \'ArrayBuffer\', function(baseObject) {\n                    baseObject.sha = object.baseSha;\n                    var newObject = doExpand(baseObject, object);\n                    callback(newObject);\n                });\n            }\n        }\n        var uncompressObject = function(objOffset, uncompressedLength) {\n            var deflated = data.subarray(objOffset);\n            var out = utils.inflate(deflated, uncompressedLength);\n\n            return {\n                buf: utils.trimBuffer(out),\n                compressedLength: out.compressedLength\n            };\n        }\n\n        var matchObjectData = function(header) {\n\n            var object = {\n                offset: header.offset,\n                //dataOffset: dataOffset,\n                //crc: crc32.crc(data.subarray(header.offset, offset)),\n                type: header.type,\n                //sha: objectHash(header.type, buf),\n                // data: objData.buf\n            }\n            switch (header.type) {\n                case PackedTypes.OFS_DELTA:\n                    object.desiredOffset = findDeltaBaseOffset(header);\n                    break;\n                case PackedTypes.REF_DELTA:\n                    var shaBytes = peek(20)\n                    advance(20)\n                    object.baseSha = _(shaBytes).map(function(b) {\n                        return b.toString(16).rjust(2, "0")\n                    }).join("")\n                    break;\n                default:\n                    break;\n\n            }\n            var objData = uncompressObject(offset, header.size);\n            object.data = objData.buf;\n\n            //var checksum = adler32(buf)\n            advance(objData.compressedLength);\n            //matchBytes(intToBytes(checksum, 4))\n\n            return object;\n        }\n\n        var matchObjectAtOffset = function(startOffset) {\n            offset = startOffset\n            var header = matchObjectHeader()\n            return matchObjectData(header);\n        }\n\n        // slightly different code path from the original parser used for building the index\n        // I\'m doing it seperately because I needed to solve a call stack overflow caused by\n        // synchronouse execution of callbacks for the case of non deltified objects in the\n        // pack.\n        var matchAndExpandObjectAtOffset = function(startOffset, dataType, callback) {\n            //var reverseMap = [null, "commit", "tree", "blob"]\n\n            var object = matchObjectAtOffset(startOffset);\n\n            var convertToDataType = function(object) {\n                //object.type = reverseMap[object.type];\n                if (dataType != \'ArrayBuffer\') {\n                    var reader = new FileReader();\n\n                    reader.onloadend = function() {\n                        var buf = reader.result;\n                        object.data = buf;\n                        callback(object);\n                    }\n                    reader[\'readAs\' + dataType](new Blob([object.data]));\n                }\n                else{\n                    callback(object);\n                }\n            }\n            switch (object.type) {\n                case PackedTypes.OFS_DELTA:\n                case PackedTypes.REF_DELTA:\n                    expandDeltifiedObject(object, function(expandedObject){\n                        convertToDataType(expandedObject);\n                    });\n                    break;\n                default:\n                    convertToDataType(object);\n                    break;\n            }\n        };\n        this.matchAndExpandObjectAtOffset = matchAndExpandObjectAtOffset;\n\n        var stripOffsetsFromObjects = function() {\n            _(objects).each(function(object) {\n                delete object.offset\n            })\n        }\n\n        var objectAtOffset = function(offset) {\n            return _(objects).detect(function(obj) {\n                return obj.offset == offset\n            })\n        }\n\n        this.matchObjectAtOffset = matchObjectAtOffset;\n\n        this.parseAll = function(success, progress) {\n            try {\n                var numObjects;\n                var i;\n                var deferredObjects = [];\n                objects = [];\n\n\n                matchPrefix()\n                matchVersion(2)\n                numObjects = matchNumberOfObjects();\n\n                if (progress){\n                    var tracker = 0;\n                    var lastPercent = 0;\n                    var trackProgress = function(){\n                        var pct = (++tracker/numObjects) * 100\n                        if (pct - lastPercent >= 1){\n                            progress({pct: pct, msg: "Unpacking " + tracker + \'/\' + numObjects + " objects"});\n                        }\n                    }\n                }\n                else{\n                    var trackProgress = function(){};\n                }\n                trackProgress();\n                for (i = 0; i < numObjects; i++) {\n                    var object = matchObjectAtOffset(offset);\n\n                    object.crc = crc32.crc(data.subarray(object.offset, offset));\n\n                    // hold on to the data for delta style objects.\n                    switch (object.type) {\n                        case PackedTypes.OFS_DELTA:\n                        case PackedTypes.REF_DELTA:\n                            {\n                                deferredObjects.push(object);\n                                break;\n                            }\n                        default:\n                            object.sha = objectHash(object.type, object.data);\n                            delete object.data;\n                            trackProgress();\n                            break;\n                    }\n                    objects.push(object);\n                }\n\n                deferredObjects.asyncEach(function(obj, done) {\n                    expandDeltifiedObject(obj, function(obj){\n                        delete obj.data;\n                        trackProgress();\n                        done();\n                    });\n                },\n                    success);\n\n            } catch (e) {\n                //console.log("Error caught in pack file parsing data") // + Git.stringToBytes(data.getRawData()))\n                throw (e)\n            }\n            return this\n        }\n\n        this.getObjects = function() {\n            return objects\n        }\n\n        // this.getObjectAtOffset = getObjectAtOffset\n    }\n\n    Pack.buildPack = function(commits, repo, callback) {\n        var visited = {};\n        var counter = {\n            x: 0,\n            numObjects: 0\n        };\n        var packed = []; //new BlobBuilder();\n\n        var map = {\n            "commit": 1,\n            "tree": 2,\n            "blob": 3\n        };\n\n        var packTypeSizeBits = function(type, size) {\n            var typeBits = type;//map[type];\n            var shifter = size;\n            var bytes = [];\n            var idx = 0;\n\n            bytes[idx] = typeBits << 4 | (shifter & 0xf);\n            shifter = shifter >>> 4;\n\n            while (shifter != 0) {\n                bytes[idx] = bytes[idx] | 0x80;\n                bytes[++idx] = shifter & 0x7f;\n                shifter = shifter >>> 7;\n            }\n            return new Uint8Array(bytes);\n        }\n\n        var packIt = function(object) {\n            var compressed;\n            var size;\n            var type = object.type;\n\n            if (object.compressedData) {\n                size = object.size;\n                // clone the data since it may be sub view of a larger buffer;\n                compressed = new Uint8Array(compressedData).buffer;\n            } else {\n                var buf = object.data;\n                var data;\n                if (buf instanceof ArrayBuffer) {\n                    data = new Uint8Array(buf);\n                } else if (buf instanceof Uint8Array) {\n                    data = buf;\n                } else {\n                    // assume it\'s a string\n                    data = utils.stringToBytes(buf);\n                }\n\n                compressed = utils.deflate(data);\n                size = data.length;\n            }\n            packed.push(packTypeSizeBits(type, size));\n            packed.push(compressed);\n            counter.numObjects++;\n        }\n\n        var finishPack = function() {\n            var packedObjects = []; //new BlobBuilder();\n\n            var buf = new ArrayBuffer(12);\n            var dv = new DataView(buf);\n\n            // \'PACK\'\n            dv.setUint32(0, 0x5041434b, false);\n            // version\n            dv.setUint32(4, 2, false);\n            //number of packed objects\n            dv.setUint32(8, counter.numObjects, false);\n\n            //finalPack.append(buf);\n            //finalPack.append(packedObjects);\n            packedObjects.push(dv);\n            //packed.reverse();\n            for (var i = 0; i < packed.length; i++) {\n                packedObjects.push(packed[i]);\n            }\n            //packed.getBlob();\n            fileutils.readBlob(new Blob(packedObjects), \'ArrayBuffer\', function(dataBuf) {\n                packed = null;\n                var dataBufArray = new Uint8Array(dataBuf);\n                var sha = Crypto.SHA1(dataBufArray, {\n                    asBytes: true\n                });\n\n                var finalPack = []; //new BlobBuilder();\n                finalPack.push(dataBufArray);\n                finalPack.push(new Uint8Array(sha));\n\n                fileutils.readBlob(new Blob(finalPack), \'ArrayBuffer\', callback);\n            });\n\n        }\n\n        var walkTree = function(treeSha, callback) {\n            if (visited[treeSha]) {\n                callback();\n                return;\n            } else {\n                visited[treeSha] = true;\n            }\n\n            var packTree = function(){\n                repo._retrieveObject(treeSha, \'Tree\', function(tree, rawObj) {\n                    var childCount = {\n                        x: 0\n                    };\n                    var handleCallback = function() {\n                        childCount.x++;\n                        if (childCount.x == tree.entries.length) {\n                            packIt(rawObj);\n                            callback();\n                        }\n                    }\n\n                    for (var i = 0; i < tree.entries.length; i++) {\n                        var nextSha = utils.convertBytesToSha(tree.entries[i].sha);\n                        if (tree.entries[i].isBlob) {\n                            if (visited[nextSha]) {\n                                handleCallback();\n                            } else {\n                                visited[nextSha] = true;\n                                repo._findPackedObject(tree.entries[i].sha, handleCallback, function(){\n                                    repo._retrieveRawObject(nextSha, \'Raw\', function(object) {\n                                        packIt(object);\n                                        handleCallback();\n                                    });\n                                });\n                            }\n                        } else {\n                            walkTree(nextSha, function() {\n                                handleCallback();\n                            });\n                        }\n                    }\n                });\n            }\n            var shaBytes = utils.convertShaToBytes(treeSha);\n            // assumes that if it\'s packed, the remote knows about the object since all stored packs came from the remote.\n            repo._findPackedObject(shaBytes, callback, packTree);\n        }\n\n        if (commits.length == 0){\n            finishPack();\n        }\n        else{\n            commits.forEach(function(commitObj) {\n                //repo._retrieveObject(commitShas[i], \'Commit\', function(commit, rawObj){\n                var commit = commitObj.commit;\n                packIt(commitObj.raw);\n                walkTree(commit.tree, function() {\n                    if (++counter.x == commits.length) {\n                        finishPack();\n                    }\n                });\n                //});\n            });\n        }\n\n    }\n    return Pack;\n});\ndefine(\'formats/upload_pack_parser\',[\'formats/pack\', \'utils/misc_utils\'], function(Pack, utils) {\n    var parse = function(arraybuffer, repo, success, progress) {\n        var data = new Uint8Array(arraybuffer); //new BinaryFile(binaryString);\n        var offset = 0;\n        var remoteLines = null;\n        var objects = null;\n\n        var peek = function(length) {\n            return data.subarray(offset, offset + length);\n        };\n\n        var advance = function(length) {\n            offset += length;\n        };\n\n        var getPktLineStr = function(pktLine){\n            return utils.bytesToString(data.subarray(pktLine.start, pktLine.end));\n        }\n\n        // A pkt-line is defined in http://git-scm.com/gitserver.txt\n        var nextPktLine = function(isShallow) {\n            var pktLine = null;\n            var length;\n            length = parseInt(utils.bytesToString(peek(4)), 16);\n            advance(4);\n            if (length == 0) {\n                if (isShallow) {\n                    return nextPktLine()\n                }\n            } else {\n                pktLine = {start: offset, end: offset + length - 4};//peek(length - 4);\n                advance(length - 4);\n            }\n            return pktLine;\n        };\n\n        //console.log("Parsing upload pack of  " + arraybuffer.byteLength + " bytes")\n        var startTime = new Date()\n        var pktLine = nextPktLine()\n        var packFileParser\n        var remoteLine = ""\n        //var packData = ""\n        var gotAckOrNak = false\n        var ackRegex = /ACK ([0-9a-fA-F]{40}) common/;\n        var common = [];\n\n        var pktLineStr = getPktLineStr(pktLine);//utils.bytesToString(data.slice(pktLine.start, pktLine.end));\n        var shallow;\n        while (pktLineStr.slice(0, 7) === "shallow") {\n            pktLine = nextPktLine(true);\n            shallow = pktLineStr.substring(8);\n            pktLineStr = getPktLineStr(pktLine);\n        }\n\n        while (pktLineStr === "NAK\\n" ||\n            pktLineStr.slice(0, 3) === "ACK") {\n            var matches = ackRegex.exec(pktLineStr);\n            if (matches) {\n                common.push(matches[1]);\n            }\n            pktLine = nextPktLine();\n            pktLineStr = getPktLineStr(pktLine);\n            gotAckOrNak = true;\n        }\n\n        if (!gotAckOrNak) {\n            throw (Error("got neither ACK nor NAK in upload pack response"))\n        }\n        var packDataLines = [];\n        while (pktLine !== null) {\n            var pktLineType = data[pktLine.start];\n            // sideband format. "2" indicates progress messages, "1" pack data\n            switch (pktLineType){\n                case 2:\n                    break;\n                case 1:\n                    //packData += utils.bytesToString(pktLine.slice(1))\n                    packDataLines.push(data.subarray(pktLine.start + 1, pktLine.end));\n                    break;\n                case 3:\n                    throw (Error("fatal error in packet line"))\n                    break;    \n            }\n            pktLine = nextPktLine()\n        }\n\n        // create a blob from the packdata lines then read it as an arraybuffer\n        var packDataBlob = new Blob(packDataLines);\n        var reader = new FileReader();\n        reader.onloadend = function(e){\n            var packData = reader.result;\n            packFileParser = new Pack(packData, repo);\n\n            packFileParser.parseAll(function() {\n                objects = packFileParser.getObjects()\n\n               // console.log("took " + (new Date().getTime() - startTime.getTime()) + "ms")\n                success(objects, packFileParser.getData(), common, shallow);\n            }, progress);\n        }\n        reader.readAsArrayBuffer(packDataBlob);\n    };\n    return {\n        parse: parse\n    };\n});\ndefine(\'utils/errors\',[],function() {\n\n    var errors = {\n        // Indicates an unexpected error in the file system.\n        FILE_IO_ERROR: 0,\n        FILE_IO_ERROR_MSG: \'Unexpected File I/O error\',\n        // Indicates an unexpected ajax error when trying to make a request\n        AJAX_ERROR: 1, \n        AJAX_ERROR_MSG: \'Unexpected ajax error\',\n        \n        // trying to clone into a non-empty directory\n        CLONE_DIR_NOT_EMPTY: 2,\n        CLONE_DIR_NOT_EMPTY_MSG: \'The target directory contains files\',\n        // No .git directory\n        CLONE_DIR_NOT_INTIALIZED: 3,\n        CLONE_DIR_NOT_INTIALIZED_MSG: \'The target directory hasn\\\'t been initialized.\',\n        // .git directory already contains objects\n        CLONE_GIT_DIR_IN_USE: 4,\n        CLONE_GIT_DIR_IN_USE_MSG: \'The target directory contains a .git directory already in use.\',\n        // No branch found with the name given\n        REMOTE_BRANCH_NOT_FOUND: 5,\n        REMOTE_BRANCH_NOT_FOUND_MSG: \'Can\\\'t find the branch name in the remote repository\',\n\n        // only supports fast forward merging at the moment.\n        PULL_NON_FAST_FORWARD: 6,\n        PULL_NON_FAST_FORWARD_MSG: \'Pulling from the remote repo requires a merge.\',\n        // Branch is up to date\n        PULL_UP_TO_DATE: 7,\n        PULL_UP_TO_DATE_MSG: \'Everything is up to date\',\n\n\n        UNCOMMITTED_CHANGES: 11,\n        UNCOMMITTED_CHANGES_MSG: \'There are changes in the working directory that haven\\\'t been committed\',\n\n        // Nothing to commit\n        COMMIT_NO_CHANGES: 8,\n        COMMIT_NO_CHANGES_MSG: \'No changes to commit\',\n\n        // The remote repo and the local repo share the same head.\n        PUSH_NO_CHANGES: 9,\n        PUSH_NO_CHANGES_MSG: \'No new commits to push to the repository\',\n\n        PUSH_NO_REMOTE: 16,\n        PUSH_NO_REMOTE_MSG: \'No remote to push to\',\n\n        // Need to merge remote changes first. \n        PUSH_NON_FAST_FORWARD: 10,\n        PUSH_NON_FAST_FORWARD_MSG: \'The remote repo has new commits on your current branch. You need to merge them first.\',\n\n        BRANCH_ALREADY_EXISTS: 14,\n        BRANCH_ALREADY_EXISTS_MSG: \'A local branch with that name already exists\',\n\n        BRANCH_NAME_NOT_VALID: 12,\n        BRANCH_NAME_NOT_VALID_MSG: \'The branch name is not valid.\',\n\n        CHECKOUT_BRANCH_NO_EXISTS: 15,\n        CHECKOUT_BRANCH_NO_EXISTS_MSG: \'No local branch with that name exists\',\n\n        // unexpected problem retrieving objects\n        OBJECT_STORE_CORRUPTED: 200,\n        OBJECT_STORE_CORRUPTED_MSG: \'Git object store may be corrupted\',\n\n        HTTP_AUTH_ERROR: 201,\n        HTTP_AUTH_ERROR_MSG: \'Http authentication failed\',\n\n        UNPACK_ERROR: 202,\n        UNPACK_ERROR_MSG: \'The remote git server wasn\\\'t able to understand the push request.\',\n\n        \n        fileErrorFunc : function(onError){\n            if (!onError){\n                return function(){};\n            }\n            return function(e) {\n                var msg = errors.getFileErrorMsg(e);\n                onError({type : errors.FILE_IO_ERROR, msg: msg, fe: e.code});\n            }\n        },\n\n        ajaxErrorFunc : function(onError){\n            return function(xhr){\n                var url = this.url,\n                    reqType = this.type;\n\n                var httpErr;\n                if (xhr.status == 401){\n                    var auth = xhr.getResponseHeader(\'WWW-Authenticate\');\n                    httpErr = {type: errors.HTTP_AUTH_ERROR, msg: errors.HTTP_AUTH_ERROR_MSG, auth: auth};\n                }\n                else{\n                    httpErr = {type: errors.AJAX_ERROR, url: url, reqType: reqType, statusText: xhr.statusText, status: xhr.status, msg: "Http error with status code: " + xhr.status + \' and status text: "\' + xhr.statusText + \'"\'};\n                }\n                onError(httpErr);  \n            }\n        },\n\n        getFileErrorMsg: function(e) {\n            var msg = \'\';\n\n            switch (e.code) {\n                case FileError.QUOTA_EXCEEDED_ERR:\n                    msg = \'QUOTA_EXCEEDED_ERR\';\n                    break;\n                case FileError.NOT_FOUND_ERR:\n                    msg = \'NOT_FOUND_ERR\';\n                    break;\n                case FileError.SECURITY_ERR:\n                    msg = \'SECURITY_ERR\';\n                    break;\n                case FileError.INVALID_MODIFICATION_ERR:\n                    msg = \'INVALID_MODIFICATION_ERR\';\n                    break;\n                case FileError.INVALID_STATE_ERR:\n                    msg = \'INVALID_STATE_ERR\';\n                    break;\n                case FileError.ABORT_ERR:\n                    msg = \'ABORT_ERR\';\n                    break;\n                case FileError.ENCODING_ERR:\n                    msg = \'ENCODING_ERR\';\n                    break;\n                case FileError.NOT_READABLE_ERR:\n                    msg = \'NOT_READABLE_ERR\';\n                    break;\n                case FileError.NO_MODIFICATION_ALLOWED_ERR:\n                    msg = \'NO_MODIFICATION_ALLOWED_ERR\';\n                    break;\n                case FileError.PATH_EXISTS_ERR:\n                    msg = \'PATH_EXISTS_ERR\';\n                    break;\n                case FileError.SYNTAX_ERR:\n                    msg = \'SYNTAX_ERR\';\n                    break;\n                case FileError.TYPE_MISMATCH_ERR:\n                    msg = \'TYPE_MISMATCH_ERR\';\n                    break;\n                default:\n                    msg = \'Unknown Error \' + e.code;\n                    break;\n            };\n        },\n        errorHandler: function(e) {\n            msg = utils.getFileErrorMsg(e);\n            console.log(\'Error: \' + msg);\n        }\n    }\n    return errors;\n\n});\ndefine(\'utils/progress_chunker\',[],function(){\n\n    var ProgressChunker = function(func){\n        this.master = func;\n    }\n\n    ProgressChunker.prototype = {\n        getChunk : function(start, fraction){\n            var self = this;\n            return function(data){\n                var newPct = start + (data.pct * fraction)\n                self.master({pct: newPct, msg: data.msg});\n            }\n        }\n    }\n\n    return ProgressChunker;\n\n});\ndefine(\'formats/smart_http_remote\',[\'formats/upload_pack_parser\', \'utils/errors\', \'utils/progress_chunker\'], function(UploadPackParser, errutils, ProgressChunker) {\n    // var workerBlob = new Blob([packWorkerText]);\n    // var workerUrl = URL.createObjectURL(workerBlob);\n\n    var SmartHttpRemote = function(store, name, repoUrl, username, password, error) {\n        this.store = store;\n        this.name = name;\n        this.refs = {};\n        this.url = repoUrl.replace(/\\?.*/, "").replace(/\\/$/, "");\n        username = username || "";\n        password = password || "";\n\n        var ajaxErrorHandler = errutils.ajaxErrorFunc(error);\n\n        var parseDiscovery = function(data) {\n            var lines = data.split("\\n")\n            var result = {\n                "refs": []\n            }\n            for (i = 1; i < lines.length - 1; i++) {\n                var thisLine = lines[i]\n                if (i == 1) {\n                    var bits = thisLine.split("\\0")\n                    result["capabilities"] = bits[1]\n                    var bits2 = bits[0].split(" ")\n                    result["refs"].push({\n                        name: bits2[1],\n                        sha: bits2[0].substring(8)\n                    })\n                } else {\n                    var bits2 = thisLine.split(" ")\n                    result["refs"].push({\n                        name: bits2[1],\n                        sha: bits2[0].substring(4)\n                    })\n                }\n            }\n            return result\n        }\n\n        var padWithZeros = function(num) {\n            var hex = num.toString(16);\n            var pad = 4 - hex.length;\n            for (var x = 0; x < pad; x++) {\n                hex = \'0\' + hex;\n            }\n            return hex;\n        }\n\n        var pushRequest = function(refPaths, packData) {\n            \n\n            var pktLine = function(refPath) {\n                return refPath.sha + \' \' + refPath.head + \' \' + refPath.name;\n            }\n            var bb = []; //new BlobBuilder();\n            var str = pktLine(refPaths[0]) + \'\\0report-status\\n\';\n            str = padWithZeros(str.length + 4) + str;\n            bb.push(str);\n            for (var i = 1; i < refPaths.length; i++) {\n                if (!refPaths[i].head) continue;\n                var val = pktLine(refPaths[i]) + \'\\n\';\n                val = padWithZeros(val.length + 4)\n                bb.push(val);\n            }\n            bb.push(\'0000\');\n            bb.push(new Uint8Array(packData));\n            var blob = new Blob(bb);\n            return blob;\n\n        }\n\n        var refWantRequest = function(wantRefs, haveRefs, shallow, depth, moreHaves) {\n            var str = "0067want " + wantRefs[0].sha + " multi_ack_detailed side-band-64k thin-pack ofs-delta\\n"\n            for (var i = 1; i < wantRefs.length; i++) {\n                str += "0032want " + wantRefs[i].sha + "\\n"\n            }\n            if (haveRefs && haveRefs.length) {\n                if (shallow){\n                    str += "0034shallow " + shallow;\n                }\n                str += "0000"\n                _(haveRefs).each(function(haveRef) {\n                    str += "0032have " + haveRef.sha + "\\n"\n                });\n                if (moreHaves) {\n                    str += "0000"\n                } else {\n                    str += "0009done\\n"\n                }\n\n            } else {\n                if (depth){\n                    var depthStr = "deepen " + depth;\n                    str += (padWithZeros(depthStr.length + 4) + depthStr);\n                }\n                str += "0000"\n                str += "0009done\\n"\n            }\n            return str\n        }\n\n        var queryParams = function(uri) {\n            var paramString = uri.split("?")[1]\n            if (!paramString) {\n                return {}\n            }\n\n            var paramStrings = paramString.split("&")\n            var params = {}\n            _(paramStrings).each(function(paramString) {\n                var pair = paramString.split("=")\n                params[pair[0]] = decodeURI(pair[1])\n            })\n            return params\n        }\n\n        this.urlOptions = queryParams(repoUrl);\n\n        function doGet(url, success){\n            var xhr = new XMLHttpRequest();\n            xhr.open("GET", url, true, username, password);\n\n            xhr.onload = function(evt){\n                if (xhr.readyState == 4) {\n                    if (xhr.status == 200) {\n                        success(xhr.responseText);\n                    } \n                    else{ \n                        var obj = {url: url, type: \'GET\'};\n                        ajaxErrorHandler.call(obj, xhr); \n                    }\n                }\n            }\n            \n\n            var xhr2ErrorShim = function(){\n                var obj = {url: url, type: \'POST\'};\n                ajaxErrorHandler.call(obj, xhr); \n            }\n            xhr.onerror = xhr2ErrorShim;\n            xhr.onabort = xhr2ErrorShim;\n            xhr.send();\n        }\n        this.fetchRefs = function(callback) {\n            var remote = this,\n                uri = this.makeUri(\'/info/refs\', {service: "git-upload-pack"});\n            doGet(uri, function(data) {\n                var discInfo = parseDiscovery(data)\n                var i, ref\n                for (i = 0; i < discInfo.refs.length; i++) {\n                    ref = discInfo.refs[i]\n                    remote.addRef(ref.name, ref.sha)\n                }\n                if (callback != "undefined") {\n                    callback(discInfo.refs)\n                }\n            });\n        }\n\n        this.fetchReceiveRefs = function(callback) {\n            var remote = this,\n                uri = this.makeUri(\'/info/refs\', {service: "git-receive-pack"});\n            doGet(uri, function(data) {\n                var discInfo = parseDiscovery(data)\n                var i, ref\n                for (i = 0; i < discInfo.refs.length; i++) {\n                    ref = discInfo.refs[i]\n                    remote.addRef(ref.name, ref.sha)\n                }\n                if (callback != "undefined") {\n                    callback(discInfo.refs)\n                }\n            });\n        }\n\n        this.fetchRef = function(wantRefs, haveRefs, shallow, depth, moreHaves, callback, noCommon, progress) {\n            var url = this.makeUri(\'/git-upload-pack\')\n            var body = refWantRequest(wantRefs, haveRefs, shallow, depth);\n            var thisRemote = this\n            var xhr = new XMLHttpRequest();\n\n            var packProgress, receiveProgress;\n            if (progress){\n                var chunker = new ProgressChunker(progress);\n                receiveProgress = chunker.getChunk(0, 0.2);\n                packProgress = chunker.getChunk(20, 0.8);\n            }\n\n            xhr.open("POST", url, true, username, password);\n            xhr.responseType = \'arraybuffer\';\n            xhr.setRequestHeader("Content-Type", "application/x-git-upload-pack-request");\n\n            xhr.onload = function() {\n\n                var binaryData = xhr.response;\n                if (haveRefs && String.fromCharCode.apply(null, new Uint8Array(binaryData, 4, 3)) == "NAK") {\n                    if (moreHaves) {\n                        thisRemote.store._getCommitGraph(moreHaves, 32, function(commits, next) {\n                            thisRemote.fetchRef(wantRefs, commits, depth, next, callback, noCommon);\n                        });\n                    }\n                    else if (noCommon){\n                        noCommon();\n                    }\n                } else {\n                    if (packProgress){\n                        packProgress({pct: 0, msg: "Parsing pack data"});\n                    }\n                    UploadPackParser.parse(binaryData, store, function(objects, packData, common, shallow) {\n                        if (callback) {\n                            callback(objects, packData, common, shallow);\n                        }\n                    }, packProgress);\n                    // var packWorker = new Worker(workerUrl);\n                    // packWorker.onmessage = function(evt){\n                    //     var msg = evt.data;\n                    //     if (msg.type == GitLiteWorkerMessages.FINISHED && callback){\n                    //         packWorker.terminate();\n                    //         callback(msg.objects, new Uint8Array(msg.data), msg.common);\n                    //     }\n                    //     else if (msg.type == GitLiteWorkerMessages.RETRIEVE_OBJECT){\n                    //         store._retrieveRawObject(msg.sha, "ArrayBuffer", function(baseObject){\n                    //             packWorker.postMessage({type: GitLiteWorkerMessages.OBJECT_RETRIEVED, id: msg.id, object: baseObject}, [baseObject.data]);\n                    //             var x = 0;\n                    //         });\n                    //     }\n                    //     else if (progress && msg.type == GitLiteWorkerMessages.PROGRESS){\n                    //         progress(msg);\n                    //     }\n                    // }\n                    // packWorker.postMessage({type: GitLiteWorkerMessages.START, data:binaryData}, [binaryData]);\n                }\n            }\n            if (receiveProgress){\n                xhr.onprogress = function(evt){\n                    // if (evt.lengthComputable){\n                    //     var pct = evt.loaded / evt.total;\n                    //     receiveProgress({pct: pct, msg: "Received " + evt.loaded + "/" + evt.total + " bytes"});\n                    // }\n                    // else{\n\n                        receiveProgress({pct: 100, msg: "Received " + (evt.loaded/1048576).toFixed(2) + " MB"});\n                    // }\n                }\n            }\n            var xhr2ErrorShim = function(){\n                var obj = {url: url, type: \'POST\'};\n                ajaxErrorHandler.call(obj, xhr); \n            }\n\n            xhr.onerror = xhr2ErrorShim;\n            xhr.onabort = xhr2ErrorShim;\n\n            xhr.send(body);\n\n            //  $.ajax({\n            //    url: url,\n            //    data: body,\n            //    type: "POST",\n            //    contentType: "application/x-git-upload-pack-request",\n            //    beforeSend: function(xhr) {\n            //      xhr.overrideMimeType(\'text/plain; charset=x-user-defined\')\n            //    },\n            //    success: function(data, textStatus, xhr) {\n            //      var binaryData = xhr.responseText\n            //      if (haveRefs && binaryData.indexOf("NAK") == 4){\n            //      \tif (moreHaves){\n            // \tthisRemote.repo._getCommitGraph(moreHaves, 32, function(commits, next){\n            // \t\tthisRemote.fetchRef(wantRefs, commits, next, callback);\n            // \t});\n            // }\n            //      }\n            //      else{\n            // var parser = new Git.UploadPackParser(binaryData, repo)\n            // parser.parse(function(objects, packData, common){\n            // \tif (callback != "undefined") {\n            // \t  callback(objects, packData, common);\n            // \t}\n            // });\n            // }        \n            //    },\n            //    error: function(xhr, data, e) {\n            //      Git.displayError("ERROR Status: " + xhr.status + ", response: " + xhr.responseText)\n            //    }\n            //  });\n        },\n\n        this.pushRefs = function(refPaths, packData, success, progress) {\n            var url = this.makeUri(\'/git-receive-pack\');\n            var body = pushRequest(refPaths, packData);\n            var xhr = new XMLHttpRequest();\n            xhr.open("POST", url, true, username, password);\n            xhr.onload = function(evt) {\n                if (xhr.readyState == 4) {\n                    if (xhr.status == 200) {\n                        var msg = xhr.response;\n                        if (msg.indexOf(\'000eunpack ok\') == 0){\n                            success();\n                        }\n                        else{\n                            error({type: errutils.UNPACK_ERROR, msg: errutils.UNPACK_ERROR_MSG});\n                        }\n                    } \n                    else{ \n                        var obj = {url: url, type: \'POST\'};\n                        ajaxErrorHandler.call(obj, xhr); \n                    }\n                }\n            }\n            xhr.setRequestHeader(\'Content-Type\', \'application/x-git-receive-pack-request\');\n            var bodySize = (body.size/1024).toFixed(2);\n            xhr.upload.onprogress = function(evt){\n                progress({pct: evt.loaded/body.size * 100, msg: \'Sending \' + (evt.loaded/1024).toFixed(2) + \'/\' + bodySize + " KB"});\n            }\n            xhr.send(body);\n            /*Gito.FileUtils.readBlob(body, \'BinaryString\', function(strData){\n    \t\t$.ajax({\n    \t\t\turl : url,\n    \t\t\tdata : strData,\n    \t\t\ttype : \'POST\',\n    \t\t\tcontentType : \'application/x-git-receive-pack-request\',\n    \t\t\tprocessData : false,\n    \t\t\t//mimeType :\'text/plain; charset=x-user-defined\',\n  \t\t  \tsuccess : function(data, textstatus, xhr){\n  \t\t  \t\tvar x = 0;\n  \t\t  \t},\n  \t\t  \terror : function (xhr, data, e){\n  \t\t  \t\tGit.displayError("ERROR Status: " + xhr.status + ", response: " + xhr.responseText)\n  \t\t  \t}\n    \t\t});\n    \t});*/\n        }\n\n        this.makeUri = function(path, extraOptions) {\n            var uri = this.url + path\n            var options = _(this.urlOptions).extend(extraOptions || {})\n            if (options && _(options).size() > 0) {\n                var optionKeys = _(options).keys()\n                var optionPairs = _(optionKeys).map(function(optionName) {\n                    return optionName + "=" + encodeURI(options[optionName])\n                })\n\n                return uri + "?" + optionPairs.join("&")\n            } else {\n                return uri\n            }\n        }\n\n        // Add a ref to this remote. fullName is of the form:\n        //   refs/heads/master or refs/tags/123\n        this.addRef = function(fullName, sha) {\n            var type, name\n            if (fullName.slice(0, 5) == "refs/") {\n                type = fullName.split("/")[1]\n                name = this.name + "/" + fullName.split("/")[2]\n            } else {\n                type = "HEAD"\n                name = this.name + "/" + "HEAD"\n            }\n            this.refs[name] = {\n                name: name,\n                sha: sha,\n                remote: this,\n                type: type\n            }\n        }\n\n        this.getRefs = function() {\n            return _(this.refs).values()\n        }\n\n        this.getRef = function(name) {\n            return this.refs[this.name + "/" + name]\n        }\n    }\n    return SmartHttpRemote;\n});\n\ndefine(\'formats/pack_index\',[],function(){\n    // This object partially parses the data contained in a pack-*.idx file, and provides\n    // access to the offsets of the objects the packfile and the crc checksums of the objects.\n    PackIndex = function(buf) {\n    \tvar data = new DataView(buf);\n    \tthis.data = data;\n    \t// load the index into memory\n    \tvar magicNum = data.getUint32(0, false);\n    \tvar versionNum = data.getUint32(4, false);\n    \t\n    \tif (magicNum != 0xff744f63 || versionNum != 2){\n    \t\tthrow(Error("Bad pack index header. Only version 2 is supported"))\n    \t}\n    \t\n    \tvar byteOffset = 8\n    \t//this.fanOffset = byteOffset;\n    \t\n    \tvar numObjects = data.getUint32(byteOffset + (255 * 4), false);\n    \t\n    \t// skip past fanout table\n    \tvar fanTableLen = 256 * 4;\n    \tbyteOffset += fanTableLen; \n    \tvar shaTableLen = numObjects * 20;\n    \tthis.shaList = new Uint8Array(buf, byteOffset, shaTableLen);\n    \t\n    \t// skip past shas and the CRC vals\n    \tbyteOffset += shaTableLen + (numObjects * 4);\n    \t\n    \tthis.offsetsOffset = byteOffset;\n    \tthis.numObjects = numObjects;\n    }\n\n    PackIndex.prototype = {\n    \t_compareShas : function(sha1, sha2){\n    \t\t // assume the first byte has been matched in the fan out table\n    \t\tfor (var i = 1; i < 20; i++){\n    \t\t\tif (sha1[i] != sha2[i]){\n    \t\t\t\treturn sha1[i] - sha2[i];\n    \t\t\t}\n    \t\t}\n    \t\treturn 0;\n    \t},\n    \t_getShaAtIndex : function(index){\n    \t    var byteOffset = index * 20;\n    \t    return this.shaList.subarray(byteOffset, byteOffset + 20); \n    \t},\n    \tgetObjectOffset : function(sha){\n    \t\tvar fanIndex = sha[0];\n    \t\t\n    \t\tvar sliceStart = fanIndex > 0 ? (this.data.getUint32(8 + ((fanIndex - 1) * 4), false)) : 0;\n    \t\tvar sliceEnd = this.data.getUint32(8 + (fanIndex * 4), false);\n    \t\t\n    \t\tif (sliceEnd - sliceStart == 0){\n    \t\t\treturn -1;\n    \t\t}\n    \t\t\n    \t\tvar index;\n    \t\twhile (sliceEnd - sliceStart  > 1){\n    \t\t\tvar split = sliceStart + Math.floor(((sliceEnd - sliceStart)/2));\n    \t\t\t\n    \t\t\tvar mid = this._getShaAtIndex(split);\n    \t\t\t\n    \t\t\tvar compare = this._compareShas(sha, mid);\n    \t\t\tif (compare == 0){\n    \t\t\t\tindex = split;\n    \t\t\t\tbreak;\n    \t\t\t}\n    \t\t\telse if (compare < 0){\n    \t\t\t\tsliceEnd = split;\n    \t\t\t}\n    \t\t\telse{\n    \t\t\t\tsliceStart = split + 1;\n    \t\t\t}\n    \t\t}\n    \t\t// if we\'ve exited the loop without a match, sliceStart should hold the index or it\'s not here. \n    \t\tif (!index){\n    \t\t\tif (sliceStart < this.numObjects && this._compareShas(sha, this._getShaAtIndex(sliceStart)) == 0){\n    \t\t\t\tindex = sliceStart;\n    \t\t\t}\n    \t\t\telse{\n    \t\t\t\treturn -1;\n    \t\t\t}\n    \t\t}\n    \t\t\n    \t\tvar objOffset = this.data.getUint32(this.offsetsOffset + (index * 4), false); \n    \t\treturn objOffset;\n    \t\t\n    \t}\n    }\n    /*\n    * = Version 2 pack-*.idx files support packs larger than 4 GiB, and\n    *  have some other reorganizations.  They have the format:\n    *\n    *  - A 4-byte magic number \'\\377tOc\' which is an unreasonable\n    *    fanout[0] value.\n    *\n    *  - A 4-byte version number (= 2)\n    *\n    *  - A 256-entry fan-out table just like v1.\n    *\n    *  - A table of sorted 20-byte SHA1 object names.  These are\n    *    packed together without offset values to reduce the cache\n    *    footprint of the binary search for a specific object name.\n    *\n    *  - A table of 4-byte CRC32 values of the packed object data.\n    *    This is new in v2 so compressed data can be copied directly\n    *    from pack to pack during repacking without undetected\n    *    data corruption.\n    *\n    *  - A table of 4-byte offset values (in network byte order).\n    *    These are usually 31-bit pack file offsets, but large\n    *    offsets are encoded as an index into the next table with\n    *    the msbit set.\n    *\n    *  - A table of 8-byte offset entries (empty for pack files less\n    *    than 2 GiB).  Pack files are organized with heavily used\n    *    objects toward the front, so most object references should\n    *    not need to refer to this table.\n    *\n    *  - The same trailer as a v1 pack file:\n    *\n    *    A copy of the 20-byte SHA1 checksum at the end of\n    *    corresponding packfile.\n    *\n    *    20-byte SHA1-checksum of all of the above.\n    */\n    PackIndex.writePackIdx = function(objects, packSha){\n    \tvar size = 4 + 4 + (256 * 4) + (objects.length * 20) + (objects.length * 4)  + (objects.length * 4) + (20 * 2);\n    \tvar buf = new ArrayBuffer(size);\n    \t\n    \tobjects.sort(function(obj1, obj2){\n    \t\tfor (var i = 0; i < 20; i++){\n    \t\t\tif (obj1.sha[i] != obj2.sha[i]){\n    \t\t\t\treturn obj1.sha[i] - obj2.sha[i];\n    \t\t\t}\n    \t\t}\n    \t\treturn 0; // shouldn\'t happen but just in case\n    \t});\n    \t\n        var data = new DataView(buf);\n        \n        // magic number\n        data.setUint32(0, 0xff744f63, false);\n        \n        //version number\n        data.setUint32(4, 2, false);\n        \n        // fan table\n        var byteOffset = 8, current = 0;\n     \n        for (var i = 0; i < objects.length; i++){\n        \tvar next = objects[i].sha[0];\n        \tif (next != current){\n        \t\t\n        \t\tfor (var j = current; j < next; j++){\n        \t\t\tdata.setUint32(byteOffset + (j * 4), i, false);\n        \t\t}\n        \t}\n        \tcurrent = next;\n        }\n        for (var j = current; j < 256; j++){\n    \t\tdata.setUint32(byteOffset + (j * 4), objects.length, false);\n    \t}\n        \n        // list of shas\n        byteOffset += (256 * 4);\n        \n        for (var i = 0; i < objects.length; i++){\n        \tfor (var j = 0; j < 20; j++){\n        \t\tdata.setUint8(byteOffset++, objects[i].sha[j]);\n        \t}\n        }\n        \n        // list of crcs\n        for (var i = 0; i < objects.length; i++){\n        \tdata.setUint32(byteOffset, objects[i].crc, false);\n        \tbyteOffset += 4;\n        }\n        \n        // list of offsets. Note that I\'m not going to bother with large offsets. You shouldn\'t be loading a packfile >2GB inside a web browser anyway.\n        for (var i = 0; i < objects.length; i++){\n        \tdata.setUint32(byteOffset, objects[i].offset, false);\n        \tbyteOffset += 4;\n        }\n        \n        // the pack file sha\n        for (var i = 0; i < 20; i++){\n        \tdata.setUint8(byteOffset++, packSha[i]);\n        }\n        \n        // sha for all of the above\n        var indexSha = Crypto.SHA1(new Uint8Array(buf, 0, byteOffset), {asBytes:true});\n        for (var i = 0; i < 20; i++){\n        \tdata.setUint8(byteOffset++, indexSha[i]);\n        }\n        return buf;\n    }\n      \n    return PackIndex;\n});\n\n\ndefine(\'commands/clone\',[\'commands/object2file\', \'formats/smart_http_remote\', \'formats/pack_index\', \'formats/pack\', \'utils/file_utils\', \'utils/errors\', \'utils/progress_chunker\'], function(object2file, SmartHttpRemote, PackIndex, Pack, fileutils, errutils, ProgressChunker){\n    \n    var _createCurrentTreeFromPack = function(dir, store, headSha, callback){\n         store._retrieveObject(headSha, "Commit", function(commit){\n            var treeSha = commit.tree;\n            object2file.expandTree(dir, store, treeSha, callback);\n         });\n    }\n    \n    var checkDirectory = function(dir, store, success, error, ferror){\n        fileutils.ls(dir, function(entries){\n            \n            if (entries.length == 0){\n                error({type: errutils.CLONE_DIR_NOT_INTIALIZED, msg: errutils.CLONE_DIR_NOT_INTIALIZED_MSG});\n            }\n            else if (entries.length != 1 || entries[0].isFile || entries[0].name != \'.git\'){\n                error({type: errutils.CLONE_DIR_NOT_EMPTY, msg: errutils.CLONE_DIR_NOT_EMPTY_MSG});\n            }\n            else{\n                fileutils.ls(store.objectsDir, function(entries){\n                    if (entries.length > 1){\n                        error({type: errutils.CLONE_GIT_DIR_IN_USE, msg: errutils.CLONE_GIT_DIR_IN_USE_MSG});\n                    }\n                    else if (entries.length == 1){\n                        if (entries[0].name == "pack"){\n                            store.objectsDir.getDirectory(\'pack\', {create: false}, function(packDir){\n                                fileutils.ls(packDir, function(entries){\n                                    if (entries.length > 0){\n                                        error({type: errutils.CLONE_GIT_DIR_IN_USE, msg: errutils.CLONE_GIT_DIR_IN_USE_MSG});\n                                    }\n                                    else{\n                                        success();\n                                    }\n                                }, ferror);\n                            }, success);\n                        }\n                        else{\n                            error({type: errutils.CLONE_GIT_DIR_IN_USE, msg: errutils.CLONE_GIT_DIR_IN_USE_MSG});\n                        }\n                    }\n                    else{\n                        success();\n                    }\n                }, ferror);\n            }\n\n        }, ferror);\n    };\n\n    var clone = function(options, success, error){\n        \n        var dir = options.dir,\n            store = options.objectStore,\n            url = options.url,\n            callback = success,\n            depth = options.depth,\n            branch = options.branch || \'master\',\n            progress = options.progress || function(){},\n            username = options.username,\n            password = options.password,\n            ferror = errutils.fileErrorFunc(error);\n    \n        var chunker = new ProgressChunker(progress);\n        var packProgress = chunker.getChunk(0, .95);\n\n        var mkdirs = fileutils.mkdirs,\n            mkfile = fileutils.mkfile,\n            remote = new SmartHttpRemote(store, "origin", url, username, password, error);\n\n        var createInitialConfig = function(shallow, localHeadRef, callback){\n            var config = {url: url, time: new Date()};\n            if (options.depth && shallow){\n                config.shallow = shallow;\n            }\n            config.remoteHeads = {};\n            \n            if (localHeadRef)\n                config.remoteHeads[localHeadRef.name] = localHeadRef.sha;\n\n            store.setConfig(config, callback);  \n        }\n\n        checkDirectory(dir, store, function(){ \n            mkdirs(dir, ".git", function(gitDir){\n                remote.fetchRefs(function(refs){\n                    \n                    if (!refs.length){\n                        createInitialConfig(null, null, success);\n                        return;\n                    }\n\n                    var remoteHead, remoteHeadRef, localHeadRef;\n\n                    _(refs).each(function(ref){\n                        if (ref.name == "HEAD"){\n                            remoteHead = ref.sha;\n                        }\n                        else if (ref.name == "refs/heads/" + branch){\n                            localHeadRef = ref;\n                        }\n                        else if (ref.name.indexOf("refs/heads/") == 0){\n                            if (ref.sha == remoteHead){\n                                remoteHeadRef = ref;\n                            }\n                        }\n                    });\n\n                    if (!localHeadRef){\n                        if (options.branch){\n                            error({type: errutils.REMOTE_BRANCH_NOT_FOUND, msg: errutils.REMOTE_BRANCH_NOT_FOUND_MSG});\n                            return;\n                        }\n                        else{\n                            localHeadRef = remoteHeadRef;\n                        }\n                    }\n\n                    mkfile(gitDir, "HEAD", \'ref: \' + localHeadRef.name + \'\\n\', function(){\n                        mkfile(gitDir, localHeadRef.name, localHeadRef.sha + \'\\n\', function(){\n                            remote.fetchRef([localHeadRef], null, null, depth, null, function(objects, packData, common, shallow){\n                                var packSha = packData.subarray(packData.length - 20);\n                                \n                                var packIdxData = PackIndex.writePackIdx(objects, packSha);\n                                \n                                // get a view of the sorted shas\n                                var sortedShas = new Uint8Array(packIdxData, 4 + 4 + (256 * 4), objects.length * 20);\n                                packNameSha = Crypto.SHA1(sortedShas);\n                                \n                                var packName = \'pack-\' + packNameSha;\n                                mkdirs(gitDir, \'objects\', function(objectsDir){\n                                    mkfile(objectsDir, \'pack/\' + packName + \'.pack\', packData.buffer);\n                                    mkfile(objectsDir, \'pack/\' + packName + \'.idx\', packIdxData);\n                                    \n                                    var packIdx = new PackIndex(packIdxData);\n                                    store.loadWith(objectsDir, [{pack: new Pack(packData, self), idx: packIdx}]);\n                                    progress({pct: 95, msg: "Building file tree from pack. Be patient..."});\n                                    _createCurrentTreeFromPack(dir, store, localHeadRef.sha, function(){\n                                        createInitialConfig(shallow, localHeadRef, callback);\n                                    });\n                                }, ferror); \n                            }, null, packProgress);\n                        }, ferror);\n                    }, ferror);\n                });\n            }, ferror);\n        }, error, ferror);\n    }\n    return clone;\n});\ndefine(\'commands/commit\',[\'utils/file_utils\', \'utils/misc_utils\', \'utils/errors\'], function (fileutils, miscutils, errutils) {\n\n    var walkFiles = function(dir, store, success){\n               \n        fileutils.ls(dir, function(entries){\n            if (!entries.length){\n                success();\n                return;\n            }\n\n            var treeEntries = [];\n            entries.asyncEach(function(entry, done){\n                if (entry.name == \'.git\'){\n                    done();\n                    return;\n                }\n                if (entry.isDirectory){\n                    walkFiles(entry, store, function(sha){\n                        if (sha){\n                            treeEntries.push({name: /*\'40000 \' + */entry.name, sha: miscutils.convertShaToBytes(sha), isBlob: false});\n                        }\n                        done();\n                    });\n                    \n                }\n                else{\n                    entry.file(function(file){\n                        var reader = new FileReader();\n                        reader.onloadend = function(){\n                            store.writeRawObject(\'blob\', new Uint8Array(reader.result), function(sha){\n                                treeEntries.push({name: /*\'100644 \' + */entry.name, sha: miscutils.convertShaToBytes(sha), isBlob: true});\n                                done();\n                            });\n                        }\n                        reader.readAsArrayBuffer(file);\n                    });\n                }\n            },\n            function(){\n                treeEntries.sort(function(a,b){\n                    //http://permalink.gmane.org/gmane.comp.version-control.git/195004\n                    var aName = a.isBlob ? a.name : (a.name + \'/\');\n                    var bName = b.isBlob ? b.name : (b.name + \'/\');\n                    if (aName < bName) return -1;\n                    else if (aName > bName) return 1;\n                    else\n                    return 0;\n                });\n                store._writeTree(treeEntries, success);\n            })\n        });       \n    }\n\n    var checkTreeChanged = function(store, parent, sha, success, error){\n        if (!parent || !parent.length){\n            success();\n        }\n        else{\n            store._retrieveObject(parent, "Commit", function(parentCommit){\n                var oldTree = parentCommit.tree;\n                if (oldTree == sha){\n                    error({type: errutils.COMMIT_NO_CHANGES, msg: errutils.COMMIT_NO_CHANGES_MSG});\n                }\n                else{\n                    success();\n                }\n            }, function(){\n                error({type: errutils.OBJECT_STORE_CORRUPTED, msg: errutils.OBJECT_STORE_CORRUPTED_MSG});  \n            })\n        }\n    }\n\n    var _createCommitFromWorkingTree =  function(options, parent, ref, success, error){ \n\n        var dir = options.dir,\n            store = options.objectStore,\n            username = options.username,\n            email = options.email,\n            commitMsg = options.commitMsg;\n\n        walkFiles(dir, store, function(sha){\n            checkTreeChanged(store, parent, sha, function(){\n                var now = new Date();\n                var dateString = Math.floor(now.getTime()/1000);\n                var offset = now.getTimezoneOffset()/-60;\n                var absOffset = Math.abs(offset);\n                var offsetStr = \'\' + (offset < 0 ? \'-\' : \'+\') + (absOffset < 10 ? \'0\' : \'\') + absOffset + \'00\';\n                dateString = dateString + \' \' + offsetStr;\n                var commitContent = [\'tree \',sha,\'\\n\'];\n                if (parent && parent.length){\n                    commitContent.push(\'parent \', parent);\n                    if (parent.charAt(parent.length - 1) != \'\\n\'){\n                        commitContent.push(\'\\n\');\n                    }\n                }\n                    \n                commitContent.push(\'author \', username, \' <\',email, \'> \',  dateString,\'\\n\', \n                    \'committer \', username,\' <\', email, \'> \', dateString, \'\\n\\n\', commitMsg,\'\\n\');\n                store.writeRawObject(\'commit\', commitContent.join(\'\'), function(commitSha){\n                    fileutils.mkfile(dir, \'.git/\' + ref, commitSha + \'\\n\', function(){\n                        store.updateLastChange(null, function(){\n                            success(commitSha);\n                        });\n                    });\n                });\n            }, error);\n        });\n    }\n\n    var commit = function(options, success, error){\n        var rootDir = options.dir,\n            objectStore = options.objectStore;\n\n        var ref;\n        var buildCommit = function(parent){\n            _createCommitFromWorkingTree(options, parent, ref, success, error);\n        }\n        objectStore.getHeadRef(function(headRef){\n            ref = headRef;\n            objectStore._getHeadForRef(ref, buildCommit, function(){ buildCommit(); });\n        });\n    }\n\n    return commit;\n\n});\ndefine(\'commands/init\',[],function(){\n    var init = function(options, success, error){\n        var objectStore = options.objectStore;\n        objectStore.init(success, error);\n    }\n    return init; \n});\ndefine(\'commands/treemerger\',[\'utils/misc_utils\'], function(utils){\n\n\tvar diffTree = function(oldTree, newTree){\n\t\toldTree.sortEntries();\n\t\tnewTree.sortEntries();\n\t\t\n\t\tvar oldEntries = oldTree.entries, \n\t\t\tnewEntries = newTree.entries;\n\t\tvar oldIdx = newIdx = 0;\n\t\t\n\t\tvar remove = [],\n\t\t\tadd = [], \n\t\t\tmerge = [];\n\t\t\n\t\t\n\t\t\n\t\twhile (true){\n\t\t\tvar nu = newEntries[newIdx];\n\t\t\tvar old = oldEntries[oldIdx];\n\t\t\t\n\t\t\tif (!nu){\n\t\t\t\tif (!old){\n\t\t\t\t\tbreak;\n\t\t\t\t}\n\t\t\t\tremove.push(old);\n\t\t\t\toldIdx++;\n\t\t\t}\n\t\t\telse if (!old){\n\t\t\t\tadd.push(nu);\n\t\t\t\tnewIdx++;\n\t\t\t}\n\t\t\telse if (nu.name < old.name){\n\t\t\t\tadd.push(nu);\n\t\t\t\tnewIdx++;\n\t\t\t}\n\t\t\telse if (nu.name > old.name){\n\t\t\t\tremove.push(old);\n\t\t\t\toldIdx++;\n\t\t\t}\n\t\t\telse{\n\t\t\t\tif (utils.compareShas(nu.sha,old.sha) != 0){\n\t\t\t\t\tmerge.push({nu:nu, old:old});\n\t\t\t\t}\n\t\t\t\toldIdx++;\n\t\t\t\tnewIdx++;\n\t\t\t}\n\t\t}\n\t\treturn {add:add, remove:remove, merge: merge};\n\t};\n\t\n\t var mergeTrees = function(store, ourTree, baseTree, theirTree, success, error){\n\t\t\n\t\tvar finalTree = [], \n\t\t\tnext = null;\n\t\t\tindices = [0,0,0],\n\t\t\tconflicts = [];\n\t\t\n\t\t\n\t\t\n\t\t// base tree can be null if we\'re merging a sub tree from ours and theirs with the same name\n\t\t// but it didn\'t exist in base. \n\t\tif (baseTree == null){\n\t\t\tbaseTree = {entries:[], sortEntries: function(){}};\n\t\t}\n\t\t\n\t\tourTree.sortEntries();\n\t\ttheirTree.sortEntries();\n\t\tbaseTree.sortEntries();\n\t\t\n\t\tvar allTrees = [ourTree.entries, baseTree.entries, theirTree.entries];\n\t\t\n\t\twhile (conflicts.length == 0){\n\t\t\tnext = null;\n\t\t\tvar nextX = 0;\n\t\t\tfor (var x = 0; x < allTrees.length; x++){\n\t\t\t\tvar treeEntries = allTrees[x];\n\t\t\t\tvar top = treeEntries[indices[x]];\n\t\t\t\t\n\t\t\t\tif (!next || (top && top.name < next.name)){\n\t\t\t\t\tnext = top;\n\t\t\t\t\tnextX = x;\n\t\t\t\t}\n\t\t\t}\n\t\t\t\n\t\t\tif (!next){\n\t\t\t\tbreak;\n\t\t\t}\n\t\t\t\n\t\t\tfunction shasEqual(sha1, sha2){\n\t\t\t\tfor (var i = 0; i < sha1.length; i++){\n\t\t\t\t\tif (sha1[i] != sha2[i]){\n\t\t\t\t\t\treturn false;\n\t\t\t\t\t}\n\t\t\t\t}\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\tswitch (nextX){\n\t\t\t\tcase 0:\n\t\t\t\t\tvar theirEntry = allTrees[2][indices[2]];\n\t\t\t\t\tvar baseEntry = allTrees[1][indices[1]];\n\t\t\t\t\tif (theirEntry.name == next.name){\n\t\t\t\t\t\tif (!shasEqual(theirEntry.sha,next.sha)){\n\t\t\t\t\t\t\tif (baseEntry.name != next.name){\n\t\t\t\t\t\t\t\tbaseEntry = {entries:[]};\n\t\t\t\t\t\t\t\tif (next.isBlob){\n\t\t\t\t\t\t\t\t\tconflicts.push({conflict:true, ours: next, base: null, theirs: theirEntry});\n\t\t\t\t\t\t\t\t\tbreak;\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\tif (next.isBlob === theirEntry.isBlob && (baseEntry.isBlob === next.isBlob)){\n\t\t\t\t\t\t\t\tif (shasEqual(next.sha, baseEntry.sha)){\n\t\t\t\t\t\t\t\t\tfinalTree.push(theirEntry);\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\telse{\n\t\t\t\t\t\t\t\t\tfinalTree.push({merge:true, ours: next, base: baseEntry, theirs: theirEntry});\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\telse{\n\t\t\t\t\t\t\t\tconflicts.push({conflict:true, ours: next, base: baseEntry, theirs: theirEntry});\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t}\n\t\t\t\t\t\telse{\n\t\t\t\t\t\t\tfinalTree.push(next);\n\t\t\t\t\t\t}\n\t\t\t\t\t}\n\t\t\t\t\telse if (baseEntry.name == next.name){\n\t\t\t\t\t\tif (!shasEqual(baseEntry.sha, next.sha)){\n\t\t\t\t\t\t\t//deleted from theirs but changed in ours. Delete/modify conflict.\n\t\t\t\t\t\t\tconflicts.push({conflict:true, ours: next, base: baseEntry, theirs: null});\n\t\t\t\t\t\t}\n\t\t\t\t\t}\n\t\t\t\t\telse{\n\t\t\t\t\t\tfinalTree.push(next);\n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\t\t\t\tcase 1:\n\t\t\t\t\tvar theirEntry = allTrees[indices[2]];\n\t\t\t\t\tif (next.name == theirEntry.name && !shasEqual(next.sha, theirEntry.sha)){\n\t\t\t\t\t\t// deleted from ours but changed in theirs. Delete/modify conflict\n\t\t\t\t\t\tconflicts.push({conflict: true, ours: null, base: next, theirs: theirEntry}); \n\t\t\t\t\t}\n\t\t\t\t\tbreak;\n\t\t\t\tcase 2:\n\t\t\t\t\tfinalTree.push(next);\n\t\t\t\t\tbreak;\n\t\t\t}\n\t\t\t\n\t\t\tfor (var x = 0; x < allTrees.length; x++){\n\t\t\t\tvar treeEntries = allTrees[x];\t\n\t\t\t\tif (treeEntries[indices[x]].name == next.name){\n\t\t\t\t\tindices[x]++;\n\t\t\t\t}\n\t\t\t}\n\t\t\t\n\t\t}\n\t\t\n\t\tif (conflicts.length){\n\t\t\terror(conflicts);\n\t\t}\n\t\t\n\t\t//var mergeBlobs = function(\n\t\tvar self = this;\n\t\t\n\t\tfinalTree.asyncEach(function(item, done, index){\n\t\t\tif (item.merge){\n\t\t\t\t\n\t\t\t\tvar shas = [item.ours.sha, item.base.sha, item.theirs.sha];\n\t\t\t\tif (item.ours.isBlob){\n\t\t\t\t\tstore._retrieveBlobsAsStrings(shas, function(blobs){\n\t\t\t\t\t\tvar newBlob = Diff.diff3_dig(blobs[0].data, blobs[1].data, blobs[2].data); \n\t\t\t\t\t\tif (newBlob.conflict){\n\t\t\t\t\t\t\tconflicts.push(newBlob);\n\t\t\t\t\t\t\tdone();\n\t\t\t\t\t\t}\n\t\t\t\t\t\telse{\n\t\t\t\t\t\t\tstore.writeRawObject(objectDir, \'blob\', newBlob.text, function(sha){\n\t\t\t\t\t\t\t\tfinalTree[index].sha = sha;\n\t\t\t\t\t\t\t\tdone(); \n\t\t\t\t\t\t\t});\n\t\t\t\t\t\t}\n\t\t\t\t\t\t\n\t\t\t\t\t});\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tstore._retrieveObjectList(shas, \'Tree\', function(trees){\n\t\t\t\t\t\tself.mergeTrees(trees[0], trees[1], trees[2], function(mergedSha){\n\t\t\t\t\t\t\tfinalTree[index] = item.ours;\n\t\t\t\t\t\t\titem.ours.sha = mergedSha;\n\t\t\t\t\t\t\tdone();\n\t\t\t\t\t\t},\n\t\t\t\t\t\tfunction(newConflicts){\n\t\t\t\t\t\t\tconflicts = conflicts.concat(newConflicts);\n\t\t\t\t\t\t\tdone();\n\t\t\t\t\t\t});\n\t\t\t\t\t});\n\t\t\t\t}\n\t\t\t\t\n\t\t\t}\n\t\t\telse{\n\t\t\t\tdone();\n\t\t\t}\n\t\t},\n\t\tfunction(){\n\t\t\tif (!conflicts.length){\n\t\t\t\t//Gito.FileUtils.mkdirs(self.dir, \'.git/objects\', function(objectDir){\n\t\t\t\tstore._writeTree(finalTree, success);\n\t\t\t\t//});\n\t\t\t\t//success(finalTree)\n\t\t\t}\n\t\t\telse{\n\t\t\t\terror(conflicts);\n\t\t\t}\n\t\t});\n\t\t\n\t}\n\treturn {\n\t\tmergeTrees : mergeTrees,\n\t\tdiffTree : diffTree\n\t}\n\t\n});\n\ndefine(\'commands/conditions\',[\'utils/file_utils\', \'utils/errors\'],function(fileutils, errutils){\n\n    var conditions = {\n\n        checkForUncommittedChanges : function(dir, store, callback, error){\n            var lastUpdate;\n            var walkDir = function(dir, callback){\n                \n                dir.getMetadata(function(md){\n                    if (md.modificationTime > lastUpdate){\n                        callback(true);\n                        return;\n                    }\n                    fileutils.ls(dir, function(entries){\n                        var changed;\n                        entries.asyncEach(function(entry, done){\n                            if (changed){\n                                done();\n                                return;\n                            }\n\n                            if (entry.isDirectory){\n                                if (entry.name == \'.git\'){\n                                    done();\n                                    return;\n                                }\n                                entry.getMetadata(function(md){\n                                    walkDir(entry, function(isChanged){\n                                        changed |= isChanged;\n                                        done();\n                                    });\n                                }, done);\n                            }\n                            else{\n                                entry.getMetadata(function(md){\n                                    if (md.modificationTime > lastUpdate){\n                                        changed = true;\n                                    }\n                                    done();\n                                }, done);\n                                \n                            }\n                        },function(){\n                            callback(changed);\n                        });\n                    });\n                });\n            };\n\n            store.getConfig(function(config){\n                // this would mean we have no commits.\n                if (!config.time){\n                    config.time = 1;\n                }\n                lastUpdate = new Date(config.time);\n                walkDir(dir, function(changed){\n                    if (changed){\n                        error({type: errutils.UNCOMMITTED_CHANGES, msg: errutils.UNCOMMITTED_CHANGES_MSG});\n                    }\n                    else{\n                        callback(config);\n                    }\n                });\n            });\n        }\n    }\n    return conditions;\n});\ndefine(\'commands/pull\',[\'commands/treemerger\', \'commands/object2file\', \'commands/conditions\', \'formats/smart_http_remote\', \'formats/pack_index\', \'formats/pack\', \'utils/file_utils\', \'utils/errors\', \'utils/progress_chunker\'], function(treeMerger, object2file, Conditions, SmartHttpRemote, PackIndex, Pack, fileutils, errutils, ProgressChunker){\n    \n\n    var _updateWorkingTree = function (dir, store, fromTree, toTree, success){\n        \n        var processOps = function(rootDir, ops, callback){\n            ops.remove.asyncEach(function(entry, done){\n                var rm = entry.isBlob ? fileutils.rmFile : fileutils.rmDir;\n                rm(rootDir, entry.name, done);\n            },\n            function(){\n                ops.add.asyncEach(function(entry, done){\n                    if (!entry.isBlob){\n                        fileutils.mkdirs(rootDir, entry.name, function(dirEntry){\n                            object2file.expandTree(dirEntry, store, entry.sha, done);\n                        });\n                    }\n                    else{\n                        object2file.expandBlob(rootDir, store, entry.name, entry.sha, done); \n                    }\n                },\n                function(){\n                    ops.merge.asyncEach(function(entry, done){\n                        if (entry.nu.isBlob){\n                            object2file.expandBlob(rootDir, store, entry.nu.name, entry.nu.sha, done); \n                        }\n                        else{\n                            store._retrieveObjectList([entry.old.sha, entry.nu.sha], \'Tree\', function(trees){\n                                var newOps = treeMerger.diffTree(trees[0], trees[1]);\n                                fileutils.mkdirs(rootDir, entry.nu.name, function(dirEntry){\n                                    processOps(dirEntry, newOps, done);\n                                });\n                            });\n                        }\n                    },\n                    function(){\n                        callback();\n                    });\n                });\n            });\n        }\n        \n        \n        var ops = treeMerger.diffTree(fromTree, toTree);\n        processOps(dir, ops, success);\n    \n    };\n\n    \n\n    var pull = function(options, success, error){\n\n        var dir = options.dir,\n            store = options.objectStore,\n            username = options.username,\n            password = options.password,\n            progress = options.progress || function(){},\n            callback = success,\n            ferror = errutils.fileErrorFunc(error);\n\n        var mkdirs = fileutils.mkdirs,\n            mkfile = fileutils.mkfile;\n\n        var fetchProgress;\n        if (options.progress){\n            var chunker = new ProgressChunker(progress);\n            fetchProgress = chunker.getChunk(20, 0.5);\n        }\n        else{\n            fetchProgress = function(){};\n        }\n\n        progress({pct: 0, msg: \'Checking for uncommitted changes...\'});\n        Conditions.checkForUncommittedChanges(dir, store, function(repoConfig){\n            var url = repoConfig.url;\n\n            remote = new SmartHttpRemote(store, "origin", url, username, password, error);\n        \n            var nonFastForward = function(){\n                error({type: errutils.PULL_NON_FAST_FORWARD, msg: errutils.PULL_NON_FAST_FORWARD_MSG});\n            };\n\n            var upToDate = function(){\n                error({type: errutils.PULL_UP_TO_DATE, msg: errutils.PULL_UP_TO_DATE_MSG});\n            };\n\n            // get the current branch\n            fileutils.readFile(dir, \'.git/HEAD\', \'Text\', function(headStr){\n                \n                progress({pct: 10, msg: \'Querying remote git server...\'});\n                // get rid of the initial \'ref: \' plus newline at end\n                var headRefName = headStr.substring(5).trim();\n                remote.fetchRefs(function(refs){\n                    var headSha, branchRef, wantRef;\n                    \n                    refs.some(function(ref){\n                        if (ref.name == headRefName){\n                            branchRef = ref;\n                            return true;\n                        }\n                    });\n\n                    if (branchRef){\n                         // see if we know about the branch\'s head commit if so, we\'re up to date, if not, request from remote\n                        store._retrieveRawObject(branchRef.sha, \'ArrayBuffer\', upToDate, function(){\n                            wantRef = branchRef;\n                            // Get the sha from the ref name \n                            store._getHeadForRef(branchRef.name, function(sha){\n                                branchRef.localHead = sha;\n                                \n                                store._getCommitGraph([sha], 32, function(commits, nextLevel){\n                                    remote.fetchRef([wantRef], commits, repoConfig.shallow, null, nextLevel, function(objects, packData, common){\n                                        // fast forward merge\n                                        if (common.indexOf(wantRef.localHead) != -1){\n                                            var packSha = packData.subarray(packData.length - 20);\n                                            \n                                            var packIdxData = PackIndex.writePackIdx(objects, packSha);\n                                            \n                                            // get a view of the sorted shas\n                                            var sortedShas = new Uint8Array(packIdxData, 4 + 4 + (256 * 4), objects.length * 20);\n                                            packNameSha = Crypto.SHA1(sortedShas);\n                                            \n                                            var packName = \'pack-\' + packNameSha;\n                                            mkdirs(store.dir, \'.git/objects\', function(objectsDir){\n                                                store.objectsDir = objectsDir;\n                                                mkfile(objectsDir, \'pack/\' + packName + \'.pack\', packData.buffer);\n                                                mkfile(objectsDir, \'pack/\' + packName + \'.idx\', packIdxData);\n                                                \n                                                var packIdx = new PackIndex(packIdxData);\n                                                if (!store.packs){\n                                                    store.packs = [];\n                                                }\n                                                store.packs.push({pack: new Pack(packData, store), idx: packIdx});\n                                            \n                                                mkfile(store.dir, \'.git/\' + wantRef.name, wantRef.sha, function(){\n                                                    progress({pct: 70, msg: \'Applying fast-forward merge\'});\n                                                    store._getTreesFromCommits([wantRef.localHead, wantRef.sha], function(trees){\n                                                        _updateWorkingTree(dir, store, trees[0], trees[1], function(){\n                                                            progress({pct: 99, msg: \'Finishing up\'})\n                                                            repoConfig.remoteHeads[branchRef.name] = branchRef.sha;\n                                                            store.updateLastChange(repoConfig, success);\n                                                        });\n                                                    });\n                                                }); \n                                            });\n                                        }\n                                        else{\n                                            // non-fast-forward merge\n                                            nonFastForward();\n                                            // var shas = [wantRef.localHead, common[i], wantRef.sha]\n                                            // store._getTreesFromCommits(shas, function(trees){\n                                            //     treeMerger.mergeTrees(store, trees[0], trees[1], trees[2], function(finalTree){\n                                            //         mkfile(store.dir, \'.git/\' + wantRef.name, sha, done); \n                                            //     }, function(e){errors.push(e);done();});\n                                            // });\n                                            \n                                        }\n                                            \n                                        \n                                    }, nonFastForward, fetchProgress);\n                                });\n                                                             \n                            }, ferror);\n                        }); \n                    }\n                    else{\n                        error({type: errutils.REMOTE_BRANCH_NOT_FOUND, msg: errutils.REMOTE_BRANCH_NOT_FOUND_MSG});\n                    }        \n                });\n            }, ferror);\n        }, error);\n        \n    }\n    return pull;\n});\ndefine(\'commands/push\',[\'formats/smart_http_remote\', \'formats/pack\', \'utils/progress_chunker\', \'utils/errors\'], function(SmartHttpRemote, Pack, ProgressChunker, errutils){\n    var push = function(options, success, error){\n        \n        var store = options.objectStore,\n            username = options.username,\n            password = options.password,\n            progress = options.progress || function(){};\n\n        var remotePushProgress;\n        if (options.progress){\n            var chunker = new ProgressChunker(progress);\n            remotePushProgress = chunker.getChunk(40, .6);\n        }\n        else{\n            remotePushProgress = function(){};\n        }\n\n        store.getConfig(function(config){\n            var url = config.url || options.url;\n\n            if (!url){\n                error({type: errutils.PUSH_NO_REMOTE, msg: errutils.PUSH_NO_REMOTE_MSG});\n                return;\n            }\n\n            var remote = new SmartHttpRemote(store, "origin", url, username, password, error);\n            progress({pct:0, msg: \'Contacting server...\'});\n            remote.fetchReceiveRefs(function(refs){\n                store._getCommitsForPush(refs, config.remoteHeads, function(commits, ref){\n                    progress({pct: 20, msg: \'Building pack...\'});\n                    Pack.buildPack(commits, store, function(packData){\n                        progress({pct: 40, msg: \'Sending pack...\'});\n                        remote.pushRefs([ref], packData, function(){\n                            config.remoteHeads = config.remoteHeads || {};\n                            config.remoteHeads[ref.name] = ref.head;\n                            config.url = url;\n                            store.setConfig(config, success);\n                        }, remotePushProgress);\n                    });\n                }, error);\n            });\n        });\n    }\n    return push;\n});\ndefine(\'commands/branch\',[\'utils/file_utils\', \'utils/errors\'], function(fileutils, errutils){\n    \n    var branchRegex = new RegExp("^(?!/|.*([/.]\\\\.|//|@\\\\{|\\\\\\\\))[^\\\\x00-\\\\x20 ~^:?*\\\\[]+$");\n    \n\n\n    var checkBranchName = function(branchName){\n        if (branchName && branchName.length && branchName.match(branchRegex)){\n            if (branchName.lastIndexOf(\'.lock\') != branchName.length - \'.lock\'.length &&\n                branchName.charAt(branchName.length - 1) != \'.\' && \n                branchName.charAt(branchName.length - 1) != \'/\' &&\n                branchName.charAt(0) != \'.\'){\n                return true;\n            }\n        };\n        return false\n    }\n\n    var branch = function(options, success, error){\n        var store = options.objectStore,\n            ferror = errutils.fileErrorFunc(error),\n            branchName = options.branch;\n\n        if (!checkBranchName(branchName)){\n            error({type: errutils.BRANCH_NAME_NOT_VALID, msg: errutils.BRANCH_NAME_NOT_VALID_MSG});\n            return;\n        }\n\n        var branchAlreadyExists = function(){\n            error({type: errutils.BRANCH_ALREADY_EXISTS, msg: errutils.BRANCH_ALREADY_EXISTS_MSG});\n        }\n\n        store._getHeadForRef(\'refs/heads/\' + branchName, branchAlreadyExists, function(e){\n            if (e.code == FileError.NOT_FOUND_ERR){\n                store.getHeadRef(function(refName){\n                    store._getHeadForRef(refName, function(sha){\n                        store.createNewRef(\'refs/heads/\' + branchName, sha, success);\n                    }, ferror);\n                });\n            }\n            else{\n                ferror(e);\n            }\n        });\n    }\n    return branch;\n});\ndefine(\'commands/checkout\',[\'commands/object2file\', \'commands/conditions\', \'utils/file_utils\', \'utils/errors\'], function(object2file, Conditions, fileutils, errutils){\n    \n    var blowAwayWorkingDir = function(dir, success, error){\n        fileutils.ls(dir, function(entries){\n            entries.asyncEach(function(entry, done){\n                if (entry.isDirectory){\n                    if (entry.name == \'.git\'){\n                        done();\n                        return;\n                    }\n                    else{\n                        entry.removeRecursively(done, error);\n                    }\n                }\n                else{\n                    entry.remove(done, error);\n                }\n            }, success);\n        }, error)\n    }\n\n    var checkout = function(options, success, error){\n        var dir = options.dir,\n            store = options.objectStore,\n            branch = options.branch,\n            ferror = errutils.fileErrorFunc(error);\n\n        \n        store._getHeadForRef(\'refs/heads/\' + branch, function(branchSha){\n            store.getHeadSha(function(currentSha){\n                if (currentSha != branchSha){\n                    Conditions.checkForUncommittedChanges(dir, store, function(config){\n                        blowAwayWorkingDir(dir, function(){\n                            store._retrieveObject(branchSha, "Commit", function(commit){\n                                var treeSha = commit.tree;\n                                object2file.expandTree(dir, store, treeSha, function(){\n                                    store.setHeadRef(\'refs/heads/\' + branch, function(){\n                                        store.updateLastChange(null, success);\n                                    });\n                                });\n                             });\n                        }, ferror);\n                    }, error);\n                }\n                else{\n                    store.setHeadRef(\'refs/heads/\' + branch, success);\n                }\n            });\n        }, \n        function(e){\n            if (e.code == FileError.NOT_FOUND_ERR){\n                error({type: errutils.CHECKOUT_BRANCH_NO_EXISTS, msg: CHECKOUT_BRANCH_NO_EXISTS_MSG});\n            }\n            else{\n                ferror(e);\n            }\n        });\n        \n    }\n    return checkout;\n});\n// Underscore.js 1.1.3\n// (c) 2010 Jeremy Ashkenas, DocumentCloud Inc.\n// Underscore is freely distributable under the MIT license.\n// Portions of Underscore are inspired or borrowed from Prototype,\n// Oliver Steele\'s Functional, and John Resig\'s Micro-Templating.\n// For all details and documentation:\n// http://documentcloud.github.com/underscore\n(function(){var p=this,C=p._,m={},j=Array.prototype,n=Object.prototype,i=j.slice,D=j.unshift,E=n.toString,q=n.hasOwnProperty,s=j.forEach,t=j.map,u=j.reduce,v=j.reduceRight,w=j.filter,x=j.every,y=j.some,o=j.indexOf,z=j.lastIndexOf;n=Array.isArray;var F=Object.keys,c=function(a){return new l(a)};if(typeof module!=="undefined"&&module.exports){module.exports=c;c._=c}else p._=c;c.VERSION="1.1.3";var k=c.each=c.forEach=function(a,b,d){if(s&&a.forEach===s)a.forEach(b,d);else if(c.isNumber(a.length))for(var e=\n0,f=a.length;e<f;e++){if(b.call(d,a[e],e,a)===m)break}else for(e in a)if(q.call(a,e))if(b.call(d,a[e],e,a)===m)break};c.map=function(a,b,d){if(t&&a.map===t)return a.map(b,d);var e=[];k(a,function(f,g,h){e[e.length]=b.call(d,f,g,h)});return e};c.reduce=c.foldl=c.inject=function(a,b,d,e){var f=d!==void 0;if(u&&a.reduce===u){if(e)b=c.bind(b,e);return f?a.reduce(b,d):a.reduce(b)}k(a,function(g,h,G){d=!f&&h===0?g:b.call(e,d,g,h,G)});return d};c.reduceRight=c.foldr=function(a,b,d,e){if(v&&a.reduceRight===\nv){if(e)b=c.bind(b,e);return d!==void 0?a.reduceRight(b,d):a.reduceRight(b)}a=(c.isArray(a)?a.slice():c.toArray(a)).reverse();return c.reduce(a,b,d,e)};c.find=c.detect=function(a,b,d){var e;A(a,function(f,g,h){if(b.call(d,f,g,h)){e=f;return true}});return e};c.filter=c.select=function(a,b,d){if(w&&a.filter===w)return a.filter(b,d);var e=[];k(a,function(f,g,h){if(b.call(d,f,g,h))e[e.length]=f});return e};c.reject=function(a,b,d){var e=[];k(a,function(f,g,h){b.call(d,f,g,h)||(e[e.length]=f)});return e};\nc.every=c.all=function(a,b,d){b=b||c.identity;if(x&&a.every===x)return a.every(b,d);var e=true;k(a,function(f,g,h){if(!(e=e&&b.call(d,f,g,h)))return m});return e};var A=c.some=c.any=function(a,b,d){b=b||c.identity;if(y&&a.some===y)return a.some(b,d);var e=false;k(a,function(f,g,h){if(e=b.call(d,f,g,h))return m});return e};c.include=c.contains=function(a,b){if(o&&a.indexOf===o)return a.indexOf(b)!=-1;var d=false;A(a,function(e){if(d=e===b)return true});return d};c.invoke=function(a,b){var d=i.call(arguments,\n2);return c.map(a,function(e){return(b?e[b]:e).apply(e,d)})};c.pluck=function(a,b){return c.map(a,function(d){return d[b]})};c.max=function(a,b,d){if(!b&&c.isArray(a))return Math.max.apply(Math,a);var e={computed:-Infinity};k(a,function(f,g,h){g=b?b.call(d,f,g,h):f;g>=e.computed&&(e={value:f,computed:g})});return e.value};c.min=function(a,b,d){if(!b&&c.isArray(a))return Math.min.apply(Math,a);var e={computed:Infinity};k(a,function(f,g,h){g=b?b.call(d,f,g,h):f;g<e.computed&&(e={value:f,computed:g})});\nreturn e.value};c.sortBy=function(a,b,d){return c.pluck(c.map(a,function(e,f,g){return{value:e,criteria:b.call(d,e,f,g)}}).sort(function(e,f){var g=e.criteria,h=f.criteria;return g<h?-1:g>h?1:0}),"value")};c.sortedIndex=function(a,b,d){d=d||c.identity;for(var e=0,f=a.length;e<f;){var g=e+f>>1;d(a[g])<d(b)?e=g+1:f=g}return e};c.toArray=function(a){if(!a)return[];if(a.toArray)return a.toArray();if(c.isArray(a))return a;if(c.isArguments(a))return i.call(a);return c.values(a)};c.size=function(a){return c.toArray(a).length};\nc.first=c.head=function(a,b,d){return b&&!d?i.call(a,0,b):a[0]};c.rest=c.tail=function(a,b,d){return i.call(a,c.isUndefined(b)||d?1:b)};c.last=function(a){return a[a.length-1]};c.compact=function(a){return c.filter(a,function(b){return!!b})};c.flatten=function(a){return c.reduce(a,function(b,d){if(c.isArray(d))return b.concat(c.flatten(d));b[b.length]=d;return b},[])};c.without=function(a){var b=i.call(arguments,1);return c.filter(a,function(d){return!c.include(b,d)})};c.uniq=c.unique=function(a,\nb){return c.reduce(a,function(d,e,f){if(0==f||(b===true?c.last(d)!=e:!c.include(d,e)))d[d.length]=e;return d},[])};c.intersect=function(a){var b=i.call(arguments,1);return c.filter(c.uniq(a),function(d){return c.every(b,function(e){return c.indexOf(e,d)>=0})})};c.zip=function(){for(var a=i.call(arguments),b=c.max(c.pluck(a,"length")),d=Array(b),e=0;e<b;e++)d[e]=c.pluck(a,""+e);return d};c.indexOf=function(a,b){if(o&&a.indexOf===o)return a.indexOf(b);for(var d=0,e=a.length;d<e;d++)if(a[d]===b)return d;\nreturn-1};c.lastIndexOf=function(a,b){if(z&&a.lastIndexOf===z)return a.lastIndexOf(b);for(var d=a.length;d--;)if(a[d]===b)return d;return-1};c.range=function(a,b,d){var e=i.call(arguments),f=e.length<=1;a=f?0:e[0];b=f?e[0]:e[1];d=e[2]||1;e=Math.max(Math.ceil((b-a)/d),0);f=0;for(var g=Array(e);f<e;){g[f++]=a;a+=d}return g};c.bind=function(a,b){var d=i.call(arguments,2);return function(){return a.apply(b||{},d.concat(i.call(arguments)))}};c.bindAll=function(a){var b=i.call(arguments,1);if(b.length==\n0)b=c.functions(a);k(b,function(d){a[d]=c.bind(a[d],a)});return a};c.memoize=function(a,b){var d={};b=b||c.identity;return function(){var e=b.apply(this,arguments);return e in d?d[e]:d[e]=a.apply(this,arguments)}};c.delay=function(a,b){var d=i.call(arguments,2);return setTimeout(function(){return a.apply(a,d)},b)};c.defer=function(a){return c.delay.apply(c,[a,1].concat(i.call(arguments,1)))};var B=function(a,b,d){var e;return function(){var f=this,g=arguments,h=function(){e=null;a.apply(f,g)};d&&\nclearTimeout(e);if(d||!e)e=setTimeout(h,b)}};c.throttle=function(a,b){return B(a,b,false)};c.debounce=function(a,b){return B(a,b,true)};c.wrap=function(a,b){return function(){var d=[a].concat(i.call(arguments));return b.apply(b,d)}};c.compose=function(){var a=i.call(arguments);return function(){for(var b=i.call(arguments),d=a.length-1;d>=0;d--)b=[a[d].apply(this,b)];return b[0]}};c.keys=F||function(a){if(c.isArray(a))return c.range(0,a.length);var b=[],d;for(d in a)if(q.call(a,d))b[b.length]=d;return b};\nc.values=function(a){return c.map(a,c.identity)};c.functions=c.methods=function(a){return c.filter(c.keys(a),function(b){return c.isFunction(a[b])}).sort()};c.extend=function(a){k(i.call(arguments,1),function(b){for(var d in b)a[d]=b[d]});return a};c.clone=function(a){return c.isArray(a)?a.slice():c.extend({},a)};c.tap=function(a,b){b(a);return a};c.isEqual=function(a,b){if(a===b)return true;var d=typeof a;if(d!=typeof b)return false;if(a==b)return true;if(!a&&b||a&&!b)return false;if(a.isEqual)return a.isEqual(b);\nif(c.isDate(a)&&c.isDate(b))return a.getTime()===b.getTime();if(c.isNaN(a)&&c.isNaN(b))return false;if(c.isRegExp(a)&&c.isRegExp(b))return a.source===b.source&&a.global===b.global&&a.ignoreCase===b.ignoreCase&&a.multiline===b.multiline;if(d!=="object")return false;if(a.length&&a.length!==b.length)return false;d=c.keys(a);var e=c.keys(b);if(d.length!=e.length)return false;for(var f in a)if(!(f in b)||!c.isEqual(a[f],b[f]))return false;return true};c.isEmpty=function(a){if(c.isArray(a)||c.isString(a))return a.length===\n0;for(var b in a)if(q.call(a,b))return false;return true};c.isElement=function(a){return!!(a&&a.nodeType==1)};c.isArray=n||function(a){return!!(a&&a.concat&&a.unshift&&!a.callee)};c.isArguments=function(a){return!!(a&&a.callee)};c.isFunction=function(a){return!!(a&&a.constructor&&a.call&&a.apply)};c.isString=function(a){return!!(a===""||a&&a.charCodeAt&&a.substr)};c.isNumber=function(a){return!!(a===0||a&&a.toExponential&&a.toFixed)};c.isNaN=function(a){return E.call(a)==="[object Number]"&&isNaN(a)};\nc.isBoolean=function(a){return a===true||a===false};c.isDate=function(a){return!!(a&&a.getTimezoneOffset&&a.setUTCFullYear)};c.isRegExp=function(a){return!!(a&&a.test&&a.exec&&(a.ignoreCase||a.ignoreCase===false))};c.isNull=function(a){return a===null};c.isUndefined=function(a){return a===void 0};c.noConflict=function(){p._=C;return this};c.identity=function(a){return a};c.times=function(a,b,d){for(var e=0;e<a;e++)b.call(d,e)};c.mixin=function(a){k(c.functions(a),function(b){H(b,c[b]=a[b])})};var I=\n0;c.uniqueId=function(a){var b=I++;return a?a+b:b};c.templateSettings={evaluate:/<%([\\s\\S]+?)%>/g,interpolate:/<%=([\\s\\S]+?)%>/g};c.template=function(a,b){var d=c.templateSettings;d="var __p=[],print=function(){__p.push.apply(__p,arguments);};with(obj||{}){__p.push(\'"+a.replace(/\\\\/g,"\\\\\\\\").replace(/\'/g,"\\\\\'").replace(d.interpolate,function(e,f){return"\',"+f.replace(/\\\\\'/g,"\'")+",\'"}).replace(d.evaluate||null,function(e,f){return"\');"+f.replace(/\\\\\'/g,"\'").replace(/[\\r\\n\\t]/g," ")+"__p.push(\'"}).replace(/\\r/g,\n"\\\\r").replace(/\\n/g,"\\\\n").replace(/\\t/g,"\\\\t")+"\');}return __p.join(\'\');";d=new Function("obj",d);return b?d(b):d};var l=function(a){this._wrapped=a};c.prototype=l.prototype;var r=function(a,b){return b?c(a).chain():a},H=function(a,b){l.prototype[a]=function(){var d=i.call(arguments);D.call(d,this._wrapped);return r(b.apply(c,d),this._chain)}};c.mixin(c);k(["pop","push","reverse","shift","sort","splice","unshift"],function(a){var b=j[a];l.prototype[a]=function(){b.apply(this._wrapped,arguments);\nreturn r(this._wrapped,this._chain)}});k(["concat","join","slice"],function(a){var b=j[a];l.prototype[a]=function(){return r(b.apply(this._wrapped,arguments),this._chain)}});l.prototype.chain=function(){this._chain=true;return this};l.prototype.value=function(){return this._wrapped}})();\n\ndefine("thirdparty/underscore-min", function(){});\n\ndefine(\'objectstore/objects\',[\'utils/misc_utils\', \'thirdparty/underscore-min\'], function(utils) {\n    var map = {\n        "commit": 1,\n        "tree": 2,\n        "blob": 3\n    };\n    GitObjects = {\n        CONSTRUCTOR_NAMES: {\n            "blob": "Blob",\n            "tree": "Tree",\n            "commit": "Commit",\n            "comm": "Commit",\n            "tag": "Tag",\n            "tag ": "Tag"\n        },\n\n        make: function(sha, type, content) {\n            var constructor = Git.objects[this.CONSTRUCTOR_NAMES[type]]\n            if (constructor) {\n                return new constructor(sha, content)\n            } else {\n                throw ("no constructor for " + type)\n            }\n        },\n\n        Blob: function(sha, data) {\n            this.type = "blob"\n            this.sha = sha\n            this.data = data\n            this.toString = function() {\n                return data\n            }\n        },\n\n        Tree: function(sha, buf) {\n            var data = new Uint8Array(buf);\n            var treeEntries = [];\n\n            var idx = 0;\n            while (idx < data.length) {\n                var entryStart = idx;\n                while (data[idx] != 0) {\n                    if (idx >= data.length) {\n                        throw Error("object is not a tree");\n                    }\n                    idx++;\n                }\n                var isBlob = data[entryStart] == 49; // \'1\' character\n                var nameStr = utils.bytesToString(data.subarray(entryStart + (isBlob ? 7 : 6), idx++));\n                nameStr = decodeURIComponent(escape(nameStr));\n                var entry = {\n                    isBlob: isBlob,\n                    name: nameStr,\n                    sha: data.subarray(idx, idx + 20)\n                };\n                treeEntries.push(entry);\n                idx += 20;\n            }\n            this.entries = treeEntries;\n\n            var sorter = function(a, b) {\n                var nameA = a.name,\n                    nameB = b.name;\n                if (nameA < nameB) //sort string ascending\n                    return -1;\n                if (nameA > nameB)\n                    return 1;\n                return 0;\n            }\n            this.sortEntries = function() {\n                this.entries.sort(sorter);\n            }\n        },\n\n        Commit: function(sha, data) {\n            this.type = "commit"\n            this.sha = sha\n            this.data = data\n\n            var lines = data.split("\\n")\n            this.tree = lines[0].split(" ")[1]\n            var i = 1\n            this.parents = []\n            while (lines[i].slice(0, 6) === "parent") {\n                this.parents.push(lines[i].split(" ")[1])\n                i += 1\n            }\n\n            var parseAuthor = function(line) {\n                var match = /^(.*) <(.*)> (\\d+) (\\+|\\-)\\d\\d\\d\\d$/.exec(line)\n                var result = {}\n\n                result.name = match[1]\n                result.email = match[2]\n                result.timestamp = parseInt(match[3])\n                result.date = new Date(result.timestamp * 1000)\n                return result\n            }\n\n            var authorLine = lines[i].replace("author ", "")\n            this.author = parseAuthor(authorLine)\n\n            var committerLine = lines[i + 1].replace("committer ", "")\n            this.committer = parseAuthor(committerLine)\n\n            if (lines[i + 2].split(" ")[0] == "encoding") {\n                this.encoding = lines[i + 2].split(" ")[1]\n            }\n            this.message = _(lines.slice(i + 2, lines.length)).select(function(line) {\n                return line !== ""\n            }).join("\\n")\n\n            this.toString = function() {\n                var str = "commit " + sha + "\\n"\n                str += "Author: " + this.author.name + " <" + this.author.email + ">\\n"\n                str += "Date:   " + this.author.date + "\\n"\n                str += "\\n"\n                str += this.message\n                return str\n            }\n        },\n\n        Tag: function(sha, data) {\n            this.type = "tag"\n            this.sha = sha\n            this.data = data\n        },\n\n        RawLooseObject: function(buf) {\n\n            var header, i, data;\n            var funcName;\n            if (buf instanceof ArrayBuffer) {\n                var data = new Uint8Array(buf);\n                var headChars = [];\n                i = 0;\n                for (; i < data.length; i++) {\n                    if (data[i] != 0)\n                        headChars.push(String.fromCharCode(data[i]));\n                    else\n                        break;\n                }\n                header = headChars.join(\'\');\n                funcName = \'subarray\';\n            } else {\n                data = buf;\n                i = buf.indexOf(\'\\0\');\n                header = buf.substring(0, i);\n                funcName = \'substring\';\n            }\n            var parts = header.split(\' \');\n            this.type = map[parts[0]];\n            this.size = parseInt(parts[1]);\n            // move past nul terminator but keep zlib header\n            this.data = data[funcName](i + 1);\n        }\n    }\n    return GitObjects;\n});\ndefine(\'objectstore/file_repo\',[\'formats/pack\', \'formats/pack_index\', \'objectstore/objects\', \'utils/misc_utils\', \'utils/file_utils\', \'utils/errors\'], function(Pack, PackIndex, GitObjects, utils, fileutils, errutils){\n\n\tString.prototype.endsWith = function(suffix){\n    \treturn this.lastIndexOf(suffix) == (this.length - suffix.length);\n\t}\n\n\tvar FileObjectStore = function(rootDir) {\n\t \tthis.dir = rootDir;\n\t \tthis.packs = [];\n\t}\n\n\tFileObjectStore.prototype = {\n\t\thaveRefs: function(){\n\t\t\treturn [];\n\t\t},\n\t\tload : function(callback){\n\t\t\tvar rootDir = this.dir;\n\t\t\tvar thiz = this;\n\t\t\tvar fe = this.fileError;\n\n\t\t\trootDir.getDirectory(\'.git/objects\', {create:true}, function(objectsDir){\n\t\t\t\tthiz.objectsDir = objectsDir;\n\t\t\t\tobjectsDir.getDirectory(\'pack\', {create:true}, function(packDir){\n\t\t\t\t\tvar packEntries = [];\n\t\t\t\t\tvar reader = packDir.createReader();\n\t\t\t\t\tvar readEntries = function(){\n\t\t\t\t\t\treader.readEntries(function(entries){\n\t\t\t\t\t\t    if (entries.length){\n\t\t\t\t\t\t\t\tfor (var i = 0; i < entries.length; i++){\n\t\t\t\t\t\t\t\t\tif (entries[i].name.endsWith(\'.pack\'))\n\t\t\t\t\t\t\t\t\t\tpackEntries.push(entries[i]);\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\treadEntries();\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\telse{\n\t\t\t\t\t\t\t\tif (packEntries.length){\n\t\t\t\t\t\t\t\t\tvar counter = {x : 0};\n\t\t\t\t\t\t\t\t\tpackEntries.forEach(function(entry, i){\n\t\t\t\t\t\t\t\t\t\tfileutils.readFile(packDir, entry.name, "ArrayBuffer", function(packData){\n\t\t\t\t\t\t\t\t\t\t\tvar nameRoot = entry.name.substring(0, entry.name.lastIndexOf(\'.pack\'));\n\t\t\t\t\t\t\t\t\t\t\tfileutils.readFile(packDir, nameRoot + \'.idx\', \'ArrayBuffer\', function(idxData){\n\t\t\t\t\t\t\t\t\t\t\t\tthiz.packs.push({pack: new Pack(packData, thiz), idx: new PackIndex(idxData)});\n\t\t\t\t\t\t\t\t\t\t\t\tcounter.x += 1;\n\t\t\t\t\t\t\t\t\t\t\t\tif (counter.x == packEntries.length){\n\t\t\t\t\t\t\t\t\t\t\t\t\tcallback();\n\t\t\t\t\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\t\t\t\t}, fe);\n\t\t\t\t\t\t\t\t\t\t}, fe);\n\t\t\t\t\t\t\t\t\t});\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\telse{\n\t\t\t\t\t\t\t\t\tcallback();\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t}, fe);\n\t\t\t\t\t}\n\t\t\t\t\treadEntries();\n\t\t\t\t}, fe);\n\t\t\t}, fe);\n\t\t},\n\t\tloadWith : function(objectsDir, packs){\n\t\t\tthis.objectsDir = objectsDir;\n\t\t\tthis.packs = packs;\n\t\t},\n\t\t_getCommitGraph : function(headShas, limit, callback){\n\t\t\tvar commits = [];\n\t\t\tvar thiz = this;\n\t\t\tvar seen = {};\n\t\t\t\n\t\t\tvar walkLevel = function(shas, callback){\n\t\t\t\tvar nextLevel = [];\n\t\t\t\tshas.asyncEach(function(sha, callback){\n\t\t\t\t\tif (seen[sha]){\n\t\t\t\t\t\tcallback();\n\t\t\t\t\t\treturn;\n\t\t\t\t\t}\n\t\t\t\t\telse{\n\t\t\t\t\t\tseen[sha] = true;\n\t\t\t\t\t}\n\t\t\t\t\t// it\'s possible for this to fail since we support shallow clones\n\t\t\t\t\tthiz._retrieveObject(sha, \'Commit\', function(obj){\n\t\t\t\t\t\tnextLevel = nextLevel.concat(obj.parents);\n\t\t\t\t\t\tvar i = commits.length - 1\n\t\t\t\t\t\tfor (; i >= 0; i--){\n\t\t\t\t\t\t\tif (commits[i].author.timestamp > obj.author.timestamp){\n\t\t\t\t\t\t\t\tcommits.splice(i + 1, 0, obj);\n\t\t\t\t\t\t\t\tbreak;\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t}\n\t\t\t\t\t\tif (i < 0){\n\t\t\t\t\t\t\tcommits.unshift(obj);\n\t\t\t\t\t\t}\n\t\t\t\t\t\tcallback();\n\t\t\t\t\t}, callback);\n\t\t\t\t}, function(){\n\t\t\t\t\tif (commits.length >= limit || nextLevel.length == 0){\n\t\t\t\t\t\t/*var shas = [];\n\t\t\t\t\t\tfor (var i = 0; i < commits.length; i++){\n\t\t\t\t\t\t\tshas.push(commit.sha);\n\t\t\t\t\t\t}*/\n\t\t\t\t\t\tcallback(commits, nextLevel);\n\t\t\t\t\t}\n\t\t\t\t\telse{\n\t\t\t\t\t\twalkLevel(nextLevel, callback);\n\t\t\t\t\t}\n\t\t\t\t});\n\t\t\t}\n\t\t\twalkLevel(headShas, callback);\n\t\t},\n\t\t_getCommitsForPush : function(baseRefs, remoteHeads, callback, error){\n\t\t\t\n\t\t\t// special case of empty remote. \n\t\t\tif (baseRefs.length == 1 && baseRefs[0].sha == "0000000000000000000000000000000000000000"){\n\t\t\t\tbaseRefs[0].name = \'refs/heads/master\';\n\t\t\t}\n\n\t\t\tvar self = this;\n\t\t\t// find the remote branch corresponding to our local one.\n\t\t\tvar remoteRef, headRef;\n\t\t\tthis.getHeadRef(function(refName){\n\t\t\t\theadRef = refName;\n\t\t\t\tfor (var i = 0; i < baseRefs.length; i++){\n\t\t\t\t\tif (baseRefs[i].name == headRef){\n\t\t\t\t\t\tremoteRef = baseRefs[i];\n\t\t\t\t\t\tbreak;\n\t\t\t\t\t}\n\t\t\t\t}\n\n\t\t\t\t// Didn\'t find a remote branch for our local so base the commits we \n\t\t\t\t// need to push on what we already know about the remote\n\t\t\t\tvar newBranch = !remoteRef;\n\t\t\t\tvar remoteShas = {};\n\t\t\t\tif (newBranch){\n\t\t\t\t\tremoteRef = {\n\t\t\t\t\t\tsha: "0000000000000000000000000000000000000000",\n\t\t\t\t\t\tname: headRef\n\t\t\t\t\t}\n\t\t\t\t\tfor (var remoteHead in remoteHeads){\n\t\t\t\t\t\tvar remoteSha = remoteHeads[remoteHead];\n\t\t\t\t\t\tremoteShas[remoteSha] = true;\n\t\t\t\t\t}\n\t\t\t\t}\n\t\t\t\t\n\t\t\t\tvar nonFastForward = function(){\n\t\t\t\t\terror({type: errutils.PUSH_NON_FAST_FORWARD, msg: errutils.PUSH_NON_FAST_FORWARD_MSG});\n\t\t\t\t}\n\n\t\t\t\tvar checkRemoteHead = function(success){\n\t\t\t\t\t// See if the remote head exists in our repo.  \n\t\t\t\t\tif (remoteRef.sha != "0000000000000000000000000000000000000000"){\n\t\t\t\t\t\tself._retrieveObject(remoteRef.sha, \'Commit\', success, nonFastForward);\n\t\t\t\t\t}\t\n\t\t\t\t\telse{\n\t\t\t\t\t\tsuccess();\n\t\t\t\t\t}\n\t\t\t\t} \n\n\n\t\t\t\tcheckRemoteHead(function(){\n\t\t\t\t\tself._getHeadForRef(headRef, function(sha){\n\n\t\t\t\t\t\tif (sha == remoteRef.sha){\n\t\t\t\t\t\t\terror({type: errutils.PUSH_NO_CHANGES, msg: errutils.PUSH_NO_CHANGES_MSG});\n\t\t\t\t\t\t\treturn;\n\t\t\t\t\t\t}\n\n\t\t\t\t\t\tremoteRef.head = sha;\n\t\t\t\t\t\t\n\t\t\t\t\t\t// case of new branch with no new commits\n\t\t\t\t\t\tif (newBranch && remoteShas[sha]){\n\t\t\t\t\t\t\tcallback([], remoteRef);\n\t\t\t\t\t\t\treturn;\n\t\t\t\t\t\t}\n\n\t\t\t\t\t\t// we don\'t support local merge commits so finding commits to push should be a \n\t\t\t\t\t\t// matter of looking at a non-branching list of ancestors of the current commit.\n\t\t\t\t\t\tvar commits = [];\n\t\t\t\t\t\tvar getNextCommit = function(sha){\n\t\t\t\t\t\t\tself._retrieveObject(sha, \'Commit\', function(commit, rawObj){\n\t\t\t\t\t\t\t\tcommits.push({commit: commit, raw: rawObj});\n\t\t\t\t\t\t\t\tif (commit.parents.length > 1){\n\t\t\t\t\t\t\t\t\t// this would mean a local merge commit. It shouldn\'t happen, \n\t\t\t\t\t\t\t\t\t// therefore we\'ve strayed into somewhere we shouldn\'t be.\n\t\t\t\t\t\t\t\t\tnonFastForward();\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\telse if (commit.parents.length == 0 || commit.parents[0] == remoteRef.sha || remoteShas[commit.parents[0]]){\n\t\t\t\t\t\t\t\t\tcallback(commits, remoteRef);\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\telse{\n\t\t\t\t\t\t\t\t\tgetNextCommit(commit.parents[0]);\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}, nonFastForward);\n\t\t\t\t\t\t}\n\t\t\t\t\t\tgetNextCommit(sha);\n\t\t\t\t\t},\n\t\t\t\t\tfunction(e){\n\t\t\t\t\t\t// No commits yet\n\t\t\t\t\t\tif (e.code == FileError.NOT_FOUND_ERR){\n\t\t\t\t\t\t\terror({type: errutils.COMMIT_NO_CHANGES, msg: errutils.COMMIT_NO_CHANGES_MSG});\n\t\t\t\t\t\t}\n\t\t\t\t\t\telse{\n\t\t\t\t\t\t\tself.fileError(e);\n\t\t\t\t\t\t}\n\t\t\t\t\t});\n\t\t\t\t});\n\n\t\t\t});\n\t\t\t\n\t\t},\n\t\tcreateNewRef : function(refName, sha, success){\n\t\t\tfileutils.mkfile(this.dir, \'.git/\' + refName, sha + \'\\n\', success, this.fileError);\n\t\t},\n\t\tsetHeadRef : function(refName, callback){\n\t\t\tfileutils.mkfile(this.dir, \'.git/HEAD\', \'ref: \' + refName + \'\\n\', callback, this.fileError);\n\t\t},\n\t\tgetHeadRef : function(callback){\n\t\t\tfileutils.readFile(this.dir, \'.git/HEAD\', \'Text\', function(headStr){\n\t\t\t\t// get rid of the initial \'ref: \' plus newline at end\n            \tvar headRefName = headStr.substring(5).trim();\n            \tcallback(headRefName);\n\t\t\t},this.fileError);\n\t\t},\n\t\tgetHeadSha : function(callback){\n\t\t\tvar self = this;\n\t\t\tthis.getHeadRef(function(ref){\n\t\t\t\tself._getHeadForRef(ref, callback, self.fileError);\n\t\t\t});\n\t\t},\n\t\tgetAllHeads : function(callback){\n\t\t\tvar fe = this.fileError;\n\t\t\tthis.dir.getDirectory(\'.git/refs/heads\', {create: false}, function(de){\n\t\t\t\tfileutils.ls(de, function(entries){\n\t\t\t\t\tvar branches = [];\n\t\t\t\t\tentries.forEach(function(entry){\n\t\t\t\t\t\tbranches.push(entry.name);\n\t\t\t\t\t});\n\t\t\t\t\tcallback(branches);\n\t\t\t\t}, fe);\n\t\t\t}, \n\t\t\tfunction(e){\n\t\t\t\tif (e.code == FileError.NOT_FOUND_ERR){\n\t\t\t\t\tcallback([]);\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tfe(e);\n\t\t\t\t}\n\t\t\t});\n\t\t},\n\t\t_getHeadForRef : function(name, callback, onerror){\n\t\t\tfileutils.readFile(this.dir, \'.git/\' + name, \'Text\', function(data){callback(data.substring(0, 40));}, onerror) ;\t\n\t\t},\n\t\t\n\t\t_findLooseObject : function(sha, success, error){\n\t\t\tthis.objectsDir.getFile(sha.substring(0,2) + \'/\' + sha.substring(2), {create:false}, function(fileEntry){\n\t\t\t\tsuccess(fileEntry);\n\t\t\t},\n\t\t\tfunction(e){\n\t\t\t\terror(e);\n\t\t\t});\n\t\t},\n\t\t_findPackedObject : function(sha, success, error){\n\t\t\tfor (var i = 0; i < this.packs.length; i++){\n\t\t\t\tvar offset = this.packs[i].idx.getObjectOffset(sha);\n\t\t\t\tif (offset != -1){\n\t\t\t\t\tsuccess(offset, this.packs[i].pack);\n\t\t\t\t\treturn;\n\t\t\t\t}\n\t\t\t}\n\t\t\terror();\n\t\t},\n\t\t_retrieveRawObject : function(sha, dataType, callback, error){\n\t\t     var shaBytes;\n\t\t     if (sha instanceof Uint8Array){\n\t\t     \tshaBytes = sha;\n\t\t     \tsha = utils.convertBytesToSha(shaBytes);\n\t\t     }\n\t\t     else{\n\t\t     \tshaBytes = utils.convertShaToBytes(sha);\n\t\t     }\n\t\t     \n\t\t\t \n\t\t\t \n\t\t\t var thiz = this;\n\t\t\t this._findLooseObject(sha, function(fileEntry){\n\t\t\t \tfileutils.readFileEntry(fileEntry, \'ArrayBuffer\', function(buf){\n\t\t\t \t\tvar inflated = utils.inflate(new Uint8Array(buf));\n\t\t\t \t\tif (dataType == \'Raw\' || dataType == \'ArrayBuffer\'){\n\t\t\t \t\t\tvar buffer = utils.trimBuffer(inflated);\n\t\t\t \t\t\tcallback(new GitObjects.RawLooseObject(buffer));\n\t\t\t \t\t}\n\t\t\t \t\telse{\n\t\t\t \t\t\tfileutils.readBlob(new Blob([inflated]), dataType, function(data){\n\t\t\t\t\t\t\tcallback(new GitObjects.RawLooseObject(data));\n\t\t\t\t\t\t});\n\t\t\t \t\t}\n\t\t\t \t});\n\t\t\t }, function(e){\n\t\t\t \t\tthiz._findPackedObject(shaBytes, function(offset, pack){\n\t\t\t \t\t\tdataType = dataType == \'Raw\' ? \'ArrayBuffer\' : dataType;\n\t\t\t \t\t\tpack.matchAndExpandObjectAtOffset(offset, dataType, function(object){\n\t\t\t\t\t\t\tcallback(object);\n\t\t\t\t\t\t});\n\t\t\t \t}, function(){\n\t\t\t \t    if (error) error.call(thiz);\n\t\t\t \t    else throw(Error("Can\'t find object with SHA " + sha));\n\t\t\t \t});\n\t\t\t });\n\t\t},\n\t\t_retrieveBlobsAsStrings : function(shas, callback){\n\t\t\tvar blobs =new Array(shas.length),\n\t\t\t\tself = this;\n\t\t\t\t\n\t\t\tshas.asyncEach(function(sha, done, i){\n\t\t\t\tself._retrieveRawObject(sha, \'Text\', function(object){\n\t\t\t\t\tblobs[i] = new GitObjects.Blob(sha, object.data);\n\t\t\t\t\tdone();\n\t\t\t\t });\n\t\t\t},\n\t\t\tfunction(){\n\t\t\t\tcallback(blobs);\n\t\t\t});\n\t\t},\n\t\t_retrieveObjectList : function(shas, objType, callback){\n\t\t\tvar objects = new Array(shas.length),\n\t\t\t\tself = this;\n\t\t\t\t\n\t\t\tshas.asyncEach(function(sha, done, i){\n\t\t\t\tself._retrieveObject(sha, objType, function(obj){\n\t\t\t\t\tobjects[i] = obj;\n\t\t\t\t\tdone();\n\t\t\t\t});\n\t\t\t},\n\t\t\tfunction(){\n\t\t\t\tcallback(objects);\n\t\t\t});\n\t\t},\n\t\t_retrieveObject : function(sha, objType, callback, error){\n\t\t\t var dataType = "ArrayBuffer";\n\t\t\t if (objType == "Commit"){\n\t\t\t \tdataType = "Text";\n\t\t\t }\n\t\t\t \n\t\t\t this._retrieveRawObject(sha, dataType, function(object){\n\t\t\t \tcallback(new GitObjects[objType](sha, object.data), object);\n\t\t\t }, error);\n\t\t},\n\n\t\tinit : function(success, error){\n\t\t\tvar root = this.dir;\n\t\t\tvar self = this;\n\t\t\tthis.error = error;\n\t\t\tthis.fileError = errutils.fileErrorFunc(error);\n\t\t\t\n\t\t\troot.getDirectory(\'.git\', {create:false}, function(gitDir){\n\t\t\t\tself.load(success);\n\t\t\t},\n\t\t\tfunction(e){\n\t\t\t\tif (e.code == FileError.NOT_FOUND_ERR){\n\t\t\t\t\tself._init(success);\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tself.fileError(e);\n\t\t\t\t}\n\t\t\t});\n\t\t},\n\n\t\t_init : function(success){\n\t\t\tvar root = this.dir;\n\t\t\tvar self = this;\n\t\t\tfileutils.mkdirs(root, \'.git/objects\', function(objectsDir){\n\t\t\t\tself.objectsDir = objectsDir;\n\t\t\t\tfileutils.mkfile(root, \'.git/HEAD\', \'ref: refs/heads/master\\n\', success, self.fileError);\n\t\t\t}, this.fileError);\n\n\t\t},\n\t\t\n\t\t_getTreesFromCommits : function(shas, callback){\n\t\t\tvar trees = [],\n\t\t\t    shaIndex = 0,\n\t\t\t\tself = this;\n\t\t\t\n\t\t\tvar fillTrees = function(){\n\t\t\t\tself._getTreeFromCommitSha(shas[shaIndex++], function(tree){\n\t\t\t\t\ttrees.push(tree);\n\t\t\t\t\tif (shaIndex >= shas.length){\n\t\t\t\t\t\tcallback(trees);\n\t\t\t\t\t}\n\t\t\t\t\telse{\n\t\t\t\t\t\tfillTrees();\n\t\t\t\t\t}\n\t\t\t\t});\n\t\t\t}\n\t\t\tfillTrees();\n\t\t},\n\t\t_getTreeFromCommitSha : function(sha, callback){\n\t\t\tvar self = this;\n\t\t\tthis._retrieveObject(sha, \'Commit\', function(commit){\n\t\t\t\tself._retrieveObject(commit.tree, \'Tree\', callback);\n\t\t\t});\n\t\t},\n\t\twriteRawObject : function(type, content, callback){\n\t\t\tvar bb = [];//new BlobBuilder();\n\t\t\tvar size = content.byteLength || content.length || content.size || 0;\n\t\t\tvar header = type + \' \' + String(size) ;\n\t\t\t\n\t\t\t//var store = header + content;\n\t\t\t\n\t\t\tbb.push(header);\n\t\t\tbb.push(new Uint8Array([0]));\n\t\t\tbb.push(content);\n\t\t\tvar thiz = this;\n\t\t\tvar fr = new FileReader();\n\t\t\tfr.onloadend = function(e){\n\t\t\t\tvar buf = fr.result;\n\t\t\t\tvar store = new Uint8Array(buf);\n\t\t\t\tvar digest = Crypto.SHA1(store);\n\t\t\t\tthiz._findPackedObject(utils.convertShaToBytes(digest), function(){callback(digest);}, function(){\n\t\t\t\t\tthiz._storeInFile(digest, store, callback);\n\t\t\t\t});\n\t\t\t}\n\t\t\t\n\t\t\tfr.readAsArrayBuffer(new Blob(bb));   \n\t\t},\n\t\t\n\t\t_storeInFile : function(digest, store, callback){\n\t\t\tvar subDirName = digest.substr(0,2); \t\n\t\t\tvar objectFileName = digest.substr(2);\n\t\t\t\n\t\t\tthis.objectsDir.getDirectory(subDirName, {create:true}, function(dirEntry){\n\t\t\t\tdirEntry.getFile(objectFileName, {create:true}, function(fileEntry){\n\t\t\t\t\tfileEntry.file(function(file){\n\t\t\t\t\t\t if(!file.size){\n\t\t\t\t\t\t \tvar content = utils.deflate(store);\n\t\t\t\t\t\t \tfileEntry.createWriter(function(fileWriter){\n\t\t\t\t\t\t \t\tfileWriter.write(new Blob([content]));;\n\t\t\t\t\t\t \t\tcallback(digest);\n\t\t\t\t\t\t \t}, utils.errorHandler);\n\t\t\t\t\t\t }\n\t\t\t\t\t\t else{\n\t\t\t\t\t\t \tcallback(digest);\n\t\t\t\t\t\t }\n\t\t\t\t\t}, utils.errorHandler);\n\t\t\t\t\n\t\t\t\t}, utils.errorHandler);\n\t\t\t\t\n\t\t\t}, utils.errorHandler);\n\t\t},\n\n\t\t_writeTree : function(treeEntries, success){\n\t\t\tvar bb = [];//new BlobBuilder();\n\t\t\tfor (var i = 0; i < treeEntries.length; i++){\n\t\t\t\tbb.push((treeEntries[i].isBlob ? \'100644 \' : \'40000 \') + treeEntries[i].name);\n\t\t\t\tbb.push(new Uint8Array([0]));\n\t\t\t\tbb.push(treeEntries[i].sha);\n\t\t\t}\n\t\t\tthis.writeRawObject(\'tree\', new Blob(bb), function(sha){\n\t\t\t\tsuccess(sha);\n\t\t\t});\n\t\t},\n\n\t\tgetConfig : function(success){\n\t\t\tvar fe = this.fileError;\n\n\t\t\tfileutils.readFile(this.dir, \'.git/config.json\', \'Text\', function(configStr){\n\t\t\t\tsuccess(JSON.parse(configStr));\n\t\t\t}, function(e){\n\t\t\t\tif (e.code == FileError.NOT_FOUND_ERR){\n\t\t\t\t\tsuccess({});\n\t\t\t\t}\n\t\t\t\telse{\n\t\t\t\t\tfe(e);\n\t\t\t\t}\n\t\t\t});\n\t\t},\n\t\tsetConfig : function(config, success){\n\t\t\tvar configStr = JSON.stringify(config);\n\t\t\tfileutils.mkfile(this.dir, \'.git/config.json\', configStr, success, this.fileError);\n\t\t},\n\n\t\tupdateLastChange : function(config, success){\n\t\t\tvar dir = this.dir,\n\t\t\t\tsetConfig = this.setConfig.bind(this);\n\t\t\t\tfe = this.fileError;\n\n\t\t\tvar doUpdate = function(config){\n\t\t\t\tconfig.time = new Date();\n\t\t\t\tsetConfig(config, success);\n\t\t\t}\n\t\t\tif (config){\n\t\t\t\tdoUpdate(config);\n\t\t\t}\n\t\t\telse{\n\t\t\t\tthis.getConfig(doUpdate);\n\t\t\t}\n\t\t}\n\t\t\n\t}\n\n\treturn FileObjectStore;\n});\n\n\n/*\n\nThis is a Javascript implementation of the C implementation of the CRC-32\nalgorithm available at http://www.w3.org/TR/PNG-CRCAppendix.html\n\nUsage License at\nhttp://www.w3.org/Consortium/Legal/2002/copyright-software-20021231\n\nCopyright (C) W3C\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nPermission to copy, modify, and distribute this software and its\ndocumentation, with or without modification, for any purpose and without\nfee or royalty is hereby granted, provided that you include the\nfollowing on ALL copies of the software and documentation or portions\nthereof, including modifications:\n\n1. The full text of this NOTICE in a location viewable to users of\nthe redistributed or derivative work.\n2. Any pre-existing intellectual property disclaimers, notices, or\nterms and conditions. If none exist, the W3C Software Short Notice\nshould be included (hypertext is preferred, text is permitted)\nwithin the body of any redistributed or derivative code.\n3. Notice of any changes or modifications to the files,\nincluding the date changes were made. (We recommend you provide\nURIs to the location from which the code is derived.)\n\nTHIS SOFTWARE AND DOCUMENTATION IS PROVIDED "AS IS," AND\nCOPYRIGHT HOLDERS MAKE NO REPRESENTATIONS OR WARRANTIES,\nEXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO, WARRANTIES OF\nMERCHANTABILITY OR FITNESS FOR ANY PARTICULAR PURPOSE OR THAT\nTHE USE OF THE SOFTWARE OR DOCUMENTATION WILL NOT INFRINGE ANY\nTHIRD PARTY PATENTS, COPYRIGHTS, TRADEMARKS OR OTHER RIGHTS.\n\nCOPYRIGHT HOLDERS WILL NOT BE LIABLE FOR ANY DIRECT, INDIRECT,\nSPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF ANY USE OF THE\nSOFTWARE OR DOCUMENTATION.\n\nThe name and trademarks of copyright holders may NOT be used in\nadvertising or publicity pertaining to the software without\nspecific, written prior permission. Title to copyright in this\nsoftware and any associated documentation will at all times\nremain with copyright holders.\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n*/\n\nvar crc32 = {\n    table: [\n        0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419, 0x706af48f, 0xe963a535, 0x9e6495a3,\n             0x0edb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91,\n             0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7,\n             0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5,\n             0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b,\n             0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59,\n             0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f,\n             0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d,\n             0x76dc4190, 0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f, 0x9fbfe4a5, 0xe8b8d433,\n             0x7807c9a2, 0x0f00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01,\n             0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457,\n             0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65,\n             0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb,\n             0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9,\n             0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,\n             0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad,\n             0xedb88320, 0x9abfb3b6, 0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615, 0x73dc1683,\n             0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1,\n             0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7,\n             0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,\n             0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b,\n             0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79,\n             0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f,\n             0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d,\n             0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713,\n             0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21,\n             0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777,\n             0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45,\n             0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db,\n             0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,\n             0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf,\n             0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d,\n    ],\n\n    crc: function(data)\n    {\n        var crc = 0xffffffff;\n\n        for(var i = 0; i < data.length; i++) {\n            var b = data[i];\n            crc = (crc >>> 8) ^ this.table[(crc ^ b) & 0xff];\n            //crc = this.table[(crc ^ data[i]) & 0xff] ^ (crc >> 8);\n        }\n\n        crc = crc ^ 0xffffffff;\n        return crc;\n    },\n};\ndefine("thirdparty/crc32", function(){});\n\nGitLiteWorkerMessages = {\n    PROGRESS : 0,\n    FINISHED: 1,\n    RETRIEVE_OBJECT: 2,\n    START: 4,\n    OBJECT_RETRIEVED: 5,\n\n    API_CALL_CLONE: 6,\n    API_CALL_COMMIT: 7,\n    API_CALL_PULL: 8,\n    API_CALL_PUSH: 9,\n    API_CALL_CHECKOUT: 12,\n    API_CALL_BRANCH: 14,\n    API_CALL_UNCOMMITTED: 15,\n    API_CALL_CURRENT_BRANCH: 16, \n    API_CALL_LOCAL_BRANCHES: 17,\n    API_CALL_REMOTE_BRANCHES: 18,\n\n    SUCCESS: 10,\n    ERROR: 11\n};\ndefine("workers/worker_messages", function(){});\n\n// requirejs.config({\n//     shim: {\n\n//     }\n// });\n\ndefine(\'api\',[\'commands/clone\', \'commands/commit\', \'commands/init\', \'commands/pull\', \'commands/push\', \'commands/branch\', \'commands/checkout\', \'commands/conditions\', \'objectstore/file_repo\', \'formats/smart_http_remote\', \'utils/errors\', \'thirdparty/2.2.0-sha1\', \'thirdparty/crc32\', \'thirdparty/deflate.min\', \'thirdparty/inflate.min\', "workers/worker_messages"], function(clone, commit, init, pull, push, branch, checkout, Conditions, FileObjectStore, SmartHttpRemote, errutils){\n    \n    var api = {\n\n         // Indicates an unexpected error in the file system.\n        FILE_IO_ERROR: errutils.FILE_IO_ERROR,\n        // Indicates an unexpected ajax error when trying to make a request\n        AJAX_ERROR: errutils.AJAX_ERROR, \n        // trying to clone into a non-empty directory\n        CLONE_DIR_NOT_EMPTY: errutils.CLONE_DIR_NOT_EMPTY,\n        // .git directory already contains objects\n        CLONE_GIT_DIR_IN_USE: errutils.CLONE_GIT_DIR_IN_USE,\n        // No branch found with the name given\n        REMOTE_BRANCH_NOT_FOUND: errutils.REMOTE_BRANCH_NOT_FOUND,\n        // only supports fast forward merging at the moment.\n        PULL_NON_FAST_FORWARD: errutils.PULL_NON_FAST_FORWARD,\n        // Branch is up to date\n        PULL_UP_TO_DATE: errutils.PULL_UP_TO_DATE,\n        // Nothing to commit\n        COMMIT_NO_CHANGES: errutils.COMMIT_NO_CHANGES,\n        // The remote repo and the local repo share the same head.\n        PUSH_NO_CHANGES: errutils.PUSH_NO_CHANGES,\n        // Need to merge remote changes first.\n        PUSH_NON_FAST_FORWARD: errutils.PUSH_NON_FAST_FORWARD,\n        // unexpected problem retrieving objects\n        OBJECT_STORE_CORRUPTED: errutils.OBJECT_STORE_CORRUPTED,\n        // pull is attempted with uncommitted changed\n        UNCOMMITTED_CHANGES: errutils.UNCOMMITTED_CHANGES,\n        // 401 when attempting to make a request\n        HTTP_AUTH_ERROR: errutils.HTTP_AUTH_ERROR,\n\n        BRANCH_NAME_NOT_VALID: errutils.BRANCH_NAME_NOT_VALID,\n\n        PUSH_NO_REMOTE: errutils.PUSH_NO_REMOTE,\n        \n        // init : function(options, success, error){\n        //     var objectStore = new FileObjectStore(options.dir);\n        //     init({objectStore: objectStore}, success, error);\n        // },\n        clone : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                \n                clone({\n                        dir: options.dir, \n                        branch: options.branch, \n                        objectStore: objectStore, \n                        url: options.url, \n                        depth: options.depth, \n                        progress: options.progress,\n                        username: options.username,\n                        password: options.password\n                    }, \n                    success, error);\n\n            }, error);\n        },\n        pull : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                \n                pull({\n                        dir: options.dir, \n                        objectStore: objectStore,\n                        username: options.username,\n                        password: options.password,\n                        progress: options.progress\n                    }, \n                    success, error);\n\n            }, error);\n        },\n        commit : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                \n                commit({\n                            dir: options.dir, \n                            username: options.name, \n                            email: options.email, \n                            commitMsg: options.commitMsg, \n                            objectStore: objectStore\n                        }, success, error);\n            }, error);\n        },\n        push : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                push({\n                        objectStore: objectStore, \n                        dir: options.dir, \n                        url: options.url,\n                        username: options.username,\n                        password: options.password,\n                        progress: options.progress\n                    }, \n                    success, error);\n            }, error);\n        },\n        branch: function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                branch({\n                    objectStore: objectStore,\n                    dir: options.dir,\n                    branch: options.branch\n                }, success, error);\n            }, error);\n\n        },\n        checkout: function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                checkout({\n                    objectStore: objectStore,\n                    dir: options.dir,\n                    branch: options.branch\n                }, success, error);\n            }, error);\n        },\n\n        checkForUncommittedChanges: function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                Conditions.checkForUncommittedChanges(options.dir, objectStore, success, error);\n            }, error);\n        },\n        getCurrentBranch : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                objectStore.getHeadRef(function(ref){\n                    success(ref.substring(\'refs/heads/\'.length));\n                });\n            }, error);\n        },\n        getLocalBranches : function(options, success, error){\n            var objectStore = new FileObjectStore(options.dir);\n            objectStore.init(function(){\n                objectStore.getAllHeads(success);\n            }, error);\n        },\n        getRemoteBranches : function(options, success, error){\n            var remote = SmartHttpRemote(null, null, options.url, options.username, options.password, error);\n            remote.fetchRefs(function(refs){\n                var remoteBranches = [];\n                refs.forEach(function(ref){\n                    if (ref.name.indexOf(\'refs/heads/\') == 0){\n                        remoteBranches.push(ref.name.substring(\'refs/heads/\'.length));\n                    }\n                });\n                success(remoteBranches);\n            });\n        }\n\n    }\n    return api;\n});\ndefine(\'workers/api-worker\',[\'api\', \'utils/errors\', \'workers/worker_messages\'], function(GitLite, errutils){\n\n    self.requestFileSystem = self.requestFileSystem || self.webkitRequestFileSystem;\n\n    var convertToDirEntry = function(dir, success, error){\n        self.requestFileSystem(PERSISTENT, 5 * 1024 * 1024 * 1024, function(fs){\n            fs.root.getDirectory(dir, {create: false}, success, error);\n        }, error);\n    };\n\n    return function(){\n        onmessage = function(evt){\n            var msg = evt.data;\n            var id = evt.data.id;\n            var scrubArgs = function(args){\n                for (var i = 0; i < args.length; i++){\n                    args[i] = args[i].fullPath || args[i];\n                }\n            }\n            var successCallback = function(){\n                var args = Array.prototype.slice.call(arguments);\n                scrubArgs(args);\n                postMessage({id: id, type: GitLiteWorkerMessages.SUCCESS, args: args});\n                //self.close();\n            }\n\n            var errCallback = function(e){\n                postMessage({id: id, type: GitLiteWorkerMessages.ERROR, error:e});\n                //self.close();\n            }\n            var progressCallback;\n            if (msg.options.progress){\n                progressCallback = function(){\n                    var args = Array.prototype.slice.call(arguments);\n                    postMessage({id: id, type: GitLiteWorkerMessages.PROGRESS, args: args});\n                }\n                msg.options.progress = progressCallback;\n            }\n            var ferror = errutils.fileErrorFunc(errCallback);\n\n            var doApiCall = function(func){\n                convertToDirEntry(msg.options.dir, function(dirEntry){\n                    msg.options.dir = dirEntry;\n                    func.call(null, msg.options, successCallback, errCallback);\n                }, ferror);\n            }\n\n            switch(msg.type){\n                \n                case GitLiteWorkerMessages.API_CALL_CLONE:\n                    doApiCall(GitLite.clone);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_COMMIT:\n                    doApiCall(GitLite.commit);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_PULL:\n                    doApiCall(GitLite.pull);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_PUSH:\n                    doApiCall(GitLite.push);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_BRANCH:\n                    doApiCall(GitLite.branch);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_CHECKOUT:\n                    doApiCall(GitLite.checkout);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_UNCOMMITTED:\n                    doApiCall(GitLite.checkForUncommittedChanges);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_CURRENT_BRANCH:\n                    doApiCall(GitLite.getCurrentBranch);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_LOCAL_BRANCHES:\n                    doApiCall(GitLite.getLocalBranches);\n                    break;\n                case GitLiteWorkerMessages.API_CALL_REMOTE_BRANCHES:\n                    doApiCall(GitLite.getRemoteBranches);\n                    break;\n            }\n        }\n    }\n});    //The modules for your project will be inlined above\n    //this snippet. Ask almond to synchronously require the\n    //module value for \'main\' here and return it as the\n    //value to use for the public API for the built file.\n    return require(\'workers/api-worker\');\n}));';});

define('utils/errors',[],function() {

    var errors = {
        // Indicates an unexpected error in the file system.
        FILE_IO_ERROR: 0,
        FILE_IO_ERROR_MSG: 'Unexpected File I/O error',
        // Indicates an unexpected ajax error when trying to make a request
        AJAX_ERROR: 1, 
        AJAX_ERROR_MSG: 'Unexpected ajax error',
        
        // trying to clone into a non-empty directory
        CLONE_DIR_NOT_EMPTY: 2,
        CLONE_DIR_NOT_EMPTY_MSG: 'The target directory contains files',
        // No .git directory
        CLONE_DIR_NOT_INTIALIZED: 3,
        CLONE_DIR_NOT_INTIALIZED_MSG: 'The target directory hasn\'t been initialized.',
        // .git directory already contains objects
        CLONE_GIT_DIR_IN_USE: 4,
        CLONE_GIT_DIR_IN_USE_MSG: 'The target directory contains a .git directory already in use.',
        // No branch found with the name given
        REMOTE_BRANCH_NOT_FOUND: 5,
        REMOTE_BRANCH_NOT_FOUND_MSG: 'Can\'t find the branch name in the remote repository',

        // only supports fast forward merging at the moment.
        PULL_NON_FAST_FORWARD: 6,
        PULL_NON_FAST_FORWARD_MSG: 'Pulling from the remote repo requires a merge.',
        // Branch is up to date
        PULL_UP_TO_DATE: 7,
        PULL_UP_TO_DATE_MSG: 'Everything is up to date',


        UNCOMMITTED_CHANGES: 11,
        UNCOMMITTED_CHANGES_MSG: 'There are changes in the working directory that haven\'t been committed',

        // Nothing to commit
        COMMIT_NO_CHANGES: 8,
        COMMIT_NO_CHANGES_MSG: 'No changes to commit',

        // The remote repo and the local repo share the same head.
        PUSH_NO_CHANGES: 9,
        PUSH_NO_CHANGES_MSG: 'No new commits to push to the repository',

        PUSH_NO_REMOTE: 16,
        PUSH_NO_REMOTE_MSG: 'No remote to push to',

        // Need to merge remote changes first. 
        PUSH_NON_FAST_FORWARD: 10,
        PUSH_NON_FAST_FORWARD_MSG: 'The remote repo has new commits on your current branch. You need to merge them first.',

        BRANCH_ALREADY_EXISTS: 14,
        BRANCH_ALREADY_EXISTS_MSG: 'A local branch with that name already exists',

        BRANCH_NAME_NOT_VALID: 12,
        BRANCH_NAME_NOT_VALID_MSG: 'The branch name is not valid.',

        CHECKOUT_BRANCH_NO_EXISTS: 15,
        CHECKOUT_BRANCH_NO_EXISTS_MSG: 'No local branch with that name exists',

        // unexpected problem retrieving objects
        OBJECT_STORE_CORRUPTED: 200,
        OBJECT_STORE_CORRUPTED_MSG: 'Git object store may be corrupted',

        HTTP_AUTH_ERROR: 201,
        HTTP_AUTH_ERROR_MSG: 'Http authentication failed',

        UNPACK_ERROR: 202,
        UNPACK_ERROR_MSG: 'The remote git server wasn\'t able to understand the push request.',

        
        fileErrorFunc : function(onError){
            if (!onError){
                return function(){};
            }
            return function(e) {
                var msg = errors.getFileErrorMsg(e);
                onError({type : errors.FILE_IO_ERROR, msg: msg, fe: e.code});
            }
        },

        ajaxErrorFunc : function(onError){
            return function(xhr){
                var url = this.url,
                    reqType = this.type;

                var httpErr;
                if (xhr.status == 401){
                    var auth = xhr.getResponseHeader('WWW-Authenticate');
                    httpErr = {type: errors.HTTP_AUTH_ERROR, msg: errors.HTTP_AUTH_ERROR_MSG, auth: auth};
                }
                else{
                    httpErr = {type: errors.AJAX_ERROR, url: url, reqType: reqType, statusText: xhr.statusText, status: xhr.status, msg: "Http error with status code: " + xhr.status + ' and status text: "' + xhr.statusText + '"'};
                }
                onError(httpErr);  
            }
        },

        getFileErrorMsg: function(e) {
            var msg = '';

            switch (e.code) {
                case FileError.QUOTA_EXCEEDED_ERR:
                    msg = 'QUOTA_EXCEEDED_ERR';
                    break;
                case FileError.NOT_FOUND_ERR:
                    msg = 'NOT_FOUND_ERR';
                    break;
                case FileError.SECURITY_ERR:
                    msg = 'SECURITY_ERR';
                    break;
                case FileError.INVALID_MODIFICATION_ERR:
                    msg = 'INVALID_MODIFICATION_ERR';
                    break;
                case FileError.INVALID_STATE_ERR:
                    msg = 'INVALID_STATE_ERR';
                    break;
                case FileError.ABORT_ERR:
                    msg = 'ABORT_ERR';
                    break;
                case FileError.ENCODING_ERR:
                    msg = 'ENCODING_ERR';
                    break;
                case FileError.NOT_READABLE_ERR:
                    msg = 'NOT_READABLE_ERR';
                    break;
                case FileError.NO_MODIFICATION_ALLOWED_ERR:
                    msg = 'NO_MODIFICATION_ALLOWED_ERR';
                    break;
                case FileError.PATH_EXISTS_ERR:
                    msg = 'PATH_EXISTS_ERR';
                    break;
                case FileError.SYNTAX_ERR:
                    msg = 'SYNTAX_ERR';
                    break;
                case FileError.TYPE_MISMATCH_ERR:
                    msg = 'TYPE_MISMATCH_ERR';
                    break;
                default:
                    msg = 'Unknown Error ' + e.code;
                    break;
            };
        },
        errorHandler: function(e) {
            msg = utils.getFileErrorMsg(e);
            console.log('Error: ' + msg);
        }
    }
    return errors;

});
GitLiteWorkerMessages = {
    PROGRESS : 0,
    FINISHED: 1,
    RETRIEVE_OBJECT: 2,
    START: 4,
    OBJECT_RETRIEVED: 5,

    API_CALL_CLONE: 6,
    API_CALL_COMMIT: 7,
    API_CALL_PULL: 8,
    API_CALL_PUSH: 9,
    API_CALL_CHECKOUT: 12,
    API_CALL_BRANCH: 14,
    API_CALL_UNCOMMITTED: 15,
    API_CALL_CURRENT_BRANCH: 16, 
    API_CALL_LOCAL_BRANCHES: 17,
    API_CALL_REMOTE_BRANCHES: 18,

    SUCCESS: 10,
    ERROR: 11
};
define("workers/worker_messages", function(){});

define('workers/api-worker-proxy',['text!workers/api-worker-built.js', 'utils/errors', 'workers/worker_messages'],function(apiWorkerText, errutils){
    var workerBlob = new Blob([apiWorkerText], {type: "text/javascript"});
    var workerUrl = URL.createObjectURL(workerBlob);

    var newResponseHandler = function(success, error, progress, worker){
        var updateProgress = function(){}
        if (progress){
            updateProgress = function(msg){
                progress.apply(null, msg.args || []);
            }
        }
        error = error || function(e){console.error(e);}
        return function(evt){

            var msg = evt.data;
            switch(msg.type){
                case GitLiteWorkerMessages.SUCCESS:
                    console.log('success');
                    success.apply(null, msg.args || []);
                    //worker.terminate();
                    break;
                case GitLiteWorkerMessages.ERROR:
                    error(msg.error);
                    //worker.terminate();
                    break;
                case GitLiteWorkerMessages.PROGRESS:
                    updateProgress(msg);
                    break;
            }
        }
    }
    var id = 0;
    callbacks = {};
    var worker;
    var doApiCall = function(type, options, success, error){
        options.dir = options.dir.fullPath;
        if (!worker){
            worker = new Worker(workerUrl);
            var errorHandler = error || function(e){};
            worker.onmessage = function(evt){
                var msgHandler = callbacks[evt.data.id];
                msgHandler.call(null, evt);
                if (evt.data.type == GitLiteWorkerMessages.SUCCESS || evt.data.type == GitLiteWorkerMessages.ERROR){
                    delete callbacks[id];
                }
            }
            worker.onerror = function(e){
                errorHandler({msg: e.message});
            }
        }
        callbacks[id] = newResponseHandler(success, error, options.progress);
        if (options.progress){
            options.progress = true;
        }
        worker.postMessage({id: id++, type: type, options: options});
    }

    var api = {

         // Indicates an unexpected error in the file system.
        FILE_IO_ERROR: errutils.FILE_IO_ERROR,
        // Indicates an unexpected ajax error when trying to make a request
        AJAX_ERROR: errutils.AJAX_ERROR, 
        // trying to clone into a non-empty directory
        CLONE_DIR_NOT_EMPTY: errutils.CLONE_DIR_NOT_EMPTY,
        // .git directory already contains objects
        CLONE_GIT_DIR_IN_USE: errutils.CLONE_GIT_DIR_IN_USE,
        // No branch found with the name given
        REMOTE_BRANCH_NOT_FOUND: errutils.REMOTE_BRANCH_NOT_FOUND,
        // only supports fast forward merging at the moment.
        PULL_NON_FAST_FORWARD: errutils.PULL_NON_FAST_FORWARD,
        // Branch is up to date
        PULL_UP_TO_DATE: errutils.PULL_UP_TO_DATE,
        // Nothing to commit
        COMMIT_NO_CHANGES: errutils.COMMIT_NO_CHANGES,
        // The remote repo and the local repo share the same head.
        PUSH_NO_CHANGES: errutils.PUSH_NO_CHANGES,
        // Need to merge remote changes first.
        PUSH_NON_FAST_FORWARD: errutils.PUSH_NON_FAST_FORWARD,
        // unexpected problem retrieving objects
        OBJECT_STORE_CORRUPTED: errutils.OBJECT_STORE_CORRUPTED,
        // pull is attempted with uncommitted changed
        UNCOMMITTED_CHANGES: errutils.UNCOMMITTED_CHANGES,
        // 401 when attempting to make a request
        HTTP_AUTH_ERROR: errutils.HTTP_AUTH_ERROR,

        BRANCH_NAME_NOT_VALID: errutils.BRANCH_NAME_NOT_VALID,

        PUSH_NO_REMOTE: errutils.PUSH_NO_REMOTE,

        
        clone : function(options, success, error){
            // var objectStore = new FileObjectStore(options.dir);
            // objectStore.init(function(){
            //     //clone(dir, objectStore, url, callback);
            //     clone({dir: options.dir, branch: options.branch, objectStore: objectStore, url: options.url, depth: options.depth, progress: options.progress}, success, error);
            // }, error);
            doApiCall(GitLiteWorkerMessages.API_CALL_CLONE, options, success, error);
            
        },
        pull : function(options, success, error){
            // var objectStore = new FileObjectStore(options.dir);
            // objectStore.init(function(){
            //     //pull(dir, objectStore, url, callback);
            //     pull({dir: options.dir, objectStore: objectStore}, success, error);
            // }, error);
            doApiCall(GitLiteWorkerMessages.API_CALL_PULL, options, success, error);

        },
        commit : function(options, success, error){
            // var objectStore = new FileObjectStore(options.dir);
            // objectStore.init(function(){
            //     //commit(dir, objectStore, callback);
            //     commit({dir: options.dir, username: options.username, email: options.email, commitMsg: options.commitMsg, objectStore: objectStore}, success, error);
            // }, error);
            doApiCall(GitLiteWorkerMessages.API_CALL_COMMIT, options, success, error);
        },
        push : function(options, success, error){
            // var objectStore = new FileObjectStore(options.dir);
            // objectStore.init(function(){
            //     push({objectStore: objectStore, dir: options.dir, url: options.url}, success, error);
            // }, error);
            doApiCall(GitLiteWorkerMessages.API_CALL_PUSH, options, success, error);
        },

        branch : function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_BRANCH, options, success, error);
        },
        checkout : function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_CHECKOUT, options, success, error);
        },
        checkForUncommittedChanges: function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_UNCOMMITTED, options, success, error);
        },
        getCurrentBranch : function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_CURRENT_BRANCH, options, success, error);
        },
        getLocalBranches : function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_LOCAL_BRANCHES, options, success, error);
        },
        getRemoteBranches : function(options, success, error){
            doApiCall(GitLiteWorkerMessages.API_CALL_REMOTE_BRANCHES, options, success, error);
        }
    }
    return api;
});    //The modules for your project will be inlined above
    //this snippet. Ask almond to synchronously require the
    //module value for 'main' here and return it as the
    //value to use for the public API for the built file.
    return require('workers/api-worker-proxy');
}));
/*!
 *  Copyright 2011 Twitter, Inc.
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */



var Hogan = {};

(function (Hogan, useArrayBuffer) {
  Hogan.Template = function (codeObj, text, compiler, options) {
    codeObj = codeObj || {};
    this.r = codeObj.code || this.r;
    this.c = compiler;
    this.options = options || {};
    this.text = text || '';
    this.partials = codeObj.partials || {};
    this.subs = codeObj.subs || {};
    this.ib();
  }

  Hogan.Template.prototype = {
    // render: replaced by generated code.
    r: function (context, partials, indent) { return ''; },

    // variable escaping
    v: hoganEscape,

    // triple stache
    t: coerceToString,

    render: function render(context, partials, indent) {
      return this.ri([context], partials || {}, indent);
    },

    // render internal -- a hook for overrides that catches partials too
    ri: function (context, partials, indent) {
      return this.r(context, partials, indent);
    },

    // ensurePartial
    ep: function(symbol, partials) {
      var partial = this.partials[symbol];

      // check to see that if we've instantiated this partial before
      var template = partials[partial.name];
      if (partial.instance && partial.base == template) {
        return partial.instance;
      }

      if (typeof template == 'string') {
        if (!this.c) {
          throw new Error("No compiler available.");
        }
        template = this.c.compile(template, this.options);
      }

      if (!template) {
        return null;
      }

      // We use this to check whether the partials dictionary has changed
      this.partials[symbol].base = template;

      if (partial.subs) {
        // Make sure we consider parent template now
        if (this.activeSub === undefined) {
          // Store parent template text in partials.stackText to perform substitutions in child templates correctly
          partials.stackText  = this.text;
        }
         template = createSpecializedPartial(template, partial.subs, partial.partials, partials.stackText || this.text);
       }
      this.partials[symbol].instance = template;
      return template;
    },

    // tries to find a partial in the current scope and render it
    rp: function(symbol, context, partials, indent) {
      var partial = this.ep(symbol, partials);
      if (!partial) {
        return '';
      }

      return partial.ri(context, partials, indent);
    },

    // render a section
    rs: function(context, partials, section) {
      var tail = context[context.length - 1];

      if (!isArray(tail)) {
        section(context, partials, this);
        return;
      }

      for (var i = 0; i < tail.length; i++) {
        context.push(tail[i]);
        section(context, partials, this);
        context.pop();
      }
    },

    // maybe start a section
    s: function(val, ctx, partials, inverted, start, end, tags) {
      var pass;

      if (isArray(val) && val.length === 0) {
        return false;
      }

      if (typeof val == 'function') {
        val = this.ms(val, ctx, partials, inverted, start, end, tags);
      }

      pass = !!val;

      if (!inverted && pass && ctx) {
        ctx.push((typeof val == 'object') ? val : ctx[ctx.length - 1]);
      }

      return pass;
    },

    // find values with dotted names
    d: function(key, ctx, partials, returnFound) {
      var found,
          names = key.split('.'),
          val = this.f(names[0], ctx, partials, returnFound),
          doModelGet = this.options.modelGet,
          cx = null;

      if (key === '.' && isArray(ctx[ctx.length - 2])) {
        val = ctx[ctx.length - 1];
      } else {
        for (var i = 1; i < names.length; i++) {
          found = findInScope(names[i], val, doModelGet);
          if (found != null) {
            cx = val;
            val = found;
          } else {
            val = '';
          }
        }
      }

      if (returnFound && !val) {
        return false;
      }

      if (!returnFound && typeof val == 'function') {
        ctx.push(cx);
        val = this.mv(val, ctx, partials);
        ctx.pop();
      }

      return val;
    },

    // find values with normal names
    f: function(key, ctx, partials, returnFound) {
      var val = false,
          v = null,
          found = false,
          doModelGet = this.options.modelGet;

      for (var i = ctx.length - 1; i >= 0; i--) {
        v = ctx[i];
        val = findInScope(key, v, doModelGet);
        if (val != null) {
          found = true;
          break;
        }
      }

      if (!found) {
        return (returnFound) ? false : "";
      }

      if (!returnFound && typeof val == 'function') {
        val = this.mv(val, ctx, partials);
      }

      return val;
    },

    // higher order templates
    ls: function(func, cx, partials, text, tags) {
      var oldTags = this.options.delimiters;

      this.options.delimiters = tags;
      this.b(this.ct(coerceToString(func.call(cx, text)), cx, partials));
      this.options.delimiters = oldTags;

      return false;
    },

    // compile text
    ct: function(text, cx, partials) {
      if (this.options.disableLambda) {
        throw new Error('Lambda features disabled.');
      }
      return this.c.compile(text, this.options).render(cx, partials);
    },

    // template result buffering
    b: (useArrayBuffer) ? function(s) { this.buf.push(s); } :
                          function(s) { this.buf += s; },

    fl: (useArrayBuffer) ? function() { var r = this.buf.join(''); this.buf = []; return r; } :
                           function() { var r = this.buf; this.buf = ''; return r; },
    // init the buffer
    ib: function () {
      this.buf = (useArrayBuffer) ? [] : '';
    },

    // method replace section
    ms: function(func, ctx, partials, inverted, start, end, tags) {
      var textSource,
          cx = ctx[ctx.length - 1],
          result = func.call(cx);

      if (typeof result == 'function') {
        if (inverted) {
          return true;
        } else {
          textSource = (this.activeSub && this.subsText[this.activeSub]) ? this.subsText[this.activeSub] : this.text;
          return this.ls(result, cx, partials, textSource.substring(start, end), tags);
        }
      }

      return result;
    },

    // method replace variable
    mv: function(func, ctx, partials) {
      var cx = ctx[ctx.length - 1];
      var result = func.call(cx);

      if (typeof result == 'function') {
        return this.ct(coerceToString(result.call(cx)), cx, partials);
      }

      return result;
    },

    sub: function(name, context, partials, indent) {
      var f = this.subs[name];
      if (f) {
        this.activeSub = name;
        f(context, partials, this, indent);
        this.activeSub = false;
      }
    }

  };

  //Find a key in an object
  function findInScope(key, scope, doModelGet) {
    var val, checkVal;

    if (scope && typeof scope == 'object') {

      if (scope[key] != null) {
        val = scope[key];

      // try lookup with get for backbone or similar model data
      } else if (doModelGet && scope.get && typeof scope.get == 'function') {
        val = scope.get(key);
      }
    }

    return val;
  }

  function createSpecializedPartial(instance, subs, partials, childText) {
    function PartialTemplate() {};
    PartialTemplate.prototype = instance;
    function Substitutions() {};
    Substitutions.prototype = instance.subs;
    var key;
    var partial = new PartialTemplate();
    partial.subs = new Substitutions();
    partial.subsText = {};  //hehe. substext.
    partial.ib();

    for (key in subs) {
      partial.subs[key] = subs[key];
      partial.subsText[key] = childText;
    }

    for (key in partials) {
      partial.partials[key] = partials[key];
    }

    return partial;
  }

  var rAmp = /&/g,
      rLt = /</g,
      rGt = />/g,
      rApos = /\'/g,
      rQuot = /\"/g,
      hChars = /[&<>\"\']/;

  function coerceToString(val) {
    return String((val === null || val === undefined) ? '' : val);
  }

  function hoganEscape(str) {
    str = coerceToString(str);
    return hChars.test(str) ?
      str
        .replace(rAmp, '&amp;')
        .replace(rLt, '&lt;')
        .replace(rGt, '&gt;')
        .replace(rApos, '&#39;')
        .replace(rQuot, '&quot;') :
      str;
  }

  var isArray = Array.isArray || function(a) {
    return Object.prototype.toString.call(a) === '[object Array]';
  };

})(typeof exports !== 'undefined' ? exports : Hogan);



(function (Hogan) {
  // Setup regex  assignments
  // remove whitespace according to Mustache spec
  var rIsWhitespace = /\S/,
      rQuot = /\"/g,
      rNewline =  /\n/g,
      rCr = /\r/g,
      rSlash = /\\/g;

  Hogan.tags = {
    '#': 1, '^': 2, '<': 3, '$': 4,
    '/': 5, '!': 6, '>': 7, '=': 8, '_v': 9,
    '{': 10, '&': 11, '_t': 12
  };

  Hogan.scan = function scan(text, delimiters) {
    var len = text.length,
        IN_TEXT = 0,
        IN_TAG_TYPE = 1,
        IN_TAG = 2,
        state = IN_TEXT,
        tagType = null,
        tag = null,
        buf = '',
        tokens = [],
        seenTag = false,
        i = 0,
        lineStart = 0,
        otag = '{{',
        ctag = '}}';

    function addBuf() {
      if (buf.length > 0) {
        tokens.push({tag: '_t', text: new String(buf)});
        buf = '';
      }
    }

    function lineIsWhitespace() {
      var isAllWhitespace = true;
      for (var j = lineStart; j < tokens.length; j++) {
        isAllWhitespace =
          (Hogan.tags[tokens[j].tag] < Hogan.tags['_v']) ||
          (tokens[j].tag == '_t' && tokens[j].text.match(rIsWhitespace) === null);
        if (!isAllWhitespace) {
          return false;
        }
      }

      return isAllWhitespace;
    }

    function filterLine(haveSeenTag, noNewLine) {
      addBuf();

      if (haveSeenTag && lineIsWhitespace()) {
        for (var j = lineStart, next; j < tokens.length; j++) {
          if (tokens[j].text) {
            if ((next = tokens[j+1]) && next.tag == '>') {
              // set indent to token value
              next.indent = tokens[j].text.toString()
            }
            tokens.splice(j, 1);
          }
        }
      } else if (!noNewLine) {
        tokens.push({tag:'\n'});
      }

      seenTag = false;
      lineStart = tokens.length;
    }

    function changeDelimiters(text, index) {
      var close = '=' + ctag,
          closeIndex = text.indexOf(close, index),
          delimiters = trim(
            text.substring(text.indexOf('=', index) + 1, closeIndex)
          ).split(' ');

      otag = delimiters[0];
      ctag = delimiters[delimiters.length - 1];

      return closeIndex + close.length - 1;
    }

    if (delimiters) {
      delimiters = delimiters.split(' ');
      otag = delimiters[0];
      ctag = delimiters[1];
    }

    for (i = 0; i < len; i++) {
      if (state == IN_TEXT) {
        if (tagChange(otag, text, i)) {
          --i;
          addBuf();
          state = IN_TAG_TYPE;
        } else {
          if (text.charAt(i) == '\n') {
            filterLine(seenTag);
          } else {
            buf += text.charAt(i);
          }
        }
      } else if (state == IN_TAG_TYPE) {
        i += otag.length - 1;
        tag = Hogan.tags[text.charAt(i + 1)];
        tagType = tag ? text.charAt(i + 1) : '_v';
        if (tagType == '=') {
          i = changeDelimiters(text, i);
          state = IN_TEXT;
        } else {
          if (tag) {
            i++;
          }
          state = IN_TAG;
        }
        seenTag = i;
      } else {
        if (tagChange(ctag, text, i)) {
          tokens.push({tag: tagType, n: trim(buf), otag: otag, ctag: ctag,
                       i: (tagType == '/') ? seenTag - otag.length : i + ctag.length});
          buf = '';
          i += ctag.length - 1;
          state = IN_TEXT;
          if (tagType == '{') {
            if (ctag == '}}') {
              i++;
            } else {
              cleanTripleStache(tokens[tokens.length - 1]);
            }
          }
        } else {
          buf += text.charAt(i);
        }
      }
    }

    filterLine(seenTag, true);

    return tokens;
  }

  function cleanTripleStache(token) {
    if (token.n.substr(token.n.length - 1) === '}') {
      token.n = token.n.substring(0, token.n.length - 1);
    }
  }

  function trim(s) {
    if (s.trim) {
      return s.trim();
    }

    return s.replace(/^\s*|\s*$/g, '');
  }

  function tagChange(tag, text, index) {
    if (text.charAt(index) != tag.charAt(0)) {
      return false;
    }

    for (var i = 1, l = tag.length; i < l; i++) {
      if (text.charAt(index + i) != tag.charAt(i)) {
        return false;
      }
    }

    return true;
  }

  // the tags allowed inside super templates
  var allowedInSuper = {'_t': true, '\n': true, '$': true, '/': true};

  function buildTree(tokens, kind, stack, customTags) {
    var instructions = [],
        opener = null,
        tail = null,
        token = null;

    tail = stack[stack.length - 1];

    while (tokens.length > 0) {
      token = tokens.shift();

      if (tail && tail.tag == '<' && !(token.tag in allowedInSuper)) {
        throw new Error('Illegal content in < super tag.');
      }

      if (Hogan.tags[token.tag] <= Hogan.tags['$'] || isOpener(token, customTags)) {
        stack.push(token);
        token.nodes = buildTree(tokens, token.tag, stack, customTags);
      } else if (token.tag == '/') {
        if (stack.length === 0) {
          throw new Error('Closing tag without opener: /' + token.n);
        }
        opener = stack.pop();
        if (token.n != opener.n && !isCloser(token.n, opener.n, customTags)) {
          throw new Error('Nesting error: ' + opener.n + ' vs. ' + token.n);
        }
        opener.end = token.i;
        return instructions;
      } else if (token.tag == '\n') {
        token.last = (tokens.length == 0) || (tokens[0].tag == '\n');
      }

      instructions.push(token);
    }

    if (stack.length > 0) {
      throw new Error('missing closing tag: ' + stack.pop().n);
    }

    return instructions;
  }

  function isOpener(token, tags) {
    for (var i = 0, l = tags.length; i < l; i++) {
      if (tags[i].o == token.n) {
        token.tag = '#';
        return true;
      }
    }
  }

  function isCloser(close, open, tags) {
    for (var i = 0, l = tags.length; i < l; i++) {
      if (tags[i].c == close && tags[i].o == open) {
        return true;
      }
    }
  }

  function stringifySubstitutions(obj) {
    var items = [];
    for (var key in obj) {
      items.push('"' + esc(key) + '": function(c,p,t,i) {' + obj[key] + '}');
    }
    return "{ " + items.join(",") + " }";
  }

  function stringifyPartials(codeObj) {
    var partials = [];
    for (var key in codeObj.partials) {
      partials.push('"' + esc(key) + '":{name:"' + esc(codeObj.partials[key].name) + '", ' + stringifyPartials(codeObj.partials[key]) + "}");
    }
    return "partials: {" + partials.join(",") + "}, subs: " + stringifySubstitutions(codeObj.subs);
  }

  Hogan.stringify = function(codeObj, text, options) {
    return "{code: function (c,p,i) { " + Hogan.wrapMain(codeObj.code) + " }," + stringifyPartials(codeObj) +  "}";
  }

  var serialNo = 0;
  Hogan.generate = function(tree, text, options) {
    serialNo = 0;
    var context = { code: '', subs: {}, partials: {} };
    Hogan.walk(tree, context);

    if (options.asString) {
      return this.stringify(context, text, options);
    }

    return this.makeTemplate(context, text, options);
  }

  Hogan.wrapMain = function(code) {
    return 'var t=this;t.b(i=i||"");' + code + 'return t.fl();';
  }

  Hogan.template = Hogan.Template;

  Hogan.makeTemplate = function(codeObj, text, options) {
    var template = this.makePartials(codeObj);
    template.code = new Function('c', 'p', 'i', this.wrapMain(codeObj.code));
    return new this.template(template, text, this, options);
  }

  Hogan.makePartials = function(codeObj) {
    var key, template = {subs: {}, partials: codeObj.partials, name: codeObj.name};
    for (key in template.partials) {
      template.partials[key] = this.makePartials(template.partials[key]);
    }
    for (key in codeObj.subs) {
      template.subs[key] = new Function('c', 'p', 't', 'i', codeObj.subs[key]);
    }
    return template;
  }

  function esc(s) {
    return s.replace(rSlash, '\\\\')
            .replace(rQuot, '\\\"')
            .replace(rNewline, '\\n')
            .replace(rCr, '\\r');
  }

  function chooseMethod(s) {
    return (~s.indexOf('.')) ? 'd' : 'f';
  }

  function createPartial(node, context) {
    var prefix = "<" + (context.prefix || "");
    var sym = prefix + node.n + serialNo++;
    context.partials[sym] = {name: node.n, partials: {}};
    context.code += 't.b(t.rp("' +  esc(sym) + '",c,p,"' + (node.indent || '') + '"));';
    return sym;
  }

  Hogan.codegen = {
    '#': function(node, context) {
      context.code += 'if(t.s(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,1),' +
                      'c,p,0,' + node.i + ',' + node.end + ',"' + node.otag + " " + node.ctag + '")){' +
                      't.rs(c,p,' + 'function(c,p,t){';
      Hogan.walk(node.nodes, context);
      context.code += '});c.pop();}';
    },

    '^': function(node, context) {
      context.code += 'if(!t.s(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,1),c,p,1,0,0,"")){';
      Hogan.walk(node.nodes, context);
      context.code += '};';
    },

    '>': createPartial,
    '<': function(node, context) {
      var ctx = {partials: {}, code: '', subs: {}, inPartial: true};
      Hogan.walk(node.nodes, ctx);
      var template = context.partials[createPartial(node, context)];
      template.subs = ctx.subs;
      template.partials = ctx.partials;
    },

    '$': function(node, context) {
      var ctx = {subs: {}, code: '', partials: context.partials, prefix: node.n};
      Hogan.walk(node.nodes, ctx);
      context.subs[node.n] = ctx.code;
      if (!context.inPartial) {
        context.code += 't.sub("' + esc(node.n) + '",c,p,i);';
      }
    },

    '\n': function(node, context) {
      context.code += write('"\\n"' + (node.last ? '' : ' + i'));
    },

    '_v': function(node, context) {
      context.code += 't.b(t.v(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,0)));';
    },

    '_t': function(node, context) {
      context.code += write('"' + esc(node.text) + '"');
    },

    '{': tripleStache,

    '&': tripleStache
  }

  function tripleStache(node, context) {
    context.code += 't.b(t.t(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,0)));';
  }

  function write(s) {
    return 't.b(' + s + ');';
  }

  Hogan.walk = function(nodelist, context) {
    var func;
    for (var i = 0, l = nodelist.length; i < l; i++) {
      func = Hogan.codegen[nodelist[i].tag];
      func && func(nodelist[i], context);
    }
    return context;
  }

  Hogan.parse = function(tokens, text, options) {
    options = options || {};
    return buildTree(tokens, '', [], options.sectionTags || []);
  }

  Hogan.cache = {};

  Hogan.cacheKey = function(text, options) {
    return [text, !!options.asString, !!options.disableLambda, options.delimiters, !!options.modelGet].join('||');
  }

  Hogan.compile = function(text, options) {
    options = options || {};
    var key = Hogan.cacheKey(text, options);
    var template = this.cache[key];

    if (template) {
      return template;
    }

    template = this.generate(this.parse(this.scan(text, options.delimiters), text, options), text, options);
    return this.cache[key] = template;
  }
})(typeof exports !== 'undefined' ? exports : Hogan);


if (typeof define === 'function' && define.amd) {
  define('hogan',Hogan);
}
;
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

/**@license
 * RequireJS Hogan Plugin | v0.2.1 (2013/02/08)
 * Author: Miller Medeiros | MIT License
 */
define('hgn',['hogan', 'text'], function (hogan, text) {

    var DEFAULT_EXTENSION = '.mustache';

    var _buildMap = {};
    var _buildTemplateText = 'define("{{pluginName}}!{{moduleName}}", ["hogan"], function(hogan){'+
                             '  var tmpl = new hogan.Template({{{fn}}}, "", hogan);'+
                             // need to use apply to bind the proper scope.
                             '  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;'+
                             '});\n';
    var _buildTemplate;


    function load(name, req, onLoad, config){
        var hgnConfig = config.hgn || {};
        var fileName = name;
        fileName += hgnConfig && hgnConfig.templateExtension != null? hgnConfig.templateExtension : DEFAULT_EXTENSION;

        // load text files with text plugin
        text.get(req.toUrl(fileName), function(data){
            var compilationOptions = hgnConfig.compilationOptions? mixIn({}, hgnConfig.compilationOptions) : {};

            if (config.isBuild) {
                // store compiled function if build
                // and should always be a string
                compilationOptions.asString = true;
                _buildMap[name] = hogan.compile(data, compilationOptions);
            }

            // maybe it's required by some other plugin during build
            // so return the compiled template even during build
            var template = hogan.compile(data, compilationOptions);
            var render = bind(template.render, template);
            // add text property for debugging if needed.
            // it's important to notice that this value won't be available
            // after build.
            render.text = template.text;
            render.template = template;
            // return just the render method so it's easier to use
            onLoad( render );
        });
    }

    function bind(fn, context) {
        return function(){
            return fn.apply(context, arguments);
        };
    }

    function mixIn(target, source) {
        var key;
        for (key in source){
            if ( Object.prototype.hasOwnProperty.call(source, key) ) {
                target[key] = source[key];
            }
        }
        return target;
    }

    function write(pluginName, moduleName, writeModule){
        if(moduleName in _buildMap){
            if (! _buildTemplate) {
                // using templates to generate compiled templates, so meta :P
                _buildTemplate = hogan.compile( _buildTemplateText );
            }
            var fn = _buildMap[moduleName];
            writeModule( _buildTemplate.render({
                pluginName : pluginName,
                moduleName : moduleName,
                fn : fn
            }) );
        }
    }

    return {
        load : load,
        write : write
    };

});
define("hgn!CreateProjectDialogTemplate.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"new-project-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">New Project</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <a href=\"#\" class=\"btn btn-large btn-block git-import-btn\" type=\"button\"><span class=\"btn-text\">Import from Git</span></a>");t.b("\n" + i);t.b("        <a href=\"#\" class=\"btn btn-large btn-block new-project-btn\" type=\"button\"><span class=\"btn-text blank-project-text\">Create Blank Project</span></a>");t.b("\n" + i);t.b("        <!-- <div class=\"field-container\">");t.b("\n" + i);t.b("            <label>");t.b("\n" + i);t.b("                ");t.b(t.v(t.d("Strings.PROJECT_SETTING_BASE_URL",c,p,0)));t.b(": <input type=\"text\" placeholder=\"");t.b(t.v(t.d("Strings.PROJECT_SETTING_BASE_URL_HINT",c,p,0)));t.b("\" value=\"");t.b(t.v(t.f("baseUrl",c,p,0)));t.b("\" class=\"url\" />");t.b("\n" + i);t.b("            </label>");t.b("\n" + i);t.b("            ");if(t.s(t.f("errorMessage",c,p,1),c,p,0,724,792,"{{ }}")){t.rs(c,p,function(c,p,t){t.b("<div class=\"alert\" style=\"margin-bottom: 0\">");t.b(t.t(t.f("errorMessage",c,p,0)));t.b("</div>");});c.pop();}t.b("\n" + i);t.b("         </div> -->");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);if(t.s(t.f("cancelable",c,p,1),c,p,0,860,1020,"{{ }}")){t.rs(c,p,function(c,p,t){t.b("        <div class=\"modal-footer\">");t.b("\n" + i);t.b("            <button class=\"dialog-button btn primary git-cancel\" data-button-id=\"cancel\">Cancel</button>");t.b("\n" + i);t.b("        </div>");t.b("\n" + i);});c.pop();}t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!GitImport.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-import-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">");t.b(t.v(t.f("title",c,p,0)));t.b("</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <!-- <div class=\"field-container\">");t.b("\n" + i);t.b("            <label>");t.b("\n" + i);t.b("                Remote Git Url: ");t.b("\n" + i);t.b("            </label>");t.b("\n" + i);t.b("         </div> -->");t.b("\n" + i);t.b("         <form class=\"form-horizontal\">");t.b("\n" + i);t.b("         <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\">Remote Git Url</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"text\" placeholder=\"https://hostname/repo.git\" value=\"\" class=\"url input-xlarge\" />");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("            <span class=\"help-block\" style=\"color:red;display:none;\"></span>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputUsername\">Username</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"text\" id=\"inputUsername\" placeholder=\"\" class=\"input-medium\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputPassword\">Password</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"password\" id=\"inputPassword\" placeholder=\"\" class=\"input-medium\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("         </form>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn git-create-cancel\" data-button-id=\"back\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!ProgressTemplate.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-progress modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">");t.b(t.v(t.f("title",c,p,0)));t.b("</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <div class=\"progress progress-striped active\">");t.b("\n" + i);t.b("            <div class=\"bar\" style=\"width: 0%;\"></div>");t.b("\n" + i);t.b("        </div>");t.b("\n" + i);t.b("        <span id=\"import-status\">");t.b("\n" + i);t.b("            ");t.b(t.v(t.f("initialMsg",c,p,0)));t.b("\n" + i);t.b("        </span>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary git-progress-cancel\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");t.b("\n");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!CommitForm.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-commit-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">Commit</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <form class=\"form-horizontal\">");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputEmail\">Email</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"text\" value=\"");t.b(t.v(t.f("email",c,p,0)));t.b("\" id=\"inputEmail\" placeholder=\"Email\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputName\">Name</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"text\" value=\"");t.b(t.v(t.f("name",c,p,0)));t.b("\" id=\"inputName\" placeholder=\"Name\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputCommitMsg\">Commit Message</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <textarea id=\"inputCommitMsg\" rows=\"3\"></textarea>");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("        </form>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn git-cancel\" data-button-id=\"back\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!AuthDialog.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-auth-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">Authentication required for \"");t.b(t.v(t.f("realm",c,p,0)));t.b("\"</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <!-- <div class=\"field-container\">");t.b("\n" + i);t.b("            <label>");t.b("\n" + i);t.b("                Remote Git Url: ");t.b("\n" + i);t.b("            </label>");t.b("\n" + i);t.b("         </div> -->");t.b("\n" + i);t.b("         <form class=\"form-horizontal\">");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputUsername\">Username</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"text\" id=\"inputUsername\" placeholder=\"\" class=\"input-medium\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputPassword\">Password</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <input type=\"password\" id=\"inputPassword\" placeholder=\"\" class=\"input-medium\">");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("         </form>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn git-cancel\" data-button-id=\"cancel\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!BranchDialog.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-branch-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">Create a New Local Branch</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <label>Create a local branch from the HEAD commit on branch \"<em>");t.b(t.v(t.f("currentBranch",c,p,0)));t.b("</em>\"</label>");t.b("\n" + i);t.b("        <input type=\"text\" id=\"inputBranch\" placeholder=\"New Branch Name\">");t.b("\n" + i);t.b("        <span class=\"help-block\" style=\"color:red;display:none;\"></span>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn cancel git-cancel\" data-button-id=\"cancel\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!CheckoutDialog.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-checkout-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">Checkout local branch</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <label></label>");t.b("\n" + i);t.b("        <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\">Current Branch</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <span class=\"uneditable-input\">");t.b(t.v(t.f("currentBranch",c,p,0)));t.b("</span>");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("          <div class=\"control-group\">");t.b("\n" + i);t.b("            <label class=\"control-label\" for=\"inputNewBranch\">New Branch</label>");t.b("\n" + i);t.b("            <div class=\"controls\">");t.b("\n" + i);t.b("              <select id=\"inputNewBranch\">");t.b("\n" + i);if(t.s(t.f("branches",c,p,1),c,p,0,662,720,"{{ }}")){t.rs(c,p,function(c,p,t){t.b("                  <option>");t.b(t.v(t.d(".",c,p,0)));t.b("</option>");t.b("\n" + i);});c.pop();}t.b("              </select>");t.b("\n" + i);t.b("            </div>");t.b("\n" + i);t.b("          </div>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn git-cancel\" data-button-id=\"cancel\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define("hgn!BlankProject.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("<div class=\"git-blank-dialog modal\">");t.b("\n" + i);t.b("    <div class=\"modal-header\">");t.b("\n" + i);t.b("        <h1 class=\"dialog-title\">Create a New Project</h1>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-body\">");t.b("\n" + i);t.b("        <label>Specify a project name</label>");t.b("\n" + i);t.b("        <input type=\"text\" id=\"inputProjectName\" placeholder=\"New Project Name\">");t.b("\n" + i);t.b("        <span class=\"help-block\" style=\"color:red;display:none;\"></span>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <div class=\"modal-footer\">");t.b("\n" + i);t.b("        <button class=\"dialog-button btn primary\" data-button-id=\"ok\">OK</button>");t.b("\n" + i);t.b("        <button class=\"dialog-button btn git-create-cancel\" data-button-id=\"cancel\">Cancel</button>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

define('main',['require','exports','module','hgn!CreateProjectDialogTemplate.html','hgn!GitImport.html','hgn!ProgressTemplate.html','hgn!CommitForm.html','hgn!AuthDialog.html','hgn!BranchDialog.html','hgn!CheckoutDialog.html','hgn!BlankProject.html','thirdparty/gitlite.js/api-built'],function (require, exports, module) {
    

    var ProjectManager      = brackets.getModule("project/ProjectManager"),
        Dialogs             = brackets.getModule("widgets/Dialogs"),
        NewProjectTemplate  = require("hgn!CreateProjectDialogTemplate.html"),
        GitImportTemplate   = require("hgn!GitImport.html"),
        ProgressTemplate    = require("hgn!ProgressTemplate.html"),
        CommitFormTemplate  = require("hgn!CommitForm.html"),
        AuthTemplate        = require("hgn!AuthDialog.html"),
        BranchTemplate      = require("hgn!BranchDialog.html"),
        CheckoutTemplate    = require("hgn!CheckoutDialog.html"),
        BlankProjectTemplate = require("hgn!BlankProject.html"),
        AppInit             = brackets.getModule("utils/AppInit"),
        ExtensionUtils      = brackets.getModule("utils/ExtensionUtils"),
        PlatformFileSystem  = brackets.getModule("file/PlatformFileSystem").PlatformFileSystem,
        GitApi              = require("thirdparty/gitlite.js/api-built"),
        Menus               = brackets.getModule("command/Menus"),
        CommandManager      = brackets.getModule("command/CommandManager"),
        Commands            = brackets.getModule("command/Commands"),
        FileSyncManager     = brackets.getModule("project/FileSyncManager");
        Strings             = brackets.getModule("strings");

    var gitSettings = {};
    chrome.storage.local.get('git', function(obj){
        if (obj)
            gitSettings = obj['git'] || gitSettings;
    });

    var storeGitSettings = function(){
        chrome.storage.local.set({git : gitSettings});
    }

    $(document.body).on('click', '.git-cancel', function(e){
        Dialogs.cancelModalDialogIfOpen('modal');
    });

    function dirNameForGitUrl(path) {
        var index   = path.lastIndexOf("/"),
            dirName    = path.substring(index + 1, path.length);
        
        if (dirName.lastIndexOf(".git") == (dirName.length - 4)){
            dirName = dirName.substring(0, dirName.length - 4);
        }
        return dirName;
    }

    function authError(authHdr, dir, retry){
        var realmIdx = authHdr.toLowerCase().indexOf('realm="');
        var realm;
        if (realmIdx != -1){
            var endIdx = authHdr.indexOf('"', realmIdx + 7);
            if(endIdx != -1){
                realm = authHdr.substring(realmIdx + 7, endIdx);
            }
        }
        realm = realm || "Unknown";
        Dialogs.showModalDialogUsingTemplate(AuthTemplate({realm:realm}), false);
        $('.git-auth-dialog .primary').click(function(){
            var username = $('#inputUsername').val();
            var password = $('#inputPassword').val();
            dir = dir.fullPath || ProjectManager.getProjectRoot().fullPath;
            var settings = gitSettings[dir] || {};
            settings.username = username;
            settings.password = password;

            gitSettings[dir] = settings;
            storeGitSettings();
            Dialogs.cancelModalDialogIfOpen('git-auth-dialog');
            retry();
        });
    }

    function genericErrorHandler(title){
        return function(e){
            Dialogs.cancelModalDialogIfOpen('modal');
            Dialogs.showModalDialog('git-error', title, e.msg);
        }
    }

    function fileErrorHandler(e){
        Dialogs.cancelModalDialogIfOpen("modal");
        Dialogs.showModalDialog('git-file-error', 'Unexpected File Error', 'File error code is ' + e.code);
    }

    function getProjectsRootDir(callback){
        PlatformFileSystem.requestNativeFileSystem('projects', function(fs){
            callback(fs.root);
        }, function(e){
            PlatformFileSystem.requestNativeFileSystem(null, function(fs){
                fs.root.getDirectory("projects", {create:true}, callback, fileErrorHandler);
            }, fileErrorHandler);
        });
    }

    function newBlankProject(fromWelcomeMat){
        Dialogs.cancelModalDialogIfOpen("new-project-dialog");
        Dialogs.showModalDialogUsingTemplate(BlankProjectTemplate(), false);

        var inlineError = function(msg){
            $('.git-blank-dialog .help-block').text(msg).show();
        }
        $('.git-blank-dialog .primary').click(function(){
            var projectName = $('#inputProjectName').val();
            if (!projectName || projectName.trim().length == 0){
                inlineError('Project name can\'t be blank');
                return;
            }
            else{
                getProjectsRootDir(function(projectsDir){
                    projectsDir.getDirectory(projectName, {create: true, exclusive: true}, function(newDir){
                        ProjectManager.openProject(newDir.fullPath);
                        Dialogs.cancelModalDialogIfOpen('git-blank-dialog');
                    },
                    function(e){
                        inlineError('File error code: ' + e.code + ' try again');
                    });
                });
            }

        });

        $('.git-blank-dialog .git-create-cancel').click(function(e){
            Dialogs.cancelModalDialogIfOpen("modal");
            if (fromWelcomeMat){
                promptForNewProject(fromWelcomeMat);
            }
        });
    }

    function createProgressMonitor(){
        var bar = $('.git-progress .bar')[0];
        var $msg = $('#import-status')

        var progress = function(data){
            bar.style.width = data.pct + '%';
            $msg.text(data.msg);
        }
        return progress;
    }

    function showBitbucketError(){
        return Dialogs.showModalDialog('git-bb-error', 'Bitbucket', 'Tailor cannot pull from or push to remote repositories on Bitbucket. The root cause is <a href="https://bitbucket.org/site/master/issue/6666/detect-git-requests-by-content-type-header" target="_blank">this issue</a>. Comment on that issue or <a href="https://support.atlassian.com" target="_blank">contact Atlassian support</a> if you want to see this fixed.');
    }

    function doGitImport(options){
        
        var gitRepoUrl = options.url,//$('.git-import-dialog .url').val(),
            username = options.username,
            password = options.password,
            fromWelcomeMat = options.fromWelcomeMat;
            dirName = dirNameForGitUrl(gitRepoUrl);


        Dialogs.cancelModalDialogIfOpen("git-import-dialog");
        
        if (username && password){
            gitSettings['/projects/' + dirName] = {username: username, password: password};
            storeGitSettings();
        }

        var cloneError = function(e){
            Dialogs.cancelModalDialogIfOpen('git-progress');
            var result;
            if (e.type == GitApi.HTTP_AUTH_ERROR){
                authError(e.auth, newDir, doClone);
            }
            else if (e.type == GitApi.AJAX_ERROR && e.url.indexOf('bitbucket.org') != -1){
                result = showBitbucketError();
            }
            else{
                result = Dialogs.showModalDialog('git-clone-error', 'Clone error', e.msg);
            }
            if (result && fromWelcomeMat){
                result.done(function(){
                    promptForNewProject(true);
                });
            }
        }

        var newDir;
        var doClone = function(){
            var settings = gitSettings['/projects/' + dirName] || {};
            var username = settings.username,
                password = settings.password;

            var progress = showProgress("Cloning Git Repo...", "Connecting to server...");
            GitApi.clone({dir: newDir, url: gitRepoUrl, depth: 1, progress: progress, username:username, password:password}, function(){
                ProjectManager.openProject(newDir.fullPath);
                Dialogs.cancelModalDialogIfOpen('git-progress');
            }, cloneError);
        }

        getProjectsRootDir(function(projectsDir){
            projectsDir.getDirectory(dirName, {create:true}, function(dir){
                newDir = dir;
                doClone();
            }, fileErrorHandler);
        });
    }

    function promptForGitImport(fromWelcomeMat){
        // Dialogs.cancelModalDialogIfOpen("new-project-dialog");
        // Dialogs.showModalDialogUsingTemplate(GitImportTemplate({title: "Import from Git"}), false);

        // $('.git-import-dialog button[data-button-id="ok"]').click(doGitImport);
        showAndValidateGitRemoteSetup("Git Import", fromWelcomeMat, doGitImport);
    }


    function showAndValidateGitRemoteSetup(title, fromWelcomeMat, callback){
        Dialogs.cancelModalDialogIfOpen("modal");
        Dialogs.showModalDialogUsingTemplate(GitImportTemplate({title: title}), false);

        $('.git-import-dialog button[data-button-id="ok"]').click(function(){
            var gitRepoUrl = $('.git-import-dialog .url').val(),
                username = $('#inputUsername').val(),
                password = $('#inputPassword').val();

            if (!gitRepoUrl || !gitRepoUrl.trim().length){
                $('.git-import-dialog .help-block').text('URL can\'t be blank').show();
            }
            else{
                callback({url: gitRepoUrl, username: username, password: password, fromWelcomeMat: fromWelcomeMat});
            }
        });

        $('.git-import-dialog .git-create-cancel').click(function(e){
            Dialogs.cancelModalDialogIfOpen("modal");
            if (fromWelcomeMat){
                promptForNewProject(true);
            }
        });
    }

    function promptForNewProject(fromWelcomeMat){
        var cancelable = !fromWelcomeMat;
        Dialogs.showModalDialogUsingTemplate(NewProjectTemplate({cancelable: cancelable}), false);

        $('.git-import-btn').one('click', function(){
            promptForGitImport(fromWelcomeMat);
        });
        $('.new-project-btn').one('click', function(){
            newBlankProject(fromWelcomeMat);
        });
    }

    var welcomeMat = {
        launch: function(){
            promptForNewProject(true);
        }
    }

    function doPull(){
        var dir = ProjectManager.getProjectRoot();

        var pullError = function(e){
            Dialogs.cancelModalDialogIfOpen('git-progress');
            if (e.type == GitApi.HTTP_AUTH_ERROR){
                authError(e.auth, dir, pushInternal);
            }
            else{
                Dialogs.showModalDialog('git-pull-error', 'Pull error', e.msg);
            }
        }
        var pullInternal = function(){
            var settings = gitSettings[dir.fullPath] || {};
            var progress = showProgress("Pulling from Remote Repo", "Looking for uncommitted changes...")
            GitApi.pull({dir: dir, username: settings.username, password: settings.password, progress: progress}, function(){
                Dialogs.cancelModalDialogIfOpen('git-progress');
                Dialogs.showModalDialog('git-pull-success', 'Pull successful', 'The pull was successful');
                ProjectManager.refreshFileTree();
                refreshOpenEditors();
            },pullError)
        }
        CommandManager.execute(Commands.FILE_CLOSE_ALL, { promptOnly: true }).done(function () {
            pullInternal();
        });
    }

    function showProgress(title, initialMsg){
        //if (toClose){
        Dialogs.cancelModalDialogIfOpen('modal');
        //}
        Dialogs.showModalDialogUsingTemplate(ProgressTemplate({title: title, initialMsg: initialMsg}), false);
        return createProgressMonitor();
    }

    function doPush(){
        

        var pushError = function(e){
            Dialogs.cancelModalDialogIfOpen('modal');
            if (e.type == GitApi.HTTP_AUTH_ERROR){
                authError(e.auth, dir, pushInternal);
            }
            else if (e.type == GitApi.AJAX_ERROR && e.url.indexOf('bitbucket.org') != -1){
                showBitbucketError();
            }
            else{
                Dialogs.showModalDialog('git-push-error', 'Push error', e.msg);
            } 
        }

        var dir = ProjectManager.getProjectRoot();

        var pushInternal = function(url){
            var progress = showProgress("Pushing to Remote Repo", "Looking for new changes...");
            var settings = gitSettings[dir.fullPath] || {};
            var options = {dir:dir, username: settings.username, password: settings.password, progress:progress};
            if (url){
                options.url = url;
            }
            GitApi.push(options, function(){
                Dialogs.cancelModalDialogIfOpen('git-progress');
                Dialogs.showModalDialog('git-push-success', 'Push successful', 'The push was successful');
            }, function(e){
                if (e.type == GitApi.PUSH_NO_REMOTE){
                    showAndValidateGitRemoteSetup("Push to a Remote Git Repo", false, function(options){
                        var gitRepoUrl = options.url,//$('.git-import-dialog .url').val(),
                            username = options.username,
                            password = options.password;

                        if (username && password){
                            gitSettings[dir.fullPath] = {username: username, password: password};
                            storeGitSettings();
                        }
                        pushInternal(gitRepoUrl);
                    });
                }
                else{
                    pushError(e);
                }
            });
        }
        CommandManager.execute(Commands.FILE_CLOSE_ALL, { promptOnly: true })
            .done(function () {
                GitApi.checkForUncommittedChanges({dir: dir}, function(){pushInternal()}, function(){
                    Dialogs.showModalDialog("git-new-changes", 
                                            "Uncommitted Changes", 
                                            "You have uncommitted changes in your working copy. Do you want to commit them before pushing?",
                                            [{ className: Dialogs.DIALOG_BTN_CLASS_PRIMARY, id: Dialogs.DIALOG_BTN_OK, text: "Yes" },
                                             { className: Dialogs.DIALOG_BTN_CLASS_NORMAL, id: Dialogs.DIALOG_BTN_CANCEL, text: "No"}]);
                    //doCommitInternal(pushInternal);

                    $('.git-new-changes .dialog-button').click(function(e){
                        if ($(this).hasClass('primary')){
                            doCommitInternal(pushInternal)
                        }else{
                            pushInternal();
                        }
                    });
                });
            });

        //Dialogs.showModalDialogUsingTemplate(CommitFormTemplate(), false);
        //$('.git-commit-dialog .primary').click(function(){
            
            //GitApi.commit({dir:dir, name: $('#inputName').val(), email: $('#inputEmail').val(), commitMsg: $('#inputCommitMsg').val()}, function(){
                //pushInternal();
            //}, commitError);
        //});
    }

    function doCommit(){
        CommandManager.execute(Commands.FILE_CLOSE_ALL, { promptOnly: true }).done(function () {
            doCommitInternal(function(){
                Dialogs.showModalDialog('git-commit-success', 'Commit successful', 'The commit was successful');
            });
        });
    }

    function doBranch(){
        var dir = ProjectManager.getProjectRoot();
        var branchError = genericErrorHandler('Branch Error');

        var inlineBranchError = function(msg){
            $('.git-branch-dialog .help-block').text(msg).show();
        }

        GitApi.getCurrentBranch({dir: dir}, function(branchName){
            Dialogs.showModalDialogUsingTemplate(BranchTemplate({currentBranch: branchName}), false);
            $('.git-branch-dialog .primary').click(function(){
                var newBranchName = $('#inputBranch').val();
                if (!newBranchName || newBranchName.trim().length == 0){
                    inlineBranchError('Branch name is blank');
                }
                else{
                    GitApi.branch({dir: dir, branch: newBranchName}, function(){
                        Dialogs.cancelModalDialogIfOpen('git-branch-dialog');
                        Dialogs.showModalDialog('git-branch-success', 'Branch successful', 'Branch \'' + newBranchName + '\' successfully created');
                    },
                    function(e){
                        inlineBranchError(e.msg);
                    });
                }
            });
        }, branchError);
    }
    function refreshOpenEditors(){
        FileSyncManager.syncOpenDocuments('sdasdasd');
    }

    function doCheckout(){
        var dir = ProjectManager.getProjectRoot();

        var checkoutError = genericErrorHandler('Checkout Error');

        var scrubBranchList = function(branches, currentBranch){
            var idx = branches.indexOf(currentBranch);
            if (idx != -1){
                branches.splice(idx, 1);
            }
        }

        CommandManager.execute(Commands.FILE_CLOSE_ALL, { promptOnly: true }).done(function () {
            GitApi.getLocalBranches({dir: dir}, function(branches){
                GitApi.getCurrentBranch({dir: dir}, function(currentBranch){
                    scrubBranchList(branches, currentBranch);
                    if (branches.length){
                        Dialogs.showModalDialogUsingTemplate(CheckoutTemplate({branches: branches, currentBranch: currentBranch}), false);
                        $('.git-checkout-dialog select').val(branches[0]);
                        $('.git-checkout-dialog .primary').click(function(){
                            var newBranch = $('.git-checkout-dialog select').val();
                            GitApi.checkout({dir: dir, branch: newBranch}, function(){
                                ProjectManager.refreshFileTree();
                                Dialogs.cancelModalDialogIfOpen('git-checkout-dialog');
                                Dialogs.showModalDialog('git-checkout-success', 'Checkout successful', 'Checked out \'' + newBranch + '\' successfully.');
                                refreshOpenEditors();
                            }, checkoutError);
                        })
                    }
                    else{
                        checkoutError({msg: 'No local branches to checkout'});
                    }

                }, checkoutError);
            }, checkoutError);
        });
    };

    function doCommitInternal(callback){
        var dir = ProjectManager.getProjectRoot();
        var commitError = function(e){
            Dialogs.cancelModalDialogIfOpen('modal');
            Dialogs.showModalDialog('git-commit-error', 'Commit Error', e.msg);       
        }

        var settings = gitSettings[dir.fullPath] || {},
            name = settings.name || "",
            email = settings.email || "",
            focusOnMsg = name.trim().length > 0 || email.trim().length > 0;


        Dialogs.showModalDialogUsingTemplate(CommitFormTemplate({name: name, email: email}), false);
        var focusSelector = focusOnMsg ? '#inputCommitMsg': '#inputEmail';
        $(focusSelector).focus();

        $('.git-commit-dialog .primary').click(function(){
            settings.name = $('#inputName').val();
            settings.email = $('#inputEmail').val();

            var options = {dir:dir, name: settings.name, email: settings.email, commitMsg: $('#inputCommitMsg').val()}
            var progress = showProgress("Commit", "Finding the latest changes...");
            progress({pct: 95, msg: "Finding the latest changes..."});

            GitApi.commit(options, function(){
                gitSettings[dir.fullPath] = settings;
                storeGitSettings();
                Dialogs.cancelModalDialogIfOpen('git-progress');
                callback();
            }, commitError);
        });
    }

    ProjectManager.registerWelcomeMat(welcomeMat);

    CommandManager.register('Pull', 'pull', doPull);
    CommandManager.register('Push', 'push', doPush);
    CommandManager.register('Commit', 'commit', doCommit);
    CommandManager.register('Branch', 'branch', doBranch);
    CommandManager.register('Checkout', 'checkout', doCheckout);
    CommandManager.register('Tailor project home page', "gotailorhome", function(){window.open("https://github.com/ryanackley/tailor", "_blank")});

    var menu = Menus.addMenu('Git', 'git', Menus.BEFORE, Menus.AppMenuBar.HELP_MENU);
    menu.addMenuItem('pull');
    menu.addMenuItem('push');
    menu.addMenuItem('commit');
    menu.addMenuItem('branch');
    menu.addMenuItem('checkout');

    AppInit.htmlReady(function () {
        ExtensionUtils.loadStyleSheet(module, "dialog.css");
        //window.setTimeout(function(){
        var fileMenu = Menus.getMenu(Menus.AppMenuBar.FILE_MENU);
        fileMenu.removeMenuItem(Commands.FILE_OPEN);
        fileMenu.removeMenuItem(Commands.FILE_LIVE_FILE_PREVIEW);
        fileMenu.removeMenuItem(Commands.FILE_LIVE_HIGHLIGHT);
        fileMenu.removeMenuItem(Commands.FILE_PROJECT_SETTINGS);
        
        fileMenu.removeMenuItem(Commands.FILE_EXTENSION_MANAGER);
        $('#' + fileMenu.id + ' .divider').parent().remove();

        fileMenu.addMenuDivider(Menus.AFTER, Commands.FILE_CLOSE_ALL);

        var helpMenu = Menus.getMenu(Menus.AppMenuBar.HELP_MENU);
        helpMenu.removeMenuItem(Commands.HELP_CHECK_FOR_UPDATE);
        helpMenu.removeMenuItem(Commands.HELP_HOW_TO_USE_BRACKETS);
        helpMenu.removeMenuItem(Commands.HELP_FORUM);
        helpMenu.removeMenuItem(Commands.HELP_RELEASE_NOTES);
        helpMenu.removeMenuItem(Commands.HELP_REPORT_AN_ISSUE);
        helpMenu.removeMenuItem(Commands.HELP_SHOW_EXT_FOLDER);
        helpMenu.removeMenuItem(Commands.HELP_TWITTER);
        helpMenu.removeMenuItem(Commands.HELP_ABOUT);
        $('#' + helpMenu.id + ' .divider').parent().remove();
        helpMenu.addMenuItem("gotailorhome");


    //}, 15000);

        var folderOpen = CommandManager.get(Commands.FILE_OPEN_FOLDER);
        folderOpen.setName('New Project...')
        folderOpen._commandFn = promptForNewProject;
        // so it's picked up by the recent projects list.
        Strings.CMD_OPEN_FOLDER = 'New Project...';
    });
});