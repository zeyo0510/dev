// --- remember to remove everything from google_previews folder before next updatefonts call.

// by enabling APPEND_MODE below, we append fonts to current ones, rather than download from beginning;
// also remember to rename googlefonts.json to fonts.json, to enable appending, and unpack previews.zip
var APPEND_MODE = false;

var async = require('async');
var http = require('http');
var https = require('https');
var opentype = require('./../../../node_modules/gravit/lib/core/font/opentype/opentype.js');
var StringDecoder = require('string_decoder').StringDecoder;
var finish = [];
var previews = [];
var fs = require('fs');

var SORT_DATE = "date";
var SORT_STYLE = "style";
var SORT_TRENDING = "trending";
var SORT_ALPHABETIC = "alpha";
var SORT_POPULARITY = "popularity";

var SORT_METHOD = SORT_ALPHABETIC;
var STYLE = {
    Normal:"N",
    Italic:"I"
}

function handle_font(family, category, weight, style, url, cb) {
    // Convert a Node.js Buffer to an ArrayBuffer
    function toArrayBuffer(buffer) {
        var arrayBuffer = new ArrayBuffer(buffer.length);
        var data = new Uint8Array(arrayBuffer);
        for (var i = 0; i < buffer.length; i += 1) {
            data[i] = buffer[i];
        }

        return arrayBuffer;
    }
    
    function generate_preview(font, previewTextTemplate) {
        var previewText = '';
        for (var i = 0; i < previewTextTemplate.length; ++i) {
            var c = previewTextTemplate[i];
            var glyphIndex = font.charToGlyphIndex(c);
            var glyph = font.glyphs.get(glyphIndex);
            if (c === ' ' || glyph !== null) {
                previewText += c;
            }
        }

        var path = null;
        try {
            path = font.getPath(previewText, 0, 72, 72);
            if (!path || !path.commands || !path.commands.length) {
                console.log("NO PATH RETURNED");
                throw new Error('No path returned.');
            }
        } catch (e) {
            console.log("ERROR:" + e);
            console.log(e.stack);
            return null;
        }

        var minX = null;
        var maxX = null;
        var minY = null;
        var maxY = null;

        var svgPath = '<path fill="currentColor" d="';

        function _writePos(x, y) {
            if (x < minX || minX === null) minX = x;
            if (x > maxX || maxX === null) maxX = x;
            if (y < minY || minY === null) minY = y;
            if (y > maxY || maxY === null) maxY = y;

            return x.toFixed(2) + ' ' + y.toFixed(2);
        }

        for (var i = 0; i < path.commands.length; i += 1) {
            var cmd = path.commands[i];
            if (cmd.type === 'M') {
                svgPath += 'M' + _writePos(cmd.x, cmd.y);
            } else if (cmd.type === 'L') {
                svgPath += 'L' + _writePos(cmd.x, cmd.y);
            } else if (cmd.type === 'C') {
                svgPath += 'C' + _writePos(cmd.x1, cmd.y1) + ' ' + _writePos(cmd.x2, cmd.y2) + ' ' + _writePos(cmd.x, cmd.y);
            } else if (cmd.type === 'Q') {
                svgPath += 'Q' + _writePos(cmd.x1, cmd.y1) + ' ' + _writePos(cmd.x, cmd.y);
            } else if (cmd.type === 'Z') {
                svgPath += 'Z';
            }
        }

        svgPath += '"/>';

        return '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 ' + (minX + maxX).toFixed(2) + ' ' + (minY + maxY).toFixed(2) + '" height="100%">' + svgPath + '</svg>';
    }

    function parse_and_add_font(ttfBuffer) {
        var parsedFont = null;
        try {
            parsedFont = opentype.parse(toArrayBuffer(ttfBuffer));
        } catch (e) {
            return cb('Unable to parse font buffer. - ' + e.message);
        }

        if (!parsedFont || !parsedFont.supported) {
            return cb('Invalid or not supported font buffer/code.');
        }

        var preview = generate_preview(parsedFont, family);
        if (!preview) {
            return cb('Unable to get a preview path for the font');
        }
        
        // var previewExtra = generate_preview(parsedFont, 'AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz 0123456789');
        var fontWidth = 0;
        if (parsedFont.tables && parsedFont.tables.hasOwnProperty('os2')) {
            fontWidth = parsedFont.tables['os2'].xAvgCharWidth || 0;
        }

        var ff = family.toLowerCase();
        var entry = null;
        for (var i = 0; i < finish.length; i++) {
            if (finish[i].family.toLowerCase() === ff) {
                entry = finish[i];
                
                var fontToUpdate = null;
                for (var j = 0; j < entry.fonts.length; j++) {
                    var font = entry.fonts[j];
                    if (font.weight === weight && font.style === style && url === url) {
                        fontToUpdate = font;
                        return cb();
                    }
                };

                if (!fontToUpdate) {
                    entry.fonts.push({});
                    fontToUpdate = entry.fonts[entry.fonts.length - 1];
                    fontToUpdate.style = style;
                    fontToUpdate.weight = weight;
                    fontToUpdate.url = url;
                }
                break;
            }
        }
        if (!entry && family !== "Open Sans") {
            entry = {
                family: family,
                fonts: []
            };
            
            entry.fonts.push({});
            fontToUpdate = entry.fonts[entry.fonts.length - 1];
            fontToUpdate.style = style;
            fontToUpdate.weight = weight;
            fontToUpdate.url = url;
            previews.push(preview);
            finish.push(entry);
        }
        return cb();
    }

    var request = url.indexOf('https') === 0 ? https : http;
    request.get(url, function (res) {
        var data = [];
        res.on('data', function (chunk) {
            data.push(chunk);
        });
        res.on('end', function () {
            var buffer = new Buffer(data.reduce(function (prev, current) {
                return prev.concat(Array.prototype.slice.call(current));
            }, []));

            parse_and_add_font(buffer);
        });
    }).on('error', function (err) {
        return cb('Error loading ttf font from ' + url + ' - ' + err.message);
    });
}

function handle_google_webfont_item(item, cb) {
    if (!item.family) {
        return cb('No family found.');
    }
    if (!item.variants || !item.variants.length) {
        return cb('No variants found.');
    }
    if (!item.subsets || (item.subsets.indexOf('latin') < 0 && item.subsets.indexOf('latin-ext') < 0)) {
        return cb('No latin nor latin-ext subset found.');
    }

    // Handle each variant as single font now
    async.series(item.variants.map(function (variant) {
        return function (cb) {
            var weight = null;
            var style = STYLE.Normal;
            var url = item.files[variant];
            if (!url || url.lastIndexOf('.ttf') < 0) {
                return cb('No ttf file found for variant ' + variant);
            }

            // make sure to use google https
            if (url.indexOf('http://') === 0) {
                url = 'https://' + url.substr('http://'.length);
            }

            switch (variant) {
                case '100italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '100':
                    weight = 100;
                    break;
                case '200italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '200':
                    weight = 200;
                    break;
                case '300italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '300':
                    weight = 300;
                    break;
                case '400italic':
                case 'italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '400':
                case 'regular':
                    weight = 400;
                    break;
                case '500italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '500':
                    weight = 500;
                    break;
                case '600italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '600':
                    weight = 600;
                    break;
                case '700italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '700':
                    weight = 700;
                    break;
                case '800italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '800':
                    weight = 800;
                    break;
                case '900italic':
                    style = STYLE.Italic;
                // fall-through intended
                case '900':
                    weight = 900;
                    break;
                default:
                    return cb('Invalid/unknown variant: ' + variant);
            }
            async.setImmediate(function () {
                handle_font(item.family, null, weight, style, url, cb);
            });
        }
    }), cb);
}


    var URL = 'https://www.googleapis.com/webfonts/v1/webfonts?key=AIzaSyBNPUf__S_WULhgSaYh1r9UuKBck4wicIg&sort=' + SORT_METHOD;

    console.log('info', 'Synching Google WebFonts...');

    https.get(URL, function (res) {
        var data = '';
        var decoder = new StringDecoder('utf8');

        res.on('data', function (chunk) {
            var textChunk = decoder.write(chunk);
            data += textChunk;
        });
        res.on('end', function () {
            var parsedData = null;
            try {
                parsedData = JSON.parse(data);
                if (!parsedData || !parsedData.items || !parsedData.items.length) {
                    throw new Error('Invalid data or no items found.');
                }
            } catch (e) {
                console.log('error', 'Invalid return value from Google WebFonts API', e, data);
                return done();
            }

            console.log('Retrieved '+ parsedData.items.length+' fonts from Google WebFonts...');
            // parsedData.items = parsedData.items.splice(0,12); <= to test lower number of fonts
            var fontsHandled = 0;
            var additional = [];
            var fileName = "fonts.json";

            // $$$$$$$$$$
            // appending
            var currentFonts;
            if (APPEND_MODE) {
                try {
                    currentFonts = JSON.parse(fs.readFileSync(fileName, 'utf8'));
                } catch (e) {}
                if (currentFonts) {
                    additional = parsedData.items.filter(function (item) {
                        return !currentFonts.some(function (check) {
                            // update fonts that do not exist or their number of variants changed
                            return ((check.family === item.family) && (check.fonts.length === item.variants.length));
                        });
                    });
                }
            }
            // $$$$$$$$$$

            if (additional.length) {
                parsedData.items = additional;
            }

            async.parallelLimit(parsedData.items.map(function (item) {
                return function (cb) {
                    handle_google_webfont_item(item, function (err) {
                        if (err) {
                            console.log('Error handling Google WebFonts family ' + item.family + ' error:' + err + ' item num:' + parsedData.items.indexOf(item));
                        } else {
                            fontsHandled++;
                            console.log("Handled " + fontsHandled + "(" + item.family + ") of " + parsedData.items.length);
                        }
                        
                        cb();
                    });
                };
            }), 8, function () {
                console.log('Synched '+fontsHandled+' fonts from Google WebFonts.');
                console.log('$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$');
                if (additional.length) {
                    console.log("Appending fonts");
                    // var currentFonts = JSON.parse(fs.readFileSync(fileName, 'utf8'));
                    var currentPreviews = JSON.parse(fs.readFileSync("previews.json", 'utf8'));
                    console.log("Current fonts number:" + finish.length + " previews:" + previews.length);
                    // concat with replace
                    var flen = finish.length;
                    for (var i = flen-1; i >= 0; i--) {
                        var idx = currentFonts.findIndex(function (item) {
                            return (item.family === finish[i].family);
                        });
                        if (idx >= 0) {
                            currentFonts[idx] = finish[i];
                            currentPreviews[idx] = previews[i];
                            previews.splice(i, 1);
                            finish.splice(i, 1);
                        }
                    }
                    finish = finish.concat(currentFonts);
                    previews = previews.concat(currentPreviews);
                    console.log("After update fonts number:" + finish.length + " previews:" + previews.length);
                    // super sophisticated sorting algorithm
                    for (var i = 0; i < finish.length; i++) {
                        for (var j = i; j < finish.length; j++) {
                            if (finish[i].family.localeCompare(finish[j].family) > 0) {
                                var tmp = finish[i];
                                finish[i] = finish[j];
                                finish[j] = tmp;
                                var tmp2 = previews[i];
                                previews[i] = previews[j];
                                previews[j] = tmp2;
                            }
                        }
                    }
                    
                }
                fs.writeFile(fileName, JSON.stringify(finish), function(err) {
                    if(err) {
                        return console.log(err);
                    }

                    console.log("Writing previews file. Remember to delete it.");
                    try {
                        fs.writeFileSync("previews.json", JSON.stringify(previews));
                    } catch (e) {
                        console.log('error', "Writing failed", e)
                    }

                    var CHUNKS = 10;
                    var chunkNum = previews.length/CHUNKS;
                    var i = 0;
                    var j = 0;
                    while (i < previews.length) {
                        fs.writeFile("google_previews/previews" + j + ".json", JSON.stringify(previews.slice(i, i+CHUNKS)), function(err) {
                            if(err) {
                                return console.log(err);
                            } else {

                            }
                        });
                        i += CHUNKS;
                        j++;
                    }
                });
            });
        });
    }).on('error', function (err) {
        console.log('error', 'Error loading Google WebFonts List via API', err);
        return done();
    });