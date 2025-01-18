var BlocklyInterpreter = function(blocklyWorkspace){
  this.debugging    = false;  //true if we are in debugging mode and next breakpoint will suspend
  this.debugSuspend = false;  //true if the usual execution should stop (because the user will now step through manually)
  this.highlightPause = false
  this.eventLoopPause = false
  this.current_time = 0
  this.current_real_time = 0
  this.last_real_time = new Date().getTime()
  this.blocklyWorkspace = blocklyWorkspace
  this.scriptPreprocess = function(text){return text}
  this.highlightedBlock = undefined
  this.onHighlightBlock = function(){ }

  this.onDebugSuspend = function(){ }
  this.onDebugUnsuspend = function(){ }
  this.onDebugStep = function(){ }

  this.ajaxLoadLibrary = undefined

  this.halted = false

  this.runtimeError = undefined

  this.asyncSuspend = false

  this.summaries = {} //Keeps a log of SendUnityMessage calls, so we know how the program has been behaving.  Useful for comparing two programs.

  this.no_effect = false //Set to true to make the simulator not affect the world when the program runs.

  this.no_events = false //Set to true to just run the main() method and not listen for events afterwards

  this.parseError = false // Gets set if the program can't be run, due to some kind of error in acorn's parsing
  this.infiniteLoopDetected = false // Gets set if the interpreter halts because it thinks there's an infinite loop

  var that = this


  this.do_blockly_stuff = true


  /*
     Runs the script that comes from the current blockly workspace,
     preprocesses the script if necessary.  The script will run to 
     completion then start listening for events.

     Note that termination_callback will not run unless no_events is set to true.  By default, programs don't terminate.  They run the main() method and then loop forever, waiting for events to fire.
   */
  this.runScript = function(termination_callback){
    this.termination_callback = termination_callback
 
    if(this.do_blockly_stuff)
      Blockly.JavaScript.STATEMENT_PREFIX = undefined

    if(this.debugging){
      if(this.do_blockly_stuff){
        Blockly.JavaScript.STATEMENT_PREFIX = 'highlightBlock(%1);\n';
        Blockly.JavaScript.addReservedWords('highlightBlock');
      }
    }

    var text = Blockly.JavaScript.workspaceToCodeAlpha(this.blocklyWorkspace)

    text = this.scriptPreprocess(text)

    this.runInterpreter(text)
  }

  this.haltScript = function(){
    this.halted = true
  }

  this.startDebugging = function(){
    this.debugging = true;
  }

  this.highlightBlock = function(id) {
    console.log("Highlight Block")
    if(!this.debugging)
      return

    this.highlightedBlock = mainBlockly.getBlockById(id)
    console.log(this.highlightedBlock)

    //this.highlightedBlock.select()
    this.flashBlock(undefined)
    this.flashBlock(this.highlightedBlock)
    this.debugSuspend = true;
    this.highlightPause = true;
    this.onHighlightBlock();
  }

  /*
    Needs some more refactoring.  The SendMessage function should not 
    be defined here, since it's specific to the simulator, not to all
    blockly programs. 
  */
  this.myApi = function(interpreter, scope){
      // Add an API function for highlighting blocks.
      var wrapper = function(id) {
        id = id ? id.toString() : '';
        return interpreter.createPrimitive(that.highlightBlock(id));
      };
      interpreter.setProperty(scope, 'highlightBlock',
          interpreter.createNativeFunction(wrapper));

      wrapper = function(params) {
        params = params ? params.toString() : '';

        var fun = JSON.parse(params).type

        if(!that.summaries[fun])
          that.summaries[fun] = []

        that.summaries[fun].push(JSON.parse(params))

        if(that.no_effect)
          return interpreter.createPrimitive(0);
        else
        {
          window.message_bus.sendToUnity(this, params)
          return interpreter.createPrimitive(0);
        }
      };

      interpreter.setProperty(scope, 'SendUnityMessage',
          interpreter.createNativeFunction(wrapper));





      wrapper = function(text){
        //text = text.toString()
        return interpreter.createPrimitive(console.log(text))
      }

      interpreter.setProperty(scope, 'log',
          interpreter.createNativeFunction(wrapper));

      wrapper = function(){
        that.eventLoopPause = true;

        interpreter.getScope().properties.current_time = interpreter.createPrimitive(that.current_real_time)

        return interpreter.createPrimitive(0) //Needed?
      }

      interpreter.setProperty(scope, 'setCurrentTime',
          interpreter.createNativeFunction(wrapper));

      wrapper = function(){
        interpreter.getScope().properties.local_player_id = interpreter.createPrimitive(message_bus.getLocalPlayerId())

        var queue = that.message_queue
        that.message_queue = []

        if(that.last_player_location_message)
          queue.push(that.last_player_location_message)

        if(that.last_player_look_location_message)
          queue.push(that.last_player_look_location_message)

        if(that.last_player_facing_message)
          queue.push(that.last_player_facing_message)

        return interpreter.createPrimitive(JSON.stringify(queue))
      }

      interpreter.setProperty(scope, 'getMessages',
          interpreter.createNativeFunction(wrapper));


      wrapper = function(){
        that.halted = true;

        return interpreter.createPrimitive(0) //Needed?
      }

      interpreter.setProperty(scope, 'exit',
          interpreter.createNativeFunction(wrapper));

      wrapper = function(){
        that.current_time = 0
        that.current_real_time = 0
        that.last_real_time = new Date().getTime()
        return interpreter.createPrimitive(0) //Needed?
      }

      interpreter.setProperty(scope, 'startTime',
          interpreter.createNativeFunction(wrapper));

      wrapper = function(state){
        that.last_state = JSON.parse(state.toString())
        return interpreter.createPrimitive(0) //Needed?
      }

      interpreter.setProperty(scope, 'reportState',
          interpreter.createNativeFunction(wrapper));


      
      wrapper = function(path, callback){
        that.asyncSuspend = true

        $.get(path, function(data){
          that.asyncSuspend = false
          callback(interpreter.createPrimitive(JSON.stringify(data)))
        })

        return interpreter.createPrimitive(0)
      }

      interpreter.setProperty(scope, 'ajaxGet',
          interpreter.createAsyncFunction(wrapper));
  }

  this.runInterpreter = function(code){
    console.log("Running Interpreter")
    
    this.debugSuspend = false

    

    var that = this
    var go = function(prelude){
      if(that.no_events){
        that.myInterpreter = new Interpreter(prelude + "startTime();"+ code + "; \nexit()", that.myApi)  
      }
      else{
        that.myInterpreter = new Interpreter(prelude + "startTime(); message_queue = getMessages(); handleMessages();" + code + that.eventLoop() + "\nexit();", that.myApi)  
      }

      var stepper = setInterval(function(){
        if(that.halted){
          console.log("Program ended")

          window.message_bus.unsubscribe(this)

          clearInterval(stepper) 
          if(that.do_blockly_stuff)
            Blockly.JavaScript.STATEMENT_PREFIX = undefined

          if(that.termination_callback)
            that.termination_callback()

          return
        }

        try{
          that.bigStepInterpreter();
        }catch(e){
          console.log(e)
          that.runtimeError = e 
          that.halted = true

          if(that.do_blockly_stuff)
            Blockly.JavaScript.STATEMENT_PREFIX = undefined
          if(that.termination_callback)
            that.termination_callback()
        }
      }, 10)

      var timer = setInterval(function(){
        if(that.halted){
          clearInterval(timer) 
          return
        }

        if(!that.debugSuspend) {
          that.current_real_time += (new Date().getTime() - that.last_real_time)
          that.last_real_time = new Date().getTime()
          that.current_time += 10
          that.onDebugUnsuspend()
        }
        else{
          that.last_real_time = new Date().getTime()
          that.onDebugSuspend()
        }
      }, 10)
    }

    if(this.ajaxLoadLibrary){
      $.ajax({url:this.ajaxLoadLibrary, dataType: "text", success:function(data){
        try{
          go(data)
        } catch(e) {
          console.log(e)
          that.parseError = true

          if(that.do_blockly_stuff)
            Blockly.JavaScript.STATEMENT_PREFIX = undefined

          if(that.termination_callback)
            that.termination_callback()
        }
      }, error:function(data, other){
        console.log("Error")
        console.log(data)
        console.log(other)
      }
      })
    } else {
      try{
        go("")
      } catch(e) {
          console.log(e)
          that.parseError = true
          if(that.do_blockly_stuff)
            Blockly.JavaScript.STATEMENT_PREFIX = undefined
          if(that.termination_callback)
            that.termination_callback()
      }
    }
  }

  this.eventLoop = function(){
     return "\n\nwhile(true){\n"+
       "setCurrentTime()\n"+
       "handleTimedOutFunctions()\n"+
       "message_queue = getMessages()\n"+
       "handleMessages()"+
     "}"
  }

  //Steps over everything until the event loop begins, then stops on every iteration.  
  this.bigStepInterpreter = function(){
    while(!this.eventLoopPause && !this.debugSuspend && !this.halted && !this.asyncSuspend){
      this.myInterpreter.step()
    }

    if(!this.debugSuspend)
      this.eventLoopPause = false;
  }

  //For debugging.  Steps blockly block by blockly block
  this.stepCode = function(){
    if(this.halted)
      return

    if(this.eventLoopPause){
      this.flashBlock(undefined)
      //this.highlightedBlock.unselect()

      this.debugSuspend = false;
      return;  //Cuz the user was debugging and ended up returning from their function to the event loop, so we need to take them out of debug mode or else the step function will not halt
    }

    while(!this.highlightPause && !this.eventLoopPause && !this.asyncSuspend){
      this.myInterpreter.step()
    }

    this.onDebugStep()

    // A block has been highlighted.  Pause execution here.

    this.highlightPause = false;
  }


  this.getVariableValues = function(){
    var vars = Blockly.Variables.allVariables(this.blocklyWorkspace)

    var ret = {} 

    for(var i = 0; i < vars.length; i++){
      var curr = vars[i]
      ret[curr] = this.getVariableValue(curr)
    }

    return ret
  }

  this.getVariableValue = function(v){
    var scope = this.myInterpreter.getScope() 

    while(scope != undefined){
      if(scope.properties[v])
      {
         if(scope.properties[v].isPrimitive)
           return scope.properties[v].toString()
         else{
           this.seen_properties = []
           return JSON.parse(this.stringifyProperty(scope.properties[v]))
         }
      }

      scope = scope.parentScope
    }

    return undefined
  }

  this.seen_properties = []
  this.stringifyProperty = function(property){
    var ret = "{"
    for(var i in property.properties){
      var curr = property.properties[i]
      if(curr.type == "function" || i == "game_data")
        continue;  

      if(this.seen_properties.indexOf(curr) >= 0)
        continue;

      this.seen_properties.push(curr)

      if(curr.isPrimitive || typeof(curr) == "number" || typeof(curr) == "boolean" || typeof(curr) == "string")
        ret += "\""+i + "\":\"" + property.properties[i] + "\","
      else
        ret += "\""+i + "\":" + this.stringifyProperty(property.properties[i]) + ","
    }
    ret += "}"

    ret = ret.replace(/,}/g,"}")

    return ret
  }

   this.topScope = function(){
     var scope = this.myInterpreter.getScope()

     while(scope.parentScope)
       scope = scope.parentScope

     return scope
   }


  this.flash_interval
  this.flashBlock = function(b){
    if(!b){
      if(this.flashing_block){
        this.flashing_block.colourHue_ = this.flashing_block.originalHue_
        
        try{
          this.flashing_block.updateColour()
        }catch(e){
        
        }
      }
      clearInterval(this.flash_interval)

      return;
    }

    this.flashing_block = b
    this.flashing_block.originalHue_ = this.flashing_block.colourHue_

    var toggle = true
    this.flash_interval = setInterval(function(){
      toggle = !toggle
      if(toggle){
        b.colourHue_ = 100
        b.updateColour()
      }
      else{
        b.colourHue_ = 0
        b.updateColour()
      }

      if(that.halted)
        that.flashBlock(undefined)
        
    }, 500) 
  }

  //Gets messages from unity
  this.message_queue = []
  this.last_player_location_message
  this.last_player_look_location_message
  this.last_player_facing_message
  this.receive = function(msg_object){

    this.message_queue.push(msg_object) 
    if(msg_object.type == "entity_movement" && msg_object.data.entity_id.split("_")[0] == "player")
      this.last_player_location_message = msg_object

    if(msg_object.type == "entity_look" && msg_object.data.entity_id.split("_")[0] == "player")
      this.last_player_look_location_message = msg_object

    if(msg_object.type == "entity_facing" && msg_object.data.entity_id.split("_")[0] == "player")
      this.last_player_facing_message = msg_object
  }

  window.message_bus.subscribe(this)
}
;
