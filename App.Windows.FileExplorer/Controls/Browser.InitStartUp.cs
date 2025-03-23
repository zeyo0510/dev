using System;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitStartUp() {
      if (startupDir != SpecialFolders.Other)
        SelectPath(startupDir, true);
      else
        SelectPath(otherStartupDir, true);

      navAddressBox.SelectionLength = 0;
      navAddressBox.Text = navAddressBox.CurrentItem.Text;
      folderView.Focus();
    }
  }
}