using System;
using System.Management;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Controls
{
  public partial class EnvVarListView : ListView
  {
    public EnvVarListView()
    {
      this.InitializeComponent();
      /************************************************/
      this.ReloadEnvironmentVariable();
    }
    /************************************************/
    public void ReloadEnvironmentVariable()
    {
      base.Items.Clear();
      /************************************************/
      string query = "SELECT * FROM Win32_Environment";
      /************************************************/
      ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
      /************************************************/
      foreach (ManagementObject envVar in searcher.Get())
      {
        string[] arr = new string[] {
          envVar["Name"].ToString(),
          envVar["VariableValue"].ToString(),
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