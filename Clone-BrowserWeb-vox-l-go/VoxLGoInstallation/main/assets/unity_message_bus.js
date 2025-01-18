/// <reference path="/home/dev/LTM-Web/lib/assets/js/typings/tsd.d.ts" />
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
