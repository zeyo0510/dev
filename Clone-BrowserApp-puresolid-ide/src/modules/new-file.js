


Core.register('newFile', function (runtime) {

    var newFileWnd = new ChildWnd();
    var node = null;
    var list = new ListView();
    return {
        init: function () {

          


            var layout1 = new Layout();
            var layout2 = new Layout();
          



          
      
            //Project name generator
            var nameText = document.createElement('input');
            nameText.type = 'text';
            nameText.classList.add('new-project-text');
            nameText.style.marginLeft = '10px';
            nameText.style.width = '478px'
            function generateName(title) {
                title = title.replace(/ /g, '') ;
                nameText.value = title;
            }
            //Parse project templates
         
            layout1.addItem(list);
            layout1.domElem.style.height = "330px";
            layout1.domElem.style.overflow = "auto";
            layout1.domElem.style.backgroundColor = '#2d2d30';
            layout2.domElem.style.backgroundColor = '#252526';

          
           
            layout2.orientation = 'vertical';


            layout1.orientation = 'vertical';
            newFileWnd.addItem(layout1);
            newFileWnd.addItem(layout2);

            var layout4 = new Layout();
            //    layout4.domElem.style.backgroundColor = 'red';
            layout4.domElem.style.flex = '1';
            var layout5 = new Layout();
          
           

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
  

            //Items
            var files=[{
                title:'JavaScript File',
                extension:'js',
                description:''
            },{
                title: 'HTML Page',
                extension:'html',
                description:''
            },{
                title: 'Style Sheet',
                extension:'css',
                description:''
            },{
                title: 'JSON Data File',
                extension:'json',
                description:''
            },             {
                 title: 'PHP File',
                 extension: 'php',
                 description: ''
             },
            {
                title: 'Text File',
                extension: 'txt',
                description: ''
            }]
            for (var i = 0; i < files.length; i++) {
                var listItem = new ListViewItem();
                listItem.title = files[i].title + ' (*.' + files[i].extension + ')';

                listItem.image = '/src/templates/res/' + files[i].extension + '-big.png';
                listItem.extension = files[i].extension;
                listItem.name = files[i].title;
                listItem.addEventListener('click', function () {
                    generateName(list.selectedItem.name + '.' + list.selectedItem.extension);
                });
                list.addItem(listItem);
        }
        

            //ok cancel
            var okCancelLayout = new Layout();
            okCancelLayout.domElem.style.float = 'right';
            okCancelLayout.domElem.style.marginRight = '10px';
            okCancelLayout.domElem.style.marginTop = '15px';
            var okButton = new Button();
            okButton.domElem.style.marginRight = '6px';

            okButton.title = "OK";
            var cancelButton = new Button();
            cancelButton.title = "Cancel";
            okCancelLayout.addItem(okButton);
            okCancelLayout.addItem(cancelButton);
            layout5.addItem(okCancelLayout);
         
            cancelButton.addEventListener('click', function () {
                newFileWnd.close(true);
            });
            okButton.addEventListener('click', function () {
             

                IO.exist(nameText.value.replace(/ /g, ''),node.href.entry, function (e) {
                    if (e)
                        MessageBox.show('Pursolid', 'File already exist', "OK", "Error");
                    else {
                        
                        runtime.addFile(node, nameText.value,list.selectedItem.extension);
                        newFileWnd.close(true);
                    }
                },'file');


            });
         //   okCancelLayout.domElem.classList.add('okCancelLayout');
            generateName(list.selectedItem.name + '.' + list.selectedItem.extension);
           
        },
        show: function (target) {
         
            newFileWnd.title = "Add New File";
            newFileWnd.borderStyle = ChildWnd.borderStyle.normal;
            newFileWnd.width = 555;
            newFileWnd.height = 440;
            document.body.appendChild(newFileWnd.domElem);
            newFileWnd.showDialog(true);
          
          
           
            node = target;
        }



    }
});
