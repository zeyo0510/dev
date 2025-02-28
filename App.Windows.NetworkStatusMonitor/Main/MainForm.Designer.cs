using System.ComponentModel;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Main
{
  partial class MainForm
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
      // 
      // MainForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Text = "Windows.NetworkStatusMonitor";
      this.Name = "MainForm";
    }
  }
}