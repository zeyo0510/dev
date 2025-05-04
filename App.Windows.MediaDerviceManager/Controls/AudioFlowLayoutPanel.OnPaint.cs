using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class AudioFlowLayoutPanel
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      int l = rect.Left   + 0;
      int t = rect.Top    + 0;
      int r = rect.Right  - 1;
      int b = rect.Bottom - 1;
      /************************************************/
      using (Brush brush = new SolidBrush(Color.FromArgb(50, 50, 50)))
      {
        using (Pen pen = new Pen(brush, 3))
        {
          g.DrawRectangle(pen, l, t, r, b);
        }
      }
    }
    /************************************************/
    protected override void OnResize(EventArgs eventargs)
    {
      base.OnResize(eventargs);
      /************************************************/
      base.Invalidate();
    }
  }
}
