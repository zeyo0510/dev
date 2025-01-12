/// <reference path="../views/toolBar/toolbar.js" />
/// <reference path="../views/toolBar/toolbarItem.js" />
/// <reference path="../appCore/runtime.js" />


Core.register('toolbox', function (runtime) {

    var toolBar;
    return {
        init: function () {
            toolBar = new Toolbar();
            cnt = document.getElementById('titlebar');
            cnt.insertBefore(toolBar.domElem, cnt.children[1]);
             //New Project
            newToolbarItem = new ToolbarItem();
            toolBar.addItem(newToolbarItem);
            newToolbarItem.title = 'New Project';
            newToolbarItem.image = '/res/new.png';

            //Open Project
            openToolbarItem = new ToolbarItem();
            toolBar.addItem(openToolbarItem);
            openToolbarItem.title = 'Load Project';
            openToolbarItem.image = '/res/open.png';

            //Save
            saveToolbarItem = new ToolbarItem();
            toolBar.addItem(saveToolbarItem);
            saveToolbarItem.title = 'Save All';
            saveToolbarItem.image = '/res/save.png';

            //Run
            runToolbarItem = new ToolbarItem();
            toolBar.addItem(runToolbarItem);
            runToolbarItem.title = 'Run';
            runToolbarItem.image = '/res/run.png';

            reportToolbarItem = new ToolbarItem();
            toolBar.addItem(reportToolbarItem);
            reportToolbarItem.title = 'Report';
            reportToolbarItem.image = '/res/report.png';

            //Events

            newToolbarItem.addEventListener('mouseup', function () {
                runtime.newProject();
            });
            runToolbarItem.addEventListener('mouseup', function () {
         
             runtime.runProject();
            });
            reportToolbarItem.addEventListener('mouseup', function () {

                runtime.openReportWnd();
            });
            saveToolbarItem.addEventListener('mouseup', function () {
        
                runtime.saveAll();
          
            });
            openToolbarItem.addEventListener('mouseup', function () {
               
                chrome.fileSystem.chooseEntry({ type: "openDirectory" }, function (dirEntry) {
                    if(dirEntry)
                    runtime.openExistingProject(dirEntry);
                })
            });
        }
    }
})