using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitBaseItems() {
      if (ShellBrowser == null)
        ShellBrowser = new ShellBrowser();

      desktopNode = new TreeNode(ShellBrowser.DesktopItem.Text,ShellBrowser.DesktopItem.ImageIndex,ShellBrowser.DesktopItem.SelectedImageIndex);
      desktopNode.Tag = ShellBrowser.DesktopItem;
      desktopNode.Name = desktopNode.Text;

      folderView.Nodes.Add(desktopNode);
      navAddressBox.Items.Clear();
      navAddressBox.Items.Add(new BrowserComboItem(ShellBrowser.DesktopItem, 0));

      navAddressBox.CurrentItem = (BrowserComboItem)navAddressBox.Items[0];
      selectedNode = desktopNode;
      selectedItem = ShellBrowser.DesktopItem;

      ShellBrowser.DesktopItem.Expand(false, true, IntPtr.Zero);

      foreach (ShellItem desktopChild in ShellBrowser.DesktopItem.SubFolders) {
        TreeNode desktopChildNode = new TreeNode(desktopChild.Text, desktopChild.ImageIndex, desktopChild.SelectedImageIndex);
        desktopChildNode.Tag = desktopChild;
        desktopChildNode.Name = desktopChildNode.Text;

        navAddressBox.Items.Add(new BrowserComboItem(desktopChild, 1));

        if (desktopChildNode.Text == ShellBrowser.MyComputerName) {
            myCompNode = desktopChildNode;
            desktopChild.Expand(false, true, IntPtr.Zero);

            foreach (ShellItem myCompChild in desktopChild.SubFolders) {
              TreeNode myCompChildNode = new TreeNode(myCompChild.Text, myCompChild.ImageIndex, myCompChild.SelectedImageIndex);
              myCompChildNode.Tag = myCompChild;
              myCompChildNode.Name = myCompChildNode.Text;

              if (myCompChild.HasSubfolder)
                  myCompChildNode.Nodes.Add(string.Empty);

              navAddressBox.Items.Add(new BrowserComboItem(myCompChild, 2));
              desktopChildNode.Nodes.Add(myCompChildNode);
            }
        }
        else if (desktopChild.HasSubfolder)
          desktopChildNode.Nodes.Add(string.Empty);

        desktopNode.Nodes.Add(desktopChildNode);
      }
    }
  }
}