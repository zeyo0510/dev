using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class TrackBarEx
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
      // TrackBarEx
      {
        base.Name       = "TrackBarEx";
        base.ClientSize = new Size(150, 030);
      }
    }
  }
}