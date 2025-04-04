using System;
using System.ComponentModel;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class AudioSessionManagerPanel
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
      // AudioSessionManagerPanel
      base.Name = "AudioSessionManagerPanel";
      base.AutoScroll = true;
    }
  }
}