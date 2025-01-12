function HTML5FileSystem() {
    window.requestFileSystem = window.requestFileSystem || window.webkitRequestFileSystem || window.mozRequestFileSystem;
    var fileSystem = null;
   

  
    this.init = function (callback) {
      
        window.requestFileSystem(window.TEMPORARY, 1024 * 1000024, onInitFs, errorHandler);
        function onInitFs(fs) {

            fileSystem = fs;
            callback();

        }
    }
    this.createFolder = function (name,callback) {
       
        fileSystem.root.getDirectory(name, { create: true }, function (dirEntry) {
            var folder = new Folder();
            folder.name = dirEntry.name;
            folder.phisicalPath = dirEntry.fullPath;
            callback(folder);
        }, errorHandler);
    }
    this.renameFolder = function (entry, name) {
        var paths = entry.phisicalPath.split('/');
        paths.pop();
      
        var parentPath = paths.join('/');
      
        fileSystem.root.getDirectory(parentPath, {}, function (parentEntry) {
        fileSystem.root.getDirectory(entry.phisicalPath, {}, function (dirEntry) {
            dirEntry.moveTo(parentEntry, name, function () {
                entry.rename(name);
            }, errorHandler);
        });
        });
    };
    this.getEntries = function (path, callback) {
        if (path == null) {
            var dirReader = fileSystem.root.createReader();

            var entries = [];
            dirReader.readEntries(function (result) {
                 
                for (var i = 0; i < result.length; i++) {

               
                    if (result[i].isDirectory) {
                        var folder = new Folder();
                        folder.phisicalPath = result[i].fullPath;
                        folder.name = result[i].name;
                        entries.push(folder);
                    }
                    else {
                        var file = new File();
                        file.phisicalPath = result[i].fullPath;
                        file.name = result[i].name;
                        entries.push(file);
                    }
                }
                    callback(entries);
              
            });
        }
        else {
            fileSystem.root.getDirectory(path, { create: false }, function (dirEntry) {
                var dirReader = dirEntry.createReader();
                var entries = [];
                dirReader.readEntries(function (result) {
                    for (var i = 0; i < result.length; i++) {


                        if (result[i].isDirectory) {
                            var folder = new Folder();
                            folder.phisicalPath = result[i].fullPath;
                            folder.name = result[i].name;
                            entries.push(folder);
                        }
                        else {
                            var file = new File();
                            file.phisicalPath = result[i].fullPath;
                            file.name = result[i].name;
                            entries.push(file);
                        }
                    }
                    callback(entries);
                });
            })
        }
    }
    this.exist = function (name,callback) {
        fileSystem.root.getDirectory(name, { create: false }, function () {
            callback(true);
        }, function () {
            callback(false);
        });
    }
    this.fileExist = function (name, callback) {
        fileSystem.root.getFile(name, { create: false }, function () {
            callback(true);
        }, function () {
            callback(false);
        });
    }
    this.createFile = function (path, content, callback) {
      
        fileSystem.root.getFile(path, { create: true, exclusive: false }, function (fileEntry) {

            var file = new File();
            file.name = fileEntry.name;
            file.phisicalPath = fileEntry.fullPath;

            if (content != null) {
                fileEntry.createWriter(function (fileWriter) {
                    fileWriter.onwriteend = function (e) {
                      
                        callback(file);
                    };
                    fileWriter.onerror = function (e) {
                      console.log('Write faild: ' + e.toString());
                    }
                    var blob = new Blob([content], { type: 'text/plain' })
                    fileWriter.write(blob);

                });
            } else {
                callback(file);
            }
        },errorHandler);
    }
    this.getFile = function (path, callback) {
        fileSystem.root.getFile(path, { create: false }, function (fileEntry) {
            fileEntry.file(function (file) {
                callback(file);
            }, errorHandler);

         
         
           
        });
    }
    this.removeFile = function (path, callback) {
        fileSystem.root.getFile(path, {create:false}, function (fileEntry) {
            fileEntry.remove(function () {
                callback();
            }, errorHandler);
        });
    }
    this.readFile = function (path, callback) {
        fileSystem.root.getFile(path, {}, function (fileEntry) {
            fileEntry.file(function (file) {
                var reader = new FileReader();
                reader.onloadend = function (e) {
                    
                    callback(this.result);
                };
                reader.readAsText(file);
            },errorHandler);
        },errorHandler);
    }
    //Error handling
    function errorHandler(e) {
        var msg = '';

        switch (e.code) {
            case FileError.QUOTA_EXCEEDED_ERR:
                msg = 'QUOTA_EXCEEDED_ERR';
                break;
            case FileError.NOT_FOUND_ERR:
                msg = 'NOT_FOUND_ERR';
                break;
            case FileError.SECURITY_ERR:
                msg = 'SECURITY_ERR';
                break;
            case FileError.INVALID_MODIFICATION_ERR:
                msg = 'INVALID_MODIFICATION_ERR';
                break;
            case FileError.INVALID_STATE_ERR:
                msg = 'INVALID_STATE_ERR';
                break;
            default:
                msg = 'Unknown Error';
                break;
        };

       alert('FileSystem Error: ' + msg);
    }

}