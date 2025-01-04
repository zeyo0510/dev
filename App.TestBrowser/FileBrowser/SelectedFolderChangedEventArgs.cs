using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser {
  public class SelectedFolderChangedEventArgs : EventArgs {
    private TreeNode node;
    private ShellItem item;

    public SelectedFolderChangedEventArgs(TreeNode node) {
      this.node = node;
      item = (ShellItem)node.Tag;
    }

    // The TreeNode of the new current selected directory
    public TreeNode Node { get { return node; } }

    // The ShellItem of the new current selected directory
    public ShellItem Item { get { return item; } }

    // The full path to the new current selected directory
    public string Path { get { return ShellItem.GetRealPath(item); } }
  }
}