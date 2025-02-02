using System;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Core;
/************************************************/
namespace App.EnvironmentVariableEditor.Controls
{
  public partial class EnvVarListView : ListView
  {
    public EnvVarListView()
    {
      this.InitializeComponent();
      /************************************************/
      base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      /************************************************/
      this.ReloadEnvironmentVariable();
    }
    /************************************************/
    public void ReloadEnvironmentVariable()
    {
      base.Items.Clear();
      /************************************************/
      foreach (EnvVarEntry _ in EnvVar.Query())
      {
        string[] arr = new string[] {
          _.Variable,
          _.Value,
        };
        /************************************************/
        ListViewItem item = new ListViewItem(arr);
        /************************************************/
        this.Items.Add(item);
        /************************************************/
      }
      /************************************************/
      this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }
    public void ReloadEnvironmentVariable(string account)
    {
      base.Items.Clear();
      /************************************************/
      foreach (EnvVarEntry _ in EnvVar.Query(account))
      {
        string[] arr = new string[] {
          _.Variable,
          _.Value,
        };
        /************************************************/
        ListViewItem item = new ListViewItem(arr);
        /************************************************/
        this.Items.Add(item);
        /************************************************/
      }
      /************************************************/
      this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }
  }
}