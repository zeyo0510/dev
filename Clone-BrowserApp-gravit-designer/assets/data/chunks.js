var fs = require('fs');
fs.readFile('previews.json' , function (err, data) {
	var previews = JSON.parse(data);
	// remove open sans
	previews.splice(10,1);
	var CHUNKS = 10;
	var chunkNum = previews.length/CHUNKS;
	var i = 0;
	var j = 0;
	while (i < previews.length) {
		fs.writeFile("google_previews/previews" + j + ".json", JSON.stringify(previews.slice(i, i+CHUNKS)), function(err) {
		    if(err) {
		        return console.log(err);
		    }
		});
		i += CHUNKS;
		j++;
	}
})