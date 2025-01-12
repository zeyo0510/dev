function Button() {

   
    this.domElem = null;

    this.create();
 
    Object.defineProperty(this, 'title', {
        set: function (value) { this.domElem.innerHTML = value },
        get: function () { return this.domElem.innerHTML },
        configurable: false
    });


}

Button.prototype.create = function () {
    this.domElem = document.createElement('button');
    this.domElem.classList.add('button');


}
Button.prototype.addEventListener = function (event, handler) {
    this.domElem.addEventListener(event, handler, false);
}


