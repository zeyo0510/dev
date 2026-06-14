using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    private bool suspendHeaderContextMenu;
    /************************************************/
    [Browsable(false)]
    public bool SuspendHeaderContextMenu
    {
      get
      {
        return suspendHeaderContextMenu;
      }
      set
      {
        suspendHeaderContextMenu = value;
      }
    }
  }
}