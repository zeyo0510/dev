// assets path (Fixtures loader/Workers)
//window.effluxPath = "chrome-extension://lkbkmnajcnejkhpldnlidicmlhbgofhg"; // local testing
window.effluxPath = "chrome-extension://adpmbmefblipmmneilicljneagbenich"; // production path

(function() {

    // load Google webfont
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'https://fonts.gstatic.com/s/montserrat/v7/zhcz-_WihjSQC0oHJ9TCYAzyDMXhdD8sAj6OAJTFsBI.woff2', true);
    xhr.responseType = "blob";
    xhr.onreadystatechange = function() {
        if (xhr.readyState == 4) {
            var myfontblob = window.URL.createObjectURL(xhr.response);
            var newStyle = document.createElement('style');
            newStyle.appendChild(document.createTextNode("\
            @font-face {\
              font-family: 'Montserrat';\
              font-style: normal;\
              font-weight: 400;\
              src: url('" + myfontblob + "') format(woff);\
            }\
            "));
            document.head.appendChild(newStyle);
        }
    };
    xhr.send();
})();
