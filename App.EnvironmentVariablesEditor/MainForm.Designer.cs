using System;
/************************************************/
namespace App.EnvironmentVariablesEditor.Main
{
  partial class MainForm
  {
    private System.ComponentModel.IContainer components = null;
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
      // MainForm
      {
        base.Name = "MainForm";
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Text = "App.EnvironmentVariablesEditor";
      }
    }
  }
}