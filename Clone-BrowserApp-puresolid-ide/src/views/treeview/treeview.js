
//constructor
function TreeView(elemId) {

    this.items = [];
    this.selectedItem = null;
    this.domElem = null;
    if (elemId)
        this.bind(elemId);
    else {
        this.create();
    }

}

TreeView.prototype.create = function () {
    this.domElem = document.createElement('ul');

    this.domElem.classList.add('tree-view');

}
TreeView.prototype.hasChild = function (title) {

    for (var i = 0; i < this.items.length; i++) {
        if (this.items[i].title == title)
            return this.items[i];

    }
    return undefined;
}
TreeView.prototype.addItem = function (treeViewItem) {

    treeViewItem.parent = this;
    treeViewItem.root = this;
    treeViewItem.enableBehavior();
    this.items.push(treeViewItem);


    this.domElem.appendChild(treeViewItem.domElem);

}

TreeView.prototype.clear = function () {
    this.items = [];
    this.domElem.innerHTML = "";
}