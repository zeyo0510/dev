using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    public event DragEnterEventHandler DragStart = null;
    /************************************************/
    private void OnDragStart(DragEnterEventArgs e)
    {
      if (this.DragStart != null) this.DragStart(this, e);
    }
  }
}