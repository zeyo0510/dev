using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class VTrackBar : Control
  {
    public VTrackBar()
    {
      this.InitializeComponent();
      /************************************************/
      base.SetStyle(ControlStyles.DoubleBuffer, true);
    }
    /************************************************/
    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
//    base.ScaleControl(factor, specified);
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
  }
}