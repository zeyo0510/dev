var IO = {};
(function (x) {
   x.clipboard= {};
    x.createFolder = function (name, parentEntry, callback) {
        parentEntry.filesystem.root.getDirectory(parentEntry.fullPath + "/" + name, { create: true }, function (dirEntry) {
            var folder = new Folder();
            folder.name = dirEntry.name;
            folder.phisicalPath = dirEntry.fullPath;
            folder.entry = dirEntry;
            if (callback)
                callback(folder);
        }, errorHandler);
    };
    x.createFile = function (name, parentEntry, content, callback) {

        parentEntry.filesystem.root.getFile(parentEntry.fullPath + "/" + name, { create: true, exclusive: false }, function (fileEntry) {

            var file = new File();
            file.name = fileEntry.name;
            file.phisicalPath = fileEntry.fullPath;
            file.entry = fileEntry;
            if (content != null) {
                fileEntry.createWriter(function (fileWriter) {
                    fileWriter.onwriteend = function (e) {
                        
                        callback(file);
                    };
                    fileWriter.onerror = function (e) {
                     
                    }
                    var blob = new Blob([content], { type: 'text/plain' })
                    fileWriter.write(blob);

                });
            } else {

                callback(file);
            }
        }, errorHandler);

    };
    x.appendData = function (file, content,callback) {
       if(content!=undefined)
        file.entry.createWriter(function (fileWriter) {
            var truncated = false;
            fileWriter.onwriteend = function (e) {
                if (!truncated) {
                    truncated = true;
                    this.truncate(this.position);
                  
                    if (this.error) console.log(this.error);
                    if (callback) callback();
                    return;
                }  
            }
            var blob = new Blob([content], { type: MimeType.fromExtenstion(file.extension) });
            fileWriter.write(blob);
        }, errorHandler);
    }
    
    x.copy = function (srcEntry, destEntry, callback) {
      
        srcEntry.copyTo(destEntry, srcEntry.name, function (dirEntry) {
            if (!dirEntry.isFile) {
                var folder = new Folder();
                folder.name = dirEntry.name;
                folder.phisicalPath = dirEntry.fullPath;
                folder.entry = dirEntry;


                callback(folder);
            }
            else {
                var file = new File();
                file.name = dirEntry.name;
                file.phisicalPath = dirEntry.fullPath;
                file.entry = dirEntry;
                callback(file);
            }
              
               
            }, errorHandler);
      
          
      
    };
    x.remove = function (entry, callback) {
        if (entry.isDirectory) {
            entry.removeRecursively(callback, errorHandler)
        }
        else {
            entry.remove(callback, errorHandler);
        }
    };
    x.cut = function (srcEntry, destEntry, callback) {
     
        srcEntry.moveTo(destEntry,  srcEntry.name, function (dirEntry) {
            if (!dirEntry.isFile) {
                var folder = new Folder();
                folder.name = dirEntry.name;
                folder.phisicalPath = dirEntry.fullPath;
                folder.entry = dirEntry;


                callback(folder);
            }
            else {
                var file = new File();
                file.name = dirEntry.name;
                file.phisicalPath = dirEntry.fullPath;
                file.entry = dirEntry;
                callback(file);
            }

            }, errorHandler);


        
    };
    x.rename = function (parent, target,name, callback) {

        target.moveTo(parent, name, function (dirEntry) {
            if (!dirEntry.isFile) {
                var folder = new Folder();
                folder.name = dirEntry.name;
                folder.phisicalPath = dirEntry.fullPath;
                folder.entry = dirEntry;

                callback(folder);
            }
            else {
                var file = new File();
                file.name = dirEntry.name;
                file.phisicalPath = dirEntry.fullPath;
                file.entry = dirEntry;
                callback(file);
            }

        }, errorHandler);



    };
    x.getFile = function (path, parentEntry, callback,error) {

        parentEntry.filesystem.root.getFile(path, { create: false }, function (fileEntry) {
                    fileEntry.file(function (file) {
                        if (callback)
                            callback(file);
                    },errorHandler );

        },function () {
            if (error)
                error();
            else {
                errorHandler();
            }
        });  
    };
    x.getEntry = function (path, parentEntry,type, callback) {
        if (type == "Folder") {
            parentEntry.filesystem.root.getDirectory(path, { create: false }, function (dirEntry) {
                var folder = new Folder();
                folder.name = dirEntry.name;
                folder.phisicalPath = dirEntry.fullPath;
                folder.entry = dirEntry;

                callback(folder);
            }, errorHandler);
        }
        else {
            parentEntry.filesystem.root.getFile(path, { create: false }, function (fileEntry) {
                var file = new File();
                file.name = fileEntry.name;
                file.phisicalPath = fileEntry.fullPath;
                file.entry = fileEntry;
                callback(file);
            }, errorHandler);
        }
    };
    x.getEntries = function (path, parentEntry, callback) {
        parentEntry.filesystem.root.getDirectory(path, { create: false }, function (dirEntry) { 
            var dirReader = dirEntry.createReader();
            var entries = [];
            dirReader.readEntries(function (result) {
                for (var i = 0; i < result.length; i++) {


                    if (result[i].isDirectory) {
                        var folder = new Folder();
                        folder.phisicalPath = result[i].fullPath;
                        folder.name = result[i].name;
                        folder.entry = result[i];
                        entries.push(folder);
                    }
                    else {
                        var file = new File();
                        file.phisicalPath = result[i].fullPath;
                        file.name = result[i].name;
                       file.entry = result[i];
                        entries.push(file);
                    }
                }
                callback(entries);
            });
     
        });

    };
    x.exist = function (name, parentEntry, callback,ind) {
       
        if (ind=='file') {
            parentEntry.filesystem.root.getFile(parentEntry.fullPath + '/' + name, { create: false }, function () {
                callback(true);
            }, function () {
                callback(false);
            });
        }
        else if (ind=='all'){
            parentEntry.filesystem.root.getFile(parentEntry.fullPath + '/' + name, { create: false }, function () {
                callback(true);
            }, function () {
                parentEntry.filesystem.root.getDirectory(parentEntry.fullPath + '/' + name, { create: false }, function () {
                    callback(true);
                }, function () {
                    callback(false);
                });
            });

        }
        else {

        parentEntry.filesystem.root.getDirectory(parentEntry.fullPath + '/' + name, { create: false }, function () {
            callback(true);
        }, function () {
            callback(false);
        });

        }

   
      
    };
    x.setToLocal = function (obj, callback) {
        chrome.storage.local.set(obj, function () {
            if(callback)callback();
            
        });

    };
    x.getFromLocal = function (callback) {
        chrome.storage.local.get(function(obj){
            if (callback)
                callback(obj);
        });
    };
    function errorHandler(e) {  
      

        MessageBox.show('Pursolid', e.name+'\n'+e.message, "OK", "Error");
    }
})(IO)
