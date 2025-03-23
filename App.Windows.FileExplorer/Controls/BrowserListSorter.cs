using System;
using System.Collections;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  internal class BrowserListSorter : IComparer
  {
    public int Compare(object x, object y)
    {
      ListViewItem itemX = x as ListViewItem;
      ListViewItem itemY = y as ListViewItem;
      /************************************************/
      if (itemX.Tag != null && itemY.Tag != null)
        return ((ShellItem)itemX.Tag).CompareTo(itemY.Tag);
      else if (itemX.Tag != null)
        return 1;
      else if (itemY.Tag != null)
        return -1;
      else
        return 0;
    }
  }
}