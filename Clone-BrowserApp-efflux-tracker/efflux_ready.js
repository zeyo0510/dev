(function() {
    // show deprecation message
    // when menu item is present, we know Efflux bootstrap has finished

    var pre = document.querySelector( "#songReset" );
    if ( !pre ) {
        var ps = window.efflux ? window.efflux.Pubsub : null;
        if ( ps ) {
            ps.subscribe( "UI:MI", showDeprecation );
        }
    }
    else {
        showDeprecation();
    }

    function showDeprecation() {
        window.efflux.Pubsub.publish("SYSDL", {
            title: "Efflux App deprecated as of december 2017",
            message: "As Google is discontinuing the Chrome App platform, this application will no longer " +
            "be maintained.<br /><br />" +
            "The <a href='https://www.igorski.nl/experiment/efflux' target='_blank'>online version</a> " +
            "of Efflux will however be updating continuously and contains all features currently supported " +
            "by this Chrome App, including offline support. Additionally, you will find workflow improvements, an " +
            "updated interface, more waveforms and sound shaping capabilities, etc. Just believe this text when it says it's just <i>better</i> ;).<br /><br />" +
            "As for migrating your work: you can export the songs and instruments you have saved in this app by using the 'Export song' & " +
            "'Export instrument presets'-options from the 'File'-menu and import them again in the online version of Efflux."
        });
    }
})();
