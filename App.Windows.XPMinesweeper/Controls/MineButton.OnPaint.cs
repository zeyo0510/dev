using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineButton
  {
    protected override void OnPaint(PaintEventArgs pevent)
    {
      base.OnPaint(pevent);
      /************************************************/
      Graphics g = pevent.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      this.DrawBorder(g, rect);
      /************************************************/
      this.DrawBackground(g, rect);
      /************************************************/
      this.DrawFrame(g, rect);
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
            g.DrawLine(darkPen, l + 0, b - 1, l + 0, t + 0); // Left
            g.DrawLine(darkPen, l + 0, t + 0, r - 1, t + 0); // Top
            g.DrawLine(darkPen, r - 0, t + 1, r - 0, b - 0); // Right
            g.DrawLine(darkPen, r - 0, b - 0, l + 1, b - 0); // Bottom
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
      using (Brush brush = new SolidBrush(Color.Silver))
      {
        g.FillRectangle(brush, l + 1, t + 1, r - 1, b - 1);
      }
    }
    /************************************************/
    private void DrawFrame(Graphics g, Rectangle rect)
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
          if (this._pressed)
          {
            for (int i = 0; i < 1; i++)
            {
              g.DrawLine(darkPen, l + 1, b - 1, l + 1, t + 1); // Left
              g.DrawLine(darkPen, l + 1, t + 1, r - 1, t + 1); // Top
//            g.DrawLine(darkPen, r - 1, t + 1, r - 1, b - 1); // Right
//            g.DrawLine(darkPen, r - 1, b - 1, l + 1, b - 1); // Bottom
              /************************************************/
              l++;
              t++;
              r--;
              b--;
            }
          }
          /************************************************/
          if (!this._pressed)
          {
            for (int i = 0; i < 2; i++)
            {
              g.DrawLine(lightPen, l + 1, b - 2, l + 1, t + 1); // Left
              g.DrawLine(lightPen, l + 1, t + 1, r - 2, t + 1); // Top
              g.DrawLine(darkPen , r - 1, t + 2, r - 1, b - 1); // Right
              g.DrawLine(darkPen , r - 1, b - 1, l + 2, b - 1); // Bottom
              /************************************************/
              l++;
              t++;
              r--;
              b--;
            }
          }
        }
      }
    }
    /************************************************/
    private void DrawForeground(Graphics g, Rectangle rect)
    {
      if (base.Image == null) return;
      /************************************************/
      int offset = 0;
      /************************************************/
      if ( this._pressed) offset = 1;
      if (!this._pressed) offset = 0;
      /************************************************/
      // 5 = floor(rect.Width - base.Image.Width) / 2
      //   = floor(26         - 17              ) / 2
      int x = rect.Left + 5 + offset;
      int y = rect.Top  + 5 + offset;
      /************************************************/
      g.DrawImage(base.Image, x, y);
    }
  }
}