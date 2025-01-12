var ContextMenuItem = function () {

   
    //constructor
    function ContextMenuItem() {

        ///public members
        this.items = [];
        this.domElem = null;
        this.parent = null;

     
            this.create();
       

        //properties
        Object.defineProperty(this, 'title', {
            set: function (value) { this.domElem.children[1].innerHTML = value },
            get: function () { return this.domElem.children[1].innerHTML },
            configurable: false
        });
        Object.defineProperty(this, 'separator', {
            set: function (value) {
                if (value == true)
                    this.domElem.classList.add('separator');
                else {
                    this.domElem.classList.remove('separator');
                }
            },
            configurable: false
        });
        Object.defineProperty(this, 'isRootItem', {
            get: function () {
                return this.parent instanceof Menu;
            },
            configurable: false
        });
        Object.defineProperty(this, 'image', {
            set: function (value) {
             
               
             
                this.domElem.image.style.backgroundImage = "url('" + value + "')";
                _imagePath = value;
            },
            get: function () {
                return _imagePath;
            },
            configurable: false
        });




    }

    ContextMenuItem.prototype.create = function () {
        //create node
        this.domElem = document.createElement('li');

        //add image
        this.domElem.image = document.createElement('div');
        this.domElem.image.classList.add('contextMenu-item-image');
        this.domElem.appendChild(this.domElem.image);


        //add title
        var title = document.createElement('span');
       
        title.classList.add('contextMenu-item-title');

        this.domElem.appendChild(title);
        //add arrow
        this.domElem.arrow = document.createElement('div');
        this.domElem.appendChild(this.domElem.arrow);

       
    }
  
    ContextMenuItem.prototype.addItem = function (item) {

        if (this.items.length == 0) {
            var list = document.createElement('ul');
            this.domElem.appendChild(list);
        }
        this.domElem.children[3].appendChild(item.domElem);
        item.parent = this;
        this.items.push(item);

        if (!this.isRootItem && this.items.length > 0) {
            this.domElem.children[2].classList.add('contextMenu-item-arrow');

        }

        item.enableBehavior();
    }
    ContextMenuItem.prototype.addItems = function () {
        for (var i = 0; i < arguments.length; i++)
            this.addItem(arguments[i]);
    }

    ContextMenuItem.prototype.setImage = function (url) {
        this.domElem.image.style.backgroundImage = "url('" + url + "')";

    }
    ContextMenuItem.prototype.addEventListener = function(event, handler) {
        var obj = {};
        obj.target = this;
        this.domElem.addEventListener(event, function() {
            handler(obj);
        }, false);
    };
    ContextMenuItem.prototype.enableBehavior = function () {
        var self = this;
        this.domElem.addEventListener('mousedown', function (e) { e.cancelBubble = true; }, false);
        this.domElem.addEventListener('mouseup', function (e) { self.parent.domElem.style.display = 'none'; }, false)
    }

    return ContextMenuItem;
}();