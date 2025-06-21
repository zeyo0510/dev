globalThis._ = globalThis._ || {};
/************************************************/
globalThis._.DiscordWindow = class $ extends globalThis._.Window
{
  constructor()
  {
    let url = 'https://discord.com/app';
    /************************************************/
    super(url);
  }
  /************************************************/
  // onnewwindow
  // onpermissionrequest
  // onzoomchange
  /************************************************/
  onnewwindow(window, targetUrl, initialWidth, initialHeight, name, windowOpenDisposition)
  {
    if (windowOpenDisposition == 'new_foreground_tab')
    {
      super.OpenBrowser(targetUrl);
    }
    /************************************************/
    super.onnewwindow(window, targetUrl, initialWidth, initialHeight, name, windowOpenDisposition);
  }
  /************************************************/
  onpermissionrequest(permission, request)
  {
    request.allow();
    /************************************************/
    super.onpermissionrequest(permission, request);
  }
  /************************************************/
  onzoomchange(oldZoomFactor, newZoomFactor)
  {
    console.log({
      onzoomchange: {
        oldZoomFactor: oldZoomFactor,
        newZoomFactor: newZoomFactor
      }
    });
    /************************************************/
    super.onzoomchange(oldZoomFactor, newZoomFactor);
  }
}