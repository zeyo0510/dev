/// <reference path="treeView.js" />


var TreeViewItem = function () {

    var openMenu;
    
    //constructor
    function TreeViewItem(elem) {

        ///public members
        this.items = [];
        this.domElem = null;
        this.parent = null;
        this.root = null;
        var _imagePath = null;
        if (elem)
            this.bind(elem);
        else {
            this.create();
        }
        this.contextMenu = null;
        //this.enableBehavior();

        //properties
        Object.defineProperty(this, 'title', {
            set: function (value) { this.domElem.children[2].innerHTML = value },
            get: function () { return this.domElem.children[2].innerHTML },
            configurable: false
        });
       
        Object.defineProperty(this, 'isRootItem', {
            get: function () {
                return this.parent instanceof TreeView;
            },
            configurable: false
        });
        this.isOpen = false;

        Object.defineProperty(this, 'image', {
            set: function (value) {
                this.domElem.image.style.display = 'block';
                this.domElem.image.style.backgroundImage = "url('" + value + "')";
                _imagePath = value;
            },
            get:function(){
                return _imagePath;
            },
            configurable: false
        });


    }

    TreeViewItem.prototype.create = function () {
        //create node
        this.domElem = document.createElement('li');
        this.domElem.classList.add('tree-view-item');


        //add arrow
        this.domElem.arrow = document.createElement('div');
       this.domElem.arrow.classList.add('treeview-item-arrow');
        this.domElem.appendChild(this.domElem.arrow);

        //add image
        this.domElem.image = document.createElement('div');
        this.domElem.image.classList.add('treeview-item-image');
        this.domElem.appendChild(this.domElem.image);


        //add title
        var title = document.createElement('span');
        title.classList.add('treeview-item-title');
      
        this.domElem.appendChild(title);
       


    }

    TreeViewItem.prototype.addItem = function (item) {

        if (this.items.length == 0) {
            var list = document.createElement('ul');
            this.domElem.parentNode.insertBefore(list, this.domElem.nextElementSibling);
            this.domElem.list = list;
        }
     
        if (item.href instanceof Folder) {
            this.domElem.nextElementSibling.insertBefore(item.domElem, this.domElem.nextElementSibling.firstChild);
        }
        else {
            this.domElem.nextElementSibling.appendChild(item.domElem);
        }
        item.parent = this;
        item.root = this.root;
        this.items.push(item);

        if (this.items.length > 0) {
            this.domElem.children[0].classList.add('treeview-item-arrow-image');
           
        }
     
        
            item.enableBehavior();
        
    }
   
    TreeViewItem.prototype.addItems = function () {
        for (var i = 0; i < arguments.length; i++)
            this.addItem(arguments[i]);
    }

    TreeViewItem.prototype.addEventListener = function (event, handler) {
      
        this.domElem.addEventListener(event, handler, false);
    }
    TreeViewItem.prototype.matchTitle = function (title) {
        for (var i = 0; i < this.items.length; i++) {
            if (this.items[i].title.toLowerCase() == title.toLowerCase())
                return i;
        }
        return -1;
    }
    TreeViewItem.prototype.hasChild = function (title) {

        for (var i = 0; i < this.items.length; i++) {
            if (this.items[i].title == title)
                return this.items[i];

        }
        return undefined;
    }
    TreeViewItem.prototype.isNodeOf = function (treeViewItem) {
        var self = this;
        var thereIs = false;
        function searchRecursive(treeItem) {
            treeItem.items.forEach(function (item) {
                if (item == self) {
                    thereIs = true;
                   
                }
                else {
                    searchRecursive(item);
                }
            });


        }
        searchRecursive(treeViewItem);
        return thereIs;
        
    }
    TreeViewItem.prototype.remove = function () {
        if (this.parent) {
            if (this.items.length > 0) {

                this.domElem.parentElement.removeChild(this.domElem.nextElementSibling);
            }
            this.domElem.parentElement.removeChild(this.domElem);
            var index = this.parent.items.indexOf(this);
            if (index > -1) {
                console.log('removed');
                this.parent.items.splice(index, 1);
             
            }
            if (this.parent.items.length == 0) {
                console.log('asdasdas');
                this.toggleExpand();
                this.parent.toggleExpand();
                this.parent.domElem.children[0].classList.remove('treeview-item-arrow-image');
             
            }
          
        }
    }
    function selectText(element) {
        var doc = document;
        var text = element;
        if (doc.body.createTextRange) {
            var range = document.body.createTextRange();
            range.moveToElementText(text);
            range.select();
        } else if (window.getSelection) {
            var selection = window.getSelection();
            var range = document.createRange();
            range.selectNodeContents(text);
            selection.removeAllRanges();
            selection.addRange(range);
        }
    }
    TreeViewItem.prototype.edit = function (callback) {

        this.isEditMode = true;
        this.domElem.children[2].setAttribute('spellcheck', 'false');
        var self = this;
        this.domElem.children[2].classList.add('treeview-item-edit');
     
        this.domElem.children[2].setAttribute('contenteditable', 'true');
        this.domElem.children[2].focus();
        
    
        var title = this.domElem.children[2];
      
            document.addEventListener('mousedown', editMouseDown, false);
            document.addEventListener('keydown', editKeyDown, false);
        function submitChanges() {
            document.removeEventListener('mousedown', editMouseDown, false);
            document.removeEventListener('keydown', editKeyDown, false);
            self.isEditMode = false;
            title.removeAttribute('contenteditable');
            title.classList.remove('treeview-item-edit');
            document.removeEventListener('mousedown', editMouseDown, false);
            if (callback) {
                callback(title.innerHTML);
                self.select();
            }
        }
        function editMouseDown(e) {
            if (e.target != title) {
                submitChanges();
            }
        }
        function editKeyDown(e) {
            if (e.which == 13) {
                submitChanges();

                e.preventDefault();
                return false;
            }
        }
      
    };
    TreeViewItem.prototype.select = function () {
        if (!this.isEditMode) {
      
            if (this.root.selectedItem != null)
                this.root.selectedItem.domElem.children[2].classList.remove('selected');
            this.root.selectedItem = this;
            this.domElem.children[2].classList.add('selected');
        }
        else {
            this.root.selectedItem.domElem.children[2].classList.remove('selected');
            selectText(this.domElem.children[2]);
        }
    };
    TreeViewItem.prototype.enableBehavior = function () {



     
        var arrow = this.domElem.children[0];
        var self = this;
        arrow.addEventListener('click', function (e) {
            e.cancelBubble = true;
            self.toggleExpand();
        }, false);
     
        this.domElem.addEventListener('mousedown', function () {
         
            self.select();
        }, false);
        var contextMenu = this.contextMenu;
        this.addEventListener('contextmenu', function (e) {
         
            if (contextMenu != null && !self.isEditMode) {
                document.body.appendChild(contextMenu.domElem);
                contextMenu.target = self;
                contextMenu.show(e.pageX, e.pageY);
                e.preventDefault();
                return false;
             
            }


        });

    }
    TreeViewItem.prototype.toggleExpand = function () {
        this.isOpen = !this.isOpen;
        if (this.domElem.nextElementSibling != null && this.isOpen && (this.domElem.nextElementSibling instanceof HTMLUListElement)) {
            if (this.onOpen)
                this.onOpen();
            this.domElem.nextElementSibling.style.display = 'inline-block';
            this.domElem.children[0].style.webkitTransform = 'rotate(90deg)';
        }
        else if (this.domElem.nextElementSibling != null && !this.isOpen && (this.domElem.nextElementSibling instanceof HTMLUListElement)) {
            if (this.onClose)
                this.onClose();
            this.domElem.nextElementSibling.style.display = 'none';
            this.domElem.children[0].style.webkitTransform = '';

        }
     

    }
    return TreeViewItem;
}();