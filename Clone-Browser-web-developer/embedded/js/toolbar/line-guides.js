var WebDeveloper = WebDeveloper || {}; // eslint-disable-line no-redeclare, no-use-before-define

WebDeveloper.LineGuides                   = WebDeveloper.LineGuides || {};
WebDeveloper.LineGuides.padding           = 2;
WebDeveloper.LineGuides.selectedlineGuide = null;
WebDeveloper.LineGuides.spacing           = 98;
WebDeveloper.LineGuides.toolbarDocument   = null;

// Adds a horizontal line guide
WebDeveloper.LineGuides.addHorizontalLineGuide = function()
{
  var contentDocument    = WebDeveloper.Common.getContentDocument();
  var contentWindow      = WebDeveloper.Common.getContentWindow();
  var documentHeight     = contentDocument.body.offsetHeight;
  var lineGuide          = contentDocument.createElement("div");
  var lineGuideColor     = contentDocument.createElement("div");
  var lineGuidePositions = WebDeveloper.LineGuides.getHorizontalLineGuidePositions(contentDocument);
  var spacing            = contentWindow.pageYOffset + WebDeveloper.LineGuides.spacing;

  lineGuideColor.style.setProperty("background-color", WebDeveloper.LineGuides.getColor(), "important");
  lineGuide.style.setProperty("top", 0, "important");

  lineGuide.addEventListener("mousedown", WebDeveloper.LineGuides.mouseDown, false);
  lineGuide.addEventListener("mouseout", WebDeveloper.LineGuides.mouseOut, false);
  lineGuide.addEventListener("mouseover", WebDeveloper.LineGuides.mouseOver, false);

  lineGuide.setAttribute("class", "web-developer-line-guide web-developer-horizontal-line-guide");
  lineGuide.appendChild(lineGuideColor);
  WebDeveloper.LineGuides.sizeLineGuide(lineGuide, contentDocument, contentWindow);

  // While the spacing is less than the document height
  while(spacing < documentHeight)
  {
    // If there is already a line guide at this position
    if(WebDeveloper.Common.contains(lineGuidePositions, spacing + "px"))
    {
      spacing += WebDeveloper.LineGuides.spacing + WebDeveloper.LineGuides.padding;
    }
    else
    {
      lineGuide.style.setProperty("top", spacing + "px", "important");

      break;
    }
  }

  WebDeveloper.Common.getDocumentBodyElement(contentDocument).appendChild(lineGuide);
};

// Adds a vertical line guide
WebDeveloper.LineGuides.addVerticalLineGuide = function()
{
  var contentDocument    = WebDeveloper.Common.getContentDocument();
  var contentWindow      = WebDeveloper.Common.getContentWindow();
  var documentWidth      = contentDocument.body.offsetWidth;
  var lineGuide          = contentDocument.createElement("div");
  var lineGuideColor     = contentDocument.createElement("div");
  var lineGuidePositions = WebDeveloper.LineGuides.getVerticalLineGuidePositions(contentDocument);
  var spacing            = contentWindow.pageXOffset + WebDeveloper.LineGuides.spacing;

  lineGuideColor.style.setProperty("background-color", WebDeveloper.LineGuides.getColor(), "important");
  lineGuide.style.setProperty("left", 0, "important");

  lineGuide.addEventListener("mousedown", WebDeveloper.LineGuides.mouseDown, false);
  lineGuide.addEventListener("mouseout", WebDeveloper.LineGuides.mouseOut, false);
  lineGuide.addEventListener("mouseover", WebDeveloper.LineGuides.mouseOver, false);

  lineGuide.setAttribute("class", "web-developer-line-guide web-developer-vertical-line-guide");
  lineGuide.appendChild(lineGuideColor);
  WebDeveloper.LineGuides.sizeLineGuide(lineGuide, contentDocument, contentWindow);

  // While the spacing is less than the document width
  while(spacing < documentWidth)
  {
    // If there is already a line guide at this position
    if(WebDeveloper.Common.contains(lineGuidePositions, spacing + "px"))
    {
      spacing += WebDeveloper.LineGuides.spacing + WebDeveloper.LineGuides.padding;
    }
    else
    {
      lineGuide.style.setProperty("left", spacing + "px", "important");

      break;
    }
  }

  WebDeveloper.Common.getDocumentBodyElement(contentDocument).appendChild(lineGuide);
};

// Creates the line guides events
WebDeveloper.LineGuides.createEvents = function(contentDocument)
{
  window.WebDeveloperEvents                      = window.WebDeveloperEvents || {};
  window.WebDeveloperEvents.LineGuides           = window.WebDeveloperEvents.LineGuides || {};
  window.WebDeveloperEvents.LineGuides.mouseMove = WebDeveloper.LineGuides.mouseMove;
  window.WebDeveloperEvents.LineGuides.mouseUp   = WebDeveloper.LineGuides.mouseUp;
  window.WebDeveloperEvents.LineGuides.resize    = WebDeveloper.LineGuides.resize;

  contentDocument.addEventListener("mousemove", window.WebDeveloperEvents.LineGuides.mouseMove, false);
  contentDocument.addEventListener("mouseup", window.WebDeveloperEvents.LineGuides.mouseUp, false);
  contentDocument.addEventListener("resize", window.WebDeveloperEvents.LineGuides.resize, false);
};

// Creates the line guides
WebDeveloper.LineGuides.createLineGuides = function()
{
  WebDeveloper.LineGuides.addHorizontalLineGuide();
  WebDeveloper.LineGuides.addVerticalLineGuide();
};

// Creates the line guides toolbar
WebDeveloper.LineGuides.createToolbar = function(contentDocument, locale)
{
  var body              = null;
  var lineGuidesToolbar = contentDocument.createElement("iframe");
  var styleSheet        = null;

  lineGuidesToolbar.setAttribute("class", "web-developer-toolbar");
  lineGuidesToolbar.setAttribute("id", "web-developer-line-guides-toolbar");

  WebDeveloper.Common.getDocumentBodyElement(contentDocument).appendChild(lineGuidesToolbar);
  lineGuidesToolbar.contentWindow.stop();

  WebDeveloper.LineGuides.toolbarDocument = lineGuidesToolbar.contentDocument;
  body                                    = WebDeveloper.Common.getDocumentBodyElement(WebDeveloper.LineGuides.toolbarDocument);
  styleSheet                              = WebDeveloper.LineGuides.toolbarDocument.createElement("link");

  styleSheet.setAttribute("rel", "stylesheet");
  styleSheet.setAttribute("href", WebDeveloper.Common.getChromeURL("lib/bootstrap/bootstrap.css"));
  WebDeveloper.Common.getDocumentHeadElement(WebDeveloper.LineGuides.toolbarDocument).appendChild(styleSheet);

  styleSheet = WebDeveloper.LineGuides.toolbarDocument.createElement("link");

  styleSheet.setAttribute("rel", "stylesheet");
  styleSheet.setAttribute("href", WebDeveloper.Common.getChromeURL("embedded/css/toolbar/internal/toolbar.css"));
  WebDeveloper.Common.getDocumentHeadElement(WebDeveloper.LineGuides.toolbarDocument).appendChild(styleSheet);

  styleSheet = WebDeveloper.LineGuides.toolbarDocument.createElement("link");

  styleSheet.setAttribute("rel", "stylesheet");
  styleSheet.setAttribute("href", WebDeveloper.Common.getChromeURL("embedded/css/toolbar/internal/line-guides.css"));
  WebDeveloper.Common.getDocumentHeadElement(WebDeveloper.LineGuides.toolbarDocument).appendChild(styleSheet);

  body.insertAdjacentHTML("beforeend", WebDeveloper.LineGuides.getLineGuidesTemplate(locale));
  body.setAttribute("class", "bg-body-secondary");

  WebDeveloper.LineGuides.toolbarDocument.getElementById("add-horizontal-line-guide").addEventListener("click", WebDeveloper.LineGuides.addHorizontalLineGuide, false);
  WebDeveloper.LineGuides.toolbarDocument.getElementById("add-vertical-line-guide").addEventListener("click", WebDeveloper.LineGuides.addVerticalLineGuide, false);
};

// Displays line guides
WebDeveloper.LineGuides.displayLineGuides = function(display, contentDocument, locale)
{
  // If displaying line guides
  if(display)
  {
    WebDeveloper.LineGuides.createLineGuides(contentDocument);
    WebDeveloper.LineGuides.createEvents(contentDocument);
    WebDeveloper.LineGuides.createToolbar(contentDocument, locale);
  }
  else
  {
    WebDeveloper.LineGuides.removeLineGuides(contentDocument);
    WebDeveloper.LineGuides.removeEvents(contentDocument);
    WebDeveloper.LineGuides.removeToolbar(contentDocument);
  }

  WebDeveloper.Common.toggleStyleSheet("/embedded/css/toolbar/external/line-guides.css", "web-developer-line-guides-styles", contentDocument, false);
  WebDeveloper.Common.toggleStyleSheet("/embedded/css/toolbar/external/toolbar.css", "web-developer-line-guides-toolbar-styles", contentDocument, false);
};

// Returns the line guides color
WebDeveloper.LineGuides.getColor = function()
{
  return "#dc3545";
};

// Returns an array containing the horizontal line guide positions
WebDeveloper.LineGuides.getHorizontalLineGuidePositions = function(contentDocument)
{
  return WebDeveloper.LineGuides.getLineGuidePositions(contentDocument, "horizontal");
};

// Returns the line guide position nearest to the given line guide position
WebDeveloper.LineGuides.getLineGuidePosition = function(contentDocument, direction, lineGuidePosition, next)
{
  var lineGuidePositions     = WebDeveloper.LineGuides.getLineGuidePositions(contentDocument, direction);
  var otherLineGuidePosition = 0;
  var position               = 0;

  // Loop through the line guide positions
  for(var i = 0, l = lineGuidePositions.length; i < l; i++)
  {
    otherLineGuidePosition = parseInt(lineGuidePositions[i].replace(/px/gi, ""), 10) + WebDeveloper.LineGuides.padding;

    // If looking for the next line guide position, the other line guide position is greater than the line guide position and the other line guide position is greater than the saved position
    if(next && otherLineGuidePosition > lineGuidePosition && otherLineGuidePosition > position)
    {
      position = otherLineGuidePosition;
    }
    else if(!next && otherLineGuidePosition < lineGuidePosition && otherLineGuidePosition > position)
    {
      position = otherLineGuidePosition;
    }
  }

  return position;
};

// Returns an array containing the line guide positions
WebDeveloper.LineGuides.getLineGuidePositions = function(contentDocument, direction)
{
  var lineGuidePositions = [];
  var lineGuides         = contentDocument.getElementsByClassName("web-developer-" + direction + "-line-guide");

  // Loop through the line guides
  for(var i = 0, l = lineGuides.length; i < l; i++)
  {
    // If we are looking at horizontal line guides
    if(direction == "horizontal")
    {
      lineGuidePositions.push(lineGuides[i].style.top);
    }
    else
    {
      lineGuidePositions.push(lineGuides[i].style.left);
    }
  }

  return lineGuidePositions;
};

// Returns the line guides template
WebDeveloper.LineGuides.getLineGuidesTemplate = function(locale)
{
  return `<div class="container-fluid py-1">
  <div class="align-items-center justify-content-between row">
  <div class="col-auto">
  <h1><img src="` + WebDeveloper.Common.getChromeURL("svg/logos/color/logo.svg") + '" alt="" class="me-1">' + locale.title + `</h1>
  </div>
  <div class="col-auto">
  <span id="line-guide-information" class="me-5">
  <span>` + locale.positionLabel + ` </span>
  <span id="line-guide-position" class="me-5"></span>
  <span>` + locale.previousPosition + `</span>
  <span id="previous-line-guide-position" class="me-2"></span>
  <span>` + locale.nextPosition + `</span>
  <span id="next-line-guide-position"></span>
  </span>
  <button id="add-horizontal-line-guide" type="button" class="btn btn-primary btn-sm">` + locale.addHorizontalLineGuide + `</button>
  <button id="add-vertical-line-guide" type="button" class="btn btn-primary btn-sm">` + locale.addVerticalLineGuide + `</button>
  </div>
  </div>
  </div>`;
};

// Returns an array containing the vertical line guide positions
WebDeveloper.LineGuides.getVerticalLineGuidePositions = function(contentDocument)
{
  return WebDeveloper.LineGuides.getLineGuidePositions(contentDocument, "vertical");
};

// Hides the line guide information
WebDeveloper.LineGuides.hideInformation = function()
{
  WebDeveloper.Common.removeClass(WebDeveloper.Common.getDocumentBodyElement(WebDeveloper.LineGuides.toolbarDocument), "display-information");
};

// Handles the mouse down event on a line guide
WebDeveloper.LineGuides.mouseDown = function(event)
{
  // If the click was not a right click
  if(event.button != 2)
  {
    var element = event.target;

    // If the element is set
    if(element)
    {
      // If the element is a line guide
      if(element.classList.contains("web-developer-line-guide"))
      {
        WebDeveloper.LineGuides.selectedlineGuide = element;
      }
      else
      {
        element = element.parentNode;

        // If the element is a line guide
        if(element.classList.contains("web-developer-line-guide"))
        {
          WebDeveloper.LineGuides.selectedlineGuide = element;
        }
      }
    }
  }
};

// Handles the mouse move event on the document
WebDeveloper.LineGuides.mouseMove = function(event)
{
  // If a line guide is selected
  if(WebDeveloper.LineGuides.selectedlineGuide)
  {
    // If the line guide is horizontal
    if(WebDeveloper.Common.hasClass(WebDeveloper.LineGuides.selectedlineGuide, "web-developer-horizontal-line-guide"))
    {
      WebDeveloper.LineGuides.selectedlineGuide.style.setProperty("top", event.pageY + "px", "important");
    }
    else
    {
      WebDeveloper.LineGuides.selectedlineGuide.style.setProperty("left", event.pageX + "px", "important");
    }

    WebDeveloper.LineGuides.updateLineGuideInformation(WebDeveloper.LineGuides.selectedlineGuide);
  }
};

// Handles the mouse out event on a line guide
WebDeveloper.LineGuides.mouseOut = function(event)
{
  var eventTarget = event.target;

  // If the event target is set
  if(eventTarget)
  {
    var ownerDocument = eventTarget.ownerDocument;

    // If the owner document is set
    if(ownerDocument)
    {
      WebDeveloper.LineGuides.hideInformation();
    }
  }
};

// Handles the mouse over event on a line guide
WebDeveloper.LineGuides.mouseOver = function(event)
{
  var lineGuide = event.target;

  // If the line guide is set
  if(lineGuide)
  {
    var ownerDocument = lineGuide.ownerDocument;

    // If the owner document is set
    if(ownerDocument)
    {
      // If this is not a line guide
      if(!WebDeveloper.Common.hasClass(lineGuide, "web-developer-line-guide"))
      {
        lineGuide = lineGuide.parentNode;
      }

      WebDeveloper.LineGuides.updateLineGuideInformation(lineGuide);
    }
  }
};

// Handles the mouse up event on a line guide
WebDeveloper.LineGuides.mouseUp = function()
{
  WebDeveloper.LineGuides.selectedlineGuide = null;
};

// Removes the line guides events
WebDeveloper.LineGuides.removeEvents = function(contentDocument)
{
  contentDocument.removeEventListener("mousemove", window.WebDeveloperEvents.LineGuides.mouseMove, false);
  contentDocument.removeEventListener("resize", window.WebDeveloperEvents.LineGuides.resize, false);

  window.WebDeveloperEvents.LineGuides = null;
};

// Removes the line guides
WebDeveloper.LineGuides.removeLineGuides = function(contentDocument)
{
  WebDeveloper.Common.removeMatchingElements("#web-developer-line-guide-information, .web-developer-line-guide", contentDocument);
};

// Removes the line guides toolbar
WebDeveloper.LineGuides.removeToolbar = function(contentDocument)
{
  // If the toolbar document is set
  if(WebDeveloper.LineGuides.toolbarDocument)
  {
    WebDeveloper.LineGuides.toolbarDocument.getElementById("add-horizontal-line-guide").removeEventListener("click", WebDeveloper.LineGuides.addHorizontalLineGuide, false);
    WebDeveloper.LineGuides.toolbarDocument.getElementById("add-vertical-line-guide").removeEventListener("click", WebDeveloper.LineGuides.addVerticalLineGuide, false);
  }

  WebDeveloper.Common.removeMatchingElements("#web-developer-line-guides-toolbar", contentDocument);
};

// Handles the resize event on the window
WebDeveloper.LineGuides.resize = function()
{
  var contentDocument = WebDeveloper.Common.getContentDocument();
  var contentWindow   = WebDeveloper.Common.getContentWindow();
  var lineGuides      = contentDocument.getElementsByClassName("web-developer-line-guide");

  // Loop through the line guides
  for(var i = 0, l = lineGuides.length; i < l; i++)
  {
    WebDeveloper.LineGuides.sizeLineGuide(lineGuides[i], contentDocument, contentWindow);
  }
};

// Sets the size of a line guide
WebDeveloper.LineGuides.sizeLineGuide = function(lineGuide, contentDocument, contentWindow)
{
  // If the line guide is horizontal
  if(WebDeveloper.Common.hasClass(lineGuide, "web-developer-horizontal-line-guide"))
  {
    var documentWidth = contentDocument.body.offsetWidth;
    var viewportWidth = contentWindow.innerWidth;

    // If the viewport width is greater than the document width
    if(viewportWidth > documentWidth)
    {
      lineGuide.style.setProperty("width", viewportWidth + "px", "important");
    }
    else
    {
      lineGuide.style.setProperty("width", documentWidth + "px", "important");
    }
  }
  else
  {
    var documentHeight = contentDocument.body.offsetHeight;
    var viewportHeight = contentWindow.innerHeight;

    // If the viewport height is greater than the document height
    if(viewportHeight > documentHeight)
    {
      lineGuide.style.setProperty("height", viewportHeight + "px", "important");
    }
    else
    {
      lineGuide.style.setProperty("height", documentHeight + "px", "important");
    }
  }
};

// Updates the line guide information
WebDeveloper.LineGuides.updateInformation = function(position, previousPosition, nextPosition)
{
  WebDeveloper.LineGuides.toolbarDocument.getElementById("line-guide-position").textContent          = position + "px";
  WebDeveloper.LineGuides.toolbarDocument.getElementById("next-line-guide-position").textContent     = nextPosition + "px";
  WebDeveloper.LineGuides.toolbarDocument.getElementById("previous-line-guide-position").textContent = previousPosition + "px";

  WebDeveloper.Common.addClass(WebDeveloper.Common.getDocumentBodyElement(WebDeveloper.LineGuides.toolbarDocument), "display-information");
};

// Updates the line guide information
WebDeveloper.LineGuides.updateLineGuideInformation = function(lineGuide)
{
  var nextPosition     = null;
  var ownerDocument    = lineGuide.ownerDocument;
  var position         = null;
  var previousPosition = null;

  // If the owner document is set
  if(ownerDocument)
  {
    // If this is not a line guide
    if(!WebDeveloper.Common.hasClass(lineGuide, "web-developer-line-guide"))
    {
      lineGuide = lineGuide.parentNode;
    }

    // If this is a horizontal line guide
    if(WebDeveloper.Common.hasClass(lineGuide, "web-developer-horizontal-line-guide"))
    {
      position         = WebDeveloper.Common.getElementPositionY(lineGuide) + WebDeveloper.LineGuides.padding;
      nextPosition     = WebDeveloper.LineGuides.getLineGuidePosition(ownerDocument, "horizontal", position, true);
      previousPosition = WebDeveloper.LineGuides.getLineGuidePosition(ownerDocument, "horizontal", position, false);
    }
    else
    {
      position         = WebDeveloper.Common.getElementPositionX(lineGuide) + WebDeveloper.LineGuides.padding;
      nextPosition     = WebDeveloper.LineGuides.getLineGuidePosition(ownerDocument, "vertical", position, true);
      previousPosition = WebDeveloper.LineGuides.getLineGuidePosition(ownerDocument, "vertical", position, false);
    }

    WebDeveloper.LineGuides.updateInformation(position, previousPosition, nextPosition);
  }
};

// Fixes a non-structured-clonable data error in Firefox
""; // eslint-disable-line no-unused-expressions
