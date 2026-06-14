using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    public bool GetListItem(ShellItem shellItem, out ListViewItem listItem)
    {
      listItem = null;

      foreach (ListViewItem item in Items)
      {
        if (shellItem.Equals(item.Tag))
        {
          listItem = item;
          return true;
        }
      }

      return false;
    }
  }
}