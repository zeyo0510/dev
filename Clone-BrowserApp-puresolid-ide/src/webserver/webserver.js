var WebServer = function (rootEntry,rootFolder) {

    var socket = chrome.socket;
    var socketInfo;
    var ipAddress = {
        host: "127.0.0.1",
        port:"1234"
    }
    var stringToUint8Array = function (string) {
        var buffer = new ArrayBuffer(string.length);
        var view = new Uint8Array(buffer);
        for (var i = 0; i < string.length; i++) {
            view[i] = string.charCodeAt(i);
        }
        return view;
    };
    var arrayBufferToString = function (buffer) {
        var str = '';
        var uArrayVal = new Uint8Array(buffer);
        for (var s = 0; s < uArrayVal.length; s++) {
            str += String.fromCharCode(uArrayVal[s]);
        }
        return str;
    };
    this.start=function(){
        socket.create("tcp", {}, function (_socketInfo) {
            socketInfo = _socketInfo;
            socket.listen(socketInfo.socketId, ipAddress.host, parseInt(ipAddress.port),  function (result) {
                console.log("LISTENING: " + ipAddress.host+ parseInt(ipAddress.port), result);
                socket.accept(socketInfo.socketId, onAccept);
            });
        });

    }
    var onAccept = function (acceptInfo) {
        console.log("ACCEPT", acceptInfo)
        readFromSocket(acceptInfo.socketId);
    };

    var readFromSocket = function (socketId) {
        //  Read in the data
        socket.read(socketId,4096, function (readInfo) {
            console.log("READ", readInfo);
            // Parse the request.
            var data = arrayBufferToString(readInfo.data);
            if (data.indexOf("GET ") == 0) {
                var keepAlive = false;
                if (data.indexOf("Connection: keep-alive") != -1) {
                    keepAlive = true;
                }

                // we can only deal with GET requests
                var uriEnd = data.indexOf(" ", 4);
                if (uriEnd < 0) { /* throw a wobbler */ return; }
                var uri = data.substring(4, uriEnd);
                // strip qyery string
                var q = uri.indexOf("?");
                if (q != -1) {
                    uri = uri.substring(0, q);
                }
        
               
               

               
                                IO.getFile(rootFolder + uri,rootEntry, function (file) {
                                    if (!!file == false) {
                                        console.warn("File does not exist..." + uri);
                                        writeErrorResponse(socketId, 404, false);
                                        return;
                                    }
                                    console.log("GET 200 " + uri);
                                   
                                    write200Response(socketId, file, false);
                                 

                                });
                          
               
               
            }
            else {
                // Throw an error
                console.log("DESTROY");
                socket.destroy(socketId);
            }
        });
    }; var writeErrorResponse = function (socketId, errorCode, keepAlive) {
        var file = { size: 0 };
        console.info("writeErrorResponse:: begin... ");
        console.info("writeErrorResponse:: file = " + file);
        var contentType = "text/plain"; //(file.type === "") ? "text/plain" : file.type;
        var contentLength = file.size;
        var header = stringToUint8Array("HTTP/1.0 " + errorCode + " Not Found\nContent-length: " + file.size + "\nContent-type:" + contentType + (keepAlive ? "\nConnection: keep-alive" : "") + "\n\n");
        console.info("writeErrorResponse:: Done setting header...");
        var outputBuffer = new ArrayBuffer(header.byteLength + file.size);
        var view = new Uint8Array(outputBuffer)
        view.set(header, 0);
        console.info("writeErrorResponse:: Done setting view...");
        socket.write(socketId, outputBuffer, function (writeInfo) {
            console.log("WRITE", writeInfo);
           
           if (keepAlive) {
                readFromSocket(socketId);

             
           } else {
               console.log("DESTROY");
           
               socket.destroy(socketId);
          socket.accept(socketInfo.socketId, onAccept); 
           }
        });
        console.info("writeErrorResponse::filereader:: end onload...");

        console.info("writeErrorResponse:: end...");
    };
    var self = this;
    var write200Response = function (socketId, file, keepAlive) {
        var contentType = (file.type === "") ? "text/plain" : file.type;
        var contentLength = file.size;
        var header = stringToUint8Array("HTTP/1.0 200 OK\nContent-length: " + file.size + "\nContent-type:" + contentType + (keepAlive ? "\nConnection: keep-alive" : "") + "\n\n");
        var outputBuffer = new ArrayBuffer(header.byteLength + file.size);
        var view = new Uint8Array(outputBuffer)
        view.set(header, 0);
        
        var fileReader = new FileReader();
        fileReader.onload = function (e) {
            view.set(new Uint8Array(e.target.result), header.byteLength);
            socket.write(socketId, outputBuffer, function (writeInfo) {
                console.log("WRITE", writeInfo);
                console.log(keepAlive);
                if (keepAlive) {
                   
                    readFromSocket(socketId);
                  
                } else {
                    console.log("DESTROY");
           
                    socket.destroy(socketId);
                
                  

                  socket.accept(socketInfo.socketId, onAccept); 
               }
            });
        };

        fileReader.readAsArrayBuffer(file);
    };

}