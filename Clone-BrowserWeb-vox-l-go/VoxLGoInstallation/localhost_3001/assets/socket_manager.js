var SocketManager = function(){
  this.socket = io.connect("http://159.203.85.179:5000");
  this.room = undefined
  this.startTime = undefined // For latency calcs
  this.latency = 0           // For latency calcs
  var socket_manager = this

  this.avatar 

  this.receive_unity = true

  this.listeners = []
  this.addMessageListener = function(fun){
    this.listeners.push(fun)
  }

  this.socket.on('chat message', function(msg){
    var obj = JSON.parse(msg)


    for(var i in socket_manager.listeners){
      socket_manager.listeners[i](obj)
    }


    if(obj.type.indexOf("blockly") >= 0){
      window.blockly_message_bus.receive(obj)

      return
    }

    if(obj.type == 'ping'){
      if(!socket_manager.is_server)
        return

      socket_manager.sendMessage(JSON.stringify({type: "pong", data: {socket_id: obj.data.socket_id}}));

      return
    }

    if(!socket_manager.is_server){
      if(obj.data.socket_id && obj.data.socket_id != socket_manager.getId()){
        //We politely disregard messages that aren't for us.  (Trusted clients.)
        return
      }
    }

    if(obj.type == 'pong'){
      socket_manager.latency = (Date.now() - socket_manager.startTime)/2;

      return
    }



    if(obj.type == "dump_request" && socket_manager.is_server){
      var temp_sub = new function(){
        this.receive = function(msg){

          if(msg.type == "dump"){
            msg.data.socket_id = obj.data.socket_id
            socket_manager.sendMessage(JSON.stringify(msg));   
            window.message_bus.unsubscribe(temp_sub)
          }
        }
      }

      window.message_bus.subscribe(temp_sub)

      return
    } 

    obj.data.latency = socket_manager.latency / 1000
    var msg = JSON.stringify(obj)


    if(!socket_manager.receive_unity)
      return

    SendMessage("Browser", "ApplyMessage", msg)
  });

  this.createRoom = function(id, video, unity){
    this.receive_unity = unity

    this.is_server = true
    this.getId() //Generates
    this.room = id
    this.socket.emit('create room', id)

    if(video)
      this.setupVideo()
  }

  this.setupVideo = function(){
    $("body").append("<div id='remoteVideos'></div>")
    this.webrtc = new SimpleWebRTC({
      localVideoEl: 'localVideo',
      remoteVideosEl: 'remoteVideos',
      autoRequestMedia: true
    });

    this.webrtc.on('readyToCall', function () {
      socket_manager.webrtc.joinRoom(socket_manager.room + "_" + window.nickname);
   })
  }

  this.videoChat = function(nick){
    this.webrtc.joinRoom(this.room + "_" + nick);
  }

  //Generates random id.
  this.id = undefined
  this.getId = function(){
    if(!this.id){
      var r = Math.random() + "";
      this.id = r.split(".")[1];
    }

    return this.id
  }

  this.joinRoom = function(id, video, unity){
    console.log("ERR: This doesn't currently work...")
    this.receive_unity = unity
    this.room = id
    this.is_server = false
    //  Commenting out (see err message above)
    //  Causes entity_moveevents not to be sent from unity
    //  Breaks things like AI scripts, that wait for the entity to move (be spawned) before sending ai messages.
    //SendMessage("Browser", "DisableServer", "")
    this.socket.emit('join room', id)
    this.socket.emit('chat message', JSON.stringify({type: "dump_request", data: {socket_id: this.getId()}}), id)


    setInterval(function() {
       socket_manager.startTime = Date.now();
       socket_manager.sendMessage(JSON.stringify({type: "ping", data: {socket_id: socket_manager.getId()}}));
    }, 2000);

    if(video)
      this.setupVideo()
  }

  this.setAvatar = function(name){
    SendMessage("Browser", "SetAvatarAsset", name)
  }

  this.sendMessage = function(msg){
    if(!this.room)
      return

    var obj = JSON.parse(msg)
    obj.nickname = window.nickname
    msg = JSON.stringify(obj)

    this.socket.emit('chat message', msg, this.room);
  }


  //Gets messages from Unity, via the message bus
  this.receive = function(msg_object){
     this.sendMessage(JSON.stringify(msg_object))
  }
}

;
