var WebDeveloper = WebDeveloper || {}; // eslint-disable-line no-redeclare, no-use-before-define

WebDeveloper.Common                = WebDeveloper.Common || {};
WebDeveloper.Common.requestTimeout = 10000;

// Adds a class to an element
WebDeveloper.Common.addClass = function(element, className)
{
  // If the element and class name are set and the element does not already have this class
  if(element && className && !WebDeveloper.Common.hasClass(element, className))
  {
    // If the classes are on an SVG
    if(element.className instanceof SVGAnimatedString)
    {
      element.className.baseVal = (element.className.baseVal + " " + className).trim();
    }
    else
    {
      element.className = (element.className + " " + className).trim();
    }
  }
};

// Adjusts the position of the given element
WebDeveloper.Common.adjustElementPosition = function(element, xPosition, yPosition, offset)
{
  // If the element is set
  if(element)
  {
    var contentWindow = WebDeveloper.Common.getContentWindow();
    var innerHeight   = contentWindow.innerHeight;
    var innerWidth    = contentWindow.innerWidth;
    var offsetHeight  = element.offsetHeight;
    var offsetWidth   = element.offsetWidth;
    var offsetX       = contentWindow.pageXOffset;
    var offsetY       = contentWindow.pageYOffset;

    // If the x position is less than 0
    if(xPosition < 0)
    {
      xPosition = 0;
    }

    // If the y position is less than 0
    if(yPosition < 0)
    {
      yPosition = 0;
    }

    // If the element will fit at the x position
    if(xPosition + offsetWidth + offset + 5 < innerWidth + offsetX)
    {
      element.style.left = xPosition + offset + "px";
    }
    else
    {
      element.style.left = innerWidth + offsetX - offsetWidth - offset + "px";
    }

    // If the element will fit at the y position
    if(yPosition + offsetHeight + offset + 5 < innerHeight + offsetY)
    {
      element.style.top = yPosition + offset + "px";
    }
    else
    {
      element.style.top = innerHeight + offsetY - offsetHeight - offset + "px";
    }
  }
};

// Adjusts the position of the given element
WebDeveloper.Common.appendHTML = function(html, element)
{
  // If the HTML and element are set
  if(html && element)
  {
    element.insertAdjacentHTML("beforeend", html);
  }
};

// Returns true if the array contains the element
WebDeveloper.Common.contains = function(array, element)
{
  // If the array and element are set
  if(array && element)
  {
    try
    {
      // If the element does not exist in the array
      if(array.indexOf(element) == -1)
      {
        return false;
      }

      return true;
    }
    catch(exception)
    {
      // Loop through the array
      for(var i = 0, l = array.length; i < l; i++)
      {
        // If the element is found
        if(array[i] == element)
        {
          return true;
        }
      }
    }
  }

  return false;
};

// Removes all child elements from an element
WebDeveloper.Common.empty = function(element)
{
  // If the element is set
  if(element)
  {
    var childElements = element.childNodes;

    // Loop through the child elements
    while(childElements.length)
    {
      element.removeChild(childElements[0]);
    }
  }
};

// Returns true if a string ends with another string
WebDeveloper.Common.endsWith = function(string, endsWith)
{
  return new RegExp(endsWith + "$").test(string);
};

// Formats dimensions
WebDeveloper.Common.formatDimensions = function(width, height, locale)
{
  // If the width and height are set
  if(width && height)
  {
    return locale.width + " = " + width + "px " + locale.height + " = " + height + "px";
  }
  else if(width)
  {
    return locale.width + " = " + width + "px";
  }
  else if(height)
  {
    return locale.height + " = " + height + "px";
  }

  return "";
};

// Returns a chrome URL
WebDeveloper.Common.getChromeURL = function(url)
{
  return chrome.runtime.getURL(url);
};

// Returns the current content document
WebDeveloper.Common.getContentDocument = function()
{
  return document;
};

// Returns the current content window
WebDeveloper.Common.getContentWindow = function()
{
  return window;
};

// Returns a CSS primitive value
WebDeveloper.Common.getCSSPrimitiveValue = function(type)
{
  var cssPrimitiveValueExists = false;

  // Try to access the CSS primitive value
  try
  {
    // If the CSS primitive value exists
    if(CSSPrimitiveValue)
    {
      cssPrimitiveValueExists = true;
    }
  }
  catch(exception)
  {
    // Ignore
  }

  // Switch on the style property
  switch(type)
  {
    case "IDENT":
      return cssPrimitiveValueExists ? CSSPrimitiveValue.CSS_IDENT : 21;
    case "NUMBER":
      return cssPrimitiveValueExists ? CSSPrimitiveValue.CSS_NUMBER : 1;
    case "RGBCOLOR":
      return cssPrimitiveValueExists ? CSSPrimitiveValue.CSS_RGBCOLOR : 25;
    case "URI":
      return cssPrimitiveValueExists ? CSSPrimitiveValue.CSS_URI : 20;
    default:
      return null;
  }
};

// Returns a CSS property
WebDeveloper.Common.getCSSProperty = function(property)
{
  return property;
};

// Returns the CSS text from a property
WebDeveloper.Common.getCSSText = function(property)
{
  // If the property is set
  if(property)
  {
    // If the property has CSS text
    if(property.cssText)
    {
      return property.cssText;
    }

    return property;
  }

  return null;
};

// Returns the CSS URI from a property
WebDeveloper.Common.getCSSURI = function(property)
{
  // If the property is set
  if(property)
  {
    // If the property has a primitive type
    if(property.primitiveType)
    {
      return property.getStringValue();
    }
    else
    { // eslint-disable-line no-else-return
      var urlRegularExpression = /(?:\(['|"]?)(.*?)(?:['|"]?\))/;
      var uri                  = urlRegularExpression.exec(property);

      // If the uri was found
      if(uri)
      {
        return uri[1];
      }
    }
  }

  return null;
};

// Returns the document body element
WebDeveloper.Common.getDocumentBodyElement = function(contentDocument)
{
  // If there is a body element
  if(contentDocument.body)
  {
    return contentDocument.body;
  }
  else
  { // eslint-disable-line no-else-return
    var bodyElement = contentDocument.querySelector("body");

    // If there is a body element
    if(bodyElement)
    {
      return bodyElement;
    }
  }

  return contentDocument.documentElement;
};

// Returns the document head element
WebDeveloper.Common.getDocumentHeadElement = function(contentDocument)
{
  var headElement = contentDocument.querySelector("head");

  // If there is a head element
  if(headElement)
  {
    return headElement;
  }

  return contentDocument.documentElement;
};

// Returns all of the images in the document
WebDeveloper.Common.getDocumentImages = function(contentDocument)
{
  var uniqueImages = [];

  // If the content document is set
  if(contentDocument)
  {
    var computedStyle = null;
    var image         = null;
    var images        = [];
    var node          = null;
    var styleImage    = null;
    var treeWalker    = contentDocument.createTreeWalker(contentDocument, NodeFilter.SHOW_ELEMENT, null, false);

    // While the tree walker has more nodes
    while((node = treeWalker.nextNode()) !== null)
    {
      // If this is an image element
      if(node.tagName.toLowerCase() == "img")
      {
        images.push(node);
      }
      else if(node.tagName.toLowerCase() == "input" && node.src && node.type && node.type.toLowerCase() == "image")
      {
        image     = new Image();
        image.src = node.src;

        // If this is not a chrome image
        if(image.src.indexOf("chrome://") !== 0)
        {
          images.push(image);
        }
      }
      else if(node.tagName.toLowerCase() == "link" && node.href && node.href.indexOf("chrome://") !== 0 && node.rel && node.rel.indexOf("icon") != -1)
      {
        image     = new Image();
        image.src = node.href;

        images.push(image);
      }
      else
      {
        // Try to get the computed styles
        try
        {
          computedStyle = node.ownerDocument.defaultView.getComputedStyle(node, null);
        }
        catch(exception)
        {
          // Ignore
        }

        // If the computed style is set
        if(computedStyle)
        {
          styleImage = WebDeveloper.Common.getCSSProperty(WebDeveloper.Common.getPropertyCSSValue(computedStyle, "background-image"));

          // If this element has a background image and it is a URI
          if(WebDeveloper.Common.isCSSURI(styleImage))
          {
            image     = new Image();
            image.src = WebDeveloper.Common.getCSSURI(styleImage);

            // If this is not a chrome image
            if(image.src.indexOf("chrome://") !== 0)
            {
              images.push(image);
            }
          }

          styleImage = WebDeveloper.Common.getPropertyCSSValue(computedStyle, "list-style-image");

          // If this element has a list style image and it is a URI
          if(WebDeveloper.Common.isCSSURI(styleImage))
          {
            image     = new Image();
            image.src = WebDeveloper.Common.getCSSURI(styleImage);

            // If this is not a chrome image
            if(image.src.indexOf("chrome://") !== 0)
            {
              images.push(image);
            }
          }
        }
      }
    }

    images.sort(WebDeveloper.Common.sortImages);

    // Loop through the images
    for(var i = 0, l = images.length; i < l; i++)
    {
      image = images[i];

      // If this is not the last image and the image is the same as the next image
      if(i + 1 < l && image.src == images[i + 1].src)
      {
        continue;
      }

      uniqueImages.push(image);
    }
  }

  return uniqueImages;
};

// Get the position of an element
WebDeveloper.Common.getElementPosition = function(element, xPosition)
{
  var position = 0;

  // If the element is set
  if(element)
  {
    var elementOffsetParent = element.offsetParent;

    // If the element has an offset parent
    if(elementOffsetParent)
    {
      // While there is an offset parent
      while((elementOffsetParent = element.offsetParent) !== null)
      {
        // If getting the x position
        if(xPosition)
        {
          position += element.offsetLeft;
        }
        else
        {
          position += element.offsetTop;
        }

        element = elementOffsetParent;
      }
    }
    else if(xPosition)
    {
      position = element.offsetLeft;
    }
    else
    {
      position = element.offsetTop;
    }
  }

  return position;
};

// Get the x position of an element
WebDeveloper.Common.getElementPositionX = function(element)
{
  return WebDeveloper.Common.getElementPosition(element, true);
};

// Get the y position of an element
WebDeveloper.Common.getElementPositionY = function(element)
{
  return WebDeveloper.Common.getElementPosition(element, false);
};

// Returns the text from an element
WebDeveloper.Common.getElementText = function(element)
{
  var elementText = "";

  // If the element is set
  if(element)
  {
    var childNode     = null;
    var childNodes    = element.childNodes;
    var childNodeType = null;

    // Loop through the child nodes
    for(var i = 0, l = childNodes.length; i < l; i++)
    {
      childNode     = childNodes[i];
      childNodeType = childNode.nodeType;

      // If the child node type is an element
      if(childNodeType == Node.ELEMENT_NODE)
      {
        elementText += WebDeveloper.Common.getElementText(childNode);
      }
      else if(childNodeType == Node.TEXT_NODE)
      {
        elementText += childNode.nodeValue + " ";
      }
    }
  }

  return elementText;
};

// Returns the next siblings of an element with an optional match
WebDeveloper.Common.getNextSiblings = function(element, match)
{
  var nextSiblings = [];

  // If the element is set
  if(element)
  {
    var childNode     = null;
    var childNodes    = element.parentNode.childNodes;
    var childNodeType = null;
    var elementFound  = false;

    // Loop through the child nodes
    for(var i = 0, l = childNodes.length; i < l; i++)
    {
      childNode = childNodes[i];

      // If the element was found
      if(elementFound)
      {
        childNodeType = childNode.nodeType;

        // If the child node type is an element
        if(childNodeType == Node.ELEMENT_NODE && childNode != element)
        {
          // If a match is set
          if(match)
          {
            // If the child node matches
            if(childNode.matches(match))
            {
              nextSiblings.push(childNode);
            }
          }
          else
          {
            nextSiblings.push(childNode);
          }
        }
      }
      else if(childNode == element)
      {
        elementFound = true;
      }
    }
  }

  // If no next siblings were found
  if(nextSiblings.length === 0)
  {
    nextSiblings = null;
  }

  return nextSiblings;
};

// Returns the number of occurrences of a substring in a string
WebDeveloper.Common.getOccurrenceCount = function(string, substring)
{
  var count = 0;

  // If the string and substring are set
  if(string && substring)
  {
    var position = 0;
    var shift    = substring.length;

    // While the substring was found
    while(position != -1)
    {
      position = string.indexOf(substring, position);

      // If the substring was found
      if(position != -1)
      {
        position += shift;

        count++;
      }
    }
  }

  return count;
};

// Returns the previous siblings of an element with an optional match
WebDeveloper.Common.getPreviousSiblings = function(element, match)
{
  var previousSiblings = [];

  // If the element is set
  if(element)
  {
    var childNode     = null;
    var childNodes    = element.parentNode.childNodes;
    var childNodeType = null;

    // Loop through the child nodes
    for(var i = 0, l = childNodes.length; i < l; i++)
    {
      childNode = childNodes[i];

      // If this is the element
      if(childNode == element)
      {
        break;
      }
      else
      {
        childNodeType = childNode.nodeType;

        // If the child node type is an element
        if(childNodeType == Node.ELEMENT_NODE)
        {
          // If a match is set
          if(match)
          {
            // If the child node matches
            if(childNode.matches(match))
            {
              previousSiblings.push(childNode);
            }
          }
          else
          {
            previousSiblings.push(childNode);
          }
        }
      }
    }
  }

  // If no next siblings were found
  if(previousSiblings.length === 0)
  {
    previousSiblings = null;
  }

  return previousSiblings;
};

// Gets the property CSS value for a computed style
WebDeveloper.Common.getPropertyCSSValue = function(computedStyle, property)
{
  var cssProperty = null;

  // If the computed style is set
  if(computedStyle)
  {
    // Try to get the computed style (fails in newer versions of Chrome)
    try
    {
      cssProperty = computedStyle.getPropertyCSSValue(property);
    }
    catch(exception)
    {
      cssProperty = computedStyle.getPropertyValue(property);
    }
  }

  return cssProperty;
};

// Gets the content from a URL
WebDeveloper.Common.getURLContent = function(urlContentRequest, errorMessage, configuration)
{
  var url = urlContentRequest.url;

  // If the URL is not entirely generated
  if(url.indexOf("wyciwyg://") !== 0)
  {
    // Try to download the file
    try
    {
      var request = new XMLHttpRequest();

      request.timeout = WebDeveloper.Common.requestTimeout;

      request.onreadystatechange = function()
      {
        // If the request completed
        if(request.readyState == 4)
        {
          WebDeveloper.Common.urlContentRequestComplete(request.responseText, urlContentRequest, configuration);
        }
      };

      request.ontimeout = function()
      {
        WebDeveloper.Common.urlContentRequestComplete(errorMessage, urlContentRequest, configuration);
      };

      request.open("get", url);
      request.send(null);
    }
    catch(exception)
    {
      WebDeveloper.Common.urlContentRequestComplete(errorMessage, urlContentRequest, configuration);
    }
  }
};

// Returns the contents of the given URLs
WebDeveloper.Common.getURLContents = function(urlContentRequests, errorMessage, callback)
{
  var urlContentRequestsRemaining = urlContentRequests.length;
  var configuration               = { callback: callback, urlContentRequestsRemaining: urlContentRequestsRemaining };

  // Loop through the URL content requests
  for(var i = 0, l = urlContentRequests.length; i < l; i++)
  {
    WebDeveloper.Common.getURLContent(urlContentRequests[i], errorMessage, configuration);
  }
};

// Returns true if an element has the specified class
WebDeveloper.Common.hasClass = function(element, className)
{
  // If the element and class name are set
  if(element && className)
  {
    var classes = element.className;

    // If the classes are on an SVG
    if(classes instanceof SVGAnimatedString)
    {
      classes = classes.baseVal;
    }

    classes = classes.split(" ");

    // Loop through the classes
    for(var i = 0, l = classes.length; i < l; i++)
    {
      // If the classes match
      if(className == classes[i])
      {
        return true;
      }
    }
  }

  return false;
};

// Returns true if the item is in the array
WebDeveloper.Common.inArray = function(item, array)
{
  return WebDeveloper.Common.positionInArray(item, array) != -1;
};

// Includes JavaScript in a document
WebDeveloper.Common.includeJavaScript = function(url, contentDocument, callback)
{
  var scriptElement = contentDocument.createElement("script");

  // If a callback is set
  if(callback)
  {
    var load = (function(callbackFunction)
    {
      var handler = function()
      {
        callbackFunction();

        scriptElement.removeEventListener("load", handler, true);
      };

      return handler;
    })(callback);

    scriptElement.addEventListener("load", load, true);
  }

  scriptElement.setAttribute("src", WebDeveloper.Common.getChromeURL(url));
  WebDeveloper.Common.getDocumentBodyElement(contentDocument).appendChild(scriptElement);
};

// Inserts the given child after the element
WebDeveloper.Common.insertAfter = function(child, after)
{
  // If the child and after are set
  if(child && after)
  {
    var nextSibling = after.nextSibling;
    var parent      = after.parentNode;

    // If the element has a next sibling
    if(nextSibling)
    {
      parent.insertBefore(child, nextSibling);
    }
    else
    {
      parent.appendChild(child);
    }
  }
};

// Inserts the given element as the first child of the element
WebDeveloper.Common.insertAsFirstChild = function(element, child)
{
  // If the element and child are set
  if(element && child)
  {
    // If the element has child nodes
    if(element.hasChildNodes())
    {
      element.insertBefore(child, element.firstChild);
    }
    else
    {
      element.appendChild(child);
    }
  }
};

// Returns true if the ancestor element is an ancestor of the element
WebDeveloper.Common.isAncestor = function(element, ancestorElement)
{
  // If the element and ancestor element are set
  if(element && ancestorElement)
  {
    var parentElement = null;

    // Loop through the parent elements
    while((parentElement = element.parentNode) !== null)
    {
      // If the parent element is the ancestor element
      if(parentElement == ancestorElement)
      {
        return true;
      }

      element = parentElement;
    }
  }

  return false;
};

// Returns true if this CSS property is a URI
WebDeveloper.Common.isCSSURI = function(property)
{
  // If the property is set
  if(property)
  {
    // If the property has a primitive type
    if(property.primitiveType)
    {
      // If the property primitive type is a URI
      if(property.primitiveType == WebDeveloper.Common.getCSSPrimitiveValue("URI"))
      {
        return true;
      }
    }
    else
    {
      var urlRegularExpression = /(?:\(['|"]?)(.*?)(?:['|"]?\))/;
      var uri                  = urlRegularExpression.exec(property);

      // If the uri was found
      if(uri)
      {
        return true;
      }
    }
  }

  return false;
};

// Logs a message
WebDeveloper.Common.log = function(message, exception)
{
  var consoleMessage = "WEB DEVELOPER LOG: " + message;

  // If an exception is set
  if(exception)
  {
    console.warn(consoleMessage, exception); // eslint-disable-line no-console
  }
  else
  {
    console.warn(consoleMessage); // eslint-disable-line no-console
  }
};

// Returns the position if the item is in the array or -1 if it is not
WebDeveloper.Common.positionInArray = function(item, array)
{
  // If the array is set
  if(array)
  {
    // Loop through the array
    for(var i = 0, l = array.length; i < l; i++)
    {
      // If the item is in the array
      if(array[i] == item)
      {
        return i;
      }
    }
  }

  return -1;
};

// Removes a class from an element
WebDeveloper.Common.removeClass = function(element, className)
{
  // If the element and class name are set
  if(element && className)
  {
    var classes = element.className;

    // If the classes are on an SVG
    if(classes instanceof SVGAnimatedString)
    {
      classes = classes.baseVal;
    }

    classes = classes.split(" ");

    // Loop through the classes
    for(var i = 0, l = classes.length; i < l; i++)
    {
      // If the classes match
      if(className == classes[i])
      {
        classes.splice(i, 1);

        // If the classes are on an SVG
        if(element.className instanceof SVGAnimatedString)
        {
          element.className.baseVal = classes.join(" ").trim();
        }
        else
        {
          element.className = classes.join(" ").trim();
        }

        break;
      }
    }
  }
};

// Removes all matching elements from a document
WebDeveloper.Common.removeMatchingElements = function(selector, contentDocument)
{
  var matchingElement  = null;
  var matchingElements = contentDocument.querySelectorAll(selector);

  // Loop through the matching elements
  for(var i = 0, l = matchingElements.length; i < l; i++)
  {
    matchingElement = matchingElements[i];

    // If the matching element has a parent node
    if(matchingElement.parentNode)
    {
      matchingElement.parentNode.removeChild(matchingElement);
    }
  }
};

// Removes the reload parameter from a URL
WebDeveloper.Common.removeReloadParameterFromURL = function(url)
{
  // If the URL is set
  if(url)
  {
    return url.replace(/(&|\?)web-developer-reload=\d+/, "");
  }

  return null;
};

// Removes a substring from a string
WebDeveloper.Common.removeSubstring = function(string, substring)
{
  // If the string and substring are not empty
  if(string && substring)
  {
    var substringStart = string.indexOf(substring);

    // If the substring is found in the string
    if(substring && substringStart != -1)
    {
      return string.substring(0, substringStart) + string.substring(substringStart + substring.length, string.length);
    }

    return string;
  }

  return "";
};

// Sorts two images
WebDeveloper.Common.sortImages = function(imageOne, imageTwo)
{
  // If both images are set
  if(imageOne && imageTwo)
  {
    var imageOneSrc = imageOne.src;
    var imageTwoSrc = imageTwo.src;

    // If the images are equal
    if(imageOneSrc == imageTwoSrc)
    {
      return 0;
    }
    else if(imageOneSrc < imageTwoSrc)
    {
      return -1;
    }
  }

  return 1;
};

// Toggles a class on an element
WebDeveloper.Common.toggleClass = function(element, className, value)
{
  // If the value is set
  if(value)
  {
    WebDeveloper.Common.addClass(element, className);
  }
  else
  {
    WebDeveloper.Common.removeClass(element, className);
  }
};

// Toggles a style sheet in a document
WebDeveloper.Common.toggleStyleSheet = function(url, id, contentDocument, insertFirst)
{
  var styleSheet = contentDocument.getElementById(id);

  // If the style sheet is already in the document
  if(styleSheet)
  {
    WebDeveloper.Common.removeMatchingElements("#" + id, contentDocument);
  }
  else
  {
    var headElement = WebDeveloper.Common.getDocumentHeadElement(contentDocument);
    var firstChild  = headElement.firstChild;
    var linkElement = contentDocument.createElement("link");

    linkElement.setAttribute("href", WebDeveloper.Common.getChromeURL(url));
    linkElement.setAttribute("id", id);
    linkElement.setAttribute("rel", "stylesheet");

    // If there is a first child
    if(insertFirst && firstChild)
    {
      headElement.insertBefore(linkElement, firstChild);
    }
    else
    {
      headElement.appendChild(linkElement);
    }
  }
};

// Handles the completion of a URL content request
WebDeveloper.Common.urlContentRequestComplete = function(content, urlContentRequest, configuration)
{
  urlContentRequest.content = content;

  configuration.urlContentRequestsRemaining--;

  // If there are no URL content requests remaining
  if(configuration.urlContentRequestsRemaining === 0)
  {
    configuration.callback();
  }
};

var WebDeveloper = WebDeveloper || {}; // eslint-disable-line no-redeclare, no-use-before-define

WebDeveloper.Common = WebDeveloper.Common || {};

// Displays a notification
WebDeveloper.Common.displayNotification = function(message, parameters)
{
  chrome.runtime.sendMessage({ message: message, parameters: parameters, type: "display-notification" });
};

var WebDeveloper = WebDeveloper || {}; // eslint-disable-line no-redeclare, no-use-before-define

WebDeveloper.Outline = WebDeveloper.Outline || {};

// Outlines all block level elements
WebDeveloper.Outline.outlineBlockLevelElements = function(documents, showElementTagNames)
{
  var contentDocument = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-block-level-elements.css", "web-developer-outline-block-level-elements", contentDocument, false);

    // If showing element tag names
    if(showElementTagNames)
    {
      WebDeveloper.Common.toggleStyleSheet("features/css/before.css", "web-developer-outline-block-level-elements-before-common", contentDocument, false);
      WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-block-level-elements-before.css", "web-developer-outline-block-level-elements-before", contentDocument, false);
    }
  }
};

// Outlines all deprecated elements
WebDeveloper.Outline.outlineDeprecatedElements = function(documents, showElementTagNames)
{
  var contentDocument = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-deprecated-elements.css", "web-developer-outline-deprecated-elements", contentDocument, false);

    // If showing element tag names
    if(showElementTagNames)
    {
      WebDeveloper.Common.toggleStyleSheet("features/css/before.css", "web-developer-outline-deprecated-elements-before-common", contentDocument, false);
      WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-deprecated-elements-before.css", "web-developer-outline-deprecated-elements-before", contentDocument, false);
    }
  }
};

// Outlines all external links
WebDeveloper.Outline.outlineExternalLinks = function(outline, documents)
{
  var contentDocument = null;
  var hostName        = null;
  var location        = null;
  var styleElement    = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    // If outlining external links
    if(outline)
    {
      location     = contentDocument.location;
      hostName     = location.hostname.replace(/\./gi, "\\.");
      styleElement = contentDocument.createElement("style");

      styleElement.setAttribute("id", "web-developer-outline-external-links");
      styleElement.appendChild(contentDocument.createTextNode("a:not([href^=http\\:\\/\\/" + hostName + "]):not([href^=https\\:\\/\\/" + hostName + "]) { outline: 1px solid #b94a48 !important; }"));
      styleElement.appendChild(contentDocument.createTextNode("a:not([href^=http\\:\\/\\/]):not([href^=https\\:\\/\\/]) { outline-style: none !important; }"));

      WebDeveloper.Common.getDocumentHeadElement(contentDocument).appendChild(styleElement);
    }
    else
    {
      WebDeveloper.Common.removeMatchingElements("#web-developer-outline-external-links", contentDocument);
    }
  }
};

// Outlines all floated elements
WebDeveloper.Outline.outlineFloatedElements = function(outline, documents)
{
  var contentDocument = null;
  var float           = null;
  var floatedElements = null;
  var node            = null;
  var treeWalker      = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    // If outlining floated elements
    if(outline)
    {
      treeWalker = contentDocument.createTreeWalker(WebDeveloper.Common.getDocumentBodyElement(contentDocument), NodeFilter.SHOW_ELEMENT, null, false);

      // While the tree walker has more nodes
      while((node = treeWalker.nextNode()) !== null)
      {
        float = WebDeveloper.Common.getCSSText(WebDeveloper.Common.getPropertyCSSValue(node.ownerDocument.defaultView.getComputedStyle(node, null), "float"));

        // If this element has a background image and it is a URL
        if(float && float != "none")
        {
          WebDeveloper.Common.addClass(node, "web-developer-outline-floated-elements");
        }
      }
    }
    else
    {
      floatedElements = contentDocument.getElementsByClassName("web-developer-outline-floated-elements");

      // While there are floated elements
      while(floatedElements.length > 0)
      {
        WebDeveloper.Common.removeClass(floatedElements[0], "web-developer-outline-floated-elements");
      }
    }

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-floated-elements.css", "web-developer-outline-floated-elements", contentDocument, false);
  }
};

// Outlines all frames
WebDeveloper.Outline.outlineFrames = function(documents)
{
  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-frames.css", "web-developer-outline-frames", documents[i], false);
  }
};

// Outlines all headingss
WebDeveloper.Outline.outlineHeadings = function(documents, showElementTagNames)
{
  var contentDocument = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-headings.css", "web-developer-outline-headings", contentDocument, false);

    // If showing element tag names
    if(showElementTagNames)
    {
      WebDeveloper.Common.toggleStyleSheet("features/css/before.css", "web-developer-outline-headings-before-common", contentDocument, false);
      WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-headings-before.css", "web-developer-outline-headings-before", contentDocument, false);
    }
  }
};

// Outlines all non-secure elements
WebDeveloper.Outline.outlineNonSecureElements = function(documents)
{
  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-non-secure-elements.css", "web-developer-outline-non-secure-elements", documents[i], false);
  }
};

// Outlines all positioned elements
WebDeveloper.Outline.outlinePositionedElements = function(positionType, outline, documents)
{
  var className     = "web-developer-outline-" + positionType + "-positioned-elements";
  var contentDocument = null;
  var node            = null;
  var position        = null;
  var treeWalker      = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    // If outlining positioned elements
    if(outline)
    {
      treeWalker = contentDocument.createTreeWalker(WebDeveloper.Common.getDocumentBodyElement(contentDocument), NodeFilter.SHOW_ELEMENT, null, false);

      // While the tree walker has more nodes
      while((node = treeWalker.nextNode()) !== null)
      {
        position = WebDeveloper.Common.getCSSText(WebDeveloper.Common.getPropertyCSSValue(node.ownerDocument.defaultView.getComputedStyle(node, null), "position"));

        // If this element has a background image and it is a URL
        if(position && position == positionType)
        {
          WebDeveloper.Common.addClass(node, className);
        }
      }
    }
    else
    {
      var positionedElements = contentDocument.getElementsByClassName(className);

      // While there are positioned elements
      while(positionedElements.length > 0)
      {
        WebDeveloper.Common.removeClass(positionedElements[0], className);
      }
    }

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-positioned-elements.css", className, contentDocument, false);
  }
};

// Outlines all table captions
WebDeveloper.Outline.outlineTableCaptions = function(documents)
{
  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-table-captions.css", "web-developer-outline-table-captions", documents[i], false);
  }
};

// Outlines all table cells
WebDeveloper.Outline.outlineTableCells = function(documents, showElementTagNames)
{
  var contentDocument = null;

  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    contentDocument = documents[i];

    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-table-cells.css", "web-developer-outline-table-cells", contentDocument, false);

    // If showing element tag names
    if(showElementTagNames)
    {
      WebDeveloper.Common.toggleStyleSheet("features/css/before.css", "web-developer-outline-table-cells-before-common", contentDocument, false);
      WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-table-cells-before.css", "web-developer-outline-table-cells-before", contentDocument, false);
    }
  }
};

// Outlines all tables
WebDeveloper.Outline.outlineTables = function(documents)
{
  // Loop through the documents
  for(var i = 0, l = documents.length; i < l; i++)
  {
    WebDeveloper.Common.toggleStyleSheet("features/css/outline/outline-tables.css", "web-developer-outline-tables", documents[i], false);
  }
};

void(0);