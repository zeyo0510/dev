


Core.register('newProject', function (runtime) {

    var newProjectWnd = new ChildWnd();
    var list = null;
    var newTree = null;
    var descLayout = null;
    var _treeListLayout = null;
 
    return {
        init: function () {
          
                                list = new ListView();
                              

                               _treeListLayout = new Layout();
                                _treeListLayout.domElem.style.height = '300px';


                                var layout2 = new Layout();
                                descLayout = new Layout();
                             //   layout2.domElem.style.display = 'none';
                               


                               newTree = new TreeView();
                             
                                newTree.domElem.style.width = '220px';
                                var treeItem1 = new TreeViewItem();

                                _treeListLayout.addItem(newTree);
                                _treeListLayout.addItem(list);
                                _treeListLayout.addItem(descLayout);
                                _treeListLayout.orientation = 'horizontal';
                                treeItem1.title = "Project Templates";
                                
           
                                newTree.addItem(treeItem1);
            //Project name generator
                                var nameText = document.createElement('input');
                                nameText.type = 'text';
                                nameText.classList.add('new-project-text');
                                function generateName(title) {
                                    title = title.replace(/ /g, '') + 1;
                                    nameText.value = title;
                                }
            //Parse project templates

            //Select template
                                function showItems(lang, type) {
                                  
                                    var listItem = null;
                                    list.clear();

                                    //If only language clicked return all templates by this language
                                    if (!type) {
                                        for (var i in templates) {

                                            if ( templates[i].language == lang) {
                                                listItem = new ListViewItem();
                                                listItem.title = templates[i].name;
                                                if (templates[i].hasOwnProperty('image'))
                                                    listItem.image = templates[i].image;
                                                listItem.data = i;
                                                list.addItem(listItem);
                                                listItem.addEventListener('click', function () {
                                                    generateName(list.selectedItem.title);
                                                });
                                            }
                                        }

                                    }
                                        //else return all templates by this language and type
                                    else {
                                        for (var i in templates) {

                                            if (templates[i].type == type && templates[i].language == lang) {
                                                listItem = new ListViewItem();

                                                listItem.title = templates[i].name;
                                                if (templates[i].hasOwnProperty('image'))
                                                    listItem.image = templates[i].image;
                                                listItem.data = i;
                                                list.addItem(listItem);
                                                listItem.addEventListener('click', function () {
                                                    generateName(list.selectedItem.title);
                                                });
                                            }
                                        }
                                    }
                                    generateName(list.selectedItem.title);
                                }

                                for (var item in templates) {
                                
                                    var lang;
                                    var type;
                                    
                                    if (templates[item].hasOwnProperty('language') && templates[item].hasOwnProperty('type') && templates[item].hasOwnProperty('name')) {
                                        var titleMatch=treeItem1.matchTitle(templates[item].language)
                                        if (titleMatch == -1) {
                                            lang = new TreeViewItem();
                                            lang.title = templates[item].language;
                                            treeItem1.addItem(lang);
                                            lang.addEventListener('click', function (x) {
                                                return function () {showItems(x.title) };
                                            }(lang));
                                        }
                                        else {
                                            lang = treeItem1.items[titleMatch];
                                        }
                                      
                                       

                                        titleMatch = lang.matchTitle(templates[item].type);

                                        if (titleMatch == -1) {
                                            type = new TreeViewItem();
                                            type.title = templates[item].type;
                                         
                                            lang.addItem(type);
                                            type.addEventListener('click', function (x,y) {
                                                return function () { showItems(y.title,x.title) };
                                            }(type, lang));
                                        }
                                        else {
                                            type = lang.items[titleMatch];
                                        }

                                      
                                     
                                      

                                    }
                                    else {
                                        throw new Error('ParseError: Cannot parse project template');
                                        continue;
                                    }

                                 }                           
           
                                treeItem1.toggleExpand();
                                treeItem1.items[0].toggleExpand();
                                treeItem1.items[0].domElem.click();
                                descLayout.domElem.style.backgroundColor = "#2d2d30";
          
                            
                                descLayout.domElem.style.width = '240px';






                                descLayout.domElem.style.height = _treeListLayout.domElem.style.height;
                               
                                newTree.domElem.style.backgroundColor = '#2d2d30';

                             
                               


                   
                                layout2.domElem.style.backgroundColor ='#252526';

                                
                                layout2.orientation = 'vertical';


                                newProjectWnd.addItem(_treeListLayout);
                                newProjectWnd.addItem(layout2);

                                var layout4 = new Layout();
                            //    layout4.domElem.style.backgroundColor = 'red';
                               
                                var layout5 = new Layout();
                              
                                layout5.domElem.style.width = '233px';
                             //   layout5.domElem.style.backgroundColor = 'green';
                            
                                layout2.addItem(layout4);
                                layout2.addItem(layout5);
                           
          
        
            //Name
                                var nameLayout = new Layout();
                               
                               
                                nameLayout.domElem.style.height = '27px';
                                layout4.addItem(nameLayout);
                                nameLayout.domElem.style.paddingLeft = '10px';
                                nameLayout.domElem.style.paddingTop = '5px';
                             
                                var nameLabel = document.createElement('div');
                                nameLabel.innerHTML = 'Name:';
                                nameLabel.classList.add('new-project-label');
                               nameLayout.domElem.appendChild(nameLabel);
                               nameLabel.classList.add('newpejLabel');

                              
                               nameLayout.domElem.appendChild(nameText);
            //Location
                               var locationLayout = new Layout();
                              

                               locationLayout.domElem.style.height = '27px';
                               layout4.addItem(locationLayout);
                               locationLayout.domElem.style.paddingLeft = '10px';
                               locationLayout.domElem.style.paddingTop = '5px';

                               var locationLabel = document.createElement('div');
                               locationLabel.innerHTML = 'Location:';
                               locationLabel.classList.add('new-project-label');
                               locationLayout.domElem.appendChild(locationLabel);
                               locationLabel.classList.add('newpejLabel');

                               var locationText = document.createElement('input');
                               locationText.type = 'text';
                               var rootEntry = null;
            //Trying to restore previusly saved project location
                              IO.getFromLocal(function (obj) {
                                  if (obj.folderID) {
                                      
                                       chrome.fileSystem.isRestorable(obj.folderID, function (isRestore) {
                                           if (isRestore) {
                                               chrome.fileSystem.restoreEntry(obj.folderID, function (dirEntry) {
                                                   rootEntry = dirEntry;
                                                   chrome.fileSystem.getDisplayPath(dirEntry, function (dirName) {

                                                       locationText.value = dirName;

                                                   });
                                               });
                                           }
                                           else {
                                               MessageBox.show('Pursolid', 'Cannot restore location', "OK", "Error");
                                             
                                               locationText.value == '[Select Location]';
                                           }
                                       });
                                    
                                   }
                                   else {
                                       locationText.value == '[Select Location]';
                                    
                                   }
                               });
                            

                          
                                   
                               locationText.readOnly = true;
                               locationText.style.marginLeft = '16px';
                               locationText.classList.add('new-project-text');
                               locationLayout.domElem.appendChild(locationText);

            //browse;
                          
                               var browseBtn = new Button();
                               browseBtn.domElem.style.marginLeft = '10px';
                               browseBtn.domElem.style.marginTop = '-1px';
                               browseBtn.title = 'Choose';
                               locationLayout.addItem(browseBtn);
                               browseBtn.addEventListener('click', function () {
                                   
                                   chrome.fileSystem.chooseEntry({ type: "openDirectory" }, function (dirEntry) {
                                       runtime.descriptor.rootEntry = dirEntry;
                                       rootEntry = runtime.descriptor.rootEntry;
                                       var folderId = chrome.fileSystem.retainEntry(dirEntry);
                                   
                                       IO.setToLocal({ 'folderID': folderId }, function () {


                                           chrome.fileSystem.getDisplayPath(dirEntry, function (dirName) {

                                               locationText.value = dirName;
                                        
                                           });
                                       });
                            
                                   })
                               });

            //ok cancel
                               var okCancelLayout = new Layout();
                              
                               var okButton = new Button();
                               okButton.domElem.style.marginRight = '6px';
                               okButton.title = "OK";
                               var cancelButton = new Button();
                               cancelButton.title = "Cancel";
                               okCancelLayout.addItem(okButton);
                               okCancelLayout.addItem(cancelButton);
                             
                               newProjectWnd.addItem(okCancelLayout);
                               cancelButton.addEventListener('click', function () {
                                   newProjectWnd.close(true);
                               });
                               okButton.addEventListener('click', function () {
                                  
                                   if (!rootEntry) {
                                       MessageBox.show('Pursolid', 'Please select project location', "OK", "Error");
                                       return;
                                   }
                               
                                   IO.exist(nameText.value.replace(/ /g, ''), rootEntry, function (e) {
                                    if (e)
                                        MessageBox.show('Pursolid', 'The project already exist', "OK", "Error");
                                       else {
                                        newProjectWnd.close(true);
                                        runtime.descriptor.rootEntry = rootEntry;
                                               runtime.createNewProject(list.selectedItem.data, nameText.value);
                                      }
                                    });
                                 
                                  
                               });
                               okCancelLayout.domElem.classList.add('okCancelLayout');
                              generateName(list.selectedItem.title);
        },
        show: function () {

            newProjectWnd.title = "New Project";
            newProjectWnd.borderStyle = ChildWnd.borderStyle.normal;
         
            document.body.appendChild(newProjectWnd.domElem);

            newProjectWnd.width = 790;
            newProjectWnd.height = 455;
            newProjectWnd.showDialog(true);
         

            //Size updates
            list.domElem.style.width = _treeListLayout.domElem.offsetWidth - newTree.domElem.offsetWidth - descLayout.domElem.offsetWidth + 'px';
            newTree.domElem.style.height = _treeListLayout.domElem.style.height;
        }



    }
});
