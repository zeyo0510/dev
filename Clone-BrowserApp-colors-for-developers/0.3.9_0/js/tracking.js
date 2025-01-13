$(document).ready(function() {

    // You'll usually only ever have to create one service instance.
    var service = analytics.getService('ice_cream_app');

    // You can create as many trackers as you want. Each tracker has its own state
    // independent of other tracker instances.
    var tracker = service.getTracker('UA-62957601-2');  // Supply your GA Tracking ID.
    tracker.sendAppView('MainView');


    $(".color_data input").change(function(){
        tracker.sendEvent('Input', 'Change', $(this).attr("name"));
    });
    $(".color_data input").keyup(function(){
        tracker.sendEvent('Input', 'Write', $(this).attr("name"));
    });

    $(".preset").click(function(){
        tracker.sendEvent('Preset', 'Choose', $(this).attr("data-tracker"));
    });

    $(".regex").click(function(){
        tracker.sendEvent('Regex', 'Choose', $(this).attr("data-tracker"));
    });
});