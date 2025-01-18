
/***
*  This file is for adding examples to blocks' context menues
***/


//This is the only thing that needs to be edited.  Examples are automatically added to the context menus for blocks that are used in these examples
var exampleDB = [
  {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">example</field><statement name="STACK"><block type="perform_command"><value name="COMMAND"><block type="text"><field name="TEXT">summon Cow</field></block></value><value name="PLAYER"><block type="me"></block></value><next><block type="perform_command"><value name="COMMAND"><block type="text"><field name="TEXT">summon Cow</field></block></value><value name="PLAYER"><block type="me"></block></value></block></next></block></statement></block></xml>'},
  {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">example</field><statement name="STACK"><block type="variables_set"><field name="VAR">entity</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">PIG</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">entity2</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">CREEPER</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">nearby</field><value name="VALUE"><block type="entitieswithinrange"><value name="NAME"><block type="math_number"><field name="NUM">10</field></block></value><value name="entity"><block type="me"></block></value></block></value><next><block type="send"><value name="MESSAGE"><block type="text_join"><mutation items="2"></mutation><value name="ADD0"><block type="text"><field name="TEXT">Entities near by: </field></block></value><value name="ADD1"><block type="lists_length"><value name="VALUE"><block type="variables_get"><field name="VAR">nearby</field></block></value></block></value></block></value><value name="PLAYER"><block type="me"></block></value></block></next></block></next></block></next></block></statement></block></xml>'},
   {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">main</field><statement name="STACK"><block type="variables_set"><field name="VAR">x</field><value name="VALUE"><block type="math_number"><field name="NUM">0</field></block></value><next><block type="interval"><value name="FUNCTION"><block type="anon_func"><statement name="CODE"><block type="variables_set"><field name="VAR">x</field><value name="VALUE"><block type="math_arithmetic"><field name="OP">ADD</field><value name="A"><block type="variables_get"><field name="VAR">x</field></block></value><value name="B"><block type="math_number"><field name="NUM">1</field></block></value></block></value><next><block type="send"><value name="MESSAGE"><block type="variables_get"><field name="VAR">x</field></block></value><value name="PLAYER"><block type="me"></block></value></block></next></block></statement></block></value><value name="MILLIS"><block type="math_number"><field name="NUM">1000</field></block></value></block></next></block></statement></block></xml>'},
   {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">main</field><statement name="STACK"><block type="variables_set"><field name="VAR">entity</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">PIG</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">entity2</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">CREEPER</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="interval"><value name="FUNCTION"><block type="anon_func"><statement name="CODE"><block type="variables_set"><field name="VAR">nearby</field><value name="VALUE"><block type="entitieswithinrange"><value name="NAME"><block type="math_number"><field name="NUM">10</field></block></value><value name="entity"><block type="me"></block></value></block></value><next><block type="controls_forEach"><field name="VAR">i</field><value name="LIST"><block type="variables_get"><field name="VAR">nearby</field></block></value><statement name="DO"><block type="controls_if"><value name="IF0"><block type="logic_compare"><field name="OP">EQ</field><value name="A"><block type="typeof"><value name="NAME"><block type="variables_get"><field name="VAR">i</field></block></value></block></value><value name="B"><block type="entity_type"><field name="TYPE">CREEPER</field></block></value></block></value><statement name="DO0"><block type="send"><value name="MESSAGE"><block type="text"><field name="TEXT">There is a creeper nearby!  Run!</field></block></value><value name="PLAYER"><block type="me"></block></value></block></statement></block></statement></block></next></block></statement></block></value><value name="MILLIS"><block type="math_number"><field name="NUM">1000</field></block></value></block></next></block></next></block></statement></block></xml>'},
   {text: '<xml xmlns="http://www.w3.org/1999/xhtml"><block type="procedures_defnoreturn" x="99" y="27"><field name="NAME">main</field><statement name="STACK"><block type="variables_set"><field name="VAR">entity</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">PIG</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">d</field><value name="VALUE"><block type="new_drone"></block></value><next><block type="interval"><value name="FUNCTION"><block type="anon_func"><statement name="CODE"><block type="variables_set"><field name="VAR">new_location</field><value name="VALUE"><block type="get_location"><value name="THING"><block type="variables_get"><field name="VAR">d</field></block></value></block></value><next><block type="variables_set"><field name="VAR">old_y</field><value name="VALUE"><block type="accessjso"><value name="NAME"><block type="text"><field name="TEXT">y</field></block></value><value name="JSO"><block type="variables_get"><field name="VAR">new_location</field></block></value></block></value><next><block type="setjso"><value name="value"><block type="math_arithmetic"><field name="OP">ADD</field><value name="A"><block type="variables_get"><field name="VAR">old_y</field></block></value><value name="B"><block type="math_number"><field name="NUM">5</field></block></value></block></value><value name="NAME"><block type="text"><field name="TEXT">y</field></block></value><value name="JSO"><block type="variables_get"><field name="VAR">new_location</field></block></value><next><block type="set_location"><value name="ENTITY"><block type="variables_get"><field name="VAR">entity</field></block></value><value name="LOCATION"><block type="variables_get"><field name="VAR">new_location</field></block></value></block></next></block></next></block></next></block></statement></block></value><value name="MILLIS"><block type="math_number"><field name="NUM">1000</field></block></value></block></next></block></next></block></statement></block></xml>'},
   {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">main</field><statement name="STACK"><block type="variables_set"><field name="VAR">entity</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">PIG</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="interval"><value name="FUNCTION"><block type="anon_func"><statement name="CODE"><block type="rotate_player"><value name="player"><block type="variables_get"><field name="VAR">entity</field></block></value><value name="degrees"><block type="math_number"><field name="NUM">180</field></block></value></block></statement></block></value><value name="MILLIS"><block type="math_number"><field name="NUM">500</field></block></value></block></next></block></statement></block></xml>'},
   {text: '<xml><block xmlns="http://www.w3.org/1999/xhtml" type="procedures_defnoreturn"><field name="NAME">main</field><statement name="STACK"><block type="variables_set"><field name="VAR">entity</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">PIG</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">entity2</field><value name="VALUE"><block type="spawn_entity"><value name="ENTITY"><block type="entity_type"><field name="TYPE">CREEPER</field></block></value><value name="LOCATION"><block type="get_location"><value name="THING"><block type="me"></block></value></block></value></block></value><next><block type="variables_set"><field name="VAR">nearby</field><value name="VALUE"><block type="entitieswithinrange_mod"><value name="type"><block type="entity_type"><field name="TYPE">CREEPER</field></block></value><value name="range"><block type="math_number"><field name="NUM">10</field></block></value><value name="entity"><block type="me"></block></value></block></value><next><block type="send"><value name="MESSAGE"><block type="text_join"><mutation items="2"></mutation><value name="ADD0"><block type="text"><field name="TEXT">Creepers near by: </field></block></value><value name="ADD1"><block type="lists_length"><value name="VALUE"><block type="variables_get"><field name="VAR">nearby</field></block></value></block></value></block></value><value name="PLAYER"><block type="me"></block></value></block></next></block></next></block></next></block></statement></block></xml>'}


]





var createExamples = function(options) {
    var block = this
    var count = 0
    for(var i in exampleDB){
      var example = exampleDB[i]
      if(!example.text.match(block.type))
        continue

      count++
      var option = {enabled: true};
      option.text = "See Example #" + (count)

      var xml = example.text
      showExample(xml,option,options, block)
    }
}

var showExample = function(xml, option, options, block){
  option.callback = function(){
    var workspace = block.workspace
    var dom = Blockly.Xml.textToDom(xml).childNodes[0]
    var newBlock = Blockly.Xml.domToBlock(workspace,dom)
    var xy = block.getRelativeToSurfaceXY();
    xy.x += Blockly.SNAP_RADIUS;
    xy.y += Blockly.SNAP_RADIUS * 2;
    newBlock.moveBy(xy.x, xy.y);
  }
  options.push(option);
}




//Searches through the examples above and adds them as context menu items to any blocks that are involved in the examples.
for(var i in exampleDB){
  var example = exampleDB[i].text

  var matches = example.match(/type="(.*?)"/g).map(function(m){
    return m.match(/type="(.*?)"/)[1]
  })

  for(var j in matches){
    var b = Blockly.Blocks[matches[j]]
    if(b && !b.customContextMenu)
      b.customContextMenu = createExamples
  }
}

;
