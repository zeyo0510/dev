using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Dialogs
{
  partial class AdjuestDailog
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
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
      this.components = new Container();
      /************************************************/
      this.guiTimer = new Timer(this.components);
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
      // AdjuestDailog
      {
        base.Name          = "AdjuestDailog";
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Text          = "Adjuest";
      }
    }
  }
}