define([
    "settings!menus,keys",
    "editor",
    "ui/dialog",
    "command",
    "util/template!templates/menuItem.html",
    "util/i18n",
    "util/chromePromise"
  ], function(Settings, editor, dialog, command, inflate, i18n, chromeP) {
    
  //default "Windows", will be adjusted during menu creation because async
  var platform = "win";
  
  // walker() renders the menu and returns a document fragment
  var walker = function(list, depth) {
    var fragment = document.createDocumentFragment();
    for (var i = 0; i < list.length; i++) {
      var entry = list[i];
      if (typeof entry == "string") {
        var preset;
        switch (entry) {
          case "divider":
            preset = document.createElement("hr");
            break;
          //other special string keys go here - spacer? dynamic menus?
        }
        fragment.appendChild(preset);
        continue;
      }
      //version testing to hide entries that this version of Chrome can't run
      //originally for project support, should remove soon
      if (entry.minVersion && entry.minVersion > chrome.version) {
        continue;
      }
      var isAce = entry.command == "ace:command";
      var keyCombo = isAce ? findKeyCombo(entry.argument) : findKeyCombo(entry.command, entry.argument);
      if (keyCombo && platform == "mac") {
        //special snowflake keyboard adjustment
        keyCombo = keyCombo
          .replace("Command-", "Cmd-")
          .replace("Ctrl-", "Cmd-")
          .replace("Alt-", "Option-");
      }
      var data = {
        command: entry.command,
        argument: entry.argument,
        shortcut: keyCombo,
        hasChildren: entry.sub && !!entry.sub.length,
        isRoot: !depth,
        retainFocus: entry.retainFocus,
        label: i18n.get(entry.label)
      };
      var element = inflate.get("templates/menuItem.html", data);
      if (entry.sub) {
        var ul = element.querySelector("ul");
        ul.appendChild(walker(entry.sub, depth + 1));
      }
      fragment.appendChild(element);
    }
    return fragment;
  };

  var capitalizeAceKeys = function(s) {
    return s.replace(/(^\w|\b\w)/g, function(l) { return l.toUpperCase() });
  };

  var testAceBinding = function(key, binding, command) {
    if (typeof binding == "string" && binding == command) {
      return key;
    } else if (binding instanceof Array) {
      for (var i = 0; i < binding.length; i++) {
        var b = testAceBinding(key, binding[i], command);
        if (b) return b;
      }
    } else if (binding.name == command && binding.bindKey[platform]) {
      return binding.bindKey[platform].split("|").shift();
    }
    return false;
  };
  
  // We load match commands to the key config, so they're always current
  var findKeyCombo = function(command, arg) {
    var keys = Settings.get("keys");
    var handler = editor.keyBinding.getKeyboardHandler();
    var ckb = handler.commandKeyBinding;
    //check key config
    for (var key in keys) {
      var action = keys[key];
      if (!action) continue;
      var verb = action.ace || action.command || action;
      var object = action.argument;
      if (verb == command) {
        if (arg && object !== arg) continue;
        //transform old keys and lower-case
        key = key
          //back-compat
          .replace(/(\^|M)-([A-Z]+)$/, "$1-Shift-$2")
          .replace(/\^-/g, "Ctrl-")
          .replace(/M-/g, "Alt-")
          //capitalize keys for lazy people
          .replace(/(^|-)([a-z])/g, function(match) { return match.toUpperCase(); });
        return key;
      }
    }
    //fall back to Ace defaults
    if (command) for (var k in ckb) {
      var binding = testAceBinding(k, ckb[k], command);
      if (binding) return capitalizeAceKeys(binding);
    }
    return false;
  };
  
  var Menu = function() {
    this.element = document.querySelector(".toolbar");
    this.active = false;
    this.bindEvents();
  };
  Menu.prototype = {
    create: async function() {
      var cfg = Settings.get("menus");
      var info = await chromeP.runtime.getPlatformInfo();
      if (info.os == "mac") platform = "mac";
      var elements = walker(cfg, 0);
      this.element.innerHTML = "";
      this.element.appendChild(elements);
    },
    bindEvents: function() {
      var self = this;
      var menubar = this.element;
      var clickElsewhere = function(e) {
        if (e.target.matches(".toolbar *")) return;
        self.deactivate();
        self.active = false;
        document.body.removeEventListener("click", clickElsewhere);
      };
      menubar.addEventListener("click", function(e) {
        document.body.addEventListener("click", clickElsewhere);
        var el = e.target;
        if (el.classList.contains("top")) {
          el.classList.toggle("active");
          self.active = !self.active;
        } else {
          self.active = false;
        }
        if (!self.active && !el.classList.contains("no-refocus")) {
          editor.focus();
        }
        Array.from(menubar.querySelectorAll(".active"))
          .filter(n => n != el)
          .forEach(n => n.classList.remove("active"));
      });
      menubar.addEventListener("mousemove", function(e) {
        var el = e.target;
        if (el.classList.contains("top") && self.active) {
          self.deactivate();
          el.classList.add("active");
        }
      });
    },
    deactivate: function() {
      this.element.querySelectorAll(".active").forEach(node => node.classList.remove("active"));
    }
  };
  
  var menu = new Menu();
  
  command.on("init:startup", menu.create.bind(menu));
  command.on("init:restart", menu.create.bind(menu));

  command.on("app:about", function() {
    inflate.load("templates/about.html").then(function() {
      dialog(
        inflate.getHTML("templates/about.html", {
          version: chrome.runtime.getManifest().version
        })
      );
    });
  });
  
  return menu;
  
});