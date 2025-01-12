var ImageViewer = function (cnt, file) {

    var img = document.createElement('img');

    var path = window.URL.createObjectURL(file);
    img.src = path;
    cnt.appendChild(img);
    cnt.style.overflow = 'auto';
    this.setSize = function (w,h) {
        cnt.style.width = w + 'px';
        cnt.style.height = h + 'px';
    }
}