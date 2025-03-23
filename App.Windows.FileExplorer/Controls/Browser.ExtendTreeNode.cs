using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private bool ExtendTreeNode(TreeNode node, bool overwrite, IntPtr handle) {
      if (overwrite || !IsExtended(node)) {
        ShellItem nodeItem = (ShellItem)node.Tag;
        ShellBrowser.UpdateCondition.ContinueUpdate = false;

        if (nodeItem.Expand(false, true, handle)) {
          folderView.BeginUpdate();
          node.Nodes.Clear();

          TreeNode[] newNodesArray = new TreeNode[nodeItem.SubFolders.Count];
          for (int i = 0; i < newNodesArray.Length; i++) {
            newNodesArray[i] = new TreeNode(nodeItem.SubFolders[i].Text, nodeItem.SubFolders[i].ImageIndex, nodeItem.SubFolders[i].SelectedImageIndex);
            newNodesArray[i].Tag = nodeItem.SubFolders[i];

            if (nodeItem.SubFolders[i].HasSubfolder)
              newNodesArray[i].Nodes.Add(string.Empty);

            newNodesArray[i].Name = newNodesArray[i].Text;
          }

          node.Nodes.AddRange(newNodesArray);

          folderView.EndUpdate();
          return true;
        }
        else
          return false;
      }
      else
        return true;
    }
    /************************************************/
    private bool ExtendTreeNode(TreeNode node, bool overwrite) {
      return ExtendTreeNode(node, overwrite, IntPtr.Zero);
    }
  }
}