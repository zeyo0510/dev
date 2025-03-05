using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      using (Pen lightPen = new Pen(Color.White, 1))
      {
        // Top Border
        g.DrawLine(lightPen, rect.Left, rect.Top + 0, rect.Right - 1, rect.Top + 0);
        g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 1, rect.Top + 1);
        g.DrawLine(lightPen, rect.Left, rect.Top + 2, rect.Right - 1, rect.Top + 2);
        //Left Border
        g.DrawLine(lightPen, rect.Left + 0, rect.Top, rect.Left + 0, rect.Bottom - 1);
        g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 1);
        g.DrawLine(lightPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 1);
      }
      /************************************************/
      this.DrawFrame(g, this.panel1.Bounds);
      this.DrawFrame(g, this.panel2.Bounds);
    }
    
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
          g.DrawLine( darkPen, l - 2, b + 1, l - 2, t - 2); // Left
          g.DrawLine( darkPen, l - 1, b + 0, l - 1, t - 1); // Left
          g.DrawLine( darkPen, l - 2, t - 2, r + 1, t - 2); // Top
          g.DrawLine( darkPen, l - 1, t - 1, r + 0, t - 1); // Top
          g.DrawLine(lightPen, r + 2, t - 1, r + 2, b + 2); // Right
          g.DrawLine(lightPen, r + 1, t - 0, r + 1, b + 1); // Right
          g.DrawLine(lightPen, r + 2, b + 2, l - 1, b + 2); // Bottom
          g.DrawLine(lightPen, r + 1, b + 1, l - 0, b + 1); // Bottom
        }
      }
    }
  }
}