using System;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    public event DropEventHandler Drop;
    
    private void OnDrop(DropEventArgs e)
    {
      if (Drop != null) Drop(this, e);
    }
  }
}