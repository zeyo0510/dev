using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    public void SetSorting(bool sorting) {
      if (sorting)
        this.ListViewItemSorter = sorter;
      else
        this.ListViewItemSorter = null;
    }
  }
}