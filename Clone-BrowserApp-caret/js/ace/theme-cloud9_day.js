/* ***** BEGIN LICENSE BLOCK *****
 * Distributed under the BSD license:
 *
 * Copyright (c) 2010, Ajax.org B.V.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of Ajax.org B.V. nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL AJAX.ORG B.V. BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * ***** END LICENSE BLOCK ***** */

ace.define('ace/theme/cloud9_day', ['require', 'exports', 'module' , 'ace/lib/dom'], function(require, exports, module) {


exports.isDark = false;
exports.cssClass = "ace-cloud9-day";
exports.cssText = ".ace-cloud9-day .ace_gutter {\
background: #ECECEC;\
color: #333;\
}\
.ace-cloud9-day .ace_print-margin {\
width: 1px;\
background: #e8e8e8;\
}\
.ace-cloud9-day .ace_fold {\
background-color: #6B72E6;\
}\
.ace-cloud9-day {\
background-color: #F8F8F8;\
color: black;\
}\
.ace-cloud9-day .ace_cursor {\
color: black;\
}\
.ace-cloud9-day .ace_invisible {\
color: rgb(191, 191, 191);\
}\
.ace-cloud9-day .ace_storage,\
.ace-cloud9-day .ace_keyword {\
color: blue;\
}\
.ace-cloud9-day .ace_constant {\
color: rgb(197, 6, 11);\
}\
.ace-cloud9-day .ace_constant.ace_buildin {\
color: rgb(88, 72, 246);\
}\
.ace-cloud9-day .ace_constant.ace_language {\
color: rgb(88, 92, 246);\
}\
.ace-cloud9-day .ace_constant.ace_library {\
color: rgb(6, 150, 14);\
}\
.ace-cloud9-day .ace_invalid {\
background-color: rgba(255, 0, 0, 0.1);\
color: red;\
}\
.ace-cloud9-day .ace_support.ace_function {\
color: rgb(60, 76, 114);\
}\
.ace-cloud9-day .ace_support.ace_constant {\
color: rgb(6, 150, 14);\
}\
.ace-cloud9-day .ace_support.ace_type,\
.ace-cloud9-day .ace_support.ace_class {\
color: rgb(109, 121, 222);\
}\
.ace-cloud9-day .ace_keyword.ace_operator {\
color: rgb(104, 118, 135);\
}\
.ace-cloud9-day .ace_string {\
color: rgb(3, 106, 7);\
}\
.ace-cloud9-day .ace_comment {\
color: rgb(76, 136, 107);\
}\
.ace-cloud9-day .ace_comment.ace_doc {\
color: rgb(0, 102, 255);\
}\
.ace-cloud9-day .ace_comment.ace_doc.ace_tag {\
color: rgb(128, 159, 191);\
}\
.ace-cloud9-day .ace_constant.ace_numeric {\
color: rgb(0, 0, 205);\
}\
.ace-cloud9-day .ace_variable {\
color: rgb(49, 132, 149);\
}\
.ace-cloud9-day .ace_xml-pe {\
color: rgb(104, 104, 91);\
}\
.ace-cloud9-day .ace_entity.ace_name.ace_function {\
color: #0000A2;\
}\
.ace-cloud9-day .ace_heading {\
color: rgb(12, 7, 255);\
}\
.ace-cloud9-day .ace_list {\
color:rgb(185, 6, 144);\
}\
.ace-cloud9-day .ace_meta.ace_tag {\
color:rgb(0, 22, 142);\
}\
.ace-cloud9-day .ace_string.ace_regex {\
color: rgb(255, 0, 0)\
}\
.ace-cloud9-day .ace_marker-layer .ace_selection {\
background: rgb(181, 213, 255);\
}\
.ace-cloud9-day.ace_multiselect .ace_selection.ace_start {\
box-shadow: 0 0 3px 0px white;\
border-radius: 2px;\
}\
.ace-cloud9-day .ace_marker-layer .ace_step {\
background: rgb(252, 255, 0);\
}\
.ace-cloud9-day .ace_marker-layer .ace_stack {\
background: rgb(164, 229, 101);\
}\
.ace-cloud9-day .ace_marker-layer .ace_bracket {\
margin: -1px 0 0 -1px;\
border: 1px solid rgb(192, 192, 192);\
}\
.ace-cloud9-day .ace_marker-layer .ace_active-line {\
background: rgba(0, 0, 0, 0.07);\
}\
.ace-cloud9-day .ace_gutter-active-line {\
background-color : #dcdcdc;\
}\
.ace-cloud9-day .ace_marker-layer .ace_selected-word {\
background: rgb(250, 250, 255);\
border: 1px solid rgb(200, 200, 250);\
}\
.ace-cloud9-day .ace_indent-guide {\
background: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAACCAYAAACZgbYnAAAAE0lEQVQImWP4////f4bLly//BwAmVgd1/w11/gAAAABJRU5ErkJggg==\") right repeat-y;\
}\
";

var dom = require("../lib/dom");
dom.importCssString(exports.cssText, exports.cssClass);
});
