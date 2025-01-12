function File() {


    this.phisicalPath = null;
    this.content = null;
    Object.defineProperty(this, 'extension', {
       
        get: function () { return this.name.split('.').pop(); },
        configurable: false
    });
    Object.defineProperty(this, 'contentType', {

        get: function () { return MimeType.fromExtenstion(this.extension) },
        configurable: false
    });
    this.name = null;
    this.rename = function(value) {
        this.name = value;
        var paths = this.phisicalPath.split('/');
        paths.pop();
        paths.push(value);
        this.phisicalPath = paths.join('/');
    };
}
function Folder() {
    this.phisicalPath = null;
    this.files = [];
    this.name = null;
    this.folders = [];
    this.rename = function (value) {
        this.name = value;
        var paths = this.phisicalPath.split('/');
        paths.pop();
        paths.push(value);
        this.phisicalPath = paths.join('/');
    };
}
Folder.prototype.addFile=function(file){
    this.files.push(file);

}
Folder.prototype.addFiles = function () {
    for (var i = 0; i < arguments.length; i++)
        this.addFile(arguments[i]);
}
Folder.prototype.addFolder = function (file) {
    this.folders.push(file);

}
Folder.prototype.addFolders = function () {
    for (var i = 0; i < arguments.length; i++)
        this.addFolder(arguments[i]);
}

