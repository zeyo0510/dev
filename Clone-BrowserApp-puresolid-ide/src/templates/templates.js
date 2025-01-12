var templates = {
    chromeApp: {
        language: 'JavaScript',
        type: 'Desktop',
        name: 'Chrome App',
        image: '../../src/templates/res/chromeapp.png',
        description: "A project for ceating Chrome App using JavaScript/HTML/CSS",
        files: {
            "manifest.json": '{\n  "name": "",\n  "description": "My Chrome App.",\n  "version": "1.0.0",\n  "app": {\n    "background": {\n        "scripts": ["background.js"]\n    }\n  }\n }',
            "background.js": 'chrome.app.runtime.onLaunched.addListener(function() {\n  chrome.app.window.create("window.html", {\n    "bounds": {\n        "width": 400,\n        "height": 500\n    }\n    });\n});',
            "window.html":'<!DOCTYPE html>\n<html>\n  <head>\n  </head>\n  <body>\n    <div>Hello, world!</div>\n  </body>\n</html>',
            "styles.css":'body{}',
           
        }


    },
  
    webApplication: {
        language: 'JavaScript',
        type: 'Web',
        name: 'Web Application',
        image: '../../src/templates/res/webapp.png',
        description: "A project for ceating web application using JavaScript",
        files: {
            "Index.html": '<!DOCTYPE html>\n<html xmlns="http://www.w3.org/1999/xhtml">\n<head>\n     <link href="../../Styles/style.css" rel="stylesheet"/>\n     <script src="../../Scripts/main.js"></script>\n    <title>Index</title>\n</head>\n<body>\n\nYour Web Application\n\n</body>\n</html>',
            
            "Styles": {
                "style.css": 'body{\n}'
            },
            "Scripts": {
                "main.js": 'window.addEventListener("load", onLoad, false);\n\n  function onLoad() {\n\nconsole.log("Hello World!")\n\n}'
            }
        }


    },
    phpWebsite: {
        language: 'PHP',
        type: 'Web',
        name: 'PHP Website',
        image: '../../src/templates/res/phpapp.png',
        description: "A project for ceating web sites using PHP and Apache server",
        files: {
            "Index.php": '<!DOCTYPE html>\n<html xmlns="http://www.w3.org/1999/xhtml">\n<head>\n     <link href="../../Styles/style.css" rel="stylesheet"/>\n     <script src="../../Scripts/main.js"></script>\n    <title>Index</title>\n</head>\n<body>\n\n <?php echo "Hello World"; ?> \n\n</body>\n</html>',

            "Styles": {
                "style.css": 'body{\n}'
            },
            "Scripts": {
                "main.js": 'window.addEventListener("load", onLoad, false);\n\n  function onLoad() {\n\nconsole.log("Hello World!")\n\n}'
            }
        }


    }
}