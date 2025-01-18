chrome.app.runtime.onLaunched.addListener(function () {
    // Center window on screen.
    var screenWidth = screen.availWidth;
    var screenHeight = screen.availHeight;
    var width = 450;
    var height = 300;

    var appWin = chrome.app.window.create('app/popup.html', {
        frame: "none",
        id: "QuickGUID",
        bounds: {
            width: width,
            height: height,
            left: Math.round((screenWidth - width) / 2),
            top: Math.round((screenHeight - height) / 2)
        },
        resizable: false
    });
});