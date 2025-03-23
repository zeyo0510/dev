using System;
using System.Collections;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  internal class BrowserTreeSorter : IComparer
  {
    public int Compare(object x, object y)
    {
      TreeNode nodeX = x as TreeNode;
      TreeNode nodeY = y as TreeNode;
      /************************************************/
      if (nodeX.Tag != null && nodeY.Tag != null)
        return ((ShellItem)nodeX.Tag).CompareTo(nodeY.Tag);
      else if (nodeX.Tag != null)
        return 1;
      else if (nodeY.Tag != null)
        return -1;
      else
        return 0;
    }
  }
}