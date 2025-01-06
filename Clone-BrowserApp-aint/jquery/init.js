
$(document).ready(function() {
/*
*/
//________________________________________________________
chrome.app.window.current().maximize();
//________________________________________________________
function debug(a) {$("#debug").html(a).fadeIn(300);}
//_______________________________________________________VAR__________________________________________________________
var $ed = $("#editor");
var $ro = $("#rotide");
var $ol = $("#overlay");
var $po = $("#pop");
var $pl = $("#project-list");
var $cu = $("#current");
var $pd = $("#project-data");
var $ma = $("#margin");
var $p1 = $("#pane-1");
var fame = "";
var ftype = "";
var ftype_pro = "";
var sup = 0;
var seresArray = [];
var thistableid = "";
var dragW = "50";
//_______________________________________________________STO__________________________________________________________
chrome.storage.local.get(['fsiz','ffam','fcol','back','asoo'], function(result) {
var fsiz_l = result.fsiz;
var ffam_l = result.ffam;
var fcol_l = result.fcol;
var fbac_l = result.back;
var asoo_l = result.asoo;
if (!fcol_l || fcol_l.length < 1 || fcol_l == ""){
$("#input-fsize").val("12");
$("#font-type input.select-in").val("sans-serif");
$("#input-font-color").val("#444");
$("#input-bg-color").val("#fff");
$("#font-type input.select-in").css({"font-family":"sans-serif"});
$ed.css({"font-size":"12px","font-family":"san-serif","color":"#444","background-color":"#fff"});
}else{
$("#input-fsize").val(fsiz_l[0]);
$("#font-type input.select-in").val(ffam_l[0]);
if (fcol_l[0].indexOf("rgb") >= 0) {
var chex = colorToHex(fcol_l[0]);
$("#input-font-color").val(chex);
} else {
$("#input-font-color").val(fcol_l[0]);
};
if (fbac_l[0].indexOf("rgb") >= 0) {
var chex = colorToHex(fcol_l[0]);
$("#input-bg-color").val(chex);
} else {
$("#input-bg-color").val(fbac_l[0]);
};
$ed.css({"color":fcol_l[0],"background-color":fbac_l[0],"font-size":fsiz_l[0] + "px","font-family":ffam_l[0]});
$("hr").css({"border-color":fcol_l[0]});
$("table").css({"border-color":fcol_l[0]});
$("table td").css({"border-color":fcol_l[0]});
$("table th").css({"border-color":fcol_l[0]});
$("#font-type input.select-in").css({"font-family":ffam_l[0]});
}
if (asoo_l[0] === "autosave on"){
$("div.as-tog").removeClass("as-off").html("autosave on");
}else{
$("div.as-tog").addClass("as-off").html("autosave off");
}
});
//_______________________________________________________SIZ__________________________________________________________
var w = $("html").width();
var winh = $("html").height();
var navh = $("#nav-wrap").height();
var h = winh - navh;
var poph = h / 6;
var popw = w / 4;
poph = h - Math.round(poph);
popw = w - Math.round(popw);
var popmt = poph / 2;
var popml = popw / 2;
popmt = "-" + Math.round(popmt) + "px";
popml = "-" + Math.round(popml) + "px";
$("main").css({height:h+"px"});
$ma.css({height:h + "px"});
var dill = "" + w * 80;
dill = dill.slice(0,4);
var dillbar = dill - 60;
$pl.css({height:h + "px"});
$ed.css({height:h + "px"});
$("#help-win").css({height:h + 2 + "px"});
$("#notepad").css({height:h + "px",top:navh + "px",width:w / 2 + "px"});
$("#iframe").css({height:h - 50 + "px"});
var namein = $("#name-in").attr("data-in");
var renamein = $("#rename-in").attr("data-in");
var searchin = $("#search-in").attr("data-in");
var replacein = $("#replace-with").attr("data-in");
$("#name-in").val(namein);
$("#rename-in").val(renamein);
$("#search-in").val(searchin);
$("#replace-with").val(replacein);
//_______________________________________________________SUP__________________________________________________________
weasel("startup");
//_______________________________________________________EVT__________________________________________________________
//________________________________________________________
$('#t-buttons button').mousedown(function(){
var gcom = $(this).attr("data-com");
var gstr = $(this).attr("data-str");
if(gstr === "hr"){
gstr = "<hr/>";
}
if(gstr === "timestamp"){
gstr = ticdate() + " | " + tictoc();
}
if(gstr === "table"){
var tid = $.now();
var tdie = true;
gstr = '<table id="'+tid+'" class="user"><thead><tr><th></th></tr></thead></table><br><br>';
}
document.execCommand("styleWithCSS", 0, true);
document.execCommand(gcom, false, gstr);
if(tdie === true){
$("#"+tid).attr("contenteditable","false");
tdie = false;
}
return false;
});
//________________________________________________________
$ro.delegate("table.user", "click", function(){
thistableid = $(this).attr("id");
gettable(thistableid);
});
//________________________________________________________
$ma.draggable({
axis: "x",
containment: "#margin-wrap"
});
$("main").droppable({
accept: "#margin",
drop: function( event, ui ) {
dragW = ui.draggable.position().left;
dragW = dill - dragW + 26;
$ed.css({"padding-right":dragW+"px"});
}
});
//________________________________________________________
$po.draggable({
containment: "#wrapper"
});
//Not unless my ass sets it on fire first! - Emily Dustman, 2016
//________________________________________________________
$("#open-print").click(function(){
var now = $ro.html();
$("#printo").html(now);
$("#print-win").show();
window.print();
$("#print-win").hide();
});
//________________________________________________________
$("#print-help").click(function(){
var h_html = $('#i-dummy').contents().find('#h-wrapper').html();
$("#printo").html(h_html).css({"line-height":"100%","white-space":"normal"});
$("#print-win").show()
window.print();
$("#print-win").hide();
$("#printo").css({"line-height":"36px","white-space":"pre-wrap"});
});
//________________________________________________________
$('#wordcount').click(function(){
var shit = $ro.html();
$("#bin").html(shit);
var thistxt = $("#bin").html();
thistxt = thistxt.replace(/&#09;/g, "").replace(/\<\/h1\>/g, " @ ").replace(/\<div\>/g, " @ ").replace(/\<\/div\>/g, " @ \n");
$("#bin").html(thistxt);
thistxt = $("#bin").text();
thistxt = thistxt.replace(/\n/g, " @ ").replace(/\./g, "").replace(/\?/g, "").replace(/\!/g, "").replace(/\"/g, "").replace(/\:/g, "").replace(/\;/g, "").replace(/\-/g, "").replace(/\'/g, "").replace(/[\“\”\",\/#$%\^&\*{}=`\‘\’~\(\)@\+><\[\]]/g," ").replace(/[^\w ]/g, "").split(/\s+/).length - 1;
$("#dis-count").html(thistxt + " Words");
weasel("count");
$("#bin").empty();
});
//________________________________________________________
$('#table-ed-close').click(function(){
var thistable = $("#table-csv").val();
txttable(thistable);
});
//________________________________________________________
$("#set-font-color").click(function() {
var thiscol = $("#input-font-color").val();
$ed.css({"color":thiscol});
$("hr").css({"border-color":thiscol});
$("table").css({"border-color":thiscol});
$("table td").css({"border-color":thiscol});
$("table th").css({"border-color":thiscol});
});
//________________________________________________________
$("#set-bg-color").click(function() {
var thiscol = $("#input-bg-color").val();
$ed.css({"background-color":thiscol});
});
//________________________________________________________
$("#set-fsize").click(function() {
var thiscol = $("#input-fsize").val();
$ed.css({"font-size":thiscol + "px"});
});
//________________________________________________________
$("#set-reset").click(function() {
$("#input-fsize").val("12");
$("#font-type input.select-in").val("sans-serif");
$("#input-font-color").val("#444444");
$("#input-bg-color").val("#ffffff");
$("#font-type input.select-in").css({"font-family":"sans-serif"});
$ed.css({"color":"#444444","background-color":"#ffffff","font-size":"12px","font-family":"sans-serif"});
$("hr").css({"border-color":"#444"});
$("table").css({"border-color":"#444"});
$("table td").css({"border-color":"#444"});
$("table th").css({"border-color":"#444"});
});
//________________________________________________________
$("#font-type div.select-options div").click(function() {
var thiscol = $(this).text();
$ed.css({"font-family":thiscol});
$("#font-type input.select-in").css({"font-family":thiscol});
$("#font-type input.select-in").val(thiscol);
$("#font-type div.select-options").fadeOut();
});
//________________________________________________________
$("#font-type input.select-in").click(function() {
var scooby = $("#font-type div.select-options");
if(scooby.is(":hidden")){
scooby.fadeIn();
}else{
scooby.fadeOut();
}
});
$("#font-type button.select-drop").click(function() {
$("#font-type input.select-in").click();
});
//________________________________________________________
$("#search").click(function() {
getpos();
var input = $("#search-in").val();
if (!input | input.length < 1){return false}
$("#rotide seres[class]").removeAttr("class");
var gtx = $ro.html();
var $been = $("#bin");
$been.html(gtx);
gtx = $been.html();
gtx = gtx.replace(/<seres>/g,"");
gtx = gtx.replace(/<\/seres>/g,"");
$ro.prepend("\n");
if (! /^[a-zA-Z0-9\s\!\@\#\$\%\^\&\*\(\)\-\_\+\=\[\]\{\}\|\;\'\,\.\/\:\"\?\`\~\\]+$/.test(input)) {
$("#search-in").css({"background-color":"red"});
$("#search-in").val("No results found");
setTimeout( function() {
$("#search-in").css({"background-color":"white"});
$("#search-in").val(input);
}, 1000);
respos();
return false;
}
var what = new RegExp(input,"g");
var gsearch = gtx.replace(what,"<seres>"+input+"</seres>");
$ro.focus();
document.execCommand('selectAll');
document.execCommand('insertHTML',null,gsearch);
var seresLen = $("seres").length;
if (!seresLen){
$("#search-in").css({"background-color":"red"});
$("#search-in").val("No results found");
setTimeout( function() {
$("#search-in").css({"background-color":"white"});
$("#search-in").val(input);
}, 1000);
respos();
return false;
}
$ed.scrollTop(0);
seresArray = [];
i = 0;
do {
var res = $("#rotide seres:eq("+i+")").offset().top;
seresArray.push(res);
i += 1;
}while(i<seresLen);
sup = 0;
$("#rotide seres:eq(0)").addClass("curhi");
$ed.scrollTop(seresArray[0] - 100);
});
//________________________________________________________
$("#search-remove").click(function() {
$ol.css({"height":"100%"}).fadeOut();
getpos();
$("#rotide seres[class]").removeAttr("class");
var gtx = $ro.html();
var $been = $("#bin");
$been.html(gtx);
gtx = $been.html();
gtx = gtx.replace(/<seres>/g,"");
gtx = gtx.replace(/<\/seres>/g,"");
$ro.prepend("\n");
$ro.focus();
document.execCommand('selectAll');
document.execCommand('insertHTML',null,gtx);
$("#search-in").val(searchin);
$("#replace-with").val(replacein);
$po.fadeOut(300);
respos();
});
//________________________________________________________
$("#search-restore").click(function() {
$ol.css({"height":"100%"}).fadeOut();
$("#rotide seres[class]").removeAttr("class");
var gtx = $ro.html();
var $been = $("#bin");
$been.html(gtx);
gtx = $been.html();
gtx = gtx.replace(/<seres>/g,"");
gtx = gtx.replace(/<\/seres>/g,"");
$ro.prepend("\n");
$ro.focus();
document.execCommand('selectAll');
document.execCommand('insertHTML',null,gtx);
$("#search-in").val(searchin);
$("#replace-with").val(replacein);
$po.fadeOut(300);
respos();
});
//________________________________________________________
$("#search-next").click(function() {
if (sup < seresArray.length - 1){
$("#rotide seres[class]").removeAttr("class");
sup += 1;
$("#rotide seres:eq("+sup+")").addClass("curhi");
$ed.scrollTop(seresArray[sup] - 100);
}
});
//________________________________________________________
$("#search-prev").click(function() {
if (sup > 0){
$("#rotide seres[class]").removeAttr("class");
sup -= 1;
$("#rotide seres:eq("+sup+")").addClass("curhi");
$ed.scrollTop(seresArray[sup] - 100);
}
});
//________________________________________________________
$('#speak-play').toggle(function(){
var sel, range;
var selectedText;
if (window.getSelection) {
sel = window.getSelection();
if (sel.rangeCount) {
range = sel.getRangeAt(0);
selectedText = range.toString();
$(this).html("<i class='fa fa-stop'></i>");
chrome.tts.speak(selectedText, {'lang': 'en-US', 'rate': 0.75});
}
}
else if (document.selection && document.selection.createRange) {
range = document.selection.createRange();
selectedText = document.selection.createRange().text + "";
$(this).html("<i class='fa fa-stop'></i>");
chrome.tts.speak(selectedText, {'lang': 'en-US', 'rate': 0.75});
}
},
function() {
$(this).html("<i class='fa fa-play-circle'></i>");
chrome.tts.stop();
});
//________________________________________________________
$("#s-replace").click(function() {
var input = $("#search-in").val();
if (! /^[a-zA-Z0-9\s\!\@\#\$\%\^\&\*\(\)\-\_\+\=\[\]\{\}\|\;\'\,\.\/\:\"\?\`\~\\]+$/.test(input)) {
$("#search-in").css({"background-color":"red"});
$("#search-in").val("No results found");
setTimeout( function() {
$("#search-in").css({"background-color":"white"});
$("#search-in").val(input);
}, 1000);
respos();
return false;
}
var input_re = $("#replace-with").val();
var gex = $ro.html();
var $been = $("#bin");
$been.html(gex);
gex = $been.html();
var what = new RegExp(input,"g");
var gsearch = gex.replace(what,input_re);
$ro.prepend("\n");
$ro.focus();
document.execCommand('selectAll');
document.execCommand('insertHTML',null,gsearch);
respos();
});
//________________________________________________________
$('#tog-full').click(function(){
if ($(this).attr("disabled")){return false}
var current = $cu.val();
if(!current){return false}
$("#tog-full-pane").show();
var row = $ro.width();
$ro.css({width:row+"px",margin:"0 auto"});
$ed.css({width:"100%",height:"100%",position:"fixed",top:"0",left:"0","border-width":"0","padding-right":"50px"});
$p1.css({"z-index":"101"});
$("#margin-wrap").css({"z-index":"2"});
$(".size-con").attr("disabled","disabled");
$(this).removeAttr("disabled");
});
//________________________________________________________
$('#tog-full-pane').click(function() {
if ($(this).attr("disabled")){return false}
$(this).hide();
$ed.css({height:h + "px",width:"80%",position:"relative",top:"auto",left:"auto","border-width":"1px","padding-right":dragW+"px"});
$ro.css({width:"100%"});
$p1.css({"z-index":"2"});
$("#margin-wrap").css({"z-index":"101"});
$(".size-con").removeAttr("disabled");
});
//________________________________________________________
$('#tog-list').toggle(function(){
if ($(this).attr("disabled")){return false}
var row = $ro.width();
$ro.css({width:row+"px",margin:"0 auto"});
$ed.css({width:"100%","padding-right":"50px"});
$ma.hide();
$pl.hide();
$(".size-con").attr("disabled","disabled");
$(this).removeAttr("disabled");
},
function() {
if ($(this).attr("disabled")){return false}
$ed.css({width:"80%","padding-right":dragW+"px"});
$ro.css({width:"100%"});
$ma.show();
$pl.show();
$(".size-con").removeAttr("disabled");
$pl.animate({scrollTop: $(".selected").offset().top - 50});
});
//________________________________________________________
$('#tog-notepad').toggle(function(){
if ($(this).attr("disabled")){return false}
var row = $ro.width();
$ma.hide();
$pl.hide();
$(".size-con").attr("disabled","disabled");
$(this).removeAttr("disabled");
$ed.css({width:"50%"});
$ro.css({width:"100%",margin:"0 auto"});
$("#notepad").show();
},
function() {
if ($(this).attr("disabled")){return false}
$ed.css({width:"80%"});
$ro.css({width:"100%"});
$("#notepad").hide();
$ma.show();
$pl.show();
$(".size-con").removeAttr("disabled");
});
//________________________________________________________
$pl.delegate("li.p-item", "mousedown", function(){
getpos();
var current = $cu.val();
var curdat = $ro.html();
$("#"+current).text(curdat);
var idee = $(this).attr("data-id");
var getcode = $("#"+idee).text();
$cu.val(idee);
$ro.html(getcode);
$("li.p-item").removeClass("selected");
$(this).addClass("selected");
respos();
});
//________________________________________________________
$("#imex").click(function() {
var current = $cu.val();
if (!current) {
//
}else{
var curdat = $ro.html();
$("#"+current).text(curdat);
}
weasel("imex");
});
//________________________________________________________
$("#search-tog").click(function() {
$ol.css({"height":navh + "px"}).fadeIn();
weasel("search");
getpos();
});
//________________________________________________________
$("#tog-page-setup").click(function() {
weasel("setup");
getpos();
});
//________________________________________________________
$("#donate").click(function() {
weasel("donate");
getpos();
});
//________________________________________________________
$("#pop input").click(function() {
var getin = $(this).val();
var getdata = $(this).attr("data-in");
if (getin === getdata) {
$(this).val("");
}
});
//________________________________________________________
$("#add").click(function() {
var current = $cu.val();
if (!current) {
//
}else{
var curdat = $ro.html();
$("#"+current).text(curdat);
}
weasel("add");
});
//________________________________________________________
$("#add-name").click(function() {
var add_id = $.now();
var add_name = $("#name-in").val();
if(!add_name){
//
}else{
$("li.p-item").removeClass("selected");
$("ul.project").prepend('\n<li class="p-item selected" data-id="'+add_id+'">'+add_name+'</li>');
$pd.prepend('\n<textarea class="pd-item" id="'+add_id+'"></textarea>');
$ro.html("");
$cu.val(add_id);
$po.fadeOut(300);
$ol.fadeOut(300);
$("#name-in").val(namein);
$p1.show();
$pl.animate({scrollTop: $(".selected").offset().top - 50});
$ed.scrollTop(0);
$ro.focus();
}
});
//________________________________________________________
$("#delete").click(function(){
var selected = $(".selected").html();
$("#del-yes").html("<i class='fa fa-minus-circle'></i> Delete : "+selected);
weasel("delete");
});
//________________________________________________________
$("#del-yes").click(function(){
var newsel = "";
var current = $cu.val();
var newval = $(".selected").next().attr("data-id");
if (!newval){
newval = $(".selected").prev().attr("data-id");
if(!newval){
newval = "";
newsel = "";
}else{
newsel = $(".selected").prev();
}
}else{
newsel = $(".selected").next();
}
$(".selected").replaceWith("<!--removed-->");
$("#"+current).replaceWith("<!--removed-->");
if (!newsel){
current = "";
$p1.hide();
weasel("add");
}else{
$(newsel).addClass("selected");
current = $("#"+newval).text();
$po.fadeOut(300);
$ol.fadeOut(300);
$pl.animate({scrollTop: $(".selected").offset().top - 50});
}
$ro.html(current);
$cu.val(newval);
respos();
});
//________________________________________________________
$("#add-rename").click(function() {
var add_name = $("#rename-in").val();
$("#rename-in").val(renamein);
$(".selected").html(add_name);
$po.fadeOut(300);
$ol.fadeOut(300);
$("#rename-in").val("");
});
//________________________________________________________
$("#rename").click(function(){
var selected = $(".selected").html();
$("#add-rename").html("<i class='fa fa-tag'></i> Rename : "+selected);
weasel("rename");
});
//________________________________________________________
$("button.cancel-pop").click(function() {
$("#name-in").val(namein);
$("#rename-in").val(renamein);
$("#search-in").val(searchin);
$("#replace-with").val(replacein);
var current = $cu.val();
if (!current) {
var add_id = $.now();
var add_name = "Entry Name";
$("li.p-item").removeClass("selected");
$("ul.project").prepend('\n<li class="p-item selected" data-id="'+add_id+'">'+add_name+'</li>');
$pd.prepend('\n<textarea class="pd-item" id="'+add_id+'"></textarea>');
$ro.html("Entry Text");
$cu.val(add_id);
$p1.show();
$ed.scrollTop(0);
$ro.focus();
}
$po.fadeOut(300).blur();
$ol.fadeOut(300);
});
//________________________________________________________
$("button.cancel-pop-x").click(function() {
$("#name-in").val(namein);
$("#rename-in").val(renamein);
$("#search-in").val(searchin);
$("#replace-with").val(replacein);
var current = $cu.val();
if (!current) {
var add_id = $.now();
var add_name = "Entry Name";
$("li.p-item").removeClass("selected");
$("ul.project").prepend('\n<li class="p-item selected" data-id="'+add_id+'">'+add_name+'</li>');
$pd.prepend('\n<textarea class="pd-item" id="'+add_id+'"></textarea>');
$ro.html("Entry Text");
$cu.val(add_id);
$p1.show();
$ed.scrollTop(0);
$ro.focus();
}
$po.fadeOut(300).blur();
$ol.fadeOut(300);
});
//________________________________________________________
$("#close").click(function() {
var current = $cu.val();
if (!current){
chrome.app.window.current().maximize();
chrome.app.window.current().close();
}else{
getpos();
nipSave();
storage_l();
chrome.app.window.current().maximize();
chrome.app.window.current().close();
}
});
//________________________________________________________
$("#minimize").click(function() {
var current = $cu.val();
if (!current){
chrome.app.window.current().minimize();
}else{
nipSave();
storage_l();
chrome.app.window.current().minimize();
}
});
//________________________________________________________
$("#minify").toggle(function() {
if ($(this).attr("disabled")){return false}
var current = $cu.val();
if(!current){return false}
nipSave();
storage_l();
var row = $ro.width();
$ro.css({width:"400px", padding:"20px"});
$ed.css({width:"100%","padding":"0"});
$("#tools").hide();
$pl.hide();
$("#tog-list").hide();
$ma.hide();
$("#help").hide();
$("#tog-full").hide();
$("#tog-notepad").hide();
$("#drag-l").fadeIn();
$("#drag-r").fadeIn();
$("#nav").fadeOut(300);
chrome.app.window.current().setAlwaysOnTop(true);
chrome.app.window.current().restore();
},
function() {
if ($(this).attr("disabled")){return false}
$p1.hide();
$ed.css({width:"80%","padding":"50px","padding-right":dragW+"px"});
setTimeout( function() {chrome.app.window.current().maximize();}, 200);
setTimeout( function() {
$("#drag-l").hide();
$("#drag-r").hide();
$p1.show();
$("#nav").fadeIn(300);
$ma.show();
$pl.show();
$("#tog-list").show();
$("#help").show();
$("#tog-full").show();
$("#tog-notepad").show();
$("#tools").show();
$ro.css({width:"100%",padding:"0"});
}, 500);
chrome.app.window.current().setAlwaysOnTop(false);
});
//________________________________________________________
$("#help").click(function() {
if ($("#help-win").is(":hidden")) {
var h_html = $('#i-dummy').contents().find('#h-wrapper').html();
$("#iframe").html(h_html);
$("#help-win").fadeIn(300);
} else {
  $("#help-win").fadeOut(300);
}
});
//________________________________________________________
$("#su-help").click(function() {
var h_html = $('#i-dummy').contents().find('#h-wrapper').html();
$("#iframe").html(h_html);
$("#help-win").fadeIn(300);
});
//________________________________________________________
$("#close-help").click(function() {
var current = $("#current").val();
if(!current){
  //
}else{
$ol.fadeOut(300).css({top:navh + "px"});
}
$("#help-win").fadeOut(300);});
//________________________________________________________
$("div.as-tog").click(function() {
var asoo_l = $(this).html();
if (asoo_l === "autosave on"){
$(this).addClass("as-off").html("autosave off");
}else{
$(this).removeClass("as-off").html("autosave on");
}
});
//_______________________________________________________FNC__________________________________________________________
//________________________________________________________
function gettable(b) {
var table_data = [];
$('#' + b + ' tr').each(function(){
var row_data = [];
$('th', this).each(function(){row_data.push($(this).html());});
$('td', this).each(function(){row_data.push($(this).html());});
table_data.push(row_data + "^Xv");
});
table_data = ""+table_data;
table_data = table_data.replace(/\^Xv,/g,"\n");
table_data = table_data.replace(/\^Xv/g,"\n");
table_data = $.trim(table_data);
$("#table-csv").val(table_data);
$("#table-ed").fadeIn(300);
$ol.css({top:"0"}).fadeIn(300);
}
//________________________________________________________
function txttable(txt) {
var stecho = "";
var splittxt = txt.split("\n");
if (!splittxt[0]) {
$("#"+thistableid).html("<th></th>");
$("#table-ed").fadeOut(300);
$ol.fadeOut(300).css({top:navh + "px"});
return false;
}
var sx = 0;
var i = 1;
var st = splittxt[0].split(",");
stecho += '<thead><tr>';
do {
stecho += '<th>'+st[sx]+'</th>' + '\n';
sx+=1;
i+=1;
}while(sx<st.length);
stecho += "</tr></thead>";
if (!splittxt[1]) {
 //
}else{
i = 1;
do {
sx = 0;
stecho += "<tr>";
st = splittxt[i].split(",");
do {
stecho += '<td>'+st[sx]+'</td>' + "\n";
sx+=1;
}while(sx<st.length);
stecho += "</tr>";
i+=1;
}while(i<splittxt.length);
}
$("#"+thistableid).html(stecho);
$("#table-ed").fadeOut(300);
$ol.fadeOut(300).css({top:navh + "px"});
}
//________________________________________________________
function getpos() {
var current = $cu.val();
var fart = $ro.offset().top;
$("#"+current).attr("data-pos",fart);
}
//________________________________________________________
function respos() {
var current = $cu.val();
fart = $("#"+current).attr("data-pos");
fart = parseInt(fart);
if(fart >= navh) {
fart = 0;
} else {
fart = fart + "";
fart = fart.replace("-","");
fart = parseInt(fart);
fart = fart + 50 + navh + 1;
};
$ed.scrollTop(fart);
}
//________________________________________________________
function cleanup(geth) {
var beta = "textarea>";
var betb = "</data>";
var pat = beta+'\s*[\r\n]+\s*'+betb;
pat = new RegExp(pat, 'g');
geth = geth.replace(pat,beta + "\n" + betb);
geth = geth.replace(/\n\<\!\-\-removed\-\-\>/g, "");
return geth;
}
//________________________________________________________
function storage_l() {
chrome.storage.local.set({
'fsiz': [$("#input-fsize").val()],
'ffam': [$("#font-type input.select-in").val()],
'fcol': [$("#input-font-color").val()],
'back': [$("#input-bg-color").val()],
'asoo': [$("div.as-tog").html()]
});
}
//________________________________________________________
var timerID = setInterval(function() {
writeEditorToFileInc();
}, 60000);
//________________________________________________________
function weasel(a) {
if(a !== "search"){
$ol.fadeIn(300);
}
$po.fadeIn(300);
$ro.blur();
$("div.dia").hide(1);
var popos = $("#pop-"+a).height();
popos += 64;
$("#pop").css({"margin-top":"-" + popos / 2 + "px"});
$("#pop-"+a).fadeIn(200);
$("#pop-"+a+" input").focus();
}
//________________________________________________________
function ticdate() {
var now = new Date();
months = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var formattedDAte = months[now.getMonth()]+"-"+now.getDate()+"-"+now.getFullYear();
return formattedDAte;
}
//________________________________________________________
function tictoc() {
var a_p = "";
var d = new Date();
var curr_hour = d.getHours();
if (curr_hour < 12) {
a_p = "AM";
}else{
a_p = "PM";
}
if (curr_hour == 0) {
curr_hour = 12;
}
if (curr_hour > 12) {
curr_hour = curr_hour - 12;
}
var curr_min = d.getMinutes();
curr_min = curr_min + "";
if (curr_min.length == 1) {
curr_min = "0" + curr_min;
}
var timenow = "";
timenow = curr_hour + " : " + curr_min + " " + a_p;
return timenow;
}
//________________________________________________________
function colorToHex(color) {
if (color.substr(0, 1) === '#') {
return color;
}
var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);
var red = parseInt(digits[2]);
var green = parseInt(digits[3]);
var blue = parseInt(digits[4]);
var rgb = blue | (green << 8) | (red << 16);
return digits[1] + '#' + rgb.toString(16);
};
//_______________________________________________________FIL__________________________________________________________
//________________________________________________________
$("#su-open").click(function() {
chrome.fileSystem.chooseEntry({type: 'openWritableFile',accepts: [{'extensions': ['aint']}],acceptsAllTypes: false}, onWritableFileToOpen);
});
//________________________________________________________
$("#su-new").click(function() {
ftype = "new";
chrome.fileSystem.chooseEntry({type: 'saveFile',suggestedName: fame+'.aint',accepts: [{'extensions': ['aint']}],acceptsAllTypes: false}, onChosenFileToSave);
});
//________________________________________________________
$("#new").click(function() {
nipSave();
ftype = "new";
chrome.fileSystem.chooseEntry({type: 'saveFile',suggestedName: fame+'.aint',accepts: [{'extensions': ['aint']}],acceptsAllTypes: false}, onChosenFileToSave);
});
//________________________________________________________
$("#open").click(function() {
nipSave();
chrome.fileSystem.chooseEntry({type: 'openWritableFile',accepts: [{'extensions': ['aint']}],acceptsAllTypes: false}, onWritableFileToOpen);
});
//________________________________________________________
$("#save").click(function() {
ftype = "saveas";
chrome.fileSystem.chooseEntry({type: 'saveFile',suggestedName: fame+'.aint',accepts: [{'extensions': ['aint']}],acceptsAllTypes: false}, onChosenFileToSave);
});
//________________________________________________________
$("#import-data button").click(function() {
ftype = $(this).attr("data-type");
nipSave();
chrome.fileSystem.chooseEntry({type: 'openWritableFile',accepts: [{'extensions': [ftype]}],acceptsAllTypes: false}, IEonWritableFileToOpen);
$po.fadeOut(300);
$ol.fadeOut(300);
$p1.show();
});
//________________________________________________________
$("#export-data button").click(function() {
nipSave();
ftype = $(this).attr("data-type");
ftype_pro = ftype;
ftype = ftype.replace("pro-","");
//debug(ftype + " - " + ftype_pro);
chrome.fileSystem.chooseEntry({type: 'saveFile',suggestedName: fame+'.'+ftype,accepts: [{'extensions': [ftype]}],acceptsAllTypes: false}, IEonChosenFileToSave);
$po.fadeOut(300);
$ol.fadeOut(300);
$p1.show();
});
//________________________________________________________
$("#export-pro-data button").click(function() {
nipSave();
ftype = $(this).attr("data-type");
ftype_pro = ftype;
ftype = ftype.replace("pro-","");
chrome.fileSystem.chooseEntry({type: 'saveFile',suggestedName: fame+'.'+ftype,accepts: [{'extensions': [ftype]}],acceptsAllTypes: false}, IEonChosenFileToSave);
$po.fadeOut(300);
$ol.fadeOut(300);
$p1.show();
});
//________________________________________________________
var fileRentry;
var fileEntry;
var hasWriteAccess;
var IEfileEntry;
var IEhasWriteAccess;
//________________________________________________________
function errorHandler(e) {
var msg = "";
switch (e.code) {
case FileError.QUOTA_EXCEEDED_ERR:
msg = "QUOTA_EXCEEDED_ERR";
break;
case FileError.NOT_FOUND_ERR:
msg = "NOT_FOUND_ERR";
break;
case FileError.SECURITY_ERR:
msg = "SECURITY_ERR";
break;
case FileError.INVALID_MODIFICATION_ERR:
msg = "INVALID_MODIFICATION_ERR";
break;
case FileError.INVALID_STATE_ERR:
msg = "INVALID_STATE_ERR";
break;
default:
msg = "Unknown Error";
break;
};
console.log("Error: " + msg);
}
//________________________________________________________
function IEsetFile(theFileEntry, isWritable) {
IEfileEntry = theFileEntry;
IEhasWriteAccess = isWritable;
}
//________________________________________________________
function setFile(theFileEntry, isWritable) {
fileEntry = theFileEntry;
hasWriteAccess = isWritable;
if(!fileEntry){fileEntry = fileRentry};
}
//________________________________________________________
function readFileIntoEditor(theFileEntry) {
if (theFileEntry) {
theFileEntry.file(function(file) {
var fileReader = new FileReader();
fileReader.onload = function(e) {
var opfile = e.target.result;
fileRentry = theFileEntry;
var fool = theFileEntry.fullPath;
fool = fool.split("/");
fool = fool[1];
fame = fool.replace(".aint","");
$("#file-name").html(fame);
opfile = cleanup(opfile);
$("#bin").html(opfile);
var glist = $("project").html();
var gdata = $("data").html();
var gnote = $("notepad").html();
$("#bin").empty();
$pl.html(glist);
$pd.html(gdata);
$("#notepad").val(gnote);
var newval = $(".selected").attr("data-id");
if (!newval){
var data_first = $("#project-data textarea:first").text();
var current = $("#project-data textarea:first").attr("id");
if (!current) {
$p1.hide();
weasel("add");
}else{
$("ul.project li:first").addClass("selected");
$cu.val(current);
$ro.html(data_first);
$p1.fadeIn(300);
}
}else{
var current = $("#"+newval).text();
$cu.val(newval);
$ro.html(current);
$p1.fadeIn(300);
}
$po.fadeOut(300);
$ol.fadeOut(300);
$("#app-splash").remove();
$("#nav-wrap").show();
$pl.scrollTop(0);
$pl.animate({scrollTop: $(".selected").offset().top - 50});
respos();
$(".project").sortable();
};
fileReader.onerror = function(e) {
console.log("Read failed: " + e.toString());
};
fileReader.readAsText(file);
}, errorHandler);
}
}
//________________________________________________________
function writeEditorToFileInc() {
if (!fileEntry | !fileRentry){return false};
var saveon = $("div.as-tog").html();
if (saveon === "autosave off") {return false};
fileEntry.createWriter(function(fileWriter) {
fileWriter.onerror = function(e) {
//
};
getpos();
var current = $cu.val();
if (!current){
current = "editor";
}
var curdat = $ro.html();
var curnot = $("#notepad").val();
$("#"+current).text(curdat);
var timenow = tictoc();
$("#as-dis").html("Last saved - " + timenow);
var glist = "<project>" + $pl.html() + "</project>\n";
var gnote = "<notepad>" + curnot + "</notepad>\n";
var gdata = "<data>" + $pd.html() + "</data>";
var w_data = glist + gnote + gdata;
var blob = new Blob([w_data], {type: 'text/plain'});
fileWriter.truncate(blob.size);
fileWriter.onwriteend = function() {
fileWriter.onwriteend = function(e) {
/*console.log("Write completed.");*/
};
fileWriter.write(blob);
};
}, errorHandler);
}
//________________________________________________________
function nipSave() {
if (!fileEntry | !fileRentry){return false};
fileEntry.createWriter(function(fileWriter) {
fileWriter.onerror = function(e) {
//
};
getpos();
var current = $cu.val();
if (!current){
current = "editor";
}
var curdat = $ro.html();
var curnot = $("#notepad").val();
$("#"+current).text(curdat);
var timenow = tictoc();
$("#as-dis").html("Last saved - " + timenow);
var glist = "<project>" + $pl.html() + "</project>\n";
var gnote = "<notepad>" + curnot + "</notepad>\n";
var gdata = "<data>" + $pd.html() + "</data>";
var w_data = glist + gnote + gdata;
var blob = new Blob([w_data], {type: 'text/plain'});
fileWriter.truncate(blob.size);
fileWriter.onwriteend = function() {
fileWriter.onwriteend = function(e) {
/*console.log("Write completed.");*/
};
fileWriter.write(blob);
};
}, errorHandler);
}
//________________________________________________________
function writeEditorToFile(theFileEntry) {
theFileEntry.createWriter(function(fileWriter) {
fileWriter.onerror = function(e) {
//
};
fileRentry = theFileEntry;
$("#nav-wrap").show();
var fool = theFileEntry.fullPath;
fool = fool.split("/");
fool = fool[1];
fame = fool.replace(".aint","");
$("#file-name").html(fame);
if (ftype === "new") {
$("#app-splash").remove();
$ro.html("");
$("#notepad").val("");
$pl.empty();
$pd.empty();
$("#as-dis").empty();
var glist = '<project>\n<ul class="project" data-id="project">\n</ul>\n</project>\n';
var gdata = '<data>\n</data>';
$pl.html('\n<ul class="project" data-id="project">\n</ul>\n');
$p1.hide();
weasel("add");
$(".project").sortable();
}else{
var current = $cu.val();
var curdat = $ro.html();
var curnot = $("#notepad").val();
$("#"+current).text(curdat);
var glist = "<project>\n" + $pl.html() + "\n</project>\n";
var gnote = "<notepad>\n" + curnot + "\n</notepad>\n";
var gdata = "<data>\n" + $pd.html() + "\n</data>\n";
}
var w_data = glist + gnote + gdata;
var blob = new Blob([w_data], {type: 'text/plain'});
fileWriter.truncate(blob.size);
fileWriter.onwriteend = function() {
fileWriter.onwriteend = function(e) {
/*console.log("Write completed.");*/
};
fileWriter.write(blob);
};
}, errorHandler);
}
//________________________________________________________
var onChosenFileToSave = function(theFileEntry) {
setFile(theFileEntry, true);
writeEditorToFile(theFileEntry);
};
//________________________________________________________
var onWritableFileToOpen = function(theFileEntry) {
setFile(theFileEntry, true);
readFileIntoEditor(theFileEntry);
};
//________________________________________________________
var IEonChosenFileToSave = function(theFileEntry) {
IEsetFile(theFileEntry, true);
IEwriteEditorToFile(theFileEntry);
};
//________________________________________________________
var IEonWritableFileToOpen = function(theFileEntry) {
IEsetFile(theFileEntry, true);
IEreadFileIntoEditor(theFileEntry);
};
//________________________________________________________
function IEreadFileIntoEditor(theFileEntry) {
if (theFileEntry) {
theFileEntry.file(function(file) {
var fileReader = new FileReader();
fileReader.onload = function(e) {
var opfile = e.target.result;
var fool = theFileEntry.fullPath;
fool = fool.split("/");
fool = fool[1];
fool = fool.replace("."+ftype,"");
getpos();
var add_id = $.now();
var add_name = fool;
var current = $cu.val();
if (!current){
$pl.html('\n<ul class="project" data-id="project">\n</ul>\n');
$p1.show();
}
if(ftype === "txt"){
$("li.p-item").removeClass("selected");
$("ul.project").prepend('\n<li class="p-item selected" data-id="'+add_id+'">'+add_name+'</li>');
var fixit = opfile.replace(/\n/g,"<br />");
$pd.prepend('\n<textarea class="pd-item" id="'+add_id+'">'+fixit+'</textarea>');
$ro.html(fixit);
$cu.val(add_id);
}
if(ftype === "html") {
$("li.p-item").removeClass("selected");
var fixit = opfile.split("<body>");
$("#bin").html(fixit[1]);
$("#bin *[id]").removeAttr("id");
$("#bin *[class]").removeAttr("class");
$("#bin *[contenteditable]").removeAttr("contenteditable");
fixit = $("#bin").html();
$("#bin").empty();
fixit = fixit + "<br /><br />";
fixit = fixit.replace(/\<\/body\>/g,"");
fixit = fixit.replace(/\<\/html\>/g,"");
fixit = fixit.replace("<main>","");
fixit = fixit.replace("</main>","");
fixit = fixit.replace(/\n/g,"");
$("ul.project").prepend('\n<li class="p-item selected" data-id="'+add_id+'">'+add_name+'</li>');
$pd.prepend('\n<textarea class="pd-item" id="'+add_id+'">'+fixit+'</textarea>');
$ro.html(fixit);
$cu.val(add_id);
$("#rotide table").each(function(){
$(this).addClass("user");
var thistableid = $.now();
$(this).attr("id",thistableid);
});
}
$pl.animate({scrollTop: $(".selected").offset().top - 50});
$ed.scrollTop(0);
};
fileReader.onerror = function(e) {
console.log("Read failed: " + e.toString());
};
fileReader.readAsText(file);
}, errorHandler);
}
}
//________________________________________________________
function IEwriteEditorToFile(theFileEntry) {
theFileEntry.createWriter(function(fileWriter) {
fileWriter.onerror = function(e) {
//
};
$("#nav-wrap").show();
var fool = theFileEntry.fullPath;
fool = fool.split("/");
fool = fool[1];
fool = fool.replace("."+ftype,"");
//----------------------------------------------------------------write project text
//----------------------------------------------------------------
if (ftype_pro === "pro-txt" && ftype === "txt") {
var gex = "";
$("#project-data textarea").each(function(){
gex += $(this).val() + "\n\n_______________________________________________________\n\n";
});
gex = gex.replace(/\<\/span\>/g,"");
gex = gex.replace(/\<span\>/g,"");
gex = gex.replace(/\<div\>/g,"");
gex = gex.replace(/\<br\>\<\/div\>/g,"</div>");
gex = gex.replace(/\<\/div\>/g,"\n");
gex = gex.replace(/\&nbsp\;/gi,"");
gex = gex.replace(/\<br\>\<\/li\>/g,"</li>");
gex = gex.replace(/\<li\>/g,"\n    * ");
gex = gex.replace(/\<\/li\>/g,"");
gex = gex.replace(/\<ul\>/g,"\n");
gex = gex.replace(/\<ol\>/g,"\n");
gex = gex.replace(/\<\/ul\>/g,"\n");
gex = gex.replace(/\<\/ol\>/g,"\n");
$("#bin").html(gex);
$("#bin br").replaceWith("\n");
$("#bin hr").replaceWith("\n---------------------------------------------------------------------------------------------\n");
if($("#bin h1").length){
$("#bin h1").each(function(){
var geth = $(this).html();
$(this).replaceWith(geth.toUpperCase() + "\n");
});
}
if($("#bin table").length){
$("#bin table").each(function(){
var table_data = [];
var spacer = [];
var cell = "";
var div = "                                                             ";
var divx = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
var i = 0;
$('tr', this).each(function(){
var row_data = [];
$('th', this).each(function(){
cell = $(this).text() + div;
cell = cell.substr(0,20);
row_data.push(cell);
});
i = 0;
$('td', this).each(function(){
cell = $(this).text() + div;
cell =  cell.substr(0,20);
row_data.push(cell);
});
table_data.push(row_data + "^Xv");
});
table_data = ""+table_data;
table_data = table_data.replace(/\n/g,"");
table_data = table_data.replace(/\^Xv,/g, "\n");
table_data = table_data.replace(/\^Xv/g, "\n");
table_data = table_data.replace(/,/g,"");
var baltab = table_data.split("\n");
baltab = baltab[0].length;
table_data = table_data.replace(/\n/g,"\n" + divx.substr(0,baltab) + "\n");
table_data = $.trim(table_data) + "\n";
$(this).replaceWith(table_data);
});
}
$("#bin").html("<pre>" + $("#bin").html() + "</pre>");
var w_data = $("#bin").text();
w_data = w_data.replace(/\n\n---------------------------------------------------------------------------------------------\n/g,"\n---------------------------------------------------------------------------------------------\n");
w_data = w_data.replace(/---------------------------------------------------------------------------------------------\n\n/g,"---------------------------------------------------------------------------------------------\n");
$("#bin").empty();
};
//----------------------------------------------------------------
//----------------------------------------------------------------end write project text

//----------------------------------------------------------------write project html
//----------------------------------------------------------------
if (ftype_pro === "pro-html" && ftype === "html") {
var gex = "";
$("#project-data textarea").each(function(){
gex += $(this).val() + "\n<br>\n<br><hr/>\n<br>\n<br>\n";
});
var css_temp = $("#css_temp").val();
var ffam = $("#font-type input.select-in").val();
var fsiz = $("#font-size input.select-in").val();
var fcol = $("#ps-font-color input.select-in").val();
var fbac = $("#ps-bg-color input.select-in").val();
css_temp = css_temp.replace("_ffam_",ffam);
css_temp = css_temp.replace("_fsiz_",fsiz);
css_temp = css_temp.replace(/_fcol_/g,fcol);
css_temp = css_temp.replace(/_fbac_/g,fbac);
var hTop = '<!DOCTYPE html>\n'+
'<html dir="ltr" lang="en-US">\n'+
'<head>\n'+
'<title>'+fool+'</title>\n'+
'<meta charset="UTF-8">\n'+
'<style>\n'+
css_temp + "</style>\n"+
'</head>\n'+
'<body>\n';
var hBot = '</body>\n'+
'</html>';
$("#bin").html(gex);
$("#bin *[id]").removeAttr("id");
$("#bin *[class]").removeAttr("class");
$("#bin *[contenteditable]").removeAttr("contenteditable");
gex = $("#bin").html();
$("#bin").empty();
gex = gex.replace(/\n/g,"<br>");
gex = gex.replace(/\n\</g,"<");
gex = gex.replace(/\>\n/g,">");
gex = gex.replace(/\<div/g,"\n<div");
gex = gex.replace(/\<\/div\>/g,"</div>\n");
gex = gex.replace(/\<span/g,"\n<span");
gex = gex.replace(/\<\/span\>/g,"</span>\n");
gex = gex.replace(/\<h1/g,"\n<h1");
gex = gex.replace(/\<\/h1\>/g,"</h1>\n");
gex = gex.replace(/\<ol/g,"\n<ol");
gex = gex.replace(/\<\/ol\>/g,"</ol>\n");
gex = gex.replace(/\<ul/g,"\n<ul");
gex = gex.replace(/\<\/ul\>/g,"</ul>\n");
gex = gex.replace(/\<li/g,"\n<li");
gex = gex.replace(/\<\/li\>/g,"</li>\n");
gex = gex.replace(/\<table/g,"\n<table");
gex = gex.replace(/\<\/table\>/g,"</table>\n");
gex = gex.replace(/\<thead/g,"\n<thead");
gex = gex.replace(/\<\/thead\>/g,"</thead>\n");
gex = gex.replace(/\<tbody/g,"\n<tbody");
gex = gex.replace(/\<\/tbody\>/g,"</tbody>\n");
gex = gex.replace(/\<tr/g,"\n<tr");
gex = gex.replace(/\<\/tr\>/g,"</tr>\n");
gex = gex.replace(/\<th/g,"\n<th");
gex = gex.replace(/\<\/th\>/g,"</th>\n");
gex = gex.replace(/\<td/g,"\n<td");
gex = gex.replace(/\<\/td\>/g,"</td>\n");
gex = gex.replace(/\>\n\n\</g,">\n<");
gex = gex.replace(/\<div\>\<br\>\<\/div\>/g,"<br>");
gex = gex.replace(/\<br\>\<\/div\>/g,"</div>");
gex = gex.replace(/\<br\>\<\/li\>/g,"</li>");
gex = gex.replace(/\<div\>\<hr\>\<\/div\>/g,"<hr>");
var w_data = hTop + gex + hBot;
};
//----------------------------------------------------------------
//----------------------------------------------------------------end write project html
if (ftype === "txt" && ftype_pro === "txt") {
//----------------------------------------------------------------write text
//----------------------------------------------------------------
var gex = $ro.html();
gex = gex.replace(/\<\/span\>/g,"");
gex = gex.replace(/\<span\>/g,"");
gex = gex.replace(/\<div\>/g,"");
gex = gex.replace(/\<br\>\<\/div\>/g,"</div>");
gex = gex.replace(/\<\/div\>/g,"\n");
gex = gex.replace(/\&nbsp\;/gi,"");
gex = gex.replace(/\<br\>\<\/li\>/g,"</li>");
gex = gex.replace(/\<li\>/g,"\n    * ");
gex = gex.replace(/\<\/li\>/g,"");
gex = gex.replace(/\<ul\>/g,"\n");
gex = gex.replace(/\<ol\>/g,"\n");
gex = gex.replace(/\<\/ul\>/g,"\n");
gex = gex.replace(/\<\/ol\>/g,"\n");
$("#bin").html(gex);
$("#bin br").replaceWith("\n");
$("#bin hr").replaceWith("\n---------------------------------------------------------------------------------------------\n");
if($("#bin h1").length){
$("#bin h1").each(function(){
var geth = $(this).html();
$(this).replaceWith(geth.toUpperCase() + "\n");
});
}
if($("#bin table").length){
$("#bin table").each(function(){
var table_data = [];
var spacer = [];
var cell = "";
var div = "                                                             ";
var divx = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
var i = 0;
$('tr', this).each(function(){
var row_data = [];
$('th', this).each(function(){
cell = $(this).text() + div;
cell = cell.substr(0,20);
row_data.push(cell);
});
i = 0;
$('td', this).each(function(){
cell = $(this).text() + div;
cell =  cell.substr(0,20);
row_data.push(cell);
});
table_data.push(row_data + "^Xv");
});
table_data = ""+table_data;
table_data = table_data.replace(/\n/g,"");
table_data = table_data.replace(/\^Xv,/g, "\n");
table_data = table_data.replace(/\^Xv/g, "\n");
table_data = table_data.replace(/,/g,"");
var baltab = table_data.split("\n");
baltab = baltab[0].length;
table_data = table_data.replace(/\n/g,"\n" + divx.substr(0,baltab) + "\n");
table_data = $.trim(table_data) + "\n";
$(this).replaceWith(table_data);
});
}
$("#bin").html("<pre>" + $("#bin").html() + "</pre>");
var w_data = $("#bin").text();
w_data = w_data.replace(/\n\n---------------------------------------------------------------------------------------------\n/g,"\n---------------------------------------------------------------------------------------------\n");
w_data = w_data.replace(/---------------------------------------------------------------------------------------------\n\n/g,"---------------------------------------------------------------------------------------------\n");
$("#bin").empty();
//----------------------------------------------------------------
//----------------------------------------------------------------end write text
}
if (ftype === "html" && ftype_pro === "html"){
//----------------------------------------------------------------write html
//----------------------------------------------------------------
var css_temp = $("#css_temp").val();
var ffam = $("#font-type input.select-in").val();
var fsiz = $("#font-size input.select-in").val();
var fcol = $("#ps-font-color input.select-in").val();
var fbac = $("#ps-bg-color input.select-in").val();
css_temp = css_temp.replace("_ffam_",ffam);
css_temp = css_temp.replace("_fsiz_",fsiz);
css_temp = css_temp.replace(/_fcol_/g,fcol);
css_temp = css_temp.replace(/_fbac_/g,fbac);
var hTop = '<!DOCTYPE html>\n'+
'<html dir="ltr" lang="en-US">\n'+
'<head>\n'+
'<title>'+fool+'</title>\n'+
'<meta charset="UTF-8">\n'+
'<style>\n'+
css_temp + "</style>\n"+
'</head>\n'+
'<body>\n';
var hBot = '</body>\n'+
'</html>';
var gex = $ro.html();
$("#bin").html(gex);
$("#bin *[id]").removeAttr("id");
$("#bin *[class]").removeAttr("class");
$("#bin *[contenteditable]").removeAttr("contenteditable");
gex = $("#bin").html();
$("#bin").empty();
gex = gex.replace(/\n/g,"<br>");
gex = gex.replace(/\n\</g,"<");
gex = gex.replace(/\>\n/g,">");
gex = gex.replace(/\<div/g,"\n<div");
gex = gex.replace(/\<\/div\>/g,"</div>\n");
gex = gex.replace(/\<span/g,"\n<span");
gex = gex.replace(/\<\/span\>/g,"</span>\n");
gex = gex.replace(/\<h1/g,"\n<h1");
gex = gex.replace(/\<\/h1\>/g,"</h1>\n");
gex = gex.replace(/\<ol/g,"\n<ol");
gex = gex.replace(/\<\/ol\>/g,"</ol>\n");
gex = gex.replace(/\<ul/g,"\n<ul");
gex = gex.replace(/\<\/ul\>/g,"</ul>\n");
gex = gex.replace(/\<li/g,"\n<li");
gex = gex.replace(/\<\/li\>/g,"</li>\n");
gex = gex.replace(/\<table/g,"\n<table");
gex = gex.replace(/\<\/table\>/g,"</table>\n");
gex = gex.replace(/\<thead/g,"\n<thead");
gex = gex.replace(/\<\/thead\>/g,"</thead>\n");
gex = gex.replace(/\<tbody/g,"\n<tbody");
gex = gex.replace(/\<\/tbody\>/g,"</tbody>\n");
gex = gex.replace(/\<tr/g,"\n<tr");
gex = gex.replace(/\<\/tr\>/g,"</tr>\n");
gex = gex.replace(/\<th/g,"\n<th");
gex = gex.replace(/\<\/th\>/g,"</th>\n");
gex = gex.replace(/\<td/g,"\n<td");
gex = gex.replace(/\<\/td\>/g,"</td>\n");
gex = gex.replace(/\>\n\n\</g,">\n<");
gex = gex.replace(/\<div\>\<br\>\<\/div\>/g,"<br>");
gex = gex.replace(/\<br\>\<\/div\>/g,"</div>");
gex = gex.replace(/\<br\>\<\/li\>/g,"</li>");
gex = gex.replace(/\<div\>\<hr\>\<\/div\>/g,"<hr>");
var w_data = hTop + gex + hBot;
//----------------------------------------------------------------
//----------------------------------------------------------------end write html
}
var blob = new Blob([w_data], {type: 'text/plain'});
fileWriter.truncate(blob.size);
fileWriter.onwriteend = function() {
fileWriter.onwriteend = function(e) {
/*console.log("Write completed.");*/
};
fileWriter.write(blob);
};
}, errorHandler);
}
//__________________________________________________hotkeys___________________________________________________
shortcut.add("Tab",function() {document.execCommand("insertHTML", false, "&#09;");});
shortcut.add("Shift+Alt+F",function() {
if($("#tog-full-pane").is(":hidden")){
if ($("#tog-full").attr("disabled")){return false}
$("#tog-full").click();
}else{
if ($("#tog-full-pane").attr("disabled")){return false}
$("#tog-full-pane").click();
}
});
shortcut.add("Ctrl+P",function() {$("#open-print").click();});
shortcut.add("Shift+Alt+Left",function() {$("#minify").click();});
shortcut.add("Shift+Alt+Right",function() {$("#minify").click();});
shortcut.add("Shift+Alt+M",function() {$("#tog-list").click();});
shortcut.add("Shift+Alt+N",function() {$("#tog-notepad").click();});
shortcut.add("Alt+0",function() {$("#help").click();});
shortcut.add("Alt+4",function() {$("#donate").click();});
shortcut.add("Alt+p",function() {$("#tog-page-setup").click();});
shortcut.add("Alt+s",function() {$("#speak-play").click();});
shortcut.add("Alt+w",function() {$("#wordcount").click();});
shortcut.add("Alt+B",function() {$("#bold").mousedown();});
shortcut.add("Alt+U",function() {$("#underline").mousedown();});
shortcut.add("Alt+I",function() {$("#italic").mousedown();});
shortcut.add("Alt+o",function() {$("#ol").mousedown();});
shortcut.add("Alt+l",function() {$("#ul").mousedown();});
shortcut.add("Alt+c",function() {$("#center").mousedown();});
shortcut.add("Alt+x",function() {$("#left").mousedown();});
shortcut.add("Alt+f",function() {$("#full").mousedown();});
shortcut.add("Alt+v",function() {$("#right").mousedown();});
shortcut.add("Alt+.",function() {$("#indent").mousedown();});
shortcut.add("Alt+,",function() {$("#outdent").mousedown();});
shortcut.add("Alt+r",function() {$("#rule").mousedown();});
shortcut.add("Alt+h",function() {$("#block-header").mousedown();});
shortcut.add("Alt+t",function() {$("#block-table").mousedown();});
shortcut.add("Alt+d",function() {$("#block-date").mousedown();});
shortcut.add("Ctrl+Z",function() {$("#undo").mousedown();});
shortcut.add("Ctrl+Y",function() {$("#redo").mousedown();});
shortcut.add("Alt+n",function() {$(".hig").mousedown();});
shortcut.add("Alt+z",function() {$("#remove-format").mousedown();});
shortcut.add("Ctrl+O",function() {$("#open").click();});
shortcut.add("Ctrl+N",function() {$("#new").click();});
shortcut.add("Ctrl+S",function() {nipSave();});
shortcut.add("Ctrl+Alt+S",function() {$("#save").click();});
shortcut.add("Ctrl+Alt+A",function() {$("#add").click();});
shortcut.add("Ctrl+Alt+R",function() {$("#rename").click();});
shortcut.add("Ctrl+Alt+D",function() {$("#delete").click();});
shortcut.add("Ctrl+Alt+I",function() {$("#imex").click();});
shortcut.add("Ctrl+Alt+F",function() {$("#search-tog").click();});
//__________________________________________________END___________________________________________________
});//end page ready