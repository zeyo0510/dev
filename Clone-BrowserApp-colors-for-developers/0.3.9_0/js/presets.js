$(document).ready(function(){

    initPreset();

});


function initPreset () {
    // TODO Retrieve from Storage the user's presets
    chrome.storage.sync.get('customPresets', function(items){
        window.customPresets = items.customPresets;
        if (window.customPresets == undefined) {
            window.customPresets = [];
        }

        // TODO Merge all presets
        var workPresets = presets.concat(window.customPresets);

        for (var i = 0; i < workPresets.length; i++) {
            presetBuilder(i, workPresets[i]);
        }

        initEnd();
    });
}

function initEnd () {
    // Active the first
    $('.preset_tab').first().addClass('active');
    $('.preset_group').first().addClass('active');

    $(".presets_add_group").click(addPresetGroup);

    randomizeInitialColor();
    updatePreview();




    $('.import_swatch_button').click(function(e) {
        chrome.fileSystem.chooseEntry(
            {
                type: 'openFile',
                accepts: [
                    { description: '*.aco, *.ase' },
                    { extensions: ['aco','ase'] }
                ],
                acceptsAllTypes: false
            },
            function(readOnlyEntry) {
                console.log(readOnlyEntry);
                readOnlyEntry.file(function(file) {
                    var reader = new FileReader();

                    reader.onerror = errorHandler;
                    reader.onloadend = function(e) {
                        console.log(e.target.result);
                    };

                    reader.readAsText(file);
                });
            }
        );
    });




}





function addPresetGroup () {
    // TODO Create new preset
    newPreset = {
        "titre" 	: "New preset",
        "icon" 		: "&#xf007;",
        "lien" 		: "",
        "author"	: "Custom",
        "colors"	: [
        ]
    };
    var newPresetIndex = window.customPresets.length + presets.length;
    window.customPresets[window.customPresets.length] = newPreset;
    // TODO Save in storage
    saveCustomPresets();
    // TODO Add it in the page
    presetBuilder(newPresetIndex, newPreset);

    // Events
    $('.preset_tab[data-group="'+newPresetIndex+'"]').trigger('click');
}

function presetBuilder(id, preset) {
    // Tabs
    $('#presets_tabs .presets_add_group').before('<span class="preset_tab" data-group="'+id+'">'+preset.titre+' <i data-icon="'+preset.icon+'"></i></span>');
    // Group
    $('#presets_groups').append('<div id="group'+id+'" class="preset_group"></div>');
    if (preset.author == 'ColorForDeveloppers') {
        $('#group'+id).append('<div class="preset_source"><a href="'+preset.lien+'" target="_blank">Source <i data-icon="&#xf08e;"></i></a></div>');
    } else {
        $settings = $('<div class="preset_settings"></div>');
        $settings.append('<label>Name: <input class="preset_name" data-group="'+id+'" value="'+preset.titre+'"></label>');
        $settings.append('<label>Icon: <select class="preset_icon" data-group="'+id+'"></select></label>');
        $settings.append('<label><a class="preset_delete" data-group="'+id+'"><i data-icon="&#xf1f8;"></i></a></label>');
        $settings.append('<label><a class="import_swatch_button" data-group="'+id+'"><i data-icon="&#xf093;"></i></a></label>');
        for (var k = 0; k < icons.length; k++) {
            var $option = $('<option value="'+icons[k].code+'">'+icons[k].name+'</option>');
            if (('&#xf'+icons[k].code+';') == preset.icon) {
                $option.prop('selected', true);
            }
            $settings.find('select').append($option);
        }
        $('#group'+id).append($settings);
    }
    $('#group'+id).append('<div class="preset_list"></div>');
    for (var j = 0; j < preset.colors.length; j++) {
        $('#group'+id+' .preset_list').append('<div class="preset" data-preset="'+preset.colors[j].hex+'"></div>');
        $('#group'+id+' .preset_list [data-preset="'+preset.colors[j].hex+'"]').append('<div class="prev"><div style="background-color:'+convertHexaToRgba(preset.colors[j].hex)+'"></div></div>');
        $('#group'+id+' .preset_list [data-preset="'+preset.colors[j].hex+'"]').append(' <span>    #'+preset.colors[j].hex+'   </span>  ');
        if (preset.colors[j].name != "") {
            $('#group'+id+' .preset_list [data-preset="'+preset.colors[j].hex+'"]').append(' <span class="preset-name">'+preset.colors[j].name+'</span>  ');
        }
    }
    if (preset.author != 'ColorForDeveloppers') {
        var $newButton = $('<div class="preset preset_new" data-group="'+id+'"></div>');
        $newButton.append('<div class="prev"><div class="preset-to-add" style="color:#cccccc"><i data-icon="&#xf055;"></i></div></div>');
        $newButton.append(' <span class="hex-to-add">#cccccc</span>  ');
        $newButton.append(' <span class="preset-name">Add it</span>  ');
        $('#group'+id+' .preset_list').append($newButton);
    }

    $('.preset_tab[data-group="'+id+'"]').click(function(){
        $('.preset_tab').removeClass('active');
        $(this).addClass('active');

        $('.preset_group').removeClass('active');
        $('#group'+$(this).attr('data-group')).addClass('active');
    });
    $('#group'+id+' .preset_name').on('change', function(){
        var title = $(this).val();
        // Save preset
        window.customPresets[$(this).attr('data-group')-presets.length].titre = title;
        saveCustomPresets();
        // Change the tab
        var $tab = $('.preset_tab[data-group="'+$(this).attr('data-group')+'"]');
        var $icon = $tab.find('i');
        $tab.text(title+' ');
        $tab.append($icon);
    });
    $('#group'+id+' .preset_icon').on('change', function(){
        var iconCode = '&#xf'+$(this).val()+';';
        // Save preset
        window.customPresets[$(this).attr('data-group')-presets.length].icon = iconCode;
        saveCustomPresets();
        // Change the tab
        var $tab = $('.preset_tab[data-group="'+$(this).attr('data-group')+'"]');
        var text = $tab.text();
        $tab.html(text+'<i data-icon="'+iconCode+'"></i>')
    });
    $('#group'+id+' .preset_delete').click(function(){
        // TODO Safe delete by add UNDO
        // Remove preset
        window.customPresets.splice($(this).attr('data-group')-presets.length, 1);
        saveCustomPresets();
        // Reset preset DOM
        //$('.preset_tab[data-group="'+$(this).attr('data-group')+'"]').remove();
        //$('#group'+$(this).attr('data-group')).remove();
        $('.preset_tab').remove();
        $('.preset_group').remove();
        initPreset();
    });
    $('#group'+id+' .preset_new').click(function(){
        var hex = $('#hexa').val();
        // Save preset
        var colors = window.customPresets[$(this).attr('data-group')-presets.length].colors;
        colors[colors.length] = {"name": "", "hex": hex};
        window.customPresets[$(this).attr('data-group')-presets.length].colors = colors;
        saveCustomPresets();
        // Add the preset
        var id = $(this).attr('data-group');
        var $newPreset = $('<div class="preset" data-preset="'+hex+'"></div>');
        $newPreset.append('<div class="prev"><div style="background-color:'+convertHexaToRgba(hex)+'"></div></div>');
        $newPreset.append(' <span>    #'+hex+'   </span>  ');

        $(this).before($newPreset);
    });

    $('#group'+id+' .preset:not(.preset_new)').click(function(){
        $("#hexa").val($(this).attr("data-preset"));
        update255FromHexa();
        update1From255();
        updateCMYKFromRGB();
        updateHSLFromRGB();
        updatePreview();
        //$("body").scrollTop(100);
    });
}

function saveCustomPresets() {
    chrome.storage.sync.set({"customPresets":window.customPresets}, function(){
        console.log('Presets saved');
    });
}




var chosenFileEntry = null;

