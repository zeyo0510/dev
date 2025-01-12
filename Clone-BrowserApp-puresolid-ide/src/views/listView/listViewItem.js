

var ListViewItem = function () {


    //constructor
    function ListViewItem(elem) {

        ///public members
       
        this.domElem = null;
        this.parent = null;

      
            this.create();
      
        //properties
        Object.defineProperty(this, 'title', {
            set: function (value) { this.domElem.children[1].innerHTML = value },
            get: function () { return this.domElem.children[1].innerHTML },
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

    ListViewItem.prototype.create = function () {
        //create node
        this.domElem = document.createElement('li');
        this.domElem.classList.add('list-view-item');
        //add image
        this.domElem.image = document.createElement('div');
        this.domElem.image.classList.add('list-item-image');
        this.domElem.insertBefore(this.domElem.image, this.domElem.firstChild);


        //add title
        var title = document.createElement('span');
        title.classList.add('list-item-title');

        this.domElem.appendChild(title);
        


    }

    ListViewItem.prototype.addEventListener = function (event, handler) {
        this.domElem.addEventListener(event, handler, false);
    }
    ListViewItem.prototype.enableBehavior = function () {

        

    }

    return ListViewItem;
}();