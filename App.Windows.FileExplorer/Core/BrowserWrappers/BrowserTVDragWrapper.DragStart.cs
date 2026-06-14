using System;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
  {
    public event DragEnterEventHandler DragStart;
    
    private void OnDragStart(DragEnterEventArgs e)
    {
      if (DragStart != null) DragStart(this, e);
    }
  }
}