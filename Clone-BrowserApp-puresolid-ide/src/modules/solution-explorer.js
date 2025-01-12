



Core.register('solutionExplorer', function (runtime) {

    var solutionExplorer = new ChildWnd();
    var tabView = new TabView();
    var projectTabItem = new TabItem();

    var commentsTabItem = new TabItem();
    var tree = new TreeView();
    var folderMenu = new ContextMenu();
    
    return {
        init: function () {
            //init tabs
            tabView.domElem.classList.add('se-tabview-cnt');
            projectTabItem.title = 'Project';

            commentsTabItem.title = 'Collaborate';
            
            tabView.addItem(projectTabItem);
            tabView.addItem(commentsTabItem);
            commentsTabItem.addItem('<div style="text-align:center;color:#888888;margin-top:20px;">Under Development</div>');
            tabView.setActiveTab(0);
            solutionExplorer.domElem.id = 'solution-explorer';
            solutionExplorer.title = 'Project Explorer';
            solutionExplorer.addItem(tabView);
            solutionExplorer.borderStyle = 2;
            projectTabItem.addItem(tree);
            this.show();
          

            //init contextMenu
            var addFolderMenuItem = new ContextMenuItem();
            addFolderMenuItem.title = "Add Folder";
            addFolderMenuItem.image='/res/add_folder.png'
            var addFileMenuItem = new ContextMenuItem();
            addFileMenuItem.title = "Add File";
            addFileMenuItem.image='/res/add_file.png'
            var openFileMenuItem = new ContextMenuItem();
            openFileMenuItem.title = "Open File";
            openFileMenuItem.separator = true;
            var cutMenuItem = new ContextMenuItem();
            cutMenuItem.title = "Cut";
            cutMenuItem.image = '/res/cut.png';
            var copyMenuItem = new ContextMenuItem();
            copyMenuItem.title = "Copy";
            copyMenuItem.image = "/res/copy.png";
            var pasteMenuItem = new ContextMenuItem();
            pasteMenuItem.title = "Paste";
            pasteMenuItem.image = "/res/paste.png";
            var deleteMenuItem = new ContextMenuItem();
            deleteMenuItem.title = "Delete";
            deleteMenuItem.image = "/res/delete.png";
            var renameMenuItem = new ContextMenuItem();
            renameMenuItem.title = "Rename";
            renameMenuItem.image = "/res/rename.png";
            folderMenu.addItem(addFolderMenuItem);
            folderMenu.addItem(addFileMenuItem);
            folderMenu.addItem(openFileMenuItem);
            addFileMenuItem.separator = true;
            folderMenu.addItem(cutMenuItem);
            folderMenu.addItem(copyMenuItem);
            folderMenu.addItem(pasteMenuItem);
            folderMenu.addItem(deleteMenuItem);            
            folderMenu.addItem(renameMenuItem);
            //Adding some behavior
            folderMenu.onOpen = function () {
                cutMenuItem.domElem.style.display = 'block';
                copyMenuItem.domElem.style.display = 'block';
                renameMenuItem.domElem.style.display = 'block';
                deleteMenuItem.domElem.style.display = 'block';
                if (folderMenu.target.href instanceof Folder) {
                    addFolderMenuItem.domElem.style.display = 'block';
                    addFileMenuItem.domElem.style.display = 'block';
                    openFileMenuItem.domElem.style.display = 'none';
                    pasteMenuItem.domElem.style.display = 'block';

                    if (folderMenu.target.parent instanceof TreeView) {
                        cutMenuItem.domElem.style.display = 'none';
                        copyMenuItem.domElem.style.display = 'none';
                        renameMenuItem.domElem.style.display = 'none';
                        deleteMenuItem.domElem.style.display = 'none';
                    };
                }
                else if (folderMenu.target.href instanceof File) {
                    addFolderMenuItem.domElem.style.display = 'none';
                    addFileMenuItem.domElem.style.display = 'none';
                    pasteMenuItem.domElem.style.display = 'none';
                    openFileMenuItem.domElem.style.display = 'block';

       
                    
                }
            }
             //Register contextMenu events
            //add file 
            addFileMenuItem.addEventListener('mouseup', function (e) {

                runtime.newFile(addFileMenuItem.parent.target);
            }, false);
            //add folder
            addFolderMenuItem.addEventListener('mouseup', function (e) {

                runtime.newFolder(addFolderMenuItem.parent.target, 'New Folder');
            }, false);
            //cut
            cutMenuItem.addEventListener('mouseup', function (e) {

                IO.clipboard.object = copyMenuItem.parent.target;
                IO.clipboard.command = "cut";
            }, false);
            //copy
            copyMenuItem.addEventListener('mouseup', function (e) {
            
                IO.clipboard.object = copyMenuItem.parent.target;
                IO.clipboard.command = "copy";
            }, false);
            //paste
            var se = this;

            pasteMenuItem.addEventListener('mouseup', function (e) {
                var ind=(IO.clipboard.object.href instanceof File)?"file":"folder";
                if (pasteMenuItem.parent.target.isNodeOf(IO.clipboard.object))
                {
                    MessageBox.show('Pursolid', 'The destination folder is a subfolder of the source folder', "OK", "Error");
                    return;
                }
                IO.exist(IO.clipboard.object.title, pasteMenuItem.parent.target.href.entry, function (e) {
                    if (e) {
                        MessageBox.show('Pursolid', 'The folder already exist', "OK", "Error");
                        return;
                    }
                if (IO.clipboard.command == "copy") {
                    IO.copy(IO.clipboard.object.href.entry, pasteMenuItem.parent.target.href.entry, function (folder) {


                      se.copy(IO.clipboard.object,pasteMenuItem.parent.target,folder);
                        


                    });
                }
                else if (IO.clipboard.command == "cut") {
                    IO.cut(IO.clipboard.object.href.entry, pasteMenuItem.parent.target.href.entry, function (folder) {

                        se.copy(IO.clipboard.object, pasteMenuItem.parent.target, folder);
                        IO.clipboard.object.remove();
                        IO.clipboard.object = null;

                    });
                }

                }, ind);
            }, false);
            //delete
            deleteMenuItem.addEventListener('mouseup', function (e) {
                IO.remove(deleteMenuItem.parent.target.href.entry, function () {
                    deleteMenuItem.parent.target.remove();
                    deleteMenuItem.parent.target = null;
                });

            }, false);
            //open
            openFileMenuItem.addEventListener('mouseup',function(e){
                runtime.showFile(openFileMenuItem.parent.target.href);
            });
            //rename
            renameMenuItem.addEventListener('mouseup', function (e) {
                var oldName = renameMenuItem.parent.target.title;

             
                renameMenuItem.parent.target.edit(function editProcess(result) {
                    if (result == oldName) return;
                    IO.exist(result, renameMenuItem.parent.target.parent.href.entry, function (exist) {
                        if (exist) {
                            renameMenuItem.parent.target.parent.select();
                            MessageBox.show('Pursolid', 'The file already exist', "OK", "Error", function (result) {
                                if (result == "OK") {

                                    renameMenuItem.parent.target.edit(editProcess);

                                    renameMenuItem.parent.target.select();

                                }
                            });
                        }
                        else {

                      
                    IO.rename(renameMenuItem.parent.target.parent.href.entry, renameMenuItem.parent.target.href.entry, result, function (folder) {

                    se.copy(renameMenuItem.parent.target, renameMenuItem.parent.target.parent, folder);
                    renameMenuItem.parent.target.remove();
                    renameMenuItem.parent.target = null;

                    });
                        }
                    },'all');
                }, false);
                renameMenuItem.parent.target.select();
            });

        },
        show: function () {
            document.querySelector('#solution-explorer-cnt').appendChild(solutionExplorer.domElem);
            solutionExplorer.show();


        },
        addItem: function (parent, child, editable) {
            var childItem = new TreeViewItem();
            childItem.title = child.name;
            childItem.href = child;
            childItem.contextMenu = folderMenu;
            if (parent != null) {

                if (child instanceof Folder) {

                    childItem.image = '../../src/templates/res/folder-cl.png';
                    childItem.onOpen = function () {
                        childItem.image = '../../src/templates/res/folder-op.png';
                    };
                    childItem.onClose = function () {
                        childItem.image = '../../src/templates/res/folder-cl.png';
                    };
                  
                } else {
                    if (MimeType.fromExtenstion(child.extension) && !MimeType.isImage(child.extension))
                    {
                       
                        childItem.image = '../../src/templates/res/' + child.extension + '-big.png';
                    }
                    else if (MimeType.isImage(child.extension)) {
                        childItem.image = '../../src/templates/res/image-big.png';
                    }
                    else {
                        childItem.image = '../../src/templates/res/file-big.png';
                    }
                   
                  
                }
                childItem.addEventListener('dblclick', function (x) {
                    if (child instanceof File)
                    runtime.showFile(child);
                    else if (child instanceof Folder) {
                        childItem.toggleExpand();
                    }
                });
                parent.addItem(childItem);
            } else {

                if (child.type)
                childItem.image = templates[child.type].image;
                tree.addItem(childItem);

            }
            if (editable) {

                childItem.edit(editProcess);
            }
            function editProcess(name) {
                 

                IO.exist(name, childItem.parent.href.entry, function (e) {
                    if (e) {
                        childItem.parent.select();
                        MessageBox.show('Pursolid', 'The file already exist', "OK", "Error", function (result) {
                            if (result == "OK") {

                                childItem.edit(editProcess);

                                childItem.select();

                            }
                        });
                    }
                    else {


                        IO.createFolder(name, childItem.parent.href.entry, function (folder) {
                            childItem.href = folder;
                            childItem.parent.href.addFolder(childItem.href);
                        });
                    }
                });

            }
            return childItem;


        },
        clearItems: function () {
            tree.clear();
        },
        updateLayout: function () {
            solutionExplorer.height = solutionExplorer.domElem.parentElement.offsetHeight;
            tabView.height = solutionExplorer.domElem.parentElement.offsetHeight;
        },
        copy: function (srcItem, destItem,folder) {     
        var self = this;
        function cloneRecursive(s,n) {
            s.forEach(function (i) {
        
                if (i.href instanceof Folder) {
                    IO.getEntry(n.href.phisicalPath + '/' + i.title, runtime.descriptor.rootEntry, "Folder", function (result) {
                     
                        cloneRecursive(i.items,  self.addItem(n, result,false));
                    });
                }
                else {
                    IO.getEntry(n.href.phisicalPath + '/' + i.title, runtime.descriptor.rootEntry, "File", function (result) {
                     
                        cloneRecursive(i.items,    self.addItem(n, result, false));
                    });
                }
              
            
            });
        }      
        cloneRecursive(srcItem.items,    this.addItem(destItem, folder));      
       
        }
    };
});
