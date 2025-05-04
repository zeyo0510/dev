using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class AudioFlowLayoutPanel
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
      // AudioFlowLayoutPanel
      {
        base.Name         = "AudioFlowLayoutPanel";
        base.AutoSize     = true;
        base.BackColor    = Color.Transparent;
        base.ClientSize   = new Size(0, 0);
        base.Padding      = new Padding(2);
        base.WrapContents = false;
      }
    }
  }
}