chrome.app.runtime.onLaunched.addListener(function(intentData) {
	chrome.app.window.create('index.html', {
		width: 1200,
		height: 800,
		minWidth: 1280,
		minHeight: 600,
		type: 'shell'
	});
});