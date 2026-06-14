using System;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
  {
    public event EventHandler DragEnd;
    
    private void OnDragEnd(EventArgs e)
    {
      if (DragEnd != null) DragEnd(this, e);
    }
  }
}