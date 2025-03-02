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
      this.DrawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9         , rect.Width - 9 - 6,                           36), false);
      this.DrawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9 + 36 + 6, rect.Width - 9 - 6, rect.Height - 9 - 36 - 6 - 6), true);
    }
    
    private void DrawFrame(Graphics g, Rectangle rect, bool frameWidthIsThree)
    {
      using (Pen lightPen = new Pen(Color.White, 1))
      {
        #region Top Border
        g.DrawLine(darkGrayPen, rect.Left, rect.Top + 0, rect.Right - 1, rect.Top + 0);
        g.DrawLine(darkGrayPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
        if (frameWidthIsThree)
          g.DrawLine(darkGrayPen, rect.Left, rect.Top + 2, rect.Right - 3, rect.Top + 2);
        #endregion
  
        #region Bottom Border
        g.DrawLine(lightPen, rect.Left + 1, rect.Bottom - 0, rect.Right, rect.Bottom - 0);
        g.DrawLine(lightPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
        if (frameWidthIsThree)
          g.DrawLine(lightPen, rect.Left + 3, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
        #endregion
  
        #region Left Border
        g.DrawLine(darkGrayPen, rect.Left + 0, rect.Top, rect.Left + 0, rect.Bottom - 1);
        g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2);
        if (frameWidthIsThree)
          g.DrawLine(darkGrayPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 3);
        #endregion
  
        #region Right Border
        g.DrawLine(lightPen, rect.Right - 0, rect.Top + 1, rect.Right - 0, rect.Bottom);
        g.DrawLine(lightPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
        if (frameWidthIsThree)
          g.DrawLine(lightPen, rect.Right - 2, rect.Top + 3, rect.Right - 2, rect.Bottom);
        #endregion
      }
    }
  }
}