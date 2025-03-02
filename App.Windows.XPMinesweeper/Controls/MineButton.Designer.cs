using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineButton
  {
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        darkGrayPen.Dispose();
        lightPen.Dispose();
        grayPen.Dispose();
        grayBrush.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.darkGrayPen = new Pen(darkGray, 1);
      this.lightPen    = new Pen(Color.White, 1);
      this.grayPen     = new Pen(gray, 1);
      this.grayBrush   = new SolidBrush(gray);
      /************************************************/
      base.Name = "resetButton";
    }
  }
}