

var ToolbarItem = function () {


    //constructor
    function ToolbarItem(elem) {

        ///public members

        this.domElem = null;
        this.parent = null;


        this.create();

        //properties
        Object.defineProperty(this, 'title', {
            set: function (value) { this.domElem.children[1].innerHTML = value;},
            get: function () { return this.domElem.children[1].innerHTML },
            configurable: false
        });
        Object.defineProperty(this, 'mini', {
            set: function (value) {
                if (value == true)
                {
                    this.domElem.classList.remove('toolbar-item-big');
                    this.domElem.classList.add('toolbar-item-mini');
                  
                }
                else {
                    this.domElem.classList.remove('toolbar-item-mini');
                    this.domElem.classList.add('toolbar-item-big');
                }
            },
            configurable: false
        });

        Object.defineProperty(this, 'image', {
            set: function (value) {
                this.domElem.image.style.display = 'block';
                this.domElem.image.style.backgroundImage = "url('" + value + "')";
            },
            configurable: false
        });







    }

    ToolbarItem.prototype.create = function () {
        //create node
        this.domElem = document.createElement('div');
        this.domElem.classList.add('toolbar-item');
        this.domElem.classList.add('toolbar-item-big');
        //add image
        this.domElem.image = document.createElement('div');
        this.domElem.image.classList.add('toolbar-item-image');
        this.domElem.insertBefore(this.domElem.image, this.domElem.firstChild);


        //add title
        var title = document.createElement('span');
        title.classList.add('toolbar-item-title');

        this.domElem.appendChild(title);



    }

    ToolbarItem.prototype.addEventListener = function (event, handler) {
        this.domElem.addEventListener(event, handler, false);
    }
    ToolbarItem.prototype.enableBehavior = function () {



    }

    return ToolbarItem;
}();