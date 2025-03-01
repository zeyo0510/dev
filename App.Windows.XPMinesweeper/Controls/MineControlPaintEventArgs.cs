using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MineControlPaintEventArgs : EventArgs
  {
    public MineControlPaintEventArgs(Graphics graphics, Rectangle clipRectangle, int x, int y)
    {
      this.Graphics = graphics;
      /************************************************/
      this.ClipRectangle = clipRectangle;
      /************************************************/
      this.X = x;
      this.Y = y;
    }
  }
}