using System;
using App.Windows.XPMinesweeper.Controls;
/************************************************/
namespace App.Windows.XPMinesweeper.Main
{
  partial class MainForm
  {
    public void UpdateUI()
    {
      this.    beginnerMenuItem.Checked = this.minePlayer1.DifficultyLevel == DifficultyLevel.Beginner;
      this.intermediateMenuItem.Checked = this.minePlayer1.DifficultyLevel == DifficultyLevel.Intermediate;
      this.      expertMenuItem.Checked = this.minePlayer1.DifficultyLevel == DifficultyLevel.Expert;
      this.      customMenuItem.Checked = this.minePlayer1.DifficultyLevel == DifficultyLevel.Cusrom;
      this.marksMenuItem.Checked = this.mines.AllowMarkDoubt;
    }
  }
}