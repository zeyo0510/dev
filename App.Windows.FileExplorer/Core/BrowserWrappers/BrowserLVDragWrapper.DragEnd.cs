using System;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    public event EventHandler DragEnd;
    
    private void OnDragEnd(EventArgs e)
    {
      if (DragEnd != null) DragEnd(this, e);
    }
  }
}