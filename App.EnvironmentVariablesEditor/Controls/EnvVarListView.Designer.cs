using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Controls
{
  partial class EnvVarListView
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
      // EnvVarListView
      {
        base.Name          = "EnvVarListView";
        base.CheckBoxes    = true;
        base.FullRowSelect = true;
        base.GridLines     = true;
        base.View          = View.Details;
        /************************************************/
        base.Columns.Add("Variable");
        base.Columns.Add("Value");
      }
    }
  }
}