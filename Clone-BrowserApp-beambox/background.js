chrome.app.runtime.onLaunched.addListener(function() {
    // 保持宽高比 16:10
    var height = Math.ceil(window.screen.height * 0.8);
    var width = Math.ceil(height * 16 / 10 + 30);
    chrome.app.window.create('index.chrome.html', {
        id: chrome.i18n.getMessage('@@extension_id'),
        innerBounds: {
            width: width,
            height: height
        },
        resizable: false
    });
});
