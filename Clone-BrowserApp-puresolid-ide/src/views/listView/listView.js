
//constructor
function ListView() {

    this.items = [];
    this.domElem = null;
    
        this.create();
    //properties
   
        this.selectedItem ;
    
    //properties
        Object.defineProperty(this, 'selectedIndex', {
            set: function (value) {
                if (value < this.items.length) {

                    if (this.selectedItem) {
                        this.selectedItem.domElem.classList.remove('selected');
                    }
                    this.items[value].domElem.classList.add('selected');
                    this.selectedItem = this.items[value];

                }
            },
           
            configurable: false
        });
        Object.defineProperty(this, 'multicolumn', {
            set: function (value) {
            
            },

            configurable: false
        });
}

ListView.prototype.create = function () {
    this.domElem = document.createElement('ul');
    this.domElem.classList.add('list-view');

}

ListView.prototype.addItem = function (listViewItem) {
    var self = this;
    listViewItem.addEventListener('mousedown', function () {
        if (self.selectedItem) {
            self.selectedItem.domElem.classList.remove('selected');
        }
        listViewItem.domElem.classList.add('selected');
        self.selectedItem = listViewItem;

    }, false);
    listViewItem.parent = this;
    listViewItem.enableBehavior();
   
  
    this.items.push(listViewItem);
    this.domElem.appendChild(listViewItem.domElem)
    this.selectedIndex = 0;
}
ListView.prototype.clear = function () {
    this.items = [];
    this.domElem.innerHTML = "";
}

