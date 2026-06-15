using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    public event DropEventHandler Drop = null;
    /************************************************/
    private void OnDrop(DropEventArgs e)
    {
      if (this.Drop != null) this.Drop(this, e);
    }
  }
}