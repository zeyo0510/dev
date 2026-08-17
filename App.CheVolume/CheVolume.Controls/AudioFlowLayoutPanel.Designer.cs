using System.ComponentModel;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class AudioFlowLayoutPanel
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
    // AudioFlowLayoutPanel
    {
      base.Name         = "AudioFlowLayoutPanel";
      base.AutoSize     = true;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      base.BackColor    = Color.FromArgb(252, 252, 252);
      // base.Margin       = new(5);
      base.Padding      = new(2);
      base.Tag          = this.AudioDevice.ID;
      base.WrapContents = false;
    }
  }
}