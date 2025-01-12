function Toolbar() {

    this.items = [];
    this.domElem = null;


    this.create();


}

Toolbar.prototype.create = function () {
    this.domElem = document.createElement('div');
    this.domElem.classList.add('toolbar');


};

Toolbar.prototype.addItem = function (toolbarItem) {

    toolbarItem.parent = this;
    
    toolbarItem.enableBehavior();

    toolbarItem.domElem.classList.add('toolbar-item-hover');
    toolbarItem.domElem.classList.add('toolbar-item-root');


    this.items.push(toolbarItem);
    this.domElem.appendChild(toolbarItem.domElem);

};

