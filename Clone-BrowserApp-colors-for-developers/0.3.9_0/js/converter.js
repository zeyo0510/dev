$(document).ready(function(){

    initQuickCopy();

    update255FromHexa();
    update1From255();
    updateCMYKFromRGB();
    updateHSLFromRGB();
    updatePreview();

    $(".format").click(function(){
        changeFormatTo(
            $(this).attr('class')
                .replace('format', '')
                .replace('active', '')
                .replace(' ', '')
        );
    });

    $("input").change(checkAllValue);
    $("input").keyup(checkAllValue);

    $(".auto-detect input").change(updateAllFromAutodetect);
    $(".auto-detect input").keyup(updateAllFromAutodetect);

    $(".rgba255 input").change(updateAllFrom255);
    $(".rgba255 input").keyup(updateAllFrom255);

    $(".rgba1 input").change(updateAllFrom1);
    $(".rgba1 input").keyup(updateAllFrom1);

    $(".hexadecimal input").change(updateAllFromHexa);
    $(".hexadecimal input").keyup(updateAllFromHexa);

    $(".cmyk100 input").change(updateAllFromCMYK);
    $(".cmyk100 input").keyup(updateAllFromCMYK);

    $(".hsla100 input").change(updateAllFromHSLA);
    $(".hsla100 input").keyup(updateAllFromHSLA);

    $(".regex").click(function(){
        if ($(this).attr('id') == 'custom_regex') {
            window.currentRegex = $('#custom-data-regex').val();
            showCustomToolBar();
        } else {
            window.currentRegex = $(this).attr("data-regex");
            hideCustomToolBar();
        }
        $(".regex").removeClass('active');
        $(this).addClass('active');
        updateQuickCopy();
    });
    $('#custom-data-regex').keyup(function(){
        console.log('change');
        window.currentRegex = $(this).val();
        updateQuickCopy();
    }).keydown(saveCaret)
        .keypress(saveCaret)
        .mousemove(saveCaret);
    $('.custom_tool').click(addRegexKeyCode);
    hideCustomToolBar();
});

function changeFormatTo (format) {
    if (format != 'rgba255' &&
        format != 'rgba1' &&
        format != 'hexadecimal' &&
        format != 'cmyk100' &&
        format != 'hsla100' ) {
        format = 'rgba255';
    }

    $('.format, .color_input').removeClass('active');
    $('.format.'+ format +', .color_input.'+ format).addClass('active');
}

function checkAllValue () {
    // RGBA 255
    check0To255ValueSelector('#r255');
    check0To255ValueSelector('#g255');
    check0To255ValueSelector('#b255');
    check0To255ValueSelector('#a255');
    // RGBA 1
    check0To1ValueSelector('#r1');
    check0To1ValueSelector('#g1');
    check0To1ValueSelector('#b1');
    check0To1ValueSelector('#a1');
    // CMYK 100
    check0To100ValueSelector('#c100');
    check0To100ValueSelector('#m100');
    check0To100ValueSelector('#y100');
    check0To100ValueSelector('#k100');

    check0To360degValueSelector('#h360');
    check0To100ValueSelector('#s100');
    check0To100ValueSelector('#l100');
    check0To1ValueSelector('#a1_hsla');

    /*if ($('#hexa').val() == '') {
     updateHexaFrom255();
     }*/
}
function check0To1ValueSelector (selector) {
    $(selector).val(check0To1Value($(selector).val()));
}
function check0To1Value (value) {
    if (value == '' || parseFloat(value) < 0) {
        return 0;
    }
    else if (parseFloat(value) > 1) {
        value = parseFloat(value);
        do {
            value = parseFloat((value/10).toFixed(3));
        } while (value > 1);
        return value;
    }
    else {
        return value;
    }
}
function check0To100ValueSelector (selector) {
    $(selector).val(check0To100Value($(selector).val()));
}
function check0To100Value (value) {
    if (value == '' || parseInt(value) < 0) {
        return 0;
    }
    else if (parseInt(value) > 100) {
        return 100;
    }
    else {
        return value;
    }
}
function check0To255ValueSelector (selector) {
    $(selector).val(check0To255Value($(selector).val()));
}
function check0To255Value (value) {
    if (value == '' || parseInt(value) < 0) {
        return 0;
    }
    else if (parseInt(value) > 255) {
        return 255;
    }
    else {
        return value;
    }
}
function check0To360degValueSelector (selector) {
    $(selector).val(check0To360degValue($(selector).val()));
}
function check0To360degValue (value) {
    if (value == '' || parseInt(value) < 0) {
        value = parseInt(value);
        while (value < 0) {
            value += 360;
        }
        return value;
    }
    else if (parseInt(value) > 360) {
        value = parseInt(value);
        value %= 360;
        return value;
    }
    else {
        return value;
    }
}

function convert255To1 (value) {
    return Math.round((value / 255) * 1000) / 1000;
}
function convert1To255 (value) {
    return Math.round(value * 255);
}
function convert255ToFF (value) {
    var hex = parseInt(value).toString(16);
    if (hex.length == 1)
        hex = "0"+hex;
    return hex;
}
function convertFFTo255 (value) {
    return parseInt(value, 16);
}
function convertHexaToRgba (value) {
    var hex = value;
    var hexR, hexG, hexB, hexA;
    if (hex.length != 3 && hex.length != 4 && hex.length != 6 && hex.length != 8){
        return;
    } else if (hex.length <= 4) {
        hexR = hex.substr(0,1) + hex.substr(0,1);
        hexG = hex.substr(1,1) + hex.substr(1,1);
        hexB = hex.substr(2,1) + hex.substr(2,1);
        if (hex.length > 3)
            hexA = hex.substr(3,1) + hex.substr(3,1);
        else
            hexA = "FF";
    } else {
        hexR = hex.substr(0,2);
        hexG = hex.substr(2,2);
        hexB = hex.substr(4,2);
        if (hex.length > 6)
            hexA = hex.substr(6,2);
        else
            hexA = "FF";
    }

    return 'rgba('+convertFFTo255(hexR)+','+convertFFTo255(hexG)+','+convertFFTo255(hexB)+','+convert255To1(convertFFTo255(hexA))+')';
}

function update1From255 () {
    $("#r1").val(convert255To1($("#r255").val()));
    $("#g1").val(convert255To1($("#g255").val()));
    $("#b1").val(convert255To1($("#b255").val()));
    $("#a1").val(convert255To1($("#a255").val()));
}
function update255From1 () {
    $("#r255").val(convert1To255($("#r1").val()));
    $("#g255").val(convert1To255($("#g1").val()));
    $("#b255").val(convert1To255($("#b1").val()));
    $("#a255").val(convert1To255($("#a1").val()));
}
function update255FromHexa () {
    var hex = $("#hexa").val();
    var hexR, hexG, hexB, hexA;
    if (hex.length != 3 && hex.length != 4 && hex.length != 6 && hex.length != 8){
        return;
    } else if (hex.length <= 4) {
        hexR = hex.substr(0,1) + hex.substr(0,1);
        hexG = hex.substr(1,1) + hex.substr(1,1);
        hexB = hex.substr(2,1) + hex.substr(2,1);
        if (hex.length > 3)
            hexA = hex.substr(3,1) + hex.substr(3,1);
        else
            hexA = "FF";
    } else {
        hexR = hex.substr(0,2);
        hexG = hex.substr(2,2);
        hexB = hex.substr(4,2);
        if (hex.length > 6)
            hexA = hex.substr(6,2);
        else
            hexA = "FF";
    }
    $("#r255").val(convertFFTo255(hexR));
    $("#g255").val(convertFFTo255(hexG));
    $("#b255").val(convertFFTo255(hexB));
    $("#a255").val(convertFFTo255(hexA));
}
function updateHexaFrom255 () {
    var r255 = $("#r255").val();
    var g255 = $("#g255").val();
    var b255 = $("#b255").val();
    var a255 = $("#a255").val();

    $("#hexa").val(convert255ToFF(r255)+convert255ToFF(g255)+convert255ToFF(b255)+convert255ToFF(a255));
}
function updateRGBFromCMYK () {
    var c = ($("#c100").val()) / 100;
    var m = ($("#m100").val()) / 100;
    var y = ($("#y100").val()) / 100;
    var k = ($("#k100").val()) / 100;

    var r = 255 * (1-c) * (1-k);
    var g = 255 * (1-m) * (1-k);
    var b = 255 * (1-y) * (1-k);

    $("#r255").val(parseInt(r));
    $("#g255").val(parseInt(g));
    $("#b255").val(parseInt(b));
    $("#a255").val(255);
}
function updateCMYKFromRGB () {
    var a = $("#a1").val();
    var r = ((1-a) * 255 + a * $("#r255").val()) / 255;
    var g = ((1-a) * 255 + a * $("#g255").val()) / 255;
    var b = ((1-a) * 255 + a * $("#b255").val()) / 255;

    var k = 1-max(r,g,b);
    if (k < 1) {
        var c = (1-r-k) / (1-k);
        var m = (1-g-k) / (1-k);
        var y = (1-b-k) / (1-k);
    } else {
        var c = 0;
        var m = 0;
        var y = 0;
    }

    k = parseInt(k*100);
    c = parseInt(c*100);
    m = parseInt(m*100);
    y = parseInt(y*100);

    $("#k100").val(k);
    $("#c100").val(c);
    $("#m100").val(m);
    $("#y100").val(y);
}
function updateRGBFromHSL () {
    var h = parseFloat($("#h360").val()) / 360;
    var s = parseFloat($("#s100").val()) / 100;
    var l = parseFloat($("#l100").val()) / 100;
    var a = parseFloat($("#a1_hsla").val());

    var r, g, b;

    if (s == 0) {
        r = g = b = l; // achromatic
    } else {
        function hue2rgb(p, q, t) {
            if(t < 0) t += 1;
            if(t > 1) t -= 1;
            if(t < 1/6) return p + (q - p) * 6 * t;
            if(t < 1/2) return q;
            if(t < 2/3) return p + (q - p) * (2/3 - t) * 6;
            return p;
        }

        var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var p = 2 * l - q;
        r = hue2rgb(p, q, h + 1/3);
        g = hue2rgb(p, q, h);
        b = hue2rgb(p, q, h - 1/3);
    }

    $("#r255").val(parseInt(Math.round(r*255)));
    $("#g255").val(parseInt(Math.round(g*255)));
    $("#b255").val(parseInt(Math.round(b*255)));
    $("#a255").val(parseInt(Math.round(a*255)));
}
function updateHSLFromRGB () {
    var r = parseFloat($("#r1").val());
    var g = parseFloat($("#g1").val());
    var b = parseFloat($("#b1").val());
    var a = parseFloat($("#a1").val());

    var max = Math.max(r, g, b)
    var min = Math.min(r, g, b);
    var h, s, l = (max + min) / 2;

    if(max == min){
        h = s = 0; // achromatic
    } else {
        var d = max - min;
        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
        switch(max){
            case r:
                h = (g - b) / d + (g < b ? 6 : 0);
                break;
            case g:
                h = (b - r) / d + 2;
                break;
            case b:
                h = (r - g) / d + 4;
                break;
        }
        h /= 6;
    }

    $("#h360").val(parseInt(Math.round(h*360)));
    $("#s100").val(parseInt(Math.round(s*100)));
    $("#l100").val(parseInt(Math.round(l*100)));
    $("#a1_hsla").val(a);
}

function updatePreview () {
    // Title
    var value = "rgba("+$("#r255").val()+","+$("#g255").val()+","+$("#b255").val()+","+$("#a1").val()+")";
    $(".color_preview #dynamic_color").css("background-color",value);
    $(".paper-plane-colored").css("color",value);
    // Preset to add
    $(".preset_new .preset-to-add").css('color', value);
    $(".preset_new .hex-to-add").text('#'+$("#hexa").val());

    updateQuickCopy();
    updatePreviewDemo();
}

window.currentRegex = '';
function updateQuickCopy () {
    var strResult = window.currentRegex;

    strResult = strResult.replace(/@r255/ig, $("#r255").val());
    strResult = strResult.replace(/@g255/ig, $("#g255").val());
    strResult = strResult.replace(/@b255/ig, $("#b255").val());
    strResult = strResult.replace(/@a255/ig, $("#a255").val());

    strResult = strResult.replace(/@r1/ig, $("#r1").val());
    strResult = strResult.replace(/@g1/ig, $("#g1").val());
    strResult = strResult.replace(/@b1/ig, $("#b1").val());
    strResult = strResult.replace(/@a1/ig, $("#a1").val());

    strResult = strResult.replace(/@rHex/ig, convert255ToFF($("#r255").val()));
    strResult = strResult.replace(/@gHex/ig, convert255ToFF($("#g255").val()));
    strResult = strResult.replace(/@bHex/ig, convert255ToFF($("#b255").val()));
    strResult = strResult.replace(/@aHex/ig, convert255ToFF($("#a255").val()));
    strResult = strResult.replace(/@rgbHex/ig, convert255ToFF($("#r255").val())+convert255ToFF($("#g255").val())+convert255ToFF($("#b255").val()));
    strResult = strResult.replace(/@rgbaHex/ig, convert255ToFF($("#r255").val())+convert255ToFF($("#g255").val())+convert255ToFF($("#b255").val())+convert255ToFF($("#a255").val()));
    strResult = strResult.replace(/@argbHex/ig, convert255ToFF($("#a255").val())+convert255ToFF($("#r255").val())+convert255ToFF($("#g255").val())+convert255ToFF($("#b255").val()));

    strResult = strResult.replace(/@c100/ig, $("#c100").val());
    strResult = strResult.replace(/@m100/ig, $("#m100").val());
    strResult = strResult.replace(/@y100/ig, $("#y100").val());
    strResult = strResult.replace(/@k100/ig, $("#k100").val());

    strResult = strResult.replace(/@h360/ig, $("#h360").val());
    strResult = strResult.replace(/@s100/ig, $("#s100").val());
    strResult = strResult.replace(/@l100/ig, $("#l100").val());
    strResult = strResult.replace(/@a1/ig, $("#a1_hsla").val()); /* obsolete */

    $('#quick_copy').html(strResult);
}

function updateAllFromAutodetect () {
    detectFormatType();
}
function updateAllFrom255 () {
    update1From255();
    updateHexaFrom255();
    updateCMYKFromRGB();
    updateHSLFromRGB();

    updatePreview();
}
function updateAllFrom1 () {
    update255From1();
    updateHexaFrom255();
    updateCMYKFromRGB();
    updateHSLFromRGB();

    updatePreview();
}
function updateAllFromHexa () {
    update255FromHexa();
    update1From255();
    updateCMYKFromRGB();
    updateHSLFromRGB();

    updatePreview();
}
function updateAllFromCMYK () {
    updateRGBFromCMYK();
    update1From255();
    updateHexaFrom255();
    updateHSLFromRGB();

    updatePreview();
}
function updateAllFromHSLA () {
    updateRGBFromHSL();
    update1From255();
    updateHexaFrom255();
    updateCMYKFromRGB();

    updatePreview();
}

function min () {
    var min = arguments[0];
    for (i=1; i<arguments.length; i++) {
        if (arguments[i] < min) {
            min = arguments[i];
        }
    }
    return min;
}
function max () {
    var max = arguments[0];
    for (i=1; i<arguments.length; i++) {
        if (arguments[i] > max) {
            max = arguments[i];
        }
    }
    return max;
}

function initQuickCopy () {
    for (i = 0; i < quickCopy.length; i++) {
        $('#language_bar').append('<span class="regex" data-regex="'+quickCopy[i].regex+'">'+quickCopy[i].language+'</span>');
    }
    $('#language_bar').append($('#custom_regex'));
    //$('#language_bar').append('<span id="custom_regex" class="regex">Custom</span>');
    //$('#language_bar').append('<input id="custom-data-regex">');

    $('#language_bar span:first-child').addClass('active');
    window.currentRegex = $('#language_bar span:first-child').attr("data-regex");
}


function hideCustomToolBar () {
    $('#custom_toolbar').hide();
}
function showCustomToolBar () {
    $('#custom_toolbar').show();
}

function detectFormatType () {
    var results = [];
    for (var i=0; i < autoDetect.formats.length; i++) {
        var test = autoDetect.formats[i];
        var re = new RegExp(test.regex, "i");
        var matches = re.exec($("#auto-detect").val());
        if (matches != null) {
            results[results.length] = {
                "format": test.name,
                "matches": matches,
            };
        }
    }
    return results;
}

function updatePreviewDemo () {
    var value = "rgba("+$("#r255").val()+","+$("#g255").val()+","+$("#b255").val()+","+$("#a1").val()+")";
    $("#preview_zone .preview_text").css("color",value);
    $("#preview_zone .preview_background").css("background-color",value);
    $("#preview_zone .preview_border").css("border-color",value);
}




/**     Custom regex     */
window.caret = { start: 0, end: 0 };
function saveCaret () {
    window.caret.start 	= $('#custom-data-regex').caret().start;
    window.caret.end 	= $('#custom-data-regex').caret().end;
    console.log(window.caret);
}
function addRegexKeyCode () {
    var value = $(this).attr("data-value");
    console.log(value);
    console.log(window.caret.start + ' - ' + window.caret.end);

    $('#custom-data-regex').focus();
}