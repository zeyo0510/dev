
//constructor
function Layout() {

    this.items = [];
    this.domElem = null;

    this.create();
  

    Object.defineProperty(this, 'orientation', {
        set: function (value) {
            switch (value) {
                case 'horizontal': {
                    for (var i = 0; i < this.items.length; i++) {
                        this.items[i].domElem.style.float = 'left';
                    }
                }; break;
                case 'vertical': {
                    for (var i = 0; i < this.items.length; i++) {
                        this.items[i].domElem.style.float = '';
                    }
                }; break;
            }
        },

        configurable: false
    });

}

Layout.prototype.create = function () {
    this.domElem = document.createElement('div');
    this.domElem.classList.add('layout');

}

Layout.prototype.addItem = function (item) {
    if (this.orientation == 'horizontal') {
        item.domElem.style.float = 'left';
    }
    item.parent = this;

   


    this.items.push(item);
    this.domElem.appendChild(item.domElem)

}

