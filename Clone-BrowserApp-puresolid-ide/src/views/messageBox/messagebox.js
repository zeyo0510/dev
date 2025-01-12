var MessageBox = {};
(function (x) {
    var buttonsCnt = null;
    var mBox = null;
    var modalCnt = null;
    function  btnGen(btnVal,callback) {

        var btnList = btnVal.split('_');
        for (var i = btnList.length-1; i >=0 ; i--) {
            var btn = document.createElement('button');
            btn.textContent = btnList[i];
            btn.classList.add('button');
            btn.classList.add('mb-btn');
            btn.addEventListener('click', function () {
              
                if(callback)
                    callback(this.innerHTML);

                modalCnt.classList.add('flip-out');
                setTimeout(function () { modalCnt.parentElement.removeChild(modalCnt); }, 301);
               
            });
            buttonsCnt.appendChild(btn);
        }
    }
    function adjustPosition() {
        if (mBox) {
            mBox.style.left = modalCnt.offsetWidth / 2 - mBox.offsetWidth / 2 + 'px';
            mBox.style.top = modalCnt.offsetHeight / 2 - mBox.offsetHeight / 2 + 'px';
        }
    }
    window.addEventListener('resize', function () { adjustPosition(); });
    x.show = function (title, content, button, icon, callback) {
        //Modal container
        modalCnt = document.createElement('div');
        modalCnt.classList.add('modal-back');
        modalCnt.classList.add('mb-modal');
        mBox = document.createElement('div');
         mBox.classList.add('mb');

        //Title container
         var titleCnt = document.createElement('div');
             titleCnt.classList.add('mb-t-cnt');
        //Content container
         var contentCnt = document.createElement('div');
             contentCnt.classList.add('mb-c-cnt');
        //Buttons container
         buttonsCnt = document.createElement('div');
         buttonsCnt.classList.add('mb-b-cnt');
        //Initilize title
         var titleElem = document.createElement('div');
             titleElem.classList.add('mb-title');
         if(title)
         titleElem.textContent = title;
        //Initilize close
         var close = document.createElement('div');
         close.classList.add('mb-close');

         close.addEventListener('click',function () {
             if (callback)
                 callback('Cancel');

             modalCnt.classList.add('flip-out');
             setTimeout(function () { modalCnt.parentElement.removeChild(modalCnt); }, 301);
         }, false);
        //Initilize content
         var contentElem = document.createElement('div');
         contentElem.classList.add('mb-content');
         if (content)
         {
        
             content = content.replace(/\r\n\r\n/g, "</p><p>").replace(/\n\n/g, "</p><p>");
             content = content.replace(/\r\n/g, "<br />").replace(/\n/g, "<br />");
             contentElem.innerHTML = content;
         }
        //Initilize icon
         var iconElem = document.createElement('div');
         iconElem.classList.add('mb-icon');
         if (icon)
             iconElem.style.backgroundImage = "url('" + x.ICONS[icon] + "')";
        
             titleCnt.appendChild(titleElem);
             titleCnt.appendChild(close);
             contentCnt.appendChild(iconElem);
             contentCnt.appendChild(contentElem);
             btnGen(button,callback);
             mBox.appendChild(titleCnt);       
             mBox.appendChild(contentCnt);
             mBox.appendChild(buttonsCnt);
             modalCnt.appendChild(mBox);
             modalCnt.style.display = 'none';

             document.body.appendChild(modalCnt);
             modalCnt.classList.add('flip-in');
             modalCnt.style.display = 'block';
        //Set messagebox to center of screen
         
             adjustPosition()
           
    },
 
    x.ICONS = {
        Info:               '/src/views/messageBox/res/info.png',
        Error:              '/src/views/messageBox/res/error.png',
        Warning:            0x000000C,
        Question:           0x000000D,      
    }
})(MessageBox)