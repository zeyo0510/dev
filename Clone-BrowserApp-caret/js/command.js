define([
    "util/text!config/commands.json"
  ], function(list) {
    
    try {
      list = JSON.parse(list);
    } catch (e) {
      console.error(e);
      list = [];
    }

  /*
  
  The command module is the heart of Caret's loosely-coupled modules. It
  serves as an event bus for inter-module communication, but it also listens
  to the DOM for click/change events and makes it easy to bind page interactions
  to command callbacks.
  
  Command is usually called as if synchronous, but it does support asynchronous
  operation--it will pass a callback in to any subscribers, but will also
  immediately pass back the return value (if any) from the those subscribers.
  
  */
  
  var commands = {};
  var broadcast = [];
  
  //commands can pass a callback, although most don't respond that way
  var fire = async function(command, argument, callback = function() {}) {
    if (!commands[command]) return broadcast.forEach(f => f.apply(null, arguments));
    var registry = commands[command].slice();
    for (var entry of registry) {
      var result = await entry.callback(argument);
      //immediately call back if sync-style return value was provided
      if (typeof result !== "undefined" || entry.sync) {
        //console.info("Immediate return from " + name, result);
        callback.call(null, result);
      }
    };
  };
  
  var register = function(command, listener, sync) {
    if (command == "*") {
      return broadcast.push(listener);
    }
    if (!commands[command]) {
      commands[command] = [];
    }
    //we allow a sync flag to be set for operations that will definitely return
    commands[command].push({
      callback: listener,
      sync: sync
    });
  };

  //delegate for all elements that have a command attribute
  document.body.addEventListener("click", function(e) {
    //cancel on inputs, selectboxes
    if (["input", "select"].indexOf(e.target.tagName.toLowerCase()) >= 0) return;
    if (e.button != 0) return;
    //delegate all items with a command attribute
    if (e.target.hasAttribute("command")) {
      var command = e.target.getAttribute("command");
      var arg = e.target.getAttribute("argument");
      fire(command, arg);
      e.preventDefault();
    }
  });
  
  document.body.addEventListener("change", function(e) {
    if (e.target.hasAttribute("command")) {
      var command = e.target.getAttribute("command");
      var arg = e.target.value;
      fire(command, arg);
    }
  });
  
  //handle command events directly dispatched via DOM
  document.body.addEventListener("caret-command", function(e) {
    if (!e.detail || !e.detail.command) return;
    fire(e.detail.command, e.detail.argument);
  });

  //register for post-startup and fire any commands that are pending
  register("init:complete", function() {
    if (window.launchCommands) {
      window.launchCommands.forEach(bundle => fire(bundle.message.command, bundle.message.argument, bundle.sendResponse));
      delete window.launchCommands;
    }
  });
  
  var facade = {
    fire,
    list,
    on: register
  };
  
  return facade;

});