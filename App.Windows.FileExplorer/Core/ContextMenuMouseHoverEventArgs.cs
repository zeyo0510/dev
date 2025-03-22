using System;
/************************************************/
namespace FileBrowser
{
  public class ContextMenuMouseHoverEventArgs : EventArgs
  {
    public ContextMenuMouseHoverEventArgs(string info)
    {
      this.ContextMenuItemInfo = info;
    }
    /************************************************/
    public string ContextMenuItemInfo
    {
      get; private set;
    }
  }
}