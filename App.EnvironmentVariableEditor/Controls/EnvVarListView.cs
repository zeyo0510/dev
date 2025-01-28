using System;
using System.Collections.Generic;
using System.Management;
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
      foreach (KeyValuePair<string, string> _ in EnvVar.Query())
      {
        string[] arr = new string[] {
          _.Key.ToString(),
          _.Value.ToString(),
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
    /************************************************/
    public void DeleteSelectedVariables()
    {
      foreach (ListViewItem entry in this.SelectedItems)
      {
        
      }
    }
  }
}