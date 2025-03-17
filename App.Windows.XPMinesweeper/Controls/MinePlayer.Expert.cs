using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Expert()
    {
      this.Mines.Clear(30, 16, 99);
    }
  }
}