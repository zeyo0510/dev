
var ChildWnd=function() {

    ChildWnd.borderStyle = {
       normal: 0,
       tool:1,
       none:2
    }
    
function ChildWnd() {

    this.elements = [];
    this.domElem = null;
  
    this.create();
    


    Object.defineProperty(this, 'title', {
        set: function (value) { this.domElem.children[0].children[0].innerHTML = value },
        get: function () { return this.domElem.children[0].children[0].innerHTML },
        configurable: false
    });
    Object.defineProperty(this, 'borderStyle', {
        set: function (value) {
            switch (value) {
                case ChildWnd.borderStyle.normal:
                    this.domElem.classList.remove('tool-wnd');
                    this.domElem.classList.add('normal-wnd'); break;
                case ChildWnd.borderStyle.tool:
                    this.domElem.classList.remove('normal-wnd');
                    this.domElem.classList.add('tool-wnd');
                    break;
                case ChildWnd.borderStyle.none:
                    this.domElem.classList.remove('normal-wnd');
                    this.domElem.classList.add('tool-wnd');
                    this.domElem.header.style.display = 'none';
                    break;
            }
        },
        get: function () {

        },
        configurable: false
    });
    Object.defineProperty(this, 'width', {
        set: function (value) { this.domElem.style.width = value+'px' },
        get: function () { return this.domElem.style.width },
        configurable: false
    });
    Object.defineProperty(this, 'height', {
        set: function (value) {
            var offset = 30;
            if (this.domElem.classList.contains('tool-wnd')) {
                offset = 0;
            }
            this.domElem.style.height = value + 'px';

            this.domElem.children[1].style.height = value - offset + 'px';
         
        },
        get: function () { return this.domElem.style.height },
        configurable: false
    });

    this.borderStyle = ChildWnd.borderStyle.tool;
}

ChildWnd.prototype.create = function () {
    var self = this;
    this.domElem = document.createElement('div');
    this.domElem.classList.add('wnd');

    var header = document.createElement('div');
    header.classList.add('wnd-header');
    this.domElem.header = header;
    var title = document.createElement('div');
    title.classList.add('wnd-title');
    header.appendChild(title);

    var btn = document.createElement('div');
   
    btn.classList.add('wnd-btn');

    var domElem = this.domElem;
    btn.addEventListener('click', function () {
        self.close(true);
    }, false);
    header.appendChild(btn);

    this.domElem.appendChild(header);

    var cnt = document.createElement('div');
    cnt.classList.add('wnd-cnt');
  
    this.domElem.appendChild(cnt);
 
    //moving
    var domElem = this.domElem;
    var initY = null;
    var initX = null;
    var selector = document.getElementById('toolbox_cnt');
    var thead = document.getElementById('toolbox-header');
   // header.addEventListener('mousedown', hMouseDown, false);
    function hMouseDown(e) {

        var pageX = e.pageX - domElem.parentNode.offsetLeft;
        var pageY = e.pageY - domElem.parentNode.offsetTop;

        initX = pageX - parseInt(domElem.offsetTop);

        initY = pageY - parseInt(domElem.offsetLeft);

        header.addEventListener('mousemove', hMouseMove, false);
        domElem.parentNode.addEventListener('mousemove', hMouseMove, false);
        header.addEventListener('mouseup', hMouseUp, false);
        domElem.parentNode.addEventListener('mouseup', hMouseUp, false);


    }
    function hMouseMove(e) {


        var x = e.pageX - domElem.parentNode.offsetLeft;
        var y = e.pageY - domElem.parentNode.offsetTop;

        //if (y <= 153) {
        //    y = 153;
        //}

        //if (x <= 65) {
        //    x = 65;
        //}

        domElem.style.marginLeft = (x - initX) + 'px';
       
        domElem.style.marginTop = (y - initY) + 'px';


    }
    function hMouseUp() {
        header.removeEventListener('mousemove', hMouseMove, false);
        domElem.parentNode.removeEventListener('mousemove', hMouseMove, false);
    }
    }
ChildWnd.prototype.addItem = function (elem) {
   
        this.domElem.children[1].appendChild(elem.domElem);
   
  
    this.elements.push(elem);
}
ChildWnd.prototype.show = function (elem) {
  
    this.domElem.style.display = 'block';
 
}
ChildWnd.prototype.showDialog = function (animate) {
    if (this.domElem.parentNode != null) {
       this.domElem.background = document.createElement('div');
       this.domElem.background.classList.add('modal-back');
        var parent = this.domElem.parentElement;

        this.domElem.parentElement.removeChild(this.domElem);
  
  
        parent.appendChild(this.domElem.background);
       
       
        this.domElem.background.appendChild(this.domElem);
       
       
        if (animate) {
            this.domElem.classList.add('flip-in');
        }
        this.domElem.style.display = 'block';
        var left = (document.body.offsetWidth / 2) - (this.domElem.offsetWidth / 2);
        var top = (document.body.offsetHeight / 2) - (this.domElem.offsetHeight / 2);
        this.domElem.style.marginTop = top + 'px';
        this.domElem.style.marginLeft = left + 'px';

    }
}

//ChildWnd.prototype.hide = function (animate) {
   
//    if (animate) {
        
//        this.domElem.classList.add('flip-out');
//    } else {
//        this.domElem.style.display = 'none';
//    }
//}
ChildWnd.prototype.close = function (animate) {
    var self = this;
    if (animate) {
        this.domElem.classList.add('flip-out');
        setTimeout(function () { close.call(self);}, 301);
    }
    else {
        close();
    }
    function close() {
        this.domElem.classList.remove('flip-in');
        this.domElem.classList.remove('flip-out');
        if (this.domElem.background) {
            var parent = this.domElem.background.parentElement;
            parent.removeChild(this.domElem.background);

        }
        else {
            var parent = this.domElem.parentElement;
            parent.removeChild(this.domElem);
        }
    }
}
return ChildWnd;
}();