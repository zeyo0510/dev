using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    public bool Back()
    {
      if (navBackButton.DropDownItems.Count > 0)
      {
        AddNavBackForwardItem(this.navForwardButton, selectedItem);
      
        ToolStripItem item = this.navBackButton.DropDownItems[0];
        this.navBackButton.DropDownItems.Remove(item);

        if (this.navBackButton.DropDownItems.Count == 0)
          this.navBackButton.Enabled = false;

        suspendNavBackAdd = true;
        SelectPath((ShellItem)item.Tag, false);
        return true;
      }
      else
        return false;
    }
  }
}