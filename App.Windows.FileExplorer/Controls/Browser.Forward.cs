using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    public bool Forward() {
      if (navForwardButton.DropDownItems.Count > 0) {
        AddNavBackForwardItem(this.navBackButton, selectedItem);
        
        ToolStripItem item = this.navForwardButton.DropDownItems[0];
        this.navForwardButton.DropDownItems.Remove(item);
  
        if (this.navForwardButton.DropDownItems.Count == 0)
          this.navForwardButton.Enabled = false;
  
        suspendNavBackAdd = true;
        SelectPath((ShellItem)item.Tag, false);
        return true;
      }
      else
        return false;
    }
  }
}