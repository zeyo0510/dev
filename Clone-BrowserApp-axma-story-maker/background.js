var CurrentWindow;
var prefs_lang_code = chrome.i18n.getUILanguage();
var prefs_win_width = 768;
var prefs_win_height = 550;
var prefs_win_state = 'maximized';
var prefs_first_start = true;

chrome.app.runtime.onLaunched.addListener(function() {
	if (chrome.app.window.getAll().length>0) {
		return;
    }
	checkUpdates();
	loadPrefs();
});

function createWindow() {
	chrome.app.window.create(
		'asm.html?#LangCode='+prefs_lang_code+'&TargetChromeApp=true&QuickStart='+prefs_first_start,
		{
			frame: 'chrome',
			state: prefs_win_state,
			minWidth: 768,
			minHeight: 500,
			bounds: {
				width: prefs_win_width,
				height: prefs_win_height,
			},
		},
		function(win) {
			prefs_first_start = false;
			CurrentWindow = win;
			win.onClosed.addListener(function() {
				savePrefs();
			});
		}
	);
}

function loadPrefs() {
	fromStorage(function(loadObj) {
		if (loadObj.first_start!=null) {
			prefs_win_state = loadObj.win_state;
			prefs_win_width = loadObj.win_width;
			prefs_win_height = loadObj.win_height;
			prefs_lang_code = loadObj.lang_code;
			prefs_first_start = loadObj.first_start;
		}
		createWindow();
	});
}

function savePrefs() {
	if (CurrentWindow.isMaximized()) {
		prefs_win_state = 'maximized';
	} else {
		prefs_win_state = 'normal';
	}
	prefs_win_width = CurrentWindow.getBounds().width;
	prefs_win_height = CurrentWindow.getBounds().height;
	var saveObj = {
		win_state: prefs_win_state,
		win_width: prefs_win_width,
		win_height: prefs_win_height,
		first_start: prefs_first_start,
		lang_code: prefs_lang_code,
	};
	toStorage(saveObj);
}

function toStorage(obj) {
	chrome.storage.local.set(obj);
}

function fromStorage(callback) {
	chrome.storage.local.get(null,function(result) {
		callback(result);
	});
}

function checkUpdates() {
	chrome.runtime.requestUpdateCheck(function(status) {
		if (status == "update_available") {
			chrome.runtime.reload();
		} else if (status == "no_update") {
			// no update
		} else if (status == "throttled") {
			// error
		}
	});
}