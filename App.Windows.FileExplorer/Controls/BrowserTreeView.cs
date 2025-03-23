using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  internal partial class BrowserTreeView : TreeView
  {
    private BrowserTreeSorter sorter = null;
    /************************************************/
    public BrowserTreeView()
    {
      this.sorter = new BrowserTreeSorter();
      this.HandleCreated += new EventHandler(BrowserTreeView_HandleCreated);
    }
    /************************************************/
    private void BrowserTreeView_HandleCreated(object sender, EventArgs e)
    {
      ShellImageList.SetSmallImageList(this);
    }
  }
}