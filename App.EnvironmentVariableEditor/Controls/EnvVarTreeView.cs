using System;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Core;
/************************************************/
namespace App.EnvironmentVariableEditor.Controls
{
  public partial class EnvVarTreeView : TreeView
  {
    public EnvVarTreeView()
    {
      this.InitializeComponent();
      /************************************************/
      this.Reload();
    }
    protected override void OnAfterSelect(TreeViewEventArgs e)
    {
      TreeNode node = e.Node;
      /************************************************/
      if (node.Nodes.Count >= 1)
      {
        node = node.Nodes[0];
      }
      /************************************************/
      this.CurrentAccount = (EnvVarAccount)node.Tag;
      /************************************************/
      base.OnAfterSelect(e);
    }
    /************************************************/
    public void Reload()
    {
      base.Nodes.Clear();
      /************************************************/
      TreeNode   userTreeNode = base.Nodes.Add("User");
      TreeNode systemTreeNode = base.Nodes.Add("System");
      /************************************************/
      foreach (EnvVarAccount _ in EnvVar.Accounts)
      {
        TreeNode node = new TreeNode(_.Name);
        {
          node.Tag = _;
        }
        /************************************************/
        if (_.Type == EnvVarAccountType.User  )   userTreeNode.Nodes.Add(node);
        if (_.Type == EnvVarAccountType.System) systemTreeNode.Nodes.Add(node);
      }
    }
  }
}