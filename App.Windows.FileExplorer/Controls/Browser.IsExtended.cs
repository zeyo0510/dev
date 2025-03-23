using System;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private bool IsExtended(TreeNode node) {
      if (node.Nodes.Count == 1 && string.IsNullOrEmpty(node.Nodes[0].Text))
        return false;
      else
        return true;
    }
  }
}