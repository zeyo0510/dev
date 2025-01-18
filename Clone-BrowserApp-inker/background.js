var createdWindow = null;
chrome.app.runtime.onLaunched.addListener(function (data) {
    if (!createdWindow) {
        chrome.system.display.getInfo(function (info) {
            var width = info[0].workArea.width;
            var height = info[0].workArea.height;
            chrome.app.window.create('index.html', {
                id: 'inker-window',
                state: 'maximized',
                frame: {
                    type: 'none'
                },
                outerBounds: {
                    width: width,
                    height: height,
                    minWidth: 600,
                    minHeight: 400
                }
            }, function (win) {
                win.onClosed.addListener(function () {
                    createdWindow = null;
                });
                win.maximize();
                win.contentWindow['launchDataItems'] = data.items;
                createdWindow = win;
            });
        });
    }
    else {
        if (data.items) {
            var win = createdWindow;
            win.focus();
            if (win.isMinimized()) {
                win.maximize();
            }
            win.contentWindow['onLaunchDataItems'](data.items);
        }
    }
});
