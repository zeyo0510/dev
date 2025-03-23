using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void ChangeNavBarItem(SelectedFolderChangedEventArgs e) {
      if (!navAddressBox.CurrentItem.ShellItem.Equals(e.Item)) {
        int currentIndex = navAddressBox.Items.IndexOf(navAddressBox.CurrentItem);
        BrowserComboItem currentItem = navAddressBox.CurrentItem;
        int maxIndent = folderView.IsParentNode(myCompNode, selectedNode) ? 3 : 2;
        TreeNode[] path;
  
        bool isMyCompChild = folderView.IsParentNode(myCompNode, e.Node);
        if (selectedNode.Nodes.Contains(e.Node) &&
            ((isMyCompChild && e.Node.Level >= 3) || (!isMyCompChild && e.Node.Level >= 2))) {
              navAddressBox.Items.Insert(currentIndex + 1, new BrowserComboItem(e.Item, e.Node.Level));
              navAddressBox.SelectedIndex = currentIndex + 1;
        } else if (folderView.IsParentNode(e.Node, selectedNode, out path)) {
          if (e.Node.Equals(desktopNode))
            navAddressBox.SelectedIndex = 0;
          else if (e.Node.Equals(myCompNode))
            navAddressBox.SelectedIndex = desktopNode.Nodes.IndexOf(myCompNode) + 1;
          else
            navAddressBox.SelectedIndex = currentIndex - path.Length + 1;
  
          while (currentItem.Text != e.Node.Text && currentItem.Indent >= maxIndent) {
            navAddressBox.Items.Remove(currentItem);
            currentIndex--;
            currentItem = (BrowserComboItem)navAddressBox.Items[currentIndex];
          }
        } else {
          while (currentItem.Indent >= maxIndent && !e.Node.Equals(myCompNode)) {
            navAddressBox.Items.Remove(currentItem);
            currentIndex--;
            currentItem = (BrowserComboItem)navAddressBox.Items[currentIndex];
          }
  
          if (folderView.IsParentNode(myCompNode, e.Node, out path)) {
            if (path.Length > 2) {
              int startIndex = desktopNode.Nodes.IndexOf(myCompNode) + myCompNode.Nodes.IndexOf(path[1]) + 1;
  
              for (int i = 2; i < path.Length; i++) {
                navAddressBox.Items.Insert(startIndex + i, new BrowserComboItem((ShellItem)path[i].Tag, i + 1));
              }
  
              navAddressBox.SelectedIndex = startIndex + path.Length - 1;
            } else {
              navAddressBox.SelectedIndex = desktopNode.Nodes.IndexOf(myCompNode) + myCompNode.Nodes.IndexOf(e.Node) + 2;
            }
          } else if (folderView.IsParentNode(desktopNode, e.Node, out path)) {
            if (path.Length > 2) {
              int startIndex = desktopNode.Nodes.IndexOf(path[1]);
  
              for (int i = 2; i < path.Length; i++) {
                navAddressBox.Items.Insert(startIndex + i, new BrowserComboItem((ShellItem)path[i].Tag, i));
              }
  
              navAddressBox.SelectedIndex = startIndex + path.Length - 1;
            } else {
              if (desktopNode.Nodes.IndexOf(e.Node) <= desktopNode.Nodes.IndexOf(myCompNode))
                navAddressBox.SelectedIndex = desktopNode.Nodes.IndexOf(e.Node) + 1;
              else
                navAddressBox.SelectedIndex = desktopNode.Nodes.IndexOf(e.Node) + myCompNode.Nodes.Count + 1;
            }
          } else
            navAddressBox.SelectedIndex = 0;
          }
        }
      }

  }
}