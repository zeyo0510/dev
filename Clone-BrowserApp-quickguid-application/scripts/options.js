var options = function () {
    this.IsBracesOn = true;
    this.IsUpperCase = false;
    this.OmitHyphens = false;
    this.GUIDType = 1;
    setOption = function (key, data, $options) {
        var storage = chrome.storage.local;
        storage.get('options', function (store) {
            var newProp = {};
            newProp[key] = data;
            $.extend($options, store.options, newProp);
            $options.saveOptions();
        })
    };
}

options.prototype = {
    saveOptions: function (callback) {
        var storage = chrome.storage.local;
        var $options = this;
        var store = {};
        store['options'] = $options;
        storage.set(store, function () {
            if (typeof callback === 'function') {
                callback($options);
            }
        });
    },
    loadOptions: function (callback) {
        var storage = chrome.storage.local;
        var $options = this;
        storage.get('options', function (store) {
            if (typeof store.options !== 'undefined') {
                $.extend($options, store.options);
                if (typeof callback === 'function') {
                    callback($options);
                }
            }
            else {
                $options.saveOptions(callback);
            }
        });
    },
    removeOptions: function () {
        var storage = chrome.storage.local;
        storage.remove('options');
    },
    setGUIDType: function (value) {
        setOption('GUIDType', value, this);
    },
    setIsBracesOn: function (value) {
        setOption('IsBracesOn', value, this);
    },
    setIsUpperCase: function (value) {
        setOption('IsUpperCase', value, this);
    },
    setOmitHyphens: function (value) {
        setOption('OmitHyphens', value, this);
    }
}

var newOptions = new options();