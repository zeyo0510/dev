using System;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitFolderView()
    {
      folderView.BeforeExpand += new TreeViewCancelEventHandler(folderView_BeforeExpand);
      folderView.BeforeSelect += new TreeViewCancelEventHandler(folderView_BeforeSelect);
      folderView.AfterSelect += new TreeViewEventHandler(folderView_AfterSelect);
      folderView.SetSorting(true);
    }
  }
}