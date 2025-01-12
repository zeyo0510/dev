//constructor
function ContextMenu(elemId) {

    this.items = [];
    this.domElem = null;

    this.create();


}

ContextMenu.prototype.create = function () {
    this.domElem = document.createElement('ul');
    
    this.domElem.classList.add('contextMenu');
    var self = this;
    document.addEventListener('mousedown', function () {
        self.domElem.style.display = 'none';
    }, false);
   
}
ContextMenu.prototype.show = function (x, y) {

    if (this.domElem.parentNode != null) {
        if (this.onOpen)
            this.onOpen();
        this.domElem.style.top = y + 'px';
        this.domElem.style.left = x + 'px';
        this.domElem.style.display = 'block';
        this.domElem.style.zIndex = '5';
    }
}

ContextMenu.prototype.addItem = function (menuItem) {

    menuItem.parent = this;
    menuItem.enableBehavior();
    //remove non-roote styles
 //   menuItem.domElem.children[2].classList.remove('contextMenu-item-arrow');
 //   menuItem.domElem.children[1].classList.remove('contextMenu-item-title');
  //  menuItem.domElem.children[0].classList.remove('contextMenu-item-image');
    this.items.push(menuItem);
    this.domElem.appendChild(menuItem.domElem)

}