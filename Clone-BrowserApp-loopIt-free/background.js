chrome.app.runtime.onLaunched.addListener(function(data) {
  chrome.app.window.create('page.html', {
    "frame": {
      type: "chrome",
      color: "#FFF",
      //inactiveColor: "#FAFAFA"
    },
    "bounds": {
      width:1000, 
      height:600}, minWidth:1000, minHeight:600,
    "resizable": true,
    id:"LoopIt - Free"});
});