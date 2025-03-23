using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserTreeView
  {
    public void SetSorting(bool sorting)
    {
      if (sorting)
        this.TreeViewNodeSorter = sorter;
      else
        this.TreeViewNodeSorter = null;
    }
  }
}