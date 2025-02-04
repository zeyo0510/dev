var appWin = null;
var appOptions = new options();

$(function () {
    resizeWindows();
    // event handlers
    // copy the GUID to clipboard
    loadNewGUID();

    $('#copy-button').on('click', function () {
        copyTextToClipboard($("div#window-container div#quickguid").text());
    });

    // generate new GUID
    $('#refresh-button').on('click', function () {
        loadNewGUID();
    });

    // show options
    $('#appoptions').on('click', function () {
        loadOptionsPage();
        $('#quickgenerator').slideToggle({
            duration: 'normal',
            step: function (now, tween) {
                resizeWindows();
            },
            complete: function () {
                $('#options-container').slideToggle({
                    duration: 'normal',
                    step: function (now, tween) {
                        resizeWindows();
                    },
                    complete: function () {
                        resizeWindows();
                        $('#title-label').html('Quick GUID Options');
                    }
                });
            }
        });
        $('#divMainPageButtons').hide('normal');
        $('#divOptionPageButtons').show('normal');
    });

    // show main
    $('#btnOptionsBackToMain').on('click', function () {
        $('#options-container').slideToggle({
            duration: 'normal',
            step: function (now, tween) {
                resizeWindows();
            },
            complete: function () {
                $('#quickgenerator').slideToggle({
                    duration: 'normal',
                    step: function (now, tween) {
                        resizeWindows();
                    },
                    complete: function () {
                        resizeWindows();
                        $('#title-label').html('Quick GUID');
                    }
                });
        loadNewGUID();
    }});
        $('#divMainPageButtons').show('normal');
        $('#divOptionPageButtons').hide('normal');
    });

    $('#cbBraces').on('click', function () {
        appOptions.setIsBracesOn($(this).prop('checked'));
        //chrome.runtime.sendMessage({ method: "SetOption", params: { optionName: "IsBracesOn", optionValue: $(this).prop('checked') } }, function () { return; });
    });

    $('#cbUpperCase').on('click', function () {
        appOptions.setIsUpperCase($(this).prop('checked'));
        //chrome.runtime.sendMessage({ method: "SetOption", params: { optionName: "IsUpperCase", optionValue: $(this).prop('checked') } }, function () { return; });
    });

    $('#cbOmitHyphens').on('click', function () {
        appOptions.setOmitHyphens($(this).prop('checked'));
        //chrome.runtime.sendMessage({ method: "SetOption", params: { optionName: "OmitHyphens", optionValue: $(this).prop('checked') } }, function () { return; });
    });

    $('#optGUIDType').on('change', function () {
        appOptions.setGUIDType($(this).val());
        switch ($(this).val()) {
            case '1':
                $('#divMSGUIDOptions').show({
                    duration: 'normal',
                    step: function (now, tween) {
                        resizeWindows();
                    },
                    complete: function () {
                        resizeWindows();
                    }
                });
                break;
            case '2':
                $('#divMSGUIDOptions').hide({
                    duration: 'normal',
                    step: function (now, tween) {
                        resizeWindows();
                    },
                    complete: function () {
                        resizeWindows();
                    }
                });
                break;
        }
    });

    $('#close-button').on('click', function () {
        window.close();
    });
    //$(document).bind('contextmenu', function (e) {
    //    return false;
    //}); 
    //$('body').on('resize', function (data) { console.log('hello'); });
    // load GUIDType and adjust the option panel view
})

function resizeWindows() {
    $height = $('div#window-container').outerHeight();
    var appWin = chrome.app.window.current();
    appWin.setBounds({ height: $height });
}

if (!window.GUID) window.GUID = function () {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

if (!window.loadNewGUID) window.loadNewGUID = function () {
    appOptions.loadOptions(function ($options) {
        //console.log($options);
        data = $options.IsUpperCase ? GUID().toUpperCase() : GUID();
        withBraces = "{" + data + "}";
        withoutBraces = "" + data;
        data1 = $options.IsBracesOn ? withBraces : withoutBraces;
        data1 = ($options.OmitHyphens) ? data1.replace(/-/g, '') : data1;
        data1 = ($options.GUIDType == '2') ? Base64.convert(data1, false) : data1;
        $('div#window-container div#quickguid').html(data1);
    });
}

function loadOptionsPage() {
    appOptions.loadOptions(function ($options) {
        $('#cbBraces').prop('checked', $options.IsBracesOn);
        $('#cbUpperCase').prop('checked', $options.IsUpperCase);
        $('#cbOmitHyphens').prop('checked', $options.OmitHyphens);
        $('#optGUIDType').val($options.GUIDType);
        if ($options.GUIDType == 1) {
            $('#divMSGUIDOptions').show();
        }
        else {
            $('#divMSGUIDOptions').hide();
        }
    });
}

function copyTextToClipboard(text) {
    var copyFrom = $('<textarea/>');
    copyFrom.text(text);
    $('body').append(copyFrom);
    copyFrom.select();
    document.execCommand('copy', true);
    copyFrom.remove();
}