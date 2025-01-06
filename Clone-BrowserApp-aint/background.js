chrome.app.runtime.onLaunched.addListener(function() {
  var cl_h = screen.height;
 var cl_w = screen.width;
 cl_w = Math.round(cl_w / 3);
chrome.app.window.create(
"index.html",
{
  bounds: {
      width: cl_w,
      height: cl_h,
      left: 0,
      top: 0
  },
id: "aintrte",
frame: "none",
//state: "maximized",
resizable: false
}
);
});