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
      this.DrawBorder(g, rect);
      /************************************************/
      this.DrawBackground(g, rect);
      /************************************************/
      this.DrawForeground(g, rect);
    }
    /************************************************/
    private void DrawBorder(Graphics g, Rectangle rect)
    {
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      using (Pen lightPen = new Pen(Color.White))
      {
        using (Pen darkPen = new Pen(Color.Gray))
        {
          for (int i = 0; i < 1; i++)
          {
            g.DrawLine(darkPen , l + 0, b - 1, l + 0, t + 0); // Left
            g.DrawLine(darkPen , l + 0, t + 0, r - 1, t + 0); // Top
            g.DrawLine(lightPen, r - 0, t + 1, r - 0, b - 0); // Right
            g.DrawLine(lightPen, r - 0, b - 0, l + 1, b - 0); // Bottom
            /************************************************/
            l++;
            t++;
            r--;
            b--;
          }
        }
      }
    }
    /************************************************/
    private void DrawBackground(Graphics g, Rectangle rect)
    {
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      using (Brush brush = new SolidBrush(Color.Black))
      {
        g.FillRectangle(brush, l + 1, t + 1, r - 1, b - 1);
      }
    }
    /************************************************/
    private void DrawForeground(Graphics g, Rectangle rect)
    {
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      string num = "";
      /************************************************/
      if (this.Value <  0) num = this.Value.ToString("D2");
      if (this.Value >= 0) num = this.Value.ToString("D3");
      /************************************************/
      for (int i = 0; i < num.Length; i++)
      {
        Bitmap bmp = MineLED.bmp[num[i]];
        /************************************************/
        int x = rect.Left + (i * 13);
        int y = rect.Top  + (i * 00);
        /************************************************/
        g.DrawImage(bmp, x + 1, y + 1);
      }
    }
  }
}