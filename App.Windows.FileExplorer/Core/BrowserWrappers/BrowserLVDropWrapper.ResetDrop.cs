using System;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    private void ResetDrop()
    {
      if (dropListItem != null)
      {
        dropListItem.Selected = wasSelected;
        dropListItem = null;
        parentDropItem = null;
      }

      br.SelectionChange = true;
    }
  }
}