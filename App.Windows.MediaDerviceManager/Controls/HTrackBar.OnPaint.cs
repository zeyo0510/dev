using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HTrackBar
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      if (this.Direction == HDirection.LeftRight) this.DrawHorizontal(g, false);
      if (this.Direction == HDirection.RightLeft) this.DrawHorizontal(g, true );
    }
    /************************************************/
    private void DrawHorizontal(Graphics g, bool reverse)
    {
      int trackHeight = 4;
      int thumbWidth  = 10;
      int thumbHeight = 20;
      
      /************************************************/
      
      int trackX = 0;
      int trackY = (this.Height - trackHeight) / 2;
      int trackW = this.Width;
      int trackH = trackHeight;
      Rectangle trackRect = new Rectangle(trackX, trackY, trackW, trackH);
      this.DrawTrack(g, trackRect);
      
      /************************************************/
      
      // 計算填充部分
      float percentage = (float)(this.Value - this.Minimum) / (this.Maximum - this.Minimum);
      int fillWidth = (int)(percentage * this.Width);
      
      Rectangle fillRect = reverse ? 
          new Rectangle(this.Width - fillWidth, trackRect.Top, fillWidth, trackRect.Height) :
          new Rectangle(0, trackRect.Top, fillWidth, trackRect.Height);
      
      // 繪製填充部分
      this.DrawValue(g, fillRect);
      
      /************************************************/
      
      int thumbX = (int)(percentage * (this.Width - thumbWidth));
      int thumbY = (this.Height - thumbHeight) / 2;
      if (reverse) thumbX = this.Width - thumbX - thumbWidth;
      int thumbW = thumbWidth;
      int thumbH = thumbHeight;
      Rectangle thumbRect = new Rectangle( thumbX, thumbY, thumbW, thumbH);
      this.DrawThumb(g, thumbRect);
    }
    /************************************************/
    private void DrawTrack(Graphics g, Rectangle rect)
    {
      using (Brush brush = new SolidBrush(Color.LightGray))
      {
        g.FillRectangle(brush, rect);
      }
    }
    /************************************************/
    private void DrawValue(Graphics g, Rectangle rect)
    {
      using (Brush brush = new SolidBrush(Color.DodgerBlue))
      {
        g.FillRectangle(brush, rect);
      }
    }
    /************************************************/
    private void DrawThumb(Graphics g, Rectangle rect)
    {
      using (Brush brush = new SolidBrush(Color.White))
      {
        g.FillRectangle(brush, rect);
      }
      /************************************************/
      using (Pen pen = new Pen(Color.DarkGray))
      {
        g.DrawRectangle(pen, rect);
      }
    }
  }
}