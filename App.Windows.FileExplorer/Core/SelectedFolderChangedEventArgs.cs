using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  public class SelectedFolderChangedEventArgs : EventArgs
  {
    public SelectedFolderChangedEventArgs(TreeNode node)
    {
      this.Node = node;
      this.Item = (ShellItem)node.Tag;
    }
    /************************************************/
    public TreeNode Node
    {
      get; private set;
    }
    /************************************************/
    public ShellItem Item
    {
      get; private set;
    }
    /************************************************/
    public string Path
    {
      get
      {
        return ShellItem.GetRealPath(this.Item);
      }
    }
  }
}