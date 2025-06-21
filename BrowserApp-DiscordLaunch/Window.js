globalThis._ = globalThis._ || {};
/************************************************/
globalThis._.Window = class $ extends EventTarget
{
  #url = undefined;
  /************************************************/
  #createdWindow = undefined;
  /************************************************/
  #doc = undefined;
  /************************************************/
  #header     = undefined;
  #navigation = undefined;
  #logo       = undefined;
  #minimize   = undefined;
  #maximize   = undefined;
  #close      = undefined;
  #main       = undefined;
  #horizontal = undefined;
  #left       = undefined;
  #vertical   = undefined;
  #top        = undefined;
  #panel      = undefined;
  #entity     = undefined;
  #bottom     = undefined;
  #right      = undefined;
  #footer     = undefined;
  /************************************************/
  constructor(url)
  {
    super();
    /************************************************/
    return new Promise((resolve, reject) => {
      this.URL = url;
      /************************************************/
      resolve({});
    })
    .then((_) => {
      _.page = './pages/index.html';
      /************************************************/
      return _;
    })
    .then((_) => {
      _.options = {};
      /************************************************/
      _.options.id            = undefined;
      _.options.alwaysOnTop   = false;
      _.options.bounds        = {};
      _.options.bounds.height = 720;
      _.options.bounds.left   = 0;
      _.options.bounds.top    = 0;
      _.options.bounds.width  = 1280;
      _.options.focused       = true;
      _.options.frame         = 'none';
      _.options.hidden        = true;
      _.options.icon          = undefined;
      _.options.maxHeight     =  -1;
      _.options.maxWidth      =  -1;
      _.options.minHeight     =  500;
      _.options.minWidth      =  940;
      _.options.resizable     = true;
      _.options.state         = 'normal';
      /************************************************/
      return _;
    })
    .then((_) => {
      _.callback = (createdWindow) => {
        this.#createdWindow = createdWindow;
        /************************************************/
        this.#createdWindow.onClosed.addListener(() => {
          this.#createdWindow = undefined;
          /************************************************/
          this.#doc = undefined;
          /************************************************/
          this.#header     = undefined;
          this.#navigation = undefined;
          this.#logo       = undefined;
          this.#minimize   = undefined;
          this.#maximize   = undefined;
          this.#close      = undefined;
          this.#main       = undefined;
          this.#horizontal = undefined;
          this.#left       = undefined;
          this.#vertical   = undefined;
          this.#top        = undefined;
          this.#panel      = undefined;
          this.#entity     = undefined;
          this.#bottom     = undefined;
          this.#right      = undefined;
          this.#footer     = undefined;
        });
        /************************************************/
        this.#createdWindow.contentWindow.addEventListener('load', (event) =>
        {
          this.#init();
        });
      }
      /************************************************/
      return _;
    })
    .then((_) => {
      chrome.app.window.create(_.page, _.options, _.callback);
    })
    .then(async () => {
      await new Promise((resolve) => {
        const timeoutID = setTimeout(() => {
          if (this.#createdWindow != undefined)
          {
            resolve(timeoutID);
          }
        }, 100);
      })
      .then((timeoutID) => {
        clearTimeout(timeoutID);
      });
    })
    .then(() => {
      return this;
    });
  }
  /************************************************/
  get URL()
  {
    let retValue = undefined;
    /************************************************/
    retValue = this.#url;
    /************************************************/
    return retValue;
  }
  /************************************************/
  set URL(value)
  {
    this.#url = value;
    /************************************************/
    if (this.#entity)
    {
      this.#entity.src = this.#url;
    }
  }
  /************************************************/
  #init()
  {
    this.#doc = this.#createdWindow.contentWindow.document;
    /************************************************/
    this.#header     = this.#doc.getElementById('header    '.trim());
    this.#navigation = this.#doc.getElementById('navigation'.trim());
    this.#logo       = this.#doc.getElementById('logo      '.trim());
    this.#minimize   = this.#doc.getElementById('minimize  '.trim());
    this.#maximize   = this.#doc.getElementById('maximize  '.trim());
    this.#close      = this.#doc.getElementById('close     '.trim());
    this.#main       = this.#doc.getElementById('main      '.trim());
    this.#horizontal = this.#doc.getElementById('horizontal'.trim());
    this.#left       = this.#doc.getElementById('left      '.trim());
    this.#vertical   = this.#doc.getElementById('vertical  '.trim());
    this.#top        = this.#doc.getElementById('top       '.trim());
    this.#panel      = this.#doc.getElementById('panel     '.trim());
    this.#entity     = this.#doc.getElementById('entity    '.trim());
    this.#bottom     = this.#doc.getElementById('bottom    '.trim());
    this.#right      = this.#doc.getElementById('right     '.trim());
    this.#footer     = this.#doc.getElementById('footer    '.trim());
    /************************************************/
    // header
    {
    }
    // navigation
    {
    }
    // logo
    {
    }
    // minimize
    {
      this.#minimize.addEventListener('click', (event) => { this.#minimize_click(this.#minimize, event); });
    }
    // maximize
    {
      this.#maximize.addEventListener('click', (event) => { this.#maximize_click(this.#maximize, event); });
    }
    // close
    {
      this.#close.addEventListener('click', (event) => { this.#close_click(this.#close, event); });
    }
    // main
    {
    }
    // horizontal
    {
    }
    // left
    {
    }
    // vertical
    {
    }
    // top
    {
    }
    // panel
    {
    }
    // entity
    {
      this.#entity.src = this.URL;
      /************************************************/
      this.#entity.addEventListener('close'            , (event) => { this.#entity_close            (this.#entity, event); });
      this.#entity.addEventListener('consolemessage'   , (event) => { this.#entity_consolemessage   (this.#entity, event); });
      this.#entity.addEventListener('contentload'      , (event) => { this.#entity_contentload      (this.#entity, event); });
      this.#entity.addEventListener('dialog'           , (event) => { this.#entity_dialog           (this.#entity, event); });
      this.#entity.addEventListener('exit'             , (event) => { this.#entity_exit             (this.#entity, event); });
      this.#entity.addEventListener('findupdate'       , (event) => { this.#entity_findupdate       (this.#entity, event); });
      this.#entity.addEventListener('loadabort'        , (event) => { this.#entity_loadabort        (this.#entity, event); });
      this.#entity.addEventListener('loadcommit'       , (event) => { this.#entity_loadcommit       (this.#entity, event); });
      this.#entity.addEventListener('loadredirect'     , (event) => { this.#entity_loadredirect     (this.#entity, event); });
      this.#entity.addEventListener('loadstart'        , (event) => { this.#entity_loadstart        (this.#entity, event); });
      this.#entity.addEventListener('loadstop'         , (event) => { this.#entity_loadstop         (this.#entity, event); });
      this.#entity.addEventListener('newwindow'        , (event) => { this.#entity_newwindow        (this.#entity, event); });
      this.#entity.addEventListener('permissionrequest', (event) => { this.#entity_permissionrequest(this.#entity, event); });
      this.#entity.addEventListener('responsive'       , (event) => { this.#entity_responsive       (this.#entity, event); });
      this.#entity.addEventListener('sizechanged'      , (event) => { this.#entity_sizechanged      (this.#entity, event); });
      this.#entity.addEventListener('unresponsive'     , (event) => { this.#entity_unresponsive     (this.#entity, event); });
      this.#entity.addEventListener('zoomchange'       , (event) => { this.#entity_zoomchange       (this.#entity, event); });
    }
    // bottom
    {
    }
    // right
    {
    }
    // footer
    {
    }
  }
  /************************************************/
  #dispose()
  {
    this.#createdWindow = undefined;
    /************************************************/
    this.#doc = undefined;
    /************************************************/
    this.#header     = undefined;
    this.#navigation = undefined;
    this.#logo       = undefined;
    this.#minimize   = undefined;
    this.#maximize   = undefined;
    this.#close      = undefined;
    this.#main       = undefined;
    this.#horizontal = undefined;
    this.#left       = undefined;
    this.#vertical   = undefined;
    this.#top        = undefined;
    this.#panel      = undefined;
    this.#entity     = undefined;
    this.#bottom     = undefined;
    this.#right      = undefined;
    this.#footer     = undefined;
  }
  /************************************************/
  #minimize_click(sender, event)
  {
    this.Minimize();
  }
  /************************************************/
  #maximize_click(sender, event)
  {
    this.Maximize();
  }
  /************************************************/
  #close_click(sender, event)
  {
    this.Close();
  }
  /************************************************/
  #entity_close(sender, event)
  {
    this.onclose();
  }
  /************************************************/
  #entity_consolemessage(sender, event)
  {
    this.onconsolemessage(event.level, event.message, event.line, event.sourceId);
  }
  /************************************************/
  #entity_contentload(sender, event)
  {
    this.oncontentload();
  }
  /************************************************/
  #entity_dialog(sender, event)
  {
    this.ondialog(event.messageType, event.messageText, event.dialog);
  }
  /************************************************/
  #entity_exit(sender, event)
  {
    this.onexit(event.processID, event.onexit);
  }
  /************************************************/
  #entity_findupdate(sender, event)
  {
    this.onfindupdate(event.searchText, event.numberOfMatches, event.activeMatchOrdinal, event.selectionRect, event.canceled, event.finalUpdate);
  }
  /************************************************/
  #entity_loadabort(sender, event)
  {
    this.onloadabort(event.url, event.isTopLevel, event.code, event.reason);
  }
  /************************************************/
  #entity_loadcommit(sender, event)
  {
    this.onloadcommit(event.url, event.isTopLevel);
  }
  /************************************************/
  #entity_loadredirect(sender, event)
  {
    this.onloadredirect(event.oldUrl, event.newUrl, event.isTopLevel);
  }
  /************************************************/
  #entity_loadstart(sender, event)
  {
    this.onloadstart(event.url, event.isTopLevel);
  }
  /************************************************/
  #entity_loadstop(sender, event)
  {
    this.onloadstop();
  }
  /************************************************/
  #entity_newwindow(sender, event)
  {
    this.onnewwindow(event.window, event.targetUrl, event.initialWidth, event.initialHeight, event.name, event.windowOpenDisposition);
  }
  /************************************************/
  #entity_permissionrequest(sender, event)
  {
    this.onpermissionrequest(event.permissionm, event.request);
  }
  /************************************************/
  #entity_responsive(sender, event)
  {
    this.onresponsive(event.processID);
  }
  /************************************************/
  #entity_sizechanged(sender, event)
  {
    this.onsizechanged(event.oldWidth, event.oldHeight, event.newWidth, event.newHeight);
  }
  /************************************************/
  #entity_unresponsive(sender, event)
  {
    this.onunresponsive(event.processID);
  }
  /************************************************/
  #entity_zoomchange(sender, event)
  {
    this.onzoomchange(event.oldZoomFactor, event.newZoomFactor);
  }
  /************************************************/
  onclose()
  {
    super.dispatchEvent(new CustomEvent('close'));
  }
  /************************************************/
  onconsolemessage(level, message, line, sourceId)
  {
    super.dispatchEvent(new CustomEvent('consolemessage', {
      detail: {
        level   : level,
        message : message,
        line    : line,
        sourceId: sourceId
      }
    }));
  }
  /************************************************/
  oncontentload()
  {
    super.dispatchEvent(new CustomEvent('contentload'));
  }
  /************************************************/
  ondialog(messageType, messageText, dialog)
  {
    super.dispatchEvent(new CustomEvent('dialog', {
      detail: {
        messageType: messageType,
        messageText: messageText,
        dialog     : dialog
      }
    }));
  }
  /************************************************/
  onexit(processID, reason)
  {
    super.dispatchEvent(new CustomEvent('exit', {
      detail: {
        processID: processID,
        reason   : reason
      }
    }));
  }
  /************************************************/
  onfindupdate(searchText, numberOfMatches, activeMatchOrdinal, selectionRect, canceled, finalUpdate)
  {
    super.dispatchEvent(new CustomEvent('findupdate', {
      detail: {
        searchText        : searchText,
        numberOfMatches   : numberOfMatches,
        activeMatchOrdinal: activeMatchOrdinal,
        selectionRect     : selectionRect,
        canceled          : canceled,
        finalUpdate       : finalUpdate
      }
    }));
  }
  /************************************************/
  onloadabort(url, isTopLevel, code, reason)
  {
    super.dispatchEvent(new CustomEvent('loadabort', {
      detail: {
        url       : url,
        isTopLevel: isTopLevel,
        code      : code,
        reason    : reason
      }
    }));
  }
  /************************************************/
  onloadcommit(url, isTopLevel)
  {
    super.dispatchEvent(new CustomEvent('loadcommit', {
      detail: {
        url       : url,
        isTopLevel: isTopLevel
      }
    }));
  }
  /************************************************/
  onloadredirect(oldUrl, newUrl, isTopLevel)
  {
    super.dispatchEvent(new CustomEvent('loadredirect', {
      detail: {
        oldUrl    : oldUrl,
        newUrl    : newUrl,
        isTopLevel: isTopLevel
      }
    }));
  }
  /************************************************/
  onloadstart(url, isTopLevel)
  {
    super.dispatchEvent(new CustomEvent('loadstart', {
      detail: {
        url       : url,
        isTopLevel: isTopLevel
      }
    }));
  }
  /************************************************/
  onloadstop()
  {
    super.dispatchEvent(new CustomEvent('loadstop'));
  }
  /************************************************/
  onnewwindow(window, targetUrl, initialWidth, initialHeight, name, windowOpenDisposition)
  {
    super.dispatchEvent(new CustomEvent('newwindow', {
      detail: {
        window               : window,
        targetUrl            : targetUrl,
        initialWidth         : initialWidth,
        initialHeight        : initialHeight,
        name                 : name,
        windowOpenDisposition: windowOpenDisposition
      }
    }));
  }
  /************************************************/
  onpermissionrequest(permission, request)
  {
    super.dispatchEvent(new CustomEvent('permissionrequest', {
      detail: {
        permission: permission,
        request   : request
      }
    }));
  }
  /************************************************/
  onresponsive(processID)
  {
    super.dispatchEvent(new CustomEvent('responsive', {
      detail: {
        processID: processID
      }
    }));
  }
  /************************************************/
  onsizechanged(oldWidth, oldHeight, newWidth, newHeight)
  {
    super.dispatchEvent(new CustomEvent('sizechanged', {
      detail: {
        oldWidth : oldWidth,
        oldHeight: oldHeight,
        newWidth : newWidth,
        newHeight: newHeight
      }
    }));
  }
  /************************************************/
  onunresponsive(processID)
  {
    super.dispatchEvent(new CustomEvent('unresponsive', {
      detail: {
        processID: processID
      }
    }));
  }
  /************************************************/
  onzoomchange(oldZoomFactor, newZoomFactor)
  {
    super.dispatchEvent(new CustomEvent('zoomchange', {
      detail: {
        oldZoomFactor: oldZoomFactor,
        newZoomFactor: newZoomFactor
      }
    }));
  }
  /************************************************/
  Show()
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      this.#createdWindow.show();
      /************************************************/
      resolve();return;
    });
  }
  /************************************************/
  Hide()
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      this.#createdWindow.hide();
      /************************************************/
      resolve();return;
    });
  }
  /************************************************/
  Minimize()
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      this.#createdWindow.minimize();
      /************************************************/
      resolve();return;
    });
  }
  /************************************************/
  Maximize()
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      if (this.#createdWindow.isMaximized())
      {
        this.#createdWindow.restore();
        /************************************************/
        resolve();return;
      }
      /************************************************/
      this.#createdWindow.maximize();
      /************************************************/
      resolve();return;
    });
  }
  /************************************************/
  Close()
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      this.#createdWindow.close();
      /************************************************/
      this.#dispose();
      /************************************************/
      resolve();return;
    });
  }
  /************************************************/
  OpenBrowser(url)
  {
    return new Promise((resolve, reject) => {
      if (!this.#createdWindow)
      {
        reject();return;
      }
      /************************************************/
      this.#createdWindow.contentWindow.chrome.browser.openTab
      ({
        url: url
      });
      /************************************************/
      resolve();return;
    });
  }
}