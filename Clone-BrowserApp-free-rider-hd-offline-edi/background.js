chrome.app.runtime.onLaunched.addListener(function() {
  
  chrome.app.window.create('app.html', 

  {
    'frame':{type:'none'},
  	'id':'main',
	  'minWidth': 500,
    'minHeight': 300,
    'bounds':{
    	'width':900,
    	'height':600
    }
  },

  function(createdWindow){
    
    createdWindow.contentWindow.isChromeApp = true;
    
  });

});