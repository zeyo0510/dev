using System;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
   public bool Up() {
      if (folderView.SelectedNode != null && folderView.SelectedNode.Parent != null) {
        folderView.SelectedNode = folderView.SelectedNode.Parent;
        return true;
      } else {
        return false;
      }
    }
  }
}