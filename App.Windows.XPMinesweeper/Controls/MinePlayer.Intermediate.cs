using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Intermediate()
    {
      this.DifficultyLevel = DifficultyLevel.Intermediate;
      /************************************************/
      this.New();
    }
  }
}