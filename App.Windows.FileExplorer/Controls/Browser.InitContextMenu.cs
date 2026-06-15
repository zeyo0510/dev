using System;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitContextMenu()
    {
      tvContextWrapper = new BrowserTVContextMenuWrapper(this);
      lvContextWrapper = new BrowserLVContextMenuWrapper(this, PluginWrapper);
    }
  }
}