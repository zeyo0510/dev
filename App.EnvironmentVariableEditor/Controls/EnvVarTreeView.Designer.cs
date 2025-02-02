using System;
using System.ComponentModel;
/************************************************/
namespace App.EnvironmentVariableEditor.Controls
{
  partial class EnvVarTreeView
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
      // EnvVarTreeView
      {
        base.Name = "EnvVarTreeView";
      }
    }
  }
}