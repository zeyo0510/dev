using System.ComponentModel;
using AudioCore;
/************************************************/
namespace B
{
  partial class FlowLayoutPanel1
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.components?.Dispose();
      }
      /************************************************/
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      // FlowLayoutPanel1
      {
        base.Name         = "FlowLayoutPanel1";
        base.AutoSize     = true;
        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.BackColor    = Color.Transparent;
        base.Margin       = new(5);
        base.Padding      = new(2);
        base.WrapContents = false;
      }
    }
  }
}