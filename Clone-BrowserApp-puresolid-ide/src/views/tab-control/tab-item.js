/// <reference path="treeView.js" />


var TabItem = function () {


    //constructor
    function TabItem(elem) {

        ///public members
        this.items = [];
        this.domElem = {};
        this.parent = null;
        this.onClose = null;

        if (elem)
            this.bind(elem);
        else {
            this.create();
        }
       

        //properties
        Object.defineProperty(this, 'title', {
            set: function (value) { this.domElem.tab.children[0].innerHTML = value },
            get: function () { return this.domElem.tab.children[0].innerHTML },
            configurable: false
        });

        Object.defineProperty(this, 'image', {
            set: function (value) {
                this.domElem.tab.children[1].style.backgroundImage = "url('" + value + "')";
            },
            configurable: false
        });
    
      

    }

    TabItem.prototype.create = function () {
        //create tab
        this.domElem.tab = document.createElement('div');
        this.domElem.tab.classList.add('tabview-tab');


        //add title
        var title = document.createElement('span');   
        this.domElem.tab.appendChild(title);

        //add close button
        var close = document.createElement('div');
        close.classList.add('tab-close');
     
        var domElem = this.domElem;
      
        var self = this;
        close.addEventListener('click', function (e) {
            e.cancelBubble = true;
            if (self.onClose != null) {
                self.onClose();
                if (self.cancelClose) return;
            }
            self.close();
            if (self.onClosed) {
                self.onClosed();
            }
        }, false);

        this.domElem.tab.appendChild(close);
     

        this.domElem.tabCnt = document.createElement('div');
        this.domElem.tabCnt.classList.add('tab-cnt');




    }
    TabItem.prototype.close = function () {
       
        this.domElem.tab.parentElement.removeChild(this.domElem.tab);
        this.domElem.tabCnt.parentElement.removeChild(this.domElem.tabCnt);

        var index = this.parent.tabs.indexOf(this);
        if (index != -1) {
           this.parent.tabs.splice(index, 1);

        }
        if (this == this.parent.activeTab) {
            try {
                this.parent.setActiveTab(index - 1);
            }
            catch (e) {
                this.parent.setActiveTab(index);
            }
        }


        if (this.parent.tabs.length == 0) {
            this.parent.domElem.children[0].classList.remove('tabview-header');
        }
    };
    TabItem.prototype.addItem = function (item) {
        if (item.domElem)
            this.domElem.tabCnt.appendChild(item.domElem)
        else if (item instanceof HTMLElement) {
            this.domElem.tabCnt.appendChild(item)
        }
        else {
            this.domElem.tabCnt.innerHTML = item;
        }
   
    }

    TabItem.prototype.addItems = function () {
        for (var i = 0; i < arguments.length; i++)
            this.addItem(arguments[i]);
    }

  
    TabItem.prototype.addEventListener = function (event, handler) {
        
    }
  
        TabItem.prototype.enableBehavior = function () {

        var domElem = this.domElem;
        var self=this;
   
        this.domElem.tab.addEventListener('click', function () {
            self.parent.setActiveTab(self.parent.tabs.indexOf(self));

        }, false)

        this.domElem.tab.addEventListener('mouseover', function () {
            var activeTab = self.parent.activeTab;
            for (var i = 0; i < self.parent.tabs.length; i++) {
                if (self.parent.tabs[i] != activeTab)
                {
                    self.parent.tabs[i].domElem.tab.classList.remove('tab-hover');

                    self.parent.tabs[i].domElem.tab.children[1].style.visibility = '';
                }

            }                  
            if (self != activeTab) {
                domElem.tab.classList.add('tab-hover');

                domElem.tab.children[1].style.visibility = 'visible';
            }

        }, false)

        this.domElem.tab.addEventListener('mouseout', function () {
            var activeTab = self.parent.activeTab;
            for (var i = 0; i < self.parent.tabs.length; i++) {
                if (self.parent.tabs[i] != activeTab) {
                    self.parent.tabs[i].domElem.tab.classList.remove('tab-hover');
                    self.parent.tabs[i].domElem.tab.children[1].style.visibility = 'hidden';
                }

            }



        }, false)
    }

    return TabItem;
}();