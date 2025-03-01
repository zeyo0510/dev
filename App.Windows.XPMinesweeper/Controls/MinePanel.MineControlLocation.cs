using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
  {
    public Point MineControlLocation
    {
      get
      {
        return new Point(ClientRectangle.Left + 9 + 3, ClientRectangle.Top + 9 + 36 + 6 + 3);
      }
    }
  }
}