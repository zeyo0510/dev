var Core = function () {
    var moduleData = {};
  
    return {
        register: function (moduleID, creator) {
            moduleData[moduleID] = {
                creator: creator,
                instance: null
            }
        },
        start: function (moduleID) {

            moduleData[moduleID].instance = moduleData[moduleID].creator(new runtime(this));
            moduleData[moduleID].instance.init();


        },

        stop: function (moduleID) {
            var data = moduleData[moduleID];
            if (data.instance) {
                data.instance.destroy();
                data.instance = null;
            }
        },

        getModule: function (moduleID) {
            return moduleData[moduleID].instance;
        },
        updateLayout: function () {
            for (var i in moduleData) {
                if (moduleData[i].instance.updateLayout)
                    moduleData[i].instance.updateLayout();
            }
        }
       
       

    }
}();