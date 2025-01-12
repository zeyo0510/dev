var runtime = function (descriptor) {

    this.descriptor = descriptor || {};

}

runtime.prototype.initComponents = function (shape) {

}
runtime.prototype.newProject = function () {
    if (this.descriptor.getModule('newProject') != null)
        this.descriptor.getModule('newProject').show();
}
runtime.prototype.newFile = function (target) {
    if (this.descriptor.getModule('newFile') != null)
        this.descriptor.getModule('newFile').show(target);
}
runtime.prototype.closeProject = function () {
    this.descriptor.project = null;
    if (this.descriptor.getModule('solutionExplorer') != null)
        this.descriptor.getModule('solutionExplorer').clearItems();
    if (this.descriptor.getModule('sourceDesigner') != null)
        this.descriptor.getModule('sourceDesigner').close();
}
runtime.prototype.openExistingProject = function (dirEntry) {
    if (this.descriptor.getModule('solutionExplorer') != null) {
        this.closeProject();
        this.descriptor.project = new Project();
        var rootElem = null;
        var root = new Folder();
        root.name = dirEntry.name;
        root.phisicalPath = dirEntry.fullPath;
        root.entry = dirEntry;
        this.descriptor.project.addItem(root);
        this.descriptor.rootEntry = dirEntry;
    
        var descriptor = this.descriptor;


        function loadFiles(node, p) {

            IO.getEntries(node.phisicalPath, dirEntry, function (result) {

                for (var i = 0; i < result.length; i++) {
                    if (result[i] instanceof Folder) {

                        node.addFolder(result[i]);
                        loadFiles(result[i], descriptor.getModule('solutionExplorer').addItem(p, result[i]));
                        if (!rootElem.isOpen)
                            rootElem.toggleExpand();
                    } else {
                        if (result[i].name == 'project.ps') continue;

                        descriptor.getModule('solutionExplorer').addItem(p, result[i]);
                        node.addFile(result[i]);
                        if (!rootElem.isOpen)
                            rootElem.toggleExpand();

                    }
                }

            });
        }
        var parent = this.descriptor.getModule('solutionExplorer').addItem(null, root);
        var rootElem = parent;

      

   



     

        loadFiles(root, parent);

        
    }
};
runtime.prototype.addFile = function (node, name,ext) {

    var self = this;
    var extArr = name.split('.');

    if(extArr[extArr.length - 1] != ext)
    name += '.'+ext;
    IO.createFile(name, node.href.entry, '', function (file) {
        var newItem = self.descriptor.getModule('solutionExplorer').addItem(node, file);
        if(!node.isOpen)
        node.toggleExpand();
        newItem.select();
        self.showFile(file);
    });



}
runtime.prototype.newFolder = function (node, name) {


    var folder = new Folder();
    folder.name = name;

    var newItem = this.descriptor.getModule('solutionExplorer').addItem(node, folder, true);
    if (!node.isOpen)
        node.toggleExpand();
    newItem.select();


}
runtime.prototype.showSettings = function () {
    if (this.descriptor.getModule('settings') != null)
        this.descriptor.getModule('settings').show();
}
runtime.prototype.createNewProject = function (data, title) {

    if (this.descriptor.getModule('solutionExplorer') != null) {
        this.closeProject();

        this.descriptor.project = new Project();
        var descriptor = this.descriptor;
        var rootElem = null;

        if (descriptor.rootEntry) {
            IO.createFolder(title, this.descriptor.rootEntry, function (root) {
                root.type = data;
                var parent = descriptor.getModule('solutionExplorer').addItem(null, root);
                descriptor.project.addItem(root);
                rootElem = parent;
                IO.createFile('project.ps', root.entry, data, function () {
                    loadFiles(templates[data].files, root, parent);
                });

            });

        }

        function loadFiles(collection, node, p) {
            for (var item in collection) {
                if ((typeof collection[item]) == 'object') {
                    IO.createFolder(item, node.entry, function (folder) {
                        node.addFolder(folder);


                        loadFiles(collection[folder.name], folder, descriptor.getModule('solutionExplorer').addItem(p, folder));
                        if (!rootElem.isOpen)
                            rootElem.toggleExpand();
                    });
                }
                else {

                    IO.createFile(item, node.entry, collection[item], function (file) {
                        descriptor.getModule('solutionExplorer').addItem(p, file)

                        node.addFile(file);

                        if (!rootElem.isOpen)
                            rootElem.toggleExpand();
                    });
                }
            }
        }


    }




}
runtime.prototype.solutionExplorer = function () {
    if (this.descriptor.getModule('solutionExplorer') != null)

        this.descriptor.getModule('solutionExplorer').show();
}

runtime.prototype.showFile = function (file) {
    var descriptor = this.descriptor;
    if (this.descriptor.getModule('sourceDesigner') != null) {


        IO.getFile(file.phisicalPath, descriptor.rootEntry, function (fileItem) {

            var reader = new FileReader();
            reader.onloadend = function (e) {

                file.content = this.result;

                descriptor.getModule('sourceDesigner').showFile(file);
            };
            reader.readAsText(fileItem);



        });

    }
}
runtime.prototype.saveFile = function (file,content,callback) {
        
  
        IO.appendData(file, content,callback);
      
   
}
runtime.prototype.saveAll = function (callback) {
    var sd = this.descriptor.getModule('sourceDesigner');
    var no = 0;
    if (sd != null) {
        var openedFiles = sd.getOpenedFiles();
        var length = openedFiles.length;
        var self = this;
        for (var i = 0; i < length; i++) {
            if ("editedContent" in openedFiles[i]) {
                self.saveFile(openedFiles[i].href, openedFiles[i].editedContent, (function (x) {
                  
                    return function () {
                        sd.deleteOpenedFile(openedFiles[x]);
                        if ((x == length - 1) && callback) {
                            callback();
                        }
                    }
                })(i));
               
            }
            else {
                no++;
            }
        }
        if (no == length && callback) {
            callback();
        }
   
    }
}
runtime.prototype.setTheme = function (theme) {
    IO.setToLocal({ theme: theme }, function () { 
    if (theme == 'Light') {
        light = document.getElementById('lightTheme')
        if (!light) {
            var light = document.createElement('link');
            light.id = 'lightTheme';
        
            light.href = "/lib/cm/styles/light.css";
            light.rel = "stylesheet";
            document.head.appendChild(light);
        }
    }
    else {
        var light = document.getElementById('lightTheme');
        if (light) {
            document.head.removeChild(light);
        }
    }
    });
}
runtime.prototype.openReportWnd = function () {
    if (this.descriptor.getModule('report') != null)
        this.descriptor.getModule('report').show();
}
runtime.prototype.runProject = function () {
    var reader = new FileReader();
    var type = "";
    var self = this;
  
    if(self.descriptor.project){
        IO.getFile(this.descriptor.project.root[0].entry.fullPath+'/project.ps', this.descriptor.project.root[0].entry, function (file) {
   
            reader.onloadend = function (e) {
                var type = this.result;
                if (type.toLowerCase() == 'webapplication') {
                    runAsWebApplication()
                }
                else if (type.toLowerCase() == 'chromeapp') {
                    MessageBox.show('Pursolid', 'To run Chrome App\n go to Chrome->Tools->Extensions->Load unpacked extension', "OK", "Info");
                }
                else if (type.toLowerCase() == 'phpwebsite') {
                    runAsPHP();
                }
            };
            reader.readAsText(file);
 
   
        }, function () {
      
            runAsUnknown();
      
        });

    function creationCallback(notID) {

       
    }
    function runAsPHP() {
        var siteName = self.descriptor.project.root[0].name;
        window.open("http://localhost/" + siteName + "/Index.php");
    }
    function runAsUnknown() {
        var siteName = self.descriptor.project.root[0].name;
        runAsWebApplication();
    }
    function runAsWebApplication() {
      
       

        var handlers = [
    ['.*', DirectoryEntryHandler]
        ]
        window.entry = self.descriptor.project.root[0].entry;
        window.haveentry(self.descriptor.project.root[0].entry);
     
        var app = new chrome.WebApplication({ handlers: handlers,  port: 1234 })
        app.start()
        self.saveAll(function () {

     
        window.open("http://localhost:1234/Index.html");

        chrome.notifications.create("id1", {
            type: "basic",
            title: "Server is running",
            message: "Puresolid dev server is running at http://localhost:1234/",
            iconUrl: "/res/server.png"
        }, creationCallback);
        });
       
    }
}
 
}