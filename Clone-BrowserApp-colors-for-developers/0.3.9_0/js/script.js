$(document).ready(function(){

    randomizeInitialColor();
	updatePreview();
});


function randomizeInitialColor() {
    var chosenPresetGroup = Math.floor((Math.random() * presets.length-1) + 1);
    var chosenPreset =  Math.floor((Math.random() * presets[chosenPresetGroup].colors.length-1) + 1);
    var hex = presets[chosenPresetGroup].colors[chosenPreset].hex;
    $('#hexa').val(hex);
}

