using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class HTrackBar : Control
  {
    public HTrackBar()
    {
      this.SetStyle(ControlStyles.DoubleBuffer |  ControlStyles.UserPaint |  ControlStyles.AllPaintingInWmPaint, true);
      /************************************************/
      this.InitializeComponent();
    }
  }
}