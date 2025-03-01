using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControlPaintEventArgs
  {
    public Rectangle ClipRectangle
    {
      get; private set;
    }
  }
}