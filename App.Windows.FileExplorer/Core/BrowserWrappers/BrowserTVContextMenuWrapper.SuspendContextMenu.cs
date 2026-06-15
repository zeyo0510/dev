using System;

namespace FileBrowser
{
  partial class BrowserTVContextMenuWrapper
  {
    private bool suspendContextMenu;
    public bool SuspendContextMenu
    {
      get { return suspendContextMenu; }
      set { suspendContextMenu = value; }
    }
  }
}