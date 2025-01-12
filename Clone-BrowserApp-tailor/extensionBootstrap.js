(function(){
    require(["brackets", "utils/ExtensionLoader", "utils/Async"], function(brackets, extensionLoader, Async){
        
        var paths = {
            "text" : "../../thirdparty/text/text",
            "i18n" : "../../thirdparty/i18n/i18n",
            "hgn"  : "../../thirdparty/hgn",
            "hogan": "../../thirdparty/hogan"
        };

        var extensions = [];
        var processItem = function(item){
            if (item.name == "ProjectFromGit"){
                extensionLoader.loadExtension(item.name, item.data, item.module).always(function(){
                    extensionLoader.notifyPluginsLoaded();
                });
            
            }
            else{
                extensions.push(item);
            }
        }

       
        var item  = {name:"CSSCodeHints", data: { baseUrl: "extensions/CSSCodeHints", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"HTMLCodeHints", data: { baseUrl: "extensions/HTMLCodeHints", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"HtmlEntityCodeHints", data: { baseUrl: "extensions/HtmlEntityCodeHints", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"InlineColorEditor", data: { baseUrl: "extensions/InlineColorEditor", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"JavaScriptCodeHints", data: { baseUrl: "extensions/JavaScriptCodeHints", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"JavaScriptQuickEdit", data: { baseUrl: "extensions/JavaScriptQuickEdit", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"JSLint", data: { baseUrl: "extensions/JSLint", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"LESSSupport", data: { baseUrl: "extensions/LESSSupport", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"ProjectFromGit", data: { baseUrl: "extensions/ProjectFromGit", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"QuickOpenCSS", data: { baseUrl: "extensions/QuickOpenCSS", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"QuickOpenHTML", data: { baseUrl: "extensions/QuickOpenHTML", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"QuickOpenJavaScript", data: { baseUrl: "extensions/QuickOpenJavaScript", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"QuickView", data: { baseUrl: "extensions/QuickView", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"RecentProjects", data: { baseUrl: "extensions/RecentProjects", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"UrlCodeHints", data: { baseUrl: "extensions/UrlCodeHints", paths: paths}, module: "main"};
        processItem(item);
        var item  = {name:"WebPlatformDocs", data: { baseUrl: "extensions/WebPlatformDocs", paths: paths}, module: "main"};
        processItem(item);

        Async.doInParallel(extensions, function (item) {
            return extensionLoader.loadExtension(item.name, item.data, item.module);
        }).always(function(){
            //extensionLoader.notifyPluginsLoaded();
        });
    });
})();