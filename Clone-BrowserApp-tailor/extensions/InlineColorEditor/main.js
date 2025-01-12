
// TinyColor.js - <https://github.com/bgrins/TinyColor> - 2012 Brian Grinstead - v0.9.10
(function(G){function e(b,d){var a,c,n,i,h,b=b?b:"";if("object"==typeof b&&b.hasOwnProperty("_tc_id"))return b;c=b;h=i=n=255;var C=1,o=a=!1;if("string"==typeof c)a:{c=c.replace(H,"").replace(I,"").toLowerCase();var u=!1;if(z[c])c=z[c],u=!0;else if("transparent"==c){c={r:0,g:0,b:0,a:0};break a}var f;c=(f=t.rgb.exec(c))?{r:f[1],g:f[2],b:f[3]}:(f=t.rgba.exec(c))?{r:f[1],g:f[2],b:f[3],a:f[4]}:(f=t.hsl.exec(c))?{h:f[1],s:f[2],l:f[3]}:(f=t.hsla.exec(c))?{h:f[1],s:f[2],l:f[3],a:f[4]}:(f=t.hsv.exec(c))?{h:f[1],
s:f[2],v:f[3]}:(f=t.hex6.exec(c))?{r:parseInt(f[1],16),g:parseInt(f[2],16),b:parseInt(f[3],16),format:u?"name":"hex"}:(f=t.hex3.exec(c))?{r:parseInt(f[1]+""+f[1],16),g:parseInt(f[2]+""+f[2],16),b:parseInt(f[3]+""+f[3],16),format:u?"name":"hex"}:!1}if("object"==typeof c&&(c.hasOwnProperty("r")&&c.hasOwnProperty("g")&&c.hasOwnProperty("b")?(n=255*j(c.r,255),i=255*j(c.g,255),h=255*j(c.b,255),a=!0,o="%"===String(c.r).substr(-1)?"prgb":"rgb"):c.hasOwnProperty("h")&&c.hasOwnProperty("s")&&c.hasOwnProperty("v")?
(c.s=w(c.s),c.v=w(c.v),a=c.h,i=c.s,h=c.v,a=6*j(a,360),i=j(i,100),h=j(h,100),n=v.floor(a),f=a-n,a=h*(1-i),o=h*(1-f*i),f=h*(1-(1-f)*i),u=n%6,n=255*[h,o,a,a,f,h][u],i=255*[f,h,h,o,a,a][u],h=255*[a,a,f,h,h,o][u],a=!0,o="hsv"):c.hasOwnProperty("h")&&(c.hasOwnProperty("s")&&c.hasOwnProperty("l"))&&(c.s=w(c.s),c.l=w(c.l),n=c.h,a=c.s,h=c.l,i=function(a,b,c){0>c&&(c+=1);1<c&&(c-=1);return c<1/6?a+6*(b-a)*c:0.5>c?b:c<2/3?a+6*(b-a)*(2/3-c):a},n=j(n,360),a=j(a,100),h=j(h,100),0==a?a=h=o=h:(o=0.5>h?h*(1+a):h+
a-h*a,f=2*h-o,a=i(f,o,n+1/3),h=i(f,o,n),o=i(f,o,n-1/3)),n=255*a,i=255*h,h=255*o,a=!0,o="hsl"),c.hasOwnProperty("a")))C=c.a;c=c.format||o;n=q(255,r(n,0));i=q(255,r(i,0));h=q(255,r(h,0));var k=n,l=i,m=h,p=parseFloat(C),s=g(100*p)/100;1>k&&(k=g(k));1>l&&(l=g(l));1>m&&(m=g(m));return{ok:a,format:c,_tc_id:J++,alpha:p,toHsv:function(){var a=D(k,l,m);return{h:360*a.h,s:a.s,v:a.v,a:p}},toHsvString:function(){var a=D(k,l,m),b=g(360*a.h),c=g(100*a.s),a=g(100*a.v);return 1==p?"hsv("+b+", "+c+"%, "+a+"%)":"hsva("+
b+", "+c+"%, "+a+"%, "+s+")"},toHsl:function(){var a=E(k,l,m);return{h:360*a.h,s:a.s,l:a.l,a:p}},toHslString:function(){var a=E(k,l,m),b=g(360*a.h),c=g(100*a.s),a=g(100*a.l);return 1==p?"hsl("+b+", "+c+"%, "+a+"%)":"hsla("+b+", "+c+"%, "+a+"%, "+s+")"},toHex:function(){return y(k,l,m)},toHexString:function(){return"#"+y(k,l,m)},toRgb:function(){return{r:g(k),g:g(l),b:g(m),a:p}},toRgbString:function(){return 1==p?"rgb("+g(k)+", "+g(l)+", "+g(m)+")":"rgba("+g(k)+", "+g(l)+", "+g(m)+", "+s+")"},toPercentageRgb:function(){return{r:g(100*
j(k,255))+"%",g:g(100*j(l,255))+"%",b:g(100*j(m,255))+"%",a:p}},toPercentageRgbString:function(){return 1==p?"rgb("+g(100*j(k,255))+"%, "+g(100*j(l,255))+"%, "+g(100*j(m,255))+"%)":"rgba("+g(100*j(k,255))+"%, "+g(100*j(l,255))+"%, "+g(100*j(m,255))+"%, "+s+")"},toName:function(){return K[y(k,l,m)]||!1},toFilter:function(){var a=secondHex=y(k,l,m),b=secondAlphaHex=Math.round(255*parseFloat(p)).toString(16),c=d&&d.gradientType?"GradientType = 1, ":"";if(secondColor){var f=e(secondColor);secondHex=f.toHex();
secondAlphaHex=Math.round(255*parseFloat(f.alpha)).toString(16)}return"progid:DXImageTransform.Microsoft.gradient("+c+"startColorstr=#"+x(b)+a+",endColorstr=#"+x(secondAlphaHex)+secondHex+")"},toString:function(a){var a=a||this.format,b=!1;"rgb"===a&&(b=this.toRgbString());"prgb"===a&&(b=this.toPercentageRgbString());"hex"===a&&(b=this.toHexString());"name"===a&&(b=this.toName());"hsl"===a&&(b=this.toHslString());"hsv"===a&&(b=this.toHsvString());return b||this.toHexString()}}}function E(b,d,a){var b=
j(b,255),d=j(d,255),a=j(a,255),c=r(b,d,a),e=q(b,d,a),i,h=(c+e)/2;if(c==e)i=e=0;else{var g=c-e,e=0.5<h?g/(2-c-e):g/(c+e);switch(c){case b:i=(d-a)/g+(d<a?6:0);break;case d:i=(a-b)/g+2;break;case a:i=(b-d)/g+4}i/=6}return{h:i,s:e,l:h}}function D(b,d,a){var b=j(b,255),d=j(d,255),a=j(a,255),c=r(b,d,a),e=q(b,d,a),g,h=c-e;if(c==e)g=0;else{switch(c){case b:g=(d-a)/h+(d<a?6:0);break;case d:g=(a-b)/h+2;break;case a:g=(b-d)/h+4}g/=6}return{h:g,s:0==c?0:h/c,v:c}}function y(b,d,a){b=[x(g(b).toString(16)),x(g(d).toString(16)),
x(g(a).toString(16))];return b[0][0]==b[0][1]&&b[1][0]==b[1][1]&&b[2][0]==b[2][1]?b[0][0]+b[1][0]+b[2][0]:b.join("")}function j(b,d){"string"==typeof b&&(-1!=b.indexOf(".")&&1===parseFloat(b))&&(b="100%");var a="string"===typeof b&&-1!=b.indexOf("%"),b=q(d,r(0,parseFloat(b)));a&&(b=parseInt(b*d)/100);return 1E-6>v.abs(b-d)?1:b%d/parseFloat(d)}function x(b){return 1==b.length?"0"+b:""+b}function w(b){1>=b&&(b=100*b+"%");return b}var H=/^[\s,#]+/,I=/\s+$/,J=0,v=Math,g=v.round,q=v.min,r=v.max,A=v.random;
e.fromRatio=function(b){if("object"==typeof b){var d={},a;for(a in b)d[a]=w(b[a]);b=d}return e(b)};e.equals=function(b,d){return!b||!d?!1:e(b).toRgbString()==e(d).toRgbString()};e.random=function(){return e.fromRatio({r:A(),g:A(),b:A()})};e.desaturate=function(b,d){var a=e(b).toHsl();a.s-=(d||10)/100;a.s=q(1,r(0,a.s));return e(a)};e.saturate=function(b,d){var a=e(b).toHsl();a.s+=(d||10)/100;a.s=q(1,r(0,a.s));return e(a)};e.greyscale=function(b){return e.desaturate(b,100)};e.lighten=function(b,d){var a=
e(b).toHsl();a.l+=(d||10)/100;a.l=q(1,r(0,a.l));return e(a)};e.darken=function(b,d){var a=e(b).toHsl();a.l-=(d||10)/100;a.l=q(1,r(0,a.l));return e(a)};e.complement=function(b){b=e(b).toHsl();b.h=(b.h+180)%360;return e(b)};e.triad=function(b){var d=e(b).toHsl(),a=d.h;return[e(b),e({h:(a+120)%360,s:d.s,l:d.l}),e({h:(a+240)%360,s:d.s,l:d.l})]};e.tetrad=function(b){var d=e(b).toHsl(),a=d.h;return[e(b),e({h:(a+90)%360,s:d.s,l:d.l}),e({h:(a+180)%360,s:d.s,l:d.l}),e({h:(a+270)%360,s:d.s,l:d.l})]};e.splitcomplement=
function(b){var d=e(b).toHsl(),a=d.h;return[e(b),e({h:(a+72)%360,s:d.s,l:d.l}),e({h:(a+216)%360,s:d.s,l:d.l})]};e.analogous=function(b,d,a){var d=d||6,a=a||30,c=e(b).toHsl(),a=360/a,b=[e(b)];for(c.h=(c.h-(a*d>>1)+720)%360;--d;)c.h=(c.h+a)%360,b.push(e(c));return b};e.monochromatic=function(b,d){for(var d=d||6,a=e(b).toHsv(),c=a.h,g=a.s,a=a.v,i=[],h=1/d;d--;)i.push(e({h:c,s:g,v:a})),a=(a+h)%1;return i};e.readable=function(b,d){var a=e(b).toRgb(),c=e(d).toRgb();return 10404<(c.r-a.r)*(c.r-a.r)+(c.g-
a.g)*(c.g-a.g)+(c.b-a.b)*(c.b-a.b)};var z=e.names={aliceblue:"f0f8ff",antiquewhite:"faebd7",aqua:"0ff",aquamarine:"7fffd4",azure:"f0ffff",beige:"f5f5dc",bisque:"ffe4c4",black:"000",blanchedalmond:"ffebcd",blue:"00f",blueviolet:"8a2be2",brown:"a52a2a",burlywood:"deb887",burntsienna:"ea7e5d",cadetblue:"5f9ea0",chartreuse:"7fff00",chocolate:"d2691e",coral:"ff7f50",cornflowerblue:"6495ed",cornsilk:"fff8dc",crimson:"dc143c",cyan:"0ff",darkblue:"00008b",darkcyan:"008b8b",darkgoldenrod:"b8860b",darkgray:"a9a9a9",
darkgreen:"006400",darkgrey:"a9a9a9",darkkhaki:"bdb76b",darkmagenta:"8b008b",darkolivegreen:"556b2f",darkorange:"ff8c00",darkorchid:"9932cc",darkred:"8b0000",darksalmon:"e9967a",darkseagreen:"8fbc8f",darkslateblue:"483d8b",darkslategray:"2f4f4f",darkslategrey:"2f4f4f",darkturquoise:"00ced1",darkviolet:"9400d3",deeppink:"ff1493",deepskyblue:"00bfff",dimgray:"696969",dimgrey:"696969",dodgerblue:"1e90ff",firebrick:"b22222",floralwhite:"fffaf0",forestgreen:"228b22",fuchsia:"f0f",gainsboro:"dcdcdc",ghostwhite:"f8f8ff",
gold:"ffd700",goldenrod:"daa520",gray:"808080",green:"008000",greenyellow:"adff2f",grey:"808080",honeydew:"f0fff0",hotpink:"ff69b4",indianred:"cd5c5c",indigo:"4b0082",ivory:"fffff0",khaki:"f0e68c",lavender:"e6e6fa",lavenderblush:"fff0f5",lawngreen:"7cfc00",lemonchiffon:"fffacd",lightblue:"add8e6",lightcoral:"f08080",lightcyan:"e0ffff",lightgoldenrodyellow:"fafad2",lightgray:"d3d3d3",lightgreen:"90ee90",lightgrey:"d3d3d3",lightpink:"ffb6c1",lightsalmon:"ffa07a",lightseagreen:"20b2aa",lightskyblue:"87cefa",
lightslategray:"789",lightslategrey:"789",lightsteelblue:"b0c4de",lightyellow:"ffffe0",lime:"0f0",limegreen:"32cd32",linen:"faf0e6",magenta:"f0f",maroon:"800000",mediumaquamarine:"66cdaa",mediumblue:"0000cd",mediumorchid:"ba55d3",mediumpurple:"9370db",mediumseagreen:"3cb371",mediumslateblue:"7b68ee",mediumspringgreen:"00fa9a",mediumturquoise:"48d1cc",mediumvioletred:"c71585",midnightblue:"191970",mintcream:"f5fffa",mistyrose:"ffe4e1",moccasin:"ffe4b5",navajowhite:"ffdead",navy:"000080",oldlace:"fdf5e6",
olive:"808000",olivedrab:"6b8e23",orange:"ffa500",orangered:"ff4500",orchid:"da70d6",palegoldenrod:"eee8aa",palegreen:"98fb98",paleturquoise:"afeeee",palevioletred:"db7093",papayawhip:"ffefd5",peachpuff:"ffdab9",peru:"cd853f",pink:"ffc0cb",plum:"dda0dd",powderblue:"b0e0e6",purple:"800080",red:"f00",rosybrown:"bc8f8f",royalblue:"4169e1",saddlebrown:"8b4513",salmon:"fa8072",sandybrown:"f4a460",seagreen:"2e8b57",seashell:"fff5ee",sienna:"a0522d",silver:"c0c0c0",skyblue:"87ceeb",slateblue:"6a5acd",slategray:"708090",
slategrey:"708090",snow:"fffafa",springgreen:"00ff7f",steelblue:"4682b4",tan:"d2b48c",teal:"008080",thistle:"d8bfd8",tomato:"ff6347",turquoise:"40e0d0",violet:"ee82ee",wheat:"f5deb3",white:"fff",whitesmoke:"f5f5f5",yellow:"ff0",yellowgreen:"9acd32"},L=e,B=z,F={},s;for(s in B)B.hasOwnProperty(s)&&(F[B[s]]=s);var K=L.hexNames=F,t;t={rgb:RegExp("rgb[\\s|\\(]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))\\s*\\)?"),
rgba:RegExp("rgba[\\s|\\(]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))\\s*\\)?"),hsl:RegExp("hsl[\\s|\\(]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))\\s*\\)?"),hsla:RegExp("hsla[\\s|\\(]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))\\s*\\)?"),
hsv:RegExp("hsv[\\s|\\(]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))[,|\\s]+((?:[-\\+]?\\d*\\.\\d+%?)|(?:[-\\+]?\\d+%?))\\s*\\)?"),hex3:/^([0-9a-fA-F]{1})([0-9a-fA-F]{1})([0-9a-fA-F]{1})$/,hex6:/^([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})$/};"undefined"!==typeof module&&module.exports?module.exports=e:G.tinycolor=e})(this);

define("thirdparty/tinycolor-min", function(){});

/*!
 *  Copyright 2011 Twitter, Inc.
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */



var Hogan = {};

(function (Hogan, useArrayBuffer) {
  Hogan.Template = function (codeObj, text, compiler, options) {
    codeObj = codeObj || {};
    this.r = codeObj.code || this.r;
    this.c = compiler;
    this.options = options || {};
    this.text = text || '';
    this.partials = codeObj.partials || {};
    this.subs = codeObj.subs || {};
    this.ib();
  }

  Hogan.Template.prototype = {
    // render: replaced by generated code.
    r: function (context, partials, indent) { return ''; },

    // variable escaping
    v: hoganEscape,

    // triple stache
    t: coerceToString,

    render: function render(context, partials, indent) {
      return this.ri([context], partials || {}, indent);
    },

    // render internal -- a hook for overrides that catches partials too
    ri: function (context, partials, indent) {
      return this.r(context, partials, indent);
    },

    // ensurePartial
    ep: function(symbol, partials) {
      var partial = this.partials[symbol];

      // check to see that if we've instantiated this partial before
      var template = partials[partial.name];
      if (partial.instance && partial.base == template) {
        return partial.instance;
      }

      if (typeof template == 'string') {
        if (!this.c) {
          throw new Error("No compiler available.");
        }
        template = this.c.compile(template, this.options);
      }

      if (!template) {
        return null;
      }

      // We use this to check whether the partials dictionary has changed
      this.partials[symbol].base = template;

      if (partial.subs) {
        // Make sure we consider parent template now
        if (this.activeSub === undefined) {
          // Store parent template text in partials.stackText to perform substitutions in child templates correctly
          partials.stackText  = this.text;
        }
         template = createSpecializedPartial(template, partial.subs, partial.partials, partials.stackText || this.text);
       }
      this.partials[symbol].instance = template;
      return template;
    },

    // tries to find a partial in the current scope and render it
    rp: function(symbol, context, partials, indent) {
      var partial = this.ep(symbol, partials);
      if (!partial) {
        return '';
      }

      return partial.ri(context, partials, indent);
    },

    // render a section
    rs: function(context, partials, section) {
      var tail = context[context.length - 1];

      if (!isArray(tail)) {
        section(context, partials, this);
        return;
      }

      for (var i = 0; i < tail.length; i++) {
        context.push(tail[i]);
        section(context, partials, this);
        context.pop();
      }
    },

    // maybe start a section
    s: function(val, ctx, partials, inverted, start, end, tags) {
      var pass;

      if (isArray(val) && val.length === 0) {
        return false;
      }

      if (typeof val == 'function') {
        val = this.ms(val, ctx, partials, inverted, start, end, tags);
      }

      pass = !!val;

      if (!inverted && pass && ctx) {
        ctx.push((typeof val == 'object') ? val : ctx[ctx.length - 1]);
      }

      return pass;
    },

    // find values with dotted names
    d: function(key, ctx, partials, returnFound) {
      var found,
          names = key.split('.'),
          val = this.f(names[0], ctx, partials, returnFound),
          doModelGet = this.options.modelGet,
          cx = null;

      if (key === '.' && isArray(ctx[ctx.length - 2])) {
        val = ctx[ctx.length - 1];
      } else {
        for (var i = 1; i < names.length; i++) {
          found = findInScope(names[i], val, doModelGet);
          if (found != null) {
            cx = val;
            val = found;
          } else {
            val = '';
          }
        }
      }

      if (returnFound && !val) {
        return false;
      }

      if (!returnFound && typeof val == 'function') {
        ctx.push(cx);
        val = this.mv(val, ctx, partials);
        ctx.pop();
      }

      return val;
    },

    // find values with normal names
    f: function(key, ctx, partials, returnFound) {
      var val = false,
          v = null,
          found = false,
          doModelGet = this.options.modelGet;

      for (var i = ctx.length - 1; i >= 0; i--) {
        v = ctx[i];
        val = findInScope(key, v, doModelGet);
        if (val != null) {
          found = true;
          break;
        }
      }

      if (!found) {
        return (returnFound) ? false : "";
      }

      if (!returnFound && typeof val == 'function') {
        val = this.mv(val, ctx, partials);
      }

      return val;
    },

    // higher order templates
    ls: function(func, cx, partials, text, tags) {
      var oldTags = this.options.delimiters;

      this.options.delimiters = tags;
      this.b(this.ct(coerceToString(func.call(cx, text)), cx, partials));
      this.options.delimiters = oldTags;

      return false;
    },

    // compile text
    ct: function(text, cx, partials) {
      if (this.options.disableLambda) {
        throw new Error('Lambda features disabled.');
      }
      return this.c.compile(text, this.options).render(cx, partials);
    },

    // template result buffering
    b: (useArrayBuffer) ? function(s) { this.buf.push(s); } :
                          function(s) { this.buf += s; },

    fl: (useArrayBuffer) ? function() { var r = this.buf.join(''); this.buf = []; return r; } :
                           function() { var r = this.buf; this.buf = ''; return r; },
    // init the buffer
    ib: function () {
      this.buf = (useArrayBuffer) ? [] : '';
    },

    // method replace section
    ms: function(func, ctx, partials, inverted, start, end, tags) {
      var textSource,
          cx = ctx[ctx.length - 1],
          result = func.call(cx);

      if (typeof result == 'function') {
        if (inverted) {
          return true;
        } else {
          textSource = (this.activeSub && this.subsText[this.activeSub]) ? this.subsText[this.activeSub] : this.text;
          return this.ls(result, cx, partials, textSource.substring(start, end), tags);
        }
      }

      return result;
    },

    // method replace variable
    mv: function(func, ctx, partials) {
      var cx = ctx[ctx.length - 1];
      var result = func.call(cx);

      if (typeof result == 'function') {
        return this.ct(coerceToString(result.call(cx)), cx, partials);
      }

      return result;
    },

    sub: function(name, context, partials, indent) {
      var f = this.subs[name];
      if (f) {
        this.activeSub = name;
        f(context, partials, this, indent);
        this.activeSub = false;
      }
    }

  };

  //Find a key in an object
  function findInScope(key, scope, doModelGet) {
    var val, checkVal;

    if (scope && typeof scope == 'object') {

      if (scope[key] != null) {
        val = scope[key];

      // try lookup with get for backbone or similar model data
      } else if (doModelGet && scope.get && typeof scope.get == 'function') {
        val = scope.get(key);
      }
    }

    return val;
  }

  function createSpecializedPartial(instance, subs, partials, childText) {
    function PartialTemplate() {};
    PartialTemplate.prototype = instance;
    function Substitutions() {};
    Substitutions.prototype = instance.subs;
    var key;
    var partial = new PartialTemplate();
    partial.subs = new Substitutions();
    partial.subsText = {};  //hehe. substext.
    partial.ib();

    for (key in subs) {
      partial.subs[key] = subs[key];
      partial.subsText[key] = childText;
    }

    for (key in partials) {
      partial.partials[key] = partials[key];
    }

    return partial;
  }

  var rAmp = /&/g,
      rLt = /</g,
      rGt = />/g,
      rApos = /\'/g,
      rQuot = /\"/g,
      hChars = /[&<>\"\']/;

  function coerceToString(val) {
    return String((val === null || val === undefined) ? '' : val);
  }

  function hoganEscape(str) {
    str = coerceToString(str);
    return hChars.test(str) ?
      str
        .replace(rAmp, '&amp;')
        .replace(rLt, '&lt;')
        .replace(rGt, '&gt;')
        .replace(rApos, '&#39;')
        .replace(rQuot, '&quot;') :
      str;
  }

  var isArray = Array.isArray || function(a) {
    return Object.prototype.toString.call(a) === '[object Array]';
  };

})(typeof exports !== 'undefined' ? exports : Hogan);



(function (Hogan) {
  // Setup regex  assignments
  // remove whitespace according to Mustache spec
  var rIsWhitespace = /\S/,
      rQuot = /\"/g,
      rNewline =  /\n/g,
      rCr = /\r/g,
      rSlash = /\\/g;

  Hogan.tags = {
    '#': 1, '^': 2, '<': 3, '$': 4,
    '/': 5, '!': 6, '>': 7, '=': 8, '_v': 9,
    '{': 10, '&': 11, '_t': 12
  };

  Hogan.scan = function scan(text, delimiters) {
    var len = text.length,
        IN_TEXT = 0,
        IN_TAG_TYPE = 1,
        IN_TAG = 2,
        state = IN_TEXT,
        tagType = null,
        tag = null,
        buf = '',
        tokens = [],
        seenTag = false,
        i = 0,
        lineStart = 0,
        otag = '{{',
        ctag = '}}';

    function addBuf() {
      if (buf.length > 0) {
        tokens.push({tag: '_t', text: new String(buf)});
        buf = '';
      }
    }

    function lineIsWhitespace() {
      var isAllWhitespace = true;
      for (var j = lineStart; j < tokens.length; j++) {
        isAllWhitespace =
          (Hogan.tags[tokens[j].tag] < Hogan.tags['_v']) ||
          (tokens[j].tag == '_t' && tokens[j].text.match(rIsWhitespace) === null);
        if (!isAllWhitespace) {
          return false;
        }
      }

      return isAllWhitespace;
    }

    function filterLine(haveSeenTag, noNewLine) {
      addBuf();

      if (haveSeenTag && lineIsWhitespace()) {
        for (var j = lineStart, next; j < tokens.length; j++) {
          if (tokens[j].text) {
            if ((next = tokens[j+1]) && next.tag == '>') {
              // set indent to token value
              next.indent = tokens[j].text.toString()
            }
            tokens.splice(j, 1);
          }
        }
      } else if (!noNewLine) {
        tokens.push({tag:'\n'});
      }

      seenTag = false;
      lineStart = tokens.length;
    }

    function changeDelimiters(text, index) {
      var close = '=' + ctag,
          closeIndex = text.indexOf(close, index),
          delimiters = trim(
            text.substring(text.indexOf('=', index) + 1, closeIndex)
          ).split(' ');

      otag = delimiters[0];
      ctag = delimiters[delimiters.length - 1];

      return closeIndex + close.length - 1;
    }

    if (delimiters) {
      delimiters = delimiters.split(' ');
      otag = delimiters[0];
      ctag = delimiters[1];
    }

    for (i = 0; i < len; i++) {
      if (state == IN_TEXT) {
        if (tagChange(otag, text, i)) {
          --i;
          addBuf();
          state = IN_TAG_TYPE;
        } else {
          if (text.charAt(i) == '\n') {
            filterLine(seenTag);
          } else {
            buf += text.charAt(i);
          }
        }
      } else if (state == IN_TAG_TYPE) {
        i += otag.length - 1;
        tag = Hogan.tags[text.charAt(i + 1)];
        tagType = tag ? text.charAt(i + 1) : '_v';
        if (tagType == '=') {
          i = changeDelimiters(text, i);
          state = IN_TEXT;
        } else {
          if (tag) {
            i++;
          }
          state = IN_TAG;
        }
        seenTag = i;
      } else {
        if (tagChange(ctag, text, i)) {
          tokens.push({tag: tagType, n: trim(buf), otag: otag, ctag: ctag,
                       i: (tagType == '/') ? seenTag - otag.length : i + ctag.length});
          buf = '';
          i += ctag.length - 1;
          state = IN_TEXT;
          if (tagType == '{') {
            if (ctag == '}}') {
              i++;
            } else {
              cleanTripleStache(tokens[tokens.length - 1]);
            }
          }
        } else {
          buf += text.charAt(i);
        }
      }
    }

    filterLine(seenTag, true);

    return tokens;
  }

  function cleanTripleStache(token) {
    if (token.n.substr(token.n.length - 1) === '}') {
      token.n = token.n.substring(0, token.n.length - 1);
    }
  }

  function trim(s) {
    if (s.trim) {
      return s.trim();
    }

    return s.replace(/^\s*|\s*$/g, '');
  }

  function tagChange(tag, text, index) {
    if (text.charAt(index) != tag.charAt(0)) {
      return false;
    }

    for (var i = 1, l = tag.length; i < l; i++) {
      if (text.charAt(index + i) != tag.charAt(i)) {
        return false;
      }
    }

    return true;
  }

  // the tags allowed inside super templates
  var allowedInSuper = {'_t': true, '\n': true, '$': true, '/': true};

  function buildTree(tokens, kind, stack, customTags) {
    var instructions = [],
        opener = null,
        tail = null,
        token = null;

    tail = stack[stack.length - 1];

    while (tokens.length > 0) {
      token = tokens.shift();

      if (tail && tail.tag == '<' && !(token.tag in allowedInSuper)) {
        throw new Error('Illegal content in < super tag.');
      }

      if (Hogan.tags[token.tag] <= Hogan.tags['$'] || isOpener(token, customTags)) {
        stack.push(token);
        token.nodes = buildTree(tokens, token.tag, stack, customTags);
      } else if (token.tag == '/') {
        if (stack.length === 0) {
          throw new Error('Closing tag without opener: /' + token.n);
        }
        opener = stack.pop();
        if (token.n != opener.n && !isCloser(token.n, opener.n, customTags)) {
          throw new Error('Nesting error: ' + opener.n + ' vs. ' + token.n);
        }
        opener.end = token.i;
        return instructions;
      } else if (token.tag == '\n') {
        token.last = (tokens.length == 0) || (tokens[0].tag == '\n');
      }

      instructions.push(token);
    }

    if (stack.length > 0) {
      throw new Error('missing closing tag: ' + stack.pop().n);
    }

    return instructions;
  }

  function isOpener(token, tags) {
    for (var i = 0, l = tags.length; i < l; i++) {
      if (tags[i].o == token.n) {
        token.tag = '#';
        return true;
      }
    }
  }

  function isCloser(close, open, tags) {
    for (var i = 0, l = tags.length; i < l; i++) {
      if (tags[i].c == close && tags[i].o == open) {
        return true;
      }
    }
  }

  function stringifySubstitutions(obj) {
    var items = [];
    for (var key in obj) {
      items.push('"' + esc(key) + '": function(c,p,t,i) {' + obj[key] + '}');
    }
    return "{ " + items.join(",") + " }";
  }

  function stringifyPartials(codeObj) {
    var partials = [];
    for (var key in codeObj.partials) {
      partials.push('"' + esc(key) + '":{name:"' + esc(codeObj.partials[key].name) + '", ' + stringifyPartials(codeObj.partials[key]) + "}");
    }
    return "partials: {" + partials.join(",") + "}, subs: " + stringifySubstitutions(codeObj.subs);
  }

  Hogan.stringify = function(codeObj, text, options) {
    return "{code: function (c,p,i) { " + Hogan.wrapMain(codeObj.code) + " }," + stringifyPartials(codeObj) +  "}";
  }

  var serialNo = 0;
  Hogan.generate = function(tree, text, options) {
    serialNo = 0;
    var context = { code: '', subs: {}, partials: {} };
    Hogan.walk(tree, context);

    if (options.asString) {
      return this.stringify(context, text, options);
    }

    return this.makeTemplate(context, text, options);
  }

  Hogan.wrapMain = function(code) {
    return 'var t=this;t.b(i=i||"");' + code + 'return t.fl();';
  }

  Hogan.template = Hogan.Template;

  Hogan.makeTemplate = function(codeObj, text, options) {
    var template = this.makePartials(codeObj);
    template.code = new Function('c', 'p', 'i', this.wrapMain(codeObj.code));
    return new this.template(template, text, this, options);
  }

  Hogan.makePartials = function(codeObj) {
    var key, template = {subs: {}, partials: codeObj.partials, name: codeObj.name};
    for (key in template.partials) {
      template.partials[key] = this.makePartials(template.partials[key]);
    }
    for (key in codeObj.subs) {
      template.subs[key] = new Function('c', 'p', 't', 'i', codeObj.subs[key]);
    }
    return template;
  }

  function esc(s) {
    return s.replace(rSlash, '\\\\')
            .replace(rQuot, '\\\"')
            .replace(rNewline, '\\n')
            .replace(rCr, '\\r');
  }

  function chooseMethod(s) {
    return (~s.indexOf('.')) ? 'd' : 'f';
  }

  function createPartial(node, context) {
    var prefix = "<" + (context.prefix || "");
    var sym = prefix + node.n + serialNo++;
    context.partials[sym] = {name: node.n, partials: {}};
    context.code += 't.b(t.rp("' +  esc(sym) + '",c,p,"' + (node.indent || '') + '"));';
    return sym;
  }

  Hogan.codegen = {
    '#': function(node, context) {
      context.code += 'if(t.s(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,1),' +
                      'c,p,0,' + node.i + ',' + node.end + ',"' + node.otag + " " + node.ctag + '")){' +
                      't.rs(c,p,' + 'function(c,p,t){';
      Hogan.walk(node.nodes, context);
      context.code += '});c.pop();}';
    },

    '^': function(node, context) {
      context.code += 'if(!t.s(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,1),c,p,1,0,0,"")){';
      Hogan.walk(node.nodes, context);
      context.code += '};';
    },

    '>': createPartial,
    '<': function(node, context) {
      var ctx = {partials: {}, code: '', subs: {}, inPartial: true};
      Hogan.walk(node.nodes, ctx);
      var template = context.partials[createPartial(node, context)];
      template.subs = ctx.subs;
      template.partials = ctx.partials;
    },

    '$': function(node, context) {
      var ctx = {subs: {}, code: '', partials: context.partials, prefix: node.n};
      Hogan.walk(node.nodes, ctx);
      context.subs[node.n] = ctx.code;
      if (!context.inPartial) {
        context.code += 't.sub("' + esc(node.n) + '",c,p,i);';
      }
    },

    '\n': function(node, context) {
      context.code += write('"\\n"' + (node.last ? '' : ' + i'));
    },

    '_v': function(node, context) {
      context.code += 't.b(t.v(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,0)));';
    },

    '_t': function(node, context) {
      context.code += write('"' + esc(node.text) + '"');
    },

    '{': tripleStache,

    '&': tripleStache
  }

  function tripleStache(node, context) {
    context.code += 't.b(t.t(t.' + chooseMethod(node.n) + '("' + esc(node.n) + '",c,p,0)));';
  }

  function write(s) {
    return 't.b(' + s + ');';
  }

  Hogan.walk = function(nodelist, context) {
    var func;
    for (var i = 0, l = nodelist.length; i < l; i++) {
      func = Hogan.codegen[nodelist[i].tag];
      func && func(nodelist[i], context);
    }
    return context;
  }

  Hogan.parse = function(tokens, text, options) {
    options = options || {};
    return buildTree(tokens, '', [], options.sectionTags || []);
  }

  Hogan.cache = {};

  Hogan.cacheKey = function(text, options) {
    return [text, !!options.asString, !!options.disableLambda, options.delimiters, !!options.modelGet].join('||');
  }

  Hogan.compile = function(text, options) {
    options = options || {};
    var key = Hogan.cacheKey(text, options);
    var template = this.cache[key];

    if (template) {
      return template;
    }

    template = this.generate(this.parse(this.scan(text, options.delimiters), text, options), text, options);
    return this.cache[key] = template;
  }
})(typeof exports !== 'undefined' ? exports : Hogan);


if (typeof define === 'function' && define.amd) {
  define('hogan',Hogan);
}
;
/**
 * @license RequireJS text 2.0.6 Copyright (c) 2010-2012, The Dojo Foundation All Rights Reserved.
 * Available via the MIT or new BSD license.
 * see: http://github.com/requirejs/text for details
 */
/*jslint regexp: true */
/*global require, XMLHttpRequest, ActiveXObject,
  define, window, process, Packages,
  java, location, Components, FileUtils */

define('text',['module'], function (module) {
    

    var text, fs, Cc, Ci,
        progIds = ['Msxml2.XMLHTTP', 'Microsoft.XMLHTTP', 'Msxml2.XMLHTTP.4.0'],
        xmlRegExp = /^\s*<\?xml(\s)+version=[\'\"](\d)*.(\d)*[\'\"](\s)*\?>/im,
        bodyRegExp = /<body[^>]*>\s*([\s\S]+)\s*<\/body>/im,
        hasLocation = typeof location !== 'undefined' && location.href,
        defaultProtocol = hasLocation && location.protocol && location.protocol.replace(/\:/, ''),
        defaultHostName = hasLocation && location.hostname,
        defaultPort = hasLocation && (location.port || undefined),
        buildMap = [],
        masterConfig = (module.config && module.config()) || {};

    text = {
        version: '2.0.6',

        strip: function (content) {
            //Strips <?xml ...?> declarations so that external SVG and XML
            //documents can be added to a document without worry. Also, if the string
            //is an HTML document, only the part inside the body tag is returned.
            if (content) {
                content = content.replace(xmlRegExp, "");
                var matches = content.match(bodyRegExp);
                if (matches) {
                    content = matches[1];
                }
            } else {
                content = "";
            }
            return content;
        },

        jsEscape: function (content) {
            return content.replace(/(['\\])/g, '\\$1')
                .replace(/[\f]/g, "\\f")
                .replace(/[\b]/g, "\\b")
                .replace(/[\n]/g, "\\n")
                .replace(/[\t]/g, "\\t")
                .replace(/[\r]/g, "\\r")
                .replace(/[\u2028]/g, "\\u2028")
                .replace(/[\u2029]/g, "\\u2029");
        },

        createXhr: masterConfig.createXhr || function () {
            //Would love to dump the ActiveX crap in here. Need IE 6 to die first.
            var xhr, i, progId;
            if (typeof XMLHttpRequest !== "undefined") {
                return new XMLHttpRequest();
            } else if (typeof ActiveXObject !== "undefined") {
                for (i = 0; i < 3; i += 1) {
                    progId = progIds[i];
                    try {
                        xhr = new ActiveXObject(progId);
                    } catch (e) {}

                    if (xhr) {
                        progIds = [progId];  // so faster next time
                        break;
                    }
                }
            }

            return xhr;
        },

        /**
         * Parses a resource name into its component parts. Resource names
         * look like: module/name.ext!strip, where the !strip part is
         * optional.
         * @param {String} name the resource name
         * @returns {Object} with properties "moduleName", "ext" and "strip"
         * where strip is a boolean.
         */
        parseName: function (name) {
            var modName, ext, temp,
                strip = false,
                index = name.indexOf("."),
                isRelative = name.indexOf('./') === 0 ||
                             name.indexOf('../') === 0;

            if (index !== -1 && (!isRelative || index > 1)) {
                modName = name.substring(0, index);
                ext = name.substring(index + 1, name.length);
            } else {
                modName = name;
            }

            temp = ext || modName;
            index = temp.indexOf("!");
            if (index !== -1) {
                //Pull off the strip arg.
                strip = temp.substring(index + 1) === "strip";
                temp = temp.substring(0, index);
                if (ext) {
                    ext = temp;
                } else {
                    modName = temp;
                }
            }

            return {
                moduleName: modName,
                ext: ext,
                strip: strip
            };
        },

        xdRegExp: /^((\w+)\:)?\/\/([^\/\\]+)/,

        /**
         * Is an URL on another domain. Only works for browser use, returns
         * false in non-browser environments. Only used to know if an
         * optimized .js version of a text resource should be loaded
         * instead.
         * @param {String} url
         * @returns Boolean
         */
        useXhr: function (url, protocol, hostname, port) {
            var uProtocol, uHostName, uPort,
                match = text.xdRegExp.exec(url);
            if (!match) {
                return true;
            }
            uProtocol = match[2];
            uHostName = match[3];

            uHostName = uHostName.split(':');
            uPort = uHostName[1];
            uHostName = uHostName[0];

            return (!uProtocol || uProtocol === protocol) &&
                   (!uHostName || uHostName.toLowerCase() === hostname.toLowerCase()) &&
                   ((!uPort && !uHostName) || uPort === port);
        },

        finishLoad: function (name, strip, content, onLoad) {
            content = strip ? text.strip(content) : content;
            if (masterConfig.isBuild) {
                buildMap[name] = content;
            }
            onLoad(content);
        },

        load: function (name, req, onLoad, config) {
            //Name has format: some.module.filext!strip
            //The strip part is optional.
            //if strip is present, then that means only get the string contents
            //inside a body tag in an HTML string. For XML/SVG content it means
            //removing the <?xml ...?> declarations so the content can be inserted
            //into the current doc without problems.

            // Do not bother with the work if a build and text will
            // not be inlined.
            if (config.isBuild && !config.inlineText) {
                onLoad();
                return;
            }

            masterConfig.isBuild = config.isBuild;

            var parsed = text.parseName(name),
                nonStripName = parsed.moduleName +
                    (parsed.ext ? '.' + parsed.ext : ''),
                url = req.toUrl(nonStripName),
                useXhr = (masterConfig.useXhr) ||
                         text.useXhr;

            //Load the text. Use XHR if possible and in a browser.
            if (!hasLocation || useXhr(url, defaultProtocol, defaultHostName, defaultPort)) {
                text.get(url, function (content) {
                    text.finishLoad(name, parsed.strip, content, onLoad);
                }, function (err) {
                    if (onLoad.error) {
                        onLoad.error(err);
                    }
                });
            } else {
                //Need to fetch the resource across domains. Assume
                //the resource has been optimized into a JS module. Fetch
                //by the module name + extension, but do not include the
                //!strip part to avoid file system issues.
                req([nonStripName], function (content) {
                    text.finishLoad(parsed.moduleName + '.' + parsed.ext,
                                    parsed.strip, content, onLoad);
                });
            }
        },

        write: function (pluginName, moduleName, write, config) {
            if (buildMap.hasOwnProperty(moduleName)) {
                var content = text.jsEscape(buildMap[moduleName]);
                write.asModule(pluginName + "!" + moduleName,
                               "define(function () { return '" +
                                   content +
                               "';});\n");
            }
        },

        writeFile: function (pluginName, moduleName, req, write, config) {
            var parsed = text.parseName(moduleName),
                extPart = parsed.ext ? '.' + parsed.ext : '',
                nonStripName = parsed.moduleName + extPart,
                //Use a '.js' file name so that it indicates it is a
                //script that can be loaded across domains.
                fileName = req.toUrl(parsed.moduleName + extPart) + '.js';

            //Leverage own load() method to load plugin value, but only
            //write out values that do not have the strip argument,
            //to avoid any potential issues with ! in file names.
            text.load(nonStripName, req, function (value) {
                //Use own write() method to construct full module value.
                //But need to create shell that translates writeFile's
                //write() to the right interface.
                var textWrite = function (contents) {
                    return write(fileName, contents);
                };
                textWrite.asModule = function (moduleName, contents) {
                    return write.asModule(moduleName, fileName, contents);
                };

                text.write(pluginName, nonStripName, textWrite, config);
            }, config);
        }
    };

    if (masterConfig.env === 'node' || (!masterConfig.env &&
            typeof process !== "undefined" &&
            process.versions &&
            !!process.versions.node)) {
        //Using special require.nodeRequire, something added by r.js.
        fs = require.nodeRequire('fs');

        text.get = function (url, callback) {
            var file = fs.readFileSync(url, 'utf8');
            //Remove BOM (Byte Mark Order) from utf8 files if it is there.
            if (file.indexOf('\uFEFF') === 0) {
                file = file.substring(1);
            }
            callback(file);
        };
    } else if (masterConfig.env === 'xhr' || (!masterConfig.env &&
            text.createXhr())) {
        text.get = function (url, callback, errback, headers) {
            var xhr = text.createXhr(), header;
            xhr.open('GET', url, true);

            //Allow plugins direct access to xhr headers
            if (headers) {
                for (header in headers) {
                    if (headers.hasOwnProperty(header)) {
                        xhr.setRequestHeader(header.toLowerCase(), headers[header]);
                    }
                }
            }

            //Allow overrides specified in config
            if (masterConfig.onXhr) {
                masterConfig.onXhr(xhr, url);
            }

            xhr.onreadystatechange = function (evt) {
                var status, err;
                //Do not explicitly handle errors, those should be
                //visible via console output in the browser.
                if (xhr.readyState === 4) {
                    status = xhr.status;
                    if (status > 399 && status < 600) {
                        //An http 4xx or 5xx error. Signal an error.
                        err = new Error(url + ' HTTP status: ' + status);
                        err.xhr = xhr;
                        errback(err);
                    } else {
                        callback(xhr.responseText);
                    }

                    if (masterConfig.onXhrComplete) {
                        masterConfig.onXhrComplete(xhr, url);
                    }
                }
            };
            xhr.send(null);
        };
    } else if (masterConfig.env === 'rhino' || (!masterConfig.env &&
            typeof Packages !== 'undefined' && typeof java !== 'undefined')) {
        //Why Java, why is this so awkward?
        text.get = function (url, callback) {
            var stringBuffer, line,
                encoding = "utf-8",
                file = new java.io.File(url),
                lineSeparator = java.lang.System.getProperty("line.separator"),
                input = new java.io.BufferedReader(new java.io.InputStreamReader(new java.io.FileInputStream(file), encoding)),
                content = '';
            try {
                stringBuffer = new java.lang.StringBuffer();
                line = input.readLine();

                // Byte Order Mark (BOM) - The Unicode Standard, version 3.0, page 324
                // http://www.unicode.org/faq/utf_bom.html

                // Note that when we use utf-8, the BOM should appear as "EF BB BF", but it doesn't due to this bug in the JDK:
                // http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4508058
                if (line && line.length() && line.charAt(0) === 0xfeff) {
                    // Eat the BOM, since we've already found the encoding on this file,
                    // and we plan to concatenating this buffer with others; the BOM should
                    // only appear at the top of a file.
                    line = line.substring(1);
                }

                stringBuffer.append(line);

                while ((line = input.readLine()) !== null) {
                    stringBuffer.append(lineSeparator);
                    stringBuffer.append(line);
                }
                //Make sure we return a JavaScript string and not a Java string.
                content = String(stringBuffer.toString()); //String
            } finally {
                input.close();
            }
            callback(content);
        };
    } else if (masterConfig.env === 'xpconnect' || (!masterConfig.env &&
            typeof Components !== 'undefined' && Components.classes &&
            Components.interfaces)) {
        //Avert your gaze!
        Cc = Components.classes,
        Ci = Components.interfaces;
        Components.utils['import']('resource://gre/modules/FileUtils.jsm');

        text.get = function (url, callback) {
            var inStream, convertStream,
                readData = {},
                fileObj = new FileUtils.File(url);

            //XPCOM, you so crazy
            try {
                inStream = Cc['@mozilla.org/network/file-input-stream;1']
                           .createInstance(Ci.nsIFileInputStream);
                inStream.init(fileObj, 1, 0, false);

                convertStream = Cc['@mozilla.org/intl/converter-input-stream;1']
                                .createInstance(Ci.nsIConverterInputStream);
                convertStream.init(inStream, "utf-8", inStream.available(),
                Ci.nsIConverterInputStream.DEFAULT_REPLACEMENT_CHARACTER);

                convertStream.readString(inStream.available(), readData);
                convertStream.close();
                inStream.close();
                callback(readData.value);
            } catch (e) {
                throw new Error((fileObj && fileObj.path || '') + ': ' + e);
            }
        };
    }
    return text;
});

/**@license
 * RequireJS Hogan Plugin | v0.2.1 (2013/02/08)
 * Author: Miller Medeiros | MIT License
 */
define('hgn',['hogan', 'text'], function (hogan, text) {

    var DEFAULT_EXTENSION = '.mustache';

    var _buildMap = {};
    var _buildTemplateText = 'define("{{pluginName}}!{{moduleName}}", ["hogan"], function(hogan){'+
                             '  var tmpl = new hogan.Template({{{fn}}}, "", hogan);'+
                             // need to use apply to bind the proper scope.
                             '  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;'+
                             '});\n';
    var _buildTemplate;


    function load(name, req, onLoad, config){
        var hgnConfig = config.hgn || {};
        var fileName = name;
        fileName += hgnConfig && hgnConfig.templateExtension != null? hgnConfig.templateExtension : DEFAULT_EXTENSION;

        // load text files with text plugin
        text.get(req.toUrl(fileName), function(data){
            var compilationOptions = hgnConfig.compilationOptions? mixIn({}, hgnConfig.compilationOptions) : {};

            if (config.isBuild) {
                // store compiled function if build
                // and should always be a string
                compilationOptions.asString = true;
                _buildMap[name] = hogan.compile(data, compilationOptions);
            }

            // maybe it's required by some other plugin during build
            // so return the compiled template even during build
            var template = hogan.compile(data, compilationOptions);
            var render = bind(template.render, template);
            // add text property for debugging if needed.
            // it's important to notice that this value won't be available
            // after build.
            render.text = template.text;
            render.template = template;
            // return just the render method so it's easier to use
            onLoad( render );
        });
    }

    function bind(fn, context) {
        return function(){
            return fn.apply(context, arguments);
        };
    }

    function mixIn(target, source) {
        var key;
        for (key in source){
            if ( Object.prototype.hasOwnProperty.call(source, key) ) {
                target[key] = source[key];
            }
        }
        return target;
    }

    function write(pluginName, moduleName, writeModule){
        if(moduleName in _buildMap){
            if (! _buildTemplate) {
                // using templates to generate compiled templates, so meta :P
                _buildTemplate = hogan.compile( _buildTemplateText );
            }
            var fn = _buildMap[moduleName];
            writeModule( _buildTemplate.render({
                pluginName : pluginName,
                moduleName : moduleName,
                fn : fn
            }) );
        }
    }

    return {
        load : load,
        write : write
    };

});
define("hgn!ColorEditorTemplate.html", ["hogan"], function(hogan){  var tmpl = new hogan.Template({code: function (c,p,i) { var t=this;t.b(i=i||"");t.b("\n" + i);t.b("<div tabindex=\"-1\" class=\"color-editor\">");t.b("\n" + i);t.b("  <section>");t.b("\n" + i);t.b("    <div class=\"sliders\">");t.b("\n" + i);t.b("      <div class=\"color-selection-field\">");t.b("\n" + i);t.b("        <div class=\"saturation-gradient gradient-overlay\"></div>");t.b("\n" + i);t.b("        <div class=\"luminosity-gradient gradient-overlay\"></div>");t.b("\n" + i);t.b("        <div tabindex=\"0\" class=\"selector-base\">");t.b("\n" + i);t.b("          <div class=\"selector\"></div>");t.b("\n" + i);t.b("        </div>");t.b("\n" + i);t.b("      </div>");t.b("\n" + i);t.b("      <div class=\"hue-slider slider\">");t.b("\n" + i);t.b("        <div tabindex=\"0\" class=\"selector-base\">");t.b("\n" + i);t.b("          <div class=\"selector\"></div>");t.b("\n" + i);t.b("        </div>");t.b("\n" + i);t.b("      </div>");t.b("\n" + i);t.b("      <div class=\"opacity-slider slider\">");t.b("\n" + i);t.b("        <div class=\"opacity-gradient gradient-overlay\"></div>");t.b("\n" + i);t.b("        <div tabindex=\"0\" class=\"selector-base\">");t.b("\n" + i);t.b("          <div class=\"selector\"></div>");t.b("\n" + i);t.b("        </div>");t.b("\n" + i);t.b("      </div>");t.b("\n" + i);t.b("    </div>");t.b("\n" + i);t.b("    <footer>");t.b("\n" + i);t.b("      <input class=\"color-value\" />");t.b("\n" + i);t.b("      <ul class=\"button-bar\">");t.b("\n" + i);t.b("        <li class=\"selected\" title=\"");t.b(t.v(t.f("COLOR_EDITOR_RGBA_BUTTON_TIP",c,p,0)));t.b("\"><a href=\"#\" tabindex=\"0\" class=\"rgba\">RGBa</a></li>");t.b("\n" + i);t.b("        <li title=\"");t.b(t.v(t.f("COLOR_EDITOR_HEX_BUTTON_TIP",c,p,0)));t.b("\"><a href=\"#\" tabindex=\"0\" class=\"hex\">HEX</a></li>");t.b("\n" + i);t.b("        <li title=\"");t.b(t.v(t.f("COLOR_EDITOR_HSLA_BUTTON_TIP",c,p,0)));t.b("\"><a href=\"#\" tabindex=\"0\" class=\"hsla\">HSLa</a></li>");t.b("\n" + i);t.b("      </ul>");t.b("\n" + i);t.b("    </footer>");t.b("\n" + i);t.b("  </section>");t.b("\n" + i);t.b("  <aside>");t.b("\n" + i);t.b("    <header>");t.b("\n" + i);t.b("      <div class=\"large-swatches\">");t.b("\n" + i);t.b("        <div class=\"current-color large-swatch\" title=\"");t.b(t.v(t.f("COLOR_EDITOR_CURRENT_COLOR_SWATCH_TIP",c,p,0)));t.b("\"></div>");t.b("\n" + i);t.b("        <div class=\"original-color large-swatch\" title=\"");t.b(t.v(t.f("COLOR_EDITOR_ORIGINAL_COLOR_SWATCH_TIP",c,p,0)));t.b("\"></div>");t.b("\n" + i);t.b("      </div>");t.b("\n" + i);t.b("    </header>");t.b("\n" + i);t.b("    <ul class=\"swatches\"></ul>");t.b("\n" + i);t.b("  </aside>");t.b("\n" + i);t.b("</div>");return t.fl(); },partials: {}, subs: {  }}, "", hogan);  function render(){ return tmpl.render.apply(tmpl, arguments); } render.template = tmpl; return render;});

/*
 * Copyright (c) 2012 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, nomen: true, regexp: true, maxerr: 50 */
/*global define, brackets, $, window, tinycolor, Mustache */

define('ColorEditor',['require','exports','module','thirdparty/tinycolor-min','hgn!ColorEditorTemplate.html'],function (require, exports, module) {
    
    require("thirdparty/tinycolor-min");
    
    var KeyEvent    = brackets.getModule("utils/KeyEvent"),
        StringUtils = brackets.getModule("utils/StringUtils"),
        Strings     = brackets.getModule("strings");
    
    /** Mustache template that forms the bare DOM structure of the UI */
    var ColorEditorTemplate = require("hgn!ColorEditorTemplate.html");
    
    /** @const @type {number} */
    var STEP_MULTIPLIER = 5;
    
    /**
     * Color picker control; may be used standalone or within an InlineColorEditor inline widget.
     * @param {!jQuery} $parent  DOM node into which to append the root of the color picker UI
     * @param {!string} color  Initially selected color
     * @param {!function(string)} callback  Called whenever selected color changes
     * @param {!Array.<{value:string, count:number}>} swatches  Quick-access color swatches to include in UI
     */
    function ColorEditor($parent, color, callback, swatches) {
        // Create the DOM structure, filling in localized strings via Mustache
        this.$element = $(ColorEditorTemplate(Strings));
        $parent.append(this.$element);
        
        this._callback = callback;

        this._handleKeydown = this._handleKeydown.bind(this);
        this._handleOpacityKeydown = this._handleOpacityKeydown.bind(this);
        this._handleHslKeydown = this._handleHslKeydown.bind(this);
        this._handleHueKeydown = this._handleHueKeydown.bind(this);
        this._handleSelectionKeydown = this._handleSelectionKeydown.bind(this);
        this._handleOpacityDrag = this._handleOpacityDrag.bind(this);
        this._handleHueDrag = this._handleHueDrag.bind(this);
        this._handleSelectionFieldDrag = this._handleSelectionFieldDrag.bind(this);

        this._color = tinycolor(color);
        this._originalColor = color;
        this._redoColor = null;
        
        this.$colorValue = this.$element.find(".color-value");
        this.$buttonList = this.$element.find("ul.button-bar");
        this.$rgbaButton = this.$element.find(".rgba");
        this.$hexButton = this.$element.find(".hex");
        this.$hslButton = this.$element.find(".hsla");
        this.$currentColor = this.$element.find(".current-color");
        this.$originalColor = this.$element.find(".original-color");
        this.$selection = this.$element.find(".color-selection-field");
        this.$selectionBase = this.$element.find(".color-selection-field .selector-base");
        this.$hueBase = this.$element.find(".hue-slider .selector-base");
        this.$opacityGradient = this.$element.find(".opacity-gradient");
        this.$hueSlider = this.$element.find(".hue-slider");
        this.$hueSelector = this.$element.find(".hue-slider .selector-base");
        this.$opacitySlider = this.$element.find(".opacity-slider");
        this.$opacitySelector = this.$element.find(".opacity-slider .selector-base");
        this.$swatches = this.$element.find(".swatches");
        
        // Create quick-access color swatches
        this._addSwatches(swatches);
        
        // Attach event listeners to main UI elements
        this._addListeners();
        
        // Initially selected color
        this.$originalColor.css("background-color", this._originalColor);
        this._commitColor(color);
    }

    /**
     * A string or tinycolor object representing the currently selected color
     * TODO (#2201): type is unpredictable
     * @type {tinycolor|string}
     */
    ColorEditor.prototype._color = null;
    
    /**
     * An HSV representation of the currently selected color.
     * TODO (#2201): type of _hsv.s/.v is unpredictable
     * @type {!{h:number, s:number|string, v:number|string, a:number}}
     */
    ColorEditor.prototype._hsv = tinycolor("rgba(0,0,0,1)").toHsv();
    
    /**
     * Color that was selected before undo(), if undo was the last change made. Else null.
     * @type {?string}
     */
    ColorEditor.prototype._redoColor = null;
    
    /**
     * Initial value the color picker was opened with
     * @type {!string}
     */
    ColorEditor.prototype._originalColor = null;
    
    
    /** Returns the root DOM node of the ColorPicker UI */
    ColorEditor.prototype.getRootElement = function () {
        return this.$element;
    };
        
    /** Attach event listeners for main UI elements */
    ColorEditor.prototype._addListeners = function () {
        this._bindColorFormatToRadioButton("rgba");
        this._bindColorFormatToRadioButton("hex");
        this._bindColorFormatToRadioButton("hsla");
        
        this._bindInputHandlers();
        
        this._bindOriginalColorButton();
        
        this._registerDragHandler(this.$selection, this._handleSelectionFieldDrag);
        this._registerDragHandler(this.$hueSlider, this._handleHueDrag);
        this._registerDragHandler(this.$opacitySlider, this._handleOpacityDrag);
        this._bindKeyHandler(this.$selectionBase, this._handleSelectionKeydown);
        this._bindKeyHandler(this.$hueBase, this._handleHueKeydown);
        this._bindKeyHandler(this.$opacitySelector, this._handleOpacityKeydown);
        this._bindKeyHandler(this.$hslButton, this._handleHslKeydown);
        
        // General key handler gets bubbling events from any focusable part of widget
        this._bindKeyHandler(this.$element, this._handleKeydown);
    };

    /**
     * Update all UI elements to reflect the selected color (_color and _hsv). It is usually
     * incorrect to call this directly; use _commitColor() or setColorAsHsv() instead.
     */
    ColorEditor.prototype._synchronize = function () {
        var colorValue = this.getColor().toString(),
            colorObject = tinycolor(colorValue),
            hueColor = "hsl(" + this._hsv.h + ", 100%, 50%)";
        
        this._updateColorTypeRadioButtons(colorObject.format);
        this.$colorValue.val(colorValue);
        this.$currentColor.css("background-color", colorValue);
        this.$selection.css("background-color", hueColor);
        this.$hueBase.css("background-color", hueColor);
        
        // Update gradients in color square & opacity slider
        this.$selectionBase.css("background-color", colorObject.toHexString());
        this.$opacityGradient.css("background-image", "-webkit-gradient(linear, 0% 0%, 0% 100%, from(" + hueColor + "), to(transparent))");
        
        // Update slider thumb positions
        this.$hueSelector.css("bottom", (this._hsv.h / 360 * 100) + "%");
        this.$opacitySelector.css("bottom", (this._hsv.a * 100) + "%");
        if (!isNaN(this._hsv.s)) {      // TODO (#2201): type of _hsv.s/.v is unpredictable
            this._hsv.s = (this._hsv.s * 100) + "%";
        }
        if (!isNaN(this._hsv.v)) {
            this._hsv.v = (this._hsv.v * 100) + "%";
        }
        this.$selectionBase.css({
            left: this._hsv.s,
            bottom: this._hsv.v
        });
    };

    /** Focus the main color square's thumb */
    ColorEditor.prototype.focus = function () {
        if (!this.$selectionBase.is(":focus")) {
            this.$selectionBase.focus();
            return true;
        }
        return false;
    };

    /** @return {tinycolor|string} The currently selected color (TODO (#2201): type is unpredictable) */
    ColorEditor.prototype.getColor = function () {
        return this._color;
    };

    /** Update the format button bar's selection */
    ColorEditor.prototype._updateColorTypeRadioButtons = function (format) {
        this.$buttonList.find("li").removeClass("selected");
        switch (format) {
        case "rgb":
            this.$buttonList.find(".rgba").parent().addClass("selected");
            break;
        case "hex":
        case "name":
            this.$buttonList.find(".hex").parent().addClass("selected");
            break;
        case "hsl":
            this.$buttonList.find(".hsla").parent().addClass("selected");
            break;
        }
    };

    /** Add event listeners to the format button bar */
    ColorEditor.prototype._bindColorFormatToRadioButton = function (buttonClass, propertyName, value) {
        var handler,
            _this = this;
        handler = function (event) {
            var colorObject, newColor, newFormat;
            newFormat = $(event.currentTarget).html().toLowerCase().replace("%", "p");
            newColor = _this.getColor();
            colorObject = tinycolor(newColor);
            switch (newFormat) {
            case "hsla":
                newColor = colorObject.toHslString();
                break;
            case "rgba":
                newColor = colorObject.toRgbString();
                break;
            case "prgba":
                newColor = colorObject.toPercentageRgbString();
                break;
            case "hex":
                newColor = colorObject.toHexString();
                _this._hsv.a = 1;
                break;
            }
            _this._commitColor(newColor, false);
        };
        this.$element.find("." + buttonClass).click(handler);
    };

    /** Add event listener to the "original color value" swatch */
    ColorEditor.prototype._bindOriginalColorButton = function () {
        var _this = this;
        this.$originalColor.click(function (event) {
            _this._commitColor(_this._originalColor, true);
        });
    };

    /**
     * Convert percentage values in an RGB color into normal RGB values in the range of 0 - 255.
     * If the original color is already in non-percentage format, does nothing.
     * @param {string} color The color to be converted to non-percentage RGB color string.
     * @return {string} an RGB color string in the normal format using non-percentage values
     */
    ColorEditor.prototype._convertToNormalRGB = function (color) {
        var matches = color.match(/^rgb.*?([0-9]+)\%.*?([0-9]+)\%.*?([0-9]+)\%/);
        if (matches) {
            var i, percentStr, value;
            for (i = 0; i < 3; i++) {
                percentStr = matches[i + 1];
                value = Math.round(255 * Number(percentStr) / 100);
                if (!isNaN(value)) {
                    color = color.replace(percentStr + "%", value);
                }
            }
        }
        return color;
    };
    
    /**
     * Normalize the given color string into the format used by tinycolor, by adding a space 
     * after commas.
     * @param {string} color The color to be corrected if it looks like an RGB or HSL color.
     * @return {string} a normalized color string.
     */
    ColorEditor.prototype._normalizeColorString = function (color) {
        var normalizedColor = color;
                    
        // Convert 6-digit hex to 3-digit hex as tinycolor (#ffaacc -> #fac)
        if (color.match(/^#[0-9a-fA-F]{6}/)) {
            return tinycolor(color).toString();
        }
        if (color.match(/^(rgb|hsl)/)) {
            normalizedColor = normalizedColor.replace(/,\s*/g, ", ");
            normalizedColor = normalizedColor.replace(/\(\s+/, "(");
            normalizedColor = normalizedColor.replace(/\s+\)/, ")");
        }
        return normalizedColor.toLowerCase();
    };

    /** Handle changes in text field */
    ColorEditor.prototype._handleTextFieldInput = function (losingFocus) {
        var newColor = $.trim(this.$colorValue.val()),
            newColorObj = tinycolor(newColor),
            newColorOk = newColorObj.ok;

        // tinycolor will auto correct an incomplete rgb or hsl value into a valid color value.
        // eg. rgb(0,0,0 -> rgb(0, 0, 0) 
        // We want to avoid having tinycolor do this, because we don't want to sync the color
        // to the UI if it's incomplete. To accomplish this, we first normalize the original
        // color string into the format tinycolor would generate, and then compare it to what
        // tinycolor actually generates to see if it's different. If so, then we assume the color
        // was incomplete to begin with.
        if (newColorOk) {
            newColorOk = (newColorObj.toString() === this._normalizeColorString(newColor));
        }
                
        // Restore to the previous valid color if the new color is invalid or incomplete.
        if (losingFocus && !newColorOk) {
            newColor = this.getColor().toString();
        }
        
        // Sync only if we have a valid color or we're restoring the previous valid color.
        if (losingFocus || newColorOk) {
            this._commitColor(newColor, true);
        }
    };
                    
    ColorEditor.prototype._bindInputHandlers = function () {
        var _this = this;
                    
        this.$colorValue.bind("input", function (event) {
            _this._handleTextFieldInput(false);
        });

        this.$colorValue.bind("change", function (event) {
            _this._handleTextFieldInput(true);
        });
    };

    /**
     * Populate the UI with the given color swatches and add listeners so they're selectable.
     * @param {!Array.<{value:string, count:number}>} swatches
     */
    ColorEditor.prototype._addSwatches = function (swatches) {
        var _this = this;
 
        // Create swatches
        swatches.forEach(function (swatch) {
            var stringFormat = (swatch.count > 1) ? Strings.COLOR_EDITOR_USED_COLOR_TIP_PLURAL : Strings.COLOR_EDITOR_USED_COLOR_TIP_SINGULAR,
                usedColorTip = StringUtils.format(stringFormat, swatch.value, swatch.count);
            _this.$swatches.append("<li tabindex='0'><div class='swatch-bg'><div class='swatch' style='background-color: " +
                    swatch.value + ";' title='" + usedColorTip + "'></div></div> <span class='value'" + " title='" +
                    usedColorTip + "'>" + swatch.value + "</span></li>");
        });

        // Add key & click listeners to each
        this.$swatches.find("li").keydown(function (event) {
            if (event.keyCode === KeyEvent.DOM_VK_RETURN ||
                    event.keyCode === KeyEvent.DOM_VK_ENTER ||
                    event.keyCode === KeyEvent.DOM_VK_SPACE) {
                // Enter/Space is same as clicking on swatch
                _this._commitColor($(event.currentTarget).find(".value").html());
            } else if (event.keyCode === KeyEvent.DOM_VK_TAB) {
                // Tab on last swatch loops back to color square
                if (!event.shiftKey && $(this).next("li").length === 0) {
                    _this.$selectionBase.focus();
                    return false;
                }
            }
        });

        this.$swatches.find("li").click(function (event) {
            _this._commitColor($(event.currentTarget).find(".value").html());
        });
    };

    /**
     * Sets _hsv and _color based on an HSV input, and updates the UI. Attempts to preserve
     * the previous color format.
     * @param {!{h:number=, s:number=, v:number=}} hsv  Any missing values use the previous color's values.
     */
    ColorEditor.prototype.setColorAsHsv = function (hsv) {
        var colorVal, newColor, oldFormat;
        oldFormat = tinycolor(this.getColor()).format;
        
        // Set our state to the new color
        $.extend(this._hsv, hsv);
        newColor = tinycolor(this._hsv);
        
        switch (oldFormat) {
        case "hsl":
            colorVal = newColor.toHslString();
            break;
        case "rgb":
            colorVal = newColor.toRgbString();
            break;
        case "prgb":
            colorVal = newColor.toPercentageRgbString();
            break;
        case "hex":
        case "name":
            colorVal = this._hsv.a < 1 ? newColor.toRgbString() : newColor.toHexString();
            break;
        }
        this._commitColor(colorVal, false);
    };

    /**
     * Sets _color (and optionally _hsv) based on a string input, and updates the UI. The string's
     * format determines the new selected color's format.
     * @param {!string} colorVal
     * @param {boolean=} resetHsv  Pass false ONLY if hsv set already been modified to match colorVal. Default: true.
     */
    ColorEditor.prototype._commitColor = function (colorVal, resetHsv) {
        var colorObj;
        if (resetHsv === undefined) {
            resetHsv = true;
        }
        this._callback(colorVal);
        this._color = colorVal;
        if (resetHsv) {
            colorObj = tinycolor(colorVal);
            this._hsv = colorObj.toHsv();
            this._color = colorObj;
        }
        this._redoColor = null;  // if we had undone, this new value blows away the redo history
        this._synchronize();
    };

    /**
     * Sets _color and _hsv based on a string input, and updates the UI. The string's
     * format determines the new selected color's format.
     * @param {!string} colorVal
     */
    ColorEditor.prototype.setColorFromString = function (colorVal) {
        this._commitColor(colorVal, true);  // TODO (#2204): make this less entangled with setColorAsHsv()
    };
    
    /** Converts a mouse coordinate to be relative to zeroPos, and clips to [0, maxOffset] */
    function _getNewOffset(pos, zeroPos, maxOffset) {
        var offset = pos - zeroPos;
        offset = Math.min(maxOffset, Math.max(0, offset));
        return offset;
    }
    
    /** Dragging color square's thumb */
    ColorEditor.prototype._handleSelectionFieldDrag = function (event) {
        var height, hsv, width, xOffset, yOffset;
        height = this.$selection.height();
        width = this.$selection.width();
        xOffset = _getNewOffset(event.clientX, this.$selection.offset().left, width);
        yOffset = _getNewOffset(event.clientY, this.$selection.offset().top, height);
        hsv = {};
        hsv.s = xOffset / width;
        hsv.v = 1 - yOffset / height;
        this.setColorAsHsv(hsv, false);
        if (!this.$selection.find(".selector-base").is(":focus")) {
            this.$selection.find(".selector-base").focus();
        }
    };

    /** Dragging hue slider thumb */
    ColorEditor.prototype._handleHueDrag = function (event) {
        var height, hsv, offset;
        height = this.$hueSlider.height();
        offset = _getNewOffset(event.clientY, this.$hueSlider.offset().top, height);
        hsv = {};
        hsv.h = (1 - offset / height) * 360;
        this.setColorAsHsv(hsv, false);
        if (!this.$hueSlider.find(".selector-base").is(":focus")) {
            this.$hueSlider.find(".selector-base").focus();
        }
    };

    /** Dragging opacity slider thumb */
    ColorEditor.prototype._handleOpacityDrag = function (event) {
        var height, hsv, offset;
        height = this.$opacitySlider.height();
        offset = _getNewOffset(event.clientY, this.$opacitySlider.offset().top, height);
        hsv = {};
        hsv.a = 1 - offset / height;
        this.setColorAsHsv(hsv, false);
        if (!this.$opacitySlider.find(".selector-base").is(":focus")) {
            this.$opacitySlider.find(".selector-base").focus();
        }
    };

    /**
     * Helper for attaching drag-related mouse listeners to an element. It's up to
     * 'handler' to actually move the element as mouse is dragged.
     * @param {!function(jQuery.event)} handler  Called whenever drag position changes
     */
    ColorEditor.prototype._registerDragHandler = function ($element, handler) {
        var mouseupHandler = function (event) {
            $(window).unbind("mousemove", handler);
            $(window).unbind("mouseup", mouseupHandler);
        };
        $element.mousedown(function (event) {
            $(window).bind("mousemove", handler);
            $(window).bind("mouseup", mouseupHandler);
        });
        $element.mousedown(handler);  // run drag-update handler on initial mousedown too
    };
    
    /**
     * Handles undo gestures while color picker has focus. We don't want to let CodeMirror's
     * usual undo logic run since it will destroy our bookmarks.
     */
    ColorEditor.prototype.undo = function () {
        if (this._originalColor.toString() !== this._color.toString()) {
            var curColor = this._color.toString();
            this._commitColor(this._originalColor, true);
            this._redoColor = curColor;
        }
    };

    /** Similarly, handle redo gestures while color picker has focus. */
    ColorEditor.prototype.redo = function () {
        if (this._redoColor) {
            this._commitColor(this._redoColor, true);
            this._redoColor = null;
        }
    };

    /** 
     * Global handler for keys in the color editor. Catches undo/redo keys and traps
     * arrow keys that would be handled by the scroller.
     */
    ColorEditor.prototype._handleKeydown = function (event) {
        var hasCtrl = (brackets.platform === "win") ? (event.ctrlKey) : (event.metaKey);
        if (hasCtrl) {
            switch (event.keyCode) {
            case KeyEvent.DOM_VK_Z:
                if (event.shiftKey) {
                    this.redo();
                } else {
                    this.undo();
                }
                return false;
            case KeyEvent.DOM_VK_Y:
                this.redo();
                return false;
            }
        } else {
            if (event.keyCode === KeyEvent.DOM_VK_LEFT ||
                    event.keyCode === KeyEvent.DOM_VK_RIGHT ||
                    event.keyCode === KeyEvent.DOM_VK_UP ||
                    event.keyCode === KeyEvent.DOM_VK_DOWN) {
                // Prevent arrow keys that weren't handled by a child control 
                // from being handled by a parent, either through bubbling or 
                // through default native behavior. There isn't a good general
                // way to tell if the target would handle this event by default,
                // so we look to see if the target is a text input control.
                var preventDefault = false,
                    $target = $(event.target);
                    
                // If the input has no "type" attribute, it defaults to text. So we
                // have to check for both possibilities.
                if ($target.is("input:not([type])") || $target.is("input[type=text]")) {
                    // Text input control. In WebKit, if the cursor gets to the start
                    // or end of a text field and can't move any further, the default 
                    // action doesn't take place in the text field, so the event is handled
                    // by the outer scroller. We have to prevent in that case too.
                    if ($target[0].selectionStart === $target[0].selectionEnd &&
                            ((event.keyCode === KeyEvent.DOM_VK_LEFT && $target[0].selectionStart === 0) ||
                             (event.keyCode === KeyEvent.DOM_VK_RIGHT && $target[0].selectionEnd === $target.val().length))) {
                        preventDefault = true;
                    }
                } else {
                    // Not a text input control, so we want to prevent default.
                    preventDefault = true;
                }

                if (preventDefault) {
                    event.stopPropagation();
                    return false; // equivalent to event.preventDefault()
                }
            }
        }
    };

    ColorEditor.prototype._handleHslKeydown = function (event) {
        if (event.keyCode === KeyEvent.DOM_VK_TAB) {
            // If we're the last focusable element (no color swatches), Tab wraps around to color square
            if (!event.shiftKey) {
                if (this.$swatches.children().length === 0) {
                    this.$selectionBase.focus();
                    return false;
                }
            }
        }
    };

    /** Key events on the color square's thumb */
    ColorEditor.prototype._handleSelectionKeydown = function (event) {
        var hsv = {},
            step = 1.5,
            xOffset,
            yOffset,
            adjustedOffset;

        switch (event.keyCode) {
        case KeyEvent.DOM_VK_LEFT:
        case KeyEvent.DOM_VK_RIGHT:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            xOffset = Number($.trim(this.$selectionBase[0].style.left.replace("%", "")));
            adjustedOffset = (event.keyCode === KeyEvent.DOM_VK_LEFT) ? (xOffset - step) : (xOffset + step);
            xOffset = Math.min(100, Math.max(0, adjustedOffset));
            hsv.s = xOffset / 100;
            this.setColorAsHsv(hsv, false);
            return false;
        case KeyEvent.DOM_VK_DOWN:
        case KeyEvent.DOM_VK_UP:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            yOffset = Number($.trim(this.$selectionBase[0].style.bottom.replace("%", "")));
            adjustedOffset = (event.keyCode === KeyEvent.DOM_VK_DOWN) ? (yOffset - step) : (yOffset + step);
            yOffset = Math.min(100, Math.max(0, adjustedOffset));
            hsv.v = yOffset / 100;
            this.setColorAsHsv(hsv, false);
            return false;
        case KeyEvent.DOM_VK_TAB:
            // Shift+Tab loops back to last focusable element: last swatch if any; format button bar if not
            if (event.shiftKey) {
                if (this.$swatches.children().length === 0) {
                    this.$hslButton.focus();
                } else {
                    this.$swatches.find("li:last").focus();
                }
                return false;
            }
            break;
        }
    };

    /** Key events on the hue slider thumb */
    ColorEditor.prototype._handleHueKeydown = function (event) {
        var hsv = {},
            hue = Number(this._hsv.h),
            step = 3.6;

        switch (event.keyCode) {
        case KeyEvent.DOM_VK_DOWN:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            hsv.h = (hue - step) <= 0 ? 360 - step : hue - step;
            this.setColorAsHsv(hsv, false);
            return false;
        case KeyEvent.DOM_VK_UP:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            hsv.h = (hue + step) >= 360 ? step : hue + step;
            this.setColorAsHsv(hsv, false);
            return false;
        }
    };

    /** Key events on the opacity slider thumb */
    ColorEditor.prototype._handleOpacityKeydown = function (event) {
        var alpha = this._hsv.a,
            hsv = {},
            step = 0.01;

        switch (event.keyCode) {
        case KeyEvent.DOM_VK_DOWN:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            if (alpha > 0) {
                hsv.a = (alpha - step) <= 0 ? 0 : alpha - step;
                this.setColorAsHsv(hsv);
            }
            return false;
        case KeyEvent.DOM_VK_UP:
            step = event.shiftKey ? step * STEP_MULTIPLIER : step;
            if (alpha < 100) {
                hsv.a = (alpha + step) >= 1 ? 1 : alpha + step;
                this.setColorAsHsv(hsv);
            }
            return false;
        }
    };

    ColorEditor.prototype._bindKeyHandler = function ($element, handler) {
        $element.bind("keydown", handler);
    };

    // Prevent clicks on some UI elements (color selection field, slider and large swatch) from taking focus
    $(window.document).on("mousedown", ".color-selection-field, .slider, .large-swatch", function (e) {
        e.preventDefault();
    });

    exports.ColorEditor = ColorEditor;
});

/*
 * Copyright (c) 2012 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, nomen: true, regexp: true, maxerr: 50 */
/*global define, brackets, $, window */

define('InlineColorEditor',['require','exports','module','ColorEditor'],function (require, exports, module) {
    
    
    var InlineWidget         = brackets.getModule("editor/InlineWidget").InlineWidget,
        ColorEditor          = require("ColorEditor").ColorEditor,
        ColorUtils           = brackets.getModule("utils/ColorUtils");
        

    /** @const @type {number} */
    var MAX_USED_COLORS = 7;
    
    /** @type {number} Global var used to provide a unique ID for each color editor instance's _origin field. */
    var lastOriginId = 1;
    
    /**
     * Inline widget containing a ColorEditor control
     * @param {!string} color  Initially selected color
     * @param {!CodeMirror.Bookmark} startBookmark
     * @param {!CodeMirror.Bookmark} endBookmark
     */
    function InlineColorEditor(color, startBookmark, endBookmark) {
        this._color = color;
        this._startBookmark = startBookmark;
        this._endBookmark = endBookmark;
        this._isOwnChange = false;
        this._isHostChange = false;
        this._origin = "*InlineColorEditor_" + (lastOriginId++);

        this._handleColorChange = this._handleColorChange.bind(this);
        this._handleHostDocumentChange = this._handleHostDocumentChange.bind(this);
        
        InlineWidget.call(this);
    }
    
    InlineColorEditor.prototype = Object.create(InlineWidget.prototype);
    InlineColorEditor.prototype.constructor = InlineColorEditor;
    InlineColorEditor.prototype.parentClass = InlineWidget.prototype;
    
    /** @type {!ColorPicker} ColorPicker instance */
    InlineColorEditor.prototype.colorEditor = null;
    
    /** @type {!string} Current value of the color picker control */
    InlineColorEditor.prototype._color = null;
    
    /**
     * Start of the range of code we're attached to; _startBookmark.find() may by null if sync is lost.
     * @type {!CodeMirror.Bookmark}
     */
    InlineColorEditor.prototype._startBookmark = null;
    
    /**
     * End of the range of code we're attached to; _endBookmark.find() may by null if sync is lost or even
     * in some cases when it's not. Call getCurrentRange() for the definitive text range we're attached to.
     * @type {!CodeMirror.Bookmark}
     */
    InlineColorEditor.prototype._endBookmark = null;
    
    /** @type {boolean} True while we're syncing a color picker change into the code editor */
    InlineColorEditor.prototype._isOwnChange = null;
    
    /** @type {boolean} True while we're syncing a code editor change into the color picker */
    InlineColorEditor.prototype._isHostChange = null;
    
    /** @type {number} ID used to identify edits coming from this inline widget for undo batching */
    InlineColorEditor.prototype._origin = null;
    
    
    /**
     * Returns the current text range of the color we're attached to, or null if
     * we've lost sync with what's in the code.
     * @return {?{start:{line:number, ch:number}, end:{line:number, ch:number}}}
     */
    InlineColorEditor.prototype.getCurrentRange = function () {
        var start, end;
        
        start = this._startBookmark.find();
        if (!start) {
            return null;
        }
        
        end = this._endBookmark.find();
        if (!end) {
            end = { line: start.line };
        }
        
        // Even if we think we have a good end bookmark, we want to run the
        // regexp match to see if there's a valid match that extends past the bookmark.
        // This can happen if the user deletes the end of the existing color and then
        // types some more.
        // TODO: when we migrate to CodeMirror v3, we might be able to use markText()
        // instead of two bookmarks to track the range. (In our current old version of
        // CodeMirror v2, markText() isn't robust enough for this case.)
        
        var line = this.hostEditor.document.getLine(start.line),
            matches = line.substr(start.ch).match(ColorUtils.COLOR_REGEX);
        
        // Note that end.ch is exclusive, so we don't need to add 1 before comparing to
        // the matched length here.
        if (matches && (end.ch === undefined || end.ch - start.ch < matches[0].length)) {
            end.ch = start.ch + matches[0].length;
            this._endBookmark.clear();
            this._endBookmark = this.hostEditor._codeMirror.setBookmark(end);
        }
        
        if (end.ch === undefined) {
            // We were unable to resync the end bookmark.
            return null;
        } else {
            return {start: start, end: end};
        }
    };
        
    /**
     * When the color picker's selected color changes, update text in code editor
     * @param {!string} colorString
     */
    InlineColorEditor.prototype._handleColorChange = function (colorString) {
        if (colorString !== this._color) {
            var range = this.getCurrentRange();
            if (!range) {
                return;
            }

            // Don't push the change back into the host editor if it came from the host editor.
            if (!this._isHostChange) {
                // Replace old color in code with the picker's color, and select it
                this._isOwnChange = true;
                this.hostEditor.document.replaceRange(colorString, range.start, range.end, this._origin);
                this._isOwnChange = false;
                this.hostEditor.setSelection(range.start, {
                    line: range.start.line,
                    ch: range.start.ch + colorString.length
                });
            }
            
            this._color = colorString;
        }
    };
    
    /**
     * @override
     * @param {!Editor} hostEditor
     */
    InlineColorEditor.prototype.load = function (hostEditor) {
        InlineColorEditor.prototype.parentClass.load.apply(this, arguments);
        
        // Create color picker control
        var allColorsInDoc = this.hostEditor.document.getText().match(ColorUtils.COLOR_REGEX);
        var swatchInfo = this._collateColors(allColorsInDoc, MAX_USED_COLORS);
        this.colorEditor = new ColorEditor(this.$htmlContent, this._color, this._handleColorChange, swatchInfo);
    };

    /**
     * @override
     * Perform sizing & focus once we've been added to Editor's DOM
     */
    InlineColorEditor.prototype.onAdded = function () {
        InlineColorEditor.prototype.parentClass.onAdded.apply(this, arguments);
        
        var doc = this.hostEditor.document;
        doc.addRef();
        $(doc).on("change", this._handleHostDocumentChange);
        
        this.hostEditor.setInlineWidgetHeight(this, this.colorEditor.getRootElement().outerHeight(), true);
        
        this.colorEditor.focus();
    };
    
    /**
     * @override
     * Called whenever the inline widget is closed, whether automatically or explicitly
     */
    InlineColorEditor.prototype.onClosed = function () {
        InlineColorEditor.prototype.parentClass.onClosed.apply(this, arguments);

        if (this._startBookmark) {
            this._startBookmark.clear();
        }
        if (this._endBookmark) {
            this._endBookmark.clear();
        }

        var doc = this.hostEditor.document;
        $(doc).off("change", this._handleHostDocumentChange);
        doc.releaseRef();
    };

    /** Comparator to sort by which colors are used the most */
    function _colorSort(a, b) {
        if (a.count === b.count) {
            return 0;
        }
        if (a.count > b.count) {
            return -1;
        }
        if (a.count < b.count) {
            return 1;
        }
    }

    /**
     * Counts how many times each color in originalArray occurs (ignoring case) and
     * retuns the top 'maxLength' number of unique colors.
     * @param {!Array.<string>} originalArray
     * @param {number} maxLength
     * @return {!Array.<{value:string, count:number}>}
     */
    InlineColorEditor.prototype._collateColors = function (originalArray, maxLength) {
        // Maps from lowercase color name to swatch info (user-case color name & occurrence count)
        /* @type {Object.<string, {value:string, count:number}>} */
        var colorInfo = {};
        
        // Count how many times each color is used
        originalArray.forEach(function (originalColor) {
            var key = originalColor.toLowerCase();
            if (colorInfo[key]) {
                colorInfo[key].count++;
            } else {
                colorInfo[key] = { value: originalColor, count: 1 };
            }
        });
        
        // Convert to an array
        var uniqueColors = $.map(colorInfo, function (info) {
            return info;
        });
        
        // Sort by most-used and return the top N
        uniqueColors.sort(_colorSort);
        return uniqueColors.slice(0, maxLength);
    };
    
    /**
     * When text in the code editor changes, update color picker to reflect it
     */
    InlineColorEditor.prototype._handleHostDocumentChange = function () {
        // Don't push the change into the color editor if it came from the color editor.
        if (this._isOwnChange) {
            return;
        }
        
        var range = this.getCurrentRange();
        if (range) {
            var newColor = this.hostEditor.document.getRange(range.start, range.end);
            if (newColor !== this._color) {
                this._isHostChange = true;
                this.colorEditor.setColorFromString(newColor);
                this._isHostChange = false;
            }
        } else {
            // The edit caused our range to become invalid. Close the editor.
            this.close();
        }
    };

    module.exports.InlineColorEditor = InlineColorEditor;
});

/*
 * Copyright (c) 2012 Adobe Systems Incorporated. All rights reserved.
 *  
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 *  
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *  
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 * 
 */

/*jslint vars: true, plusplus: true, nomen: true, regexp: true, maxerr: 50 */
/*global define, brackets, $, document */

define('main',['require','exports','module','InlineColorEditor'],function (require, exports, module) {
    
    
    var EditorManager       = brackets.getModule("editor/EditorManager"),
        ProjectManager      = brackets.getModule("project/ProjectManager"),
        ExtensionUtils      = brackets.getModule("utils/ExtensionUtils"),
        InlineColorEditor   = require("InlineColorEditor").InlineColorEditor,
        ColorUtils          = brackets.getModule("utils/ColorUtils");
    
    
    /**
     * Registered as an inline editor provider: creates an InlineEditorColor when the cursor
     * is on a color value (in any flavor of code).
     *
     * @param {!Editor} hostEditor
     * @param {!{line:Number, ch:Number}} pos
     * @return {?$.Promise} synchronously resolved with an InlineWidget, or null if there's
     *      no color at pos.
     */
    function inlineColorEditorProvider(hostEditor, pos) {
        var colorPicker, colorRegEx, cursorLine, inlineColorEditor, match, result,
            sel, start, end, startBookmark, endBookmark;
        
        sel = hostEditor.getSelection();
        if (sel.start.line !== sel.end.line) {
            return null;
        }
        
        colorRegEx = new RegExp(ColorUtils.COLOR_REGEX);
        cursorLine = hostEditor.document.getLine(pos.line);
        
        // Loop through each match of colorRegEx and stop when the one that contains pos is found.
        do {
            match = colorRegEx.exec(cursorLine);
            if (match) {
                start = match.index;
                end = start + match[0].length;
            }
        } while (match && (pos.ch < start || pos.ch > end));
        
        if (!match) {
            return null;
        }
        
        // Adjust pos to the beginning of the match so that the inline editor won't get 
        // dismissed while we're updating the color with the new values from user's inline editing.
        pos.ch = start;
        
        startBookmark = hostEditor._codeMirror.setBookmark(pos);
        endBookmark = hostEditor._codeMirror.setBookmark({ line: pos.line, ch: end });
        
        hostEditor.setSelection(pos, { line: pos.line, ch: end });
        
        inlineColorEditor = new InlineColorEditor(match[0], startBookmark, endBookmark);
        inlineColorEditor.load(hostEditor);

        result = new $.Deferred();
        result.resolve(inlineColorEditor);
        return result.promise();
    }
    
    
    // Initialize extension
    ExtensionUtils.loadStyleSheet(module, "css/main.css");
    
    EditorManager.registerInlineEditProvider(inlineColorEditorProvider);
    
    
    // for unit tests only
    exports.inlineColorEditorProvider = inlineColorEditorProvider;
});
