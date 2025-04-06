using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class TrackBarEx : Control
  {
    private bool _dragging = false;
      
    public TrackBarEx()
    {
      this.SetStyle(ControlStyles.DoubleBuffer | 
                   ControlStyles.UserPaint | 
                   ControlStyles.AllPaintingInWmPaint, true);
      this.Height = 30;
      this.Width = 150;
    }
    
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button == MouseButtons.Left)
      {
        _dragging = true;
        UpdateValueFromMousePosition(e.X, e.Y);
      }
    }
    
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (_dragging)
      {
        UpdateValueFromMousePosition(e.X, e.Y);
      }
    }
    
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      _dragging = false;
    }
    
    private void UpdateValueFromMousePosition(int x, int y)
    {
      float percentage = 0f;
      
      switch (_direction)
      {
        case LayoutDirection.LeftToRight:
          percentage = (float)x / this.Width;
          break;
        case LayoutDirection.RightToLeft:
          percentage = (float)(this.Width - x) / this.Width;
          break;
        case LayoutDirection.TopToBottom:
          percentage = (float)y / this.Height;
          break;
        case LayoutDirection.BottomToTop:
          percentage = (float)(this.Height - y) / this.Height;
          break;
      }
      
      // 確保百分比在0到1之間
      percentage = Math.Max(0, Math.Min(1, percentage));
      Value = (int)(_min + percentage * (_max - _min));
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Graphics g = e.Graphics;
      
      // 根據方向調整繪製邏輯
      switch (_direction)
      {
        case LayoutDirection.LeftToRight:
          DrawHorizontal(g, false);
          break;
        case LayoutDirection.RightToLeft:
          DrawHorizontal(g, true);
          break;
        case LayoutDirection.TopToBottom:
          DrawVertical(g, false);
          break;
        case LayoutDirection.BottomToTop:
          DrawVertical(g, true);
          break;
      }
    }
    
    private void DrawHorizontal(Graphics g, bool reverse)
    {
      int trackHeight = 4;
      int thumbWidth = 10;
      int thumbHeight = 20;
      
      // 計算軌道位置
      Rectangle trackRect = new Rectangle(
          0, 
          (this.Height - trackHeight) / 2,
          this.Width,
          trackHeight);
      
      // 繪製軌道背景
      using (Brush trackBrush = new SolidBrush(Color.LightGray))
      {
          g.FillRectangle(trackBrush, trackRect);
      }
      
      // 計算填充部分
      float percentage = (float)(_value - _min) / (_max - _min);
      int fillWidth = (int)(percentage * this.Width);
      
      Rectangle fillRect = reverse ? 
          new Rectangle(this.Width - fillWidth, trackRect.Top, fillWidth, trackRect.Height) :
          new Rectangle(0, trackRect.Top, fillWidth, trackRect.Height);
      
      // 繪製填充部分
      using (Brush fillBrush = new SolidBrush(Color.DodgerBlue))
      {
          g.FillRectangle(fillBrush, fillRect);
      }
      
      // 計算拇指位置
      int thumbX = (int)(percentage * (this.Width - thumbWidth));
      if (reverse) thumbX = this.Width - thumbX - thumbWidth;
      
      Rectangle thumbRect = new Rectangle(
          thumbX,
          (this.Height - thumbHeight) / 2,
          thumbWidth,
          thumbHeight);
      
      // 繪製拇指按鈕
      using (Brush thumbBrush = new SolidBrush(Color.White))
      {
          g.FillRectangle(thumbBrush, thumbRect);
          g.DrawRectangle(Pens.DarkGray, thumbRect);
      }
    }
    
    private void DrawVertical(Graphics g, bool reverse)
    {
      int trackWidth = 4;
      int thumbHeight = 10;
      int thumbWidth = 20;
      
      // 計算軌道位置
      Rectangle trackRect = new Rectangle(
          (this.Width - trackWidth) / 2,
          0,
          trackWidth,
          this.Height);
      
      // 繪製軌道背景
      using (Brush trackBrush = new SolidBrush(Color.LightGray))
      {
        g.FillRectangle(trackBrush, trackRect);
      }
      
      // 計算填充部分
      float percentage = (float)(_value - _min) / (_max - _min);
      int fillHeight = (int)(percentage * this.Height);
      
      Rectangle fillRect = reverse ? 
        new Rectangle(trackRect.Left, this.Height - fillHeight, trackRect.Width, fillHeight) :
        new Rectangle(trackRect.Left, 0, trackRect.Width, fillHeight);

      
      // 繪製填充部分
      using (Brush fillBrush = new SolidBrush(Color.DodgerBlue))
      {
        g.FillRectangle(fillBrush, fillRect);
      }
      
      // 計算拇指位置
      int thumbY = (int)(percentage * (this.Height - thumbHeight));
      if (reverse) thumbY = this.Height - thumbY - thumbHeight;
      
      Rectangle thumbRect = new Rectangle(
          (this.Width - thumbWidth) / 2,
          thumbY,
          thumbWidth,
          thumbHeight);
      
      // 繪製拇指按鈕
      using (Brush thumbBrush = new SolidBrush(Color.White))
      {
        g.FillRectangle(thumbBrush, thumbRect);
        g.DrawRectangle(Pens.DarkGray, thumbRect);
      }
    }
  }
}