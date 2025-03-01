using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class ResetButton
  {
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        darkGrayPen.Dispose();
        lightPen.Dispose();
        grayPen.Dispose();
        grayBrush.Dispose();
      }
      base.Dispose( disposing );
    }
    /************************************************/
    private void InitializeComponent()
    {
      Name = "resetButton";
      darkGrayPen = new Pen(darkGray, 1);
      lightPen = new Pen(Color.White, 1);
      grayPen = new Pen(gray, 1);
      grayBrush = new SolidBrush(gray);
    }
  }
}