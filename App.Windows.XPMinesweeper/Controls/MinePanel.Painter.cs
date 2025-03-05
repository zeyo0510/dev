using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
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
      this.DrawFrame(g, this.panel1.Bounds);
      this.DrawFrame(g, this.panel2.Bounds);
    }
    /************************************************/
    private void DrawBorder(Graphics g, Rectangle rect)
    {
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      using (Pen lightPen = new Pen(Color.White, 1))
      {
        using (Pen darkPen = new Pen(Color.Gray, 1))
        {
          for (int i = 0; i < 3; i++)
          {
            g.DrawLine(lightPen, l + 0 + i, b - 1 - i, l + 0 + i, t + 0 + i); // Left
            g.DrawLine(lightPen, l + 0 + i, t + 0 + i, r - 1 - i, t + 0 + i); // Top
            g.DrawLine( darkPen, r - 0 - i, t + 1 + i, r - 0 - i, b - 0 - i); // Right
            g.DrawLine( darkPen, r - 0 - i, b - 0 - i, l + 1 + i, b - 0 - i); // Bottom
          }
        }
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
      using (Pen lightPen = new Pen(Color.White, 1))
      {
        using (Pen darkPen = new Pen(Color.Gray, 1))
        {
          for (int i = 0; i < 2; i++)
          {
            g.DrawLine( darkPen, l - 1 - i, b + 0 + i, l - 1 - i, t - 1 - i); // Left
            g.DrawLine( darkPen, l - 1 - i, t - 1 - i, r + 0 + i, t - 1 - i); // Top
            g.DrawLine(lightPen, r + 1 + i, t - 0 - i, r + 1 + i, b + 1 + i); // Right
            g.DrawLine(lightPen, r + 1 + i, b + 1 + i, l - 0 - i, b + 1 + i); // Bottom
          }
        }
      }
    }
  }
}