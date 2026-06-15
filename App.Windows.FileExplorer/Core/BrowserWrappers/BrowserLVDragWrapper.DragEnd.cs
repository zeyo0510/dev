using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    public event EventHandler DragEnd = null;
    /************************************************/
    private void OnDragEnd(EventArgs e)
    {
      if (this.DragEnd != null) this.DragEnd(this, e);
    }
  }
}