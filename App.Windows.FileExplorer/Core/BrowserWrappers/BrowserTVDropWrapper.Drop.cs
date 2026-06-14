using System;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    private void OnDrop(DropEventArgs e)
    {
      if (Drop != null) Drop(this, e);
    }
  }
}