using System;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    public event DragEnterEventHandler DragStart;
    
    private void OnDragStart(DragEnterEventArgs e)
    {
      if (DragStart != null) DragStart(this, e);
    }
  }
}