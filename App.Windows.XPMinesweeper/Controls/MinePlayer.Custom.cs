using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Custom(int width, int hegiht, int count)
    {
      this.DifficultyLevel = DifficultyLevel.Cusrom;
      /************************************************/
      this.Mines.Clear(width, hegiht, count);
    }
  }
}