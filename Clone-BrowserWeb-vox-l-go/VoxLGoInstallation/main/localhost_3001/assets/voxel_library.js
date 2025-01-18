/// <reference path="typings/tsd.d.ts" />
var MessageBus = (function () {
    function MessageBus() {
        this.subscribers = {};
        this.subscriber_id = 0;
        this.local_player_id = "0";
        this.dump_stack = [];
    }
    MessageBus.prototype.dumpBegin = function () {
        var dump = { type: "dump", data: { dump: [] } };
        if (this.currentDump() != null) {
            this.currentDump().data.dump.push(dump);
        }
        this.dump_stack.push(dump);
    };
    MessageBus.prototype.dumpEnd = function () {
        //If we're popping the top-most dump.  Send it.
        if (this.dump_stack.length == 1) {
            this.sendToUnity(undefined, JSON.stringify(this.dump_stack.pop()));
            return;
        }
        //Otherwise, just pop the this dump
        this.dump_stack.pop();
    };
    MessageBus.prototype.currentDump = function () {
        return this.dump_stack[this.dump_stack.length - 1];
    };
    MessageBus.prototype.addToCurrentDump = function (json) {
        this.currentDump().data.dump.push(JSON.parse(json));
    };
    //Registers to receive events from unity
    MessageBus.prototype.subscribe = function (subscriber, no_dump) {
        this.subscribers[this.subscriber_id] = subscriber;
        subscriber.subscriber_id = this.subscriber_id;
        this.subscriber_id++;
        if (!no_dump)
            this.requestDumpFor(subscriber);
    };
    MessageBus.prototype.unsubscribe = function (subscriber) {
        if (subscriber.subscriber_id == undefined)
            return;
        delete (this.subscribers[subscriber.subscriber_id]);
    };
    MessageBus.prototype.getLocalPlayerId = function () {
        return this.local_player_id;
    };
    //Sends an event to subscribers (Unity -> Subscriber)
    //  Called by unity
    MessageBus.prototype.broadcast = function (msg_object) {
        if (msg_object && msg_object.data.entity_id && msg_object.data.entity_id.indexOf("player") >= 0) {
            this.local_player_id = msg_object.data.entity_id;
        }
        //Only send to one if Unity has attached a recipient's id (happens with "dump" messages, e.g.)
        if (msg_object.data.subscriber_id) {
            var s = this.subscribers[msg_object.data.subscriber_id];
            s.receive(msg_object);
            return;
        }
        //Otherwise, send to all.
        for (var i in this.subscribers) {
            var s = this.subscribers[i];
            s.receive(msg_object);
        }
    };
    //Asks for a full dump of current unity data (Subscriber -> Unity)
    MessageBus.prototype.requestDumpFor = function (subscriber) {
        SendMessage("Browser", "Dump", JSON.stringify({ subscriber_id: subscriber.subscriber_id }));
    };
    //Sends an event to unity (Subscriber -> Unity)
    //  NOTE: We don't use subscriber...  Why is it there??
    MessageBus.prototype.sendToUnity = function (subscriber, json) {
        if (this.dump_stack.length == 0) {
            SendMessage("Browser", "ApplyMessage", json);
        }
        else {
            this.addToCurrentDump(json);
        }
    };
    MessageBus.prototype.localPlayerSet = function () {
        return this.local_player_id != "0";
    };
    return MessageBus;
})();
/// <reference path="/home/dev/LTM-Web/lib/assets/js/unity_message_bus.js.ts" />
/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/unity.d.ts" />
/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/jquery/jquery.d.ts" />
var ArrayToJSON = function (arr) {
    var ret = "[";
    for (var i = 0; i < arr.length; i++) {
        var x = arr[i];
        if (x.toJSON)
            ret += x.toJSON();
        else if (x.length)
            ret += ArrayToJSON(x);
        else
            ret += x.toString();
        if (i != arr.length - 1)
            ret += ",";
    }
    ret += "]";
    return ret;
};
var Events = (function () {
    function Events() {
        //Maps from bukkit to unity event names
        this.mappings = {
            "block.BlockBreakEvent": "block_change:break",
            "block.BlockPlaceEvent": "block_change:place",
            "player.PlayerMoveEvent": "entity_movement:player",
            "player.PlayerInteractEvent": "player_interact",
            "player.PlayerInteractEntityEvent": "player_interact:entity",
            "player.PlayerChatEvent": "console_log",
            "entity.EntitySpawnEvent": "entity_movement",
            "entity.EntityDamageByEntityEvent": "entity_health_change",
            "window.LootChange": "loot_window_change",
            "window.ItemAdded": "add_to_window",
            "window.ItemRemoved": "remove_from_window"
        };
        this.table = {};
    }
    Events.prototype.clear = function () {
        this.table = {};
    };
    Events.prototype.when = function (event_name, fun, player /* not currently used */) {
        this.addEvent(this.mappings[event_name], fun);
    };
    Events.prototype.addEvent = function (event_name, fun) {
        if (this.table[event_name] == null)
            this.table[event_name] = [];
        this.table[event_name].push(fun);
    };
    Events.prototype.eventToType = function (event_message) {
        var type = event_message.type;
        if (event_message.type == "entity_movement") {
            if (event_message.data.entity_type && event_message.data.entity_type == "player")
                return type + ":player";
            return type;
        }
        if (event_message.type == "block_change" && event_message.data.material == "air")
            return type + ":break";
        if (event_message.type == "block_change" && event_message.data.material != "air")
            return type + ":place";
        if (event_message.type == "player_interact") {
            if (event_message.data.interacted_entity_id)
                return type + ":entity";
        }
        if (event_message.type == "player_interact") {
            return type;
        }
        if (event_message.type == "loot_window_change") {
            return type;
        }
        if (event_message.type == "add_to_window") {
            return type;
        }
        if (event_message.type == "remove_from_window") {
            return type;
        }
        if (event_message.type == "console_log") {
            return type;
        }
        if (event_message.type == "entity_health_change") {
            return type;
        }
        return null;
    };
    Events.prototype.handleEvent = function (event_message) {
        var type = this.eventToType(event_message);
        if (!type)
            return;
        var funs = this.table[type];
        var info = this.prettifyTypes(event_message.data);
        //Done with bukkit translations
        for (var i in funs) {
            var fun = funs[i];
            fun(info);
        }
    };
    Events.prototype.prettifyTypes = function (data) {
        var info = {};
        for (var k in data) {
            var v = data[k];
            if (k == "location") {
                v = this.convertToLocation(v);
            }
            else {
                if (typeof (v) == "object") {
                    v = this.prettifyTypes(v);
                }
            }
            info[k] = v;
        }
        return info;
    };
    Events.prototype.convertToLocation = function (something) {
        if (something.x) {
            return new Voxels.Location(something.x, something.y, something.z);
        }
        if (something._x) {
            return new Voxels.Location(something._x, something._y, something._z);
        }
        return new Voxels.Location(something[0], something[1], something[2]);
    };
    return Events;
})();
var VoxelState = (function () {
    function VoxelState() {
        this.game_data = new Voxels.GameData([]);
    }
    VoxelState.prototype.toJSON = function () {
        return this.game_data.toJSON();
    };
    VoxelState.prototype.setCallbacks = function (callbacks) {
        this._callbacks = callbacks;
    };
    VoxelState.prototype.nextId = function () {
        var r = Math.random() + "";
        return r.split(".")[1];
    };
    return VoxelState;
})();
var Voxels;
(function (Voxels) {
    var Entity = (function () {
        function Entity(loc, kind, id) {
            this.kind = kind;
            this.loc = loc.copy();
            if (!id)
                this.id = voxelsAPI.nextId();
            else
                this.id = id;
        }
        Entity.prototype.getId = function () {
            return this.id;
        };
        Entity.prototype.getKind = function () {
            return this.kind;
        };
        Entity.prototype.getType = function () {
            return this.kind;
        };
        Entity.prototype.getLocation = function () {
            return this.loc.copy();
        };
        Entity.prototype.setLocation = function (new_loc) {
            this.loc = new_loc;
            if (voxelsAPI._callbacks) {
                voxelsAPI._callbacks.onEntityMove(this);
            }
        };
        Entity.prototype.setVelocity = function (vel) {
            var msg = {
                type: "entity_movement",
                data: {
                    location: this.getLocation().toArray(),
                    velocity: [vel.getX(), vel.getY(), vel.getZ()],
                    entity_type: this.getKind(),
                    entity_id: this.getId(),
                    from_interpreter: "true"
                }
            };
            SendUnityMessage(JSON.stringify(msg));
        };
        Entity.prototype.teleport = function (new_loc) {
            this.setLocation(new_loc);
        };
        Entity.prototype.destroy = function () {
            var msg = {
                type: "entity_destroy",
                data: {
                    entity_id: this.getId(),
                    from_interpreter: "true"
                }
            };
            SendUnityMessage(JSON.stringify(msg));
        };
        Entity.prototype.toString = function () {
            return this.getKind();
        };
        Entity.prototype.playAnimation = function (str) {
            var msg = {
                type: "entity_animation",
                data: {
                    entity_id: this.getId(),
                    animation: str,
                    from_interpreter: "true"
                }
            };
            SendUnityMessage(JSON.stringify(msg));
        };
        return Entity;
    })();
    Voxels.Entity = Entity;
    var Location = (function () {
        function Location(x, y, z) {
            this.game_data = voxelsAPI.game_data;
            this._x = x;
            this._y = y;
            this._z = z;
            this._yaw = 0;
        }
        Location.prototype.getX = function () { return this._x; };
        Location.prototype.getY = function () { return this._y; };
        Location.prototype.getZ = function () { return this._z; };
        Location.prototype.toJSON = function () {
            return this.toString();
        };
        Location.prototype.toString = function () {
            return "[" + this.getX() + "," + this.getY() + "," + this.getZ() + "]";
        };
        Location.prototype.toArray = function () {
            return [this._x, this._y, this._z];
        };
        Location.prototype.setYaw = function (degrees) {
            this._yaw = degrees;
        };
        Location.prototype.getYaw = function () {
            return this._yaw;
        };
        Location.prototype.add = function (loc) {
            var ret = this.copy();
            ret._x += loc.getX();
            ret._y += loc.getY();
            ret._z += loc.getZ();
            return ret;
        };
        Location.prototype.times = function (s) {
            var ret = this.copy();
            ret._x *= s;
            ret._y *= s;
            ret._z *= s;
            return ret;
        };
        Location.prototype.distanceTo = function (other) {
            var xdif = this._x - other._x;
            xdif *= xdif;
            var ydif = this._y - other._y;
            ydif *= ydif;
            var zdif = this._z - other._z;
            zdif *= zdif;
            var ret = Math.sqrt(xdif + ydif + zdif);
            //log("Distance ("+this._x+","+this._y+","+this._z+") to ("+other._x+","+other._y+","+other._z+") is " + ret)
            return ret;
        };
        Location.prototype.rotateLeft90 = function () {
            var ret = this.copy();
            ret._x = -this._z;
            ret._y = this._y;
            ret._z = this._x;
            return ret;
        };
        Location.prototype.rotateRight90 = function () {
            var ret = this.copy();
            ret._x = this._z;
            ret._y = this._y;
            ret._z = -this._x;
            return ret;
        };
        Location.prototype.copy = function () {
            return new Location(this._x, this._y, this._z);
        };
        Location.prototype.subtract = function (other) {
            var loc = this.copy();
            loc._x -= other._x;
            loc._y -= other._y;
            loc._z -= other._z;
            return loc;
        };
        Location.prototype.multiply = function (n) {
            var loc = this.copy();
            loc._x *= n;
            loc._y *= n;
            loc._z *= n;
            return loc;
        };
        return Location;
    })();
    Voxels.Location = Location;
    var Voxel = (function () {
        function Voxel(loc, name) {
            this._loc = loc;
            this._name = name;
        }
        Voxel.prototype.getName = function () {
            return this._name;
        };
        Voxel.prototype.getLocation = function () {
            return this._loc.copy();
        };
        Voxel.prototype.toString = function () {
            return "{\"name\":\"" + this.getName() + "\", \"location\":[" + this.getLocation().toString() + "]}";
        };
        return Voxel;
    })();
    Voxels.Voxel = Voxel;
    var GameData = (function () {
        function GameData(vs) {
            this.voxels = vs;
            if (this.voxels == undefined)
                this.voxels = [];
            if (this.voxel_indexes == undefined)
                this.voxel_indexes = {}; //Maps location in world to position in this.voxels list
            this.entities = [];
            this.player = undefined; // Gets set in the Player constructor (bad?)
        }
        GameData.prototype.print = function () {
            this.eachVoxel(function (v) { console.log(v.toString()); });
        };
        GameData.prototype.eachVoxel = function (fun) {
            for (var key in this.voxels) {
                fun(this.voxels[key]);
            }
        };
        GameData.prototype.addVoxel = function (v, skip_callback) {
            var i = this.voxel_indexes[v.getLocation().toString()];
            if (!i) {
                this.voxel_indexes[v.getLocation().toString()] = this.voxels.length;
                this.voxels.push(v);
            }
            else {
                this.voxels[i] = v;
            }
            if (!skip_callback) {
                if (voxelsAPI._callbacks) {
                    voxelsAPI._callbacks.onVoxelAdd(v);
                }
            }
        };
        GameData.prototype.getVoxelFromLocation = function (loc) {
            var ret = undefined;
            var i = this.voxel_indexes[loc.toString()];
            if (i == undefined) {
                //If we don't know about a block.  Use the simple terrain gen algorithm to figure it out.
                var mat = "dirt";
                if (loc.getY() >= 210)
                    mat = "air";
                ret = new Voxels.Voxel(loc.copy(), mat);
            }
            else {
                ret = this.voxels[i];
            }
            return ret;
        };
        GameData.prototype.spawnEntity = function (loc, kind, skip_callback, asset, id) {
            if (!kind)
                kind = "pig";
            var hack_loc = loc.copy();
            var entity = new Voxels.Entity(hack_loc, kind, id);
            entity.asset = asset;
            this.entities.push(entity);
            if (!skip_callback) {
                if (voxelsAPI._callbacks) {
                    voxelsAPI._callbacks.onEntityAdd(entity);
                }
            }
            return entity;
        };
        GameData.prototype.toJSON = function () {
            return "{\n" +
                "\"voxels\":" + ArrayToJSON(this.voxels) + "\n" +
                "}";
        };
        GameData.prototype.changeWorld = function (world_name, min, max) {
            throw ("Not implemented");
            //TODO: Replace this iwth the new code.  See world artifacts
            /*
            var min_chunk = min || "0_0_0";
            var max_chunk = max || "0_0_0";
            var msg = {
              type: "world_prefetch",
              data: {
                entity_id: "world_synchronizer",
                min_chunk: min_chunk, // "-12_9_-9",
                max_chunk: max_chunk, // "-3_17_2",
                url: "http://d2z8e5fgltugz2.cloudfront.net/"+world_name+"/json_chunks/",
                from_interpreter: "true"
              }
            }
      
            SendUnityMessage( JSON.stringify(msg))
            */
        };
        GameData.prototype.strikeLightning = function (loc) {
            world.spawnEntity(loc, "lightning");
        };
        GameData.prototype.changeLighting = function (level) {
            var msg = {
                type: "world_lighting",
                data: {
                    entity_id: "world_synchronizer",
                    level: level,
                    from_interpreter: "true",
                }
            };
            SendUnityMessage(JSON.stringify(msg));
        };
        GameData.prototype.changeSky = function (asset) {
            var msg = {
                type: "sky_change",
                data: {
                    entity_id: "world_synchronizer",
                    asset: asset,
                    from_interpreter: "true",
                }
            };
            SendUnityMessage(JSON.stringify(msg));
        };
        return GameData;
    })();
    Voxels.GameData = GameData;
})(Voxels || (Voxels = {}));
/**** Drones ****/
var Drone = (function () {
    function Drone(p) {
        var loc = p.getLookLocation();
        this._loc = new Voxels.Location(loc.getX(), loc.getY(), loc.getZ());
        this._dir = p.direction;
    }
    Drone.prototype.getLocation = function () { return this._loc.copy(); };
    Drone.prototype.setLocation = function (l) {
        this._loc = l.copy();
    };
    Drone.prototype.getDir = function () { return this._dir; };
    Drone.prototype.turn = function (times) {
        for (var i = 0; i < times; i++) {
            this._dir = this.getDir().rotateRight90();
        }
    };
    Drone.prototype.move = function (dir, amount) {
        if (amount == undefined)
            amount = 1;
        var diff = new Voxels.Location(dir.getX(), dir.getY(), dir.getZ());
        diff = diff.times(amount);
        this._loc = this._loc.add(diff);
    };
    Drone.prototype.up = function (amount) {
        this.move(new Voxels.Location(0, 1, 0), amount);
    };
    Drone.prototype.down = function (amount) {
        this.move(new Voxels.Location(0, -1, 0), amount);
    };
    Drone.prototype.fwd = function (amount) {
        this.move(this.getDir(), amount);
    };
    Drone.prototype.back = function (amount) {
        this.move(this.getDir().times(-1), amount);
    };
    Drone.prototype.left = function (amount) {
        this.move(this.getDir().rotateLeft90(), amount);
    };
    Drone.prototype.right = function (amount) {
        this.move(this.getDir().rotateLeft90().times(-1), amount);
    };
    Drone.prototype.box = function (kind) {
        voxelsAPI.game_data.addVoxel(new Voxels.Voxel(this._loc.copy(), kind), false);
    };
    Drone.prototype.mob = function (kind) {
        voxelsAPI.game_data.spawnEntity(this._loc, kind);
    };
    return Drone;
})();
/**** Items/Inventory ****/
var ItemStack = (function () {
    function ItemStack(kind, num) {
        this.kind = kind;
        this.num = num;
    }
    return ItemStack;
})();
var Inventory = (function () {
    function Inventory() {
    }
    Inventory.prototype.addItem = function (stacks) {
        var msg = {
            type: "entity_equip",
            data: {
                entity_id: local_player_id,
                item_type: stacks[0].kind,
                item_number: stacks[0].num,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Inventory.prototype.remove = function (material) {
        var msg = {
            type: "entity_remove_item",
            data: {
                entity_id: local_player_id,
                item_type: material,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Inventory.prototype.clear = function () {
        var msg = {
            type: "entity_remove_item",
            data: {
                entity_id: local_player_id,
                item_type: ".*",
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    return Inventory;
})();
var Player = (function () {
    function Player(voxelsAPI) {
        voxelsAPI.game_data.player = this;
        this.loc = new Voxels.Location(0, 0, 0);
        this.location = new Voxels.Location(0, 0, 0);
        this.look_loc = new Voxels.Location(0, 0, 0);
        this.direction = new Voxels.Location(0, 0, 1);
    }
    Player.prototype.setLootWindowItems = function (items) {
        var msg = {
            type: "loot_window_fill",
            data: {
                entity_id: local_player_id,
                items: items,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Player.prototype.setLocation = function (new_loc) {
        this.loc = new_loc;
        this.location = new_loc;
        if (voxelsAPI._callbacks) {
            voxelsAPI._callbacks.onEntityMove(this);
        }
    };
    Player.prototype.teleport = function (new_loc) {
        this.setLocation(new_loc);
    };
    Player.prototype.getId = function () {
        return "player";
    };
    //me.getInventory().addItem([new ItemStack(Material.DIAMOND_PICKAXE,3)]);
    Player.prototype.getInventory = function () {
        return new Inventory();
    };
    //Same as sendMessage, but it acts like a real chat and broadcasts on the network, also triggers chat events so mods can handle them.
    Player.prototype.sendMessageAndBroadcast = function (message) {
        var msg = {
            type: "console_log",
            data: {
                entity_id: "console",
                message: message
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Player.prototype.sendMessage = function (message) {
        var msg = {
            type: "console_log",
            data: {
                entity_id: "console",
                message: message,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Player.prototype.addPotionEffect = function (effect) {
        var msg = {
            type: "entity_potion",
            data: {
                entity_id: local_player_id,
                effect_name: effect.name,
                effect_duration: effect.duration,
                effect_strength: effect.strength,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    Player.prototype.setResourcePack = function (url) {
        var pack_name = url.match(/[/]([^/]+)$/)[1];
        var base = "http://" + url.match(/http:\/\/([^/]+)/)[1];
        var block_maps = [
            ["Bedrock", "blocks-bedrock.png"],
            ["Bookshelf", "blocks-bookshelf.png"],
            ["CactusSide", "blocks-cactus_side.png"],
            ["CactusTop", "blocks-cactus_top.png"],
            ["Cake", "blocks-cake_top.png"],
            ["Clay", "blocks-clay.png"],
            ["Bedrock", "blocks-bedrock.png"],
            ["Cobblestone", "blocks-cobblestone.png"],
            ["Diamond", "blocks-diamond_block.png"],
            ["Dirt", "blocks-dirt.png"],
            ["DoubleStoneSlab", "blocks-stone_slab_top.png"],
            ["Emerald", "blocks-emerald_block.png"],
            ["DiamondOre", "blocks-diamond_ore.png"],
            ["EmeraldOre", "blocks-emerald_ore.png"],
            ["Glass", "blocks-glass.png"],
            ["Glowstone", "blocks-glowstone.png"],
            ["Gold", "blocks-gold_block.png"],
            ["GoldOre", "blocks-gold_ore.png"],
            ["GrassSide", "blocks-grass_side.png"],
            ["GrassTop", "blocks-grass_top.png"],
            ["JackOLantern", "blocks-pumpkin_face_on.png"],
            ["Lava", "blocks-lava_still.png"],
            ["Leaves", "blocks-leaves_acacia.png"],
            ["MelonSide", "blocks-melon_side.png"],
            ["MelonTop", "blocks-melon_top.png"],
            ["MossyCobblestone", "blocks-cobblestone_mossy.png"],
            ["Obsidian", "blocks-obsidian.png"],
            ["PumpkinSide", "blocks-pumpkin_side.png"],
            ["PumpkinTop", "blocks-pumpkin_top.png"],
            ["Quartz", "blocks-quartz_block_side.png"],
            ["Sand", "blocks-sand.png"],
            ["Sandstone", "blocks-sandstone_normal.png"],
            ["Stone", "blocks-stone.png"],
            ["TNT", "blocks-tnt_side.png"],
            ["Wood", "blocks-log_acacia.png"]
        ];
        for (var i in block_maps) {
            var block_map = block_maps[i];
            SendUnityMessage("{\"type\": \"define_block_texture\",\"data\": {\"name\": \"" + block_map[0] + "\", \"url\": \"" + base + "/editable_images/" + pack_name + "-" + block_map[1] + "\", \"entity_id\": \"texture_synchronizer\"}}");
        }
        var item_maps = [
            ["GoldSword", "items-gold_sword.png"],
            ["DiamondSword", "items-diamond_sword.png"],
            ["IronSword", "items-iron_sword.png"],
            ["StoneSword", "items-stone_sword.png"],
            ["WoodSword", "items-wood_sword.png"],
            ["GoldAxe", "items-gold_pickaxe.png"],
            ["DiamondAxe", "items-diamond_pickaxe.png"],
            ["IronAxe", "items-iron_pickaxe.png"],
            ["StoneAxe", "items-stone_pickaxe.png"],
            ["WoodAxe", "items-wood_pickaxe.png"]
        ];
        for (var i in item_maps) {
            var item_map = item_maps[i];
            SendUnityMessage("{\"type\": \"define_item_texture\",\"data\": {\"name\": \"" + item_map[0] + "\", \"url\": \"" + base + "/editable_images/" + pack_name + "-" + item_map[1] + "\", \"entity_id\": \"texture_synchronizer\"}}");
        }
    };
    Player.prototype.updateInventory = function () {
        //Dummy
    };
    Player.prototype.getLocation = function () {
        return this.loc.copy();
    };
    Player.prototype.getLookLocation = function () {
        return this.look_loc.copy();
    };
    Player.prototype.getForward = function () {
        return this.forward.copy();
    };
    Player.prototype.getTargetBlock = function () {
        return world.getVoxelFromLocation(this.look_loc.copy());
    };
    Player.prototype.performCommand = function (str) {
        var parts = str.split(" ");
        var command = parts.shift();
        if (command == "set") {
            var key = parts[0];
            var val = parts[1];
            WritePersistentData("main", key, val); //TEMPORARY>  REMOVE main
            return;
        }
        if (command == "summon") {
            world.spawnEntity(this.getLocation(), parts[0].toLowerCase());
            return;
        }
        if (command == "give") {
            var num = 0;
            if (parts[2])
                num = parseInt(parts[2]);
            me.getInventory().addItem([new ItemStack(parts[1], num)]);
            return;
        }
        if (command == "place") {
            var loc = new Voxels.Location(parseInt(parts[1]), parseInt(parts[2]), parseInt(parts[3]));
            voxelsAPI.game_data.addVoxel(new Voxels.Voxel(loc, parts[0]), false);
            return;
        }
        if (command == "effect") {
            var duration = 1;
            if (parts[2])
                duration = parseInt(parts[2]);
            var strength = 1;
            if (parts[3])
                strength = parseInt(parts[3]);
            me.addPotionEffect(new PotionEffect(parts[1], duration, strength));
            return;
        }
    };
    return Player;
})();
var music;
(function (music) {
    function volumeChange(num) {
        var msg = {
            type: "volume_change",
            data: {
                entity_id: "music",
                volume: num,
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    }
    music.volumeChange = volumeChange;
})(music || (music = {}));
var schematics;
(function (schematics) {
    //Only works in the interpreter.  In the regualar browser you need to use pasteAsync
    function paste(name, loc) {
        var json = JSON.parse(ajaxGet("/schematics/" + name + ".json"));
        this.pasteRaw(json, loc);
    }
    schematics.paste = paste;
    function pasteAsync(name, loc, do_dump) {
        var self = this;
        $.get("/schematics/" + name + ".json", function (d) {
            if (do_dump)
                window.message_bus.dumpBegin();
            self.pasteRaw(d, loc);
            if (do_dump)
                window.message_bus.dumpEnd();
        });
    }
    schematics.pasteAsync = pasteAsync;
    function pasteRaw(json, loc) {
        for (var i in json) {
            var thing = json[i];
            var x = thing.x;
            var y = thing.y;
            var z = thing.z;
            var name = thing.name;
            var rel_loc = new Voxels.Location(x, y, z);
            var new_loc = loc.add(rel_loc);
            voxelsAPI.game_data.addVoxel(new Voxels.Voxel(new_loc, name));
        }
    }
    schematics.pasteRaw = pasteRaw;
})(schematics || (schematics = {}));
var armorstand;
(function (armorstand) {
    function entities(entity, range) {
        var ret = [];
        for (var i in voxelsAPI.game_data.entities) {
            var other = voxelsAPI.game_data.entities[i];
            if (other.getId() != entity.getId() && other.loc.distanceTo(entity.loc) <= range) {
                ret.push(other);
            }
            else {
            }
        }
        return ret;
    }
    armorstand.entities = entities;
})(armorstand || (armorstand = {}));
var PotionEffect = (function () {
    function PotionEffect(n, d, s) {
        this.name = n;
        this.duration = d;
        this.strength = s;
    }
    return PotionEffect;
})();
var PotionEffectType = (function () {
    function PotionEffectType() {
    }
    PotionEffectType.SPEED = "speed";
    PotionEffectType.CONFUSION = "confusion";
    PotionEffectType.ABSORPTION = "absorption";
    PotionEffectType.BLINDNESS = "blindness";
    PotionEffectType.DAMAGE_RESISTANCE = "damage resistance";
    PotionEffectType.FAST_DIGGING = "fast digging";
    PotionEffectType.FIRE_RESISTANCE = "fire resistance";
    PotionEffectType.HEALTH_BOOST = "health boost";
    PotionEffectType.HUNGER = "hunger";
    PotionEffectType.INCREASE_DAMAGE = "increase damage";
    PotionEffectType.JUMP = "jump";
    PotionEffectType.NIGHT_VISION = "night vision";
    PotionEffectType.POISON = "poison";
    PotionEffectType.REGENERATION = "regeneration";
    PotionEffectType.SATURATION = "saturation";
    PotionEffectType.SLOW = "slow";
    PotionEffectType.INVISIBILITY = "invisibility";
    PotionEffectType.WATER_BREATHING = "water breathing";
    PotionEffectType.WEAKNESS = "weakness";
    PotionEffectType.WITHER = "wither";
    return PotionEffectType;
})();
/***** AI / citizens ******/
var NPC = (function () {
    function NPC(e) {
        this.entity = e;
        this.invulnerable = false;
        this.name = "Bob";
        this.spawned = false;
    }
    NPC.prototype.getNavigator = function () {
        return this;
    };
    NPC.prototype.setTarget = function (thing, aggressive) {
        if (thing.getId)
            thing = thing.getId();
        var msg = {
            type: "entity_ai",
            data: {
                entity_id: this.entity.getId(),
                target: thing,
                aggressive: !!aggressive,
                from_interpreter: "true"
            }
        };
        var that = this;
        if (that.spawned) {
            SendUnityMessage(JSON.stringify(msg));
        }
        else {
            events.when("entity.EntitySpawnEvent", function (info) {
                if (that.spawned)
                    return;
                if (info.entity_id != that.entity.id)
                    return;
                that.spawned = true;
                SendUnityMessage(JSON.stringify(msg));
            });
        }
    };
    return NPC;
})();
var citizens;
(function (citizens) {
    function citizen(type, name, loc, invulnerable) {
        var e = world.spawnEntity(loc, type, false, type, name /*entity id*/);
        var n = new NPC(e);
        n.name = name;
        n.invulnerable = invulnerable;
        return n;
    }
    citizens.citizen = citizen;
})(citizens || (citizens = {}));
var UniBlocksEvents = (function () {
    function UniBlocksEvents() {
    }
    UniBlocksEvents.prototype.onVoxelAdd = function (v) {
        var msg = {
            type: "block_change",
            data: {
                location: v.getLocation().toArray(),
                material: v.getName(),
                entity_id: "chunk_synchronizer",
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
    };
    UniBlocksEvents.prototype.onEntityAdd = function (e) {
        var msg = {
            type: "entity_movement",
            data: {
                location: e.getLocation().toArray(),
                velocity: [0, 0, 0],
                entity_type: e.getKind(),
                entity_id: e.getId(),
                from_interpreter: "true"
            }
        };
        if (e.asset) {
            msg.data.asset = e.asset;
        }
        SendUnityMessage(JSON.stringify(msg));
    };
    UniBlocksEvents.prototype.onEntityMove = function (e) {
        var loc = e.getLocation();
        var msg = {
            type: "entity_movement",
            data: {
                location: loc.toArray(),
                velocity: [0, 0, 0],
                entity_type: e.getKind(),
                entity_id: e.getId(),
                from_interpreter: "true"
            }
        };
        SendUnityMessage(JSON.stringify(msg));
        if (loc.getYaw()) {
            var msg2 = {
                type: "entity_rotation",
                data: {
                    rotation_angles: [0, loc.getYaw(), 0],
                    entity_type: e.getKind(),
                    entity_id: e.getId(),
                    from_interpreter: "true"
                }
            };
            SendUnityMessage(JSON.stringify(msg2));
        }
    };
    return UniBlocksEvents;
})();
//Initialize the runtime APIs for manipulating the voxel engine.
var voxelsAPI = new VoxelState();
var world = voxelsAPI.game_data;
voxelsAPI.setCallbacks(new UniBlocksEvents());
var me = new Player(voxelsAPI);
var events = new Events();
var Material = {
    DIAMOND_BLOCK: "diamond",
    IRON_SWORD: "iron_sword",
    DIAMOND_SWORD: "diamond_sword",
    IRON_PICKAXE: "iron_axe",
    DIAMOND_PICKAXE: "diamond_axe",
    WOOD_SWORD: "wood_sword",
    GOLD_SWORD: "gold_sword",
    STONE_SWORD: "stone_sword",
    GOLD_PICKAXE: "gold_axe",
    WOOD_PICKAXE: "wood_axe",
    STONE_PICKAXE: "stone_axe",
    WOOD: "wood",
    PUMPKIN: "pumpkin",
    STONE: "stone",
    SAND: "SAND",
    GRASS: "grass",
    GLASS: "glass",
    DIAMOND: "diamond",
    COBBLESTONE: "cobblestone",
    MOSSY_COBBLESTONE: "mossy_cobblestone",
    CAKE: "cake",
    CAKE_BLOCK: "cake",
    DIRT: "dirt",
    DIAMOND_ORE: "diamond_ore",
    GOLD_ORE: "gold_ore",
    GOLD_BLOCK: "gold",
    EMERALD_ORE: "emerald_ore",
    EMERALD_BLOCK: "emerald",
    OBSIDIAN: "obsidian",
    BRICK: "brick",
    BEDROCK: "bedrock",
    CLAY: "clay",
    GLOWSTONE: "glowstone",
    CACTUS: "cactus",
    MELON: "melon",
    MELON_BLOCK: "melon",
    JACK_O_LANTERN: "jackolantern",
    TNT: "tnt",
    BOOKSHELF: "bookshelf",
    QUARTZ_BLOCK: "block_of_quartz"
};
var EntityType = {
    PIG: "pig",
    COW: "cow",
    ZOMBIE: "zombie",
    CREEPER: "creeper",
    SHEEP: "sheep",
    ENDERMAN: "enderman",
};
function _setPlayerLookLocation(x, y, z) {
    me.look_loc = new Voxels.Location(x, y, z);
}
function _setPlayerForward(x, y, z) {
    me.forward = new Voxels.Location(x, y, z);
}
function _setPlayerLocation(x, y, z) {
    me.loc = new Voxels.Location(x, y, z);
    me.location = new Voxels.Location(x, y, z);
}
function _setPlayerFacingDirection(dir) {
    if (dir == "North")
        me.direction = new Voxels.Location(0, 0, 1);
    if (dir == "South")
        me.direction = new Voxels.Location(0, 0, -1);
    if (dir == "East")
        me.direction = new Voxels.Location(1, 0, 0);
    if (dir == "West")
        me.direction = new Voxels.Location(-1, 0, 0);
}
function _moveEntity(id, kind, x, y, z) {
    var loc = new Voxels.Location(x, y, z);
    for (var i in world.entities) {
        var m = world.entities[i];
        if (m.getId() == id) {
            m.loc = loc;
            return;
        }
    }
    var e = world.spawnEntity(loc, kind, true);
    e.id = id;
}
//Home-rolled implementation of setTimeout -- to run inside interpreter.
/*
   _setTimeout(function(){
     log("HERE!")
   }, 3000);
*/
//TODO: Optimize so that functions don't stick around forever
var DelayedFunction = (function () {
    function DelayedFunction(time, fun, once, wait) {
        this.time = time;
        this.fun = fun;
        this.done = false;
        this.once = once;
        this.wait = wait;
    }
    return DelayedFunction;
})();
var current_time = 0;
var timed_out_functions = [];
function _setTimeout(fun, wait) {
    timed_out_functions.push(new DelayedFunction(current_time + wait, fun, true));
}
function _setInterval(fun, wait) {
    timed_out_functions.push(new DelayedFunction(current_time + wait, fun, false, wait));
}
function handleTimedOutFunctions() {
    var to_keep = [];
    for (var i = 0; i < timed_out_functions.length; i++) {
        if (current_time >= timed_out_functions[i].time && !timed_out_functions[i].done) {
            if (timed_out_functions[i].once) {
                timed_out_functions[i].done = true;
            }
            else {
                timed_out_functions[i].time += timed_out_functions[i].wait;
            }
            timed_out_functions[i]["fun"]();
            if (!timed_out_functions[i].once)
                to_keep.push(timed_out_functions[i]);
        }
        else {
            to_keep.push(timed_out_functions[i]);
        }
    }
    timed_out_functions = to_keep;
}
//Maps the function taht runs in bukkit to the one that runs here, where "me" doesn't matter -- e.g. interval(me, function(){...}, 1000)
function interval(dummy, fun, wait) {
    _setInterval(fun, wait);
}
var actual_message_queue = undefined;
var message_queue = undefined;
var local_player_id = undefined;
function handleMessage(m) {
    events.handleEvent(m); //For user-defined callbacks
    if (m.type.indexOf("dump") >= 0) {
        for (var i in m.data.dump) {
            handleMessage(m.data.dump[i]);
        }
    }
    else if (m.type == "block_change") {
        var l = m.data.location;
        var loc = new Voxels.Location(l[0], l[1], l[2]);
        var mat = m.data.material;
        var vox = new Voxels.Voxel(loc, mat);
        world.addVoxel(vox, true);
    }
    else if (m.type == "entity_movement") {
        var loc2 = m.data.location;
        if (m.data.entity_id.indexOf("player") >= 0) {
            if (local_player_id == m.data.entity_id) {
                _setPlayerLocation(loc2[0], loc2[1], loc2[2]);
                var loc3 = m.data.forward;
                _setPlayerForward(loc3[0], loc3[1], loc3[2]);
            }
            else {
                //Shouldn't get here.
                log("Wrong");
            }
        }
        else {
            _moveEntity(m.data.entity_id, m.data.entity_type, loc2[0], loc2[1], loc2[2]);
        }
    }
    else if (m.type == "entity_look") {
        var loc3 = m.data.location;
        _setPlayerLookLocation(loc3[0], loc3[1], loc3[2]);
    }
    else if (m.type == "entity_facing") {
        _setPlayerFacingDirection(m.data.direction);
    }
}
function handleMessages() {
    actual_message_queue = JSON.parse(message_queue); //It comes in as a string from blockly_interpreter
    for (var i in actual_message_queue) {
        var m = actual_message_queue[i];
        handleMessage(m);
    }
    message_queue = undefined;
}
;
