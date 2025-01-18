/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/ltm.d.ts" />
/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/jquery/jquery.d.ts" />
/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/unity_message_bus.js.d.ts" />
/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/voxel_library.js.d.ts" />
var BlockTracker = (function () {
    function BlockTracker() {
        this.items_added = [];
        this.blocks_added = [];
        this.texture_map = {};
    }
    BlockTracker.prototype.defineBlockTexture = function (name, url) {
        if (this.texture_map[url])
            return;
        this.texture_map[url] = name;
        window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_block_texture\",\"data\": {\"name\": \"" + name + "\", \"url\": \"" + url + "\", \"entity_id\": \"texture_synchronizer\"}}");
    };
    BlockTracker.prototype.addSimpleItem = function (options) {
        var item_name = options.name.replace(/[ _]/g, "");
        this.items_added.push(item_name);
        window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_item_texture\",\"data\": {\"name\": \"" + item_name + "\", \"url\": \"" + options.icon_url + "\", \"entity_id\": \"texture_synchronizer\"}}");
        window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_item\",\"data\": {\"name\": \"" + options.name + "\", \"asset\": \"" + options.asset_name + "\", \"icon\": \"" + item_name + "\", \"entity_id\": \"item_synchronizer\"}}");
    };
    BlockTracker.prototype.addMultiSidedBlock = function (options) {
        var block_name = options.name.replace(/[ _]/g, "");
        this.blocks_added.push(block_name);
        var texture_names = [];
        for (var i in options.textures) {
            var texture_path = options.textures[i];
            var texture_name = this.texture_map[texture_path] || (block_name + i);
            texture_names.push(texture_name);
            this.defineBlockTexture(texture_name, texture_path);
        }
        window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_block\",\"data\": {\"name\": \"" + options.name + "\", \"textures\": " + JSON.stringify(texture_names) + ", \"entity_id\": \"block_synchronizer\"}}");
    };
    BlockTracker.prototype.addSimpleBlock = function (options) {
        var new_opts = {
            name: options.name,
            textures: [options.texture]
        };
        this.addMultiSidedBlock(new_opts);
    };
    //This is a special kind of block that has associated mobs that spawn with it and are removed with it
    BlockTracker.prototype.addMobBlock = function (options) {
        var block_name = options.name.replace(/ /g, "");
        var block_texture = options.texture;
        var assets = options.assets;
        this.blocks_added.push(block_name);
        this.defineBlockTexture(block_name, block_texture);
        window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_block\",\"data\": {\"name\": \"" + block_name + "\", \"textures\": [\"" + this.texture_map[block_texture] + "\"], \"entity_id\": \"block_synchronizer\"}}");
        for (var i in assets) {
            var asset = assets[i];
            window.message_bus.sendToUnity({ receive: function () { } }, "{\"type\": \"define_mob\",\"data\": {\"name\": \"" + asset.name + "\", \"asset\": \"" + asset.name + "\", \"entity_id\": \"mob_synchronizer\"}}");
        }
        var placed = {};
        var that = this;
        function place_associated_mobs(info) {
            if (info.material.toLowerCase().replace(/[_ ]/g, "") != block_name.toLowerCase().replace(/[_ ]/g, ""))
                return;
            for (var i in assets) {
                var asset = assets[i];
                register(info.location, world.spawnEntity((info.location), asset.name));
            }
        }
        function remove_associated_mobs(info) {
            var mobs = placed[that.locationToKey(info.location)];
            if (mobs && mobs.length > 0) {
                mobs.map(function (e) { e.destroy(); });
            }
        }
        function register(loc, entity) {
            if (!placed[that.locationToKey(loc)])
                placed[that.locationToKey(loc)] = [];
            placed[that.locationToKey(loc)].push(entity);
        }
        events.when(('block.BlockPlaceEvent'), (place_associated_mobs), me);
        events.when(('block.BlockBreakEvent'), (remove_associated_mobs), me);
    };
    BlockTracker.prototype.locationToKey = function (loc) {
        return Math.floor(loc.getX()) + " " + Math.floor(loc.getY()) + " " + Math.floor(loc.getZ());
    };
    return BlockTracker;
})();
var ChestTracker = (function () {
    function ChestTracker() {
    }
    ChestTracker.prototype.persist = function () {
        WritePersistentData("main", "chests", JSON.stringify(this.chest_contents));
    };
    ChestTracker.prototype.setupChests = function () {
        this.chest_contents = {};
        var success = function (d) {
            if (d != null)
                this.chest_contents = JSON.parse(d);
        }.bind(this);
        var error = function () {
            this.chest_contents = {};
        }.bind(this);
        ReadPersistentData("main", "chests", success, error);
        //When a chest block is palced, initialize a memory store for that locaiton.
        events.when(('block.BlockPlaceEvent'), function (info) {
            if (!info.material.match(/Chest/i))
                return;
            this.placeChest(info.location);
        }.bind(this), me);
        //When a chest is interacted with, display the loot window based on memory at that location
        events.when(('player.PlayerInteractEvent'), function (info) {
            if (!info["block_clicked"])
                return;
            if (!info["block_clicked"]["material"].match(/Chest/i))
                return;
            var loc = this.locationToKey(info["block_clicked"]["location"]);
            this.last_chest_interaction = info["block_clicked"]["location"];
            if (!this.chest_contents[loc])
                return;
            me.setLootWindowItems(this.chest_contents[loc]);
        }.bind(this), me);
        //On window close (or on add to window), update items at memory location
        events.when(('window.LootChange'), function (info) {
            this.emptyChest(this.last_chest_interaction);
            for (var i in info.contents) {
                this.addToChest(this.last_chest_interaction, info.contents[i]);
            }
        }.bind(this), me);
        //On destroy, spew items.  Clear location memory
        events.when(('block.BlockBreakEvent'), function (info) {
            var items = this.getChestContents(info.location);
            if (!items)
                return;
            for (var i in items) {
                for (var j = 0; j < items[i].amount; j++) {
                    var d = (new Drone(me));
                    var s = items[i].name + '_pickup';
                    d.up(1);
                    d.mob(s);
                }
            }
            delete (this.chest_contents[this.locationToKey(info.location)]);
        }.bind(this), me);
        //Define the actual chest block
        BlockTracker.main.addMultiSidedBlock({ name: "Chest", textures: ["https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_top.png",
                "https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_side.png",
                "https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_side.png",
                "https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_side.png",
                "https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_front.png",
                "https://s3-us-west-1.amazonaws.com/voxel-engine/NewTextures/chest3_side.png"
            ] });
    };
    ChestTracker.prototype.placeChest = function (loc) {
        this.chest_contents[this.locationToKey(loc)] = [];
        this.persist();
    };
    ChestTracker.prototype.emptyChest = function (loc) {
        this.chest_contents[this.locationToKey(loc)] = [];
        this.persist();
    };
    ChestTracker.prototype.addToChest = function (loc, thing) {
        this.chest_contents[this.locationToKey(loc)].push(thing);
        this.persist();
    };
    ChestTracker.prototype.getChestContents = function (loc) {
        return this.chest_contents[this.locationToKey(loc)];
    };
    ChestTracker.prototype.locationToKey = function (loc) {
        return Math.floor(loc.getX()) + " " + Math.floor(loc.getY()) + " " + Math.floor(loc.getZ());
    };
    return ChestTracker;
})();
var VoxelMessager = (function () {
    function VoxelMessager() {
        this.funs = [];
        $(document).on("vox_l_loaded", function () {
            this.doFuns({ as_dump: true });
        }.bind(this));
    }
    VoxelMessager.prototype.doFuns = function (options) {
        if (options.as_dump) {
            window.message_bus.dumpBegin();
        }
        for (var i in this.funs) {
            this.funs[i]();
        }
        if (options.as_dump) {
            window.message_bus.dumpEnd();
        }
    };
    VoxelMessager.prototype.onVoxlLoad = function (fun) {
        this.funs.push(fun);
    };
    return VoxelMessager;
})();
var RecipeTracker = (function () {
    function RecipeTracker() {
    }
    RecipeTracker.prototype.addRecipe = function (name, output_name, output_amount, input) {
        var msg = {
            type: "define_recipe",
            data: {
                entity_id: "item_synchronizer",
                name: name,
                output_name: output_name,
                output_amount: output_amount,
                input: input
            }
        };
        window.message_bus.sendToUnity({ receive: function () { } }, JSON.stringify(msg));
    };
    RecipeTracker.prototype.addSwordRecipe = function (material) {
        var recipe_name = material + " Sword";
        RecipeTracker.main.addRecipe(recipe_name, recipe_name, 1, [
            [{ name: material, amount: 1 }, {}, {}],
            [{ name: material, amount: 1 }, {}, {}],
            [{ name: "Stick", amount: 1 }, {}, {}],
        ]);
        RecipeTracker.main.addRecipe(recipe_name, recipe_name, 1, [
            [{}, { name: material, amount: 1 }, {}],
            [{}, { name: material, amount: 1 }, {}],
            [{}, { name: "Stick", amount: 1 }, {}],
        ]);
        RecipeTracker.main.addRecipe(recipe_name, recipe_name, 1, [
            [{}, {}, { name: material, amount: 1 }],
            [{}, {}, { name: material, amount: 1 }],
            [{}, {}, { name: "Stick", amount: 1 }],
        ]);
    };
    RecipeTracker.prototype.addAxeRecipe = function (material) {
        var recipe_name = material + " Axe";
        RecipeTracker.main.addRecipe(recipe_name, recipe_name, 1, [
            [{ name: material, amount: 1 }, { name: material, amount: 1 }, {}],
            [{ name: material, amount: 1 }, { name: "Stick", amount: 1 }, {}],
            [{}, { name: "Stick", amount: 1 }, {}],
        ]);
        RecipeTracker.main.addRecipe(recipe_name, recipe_name, 1, [
            [{}, { name: material, amount: 1 }, { name: material, amount: 1 }],
            [{}, { name: material, amount: 1 }, { name: "Stick", amount: 1 }],
            [{}, {}, { name: "Stick", amount: 1 }],
        ]);
    };
    return RecipeTracker;
})();
var ItemTracker = (function () {
    function ItemTracker() {
        this.setupItemTracker();
    }
    ItemTracker.prototype.setupItemTracker = function () {
        events.when(('window.ItemAdded'), function (info) {
            if (info.window_name != "character")
                return;
            if (info.window_slot != 11)
                return;
            this.item_in_hand = { name: info.item_name, amount: info.item_amount };
        }.bind(this), me);
        events.when(('window.ItemRemoved'), function (info) {
            if (info.window_name != "character")
                return;
            if (info.window_slot != 11)
                return;
            this.item_in_hand = null;
        }.bind(this), me);
    };
    ItemTracker.prototype.itemInHand = function () {
        return this.item_in_hand;
    };
    return ItemTracker;
})();
var RecipeTracker;
(function (RecipeTracker) {
    RecipeTracker.main = new RecipeTracker();
})(RecipeTracker || (RecipeTracker = {}));
var BlockTracker;
(function (BlockTracker) {
    BlockTracker.main = new BlockTracker();
})(BlockTracker || (BlockTracker = {}));
var ChestTracker;
(function (ChestTracker) {
    ChestTracker.main = new ChestTracker();
})(ChestTracker || (ChestTracker = {}));
var ItemTracker;
(function (ItemTracker) {
    ItemTracker.main = new ItemTracker();
})(ItemTracker || (ItemTracker = {}));
var VoxelMessager;
(function (VoxelMessager) {
    VoxelMessager.main = new VoxelMessager();
})(VoxelMessager || (VoxelMessager = {}));
