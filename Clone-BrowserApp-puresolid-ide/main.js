chrome.app.runtime.onLaunched.addListener(function () {
    id = "Chrome-" + Math.random();
    var w =Math.round(window.screen.availWidth - window.screen.availWidth / 4);
    var h = Math.round(window.screen.availHeight - window.screen.availHeight / 4);

    chrome.app.window.create('Index.html', {
        'id': id,
        'frame': 'none',
        'width':w,
        'height': h,
        // screenshots
        // 'width': 1335,
        // 'height': 834,
        'minWidth': 800,
        'minHeight': 480
       
    });
});
