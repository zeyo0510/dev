var entries = {};// we have to store our entries in order to be able to save alterations

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
    //show the prompt to save the file
    if (request.action == "saveAs") {
        chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: 'Untitled.' + request.ext[0],
            accepts: [{
                extensions: request.ext
            }],
            acceptsAllTypes: false
        }, function (fileEntry) {
            if (fileEntry) {
                chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
                    //we will store the id of the path here, so we can use it when writing to the file
                    entries[path] = chrome.fileSystem.retainEntry(fileEntry);
                    sendResponse({filePath: path, fileName: fileEntry.name });
                });
            } else {
                sendResponse({canceled:true});
            }
        });
    } else if (request.action == "open") { //open existing file
        chrome.fileSystem.chooseEntry({
            type: 'openFile',
            accepts: [{
                extensions: request.ext
            }],
            acceptsAllTypes: false
        }, function (fileEntry) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
                //we will store the id of the path here, so we can use it when writing to the file
                entries[path] = chrome.fileSystem.retainEntry(fileEntry);

                var name = fileEntry.name;

                //we will have to use the entry to read the file, so let's do this here
                fileEntry.file(function(file) {
                    var reader = new FileReader();

                    reader.onloadend = function(e) {
                        //send the bytearray to the designer
                        sendResponse({filePath: path, fileName: name, fileData: Array.from(new Uint8Array(reader.result))});
                    };

                    reader.readAsArrayBuffer(file);
                });
            });
        });
    } else if (request.action == "write") { //write to new/existing file
        //restore the entry using the id we stored on the 'saveAs' action
        chrome.fileSystem.restoreEntry(entries[request.fileName], function(fileEntry) {
            fileEntry.createWriter(function(writer) {
                var dataArray = [];
                var data;
                for(data in request.fileData ) {
                    dataArray.push(request.fileData[data]);
                }
                //@TODO: Needs to improve this way. Try to use URLs: URL.createObjectURL(blob) <=> URL.revokeObjectURL(blob);
                //convert the array and write it into the file
                dataArray = new Uint8Array(dataArray);

                var truncated = false;

                writer.onwriteend = function() {
                    if (!truncated){
                        writer.truncate(dataArray.byteLength);
                        truncated = true;
                    }
                };

                writer.write(new Blob([dataArray], {type: "application/octet-stream"}));
            });
        });
    } else if (request.action === 'openLink') {
        chrome.browser.openTab({ url: request.link });
    } else if (request.action === 'openFontsEntry') {
        chrome.fileSystem.chooseEntry({
            type: 'openDirectory'
        }, function (fileEntry) {
            if (fileEntry) {
                chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
                    sendResponse({entry:chrome.fileSystem.retainEntry(fileEntry), 'path':path});
                });
            } else {
                sendResponse({entry:null});
            }
        });
    } else if (request.action === "saveFonts") {
        var fonts = request.fonts;
        if (fonts) {
            chrome.fileSystem.restoreEntry(request.entry, function(fileEntry) {
                for (var i = 0; i < fonts.length; ++i) {
                    let font = fonts[i];
                    let blob = font.blob;
                    let url = font.url;

                    let lastIndex = url.lastIndexOf("/");
                    let filepath = url.substring(0, lastIndex);
                    let filename = url.substring(lastIndex + 1, url.length);
                    
                    //this api is evil
                    chrome.fileSystem.getWritableEntry(fileEntry, function(entry) {
                        let folders = filepath.split('/');
                        let i = 0;
                        let count = folders.length;
                        var checkDirectoryAndSave = function(entry) {
                            entry.getDirectory(folders[i], {
                                create: true
                            }, function(entry) {
                                if (i < count) {
                                    i++;
                                    checkDirectoryAndSave(entry);
                                } else {
                                    entry.getFile(filename, {
                                        create: true
                                    }, function(entry) {
                                        entry.createWriter(function(writer) {
                                            writer.onwrite = function() {
                                                writer.onwrite = null;
                                                writer.truncate(writer.position);
                                            };
                                            writer.write(blob);
                                        });
                                    });
                                }
                            });
                        };
                        
                        checkDirectoryAndSave(fileEntry);
                    });
                }
            });
        }
    }

    //response is asynchronous, so we should always send a return value
    return true;
});

//when resizing the app frame, we have to manually resize the app itself
function updateWebviews() {
    var webview = document.querySelector("webview");
    webview.style.height = document.documentElement.clientHeight + "px";
    webview.style.width = document.documentElement.clientWidth + "px";
};

//register resizing events
onload = updateWebviews;
window.onresize = updateWebviews;
