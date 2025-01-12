


Core.register('settings', function (runtime) {

    var newFileWnd = new ChildWnd();
    var colorDD = null;
   
    return {
        init: function () {

          


            var layout1 = new Layout();
            var layout2 = new Layout();
          
            layout1.domElem.style.padding = '5px';
         
       
            layout1.domElem.style.height = "150px";
            layout1.domElem.style.overflow = "auto";
            layout1.domElem.style.backgroundColor = '#2d2d30';
            layout2.domElem.style.backgroundColor = '#252526';

            //Legend
            var fieldset = document.createElement('fieldset');
        
            var legend = document.createElement('legend');
            fieldset.appendChild(legend);
            legend.innerHTML = 'Apperence';
            layout1.domElem.appendChild(fieldset);
           
            layout2.orientation = 'vertical';


            layout1.orientation = 'vertical';
            newFileWnd.addItem(layout1);
            newFileWnd.addItem(layout2);

            var layout4 = new Layout();
            //    layout4.domElem.style.backgroundColor = 'red';
            layout4.domElem.style.flex = '1';
            var layout5 = new Layout();
          
           
            //Theme picker dropdown
             colorDD = document.createElement('select');
            colorDD.classList.add('dropdown');
            var optionDark = document.createElement('option');
            optionDark.value = 'Dark'; optionDark.textContent = 'Dark';
            var optionLight = document.createElement('option');
            optionLight.value = 'Light'; optionLight.textContent = 'Light';
            colorDD.appendChild(optionDark);
            colorDD.appendChild(optionLight);
            var nameLayout = new Layout();


            nameLayout.domElem.style.height = '27px';
            fieldset.appendChild(nameLayout.domElem);
            nameLayout.domElem.style.paddingLeft = '10px';
            nameLayout.domElem.style.height ='110px';
            nameLayout.domElem.style.paddingTop = '5px';

            var nameLabel = document.createElement('div');
          
            nameLabel.innerHTML = 'Document Color: ';
        
            nameLabel.classList.add('new-project-label');
            nameLabel.style.float = 'none';
            nameLayout.domElem.appendChild(nameLabel);
           
            nameLabel.classList.add('newpejLabel');
            nameLayout.domElem.appendChild(colorDD);
          


            //   layout5.domElem.style.backgroundColor = 'green';

            layout2.addItem(layout4);
            layout2.addItem(layout5);




        

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
                newFileWnd.close(true);
                runtime.setTheme(colorDD.children[colorDD.selectedIndex].value);

            });
        
           
        },
        show: function (target) {
            IO.getFromLocal(function (data) {
                if(data.theme){
                    for (var i = 0; i < colorDD.children.length; i++) {
                        if (colorDD.children[i].value == data.theme)
                            colorDD.selectedIndex = i;
                }
                }
            }
            );
            newFileWnd.title = "Settings";
            newFileWnd.borderStyle = ChildWnd.borderStyle.normal;
            newFileWnd.width =400;
            document.body.appendChild(newFileWnd.domElem);
            newFileWnd.showDialog(true);
            newFileWnd.height = 240;
          
           
          
        }



    }
});
