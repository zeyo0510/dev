

window.onerror = function (e) {
    MessageBox.show('Pursolid', e, "OK", "Error");
}
window.onload = function () {


    var toggleMax = false;
    var max = document.getElementById('max')
    var min = document.getElementById('min')
    var close = document.getElementById('close')

  
max.onclick = function () {
    if (!chrome.app.window.current().isMaximized())
    {
        this.title = "Restore";
        this.style.backgroundImage = 'url(/res/max.png)';
        chrome.app.window.current().maximize();
    }

    else {
        this.title = "Maxmimize";
        this.style.backgroundImage = 'url(/res/restore.png)';
        chrome.app.window.current().restore();
    }
}
min.onclick = function () {
   
    if (!chrome.app.window.current().isMinimized())
        chrome.app.window.current().minimize();
    else {
        chrome.app.window.current().restore();
    }
}
settings.onclick = function () {
    var rt = new runtime(Core);

    rt.showSettings();
}
close.onclick = function () {
    var close = true;
   
        var openedFiles = Core.getModule('sourceDesigner').getOpenedFiles();
        if (openedFiles.length == 0) {
            chrome.app.window.current().close();
        }
        else {
            for (var i = 0; i < openedFiles.length; i++) {
                if ('editedContent' in openedFiles[i]) {
                    close = false;
                    MessageBox.show('Pursolid', 'Do you want to save changes to this files?', "Yes_No_Cancel", "Error", function (result) {
                        if (result == 'Cancel') { return; }
                        else if (result == "Yes") {
                            var appRunTime = new runtime(Core);
                            appRunTime.saveAll(function () {
                                chrome.app.window.current().close();
                            });
                            
                        }
                        else {
                            chrome.app.window.current().close();
                        }
                    })
                    break;
                }
            }
            if (close) {
                chrome.app.window.current().close();
            }
        }
  
}




var codeEditor = document.getElementById('code-editor');
var titleBar = document.getElementById('titlebar');
var content = document.getElementById('content');
var seCnt = document.getElementById('solution-explorer-cnt');

    //Core.start('mainMenu');
Core.start('toolbox');
Core.start('newProject');
Core.start('openProject');
Core.start('solutionExplorer');
Core.start('sourceDesigner');
Core.start('newFile');
Core.start('settings');
Core.start('report');
IO.getFromLocal(function (data) {
    if (data.theme) {
        var rt = new runtime(Core);
        rt.setTheme(data.theme);
    }
});
function updateLayout() {
    content.style.height = window.innerHeight - titleBar.offsetHeight + 'px';
    codeEditor.style.height = content.style.height;
 
    codeEditor.style.width = content.offsetWidth - seCnt.offsetWidth + 'px';

    seCnt.style.height = content.style.height;
    Core.updateLayout();
}
window.addEventListener('resize', updateLayout, false);
updateLayout();


};