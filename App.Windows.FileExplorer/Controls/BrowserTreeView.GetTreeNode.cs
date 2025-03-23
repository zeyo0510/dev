using System;
using System.Collections;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class BrowserTreeView
  {
    public bool GetTreeNode(ShellItem shellItem, out TreeNode treeNode)
    {
      ArrayList pathList = new ArrayList();
        
      while (shellItem.ParentItem != null) {
        pathList.Add(shellItem);
        shellItem = shellItem.ParentItem;
      }
      pathList.Add(shellItem);

      pathList.Reverse();

      treeNode = Nodes[0];
      for (int i = 1; i < pathList.Count; i++) {
        bool found = false;
        foreach (TreeNode node in treeNode.Nodes) {
          if (node.Tag != null && node.Tag.Equals(pathList[i])) {
            treeNode = node;
            found = true;
            break;
          }
        }

        if (!found) {
          treeNode = null;
          return false;
        }
      }
      return true;
    }
  }
}