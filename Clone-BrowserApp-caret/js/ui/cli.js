define([
  "command",
  "editor"
  ], function(command, editor) {
  
  var cli = document.querySelector(".command-line");
  var input = document.querySelector(".command-line input");
  
  input.addEventListener("keyup", function(e) {
    switch (e.keyCode) {
      case 13:
        //on enter, execute command
        var value = input.value;
        var split = value.split(" ");
        var cmd = split[0];
        var arg = split[1];
        if (arg) {
          try {
            arg = JSON.parse(arg);
          } catch (e) {
            //failure to parse isn't the end of the world.
          }
        }
        if (cmd) {
          command.fire(cmd, arg);
        }
        
      case 27:
        //on escape, also hide the prompt
        cli.classList.remove("show");
        editor.focus();
    }
  });
  
  command.on("app:show-prompt", function() {
    input.value = "";
    cli.classList.add("show");
    input.focus();
  });
  
  command.on("app:hide-prompt", function() {
    cli.classList.remove("show");
    editor.focus();
  })
    
});