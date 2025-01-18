/*!
 * Bootstrap v3.2.0 (http://getbootstrap.com)
 * Copyright 2011-2014 Twitter, Inc.
 * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
 */

if("undefined"==typeof jQuery)throw new Error("Bootstrap's JavaScript requires jQuery");+function(a){"use strict";function b(){var a=document.createElement("bootstrap"),b={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"};for(var c in b)if(void 0!==a.style[c])return{end:b[c]};return!1}a.fn.emulateTransitionEnd=function(b){var c=!1,d=this;a(this).one("bsTransitionEnd",function(){c=!0});var e=function(){c||a(d).trigger(a.support.transition.end)};return setTimeout(e,b),this},a(function(){a.support.transition=b(),a.support.transition&&(a.event.special.bsTransitionEnd={bindType:a.support.transition.end,delegateType:a.support.transition.end,handle:function(b){return a(b.target).is(this)?b.handleObj.handler.apply(this,arguments):void 0}})})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var c=a(this),e=c.data("bs.alert");e||c.data("bs.alert",e=new d(this)),"string"==typeof b&&e[b].call(c)})}var c='[data-dismiss="alert"]',d=function(b){a(b).on("click",c,this.close)};d.VERSION="3.2.0",d.prototype.close=function(b){function c(){f.detach().trigger("closed.bs.alert").remove()}var d=a(this),e=d.attr("data-target");e||(e=d.attr("href"),e=e&&e.replace(/.*(?=#[^\s]*$)/,""));var f=a(e);b&&b.preventDefault(),f.length||(f=d.hasClass("alert")?d:d.parent()),f.trigger(b=a.Event("close.bs.alert")),b.isDefaultPrevented()||(f.removeClass("in"),a.support.transition&&f.hasClass("fade")?f.one("bsTransitionEnd",c).emulateTransitionEnd(150):c())};var e=a.fn.alert;a.fn.alert=b,a.fn.alert.Constructor=d,a.fn.alert.noConflict=function(){return a.fn.alert=e,this},a(document).on("click.bs.alert.data-api",c,d.prototype.close)}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.button"),f="object"==typeof b&&b;e||d.data("bs.button",e=new c(this,f)),"toggle"==b?e.toggle():b&&e.setState(b)})}var c=function(b,d){this.$element=a(b),this.options=a.extend({},c.DEFAULTS,d),this.isLoading=!1};c.VERSION="3.2.0",c.DEFAULTS={loadingText:"loading..."},c.prototype.setState=function(b){var c="disabled",d=this.$element,e=d.is("input")?"val":"html",f=d.data();b+="Text",null==f.resetText&&d.data("resetText",d[e]()),d[e](null==f[b]?this.options[b]:f[b]),setTimeout(a.proxy(function(){"loadingText"==b?(this.isLoading=!0,d.addClass(c).attr(c,c)):this.isLoading&&(this.isLoading=!1,d.removeClass(c).removeAttr(c))},this),0)},c.prototype.toggle=function(){var a=!0,b=this.$element.closest('[data-toggle="buttons"]');if(b.length){var c=this.$element.find("input");"radio"==c.prop("type")&&(c.prop("checked")&&this.$element.hasClass("active")?a=!1:b.find(".active").removeClass("active")),a&&c.prop("checked",!this.$element.hasClass("active")).trigger("change")}a&&this.$element.toggleClass("active")};var d=a.fn.button;a.fn.button=b,a.fn.button.Constructor=c,a.fn.button.noConflict=function(){return a.fn.button=d,this},a(document).on("click.bs.button.data-api",'[data-toggle^="button"]',function(c){var d=a(c.target);d.hasClass("btn")||(d=d.closest(".btn")),b.call(d,"toggle"),c.preventDefault()})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.carousel"),f=a.extend({},c.DEFAULTS,d.data(),"object"==typeof b&&b),g="string"==typeof b?b:f.slide;e||d.data("bs.carousel",e=new c(this,f)),"number"==typeof b?e.to(b):g?e[g]():f.interval&&e.pause().cycle()})}var c=function(b,c){this.$element=a(b).on("keydown.bs.carousel",a.proxy(this.keydown,this)),this.$indicators=this.$element.find(".carousel-indicators"),this.options=c,this.paused=this.sliding=this.interval=this.$active=this.$items=null,"hover"==this.options.pause&&this.$element.on("mouseenter.bs.carousel",a.proxy(this.pause,this)).on("mouseleave.bs.carousel",a.proxy(this.cycle,this))};c.VERSION="3.2.0",c.DEFAULTS={interval:5e3,pause:"hover",wrap:!0},c.prototype.keydown=function(a){switch(a.which){case 37:this.prev();break;case 39:this.next();break;default:return}a.preventDefault()},c.prototype.cycle=function(b){return b||(this.paused=!1),this.interval&&clearInterval(this.interval),this.options.interval&&!this.paused&&(this.interval=setInterval(a.proxy(this.next,this),this.options.interval)),this},c.prototype.getItemIndex=function(a){return this.$items=a.parent().children(".item"),this.$items.index(a||this.$active)},c.prototype.to=function(b){var c=this,d=this.getItemIndex(this.$active=this.$element.find(".item.active"));return b>this.$items.length-1||0>b?void 0:this.sliding?this.$element.one("slid.bs.carousel",function(){c.to(b)}):d==b?this.pause().cycle():this.slide(b>d?"next":"prev",a(this.$items[b]))},c.prototype.pause=function(b){return b||(this.paused=!0),this.$element.find(".next, .prev").length&&a.support.transition&&(this.$element.trigger(a.support.transition.end),this.cycle(!0)),this.interval=clearInterval(this.interval),this},c.prototype.next=function(){return this.sliding?void 0:this.slide("next")},c.prototype.prev=function(){return this.sliding?void 0:this.slide("prev")},c.prototype.slide=function(b,c){var d=this.$element.find(".item.active"),e=c||d[b](),f=this.interval,g="next"==b?"left":"right",h="next"==b?"first":"last",i=this;if(!e.length){if(!this.options.wrap)return;e=this.$element.find(".item")[h]()}if(e.hasClass("active"))return this.sliding=!1;var j=e[0],k=a.Event("slide.bs.carousel",{relatedTarget:j,direction:g});if(this.$element.trigger(k),!k.isDefaultPrevented()){if(this.sliding=!0,f&&this.pause(),this.$indicators.length){this.$indicators.find(".active").removeClass("active");var l=a(this.$indicators.children()[this.getItemIndex(e)]);l&&l.addClass("active")}var m=a.Event("slid.bs.carousel",{relatedTarget:j,direction:g});return a.support.transition&&this.$element.hasClass("slide")?(e.addClass(b),e[0].offsetWidth,d.addClass(g),e.addClass(g),d.one("bsTransitionEnd",function(){e.removeClass([b,g].join(" ")).addClass("active"),d.removeClass(["active",g].join(" ")),i.sliding=!1,setTimeout(function(){i.$element.trigger(m)},0)}).emulateTransitionEnd(1e3*d.css("transition-duration").slice(0,-1))):(d.removeClass("active"),e.addClass("active"),this.sliding=!1,this.$element.trigger(m)),f&&this.cycle(),this}};var d=a.fn.carousel;a.fn.carousel=b,a.fn.carousel.Constructor=c,a.fn.carousel.noConflict=function(){return a.fn.carousel=d,this},a(document).on("click.bs.carousel.data-api","[data-slide], [data-slide-to]",function(c){var d,e=a(this),f=a(e.attr("data-target")||(d=e.attr("href"))&&d.replace(/.*(?=#[^\s]+$)/,""));if(f.hasClass("carousel")){var g=a.extend({},f.data(),e.data()),h=e.attr("data-slide-to");h&&(g.interval=!1),b.call(f,g),h&&f.data("bs.carousel").to(h),c.preventDefault()}}),a(window).on("load",function(){a('[data-ride="carousel"]').each(function(){var c=a(this);b.call(c,c.data())})})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.collapse"),f=a.extend({},c.DEFAULTS,d.data(),"object"==typeof b&&b);!e&&f.toggle&&"show"==b&&(b=!b),e||d.data("bs.collapse",e=new c(this,f)),"string"==typeof b&&e[b]()})}var c=function(b,d){this.$element=a(b),this.options=a.extend({},c.DEFAULTS,d),this.transitioning=null,this.options.parent&&(this.$parent=a(this.options.parent)),this.options.toggle&&this.toggle()};c.VERSION="3.2.0",c.DEFAULTS={toggle:!0},c.prototype.dimension=function(){var a=this.$element.hasClass("width");return a?"width":"height"},c.prototype.show=function(){if(!this.transitioning&&!this.$element.hasClass("in")){var c=a.Event("show.bs.collapse");if(this.$element.trigger(c),!c.isDefaultPrevented()){var d=this.$parent&&this.$parent.find("> .panel > .in");if(d&&d.length){var e=d.data("bs.collapse");if(e&&e.transitioning)return;b.call(d,"hide"),e||d.data("bs.collapse",null)}var f=this.dimension();this.$element.removeClass("collapse").addClass("collapsing")[f](0),this.transitioning=1;var g=function(){this.$element.removeClass("collapsing").addClass("collapse in")[f](""),this.transitioning=0,this.$element.trigger("shown.bs.collapse")};if(!a.support.transition)return g.call(this);var h=a.camelCase(["scroll",f].join("-"));this.$element.one("bsTransitionEnd",a.proxy(g,this)).emulateTransitionEnd(350)[f](this.$element[0][h])}}},c.prototype.hide=function(){if(!this.transitioning&&this.$element.hasClass("in")){var b=a.Event("hide.bs.collapse");if(this.$element.trigger(b),!b.isDefaultPrevented()){var c=this.dimension();this.$element[c](this.$element[c]())[0].offsetHeight,this.$element.addClass("collapsing").removeClass("collapse").removeClass("in"),this.transitioning=1;var d=function(){this.transitioning=0,this.$element.trigger("hidden.bs.collapse").removeClass("collapsing").addClass("collapse")};return a.support.transition?void this.$element[c](0).one("bsTransitionEnd",a.proxy(d,this)).emulateTransitionEnd(350):d.call(this)}}},c.prototype.toggle=function(){this[this.$element.hasClass("in")?"hide":"show"]()};var d=a.fn.collapse;a.fn.collapse=b,a.fn.collapse.Constructor=c,a.fn.collapse.noConflict=function(){return a.fn.collapse=d,this},a(document).on("click.bs.collapse.data-api",'[data-toggle="collapse"]',function(c){var d,e=a(this),f=e.attr("data-target")||c.preventDefault()||(d=e.attr("href"))&&d.replace(/.*(?=#[^\s]+$)/,""),g=a(f),h=g.data("bs.collapse"),i=h?"toggle":e.data(),j=e.attr("data-parent"),k=j&&a(j);h&&h.transitioning||(k&&k.find('[data-toggle="collapse"][data-parent="'+j+'"]').not(e).addClass("collapsed"),e[g.hasClass("in")?"addClass":"removeClass"]("collapsed")),b.call(g,i)})}(jQuery),+function(a){"use strict";function b(b){b&&3===b.which||(a(e).remove(),a(f).each(function(){var d=c(a(this)),e={relatedTarget:this};d.hasClass("open")&&(d.trigger(b=a.Event("hide.bs.dropdown",e)),b.isDefaultPrevented()||d.removeClass("open").trigger("hidden.bs.dropdown",e))}))}function c(b){var c=b.attr("data-target");c||(c=b.attr("href"),c=c&&/#[A-Za-z]/.test(c)&&c.replace(/.*(?=#[^\s]*$)/,""));var d=c&&a(c);return d&&d.length?d:b.parent()}function d(b){return this.each(function(){var c=a(this),d=c.data("bs.dropdown");d||c.data("bs.dropdown",d=new g(this)),"string"==typeof b&&d[b].call(c)})}var e=".dropdown-backdrop",f='[data-toggle="dropdown"]',g=function(b){a(b).on("click.bs.dropdown",this.toggle)};g.VERSION="3.2.0",g.prototype.toggle=function(d){var e=a(this);if(!e.is(".disabled, :disabled")){var f=c(e),g=f.hasClass("open");if(b(),!g){"ontouchstart"in document.documentElement&&!f.closest(".navbar-nav").length&&a('<div class="dropdown-backdrop"/>').insertAfter(a(this)).on("click",b);var h={relatedTarget:this};if(f.trigger(d=a.Event("show.bs.dropdown",h)),d.isDefaultPrevented())return;e.trigger("focus"),f.toggleClass("open").trigger("shown.bs.dropdown",h)}return!1}},g.prototype.keydown=function(b){if(/(38|40|27)/.test(b.keyCode)){var d=a(this);if(b.preventDefault(),b.stopPropagation(),!d.is(".disabled, :disabled")){var e=c(d),g=e.hasClass("open");if(!g||g&&27==b.keyCode)return 27==b.which&&e.find(f).trigger("focus"),d.trigger("click");var h=" li:not(.divider):visible a",i=e.find('[role="menu"]'+h+', [role="listbox"]'+h);if(i.length){var j=i.index(i.filter(":focus"));38==b.keyCode&&j>0&&j--,40==b.keyCode&&j<i.length-1&&j++,~j||(j=0),i.eq(j).trigger("focus")}}}};var h=a.fn.dropdown;a.fn.dropdown=d,a.fn.dropdown.Constructor=g,a.fn.dropdown.noConflict=function(){return a.fn.dropdown=h,this},a(document).on("click.bs.dropdown.data-api",b).on("click.bs.dropdown.data-api",".dropdown form",function(a){a.stopPropagation()}).on("click.bs.dropdown.data-api",f,g.prototype.toggle).on("keydown.bs.dropdown.data-api",f+', [role="menu"], [role="listbox"]',g.prototype.keydown)}(jQuery),+function(a){"use strict";function b(b,d){return this.each(function(){var e=a(this),f=e.data("bs.modal"),g=a.extend({},c.DEFAULTS,e.data(),"object"==typeof b&&b);f||e.data("bs.modal",f=new c(this,g)),"string"==typeof b?f[b](d):g.show&&f.show(d)})}var c=function(b,c){this.options=c,this.$body=a(document.body),this.$element=a(b),this.$backdrop=this.isShown=null,this.scrollbarWidth=0,this.options.remote&&this.$element.find(".modal-content").load(this.options.remote,a.proxy(function(){this.$element.trigger("loaded.bs.modal")},this))};c.VERSION="3.2.0",c.DEFAULTS={backdrop:!0,keyboard:!0,show:!0},c.prototype.toggle=function(a){return this.isShown?this.hide():this.show(a)},c.prototype.show=function(b){var c=this,d=a.Event("show.bs.modal",{relatedTarget:b});this.$element.trigger(d),this.isShown||d.isDefaultPrevented()||(this.isShown=!0,this.checkScrollbar(),this.$body.addClass("modal-open"),this.setScrollbar(),this.escape(),this.$element.on("click.dismiss.bs.modal",'[data-dismiss="modal"]',a.proxy(this.hide,this)),this.backdrop(function(){var d=a.support.transition&&c.$element.hasClass("fade");c.$element.parent().length||c.$element.appendTo(c.$body),c.$element.show().scrollTop(0),d&&c.$element[0].offsetWidth,c.$element.addClass("in").attr("aria-hidden",!1),c.enforceFocus();var e=a.Event("shown.bs.modal",{relatedTarget:b});d?c.$element.find(".modal-dialog").one("bsTransitionEnd",function(){c.$element.trigger("focus").trigger(e)}).emulateTransitionEnd(300):c.$element.trigger("focus").trigger(e)}))},c.prototype.hide=function(b){b&&b.preventDefault(),b=a.Event("hide.bs.modal"),this.$element.trigger(b),this.isShown&&!b.isDefaultPrevented()&&(this.isShown=!1,this.$body.removeClass("modal-open"),this.resetScrollbar(),this.escape(),a(document).off("focusin.bs.modal"),this.$element.removeClass("in").attr("aria-hidden",!0).off("click.dismiss.bs.modal"),a.support.transition&&this.$element.hasClass("fade")?this.$element.one("bsTransitionEnd",a.proxy(this.hideModal,this)).emulateTransitionEnd(300):this.hideModal())},c.prototype.enforceFocus=function(){a(document).off("focusin.bs.modal").on("focusin.bs.modal",a.proxy(function(a){this.$element[0]===a.target||this.$element.has(a.target).length||this.$element.trigger("focus")},this))},c.prototype.escape=function(){this.isShown&&this.options.keyboard?this.$element.on("keyup.dismiss.bs.modal",a.proxy(function(a){27==a.which&&this.hide()},this)):this.isShown||this.$element.off("keyup.dismiss.bs.modal")},c.prototype.hideModal=function(){var a=this;this.$element.hide(),this.backdrop(function(){a.$element.trigger("hidden.bs.modal")})},c.prototype.removeBackdrop=function(){this.$backdrop&&this.$backdrop.remove(),this.$backdrop=null},c.prototype.backdrop=function(b){var c=this,d=this.$element.hasClass("fade")?"fade":"";if(this.isShown&&this.options.backdrop){var e=a.support.transition&&d;if(this.$backdrop=a('<div class="modal-backdrop '+d+'" />').appendTo(this.$body),this.$element.on("click.dismiss.bs.modal",a.proxy(function(a){a.target===a.currentTarget&&("static"==this.options.backdrop?this.$element[0].focus.call(this.$element[0]):this.hide.call(this))},this)),e&&this.$backdrop[0].offsetWidth,this.$backdrop.addClass("in"),!b)return;e?this.$backdrop.one("bsTransitionEnd",b).emulateTransitionEnd(150):b()}else if(!this.isShown&&this.$backdrop){this.$backdrop.removeClass("in");var f=function(){c.removeBackdrop(),b&&b()};a.support.transition&&this.$element.hasClass("fade")?this.$backdrop.one("bsTransitionEnd",f).emulateTransitionEnd(150):f()}else b&&b()},c.prototype.checkScrollbar=function(){document.body.clientWidth>=window.innerWidth||(this.scrollbarWidth=this.scrollbarWidth||this.measureScrollbar())},c.prototype.setScrollbar=function(){var a=parseInt(this.$body.css("padding-right")||0,10);this.scrollbarWidth&&this.$body.css("padding-right",a+this.scrollbarWidth)},c.prototype.resetScrollbar=function(){this.$body.css("padding-right","")},c.prototype.measureScrollbar=function(){var a=document.createElement("div");a.className="modal-scrollbar-measure",this.$body.append(a);var b=a.offsetWidth-a.clientWidth;return this.$body[0].removeChild(a),b};var d=a.fn.modal;a.fn.modal=b,a.fn.modal.Constructor=c,a.fn.modal.noConflict=function(){return a.fn.modal=d,this},a(document).on("click.bs.modal.data-api",'[data-toggle="modal"]',function(c){var d=a(this),e=d.attr("href"),f=a(d.attr("data-target")||e&&e.replace(/.*(?=#[^\s]+$)/,"")),g=f.data("bs.modal")?"toggle":a.extend({remote:!/#/.test(e)&&e},f.data(),d.data());d.is("a")&&c.preventDefault(),f.one("show.bs.modal",function(a){a.isDefaultPrevented()||f.one("hidden.bs.modal",function(){d.is(":visible")&&d.trigger("focus")})}),b.call(f,g,this)})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.tooltip"),f="object"==typeof b&&b;(e||"destroy"!=b)&&(e||d.data("bs.tooltip",e=new c(this,f)),"string"==typeof b&&e[b]())})}var c=function(a,b){this.type=this.options=this.enabled=this.timeout=this.hoverState=this.$element=null,this.init("tooltip",a,b)};c.VERSION="3.2.0",c.DEFAULTS={animation:!0,placement:"top",selector:!1,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',trigger:"hover focus",title:"",delay:0,html:!1,container:!1,viewport:{selector:"body",padding:0}},c.prototype.init=function(b,c,d){this.enabled=!0,this.type=b,this.$element=a(c),this.options=this.getOptions(d),this.$viewport=this.options.viewport&&a(this.options.viewport.selector||this.options.viewport);for(var e=this.options.trigger.split(" "),f=e.length;f--;){var g=e[f];if("click"==g)this.$element.on("click."+this.type,this.options.selector,a.proxy(this.toggle,this));else if("manual"!=g){var h="hover"==g?"mouseenter":"focusin",i="hover"==g?"mouseleave":"focusout";this.$element.on(h+"."+this.type,this.options.selector,a.proxy(this.enter,this)),this.$element.on(i+"."+this.type,this.options.selector,a.proxy(this.leave,this))}}this.options.selector?this._options=a.extend({},this.options,{trigger:"manual",selector:""}):this.fixTitle()},c.prototype.getDefaults=function(){return c.DEFAULTS},c.prototype.getOptions=function(b){return b=a.extend({},this.getDefaults(),this.$element.data(),b),b.delay&&"number"==typeof b.delay&&(b.delay={show:b.delay,hide:b.delay}),b},c.prototype.getDelegateOptions=function(){var b={},c=this.getDefaults();return this._options&&a.each(this._options,function(a,d){c[a]!=d&&(b[a]=d)}),b},c.prototype.enter=function(b){var c=b instanceof this.constructor?b:a(b.currentTarget).data("bs."+this.type);return c||(c=new this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTarget).data("bs."+this.type,c)),clearTimeout(c.timeout),c.hoverState="in",c.options.delay&&c.options.delay.show?void(c.timeout=setTimeout(function(){"in"==c.hoverState&&c.show()},c.options.delay.show)):c.show()},c.prototype.leave=function(b){var c=b instanceof this.constructor?b:a(b.currentTarget).data("bs."+this.type);return c||(c=new this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTarget).data("bs."+this.type,c)),clearTimeout(c.timeout),c.hoverState="out",c.options.delay&&c.options.delay.hide?void(c.timeout=setTimeout(function(){"out"==c.hoverState&&c.hide()},c.options.delay.hide)):c.hide()},c.prototype.show=function(){var b=a.Event("show.bs."+this.type);if(this.hasContent()&&this.enabled){this.$element.trigger(b);var c=a.contains(document.documentElement,this.$element[0]);if(b.isDefaultPrevented()||!c)return;var d=this,e=this.tip(),f=this.getUID(this.type);this.setContent(),e.attr("id",f),this.$element.attr("aria-describedby",f),this.options.animation&&e.addClass("fade");var g="function"==typeof this.options.placement?this.options.placement.call(this,e[0],this.$element[0]):this.options.placement,h=/\s?auto?\s?/i,i=h.test(g);i&&(g=g.replace(h,"")||"top"),e.detach().css({top:0,left:0,display:"block"}).addClass(g).data("bs."+this.type,this),this.options.container?e.appendTo(this.options.container):e.insertAfter(this.$element);var j=this.getPosition(),k=e[0].offsetWidth,l=e[0].offsetHeight;if(i){var m=g,n=this.$element.parent(),o=this.getPosition(n);g="bottom"==g&&j.top+j.height+l-o.scroll>o.height?"top":"top"==g&&j.top-o.scroll-l<0?"bottom":"right"==g&&j.right+k>o.width?"left":"left"==g&&j.left-k<o.left?"right":g,e.removeClass(m).addClass(g)}var p=this.getCalculatedOffset(g,j,k,l);this.applyPlacement(p,g);var q=function(){d.$element.trigger("shown.bs."+d.type),d.hoverState=null};a.support.transition&&this.$tip.hasClass("fade")?e.one("bsTransitionEnd",q).emulateTransitionEnd(150):q()}},c.prototype.applyPlacement=function(b,c){var d=this.tip(),e=d[0].offsetWidth,f=d[0].offsetHeight,g=parseInt(d.css("margin-top"),10),h=parseInt(d.css("margin-left"),10);isNaN(g)&&(g=0),isNaN(h)&&(h=0),b.top=b.top+g,b.left=b.left+h,a.offset.setOffset(d[0],a.extend({using:function(a){d.css({top:Math.round(a.top),left:Math.round(a.left)})}},b),0),d.addClass("in");var i=d[0].offsetWidth,j=d[0].offsetHeight;"top"==c&&j!=f&&(b.top=b.top+f-j);var k=this.getViewportAdjustedDelta(c,b,i,j);k.left?b.left+=k.left:b.top+=k.top;var l=k.left?2*k.left-e+i:2*k.top-f+j,m=k.left?"left":"top",n=k.left?"offsetWidth":"offsetHeight";d.offset(b),this.replaceArrow(l,d[0][n],m)},c.prototype.replaceArrow=function(a,b,c){this.arrow().css(c,a?50*(1-a/b)+"%":"")},c.prototype.setContent=function(){var a=this.tip(),b=this.getTitle();a.find(".tooltip-inner")[this.options.html?"html":"text"](b),a.removeClass("fade in top bottom left right")},c.prototype.hide=function(){function b(){"in"!=c.hoverState&&d.detach(),c.$element.trigger("hidden.bs."+c.type)}var c=this,d=this.tip(),e=a.Event("hide.bs."+this.type);return this.$element.removeAttr("aria-describedby"),this.$element.trigger(e),e.isDefaultPrevented()?void 0:(d.removeClass("in"),a.support.transition&&this.$tip.hasClass("fade")?d.one("bsTransitionEnd",b).emulateTransitionEnd(150):b(),this.hoverState=null,this)},c.prototype.fixTitle=function(){var a=this.$element;(a.attr("title")||"string"!=typeof a.attr("data-original-title"))&&a.attr("data-original-title",a.attr("title")||"").attr("title","")},c.prototype.hasContent=function(){return this.getTitle()},c.prototype.getPosition=function(b){b=b||this.$element;var c=b[0],d="BODY"==c.tagName;return a.extend({},"function"==typeof c.getBoundingClientRect?c.getBoundingClientRect():null,{scroll:d?document.documentElement.scrollTop||document.body.scrollTop:b.scrollTop(),width:d?a(window).width():b.outerWidth(),height:d?a(window).height():b.outerHeight()},d?{top:0,left:0}:b.offset())},c.prototype.getCalculatedOffset=function(a,b,c,d){return"bottom"==a?{top:b.top+b.height,left:b.left+b.width/2-c/2}:"top"==a?{top:b.top-d,left:b.left+b.width/2-c/2}:"left"==a?{top:b.top+b.height/2-d/2,left:b.left-c}:{top:b.top+b.height/2-d/2,left:b.left+b.width}},c.prototype.getViewportAdjustedDelta=function(a,b,c,d){var e={top:0,left:0};if(!this.$viewport)return e;var f=this.options.viewport&&this.options.viewport.padding||0,g=this.getPosition(this.$viewport);if(/right|left/.test(a)){var h=b.top-f-g.scroll,i=b.top+f-g.scroll+d;h<g.top?e.top=g.top-h:i>g.top+g.height&&(e.top=g.top+g.height-i)}else{var j=b.left-f,k=b.left+f+c;j<g.left?e.left=g.left-j:k>g.width&&(e.left=g.left+g.width-k)}return e},c.prototype.getTitle=function(){var a,b=this.$element,c=this.options;return a=b.attr("data-original-title")||("function"==typeof c.title?c.title.call(b[0]):c.title)},c.prototype.getUID=function(a){do a+=~~(1e6*Math.random());while(document.getElementById(a));return a},c.prototype.tip=function(){return this.$tip=this.$tip||a(this.options.template)},c.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".tooltip-arrow")},c.prototype.validate=function(){this.$element[0].parentNode||(this.hide(),this.$element=null,this.options=null)},c.prototype.enable=function(){this.enabled=!0},c.prototype.disable=function(){this.enabled=!1},c.prototype.toggleEnabled=function(){this.enabled=!this.enabled},c.prototype.toggle=function(b){var c=this;b&&(c=a(b.currentTarget).data("bs."+this.type),c||(c=new this.constructor(b.currentTarget,this.getDelegateOptions()),a(b.currentTarget).data("bs."+this.type,c))),c.tip().hasClass("in")?c.leave(c):c.enter(c)},c.prototype.destroy=function(){clearTimeout(this.timeout),this.hide().$element.off("."+this.type).removeData("bs."+this.type)};var d=a.fn.tooltip;a.fn.tooltip=b,a.fn.tooltip.Constructor=c,a.fn.tooltip.noConflict=function(){return a.fn.tooltip=d,this}}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.popover"),f="object"==typeof b&&b;(e||"destroy"!=b)&&(e||d.data("bs.popover",e=new c(this,f)),"string"==typeof b&&e[b]())})}var c=function(a,b){this.init("popover",a,b)};if(!a.fn.tooltip)throw new Error("Popover requires tooltip.js");c.VERSION="3.2.0",c.DEFAULTS=a.extend({},a.fn.tooltip.Constructor.DEFAULTS,{placement:"right",trigger:"click",content:"",template:'<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'}),c.prototype=a.extend({},a.fn.tooltip.Constructor.prototype),c.prototype.constructor=c,c.prototype.getDefaults=function(){return c.DEFAULTS},c.prototype.setContent=function(){var a=this.tip(),b=this.getTitle(),c=this.getContent();a.find(".popover-title")[this.options.html?"html":"text"](b),a.find(".popover-content").empty()[this.options.html?"string"==typeof c?"html":"append":"text"](c),a.removeClass("fade top bottom left right in"),a.find(".popover-title").html()||a.find(".popover-title").hide()},c.prototype.hasContent=function(){return this.getTitle()||this.getContent()},c.prototype.getContent=function(){var a=this.$element,b=this.options;return a.attr("data-content")||("function"==typeof b.content?b.content.call(a[0]):b.content)},c.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".arrow")},c.prototype.tip=function(){return this.$tip||(this.$tip=a(this.options.template)),this.$tip};var d=a.fn.popover;a.fn.popover=b,a.fn.popover.Constructor=c,a.fn.popover.noConflict=function(){return a.fn.popover=d,this}}(jQuery),+function(a){"use strict";function b(c,d){var e=a.proxy(this.process,this);this.$body=a("body"),this.$scrollElement=a(a(c).is("body")?window:c),this.options=a.extend({},b.DEFAULTS,d),this.selector=(this.options.target||"")+" .nav li > a",this.offsets=[],this.targets=[],this.activeTarget=null,this.scrollHeight=0,this.$scrollElement.on("scroll.bs.scrollspy",e),this.refresh(),this.process()}function c(c){return this.each(function(){var d=a(this),e=d.data("bs.scrollspy"),f="object"==typeof c&&c;e||d.data("bs.scrollspy",e=new b(this,f)),"string"==typeof c&&e[c]()})}b.VERSION="3.2.0",b.DEFAULTS={offset:10},b.prototype.getScrollHeight=function(){return this.$scrollElement[0].scrollHeight||Math.max(this.$body[0].scrollHeight,document.documentElement.scrollHeight)},b.prototype.refresh=function(){var b="offset",c=0;a.isWindow(this.$scrollElement[0])||(b="position",c=this.$scrollElement.scrollTop()),this.offsets=[],this.targets=[],this.scrollHeight=this.getScrollHeight();var d=this;this.$body.find(this.selector).map(function(){var d=a(this),e=d.data("target")||d.attr("href"),f=/^#./.test(e)&&a(e);return f&&f.length&&f.is(":visible")&&[[f[b]().top+c,e]]||null}).sort(function(a,b){return a[0]-b[0]}).each(function(){d.offsets.push(this[0]),d.targets.push(this[1])})},b.prototype.process=function(){var a,b=this.$scrollElement.scrollTop()+this.options.offset,c=this.getScrollHeight(),d=this.options.offset+c-this.$scrollElement.height(),e=this.offsets,f=this.targets,g=this.activeTarget;if(this.scrollHeight!=c&&this.refresh(),b>=d)return g!=(a=f[f.length-1])&&this.activate(a);if(g&&b<=e[0])return g!=(a=f[0])&&this.activate(a);for(a=e.length;a--;)g!=f[a]&&b>=e[a]&&(!e[a+1]||b<=e[a+1])&&this.activate(f[a])},b.prototype.activate=function(b){this.activeTarget=b,a(this.selector).parentsUntil(this.options.target,".active").removeClass("active");var c=this.selector+'[data-target="'+b+'"],'+this.selector+'[href="'+b+'"]',d=a(c).parents("li").addClass("active");d.parent(".dropdown-menu").length&&(d=d.closest("li.dropdown").addClass("active")),d.trigger("activate.bs.scrollspy")};var d=a.fn.scrollspy;a.fn.scrollspy=c,a.fn.scrollspy.Constructor=b,a.fn.scrollspy.noConflict=function(){return a.fn.scrollspy=d,this},a(window).on("load.bs.scrollspy.data-api",function(){a('[data-spy="scroll"]').each(function(){var b=a(this);c.call(b,b.data())})})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.tab");e||d.data("bs.tab",e=new c(this)),"string"==typeof b&&e[b]()})}var c=function(b){this.element=a(b)};c.VERSION="3.2.0",c.prototype.show=function(){var b=this.element,c=b.closest("ul:not(.dropdown-menu)"),d=b.data("target");if(d||(d=b.attr("href"),d=d&&d.replace(/.*(?=#[^\s]*$)/,"")),!b.parent("li").hasClass("active")){var e=c.find(".active:last a")[0],f=a.Event("show.bs.tab",{relatedTarget:e});if(b.trigger(f),!f.isDefaultPrevented()){var g=a(d);this.activate(b.closest("li"),c),this.activate(g,g.parent(),function(){b.trigger({type:"shown.bs.tab",relatedTarget:e})})}}},c.prototype.activate=function(b,c,d){function e(){f.removeClass("active").find("> .dropdown-menu > .active").removeClass("active"),b.addClass("active"),g?(b[0].offsetWidth,b.addClass("in")):b.removeClass("fade"),b.parent(".dropdown-menu")&&b.closest("li.dropdown").addClass("active"),d&&d()}var f=c.find("> .active"),g=d&&a.support.transition&&f.hasClass("fade");g?f.one("bsTransitionEnd",e).emulateTransitionEnd(150):e(),f.removeClass("in")};var d=a.fn.tab;a.fn.tab=b,a.fn.tab.Constructor=c,a.fn.tab.noConflict=function(){return a.fn.tab=d,this},a(document).on("click.bs.tab.data-api",'[data-toggle="tab"], [data-toggle="pill"]',function(c){c.preventDefault(),b.call(a(this),"show")})}(jQuery),+function(a){"use strict";function b(b){return this.each(function(){var d=a(this),e=d.data("bs.affix"),f="object"==typeof b&&b;e||d.data("bs.affix",e=new c(this,f)),"string"==typeof b&&e[b]()})}var c=function(b,d){this.options=a.extend({},c.DEFAULTS,d),this.$target=a(this.options.target).on("scroll.bs.affix.data-api",a.proxy(this.checkPosition,this)).on("click.bs.affix.data-api",a.proxy(this.checkPositionWithEventLoop,this)),this.$element=a(b),this.affixed=this.unpin=this.pinnedOffset=null,this.checkPosition()};c.VERSION="3.2.0",c.RESET="affix affix-top affix-bottom",c.DEFAULTS={offset:0,target:window},c.prototype.getPinnedOffset=function(){if(this.pinnedOffset)return this.pinnedOffset;this.$element.removeClass(c.RESET).addClass("affix");var a=this.$target.scrollTop(),b=this.$element.offset();return this.pinnedOffset=b.top-a},c.prototype.checkPositionWithEventLoop=function(){setTimeout(a.proxy(this.checkPosition,this),1)},c.prototype.checkPosition=function(){if(this.$element.is(":visible")){var b=a(document).height(),d=this.$target.scrollTop(),e=this.$element.offset(),f=this.options.offset,g=f.top,h=f.bottom;"object"!=typeof f&&(h=g=f),"function"==typeof g&&(g=f.top(this.$element)),"function"==typeof h&&(h=f.bottom(this.$element));var i=null!=this.unpin&&d+this.unpin<=e.top?!1:null!=h&&e.top+this.$element.height()>=b-h?"bottom":null!=g&&g>=d?"top":!1;if(this.affixed!==i){null!=this.unpin&&this.$element.css("top","");var j="affix"+(i?"-"+i:""),k=a.Event(j+".bs.affix");this.$element.trigger(k),k.isDefaultPrevented()||(this.affixed=i,this.unpin="bottom"==i?this.getPinnedOffset():null,this.$element.removeClass(c.RESET).addClass(j).trigger(a.Event(j.replace("affix","affixed"))),"bottom"==i&&this.$element.offset({top:b-this.$element.height()-h}))}}};var d=a.fn.affix;a.fn.affix=b,a.fn.affix.Constructor=c,a.fn.affix.noConflict=function(){return a.fn.affix=d,this},a(window).on("load",function(){a('[data-spy="affix"]').each(function(){var c=a(this),d=c.data();d.offset=d.offset||{},d.offsetBottom&&(d.offset.bottom=d.offsetBottom),d.offsetTop&&(d.offset.top=d.offsetTop),b.call(c,d)})})}(jQuery);
/*
 * Chartkick.js
 * Create beautiful Javascript charts with minimal code
 * https://github.com/ankane/chartkick.js
 * v1.4.1
 * MIT License
 */

/*jslint browser: true, indent: 2, plusplus: true, vars: true */


(function (window) {
  'use strict';

  var config = window.Chartkick || {};
  var Chartkick, ISO8601_PATTERN, DECIMAL_SEPARATOR, adapters = [];

  // helpers

  function isArray(variable) {
    return Object.prototype.toString.call(variable) === "[object Array]";
  }

  function isFunction(variable) {
    return variable instanceof Function;
  }

  function isPlainObject(variable) {
    return !isFunction(variable) && variable instanceof Object;
  }

  // https://github.com/madrobby/zepto/blob/master/src/zepto.js
  function extend(target, source) {
    var key;
    for (key in source) {
      if (isPlainObject(source[key]) || isArray(source[key])) {
        if (isPlainObject(source[key]) && !isPlainObject(target[key])) {
          target[key] = {};
        }
        if (isArray(source[key]) && !isArray(target[key])) {
          target[key] = [];
        }
        extend(target[key], source[key]);
      } else if (source[key] !== undefined) {
        target[key] = source[key];
      }
    }
  }

  function merge(obj1, obj2) {
    var target = {};
    extend(target, obj1);
    extend(target, obj2);
    return target;
  }

  // https://github.com/Do/iso8601.js
  ISO8601_PATTERN = /(\d\d\d\d)(\-)?(\d\d)(\-)?(\d\d)(T)?(\d\d)(:)?(\d\d)?(:)?(\d\d)?([\.,]\d+)?($|Z|([\+\-])(\d\d)(:)?(\d\d)?)/i;
  DECIMAL_SEPARATOR = String(1.5).charAt(1);

  function parseISO8601(input) {
    var day, hour, matches, milliseconds, minutes, month, offset, result, seconds, type, year;
    type = Object.prototype.toString.call(input);
    if (type === '[object Date]') {
      return input;
    }
    if (type !== '[object String]') {
      return;
    }
    if (matches = input.match(ISO8601_PATTERN)) {
      year = parseInt(matches[1], 10);
      month = parseInt(matches[3], 10) - 1;
      day = parseInt(matches[5], 10);
      hour = parseInt(matches[7], 10);
      minutes = matches[9] ? parseInt(matches[9], 10) : 0;
      seconds = matches[11] ? parseInt(matches[11], 10) : 0;
      milliseconds = matches[12] ? parseFloat(DECIMAL_SEPARATOR + matches[12].slice(1)) * 1000 : 0;
      result = Date.UTC(year, month, day, hour, minutes, seconds, milliseconds);
      if (matches[13] && matches[14]) {
        offset = matches[15] * 60;
        if (matches[17]) {
          offset += parseInt(matches[17], 10);
        }
        offset *= matches[14] === '-' ? -1 : 1;
        result -= offset * 60 * 1000;
      }
      return new Date(result);
    }
  }
  // end iso8601.js

  function negativeValues(series) {
    var i, j, data;
    for (i = 0; i < series.length; i++) {
      data = series[i].data;
      for (j = 0; j < data.length; j++) {
        if (data[j][1] < 0) {
          return true;
        }
      }
    }
    return false;
  }

  function jsOptionsFunc(defaultOptions, hideLegend, setMin, setMax, setStacked, setXtitle, setYtitle) {
    return function (series, opts, chartOptions) {
      var options = merge({}, defaultOptions);
      options = merge(options, chartOptions || {});

      // hide legend
      // this is *not* an external option!
      if (opts.hideLegend) {
        hideLegend(options);
      }

      // min
      if ("min" in opts) {
        setMin(options, opts.min);
      } else if (!negativeValues(series)) {
        setMin(options, 0);
      }

      // max
      if (opts.max) {
        setMax(options, opts.max);
      }

      if (opts.stacked) {
        setStacked(options);
      }

      if (opts.colors) {
        options.colors = opts.colors;
      }

      if (opts.xtitle) {
        setXtitle(options, opts.xtitle);
      }

      if (opts.ytitle) {
        setYtitle(options, opts.ytitle);
      }

      // merge library last
      options = merge(options, opts.library || {});

      return options;
    };
  }

  function setText(element, text) {
    if (document.body.innerText) {
      element.innerText = text;
    } else {
      element.textContent = text;
    }
  }

  function chartError(element, message) {
    setText(element, "Error Loading Chart: " + message);
    element.style.color = "#ff0000";
  }

  function getJSON(element, url, success) {
    var $ = window.jQuery || window.Zepto || window.$;
    $.ajax({
      dataType: "json",
      url: url,
      success: success,
      error: function (jqXHR, textStatus, errorThrown) {
        var message = (typeof errorThrown === "string") ? errorThrown : errorThrown.message;
        chartError(element, message);
      }
    });
  }

  function errorCatcher(chart, callback) {
    try {
      callback(chart);
    } catch (err) {
      chartError(chart.element, err.message);
      throw err;
    }
  }

  function fetchDataSource(chart, callback) {
    if (typeof chart.dataSource === "string") {
      getJSON(chart.element, chart.dataSource, function (data, textStatus, jqXHR) {
        chart.data = data;
        errorCatcher(chart, callback);
      });
    } else {
      chart.data = chart.dataSource;
      errorCatcher(chart, callback);
    }
  }

  // type conversions

  function toStr(n) {
    return "" + n;
  }

  function toFloat(n) {
    return parseFloat(n);
  }

  function toDate(n) {
    if (typeof n !== "object") {
      if (typeof n === "number") {
        n = new Date(n * 1000); // ms
      } else { // str
        // try our best to get the str into iso8601
        // TODO be smarter about this
        var str = n.replace(/ /, "T").replace(" ", "").replace("UTC", "Z");
        n = parseISO8601(str) || new Date(n);
      }
    }
    return n;
  }

  function toArr(n) {
    if (!isArray(n)) {
      var arr = [], i;
      for (i in n) {
        if (n.hasOwnProperty(i)) {
          arr.push([i, n[i]]);
        }
      }
      n = arr;
    }
    return n;
  }

  function sortByTime(a, b) {
    return a[0].getTime() - b[0].getTime();
  }

  if ("Highcharts" in window) {
    var HighchartsAdapter = new function () {
      var Highcharts = window.Highcharts;

      this.name = "highcharts";

      var defaultOptions = {
        chart: {},
        xAxis: {
          title: {
            text: null
          },
          labels: {
            style: {
              fontSize: "12px"
            }
          }
        },
        yAxis: {
          title: {
            text: null
          },
          labels: {
            style: {
              fontSize: "12px"
            }
          }
        },
        title: {
          text: null
        },
        credits: {
          enabled: false
        },
        legend: {
          borderWidth: 0
        },
        tooltip: {
          style: {
            fontSize: "12px"
          }
        },
        plotOptions: {
          areaspline: {},
          series: {
            marker: {}
          }
        }
      };

      var hideLegend = function (options) {
        options.legend.enabled = false;
      };

      var setMin = function (options, min) {
        options.yAxis.min = min;
      };

      var setMax = function (options, max) {
        options.yAxis.max = max;
      };

      var setStacked = function (options) {
        options.plotOptions.series.stacking = "normal";
      };

      var setXtitle = function (options, title) {
        options.xAxis.title.text = title;
      };

      var setYtitle = function (options, title) {
        options.yAxis.title.text = title;
      };

      var jsOptions = jsOptionsFunc(defaultOptions, hideLegend, setMin, setMax, setStacked, setXtitle, setYtitle);

      this.renderLineChart = function (chart, chartType) {
        chartType = chartType || "spline";
        var chartOptions = {};
        if (chartType === "areaspline") {
          chartOptions = {
            plotOptions: {
              areaspline: {
                stacking: "normal"
              },
              series: {
                marker: {
                  enabled: false
                }
              }
            }
          };
        }
        var options = jsOptions(chart.data, chart.options, chartOptions), data, i, j;
        options.xAxis.type = chart.options.discrete ? "category" : "datetime";
        options.chart.type = chartType;
        options.chart.renderTo = chart.element.id;

        var series = chart.data;
        for (i = 0; i < series.length; i++) {
          data = series[i].data;
          if (!chart.options.discrete) {
            for (j = 0; j < data.length; j++) {
              data[j][0] = data[j][0].getTime();
            }
          }
          series[i].marker = {symbol: "circle"};
        }
        options.series = series;
        new Highcharts.Chart(options);
      };

      this.renderScatterChart = function (chart) {
        var chartOptions = {};
        var options = jsOptions(chart.data, chart.options, chartOptions);
        options.chart.type = 'scatter';
        options.chart.renderTo = chart.element.id;
        options.series = chart.data;
        new Highcharts.Chart(options);
      };

      this.renderPieChart = function (chart) {
        var chartOptions = {};
        if (chart.options.colors) {
          chartOptions.colors = chart.options.colors;
        }
        var options = merge(merge(defaultOptions, chartOptions), chart.options.library || {});
        options.chart.renderTo = chart.element.id;
        options.series = [{
          type: "pie",
          name: "Value",
          data: chart.data
        }];
        new Highcharts.Chart(options);
      };

      this.renderColumnChart = function (chart, chartType) {
        var chartType = chartType || "column";
        var series = chart.data;
        var options = jsOptions(series, chart.options), i, j, s, d, rows = [];
        options.chart.type = chartType;
        options.chart.renderTo = chart.element.id;

        for (i = 0; i < series.length; i++) {
          s = series[i];

          for (j = 0; j < s.data.length; j++) {
            d = s.data[j];
            if (!rows[d[0]]) {
              rows[d[0]] = new Array(series.length);
            }
            rows[d[0]][i] = d[1];
          }
        }

        var categories = [];
        for (i in rows) {
          if (rows.hasOwnProperty(i)) {
            categories.push(i);
          }
        }
        options.xAxis.categories = categories;

        var newSeries = [];
        for (i = 0; i < series.length; i++) {
          d = [];
          for (j = 0; j < categories.length; j++) {
            d.push(rows[categories[j]][i] || 0);
          }

          newSeries.push({
            name: series[i].name,
            data: d
          });
        }
        options.series = newSeries;

        new Highcharts.Chart(options);
      };

      var self = this;

      this.renderBarChart = function (chart) {
        self.renderColumnChart(chart, "bar");
      };

      this.renderAreaChart = function (chart) {
        self.renderLineChart(chart, "areaspline");
      };
    };
    adapters.push(HighchartsAdapter);
  }
  if (window.google && window.google.setOnLoadCallback) {
    var GoogleChartsAdapter = new function () {
      var google = window.google;

      this.name = "google";

      var loaded = {};
      var callbacks = [];

      var runCallbacks = function () {
        var cb, call;
        for (var i = 0; i < callbacks.length; i++) {
          cb = callbacks[i];
          call = google.visualization && ((cb.pack === "corechart" && google.visualization.LineChart) || (cb.pack === "timeline" && google.visualization.Timeline))
          if (call) {
            cb.callback();
            callbacks.splice(i, 1);
            i--;
          }
        }
      };

      var waitForLoaded = function (pack, callback) {
        if (!callback) {
          callback = pack;
          pack = "corechart";
        }

        callbacks.push({pack: pack, callback: callback});

        if (loaded[pack]) {
          runCallbacks();
        } else {
          loaded[pack] = true;

          // https://groups.google.com/forum/#!topic/google-visualization-api/fMKJcyA2yyI
          var loadOptions = {
            packages: [pack],
            callback: runCallbacks
          };
          if (config.language) {
            loadOptions.language = config.language;
          }
          google.load("visualization", "1", loadOptions);
        }
      };

      // Set chart options
      var defaultOptions = {
        chartArea: {},
        fontName: "'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helvetica, sans-serif",
        pointSize: 6,
        legend: {
          textStyle: {
            fontSize: 12,
            color: "#444"
          },
          alignment: "center",
          position: "right"
        },
        curveType: "function",
        hAxis: {
          textStyle: {
            color: "#666",
            fontSize: 12
          },
          titleTextStyle: {},
          gridlines: {
            color: "transparent"
          },
          baselineColor: "#ccc",
          viewWindow: {}
        },
        vAxis: {
          textStyle: {
            color: "#666",
            fontSize: 12
          },
          titleTextStyle: {},
          baselineColor: "#ccc",
          viewWindow: {}
        },
        tooltip: {
          textStyle: {
            color: "#666",
            fontSize: 12
          }
        }
      };

      var hideLegend = function (options) {
        options.legend.position = "none";
      };

      var setMin = function (options, min) {
        options.vAxis.viewWindow.min = min;
      };

      var setMax = function (options, max) {
        options.vAxis.viewWindow.max = max;
      };

      var setBarMin = function (options, min) {
        options.hAxis.viewWindow.min = min;
      };

      var setBarMax = function (options, max) {
        options.hAxis.viewWindow.max = max;
      };

      var setStacked = function (options) {
        options.isStacked = true;
      };

      var setXtitle = function (options, title) {
        options.hAxis.title = title;
        options.hAxis.titleTextStyle.italic = false;
      }

      var setYtitle = function (options, title) {
        options.vAxis.title = title;
        options.vAxis.titleTextStyle.italic = false;
      };

      var jsOptions = jsOptionsFunc(defaultOptions, hideLegend, setMin, setMax, setStacked, setXtitle, setYtitle);

      // cant use object as key
      var createDataTable = function (series, columnType) {
        var data = new google.visualization.DataTable();
        data.addColumn(columnType, "");

        var i, j, s, d, key, rows = [];
        for (i = 0; i < series.length; i++) {
          s = series[i];
          data.addColumn("number", s.name);

          for (j = 0; j < s.data.length; j++) {
            d = s.data[j];
            key = (columnType === "datetime") ? d[0].getTime() : d[0];
            if (!rows[key]) {
              rows[key] = new Array(series.length);
            }
            rows[key][i] = toFloat(d[1]);
          }
        }

        var rows2 = [];
        var value;
        for (i in rows) {
          if (rows.hasOwnProperty(i)) {
            if (columnType === "datetime") {
              value = new Date(toFloat(i));
            } else if (columnType === "number") {
              value = toFloat(i);
            } else {
              value = i;
            }
            rows2.push([value].concat(rows[i]));
          }
        }
        if (columnType === "datetime") {
          rows2.sort(sortByTime);
        }
        data.addRows(rows2);

        return data;
      };

      var resize = function (callback) {
        if (window.attachEvent) {
          window.attachEvent("onresize", callback);
        } else if (window.addEventListener) {
          window.addEventListener("resize", callback, true);
        }
        callback();
      };

      this.renderLineChart = function (chart) {
        waitForLoaded(function () {
          var options = jsOptions(chart.data, chart.options);
          var data = createDataTable(chart.data, chart.options.discrete ? "string" : "datetime");
          chart.chart = new google.visualization.LineChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderPieChart = function (chart) {
        waitForLoaded(function () {
          var chartOptions = {
            chartArea: {
              top: "10%",
              height: "80%"
            }
          };
          if (chart.options.colors) {
            chartOptions.colors = chart.options.colors;
          }
          var options = merge(merge(defaultOptions, chartOptions), chart.options.library || {});

          var data = new google.visualization.DataTable();
          data.addColumn("string", "");
          data.addColumn("number", "Value");
          data.addRows(chart.data);

          chart.chart = new google.visualization.PieChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderColumnChart = function (chart) {
        waitForLoaded(function () {
          var options = jsOptions(chart.data, chart.options);
          var data = createDataTable(chart.data, "string");
          chart.chart = new google.visualization.ColumnChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderBarChart = function (chart) {
        waitForLoaded(function () {
          var chartOptions = {
            hAxis: {
              gridlines: {
                color: "#ccc"
              }
            }
          };
          var options = jsOptionsFunc(defaultOptions, hideLegend, setBarMin, setBarMax, setStacked)(chart.data, chart.options, chartOptions);
          var data = createDataTable(chart.data, "string");
          chart.chart = new google.visualization.BarChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderAreaChart = function (chart) {
        waitForLoaded(function () {
          var chartOptions = {
            isStacked: true,
            pointSize: 0,
            areaOpacity: 0.5
          };
          var options = jsOptions(chart.data, chart.options, chartOptions);
          var data = createDataTable(chart.data, chart.options.discrete ? "string" : "datetime");
          chart.chart = new google.visualization.AreaChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderGeoChart = function (chart) {
        waitForLoaded(function () {
          var chartOptions = {
            legend: "none",
            colorAxis: {
              colors: chart.options.colors || ["#f6c7b6", "#ce502d"]
            }
          };
          var options = merge(merge(defaultOptions, chartOptions), chart.options.library || {});

          var data = new google.visualization.DataTable();
          data.addColumn("string", "");
          data.addColumn("number", "Value");
          data.addRows(chart.data);

          chart.chart = new google.visualization.GeoChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderScatterChart = function (chart) {
        waitForLoaded(function () {
          var chartOptions = {};
          var options = jsOptions(chart.data, chart.options, chartOptions);
          var data = createDataTable(chart.data, "number");

          chart.chart = new google.visualization.ScatterChart(chart.element);
          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };

      this.renderTimeline = function (chart) {
        waitForLoaded("timeline", function () {
          var chartOptions = {
            legend: "none"
          };

          if (chart.options.colors) {
            chartOptions.colors = chart.options.colors;
          }
          var options = merge(merge(defaultOptions, chartOptions), chart.options.library || {});

          var data = new google.visualization.DataTable();
          data.addColumn({type: "string", id: "Name"});
          data.addColumn({type: "date", id: "Start"});
          data.addColumn({type: "date", id: "End"});
          data.addRows(chart.data);

          chart.chart = new google.visualization.Timeline(chart.element);

          resize(function () {
            chart.chart.draw(data, options);
          });
        });
      };
    };

    adapters.push(GoogleChartsAdapter);
  }

  // TODO remove chartType if cross-browser way
  // to get the name of the chart class
  function renderChart(chartType, chart) {
    var i, adapter, fnName, adapterName;
    fnName = "render" + chartType;
    adapterName = chart.options.adapter;
    for (i = 0; i < adapters.length; i++) {
      adapter = adapters[i];
      if ((!adapterName || adapterName === adapter.name) && isFunction(adapter[fnName])) {
        return adapter[fnName](chart);
      }
    }
    throw new Error("No adapter found");
  }

  // process data

  var toFormattedKey = function (key, keyType) {
    if (keyType === "number") {
      key = toFloat(key);
    } else if (keyType === "datetime") {
      key = toDate(key);
    } else {
      key = toStr(key);
    }
    return key;
  };

  var formatSeriesData = function (data, keyType) {
    var r = [], key, j;
    for (j = 0; j < data.length; j++) {
      key = toFormattedKey(data[j][0], keyType);
      r.push([key, toFloat(data[j][1])]);
    }
    if (keyType === "datetime") {
      r.sort(sortByTime);
    }
    return r;
  };

  function processSeries(series, opts, keyType) {
    var i;

    // see if one series or multiple
    if (!isArray(series) || typeof series[0] !== "object" || isArray(series[0])) {
      series = [{name: "Value", data: series}];
      opts.hideLegend = true;
    } else {
      opts.hideLegend = false;
    }
    if (opts.discrete) {
      keyType = "string";
    }

    // right format
    for (i = 0; i < series.length; i++) {
      series[i].data = formatSeriesData(toArr(series[i].data), keyType);
    }

    return series;
  }

  function processSimple(data) {
    var perfectData = toArr(data), i;
    for (i = 0; i < perfectData.length; i++) {
      perfectData[i] = [toStr(perfectData[i][0]), toFloat(perfectData[i][1])];
    }
    return perfectData;
  }

  function processTime(data)
  {
    var i;
    for (i = 0; i < data.length; i++) {
      data[i][1] = toDate(data[i][1]);
      data[i][2] = toDate(data[i][2]);
    }
    return data;
  }

  function processLineData(chart) {
    chart.data = processSeries(chart.data, chart.options, "datetime");
    renderChart("LineChart", chart);
  }

  function processColumnData(chart) {
    chart.data = processSeries(chart.data, chart.options, "string");
    renderChart("ColumnChart", chart);
  }

  function processPieData(chart) {
    chart.data = processSimple(chart.data);
    renderChart("PieChart", chart);
  }

  function processBarData(chart) {
    chart.data = processSeries(chart.data, chart.options, "string");
    renderChart("BarChart", chart);
  }

  function processAreaData(chart) {
    chart.data = processSeries(chart.data, chart.options, "datetime");
    renderChart("AreaChart", chart);
  }

  function processGeoData(chart) {
    chart.data = processSimple(chart.data);
    renderChart("GeoChart", chart);
  }

  function processScatterData(chart) {
    chart.data = processSeries(chart.data, chart.options, "number");
    renderChart("ScatterChart", chart);
  }

  function processTimelineData(chart) {
    chart.data = processTime(chart.data);
    renderChart("Timeline", chart);
  }

  function setElement(chart, element, dataSource, opts, callback) {
    if (typeof element === "string") {
      element = document.getElementById(element);
    }
    chart.element = element;
    chart.options = opts || {};
    chart.dataSource = dataSource;
    Chartkick.charts[element.id] = chart;
    fetchDataSource(chart, callback);
  }

  // define classes

  Chartkick = {
    LineChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processLineData);
    },
    PieChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processPieData);
    },
    ColumnChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processColumnData);
    },
    BarChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processBarData);
    },
    AreaChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processAreaData);
    },
    GeoChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processGeoData);
    },
    ScatterChart: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processScatterData);
    },
    Timeline: function (element, dataSource, opts) {
      setElement(this, element, dataSource, opts, processTimelineData);
    },
    charts: {}
  };

  window.Chartkick = Chartkick;
}(window));
(function() {


}).call(this);

var FlushJob = function(eventBatch, loggerStorage, localStorageKey) {
  
  var self = this
  
  var skey = localStorageKey || Logger.generateId()
  self.key = skey
  self.storage = loggerStorage
  
  //instead should be the event batch, or the locally stored key
  self.job = (localStorageKey) ? self.storage.getObject(localStorageKey) : eventBatch
  
  //example jobs structure:
  //[key1, key2, key3]
  var jobs = self.storage.getObject("jobs")
  
  if(!jobs) {
    self.storage.setObject("jobs", [])
    jobs = []
  }
  
  if(!localStorageKey)
    jobs.push(skey)
  
  //example structure for job:
  //{key1: eventCueArr}
  this.storage.setObject(skey, this.job)
  this.storage.setObject("jobs", jobs)
}

FlushJob.prototype.flush = function(afterCb) {
  if(!this.job) return;
  var self = this;
  try {
  $.ajax({
    method:"POST",
    url:"/ahoy/events",
    data:JSON.stringify(self.job),
    success:function(){
      if(afterCb)
        afterCb();
      self.clear();
    },
    error:function(){
      if(afterCb)
        afterCb();
      self.clear();
    }
  })
  } catch (err){
      console.log("ERR in FlushJob flush(): ", err)
      self.clear();
  }
}

FlushJob.prototype.clear = function() {
  var jobs = this.storage.getObject("jobs");
 
  this.storage.removeObject(this.key);
    
  var index = jobs.indexOf(this.key);
  if (index > -1) {
      jobs.splice(index, 1);
  }
  this.storage.setObject("jobs", jobs);
}
;
function getUserBrowser(uaStrArg){
  var uaStr = uaStrArg || navigator.userAgent
  var ua= uaStr, tem, 
  M= ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
  if(/trident/i.test(M[1])){
      tem=  /\brv[ :]+(\d+)/g.exec(ua) || [];
      return 'IE '+(tem[1] || '');
  }
  if(M[1]=== 'Chrome'){
      tem= ua.match(/\bOPR\/(\d+)/)
      if(tem!= null) return 'Opera '+tem[1];
  }
  M= M[2]? [M[1], M[2]]: [navigator.appName, navigator.appVersion, '-?'];
  if((tem= ua.match(/version\/(\d+)/i))!= null) M.splice(1, 1, tem[1]);
  return M.join(' ');
}
;
!function(d,y,k,j){function s(a,b){var c=Math.max(0,a[0]-b[0],b[0]-a[1]),e=Math.max(0,a[2]-b[1],b[1]-a[3]);return c+e}function t(a,b,c,e){for(var h=a.length,e=e?"offset":"position",c=c||0;h--;){var f=a[h].el?a[h].el:d(a[h]),i=f[e]();i.left+=parseInt(f.css("margin-left"),10);i.top+=parseInt(f.css("margin-top"),10);b[h]=[i.left-c,i.left+f.outerWidth()+c,i.top-c,i.top+f.outerHeight()+c]}}function m(a,b){var c=b.offset();return{left:a.left-c.left,top:a.top-c.top}}function u(a,b,c){for(var b=[b.left,b.top],
c=c&&[c.left,c.top],e,h=a.length,d=[];h--;)e=a[h],d[h]=[h,s(e,b),c&&s(e,c)];return d=d.sort(function(a,b){return b[1]-a[1]||b[2]-a[2]||b[0]-a[0]})}function n(a){this.options=d.extend({},l,a);this.containers=[];this.options.rootGroup||(this.scrollProxy=d.proxy(this.scroll,this),this.dragProxy=d.proxy(this.drag,this),this.dropProxy=d.proxy(this.drop,this),this.placeholder=d(this.options.placeholder),a.isValidTarget||(this.options.isValidTarget=j))}function q(a,b){this.el=a;this.options=d.extend({},
w,b);this.group=n.get(this.options);this.rootGroup=this.options.rootGroup||this.group;this.handle=this.rootGroup.options.handle||this.rootGroup.options.itemSelector;var c=this.rootGroup.options.itemPath;this.target=c?this.el.find(c):this.el;this.target.on(o.start,this.handle,d.proxy(this.dragInit,this));this.options.drop&&this.group.containers.push(this)}var o,w={drag:!0,drop:!0,exclude:"",nested:!0,vertical:!0},l={afterMove:function(){},containerPath:"",containerSelector:"ol, ul",distance:0,delay:0,
handle:"",itemPath:"",itemSelector:"li",isValidTarget:function(){return!0},onCancel:function(){},onDrag:function(a,b){a.css(b)},onDragStart:function(a){a.css({height:a.height(),width:a.width()});a.addClass("dragged");d("body").addClass("dragging")},onDrop:function(a){a.removeClass("dragged").removeAttr("style");d("body").removeClass("dragging")},onMousedown:function(a,b,c){if(!c.target.nodeName.match(/^(input|select)$/i))return c.preventDefault(),!0},placeholder:'<li class="placeholder"/>',pullPlaceholder:!0,
serialize:function(a,b,c){a=d.extend({},a.data());if(c)return[b];b[0]&&(a.children=b);delete a.subContainers;delete a.sortable;return a},tolerance:0},p={},v=0,x={left:0,top:0,bottom:0,right:0};o={start:"touchstart.sortable mousedown.sortable",drop:"touchend.sortable touchcancel.sortable mouseup.sortable",drag:"touchmove.sortable mousemove.sortable",scroll:"scroll.sortable"};n.get=function(a){p[a.group]||(a.group===j&&(a.group=v++),p[a.group]=new n(a));return p[a.group]};n.prototype={dragInit:function(a,
b){this.$document=d(b.el[0].ownerDocument);this.item=d(a.target).closest(this.options.itemSelector);this.itemContainer=b;!this.item.is(this.options.exclude)&&this.options.onMousedown(this.item,l.onMousedown,a)&&(this.setPointer(a),this.toggleListeners("on"),this.setupDelayTimer(),this.dragInitDone=!0)},drag:function(a){if(!this.dragging){if(!this.distanceMet(a)||!this.delayMet)return;this.options.onDragStart(this.item,this.itemContainer,l.onDragStart,a);this.item.before(this.placeholder);this.dragging=
!0}this.setPointer(a);this.options.onDrag(this.item,m(this.pointer,this.item.offsetParent()),l.onDrag,a);var b=a.pageX||a.originalEvent.pageX,a=a.pageY||a.originalEvent.pageY,c=this.sameResultBox,e=this.options.tolerance;if(!c||c.top-e>a||c.bottom+e<a||c.left-e>b||c.right+e<b)this.searchValidTarget()||this.placeholder.detach()},drop:function(a){this.toggleListeners("off");this.dragInitDone=!1;if(this.dragging){if(this.placeholder.closest("html")[0])this.placeholder.before(this.item).detach();else this.options.onCancel(this.item,
this.itemContainer,l.onCancel,a);this.options.onDrop(this.item,this.getContainer(this.item),l.onDrop,a);this.clearDimensions();this.clearOffsetParent();this.lastAppendedItem=this.sameResultBox=j;this.dragging=!1}},searchValidTarget:function(a,b){a||(a=this.relativePointer||this.pointer,b=this.lastRelativePointer||this.lastPointer);for(var c=u(this.getContainerDimensions(),a,b),e=c.length;e--;){var d=c[e][0];if(!c[e][1]||this.options.pullPlaceholder)if(d=this.containers[d],!d.disabled){if(!this.$getOffsetParent())var f=
d.getItemOffsetParent(),a=m(a,f),b=m(b,f);if(d.searchValidTarget(a,b))return!0}}this.sameResultBox&&(this.sameResultBox=j)},movePlaceholder:function(a,b,c,e){var d=this.lastAppendedItem;if(e||!(d&&d[0]===b[0]))b[c](this.placeholder),this.lastAppendedItem=b,this.sameResultBox=e,this.options.afterMove(this.placeholder,a,b)},getContainerDimensions:function(){this.containerDimensions||t(this.containers,this.containerDimensions=[],this.options.tolerance,!this.$getOffsetParent());return this.containerDimensions},
getContainer:function(a){return a.closest(this.options.containerSelector).data(k)},$getOffsetParent:function(){if(this.offsetParent===j){var a=this.containers.length-1,b=this.containers[a].getItemOffsetParent();if(!this.options.rootGroup)for(;a--;)if(b[0]!=this.containers[a].getItemOffsetParent()[0]){b=!1;break}this.offsetParent=b}return this.offsetParent},setPointer:function(a){a=this.getPointer(a);if(this.$getOffsetParent()){var b=m(a,this.$getOffsetParent());this.lastRelativePointer=this.relativePointer;
this.relativePointer=b}this.lastPointer=this.pointer;this.pointer=a},distanceMet:function(a){a=this.getPointer(a);return Math.max(Math.abs(this.pointer.left-a.left),Math.abs(this.pointer.top-a.top))>=this.options.distance},getPointer:function(a){return{left:a.pageX||a.originalEvent.pageX,top:a.pageY||a.originalEvent.pageY}},setupDelayTimer:function(){var a=this;this.delayMet=!this.options.delay;this.delayMet||(clearTimeout(this._mouseDelayTimer),this._mouseDelayTimer=setTimeout(function(){a.delayMet=
!0},this.options.delay))},scroll:function(){this.clearDimensions();this.clearOffsetParent()},toggleListeners:function(a){var b=this;d.each(["drag","drop","scroll"],function(c,e){b.$document[a](o[e],b[e+"Proxy"])})},clearOffsetParent:function(){this.offsetParent=j},clearDimensions:function(){this.traverse(function(a){a._clearDimensions()})},traverse:function(a){a(this);for(var b=this.containers.length;b--;)this.containers[b].traverse(a)},_clearDimensions:function(){this.containerDimensions=j},_destroy:function(){p[this.options.group]=
j}};q.prototype={dragInit:function(a){var b=this.rootGroup;!this.disabled&&!b.dragInitDone&&this.options.drag&&this.isValidDrag(a)&&b.dragInit(a,this)},isValidDrag:function(a){return 1==a.which||"touchstart"==a.type&&1==a.originalEvent.touches.length},searchValidTarget:function(a,b){var c=u(this.getItemDimensions(),a,b),e=c.length,d=this.rootGroup,f=!d.options.isValidTarget||d.options.isValidTarget(d.item,this);if(!e&&f)return d.movePlaceholder(this,this.target,"append"),!0;for(;e--;)if(d=c[e][0],
!c[e][1]&&this.hasChildGroup(d)){if(this.getContainerGroup(d).searchValidTarget(a,b))return!0}else if(f)return this.movePlaceholder(d,a),!0},movePlaceholder:function(a,b){var c=d(this.items[a]),e=this.itemDimensions[a],h="after",f=c.outerWidth(),i=c.outerHeight(),g=c.offset(),g={left:g.left,right:g.left+f,top:g.top,bottom:g.top+i};this.options.vertical?b.top<=(e[2]+e[3])/2?(h="before",g.bottom-=i/2):g.top+=i/2:b.left<=(e[0]+e[1])/2?(h="before",g.right-=f/2):g.left+=f/2;this.hasChildGroup(a)&&(g=x);
this.rootGroup.movePlaceholder(this,c,h,g)},getItemDimensions:function(){this.itemDimensions||(this.items=this.$getChildren(this.el,"item").filter(":not(.placeholder, .dragged)").get(),t(this.items,this.itemDimensions=[],this.options.tolerance));return this.itemDimensions},getItemOffsetParent:function(){var a=this.el;return"relative"===a.css("position")||"absolute"===a.css("position")||"fixed"===a.css("position")?a:a.offsetParent()},hasChildGroup:function(a){return this.options.nested&&this.getContainerGroup(a)},
getContainerGroup:function(a){var b=d.data(this.items[a],"subContainers");if(b===j){var c=this.$getChildren(this.items[a],"container"),b=!1;c[0]&&(b=d.extend({},this.options,{rootGroup:this.rootGroup,group:v++}),b=c[k](b).data(k).group);d.data(this.items[a],"subContainers",b)}return b},$getChildren:function(a,b){var c=this.rootGroup.options,e=c[b+"Path"],c=c[b+"Selector"],a=d(a);e&&(a=a.find(e));return a.children(c)},_serialize:function(a,b){var c=this,e=this.$getChildren(a,b?"item":"container").not(this.options.exclude).map(function(){return c._serialize(d(this),
!b)}).get();return this.rootGroup.options.serialize(a,e,b)},traverse:function(a){d.each(this.items||[],function(){var b=d.data(this,"subContainers");b&&b.traverse(a)});a(this)},_clearDimensions:function(){this.itemDimensions=j},_destroy:function(){var a=this;this.target.off(o.start,this.handle);this.el.removeData(k);this.options.drop&&(this.group.containers=d.grep(this.group.containers,function(b){return b!=a}));d.each(this.items||[],function(){d.removeData(this,"subContainers")})}};var r={enable:function(){this.traverse(function(a){a.disabled=
!1})},disable:function(){this.traverse(function(a){a.disabled=!0})},serialize:function(){return this._serialize(this.el,!0)},refresh:function(){this.traverse(function(a){a._clearDimensions()})},destroy:function(){this.traverse(function(a){a._destroy()})}};d.extend(q.prototype,r);d.fn[k]=function(a){var b=Array.prototype.slice.call(arguments,1);return this.map(function(){var c=d(this),e=c.data(k);if(e&&r[a])return r[a].apply(e,b)||this;!e&&(a===j||"object"===typeof a)&&c.data(k,new q(c,a));return this})}}(jQuery,
window,"sortable");
/*!
	Colorbox v1.5.10 - 2014-06-26
	jQuery lightbox and modal window plugin
	(c) 2014 Jack Moore - http://www.jacklmoore.com/colorbox
	license: http://www.opensource.org/licenses/mit-license.php
*/

(function ($, document, window) {
	var
	// Default settings object.
	// See http://jacklmoore.com/colorbox for details.
	defaults = {
		// data sources
		html: false,
		photo: false,
		iframe: false,
		inline: false,

		// behavior and appearance
		transition: "elastic",
		speed: 300,
		fadeOut: 300,
		width: false,
		initialWidth: "600",
		innerWidth: false,
		maxWidth: false,
		height: false,
		initialHeight: "450",
		innerHeight: false,
		maxHeight: false,
		scalePhotos: true,
		scrolling: true,
		opacity: 0.9,
		preloading: true,
		className: false,
		overlayClose: true,
		escKey: true,
		arrowKey: true,
		top: false,
		bottom: false,
		left: false,
		right: false,
		fixed: false,
		data: undefined,
		closeButton: true,
		fastIframe: true,
		open: false,
		reposition: true,
		loop: true,
		slideshow: false,
		slideshowAuto: true,
		slideshowSpeed: 2500,
		slideshowStart: "start slideshow",
		slideshowStop: "stop slideshow",
		photoRegex: /\.(gif|png|jp(e|g|eg)|bmp|ico|webp|jxr|svg)((#|\?).*)?$/i,

		// alternate image paths for high-res displays
		retinaImage: false,
		retinaUrl: false,
		retinaSuffix: '@2x.$1',

		// internationalization
		current: "image {current} of {total}",
		previous: "previous",
		next: "next",
		close: "close",
		xhrError: "This content failed to load.",
		imgError: "This image failed to load.",

		// accessbility
		returnFocus: true,
		trapFocus: true,

		// callbacks
		onOpen: false,
		onLoad: false,
		onComplete: false,
		onCleanup: false,
		onClosed: false,

		rel: function() {
			return this.rel;
		},
		href: function() {
			// using this.href would give the absolute url, when the href may have been inteded as a selector (e.g. '#container')
			return $(this).attr('href');
		},
		title: function() {
			return this.title;
		}
	},

	// Abstracting the HTML and event identifiers for easy rebranding
	colorbox = 'colorbox',
	prefix = 'cbox',
	boxElement = prefix + 'Element',
	
	// Events
	event_open = prefix + '_open',
	event_load = prefix + '_load',
	event_complete = prefix + '_complete',
	event_cleanup = prefix + '_cleanup',
	event_closed = prefix + '_closed',
	event_purge = prefix + '_purge',

	// Cached jQuery Object Variables
	$overlay,
	$box,
	$wrap,
	$content,
	$topBorder,
	$leftBorder,
	$rightBorder,
	$bottomBorder,
	$related,
	$window,
	$loaded,
	$loadingBay,
	$loadingOverlay,
	$title,
	$current,
	$slideshow,
	$next,
	$prev,
	$close,
	$groupControls,
	$events = $('<a/>'), // $({}) would be prefered, but there is an issue with jQuery 1.4.2
	
	// Variables for cached values or use across multiple functions
	settings,
	interfaceHeight,
	interfaceWidth,
	loadedHeight,
	loadedWidth,
	index,
	photo,
	open,
	active,
	closing,
	loadingTimer,
	publicMethod,
	div = "div",
	requests = 0,
	previousCSS = {},
	init;

	// ****************
	// HELPER FUNCTIONS
	// ****************
	
	// Convenience function for creating new jQuery objects
	function $tag(tag, id, css) {
		var element = document.createElement(tag);

		if (id) {
			element.id = prefix + id;
		}

		if (css) {
			element.style.cssText = css;
		}

		return $(element);
	}
	
	// Get the window height using innerHeight when available to avoid an issue with iOS
	// http://bugs.jquery.com/ticket/6724
	function winheight() {
		return window.innerHeight ? window.innerHeight : $(window).height();
	}

	function Settings(element, options) {
		if (options !== Object(options)) {
			options = {};
		}

		this.cache = {};
		this.el = element;

		this.value = function(key) {
			var dataAttr;

			if (this.cache[key] === undefined) {
				dataAttr = $(this.el).attr('data-cbox-'+key);

				if (dataAttr !== undefined) {
					this.cache[key] = dataAttr;
				} else if (options[key] !== undefined) {
					this.cache[key] = options[key];
				} else if (defaults[key] !== undefined) {
					this.cache[key] = defaults[key];
				}
			}

			return this.cache[key];
		};

		this.get = function(key) {
			var value = this.value(key);
			return $.isFunction(value) ? value.call(this.el, this) : value;
		};
	}

	// Determine the next and previous members in a group.
	function getIndex(increment) {
		var
		max = $related.length,
		newIndex = (index + increment) % max;
		
		return (newIndex < 0) ? max + newIndex : newIndex;
	}

	// Convert '%' and 'px' values to integers
	function setSize(size, dimension) {
		return Math.round((/%/.test(size) ? ((dimension === 'x' ? $window.width() : winheight()) / 100) : 1) * parseInt(size, 10));
	}
	
	// Checks an href to see if it is a photo.
	// There is a force photo option (photo: true) for hrefs that cannot be matched by the regex.
	function isImage(settings, url) {
		return settings.get('photo') || settings.get('photoRegex').test(url);
	}

	function retinaUrl(settings, url) {
		return settings.get('retinaUrl') && window.devicePixelRatio > 1 ? url.replace(settings.get('photoRegex'), settings.get('retinaSuffix')) : url;
	}

	function trapFocus(e) {
		if ('contains' in $box[0] && !$box[0].contains(e.target) && e.target !== $overlay[0]) {
			e.stopPropagation();
			$box.focus();
		}
	}

	function setClass(str) {
		if (setClass.str !== str) {
			$box.add($overlay).removeClass(setClass.str).addClass(str);
			setClass.str = str;
		}
	}

	function getRelated(rel) {
		index = 0;
		
		if (rel && rel !== false && rel !== 'nofollow') {
			$related = $('.' + boxElement).filter(function () {
				var options = $.data(this, colorbox);
				var settings = new Settings(this, options);
				return (settings.get('rel') === rel);
			});
			index = $related.index(settings.el);
			
			// Check direct calls to Colorbox.
			if (index === -1) {
				$related = $related.add(settings.el);
				index = $related.length - 1;
			}
		} else {
			$related = $(settings.el);
		}
	}

	function trigger(event) {
		// for external use
		$(document).trigger(event);
		// for internal use
		$events.triggerHandler(event);
	}

	var slideshow = (function(){
		var active,
			className = prefix + "Slideshow_",
			click = "click." + prefix,
			timeOut;

		function clear () {
			clearTimeout(timeOut);
		}

		function set() {
			if (settings.get('loop') || $related[index + 1]) {
				clear();
				timeOut = setTimeout(publicMethod.next, settings.get('slideshowSpeed'));
			}
		}

		function start() {
			$slideshow
				.html(settings.get('slideshowStop'))
				.unbind(click)
				.one(click, stop);

			$events
				.bind(event_complete, set)
				.bind(event_load, clear);

			$box.removeClass(className + "off").addClass(className + "on");
		}

		function stop() {
			clear();
			
			$events
				.unbind(event_complete, set)
				.unbind(event_load, clear);

			$slideshow
				.html(settings.get('slideshowStart'))
				.unbind(click)
				.one(click, function () {
					publicMethod.next();
					start();
				});

			$box.removeClass(className + "on").addClass(className + "off");
		}

		function reset() {
			active = false;
			$slideshow.hide();
			clear();
			$events
				.unbind(event_complete, set)
				.unbind(event_load, clear);
			$box.removeClass(className + "off " + className + "on");
		}

		return function(){
			if (active) {
				if (!settings.get('slideshow')) {
					$events.unbind(event_cleanup, reset);
					reset();
				}
			} else {
				if (settings.get('slideshow') && $related[1]) {
					active = true;
					$events.one(event_cleanup, reset);
					if (settings.get('slideshowAuto')) {
						start();
					} else {
						stop();
					}
					$slideshow.show();
				}
			}
		};

	}());


	function launch(element) {
		var options;

		if (!closing) {

			options = $(element).data(colorbox);

			settings = new Settings(element, options);
			
			getRelated(settings.get('rel'));

			if (!open) {
				open = active = true; // Prevents the page-change action from queuing up if the visitor holds down the left or right keys.

				setClass(settings.get('className'));
				
				// Show colorbox so the sizes can be calculated in older versions of jQuery
				$box.css({visibility:'hidden', display:'block', opacity:''});
				
				$loaded = $tag(div, 'LoadedContent', 'width:0; height:0; overflow:hidden; visibility:hidden');
				$content.css({width:'', height:''}).append($loaded);

				// Cache values needed for size calculations
				interfaceHeight = $topBorder.height() + $bottomBorder.height() + $content.outerHeight(true) - $content.height();
				interfaceWidth = $leftBorder.width() + $rightBorder.width() + $content.outerWidth(true) - $content.width();
				loadedHeight = $loaded.outerHeight(true);
				loadedWidth = $loaded.outerWidth(true);

				// Opens inital empty Colorbox prior to content being loaded.
				var initialWidth = setSize(settings.get('initialWidth'), 'x');
				var initialHeight = setSize(settings.get('initialHeight'), 'y');
				var maxWidth = settings.get('maxWidth');
				var maxHeight = settings.get('maxHeight');

				settings.w = (maxWidth !== false ? Math.min(initialWidth, setSize(maxWidth, 'x')) : initialWidth) - loadedWidth - interfaceWidth;
				settings.h = (maxHeight !== false ? Math.min(initialHeight, setSize(maxHeight, 'y')) : initialHeight) - loadedHeight - interfaceHeight;

				$loaded.css({width:'', height:settings.h});
				publicMethod.position();

				trigger(event_open);
				settings.get('onOpen');

				$groupControls.add($title).hide();

				$box.focus();
				
				if (settings.get('trapFocus')) {
					// Confine focus to the modal
					// Uses event capturing that is not supported in IE8-
					if (document.addEventListener) {

						document.addEventListener('focus', trapFocus, true);
						
						$events.one(event_closed, function () {
							document.removeEventListener('focus', trapFocus, true);
						});
					}
				}

				// Return focus on closing
				if (settings.get('returnFocus')) {
					$events.one(event_closed, function () {
						$(settings.el).focus();
					});
				}
			}

			$overlay.css({
				opacity: parseFloat(settings.get('opacity')) || '',
				cursor: settings.get('overlayClose') ? 'pointer' : '',
				visibility: 'visible'
			}).show();
			
			if (settings.get('closeButton')) {
				$close.html(settings.get('close')).appendTo($content);
			} else {
				$close.appendTo('<div/>'); // replace with .detach() when dropping jQuery < 1.4
			}

			load();
		}
	}

	// Colorbox's markup needs to be added to the DOM prior to being called
	// so that the browser will go ahead and load the CSS background images.
	function appendHTML() {
		if (!$box && document.body) {
			init = false;
			$window = $(window);
			$box = $tag(div).attr({
				id: colorbox,
				'class': $.support.opacity === false ? prefix + 'IE' : '', // class for optional IE8 & lower targeted CSS.
				role: 'dialog',
				tabindex: '-1'
			}).hide();
			$overlay = $tag(div, "Overlay").hide();
			$loadingOverlay = $([$tag(div, "LoadingOverlay")[0],$tag(div, "LoadingGraphic")[0]]);
			$wrap = $tag(div, "Wrapper");
			$content = $tag(div, "Content").append(
				$title = $tag(div, "Title"),
				$current = $tag(div, "Current"),
				$prev = $('<button type="button"/>').attr({id:prefix+'Previous'}),
				$next = $('<button type="button"/>').attr({id:prefix+'Next'}),
				$slideshow = $tag('button', "Slideshow"),
				$loadingOverlay
			);

			$close = $('<button type="button"/>').attr({id:prefix+'Close'});
			
			$wrap.append( // The 3x3 Grid that makes up Colorbox
				$tag(div).append(
					$tag(div, "TopLeft"),
					$topBorder = $tag(div, "TopCenter"),
					$tag(div, "TopRight")
				),
				$tag(div, false, 'clear:left').append(
					$leftBorder = $tag(div, "MiddleLeft"),
					$content,
					$rightBorder = $tag(div, "MiddleRight")
				),
				$tag(div, false, 'clear:left').append(
					$tag(div, "BottomLeft"),
					$bottomBorder = $tag(div, "BottomCenter"),
					$tag(div, "BottomRight")
				)
			).find('div div').css({'float': 'left'});
			
			$loadingBay = $tag(div, false, 'position:absolute; width:9999px; visibility:hidden; display:none; max-width:none;');
			
			$groupControls = $next.add($prev).add($current).add($slideshow);

			$(document.body).append($overlay, $box.append($wrap, $loadingBay));
		}
	}

	// Add Colorbox's event bindings
	function addBindings() {
		function clickHandler(e) {
			// ignore non-left-mouse-clicks and clicks modified with ctrl / command, shift, or alt.
			// See: http://jacklmoore.com/notes/click-events/
			if (!(e.which > 1 || e.shiftKey || e.altKey || e.metaKey || e.ctrlKey)) {
				e.preventDefault();
				launch(this);
			}
		}

		if ($box) {
			if (!init) {
				init = true;

				// Anonymous functions here keep the public method from being cached, thereby allowing them to be redefined on the fly.
				$next.click(function () {
					publicMethod.next();
				});
				$prev.click(function () {
					publicMethod.prev();
				});
				$close.click(function () {
					publicMethod.close();
				});
				$overlay.click(function () {
					if (settings.get('overlayClose')) {
						publicMethod.close();
					}
				});
				
				// Key Bindings
				$(document).bind('keydown.' + prefix, function (e) {
					var key = e.keyCode;
					if (open && settings.get('escKey') && key === 27) {
						e.preventDefault();
						publicMethod.close();
					}
					if (open && settings.get('arrowKey') && $related[1] && !e.altKey) {
						if (key === 37) {
							e.preventDefault();
							$prev.click();
						} else if (key === 39) {
							e.preventDefault();
							$next.click();
						}
					}
				});

				if ($.isFunction($.fn.on)) {
					// For jQuery 1.7+
					$(document).on('click.'+prefix, '.'+boxElement, clickHandler);
				} else {
					// For jQuery 1.3.x -> 1.6.x
					// This code is never reached in jQuery 1.9, so do not contact me about 'live' being removed.
					// This is not here for jQuery 1.9, it's here for legacy users.
					$('.'+boxElement).live('click.'+prefix, clickHandler);
				}
			}
			return true;
		}
		return false;
	}

	// Don't do anything if Colorbox already exists.
	if ($[colorbox]) {
		return;
	}

	// Append the HTML when the DOM loads
	$(appendHTML);


	// ****************
	// PUBLIC FUNCTIONS
	// Usage format: $.colorbox.close();
	// Usage from within an iframe: parent.jQuery.colorbox.close();
	// ****************
	
	publicMethod = $.fn[colorbox] = $[colorbox] = function (options, callback) {
		var settings;
		var $obj = this;

		options = options || {};

		if ($.isFunction($obj)) { // assume a call to $.colorbox
			$obj = $('<a/>');
			options.open = true;
		} else if (!$obj[0]) { // colorbox being applied to empty collection
			return $obj;
		}


		if (!$obj[0]) { // colorbox being applied to empty collection
			return $obj;
		}
		
		appendHTML();

		if (addBindings()) {

			if (callback) {
				options.onComplete = callback;
			}

			$obj.each(function () {
				var old = $.data(this, colorbox) || {};
				$.data(this, colorbox, $.extend(old, options));
			}).addClass(boxElement);

			settings = new Settings($obj[0], options);
			
			if (settings.get('open')) {
				launch($obj[0]);
			}
		}
		
		return $obj;
	};

	publicMethod.position = function (speed, loadedCallback) {
		var
		css,
		top = 0,
		left = 0,
		offset = $box.offset(),
		scrollTop,
		scrollLeft;
		
		$window.unbind('resize.' + prefix);

		// remove the modal so that it doesn't influence the document width/height
		$box.css({top: -9e4, left: -9e4});

		scrollTop = $window.scrollTop();
		scrollLeft = $window.scrollLeft();

		if (settings.get('fixed')) {
			offset.top -= scrollTop;
			offset.left -= scrollLeft;
			$box.css({position: 'fixed'});
		} else {
			top = scrollTop;
			left = scrollLeft;
			$box.css({position: 'absolute'});
		}

		// keeps the top and left positions within the browser's viewport.
		if (settings.get('right') !== false) {
			left += Math.max($window.width() - settings.w - loadedWidth - interfaceWidth - setSize(settings.get('right'), 'x'), 0);
		} else if (settings.get('left') !== false) {
			left += setSize(settings.get('left'), 'x');
		} else {
			left += Math.round(Math.max($window.width() - settings.w - loadedWidth - interfaceWidth, 0) / 2);
		}
		
		if (settings.get('bottom') !== false) {
			top += Math.max(winheight() - settings.h - loadedHeight - interfaceHeight - setSize(settings.get('bottom'), 'y'), 0);
		} else if (settings.get('top') !== false) {
			top += setSize(settings.get('top'), 'y');
		} else {
			top += Math.round(Math.max(winheight() - settings.h - loadedHeight - interfaceHeight, 0) / 2);
		}

		$box.css({top: offset.top, left: offset.left, visibility:'visible'});
		
		// this gives the wrapper plenty of breathing room so it's floated contents can move around smoothly,
		// but it has to be shrank down around the size of div#colorbox when it's done.  If not,
		// it can invoke an obscure IE bug when using iframes.
		$wrap[0].style.width = $wrap[0].style.height = "9999px";
		
		function modalDimensions() {
			$topBorder[0].style.width = $bottomBorder[0].style.width = $content[0].style.width = (parseInt($box[0].style.width,10) - interfaceWidth)+'px';
			$content[0].style.height = $leftBorder[0].style.height = $rightBorder[0].style.height = (parseInt($box[0].style.height,10) - interfaceHeight)+'px';
		}

		css = {width: settings.w + loadedWidth + interfaceWidth, height: settings.h + loadedHeight + interfaceHeight, top: top, left: left};

		// setting the speed to 0 if the content hasn't changed size or position
		if (speed) {
			var tempSpeed = 0;
			$.each(css, function(i){
				if (css[i] !== previousCSS[i]) {
					tempSpeed = speed;
					return;
				}
			});
			speed = tempSpeed;
		}

		previousCSS = css;

		if (!speed) {
			$box.css(css);
		}

		$box.dequeue().animate(css, {
			duration: speed || 0,
			complete: function () {
				modalDimensions();
				
				active = false;
				
				// shrink the wrapper down to exactly the size of colorbox to avoid a bug in IE's iframe implementation.
				$wrap[0].style.width = (settings.w + loadedWidth + interfaceWidth) + "px";
				$wrap[0].style.height = (settings.h + loadedHeight + interfaceHeight) + "px";
				
				if (settings.get('reposition')) {
					setTimeout(function () {  // small delay before binding onresize due to an IE8 bug.
						$window.bind('resize.' + prefix, publicMethod.position);
					}, 1);
				}

				if (loadedCallback) {
					loadedCallback();
				}
			},
			step: modalDimensions
		});
	};

	publicMethod.resize = function (options) {
		var scrolltop;
		
		if (open) {
			options = options || {};
			
			if (options.width) {
				settings.w = setSize(options.width, 'x') - loadedWidth - interfaceWidth;
			}

			if (options.innerWidth) {
				settings.w = setSize(options.innerWidth, 'x');
			}

			$loaded.css({width: settings.w});
			
			if (options.height) {
				settings.h = setSize(options.height, 'y') - loadedHeight - interfaceHeight;
			}

			if (options.innerHeight) {
				settings.h = setSize(options.innerHeight, 'y');
			}

			if (!options.innerHeight && !options.height) {
				scrolltop = $loaded.scrollTop();
				$loaded.css({height: "auto"});
				settings.h = $loaded.height();
			}

			$loaded.css({height: settings.h});

			if(scrolltop) {
				$loaded.scrollTop(scrolltop);
			}
			
			publicMethod.position(settings.get('transition') === "none" ? 0 : settings.get('speed'));
		}
	};

	publicMethod.prep = function (object) {
		if (!open) {
			return;
		}
		
		var callback, speed = settings.get('transition') === "none" ? 0 : settings.get('speed');

		$loaded.remove();

		$loaded = $tag(div, 'LoadedContent').append(object);
		
		function getWidth() {
			settings.w = settings.w || $loaded.width();
			settings.w = settings.mw && settings.mw < settings.w ? settings.mw : settings.w;
			return settings.w;
		}
		function getHeight() {
			settings.h = settings.h || $loaded.height();
			settings.h = settings.mh && settings.mh < settings.h ? settings.mh : settings.h;
			return settings.h;
		}
		
		$loaded.hide()
		.appendTo($loadingBay.show())// content has to be appended to the DOM for accurate size calculations.
		.css({width: getWidth(), overflow: settings.get('scrolling') ? 'auto' : 'hidden'})
		.css({height: getHeight()})// sets the height independently from the width in case the new width influences the value of height.
		.prependTo($content);
		
		$loadingBay.hide();
		
		// floating the IMG removes the bottom line-height and fixed a problem where IE miscalculates the width of the parent element as 100% of the document width.
		
		$(photo).css({'float': 'none'});

		setClass(settings.get('className'));

		callback = function () {
			var total = $related.length,
				iframe,
				complete;
			
			if (!open) {
				return;
			}
			
			function removeFilter() { // Needed for IE8 in versions of jQuery prior to 1.7.2
				if ($.support.opacity === false) {
					$box[0].style.removeAttribute('filter');
				}
			}
			
			complete = function () {
				clearTimeout(loadingTimer);
				$loadingOverlay.hide();
				trigger(event_complete);
				settings.get('onComplete');
			};

			
			$title.html(settings.get('title')).show();
			$loaded.show();
			
			if (total > 1) { // handle grouping
				if (typeof settings.get('current') === "string") {
					$current.html(settings.get('current').replace('{current}', index + 1).replace('{total}', total)).show();
				}
				
				$next[(settings.get('loop') || index < total - 1) ? "show" : "hide"]().html(settings.get('next'));
				$prev[(settings.get('loop') || index) ? "show" : "hide"]().html(settings.get('previous'));
				
				slideshow();
				
				// Preloads images within a rel group
				if (settings.get('preloading')) {
					$.each([getIndex(-1), getIndex(1)], function(){
						var img,
							i = $related[this],
							settings = new Settings(i, $.data(i, colorbox)),
							src = settings.get('href');

						if (src && isImage(settings, src)) {
							src = retinaUrl(settings, src);
							img = document.createElement('img');
							img.src = src;
						}
					});
				}
			} else {
				$groupControls.hide();
			}
			
			if (settings.get('iframe')) {
				iframe = document.createElement('iframe');
				
				if ('frameBorder' in iframe) {
					iframe.frameBorder = 0;
				}
				
				if ('allowTransparency' in iframe) {
					iframe.allowTransparency = "true";
				}

				if (!settings.get('scrolling')) {
					iframe.scrolling = "no";
				}
				
				$(iframe)
					.attr({
						src: settings.get('href'),
						name: (new Date()).getTime(), // give the iframe a unique name to prevent caching
						'class': prefix + 'Iframe',
						allowFullScreen : true // allow HTML5 video to go fullscreen
					})
					.one('load', complete)
					.appendTo($loaded);
				
				$events.one(event_purge, function () {
					iframe.src = "//about:blank";
				});

				if (settings.get('fastIframe')) {
					$(iframe).trigger('load');
				}
			} else {
				complete();
			}
			
			if (settings.get('transition') === 'fade') {
				$box.fadeTo(speed, 1, removeFilter);
			} else {
				removeFilter();
			}
		};
		
		if (settings.get('transition') === 'fade') {
			$box.fadeTo(speed, 0, function () {
				publicMethod.position(0, callback);
			});
		} else {
			publicMethod.position(speed, callback);
		}
	};

	function load () {
		var href, setResize, prep = publicMethod.prep, $inline, request = ++requests;
		
		active = true;
		
		photo = false;
		
		trigger(event_purge);
		trigger(event_load);
		settings.get('onLoad');
		
		settings.h = settings.get('height') ?
				setSize(settings.get('height'), 'y') - loadedHeight - interfaceHeight :
				settings.get('innerHeight') && setSize(settings.get('innerHeight'), 'y');
		
		settings.w = settings.get('width') ?
				setSize(settings.get('width'), 'x') - loadedWidth - interfaceWidth :
				settings.get('innerWidth') && setSize(settings.get('innerWidth'), 'x');
		
		// Sets the minimum dimensions for use in image scaling
		settings.mw = settings.w;
		settings.mh = settings.h;
		
		// Re-evaluate the minimum width and height based on maxWidth and maxHeight values.
		// If the width or height exceed the maxWidth or maxHeight, use the maximum values instead.
		if (settings.get('maxWidth')) {
			settings.mw = setSize(settings.get('maxWidth'), 'x') - loadedWidth - interfaceWidth;
			settings.mw = settings.w && settings.w < settings.mw ? settings.w : settings.mw;
		}
		if (settings.get('maxHeight')) {
			settings.mh = setSize(settings.get('maxHeight'), 'y') - loadedHeight - interfaceHeight;
			settings.mh = settings.h && settings.h < settings.mh ? settings.h : settings.mh;
		}
		
		href = settings.get('href');
		
		loadingTimer = setTimeout(function () {
			$loadingOverlay.show();
		}, 100);
		
		if (settings.get('inline')) {
			var $target = $(href);
			// Inserts an empty placeholder where inline content is being pulled from.
			// An event is bound to put inline content back when Colorbox closes or loads new content.
			$inline = $('<div>').hide().insertBefore($target);

			$events.one(event_purge, function () {
				$inline.replaceWith($target);
			});

			prep($target);
		} else if (settings.get('iframe')) {
			// IFrame element won't be added to the DOM until it is ready to be displayed,
			// to avoid problems with DOM-ready JS that might be trying to run in that iframe.
			prep(" ");
		} else if (settings.get('html')) {
			prep(settings.get('html'));
		} else if (isImage(settings, href)) {

			href = retinaUrl(settings, href);

			photo = new Image();

			$(photo)
			.addClass(prefix + 'Photo')
			.bind('error',function () {
				prep($tag(div, 'Error').html(settings.get('imgError')));
			})
			.one('load', function () {
				if (request !== requests) {
					return;
				}

				// A small pause because some browsers will occassionaly report a 
				// img.width and img.height of zero immediately after the img.onload fires
				setTimeout(function(){
					var percent;

					$.each(['alt', 'longdesc', 'aria-describedby'], function(i,val){
						var attr = $(settings.el).attr(val) || $(settings.el).attr('data-'+val);
						if (attr) {
							photo.setAttribute(val, attr);
						}
					});

					if (settings.get('retinaImage') && window.devicePixelRatio > 1) {
						photo.height = photo.height / window.devicePixelRatio;
						photo.width = photo.width / window.devicePixelRatio;
					}

					if (settings.get('scalePhotos')) {
						setResize = function () {
							photo.height -= photo.height * percent;
							photo.width -= photo.width * percent;
						};
						if (settings.mw && photo.width > settings.mw) {
							percent = (photo.width - settings.mw) / photo.width;
							setResize();
						}
						if (settings.mh && photo.height > settings.mh) {
							percent = (photo.height - settings.mh) / photo.height;
							setResize();
						}
					}
					
					if (settings.h) {
						photo.style.marginTop = Math.max(settings.mh - photo.height, 0) / 2 + 'px';
					}
					
					if ($related[1] && (settings.get('loop') || $related[index + 1])) {
						photo.style.cursor = 'pointer';
						photo.onclick = function () {
							publicMethod.next();
						};
					}

					photo.style.width = photo.width + 'px';
					photo.style.height = photo.height + 'px';
					prep(photo);
				}, 1);
			});
			
			photo.src = href;

		} else if (href) {
			$loadingBay.load(href, settings.get('data'), function (data, status) {
				if (request === requests) {
					prep(status === 'error' ? $tag(div, 'Error').html(settings.get('xhrError')) : $(this).contents());
				}
			});
		}
	}
		
	// Navigates to the next page/image in a set.
	publicMethod.next = function () {
		if (!active && $related[1] && (settings.get('loop') || $related[index + 1])) {
			index = getIndex(1);
			launch($related[index]);
		}
	};
	
	publicMethod.prev = function () {
		if (!active && $related[1] && (settings.get('loop') || index)) {
			index = getIndex(-1);
			launch($related[index]);
		}
	};

	// Note: to use this within an iframe use the following format: parent.jQuery.colorbox.close();
	publicMethod.close = function () {
		if (open && !closing) {
			
			closing = true;
			open = false;
			trigger(event_cleanup);
			settings.get('onCleanup');
			$window.unbind('.' + prefix);
			$overlay.fadeTo(settings.get('fadeOut') || 0, 0);
			
			$box.stop().fadeTo(settings.get('fadeOut') || 0, 0, function () {
				$box.hide();
				$overlay.hide();
				trigger(event_purge);
				$loaded.remove();
				
				setTimeout(function () {
					closing = false;
					trigger(event_closed);
					settings.get('onClosed');
				}, 1);
			});
		}
	};

	// Removes changes Colorbox made to the document, but does not remove the plugin.
	publicMethod.remove = function () {
		if (!$box) { return; }

		$box.stop();
		$[colorbox].close();
		$box.stop(false, true).remove();
		$overlay.remove();
		closing = false;
		$box = null;
		$('.' + boxElement)
			.removeData(colorbox)
			.removeClass(boxElement);

		$(document).unbind('click.'+prefix).unbind('keydown.'+prefix);
	};

	// A method for fetching the current element Colorbox is referencing.
	// returns a jQuery object.
	publicMethod.element = function () {
		return $(settings.el);
	};

	publicMethod.settings = defaults;

}(jQuery, document, window));
/*!
 * jQuery Cookie Plugin v1.4.1
 * https://github.com/carhartl/jquery-cookie
 *
 * Copyright 2006, 2014 Klaus Hartl
 * Released under the MIT license
 */

(function (factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD
    define(['jquery'], factory);
  } else if (typeof exports === 'object') {
    // CommonJS
    factory(require('jquery'));
  } else {
    // Browser globals
    factory(jQuery);
  }
}(function ($) {

  var pluses = /\+/g;

  function encode(s) {
    return config.raw ? s : encodeURIComponent(s);
  }

  function decode(s) {
    return config.raw ? s : decodeURIComponent(s);
  }

  function stringifyCookieValue(value) {
    return encode(config.json ? JSON.stringify(value) : String(value));
  }

  function parseCookieValue(s) {
    if (s.indexOf('"') === 0) {
      // This is a quoted cookie as according to RFC2068, unescape...
      s = s.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, '\\');
    }

    try {
      // Replace server-side written pluses with spaces.
      // If we can't decode the cookie, ignore it, it's unusable.
      // If we can't parse the cookie, ignore it, it's unusable.
      s = decodeURIComponent(s.replace(pluses, ' '));
      return config.json ? JSON.parse(s) : s;
    } catch(e) {}
  }

  function read(s, converter) {
    var value = config.raw ? s : parseCookieValue(s);
    return $.isFunction(converter) ? converter(value) : value;
  }

  var config = $.cookie = function (key, value, options) {

    // Write

    if (arguments.length > 1 && !$.isFunction(value)) {
      options = $.extend({}, config.defaults, options);

      if (typeof options.expires === 'number') {
        var days = options.expires, t = options.expires = new Date();
        t.setTime(+t + days * 864e+5);
      }

      return (document.cookie = [
        encode(key), '=', stringifyCookieValue(value),
        options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
        options.path    ? '; path=' + options.path : '',
        options.domain  ? '; domain=' + options.domain : '',
        options.secure  ? '; secure' : ''
      ].join(''));
    }

    // Read

    var result = key ? undefined : {};

    // To prevent the for loop in the first place assign an empty array
    // in case there are no cookies at all. Also prevents odd result when
    // calling $.cookie().
    var cookies = document.cookie ? document.cookie.split('; ') : [];

    for (var i = 0, l = cookies.length; i < l; i++) {
      var parts = cookies[i].split('=');
      var name = decode(parts.shift());
      var cookie = parts.join('=');

      if (key && key === name) {
        // If second argument (value) is a function it's a converter...
        result = read(cookie, value);
        break;
      }

      // Prevent storing a cookie that we couldn't decode.
      if (!key && (cookie = read(cookie)) !== undefined) {
        result[name] = cookie;
      }
    }

    return result;
  };

  config.defaults = {};

  $.removeCookie = function (key, options) {
    if ($.cookie(key) === undefined) {
      return false;
    }

    // Must not alter options, thus extending a fresh object...
    $.cookie(key, '', $.extend({}, options, { expires: -1 }));
    return !$.cookie(key);
  };

}));
/*
 * jQuery Easing v1.3 - http://gsgd.co.uk/sandbox/jquery/easing/
 *
 * Uses the built in easing capabilities added In jQuery 1.1
 * to offer multiple easing options
 *
 * TERMS OF USE - jQuery Easing
 * 
 * Open source under the BSD License. 
 * 
 * Copyright Â© 2008 George McGinley Smith
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of 
 * conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list 
 * of conditions and the following disclaimer in the documentation and/or other materials 
 * provided with the distribution.
 * 
 * Neither the name of the author nor the names of contributors may be used to endorse 
 * or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 *  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
 *  GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 *
*/

// t: current time, b: begInnIng value, c: change In value, d: duration
jQuery.easing['jswing'] = jQuery.easing['swing'];

jQuery.extend( jQuery.easing,
{
        def: 'easeOutQuad',
        swing: function (x, t, b, c, d) {
                //alert(jQuery.easing.default);
                return jQuery.easing[jQuery.easing.def](x, t, b, c, d);
        },
        easeInQuad: function (x, t, b, c, d) {
                return c*(t/=d)*t + b;
        },
        easeOutQuad: function (x, t, b, c, d) {
                return -c *(t/=d)*(t-2) + b;
        },
        easeInOutQuad: function (x, t, b, c, d) {
                if ((t/=d/2) < 1) return c/2*t*t + b;
                return -c/2 * ((--t)*(t-2) - 1) + b;
        },
        easeInCubic: function (x, t, b, c, d) {
                return c*(t/=d)*t*t + b;
        },
        easeOutCubic: function (x, t, b, c, d) {
                return c*((t=t/d-1)*t*t + 1) + b;
        },
        easeInOutCubic: function (x, t, b, c, d) {
                if ((t/=d/2) < 1) return c/2*t*t*t + b;
                return c/2*((t-=2)*t*t + 2) + b;
        },
        easeInQuart: function (x, t, b, c, d) {
                return c*(t/=d)*t*t*t + b;
        },
        easeOutQuart: function (x, t, b, c, d) {
                return -c * ((t=t/d-1)*t*t*t - 1) + b;
        },
        easeInOutQuart: function (x, t, b, c, d) {
                if ((t/=d/2) < 1) return c/2*t*t*t*t + b;
                return -c/2 * ((t-=2)*t*t*t - 2) + b;
        },
        easeInQuint: function (x, t, b, c, d) {
                return c*(t/=d)*t*t*t*t + b;
        },
        easeOutQuint: function (x, t, b, c, d) {
                return c*((t=t/d-1)*t*t*t*t + 1) + b;
        },
        easeInOutQuint: function (x, t, b, c, d) {
                if ((t/=d/2) < 1) return c/2*t*t*t*t*t + b;
                return c/2*((t-=2)*t*t*t*t + 2) + b;
        },
        easeInSine: function (x, t, b, c, d) {
                return -c * Math.cos(t/d * (Math.PI/2)) + c + b;
        },
        easeOutSine: function (x, t, b, c, d) {
                return c * Math.sin(t/d * (Math.PI/2)) + b;
        },
        easeInOutSine: function (x, t, b, c, d) {
                return -c/2 * (Math.cos(Math.PI*t/d) - 1) + b;
        },
        easeInExpo: function (x, t, b, c, d) {
                return (t==0) ? b : c * Math.pow(2, 10 * (t/d - 1)) + b;
        },
        easeOutExpo: function (x, t, b, c, d) {
                return (t==d) ? b+c : c * (-Math.pow(2, -10 * t/d) + 1) + b;
        },
        easeInOutExpo: function (x, t, b, c, d) {
                if (t==0) return b;
                if (t==d) return b+c;
                if ((t/=d/2) < 1) return c/2 * Math.pow(2, 10 * (t - 1)) + b;
                return c/2 * (-Math.pow(2, -10 * --t) + 2) + b;
        },
        easeInCirc: function (x, t, b, c, d) {
                return -c * (Math.sqrt(1 - (t/=d)*t) - 1) + b;
        },
        easeOutCirc: function (x, t, b, c, d) {
                return c * Math.sqrt(1 - (t=t/d-1)*t) + b;
        },
        easeInOutCirc: function (x, t, b, c, d) {
                if ((t/=d/2) < 1) return -c/2 * (Math.sqrt(1 - t*t) - 1) + b;
                return c/2 * (Math.sqrt(1 - (t-=2)*t) + 1) + b;
        },
        easeInElastic: function (x, t, b, c, d) {
                var s=1.70158;var p=0;var a=c;
                if (t==0) return b;  if ((t/=d)==1) return b+c;  if (!p) p=d*.3;
                if (a < Math.abs(c)) { a=c; var s=p/4; }
                else var s = p/(2*Math.PI) * Math.asin (c/a);
                return -(a*Math.pow(2,10*(t-=1)) * Math.sin( (t*d-s)*(2*Math.PI)/p )) + b;
        },
        easeOutElastic: function (x, t, b, c, d) {
                var s=1.70158;var p=0;var a=c;
                if (t==0) return b;  if ((t/=d)==1) return b+c;  if (!p) p=d*.3;
                if (a < Math.abs(c)) { a=c; var s=p/4; }
                else var s = p/(2*Math.PI) * Math.asin (c/a);
                return a*Math.pow(2,-10*t) * Math.sin( (t*d-s)*(2*Math.PI)/p ) + c + b;
        },
        easeInOutElastic: function (x, t, b, c, d) {
                var s=1.70158;var p=0;var a=c;
                if (t==0) return b;  if ((t/=d/2)==2) return b+c;  if (!p) p=d*(.3*1.5);
                if (a < Math.abs(c)) { a=c; var s=p/4; }
                else var s = p/(2*Math.PI) * Math.asin (c/a);
                if (t < 1) return -.5*(a*Math.pow(2,10*(t-=1)) * Math.sin( (t*d-s)*(2*Math.PI)/p )) + b;
                return a*Math.pow(2,-10*(t-=1)) * Math.sin( (t*d-s)*(2*Math.PI)/p )*.5 + c + b;
        },
        easeInBack: function (x, t, b, c, d, s) {
                if (s == undefined) s = 1.70158;
                return c*(t/=d)*t*((s+1)*t - s) + b;
        },
        easeOutBack: function (x, t, b, c, d, s) {
                if (s == undefined) s = 1.70158;
                return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + b;
        },
        easeInOutBack: function (x, t, b, c, d, s) {
                if (s == undefined) s = 1.70158; 
                if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525))+1)*t - s)) + b;
                return c/2*((t-=2)*t*(((s*=(1.525))+1)*t + s) + 2) + b;
        },
        easeInBounce: function (x, t, b, c, d) {
                return c - jQuery.easing.easeOutBounce (x, d-t, 0, c, d) + b;
        },
        easeOutBounce: function (x, t, b, c, d) {
                if ((t/=d) < (1/2.75)) {
                        return c*(7.5625*t*t) + b;
                } else if (t < (2/2.75)) {
                        return c*(7.5625*(t-=(1.5/2.75))*t + .75) + b;
                } else if (t < (2.5/2.75)) {
                        return c*(7.5625*(t-=(2.25/2.75))*t + .9375) + b;
                } else {
                        return c*(7.5625*(t-=(2.625/2.75))*t + .984375) + b;
                }
        },
        easeInOutBounce: function (x, t, b, c, d) {
                if (t < d/2) return jQuery.easing.easeInBounce (x, t*2, 0, c, d) * .5 + b;
                return jQuery.easing.easeOutBounce (x, t*2-d, 0, c, d) * .5 + c*.5 + b;
        }
});

/*
 *
 * TERMS OF USE - EASING EQUATIONS
 * 
 * Open source under the BSD License. 
 * 
 * Copyright Â© 2001 Robert Penner
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of 
 * conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list 
 * of conditions and the following disclaimer in the documentation and/or other materials 
 * provided with the distribution.
 * 
 * Neither the name of the author nor the names of contributors may be used to endorse 
 * or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 *  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
 *  GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 *
 */
;
var Logger = function() {
   var self = this
   this.loggerStorage = new StorageInterface("logger")
   
   this.cleanupStore()
   
   this.initEvents()
   
   this.paused = false
   
   this.lastMoveEvent = null
   this.flushInterval = setInterval(function(){self.flush()}, 10000)
   
}

Logger.prototype.pause = function(){
  this.paused = true
}

Logger.prototype.unpause = function(){
  this.paused = false
}

Logger.prototype.flush = function(afterCb) {
  if(this.eventCue.length == 0)
    return  

  var self = this
  
  var fj = new FlushJob(self.eventCue.slice(), self.loggerStorage)
  self.eventCue = []
  self.loggerStorage.setObject("events", [])
  fj.flush(afterCb)
}

Logger.prototype.log = function(name, properties, skipAutoFlush) {
  if(this.paused) return
  var self = this
  var evt = {"id":Logger.generateId(), "name":name, "properties": properties, "time": (new Date().getTime())/1000 }
  
  if(this.isEventSignificant(evt)) {
    this.eventCue.push(evt)
    this.addEvtToLocalStorage(evt)
    if(window.quest && !skipAutoFlush) {
      quest.checkEvent(evt)
    }
  }
}

Logger.generateId = function() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8)
    return v.toString(16)
  });
}

Logger.integerCoordsDiffer = function(loc1, loc2) {
  return (Math.floor(loc1["x"]) != Math.floor(loc2["x"]) ||
          Math.floor(loc1["y"]) != Math.floor(loc2["y"]) ||
          Math.floor(loc1["z"]) != Math.floor(loc2["z"]))
}

Logger.prototype.isEventSignificant = function(evt){
  if(evt["name"] == "VoxLMove") {
    if(this.lastMoveEvent == null) {
      this.lastMoveEvent = evt
      return true
    }
    var newLoc=evt["properties"]["location"]
    var lastLoc = this.lastMoveEvent["properties"]["location"]
    if(Logger.integerCoordsDiffer(newLoc, lastLoc)) {
      this.lastMoveEvent = evt
      return true
    } else {
      return false
    }
  }
  return true
}

Logger.prototype.initEvents = function(){
  var self = this
  var events = this.loggerStorage.getObject("events")
  var oldJobs = this.loggerStorage.getObject("jobs")
  
  if(oldJobs){
    oldJobs.forEach(function(job){
        var fj = new FlushJob(self.loggerStorage.getObject(job), self.loggerStorage, job);
        fj.flush();
    })
  }
  
  if(!events)  {
    this.loggerStorage.setObject("events", [])
    this.eventCue = []
  } else {
    this.eventCue = events
    
    setTimeout(function(){
      self.flush()
      if(window.quest){
        for(var i in events) {
          quest.checkEvent(events[i])
        } 
      } 
      },500)
  }
}

Logger.prototype.addEvtToLocalStorage = function(evt) {
  var eventCache = this.loggerStorage.getObject("events")
  if(!eventCache) {
    eventCache = []
  }
  eventCache.push(evt)
  this.loggerStorage.setObject("events", eventCache)
}

//so storage can purge any orphaned jobs
Logger.prototype.cleanupStore = function() {
  var logStore = JSON.parse(localStorage.getItem("logger"));
  var jobs = logStore.jobs;
  for(var prop in logStore) {
    if(prop == "events" || prop == "jobs")
      continue;
    if(jobs.indexOf(prop) == -1) {
        this.loggerStorage.removeObject(prop);
    }
  }
}



;
var VideoPlayer = (function () {
    function VideoPlayer(v) {
        this.video = v;
    }
    VideoPlayer.prototype.playPause = function () {
        if (this.video.paused)
            this.video.play();
        else
            this.video.pause();
    };
    VideoPlayer.prototype.play = function () {
        this.video.play();
    };
    VideoPlayer.prototype.pause = function () {
        this.video.pause();
    };
    VideoPlayer.prototype.isPlaying = function () {
        return !(this.video.paused);
    };
    VideoPlayer.prototype.changeSize = function (delta) {
        this.video.width += delta;
    };
    VideoPlayer.prototype.getCurrentTime = function () {
        return this.video.currentTime;
    };
    VideoPlayer.prototype.getDuration = function () {
        return this.video.duration;
    };
    VideoPlayer.prototype.setCurrentTime = function (t) {
        return this.video.currentTime = t;
    };
    VideoPlayer.prototype.getDonePercent = function () {
        return Math.ceil(100 * (this.video.currentTime / this.video.duration));
    };
    return VideoPlayer;
})();
/// <reference path="typings/jquery.d.ts"/>
var Util;
(function (Util) {
    function uniqueIn(arr) {
        var ret = [];
        arr.map(function (e) {
            if (ret.indexOf(e) == -1)
                ret.push(e);
        });
        return ret;
    }
    Util.uniqueIn = uniqueIn;
    function compact(arr) {
        var ret = [];
        arr.map(function (e) {
            if (typeof e != "undefined")
                ret.push(e);
        });
        return ret;
    }
    Util.compact = compact;
    function daydiff(first, second) {
        return Math.round((second - first) / (1000 * 60 * 60 * 24));
    }
    Util.daydiff = daydiff;
    // This is done in a linear time O(n) without recursion
    // memory complexity is O(1) or O(n) if mutable param is set to false
    function flatten(array, mutable) {
        var toString = Object.prototype.toString;
        var arrayTypeStr = '[object Array]';
        var result = [];
        var nodes = (mutable && array) || array.slice();
        var node;
        if (!array.length) {
            return result;
        }
        node = nodes.pop();
        do {
            if (toString.call(node) === arrayTypeStr) {
                nodes.push.apply(nodes, node);
            }
            else {
                result.push(node);
            }
        } while (nodes.length && (node = nodes.pop()) !== undefined);
        result.reverse(); // we reverse result to restore the original order
        return result;
    }
    Util.flatten = flatten;
    function degRotationFrom(elemId) {
        var el = document.getElementById(elemId);
        var st = window.getComputedStyle(el, null);
        var tr = st.getPropertyValue("-webkit-transform") ||
            st.getPropertyValue("-moz-transform") ||
            st.getPropertyValue("-ms-transform") ||
            st.getPropertyValue("-o-transform") ||
            st.getPropertyValue("transform") ||
            "fail...";
        if (tr == "none") {
            return 0;
        }
        // With rotate(30deg)...
        // matrix(0.866025, 0.5, -0.5, 0.866025, 0px, 0px)
        // rotation matrix - http://en.wikipedia.org/wiki/Rotation_matrix
        var temp = tr.split('(')[1];
        temp = temp.split(')')[0];
        var values = temp.split(',');
        var a = parseFloat(values[0]);
        var b = parseFloat(values[1]);
        var c = parseFloat(values[2]);
        var d = parseFloat(values[3]);
        var scale = Math.sqrt(a * a + b * b);
        var sin = b / scale;
        var angle = Math.round(Math.asin(sin) * (180 / Math.PI));
        return angle;
    }
    Util.degRotationFrom = degRotationFrom;
    function animateTransformRotation(imgId, args) {
        // caching the object for performance reasons
        var $elem = $("#" + imgId);
        // we use a pseudo object for the animation
        // (starts from `0` to `angle`), you can name it as you want
        $({ deg: Util.degRotationFrom(imgId) }).animate({ deg: args.angle }, {
            duration: args.duration,
            step: function (now) {
                // in the step-callback (that is fired each step of the animation),
                // you can use the `now` paramter which contains the current
                // animation-position (`0` up to `angle`)
                $elem.css({
                    transform: 'rotate(' + now + 'deg)'
                });
            }
        });
    }
    Util.animateTransformRotation = animateTransformRotation;
    function toTitleCase(str) {
        return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    }
    Util.toTitleCase = toTitleCase;
})(Util || (Util = {}));
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/logger.d.ts"/>
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/jquery.d.ts"/>
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/colorbox.d.ts"/>
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/video_player.js.ts"/>
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/util.js.ts"/>
var LtmTutorial = (function () {
    function LtmTutorial(data) {
        var self = this;
        this.hidden = true;
        this.lastTime = -1;
        this.completed = data.completed;
        //I have the content as an optional param to make it easier to test, and in case we want more than one tutorial per page at some point
        try {
            this.htmlContentId = data.contentId || "#tutorial_video_content";
        }
        catch (dataNotSet) {
            this.htmlContentId = "#tutorial_video_content";
        }
        this.logger = data.logger || window.logger;
        this.intervalTrack = [];
        this.doneIntervals = [];
        this.percentDone = 0;
        this.size = data.size || { x: 760, y: 590 };
        this.lastLoggedSecond = null;
        this.data = data;
        this.avatar_hidden = true;
        if (!this.completed)
            this.notDoneRoutine();
    }
    LtmTutorial.prototype.flagComplete = function () {
        var self = this;
        $.post('/tutorial_completions', {
            username: self.data.username,
            tutorial_name: self.data.tutorial_name
        }, function (d) {
            self.completed = true;
        });
    };
    LtmTutorial.prototype.showHide = function () {
        if (this.hidden) {
            this.show();
        }
        else {
            this.hide();
        }
        this.hidden = !this.hidden;
    };
    LtmTutorial.prototype.show = function () {
        var self = this;
        var innerHtml = $(this.htmlContentId).html();
        $.colorbox({
            html: innerHtml,
            width: self.size.x,
            height: self.size.y,
            onClosed: function () {
                self.hidden = true;
                clearTimeout(self.playTimeout);
                $(self.htmlContentId).html(innerHtml);
                self.lastTime = self.player.getCurrentTime();
                console.log("closing with this stuff: ", [self.getVideoDonePercent(), self.completed]);
                if (self.getVideoDonePercent() > 50 && !self.completed)
                    self.flagComplete();
                //restore the content
            },
            onComplete: function () {
                //this is so we dont end up with colliding video elements; we only have one in the DOM at a time...
                $(self.htmlContentId).html("");
                var vidElem = $("#cboxLoadedContent").find("video")[0];
                if (vidElem) {
                    self.player = new VideoPlayer(vidElem);
                    vidElem.addEventListener("timeupdate", function () {
                        self.checkSecond();
                    });
                    if (self.lastTime) {
                        self.player.setCurrentTime(self.lastTime);
                    }
                    self.playTimeout = setTimeout(function () {
                        self.player.play();
                    }, 2500);
                }
            }
        }); //colorbox call end
    };
    LtmTutorial.prototype.checkSecond = function () {
        if (this.lastLoggedSecond == Math.floor(this.player.getCurrentTime())) {
            console.log("last second was the same: ", [this.lastLoggedSecond, this.player.getCurrentTime()]);
            return;
        }
        if (!this.logger) {
            console.log("hey");
            return;
        }
        this.lastLoggedSecond = Math.floor(this.player.getCurrentTime());
        var secondsPerInterval = 5;
        if (this.intervalTrack.length == 0) {
            //populate seconds watched in this way: 
            //[[true,true,true,true,false], [...], [...]]
            //   ^second                    ^interval
            var effectiveDuration = Math.floor(this.player.getDuration());
            var intervals = Math.floor(effectiveDuration / secondsPerInterval);
            var oddRemaining = Math.floor(this.player.getDuration() % secondsPerInterval);
            for (var i = 0; i < intervals; i++) {
                var secondsWatchedInInterval = [];
                for (var k = 0; k < secondsPerInterval; k++) {
                    secondsWatchedInInterval.push(false);
                }
                this.intervalTrack.push(secondsWatchedInInterval);
            }
            var lastInterval = [];
            for (var i = 0; i < oddRemaining; i++) {
                lastInterval.push(false);
            }
            if (lastInterval.length != 0) {
                this.intervalTrack.push(lastInterval);
            }
        }
        var second = this.player.getCurrentTime();
        //decide what interval the second is in...
        var interval = Math.floor(second / secondsPerInterval);
        if (interval > this.intervalTrack.length - 1)
            return;
        var intervalSec = Math.floor(second % secondsPerInterval);
        console.log("checking second in ", this.intervalTrack[interval]);
        this.intervalTrack[interval][intervalSec] = true;
        if (this.intervalTrack[interval].indexOf(false) == -1) {
            if (this.doneIntervals.indexOf(interval) == -1) {
                this.doneIntervals.push(interval);
            }
            this.intervalTrack[interval] = this.intervalTrack[interval].map(function () { return false; });
            this.logger.log("VideoWatched", { username: this.data.username, video_name: this.data.video_name, interval: interval, totalIntervals: this.intervalTrack.length, secondsPerInterval: secondsPerInterval });
        }
    };
    LtmTutorial.prototype.getVideoDonePercent = function () {
        return Math.ceil(100 * (this.doneIntervals.length / this.intervalTrack.length));
    };
    LtmTutorial.prototype.hide = function () {
    };
    LtmTutorial.prototype.toggleInstructor = function () {
        if (this.avatar_hidden) {
            $.colorbox.close();
            $("#callout_avatar").animate({ right: "-100px" }, 700);
            $(".tutorial-tab").hide(700);
            Util.animateTransformRotation("callout_avatar", { angle: -55, duration: 700 });
        }
        else {
            $("#callout_avatar").animate({ right: "-350px" }, 700);
            $(".tutorial-tab").show(700);
            Util.animateTransformRotation("callout_avatar", { angle: 0, duration: 700 });
            $(".bubble").hide(200);
        }
        this.avatar_hidden = !this.avatar_hidden;
    };
    LtmTutorial.prototype.instructorSpeech = function (text) {
        var h = parseInt($(".bubble").css("height"));
        var topDist = 175 - h;
        console.log("top", topDist);
        if ($(".bubble").css("display") == "none") {
            $(".bubble").html(text).fadeIn(500);
            $(".bubble").css({ position: "fixed", top: topDist + "px", right: "120px" });
        }
        else {
            $(".bubble").fadeOut(250, function () {
                $(".bubble").html(text);
                $(".bubble").css({ position: "fixed", top: topDist + "px", right: "120px" });
            }).fadeIn(250);
        }
    };
    LtmTutorial.prototype.instructionsStory = function (storyList) {
        var self = this;
        //storyList should have a list of things to do, and a duration for how long each thing should take
        var story = storyList.shift();
        if (story) {
            story.func();
            setTimeout(function () {
                self.instructionsStory(storyList);
            }, story.duration);
        }
    };
    LtmTutorial.prototype.notDoneRoutine = function () {
        var self = this;
        var choiceHtml = "<h3>Want to watch the tutorial?</h3>";
        choiceHtml += "<ul style='text-align:left;margin-left:40px;list-style:none;'>" +
            "<li>" +
            "<a style='cursor:pointer;' onclick='global_tutorial.show();global_tutorial.toggleInstructor();' >" +
            "\"Okay!\"" +
            "</a>" +
            "</li>" +
            "<li>" +
            "<a style='cursor:pointer;' onclick='global_tutorial.toggleInstructor();' >" +
            "\"Not right now.\"" +
            "</a>" +
            "</li>" +
            "<li>" +
            "<a style='cursor:pointer;' onclick='global_tutorial.toggleInstructor();global_tutorial.flagComplete();' >" +
            "\"No. Don't ask again!\"" +
            "</a>" +
            "</li>";
        "</ul>" +
            this.instructionsStory([
                { func: function () { }, duration: 2000 },
                { func: function () { self.toggleInstructor(); }, duration: 1500 },
                { func: function () { self.instructorSpeech("<h3>Hey there!  We have a tutorial for this part of the site.</h3>"); }, duration: 4000 },
                { func: function () { self.instructorSpeech(choiceHtml); }, duration: 2500 }]);
    };
    return LtmTutorial;
})();
var ObjectSifter = function(objList) {
    this.objList = objList.slice()
}

ObjectSifter.prototype.countOfMatching = function(key, val) {
    var count = 0

    for(var i in this.objList) {
      d = this.objList[i]
      for(var k in d) {
        if(k == key && d[k] == val) {
          count++
          break
        }
      }
    }
    return count
}
    
ObjectSifter.prototype.getDistinctValuesWithKey = function(key) {
  var agg = []
  for(var i in this.objList) {
    d = this.objList[i]
    if(!d) 
      continue
    if(agg.indexOf(d[key]) < 0 && d[key]) {
      agg.push(d[key])
    }
  }
  return agg
}

//returns a list of objects who have a given key, but limits based on the value to make sure there are no duplicate
//key -> vals in the set. 
ObjectSifter.prototype.getDistinctOfKey = function(key) {
  
  var agg = []
  var s = new ObjectSifter(agg);
  this.objList.forEach(function(obj){
    d = obj
    if(s.getMatching(key, d[key]).length == 0) {
      s.objList.push(d)
    }
  })

  return agg
}

//returns a list of objects who both have $1 as a key, and whose value equals $2
ObjectSifter.prototype.getMatching = function(key, val) {
  var newList = []
  this.objList.forEach(function(obj){
    if(!obj)
      return
    if(obj[key] && obj[key] == val) {
      newList.push(obj)
    }
  })
  return newList
}

ObjectSifter.prototype.filterMatching = function(key, val) {
  var ret = []
  for(var i = (this.objList.length - 1); i >= 0; i--) {
      var obj = this.objList[i]      
      if(!obj)
          return
      if(obj[key] && obj[key] == val) {
        ret.push(obj)
        this.objList.splice(i, 1)
      }
  }
  return ret
}


ObjectSifter.prototype.map = function(func) {
    var res = []
    for(var i = 0; i < this.objList.length; i++) {
        res.push(func(this.objList[i]))
    }
    return res
}

;
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/ltm.d.ts" />
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/jquery.d.ts" />
var PaginationLoader = function (args) {
    this.currentPage = (args && args.currentPage) || 2;
    this.containerId = (args && args.containerId); //string
    this.fetchUrl = (args && args.fetchUrl); //string
    this.cachedPages = {}; // <number, string>
};
PaginationLoader.prototype.fetch = function (args, cbFunc) {
    var self = this;
    $.ajax({
        method: "GET",
        url: self.fetchUrl,
        data: self.prepParams(args),
        success: function (d) { self.receiveDataPage(d, cbFunc); },
        error: function (e) {
            console.log("Error fetching pagination data (page " + this.currentPage + ") at " + this.fetchUrl + ": " + e);
        }
    });
};
PaginationLoader.prototype.reset = function () {
    this.currentPage = 2;
};
PaginationLoader.prototype.receiveDataPage = function (d, cbFunc) {
    this.cachedPages[this.currentPage] = d;
    this.currentPage++;
    if (cbFunc)
        cbFunc(d);
};
PaginationLoader.prototype.appendLatestPage = function () {
    var lastPage = this.currentPage - 1;
    if (lastPage < 2) {
        return false;
    }
    if (this.containerId) {
        $("#" + this.containerId).append(this.cachedPages[lastPage]);
        resetMetro();
        return true;
    }
};
PaginationLoader.prototype.appendPage = function (p) {
    var d = this.cachedPages[p];
    if (typeof d != "undefined" && this.containerId) {
        $("#" + this.containerId).append(d);
        resetMetro();
    }
};
PaginationLoader.prototype.prepParams = function (args) {
    var ret = {};
    for (var arg in args) {
        ret[arg] = args[arg];
    }
    ret.page = this.currentPage;
    return ret;
};

      function awardPoints(message, reason){
                                  
        $.ajax({type: "POST", url: "/bundles", data: {description: reason}, success: function(data){
          if(data.success)
          {
             quickAlert({selector:'.left-flag', 
                         html:"<h2>+10 Points!</h2><p>"+message+"</p><img width='100%' style='max-height:500px' src='/assets/brain.png'/>", 
                         duration:5000, 
                         soundfx:{soundId: 'points_earned', volume: .7}
                         })
                                  
          }
        }})
      }

      function videoWatched(message){
        awardPoints("Your brain just formed new synaptic connections by watching a video!", "Watched Video "+window.location)
      }

       var player;
        function onYouTubePlayerAPIReady() {
            player = new YT.Player('tutorial_video', {
              events: {
                'onStateChange': onPlayerStateChange
              }
            });
        }

       function onPlayerStateChange(event) {        
            if(event.data === 0) {          
              videoWatched()
            }
        }
      
      
;


  
$(document).ready(function() {
  

  
})
;
var alertCue = []
var alerting = false
var currentSelecter = ''

//thisAlert should have a selector, html, duration, and 
function quickAlert(thisAlert) {
  thisAlert.selector = '.quick-alert' + thisAlert.selector
  thisAlert.html += "<span style='font-size:12px;'>(click anywhere to close)</span>"
  if(alerting || alertCue.length > 0) {
    alertCue.push(thisAlert)
    //console.log('pushing an alert on cue...')
  } else {
    //console.log('alerting...')
    alerting = true
    if(thisAlert.soundfx) playSoundFX(thisAlert.soundfx.soundId, thisAlert.soundfx.volume)
    currentSelecter = thisAlert.selector;
    $(thisAlert.selector + " div").html(thisAlert.html)
    $(thisAlert.selector).show("700")
    setTimeout(cleanupQuickAlert, thisAlert.duration)
  }
}

function cleanupQuickAlert() {
  $(currentSelecter).hide('700')
  setTimeout(function() {
     if(alertCue.length > 0) {
        //console.log('alerting...')
        var nextAlert = alertCue.shift()
        if(nextAlert.soundfx) playSoundFX(nextAlert.soundfx.soundId, nextAlert.soundfx.volume)
        $(nextAlert.selector + " div").html(nextAlert.html)
        $(nextAlert.selector).show("700")
        currentSelecter = nextAlert.selector
        setTimeout(cleanupQuickAlert, nextAlert.duration)
     } else {
        //console.log('cleaned up all alerts...')
        alerting = false
     }
  }, 700)
}

$('body').click(function(){
  if(alerting) cleanupQuickAlert()
})
;
(function(factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }

}(function($) {
    'use strict';
    var Slick = window.Slick || {};

    Slick = (function() {

        var instanceUid = 0;

        function Slick(element, settings) {

            var _ = this, dataSettings;

            _.defaults = {
                accessibility: true,
                adaptiveHeight: false,
                appendArrows: $(element),
                appendDots: $(element),
                arrows: true,
                asNavFor: null,
                prevArrow: '<button type="button" data-role="none" class="slick-prev" aria-label="Previous" tabindex="0" role="button">Previous</button>',
                nextArrow: '<button type="button" data-role="none" class="slick-next" aria-label="Next" tabindex="0" role="button">Next</button>',
                autoplay: false,
                autoplaySpeed: 3000,
                centerMode: false,
                centerPadding: '50px',
                cssEase: 'ease',
                customPaging: function(slider, i) {
                    return '<button type="button" data-role="none" role="button" aria-required="false" tabindex="0">' + (i + 1) + '</button>';
                },
                dots: false,
                dotsClass: 'slick-dots',
                draggable: true,
                easing: 'linear',
                edgeFriction: 0.35,
                fade: false,
                focusOnSelect: false,
                infinite: true,
                initialSlide: 0,
                lazyLoad: 'ondemand',
                mobileFirst: false,
                pauseOnHover: true,
                pauseOnDotsHover: false,
                respondTo: 'window',
                responsive: null,
                rows: 1,
                rtl: false,
                slide: '',
                slidesPerRow: 1,
                slidesToShow: 1,
                slidesToScroll: 1,
                speed: 500,
                swipe: true,
                swipeToSlide: false,
                touchMove: true,
                touchThreshold: 5,
                useCSS: true,
                useTransform: false,
                variableWidth: false,
                vertical: false,
                verticalSwiping: false,
                waitForAnimate: true,
                zIndex: 1000
            };

            _.initials = {
                animating: false,
                dragging: false,
                autoPlayTimer: null,
                currentDirection: 0,
                currentLeft: null,
                currentSlide: 0,
                direction: 1,
                $dots: null,
                listWidth: null,
                listHeight: null,
                loadIndex: 0,
                $nextArrow: null,
                $prevArrow: null,
                slideCount: null,
                slideWidth: null,
                $slideTrack: null,
                $slides: null,
                sliding: false,
                slideOffset: 0,
                swipeLeft: null,
                $list: null,
                touchObject: {},
                transformsEnabled: false,
                unslicked: false
            };

            $.extend(_, _.initials);

            _.activeBreakpoint = null;
            _.animType = null;
            _.animProp = null;
            _.breakpoints = [];
            _.breakpointSettings = [];
            _.cssTransitions = false;
            _.hidden = 'hidden';
            _.paused = false;
            _.positionProp = null;
            _.respondTo = null;
            _.rowCount = 1;
            _.shouldClick = true;
            _.$slider = $(element);
            _.$slidesCache = null;
            _.transformType = null;
            _.transitionType = null;
            _.visibilityChange = 'visibilitychange';
            _.windowWidth = 0;
            _.windowTimer = null;

            dataSettings = $(element).data('slick') || {};

            _.options = $.extend({}, _.defaults, dataSettings, settings);

            _.currentSlide = _.options.initialSlide;

            _.originalSettings = _.options;

            if (typeof document.mozHidden !== 'undefined') {
                _.hidden = 'mozHidden';
                _.visibilityChange = 'mozvisibilitychange';
            } else if (typeof document.webkitHidden !== 'undefined') {
                _.hidden = 'webkitHidden';
                _.visibilityChange = 'webkitvisibilitychange';
            }

            _.autoPlay = $.proxy(_.autoPlay, _);
            _.autoPlayClear = $.proxy(_.autoPlayClear, _);
            _.changeSlide = $.proxy(_.changeSlide, _);
            _.clickHandler = $.proxy(_.clickHandler, _);
            _.selectHandler = $.proxy(_.selectHandler, _);
            _.setPosition = $.proxy(_.setPosition, _);
            _.swipeHandler = $.proxy(_.swipeHandler, _);
            _.dragHandler = $.proxy(_.dragHandler, _);
            _.keyHandler = $.proxy(_.keyHandler, _);
            _.autoPlayIterator = $.proxy(_.autoPlayIterator, _);

            _.instanceUid = instanceUid++;

            // A simple way to check for HTML strings
            // Strict HTML recognition (must start with <)
            // Extracted from jQuery v1.11 source
            _.htmlExpr = /^(?:\s*(<[\w\W]+>)[^>]*)$/;


            _.registerBreakpoints();
            _.init(true);
            _.checkResponsive(true);

        }

        return Slick;

    }());

    Slick.prototype.addSlide = Slick.prototype.slickAdd = function(markup, index, addBefore) {

        var _ = this;

        if (typeof(index) === 'boolean') {
            addBefore = index;
            index = null;
        } else if (index < 0 || (index >= _.slideCount)) {
            return false;
        }

        _.unload();

        if (typeof(index) === 'number') {
            if (index === 0 && _.$slides.length === 0) {
                $(markup).appendTo(_.$slideTrack);
            } else if (addBefore) {
                $(markup).insertBefore(_.$slides.eq(index));
            } else {
                $(markup).insertAfter(_.$slides.eq(index));
            }
        } else {
            if (addBefore === true) {
                $(markup).prependTo(_.$slideTrack);
            } else {
                $(markup).appendTo(_.$slideTrack);
            }
        }

        _.$slides = _.$slideTrack.children(this.options.slide);

        _.$slideTrack.children(this.options.slide).detach();

        _.$slideTrack.append(_.$slides);

        _.$slides.each(function(index, element) {
            $(element).attr('data-slick-index', index);
        });

        _.$slidesCache = _.$slides;

        _.reinit();

    };

    Slick.prototype.animateHeight = function() {
        var _ = this;
        if (_.options.slidesToShow === 1 && _.options.adaptiveHeight === true && _.options.vertical === false) {
            var targetHeight = _.$slides.eq(_.currentSlide).outerHeight(true);
            _.$list.animate({
                height: targetHeight
            }, _.options.speed);
        }
    };

    Slick.prototype.animateSlide = function(targetLeft, callback) {

        var animProps = {},
            _ = this;

        _.animateHeight();

        if (_.options.rtl === true && _.options.vertical === false) {
            targetLeft = -targetLeft;
        }
        if (_.transformsEnabled === false) {
            if (_.options.vertical === false) {
                _.$slideTrack.animate({
                    left: targetLeft
                }, _.options.speed, _.options.easing, callback);
            } else {
                _.$slideTrack.animate({
                    top: targetLeft
                }, _.options.speed, _.options.easing, callback);
            }

        } else {

            if (_.cssTransitions === false) {
                if (_.options.rtl === true) {
                    _.currentLeft = -(_.currentLeft);
                }
                $({
                    animStart: _.currentLeft
                }).animate({
                    animStart: targetLeft
                }, {
                    duration: _.options.speed,
                    easing: _.options.easing,
                    step: function(now) {
                        now = Math.ceil(now);
                        if (_.options.vertical === false) {
                            animProps[_.animType] = 'translate(' +
                                now + 'px, 0px)';
                            _.$slideTrack.css(animProps);
                        } else {
                            animProps[_.animType] = 'translate(0px,' +
                                now + 'px)';
                            _.$slideTrack.css(animProps);
                        }
                    },
                    complete: function() {
                        if (callback) {
                            callback.call();
                        }
                    }
                });

            } else {

                _.applyTransition();
                targetLeft = Math.ceil(targetLeft);

                if (_.options.vertical === false) {
                    animProps[_.animType] = 'translate3d(' + targetLeft + 'px, 0px, 0px)';
                } else {
                    animProps[_.animType] = 'translate3d(0px,' + targetLeft + 'px, 0px)';
                }
                _.$slideTrack.css(animProps);

                if (callback) {
                    setTimeout(function() {

                        _.disableTransition();

                        callback.call();
                    }, _.options.speed);
                }

            }

        }

    };

    Slick.prototype.asNavFor = function(index) {

        var _ = this,
            asNavFor = _.options.asNavFor;

        if ( asNavFor && asNavFor !== null ) {
            asNavFor = $(asNavFor).not(_.$slider);
        }

        if ( asNavFor !== null && typeof asNavFor === 'object' ) {
            asNavFor.each(function() {
                var target = $(this).slick('getSlick');
                if(!target.unslicked) {
                    target.slideHandler(index, true);
                }
            });
        }

    };

    Slick.prototype.applyTransition = function(slide) {

        var _ = this,
            transition = {};

        if (_.options.fade === false) {
            transition[_.transitionType] = _.transformType + ' ' + _.options.speed + 'ms ' + _.options.cssEase;
        } else {
            transition[_.transitionType] = 'opacity ' + _.options.speed + 'ms ' + _.options.cssEase;
        }

        if (_.options.fade === false) {
            _.$slideTrack.css(transition);
        } else {
            _.$slides.eq(slide).css(transition);
        }

    };

    Slick.prototype.autoPlay = function() {

        var _ = this;

        if (_.autoPlayTimer) {
            clearInterval(_.autoPlayTimer);
        }

        if (_.slideCount > _.options.slidesToShow && _.paused !== true) {
            _.autoPlayTimer = setInterval(_.autoPlayIterator,
                _.options.autoplaySpeed);
        }

    };

    Slick.prototype.autoPlayClear = function() {

        var _ = this;
        if (_.autoPlayTimer) {
            clearInterval(_.autoPlayTimer);
        }

    };

    Slick.prototype.autoPlayIterator = function() {

        var _ = this;

        if (_.options.infinite === false) {

            if (_.direction === 1) {

                if ((_.currentSlide + 1) === _.slideCount -
                    1) {
                    _.direction = 0;
                }

                _.slideHandler(_.currentSlide + _.options.slidesToScroll);

            } else {

                if ((_.currentSlide - 1 === 0)) {

                    _.direction = 1;

                }

                _.slideHandler(_.currentSlide - _.options.slidesToScroll);

            }

        } else {

            _.slideHandler(_.currentSlide + _.options.slidesToScroll);

        }

    };

    Slick.prototype.buildArrows = function() {

        var _ = this;

        if (_.options.arrows === true ) {

            _.$prevArrow = $(_.options.prevArrow).addClass('slick-arrow');
            _.$nextArrow = $(_.options.nextArrow).addClass('slick-arrow');

            if( _.slideCount > _.options.slidesToShow ) {

                _.$prevArrow.removeClass('slick-hidden').removeAttr('aria-hidden tabindex');
                _.$nextArrow.removeClass('slick-hidden').removeAttr('aria-hidden tabindex');

                if (_.htmlExpr.test(_.options.prevArrow)) {
                    _.$prevArrow.prependTo(_.options.appendArrows);
                }

                if (_.htmlExpr.test(_.options.nextArrow)) {
                    _.$nextArrow.appendTo(_.options.appendArrows);
                }

                if (_.options.infinite !== true) {
                    _.$prevArrow
                        .addClass('slick-disabled')
                        .attr('aria-disabled', 'true');
                }

            } else {

                _.$prevArrow.add( _.$nextArrow )

                    .addClass('slick-hidden')
                    .attr({
                        'aria-disabled': 'true',
                        'tabindex': '-1'
                    });

            }

        }

    };

    Slick.prototype.buildDots = function() {

        var _ = this,
            i, dotString;

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            dotString = '<ul class="' + _.options.dotsClass + '">';

            for (i = 0; i <= _.getDotCount(); i += 1) {
                dotString += '<li>' + _.options.customPaging.call(this, _, i) + '</li>';
            }

            dotString += '</ul>';

            _.$dots = $(dotString).appendTo(
                _.options.appendDots);

            _.$dots.find('li').first().addClass('slick-active').attr('aria-hidden', 'false');

        }

    };

    Slick.prototype.buildOut = function() {

        var _ = this;

        _.$slides =
            _.$slider
                .children( _.options.slide + ':not(.slick-cloned)')
                .addClass('slick-slide');

        _.slideCount = _.$slides.length;

        _.$slides.each(function(index, element) {
            $(element)
                .attr('data-slick-index', index)
                .data('originalStyling', $(element).attr('style') || '');
        });

        _.$slider.addClass('slick-slider');

        _.$slideTrack = (_.slideCount === 0) ?
            $('<div class="slick-track"/>').appendTo(_.$slider) :
            _.$slides.wrapAll('<div class="slick-track"/>').parent();

        _.$list = _.$slideTrack.wrap(
            '<div aria-live="polite" class="slick-list"/>').parent();
        _.$slideTrack.css('opacity', 0);

        if (_.options.centerMode === true || _.options.swipeToSlide === true) {
            _.options.slidesToScroll = 1;
        }

        $('img[data-lazy]', _.$slider).not('[src]').addClass('slick-loading');

        _.setupInfinite();

        _.buildArrows();

        _.buildDots();

        _.updateDots();


        _.setSlideClasses(typeof _.currentSlide === 'number' ? _.currentSlide : 0);

        if (_.options.draggable === true) {
            _.$list.addClass('draggable');
        }

    };

    Slick.prototype.buildRows = function() {

        var _ = this, a, b, c, newSlides, numOfSlides, originalSlides,slidesPerSection;

        newSlides = document.createDocumentFragment();
        originalSlides = _.$slider.children();

        if(_.options.rows > 1) {

            slidesPerSection = _.options.slidesPerRow * _.options.rows;
            numOfSlides = Math.ceil(
                originalSlides.length / slidesPerSection
            );

            for(a = 0; a < numOfSlides; a++){
                var slide = document.createElement('div');
                for(b = 0; b < _.options.rows; b++) {
                    var row = document.createElement('div');
                    for(c = 0; c < _.options.slidesPerRow; c++) {
                        var target = (a * slidesPerSection + ((b * _.options.slidesPerRow) + c));
                        if (originalSlides.get(target)) {
                            row.appendChild(originalSlides.get(target));
                        }
                    }
                    slide.appendChild(row);
                }
                newSlides.appendChild(slide);
            }

            _.$slider.html(newSlides);
            _.$slider.children().children().children()
                .css({
                    'width':(100 / _.options.slidesPerRow) + '%',
                    'display': 'inline-block'
                });

        }

    };

    Slick.prototype.checkResponsive = function(initial, forceUpdate) {

        var _ = this,
            breakpoint, targetBreakpoint, respondToWidth, triggerBreakpoint = false;
        var sliderWidth = _.$slider.width();
        var windowWidth = window.innerWidth || $(window).width();

        if (_.respondTo === 'window') {
            respondToWidth = windowWidth;
        } else if (_.respondTo === 'slider') {
            respondToWidth = sliderWidth;
        } else if (_.respondTo === 'min') {
            respondToWidth = Math.min(windowWidth, sliderWidth);
        }

        if ( _.options.responsive &&
            _.options.responsive.length &&
            _.options.responsive !== null) {

            targetBreakpoint = null;

            for (breakpoint in _.breakpoints) {
                if (_.breakpoints.hasOwnProperty(breakpoint)) {
                    if (_.originalSettings.mobileFirst === false) {
                        if (respondToWidth < _.breakpoints[breakpoint]) {
                            targetBreakpoint = _.breakpoints[breakpoint];
                        }
                    } else {
                        if (respondToWidth > _.breakpoints[breakpoint]) {
                            targetBreakpoint = _.breakpoints[breakpoint];
                        }
                    }
                }
            }

            if (targetBreakpoint !== null) {
                if (_.activeBreakpoint !== null) {
                    if (targetBreakpoint !== _.activeBreakpoint || forceUpdate) {
                        _.activeBreakpoint =
                            targetBreakpoint;
                        if (_.breakpointSettings[targetBreakpoint] === 'unslick') {
                            _.unslick(targetBreakpoint);
                        } else {
                            _.options = $.extend({}, _.originalSettings,
                                _.breakpointSettings[
                                    targetBreakpoint]);
                            if (initial === true) {
                                _.currentSlide = _.options.initialSlide;
                            }
                            _.refresh(initial);
                        }
                        triggerBreakpoint = targetBreakpoint;
                    }
                } else {
                    _.activeBreakpoint = targetBreakpoint;
                    if (_.breakpointSettings[targetBreakpoint] === 'unslick') {
                        _.unslick(targetBreakpoint);
                    } else {
                        _.options = $.extend({}, _.originalSettings,
                            _.breakpointSettings[
                                targetBreakpoint]);
                        if (initial === true) {
                            _.currentSlide = _.options.initialSlide;
                        }
                        _.refresh(initial);
                    }
                    triggerBreakpoint = targetBreakpoint;
                }
            } else {
                if (_.activeBreakpoint !== null) {
                    _.activeBreakpoint = null;
                    _.options = _.originalSettings;
                    if (initial === true) {
                        _.currentSlide = _.options.initialSlide;
                    }
                    _.refresh(initial);
                    triggerBreakpoint = targetBreakpoint;
                }
            }

            // only trigger breakpoints during an actual break. not on initialize.
            if( !initial && triggerBreakpoint !== false ) {
                _.$slider.trigger('breakpoint', [_, triggerBreakpoint]);
            }
        }

    };

    Slick.prototype.changeSlide = function(event, dontAnimate) {

        var _ = this,
            $target = $(event.target),
            indexOffset, slideOffset, unevenOffset;

        // If target is a link, prevent default action.
        if($target.is('a')) {
            event.preventDefault();
        }

        // If target is not the <li> element (ie: a child), find the <li>.
        if(!$target.is('li')) {
            $target = $target.closest('li');
        }

        unevenOffset = (_.slideCount % _.options.slidesToScroll !== 0);
        indexOffset = unevenOffset ? 0 : (_.slideCount - _.currentSlide) % _.options.slidesToScroll;

        switch (event.data.message) {

            case 'previous':
                slideOffset = indexOffset === 0 ? _.options.slidesToScroll : _.options.slidesToShow - indexOffset;
                if (_.slideCount > _.options.slidesToShow) {
                    _.slideHandler(_.currentSlide - slideOffset, false, dontAnimate);
                }
                break;

            case 'next':
                slideOffset = indexOffset === 0 ? _.options.slidesToScroll : indexOffset;
                if (_.slideCount > _.options.slidesToShow) {
                    _.slideHandler(_.currentSlide + slideOffset, false, dontAnimate);
                }
                break;

            case 'index':
                var index = event.data.index === 0 ? 0 :
                    event.data.index || $target.index() * _.options.slidesToScroll;

                _.slideHandler(_.checkNavigable(index), false, dontAnimate);
                $target.children().trigger('focus');
                break;

            default:
                return;
        }

    };

    Slick.prototype.checkNavigable = function(index) {

        var _ = this,
            navigables, prevNavigable;

        navigables = _.getNavigableIndexes();
        prevNavigable = 0;
        if (index > navigables[navigables.length - 1]) {
            index = navigables[navigables.length - 1];
        } else {
            for (var n in navigables) {
                if (index < navigables[n]) {
                    index = prevNavigable;
                    break;
                }
                prevNavigable = navigables[n];
            }
        }

        return index;
    };

    Slick.prototype.cleanUpEvents = function() {

        var _ = this;

        if (_.options.dots && _.$dots !== null) {

            $('li', _.$dots).off('click.slick', _.changeSlide);

            if (_.options.pauseOnDotsHover === true && _.options.autoplay === true) {

                $('li', _.$dots)
                    .off('mouseenter.slick', $.proxy(_.setPaused, _, true))
                    .off('mouseleave.slick', $.proxy(_.setPaused, _, false));

            }

        }

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {
            _.$prevArrow && _.$prevArrow.off('click.slick', _.changeSlide);
            _.$nextArrow && _.$nextArrow.off('click.slick', _.changeSlide);
        }

        _.$list.off('touchstart.slick mousedown.slick', _.swipeHandler);
        _.$list.off('touchmove.slick mousemove.slick', _.swipeHandler);
        _.$list.off('touchend.slick mouseup.slick', _.swipeHandler);
        _.$list.off('touchcancel.slick mouseleave.slick', _.swipeHandler);

        _.$list.off('click.slick', _.clickHandler);

        $(document).off(_.visibilityChange, _.visibility);

        _.$list.off('mouseenter.slick', $.proxy(_.setPaused, _, true));
        _.$list.off('mouseleave.slick', $.proxy(_.setPaused, _, false));

        if (_.options.accessibility === true) {
            _.$list.off('keydown.slick', _.keyHandler);
        }

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().off('click.slick', _.selectHandler);
        }

        $(window).off('orientationchange.slick.slick-' + _.instanceUid, _.orientationChange);

        $(window).off('resize.slick.slick-' + _.instanceUid, _.resize);

        $('[draggable!=true]', _.$slideTrack).off('dragstart', _.preventDefault);

        $(window).off('load.slick.slick-' + _.instanceUid, _.setPosition);
        $(document).off('ready.slick.slick-' + _.instanceUid, _.setPosition);
    };

    Slick.prototype.cleanUpRows = function() {

        var _ = this, originalSlides;

        if(_.options.rows > 1) {
            originalSlides = _.$slides.children().children();
            originalSlides.removeAttr('style');
            _.$slider.html(originalSlides);
        }

    };

    Slick.prototype.clickHandler = function(event) {

        var _ = this;

        if (_.shouldClick === false) {
            event.stopImmediatePropagation();
            event.stopPropagation();
            event.preventDefault();
        }

    };

    Slick.prototype.destroy = function(refresh) {

        var _ = this;

        _.autoPlayClear();

        _.touchObject = {};

        _.cleanUpEvents();

        $('.slick-cloned', _.$slider).detach();

        if (_.$dots) {
            _.$dots.remove();
        }


        if ( _.$prevArrow && _.$prevArrow.length ) {

            _.$prevArrow
                .removeClass('slick-disabled slick-arrow slick-hidden')
                .removeAttr('aria-hidden aria-disabled tabindex')
                .css("display","");

            if ( _.htmlExpr.test( _.options.prevArrow )) {
                _.$prevArrow.remove();
            }
        }

        if ( _.$nextArrow && _.$nextArrow.length ) {

            _.$nextArrow
                .removeClass('slick-disabled slick-arrow slick-hidden')
                .removeAttr('aria-hidden aria-disabled tabindex')
                .css("display","");

            if ( _.htmlExpr.test( _.options.nextArrow )) {
                _.$nextArrow.remove();
            }

        }


        if (_.$slides) {

            _.$slides
                .removeClass('slick-slide slick-active slick-center slick-visible slick-current')
                .removeAttr('aria-hidden')
                .removeAttr('data-slick-index')
                .each(function(){
                    $(this).attr('style', $(this).data('originalStyling'));
                });

            _.$slideTrack.children(this.options.slide).detach();

            _.$slideTrack.detach();

            _.$list.detach();

            _.$slider.append(_.$slides);
        }

        _.cleanUpRows();

        _.$slider.removeClass('slick-slider');
        _.$slider.removeClass('slick-initialized');

        _.unslicked = true;

        if(!refresh) {
            _.$slider.trigger('destroy', [_]);
        }

    };

    Slick.prototype.disableTransition = function(slide) {

        var _ = this,
            transition = {};

        transition[_.transitionType] = '';

        if (_.options.fade === false) {
            _.$slideTrack.css(transition);
        } else {
            _.$slides.eq(slide).css(transition);
        }

    };

    Slick.prototype.fadeSlide = function(slideIndex, callback) {

        var _ = this;

        if (_.cssTransitions === false) {

            _.$slides.eq(slideIndex).css({
                zIndex: _.options.zIndex
            });

            _.$slides.eq(slideIndex).animate({
                opacity: 1
            }, _.options.speed, _.options.easing, callback);

        } else {

            _.applyTransition(slideIndex);

            _.$slides.eq(slideIndex).css({
                opacity: 1,
                zIndex: _.options.zIndex
            });

            if (callback) {
                setTimeout(function() {

                    _.disableTransition(slideIndex);

                    callback.call();
                }, _.options.speed);
            }

        }

    };

    Slick.prototype.fadeSlideOut = function(slideIndex) {

        var _ = this;

        if (_.cssTransitions === false) {

            _.$slides.eq(slideIndex).animate({
                opacity: 0,
                zIndex: _.options.zIndex - 2
            }, _.options.speed, _.options.easing);

        } else {

            _.applyTransition(slideIndex);

            _.$slides.eq(slideIndex).css({
                opacity: 0,
                zIndex: _.options.zIndex - 2
            });

        }

    };

    Slick.prototype.filterSlides = Slick.prototype.slickFilter = function(filter) {

        var _ = this;

        if (filter !== null) {

            _.$slidesCache = _.$slides;

            _.unload();

            _.$slideTrack.children(this.options.slide).detach();

            _.$slidesCache.filter(filter).appendTo(_.$slideTrack);

            _.reinit();

        }

    };

    Slick.prototype.getCurrent = Slick.prototype.slickCurrentSlide = function() {

        var _ = this;
        return _.currentSlide;

    };

    Slick.prototype.getDotCount = function() {

        var _ = this;

        var breakPoint = 0;
        var counter = 0;
        var pagerQty = 0;

        if (_.options.infinite === true) {
            while (breakPoint < _.slideCount) {
                ++pagerQty;
                breakPoint = counter + _.options.slidesToScroll;
                counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
            }
        } else if (_.options.centerMode === true) {
            pagerQty = _.slideCount;
        } else {
            while (breakPoint < _.slideCount) {
                ++pagerQty;
                breakPoint = counter + _.options.slidesToScroll;
                counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
            }
        }

        return pagerQty - 1;

    };

    Slick.prototype.getLeft = function(slideIndex) {

        var _ = this,
            targetLeft,
            verticalHeight,
            verticalOffset = 0,
            targetSlide;

        _.slideOffset = 0;
        verticalHeight = _.$slides.first().outerHeight(true);

        if (_.options.infinite === true) {
            if (_.slideCount > _.options.slidesToShow) {
                _.slideOffset = (_.slideWidth * _.options.slidesToShow) * -1;
                verticalOffset = (verticalHeight * _.options.slidesToShow) * -1;
            }
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                if (slideIndex + _.options.slidesToScroll > _.slideCount && _.slideCount > _.options.slidesToShow) {
                    if (slideIndex > _.slideCount) {
                        _.slideOffset = ((_.options.slidesToShow - (slideIndex - _.slideCount)) * _.slideWidth) * -1;
                        verticalOffset = ((_.options.slidesToShow - (slideIndex - _.slideCount)) * verticalHeight) * -1;
                    } else {
                        _.slideOffset = ((_.slideCount % _.options.slidesToScroll) * _.slideWidth) * -1;
                        verticalOffset = ((_.slideCount % _.options.slidesToScroll) * verticalHeight) * -1;
                    }
                }
            }
        } else {
            if (slideIndex + _.options.slidesToShow > _.slideCount) {
                _.slideOffset = ((slideIndex + _.options.slidesToShow) - _.slideCount) * _.slideWidth;
                verticalOffset = ((slideIndex + _.options.slidesToShow) - _.slideCount) * verticalHeight;
            }
        }

        if (_.slideCount <= _.options.slidesToShow) {
            _.slideOffset = 0;
            verticalOffset = 0;
        }

        if (_.options.centerMode === true && _.options.infinite === true) {
            _.slideOffset += _.slideWidth * Math.floor(_.options.slidesToShow / 2) - _.slideWidth;
        } else if (_.options.centerMode === true) {
            _.slideOffset = 0;
            _.slideOffset += _.slideWidth * Math.floor(_.options.slidesToShow / 2);
        }

        if (_.options.vertical === false) {
            targetLeft = ((slideIndex * _.slideWidth) * -1) + _.slideOffset;
        } else {
            targetLeft = ((slideIndex * verticalHeight) * -1) + verticalOffset;
        }

        if (_.options.variableWidth === true) {

            if (_.slideCount <= _.options.slidesToShow || _.options.infinite === false) {
                targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex);
            } else {
                targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex + _.options.slidesToShow);
            }

            if (_.options.rtl === true) {
                if (targetSlide[0]) {
                    targetLeft = (_.$slideTrack.width() - targetSlide[0].offsetLeft - targetSlide.width()) * -1;
                } else {
                    targetLeft =  0;
                }
            } else {
                targetLeft = targetSlide[0] ? targetSlide[0].offsetLeft * -1 : 0;
            }

            if (_.options.centerMode === true) {
                if (_.slideCount <= _.options.slidesToShow || _.options.infinite === false) {
                    targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex);
                } else {
                    targetSlide = _.$slideTrack.children('.slick-slide').eq(slideIndex + _.options.slidesToShow + 1);
                }

                if (_.options.rtl === true) {
                    if (targetSlide[0]) {
                        targetLeft = (_.$slideTrack.width() - targetSlide[0].offsetLeft - targetSlide.width()) * -1;
                    } else {
                        targetLeft =  0;
                    }
                } else {
                    targetLeft = targetSlide[0] ? targetSlide[0].offsetLeft * -1 : 0;
                }

                targetLeft += (_.$list.width() - targetSlide.outerWidth()) / 2;
            }
        }

        return targetLeft;

    };

    Slick.prototype.getOption = Slick.prototype.slickGetOption = function(option) {

        var _ = this;

        return _.options[option];

    };

    Slick.prototype.getNavigableIndexes = function() {

        var _ = this,
            breakPoint = 0,
            counter = 0,
            indexes = [],
            max;

        if (_.options.infinite === false) {
            max = _.slideCount;
        } else {
            breakPoint = _.options.slidesToScroll * -1;
            counter = _.options.slidesToScroll * -1;
            max = _.slideCount * 2;
        }

        while (breakPoint < max) {
            indexes.push(breakPoint);
            breakPoint = counter + _.options.slidesToScroll;
            counter += _.options.slidesToScroll <= _.options.slidesToShow ? _.options.slidesToScroll : _.options.slidesToShow;
        }

        return indexes;

    };

    Slick.prototype.getSlick = function() {

        return this;

    };

    Slick.prototype.getSlideCount = function() {

        var _ = this,
            slidesTraversed, swipedSlide, centerOffset;

        centerOffset = _.options.centerMode === true ? _.slideWidth * Math.floor(_.options.slidesToShow / 2) : 0;

        if (_.options.swipeToSlide === true) {
            _.$slideTrack.find('.slick-slide').each(function(index, slide) {
                if (slide.offsetLeft - centerOffset + ($(slide).outerWidth() / 2) > (_.swipeLeft * -1)) {
                    swipedSlide = slide;
                    return false;
                }
            });

            slidesTraversed = Math.abs($(swipedSlide).attr('data-slick-index') - _.currentSlide) || 1;

            return slidesTraversed;

        } else {
            return _.options.slidesToScroll;
        }

    };

    Slick.prototype.goTo = Slick.prototype.slickGoTo = function(slide, dontAnimate) {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'index',
                index: parseInt(slide)
            }
        }, dontAnimate);

    };

    Slick.prototype.init = function(creation) {

        var _ = this;

        if (!$(_.$slider).hasClass('slick-initialized')) {

            $(_.$slider).addClass('slick-initialized');

            _.buildRows();
            _.buildOut();
            _.setProps();
            _.startLoad();
            _.loadSlider();
            _.initializeEvents();
            _.updateArrows();
            _.updateDots();

        }

        if (creation) {
            _.$slider.trigger('init', [_]);
        }

        if (_.options.accessibility === true) {
            _.initADA();
        }

    };

    Slick.prototype.initArrowEvents = function() {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {
            _.$prevArrow.on('click.slick', {
                message: 'previous'
            }, _.changeSlide);
            _.$nextArrow.on('click.slick', {
                message: 'next'
            }, _.changeSlide);
        }

    };

    Slick.prototype.initDotEvents = function() {

        var _ = this;

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {
            $('li', _.$dots).on('click.slick', {
                message: 'index'
            }, _.changeSlide);
        }

        if (_.options.dots === true && _.options.pauseOnDotsHover === true && _.options.autoplay === true) {
            $('li', _.$dots)
                .on('mouseenter.slick', $.proxy(_.setPaused, _, true))
                .on('mouseleave.slick', $.proxy(_.setPaused, _, false));
        }

    };

    Slick.prototype.initializeEvents = function() {

        var _ = this;

        _.initArrowEvents();

        _.initDotEvents();

        _.$list.on('touchstart.slick mousedown.slick', {
            action: 'start'
        }, _.swipeHandler);
        _.$list.on('touchmove.slick mousemove.slick', {
            action: 'move'
        }, _.swipeHandler);
        _.$list.on('touchend.slick mouseup.slick', {
            action: 'end'
        }, _.swipeHandler);
        _.$list.on('touchcancel.slick mouseleave.slick', {
            action: 'end'
        }, _.swipeHandler);

        _.$list.on('click.slick', _.clickHandler);

        $(document).on(_.visibilityChange, $.proxy(_.visibility, _));

        _.$list.on('mouseenter.slick', $.proxy(_.setPaused, _, true));
        _.$list.on('mouseleave.slick', $.proxy(_.setPaused, _, false));

        if (_.options.accessibility === true) {
            _.$list.on('keydown.slick', _.keyHandler);
        }

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().on('click.slick', _.selectHandler);
        }

        $(window).on('orientationchange.slick.slick-' + _.instanceUid, $.proxy(_.orientationChange, _));

        $(window).on('resize.slick.slick-' + _.instanceUid, $.proxy(_.resize, _));

        $('[draggable!=true]', _.$slideTrack).on('dragstart', _.preventDefault);

        $(window).on('load.slick.slick-' + _.instanceUid, _.setPosition);
        $(document).on('ready.slick.slick-' + _.instanceUid, _.setPosition);

    };

    Slick.prototype.initUI = function() {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {

            _.$prevArrow.show();
            _.$nextArrow.show();

        }

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            _.$dots.show();

        }

        if (_.options.autoplay === true) {

            _.autoPlay();

        }

    };

    Slick.prototype.keyHandler = function(event) {

        var _ = this;
         //Dont slide if the cursor is inside the form fields and arrow keys are pressed
        if(!event.target.tagName.match('TEXTAREA|INPUT|SELECT')) {
            if (event.keyCode === 37 && _.options.accessibility === true) {
                _.changeSlide({
                    data: {
                        message: 'previous'
                    }
                });
            } else if (event.keyCode === 39 && _.options.accessibility === true) {
                _.changeSlide({
                    data: {
                        message: 'next'
                    }
                });
            }
        }

    };

    Slick.prototype.lazyLoad = function() {

        var _ = this,
            loadRange, cloneRange, rangeStart, rangeEnd;

        function loadImages(imagesScope) {
            $('img[data-lazy]', imagesScope).each(function() {

                var image = $(this),
                    imageSource = $(this).attr('data-lazy'),
                    imageToLoad = document.createElement('img');

                imageToLoad.onload = function() {
                    image
                        .animate({ opacity: 0 }, 100, function() {
                            image
                                .attr('src', imageSource)
                                .animate({ opacity: 1 }, 200, function() {
                                    image
                                        .removeAttr('data-lazy')
                                        .removeClass('slick-loading');
                                });
                        });
                };

                imageToLoad.src = imageSource;

            });
        }

        if (_.options.centerMode === true) {
            if (_.options.infinite === true) {
                rangeStart = _.currentSlide + (_.options.slidesToShow / 2 + 1);
                rangeEnd = rangeStart + _.options.slidesToShow + 2;
            } else {
                rangeStart = Math.max(0, _.currentSlide - (_.options.slidesToShow / 2 + 1));
                rangeEnd = 2 + (_.options.slidesToShow / 2 + 1) + _.currentSlide;
            }
        } else {
            rangeStart = _.options.infinite ? _.options.slidesToShow + _.currentSlide : _.currentSlide;
            rangeEnd = rangeStart + _.options.slidesToShow;
            if (_.options.fade === true) {
                if (rangeStart > 0) rangeStart--;
                if (rangeEnd <= _.slideCount) rangeEnd++;
            }
        }

        loadRange = _.$slider.find('.slick-slide').slice(rangeStart, rangeEnd);
        loadImages(loadRange);

        if (_.slideCount <= _.options.slidesToShow) {
            cloneRange = _.$slider.find('.slick-slide');
            loadImages(cloneRange);
        } else
        if (_.currentSlide >= _.slideCount - _.options.slidesToShow) {
            cloneRange = _.$slider.find('.slick-cloned').slice(0, _.options.slidesToShow);
            loadImages(cloneRange);
        } else if (_.currentSlide === 0) {
            cloneRange = _.$slider.find('.slick-cloned').slice(_.options.slidesToShow * -1);
            loadImages(cloneRange);
        }

    };

    Slick.prototype.loadSlider = function() {

        var _ = this;

        _.setPosition();

        _.$slideTrack.css({
            opacity: 1
        });

        _.$slider.removeClass('slick-loading');

        _.initUI();

        if (_.options.lazyLoad === 'progressive') {
            _.progressiveLazyLoad();
        }

    };

    Slick.prototype.next = Slick.prototype.slickNext = function() {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'next'
            }
        });

    };

    Slick.prototype.orientationChange = function() {

        var _ = this;

        _.checkResponsive();
        _.setPosition();

    };

    Slick.prototype.pause = Slick.prototype.slickPause = function() {

        var _ = this;

        _.autoPlayClear();
        _.paused = true;

    };

    Slick.prototype.play = Slick.prototype.slickPlay = function() {

        var _ = this;

        _.paused = false;
        _.autoPlay();

    };

    Slick.prototype.postSlide = function(index) {

        var _ = this;

        _.$slider.trigger('afterChange', [_, index]);

        _.animating = false;

        _.setPosition();

        _.swipeLeft = null;

        if (_.options.autoplay === true && _.paused === false) {
            _.autoPlay();
        }
        if (_.options.accessibility === true) {
            _.initADA();
        }

    };

    Slick.prototype.prev = Slick.prototype.slickPrev = function() {

        var _ = this;

        _.changeSlide({
            data: {
                message: 'previous'
            }
        });

    };

    Slick.prototype.preventDefault = function(event) {
        event.preventDefault();
    };

    Slick.prototype.progressiveLazyLoad = function() {

        var _ = this,
            imgCount, targetImage;

        imgCount = $('img[data-lazy]', _.$slider).length;

        if (imgCount > 0) {
            targetImage = $('img[data-lazy]', _.$slider).first();
            targetImage.attr('src', null);
            targetImage.attr('src', targetImage.attr('data-lazy')).removeClass('slick-loading').load(function() {
                    targetImage.removeAttr('data-lazy');
                    _.progressiveLazyLoad();

                    if (_.options.adaptiveHeight === true) {
                        _.setPosition();
                    }
                })
                .error(function() {
                    targetImage.removeAttr('data-lazy');
                    _.progressiveLazyLoad();
                });
        }

    };

    Slick.prototype.refresh = function( initializing ) {

        var _ = this, currentSlide, firstVisible;

        firstVisible = _.slideCount - _.options.slidesToShow;

        // check that the new breakpoint can actually accept the
        // "current slide" as the current slide, otherwise we need
        // to set it to the closest possible value.
        if ( !_.options.infinite ) {
            if ( _.slideCount <= _.options.slidesToShow ) {
                _.currentSlide = 0;
            } else if ( _.currentSlide > firstVisible ) {
                _.currentSlide = firstVisible;
            }
        }

         currentSlide = _.currentSlide;

        _.destroy(true);

        $.extend(_, _.initials, { currentSlide: currentSlide });

        _.init();

        if( !initializing ) {

            _.changeSlide({
                data: {
                    message: 'index',
                    index: currentSlide
                }
            }, false);

        }

    };

    Slick.prototype.registerBreakpoints = function() {

        var _ = this, breakpoint, currentBreakpoint, l,
            responsiveSettings = _.options.responsive || null;

        if ( $.type(responsiveSettings) === "array" && responsiveSettings.length ) {

            _.respondTo = _.options.respondTo || 'window';

            for ( breakpoint in responsiveSettings ) {

                l = _.breakpoints.length-1;
                currentBreakpoint = responsiveSettings[breakpoint].breakpoint;

                if (responsiveSettings.hasOwnProperty(breakpoint)) {

                    // loop through the breakpoints and cut out any existing
                    // ones with the same breakpoint number, we don't want dupes.
                    while( l >= 0 ) {
                        if( _.breakpoints[l] && _.breakpoints[l] === currentBreakpoint ) {
                            _.breakpoints.splice(l,1);
                        }
                        l--;
                    }

                    _.breakpoints.push(currentBreakpoint);
                    _.breakpointSettings[currentBreakpoint] = responsiveSettings[breakpoint].settings;

                }

            }

            _.breakpoints.sort(function(a, b) {
                return ( _.options.mobileFirst ) ? a-b : b-a;
            });

        }

    };

    Slick.prototype.reinit = function() {

        var _ = this;

        _.$slides =
            _.$slideTrack
                .children(_.options.slide)
                .addClass('slick-slide');

        _.slideCount = _.$slides.length;

        if (_.currentSlide >= _.slideCount && _.currentSlide !== 0) {
            _.currentSlide = _.currentSlide - _.options.slidesToScroll;
        }

        if (_.slideCount <= _.options.slidesToShow) {
            _.currentSlide = 0;
        }

        _.registerBreakpoints();

        _.setProps();
        _.setupInfinite();
        _.buildArrows();
        _.updateArrows();
        _.initArrowEvents();
        _.buildDots();
        _.updateDots();
        _.initDotEvents();

        _.checkResponsive(false, true);

        if (_.options.focusOnSelect === true) {
            $(_.$slideTrack).children().on('click.slick', _.selectHandler);
        }

        _.setSlideClasses(0);

        _.setPosition();

        _.$slider.trigger('reInit', [_]);

        if (_.options.autoplay === true) {
            _.focusHandler();
        }

    };

    Slick.prototype.resize = function() {

        var _ = this;

        if ($(window).width() !== _.windowWidth) {
            clearTimeout(_.windowDelay);
            _.windowDelay = window.setTimeout(function() {
                _.windowWidth = $(window).width();
                _.checkResponsive();
                if( !_.unslicked ) { _.setPosition(); }
            }, 50);
        }
    };

    Slick.prototype.removeSlide = Slick.prototype.slickRemove = function(index, removeBefore, removeAll) {

        var _ = this;

        if (typeof(index) === 'boolean') {
            removeBefore = index;
            index = removeBefore === true ? 0 : _.slideCount - 1;
        } else {
            index = removeBefore === true ? --index : index;
        }

        if (_.slideCount < 1 || index < 0 || index > _.slideCount - 1) {
            return false;
        }

        _.unload();

        if (removeAll === true) {
            _.$slideTrack.children().remove();
        } else {
            _.$slideTrack.children(this.options.slide).eq(index).remove();
        }

        _.$slides = _.$slideTrack.children(this.options.slide);

        _.$slideTrack.children(this.options.slide).detach();

        _.$slideTrack.append(_.$slides);

        _.$slidesCache = _.$slides;

        _.reinit();

    };

    Slick.prototype.setCSS = function(position) {

        var _ = this,
            positionProps = {},
            x, y;

        if (_.options.rtl === true) {
            position = -position;
        }
        x = _.positionProp == 'left' ? Math.ceil(position) + 'px' : '0px';
        y = _.positionProp == 'top' ? Math.ceil(position) + 'px' : '0px';

        positionProps[_.positionProp] = position;

        if (_.transformsEnabled === false) {
            _.$slideTrack.css(positionProps);
        } else {
            positionProps = {};
            if (_.cssTransitions === false) {
                positionProps[_.animType] = 'translate(' + x + ', ' + y + ')';
                _.$slideTrack.css(positionProps);
            } else {
                positionProps[_.animType] = 'translate3d(' + x + ', ' + y + ', 0px)';
                _.$slideTrack.css(positionProps);
            }
        }

    };

    Slick.prototype.setDimensions = function() {

        var _ = this;

        if (_.options.vertical === false) {
            if (_.options.centerMode === true) {
                _.$list.css({
                    padding: ('0px ' + _.options.centerPadding)
                });
            }
        } else {
            _.$list.height(_.$slides.first().outerHeight(true) * _.options.slidesToShow);
            if (_.options.centerMode === true) {
                _.$list.css({
                    padding: (_.options.centerPadding + ' 0px')
                });
            }
        }

        _.listWidth = _.$list.width();
        _.listHeight = _.$list.height();


        if (_.options.vertical === false && _.options.variableWidth === false) {
            _.slideWidth = Math.ceil(_.listWidth / _.options.slidesToShow);
            _.$slideTrack.width(Math.ceil((_.slideWidth * _.$slideTrack.children('.slick-slide').length)));

        } else if (_.options.variableWidth === true) {
            _.$slideTrack.width(5000 * _.slideCount);
        } else {
            _.slideWidth = Math.ceil(_.listWidth);
            _.$slideTrack.height(Math.ceil((_.$slides.first().outerHeight(true) * _.$slideTrack.children('.slick-slide').length)));
        }

        var offset = _.$slides.first().outerWidth(true) - _.$slides.first().width();
        if (_.options.variableWidth === false) _.$slideTrack.children('.slick-slide').width(_.slideWidth - offset);

    };

    Slick.prototype.setFade = function() {

        var _ = this,
            targetLeft;

        _.$slides.each(function(index, element) {
            targetLeft = (_.slideWidth * index) * -1;
            if (_.options.rtl === true) {
                $(element).css({
                    position: 'relative',
                    right: targetLeft,
                    top: 0,
                    zIndex: _.options.zIndex - 2,
                    opacity: 0
                });
            } else {
                $(element).css({
                    position: 'relative',
                    left: targetLeft,
                    top: 0,
                    zIndex: _.options.zIndex - 2,
                    opacity: 0
                });
            }
        });

        _.$slides.eq(_.currentSlide).css({
            zIndex: _.options.zIndex - 1,
            opacity: 1
        });

    };

    Slick.prototype.setHeight = function() {

        var _ = this;

        if (_.options.slidesToShow === 1 && _.options.adaptiveHeight === true && _.options.vertical === false) {
            var targetHeight = _.$slides.eq(_.currentSlide).outerHeight(true);
            _.$list.css('height', targetHeight);
        }

    };

    Slick.prototype.setOption = Slick.prototype.slickSetOption = function(option, value, refresh) {

        var _ = this, l, item;

        if( option === "responsive" && $.type(value) === "array" ) {
            for ( item in value ) {
                if( $.type( _.options.responsive ) !== "array" ) {
                    _.options.responsive = [ value[item] ];
                } else {
                    l = _.options.responsive.length-1;
                    // loop through the responsive object and splice out duplicates.
                    while( l >= 0 ) {
                        if( _.options.responsive[l].breakpoint === value[item].breakpoint ) {
                            _.options.responsive.splice(l,1);
                        }
                        l--;
                    }
                    _.options.responsive.push( value[item] );
                }
            }
        } else {
            _.options[option] = value;
        }

        if (refresh === true) {
            _.unload();
            _.reinit();
        }

    };

    Slick.prototype.setPosition = function() {

        var _ = this;

        _.setDimensions();

        _.setHeight();

        if (_.options.fade === false) {
            _.setCSS(_.getLeft(_.currentSlide));
        } else {
            _.setFade();
        }

        _.$slider.trigger('setPosition', [_]);

    };

    Slick.prototype.setProps = function() {

        var _ = this,
            bodyStyle = document.body.style;

        _.positionProp = _.options.vertical === true ? 'top' : 'left';

        if (_.positionProp === 'top') {
            _.$slider.addClass('slick-vertical');
        } else {
            _.$slider.removeClass('slick-vertical');
        }

        if (bodyStyle.WebkitTransition !== undefined ||
            bodyStyle.MozTransition !== undefined ||
            bodyStyle.msTransition !== undefined) {
            if (_.options.useCSS === true) {
                _.cssTransitions = true;
            }
        }

        if ( _.options.fade ) {
            if ( typeof _.options.zIndex === 'number' ) {
                if( _.options.zIndex < 3 ) {
                    _.options.zIndex = 3;
                }
            } else {
                _.options.zIndex = _.defaults.zIndex;
            }
        }

        if (bodyStyle.OTransform !== undefined) {
            _.animType = 'OTransform';
            _.transformType = '-o-transform';
            _.transitionType = 'OTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.webkitPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.MozTransform !== undefined) {
            _.animType = 'MozTransform';
            _.transformType = '-moz-transform';
            _.transitionType = 'MozTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.MozPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.webkitTransform !== undefined) {
            _.animType = 'webkitTransform';
            _.transformType = '-webkit-transform';
            _.transitionType = 'webkitTransition';
            if (bodyStyle.perspectiveProperty === undefined && bodyStyle.webkitPerspective === undefined) _.animType = false;
        }
        if (bodyStyle.msTransform !== undefined) {
            _.animType = 'msTransform';
            _.transformType = '-ms-transform';
            _.transitionType = 'msTransition';
            if (bodyStyle.msTransform === undefined) _.animType = false;
        }
        if (bodyStyle.transform !== undefined && _.animType !== false) {
            _.animType = 'transform';
            _.transformType = 'transform';
            _.transitionType = 'transition';
        }
        _.transformsEnabled = _.options.useTransform && (_.animType !== null && _.animType !== false);
    };


    Slick.prototype.setSlideClasses = function(index) {

        var _ = this,
            centerOffset, allSlides, indexOffset, remainder;

        allSlides = _.$slider
            .find('.slick-slide')
            .removeClass('slick-active slick-center slick-current')
            .attr('aria-hidden', 'true');

        _.$slides
            .eq(index)
            .addClass('slick-current');

        if (_.options.centerMode === true) {

            centerOffset = Math.floor(_.options.slidesToShow / 2);

            if (_.options.infinite === true) {

                if (index >= centerOffset && index <= (_.slideCount - 1) - centerOffset) {

                    _.$slides
                        .slice(index - centerOffset, index + centerOffset + 1)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                } else {

                    indexOffset = _.options.slidesToShow + index;
                    allSlides
                        .slice(indexOffset - centerOffset + 1, indexOffset + centerOffset + 2)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                }

                if (index === 0) {

                    allSlides
                        .eq(allSlides.length - 1 - _.options.slidesToShow)
                        .addClass('slick-center');

                } else if (index === _.slideCount - 1) {

                    allSlides
                        .eq(_.options.slidesToShow)
                        .addClass('slick-center');

                }

            }

            _.$slides
                .eq(index)
                .addClass('slick-center');

        } else {

            if (index >= 0 && index <= (_.slideCount - _.options.slidesToShow)) {

                _.$slides
                    .slice(index, index + _.options.slidesToShow)
                    .addClass('slick-active')
                    .attr('aria-hidden', 'false');

            } else if (allSlides.length <= _.options.slidesToShow) {

                allSlides
                    .addClass('slick-active')
                    .attr('aria-hidden', 'false');

            } else {

                remainder = _.slideCount % _.options.slidesToShow;
                indexOffset = _.options.infinite === true ? _.options.slidesToShow + index : index;

                if (_.options.slidesToShow == _.options.slidesToScroll && (_.slideCount - index) < _.options.slidesToShow) {

                    allSlides
                        .slice(indexOffset - (_.options.slidesToShow - remainder), indexOffset + remainder)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                } else {

                    allSlides
                        .slice(indexOffset, indexOffset + _.options.slidesToShow)
                        .addClass('slick-active')
                        .attr('aria-hidden', 'false');

                }

            }

        }

        if (_.options.lazyLoad === 'ondemand') {
            _.lazyLoad();
        }

    };

    Slick.prototype.setupInfinite = function() {

        var _ = this,
            i, slideIndex, infiniteCount;

        if (_.options.fade === true) {
            _.options.centerMode = false;
        }

        if (_.options.infinite === true && _.options.fade === false) {

            slideIndex = null;

            if (_.slideCount > _.options.slidesToShow) {

                if (_.options.centerMode === true) {
                    infiniteCount = _.options.slidesToShow + 1;
                } else {
                    infiniteCount = _.options.slidesToShow;
                }

                for (i = _.slideCount; i > (_.slideCount -
                        infiniteCount); i -= 1) {
                    slideIndex = i - 1;
                    $(_.$slides[slideIndex]).clone(true).attr('id', '')
                        .attr('data-slick-index', slideIndex - _.slideCount)
                        .prependTo(_.$slideTrack).addClass('slick-cloned');
                }
                for (i = 0; i < infiniteCount; i += 1) {
                    slideIndex = i;
                    $(_.$slides[slideIndex]).clone(true).attr('id', '')
                        .attr('data-slick-index', slideIndex + _.slideCount)
                        .appendTo(_.$slideTrack).addClass('slick-cloned');
                }
                _.$slideTrack.find('.slick-cloned').find('[id]').each(function() {
                    $(this).attr('id', '');
                });

            }

        }

    };

    Slick.prototype.setPaused = function(paused) {

        var _ = this;

        if (_.options.autoplay === true && _.options.pauseOnHover === true) {
            _.paused = paused;
            if (!paused) {
                _.autoPlay();
            } else {
                _.autoPlayClear();
            }
        }
    };

    Slick.prototype.selectHandler = function(event) {

        var _ = this;

        var targetElement =
            $(event.target).is('.slick-slide') ?
                $(event.target) :
                $(event.target).parents('.slick-slide');

        var index = parseInt(targetElement.attr('data-slick-index'));

        if (!index) index = 0;

        if (_.slideCount <= _.options.slidesToShow) {

            _.setSlideClasses(index);
            _.asNavFor(index);
            return;

        }

        _.slideHandler(index);

    };

    Slick.prototype.slideHandler = function(index, sync, dontAnimate) {

        var targetSlide, animSlide, oldSlide, slideLeft, targetLeft = null,
            _ = this;

        sync = sync || false;

        if (_.animating === true && _.options.waitForAnimate === true) {
            return;
        }

        if (_.options.fade === true && _.currentSlide === index) {
            return;
        }

        if (_.slideCount <= _.options.slidesToShow) {
            return;
        }

        if (sync === false) {
            _.asNavFor(index);
        }

        targetSlide = index;
        targetLeft = _.getLeft(targetSlide);
        slideLeft = _.getLeft(_.currentSlide);

        _.currentLeft = _.swipeLeft === null ? slideLeft : _.swipeLeft;

        if (_.options.infinite === false && _.options.centerMode === false && (index < 0 || index > _.getDotCount() * _.options.slidesToScroll)) {
            if (_.options.fade === false) {
                targetSlide = _.currentSlide;
                if (dontAnimate !== true) {
                    _.animateSlide(slideLeft, function() {
                        _.postSlide(targetSlide);
                    });
                } else {
                    _.postSlide(targetSlide);
                }
            }
            return;
        } else if (_.options.infinite === false && _.options.centerMode === true && (index < 0 || index > (_.slideCount - _.options.slidesToScroll))) {
            if (_.options.fade === false) {
                targetSlide = _.currentSlide;
                if (dontAnimate !== true) {
                    _.animateSlide(slideLeft, function() {
                        _.postSlide(targetSlide);
                    });
                } else {
                    _.postSlide(targetSlide);
                }
            }
            return;
        }

        if (_.options.autoplay === true) {
            clearInterval(_.autoPlayTimer);
        }

        if (targetSlide < 0) {
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                animSlide = _.slideCount - (_.slideCount % _.options.slidesToScroll);
            } else {
                animSlide = _.slideCount + targetSlide;
            }
        } else if (targetSlide >= _.slideCount) {
            if (_.slideCount % _.options.slidesToScroll !== 0) {
                animSlide = 0;
            } else {
                animSlide = targetSlide - _.slideCount;
            }
        } else {
            animSlide = targetSlide;
        }

        _.animating = true;

        _.$slider.trigger('beforeChange', [_, _.currentSlide, animSlide]);

        oldSlide = _.currentSlide;
        _.currentSlide = animSlide;

        _.setSlideClasses(_.currentSlide);

        _.updateDots();
        _.updateArrows();

        if (_.options.fade === true) {
            if (dontAnimate !== true) {

                _.fadeSlideOut(oldSlide);

                _.fadeSlide(animSlide, function() {
                    _.postSlide(animSlide);
                });

            } else {
                _.postSlide(animSlide);
            }
            _.animateHeight();
            return;
        }

        if (dontAnimate !== true) {
            _.animateSlide(targetLeft, function() {
                _.postSlide(animSlide);
            });
        } else {
            _.postSlide(animSlide);
        }

    };

    Slick.prototype.startLoad = function() {

        var _ = this;

        if (_.options.arrows === true && _.slideCount > _.options.slidesToShow) {

            _.$prevArrow.hide();
            _.$nextArrow.hide();

        }

        if (_.options.dots === true && _.slideCount > _.options.slidesToShow) {

            _.$dots.hide();

        }

        _.$slider.addClass('slick-loading');

    };

    Slick.prototype.swipeDirection = function() {

        var xDist, yDist, r, swipeAngle, _ = this;

        xDist = _.touchObject.startX - _.touchObject.curX;
        yDist = _.touchObject.startY - _.touchObject.curY;
        r = Math.atan2(yDist, xDist);

        swipeAngle = Math.round(r * 180 / Math.PI);
        if (swipeAngle < 0) {
            swipeAngle = 360 - Math.abs(swipeAngle);
        }

        if ((swipeAngle <= 45) && (swipeAngle >= 0)) {
            return (_.options.rtl === false ? 'left' : 'right');
        }
        if ((swipeAngle <= 360) && (swipeAngle >= 315)) {
            return (_.options.rtl === false ? 'left' : 'right');
        }
        if ((swipeAngle >= 135) && (swipeAngle <= 225)) {
            return (_.options.rtl === false ? 'right' : 'left');
        }
        if (_.options.verticalSwiping === true) {
            if ((swipeAngle >= 35) && (swipeAngle <= 135)) {
                return 'left';
            } else {
                return 'right';
            }
        }

        return 'vertical';

    };

    Slick.prototype.swipeEnd = function(event) {

        var _ = this,
            slideCount;

        _.dragging = false;

        _.shouldClick = (_.touchObject.swipeLength > 10) ? false : true;

        if (_.touchObject.curX === undefined) {
            return false;
        }

        if (_.touchObject.edgeHit === true) {
            _.$slider.trigger('edge', [_, _.swipeDirection()]);
        }

        if (_.touchObject.swipeLength >= _.touchObject.minSwipe) {

            switch (_.swipeDirection()) {
                case 'left':
                    slideCount = _.options.swipeToSlide ? _.checkNavigable(_.currentSlide + _.getSlideCount()) : _.currentSlide + _.getSlideCount();
                    _.slideHandler(slideCount);
                    _.currentDirection = 0;
                    _.touchObject = {};
                    _.$slider.trigger('swipe', [_, 'left']);
                    break;

                case 'right':
                    slideCount = _.options.swipeToSlide ? _.checkNavigable(_.currentSlide - _.getSlideCount()) : _.currentSlide - _.getSlideCount();
                    _.slideHandler(slideCount);
                    _.currentDirection = 1;
                    _.touchObject = {};
                    _.$slider.trigger('swipe', [_, 'right']);
                    break;
            }
        } else {
            if (_.touchObject.startX !== _.touchObject.curX) {
                _.slideHandler(_.currentSlide);
                _.touchObject = {};
            }
        }

    };

    Slick.prototype.swipeHandler = function(event) {

        var _ = this;

        if ((_.options.swipe === false) || ('ontouchend' in document && _.options.swipe === false)) {
            return;
        } else if (_.options.draggable === false && event.type.indexOf('mouse') !== -1) {
            return;
        }

        _.touchObject.fingerCount = event.originalEvent && event.originalEvent.touches !== undefined ?
            event.originalEvent.touches.length : 1;

        _.touchObject.minSwipe = _.listWidth / _.options
            .touchThreshold;

        if (_.options.verticalSwiping === true) {
            _.touchObject.minSwipe = _.listHeight / _.options
                .touchThreshold;
        }

        switch (event.data.action) {

            case 'start':
                _.swipeStart(event);
                break;

            case 'move':
                _.swipeMove(event);
                break;

            case 'end':
                _.swipeEnd(event);
                break;

        }

    };

    Slick.prototype.swipeMove = function(event) {

        var _ = this,
            edgeWasHit = false,
            curLeft, swipeDirection, swipeLength, positionOffset, touches;

        touches = event.originalEvent !== undefined ? event.originalEvent.touches : null;

        if (!_.dragging || touches && touches.length !== 1) {
            return false;
        }

        curLeft = _.getLeft(_.currentSlide);

        _.touchObject.curX = touches !== undefined ? touches[0].pageX : event.clientX;
        _.touchObject.curY = touches !== undefined ? touches[0].pageY : event.clientY;

        _.touchObject.swipeLength = Math.round(Math.sqrt(
            Math.pow(_.touchObject.curX - _.touchObject.startX, 2)));

        if (_.options.verticalSwiping === true) {
            _.touchObject.swipeLength = Math.round(Math.sqrt(
                Math.pow(_.touchObject.curY - _.touchObject.startY, 2)));
        }

        swipeDirection = _.swipeDirection();

        if (swipeDirection === 'vertical') {
            return;
        }

        if (event.originalEvent !== undefined && _.touchObject.swipeLength > 4) {
            event.preventDefault();
        }

        positionOffset = (_.options.rtl === false ? 1 : -1) * (_.touchObject.curX > _.touchObject.startX ? 1 : -1);
        if (_.options.verticalSwiping === true) {
            positionOffset = _.touchObject.curY > _.touchObject.startY ? 1 : -1;
        }


        swipeLength = _.touchObject.swipeLength;

        _.touchObject.edgeHit = false;

        if (_.options.infinite === false) {
            if ((_.currentSlide === 0 && swipeDirection === 'right') || (_.currentSlide >= _.getDotCount() && swipeDirection === 'left')) {
                swipeLength = _.touchObject.swipeLength * _.options.edgeFriction;
                _.touchObject.edgeHit = true;
            }
        }

        if (_.options.vertical === false) {
            _.swipeLeft = curLeft + swipeLength * positionOffset;
        } else {
            _.swipeLeft = curLeft + (swipeLength * (_.$list.height() / _.listWidth)) * positionOffset;
        }
        if (_.options.verticalSwiping === true) {
            _.swipeLeft = curLeft + swipeLength * positionOffset;
        }

        if (_.options.fade === true || _.options.touchMove === false) {
            return false;
        }

        if (_.animating === true) {
            _.swipeLeft = null;
            return false;
        }

        _.setCSS(_.swipeLeft);

    };

    Slick.prototype.swipeStart = function(event) {

        var _ = this,
            touches;

        if (_.touchObject.fingerCount !== 1 || _.slideCount <= _.options.slidesToShow) {
            _.touchObject = {};
            return false;
        }

        if (event.originalEvent !== undefined && event.originalEvent.touches !== undefined) {
            touches = event.originalEvent.touches[0];
        }

        _.touchObject.startX = _.touchObject.curX = touches !== undefined ? touches.pageX : event.clientX;
        _.touchObject.startY = _.touchObject.curY = touches !== undefined ? touches.pageY : event.clientY;

        _.dragging = true;

    };

    Slick.prototype.unfilterSlides = Slick.prototype.slickUnfilter = function() {

        var _ = this;

        if (_.$slidesCache !== null) {

            _.unload();

            _.$slideTrack.children(this.options.slide).detach();

            _.$slidesCache.appendTo(_.$slideTrack);

            _.reinit();

        }

    };

    Slick.prototype.unload = function() {

        var _ = this;

        $('.slick-cloned', _.$slider).remove();

        if (_.$dots) {
            _.$dots.remove();
        }

        if (_.$prevArrow && _.htmlExpr.test(_.options.prevArrow)) {
            _.$prevArrow.remove();
        }

        if (_.$nextArrow && _.htmlExpr.test(_.options.nextArrow)) {
            _.$nextArrow.remove();
        }

        _.$slides
            .removeClass('slick-slide slick-active slick-visible slick-current')
            .attr('aria-hidden', 'true')
            .css('width', '');

    };

    Slick.prototype.unslick = function(fromBreakpoint) {

        var _ = this;
        _.$slider.trigger('unslick', [_, fromBreakpoint]);
        _.destroy();

    };

    Slick.prototype.updateArrows = function() {

        var _ = this,
            centerOffset;

        centerOffset = Math.floor(_.options.slidesToShow / 2);

        if ( _.options.arrows === true &&
            _.slideCount > _.options.slidesToShow &&
            !_.options.infinite ) {

            _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');
            _.$nextArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            if (_.currentSlide === 0) {

                _.$prevArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$nextArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            } else if (_.currentSlide >= _.slideCount - _.options.slidesToShow && _.options.centerMode === false) {

                _.$nextArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            } else if (_.currentSlide >= _.slideCount - 1 && _.options.centerMode === true) {

                _.$nextArrow.addClass('slick-disabled').attr('aria-disabled', 'true');
                _.$prevArrow.removeClass('slick-disabled').attr('aria-disabled', 'false');

            }

        }

    };

    Slick.prototype.updateDots = function() {

        var _ = this;

        if (_.$dots !== null) {

            _.$dots
                .find('li')
                .removeClass('slick-active')
                .attr('aria-hidden', 'true');

            _.$dots
                .find('li')
                .eq(Math.floor(_.currentSlide / _.options.slidesToScroll))
                .addClass('slick-active')
                .attr('aria-hidden', 'false');

        }

    };

    Slick.prototype.visibility = function() {

        var _ = this;

        if (document[_.hidden]) {
            _.paused = true;
            _.autoPlayClear();
        } else {
            if (_.options.autoplay === true) {
                _.paused = false;
                _.autoPlay();
            }
        }

    };
    Slick.prototype.initADA = function() {
        var _ = this;
        _.$slides.add(_.$slideTrack.find('.slick-cloned')).attr({
            'aria-hidden': 'true',
            'tabindex': '-1'
        }).find('a, input, button, select').attr({
            'tabindex': '-1'
        });

        _.$slideTrack.attr('role', 'listbox');

        _.$slides.not(_.$slideTrack.find('.slick-cloned')).each(function(i) {
            $(this).attr({
                'role': 'option',
                'aria-describedby': 'slick-slide' + _.instanceUid + i + ''
            });
        });

        if (_.$dots !== null) {
            _.$dots.attr('role', 'tablist').find('li').each(function(i) {
                $(this).attr({
                    'role': 'presentation',
                    'aria-selected': 'false',
                    'aria-controls': 'navigation' + _.instanceUid + i + '',
                    'id': 'slick-slide' + _.instanceUid + i + ''
                });
            })
                .first().attr('aria-selected', 'true').end()
                .find('button').attr('role', 'button').end()
                .closest('div').attr('role', 'toolbar');
        }
        _.activateADA();

    };

    Slick.prototype.activateADA = function() {
        var _ = this;

        _.$slideTrack.find('.slick-active').attr({
            'aria-hidden': 'false'
        }).find('a, input, button, select').attr({
            'tabindex': '0'
        });

    };

    Slick.prototype.focusHandler = function() {
        var _ = this;
        _.$slider.on('focus.slick blur.slick', '*', function(event) {
            event.stopImmediatePropagation();
            var sf = $(this);
            setTimeout(function() {
                if (_.isPlay) {
                    if (sf.is(':focus')) {
                        _.autoPlayClear();
                        _.paused = true;
                    } else {
                        _.paused = false;
                        _.autoPlay();
                    }
                }
            }, 0);
        });
    };

    $.fn.slick = function() {
        var _ = this,
            opt = arguments[0],
            args = Array.prototype.slice.call(arguments, 1),
            l = _.length,
            i,
            ret;
        for (i = 0; i < l; i++) {
            if (typeof opt == 'object' || typeof opt == 'undefined')
                _[i].slick = new Slick(_[i], opt);
            else
                ret = _[i].slick[opt].apply(_[i].slick, args);
            if (typeof ret != 'undefined') return ret;
        }
        return _;
    };

}));
/*
     _ _      _       _
 ___| (_) ___| | __  (_)___
/ __| | |/ __| |/ /  | / __|
\__ \ | | (__|   < _ | \__ \
|___/_|_|\___|_|\_(_)/ |___/
                   |__/

 Version: 1.5.9
  Author: Ken Wheeler
 Website: http://kenwheeler.github.io
    Docs: http://kenwheeler.github.io/slick
    Repo: http://github.com/kenwheeler/slick
  Issues: http://github.com/kenwheeler/slick/issues

 */

!function(a){"use strict";"function"==typeof define&&define.amd?define(["jquery"],a):"undefined"!=typeof exports?module.exports=a(require("jquery")):a(jQuery)}(function(a){"use strict";var b=window.Slick||{};b=function(){function c(c,d){var f,e=this;e.defaults={accessibility:!0,adaptiveHeight:!1,appendArrows:a(c),appendDots:a(c),arrows:!0,asNavFor:null,prevArrow:'<button type="button" data-role="none" class="slick-prev" aria-label="Previous" tabindex="0" role="button">Previous</button>',nextArrow:'<button type="button" data-role="none" class="slick-next" aria-label="Next" tabindex="0" role="button">Next</button>',autoplay:!1,autoplaySpeed:3e3,centerMode:!1,centerPadding:"50px",cssEase:"ease",customPaging:function(a,b){return'<button type="button" data-role="none" role="button" aria-required="false" tabindex="0">'+(b+1)+"</button>"},dots:!1,dotsClass:"slick-dots",draggable:!0,easing:"linear",edgeFriction:.35,fade:!1,focusOnSelect:!1,infinite:!0,initialSlide:0,lazyLoad:"ondemand",mobileFirst:!1,pauseOnHover:!0,pauseOnDotsHover:!1,respondTo:"window",responsive:null,rows:1,rtl:!1,slide:"",slidesPerRow:1,slidesToShow:1,slidesToScroll:1,speed:500,swipe:!0,swipeToSlide:!1,touchMove:!0,touchThreshold:5,useCSS:!0,useTransform:!1,variableWidth:!1,vertical:!1,verticalSwiping:!1,waitForAnimate:!0,zIndex:1e3},e.initials={animating:!1,dragging:!1,autoPlayTimer:null,currentDirection:0,currentLeft:null,currentSlide:0,direction:1,$dots:null,listWidth:null,listHeight:null,loadIndex:0,$nextArrow:null,$prevArrow:null,slideCount:null,slideWidth:null,$slideTrack:null,$slides:null,sliding:!1,slideOffset:0,swipeLeft:null,$list:null,touchObject:{},transformsEnabled:!1,unslicked:!1},a.extend(e,e.initials),e.activeBreakpoint=null,e.animType=null,e.animProp=null,e.breakpoints=[],e.breakpointSettings=[],e.cssTransitions=!1,e.hidden="hidden",e.paused=!1,e.positionProp=null,e.respondTo=null,e.rowCount=1,e.shouldClick=!0,e.$slider=a(c),e.$slidesCache=null,e.transformType=null,e.transitionType=null,e.visibilityChange="visibilitychange",e.windowWidth=0,e.windowTimer=null,f=a(c).data("slick")||{},e.options=a.extend({},e.defaults,f,d),e.currentSlide=e.options.initialSlide,e.originalSettings=e.options,"undefined"!=typeof document.mozHidden?(e.hidden="mozHidden",e.visibilityChange="mozvisibilitychange"):"undefined"!=typeof document.webkitHidden&&(e.hidden="webkitHidden",e.visibilityChange="webkitvisibilitychange"),e.autoPlay=a.proxy(e.autoPlay,e),e.autoPlayClear=a.proxy(e.autoPlayClear,e),e.changeSlide=a.proxy(e.changeSlide,e),e.clickHandler=a.proxy(e.clickHandler,e),e.selectHandler=a.proxy(e.selectHandler,e),e.setPosition=a.proxy(e.setPosition,e),e.swipeHandler=a.proxy(e.swipeHandler,e),e.dragHandler=a.proxy(e.dragHandler,e),e.keyHandler=a.proxy(e.keyHandler,e),e.autoPlayIterator=a.proxy(e.autoPlayIterator,e),e.instanceUid=b++,e.htmlExpr=/^(?:\s*(<[\w\W]+>)[^>]*)$/,e.registerBreakpoints(),e.init(!0),e.checkResponsive(!0)}var b=0;return c}(),b.prototype.addSlide=b.prototype.slickAdd=function(b,c,d){var e=this;if("boolean"==typeof c)d=c,c=null;else if(0>c||c>=e.slideCount)return!1;e.unload(),"number"==typeof c?0===c&&0===e.$slides.length?a(b).appendTo(e.$slideTrack):d?a(b).insertBefore(e.$slides.eq(c)):a(b).insertAfter(e.$slides.eq(c)):d===!0?a(b).prependTo(e.$slideTrack):a(b).appendTo(e.$slideTrack),e.$slides=e.$slideTrack.children(this.options.slide),e.$slideTrack.children(this.options.slide).detach(),e.$slideTrack.append(e.$slides),e.$slides.each(function(b,c){a(c).attr("data-slick-index",b)}),e.$slidesCache=e.$slides,e.reinit()},b.prototype.animateHeight=function(){var a=this;if(1===a.options.slidesToShow&&a.options.adaptiveHeight===!0&&a.options.vertical===!1){var b=a.$slides.eq(a.currentSlide).outerHeight(!0);a.$list.animate({height:b},a.options.speed)}},b.prototype.animateSlide=function(b,c){var d={},e=this;e.animateHeight(),e.options.rtl===!0&&e.options.vertical===!1&&(b=-b),e.transformsEnabled===!1?e.options.vertical===!1?e.$slideTrack.animate({left:b},e.options.speed,e.options.easing,c):e.$slideTrack.animate({top:b},e.options.speed,e.options.easing,c):e.cssTransitions===!1?(e.options.rtl===!0&&(e.currentLeft=-e.currentLeft),a({animStart:e.currentLeft}).animate({animStart:b},{duration:e.options.speed,easing:e.options.easing,step:function(a){a=Math.ceil(a),e.options.vertical===!1?(d[e.animType]="translate("+a+"px, 0px)",e.$slideTrack.css(d)):(d[e.animType]="translate(0px,"+a+"px)",e.$slideTrack.css(d))},complete:function(){c&&c.call()}})):(e.applyTransition(),b=Math.ceil(b),e.options.vertical===!1?d[e.animType]="translate3d("+b+"px, 0px, 0px)":d[e.animType]="translate3d(0px,"+b+"px, 0px)",e.$slideTrack.css(d),c&&setTimeout(function(){e.disableTransition(),c.call()},e.options.speed))},b.prototype.asNavFor=function(b){var c=this,d=c.options.asNavFor;d&&null!==d&&(d=a(d).not(c.$slider)),null!==d&&"object"==typeof d&&d.each(function(){var c=a(this).slick("getSlick");c.unslicked||c.slideHandler(b,!0)})},b.prototype.applyTransition=function(a){var b=this,c={};b.options.fade===!1?c[b.transitionType]=b.transformType+" "+b.options.speed+"ms "+b.options.cssEase:c[b.transitionType]="opacity "+b.options.speed+"ms "+b.options.cssEase,b.options.fade===!1?b.$slideTrack.css(c):b.$slides.eq(a).css(c)},b.prototype.autoPlay=function(){var a=this;a.autoPlayTimer&&clearInterval(a.autoPlayTimer),a.slideCount>a.options.slidesToShow&&a.paused!==!0&&(a.autoPlayTimer=setInterval(a.autoPlayIterator,a.options.autoplaySpeed))},b.prototype.autoPlayClear=function(){var a=this;a.autoPlayTimer&&clearInterval(a.autoPlayTimer)},b.prototype.autoPlayIterator=function(){var a=this;a.options.infinite===!1?1===a.direction?(a.currentSlide+1===a.slideCount-1&&(a.direction=0),a.slideHandler(a.currentSlide+a.options.slidesToScroll)):(a.currentSlide-1===0&&(a.direction=1),a.slideHandler(a.currentSlide-a.options.slidesToScroll)):a.slideHandler(a.currentSlide+a.options.slidesToScroll)},b.prototype.buildArrows=function(){var b=this;b.options.arrows===!0&&(b.$prevArrow=a(b.options.prevArrow).addClass("slick-arrow"),b.$nextArrow=a(b.options.nextArrow).addClass("slick-arrow"),b.slideCount>b.options.slidesToShow?(b.$prevArrow.removeClass("slick-hidden").removeAttr("aria-hidden tabindex"),b.$nextArrow.removeClass("slick-hidden").removeAttr("aria-hidden tabindex"),b.htmlExpr.test(b.options.prevArrow)&&b.$prevArrow.prependTo(b.options.appendArrows),b.htmlExpr.test(b.options.nextArrow)&&b.$nextArrow.appendTo(b.options.appendArrows),b.options.infinite!==!0&&b.$prevArrow.addClass("slick-disabled").attr("aria-disabled","true")):b.$prevArrow.add(b.$nextArrow).addClass("slick-hidden").attr({"aria-disabled":"true",tabindex:"-1"}))},b.prototype.buildDots=function(){var c,d,b=this;if(b.options.dots===!0&&b.slideCount>b.options.slidesToShow){for(d='<ul class="'+b.options.dotsClass+'">',c=0;c<=b.getDotCount();c+=1)d+="<li>"+b.options.customPaging.call(this,b,c)+"</li>";d+="</ul>",b.$dots=a(d).appendTo(b.options.appendDots),b.$dots.find("li").first().addClass("slick-active").attr("aria-hidden","false")}},b.prototype.buildOut=function(){var b=this;b.$slides=b.$slider.children(b.options.slide+":not(.slick-cloned)").addClass("slick-slide"),b.slideCount=b.$slides.length,b.$slides.each(function(b,c){a(c).attr("data-slick-index",b).data("originalStyling",a(c).attr("style")||"")}),b.$slider.addClass("slick-slider"),b.$slideTrack=0===b.slideCount?a('<div class="slick-track"/>').appendTo(b.$slider):b.$slides.wrapAll('<div class="slick-track"/>').parent(),b.$list=b.$slideTrack.wrap('<div aria-live="polite" class="slick-list"/>').parent(),b.$slideTrack.css("opacity",0),(b.options.centerMode===!0||b.options.swipeToSlide===!0)&&(b.options.slidesToScroll=1),a("img[data-lazy]",b.$slider).not("[src]").addClass("slick-loading"),b.setupInfinite(),b.buildArrows(),b.buildDots(),b.updateDots(),b.setSlideClasses("number"==typeof b.currentSlide?b.currentSlide:0),b.options.draggable===!0&&b.$list.addClass("draggable")},b.prototype.buildRows=function(){var b,c,d,e,f,g,h,a=this;if(e=document.createDocumentFragment(),g=a.$slider.children(),a.options.rows>1){for(h=a.options.slidesPerRow*a.options.rows,f=Math.ceil(g.length/h),b=0;f>b;b++){var i=document.createElement("div");for(c=0;c<a.options.rows;c++){var j=document.createElement("div");for(d=0;d<a.options.slidesPerRow;d++){var k=b*h+(c*a.options.slidesPerRow+d);g.get(k)&&j.appendChild(g.get(k))}i.appendChild(j)}e.appendChild(i)}a.$slider.html(e),a.$slider.children().children().children().css({width:100/a.options.slidesPerRow+"%",display:"inline-block"})}},b.prototype.checkResponsive=function(b,c){var e,f,g,d=this,h=!1,i=d.$slider.width(),j=window.innerWidth||a(window).width();if("window"===d.respondTo?g=j:"slider"===d.respondTo?g=i:"min"===d.respondTo&&(g=Math.min(j,i)),d.options.responsive&&d.options.responsive.length&&null!==d.options.responsive){f=null;for(e in d.breakpoints)d.breakpoints.hasOwnProperty(e)&&(d.originalSettings.mobileFirst===!1?g<d.breakpoints[e]&&(f=d.breakpoints[e]):g>d.breakpoints[e]&&(f=d.breakpoints[e]));null!==f?null!==d.activeBreakpoint?(f!==d.activeBreakpoint||c)&&(d.activeBreakpoint=f,"unslick"===d.breakpointSettings[f]?d.unslick(f):(d.options=a.extend({},d.originalSettings,d.breakpointSettings[f]),b===!0&&(d.currentSlide=d.options.initialSlide),d.refresh(b)),h=f):(d.activeBreakpoint=f,"unslick"===d.breakpointSettings[f]?d.unslick(f):(d.options=a.extend({},d.originalSettings,d.breakpointSettings[f]),b===!0&&(d.currentSlide=d.options.initialSlide),d.refresh(b)),h=f):null!==d.activeBreakpoint&&(d.activeBreakpoint=null,d.options=d.originalSettings,b===!0&&(d.currentSlide=d.options.initialSlide),d.refresh(b),h=f),b||h===!1||d.$slider.trigger("breakpoint",[d,h])}},b.prototype.changeSlide=function(b,c){var f,g,h,d=this,e=a(b.target);switch(e.is("a")&&b.preventDefault(),e.is("li")||(e=e.closest("li")),h=d.slideCount%d.options.slidesToScroll!==0,f=h?0:(d.slideCount-d.currentSlide)%d.options.slidesToScroll,b.data.message){case"previous":g=0===f?d.options.slidesToScroll:d.options.slidesToShow-f,d.slideCount>d.options.slidesToShow&&d.slideHandler(d.currentSlide-g,!1,c);break;case"next":g=0===f?d.options.slidesToScroll:f,d.slideCount>d.options.slidesToShow&&d.slideHandler(d.currentSlide+g,!1,c);break;case"index":var i=0===b.data.index?0:b.data.index||e.index()*d.options.slidesToScroll;d.slideHandler(d.checkNavigable(i),!1,c),e.children().trigger("focus");break;default:return}},b.prototype.checkNavigable=function(a){var c,d,b=this;if(c=b.getNavigableIndexes(),d=0,a>c[c.length-1])a=c[c.length-1];else for(var e in c){if(a<c[e]){a=d;break}d=c[e]}return a},b.prototype.cleanUpEvents=function(){var b=this;b.options.dots&&null!==b.$dots&&(a("li",b.$dots).off("click.slick",b.changeSlide),b.options.pauseOnDotsHover===!0&&b.options.autoplay===!0&&a("li",b.$dots).off("mouseenter.slick",a.proxy(b.setPaused,b,!0)).off("mouseleave.slick",a.proxy(b.setPaused,b,!1))),b.options.arrows===!0&&b.slideCount>b.options.slidesToShow&&(b.$prevArrow&&b.$prevArrow.off("click.slick",b.changeSlide),b.$nextArrow&&b.$nextArrow.off("click.slick",b.changeSlide)),b.$list.off("touchstart.slick mousedown.slick",b.swipeHandler),b.$list.off("touchmove.slick mousemove.slick",b.swipeHandler),b.$list.off("touchend.slick mouseup.slick",b.swipeHandler),b.$list.off("touchcancel.slick mouseleave.slick",b.swipeHandler),b.$list.off("click.slick",b.clickHandler),a(document).off(b.visibilityChange,b.visibility),b.$list.off("mouseenter.slick",a.proxy(b.setPaused,b,!0)),b.$list.off("mouseleave.slick",a.proxy(b.setPaused,b,!1)),b.options.accessibility===!0&&b.$list.off("keydown.slick",b.keyHandler),b.options.focusOnSelect===!0&&a(b.$slideTrack).children().off("click.slick",b.selectHandler),a(window).off("orientationchange.slick.slick-"+b.instanceUid,b.orientationChange),a(window).off("resize.slick.slick-"+b.instanceUid,b.resize),a("[draggable!=true]",b.$slideTrack).off("dragstart",b.preventDefault),a(window).off("load.slick.slick-"+b.instanceUid,b.setPosition),a(document).off("ready.slick.slick-"+b.instanceUid,b.setPosition)},b.prototype.cleanUpRows=function(){var b,a=this;a.options.rows>1&&(b=a.$slides.children().children(),b.removeAttr("style"),a.$slider.html(b))},b.prototype.clickHandler=function(a){var b=this;b.shouldClick===!1&&(a.stopImmediatePropagation(),a.stopPropagation(),a.preventDefault())},b.prototype.destroy=function(b){var c=this;c.autoPlayClear(),c.touchObject={},c.cleanUpEvents(),a(".slick-cloned",c.$slider).detach(),c.$dots&&c.$dots.remove(),c.$prevArrow&&c.$prevArrow.length&&(c.$prevArrow.removeClass("slick-disabled slick-arrow slick-hidden").removeAttr("aria-hidden aria-disabled tabindex").css("display",""),c.htmlExpr.test(c.options.prevArrow)&&c.$prevArrow.remove()),c.$nextArrow&&c.$nextArrow.length&&(c.$nextArrow.removeClass("slick-disabled slick-arrow slick-hidden").removeAttr("aria-hidden aria-disabled tabindex").css("display",""),c.htmlExpr.test(c.options.nextArrow)&&c.$nextArrow.remove()),c.$slides&&(c.$slides.removeClass("slick-slide slick-active slick-center slick-visible slick-current").removeAttr("aria-hidden").removeAttr("data-slick-index").each(function(){a(this).attr("style",a(this).data("originalStyling"))}),c.$slideTrack.children(this.options.slide).detach(),c.$slideTrack.detach(),c.$list.detach(),c.$slider.append(c.$slides)),c.cleanUpRows(),c.$slider.removeClass("slick-slider"),c.$slider.removeClass("slick-initialized"),c.unslicked=!0,b||c.$slider.trigger("destroy",[c])},b.prototype.disableTransition=function(a){var b=this,c={};c[b.transitionType]="",b.options.fade===!1?b.$slideTrack.css(c):b.$slides.eq(a).css(c)},b.prototype.fadeSlide=function(a,b){var c=this;c.cssTransitions===!1?(c.$slides.eq(a).css({zIndex:c.options.zIndex}),c.$slides.eq(a).animate({opacity:1},c.options.speed,c.options.easing,b)):(c.applyTransition(a),c.$slides.eq(a).css({opacity:1,zIndex:c.options.zIndex}),b&&setTimeout(function(){c.disableTransition(a),b.call()},c.options.speed))},b.prototype.fadeSlideOut=function(a){var b=this;b.cssTransitions===!1?b.$slides.eq(a).animate({opacity:0,zIndex:b.options.zIndex-2},b.options.speed,b.options.easing):(b.applyTransition(a),b.$slides.eq(a).css({opacity:0,zIndex:b.options.zIndex-2}))},b.prototype.filterSlides=b.prototype.slickFilter=function(a){var b=this;null!==a&&(b.$slidesCache=b.$slides,b.unload(),b.$slideTrack.children(this.options.slide).detach(),b.$slidesCache.filter(a).appendTo(b.$slideTrack),b.reinit())},b.prototype.getCurrent=b.prototype.slickCurrentSlide=function(){var a=this;return a.currentSlide},b.prototype.getDotCount=function(){var a=this,b=0,c=0,d=0;if(a.options.infinite===!0)for(;b<a.slideCount;)++d,b=c+a.options.slidesToScroll,c+=a.options.slidesToScroll<=a.options.slidesToShow?a.options.slidesToScroll:a.options.slidesToShow;else if(a.options.centerMode===!0)d=a.slideCount;else for(;b<a.slideCount;)++d,b=c+a.options.slidesToScroll,c+=a.options.slidesToScroll<=a.options.slidesToShow?a.options.slidesToScroll:a.options.slidesToShow;return d-1},b.prototype.getLeft=function(a){var c,d,f,b=this,e=0;return b.slideOffset=0,d=b.$slides.first().outerHeight(!0),b.options.infinite===!0?(b.slideCount>b.options.slidesToShow&&(b.slideOffset=b.slideWidth*b.options.slidesToShow*-1,e=d*b.options.slidesToShow*-1),b.slideCount%b.options.slidesToScroll!==0&&a+b.options.slidesToScroll>b.slideCount&&b.slideCount>b.options.slidesToShow&&(a>b.slideCount?(b.slideOffset=(b.options.slidesToShow-(a-b.slideCount))*b.slideWidth*-1,e=(b.options.slidesToShow-(a-b.slideCount))*d*-1):(b.slideOffset=b.slideCount%b.options.slidesToScroll*b.slideWidth*-1,e=b.slideCount%b.options.slidesToScroll*d*-1))):a+b.options.slidesToShow>b.slideCount&&(b.slideOffset=(a+b.options.slidesToShow-b.slideCount)*b.slideWidth,e=(a+b.options.slidesToShow-b.slideCount)*d),b.slideCount<=b.options.slidesToShow&&(b.slideOffset=0,e=0),b.options.centerMode===!0&&b.options.infinite===!0?b.slideOffset+=b.slideWidth*Math.floor(b.options.slidesToShow/2)-b.slideWidth:b.options.centerMode===!0&&(b.slideOffset=0,b.slideOffset+=b.slideWidth*Math.floor(b.options.slidesToShow/2)),c=b.options.vertical===!1?a*b.slideWidth*-1+b.slideOffset:a*d*-1+e,b.options.variableWidth===!0&&(f=b.slideCount<=b.options.slidesToShow||b.options.infinite===!1?b.$slideTrack.children(".slick-slide").eq(a):b.$slideTrack.children(".slick-slide").eq(a+b.options.slidesToShow),c=b.options.rtl===!0?f[0]?-1*(b.$slideTrack.width()-f[0].offsetLeft-f.width()):0:f[0]?-1*f[0].offsetLeft:0,b.options.centerMode===!0&&(f=b.slideCount<=b.options.slidesToShow||b.options.infinite===!1?b.$slideTrack.children(".slick-slide").eq(a):b.$slideTrack.children(".slick-slide").eq(a+b.options.slidesToShow+1),c=b.options.rtl===!0?f[0]?-1*(b.$slideTrack.width()-f[0].offsetLeft-f.width()):0:f[0]?-1*f[0].offsetLeft:0,c+=(b.$list.width()-f.outerWidth())/2)),c},b.prototype.getOption=b.prototype.slickGetOption=function(a){var b=this;return b.options[a]},b.prototype.getNavigableIndexes=function(){var e,a=this,b=0,c=0,d=[];for(a.options.infinite===!1?e=a.slideCount:(b=-1*a.options.slidesToScroll,c=-1*a.options.slidesToScroll,e=2*a.slideCount);e>b;)d.push(b),b=c+a.options.slidesToScroll,c+=a.options.slidesToScroll<=a.options.slidesToShow?a.options.slidesToScroll:a.options.slidesToShow;return d},b.prototype.getSlick=function(){return this},b.prototype.getSlideCount=function(){var c,d,e,b=this;return e=b.options.centerMode===!0?b.slideWidth*Math.floor(b.options.slidesToShow/2):0,b.options.swipeToSlide===!0?(b.$slideTrack.find(".slick-slide").each(function(c,f){return f.offsetLeft-e+a(f).outerWidth()/2>-1*b.swipeLeft?(d=f,!1):void 0}),c=Math.abs(a(d).attr("data-slick-index")-b.currentSlide)||1):b.options.slidesToScroll},b.prototype.goTo=b.prototype.slickGoTo=function(a,b){var c=this;c.changeSlide({data:{message:"index",index:parseInt(a)}},b)},b.prototype.init=function(b){var c=this;a(c.$slider).hasClass("slick-initialized")||(a(c.$slider).addClass("slick-initialized"),c.buildRows(),c.buildOut(),c.setProps(),c.startLoad(),c.loadSlider(),c.initializeEvents(),c.updateArrows(),c.updateDots()),b&&c.$slider.trigger("init",[c]),c.options.accessibility===!0&&c.initADA()},b.prototype.initArrowEvents=function(){var a=this;a.options.arrows===!0&&a.slideCount>a.options.slidesToShow&&(a.$prevArrow.on("click.slick",{message:"previous"},a.changeSlide),a.$nextArrow.on("click.slick",{message:"next"},a.changeSlide))},b.prototype.initDotEvents=function(){var b=this;b.options.dots===!0&&b.slideCount>b.options.slidesToShow&&a("li",b.$dots).on("click.slick",{message:"index"},b.changeSlide),b.options.dots===!0&&b.options.pauseOnDotsHover===!0&&b.options.autoplay===!0&&a("li",b.$dots).on("mouseenter.slick",a.proxy(b.setPaused,b,!0)).on("mouseleave.slick",a.proxy(b.setPaused,b,!1))},b.prototype.initializeEvents=function(){var b=this;b.initArrowEvents(),b.initDotEvents(),b.$list.on("touchstart.slick mousedown.slick",{action:"start"},b.swipeHandler),b.$list.on("touchmove.slick mousemove.slick",{action:"move"},b.swipeHandler),b.$list.on("touchend.slick mouseup.slick",{action:"end"},b.swipeHandler),b.$list.on("touchcancel.slick mouseleave.slick",{action:"end"},b.swipeHandler),b.$list.on("click.slick",b.clickHandler),a(document).on(b.visibilityChange,a.proxy(b.visibility,b)),b.$list.on("mouseenter.slick",a.proxy(b.setPaused,b,!0)),b.$list.on("mouseleave.slick",a.proxy(b.setPaused,b,!1)),b.options.accessibility===!0&&b.$list.on("keydown.slick",b.keyHandler),b.options.focusOnSelect===!0&&a(b.$slideTrack).children().on("click.slick",b.selectHandler),a(window).on("orientationchange.slick.slick-"+b.instanceUid,a.proxy(b.orientationChange,b)),a(window).on("resize.slick.slick-"+b.instanceUid,a.proxy(b.resize,b)),a("[draggable!=true]",b.$slideTrack).on("dragstart",b.preventDefault),a(window).on("load.slick.slick-"+b.instanceUid,b.setPosition),a(document).on("ready.slick.slick-"+b.instanceUid,b.setPosition)},b.prototype.initUI=function(){var a=this;a.options.arrows===!0&&a.slideCount>a.options.slidesToShow&&(a.$prevArrow.show(),a.$nextArrow.show()),a.options.dots===!0&&a.slideCount>a.options.slidesToShow&&a.$dots.show(),a.options.autoplay===!0&&a.autoPlay()},b.prototype.keyHandler=function(a){var b=this;a.target.tagName.match("TEXTAREA|INPUT|SELECT")||(37===a.keyCode&&b.options.accessibility===!0?b.changeSlide({data:{message:"previous"}}):39===a.keyCode&&b.options.accessibility===!0&&b.changeSlide({data:{message:"next"}}))},b.prototype.lazyLoad=function(){function g(b){a("img[data-lazy]",b).each(function(){var b=a(this),c=a(this).attr("data-lazy"),d=document.createElement("img");d.onload=function(){b.animate({opacity:0},100,function(){b.attr("src",c).animate({opacity:1},200,function(){b.removeAttr("data-lazy").removeClass("slick-loading")})})},d.src=c})}var c,d,e,f,b=this;b.options.centerMode===!0?b.options.infinite===!0?(e=b.currentSlide+(b.options.slidesToShow/2+1),f=e+b.options.slidesToShow+2):(e=Math.max(0,b.currentSlide-(b.options.slidesToShow/2+1)),f=2+(b.options.slidesToShow/2+1)+b.currentSlide):(e=b.options.infinite?b.options.slidesToShow+b.currentSlide:b.currentSlide,f=e+b.options.slidesToShow,b.options.fade===!0&&(e>0&&e--,f<=b.slideCount&&f++)),c=b.$slider.find(".slick-slide").slice(e,f),g(c),b.slideCount<=b.options.slidesToShow?(d=b.$slider.find(".slick-slide"),g(d)):b.currentSlide>=b.slideCount-b.options.slidesToShow?(d=b.$slider.find(".slick-cloned").slice(0,b.options.slidesToShow),g(d)):0===b.currentSlide&&(d=b.$slider.find(".slick-cloned").slice(-1*b.options.slidesToShow),g(d))},b.prototype.loadSlider=function(){var a=this;a.setPosition(),a.$slideTrack.css({opacity:1}),a.$slider.removeClass("slick-loading"),a.initUI(),"progressive"===a.options.lazyLoad&&a.progressiveLazyLoad()},b.prototype.next=b.prototype.slickNext=function(){var a=this;a.changeSlide({data:{message:"next"}})},b.prototype.orientationChange=function(){var a=this;a.checkResponsive(),a.setPosition()},b.prototype.pause=b.prototype.slickPause=function(){var a=this;a.autoPlayClear(),a.paused=!0},b.prototype.play=b.prototype.slickPlay=function(){var a=this;a.paused=!1,a.autoPlay()},b.prototype.postSlide=function(a){var b=this;b.$slider.trigger("afterChange",[b,a]),b.animating=!1,b.setPosition(),b.swipeLeft=null,b.options.autoplay===!0&&b.paused===!1&&b.autoPlay(),b.options.accessibility===!0&&b.initADA()},b.prototype.prev=b.prototype.slickPrev=function(){var a=this;a.changeSlide({data:{message:"previous"}})},b.prototype.preventDefault=function(a){a.preventDefault()},b.prototype.progressiveLazyLoad=function(){var c,d,b=this;c=a("img[data-lazy]",b.$slider).length,c>0&&(d=a("img[data-lazy]",b.$slider).first(),d.attr("src",null),d.attr("src",d.attr("data-lazy")).removeClass("slick-loading").load(function(){d.removeAttr("data-lazy"),b.progressiveLazyLoad(),b.options.adaptiveHeight===!0&&b.setPosition()}).error(function(){d.removeAttr("data-lazy"),b.progressiveLazyLoad()}))},b.prototype.refresh=function(b){var d,e,c=this;e=c.slideCount-c.options.slidesToShow,c.options.infinite||(c.slideCount<=c.options.slidesToShow?c.currentSlide=0:c.currentSlide>e&&(c.currentSlide=e)),d=c.currentSlide,c.destroy(!0),a.extend(c,c.initials,{currentSlide:d}),c.init(),b||c.changeSlide({data:{message:"index",index:d}},!1)},b.prototype.registerBreakpoints=function(){var c,d,e,b=this,f=b.options.responsive||null;if("array"===a.type(f)&&f.length){b.respondTo=b.options.respondTo||"window";for(c in f)if(e=b.breakpoints.length-1,d=f[c].breakpoint,f.hasOwnProperty(c)){for(;e>=0;)b.breakpoints[e]&&b.breakpoints[e]===d&&b.breakpoints.splice(e,1),e--;b.breakpoints.push(d),b.breakpointSettings[d]=f[c].settings}b.breakpoints.sort(function(a,c){return b.options.mobileFirst?a-c:c-a})}},b.prototype.reinit=function(){var b=this;b.$slides=b.$slideTrack.children(b.options.slide).addClass("slick-slide"),b.slideCount=b.$slides.length,b.currentSlide>=b.slideCount&&0!==b.currentSlide&&(b.currentSlide=b.currentSlide-b.options.slidesToScroll),b.slideCount<=b.options.slidesToShow&&(b.currentSlide=0),b.registerBreakpoints(),b.setProps(),b.setupInfinite(),b.buildArrows(),b.updateArrows(),b.initArrowEvents(),b.buildDots(),b.updateDots(),b.initDotEvents(),b.checkResponsive(!1,!0),b.options.focusOnSelect===!0&&a(b.$slideTrack).children().on("click.slick",b.selectHandler),b.setSlideClasses(0),b.setPosition(),b.$slider.trigger("reInit",[b]),b.options.autoplay===!0&&b.focusHandler()},b.prototype.resize=function(){var b=this;a(window).width()!==b.windowWidth&&(clearTimeout(b.windowDelay),b.windowDelay=window.setTimeout(function(){b.windowWidth=a(window).width(),b.checkResponsive(),b.unslicked||b.setPosition()},50))},b.prototype.removeSlide=b.prototype.slickRemove=function(a,b,c){var d=this;return"boolean"==typeof a?(b=a,a=b===!0?0:d.slideCount-1):a=b===!0?--a:a,d.slideCount<1||0>a||a>d.slideCount-1?!1:(d.unload(),c===!0?d.$slideTrack.children().remove():d.$slideTrack.children(this.options.slide).eq(a).remove(),d.$slides=d.$slideTrack.children(this.options.slide),d.$slideTrack.children(this.options.slide).detach(),d.$slideTrack.append(d.$slides),d.$slidesCache=d.$slides,void d.reinit())},b.prototype.setCSS=function(a){var d,e,b=this,c={};b.options.rtl===!0&&(a=-a),d="left"==b.positionProp?Math.ceil(a)+"px":"0px",e="top"==b.positionProp?Math.ceil(a)+"px":"0px",c[b.positionProp]=a,b.transformsEnabled===!1?b.$slideTrack.css(c):(c={},b.cssTransitions===!1?(c[b.animType]="translate("+d+", "+e+")",b.$slideTrack.css(c)):(c[b.animType]="translate3d("+d+", "+e+", 0px)",b.$slideTrack.css(c)))},b.prototype.setDimensions=function(){var a=this;a.options.vertical===!1?a.options.centerMode===!0&&a.$list.css({padding:"0px "+a.options.centerPadding}):(a.$list.height(a.$slides.first().outerHeight(!0)*a.options.slidesToShow),a.options.centerMode===!0&&a.$list.css({padding:a.options.centerPadding+" 0px"})),a.listWidth=a.$list.width(),a.listHeight=a.$list.height(),a.options.vertical===!1&&a.options.variableWidth===!1?(a.slideWidth=Math.ceil(a.listWidth/a.options.slidesToShow),a.$slideTrack.width(Math.ceil(a.slideWidth*a.$slideTrack.children(".slick-slide").length))):a.options.variableWidth===!0?a.$slideTrack.width(5e3*a.slideCount):(a.slideWidth=Math.ceil(a.listWidth),a.$slideTrack.height(Math.ceil(a.$slides.first().outerHeight(!0)*a.$slideTrack.children(".slick-slide").length)));var b=a.$slides.first().outerWidth(!0)-a.$slides.first().width();a.options.variableWidth===!1&&a.$slideTrack.children(".slick-slide").width(a.slideWidth-b)},b.prototype.setFade=function(){var c,b=this;b.$slides.each(function(d,e){c=b.slideWidth*d*-1,b.options.rtl===!0?a(e).css({position:"relative",right:c,top:0,zIndex:b.options.zIndex-2,opacity:0}):a(e).css({position:"relative",left:c,top:0,zIndex:b.options.zIndex-2,opacity:0})}),b.$slides.eq(b.currentSlide).css({zIndex:b.options.zIndex-1,opacity:1})},b.prototype.setHeight=function(){var a=this;if(1===a.options.slidesToShow&&a.options.adaptiveHeight===!0&&a.options.vertical===!1){var b=a.$slides.eq(a.currentSlide).outerHeight(!0);a.$list.css("height",b)}},b.prototype.setOption=b.prototype.slickSetOption=function(b,c,d){var f,g,e=this;if("responsive"===b&&"array"===a.type(c))for(g in c)if("array"!==a.type(e.options.responsive))e.options.responsive=[c[g]];else{for(f=e.options.responsive.length-1;f>=0;)e.options.responsive[f].breakpoint===c[g].breakpoint&&e.options.responsive.splice(f,1),f--;e.options.responsive.push(c[g])}else e.options[b]=c;d===!0&&(e.unload(),e.reinit())},b.prototype.setPosition=function(){var a=this;a.setDimensions(),a.setHeight(),a.options.fade===!1?a.setCSS(a.getLeft(a.currentSlide)):a.setFade(),a.$slider.trigger("setPosition",[a])},b.prototype.setProps=function(){var a=this,b=document.body.style;a.positionProp=a.options.vertical===!0?"top":"left","top"===a.positionProp?a.$slider.addClass("slick-vertical"):a.$slider.removeClass("slick-vertical"),(void 0!==b.WebkitTransition||void 0!==b.MozTransition||void 0!==b.msTransition)&&a.options.useCSS===!0&&(a.cssTransitions=!0),a.options.fade&&("number"==typeof a.options.zIndex?a.options.zIndex<3&&(a.options.zIndex=3):a.options.zIndex=a.defaults.zIndex),void 0!==b.OTransform&&(a.animType="OTransform",a.transformType="-o-transform",a.transitionType="OTransition",void 0===b.perspectiveProperty&&void 0===b.webkitPerspective&&(a.animType=!1)),void 0!==b.MozTransform&&(a.animType="MozTransform",a.transformType="-moz-transform",a.transitionType="MozTransition",void 0===b.perspectiveProperty&&void 0===b.MozPerspective&&(a.animType=!1)),void 0!==b.webkitTransform&&(a.animType="webkitTransform",a.transformType="-webkit-transform",a.transitionType="webkitTransition",void 0===b.perspectiveProperty&&void 0===b.webkitPerspective&&(a.animType=!1)),void 0!==b.msTransform&&(a.animType="msTransform",a.transformType="-ms-transform",a.transitionType="msTransition",void 0===b.msTransform&&(a.animType=!1)),void 0!==b.transform&&a.animType!==!1&&(a.animType="transform",a.transformType="transform",a.transitionType="transition"),a.transformsEnabled=a.options.useTransform&&null!==a.animType&&a.animType!==!1},b.prototype.setSlideClasses=function(a){var c,d,e,f,b=this;d=b.$slider.find(".slick-slide").removeClass("slick-active slick-center slick-current").attr("aria-hidden","true"),b.$slides.eq(a).addClass("slick-current"),b.options.centerMode===!0?(c=Math.floor(b.options.slidesToShow/2),b.options.infinite===!0&&(a>=c&&a<=b.slideCount-1-c?b.$slides.slice(a-c,a+c+1).addClass("slick-active").attr("aria-hidden","false"):(e=b.options.slidesToShow+a,d.slice(e-c+1,e+c+2).addClass("slick-active").attr("aria-hidden","false")),0===a?d.eq(d.length-1-b.options.slidesToShow).addClass("slick-center"):a===b.slideCount-1&&d.eq(b.options.slidesToShow).addClass("slick-center")),b.$slides.eq(a).addClass("slick-center")):a>=0&&a<=b.slideCount-b.options.slidesToShow?b.$slides.slice(a,a+b.options.slidesToShow).addClass("slick-active").attr("aria-hidden","false"):d.length<=b.options.slidesToShow?d.addClass("slick-active").attr("aria-hidden","false"):(f=b.slideCount%b.options.slidesToShow,e=b.options.infinite===!0?b.options.slidesToShow+a:a,b.options.slidesToShow==b.options.slidesToScroll&&b.slideCount-a<b.options.slidesToShow?d.slice(e-(b.options.slidesToShow-f),e+f).addClass("slick-active").attr("aria-hidden","false"):d.slice(e,e+b.options.slidesToShow).addClass("slick-active").attr("aria-hidden","false")),"ondemand"===b.options.lazyLoad&&b.lazyLoad()},b.prototype.setupInfinite=function(){var c,d,e,b=this;if(b.options.fade===!0&&(b.options.centerMode=!1),b.options.infinite===!0&&b.options.fade===!1&&(d=null,b.slideCount>b.options.slidesToShow)){for(e=b.options.centerMode===!0?b.options.slidesToShow+1:b.options.slidesToShow,c=b.slideCount;c>b.slideCount-e;c-=1)d=c-1,a(b.$slides[d]).clone(!0).attr("id","").attr("data-slick-index",d-b.slideCount).prependTo(b.$slideTrack).addClass("slick-cloned");for(c=0;e>c;c+=1)d=c,a(b.$slides[d]).clone(!0).attr("id","").attr("data-slick-index",d+b.slideCount).appendTo(b.$slideTrack).addClass("slick-cloned");b.$slideTrack.find(".slick-cloned").find("[id]").each(function(){a(this).attr("id","")})}},b.prototype.setPaused=function(a){var b=this;b.options.autoplay===!0&&b.options.pauseOnHover===!0&&(b.paused=a,a?b.autoPlayClear():b.autoPlay())},b.prototype.selectHandler=function(b){var c=this,d=a(b.target).is(".slick-slide")?a(b.target):a(b.target).parents(".slick-slide"),e=parseInt(d.attr("data-slick-index"));return e||(e=0),c.slideCount<=c.options.slidesToShow?(c.setSlideClasses(e),void c.asNavFor(e)):void c.slideHandler(e)},b.prototype.slideHandler=function(a,b,c){var d,e,f,g,h=null,i=this;return b=b||!1,i.animating===!0&&i.options.waitForAnimate===!0||i.options.fade===!0&&i.currentSlide===a||i.slideCount<=i.options.slidesToShow?void 0:(b===!1&&i.asNavFor(a),d=a,h=i.getLeft(d),g=i.getLeft(i.currentSlide),i.currentLeft=null===i.swipeLeft?g:i.swipeLeft,i.options.infinite===!1&&i.options.centerMode===!1&&(0>a||a>i.getDotCount()*i.options.slidesToScroll)?void(i.options.fade===!1&&(d=i.currentSlide,c!==!0?i.animateSlide(g,function(){i.postSlide(d);
}):i.postSlide(d))):i.options.infinite===!1&&i.options.centerMode===!0&&(0>a||a>i.slideCount-i.options.slidesToScroll)?void(i.options.fade===!1&&(d=i.currentSlide,c!==!0?i.animateSlide(g,function(){i.postSlide(d)}):i.postSlide(d))):(i.options.autoplay===!0&&clearInterval(i.autoPlayTimer),e=0>d?i.slideCount%i.options.slidesToScroll!==0?i.slideCount-i.slideCount%i.options.slidesToScroll:i.slideCount+d:d>=i.slideCount?i.slideCount%i.options.slidesToScroll!==0?0:d-i.slideCount:d,i.animating=!0,i.$slider.trigger("beforeChange",[i,i.currentSlide,e]),f=i.currentSlide,i.currentSlide=e,i.setSlideClasses(i.currentSlide),i.updateDots(),i.updateArrows(),i.options.fade===!0?(c!==!0?(i.fadeSlideOut(f),i.fadeSlide(e,function(){i.postSlide(e)})):i.postSlide(e),void i.animateHeight()):void(c!==!0?i.animateSlide(h,function(){i.postSlide(e)}):i.postSlide(e))))},b.prototype.startLoad=function(){var a=this;a.options.arrows===!0&&a.slideCount>a.options.slidesToShow&&(a.$prevArrow.hide(),a.$nextArrow.hide()),a.options.dots===!0&&a.slideCount>a.options.slidesToShow&&a.$dots.hide(),a.$slider.addClass("slick-loading")},b.prototype.swipeDirection=function(){var a,b,c,d,e=this;return a=e.touchObject.startX-e.touchObject.curX,b=e.touchObject.startY-e.touchObject.curY,c=Math.atan2(b,a),d=Math.round(180*c/Math.PI),0>d&&(d=360-Math.abs(d)),45>=d&&d>=0?e.options.rtl===!1?"left":"right":360>=d&&d>=315?e.options.rtl===!1?"left":"right":d>=135&&225>=d?e.options.rtl===!1?"right":"left":e.options.verticalSwiping===!0?d>=35&&135>=d?"left":"right":"vertical"},b.prototype.swipeEnd=function(a){var c,b=this;if(b.dragging=!1,b.shouldClick=b.touchObject.swipeLength>10?!1:!0,void 0===b.touchObject.curX)return!1;if(b.touchObject.edgeHit===!0&&b.$slider.trigger("edge",[b,b.swipeDirection()]),b.touchObject.swipeLength>=b.touchObject.minSwipe)switch(b.swipeDirection()){case"left":c=b.options.swipeToSlide?b.checkNavigable(b.currentSlide+b.getSlideCount()):b.currentSlide+b.getSlideCount(),b.slideHandler(c),b.currentDirection=0,b.touchObject={},b.$slider.trigger("swipe",[b,"left"]);break;case"right":c=b.options.swipeToSlide?b.checkNavigable(b.currentSlide-b.getSlideCount()):b.currentSlide-b.getSlideCount(),b.slideHandler(c),b.currentDirection=1,b.touchObject={},b.$slider.trigger("swipe",[b,"right"])}else b.touchObject.startX!==b.touchObject.curX&&(b.slideHandler(b.currentSlide),b.touchObject={})},b.prototype.swipeHandler=function(a){var b=this;if(!(b.options.swipe===!1||"ontouchend"in document&&b.options.swipe===!1||b.options.draggable===!1&&-1!==a.type.indexOf("mouse")))switch(b.touchObject.fingerCount=a.originalEvent&&void 0!==a.originalEvent.touches?a.originalEvent.touches.length:1,b.touchObject.minSwipe=b.listWidth/b.options.touchThreshold,b.options.verticalSwiping===!0&&(b.touchObject.minSwipe=b.listHeight/b.options.touchThreshold),a.data.action){case"start":b.swipeStart(a);break;case"move":b.swipeMove(a);break;case"end":b.swipeEnd(a)}},b.prototype.swipeMove=function(a){var d,e,f,g,h,b=this;return h=void 0!==a.originalEvent?a.originalEvent.touches:null,!b.dragging||h&&1!==h.length?!1:(d=b.getLeft(b.currentSlide),b.touchObject.curX=void 0!==h?h[0].pageX:a.clientX,b.touchObject.curY=void 0!==h?h[0].pageY:a.clientY,b.touchObject.swipeLength=Math.round(Math.sqrt(Math.pow(b.touchObject.curX-b.touchObject.startX,2))),b.options.verticalSwiping===!0&&(b.touchObject.swipeLength=Math.round(Math.sqrt(Math.pow(b.touchObject.curY-b.touchObject.startY,2)))),e=b.swipeDirection(),"vertical"!==e?(void 0!==a.originalEvent&&b.touchObject.swipeLength>4&&a.preventDefault(),g=(b.options.rtl===!1?1:-1)*(b.touchObject.curX>b.touchObject.startX?1:-1),b.options.verticalSwiping===!0&&(g=b.touchObject.curY>b.touchObject.startY?1:-1),f=b.touchObject.swipeLength,b.touchObject.edgeHit=!1,b.options.infinite===!1&&(0===b.currentSlide&&"right"===e||b.currentSlide>=b.getDotCount()&&"left"===e)&&(f=b.touchObject.swipeLength*b.options.edgeFriction,b.touchObject.edgeHit=!0),b.options.vertical===!1?b.swipeLeft=d+f*g:b.swipeLeft=d+f*(b.$list.height()/b.listWidth)*g,b.options.verticalSwiping===!0&&(b.swipeLeft=d+f*g),b.options.fade===!0||b.options.touchMove===!1?!1:b.animating===!0?(b.swipeLeft=null,!1):void b.setCSS(b.swipeLeft)):void 0)},b.prototype.swipeStart=function(a){var c,b=this;return 1!==b.touchObject.fingerCount||b.slideCount<=b.options.slidesToShow?(b.touchObject={},!1):(void 0!==a.originalEvent&&void 0!==a.originalEvent.touches&&(c=a.originalEvent.touches[0]),b.touchObject.startX=b.touchObject.curX=void 0!==c?c.pageX:a.clientX,b.touchObject.startY=b.touchObject.curY=void 0!==c?c.pageY:a.clientY,void(b.dragging=!0))},b.prototype.unfilterSlides=b.prototype.slickUnfilter=function(){var a=this;null!==a.$slidesCache&&(a.unload(),a.$slideTrack.children(this.options.slide).detach(),a.$slidesCache.appendTo(a.$slideTrack),a.reinit())},b.prototype.unload=function(){var b=this;a(".slick-cloned",b.$slider).remove(),b.$dots&&b.$dots.remove(),b.$prevArrow&&b.htmlExpr.test(b.options.prevArrow)&&b.$prevArrow.remove(),b.$nextArrow&&b.htmlExpr.test(b.options.nextArrow)&&b.$nextArrow.remove(),b.$slides.removeClass("slick-slide slick-active slick-visible slick-current").attr("aria-hidden","true").css("width","")},b.prototype.unslick=function(a){var b=this;b.$slider.trigger("unslick",[b,a]),b.destroy()},b.prototype.updateArrows=function(){var b,a=this;b=Math.floor(a.options.slidesToShow/2),a.options.arrows===!0&&a.slideCount>a.options.slidesToShow&&!a.options.infinite&&(a.$prevArrow.removeClass("slick-disabled").attr("aria-disabled","false"),a.$nextArrow.removeClass("slick-disabled").attr("aria-disabled","false"),0===a.currentSlide?(a.$prevArrow.addClass("slick-disabled").attr("aria-disabled","true"),a.$nextArrow.removeClass("slick-disabled").attr("aria-disabled","false")):a.currentSlide>=a.slideCount-a.options.slidesToShow&&a.options.centerMode===!1?(a.$nextArrow.addClass("slick-disabled").attr("aria-disabled","true"),a.$prevArrow.removeClass("slick-disabled").attr("aria-disabled","false")):a.currentSlide>=a.slideCount-1&&a.options.centerMode===!0&&(a.$nextArrow.addClass("slick-disabled").attr("aria-disabled","true"),a.$prevArrow.removeClass("slick-disabled").attr("aria-disabled","false")))},b.prototype.updateDots=function(){var a=this;null!==a.$dots&&(a.$dots.find("li").removeClass("slick-active").attr("aria-hidden","true"),a.$dots.find("li").eq(Math.floor(a.currentSlide/a.options.slidesToScroll)).addClass("slick-active").attr("aria-hidden","false"))},b.prototype.visibility=function(){var a=this;document[a.hidden]?(a.paused=!0,a.autoPlayClear()):a.options.autoplay===!0&&(a.paused=!1,a.autoPlay())},b.prototype.initADA=function(){var b=this;b.$slides.add(b.$slideTrack.find(".slick-cloned")).attr({"aria-hidden":"true",tabindex:"-1"}).find("a, input, button, select").attr({tabindex:"-1"}),b.$slideTrack.attr("role","listbox"),b.$slides.not(b.$slideTrack.find(".slick-cloned")).each(function(c){a(this).attr({role:"option","aria-describedby":"slick-slide"+b.instanceUid+c})}),null!==b.$dots&&b.$dots.attr("role","tablist").find("li").each(function(c){a(this).attr({role:"presentation","aria-selected":"false","aria-controls":"navigation"+b.instanceUid+c,id:"slick-slide"+b.instanceUid+c})}).first().attr("aria-selected","true").end().find("button").attr("role","button").end().closest("div").attr("role","toolbar"),b.activateADA()},b.prototype.activateADA=function(){var a=this;a.$slideTrack.find(".slick-active").attr({"aria-hidden":"false"}).find("a, input, button, select").attr({tabindex:"0"})},b.prototype.focusHandler=function(){var b=this;b.$slider.on("focus.slick blur.slick","*",function(c){c.stopImmediatePropagation();var d=a(this);setTimeout(function(){b.isPlay&&(d.is(":focus")?(b.autoPlayClear(),b.paused=!0):(b.paused=!1,b.autoPlay()))},0)})},a.fn.slick=function(){var f,g,a=this,c=arguments[0],d=Array.prototype.slice.call(arguments,1),e=a.length;for(f=0;e>f;f++)if("object"==typeof c||"undefined"==typeof c?a[f].slick=new b(a[f],c):g=a[f].slick[c].apply(a[f].slick,d),"undefined"!=typeof g)return g;return a}});
var playNum = 0
var playingBadgeEarned = false

function playSoundFX(soundId, volume) {
	switch(soundId) {
		case 'random_beeps':
			addAudioElement(clipSource = '/sounds/random_beeps.mp3', 'random_beeps')
			break
		case 'points_earned':
			addAudioElement(clipSource = '/points_earned.mp3', 'points_earned')
			break
		case 'badge_earned':
			if(playingBadgeEarned) return
			playingBadgeEarned = true
			addAudioElement(clipSource = '/badge_earned.m4a', 'badge_earned')
			break	
		case 'rewards_earned':
      addAudioElement(clipSource = '/sounds/counter_clipped.mp3', 'rewards_earned')
      break 
		default:
			console.log('error in soundfx.js: soundId ' + soundId + ' invalid')
			return
	}
	$('#soundfx_' + playNum.toString())[0].volume = volume;
  $('#soundfx_' + playNum.toString())[0].play();
	return playNum++
}

function addAudioElement(clipSource, type) {
	var localPlayNum = playNum;
	var audioHTML =  "<audio id='soundfx_" + localPlayNum.toString() + "' onended='removeAudioElement(" + localPlayNum.toString() + ", &apos;" + type + "&apos;)'>"
	    audioHTML += 	"<source src='" + clipSource + "' ></source>"
	    audioHTML += "</audio>"
	$('body').append(audioHTML)
}

function removeAudioElement(playNum, type) {
  $('#soundfx_' + playNum.toString())[0].pause()
	setTimeout(function(){$('#soundfx_' + playNum.toString()).remove(); cleanUp(type);},1000)
}

function cleanUp(type) {
	//do appropriate cleanup based on the type of sound
	switch(type) {
		case 'points_earned':
		//nothing
			break;
		case 'rewards_earned':
    //nothing
      break;
		case 'badge_earned':
			playingBadgeEarned = false
			break;
		default:
			console.log('error in soundfx.js: type: ' + type + ' invalid')
	}
}
;
var StorageInterface = function(namespaceKey) {
  if(localStorage.getItem(namespaceKey) === null) {
    localStorage.setItem(namespaceKey, "{}");
  }
  this.namespaceKey = namespaceKey;
};

StorageInterface.prototype.setObject = function(key, value) {
  var nsObj = JSON.parse(localStorage.getItem(this.namespaceKey));
  nsObj[key] = value;
  localStorage.setItem(this.namespaceKey, JSON.stringify(nsObj));
};


StorageInterface.prototype.getObject = function(key) {
    var nsObj = JSON.parse(localStorage.getItem(this.namespaceKey));
    return (nsObj) ? nsObj[key] : null;
};

StorageInterface.prototype.removeObject = function(key) {
  var nsObj = JSON.parse(localStorage.getItem(this.namespaceKey));
  if(!nsObj)
      return false;
  delete nsObj[key];
  localStorage.setItem(this.namespaceKey, JSON.stringify(nsObj));
};

//test comment
StorageInterface.prototype.destroy = function() {
  localStorage.removeItem(this.namespaceKey);
};
(function() {


}).call(this);
window.svg_hack = function(elem, options){
  $(document).ready(function(){
    var $img = jQuery(elem);
    var imgID = $img.attr('id');
    var imgClass = $img.attr('class');
    var imgStyle = $img.attr('style');
    var imgURL = $img.attr('src');
    if(!imgURL)
      return
    //We need to do this to bypass the CDN, which throws a cross domain error when we try to access the info through jquery.
    imgURL = "/assets" + imgURL.split("/assets")[1]
  
    jQuery.get(imgURL, function(data) {
        
        // Get the SVG tag, ignore the rest
        var $svg = jQuery(data).find('svg');
  
        // Add replaced image's ID to the new SVG
        if(typeof imgID !== 'undefined') {
            $svg = $svg.attr('id', imgID);
        }
        // Add replaced image's classes to the new SVG
        if(typeof imgClass !== 'undefined') {
            $svg = $svg.attr('class', imgClass+' replaced-svg');
            $svg.find('path').attr('class', imgClass)
        }
        
         if(typeof imgStyle !== 'undefined') {
            $svg.find('path').attr('style', imgStyle)
            $svg.attr('style', imgStyle)
        }
  
        // Remove any invalid XML tags as per http://validator.w3.org
        $svg = $svg.removeAttr('xmlns:a');
        
        // Check if the viewport is set, else we gonna set it if we can.
        if(!$svg.attr('viewBox') && $svg.attr('height') && $svg.attr('width')) {
            $svg.attr('viewBox', '0 0 ' + $svg.attr('height') + ' ' + $svg.attr('width'))
        }
        
        
        if(options) {
          if(options.hover) {
            $svg.mouseover(function() {
              $svg.find("path").attr("class",options.hover.on)
              $svg.attr("class",options.hover.on)
            })
            $svg.mouseleave(function() {
              $svg.removeClass(options.hover.on).addClass(options.hover.off)
              $svg.find("path").attr("class",options.hover.off)
              $svg.attr("class",options.hover.off)
            })
          }
        }
        
        
        
        if($img[0]){
          var e = $img[0]
          $.each(e.attributes, function() {
            if(this.specified) {
              if(this.name.indexOf("on") == 0) {
                $svg.find("path").attr(this.name, this.value)
              }
            }
          })
        }
  
        // Replace image with new SVG
        $img.replaceWith($svg);
  
    }, 'xml');
  })
  
}
;
// Type definitions for jQuery.Colorbox 1.4.15
// Project: http://www.jacklmoore.com/colorbox/
// Definitions by: Gidon Junge <https://github.com/gjunge>
// Definitions: https://github.com/borisyankov/DefinitelyTyped/
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/jquery.d.ts" />
;
// Type definitions for jQuery 1.10.x / 2.0.x
// Project: http://jquery.com/
// Definitions by: Boris Yankov <https://github.com/borisyankov/>, Christian Hoffmeister <https://github.com/choffmeister>, Steve Fenton <https://github.com/Steve-Fenton>, Diullei Gomes <https://github.com/Diullei>, Tass Iliopoulos <https://github.com/tasoili>, Jason Swearingen <https://github.com/jasons-novaleaf>, Sean Hill <https://github.com/seanski>, Guus Goossens <https://github.com/Guuz>, Kelly Summerlin <https://github.com/ksummerlin>, Basarat Ali Syed <https://github.com/basarat>, Nicholas Wolverson <https://github.com/nwolverson>, Derek Cicerone <https://github.com/derekcicerone>, Andrew Gaspar <https://github.com/AndrewGaspar>, James Harrison Fisher <https://github.com/jameshfisher>, Seikichi Kondo <https://github.com/seikichi>, Benjamin Jackman <https://github.com/benjaminjackman>, Poul Sorensen <https://github.com/s093294>, Josh Strobl <https://github.com/JoshStrobl>, John Reilly <https://github.com/johnnyreilly/>, Dick van den Brink <https://github.com/DickvdBrink>
// Definitions: https://github.com/borisyankov/DefinitelyTyped
;
// Type definitions for jQuery 1.10.x / 2.0.x
// Project: http://jquery.com/
// Definitions by: Boris Yankov <https://github.com/borisyankov/>, Christian Hoffmeister <https://github.com/choffmeister>, Steve Fenton <https://github.com/Steve-Fenton>, Diullei Gomes <https://github.com/Diullei>, Tass Iliopoulos <https://github.com/tasoili>, Jason Swearingen <https://github.com/jasons-novaleaf>, Sean Hill <https://github.com/seanski>, Guus Goossens <https://github.com/Guuz>, Kelly Summerlin <https://github.com/ksummerlin>, Basarat Ali Syed <https://github.com/basarat>, Nicholas Wolverson <https://github.com/nwolverson>, Derek Cicerone <https://github.com/derekcicerone>, Andrew Gaspar <https://github.com/AndrewGaspar>, James Harrison Fisher <https://github.com/jameshfisher>, Seikichi Kondo <https://github.com/seikichi>, Benjamin Jackman <https://github.com/benjaminjackman>, Poul Sorensen <https://github.com/s093294>, Josh Strobl <https://github.com/JoshStrobl>, John Reilly <https://github.com/johnnyreilly/>, Dick van den Brink <https://github.com/DickvdBrink>
// Definitions: https://github.com/borisyankov/DefinitelyTyped
;
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/jquery/jquery.d.ts" />
;
//goals:
//-as things are changed by users (programs, schems, etc...), they will be stored in local storage with a timestamp.
//-the cued_update_manager will periodically tick all the things it's tracking, and call the put function on the thing
var UpdateManager = function(devMode) {
    var self = this;
    this.tickFreq = 100;
    this.storage = new StorageInterface("update_cue");
    this.ids   = this.storage.getObject("update_ids") || [];
    this.cuedUpdates = [];
    if(this.ids){
        this.ids.forEach(function(id){
            self.cuedUpdates.push(self.cuedUpdateFromStorage(id));            
        })
    }
    
    //devmode for running tests...
    if(!devMode)
        this.tickInterval = setInterval(function(){self.tickCue()}, this.tickFreq);
    
    this.storage.setObject("update_ids", this.ids);
}

//should alter the local storage object
//thing needs to be a json object in this format:
// {id: Schematic_bal-schem, updateUrl: "/schematics/bal-schem", flushSpeed: 150, data: {stuff}}
UpdateManager.prototype.update = function(thing) {
    var cuedThing = this.cuedThing(thing.id);
    if(!cuedThing) {//thing isn't yet cued
        this.track(thing)
    } else {
        cuedThing.data = thing.data;
        this.storage.setObject(thing.id, this.storageFromCuedUpdate(cuedThing));
    }
};

UpdateManager.prototype.track = function(thing) {
    var id = thing.id;
    var cu = new CuedUpdate(thing.id, thing.timeUntilPut, thing.updateUrl, thing.data);
    var recoveryIds = this.ids.splice();
    var recoveryCue = this.cuedUpdates.splice();
    //store the thing in ra memory
    this.cuedUpdates.push(cu);
    this.ids.push(id);
    
    //store the thing in browser
    //needs error handling in case storage is full
    try {
        this.storage.setObject("update_ids", this.ids);
        this.storage.setObject(thing.id, this.storageFromCuedUpdate(cu));
    } catch (err) {
        console.log("ERR in tracking a thing for delayed saving....");
        console.log("Insta-flushing the data");
        cu.put();
        this.storage.removeObject(thing.id);
        this.storage.setObject("update_ids", recoveryIds);
        this.ids = recoveryIds;
        this.cuedUpdates = recoveryCue;
        console.log("ERROR in UpdateManager, storage probably full: ", err)
    }
    
};

UpdateManager.prototype.untrack = function(thing) {
    var id = thing.id
    var index = this.ids.indexOf(id);
    if(index > -1) {
        this.ids.splice(index, 1);
        this.cuedUpdates.splice(index, 1);
    }
    this.storage.setObject("update_ids", this.ids);
    this.storage.removeObject(id);
}


UpdateManager.prototype.tickCue = function() {
    var self = this;
    var things = self.cuedUpdates;
    var times  = self.cuedUpdates.length;
    for(var i = 0; i < times; i++) {
        var cuedUpdate = self.cuedUpdates[i]
        var stored = self.storage.getObject(cuedUpdate.id)
        
        stored.timeUntilPut -= self.tickFreq;
        self.storage.setObject(cuedUpdate.id, stored);
        
        if(!cuedUpdate.tick(self.tickFreq)) {
            //remove the thing
            self.untrack(cuedUpdate);
            i--;
            times--;
        }
    }
}

UpdateManager.prototype.cuedUpdateFromStorage = function(id) {
    var thing = this.storage.getObject(id)
    return new CuedUpdate(id, thing.timeUntilPut, thing.updateUrl, thing.data) 
}

UpdateManager.prototype.storageFromCuedUpdate = function(cu) {
    return {id: cu.id, timeUntilPut: cu.timeUntilPut, updateUrl: cu.updateUrl, data: cu.data}
}


UpdateManager.prototype.hasCuedThing = function(id) {
    return (this.ids.indexOf(id) > -1)
}

UpdateManager.prototype.cuedThing = function(id) {
    var retVal = false;
    this.cuedUpdates.forEach(function(cu){
        if(cu.id == id) 
            retVal = cu;
    });
    return retVal;
}

//private prototype created by the update manager either when it intializes, or when something is cued for the first time.
var CuedUpdate = function(id, timeUntilPut, updateUrl, data) {
    //id should be in the format "Schematic_balthatrix-schemname"
   this.id = id;
   this.updateUrl = updateUrl;
   this.timeUntilPut = timeUntilPut;
   this.data = data;
};



CuedUpdate.prototype.tick = function(delta) {
    this.timeUntilPut -= delta;
    if(this.timeUntilPut <= 0) {
        this.put();
        return false;
    } else { 
        return true;
    }
};


CuedUpdate.prototype.put = function() {
    var self = this;
    $.ajax({
        url: self.updateUrl,
        method: "PUT",
        data: self.data,
        success: function(d) {
            console.log("put returned: ", d);
        }
    });
};

CuedUpdate.prototype.delay = function(delta) {
    this.timeUntilPut += delta;
}

var dumdumVariableForNothing = 0
;
/// <reference path="/home/dev/LTM-Web/app/assets/javascripts/typings/jquery.d.ts"/>
var Util;
(function (Util) {
    function uniqueIn(arr) {
        var ret = [];
        arr.map(function (e) {
            if (ret.indexOf(e) == -1)
                ret.push(e);
        });
        return ret;
    }
    Util.uniqueIn = uniqueIn;
    function compact(arr) {
        var ret = [];
        arr.map(function (e) {
            if (typeof e != "undefined")
                ret.push(e);
        });
        return ret;
    }
    Util.compact = compact;
    function daydiff(first, second) {
        return Math.round((second - first) / (1000 * 60 * 60 * 24));
    }
    Util.daydiff = daydiff;
    // This is done in a linear time O(n) without recursion
    // memory complexity is O(1) or O(n) if mutable param is set to false
    function flatten(array, mutable) {
        var toString = Object.prototype.toString;
        var arrayTypeStr = '[object Array]';
        var result = [];
        var nodes = (mutable && array) || array.slice();
        var node;
        if (!array.length) {
            return result;
        }
        node = nodes.pop();
        do {
            if (toString.call(node) === arrayTypeStr) {
                nodes.push.apply(nodes, node);
            }
            else {
                result.push(node);
            }
        } while (nodes.length && (node = nodes.pop()) !== undefined);
        result.reverse(); // we reverse result to restore the original order
        return result;
    }
    Util.flatten = flatten;
    function degRotationFrom(elemId) {
        var el = document.getElementById(elemId);
        var st = window.getComputedStyle(el, null);
        var tr = st.getPropertyValue("-webkit-transform") ||
            st.getPropertyValue("-moz-transform") ||
            st.getPropertyValue("-ms-transform") ||
            st.getPropertyValue("-o-transform") ||
            st.getPropertyValue("transform") ||
            "fail...";
        if (tr == "none") {
            return 0;
        }
        // With rotate(30deg)...
        // matrix(0.866025, 0.5, -0.5, 0.866025, 0px, 0px)
        // rotation matrix - http://en.wikipedia.org/wiki/Rotation_matrix
        var temp = tr.split('(')[1];
        temp = temp.split(')')[0];
        var values = temp.split(',');
        var a = parseFloat(values[0]);
        var b = parseFloat(values[1]);
        var c = parseFloat(values[2]);
        var d = parseFloat(values[3]);
        var scale = Math.sqrt(a * a + b * b);
        var sin = b / scale;
        var angle = Math.round(Math.asin(sin) * (180 / Math.PI));
        return angle;
    }
    Util.degRotationFrom = degRotationFrom;
    function animateTransformRotation(imgId, args) {
        // caching the object for performance reasons
        var $elem = $("#" + imgId);
        // we use a pseudo object for the animation
        // (starts from `0` to `angle`), you can name it as you want
        $({ deg: Util.degRotationFrom(imgId) }).animate({ deg: args.angle }, {
            duration: args.duration,
            step: function (now) {
                // in the step-callback (that is fired each step of the animation),
                // you can use the `now` paramter which contains the current
                // animation-position (`0` up to `angle`)
                $elem.css({
                    transform: 'rotate(' + now + 'deg)'
                });
            }
        });
    }
    Util.animateTransformRotation = animateTransformRotation;
    function toTitleCase(str) {
        return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    }
    Util.toTitleCase = toTitleCase;
})(Util || (Util = {}));
var VideoPlayer = (function () {
    function VideoPlayer(v) {
        this.video = v;
    }
    VideoPlayer.prototype.playPause = function () {
        if (this.video.paused)
            this.video.play();
        else
            this.video.pause();
    };
    VideoPlayer.prototype.play = function () {
        this.video.play();
    };
    VideoPlayer.prototype.pause = function () {
        this.video.pause();
    };
    VideoPlayer.prototype.isPlaying = function () {
        return !(this.video.paused);
    };
    VideoPlayer.prototype.changeSize = function (delta) {
        this.video.width += delta;
    };
    VideoPlayer.prototype.getCurrentTime = function () {
        return this.video.currentTime;
    };
    VideoPlayer.prototype.getDuration = function () {
        return this.video.duration;
    };
    VideoPlayer.prototype.setCurrentTime = function (t) {
        return this.video.currentTime = t;
    };
    VideoPlayer.prototype.getDonePercent = function () {
        return Math.ceil(100 * (this.video.currentTime / this.video.duration));
    };
    return VideoPlayer;
})();
var PartialLoader = function(partials, container, onComplete, afterEach, loaderPath) {
  this.partials = partials
  this.onComplete = onComplete
  this.afterEach = afterEach
  this.container = container
  this.loaderPath=loaderPath
}

// todo: add error handling if request errors out
PartialLoader.prototype.requestPartial = function(name, onReceived) {
  $.ajax({
    url: "/ask_for_partial",
    data: {partial_name: name},
    success: function(d) {
      contentCache[name] = d
      if(onReceived) {
        onReceived(d)
      }
    }
  })
}

PartialLoader.prototype.nextPartial = function() {
  if (!this.partials || this.partials.length === 0) return
  var self = this
  var thisPartial = self.partials.shift()
  
  var onReceived = function(data) {
    $("#"+self.loadingGifId).before(data)
  
  if(typeof self.afterEach == "function") {
    self.afterEach()
  }
  
  if(self.partials.length > 0) {
    
    setTimeout(function(){self.nextPartial()}, 500)
  } else {
    self.onComplete()
    $("#"+self.loadingGifId).remove()
    }
  }
  
  this.requestPartial(thisPartial.name,onReceived)
}

PartialLoader.prototype.startLoading = function() {
  if(!this.partials.length > 0) return
  if(this.loaderPath) {
    this.loadingGifId = makeId()
    $(this.container).append($("<div></div>")
                              .attr("id", this.loadingGifId)
                              .append("<img src='" + this.loaderPath + "' />"))
  }                           
  this.nextPartial()
}  

function makeId()
{
    var text = "";
  var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for( var i=0; i < 10; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}
;
(function() {


}).call(this);
/*! jQuery UI - v1.10.3 - 2013-05-28
* http://jqueryui.com
* Includes: jquery.ui.widget.js
* Copyright 2013 jQuery Foundation and other contributors Licensed MIT */


(function(e,t){var i=0,s=Array.prototype.slice,n=e.cleanData;e.cleanData=function(t){for(var i,s=0;null!=(i=t[s]);s++)try{e(i).triggerHandler("remove")}catch(a){}n(t)},e.widget=function(i,s,n){var a,r,o,h,l={},u=i.split(".")[0];i=i.split(".")[1],a=u+"-"+i,n||(n=s,s=e.Widget),e.expr[":"][a.toLowerCase()]=function(t){return!!e.data(t,a)},e[u]=e[u]||{},r=e[u][i],o=e[u][i]=function(e,i){return this._createWidget?(arguments.length&&this._createWidget(e,i),t):new o(e,i)},e.extend(o,r,{version:n.version,_proto:e.extend({},n),_childConstructors:[]}),h=new s,h.options=e.widget.extend({},h.options),e.each(n,function(i,n){return e.isFunction(n)?(l[i]=function(){var e=function(){return s.prototype[i].apply(this,arguments)},t=function(e){return s.prototype[i].apply(this,e)};return function(){var i,s=this._super,a=this._superApply;return this._super=e,this._superApply=t,i=n.apply(this,arguments),this._super=s,this._superApply=a,i}}(),t):(l[i]=n,t)}),o.prototype=e.widget.extend(h,{widgetEventPrefix:r?h.widgetEventPrefix:i},l,{constructor:o,namespace:u,widgetName:i,widgetFullName:a}),r?(e.each(r._childConstructors,function(t,i){var s=i.prototype;e.widget(s.namespace+"."+s.widgetName,o,i._proto)}),delete r._childConstructors):s._childConstructors.push(o),e.widget.bridge(i,o)},e.widget.extend=function(i){for(var n,a,r=s.call(arguments,1),o=0,h=r.length;h>o;o++)for(n in r[o])a=r[o][n],r[o].hasOwnProperty(n)&&a!==t&&(i[n]=e.isPlainObject(a)?e.isPlainObject(i[n])?e.widget.extend({},i[n],a):e.widget.extend({},a):a);return i},e.widget.bridge=function(i,n){var a=n.prototype.widgetFullName||i;e.fn[i]=function(r){var o="string"==typeof r,h=s.call(arguments,1),l=this;return r=!o&&h.length?e.widget.extend.apply(null,[r].concat(h)):r,o?this.each(function(){var s,n=e.data(this,a);return n?e.isFunction(n[r])&&"_"!==r.charAt(0)?(s=n[r].apply(n,h),s!==n&&s!==t?(l=s&&s.jquery?l.pushStack(s.get()):s,!1):t):e.error("no such method '"+r+"' for "+i+" widget instance"):e.error("cannot call methods on "+i+" prior to initialization; "+"attempted to call method '"+r+"'")}):this.each(function(){var t=e.data(this,a);t?t.option(r||{})._init():e.data(this,a,new n(r,this))}),l}},e.Widget=function(){},e.Widget._childConstructors=[],e.Widget.prototype={widgetName:"widget",widgetEventPrefix:"",defaultElement:"<div>",options:{disabled:!1,create:null},_createWidget:function(t,s){s=e(s||this.defaultElement||this)[0],this.element=e(s),this.uuid=i++,this.eventNamespace="."+this.widgetName+this.uuid,this.options=e.widget.extend({},this.options,this._getCreateOptions(),t),this.bindings=e(),this.hoverable=e(),this.focusable=e(),s!==this&&(e.data(s,this.widgetFullName,this),this._on(!0,this.element,{remove:function(e){e.target===s&&this.destroy()}}),this.document=e(s.style?s.ownerDocument:s.document||s),this.window=e(this.document[0].defaultView||this.document[0].parentWindow)),this._create(),this._trigger("create",null,this._getCreateEventData()),this._init()},_getCreateOptions:e.noop,_getCreateEventData:e.noop,_create:e.noop,_init:e.noop,destroy:function(){this._destroy(),this.element.unbind(this.eventNamespace).removeData(this.widgetName).removeData(this.widgetFullName).removeData(e.camelCase(this.widgetFullName)),this.widget().unbind(this.eventNamespace).removeAttr("aria-disabled").removeClass(this.widgetFullName+"-disabled "+"ui-state-disabled"),this.bindings.unbind(this.eventNamespace),this.hoverable.removeClass("ui-state-hover"),this.focusable.removeClass("ui-state-focus")},_destroy:e.noop,widget:function(){return this.element},option:function(i,s){var n,a,r,o=i;if(0===arguments.length)return e.widget.extend({},this.options);if("string"==typeof i)if(o={},n=i.split("."),i=n.shift(),n.length){for(a=o[i]=e.widget.extend({},this.options[i]),r=0;n.length-1>r;r++)a[n[r]]=a[n[r]]||{},a=a[n[r]];if(i=n.pop(),s===t)return a[i]===t?null:a[i];a[i]=s}else{if(s===t)return this.options[i]===t?null:this.options[i];o[i]=s}return this._setOptions(o),this},_setOptions:function(e){var t;for(t in e)this._setOption(t,e[t]);return this},_setOption:function(e,t){return this.options[e]=t,"disabled"===e&&(this.widget().toggleClass(this.widgetFullName+"-disabled ui-state-disabled",!!t).attr("aria-disabled",t),this.hoverable.removeClass("ui-state-hover"),this.focusable.removeClass("ui-state-focus")),this},enable:function(){return this._setOption("disabled",!1)},disable:function(){return this._setOption("disabled",!0)},_on:function(i,s,n){var a,r=this;"boolean"!=typeof i&&(n=s,s=i,i=!1),n?(s=a=e(s),this.bindings=this.bindings.add(s)):(n=s,s=this.element,a=this.widget()),e.each(n,function(n,o){function h(){return i||r.options.disabled!==!0&&!e(this).hasClass("ui-state-disabled")?("string"==typeof o?r[o]:o).apply(r,arguments):t}"string"!=typeof o&&(h.guid=o.guid=o.guid||h.guid||e.guid++);var l=n.match(/^(\w+)\s*(.*)$/),u=l[1]+r.eventNamespace,c=l[2];c?a.delegate(c,u,h):s.bind(u,h)})},_off:function(e,t){t=(t||"").split(" ").join(this.eventNamespace+" ")+this.eventNamespace,e.unbind(t).undelegate(t)},_delay:function(e,t){function i(){return("string"==typeof e?s[e]:e).apply(s,arguments)}var s=this;return setTimeout(i,t||0)},_hoverable:function(t){this.hoverable=this.hoverable.add(t),this._on(t,{mouseenter:function(t){e(t.currentTarget).addClass("ui-state-hover")},mouseleave:function(t){e(t.currentTarget).removeClass("ui-state-hover")}})},_focusable:function(t){this.focusable=this.focusable.add(t),this._on(t,{focusin:function(t){e(t.currentTarget).addClass("ui-state-focus")},focusout:function(t){e(t.currentTarget).removeClass("ui-state-focus")}})},_trigger:function(t,i,s){var n,a,r=this.options[t];if(s=s||{},i=e.Event(i),i.type=(t===this.widgetEventPrefix?t:this.widgetEventPrefix+t).toLowerCase(),i.target=this.element[0],a=i.originalEvent)for(n in a)n in i||(i[n]=a[n]);return this.element.trigger(i,s),!(e.isFunction(r)&&r.apply(this.element[0],[i].concat(s))===!1||i.isDefaultPrevented())}},e.each({show:"fadeIn",hide:"fadeOut"},function(t,i){e.Widget.prototype["_"+t]=function(s,n,a){"string"==typeof n&&(n={effect:n});var r,o=n?n===!0||"number"==typeof n?i:n.effect||i:t;n=n||{},"number"==typeof n&&(n={duration:n}),r=!e.isEmptyObject(n),n.complete=a,n.delay&&s.delay(n.delay),r&&e.effects&&e.effects.effect[o]?s[t](n):o!==t&&s[o]?s[o](n.duration,n.easing,a):s.queue(function(i){e(this)[t](),a&&a.call(s[0]),i()})}})})(jQuery);
var METRO_AUTO_REINIT,METRO_LOCALE,METRO_WEEK_START,METRO_DIALOG=!1;(function(c){c.Metro=function(a){c.extend({},a)};c.Metro.getDeviceSize=function(){return{width:0<window.innerWidth?window.innerWidth:screen.width,height:0<window.innerHeight?window.innerHeight:screen.height}}})(jQuery);$(function(){$("html").on("click",function(c){$(".dropdown-menu").each(function(a,b){$(b).hasClass("keep-open")||"block"!=$(b).css("display")||$(b).hide()})})});
$(function(){$(window).on("resize",function(){if(METRO_DIALOG){var c=($(window).height()-METRO_DIALOG.outerHeight())/2,a=($(window).width()-METRO_DIALOG.outerWidth())/2;METRO_DIALOG.css({top:c,left:a})}})});(function(c){c.Metro.currentLocale="en";c.Metro.currentLocale=void 0!=METRO_LOCALE?METRO_LOCALE:"en";c.Metro.Locale={en:{months:"January February March April May June July August September October November December Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "),days:"Sunday Monday Tuesday Wednesday Thursday Friday Saturday Su Mo Tu We Th Fr Sa".split(" "),buttons:"Today Clear Cancel Help Prior Next Finish".split(" ")},fr:{months:"Janvier F\u00e9vrier Mars Avril Mai Juin Juillet Ao\u00fbt Septembre Octobre Novembre D\u00e9cembre Jan F\u00e9v Mars Avr Mai Juin Juil Ao\u00fbt Sept Oct Nov D\u00e9c".split(" "),
days:"Dimanche Lundi Mardi Mercredi Jeudi Vendredi Samedi Di Lu Ma Me Je Ve Sa".split(" "),buttons:"Aujourd'hui Effacer Annuler Aide Pr\u00e9cedent Suivant Fin".split(" ")},nl:{months:"Januari Februari Maart April Mei Juni Juli Augustus September Oktober November December Jan Feb Mrt Apr Mei Jun Jul Aug Sep Okt Nov Dec".split(" "),days:"Zondag Maandag Dinsdag Woensdag Donderdag Vrijdag Zaterdag Zo Ma Di Wo Do Vr Za".split(" "),buttons:"Vandaag Verwijderen Annuleren Hulp Vorige Volgende Einde".split(" ")},
ua:{months:"\u0421\u0456\u0447\u0435\u043d\u044c \u041b\u044e\u0442\u0438\u0439 \u0411\u0435\u0440\u0435\u0437\u0435\u043d\u044c \u041a\u0432\u0456\u0442\u0435\u043d\u044c \u0422\u0440\u0430\u0432\u0435\u043d\u044c \u0427\u0435\u0440\u0432\u0435\u043d\u044c \u041b\u0438\u043f\u0435\u043d\u044c \u0421\u0435\u0440\u043f\u0435\u043d\u044c \u0412\u0435\u0440\u0435\u0441\u0435\u043d\u044c \u0416\u043e\u0432\u0442\u0435\u043d\u044c \u041b\u0438\u0441\u0442\u043e\u043f\u0430\u0434 \u0413\u0440\u0443\u0434\u0435\u043d\u044c \u0421\u0456\u0447 \u041b\u044e\u0442 \u0411\u0435\u0440 \u041a\u0432\u0456 \u0422\u0440\u0430 \u0427\u0435\u0440 \u041b\u0438\u043f \u0421\u0435\u0440 \u0412\u0435\u0440 \u0416\u043e\u0432 \u041b\u0438\u0441 \u0413\u0440\u0443".split(" "),
days:"\u041d\u0435\u0434\u0456\u043b\u044f \u041f\u043e\u043d\u0435\u0434\u0456\u043b\u043e\u043a \u0412\u0456\u0432\u0442\u043e\u0440\u043e\u043a \u0421\u0435\u0440\u0435\u0434\u0430 \u0427\u0435\u0442\u0432\u0435\u0440 \u041f\u2019\u044f\u0442\u043d\u0438\u0446\u044f \u0421\u0443\u0431\u043e\u0442\u0430 \u041d\u0434 \u041f\u043d \u0412\u0442 \u0421\u0440 \u0427\u0442 \u041f\u0442 \u0421\u0431".split(" "),buttons:"\u0421\u044c\u043e\u0433\u043e\u0434\u043d\u0456 \u041e\u0447\u0438\u0441\u0442\u0438\u0442\u0438 \u0421\u043a\u0430\u0441\u0443\u0432\u0430\u0442\u0438 \u0414\u043e\u043f\u043e\u043c\u043e\u0433\u0430 \u041d\u0430\u0437\u0430\u0434 \u0412\u043f\u0435\u0440\u0435\u0434 \u0413\u043e\u0442\u043e\u0432\u043e".split(" ")},
ru:{months:"\u042f\u043d\u0432\u0430\u0440\u044c \u0424\u0435\u0432\u0440\u0430\u043b\u044c \u041c\u0430\u0440\u0442 \u0410\u043f\u0440\u0435\u043b\u044c \u041c\u0430\u0439 \u0418\u044e\u043d\u044c \u0418\u044e\u043b\u044c \u0410\u0432\u0433\u0443\u0441\u0442 \u0421\u0435\u043d\u0442\u044f\u0431\u0440\u044c \u041e\u043a\u0442\u044f\u0431\u0440\u044c \u041d\u043e\u044f\u0431\u0440\u044c \u0414\u0435\u043a\u0430\u0431\u0440\u044c \u042f\u043d\u0432 \u0424\u0435\u0432 \u041c\u0430\u0440 \u0410\u043f\u0440 \u041c\u0430\u0439 \u0418\u044e\u043d \u0418\u044e\u043b \u0410\u0432\u0433 \u0421\u0435\u043d \u041e\u043a\u0442 \u041d\u043e\u044f \u0414\u0435\u043a".split(" "),
days:"\u0412\u043e\u0441\u043a\u0440\u0435\u0441\u0435\u043d\u044c\u0435 \u041f\u043e\u043d\u0435\u0434\u0435\u043b\u044c\u043d\u0438\u043a \u0412\u0442\u043e\u0440\u043d\u0438\u043a \u0421\u0440\u0435\u0434\u0430 \u0427\u0435\u0442\u0432\u0435\u0440\u0433 \u041f\u044f\u0442\u043d\u0438\u0446\u0430 \u0421\u0443\u0431\u0431\u043e\u0442\u0430 \u0412\u0441 \u041f\u043d \u0412\u0442 \u0421\u0440 \u0427\u0442 \u041f\u0442 \u0421\u0431".split(" "),buttons:"\u0421\u0435\u0433\u043e\u0434\u043d\u044f \u041e\u0447\u0438\u0441\u0442\u0438\u0442\u044c \u041e\u0442\u043c\u0435\u043d\u0438\u0442\u044c \u041f\u043e\u043c\u043e\u0449\u044c \u041d\u0430\u0437\u0430\u0434 \u0412\u043f\u0435\u0440\u0435\u0434 \u0413\u043e\u0442\u043e\u0432\u043e".split(" ")},
zhCN:{months:"\u4e00\u6708 \u4e8c\u6708 \u4e09\u6708 \u56db\u6708 \u4e94\u6708 \u516d\u6708 \u4e03\u6708 \u516b\u6708 \u4e5d\u6708 \u5341\u6708 \u5341\u4e00\u6708 \u5341\u4e8c\u6708 \u4e00\u6708 \u4e8c\u6708 \u4e09\u6708 \u56db\u6708 \u4e94\u6708 \u516d\u6708 \u4e03\u6708 \u516b\u6708 \u4e5d\u6708 \u5341\u6708 \u5341\u4e00\u6708 \u5341\u4e8c\u6708".split(" "),days:"\u661f\u671f\u65e5 \u661f\u671f\u4e00 \u661f\u671f\u4e8c \u661f\u671f\u4e09 \u661f\u671f\u56db \u661f\u671f\u4e94 \u661f\u671f\u516d \u65e5 \u4e00 \u4e8c \u4e09 \u56db \u4e94 \u516d".split(" "),
buttons:"\u4eca\u65e5 \u6e05\u9664 Cancel Help Prior Next Finish".split(" ")},it:{months:"Gennaio;Febbraio;Marzo;Aprile;Maggio;Giugno;Luglio;Agosto;Settembre;Ottobre;Novembre;Dicembre;Gen; Feb;Mar;Apr;Mag;Giu;Lug;Ago;Set;Ott;Nov;Dic".split(";"),days:"Luned\u00ec Marted\u00ec Mercoled\u00ec Gioved\u00ec Venerd\u00ec Sabato Domenica Lun Mar Mer Gio Ven Sab Dom".split(" "),buttons:"Oggi Cancella Cancel Help Prior Next Finish".split(" ")},de:{months:"Januar Februar M\u00e4rz April Mai Juni Juli August September Oktober November Dezember Jan Feb Mrz Apr Mai Jun Jul Aug Sep Okt Nov Dez".split(" "),
days:"Sonntag Montag Dienstag Mittwoch Donnerstag Freitag Samstag So Mo Di Mi Do Fr Sa".split(" "),buttons:"Heute Zur\u00fccksetzen Abbrechen Hilfe Fr\u00fcher Sp\u00e4ter Fertig".split(" ")},es:{months:"Enero Febrero Marzo Abril Mayo Junio Julio Agosto Septiembre Octubre Noviembre Diciembre Ene Feb Mar Abr May Jun Jul Ago Sept Oct Nov Dic".split(" "),days:"Domingo Lunes Martes Mi\u00e9rcoles Jueves Viernes S\u00e1bado Do Lu Mar Mi\u00e9 Jue Vi S\u00e1b".split(" "),buttons:"Hoy Limpiar Cancel Help Prior Next Finish".split(" ")},
pt:{months:"Janeiro Fevereiro Mar\u00e7o Abril Maio Junho Julho Agosto Setembro Outubro Novembro Dezembro Jan Fev Mar Abr Mai Jun Jul Ago Set Out Nov Dez".split(" "),days:"Domingo Segunda-feira Ter\u00e7a-feira Quarta-feira Quinta-feira Sexta-feira Sabado Dom Seg Ter Qua Qui Sex Sab".split(" "),buttons:"Hoje Limpar Cancelar Ajuda Anterior Seguinte Terminar".split(" ")},pl:{months:"Stycze\u0144 Luty Marzec Kwiecie\u0144 Maj Czerwiec Lipiec Sierpie\u0144 Wrzesie\u0144 Pa\u017adziernik Listopad Grudzie\u0144 Sty Lut Mar Kwi Maj Cze Lip Sie Wrz Pa\u017a Lis Gru".split(" "),
days:"Niedziela Poniedzia\u0142ek Wtorek \u015aroda Czwartek Pi\u0105tek Sobota Nd Pon Wt \u015ar Czw Pt Sob".split(" "),buttons:"Dzisiaj Wyczy\u015b\u0107 Anuluj Pomoc Poprzedni Nast\u0119pny Koniec".split(" ")},cs:{months:"Leden \u00danor B\u0159ezen Duben Kv\u011bten \u010cerven \u010cervenec Srpen Z\u00e1\u0159\u00ed \u0158\u00edjen Listopad Prosinec Led \u00dan B\u0159 Dub Kv\u011b \u010ce \u010cer Srp Z\u00e1 \u0158\u00ed Li Pro".split(" "),days:"Ned\u011ble Pond\u011bl\u00ed \u00dater\u00fd St\u0159eda \u010ctvrtek P\u00e1tek Sobota Ne Po \u00dat St \u010ct P\u00e1 So".split(" "),
buttons:"Dnes Vy\u010distit Zru\u0161it Pomoc P\u0159ede\u0161l\u00fd Dal\u0161\u00ed Dokon\u010dit".split(" ")}};c.Metro.setLocale=function(a,b){c.Metro.Locale[a]=b}})(jQuery);var hasTouch="ontouchend"in window,eventTimer,moveDirection="undefined",startX,startY,deltaX,deltaY,mouseDown=!1;function addTouchEvents(c){hasTouch&&(c.addEventListener("touchstart",touch2Mouse,!0),c.addEventListener("touchmove",touch2Mouse,!0),c.addEventListener("touchend",touch2Mouse,!0))}
function touch2Mouse(c){var a=c.changedTouches[0],b;switch(c.type){case "touchstart":b="mousedown";break;case "touchend":b="mouseup";break;case "touchmove":b="mousemove";break;default:return}"mousedown"==b&&(eventTimer=(new Date).getTime(),startX=a.clientX,startY=a.clientY,mouseDown=!0);"mouseup"==b&&(500>=(new Date).getTime()-eventTimer?b="click":1E3<(new Date).getTime()-eventTimer&&(b="longclick"),eventTimer=0,mouseDown=!1);"mousemove"==b&&mouseDown&&(deltaX=a.clientX-startX,deltaY=a.clientY-startY,
moveDirection=deltaX>deltaY?"horizontal":"vertical");var d=document.createEvent("MouseEvent");d.initMouseEvent(b,!0,!0,window,1,a.screenX,a.screenY,a.clientX,a.clientY,!1,!1,!1,!1,0,null);a.target.dispatchEvent(d);c.preventDefault()};(function(c){c.widget("metro.accordion",{version:"1.0.0",options:{closeAny:!0,open:function(a){},action:function(a){}},_frames:{},_create:function(){var a=this.element;void 0!=a.data("closeany")&&(this.options.closeAny=a.data("closeany"));this.init()},init:function(){var a=this;a.element.on("click",".accordion-frame > .heading",function(b){b.preventDefault();b.stopPropagation();if(!c(this).attr("disabled")&&"none"!=c(this).data("action")){a.options.closeAny&&a._closeFrames();var d=c(this).parent(),
e=d.children(".content");console.log(this);c(e).is(":hidden")?(c(e).slideDown(),c(this).removeClass("collapsed"),a._trigger("frame",b,{frame:d}),a.options.open(d)):(c(e).slideUp(),c(this).addClass("collapsed"));a.options.action(d)}});this.element.children(".accordion-frame").each(function(){var a=c(this).children(".heading"),d=c(this).children(".content");c(this).hasClass("active")&&!c(this).attr("disabled")&&"none"!=c(this).data("action")?(c(d).show(),c(a).removeClass("collapsed")):c(a).addClass("collapsed")})},
_closeFrames:function(){var a=this.element.children(".accordion-frame");c.each(a,function(){var a=c(this);a.children(".heading").addClass("collapsed");a.children(".content").slideUp()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.buttonset",{version:"1.0.0",options:{click:function(a,b){}},_buttons:{},_create:function(){this._buttons=this.element.find("button, .button");this.init()},init:function(){var a=this;this._buttons.each(function(){var b=c(this);b.on("click",function(d){d.preventDefault();b.toggleClass("active");a.options.click(b,b.hasClass("active"));a._trigger("click",null,{button:b,on:b.hasClass("active")})})})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,
b)}})})(jQuery);
(function(c){c.widget("metro.buttongroup",{version:"1.0.0",options:{click:function(a,b){}},_buttons:{},_create:function(){this._buttons=this.element.find("button, .button");this.init()},init:function(){var a=this;this._buttons.each(function(){var b=c(this);b.on("click",function(d){d.preventDefault();a._buttons.removeClass("active");b.addClass("active");a.options.click(b,b.hasClass("active"));a._trigger("click",null,{button:b,on:b.hasClass("active")})})})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",
a,b)}})})(jQuery);var dateFormat=function(){var c=/d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,a=/\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,b=/[^-+\dA-Z]/g,d=function(a,b){a=String(a);for(b=b||2;a.length<b;)a="0"+a;return a};return function(e,f,g){var h=dateFormat;1!=arguments.length||("[object String]"!=Object.prototype.toString.call(e)||/\d/.test(e))||(f=e,e=void 0);e=e?new Date(e):new Date;f=String(h.masks[f]||
f||h.masks["default"]);"UTC:"==f.slice(0,4)&&(f=f.slice(4),g=!0);locale=$.Metro.currentLocale;var k=g?"getUTC":"get",h=e[k+"Date"](),l=e[k+"Day"](),n=e[k+"Month"](),p=e[k+"FullYear"](),m=e[k+"Hours"](),q=e[k+"Minutes"](),u=e[k+"Seconds"](),k=e[k+"Milliseconds"](),r=g?0:e.getTimezoneOffset(),s={d:h,dd:d(h),ddd:$.Metro.Locale[locale].days[l],dddd:$.Metro.Locale[locale].days[l+7],m:n+1,mm:d(n+1),mmm:$.Metro.Locale[locale].months[n],mmmm:$.Metro.Locale[locale].months[n+12],yy:String(p).slice(2),yyyy:p,
h:m%12||12,hh:d(m%12||12),H:m,HH:d(m),M:q,MM:d(q),s:u,ss:d(u),l:d(k,3),L:d(99<k?Math.round(k/10):k),t:12>m?"a":"p",tt:12>m?"am":"pm",T:12>m?"A":"P",TT:12>m?"AM":"PM",Z:g?"UTC":(String(e).match(a)||[""]).pop().replace(b,""),o:(0<r?"-":"+")+d(100*Math.floor(Math.abs(r)/60)+Math.abs(r)%60,4),S:["th","st","nd","rd"][3<h%10?0:(10!=h%100-h%10)*h%10]};return f.replace(c,function(a){return a in s?s[a]:a.slice(1,a.length-1)})}}();
dateFormat.masks={"default":"ddd mmm dd yyyy HH:MM:ss",shortDate:"m/d/yy",mediumDate:"mmm d, yyyy",longDate:"mmmm d, yyyy",fullDate:"dddd, mmmm d, yyyy",shortTime:"h:MM TT",mediumTime:"h:MM:ss TT",longTime:"h:MM:ss TT Z",isoDate:"yyyy-mm-dd",isoTime:"HH:MM:ss",isoDateTime:"yyyy-mm-dd'T'HH:MM:ss",isoUtcDateTime:"UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"};dateFormat.i18n={dayNames:"Sun Mon Tue Wed Thu Fri Sat Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "),monthNames:"Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec January February March April May June July August September October November December".split(" ")};
Date.prototype.format=function(c,a){return dateFormat(this,c,a)};(function(c){c.widget("metro.calendar",{version:"1.0.0",options:{format:"yyyy-mm-dd",multiSelect:!1,startMode:"day",weekStart:void 0!=METRO_WEEK_START?METRO_WEEK_START:0,otherDays:!1,date:new Date,buttons:!0,locale:c.Metro.currentLocale,getDates:function(a){},click:function(a,b){},_storage:[]},_year:0,_month:0,_day:0,_today:new Date,_event:"",_mode:"day",_distance:0,_events:[],_create:function(){var a=this.element;void 0!=a.data("multiSelect")&&(this.options.multiSelect=a.data("multiSelect"));void 0!=
a.data("format")&&(this.options.format=a.data("format"));void 0!=a.data("date")&&(this.options.date=new Date(a.data("date")));void 0!=a.data("locale")&&(this.options.locale=a.data("locale"));void 0!=a.data("startMode")&&(this.options.startMode=a.data("startMode"));void 0!=a.data("weekStart")&&(this.options.weekStart=a.data("weekStart"));void 0!=a.data("otherDays")&&(this.options.otherDays=a.data("otherDays"));this._year=this.options.date.getFullYear();this._distance=parseInt(this.options.date.getFullYear())-
4;this._month=this.options.date.getMonth();this._day=this.options.date.getDate();this._mode=this.options.startMode;a.data("_storage",[]);this._renderCalendar()},_renderMonth:function(){var a=this._year,b=this._month,d=28;1==b&&(0!=a%100&&0==a%4||0==a%400)&&(d=29);var e=["31",""+d+"","31","30","31","30","31","31","30","31","30","31"],f=e[b],g=(new Date(a,b,1)).getDay(),h,k;this.element.html("");d=c("<table/>").addClass("bordered");h=c("<tr/>");c("<td/>").addClass("text-center").html("<a class='btn-previous-year' href='#'><i class='icon-previous'></i></a>").appendTo(h);
c("<td/>").addClass("text-center").html("<a class='btn-previous-month' href='#'><i class='icon-arrow-left-4'></i></a>").appendTo(h);c("<td/>").attr("colspan",3).addClass("text-center").html("<a class='btn-select-month' href='#'>"+c.Metro.Locale[this.options.locale].months[b]+" "+a+"</a>").appendTo(h);c("<td/>").addClass("text-center").html("<a class='btn-next-month' href='#'><i class='icon-arrow-right-4'></i></a>").appendTo(h);c("<td/>").addClass("text-center").html("<a class='btn-next-year' href='#'><i class='icon-next'></i></a>").appendTo(h);
h.addClass("calendar-header").appendTo(d);var l;h=c("<tr/>");for(k=0;7>k;k++)this.options.weekStart?(l=k+1,7==l&&(l=0),c("<td/>").addClass("text-center day-of-week").html(c.Metro.Locale[this.options.locale].days[l+7]).appendTo(h)):c("<td/>").addClass("text-center day-of-week").html(c.Metro.Locale[this.options.locale].days[k+7]).appendTo(h);h.addClass("calendar-subheader").appendTo(d);h=this._month-1;0>h&&(h=11);e=e[h];l=(this.options.weekStart?g+6:g)%7;var n="";h=c("<tr/>");for(k=0;k<l;k++)this.options.otherDays&&
(n=e-(l-k-1)),c("<td/>").addClass("empty").html("<small class='other-day'>"+n+"</small>").appendTo(h);g=(this.options.weekStart?g+6:g)%7;for(k=1;k<=f;k++)g%=7,0==g&&(h.appendTo(d),h=c("<tr/>")),e=c("<td/>").addClass("text-center day").html("<a href='#'>"+k+"</a>"),a==this._today.getFullYear()&&(b==this._today.getMonth()&&this._today.getDate()==k)&&e.addClass("today"),l=(new Date(this._year,this._month,k)).format("yyyy-mm-dd"),0<=this.element.data("_storage").indexOf(l)&&e.find("a").addClass("selected"),
e.appendTo(h),g++;a="";for(k=g+1;7>=k;k++)this.options.otherDays&&(a=k-g),c("<td/>").addClass("empty").html("<small class='other-day'>"+a+"</small>").appendTo(h);h.appendTo(d);this.options.buttons&&(h=c("<tr/>").addClass("calendar-actions"),e=c("<td/>").attr("colspan",7).addClass("text-left").html("<button class='button calendar-btn-today small success'>"+c.Metro.Locale[this.options.locale].buttons[0]+"</button>&nbsp;<button class='button calendar-btn-clear small warning'>"+c.Metro.Locale[this.options.locale].buttons[1]+
"</button>"),e.appendTo(h),h.appendTo(d));d.appendTo(this.element);this.options.getDates(this.element.data("_storage"))},_renderMonths:function(){var a,b,d,e,f;this.element.html("");a=c("<table/>").addClass("bordered");b=c("<tr/>");c("<td/>").addClass("text-center").html("<a class='btn-previous-year' href='#'><i class='icon-arrow-left-4'></i></a>").appendTo(b);c("<td/>").attr("colspan",2).addClass("text-center").html("<a class='btn-select-year' href='#'>"+this._year+"</a>").appendTo(b);c("<td/>").addClass("text-center").html("<a class='btn-next-year' href='#'><i class='icon-arrow-right-4'></i></a>").appendTo(b);
b.addClass("calendar-header").appendTo(a);b=c("<tr/>");for(e=f=0;12>e;e++)d=c("<td/>").addClass("text-center month").html("<a href='#' data-month='"+e+"'>"+c.Metro.Locale[this.options.locale].months[e+12]+"</a>"),this._month==e&&(new Date).getFullYear()==this._year&&d.addClass("today"),d.appendTo(b),0==(f+1)%4&&(b.appendTo(a),b=c("<tr/>")),f+=1;a.appendTo(this.element)},_renderYears:function(){var a,b,d,e,f;this.element.html("");a=c("<table/>").addClass("bordered");b=c("<tr/>");c("<td/>").addClass("text-center").html("<a class='btn-previous-year' href='#'><i class='icon-arrow-left-4'></i></a>").appendTo(b);
c("<td/>").attr("colspan",2).addClass("text-center").html(this._distance+"-"+(this._distance+11)).appendTo(b);c("<td/>").addClass("text-center").html("<a class='btn-next-year' href='#'><i class='icon-arrow-right-4'></i></a>").appendTo(b);b.addClass("calendar-header").appendTo(a);b=c("<tr/>");f=0;for(e=this._distance;e<this._distance+12;e++)d=c("<td/>").addClass("text-center year").html("<a href='#' data-year='"+e+"'>"+e+"</a>"),(new Date).getFullYear()==e&&d.addClass("today"),d.appendTo(b),0==(f+
1)%4&&(b.appendTo(a),b=c("<tr/>")),f+=1;a.appendTo(this.element)},_renderCalendar:function(){switch(this._mode){case "year":this._renderYears();break;case "month":this._renderMonths();break;default:this._renderMonth()}this._initButtons()},_initButtons:function(){var a=this,b=this.element.find("table");"day"==this._mode?(b.find(".btn-select-month").on("click",function(b){b.preventDefault();b.stopPropagation();a._mode="month";a._renderCalendar()}),b.find(".btn-previous-month").on("click",function(b){a._event=
"eventPrevious";b.preventDefault();b.stopPropagation();a._month-=1;0>a._month&&(a._year-=1,a._month=11);a._renderCalendar()}),b.find(".btn-next-month").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._month+=1;12==a._month&&(a._year+=1,a._month=0);a._renderCalendar()}),b.find(".btn-previous-year").on("click",function(b){a._event="eventPrevious";b.preventDefault();b.stopPropagation();a._year-=1;a._renderCalendar()}),b.find(".btn-next-year").on("click",function(b){a._event=
"eventNext";b.preventDefault();b.stopPropagation();a._year+=1;a._renderCalendar()}),b.find(".calendar-btn-today").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a.options.date=new Date;a._year=a.options.date.getFullYear();a._month=a.options.date.getMonth();a._day=a.options.date.getDate();a._renderCalendar()}),b.find(".calendar-btn-clear").on("click",function(b){b.preventDefault();b.stopPropagation();a.options.date=new Date;a._year=a.options.date.getFullYear();a._month=
a.options.date.getMonth();a._day=a.options.date.getDate();a.element.data("_storage",[]);a._renderCalendar()}),b.find(".day a").on("click",function(d){d.preventDefault();d.stopPropagation();d=(new Date(a._year,a._month,parseInt(c(this).html()))).format(a.options.format,null);var e=new Date(a._year,a._month,parseInt(c(this).html()));a.options.multiSelect?(c(this).toggleClass("selected"),c(this).hasClass("selected")?a._addDate(d):a._removeDate(d)):(b.find(".day a").removeClass("selected"),c(this).addClass("selected"),
a.element.data("_storage",[]),a._addDate(d));a.options.getDates(a.element.data("_storage"));a.options.click(d,e)})):"month"==this._mode?(b.find(".month a").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._month=parseInt(c(this).data("month"));a._mode="day";a._renderCalendar()}),b.find(".btn-previous-year").on("click",function(b){a._event="eventPrevious";b.preventDefault();b.stopPropagation();a._year-=1;a._renderCalendar()}),b.find(".btn-next-year").on("click",
function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._year+=1;a._renderCalendar()}),b.find(".btn-select-year").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._mode="year";a._renderCalendar()})):(b.find(".year a").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._year=parseInt(c(this).data("year"));a._mode="month";a._renderCalendar()}),b.find(".btn-previous-year").on("click",function(b){a._event="eventPrevious";
b.preventDefault();b.stopPropagation();a._distance-=10;a._renderCalendar()}),b.find(".btn-next-year").on("click",function(b){a._event="eventNext";b.preventDefault();b.stopPropagation();a._distance+=10;a._renderCalendar()}))},_addDate:function(a){0>this.element.data("_storage").indexOf(a)&&this.element.data("_storage").push(a)},_removeDate:function(a){a=this.element.data("_storage").indexOf(a);this.element.data("_storage").splice(a,1)},setDate:function(a){a=new Date(a);a=(new Date(a.getFullYear()+
"/"+(a.getMonth()+1)+"/"+a.getDate())).format("yyyy-mm-dd");this._addDate(a);this._renderCalendar()},getDate:function(a){return(new Date(void 0!=a?this.element.data("_storage")[a]:this.element.data("_storage")[0])).format(this.options.format)},getDates:function(){return this.element.data("_storage")},unsetDate:function(a){a=new Date(a);a=(new Date(a.getFullYear()+"-"+(a.getMonth()+1)+"-"+a.getDate())).format("yyyy-mm-dd");this._removeDate(a);this._renderCalendar()},_destroy:function(){},_setOption:function(a,
b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.datepicker",{version:"1.0.0",options:{format:"dd.mm.yyyy",date:void 0,effect:"none",position:"bottom",locale:c.Metro.currentLocale,weekStart:void 0!=METRO_WEEK_START?METRO_WEEK_START:0,otherDays:!1,selected:function(a,b){},_calendar:void 0},_create:function(){var a=this,b=this.element,d=b.children("input"),e=b.children("button.btn-date");void 0!=b.data("date")&&(this.options.date=b.data("date"));void 0!=b.data("format")&&(this.options.format=b.data("format"));void 0!=
b.data("effect")&&(this.options.effect=b.data("effect"));void 0!=b.data("position")&&(this.options.position=b.data("position"));void 0!=b.data("locale")&&(this.options.locale=b.data("locale"));void 0!=b.data("weekStart")&&(this.options.weekStart=b.data("weekStart"));void 0!=b.data("otherDays")&&(this.options.otherDays=b.data("otherDays"));this._createCalendar(b,this.options.date);d.attr("readonly",!0);e.attr("type","button");e.on("click",function(b){b.stopPropagation();"none"==a.options._calendar.css("display")?
a._show():a._hide()});b.on("click",function(b){b.stopPropagation();"none"==a.options._calendar.css("display")?a._show():a._hide()});c("html").on("click",function(a){c(".calendar-dropdown").hide()})},_createCalendar:function(a,b){var d,e=this;d=c("<div/>").css({position:"absolute",display:"none","max-width":260,"z-index":1E3}).addClass("calendar calendar-dropdown").appendTo(a);void 0!=e.options.date&&d.data("date",e.options.date);d.calendar({multiSelect:!1,format:e.options.format,buttons:!1,locale:e.options.locale,
otherDays:e.options.otherDays,weekStart:e.options.weekStart,click:function(b,c){d.calendar("setDate",c);a.children("input[type=text]").val(b);e.options.selected(b,c);e._hide()}});void 0!=b&&(d.calendar("setDate",b),a.children("input[type=text]").val(d.calendar("getDate")));switch(this.options.position){case "top":d.css({top:0-d.height(),left:0});break;default:d.css({top:"100%",left:0})}this.options._calendar=d},_show:function(){"slide"==this.options.effect?(c(".calendar-dropdown").slideUp("fast"),
this.options._calendar.slideDown("fast")):"fade"==this.options.effect?(c(".calendar-dropdown").fadeOut("fast"),this.options._calendar.fadeIn("fast")):(c(".calendar-dropdown").hide(),this.options._calendar.show())},_hide:function(){"slide"==this.options.effect?this.options._calendar.slideUp("fast"):"fade"==this.options.effect?this.options._calendar.fadeOut("fast"):this.options._calendar.hide()},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.carousel",{version:"1.0.0",options:{auto:!0,period:2E3,duration:500,effect:"slowdown",direction:"left",markers:{show:!0,type:"default",position:"left"},controls:!0,stop:!0,width:"100%",height:300},_slides:{},_currentIndex:0,_interval:0,_outPosition:0,_create:function(){var a=this,b=this.options,d=carousel=this.element,c=carousel.find(".controls");void 0!=d.data("auto")&&(b.auto=d.data("auto"));void 0!=d.data("period")&&(b.period=d.data("period"));void 0!=d.data("duration")&&
(b.duration=d.data("duration"));void 0!=d.data("effect")&&(b.effect=d.data("effect"));void 0!=d.data("direction")&&(b.direction=d.data("direction"));void 0!=d.data("width")&&(b.width=d.data("width"));void 0!=d.data("height")&&(b.height=d.data("height"));void 0!=d.data("stop")&&(b.stop=d.data("stop"));void 0!=d.data("controls")&&(b.controls=d.data("controls"));void 0!=d.data("markersShow")&&(b.markers.show=d.data("markersShow"));void 0!=d.data("markersType")&&(b.markers.type=d.data("markersType"));
void 0!=d.data("markersPosition")&&(b.markers.position=d.data("markersPosition"));carousel.css({width:this.options.width,height:this.options.height});this._slides=carousel.find(".slide");if(!(1>=this._slides.length)){!1!==this.options.markers&&(this.options.markers.show&&1<this._slides.length)&&this._markers(a);this.options.controls&&1<this._slides.length?(carousel.find(".controls.left").on("click",function(){a._slideTo("prior")}),carousel.find(".controls.right").on("click",function(){a._slideTo("next")})):
c.hide();if(this.options.stop)carousel.on("mouseenter",function(){clearInterval(a._interval)}).on("mouseleave",function(){a.options.auto&&(a._autoStart(),a.options.period)});this.options.auto&&this._autoStart()}},_autoStart:function(){var a=this;this._interval=setInterval(function(){"left"==a.options.direction?a._slideTo("next"):a._slideTo("prior")},this.options.period)},_slideTo:function(a){var b=this._slides[this._currentIndex];void 0==a&&(a="next");"prior"===a?(this._currentIndex-=1,0>this._currentIndex&&
(this._currentIndex=this._slides.length-1),this._outPosition=this.element.width()):"next"===a&&(this._currentIndex+=1,this._currentIndex>=this._slides.length&&(this._currentIndex=0),this._outPosition=-this.element.width());a=this._slides[this._currentIndex];switch(this.options.effect){case "switch":this._effectSwitch(b,a);break;case "slowdown":this._effectSlowdown(b,a,this.options.duration);break;case "fade":this._effectFade(b,a,this.options.duration);break;default:this._effectSlide(b,a,this.options.duration)}var d=
this;this.element.find(".markers ul li a").each(function(){c(this).data("num")===d._currentIndex?c(this).parent().addClass("active"):c(this).parent().removeClass("active")})},_slideToSlide:function(a){var b=this._slides[this._currentIndex],d=this._slides[a];this._outPosition=a>this._currentIndex?-this.element.width():this.element.width();switch(this.options.effect){case "switch":this._effectSwitch(b,d);break;case "slowdown":this._effectSlowdown(b,d,this.options.duration);break;case "fade":this._effectFade(b,
d,this.options.duration);break;default:this._effectSlide(b,d,this.options.duration)}this._currentIndex=a},_markers:function(a){var b,d,e,f;b=c('<div class="markers '+this.options.markers.type+'" />');d=c("<ul></ul>").appendTo(b);for(f=0;f<this._slides.length;f++)e=c('<li><a href="javascript:void(0)" data-num="'+f+'"></a></li>'),0===f&&e.addClass("active"),e.appendTo(d);d.find("li a").removeClass("active").on("click",function(){var b=c(this),e=b.data("num");d.find("li").removeClass("active");b.parent().addClass("active");
if(e==a._currentIndex)return!0;a._slideToSlide(e);return!0});b.appendTo(this.element);switch(this.options.markers.position){case "top-left":b.css({left:"10px",right:"auto",bottom:"auto",top:"10px"});break;case "top-right":b.css({left:"auto",right:"10px",bottom:"auto",top:"0px"});break;case "top-center":b.css({left:this.element.width()/2-b.width()/2,right:"auto",bottom:"auto",top:"0px"});break;case "bottom-left":b.css({left:"10px",right:"auto"});break;case "bottom-right":b.css({right:"10px",left:"auto"});
break;case "bottom-center":b.css({left:this.element.width()/2-b.width()/2,right:"auto"})}},_effectSwitch:function(a,b){c(a).hide();c(b).css({left:0}).show()},_effectSlide:function(a,b,d){c(a).animate({left:this._outPosition},d);c(b).css("left",-1*this._outPosition).show().animate({left:0},d)},_effectSlowdown:function(a,b,d){d={duration:d,easing:"doubleSqrt"};c.easing.doubleSqrt=function(a){return Math.sqrt(Math.sqrt(a))};c(a).animate({left:this._outPosition},d);c(b).css("left",-1*this._outPosition).show().animate({left:0},
d)},_effectFade:function(a,b,d){c(a).fadeOut(d);c(b).css({left:0}).fadeIn(d)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.countdown",{version:"1.0.0",options:{style:{background:"bg-dark",foreground:"fg-white",divider:"fg-dark"},blink:!0,days:1,stoptimer:0,ontick:function(a,b,d,c){},onstop:function(){}},wrappers:{},_interval:0,_create:function(){var a=this,b=this.element;c.each(["Days","Hours","Minutes","Seconds"],function(){c('<span class="count'+this+'">').html('<span class="digit-wrapper">                        <span class="digit">0</span>                    </span>                    <span class="digit-wrapper">                        <span class="digit">0</span>                    </span>').appendTo(b);
"Seconds"!=this&&b.append('<span class="divider"></span>')});this.wrappers=this.element.find(".digit-wrapper");void 0!=b.data("blink")&&(this.options.blink=b.data("blick"));void 0!=b.data("styleBackground")&&(this.options.style.background=b.data("styleBackground"));void 0!=b.data("styleForeground")&&(this.options.style.foreground=b.data("styleForeground"));void 0!=b.data("styleDivider")&&(this.options.style.divider=b.data("styleDivider"));"default"!=this.options.style.background&&this.element.find(".digit").addClass(this.options.style.background);
"default"!=this.options.style.foreground&&this.element.find(".digit").addClass(this.options.style.foreground);"default"!=this.options.style.divider&&this.element.find(".divider").addClass(this.options.style.divider);void 0!=b.data("stoptimer")&&(this.options.stoptimer=new Date(b.data("stoptimer")));0==this.options.stoptimer&&(this.options.stoptimer=(new Date).getTime()+864E5*this.options.days);setTimeout(function(){a.tick()},1E3)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",
a,b)},tick:function(){var a=this;this._interval=setInterval(function(){var b,d,c,f;b=Math.floor((a.options.stoptimer-new Date)/1E3);0>b&&(b=0);d=Math.floor(b/86400);a.updateDuo(0,1,d);b-=86400*d;c=Math.floor(b/3600);a.updateDuo(2,3,c);b-=3600*c;f=Math.floor(b/60);a.updateDuo(4,5,f);b-=60*f;a.updateDuo(6,7,b);a.options.ontick(d,c,f,b);a.blinkDivider();0===d&&(0===c&&0===f&&0===b)&&(a.options.onstop(),a.stopDigit(),a._trigger("alarm"),clearInterval(a._interval))},1E3)},blinkDivider:function(){this.options.blink&&
this.element.find(".divider").toggleClass("no-visible")},stopDigit:function(){this.wrappers.each(function(){c(this).children(".digit").addClass("stop")})},updateDuo:function(a,b,d){this.switchDigit(this.wrappers.eq(a),Math.floor(d/10)%10);this.switchDigit(this.wrappers.eq(b),d%10)},switchDigit:function(a,b){var d=a.find(".digit");if(d.is(":animated")||a.data("digit")==b)return!1;a.data("digit",b);var e=c("<span>",{"class":"digit",css:{top:"-2.1em",opacity:0},html:b});e.addClass(this.options.style.background);
e.addClass(this.options.style.foreground);d.before(e).removeClass("static").animate({top:"2.5em"},"fast",function(){d.remove()});e.delay(100).animate({top:0,opacity:1},"fast");return!0}})})(jQuery);(function(c){c.widget("metro.dropdown",{version:"1.0.1",options:{effect:"slide",toggleElement:!1},_create:function(){var a=this,b=this.element,d=this.name,e=this.element.parent(),e=this.options.toggleElement||e.children(".dropdown-toggle");void 0!=b.data("effect")&&(this.options.effect=b.data("effect"));e.on("click."+d,function(d){d.preventDefault();d.stopPropagation();"block"!=b.css("display")||b.hasClass("keep-open")?(c(".dropdown-menu").each(function(d,e){b.parents(".dropdown-menu").is(e)||(c(e).hasClass("keep-open")||
"block"!=c(e).css("display"))||a._close(e)}),a._open(b)):a._close(b)});c(b).find("li.disabled a").on("click",function(a){a.preventDefault()})},_open:function(a){switch(this.options.effect){case "fade":c(a).fadeIn("fast");break;case "slide":c(a).slideDown("fast");break;default:c(a).show()}this._trigger("onOpen",null,a)},_close:function(a){switch(this.options.effect){case "fade":c(a).fadeOut("fast");break;case "slide":c(a).slideUp("fast");break;default:c(a).hide()}this._trigger("onClose",null,a)},_destroy:function(){},
_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.inputControl",{version:"1.0.0",options:{},_create:function(){var a=this.element;a.hasClass("text")?this.initTextInput(a,this):a.hasClass("password")?this.initPasswordInput(a,this):a.hasClass("checkbox")||a.hasClass("radio")||a.hasClass("switch")?this.initCheckboxInput(a,this):a.hasClass("file")&&this.initFileInput(a,this)},initCheckboxInput:function(a,b){},initFileInput:function(a,b){var d,e,f;f=c("<input type='text' id='__input_file_wrapper__' readonly style='z-index: 1; cursor: default;'>");
d=a.children(".btn-file");e=a.children('input[type="file"]');e.css("z-index",0);f.insertAfter(e);e.attr("tabindex","-1");d.attr("type","button");e.on("change",function(){var a=c(this).val();""!=a&&f.val(a)});d.on("click",function(){e.trigger("click")})},initTextInput:function(a,b){var d=a.children(".btn-clear"),e=a.children("input[type=text]");if(0!=d.length&&(d.attr("tabindex","-1"),d.attr("type","button"),d.on("click",function(){e=a.children("input");e.prop("readonly")||(e.val(""),e.focus(),b._trigger("onClear",
null,a))}),!e.attr("disabled")))e.on("click",function(){c(this).focus()})},initPasswordInput:function(a,b){var d=a.children(".btn-reveal"),e=a.children("input[type=password]");if(0!=d.length&&(d.attr("tabindex","-1"),d.attr("type","button"),d.on("mousedown",function(a){e.attr("type","text")}),d.on("mouseup, mouseleave, blur",function(a){e.attr("type","password").focus()}),!e.attr("disabled")))e.on("click",function(){c(this).focus()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",
a,b)}})})(jQuery);
(function(c){c.widget("metro.inputTransform",{version:"1.0.0",options:{transformType:"text"},_create:function(){var a=this.element,b;if(!a.parent().hasClass("input-control")){b=a.get(0).tagName.toLowerCase();"textarea"==b?this.options.transformType="textarea":"select"==b?this.options.transformType="select":void 0!=a.data("transformType")?this.options.transformType=a.data("transformType"):this.options.transformType=a.attr("type");a=void 0;switch(this.options.transformType){case "password":a=this._createInputPassword();
break;case "file":a=this._createInputFile();break;case "checkbox":a=this._createInputCheckbox();break;case "radio":a=this._createInputRadio();break;case "switch":a=this._createInputSwitch();break;case "select":a=this._createInputSelect();break;case "textarea":a=this._createInputTextarea();break;case "search":a=this._createInputSearch();break;case "email":a=this._createInputEmail();break;case "tel":a=this._createInputTel();break;case "number":a=this._createInputNum();break;default:a=this._createInputText()}a.inputControl()}},
_createInputTextarea:function(){var a=this.element,b=c("<div/>").addClass("input-control").addClass("textarea"),d=a.clone(!0);a.parent();d.appendTo(b);b.insertBefore(a);a.remove();return b},_createInputSelect:function(){var a=this.element,b=c("<div/>").addClass("input-control").addClass("select"),d=a.clone(!0);a.parent();d.val(a.val()).appendTo(b);b.insertBefore(a);a.remove();return b},_createInputSwitch:function(){var a=this.element,b=c("<div/>").addClass("input-control").addClass("switch"),d=c("<label/>"),
e=c("<span/>").addClass("check"),f=a.clone(!0);a.parent();var g=c("<span/>").addClass("caption").html(void 0!=a.data("caption")?a.data("caption"):"");d.appendTo(b);f.appendTo(d);e.appendTo(d);g.appendTo(d);b.insertBefore(a);a.remove();return b},_createInputCheckbox:function(){var a=this.element,b=c("<div/>").addClass("input-control").addClass("checkbox"),d=c("<label/>"),e=c("<span/>").addClass("check"),f=a.clone(!0);a.parent();var g=c("<span/>").addClass("caption").html(void 0!=a.data("caption")?
a.data("caption"):"");d.appendTo(b);f.appendTo(d);e.appendTo(d);g.appendTo(d);b.insertBefore(a);a.remove();return b},_createInputRadio:function(){var a=this.element,b=c("<div/>").addClass("input-control").addClass("radio"),d=c("<label/>"),e=c("<span/>").addClass("check"),f=a.clone(!0);a.parent();var g=c("<span/>").addClass("caption").html(void 0!=a.data("caption")?a.data("caption"):"");d.appendTo(b);f.appendTo(d);e.appendTo(d);g.appendTo(d);b.insertBefore(a);a.remove();return b},_createInputSearch:function(){return this._createInputVal("text",
"btn-search")},_createInputNum:function(){return this._createInputVal("number","btn-clear")},_createInputTel:function(){return this._createInputVal("tel","btn-clear")},_createInputEmail:function(){return this._createInputVal("email","btn-clear")},_createInputText:function(){return this._createInputVal("text","btn-clear")},_createInputPassword:function(){return this._createInputVal("password","btn-reveal")},_createInputFile:function(){return this._createInputVal("file","btn-file")},_createInputVal:function(a,
b){var d=this.element,e=c("<div/>").addClass("input-control").addClass(a),f=c("<button/>").addClass(b),g=d.clone(!0);d.parent();g.appendTo(e);f.appendTo(e);e.insertBefore(d);d.remove();return e},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.livetile",{version:"1.0.0",options:{effect:"slideLeft",period:4E3,duration:700,easing:"doubleSqrt"},_frames:{},_currentIndex:0,_interval:0,_outPosition:0,_size:{},_create:function(){var a=this,b=this.element;void 0!=b.data("effect")&&(this.options.effect=b.data("effect"));void 0!=b.data("direction")&&(this.options.direction=b.data("direction"));void 0!=b.data("period")&&(this.options.period=b.data("period"));void 0!=b.data("duration")&&(this.options.duration=b.data("duration"));
void 0!=b.data("easing")&&(this.options.easing=b.data("easing"));this._frames=b.children("[class*='-content']");1>=this._frames.length||(c.easing.doubleSqrt=function(a){return Math.sqrt(Math.sqrt(a))},this._size={width:b.width(),height:b.height()},b.on("mouseenter",function(){a.stop()}),b.on("mouseleave",function(){a.start()}),this.start())},start:function(){var a=this;this._interval=setInterval(function(){a._animate()},this.options.period)},stop:function(){clearInterval(this._interval)},_animate:function(){var a=
this._frames[this._currentIndex],b;this._currentIndex+=1;this._currentIndex>=this._frames.length&&(this._currentIndex=0);b=this._frames[this._currentIndex];switch(this.options.effect){case "slideLeft":this._effectSlideLeft(a,b);break;case "slideRight":this._effectSlideRight(a,b);break;case "slideDown":this._effectSlideDown(a,b);break;case "slideUpDown":this._effectSlideUpDown(a,b);break;case "slideLeftRight":this._effectSlideLeftRight(a,b);break;default:this._effectSlideUp(a,b)}},_effectSlideLeftRight:function(a,
b){0==this._currentIndex%2?this._effectSlideLeft(a,b):this._effectSlideRight(a,b)},_effectSlideUpDown:function(a,b){0==this._currentIndex%2?this._effectSlideUp(a,b):this._effectSlideDown(a,b)},_effectSlideUp:function(a,b){var d=this._size.height,e={duration:this.options.duration,easing:this.options.easing};c(a).animate({top:-d},e);c(b).css({top:d}).show().animate({top:0},e)},_effectSlideDown:function(a,b){var d=this._size.height,e={duration:this.options.duration,easing:this.options.easing};c(a).animate({top:d},
e);c(b).css({top:-d}).show().animate({top:0},e)},_effectSlideLeft:function(a,b){var d=this._size.width,e={duration:this.options.duration,easing:this.options.easing};c(a).animate({left:-1*d},e);c(b).css({left:d}).show().animate({left:0},e)},_effectSlideRight:function(a,b){var d=this._size.width,e={duration:this.options.duration,easing:this.options.easing};c(a).animate({left:d},e);c(b).css({left:-d}).show().animate({left:0},e)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",
a,b)}})})(jQuery);(function(c){c.widget("metro.dragtile",{version:"1.0.0",options:{},_create:function(){var a=(tile=this.element).parents(".tile-area");a.find(".tile");a.find(".tile-group");tile.attr("draggable","true");addTouchEvents(tile[0]);tile[0].addEventListener("dragstart",this._dragStart,!1);tile[0].addEventListener("dragend",this._dragEnd,!1);tile.on("mousedown",function(a){});tile.on("mouseup",function(a){})},_dragStart:function(a){c(this).css("opacity",0.4)},_dragEnd:function(a){c(this).css("opacity",1)},
_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.progressbar",{version:"1.0.1",options:{value:0,color:"bg-cyan",animate:!1,max:100,onchange:function(a){}},_create:function(){var a=this.element,b=this.options;void 0!=a.data("value")&&this.value(a.data("value")+"%");void 0!=a.data("color")&&(b.color=a.data("color"));void 0!=a.data("animate")&&(b.animate=a.data("animate"));void 0!=a.data("max")&&(b.max=a.data("max"));b.max=0>b.max?0:b.max;b.max=100<b.max?100:b.max;this._showBar()},_showBar:function(a){a=a||this.options.value;
var b=this.element;b.html("");var d=c("<div />");d.addClass("bar");this.options.color.indexOf("bg-")+1?d.addClass(this.options.color):d.css("background",this.options.color);d.appendTo(b);this.options.animate?d.css("width",this.value()+"%").animate({width:a+"%"}):d.css("width",a+"%");this.options.onchange(this.value())},value:function(a){if(void 0!=a)a=parseInt(a),a=a>this.max?this.max:a,a=0>a?0:a,this._showBar(a),this.options.value=a;else return parseInt(this.options.value)},color:function(a){this.options.color=
a;this.options.color.indexOf("bg-")+1?this.element.find(".bar").addClass(this.options.color):this.element.find(".bar").css("background",this.options.color);this._showBar()},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.rating",{version:"1.0.0",options:{score:0,half:!1,stars:5,"static":!0,hints:["bad","poor","regular","good","gorgeous"],showHint:!1,showScore:!1,scoreHint:"Current score: ",click:function(a,b){}},_create:function(){var a=this.element;void 0!=a.data("score")&&(this.options.score=a.data("score"));void 0!=a.data("half")&&(this.options.half=a.data("half"));void 0!=a.data("stars")&&(this.options.stars=a.data("stars"));void 0!=a.data("showHint")&&(this.options.showHint=a.data("showHint"));
void 0!=a.data("static")&&(this.options.static=a.data("static"));void 0!=a.data("showScore")&&(this.options.showScore=a.data("showScore"));void 0!=a.data("scoreHint")&&(this.options.scoreHint=a.data("scoreHint"));this._showRating()},rate:function(a){this.options.score=a;this._showRating()},_showRating:function(){var a=this,b=this.element,d=this.options,e,f;b.addClass("rating");b.html("");e=c("<ul/>");d.static||b.addClass("active");for(var g=0;g<d.stars;g++)f=c("<li/>"),f.data("value",g+1),d.showHint&&
f.attr("title",d.hints[g]),g<=d.score-1&&f.addClass("rated"),f.on("click",function(){d.click(c(this).data("value"),a)}),f.appendTo(e);e.appendTo(b);d.showScore?(c("<span/>").addClass("score-hint").html(d.scoreHint+d.score).appendTo(b),b.css("height","auto")):b.find("ul").css("margin-bottom",0)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.slider",{version:"1.0.2",options:{position:0,accuracy:0,color:"default",completeColor:"default",markerColor:"default",colors:[],showHint:!1,change:function(a,b){},changed:function(a,b){},min:0,max:100,animate:!0,_slider:{vertical:!1,offset:0,length:0,marker:0,ppp:0,start:0,stop:0}},_create:function(){var a=this,b=this.element,d=this.options,c=this.options._slider;void 0!=b.data("accuracy")&&(d.accuracy=0<b.data("accuracy")?b.data("accuracy"):0);void 0!=b.data("animate")&&
(d.animate=b.data("animate"));void 0!=b.data("min")&&(d.min=b.data("min"));d.min=0>d.min?0:d.min;d.min=d.min>d.max?d.max:d.min;void 0!=b.data("max")&&(d.max=b.data("max"));d.max=100<d.max?100:d.max;d.max=d.max<d.min?d.min:d.max;void 0!=b.data("position")&&(d.position=this._correctValue(b.data("position")>this.options.min?b.data("position")>this.options.max?this.options.max:b.data("position"):this.options.min));void 0!=b.data("color")&&(d.color=b.data("color"));void 0!=b.data("completeColor")&&(d.completeColor=
b.data("completeColor"));void 0!=b.data("markerColor")&&(d.markerColor=b.data("markerColor"));void 0!=b.data("colors")&&(d.colors=b.data("colors").split(","));void 0!=b.data("showHint")&&(d.showHint=b.data("showHint"));c.vertical=b.hasClass("vertical");this._createSlider();this._initPoints();this._placeMarker(d.position);addTouchEvents(b[0]);b.children(".marker").on("mousedown",function(b){b.preventDefault();a._startMoveMarker(b)});b.on("mousedown",function(b){b.preventDefault();a._startMoveMarker(b)})},
_startMoveMarker:function(a){var b=this.element,d=this.options,e=this,f=b.children(".hint");c(document).mousemove(function(a){e._movingMarker(a);b.hasClass("permanent-hint")||f.css("display","block")});c(document).mouseup(function(){c(document).off("mousemove");c(document).off("mouseup");b.data("value",e.options.position);b.trigger("changed",e.options.position);d.changed(e.options.position,b);b.hasClass("permanent-hint")||f.css("display","none")});this._initPoints();this._movingMarker(a)},_movingMarker:function(a){var b;
b=this.options._slider.vertical;var d=this.options._slider.offset,c=this.options._slider.start,f=this.options._slider.stop,g=this.options._slider.length,h=this.options._slider.marker;a=b?a.pageY-d:a.pageX-d;a<c?a=c:a>f&&(a=f);b=this._pixToPerc(b?g-a-h/2:a-h/2);this._placeMarker(b);this.options.position=b;this.options.change(Math.round(b),this.element)},_placeMarker:function(a){var b,d,c=this.options,f=0,g=f=0,h=this.element.children(".marker"),k=this.element.children(".complete"),l=this.element.children(".hint");
d=this._percToPix(this.options.position);f=c.colors.length;g=c._slider.length/f;if(this.options._slider.vertical){var n=this._percToPix(this.options.position)+this.options._slider.marker,p=this.options._slider.length-n;b=this._percToPix(a)+this.options._slider.marker;d=this.options._slider.length-b;this._animate(h.css("top",p),{top:d});this._animate(k.css("height",n),{height:b});f&&(f=Math.round(b/g)-1,k.css("background-color",c.colors[0>f?0:f]));c.showHint&&l.html(Math.round(a)).css("top",d-l.height()/
2)}else b=this._percToPix(a),this._animate(h.css("left",d),{left:b}),this._animate(k.css("width",d),{width:b}),f&&(f=Math.round(b/g)-1,k.css("background-color",c.colors[0>f?0:f])),c.showHint&&this._animate(l.html(Math.round(a)).css("left",d-l.width()/2),{left:b-l.width()/2})},_animate:function(a,b){this.options.animate?a.stop(!0).animate(b):a.css(b)},_pixToPerc:function(a){return this._correctValue(a*this.options._slider.ppp)},_percToPix:function(a){return 0===this.options._slider.ppp?0:a/this.options._slider.ppp},
_correctValue:function(a){var b=this.options.accuracy,d=this.options.max,c=this.options.min;if(0===b)return a;if(a===d)return d;if(a===c)return c;a=Math.floor(a/b)*b+Math.round(a%b/b)*b;return a>d?d:a<c?c:a},_initPoints:function(){var a=this.options._slider,b=this.element;a.vertical?(a.offset=b.offset().top,a.length=b.height(),a.marker=b.children(".marker").height()):(a.offset=b.offset().left,a.length=b.width(),a.marker=b.children(".marker").width());a.ppp=this.options.max/(a.length-a.marker);a.start=
a.marker/2;a.stop=a.length-a.marker/2},_createSlider:function(){var a=this.element,b=this.options,d,e;a.html("");d=c("<div/>").addClass("complete").appendTo(a);e=c("<a/>").addClass("marker").appendTo(a);b.showHint&&c("<span/>").addClass("hint").appendTo(a);"default"!=b.color&&a.css("background-color",b.color);"default"!=b.completeColor&&d.css("background-color",b.completeColor);"default"!=b.markerColor&&e.css("background-color",b.markerColor)},value:function(a){a=a>this.options.max?this.options.max:
a;a=a<this.options.min?this.options.min:a;return"undefined"!==typeof a?(this._placeMarker(parseInt(a)),this.options.position=parseInt(a),this.options.change(Math.round(parseInt(a)),this.element),this):Math.round(this.options.position)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.tabcontrol",{version:"1.0.0",options:{effect:"none",activateStoredTab:!1,tabclick:function(a){},tabchange:function(a){}},_create:function(){var a=this,b=this.element,d=c(b.children(".tabs")).children("li"),e=c(b.children(".frames")).children(".frame"),f=b.attr("id");void 0!=b.data("effect")&&(this.options.effect=b.data("effect"));this.init(d,e);b.on("click",".tabs > li > a",function(d){d.preventDefault();d.stopPropagation();a.options.tabclick(this);if(c(this).parent().hasClass("disabled"))return!1;
b.children(".tabs").children("li").removeClass("active");b.children(".frames").children(".frame").hide();c(this).parent().addClass("active");d=c(c(this).attr("href"));switch(a.options.effect){case "slide":d.slideDown();break;case "fade":d.fadeIn();break;default:d.show()}a._trigger("change",null,d);a.options.tabchange(this);void 0!=f&&window.localStorage.setItem(f+"-current-tab",c(this).attr("href"));return!0});this.options.activateStoredTab&&this._activateStoredTab(d)},init:function(a,b){var d=this;
a.each(function(){if(c(this).hasClass("active")){var a=c(c(c(this).children("a")).attr("href"));b.hide();a.show();d._trigger("change",null,a)}})},_activateStoredTab:function(a){var b=window.localStorage.getItem(this.element.attr("id")+"-current-tab");void 0!=b&&a.each(function(){var a=c(this).children("a");a.attr("href")==b&&a.click()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.tablecontrol",{version:"1.0.0",options:{width:"100%",height:"auto",cls:"table",checkRow:!1,colModel:[],data:[]},_create:function(){var a=this.element;a.css({width:this.options.width,height:this.options.height});a=this.createTable(a);this.addHeader(a,this.options.colModel);this.addTableData(a,this.options.data);a.addClass(this.options.cls)},addHeader:function(a,b){var d=c("<thead/>").appendTo(a),e=c("<tr/>").appendTo(d);c.each(b,function(a,b){c("<th/>").addClass(b.hcls).html(b.caption).appendTo(e)})},
createTable:function(a){return c("<table/>").appendTo(a)},addTableData:function(a,b){var d=this,e=c("<tbody/>").appendTo(a);c.each(b,function(a,b){d.addRowData(e,b)})},addRowData:function(a,b){var d=c("<tr/>").appendTo(a);void 0!=b.__row_class&&d.addClass(b.__row_class);c.each(this.options.colModel,function(a,f){c("<td/>").css("width",f.width).addClass(f.cls).html(b[f.field]).appendTo(d)})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);$(function(){$("[data-role=table]").tablecontrol()});(function(c){c.widget("metro.times",{version:"1.0.0",options:{style:{background:"bg-dark",foreground:"fg-white",divider:"fg-dark"},blink:!0,alarm:{h:0,m:0,s:0},ontick:function(a,b,d){},onalarm:function(){}},wrappers:{},_interval:0,_create:function(){var a=this,b=this.element;c.each(["Hours","Minutes","Seconds"],function(){c('<span class="count'+this+'">').html('<span class="digit-wrapper">                        <span class="digit">0</span>                    </span>                    <span class="digit-wrapper">                        <span class="digit">0</span>                    </span>').appendTo(b);
"Seconds"!=this&&b.append('<span class="divider"></span>')});this.wrappers=this.element.find(".digit-wrapper");void 0!=b.data("blink")&&(this.options.blink=b.data("blick"));void 0!=b.data("styleBackground")&&(this.options.style.background=b.data("styleBackground"));void 0!=b.data("styleForeground")&&(this.options.style.foreground=b.data("styleForeground"));void 0!=b.data("styleDivider")&&(this.options.style.divider=b.data("styleDivider"));"default"!=this.options.style.background&&this.element.find(".digit").addClass(this.options.style.background);
"default"!=this.options.style.foreground&&this.element.find(".digit").addClass(this.options.style.foreground);"default"!=this.options.style.divider&&this.element.find(".divider").addClass(this.options.style.divider);if(void 0!=b.data("alarm")){var d=b.data("alarm").split(":");this.options.alarm.h=void 0!=d[0]?d[0]:0;this.options.alarm.m=void 0!=d[1]?d[1]:0;this.options.alarm.s=void 0!=d[2]?d[2]:0}b.data("onalarm");setTimeout(function(){a.tick()},1E3)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",
a,b)},tick:function(){var a=this;this._interval=setInterval(function(){var b=new Date,d,c;d=b.getHours();a.updateDuo(0,1,d);c=b.getMinutes();a.updateDuo(2,3,c);b=b.getSeconds();a.updateDuo(4,5,b);a.options.ontick(d,c,b);a.blinkDivider();var f=a.options.alarm;f&&(void 0!=f.h&&f.h==d&&void 0!=f.m&&f.m==c&&void 0!=f.s&&f.s==b)&&(a.options.onalarm(),a._trigger("alarm"))},1E3)},blinkDivider:function(){this.options.blink&&this.element.find(".divider").toggleClass("no-visible")},updateDuo:function(a,b,d){this.switchDigit(this.wrappers.eq(a),
Math.floor(d/10)%10);this.switchDigit(this.wrappers.eq(b),d%10)},switchDigit:function(a,b){var d=a.find(".digit");if(d.is(":animated")||a.data("digit")==b)return!1;a.data("digit",b);var e=c("<span>",{"class":"digit",css:{top:"-2.1em",opacity:0},html:b});e.addClass(this.options.style.background);e.addClass(this.options.style.foreground);d.before(e).removeClass("static").animate({top:"2.5em",opacity:0},"fast",function(){d.remove()});e.delay(100).animate({top:0,opacity:1},"fast");return!0}})})(jQuery);(function(c){c.Dialog=function(a){if(c.Dialog.opened)return METRO_DIALOG;c.Dialog.opened=!0;c.Dialog.settings=a;a=c.extend({icon:!1,title:"",content:"",flat:!1,shadow:!1,overlay:!1,width:"auto",height:"auto",position:"default",padding:!1,overlayClickClose:!0,sysButtons:{btnClose:!0},onShow:function(a){},sysBtnCloseClick:function(a){},sysBtnMinClick:function(a){},sysBtnMaxClick:function(a){}},a);var b,d,e,f;b=c("<div/>").addClass("metro window-overlay");a.overlay&&b.css({backgroundColor:"rgba(0,0,0,.7)"});
d=c("<div/>").addClass("window");a.flat&&d.addClass("flat");a.shadow&&d.addClass("shadow").css("overflow","hidden");e=c("<div/>").addClass("caption");f=c("<div/>").addClass("content");f.css({paddingTop:32+a.padding,paddingLeft:a.padding,paddingRight:a.padding,paddingBottom:a.padding});a.sysButtons&&(a.sysButtons.btnClose&&c("<button/>").addClass("btn-close").on("click",function(b){b.preventDefault();b.stopPropagation();c.Dialog.close();a.sysBtnCloseClick(b)}).appendTo(e),a.sysButtons.btnMax&&c("<button/>").addClass("btn-max").on("click",
function(b){b.preventDefault();b.stopPropagation();a.sysBtnMaxClick(b)}).appendTo(e),a.sysButtons.btnMin&&c("<button/>").addClass("btn-min").on("click",function(b){b.preventDefault();b.stopPropagation();a.sysBtnMinClick(b)}).appendTo(e));a.icon&&c(a.icon).addClass("icon").appendTo(e);c("<div/>").addClass("title").html(a.title).appendTo(e);f.html(a.content);e.appendTo(d);f.appendTo(d);d.appendTo(b);"auto"!=a.width&&d.css("min-width",a.width);"auto"!=a.height&&d.css("min-height",a.height);b.hide().appendTo("body").fadeIn("fast");
METRO_DIALOG=d;d.css("position","fixed").css("z-index",parseInt(b.css("z-index"))+1).css("top",(c(window).height()-METRO_DIALOG.outerHeight())/2).css("left",(c(window).width()-d.outerWidth())/2);addTouchEvents(d[0]);if(a.draggable)e.on("mousedown",function(a){c.Dialog.drag=!0;e.css("cursor","move");d.css("z-index");var b=d.outerHeight(),f=d.outerWidth(),l=d.offset().top+b-a.pageY,n=d.offset().left+f-a.pageX;d.css("z-index",99999).parents().on("mousemove",function(a){var e=0<a.pageY?a.pageY+l-b:0;
a=0<a.pageX?a.pageX+n-f:0;c.Dialog.drag&&(0<=e&&e<=window.innerHeight-d.outerHeight()&&d.offset({top:e}),0<=a&&a<=window.innerWidth-d.outerWidth()&&d.offset({left:a}))});a.preventDefault()}).on("mouseup",function(){d.removeClass("draggable");c.Dialog.drag=!1;e.css("cursor","default")});d.on("click",function(a){a.stopPropagation()});if(a.overlayClickClose)b.on("click",function(a){a.preventDefault();c.Dialog.close()});a.onShow(METRO_DIALOG);c.Dialog.autoResize();return METRO_DIALOG};c.Dialog.content=
function(a){return c.Dialog.opened&&void 0!=METRO_DIALOG?a?(METRO_DIALOG.children(".content").html(a),c.Dialog.autoResize(),!0):METRO_DIALOG.children(".content").html():!1};c.Dialog.title=function(a){if(!c.Dialog.opened||void 0==METRO_DIALOG)return!1;var b=METRO_DIALOG.children(".caption").children(".title");a?b.html(a):b.html();return!0};c.Dialog.autoResize=function(){if(!c.Dialog.opened||void 0==METRO_DIALOG)return!1;var a=METRO_DIALOG.children(".content"),b=(c(window).height()-METRO_DIALOG.outerHeight())/
2,d=(c(window).width()-METRO_DIALOG.outerWidth())/2;METRO_DIALOG.css({width:a.outerWidth(),height:a.outerHeight(),top:b,left:d});return!0};c.Dialog.close=function(){if(!c.Dialog.opened||void 0==METRO_DIALOG)return!1;c.Dialog.opened=!1;METRO_DIALOG.parent(".window-overlay").fadeOut(function(){c(this).remove()});return!1}})(jQuery);(function(c){var a=!1,b=[],d={_container:null,_notify:null,_timer:null,options:{icon:"",caption:"",content:"",shadow:!0,width:"auto",height:"auto",style:!1,position:"right",timeout:3E3},init:function(a){this.options=c.extend({},this.options,a);this._build();return this},_build:function(){a=this._container=a||c("<div/>").addClass("metro notify-container").appendTo("body");var d=this.options;if(""==d.content||void 0==d.content)return!1;this._notify=c("<div/>").addClass("notify");d.shadow&&this._notify.addClass("shadow");
d.style&&void 0!=d.style.background&&this._notify.css("background-color",d.style.background);d.style&&void 0!=d.style.color&&this._notify.css("color",d.style.color);""!=d.caption&&void 0!=d.caption&&c("<div/>").addClass("caption").html(d.caption).appendTo(this._notify);""!=d.content&&void 0!=d.content&&c("<div/>").addClass("content").html(d.content).appendTo(this._notify);"auto"!=d.width&&this._notify.css("min-width",d.width);"auto"!=d.height&&this._notify.css("min-height",d.height);this._notify.hide().appendTo(this._container).fadeIn("slow");
b.push(this._notify);this.close(d.timeout)},close:function(a){this.clear();if(a==parseInt(a)){var b=this;this._timer=setTimeout(function(){b._timer=null;b._hide()},a)}else if(void 0==a)return this._hide();return this},clear:function(){return null!=this._timer?(clearTimeout(this._timer),this._timer=null,this):!1},_hide:function(){this.clear();return void 0!=this._notify?(this._notify.hide("slow",function(){c(this).remove();b.splice(b.indexOf(this._notify),1)}),this):!1},closeAll:function(){b.forEach(function(a){a.hide("slow",
function(){a.remove();b.splice(b.indexOf(a),1)})});return this}};c.Notify=function(a){return Object.create(d).init(a)};c.Notify.show=function(a,b){return c.Notify({content:a,caption:b})}})(jQuery);(function(c){c.widget("metro.listview",{version:"1.0.0",options:{onGroupExpand:function(a){},onGroupCollapse:function(a){},onListClick:function(a){}},_create:function(){var a=this,b=this.element;b.children(".collapsed").children(".group-content").hide();b.find(".group-title").on("click",function(b){var e=c(this).parent(".list-group"),f=e.children(".group-content");e.toggleClass("collapsed");e.hasClass("collapsed")?(f.slideUp(),a.options.onGroupCollapse(e)):(f.slideDown(),a.options.onGroupExpand(e));
b.preventDefault()});b.find(".list").on("click",function(d){b.find(".list").removeClass("active");c(this).toggleClass("active");a.options.onListClick(c(this));d.preventDefault()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.treeview",{version:"1.0.0",options:{onNodeClick:function(a){},onNodeCollapsed:function(a){},onNodeExpanded:function(a){}},_create:function(){var a=this,b=this.element;b.find(".node.collapsed").find("ul").hide();b.find(".node-toggle").on("click",function(b){var e=c(this).parent().parent("li");e.hasClass("keep-open")||(e.toggleClass("collapsed"),e.hasClass("collapsed")?(e.children("ul").fadeOut("fast"),a.options.onNodeCollapsed(e)):(e.children("ul").fadeIn("fast"),a.options.onNodeExpanded(e)),
a.options.onNodeClick(e),b.preventDefault(),b.stopPropagation())});b.find("a").each(function(){var a=c(this);a.css({paddingLeft:10*(a.parents("ul").length-1)})});b.find("a").on("click",function(d){var e=c(this),f=e.parent("li");b.find("a").removeClass("active");e.toggleClass("active");a.options.onNodeClick(f);d.preventDefault()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.fluentmenu",{version:"1.0.0",options:{onSpecialClick:function(a,b){},onTabClick:function(a,b){}},_create:function(){var a=this,b=this.element,d=this.options,e=b.find(".tabs-holder > li > a");this._hidePanels();this._showPanel();e.on("click",function(e){if(c(this).parent("li").hasClass("special"))d.onSpecialClick(e,c(this));else{var g=c(c(this).attr("href"));a._hidePanels();a._showPanel(g);b.find(".tabs-holder > li").removeClass("active");c(this).parent("li").addClass("active");
d.onTabClick(e,c(this))}e.preventDefault()})},_hidePanels:function(){this.element.find(".tab-panel").hide()},_showPanel:function(a){void 0==a&&(a=this.element.find(".tabs-holder li.active a").attr("href"));c(a).show()},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.hint",{version:"1.0.0",options:{position:"bottom",background:"#FFFCC0",shadow:!1,border:!1,mode:1,_hint:void 0},_create:function(){var a=this,b=this.options;this.element.on("mouseenter",function(d){c(".hint, .hint2").remove();a.createHint();b._hint.show();d.preventDefault()});this.element.on("mouseleave",function(a){b._hint.hide().remove();a.preventDefault()})},createHint:function(){var a=this.element,b=a.data("hint").split("|"),d=this.options;void 0!=a.data("hintPosition")&&
(d.position=a.data("hintPosition"));void 0!=a.data("hintBackground")&&(d.background=a.data("hintBackground"));void 0!=a.data("hintShadow")&&(d.shadow=a.data("hintShadow"));void 0!=a.data("hintBorder")&&(d.border=a.data("hintBorder"));void 0!=a.data("hintMode")&&(d.mode=a.data("hintMode"));if("TD"==a[0].tagName||"TH"==a[0].tagName){var e=c("<div/>").css("display","inline-block").html(a.html());a.html(e);a=e}var e=1<b.length?b[0]:!1,f=1<b.length?b[1]:b[0],b=c("<div/>").appendTo("body");2==d.mode?b.addClass("hint2"):
b.addClass("hint");e&&c("<div/>").addClass("hint-title").html(e).appendTo(b);c("<div/>").addClass("hint-text").html(f).appendTo(b);b.addClass(d.position);d.shadow&&b.addClass("shadow");d.background&&b.css("background-color",d.background);d.border&&b.css("border-color",d.border);"top"==d.position?b.css({top:a.offset().top-c(window).scrollTop()-b.outerHeight()-20,left:2==d.mode?a.offset().left+a.outerWidth()/2-b.outerWidth()/2-c(window).scrollLeft():a.offset().left-c(window).scrollLeft()}):"bottom"==
d.position?b.css({top:a.offset().top-c(window).scrollTop()+a.outerHeight(),left:2==d.mode?a.offset().left+a.outerWidth()/2-b.outerWidth()/2-c(window).scrollLeft():a.offset().left-c(window).scrollLeft()}):"right"==d.position?b.css({top:2==d.mode?a.offset().top+a.outerHeight()/2-b.outerHeight()/2-c(window).scrollTop()-10:a.offset().top-10-c(window).scrollTop(),left:a.offset().left+a.outerWidth()+15-c(window).scrollLeft()}):"left"==d.position&&b.css({top:2==d.mode?a.offset().top+a.outerHeight()/2-b.outerHeight()/
2-c(window).scrollTop()-10:a.offset().top-10-c(window).scrollTop(),left:a.offset().left-b.outerWidth()-10-c(window).scrollLeft()});d._hint=b},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.streamer",{version:"1.0.0",options:{scrollBar:!1,meter:{start:9,stop:19,interval:20},slideToGroup:1,slideToTime:"10:20",slideSleep:1E3,slideSpeed:1E3,onClick:function(a){},onLongClick:function(a){}},_create:function(){var a=this,b=this.element,d=this.options,e=b.find(".stream"),f=b.find(".event"),g=b.find(".events"),h=b.find(".events-area"),k=b.find(".event-group"),l=b.find(".event-stream");void 0!=b.data("scrollBar")&&(d.scrollBar=b.data("scrollBar"));void 0!=b.data("meterStart")&&
(d.meter.start=parseInt(b.data("meterStart")));void 0!=b.data("meterStop")&&(d.meter.stop=parseInt(b.data("meterStop")));void 0!=b.data("meterInterval")&&(d.meter.interval=b.data("meterInterval"));void 0!=b.data("slideToGroup")&&(d.slideToGroup=parseInt(b.data("slideToGroup")));void 0!=b.data("slideSleep")&&(d.slideSleep=parseInt(b.data("slideSleep")));void 0!=b.data("slideSpeed")&&(d.slideSpeed=parseInt(b.data("slideSpeed")));b.data("streamSelect",-1);var n=c("<ul/>").addClass("meter"),p,m,q,u=d.meter.stop,
r=d.meter.interval,s=[];for(p=d.meter.start;p<u;p++)for(m=0;60>m;m+=r)q=(10>p?"0"+p:p)+":"+(10>m?"0"+m:m),c("<li/>").addClass("js-interval-"+q.replace(":","-")).html("<em>"+q+"</em>").appendTo(n),s.push(q);b.data("intervals",s);n.insertBefore(b.find(".events-grid"));b.find(".event-stream").each(function(a,b){var d=0,f=c(b).find(".event");f.each(function(a,b){d+=c(b).outerWidth()});c(b).css({width:d+2*(f.length-1)+1});c(b).find(".time").css("background-color",c(e[a]).css("background-color"))});g.css({"overflow-x":d.scrollBar?
"scroll":"hidden"});b.css({height:b.find(".streams").outerHeight()+(d.scrollBar?20:0)});var t=0;k.each(function(a,b){t+=c(b).outerWidth()});t+=2*(k.length-1)+10;h.css("width",t);f.each(function(a,b){addTouchEvents(b)});f.mousedown(function(a){a.altKey&&c(this).toggleClass("selected")});b.mousewheel(function(a,b){var d=50*b;g.scrollLeft(g.scrollLeft()-d);return!1});e.each(function(a,d){c(d).mousedown(function(d){b.data("streamSelect")==a?(f.removeClass("event-disable"),b.data("streamSelect",-1)):(b.data("streamSelect",
a),f.addClass("event-disable"),c(l[a]).find(".event").removeClass("event-disable"))})});f.on("click",function(a){a.preventDefault();d.onClick(c(this))});f.on("longclick",function(a){c(this).toggleClass("selected");a.preventDefault();d.onLongClick(c(this))});b.find(".js-go-previous-time").on("click",function(c){c=b.data("intervals").indexOf(b.data("current-time"));0!=c&&(c--,c=b.data("intervals")[c],a.slideToTime(c,0,d.slideSpeed))});b.find(".js-go-next-time").on("click",function(c){c=b.data("intervals").indexOf(b.data("current-time"));
c!=b.data("intervals").length-1&&(c++,c=b.data("intervals")[c],a.slideToTime(c,0,d.slideSpeed))});b.find(".js-show-all-streams").on("click",function(a){b.find(".event").removeClass("event-disable");b.data("streamSelect",-1);a.preventDefault()});b.find(".js-schedule-mode").on("click",function(a){c(this).toggleClass("inverse");b.data("schedule-mode",c(this).hasClass("inverse"));a.preventDefault()});d.slideToTime?this.slideToTime(d.slideToTime,d.slideSleep,d.slideSpeed):this.slideToGroup(d.slideToGroup,
d.slideSleep,d.slideSpeed)},slideToTime:function(a,b,d){var c=this,f=this.element,g=f.find(".meter li.js-interval-"+a.replace(":","-"))[0],h=f.find(".streams").outerWidth()+2;setTimeout(function(){f.find(".events").animate({scrollLeft:"+="+(g.offsetLeft-h)},d,function(){c._afterSlide()})},b);f.data("current-time",a)},slideToGroup:function(a,b,d){var c=this,f=this.element,g=f.find(".event-group"),h=f.find(".streams").outerWidth()+2;setTimeout(function(){f.find(".events").animate({scrollLeft:"+="+(g[a-
1].offsetLeft-h)},d,function(){c._afterSlide()})},b)},_afterSlide:function(){},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.scrollbar",{version:"1.0.0",options:{height:"100%",width:"100%",axis:["x","y"],wheel:55},startSize:{width:null,height:null},elemPadding:!1,bothScroll:!1,scrollbar:null,contentHeight:0,contentWidth:0,contentMinHeight:0,contentMinWidth:0,dragElem:null,dragStart:!1,startCoords:{x:0,y:0},currCoords:{x:0,y:0},handlers:!1,_create:function(){var a=this.element,b=this;if(void 0!=a.data("scroll")){var d=a.data("scroll");"vertical"==d&&(this.options.axis="y");"horizontal"==d&&(this.options.axis=
"x");"both"==d&&(this.options.axis=["x","y"])}void 0!=a.data("height")&&(this.options.height=a.data("height"));void 0!=a.data("width")&&(this.options.width=a.data("width"));void 0!=a.data("wheel")&&(this.options.wheel=a.data("wheel"));a.css({position:"relative"});var d=a.outerWidth(),e=a.outerHeight();this.contentMinWidth=d;this.contentMinHeight=e;this.startSize.width=this.options.width;this.startSize.height=this.options.height;var f=this.startSize.width==parseInt(this.startSize.width)?this.startSize.width+
"px":this.startSize.width,g=this.startSize.height==parseInt(this.startSize.height)?this.startSize.height+"px":this.startSize.height;a.wrap('<div class="scrollbar" style="width: '+f+"; height: "+g+';"></div>');this.scrollbar=a.parents(".scrollbar").first();this.scrollbar.parents().first().css({overflow:"hidden"});this._build(d,e);c(window).resize(function(){b._resize()})},_resize:function(){var a=this.element;if(!isNaN(parseInt(this.element.css("left")))&&0!=parseInt(this.element.css("left"))||!isNaN(parseInt(this.element.css("top")))&&
parseInt(this.element.css("top"))){var b=Math.abs(parseInt(this.element.css("left"))),d=Math.abs(parseInt(this.element.css("top"))),c=this.scrollbar.width(),f=this.scrollbar.height();this.contentWidth-b<c&&(b-=c-(this.contentWidth-b),0>b&&(b=0),this.element.css("left",-1*b));this.element.css("left",-1*b);this.contentHeight-d<f&&(d-=f-(this.contentHeight-d),0>d&&(d=0),this.element.css("top",-1*d))}this.options.width=this.startSize.width;this.options.height=this.startSize.height;this.scrollbar.css({padding:0});
this.elemPadding&&(this.element.css({paddingRight:"-=5",paddingBottom:"-=5"}),this.elemPadding=!1);0<this.scrollbar.find(".scrollbar-v").length&&this.scrollbar.find(".scrollbar-v").remove();0<this.scrollbar.find(".scrollbar-h").length&&this.scrollbar.find(".scrollbar-h").remove();0<this.scrollbar.find(".scrollbar-both").length&&this.scrollbar.find(".scrollbar-both").remove();b=a.outerWidth();a=a.outerHeight();this.contentWidth=b;this.contentHeight=a;this._removeHandlers();this._build(b,a)},_build:function(a,
b){var d=this.element;this.options.width="100%"==this.options.width?this.scrollbar.outerWidth():this.options.width;this.options.height="100%"==this.options.height?this.scrollbar.outerHeight():this.options.height;this.scrollbar.css({width:this.startSize.width,height:this.startSize.height});this.contentWidth=a;this.contentHeight=b;"y"==this.options.axis?this.contentHeight>this.options.height?(this.scrollbar.css({paddingRight:20,paddingBottom:0}),a=d.outerWidth(),b=d.outerHeight(),this.contentWidth=
a,this.contentHeight=b,this._verticalScrollbar(),this._startHandlers()):(0<this.scrollbar.find(".scrollbar-v").length&&this.scrollbar.find(".scrollbar-v").hide(),this.scrollbar.css({paddingRight:0,paddingBottom:0})):"x"==this.options.axis?this.contentWidth>this.options.width?("100%"==this.startSize.height&&(this.scrollbar.css({paddingBottom:20,paddingRight:0}),a=d.outerWidth(),b=d.outerHeight(),this.contentWidth=a,this.contentHeight=b),this._horizontalScrollbar(),this._startHandlers()):(0<this.scrollbar.find(".scrollbar-h").length&&
this.scrollbar.find(".scrollbar-h").hide(),this.scrollbar.css({paddingBottom:0,paddingRight:0})):this.contentHeight>this.options.height&&this.contentWidth>this.options.width?(this.bothScroll=!0,0<this.scrollbar.find(".scrollbar-both").length?this.scrollbar.find(".scrollbar-both").show():this.scrollbar.append('<div class="scrollbar-both"></div>'),this.elemPadding||(this.element.css({paddingRight:"+=5",paddingBottom:"+=5"}),this.elemPadding=!0),a=d.outerWidth(),b=d.outerHeight(),this.contentWidth=a,
this.contentHeight=b,this._verticalScrollbar(),this._horizontalScrollbar(),this._startHandlers()):(this.bothScroll=!1,0<this.scrollbar.find(".scrollbar-both").length&&this.scrollbar.find(".scrollbar-both").hide(),this.elemPadding&&(this.element.css({paddingRight:"-=5",paddingBottom:"-=5"}),this.elemPadding=!1),this.contentWidth>this.options.width?("100%"==this.startSize.height&&(this.scrollbar.css({paddingBottom:20,paddingRight:0}),a=d.outerWidth(),b=d.outerHeight(),this.contentWidth=a,this.contentHeight=
b),this._horizontalScrollbar(),this._startHandlers()):(0<this.scrollbar.find(".scrollbar-h").length&&this.scrollbar.find(".scrollbar-h").hide(),this.scrollbar.css({paddingBottom:0,paddingRight:0})),this.contentHeight>this.options.height?(this.scrollbar.css({paddingRight:20,paddingBottom:0}),a=d.outerWidth(),b=d.outerHeight(),this.contentWidth=a,this.contentHeight=b,this._verticalScrollbar(),this._startHandlers()):(0<this.scrollbar.find(".scrollbar-v").length&&this.scrollbar.find(".scrollbar-v").hide(),
this.scrollbar.css({paddingRight:0,paddingBottom:0})))},_verticalScrollbar:function(){if(1>this.scrollbar.find(".scrollbar-v").length){var a=[];a[a.length]='<div class="scrollbar-v">';a[a.length]='<div class="scrollbar-v-up"></div>';a[a.length]='<div class="scrollbar-v-down"></div>';a[a.length]='<div class="scrollbar-line-v">';a[a.length]='<div class="scrollbar-line-v-btn"></div>';a[a.length]="</div>";a[a.length]="</div>";a=a.join("");this.scrollbar.append(a)}else this.scrollbar.find(".scrollbar-v").show();
var b=this.scrollbar.find(".scrollbar-line-v"),a=this.scrollbar.find(".scrollbar-line-v-btn"),d=this.scrollbar.find(".scrollbar-v");if(this.bothScroll){d.height(this.options.height);var c=d.height()-15;this.options.height=c;d.height(c)}else d.height(this.options.height);d=this.options.height-32;c=d/this.contentHeight;b.height(d);c>=(this.contentHeight-32)/this.contentHeight?a.hide():(b=c*this.options.height,a.show().height(b))},_horizontalScrollbar:function(){if(1>this.scrollbar.find(".scrollbar-h").length){var a=
[];a[a.length]='<div class="scrollbar-h">';a[a.length]='<div class="scrollbar-h-up"></div>';a[a.length]='<div class="scrollbar-h-down"></div>';a[a.length]='<div class="scrollbar-line-h">';a[a.length]='<div class="scrollbar-line-h-btn"></div>';a[a.length]="</div>";a[a.length]="</div>";a=a.join("");this.scrollbar.append(a)}else this.scrollbar.find(".scrollbar-h").show();var a=this.scrollbar.find(".scrollbar-line-h"),b=this.scrollbar.find(".scrollbar-line-h-btn"),d=this.scrollbar.find(".scrollbar-h");
if(this.bothScroll){d.width(this.options.width);var c=d.width()-15;this.options.width=c;d.width(c)}else d.width(this.options.width);var d=this.options.width-32,c=d/this.contentWidth,f=c*this.options.width,g=isNaN(parseInt(this.element.css("left")))?0:parseInt(this.element.css("left")),g=Math.abs(g)*c;a.width(d);c>=(this.contentWidth-32)/this.contentWidth?b.hide():b.show().width(f).css({left:g})},_startHandlers:function(){if(this.handlers)return!1;this.handlers=!0;var a=this;c(document).mousemove(function(b){a._drag(b)});
c(document).mouseup(function(b){a._dragEnd(b)});this.scrollbar.find(".scrollbar-line-v-btn,.scrollbar-line-h-btn").on("mousedown",function(b){a._dragStart(b,c(this))});this.scrollbar.find(".scrollbar-line-v,.scrollbar-line-h").on("click",function(b){a._clickPos(b,c(this))});this.scrollbar.find(".scrollbar-v-up,.scrollbar-h-up").on("click",function(b){a._fixScroll(1,c(this))});this.scrollbar.find(".scrollbar-v-down,.scrollbar-h-down").on("click",function(b){a._fixScroll(-1,c(this))});this.scrollbar.mousewheel(function(b,
d){a._fixScroll(d);return!1})},_removeHandlers:function(){if(!this.handlers)return!1;this.handlers=!1;c(document).mousemove(function(a){return!1});c(document).mouseup(function(a){return!1});this.scrollbar.find(".scrollbar-line-v-btn,.scrollbar-line-h-btn").off("mousedown");this.scrollbar.find(".scrollbar-line-v,.scrollbar-line-h").off("click");this.scrollbar.find(".scrollbar-v-up,.scrollbar-h-up").off("click");this.scrollbar.find(".scrollbar-v-down,.scrollbar-h-down").off("click")},_clickPos:function(a,
b){if("scrollbar-line-v"==c(a.target).attr("class")||"scrollbar-line-h"==c(a.target).attr("class")){var d=b.offset();if("scrollbar-line-v"==c(a.target).attr("class")){var e=a.pageY-d.top,f=b.find(".scrollbar-line-v-btn");this.dragElem={elem:f,width:f.width(),height:f.height(),parent:b,parentWidth:b.width(),parentHeight:b.height(),parentOffset:d};this._scrollTo(0,e,"y")}else e=a.pageX-d.left,f=b.find(".scrollbar-line-h-btn"),this.dragElem={elem:f,width:f.width(),height:f.height(),parent:b,parentWidth:b.width(),
parentHeight:b.height(),parentOffset:d},this._scrollTo(e,0,"x");this.dragElem=null}},_fixScroll:function(a,b){var d,c,f,g=this.options.wheel;if(void 0!==b&&(b.hasClass("scrollbar-h-up")||b.hasClass("scrollbar-h-down"))||"x"==this.options.axis||"x"!=this.options.axis&&"y"!=this.options.axis&&1>this.scrollbar.find(".scrollbar-v").length){var h=100*(g/this.contentWidth);c=(this.options.width-32)/100*h;h=parseInt(this.element.css("left"));f=isNaN(h)?0:Math.abs(h);h=this.scrollbar.find(".scrollbar-line-h-btn");
d=this.scrollbar.find(".scrollbar-line-h");var k=parseFloat(h.css("left")),k=isNaN(k)?0:k;0<a?(g=f-g,f=k-c):(g=f+g,f=k+c);0>g&&(g=0);g>this.contentWidth-this.options.width&&(g=this.contentWidth-this.options.width);c=d.width();d.height();d=h.width();h.height();0>f&&(f=0);f+d>c&&(f=c-d);this.element.css({left:-1*g});h.css({left:f})}else h=100*(g/this.contentHeight),c=(this.options.height-32)/100*h,h=parseInt(this.element.css("top")),f=isNaN(h)?0:Math.abs(h),h=this.scrollbar.find(".scrollbar-line-v-btn"),
d=this.scrollbar.find(".scrollbar-line-v"),k=parseFloat(h.css("top")),k=isNaN(k)?0:k,0<a?(g=f-g,c=k-c):(g=f+g,c=k+c),0>g&&(g=0),g>this.contentHeight-this.options.height&&(g=this.contentHeight-this.options.height),d.width(),f=d.height(),h.width(),d=h.height(),0>c&&(c=0),c+d>f&&(c=f-d),this.element.css({top:-1*g}),h.css({top:c})},_scrollTo:function(a,b,c){"x"==c?(c=(this.options.width-32)/this.contentWidth,b=a/c,0>b&&(b=0),b>this.contentWidth-this.options.width&&(b=this.contentWidth-this.options.width),
0>a&&(a=0),a>this.dragElem.parentWidth-this.dragElem.width&&(a=this.dragElem.parentWidth-this.dragElem.width),this.dragElem.elem.css({left:a}),this.element.css({left:-1*b})):(c=(this.options.height-32)/this.contentHeight,a=b/c,0>a&&(a=0),a>this.contentHeight-this.options.height&&(a=this.contentHeight-this.options.height),0>b&&(b=0),b>this.dragElem.parentHeight-this.dragElem.height&&(b=this.dragElem.parentHeight-this.dragElem.height),this.dragElem.elem.css({top:b}),this.element.css({top:-1*a}))},_scroll:function(){if(this.dragElem.elem.hasClass("scrollbar-line-h-btn")){var a=
(this.options.width-32)/this.contentWidth,b=parseInt(this.dragElem.elem.css("left")),a=(isNaN(b)?0:Math.abs(b))/a;0>a&&(a=0);a>this.contentWidth-this.options.width&&(a=this.contentWidth-this.options.width);this.element.css({left:-1*a})}else a=(this.options.height-32)/this.contentHeight,b=parseInt(this.dragElem.elem.css("top")),a=(isNaN(b)?0:Math.abs(b))/a,0>a&&(a=0),a>this.contentHeight-this.options.height&&(a=this.contentHeight-this.options.height),this.element.css({top:-1*a})},_startCoordsDiff:function(a){var b=
this.dragElem.elem.offset();this.startCoords.x=a.pageX-b.left;this.startCoords.y=a.pageY-b.top},_dragStart:function(a,b){var c=b.parents().first();this.dragElem={elem:b,width:b.width(),height:b.height(),parent:c,parentWidth:c.width(),parentHeight:c.height(),parentOffset:c.offset()};this.dragStart=!0;this.currCoords.x=a.pageX;this.currCoords.y=a.pageY;this._startCoordsDiff(a)},_drag:function(a){if(this.dragStart){this.currCoords.x=a.pageX;this.currCoords.y=a.pageY;a=this.currCoords.x-this.startCoords.x-
this.dragElem.parentOffset.left;var b=this.currCoords.y-this.startCoords.y-this.dragElem.parentOffset.top;0>a&&(a=0);a+this.dragElem.width>=this.dragElem.parentWidth&&(a=this.dragElem.parentWidth-this.dragElem.width);0>b&&(b=0);b+this.dragElem.height>this.dragElem.parentHeight&&(b=this.dragElem.parentHeight-this.dragElem.height);this.dragElem.elem.css({left:a,top:b});this._scroll()}},_dragEnd:function(a){this.dragStart&&(this.dragElem=null,this.dragStart=!1,this.startCoords.x=0,this.startCoords.y=
0,this.currCoords.x=null,this.currCoords.y=null)},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.stepper",{version:"1.0.0",options:{steps:3,start:1,clickable:!0,onStep:function(a,b){},onStepClick:function(a,b){}},_create:function(){var a=this.element,b=this.options;void 0!=a.data("steps")&&(b.steps=a.data("steps"));void 0!=a.data("start")&&(b.start=a.data("start"));this._createStepper();b.clickable&&this._createEvents();this._positioningSteps();this._stepTo(b.start)},_createEvents:function(){var a=this.element,b=this.options;a.on("click","li",function(d){d=c(this).data("step");
b.onStepClick(d-1,d);a.trigger("stepclick",d)})},_createStepper:function(){var a=this.element,b=this.options,d,e;e=c("<ul/>");for(d=0;d<b.steps;d++)c("<li/>").data("step",d+1).appendTo(e);e.appendTo(a)},_positioningSteps:function(){var a=this.element,b=a.find("li"),d=a.width(),e=b.length-1,f=c(b[0]).width();c.each(b,function(a,b){var k=0==a?0:(d-f)/e*a;c(b).animate({left:k})})},_stepTo:function(a){var b=this.options,d=this.element.find("li");d.removeClass("current").removeClass("complete");c.each(d,
function(d,f){d<a-1&&c(f).addClass("complete");d==a-1&&(c(f).addClass("current"),b.onStep(d+1,f))})},stepTo:function(a){this._stepTo(a)},first:function(){var a=this.options;a.start=1;this._stepTo(a.start)},last:function(){var a=this.options,b=this.element.find("li");a.start=b.length;this._stepTo(a.start)},next:function(){var a=this.options,b=this.element.find("li");a.start+1>b.length||(a.start++,this._stepTo(a.start))},prior:function(){var a=this.options;0!=a.start-1&&(a.start--,this._stepTo(a.start))},
_destroy:function(){this._stepper.remove()},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.pullmenu",{version:"1.0.0",options:{},_create:function(){var a=this.element,b=void 0!=a.data("relation")?a.data("relation"):a.parent().children(".element-menu, .horizontal-menu");addTouchEvents(a[0]);a.on("click",function(a){b.slideToggle();a.preventDefault();a.stopPropagation()})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);
$(window).resize(function(){800<(0<window.innerWidth?window.innerWidth:screen.width)?$(".element-menu").show():$(".element-menu").hide()});(function(c){c.widget("metro.wizard",{version:"1.0.0",options:{stepper:!0,stepperType:"default",stepperClickable:!0,startPage:"default",locale:c.Metro.currentLocale,finishStep:"default",buttons:{cancel:!0,help:!0,prior:!0,next:!0,finish:!0},onCancel:function(a,b){},onHelp:function(a,b){},onPrior:function(a,b){return!0},onNext:function(a,b){return!0},onFinish:function(a,b){},onPage:function(a,b){},onStepClick:function(a){}},_stepper:void 0,_currentStep:0,_steps:void 0,_create:function(){var a=this,
b=this.element,c=this.options,e=b.find(".step");this._steps=e;c.stepper&&(this._stepper=this._createStepper(e.length).insertBefore(b.find(".steps")).stepper({clickable:c.stepperClickable}).on("stepclick",function(b,e){a.stepTo(e);c.onStepClick(e)}));void 0!=b.data("locale")&&(c.locale=b.data("locale"));this._createEvents();e="default"!=c.startPage&&1<parseInt(c.startPage)?c.startPage:1;this.stepTo(e);this.options.onPage(this._currentStep+1,b)},_createStepper:function(a){var b=this.options;a=c("<div/>").addClass("stepper").attr("data-role",
"stepper").attr("data-steps",a);"default"!=b.stepperType&&a.addClass(b.stepperType);return a},_createEvents:function(){var a=this,b=this.element,d=this.options;if(d.buttons){var e=c("<div/>").addClass("actions").appendTo(b),f=c("<div/>").addClass("group-left").appendTo(e),e=c("<div/>").addClass("group-right").appendTo(e),g;d.buttons.cancel&&(g=c("<button type='button'/>").addClass("btn-cancel").html(c.Metro.Locale[d.locale].buttons[2]),"boolean"==typeof d.buttons.cancel?g.appendTo(f):(d.buttons.cancel.title&&
g.html(d.buttons.cancel.title),d.buttons.cancel.cls&&g.addClass(d.buttons.cancel.cls),d.buttons.cancel.group&&"left"!=d.buttons.cancel.group?g.appendTo(e):g.appendTo(f)),g.on("click",function(){d.onCancel(a._currentStep+1,b)}));d.buttons.help&&(g=c("<button type='button'/>").addClass("btn-help").html(c.Metro.Locale[d.locale].buttons[3]),"boolean"==typeof d.buttons.help?g.appendTo(e):(d.buttons.help.title&&g.html(d.buttons.help.title),d.buttons.help.cls&&g.addClass(d.buttons.help.cls),d.buttons.help.group&&
"left"!=d.buttons.help.group?g.appendTo(e):g.appendTo(f)),g.on("click",function(){d.onHelp(a._currentStep+1,b)}));d.buttons.prior&&(g=c("<button type='button'/>").addClass("btn-prior").html(c.Metro.Locale[d.locale].buttons[4]),"boolean"==typeof d.buttons.prior?g.appendTo(e):(d.buttons.prior.title&&g.html(d.buttons.prior.title),d.buttons.prior.cls&&g.addClass(d.buttons.prior.cls),d.buttons.prior.group&&"left"!=d.buttons.prior.group?g.appendTo(e):g.appendTo(f)),g.on("click",function(){d.onPrior(a._currentStep+
1,b)&&a.prior()}));d.buttons.next&&(g=c("<button type='button'/>").addClass("btn-next").html(c.Metro.Locale[d.locale].buttons[5]),"boolean"==typeof d.buttons.next?g.appendTo(e):(d.buttons.next.title&&g.html(d.buttons.next.title),d.buttons.next.cls&&g.addClass(d.buttons.next.cls),d.buttons.next.group&&"left"!=d.buttons.next.group?g.appendTo(e):g.appendTo(f)),g.on("click",function(){d.onNext(a._currentStep+1,b)&&a.next()}));d.buttons.finish&&(g=c("<button type='button'/>").addClass("btn-finish").html(c.Metro.Locale[d.locale].buttons[6]),
"boolean"==typeof d.buttons.finish?g.appendTo(e):(d.buttons.finish.title&&g.html(d.buttons.finish.title),d.buttons.finish.cls&&g.addClass(d.buttons.finish.cls),d.buttons.finish.group&&"left"!=d.buttons.finish.group?g.appendTo(e):g.appendTo(f)),g.on("click",function(){d.onFinish(a._currentStep+1,b)}))}},next:function(){var a=this._currentStep+1;if(a==this._steps.length)return!1;this._currentStep=a;this._steps.hide();c(this._steps[a]).show();this.options.onPage(this._currentStep+1,this.element);void 0!=
this._stepper&&this._stepper.stepper("stepTo",this._currentStep+1);var b=parseInt("default"==this.options.finishStep?this._steps.length-1:this.options.finishStep);a==b?this.element.find(".btn-finish").attr("disabled",!1):this.element.find(".btn-finish").attr("disabled",!0);a==this._steps.length-1?this.element.find(".btn-next").attr("disabled",!0):this.element.find(".btn-next").attr("disabled",!1);0<a&&this.element.find(".btn-prior").attr("disabled",!1);return!0},prior:function(){var a=this._currentStep-
1;if(0>a)return!1;this._currentStep=a;this._steps.hide();c(this._steps[a]).show();this.options.onPage(this._currentStep+1,this.element);void 0!=this._stepper&&this._stepper.stepper("stepTo",this._currentStep+1);var b=parseInt("default"==this.options.finishStep?this._steps.length-1:this.options.finishStep);a==b?this.element.find(".btn-finish").attr("disabled",!1):this.element.find(".btn-finish").attr("disabled",!0);0==a?this.element.find(".btn-prior").attr("disabled",!0):this.element.find(".btn-prior").attr("disabled",
!1);a<b&&this.element.find(".btn-next").attr("disabled",!1);return!0},stepTo:function(a){var b=a-1;if(0>b)return!1;this._currentStep=b;this._steps.hide();c(this._steps[b]).show();this.options.onPage(this._currentStep+1,this.element);void 0!=this._stepper&&this._stepper.stepper("stepTo",a);a=parseInt("default"==this.options.finishStep?this._steps.length-1:this.options.finishStep);b==a?this.element.find(".btn-finish").attr("disabled",!1):this.element.find(".btn-finish").attr("disabled",!0);console.log(b,
a);this.element.find(".btn-next").attr("disabled",!(b<a));this.element.find(".btn-prior").attr("disabled",!(0<b));return!0},stepper:function(){return this._stepper},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.panel",{version:"1.0.0",options:{onCollapse:function(){},onExpand:function(){}},_create:function(){var a=this.element,b=this.options,c=a.children(".panel-header"),e=a.children(".panel-content");c.on("click",function(){e.slideToggle("fast",function(){a.toggleClass("collapsed");if(a.hasClass("collapsed"))b.onCollapse();else b.onExpand()})})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.tileTransform",{version:"1.0.0",options:{},_create:function(){var a=this.element,b=a.width(),d=a.height();a.on("click",function(a){});a.on("mousedown",function(e){var f=e.pageX-c(this).offset().left;e=e.pageY-c(this).offset().top;var g="top";f<1*b/3&&(e<1*d/2||e>1*d/2)?g="left":f>2*b/3&&(e<1*d/2||e>1*d/2)?g="right":f>1*b/3&&(f<2*b/3&&e>d/2)&&(g="bottom");c(this).addClass("tile-transform-"+g);"A"==a[0].tagName&&a.attr("href")&&setTimeout(function(){document.location.href=
a.attr("href")},500)});a.on("mouseup",function(){c(this).removeClass("tile-transform-left").removeClass("tile-transform-right").removeClass("tile-transform-top").removeClass("tile-transform-bottom")});a.on("mouseleave",function(){c(this).removeClass("tile-transform-left").removeClass("tile-transform-right").removeClass("tile-transform-top").removeClass("tile-transform-bottom")})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.widget("metro.popover",{version:"1.0.0",options:{popoverText:"",popoverTimeout:3E3,popoverPosition:"top",popoverBackground:"bg-cyan",popoverColor:"fg-white",popoverMode:"none",popoverShadow:!0},popover:{},_create:function(){this.createPopover()},createPopover:function(){var a=this,b,d=this.options;b=this.element;c.each(b.data(),function(a,b){try{d[a]=c.parseJSON(b)}catch(e){d[a]=b}});var e,f;e=c("<div/>").addClass("notice").appendTo("body").hide();c("<div/>").appendTo(e);d.popoverShadow&&
e.addClass("shadow");d.popoverBackground&&("#"==d.popoverBackground[0]?e.css("background-color",d.popoverBackground):e.addClass(d.popoverBackground));d.popoverColor&&("#"==d.popoverColor[0]?e.css("color",d.popoverColor):e.addClass(d.popoverColor));switch(d.popoverPosition){case "left":f="marker-on-right";break;case "right":f="marker-on-left";break;case "bottom":f="marker-on-top";break;default:f="marker-on-bottom"}e.css({position:"fixed"});e.addClass(f);this.popover=e;this.setText(d.popoverText);b.on(d.popoverMode,
function(b){e.data("visible")||a.show()});c(window).scroll(function(){a.popover.data("visible")&&a.setPosition()})},setPosition:function(){var a=this.options,b=this.popover,d=this.element;"top"==a.popoverPosition?b.css({top:d.offset().top-c(window).scrollTop()-b.outerHeight()-10,left:d.offset().left+d.outerWidth()/2-b.outerWidth()/2-c(window).scrollLeft()}):"bottom"==a.popoverPosition?b.css({top:d.offset().top-c(window).scrollTop()+d.outerHeight()+10,left:d.offset().left+d.outerWidth()/2-b.outerWidth()/
2-c(window).scrollLeft()}):"right"==a.popoverPosition?b.css({top:d.offset().top+d.outerHeight()/2-b.outerHeight()/2-c(window).scrollTop(),left:d.offset().left+d.outerWidth()-c(window).scrollLeft()+10}):"left"==a.popoverPosition&&b.css({top:d.offset().top+d.outerHeight()/2-b.outerHeight()/2-c(window).scrollTop(),left:d.offset().left-b.outerWidth()-c(window).scrollLeft()-10});return this},setText:function(a){this.popover.children("div").html(a)},show:function(){var a=this.options,b=this.popover;this.setPosition();
b.fadeIn(function(){b.data("visible",!0);setTimeout(function(){b.fadeOut(function(){b.data("visible",!1)})},a.popoverTimeout)})},_destroy:function(){},_setOption:function(a,b){this._super("_setOption",a,b)}})})(jQuery);(function(c){c.Metro.initAccordions=function(a){void 0!=a?c(a).find("[data-role=accordion]").accordion():c("[data-role=accordion]").accordion()};c.Metro.initButtonSets=function(a){void 0!=a?(c(a).find("[data-role=button-set]").buttonset(),c(a).find("[data-role=button-group]").buttongroup()):(c("[data-role=button-set]").buttonset(),c("[data-role=button-group]").buttongroup())};c.Metro.initCalendars=function(a){void 0!=a?c(a).find("[data-role=calendar]").calendar():c("[data-role=calendar]").calendar()};
c.Metro.initCarousels=function(a){void 0!=a?c(a).find("[data-role=carousel]").carousel():c("[data-role=carousel]").carousel()};c.Metro.initCountdowns=function(a){void 0!=a?c(a).find("[data-role=countdown]").countdown():c("[data-role=countdown]").countdown()};c.Metro.initDatepickers=function(a){void 0!=a?c(a).find("[data-role=datepicker]").datepicker():c("[data-role=datepicker]").datepicker()};c.Metro.initDropdowns=function(a){void 0!=a?c(a).find("[data-role=dropdown]").dropdown():c("[data-role=dropdown]").dropdown()};
c.Metro.initFluentMenus=function(a){void 0!=a?c(a).find("[data-role=fluentmenu]").fluentmenu():c("[data-role=fluentmenu]").fluentmenu()};c.Metro.initHints=function(a){void 0!=a?c(a).find("[data-hint]").hint():c("[data-hint]").hint()};c.Metro.initInputs=function(a){void 0!=a?c(a).find("[data-role=input-control], .input-control").inputControl():c("[data-role=input-control], .input-control").inputControl()};c.Metro.transformInputs=function(a){void 0!=a?c(a).find("[data-transform=input-control]").inputTransform():
c("[data-transform=input-control]").inputTransform()};c.Metro.initListViews=function(a){void 0!=a?c(a).find("[data-role=listview]").listview():c("[data-role=listview]").listview()};c.Metro.initLives=function(a){void 0!=a?c(a).find("[data-role=live-tile], [data-role=live]").livetile():c("[data-role=live-tile], [data-role=live]").livetile()};c.Metro.initProgreeBars=function(a){void 0!=a?c(a).find("[data-role=progress-bar]").progressbar():c("[data-role=progress-bar]").progressbar()};c.Metro.initRatings=
function(a){void 0!=a?c(a).find("[data-role=rating]").rating():c("[data-role=rating]").rating()};c.Metro.initScrolls=function(a){void 0!=a?c(a).find("[data-role=scrollbox]").scrollbar():c("[data-role=scrollbox]").scrollbar()};c.Metro.initSliders=function(a){void 0!=a?c(a).find("[data-role=slider]").slider():c("[data-role=slider]").slider()};c.Metro.initTabs=function(a){void 0!=a?c(a).find("[data-role=tab-control]").tabcontrol():c("[data-role=tab-control]").tabcontrol()};c.Metro.initTimes=function(a){void 0!=
a?c(a).find("[data-role=times]").times():c("[data-role=times]").times()};c.Metro.initTrees=function(a){void 0!=a?c(a).find("[data-role=treeview]").treeview():c("[data-role=treeview]").treeview()};c.Metro.initSteppers=function(a){void 0!=a?c(a).find("[data-role=stepper]").stepper():c("[data-role=stepper]").stepper()};c.Metro.initStreamers=function(a){void 0!=a?c(a).find("[data-role=streamer]").streamer():c("[data-role=streamer]").streamer()};c.Metro.initDragTiles=function(a){void 0!=a?c(a).find("[data-role=drag-drop]").dragtile():
c("[data-role=drag-drop]").dragtile()};c.Metro.initPulls=function(a){void 0!=a&&c(a).find("[data-role=pull-menu], .pull-menu").pullmenu();c("[data-role=pull-menu], .pull-menu").pullmenu()};c.Metro.initPanels=function(a){void 0!=a&&c(a).find("[data-role=panel]").panel();c("[data-role=panel]").panel()};c.Metro.initTileTransform=function(a){void 0!=a&&c(a).find("[data-click=transform]").tileTransform();c("[data-click=transform]").tileTransform()};c.Metro.initPopover=function(a){void 0!=a&&c(a).find("[data-popover=popover]").popover();
c("[data-popover=popover]").popover()};c.Metro.initAll=function(a){c.Metro.initAccordions(a);c.Metro.initButtonSets(a);c.Metro.initCalendars(a);c.Metro.initCarousels(a);c.Metro.initCountdowns(a);c.Metro.initDatepickers(a);c.Metro.initDropdowns(a);c.Metro.initFluentMenus(a);c.Metro.initHints(a);c.Metro.initInputs(a);c.Metro.transformInputs(a);c.Metro.initListViews(a);c.Metro.initLives(a);c.Metro.initProgreeBars(a);c.Metro.initRatings(a);c.Metro.initScrolls(a);c.Metro.initSliders(a);c.Metro.initTabs(a);
c.Metro.initTimes(a);c.Metro.initTrees(a);c.Metro.initSteppers(a);c.Metro.initStreamers(a);c.Metro.initDragTiles(a);c.Metro.initPulls(a);c.Metro.initPanels(a);c.Metro.initTileTransform(a);c.Metro.initPopover(a)}})(jQuery);$(function(){$.Metro.initAll()});$(function(){if(METRO_AUTO_REINIT){var c=$(".metro").html(),a;setInterval(function(){a=$(".metro").html();c!==a&&(c=a,$.Metro.initAll())},500)}});
/*
 * Ahoy.js
 * Simple, powerful JavaScript analytics
 * https://github.com/ankane/ahoy.js
 * v0.1.0
 * MIT License
 */

/*jslint browser: true, indent: 2, plusplus: true, vars: true */


(function (window) {
  "use strict";

  var ahoy = window.ahoy || window.Ahoy || {};
  var $ = window.jQuery || window.Zepto || window.$;
  var visitId, visitorId, track;
  var visitTtl = 4 * 60; // 4 hours
  var visitorTtl = 2 * 365 * 24 * 60; // 2 years
  var isReady = false;
  var queue = [];
  var canStringify = typeof(JSON) !== "undefined" && typeof(JSON.stringify) !== "undefined";
  var eventQueue = [];
  var visitsUrl = ahoy.visitsUrl || "/ahoy/visits"
  var eventsUrl = ahoy.eventsUrl || "/ahoy/events"

  // cookies

  // http://www.quirksmode.org/js/cookies.html
  function setCookie(name, value, ttl) {
    var expires = "";
    var cookieDomain = "";
    if (ttl) {
      var date = new Date();
      date.setTime(date.getTime() + (ttl * 60 * 1000));
      expires = "; expires=" + date.toGMTString();
    }
    if (ahoy.domain) {
      cookieDomain = "; domain=" + ahoy.domain;
    }
    document.cookie = name + "=" + escape(value) + expires + cookieDomain + "; path=/";
  }

  function getCookie(name) {
    var i, c;
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (i = 0; i < ca.length; i++) {
      c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1, c.length);
      }
      if (c.indexOf(nameEQ) === 0) {
        return unescape(c.substring(nameEQ.length, c.length));
      }
    }
    return null;
  }

  function destroyCookie(name) {
    setCookie(name, "", -1);
  }

  function log(message) {
    if (getCookie("ahoy_debug")) {
      window.console.log(message);
    }
  }

  function setReady() {
    var callback;
    while (callback = queue.shift()) {
      callback();
    }
    isReady = true;
  }

  function ready(callback) {
    if (isReady) {
      callback();
    } else {
      queue.push(callback);
    }
  }

  // http://stackoverflow.com/a/2117523/1177228
  function generateId() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
      return v.toString(16);
    });
  }

  function saveEventQueue() {
    // TODO add stringify method for IE 7 and under
    if (canStringify) {
      setCookie("ahoy_events", JSON.stringify(eventQueue), 1);
    }
  }

  function trackEvent(event) {
    ready( function () {
      // ensure JSON is defined
      if (canStringify) {
        $.ajax({
          type: "POST",
          url: eventsUrl,
          data: JSON.stringify([event]),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function() {
            // remove from queue
            for (var i = 0; i < eventQueue.length; i++) {
              if (eventQueue[i].id == event.id) {
                eventQueue.splice(i, 1);
                break;
              }
            }
            saveEventQueue();
          }
        });
      }
    });
  }

  function page() {
    return ahoy.page || window.location.pathname;
  }

  function eventProperties(e) {
    var $target = $(e.currentTarget);
    return {
      tag: $target.get(0).tagName.toLowerCase(),
      id: $target.attr("id"),
      "class": $target.attr("class"),
      page: page(),
      section: $target.closest("*[data-section]").data("section")
    };
  }

  // main

  visitId = getCookie("ahoy_visit");
  visitorId = getCookie("ahoy_visitor");
  track = getCookie("ahoy_track");

  if (visitId && visitorId && !track) {
    // TODO keep visit alive?
    log("Active visit");
    setReady();
  } else {
    if (track) {
      destroyCookie("ahoy_track");
    }

    if (!visitId) {
      visitId = generateId();
      setCookie("ahoy_visit", visitId, visitTtl);
    }

    // make sure cookies are enabled
    if (getCookie("ahoy_visit")) {
      log("Visit started");

      if (!visitorId) {
        visitorId = generateId();
        setCookie("ahoy_visitor", visitorId, visitorTtl);
      }

      var data = {
        visit_token: visitId,
        visitor_token: visitorId,
        platform: ahoy.platform || "Web",
        landing_page: window.location.href,
        screen_width: window.screen.width,
        screen_height: window.screen.height
      };

      // referrer
      if (document.referrer.length > 0) {
        data.referrer = document.referrer;
      }

      log(data);

      $.post(visitsUrl, data, setReady, "json");
    } else {
      log("Cookies disabled");
      setReady();
    }
  }

  ahoy.getVisitId = ahoy.getVisitToken = function () {
    return visitId;
  };

  ahoy.getVisitorId = ahoy.getVisitorToken = function () {
    return visitorId;
  };

  ahoy.reset = function () {
    destroyCookie("ahoy_visit");
    destroyCookie("ahoy_visitor");
    destroyCookie("ahoy_events");
    destroyCookie("ahoy_track");
    return true;
  };

  ahoy.debug = function (enabled) {
    if (enabled === false) {
      destroyCookie("ahoy_debug");
    } else {
      setCookie("ahoy_debug", "t", 365 * 24 * 60); // 1 year
    }
    return true;
  };

  ahoy.track = function (name, properties) {
    // generate unique id
    var event = {
      id: generateId(),
      name: name,
      properties: properties,
      time: (new Date()).getTime() / 1000.0
    };
    log(event);

    eventQueue.push(event);
    saveEventQueue();

    // wait in case navigating to reduce duplicate events
    setTimeout( function () {
      trackEvent(event);
    }, 1000);
  };

  ahoy.trackView = function () {
    var properties = {
      url: window.location.href,
      title: document.title,
      page: page()
    };
    ahoy.track("$view", properties);
  };

  ahoy.trackClicks = function () {
    $(document).on("click", "a, button, input[type=submit]", function (e) {
      var $target = $(e.currentTarget);
      var properties = eventProperties(e);
      properties.text = properties.tag == "input" ? $target.val() : $.trim($target.text().replace(/[\s\r\n]+/g, " "));
      properties.href = $target.attr("href");
      ahoy.track("$click", properties);
    });
  };

  ahoy.trackSubmits = function () {
    $(document).on("submit", "form", function (e) {
      var properties = eventProperties(e);
      ahoy.track("$submit", properties);
    });
  };

  ahoy.trackChanges = function () {
    $(document).on("change", "input, textarea, select", function (e) {
      var properties = eventProperties(e);
      ahoy.track("$change", properties);
    });
  };

  ahoy.trackAll = function() {
    ahoy.trackView();
    ahoy.trackClicks();
    ahoy.trackSubmits();
    ahoy.trackChanges();
  };

  // push events from queue
  try {
    eventQueue = JSON.parse(getCookie("ahoy_events") || "[]");
  } catch (e) {
    // do nothing
  }

  for (var i = 0; i < eventQueue.length; i++) {
    trackEvent(eventQueue[i]);
  }

  window.ahoy = ahoy;
}(window));
// This is a manifest file that'll be compiled into application.js, which will include all the files
// listed below.
//
// Any JavaScript/Coffee file within this directory, lib/assets/javascripts, vendor/assets/javascripts,
// or vendor/assets/javascripts of plugins, if any, can be referenced here using a relative path.
//
// It's not advisable to add code directly here, but if you do, it'll appear at the bottom of the
// the compiled file.
//
// WARNING: THE FIRST BLANK LINE MARKS THE END OF WHAT'S TO BE PROCESSED, ANY BLANK LINE SHOULD
// GO AFTER THE REQUIRES BELOW.
//




;
