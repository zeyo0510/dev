window.addEventListener('message', e => {
	// Recieving from app window
	if (e.data && e.data.contents && e.data.contents.match(/<html/)) {
		const frame = document.querySelector('iframe');
		frame.src = frame.src;
		setTimeout(() => {
			frame.contentWindow.postMessage(e.data, '*');
		}, 10);
	}
	if (e.data && e.data.url && e.data.url.match(/index\.html/)) {
		document.querySelector('iframe').src = e.data.url;
	}

	// Recieving from preview iframe
	if (e.data && e.data.logs) {
		(window.opener || window.top).postMessage(e.data, '*');
	}
});
