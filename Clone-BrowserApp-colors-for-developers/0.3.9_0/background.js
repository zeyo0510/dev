chrome.app.runtime.onLaunched.addListener(function() {
    chrome.app.window.create('color_app.html', {
        'outerBounds': {
            /*'width': 1024,
            'height': 700*/
            'width': 500,
            'height': 750
        },
        'frame': {
            'type': 'chrome',
            'color': '#F0F0F0',
            'activeColor': '#FFFFFF',
            'inactiveColor': '#CCCCCC'
        }
    });
});