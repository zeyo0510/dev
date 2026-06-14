using System;
using System.Collections;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class BrowserTreeView
  {
    public bool IsParentNode(TreeNode parent, TreeNode child)
    {
      TreeNode current = child;
      while (current.Parent != null)
      {
        if (current.Parent.Equals(parent))
          return true;

        current = current.Parent;
      }
      return false;
    }
    /************************************************/
    public bool IsParentNode(TreeNode parent, TreeNode child, out TreeNode[] path)
    {
      ArrayList pathList = new ArrayList();
      TreeNode current = child;
      while (current.Parent != null)
      {
        pathList.Add(current);
        if (current.Parent.Equals(parent))
        {
          pathList.Add(parent);
          pathList.Reverse();
          path = (TreeNode[])pathList.ToArray(typeof(TreeNode));
          return true;
        }
        current = current.Parent;
      }

      path = null;
      return false;
    }
  }
}