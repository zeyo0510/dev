using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineButton
  {
    private bool pressed;
    /************************************************/
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      /************************************************/
      if (e.Button == MouseButtons.Left)
      {
        this.pressed = true;
        /************************************************/
        base.Invalidate();
      }
    }
    /************************************************/
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      /************************************************/
      if (e.Button == MouseButtons.Left)
      {
        this.pressed = false;
        /************************************************/
        base.Invalidate();
      }
    }
  }
}