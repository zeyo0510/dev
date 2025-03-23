using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    [Browsable(false)]
    public bool SuspendHeaderContextMenu {
      get { return suspendHeaderContextMenu; }
      set { suspendHeaderContextMenu = value; }
    }
  }
}