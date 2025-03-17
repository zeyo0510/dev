using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Beginner()
    {
      this.Mines.Clear(9, 9, 10);
    }
  }
}