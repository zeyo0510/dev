window.addEventListener('load', function(e) {

    var efflux = window.efflux;

    if ( !efflux )
        return;

    var ROOT = "https://www.igorski.nl/";

    var ps = efflux.Pubsub, doAn = window._gaq;

    function tp( elId, page ) {
        var el = document.querySelector( elId );
        if ( el ) {
            el.addEventListener( "click", function(e) {
                try {
                    _gaq.push([ "_trackPageview", "/experiment/efflux/" + page ]);
                }
                catch(e) {}
            });
        }
    }
    function te(c,a,l) {
        try {
            _gaq.push([ "_trackEvent", "efflux", c, a, l ]);
        } catch(e) {}
    }

    if (doAn) {
        tp("#songLoad","songBrowser");
        tp("#settingsBtn","settings");
        tp("#audioRecord","record");

        ps.subscribe( "SNG:0", function(t,p){ te( 'song', 'load' );});
        ps.subscribe( "SNG:1", function(t,p){ te( 'song', 'import' );});
        ps.subscribe( "SNG:2", function(t,p){ te( 'song', 'export' );});
        ps.subscribe( "UI:2",  function(t,p){ te( 'instrument', 'edit' );});
        ps.subscribe( "MID:3",  function(t,p){ te( 'midi', 'connected' );});
    }
    
    /*!
     * Social Share Kit v1.0.8 (http://socialsharekit.com)
     * Copyright 2015 Social Share Kit / Kaspars Sprogis.
     * Licensed under Creative Commons Attribution-NonCommercial 3.0 license:
     * https://github.com/darklow/social-share-kit/blob/master/LICENSE
     * ---
     */
    var SocialShareKit=(function(){var s=/(twitter|facebook|google-plus|pinterest|tumblr|vk|linkedin|email)/,o="*|*",j,b;b=function(z){var y=z||{},x=y.selector||".ssk";this.nodes=f(x);this.options=y};b.prototype={share:function(){var z=this.nodes,y=this.options,x={};l(function(){if(!z.length){return}e(z,function(D){var E=q(D),C;if(!E){return}p(D,"click",A);n(D,"click",A);if(D.parentNode.className.indexOf("ssk-count")!==-1){E=E[0];C=E+o+w(y,E,D);if(!(C in x)){x[C]=[]}x[C].push(D)}});B()});function A(I){var H=i(I),D=q(H),F=D[0],C;if(!D){return}C=r(y,F,H);if(!C){return}if(window.twttr&&H.getAttribute("href").indexOf("twitter.com/intent/")!==-1){H.setAttribute("href",C);return}if(F!="email"){var G=h(C);if(y.onOpen){y.onOpen(H,F,C,G)}if(y.onClose){var E=window.setInterval(function(){if(G.closed!==false){window.clearInterval(E);y.onClose(H,F,C,G)}},250)}}else{document.location=C}}function B(){var C,D;for(C in x){D=C.split(o);(function(E){t(D[0],D[1],y,function(F){for(var G in E){g(E[G],F)}})})(x[C])}}return this.nodes}};j=function(x){return new b(x)};function u(x){return j(x).share()}function l(x){if(document.readyState!="loading"){x()}else{if(document.addEventListener){document.addEventListener("DOMContentLoaded",x)}else{document.attachEvent("onreadystatechange",function(){if(document.readyState!="loading"){x()}})}}}function f(x){return document.querySelectorAll(x)}function e(z,y){for(var x=0;x<z.length;x++){y(z[x],x)}}function n(z,x,y){if(z.addEventListener){z.addEventListener(x,y)}else{z.attachEvent("on"+x,function(){y.call(z)})}}function p(z,x,y){if(z.removeEventListener){z.removeEventListener(x,y)}else{z.detachEvent("on"+x,y)}}function q(x){return x.className.match(s)}function i(y){var x=y||window.event;if(x.preventDefault){x.preventDefault()}else{x.returnValue=false;x.cancelBubble=true}return x.currentTarget||x.srcElement}function h(y){var z=575,x=400,D=(document.documentElement.clientWidth/2-z/2),C=(document.documentElement.clientHeight-x)/2,A="status=1,resizable=yes,width="+z+",height="+x+",top="+C+",left="+D,B=window.open(y,"",A);B.focus();return B}function r(H,A,z){var x,y=a(H,A,z),C=w(H,A,z,y),E=typeof y.title!=="undefined"?y.title:k(A),G=typeof y.text!=="undefined"?y.text:d(A),B=y.image?y.image:v("og:image"),F=typeof y.via!=="undefined"?y.via:v("twitter:site"),D={shareUrl:C,title:E,text:G,image:B,via:F,options:H,shareUrlEncoded:function(){return encodeURIComponent(this.shareUrl)}};switch(A){case"facebook":x="https://www.facebook.com/share.php?u="+D.shareUrlEncoded();break;case"twitter":x="https://twitter.com/intent/tweet?url="+D.shareUrlEncoded()+"&text="+encodeURIComponent(E+(G&&E?" - ":"")+G);if(F){x+="&via="+F.replace("@","")}break;case"google-plus":x="https://plus.google.com/share?url="+D.shareUrlEncoded();break;case"pinterest":x="https://pinterest.com/pin/create/button/?url="+D.shareUrlEncoded()+"&description="+encodeURIComponent(G);if(B){x+="&media="+encodeURIComponent(B)}break;case"tumblr":x="https://www.tumblr.com/share/link?url="+D.shareUrlEncoded()+"&name="+encodeURIComponent(E)+"&description="+encodeURIComponent(G);break;case"linkedin":x="https://www.linkedin.com/shareArticle?mini=true&url="+D.shareUrlEncoded()+"&title="+encodeURIComponent(E)+"&summary="+encodeURIComponent(G);break;case"vk":x="https://vkontakte.ru/share.php?url="+D.shareUrlEncoded();break;case"email":x="mailto:?subject="+encodeURIComponent(E)+"&body="+encodeURIComponent(E+"\n"+C+"\n\n"+G+"\n");break}D.networkUrl=x;if(H.onBeforeOpen){H.onBeforeOpen(z,A,D)}return D.networkUrl}function w(y,A,z,x){x=x||a(y,A,z);return x.url||window.location.href}function k(x){var y;if(x=="twitter"){y=v("twitter:title")}return y||document.title}function d(x){var y;if(x=="twitter"){y=v("twitter:description")}return y||v("description")}function v(z,y){var A,x=f("meta["+(y?y:z.indexOf("og:")===0?"property":"name")+'="'+z+'"]');if(x.length){A=x[0].getAttribute("content")||""}return A||""}function a(G,A,z){var y=["url","title","text","image"],x={},E,F,B,C,D=z.parentNode;A=="twitter"&&y.push("via");for(C in y){F=y[C];B="data-"+F;E=z.getAttribute(B)||D.getAttribute(B)||(G[A]&&typeof G[A][F]!="undefined"?G[A][F]:G[F]);if(typeof E!="undefined"){x[F]=E}}return x}function g(y,x){var z=document.createElement("div");z.innerHTML=x;z.className="ssk-num";y.appendChild(z)}function t(C,D,A,E){var z,y,x,B=encodeURIComponent(D);switch(C){case"facebook":z="https://graph.facebook.com/?id="+B;y=function(F){return E(F.shares?F.shares:0)};break;case"twitter":if(A&&A.twitter&&A.twitter.countCallback){A.twitter.countCallback(D,E)}break;case"google-plus":z="https://clients6.google.com/rpc?key=AIzaSyCKSbrvQasunBoV16zDH9R33D88CeLr9gQ";x='[{"method":"pos.plusones.get","id":"p","params":{"id":"'+D+'","userId":"@viewer","groupId":"@self","nolog":true},"jsonrpc":"2.0","key":"p","apiVersion":"v1"}]';y=function(F){F=JSON.parse(F);if(F.length){return E(F[0].result.metadata.globalCounts.count)}};m(z,y,x);return;case"linkedin":z="https://www.linkedin.com/countserv/count/share?url="+B;y=function(F){return E(F.count)};break;case"pinterest":z="https://api.pinterest.com/v1/urls/count.json?url="+B;y=function(F){return E(F.count)};break;case"vk":z="https://vk.com/share.php?act=count&url="+B;y=function(F){return E(F)};break}z&&y&&c(C,z,y,x)}function m(y,A,x){var z=new XMLHttpRequest();z.onreadystatechange=function(){if(this.readyState===4){if(this.status>=200&&this.status<400){A(this.responseText)}}};z.open("POST",y,true);z.setRequestHeader("Content-Type","application/json");z.send(x)}function c(z,y,B){var A="cb_"+z+"_"+Math.round(100000*Math.random()),x=document.createElement("script");window[A]=function(C){try{delete window[A]}catch(D){}document.body.removeChild(x);B(C)};if(z=="vk"){window.VK={Share:{count:function(D,C){window[A](C)}}}}else{if(z=="google-plus"){window.services={gplus:{cb:window[A]}}}}x.src=y+(y.indexOf("?")>=0?"&":"?")+"callback="+A;document.body.appendChild(x);return true}return{init:u}})();window.SocialShareKit=SocialShareKit;

    var xhr;

    // load deeplinked song

    var params = {};

    if ( window.location.search )
    {
        var parts = location.search.substring(1).split('&');

        for ( var i = 0; i < parts.length; i++ ) {

            var nv = parts[ i ].split( '=' );
            if ( !nv[ 0 ]) continue;
            params[ nv[ 0 ]] = nv[ 1 ] || true;
        }
    }

    // when menu item is present, we know Efflux bootstrap has finished

    var pre = document.querySelector( "#songReset" );
    if ( !pre )
        ps.subscribe( "UI:MI", effluxReady );
    else
        effluxReady();

    function effluxReady() {

        // check for deeplink

        if ( params.song && params.song.length === 32 )
        {
            xhr = createRequest( function( response )
            {
                try {
                    var song = JSON.parse( response.data );
                    ps.publishSync( "SNGTF", song );
                    ps.publishSync( "SNGLD", song );
                }
                catch ( e ) {}
            });
            xhr.open( "GET", ROOT + "/efflux/load/" + params.song, true );
            xhr.send();
        }

        // add share

        pre = document.querySelector( "#songReset" );

        if  ( !pre || !pre.parentNode )
            return;

        var button = document.createElement( "li" );
        button.innerHTML = "Share";

        pre.parentNode.insertBefore( button, pre );

        button.onclick = function( e )
        {
            ps.publish( "SNGVG", function( song )
            {
                if ( song ) {

                    var payload = {
                        "data"   : window.btoa( JSON.stringify( song )),
                        "name"   : song.meta.title,
                        "author" : song.meta.author
                    };

                    var pw = popup( "Share '" + song.meta.title + "' with your friends or network?",
                        "<p>You can! By confirming your track will be made available on the Efflux servers, " +
                        "after which you can instantly share its unique link on social media, e-mail, etc.</p>" +
                        "<button id='confirm'>Yes, share this track</button>"
                    );

                    var confirmBtn = pw.element.querySelector( "#confirm" );
                    confirmBtn.onclick = function( e )
                    {
                        pw.close();

                        xhr = createRequest( function( response )
                        {
                            if ( response )
                            {
                                if ( !response.success ) {
                                    var errors, error = "";
                                    if ( ( errors = response.errors ) instanceof Array ) {
                                        errors.forEach( function( err ) {
                                            error += err + "\n";
                                        });
                                    }
                                    else {
                                        error = "Error occurred while attempting to save song.";
                                    }
                                    ps.publish( "SYSER", error );
                                }
                                else if ( response.hash ) {

                                    var url = ROOT + "experiment/efflux/?song=" + response.hash;

                                    popup( "Song " + song.meta.title + " stored",
                                        '<p>You can let your friends listen by sharing:</p><p>' +
                                        '<span class="url">' + url + '</span></p>' +
                                        '<p>You can also share directly:</p>' +
                                        '<div class="ssk-group">' +
                                            '<a href="' + url + '#" class="ssk ssk-facebook"></a>' +
                                            '<a href="' + url + '#" class="ssk ssk-twitter"></a>' +
                                            '<a href="' + url + '#" class="ssk ssk-google-plus"></a>' +
                                            '<a href="' + url + '#" class="ssk ssk-pinterest"></a>' +
                                            '<a href="' + url + '#" class="ssk ssk-tumblr"></a>' +
                                        '</div>'
                                    );

                                    if ( doAn )
                                        te("song","shared",song.meta.title);

                                    SocialShareKit.init({
                                        selector: '#popupWindow .ssk',
                                        url: url,
                                        text: song.meta.title
                                    });
                                }
                            }
                        });

                        xhr.open( "POST", ROOT + "/efflux/save", true );
                        xhr.setRequestHeader( "Content-type", "application/x-www-form-urlencoded" );
                        xhr.send( jsonToQueryString( payload ));
                    };
                }
            });
        };
    }

    function createRequest( successCallback )
    {
        var req = new XMLHttpRequest();

        req.onreadystatechange = function()
        {
            if ( req.readyState === 4 )
            {
                toggleLoader( false );

                if ( req.status !== 200 ) {
                    ps.publish( "SYSER",
                        "Error occurred while attempting to save song on server. Please try again later."
                    );
                    return;
                }
                var response;
                try {
                    response = JSON.parse( xhr.responseText );
                }
                catch ( e ) {}

                successCallback( response );
            }
        };
        toggleLoader( true );
        return req;
    }

    function jsonToQueryString( json ) {

        return Object.keys( json ).map( function( key ) {
            return encodeURIComponent( key ) + '=' +
                   encodeURIComponent( json[ key ]);

        }).join( "&" );
    }

    function toggleLoader( visible ) {

        if ( visible )
            ps.publish( "SYSSL" );
        else
            ps.publish( "SYSHL" );
    }

    function popup( title, html ) {
        var pw = document.createElement( "div" );
        pw.setAttribute( "id", "popupWindow" );

        setTimeout( function() { ps.publish( "SYSSB" ) }, 25 );
        document.body.appendChild( pw );

        pw.innerHTML = '<h2>' + title + '</h2>' + html + '<div class="close">x</div>';

        var closeButton  = pw.querySelector(".close");
        var closeHandler = function( e ) {
            if ( e && e.type === "keyup" && e.keyCode !== 27 )
                return;

            window.removeEventListener("keyup",closeHandler);
            closeButton.removeEventListener("click", closeHandler);
            ps.publish( "SYSHB" );
            document.body.removeChild( pw );
        };
        window.addEventListener( "keyup", closeHandler );
        closeButton.addEventListener( "click", closeHandler );

        return { element: pw, close: closeHandler };
    }

});
