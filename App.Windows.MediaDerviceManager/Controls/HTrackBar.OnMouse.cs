using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HTrackBar
  {
    private bool _dragging = false;
    /************************************************/
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      /************************************************/
      if (e.Button == MouseButtons.Left)
      {
        this._dragging = true;
        /************************************************/
        this.UpdateValueFromMousePosition(e.X, e.Y);
      }
    }
    /************************************************/
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      /************************************************/
      if (this._dragging)
      {
        this.UpdateValueFromMousePosition(e.X, e.Y);
      }
    }
    /************************************************/
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      /************************************************/
      this._dragging = false;
    }
    /************************************************/
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      /************************************************/
      this.Value += e.Delta / 24;
      /************************************************/
      base.Invalidate();
    }
    /************************************************/
    private void UpdateValueFromMousePosition(int x, int y)
    {
      float percentage = 0f;
      /************************************************/
      if (this.Direction == HDirection.LeftRight) percentage = (float)(             x) / this.Width;
      if (this.Direction == HDirection.RightLeft) percentage = (float)(this.Width - x) / this.Width;
      /************************************************/
      percentage = Math.Min(1, percentage);
      percentage = Math.Max(0, percentage);
      /************************************************/
      this.Value = (int)(this.MinValue + percentage * (this.MaxValue - this.MinValue));
      /************************************************/
      base.Invalidate();
    }
  }
}