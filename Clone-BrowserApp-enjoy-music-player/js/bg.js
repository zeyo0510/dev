!function(t){function n(r){if(e[r])return e[r].exports;var o=e[r]={exports:{},id:r,loaded:!1};return t[r].call(o.exports,o,o.exports,n),o.loaded=!0,o.exports}var e={};return n.m=t,n.c=e,n.p="/",n(0)}([function(t,n,e){"use strict";function r(t){return t&&t.__esModule?t:{"default":t}}function o(t){chrome.app.window.create("index.html",{bounds:{width:900,height:600},minWidth:750,minHeight:500,frame:"none",id:"EMP"},function(t){t.onClosed.addListener(function(){c["default"].audio.pause(),c["default"].setSrc("");var t=chrome.app.window.get("MINI");t&&t.close()})})}var i=e(90),c=r(i);window.PURE=c["default"],window.welcome=!1,chrome.app.runtime.onLaunched.addListener(o),chrome.app.runtime.onRestarted.addListener(o),chrome.runtime.onInstalled.addListener(function(t){if("install"===t.reason&&(window.welcome=!0),"update"===t.reason){var n=t.previousVersion;"5.0.0">n&&(window.welcome=!0)}})},function(t,n,e){var r=e(31)("wks"),o=e(27),i=e(2).Symbol,c="function"==typeof i,u=t.exports=function(t){return r[t]||(r[t]=c&&i[t]||(c?i:o)("Symbol."+t))};u.store=r},function(t,n){var e=t.exports="undefined"!=typeof window&&window.Math==Math?window:"undefined"!=typeof self&&self.Math==Math?self:Function("return this")();"number"==typeof __g&&(__g=e)},function(t,n){var e=t.exports={version:"2.4.0"};"number"==typeof __e&&(__e=e)},function(t,n,e){var r=e(9);t.exports=function(t){if(!r(t))throw TypeError(t+" is not an object!");return t}},function(t,n,e){t.exports=!e(17)(function(){return 7!=Object.defineProperty({},"a",{get:function(){return 7}}).a})},function(t,n,e){var r=e(8),o=e(18);t.exports=e(5)?function(t,n,e){return r.f(t,n,o(1,e))}:function(t,n,e){return t[n]=e,t}},function(t,n){var e={}.hasOwnProperty;t.exports=function(t,n){return e.call(t,n)}},function(t,n,e){var r=e(4),o=e(35),i=e(32),c=Object.defineProperty;n.f=e(5)?Object.defineProperty:function(t,n,e){if(r(t),n=i(n,!0),r(e),o)try{return c(t,n,e)}catch(u){}if("get"in e||"set"in e)throw TypeError("Accessors not supported!");return"value"in e&&(t[n]=e.value),t}},function(t,n){t.exports=function(t){return"object"==typeof t?null!==t:"function"==typeof t}},function(t,n,e){var r=e(57),o=e(22);t.exports=function(t){return r(o(t))}},function(t,n,e){var r=e(21);t.exports=function(t,n,e){if(r(t),void 0===n)return t;switch(e){case 1:return function(e){return t.call(n,e)};case 2:return function(e,r){return t.call(n,e,r)};case 3:return function(e,r,o){return t.call(n,e,r,o)}}return function(){return t.apply(n,arguments)}}},function(t,n){t.exports={}},,function(t,n){var e={}.toString;t.exports=function(t){return e.call(t).slice(8,-1)}},function(t,n,e){var r=e(2),o=e(3),i=e(11),c=e(6),u="prototype",a=function(t,n,e){var s,f,l,p=t&a.F,v=t&a.G,d=t&a.S,h=t&a.P,y=t&a.B,m=t&a.W,_=v?o:o[n]||(o[n]={}),x=_[u],w=v?r:d?r[n]:(r[n]||{})[u];v&&(e=n);for(s in e)f=!p&&w&&void 0!==w[s],f&&s in _||(l=f?w[s]:e[s],_[s]=v&&"function"!=typeof w[s]?e[s]:y&&f?i(l,r):m&&w[s]==l?function(t){var n=function(n,e,r){if(this instanceof t){switch(arguments.length){case 0:return new t;case 1:return new t(n);case 2:return new t(n,e)}return new t(n,e,r)}return t.apply(this,arguments)};return n[u]=t[u],n}(l):h&&"function"==typeof l?i(Function.call,l):l,h&&((_.virtual||(_.virtual={}))[s]=l,t&a.R&&x&&!x[s]&&c(x,s,l)))};a.F=1,a.G=2,a.S=4,a.P=8,a.B=16,a.W=32,a.U=64,a.R=128,t.exports=a},,function(t,n){t.exports=function(t){try{return!!t()}catch(n){return!0}}},function(t,n){t.exports=function(t,n){return{enumerable:!(1&t),configurable:!(2&t),writable:!(4&t),value:n}}},function(t,n,e){var r=e(8).f,o=e(7),i=e(1)("toStringTag");t.exports=function(t,n,e){t&&!o(t=e?t:t.prototype,i)&&r(t,i,{configurable:!0,value:n})}},function(t,n,e){t.exports={"default":e(51),__esModule:!0}},function(t,n){t.exports=function(t){if("function"!=typeof t)throw TypeError(t+" is not a function!");return t}},function(t,n){t.exports=function(t){if(void 0==t)throw TypeError("Can't call method on  "+t);return t}},function(t,n,e){var r=e(9),o=e(2).document,i=r(o)&&r(o.createElement);t.exports=function(t){return i?o.createElement(t):{}}},function(t,n){t.exports=!0},function(t,n,e){var r=e(31)("keys"),o=e(27);t.exports=function(t){return r[t]||(r[t]=o(t))}},function(t,n){var e=Math.ceil,r=Math.floor;t.exports=function(t){return isNaN(t=+t)?0:(t>0?r:e)(t)}},function(t,n){var e=0,r=Math.random();t.exports=function(t){return"Symbol(".concat(void 0===t?"":t,")_",(++e+r).toString(36))}},,function(t,n){t.exports="constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf".split(",")},function(t,n,e){var r=e(45),o=e(29);t.exports=Object.keys||function(t){return r(t,o)}},function(t,n,e){var r=e(2),o="__core-js_shared__",i=r[o]||(r[o]={});t.exports=function(t){return i[t]||(i[t]={})}},function(t,n,e){var r=e(9);t.exports=function(t,n){if(!r(t))return t;var e,o;if(n&&"function"==typeof(e=t.toString)&&!r(o=e.call(t)))return o;if("function"==typeof(e=t.valueOf)&&!r(o=e.call(t)))return o;if(!n&&"function"==typeof(e=t.toString)&&!r(o=e.call(t)))return o;throw TypeError("Can't convert object to primitive value")}},function(t,n,e){var r=e(14),o=e(1)("toStringTag"),i="Arguments"==r(function(){return arguments}()),c=function(t,n){try{return t[n]}catch(e){}};t.exports=function(t){var n,e,u;return void 0===t?"Undefined":null===t?"Null":"string"==typeof(e=c(n=Object(t),o))?e:i?r(n):"Object"==(u=r(n))&&"function"==typeof n.callee?"Arguments":u}},function(t,n,e){t.exports=e(2).document&&document.documentElement},function(t,n,e){t.exports=!e(5)&&!e(17)(function(){return 7!=Object.defineProperty(e(23)("div"),"a",{get:function(){return 7}}).a})},function(t,n,e){"use strict";var r=e(24),o=e(15),i=e(46),c=e(6),u=e(7),a=e(12),s=e(60),f=e(19),l=e(44),p=e(1)("iterator"),v=!([].keys&&"next"in[].keys()),d="@@iterator",h="keys",y="values",m=function(){return this};t.exports=function(t,n,e,_,x,w,g){s(e,n,_);var b,O,j,T=function(t){if(!v&&t in A)return A[t];switch(t){case h:return function(){return new e(this,t)};case y:return function(){return new e(this,t)}}return function(){return new e(this,t)}},E=n+" Iterator",S=x==y,P=!1,A=t.prototype,M=A[p]||A[d]||x&&A[x],k=M||T(x),I=x?S?T("entries"):k:void 0,R="Array"==n?A.entries||M:M;if(R&&(j=l(R.call(new t)),j!==Object.prototype&&(f(j,E,!0),r||u(j,p)||c(j,p,m))),S&&M&&M.name!==y&&(P=!0,k=function(){return M.call(this)}),r&&!g||!v&&!P&&A[p]||c(A,p,k),a[n]=k,a[E]=m,x)if(b={values:S?k:T(y),keys:w?k:T(h),entries:I},g)for(O in b)O in A||i(A,O,b[O]);else o(o.P+o.F*(v||P),n,b);return b}},function(t,n,e){var r=e(4),o=e(64),i=e(29),c=e(25)("IE_PROTO"),u=function(){},a="prototype",s=function(){var t,n=e(23)("iframe"),r=i.length,o=">";for(n.style.display="none",e(34).appendChild(n),n.src="javascript:",t=n.contentWindow.document,t.open(),t.write("<script>document.F=Object</script"+o),t.close(),s=t.F;r--;)delete s[a][i[r]];return s()};t.exports=Object.create||function(t,n){var e;return null!==t?(u[a]=r(t),e=new u,u[a]=null,e[c]=t):e=s(),void 0===n?e:o(e,n)}},function(t,n){n.f={}.propertyIsEnumerable},function(t,n,e){var r,o,i,c=e(11),u=e(56),a=e(34),s=e(23),f=e(2),l=f.process,p=f.setImmediate,v=f.clearImmediate,d=f.MessageChannel,h=0,y={},m="onreadystatechange",_=function(){var t=+this;if(y.hasOwnProperty(t)){var n=y[t];delete y[t],n()}},x=function(t){_.call(t.data)};p&&v||(p=function(t){for(var n=[],e=1;arguments.length>e;)n.push(arguments[e++]);return y[++h]=function(){u("function"==typeof t?t:Function(t),n)},r(h),h},v=function(t){delete y[t]},"process"==e(14)(l)?r=function(t){l.nextTick(c(_,t,1))}:d?(o=new d,i=o.port2,o.port1.onmessage=x,r=c(i.postMessage,i,1)):f.addEventListener&&"function"==typeof postMessage&&!f.importScripts?(r=function(t){f.postMessage(t+"","*")},f.addEventListener("message",x,!1)):r=m in s("script")?function(t){a.appendChild(s("script"))[m]=function(){a.removeChild(this),_.call(t)}}:function(t){setTimeout(c(_,t,1),0)}),t.exports={set:p,clear:v}},function(t,n,e){var r=e(26),o=Math.min;t.exports=function(t){return t>0?o(r(t),9007199254740991):0}},function(t,n,e){"use strict";var r=e(68)(!0);e(36)(String,"String",function(t){this._t=String(t),this._i=0},function(){var t,n=this._t,e=this._i;return e>=n.length?{value:void 0,done:!0}:(t=r(n,e),this._i+=t.length,{value:t,done:!1})})},function(t,n,e){e(72);for(var r=e(2),o=e(6),i=e(12),c=e(1)("toStringTag"),u=["NodeList","DOMTokenList","MediaList","StyleSheetList","CSSRuleList"],a=0;5>a;a++){var s=u[a],f=r[s],l=f&&f.prototype;l&&!l[c]&&o(l,c,s),i[s]=i.Array}},function(t,n,e){var r=e(38),o=e(18),i=e(10),c=e(32),u=e(7),a=e(35),s=Object.getOwnPropertyDescriptor;n.f=e(5)?s:function(t,n){if(t=i(t),n=c(n,!0),a)try{return s(t,n)}catch(e){}return u(t,n)?o(!r.f.call(t,n),t[n]):void 0}},function(t,n,e){var r=e(7),o=e(48),i=e(25)("IE_PROTO"),c=Object.prototype;t.exports=Object.getPrototypeOf||function(t){return t=o(t),r(t,i)?t[i]:"function"==typeof t.constructor&&t instanceof t.constructor?t.constructor.prototype:t instanceof Object?c:null}},function(t,n,e){var r=e(7),o=e(10),i=e(54)(!1),c=e(25)("IE_PROTO");t.exports=function(t,n){var e,u=o(t),a=0,s=[];for(e in u)e!=c&&r(u,e)&&s.push(e);for(;n.length>a;)r(u,e=n[a++])&&(~i(s,e)||s.push(e));return s}},function(t,n,e){t.exports=e(6)},function(t,n,e){var r=e(9),o=e(4),i=function(t,n){if(o(t),!r(n)&&null!==n)throw TypeError(n+": can't set as prototype!")};t.exports={set:Object.setPrototypeOf||("__proto__"in{}?function(t,n,r){try{r=e(11)(Function.call,e(43).f(Object.prototype,"__proto__").set,2),r(t,[]),n=!(t instanceof Array)}catch(o){n=!0}return function(t,e){return i(t,e),n?t.__proto__=e:r(t,e),t}}({},!1):void 0),check:i}},function(t,n,e){var r=e(22);t.exports=function(t){return Object(r(t))}},function(t,n,e){var r=e(33),o=e(1)("iterator"),i=e(12);t.exports=e(3).getIteratorMethod=function(t){return void 0!=t?t[o]||t["@@iterator"]||i[r(t)]:void 0}},function(t,n){},function(t,n,e){e(50),e(41),e(42),e(73),t.exports=e(3).Promise},function(t,n){t.exports=function(){}},function(t,n){t.exports=function(t,n,e,r){if(!(t instanceof n)||void 0!==r&&r in t)throw TypeError(e+": incorrect invocation!");return t}},function(t,n,e){var r=e(10),o=e(40),i=e(69);t.exports=function(t){return function(n,e,c){var u,a=r(n),s=o(a.length),f=i(c,s);if(t&&e!=e){for(;s>f;)if(u=a[f++],u!=u)return!0}else for(;s>f;f++)if((t||f in a)&&a[f]===e)return t||f||0;return!t&&-1}}},function(t,n,e){var r=e(11),o=e(59),i=e(58),c=e(4),u=e(40),a=e(49),s={},f={},n=t.exports=function(t,n,e,l,p){var v,d,h,y,m=p?function(){return t}:a(t),_=r(e,l,n?2:1),x=0;if("function"!=typeof m)throw TypeError(t+" is not iterable!");if(i(m)){for(v=u(t.length);v>x;x++)if(y=n?_(c(d=t[x])[0],d[1]):_(t[x]),y===s||y===f)return y}else for(h=m.call(t);!(d=h.next()).done;)if(y=o(h,_,d.value,n),y===s||y===f)return y};n.BREAK=s,n.RETURN=f},function(t,n){t.exports=function(t,n,e){var r=void 0===e;switch(n.length){case 0:return r?t():t.call(e);case 1:return r?t(n[0]):t.call(e,n[0]);case 2:return r?t(n[0],n[1]):t.call(e,n[0],n[1]);case 3:return r?t(n[0],n[1],n[2]):t.call(e,n[0],n[1],n[2]);case 4:return r?t(n[0],n[1],n[2],n[3]):t.call(e,n[0],n[1],n[2],n[3])}return t.apply(e,n)}},function(t,n,e){var r=e(14);t.exports=Object("z").propertyIsEnumerable(0)?Object:function(t){return"String"==r(t)?t.split(""):Object(t)}},function(t,n,e){var r=e(12),o=e(1)("iterator"),i=Array.prototype;t.exports=function(t){return void 0!==t&&(r.Array===t||i[o]===t)}},function(t,n,e){var r=e(4);t.exports=function(t,n,e,o){try{return o?n(r(e)[0],e[1]):n(e)}catch(i){var c=t["return"];throw void 0!==c&&r(c.call(t)),i}}},function(t,n,e){"use strict";var r=e(37),o=e(18),i=e(19),c={};e(6)(c,e(1)("iterator"),function(){return this}),t.exports=function(t,n,e){t.prototype=r(c,{next:o(1,e)}),i(t,n+" Iterator")}},function(t,n,e){var r=e(1)("iterator"),o=!1;try{var i=[7][r]();i["return"]=function(){o=!0},Array.from(i,function(){throw 2})}catch(c){}t.exports=function(t,n){if(!n&&!o)return!1;var e=!1;try{var i=[7],c=i[r]();c.next=function(){return{done:e=!0}},i[r]=function(){return c},t(i)}catch(u){}return e}},function(t,n){t.exports=function(t,n){return{value:n,done:!!t}}},function(t,n,e){var r=e(2),o=e(39).set,i=r.MutationObserver||r.WebKitMutationObserver,c=r.process,u=r.Promise,a="process"==e(14)(c);t.exports=function(){var t,n,e,s=function(){var r,o;for(a&&(r=c.domain)&&r.exit();t;){o=t.fn,t=t.next;try{o()}catch(i){throw t?e():n=void 0,i}}n=void 0,r&&r.enter()};if(a)e=function(){c.nextTick(s)};else if(i){var f=!0,l=document.createTextNode("");new i(s).observe(l,{characterData:!0}),e=function(){l.data=f=!f}}else if(u&&u.resolve){var p=u.resolve();e=function(){p.then(s)}}else e=function(){o.call(r,s)};return function(r){var o={fn:r,next:void 0};n&&(n.next=o),t||(t=o,e()),n=o}}},function(t,n,e){var r=e(8),o=e(4),i=e(30);t.exports=e(5)?Object.defineProperties:function(t,n){o(t);for(var e,c=i(n),u=c.length,a=0;u>a;)r.f(t,e=c[a++],n[e]);return t}},function(t,n,e){var r=e(6);t.exports=function(t,n,e){for(var o in n)e&&t[o]?t[o]=n[o]:r(t,o,n[o]);return t}},function(t,n,e){"use strict";var r=e(2),o=e(3),i=e(8),c=e(5),u=e(1)("species");t.exports=function(t){var n="function"==typeof o[t]?o[t]:r[t];c&&n&&!n[u]&&i.f(n,u,{configurable:!0,get:function(){return this}})}},function(t,n,e){var r=e(4),o=e(21),i=e(1)("species");t.exports=function(t,n){var e,c=r(t).constructor;return void 0===c||void 0==(e=r(c)[i])?n:o(e)}},function(t,n,e){var r=e(26),o=e(22);t.exports=function(t){return function(n,e){var i,c,u=String(o(n)),a=r(e),s=u.length;return 0>a||a>=s?t?"":void 0:(i=u.charCodeAt(a),55296>i||i>56319||a+1===s||(c=u.charCodeAt(a+1))<56320||c>57343?t?u.charAt(a):i:t?u.slice(a,a+2):(i-55296<<10)+(c-56320)+65536)}}},function(t,n,e){var r=e(26),o=Math.max,i=Math.min;t.exports=function(t,n){return t=r(t),0>t?o(t+n,0):i(t,n)}},,,function(t,n,e){"use strict";var r=e(52),o=e(62),i=e(12),c=e(10);t.exports=e(36)(Array,"Array",function(t,n){this._t=c(t),this._i=0,this._k=n},function(){var t=this._t,n=this._k,e=this._i++;return!t||e>=t.length?(this._t=void 0,o(1)):"keys"==n?o(0,e):"values"==n?o(0,t[e]):o(0,[e,t[e]])},"values"),i.Arguments=i.Array,r("keys"),r("values"),r("entries")},function(t,n,e){"use strict";var r,o,i,c=e(24),u=e(2),a=e(11),s=e(33),f=e(15),l=e(9),p=(e(4),e(21)),v=e(53),d=e(55),h=(e(47).set,e(67)),y=e(39).set,m=e(63)(),_="Promise",x=u.TypeError,w=u.process,g=u[_],w=u.process,b="process"==s(w),O=function(){},j=!!function(){try{var t=g.resolve(1),n=(t.constructor={})[e(1)("species")]=function(t){t(O,O)};return(b||"function"==typeof PromiseRejectionEvent)&&t.then(O)instanceof n}catch(r){}}(),T=function(t,n){return t===n||t===g&&n===i},E=function(t){var n;return l(t)&&"function"==typeof(n=t.then)?n:!1},S=function(t){return T(g,t)?new P(t):new o(t)},P=o=function(t){var n,e;this.promise=new t(function(t,r){if(void 0!==n||void 0!==e)throw x("Bad Promise constructor");n=t,e=r}),this.resolve=p(n),this.reject=p(e)},A=function(t){try{t()}catch(n){return{error:n}}},M=function(t,n){if(!t._n){t._n=!0;var e=t._c;m(function(){for(var r=t._v,o=1==t._s,i=0,c=function(n){var e,i,c=o?n.ok:n.fail,u=n.resolve,a=n.reject,s=n.domain;try{c?(o||(2==t._h&&R(t),t._h=1),c===!0?e=r:(s&&s.enter(),e=c(r),s&&s.exit()),e===n.promise?a(x("Promise-chain cycle")):(i=E(e))?i.call(e,u,a):u(e)):a(r)}catch(f){a(f)}};e.length>i;)c(e[i++]);t._c=[],t._n=!1,n&&!t._h&&k(t)})}},k=function(t){y.call(u,function(){var n,e,r,o=t._v;if(I(t)&&(n=A(function(){b?w.emit("unhandledRejection",o,t):(e=u.onunhandledrejection)?e({promise:t,reason:o}):(r=u.console)&&r.error&&r.error("Unhandled promise rejection",o)}),t._h=b||I(t)?2:1),t._a=void 0,n)throw n.error})},I=function(t){if(1==t._h)return!1;for(var n,e=t._a||t._c,r=0;e.length>r;)if(n=e[r++],n.fail||!I(n.promise))return!1;return!0},R=function(t){y.call(u,function(){var n;b?w.emit("rejectionHandled",t):(n=u.onrejectionhandled)&&n({promise:t,reason:t._v})})},F=function(t){var n=this;n._d||(n._d=!0,n=n._w||n,n._v=t,n._s=2,n._a||(n._a=n._c.slice()),M(n,!0))},C=function(t){var n,e=this;if(!e._d){e._d=!0,e=e._w||e;try{if(e===t)throw x("Promise can't be resolved itself");(n=E(t))?m(function(){var r={_w:e,_d:!1};try{n.call(t,a(C,r,1),a(F,r,1))}catch(o){F.call(r,o)}}):(e._v=t,e._s=1,M(e,!1))}catch(r){F.call({_w:e,_d:!1},r)}}};j||(g=function(t){v(this,g,_,"_h"),p(t),r.call(this);try{t(a(C,this,1),a(F,this,1))}catch(n){F.call(this,n)}},r=function(t){this._c=[],this._a=void 0,this._s=0,this._d=!1,this._v=void 0,this._h=0,this._n=!1},r.prototype=e(65)(g.prototype,{then:function(t,n){var e=S(h(this,g));return e.ok="function"==typeof t?t:!0,e.fail="function"==typeof n&&n,e.domain=b?w.domain:void 0,this._c.push(e),this._a&&this._a.push(e),this._s&&M(this,!1),e.promise},"catch":function(t){return this.then(void 0,t)}}),P=function(){var t=new r;this.promise=t,this.resolve=a(C,t,1),this.reject=a(F,t,1)}),f(f.G+f.W+f.F*!j,{Promise:g}),e(19)(g,_),e(66)(_),i=e(3)[_],f(f.S+f.F*!j,_,{reject:function(t){var n=S(this),e=n.reject;return e(t),n.promise}}),f(f.S+f.F*(c||!j),_,{resolve:function(t){if(t instanceof g&&T(t.constructor,this))return t;var n=S(this),e=n.resolve;return e(t),n.promise}}),f(f.S+f.F*!(j&&e(61)(function(t){g.all(t)["catch"](O)})),_,{all:function(t){var n=this,e=S(n),r=e.resolve,o=e.reject,i=A(function(){var e=[],i=0,c=1;d(t,!1,function(t){var u=i++,a=!1;e.push(void 0),c++,n.resolve(t).then(function(t){a||(a=!0,e[u]=t,--c||r(e))},o)}),--c||r(e)});return i&&o(i.error),e.promise},race:function(t){var n=this,e=S(n),r=e.reject,o=A(function(){d(t,!1,function(t){n.resolve(t).then(e.resolve,r)})});return o&&r(o.error),e.promise}})},,,,,,,,,,,,,,,,,function(t,n,e){"use strict";function r(t){return t&&t.__esModule?t:{"default":t}}function o(){var t=arguments.length<=0||void 0===arguments[0]?1e3:arguments[0];return new u["default"](function(n,e){var r=s.currentTime;p.gain.linearRampToValueAtTime(0,r),p.gain.linearRampToValueAtTime(1,r+t/1e3),setTimeout(function(){n("ok")},t)})}function i(){var t=arguments.length<=0||void 0===arguments[0]?1e3:arguments[0];return new u["default"](function(n,e){var r=s.currentTime;p.gain.linearRampToValueAtTime(p.gain.value,r),p.gain.linearRampToValueAtTime(0,r+t/1e3),setTimeout(function(){n("ok")},t)})}Object.defineProperty(n,"__esModule",{value:!0});var c=e(20),u=r(c),a=new Audio;a.crossOrigin="anonymous",window.AudioContext=window.AudioContext||window.webkitAudioContext;var s=new AudioContext,f=s.createMediaElementSource(a),l=s.createGain(),p=s.createGain();f.connect(p),p.connect(s.destination);for(var v=[],d=[32,64,125,250,500,1e3,2e3,4e3,8e3,16e3],h=0;10>h;h++){var y=s.createBiquadFilter();y.type=y.PEAKING||"peaking",y.Q.value=1.4,y.gain.value=0,y.frequency.value=d[h],v.push(y)}for(var m=0;m<v.length-1;m++)v[m].connect(v[m+1]);var _={bruce:[0,-2,0,2,1,0,0,0,0,-2,-4],opera:[0,0,0,0,4,5,3,6,3,0,0],metal:[0,-6,0,0,0,0,0,4,0,4,0],dance:[-2,5,9,5,0,4,-4,-4,8,-2,4],pop:[-1,3,7,5,-2,3,7,-2,4,6,5],rock:[-3,7,7,9,-3,2,-3,-1,6,9,7],voice:[0,-6,-2,-4,-8,2,6,8,6,-2,-6],country:[0,-2,0,0,2,2,0,0,0,4,4],suggest:[-1,3,4,5,0,-1,3,4,5,6,7],electronic:[0,-6,1,4,-2,-2,-4,0,0,6,6],jazz:[-1,-2,5,5,1,-6,3,1,4,6,2],classic:[-3,10,8,3,1,0,0,1,3,8,10],old:[0,-4,0,2,1,0,0,0,0,-4,-6]},x=!1;a.addEventListener("progress",function(){var t=a.buffered;if(t.length>0){var n=a.buffered.end(0);!a.paused&&n-a.currentTime<6&&parseInt(n)!==parseInt(a.duration)?(a.pause(),x=!0):a.paused&&x&&(n-a.currentTime>20||parseInt(n)===parseInt(a.duration))&&(a.play(),x=!1)}},!1),n["default"]={setSrc:function(t){a.src=t},volume:function(t){p.gain.linearRampToValueAtTime(t,this.context.currentTime)},fadeOut:i,play:function(t){var n=this;"running"!==s.state&&s.resume?s.resume().then(function(){n._play(t)})["catch"](function(){n._play(t)}):this._play(t)},_play:function(t){var n=this;a.paused?(t&&this.setSrc(t),a.play(),o()):i().then(function(){t&&n.setSrc(t),a.play(),o()})},pause:function(){i().then(function(){a.pause()})},seek:function(t){a.currentTime=t,a.play(),o()},enableEQ:function(){f.disconnect(),p.disconnect(),v[9].connect(l),l.connect(p),f.connect(v[0]),p.connect(s.destination)},disableEQ:function(){v[9].disconnect(),l.disconnect(),f.disconnect(),p.disconnect(),f.connect(p),p.connect(s.destination)},setEffect:function(t){for(var n=0;n<t.length;n++){var e=t[n];0!==n?v[n-1].gain.value=e:((-12>e||e>12)&&(e=0),l.gain.value=Math.pow(10,e/12))}},audio:a,EQ_EFFECT:_}}]);