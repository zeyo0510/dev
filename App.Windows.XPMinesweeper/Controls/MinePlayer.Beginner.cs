using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Beginner()
    {
      this.DifficultyLevel = DifficultyLevel.Beginner;
      /************************************************/
      this.New();
    }
  }
}