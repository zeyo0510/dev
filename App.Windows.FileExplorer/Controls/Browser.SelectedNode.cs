using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    public TreeNode SelectedNode {
      get { return folderView.SelectedNode; }
      set  {
        if (value != null)
          folderView.SelectedNode = value;
      }
    }
  }
}