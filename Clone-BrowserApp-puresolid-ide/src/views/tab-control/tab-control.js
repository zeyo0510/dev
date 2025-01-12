
//constructor
function TabView(elemId) {

    this.tabs = [];
    this.domElem = null;
  
    if (elemId)
        this.bind(elemId);
    else {
        this.create();
    }

    this.activeTab = null;
    

    Object.defineProperty(this, 'height', {
        set: function (value) {
            this.domElem.style.height = value + 'px';
            for (var i = 0; i < this.tabs.length; i++) {
                this.tabs[i].domElem.tabCnt.style.height = value - this.domElem.header.offsetHeight + 'px';
            }
        },
        get: function () { return this.domElem.offsetHeight; },
        configurable: false
    });


}

TabView.prototype.create = function () {
    this.domElem = document.createElement('div');
    this.domElem.classList.add('tabview-cnt');
 this.domElem.header = document.createElement('div');
 // header.classList.add('tabview-header');
 this.domElem.appendChild( this.domElem.header);
 
}
TabView.prototype.closeTabs = function () {
    this.tabs = [];
    this.domElem.children[0].innerHTML = '';
    while (this.domElem.children[1]) {
        this.domElem.removeChild(this.domElem.children[1]);
    }
   
},
TabView.prototype.addItem = function (tabviewItem) {

    if (!this.domElem.children[0].classList.contains('tabview-header'))
        this.domElem.children[0].classList.add('tabview-header');
  
    tabviewItem.parent = this;
    this.tabs.push(tabviewItem);
    this.domElem.children[0].appendChild(tabviewItem.domElem.tab);

    this.domElem.appendChild(tabviewItem.domElem.tabCnt);
   
    this.setActiveTab(this.tabs.indexOf(tabviewItem));


    tabviewItem.enableBehavior();
  
}

TabView.prototype.setActiveTab = function (index) {
    if (index >= 0 && index < this.tabs.length) {
        if (this.activeTab != null) {
            var activeTab = this.activeTab;
        
            activeTab.domElem.tabCnt.style.display = '';
            activeTab.domElem.tab.children[1].style.visibility = '';
            activeTab.domElem.tab.classList.remove('activeTab');
        }


        var tabviewItem = this.tabs[index];
        tabviewItem.domElem.tab.style.backgroundColor = '';
        tabviewItem.domElem.tab.classList.add('activeTab');
     
        tabviewItem.domElem.tabCnt.style.display = 'block';
        tabviewItem.domElem.tab.children[1].style.visibility = 'visible';
        this.activeTab = tabviewItem;
    }
}