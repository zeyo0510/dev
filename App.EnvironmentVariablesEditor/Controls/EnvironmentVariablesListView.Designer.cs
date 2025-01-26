using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Controls
{
  partial class EnvironmentVariablesListView
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      // EnvironmentVariablesListView
      {
        base.Name          = "EnvironmentVariablesListView";
        base.CheckBoxes    = true;
        base.FullRowSelect = true;
        base.GridLines     = true;
        base.View          = View.Details;
        /************************************************/
        base.Columns.Add("Variable");
        base.Columns.Add("Value");
        
        // TODO: test
        base.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "A", "B" }));
        base.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { "A", "B" }));
      }
    }
  }
}