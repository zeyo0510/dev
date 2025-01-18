var BlocklyMessageBus = function(){
  this.blockly = undefined

  this.send_clicks = false

  this.blockly_subscription = undefined //Nickname of user we're subscribed to

  this.setBlockly = function(b){
    this.blockly = b
    this.startBroadcastingBlockly()
  }

  $(document).click(function(event) {
     if(!blockly_message_bus.send_clicks)
      return

     var target = $(event.target)
     var offset = target.offset()

     var path = blockly_message_bus.toX(target)
     var x = event.pageX - offset.left
     var y = event.pageY - offset.top

     //Do animation here, for testing.
     //blockly_message_bus.clickAnimation(x, y, path)

     var obj = {type: "blockly_click", data: {x: x, y: y, path: path}}

     if(window.socket_manager)
        socket_manager.sendMessage(JSON.stringify(obj))
  });

  this.clickAnimation = function(x,y, path){
    if(window.game_has_focus)
      return;

    var target = this.fromX(path)

    var y = y + target.offset().top
    var x = x + target.offset().left

    var div_text =  "<div class='click_mark' style='top: "+y+"px; left:"+x+"px'></div>"
    console.log(div_text)
    var div = $(div_text)
    $("body").append(div)

    setTimeout(function(){
      div.animate({width: 0, height: 0, opacity:0, marginLeft: 0, marginTop: 0}, 1000, function(){
        div.remove()
      })
    }, 1000)
  }

  //Got some message from the socket manager,
  //  here we translate it into changes to the blockly workspace
  this.receive = function(obj){
    if(!this.blockly) return;

    if(obj.type.indexOf("blockly_dump_request") >= 0){
      if(obj.recipient != window.nickname)
        return //Clients politely ignore irrelevant messages.

      var dump_msg = this.dumpWorkspace()
      
      if(window.socket_manager)
        socket_manager.sendMessage(JSON.stringify(dump_msg))

      this.startBroadcastingBlockly()

      return
    }

    if(!this.blockly_subscription)
      return

    if(obj.nickname != this.blockly_subscription)
      return

    if(obj.type == "blockly_click"){
      this.clickAnimation(obj.data.x, obj.data.y, obj.data.path)
      return
    }

    if(obj.type == "blockly_dump" && this.blockly){
      this.blockly.clear()
      var d = Blockly.Xml.textToDom(obj.data.xml)
      Blockly.Xml.domToWorkspace(this.blockly, d)
      return
    }
  }

  this.dumpWorkspace = function(){
    try{
      var d = Blockly.Xml.workspaceToDom(this.blockly)
      var xml = Blockly.Xml.domToText(d)

      return {type: "blockly_dump", data: {xml: xml}}
    }catch(e){
      return null
    }
  }

  this.onBlocklyChange = function(fun){
    if(this.blockly_listener)
      this.blockly.removeChangeListener(this.blockly_listener)

    var that = this
    this.blockly_listener = this.blockly.addChangeListener(function(change){
        var dump = that.dumpWorkspace()

        //For now, just send a full dump,
        // Later optimize to send just deltas
        if(dump)
          fun(dump)  
    })
  }

  this.stopBroadcastingBlockly = function(){
    //Stops sending events from our blockly, in case we had previously been subscribed to.
    window.blockly_message_bus.onBlocklyChange(function(){ })
    blockly_message_bus.send_clicks = false
  }

  this.startBroadcastingBlockly = function(){

      //Start subscribing to blockly events
      this.onBlocklyChange(function(change_msg){
        if(window.socket_manager)
            socket_manager.sendMessage(JSON.stringify(change_msg))
      })

      blockly_message_bus.send_clicks = true

      try{
        var dump_msg = this.dumpWorkspace()
        if(window.socket_manager)
            socket_manager.sendMessage(JSON.stringify(dump_msg))
      } catch(e) {

      }
  }

  this.blocklySubscribeTo = function(name){
    this.blockly_subscription = name

    //this.stopBroadcastingBlockly()

    var obj = {type: "blockly_dump_request", recipient: name}

    if(window.socket_manager)
        socket_manager.sendMessage(JSON.stringify(obj))
  }


  //Converting jquery objects into Xpaths and back.  Facilitates dom element click callouts across clients that may have different zoom settings, etc.
  this.fromX = function(STR_XPATH) {
      var xresult = document.evaluate(STR_XPATH, document, function(prefix) { 
                                    if (prefix === 'svg') 
                                    { 
                                        return 'http://www.w3.org/2000/svg';
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }, XPathResult.ANY_TYPE, null);
      var xnodes = [];
      var xres;
      while (xres = xresult.iterateNext()) {
          xnodes.push(xres);
      }

      return $(xnodes[0]);
  }

  this.toX = function( jq )
  {
      
      var element = jq[0]
      var xpath = '';
      for ( ; element && element.nodeType == 1; element = element.parentNode )
      {
          var id = $(element.parentNode).children(element.tagName).index(element) + 1;
          id > 1 ? (id = '[' + id + ']') : (id = '');
          xpath = '/' + element.tagName.toLowerCase() + id + xpath;
      }
      xpath = xpath.replace(/\/svg/g, "/svg:svg")
      xpath = xpath.replace(/\/g/g, "/svg:g")
      xpath = xpath.replace(/\/rect/g, "/svg:rect")
      xpath = xpath.replace(/\/text/g, "/svg:text")
      xpath = xpath.replace(/\/path/g, "/svg:path")
      xpath = xpath.replace(/\/image/g, "/svg:image")
      return xpath;
  }

}
;
