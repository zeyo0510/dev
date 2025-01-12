var Core = Core || {}
if (Core.register) {
    Core.register('report', function (runtime) {

        var reportWnd = new ChildWnd();
        var txtArea = document.createElement('textarea');
        return {
            init: function () {

            },
            show: function (target) {

                //reportWnd.title = "What's wrong?";
                //reportWnd.borderStyle = ChildWnd.borderStyle.normal;
                //reportWnd.width = 555;
                //reportWnd.height = 440;
                //document.body.appendChild(reportWnd.domElem);

                //reportWnd.showDialog(true);


                chrome.app.window.create('Report.html', {
                    'id': 'report',
                    'frame': 'none',
                    'width': 555,
                    'height': 440,
                    // screenshots
                    // 'width': 1335,
                    // 'height': 834,
                    'minWidth': 555,
                    'minHeight': 440,
                    'maxWidth': 555,
                    'maxHeight': 440

                });
                var reportWnd = chrome.app.window.get('report');
            }



        }
    });

}
else {
    var closeBtn = document.getElementById('close_btn');
  
    var sendBtn = document.getElementById('send_btn');
    var loader = document.getElementById('loader');
    var cancelBtn = document.getElementById('cancel_btn');
    var messageTxt = document.getElementById('message_txt');
    var emailTxt = document.getElementById('email_txt');
    sendBtn.toggle = false;
    messageTxt.addEventListener("textInput", function () {
        messageTxt.style.borderColor = '#c8c8c8';
    });
    closeBtn.addEventListener('click', function () {
        chrome.app.window.current().close();
    });
    cancelBtn.addEventListener('click', function () {
        chrome.app.window.current().close();
    });
    sendBtn.addEventListener('click', function () {
        var message = messageTxt.value.trim();
        var email = emailTxt.value;
       
        if (message != "") {
            var issue =JSON.stringify({ Message: message, Email: email });
            var xhr = new XMLHttpRequest();
            xhr.addEventListener('readystatechange', function () {
                if (xhr.readyState == 4) {
                    if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
                        loader.style.display = 'none';
                        chrome.app.window.current().close();
                    }
                    else {
                        sendBtn.style.backgroundColor = 'red';
                        sendBtn.style.color = '#ffffff';
                        loader.style.display = 'none';
                    }
                }
            });
            xhr.open('POST', 'http://puresolid.net/api/Report');
            xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
            xhr.send(issue);
            loader.style.display = '';
        }
        else {
            messageTxt.style.borderColor = 'red';
        }
    });
}
