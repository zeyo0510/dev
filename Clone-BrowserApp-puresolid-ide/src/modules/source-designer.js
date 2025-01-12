/// <reference path="../views/tabControl/tabControl.js" />
/// <reference path="../views/tabControl/tabItem.js" />
/// <reference path="../project/fileSystem.js" />


Core.register('sourceDesigner', function (runtime) {
    var tabView = new TabView();
    var openedFiles = [];
    var cnt = null;
    var editors = [];
    var toolbox = null;
    function saveFile() {
        document.body.style.cursor = 'wait';
        saveToolbarItem.domElem.style.cursor = 'wait';
        runtime.saveFile(tabView.activeTab.href, tabView.activeTab.editedContent, function () {
            delete tabView.activeTab.editedContent;
            setTimeout(function () { document.body.style.cursor = ''; saveToolbarItem.domElem.style.cursor = ''; }, 100);
            // tabItem.cancelClose = false;
        });

    }
    function fileExist(file) {
        var exist = false;
        for (var i = 0; i < openedFiles.length; i++) {
          
            if (openedFiles[i].href.phisicalPath == file.phisicalPath) {
                exist = true;
                break;
            }
                      
        }
        return exist;
    }
    return {
        init: function () {

            cnt = document.getElementById('code-editor');
            var span = document.createElement('span');
            
            span.classList.add('code_desc');
            span.innerHTML = 'Open new project or drag and drop files here';
            cnt.appendChild(span);
            //Toolbar
            toolbox = new Toolbar();
            toolbox.domElem.style.float = 'right';
           
          //Undo
            undoToolbarItem = new ToolbarItem();                       
            undoToolbarItem.image = '/res/undo.png';
            undoToolbarItem.mini = true;
            undoToolbarItem.domElem.title = 'Undo';
            toolbox.addItem(undoToolbarItem);
          //Rendo
            redoToolbarItem = new ToolbarItem();          
            redoToolbarItem.image = '/res/rendo.png';
            redoToolbarItem.mini = true;
            redoToolbarItem.domElem.title = 'Redo';
            toolbox.addItem(redoToolbarItem);
         //Save
            saveToolbarItem = new ToolbarItem();
            saveToolbarItem.image = '/res/save.png';
            saveToolbarItem.mini = true
            saveToolbarItem.domElem.title = 'Save';
            toolbox.addItem(saveToolbarItem);
            //Comment
            comToolbarItem = new ToolbarItem();        
            comToolbarItem.image = '/res/comment.png';
            comToolbarItem.mini = true;
            comToolbarItem.domElem.title = 'Comment';
            toolbox.addItem(comToolbarItem);
           
            //Uncomment
            uncoToolbarItem = new ToolbarItem();
            uncoToolbarItem.image = '/res/uncomment.png';
            uncoToolbarItem.mini = true;
            uncoToolbarItem.domElem.title = 'Uncomment';
            toolbox.addItem(uncoToolbarItem);

            //find
            findToolbarItem = new ToolbarItem();
            findToolbarItem.image = '/res/find.png';
            findToolbarItem.mini = true;
            findToolbarItem.domElem.title = 'Find';
            toolbox.addItem(findToolbarItem);
           
          
            //Format
            formatToolbarItem = new ToolbarItem();
            formatToolbarItem.image = '/res/format.png';
            formatToolbarItem.mini = true;
            formatToolbarItem.domElem.title = 'Format Document';
            toolbox.addItem(formatToolbarItem);


            cnt.appendChild(tabView.domElem);
            //register toolbox events
            function getSelectedRange(ed) {
                return { from: ed.getCursor(true), to: ed.getCursor(false) };
            }
            function selectAll(ed) {
                var end = ed.lineCount() - 1
                return { from: { line: 0, ch: 0 }, to: { line: end, ch: ed.lineInfo(end).text.length-1 } }
            }
            function commentSelection(isComment,ed) {
                var range = getSelectedRange(ed);
                ed.commentRange(isComment, range.from, range.to);
            }
            comToolbarItem.addEventListener('mouseup', function () {
                commentSelection(true, tabView.activeTab.editor);               
            });
            uncoToolbarItem.addEventListener('mouseup', function () {
                commentSelection(false, tabView.activeTab.editor);
            });
            findToolbarItem.addEventListener('mouseup', function () {
                CodeMirror.commands["find"]( tabView.activeTab.editor);
            });
           formatToolbarItem.addEventListener('mouseup', function () {
               var range = getSelectedRange(tabView.activeTab.editor);
               if (range.from.line == range.to.line && range.from.ch == range.to.ch) {
                   range = selectAll(tabView.activeTab.editor);
               }
                   tabView.activeTab.editor.autoFormatRange(range.from, range.to);
              
            });
            saveToolbarItem.addEventListener('mouseup', saveFile);
        
            undoToolbarItem.addEventListener('mouseup', function () {
                tabView.activeTab.editor.undo();
            });
            redoToolbarItem.addEventListener('mouseup', function () {
                tabView.activeTab.editor.redo();
            });
            //Drag and Drop functionality
            var self = this;
       
            cnt.addEventListener("drop", function (e) {

                //Supports only one file
                e.preventDefault();
                var length = e.dataTransfer.items.length;
                var i = 0;
            
           var en= e.dataTransfer.items[0].webkitGetAsEntry();

          
               chrome.fileSystem.getWritableEntry(en, function (entry) {
                  
            
           

                    if (entry.isFile) {
                        

                        var reader = new FileReader();
                        entry.file(function (fileItem) {
                            reader.readAsText(fileItem);

                        });
                        reader.onloadend = function () {
                            var file = new File();
                            file.name = entry.name;
                            file.phisicalPath = entry.fullPath;
                            file.entry = entry;
                            file.content = this.result;

                            self.showFile(file);
                        
                        };
                  

                       
                    } else if (entry.isDirectory) {

                    }
               });

                return false;
            }, false);

         

            cnt.addEventListener("dragover", function (e) {
               
                e.preventDefault();
              
            }, false);

        },
        reset:function(){
            openedFiles = [];
            editors = [];
        },
        getOpenedFiles:function(){
            return openedFiles;
        },
        close: function () {
            this.reset();
            tabView.closeTabs();
        },
        deleteOpenedFile:function(openedFile){
            var index = openedFiles.indexOf(openedFile);
            if (index != -1) {
                delete openedFiles[index].editedContent;

            }
        },
        showFile: function (file) {
            //Check if tab is already opened;
            var editor = null;
            if (!fileExist(file)) {
                var tabItem = new TabItem();
                tabItem.title = file.name;
                tabItem.href = file;
                chrome.fileSystem.getDisplayPath(tabItem.href.entry, function (dp) {
                    tabItem.domElem.tab.setAttribute('title', dp);
                })
               

                tabView.addItem(tabItem);

                tabItem.domElem.tabCnt.addEventListener("dragover", function (e) {
               
                    e.preventDefault();
              
                }, false);

                tabItem.domElem.tabCnt.addEventListener("drop", function (e) {

                    //Supports only one file
                    e.preventDefault();
                  

                    return false;
                }, false);

                if (!toolbox.domElem.parentElement) {

                    tabView.domElem.header.appendChild(toolbox.domElem);
                }
                
                if (MimeType.isImage(file.extension)) {
                    file.entry.file(function (f) {
                        editor = new ImageViewer(tabItem.domElem.tabCnt, f);
                        editor.setSize(cnt.offsetWidth, cnt.offsetHeight-28);
                        editors.push(editor);
                      
                    });
                  

                }
                else {
                   
                 editor = CodeMirror(tabItem.domElem.tabCnt, {
                        value: file.content,
                        lineNumbers: true,
                        mode: MimeType.fromExtenstion(file.extension),
                        matchBrackets: true,
                        autoCloseBrackets: true,
                        styleActiveLine: true,
                        extraKeys: { "Ctrl-Space": "autocomplete" },
                        autoCloseTags: true,
                        matchTags: { bothTags: true }
                    });
               
                 editor.on("dragover", function (editor, e) {
                  
                       if(e.dataTransfer.items[0].kind=='file')
                     e.preventDefault();
                 });

                 editor.on("drop", function (editor, e) {
                   
                      if(e.dataTransfer.items[0].kind=='file')
                     e.preventDefault();
                     return false;
                 });
                 editor.on('keypress', function (cm, event) {
                     if (!(event.which == 115 && event.ctrlKey) && !(event.which == 19)) return true;
                     
                     event.preventDefault();
                     saveFile();
                     return false;
                 });
                editor.on("keyup", function (cm, event) {
             
                    var ch;
                    var code = event.keyCode || event.which;
                   
                    if (code >= 96 && code <= 111) {
                        ch = String.fromCharCode(code-48)
                      
                    }
                    else {
                        ch = String.fromCharCode(code)
                     
                    }
                  
                    var canShow = /^[a-zA-Z]*$/.test(ch)
                    //Exception
                    if (code == 110 || code==190)
                        canShow = true;
                    if (canShow) {
                        switch (cm.doc.modeOption) {
                            case "text/javascript": CodeMirror.showHint(cm, CodeMirror.hint.javascript, { completeSingle: false }); break;
                        
                            case "text/css": CodeMirror.showHint(cm, CodeMirror.hint.css, { completeSingle: false }); break;

                            case "text/html": CodeMirror.showHint(cm, CodeMirror.hint.html, { completeSingle: false }); break;
                        }
                                      
                     
                    }
                });
                tabItem.editor = editor;
               
                this.updateLayout();
              
                //   editor.refresh();
                editor.setSize(cnt.offsetWidth, cnt.offsetHeight - 28);
                editors.push(editor);
                editor.on('change', function (cm, changeObj) {
                    tabItem.editedContent = cm.getValue();
                });
                }
              
             
                openedFiles.push(tabItem);
                tabItem.onClosed = function () {
                    if (tabView.tabs.length == 0) {
                      
                        tabView.domElem.header.removeChild(toolbox.domElem);
                    }
                }
                tabItem.onClose = function () {
                    if ("editedContent" in tabItem){
                        tabItem.cancelClose = true;
                    MessageBox.show('Pursolid', 'Do you want to save changes to this file?', "Yes_No_Cancel", "Error", function (result) {
                        if (result == 'Cancel') { return;}
                        else if (result == "Yes") {
                            runtime.saveFile(tabItem.href, tabItem.editedContent, function () {
                                delete tabItem.editedContent;
                            });
                        }
                        tabItem.cancelClose = false;
                        tabItem.close();

                        var index = openedFiles.indexOf(tabItem);
                        if (index != -1) {
                            openedFiles.splice(index, 1);

                        }
                    });
                       
                

                    }
                    else {
                        var index = openedFiles.indexOf(tabItem);
                        if (index != -1) {
                            openedFiles.splice(index, 1);

                        }
                    }
               
                }



            }
        },
   
        updateLayout: function () {

            tabView.height = cnt.offsetHeight;
            for (i = 0; i < editors.length; i++) {
             
                editors[i].setSize(cnt.offsetWidth, cnt.offsetHeight-28);
                
            }
        }
    }
});