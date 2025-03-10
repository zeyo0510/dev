using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      this.DrawForeground(g, rect);
    }
    /************************************************/
    private void DrawForeground(Graphics g, Rectangle rect)
    {
      string num = "";
      /************************************************/
      if (this.value <  0) num = this.value.ToString("D2");
      if (this.value >= 0) num = this.value.ToString("D3");
      /************************************************/
      for (int i = 0; i < num.Length; i++)
      {
        Bitmap bmp = MineLED.bmp[num[i]];
        /************************************************/
        int x = rect.Left + (i * 13);
        int y = rect.Top  + (i * 00);
        /************************************************/
        g.DrawImage(bmp, x, y);
      }
    }
  }
}