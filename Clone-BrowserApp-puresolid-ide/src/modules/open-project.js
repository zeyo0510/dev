
Core.register('openProject', function (runtime) {

    var openProjectWnd;
    var list = new ListView();
    return {
        init: function () {
            openProjectWnd = new ChildWnd();
            


           var layout1 = new Layout();
            var layout2 = new Layout();
         



       
         

           
            //Project name generator
            var nameText = document.createElement('input');
            nameText.type = 'text';
            nameText.classList.add('new-project-text');
            function generateName(title) {
                title = title.replace(/ /g, '') + 1;
                nameText.value = title;
            }
  


          
         

           


        
            layout1.addItem(list);
    
            layout2.domElem.style.backgroundColor = '#252526';

            layout2.domElem.style.height = '100px';
         


            openProjectWnd.addItem(layout1);
            openProjectWnd.addItem(layout2);

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
            locationText.value = 'Local Storage';
            locationText.readOnly = true;
            locationText.style.marginLeft = '16px';
            locationText.classList.add('new-project-text');
            locationLayout.domElem.appendChild(locationText);

            //browse;
            var browseLayout = new Layout();
            layout5.addItem(browseLayout);
            var browseBtn = new Button();
            browseLayout.domElem.style.marginTop = '37px';
            browseLayout.domElem.style.marginLeft = '5px';
            browseBtn.title = 'Change...';
            browseLayout.addItem(browseBtn);

            //open cancel
            var openCancelLayout = new Layout();
            openCancelLayout.orientation = 'vertical';
            var openButton = new Button();
            openButton.domElem.style.marginRight = '6px';
            openButton.title = "Open";
            var cancelButton = new Button();
            cancelButton.title = "Cancel";
            openCancelLayout.addItem(openButton);
            openCancelLayout.addItem(cancelButton);
            layout5.addItem(openCancelLayout);
            cancelButton.addEventListener('click', function () {
                openProjectWnd.close();
            });
            openButton.addEventListener('click', function () {

                //  var fs = new HTML5FileSystem();
                // fs.init(function () {
                //   fs.exist(nameText.value.replace(/ /g, ''), function (e) {
                //        if (e)
                //         alert('Project already exist')
                //   else {
                openProjectWnd.close();
               
                    runtime.openExistingProject(list.selectedItem.title);
          
                //   }
                // });
                // });

            });
            openCancelLayout.domElem.classList.add('openCancelLayout');
       
        
        },
        
        show: function () {
            {
                list.clear();
                var fs = new HTML5FileSystem();
                fs.init(function () {

                    fs.getEntries(null, function (result) {
                        for (var i = 0; i < result.length; i++) {
                            var listItem = new ListViewItem();
                            listItem.title = result[i].name;
                              
                            list.addItem(listItem);

                        }
                        openProjectWnd.title = "Open Project";
                        openProjectWnd.borderStyle = ChildWnd.borderStyle.normal;
                        openProjectWnd.width = 790;
                        openProjectWnd.height = 500;
                        document.body.appendChild(openProjectWnd.domElem);
                        openProjectWnd.showDialog();

                    });
                });
            }
           

        }



    }
});
