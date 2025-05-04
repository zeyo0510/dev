using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace B
{
  partial class AudioFlowLayoutPanel
  {
    private IContainer components = null;
        
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
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      base.Padding = new Padding(2);
      base.WrapContents = false;
      base.Paint += new PaintEventHandler(_OnPaint);
      base.Resize += new System.EventHandler(_OnResize);
    }
  }
}