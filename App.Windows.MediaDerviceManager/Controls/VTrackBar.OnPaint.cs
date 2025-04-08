using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VTrackBar
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      if (this.Direction == VDirection.TopBottom) this.DrawVertical(g, false);
      if (this.Direction == VDirection.BottomTop) this.DrawVertical(g, true );
    }
    /************************************************/
    private void DrawVertical(Graphics g, bool reverse)
    {
      int trackWidth = 4;
      int thumbHeight = 10;
      int thumbWidth = 20;
      
      /************************************************/
      
      int trackX = (this.Width - trackWidth) / 2;
      int trackY = 0;
      int trackW = trackWidth;
      int trackH = this.Height;
      Rectangle trackRect = new Rectangle(trackX, trackY, trackW, trackH);
      this.DrawTrack(g, trackRect);
      
      /************************************************/
      
      // 計算填充部分
      float percentage = (float)(this.Value - this.MinValue) / (this.MaxValue - this.MinValue);
      int fillHeight = (int)(percentage * this.Height);
      
      Rectangle fillRect = reverse ? 
        new Rectangle(trackRect.Left, this.Height - fillHeight, trackRect.Width, fillHeight) :
        new Rectangle(trackRect.Left, 0, trackRect.Width, fillHeight);
      
      // 繪製填充部分
      this.DrawValue(g, fillRect);
      
      /************************************************/
      
      int thumbX = (this.Width - thumbWidth) / 2;
      int thumbY = (int)(percentage * (this.Height - thumbHeight));
      if (reverse) thumbY = this.Height - thumbY - thumbHeight;
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