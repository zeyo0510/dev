using System;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    private void ResetDrop()
    {
        if (dropNode != null)
        {
            dropNode = null;
            parentDropItem = null;
        }

        if (lastSelectedNode != null)
            browser.FolderView.SelectedNode = lastSelectedNode;

        browser.SelectionChange = true;
    }
  }
}