var quickCopy = [
	{ "language": "Generic", "regex": "RGBA: (0-255) @r255, @g255, @b255, @a255 <br>RGBA: (0-1)  @r1, @g1, @b1, @a1 <br> RGB: #@rgbHex <br> RGBA: #@rgbaHex <br> ARGB: #@argbHex <br> CMYK: @c100, @m100, @y100, @k100 <br> HSLA: @h360, @s100, @l100, @a1" },
	{ "language": "CSS", "regex": "rgb(@r255, @g255, @b255) <br> rgba(@r255, @g255, @b255, @a1) <br> hsl(@h360, @s100, @l100) <br> hsla(@h360, @s100, @l100, @a1) <br> #@rgbHex" },
	{ "language": "Unity C#", "regex": "new Color(@r1F, @g1F, @b1F) <br> new Color(@r1F, @g1F, @b1F, @a1F)" },
	{ "language": "Unity Script", "regex": "Color(@r1, @g1, @b1) <br> Color(@r1, @g1, @b1, @a1)" },
	{ "language": "Objective-C", "regex": "[UIColor colorWithRed:@r1f / green:@g1f / blue:@b1f / alpha:@a1f]" }
];

var presets = [
	{
		"titre" 	: "Android",
		"icon" 		: "&#xf17b;",
		"lien" 		: "http://developer.android.com/design/style/color.html",
		"author"	: "ColorForDeveloppers",
		"colors"	: [
			{"name": "", "hex": "33b5e5"},
			{"name": "", "hex": "0099cc"},
			{"name": "", "hex": "99cc00"},
			{"name": "", "hex": "669900"},
			{"name": "", "hex": "ff4444"},
			{"name": "", "hex": "cc0000"},
			{"name": "", "hex": "ffbb33"},
			{"name": "", "hex": "ff8800"},
			{"name": "", "hex": "aa66cc"},
			{"name": "", "hex": "9933cc"}
		]
	},
	{
		"titre" 	: "Material",
		"icon" 		: "&#xf135;",
		"lien" 		: "http://www.google.com/design/spec/style/color.html#color-color-palette",
		"author"	: "ColorForDeveloppers",
		"colors"	: [
			{"name": "", "hex": "f44336"},
			{"name": "", "hex": "e91e63"},
			{"name": "", "hex": "9c27b0"},
			{"name": "", "hex": "673ab7"},
			{"name": "", "hex": "3f51b5"},
			{"name": "", "hex": "2196f3"},
			{"name": "", "hex": "03a9f4"},
			{"name": "", "hex": "00bcd4"},
			{"name": "", "hex": "009688"},
			{"name": "", "hex": "4caf50"},
			{"name": "", "hex": "8bc34a"},
			{"name": "", "hex": "cddc39"},
			{"name": "", "hex": "ffeb3b"},
			{"name": "", "hex": "ffc107"},
			{"name": "", "hex": "ff9800"},
			{"name": "", "hex": "ff5722"},
			{"name": "", "hex": "9e9e9e"},
			{"name": "", "hex": "607d8b"},
			{"name": "", "hex": "795548"}
		]
	},
	{
		"titre" 	: "iOS 7",
		"icon" 		: "&#xf179;",
		"lien" 		: "http://ios7colors.com/",
		"author"	: "ColorForDeveloppers",
		"colors"	: [
			{"name": "", "hex": "5856d6"},
			{"name": "", "hex": "007aff"},
			{"name": "", "hex": "34aadc"},
			{"name": "", "hex": "5ac8fa"},
			{"name": "", "hex": "4cd964"},
			{"name": "", "hex": "ff2d55"},
			{"name": "", "hex": "ff3b30"},
			{"name": "", "hex": "ff9500"},
			{"name": "", "hex": "ffcc00"},
			{"name": "", "hex": "8e8e93"}
		]
	},
	{
		"titre" 	: "Windows Metro",
		"icon" 		: "&#xf17a;",
		"lien" 		: "https://onedrive.live.com/view.aspx?resid=40CFFDE85F1AB56A!1284",
		"author"	: "ColorForDeveloppers",
		"colors"	: [
			{"name": "purple", "hex": "ff0097"},
			{"name": "magenta", "hex": "a200ff"},
			{"name": "teal", "hex": "00aba9"},
			{"name": "lime", "hex": "8cbf26"},
			{"name": "brown", "hex": "a05000"},
			{"name": "pink", "hex": "e671b8"},
			{"name": "orange", "hex": "f09609"},
			{"name": "blue", "hex": "1ba1e2"},
			{"name": "red", "hex": "e51400"},
			{"name": "green", "hex": "339933"}
		]
	},
	{
		"titre" 	: "Web Standard",
		"icon" 		: "&#xf0ac;",
		"lien" 		: "",
		"author"	: "ColorForDeveloppers",
		"colors"	: [
			{"name": "black", "hex": "000000"},
			{"name": "white", "hex": "ffffff"},
			{"name": "red", "hex": "ff0000"},
			{"name": "lime", "hex": "00ff00"},
			{"name": "blue", "hex": "0000ff"},
			{"name": "yellow", "hex": "ffff00"},
			{"name": "cyan/aqua", "hex": "00ffff"},
			{"name": "magenta", "hex": "ff00ff"},
			{"name": "silver", "hex": "c0c0c0"},
			{"name": "grey", "hex": "808080"},
			{"name": "maroon", "hex": "800000"},
			{"name": "olive", "hex": "808000"},
			{"name": "green", "hex": "008000"},
			{"name": "purple", "hex": "800080"},
			{"name": "teal", "hex": "008080"},
			{"name": "navy", "hex": "000080"}
		]
	}
];

var icons = [
	{ "name": "rocket", "code": "135" },
	//fa 4.5{ "name": "shopping bag", "code": "290" },
	//fa 4.5{ "name": "shopping basket", "code": "291" },
	{ "name": "birthday cake", "code": "1fd" },
	{ "name": "camera", "code": "030" },
	//fa 4.3{ "name": "diamond", "code": "219" },
	{ "name": "home", "code": "015" },
	{ "name": "life ring", "code": "1cd" },
	{ "name": "leaf", "code": "06c" },
	{ "name": "globe", "code": "0ac" },
	{ "name": "gift", "code": "06b" },
	{ "name": "music", "code": "001" },
	{ "name": "paper plane", "code": "1d8" },
	{ "name": "magic wand", "code": "0d0" },
	{ "name": "star", "code": "005" },
	{ "name": "trophy", "code": "091" },
	{ "name": "tree", "code": "1bb" },
	{ "name": "suitcase", "code": "0f2" },
	{ "name": "warning", "code": "071" }/*,
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" },
	{ "name": "rocket", "code": "135" }*/
];