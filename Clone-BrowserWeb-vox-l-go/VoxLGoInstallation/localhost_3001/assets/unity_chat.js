var Chat = function(){
  var chat = this
  chat.bar_elem = undefined


  chat.send = function(){
    var text = chat.bar_elem.val()

    chat.bar_elem.val("")

    
    if(text[0] == "/"){
      text = text.substr(1,text.length)
      me.performCommand(text)
    }
    else
      me.sendMessageAndBroadcast(text)
  }

  chat.setup = function(){
    chat.bar_elem = $("#chat_bar")

    chat.bar_elem.keypress(function (e) {
      if(e.which ==13){
        chat.send()       
      }
    });

    chat.bar_elem.blur(function(){
       chat.bar_elem.hide()
    })
  }

  chat.chat_bar_html = function(){
    return "<input type='text' id='chat_bar' style='display:none; width:100%; position: fixed; bottom: 0px; left: 0px; background-color: rgba(0,0,0,1); z-index: 1000; color: lime; padding: 10px;'></input>"
  }

  $("body").append(chat.chat_bar_html()) 

  $(document).keypress(function(e){
      if(e.which == 47 && !chat.bar_elem.is(':focus')){
        chat.bar_elem.show()
        chat.bar_elem.focus()
        unfocusGame()
      }
  })
  chat.setup()
}

$(document).on("vox_l_loaded",function(){       
  new Chat()
})
;
