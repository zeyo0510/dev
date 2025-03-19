using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void Expert()
    {
      this.DifficultyLevel = DifficultyLevel.Expert;
      /************************************************/
      this.New();
    }
  }
}