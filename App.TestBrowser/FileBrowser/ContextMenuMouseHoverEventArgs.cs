using System;
namespace FileBrowser {
  public class ContextMenuMouseHoverEventArgs : EventArgs {
    private string info;

    public ContextMenuMouseHoverEventArgs(string info) {
      this.info = info;
    }

    // The help info of the contextmenu item
    public string ContextMenuItemInfo { get { return info; } }
  }
}