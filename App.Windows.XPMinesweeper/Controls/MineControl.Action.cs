using System;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Core;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControl
  {
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      /************************************************/
      setCellState(MouseButtons.None);
    }
    /************************************************/
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      /************************************************/
      setCellState(MouseButtons.None);
    }
    /************************************************/
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      /************************************************/
      setCellState(mouseButton, getRect(e.X, e.Y));
    }
    /************************************************/
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      /************************************************/
      Mine m = getMine(e.X, e.Y);
      if (m == null)
        return;

      twoKeyDoubleClick = false;
      setCellState(e.Button, getRect(e.X, e.Y));
    }
    /************************************************/
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      /************************************************/
      Mine m = getMine(e.X, e.Y);
      if (m == null || !Enabled)
        return;

      if (mouseButton != MouseButtons.None && (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted))
      {
        if (compareMouseButton(mouseButton, mbLeftnRight))
        {
          m.QuickDig();
          AfterDigOrMark(this, EventArgs.Empty);
        }
        else if (compareMouseButton(e.Button, MouseButtons.Left) && !twoKeyDoubleClick)
        {
          m.Dig(true);
          AfterDigOrMark(this, EventArgs.Empty);
        }
        else if (compareMouseButton(e.Button, MouseButtons.Right) && !twoKeyDoubleClick)
        {
          m.Mark();
          AfterDigOrMark(this, EventArgs.Empty);
        }

        if (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted)
        {
          Enabled = false;
          Refresh();
        }
      }

      if (compareMouseButton(mouseButton, mbLeftnRight))
        mouseButton = e.Button == MouseButtons.Left ? MouseButtons.Right : MouseButtons.Left;
      else
        mouseButton = MouseButtons.None;
      setCellState(mouseButton, getRect(e.X, e.Y));
    }

  }
}