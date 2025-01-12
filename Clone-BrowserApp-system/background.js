chrome.app.runtime.onLaunched.addListener(function() {
	chrome.app.window.create('window.html', {
		'bounds': {
			'width': 820,
			'height': 540
		},
                frame:{color:"#808080"},
		'minWidth': 820,
		'minHeight': 540,
		'maxWidth': 820,
		'maxHeight': 540,
		'resizable': false
	});
});