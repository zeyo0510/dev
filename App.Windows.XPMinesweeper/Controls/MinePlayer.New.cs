using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public void New()
    {
      if (this.DifficultyLevel == DifficultyLevel.Beginner) {
        this.Mines.Clear(09, 09, 10);
      } else if (this.DifficultyLevel == DifficultyLevel.Intermediate) {
        this.mines.Clear(16, 16, 40);
      } else if (this.DifficultyLevel == DifficultyLevel.Expert) {
        this.Mines.Clear(30, 16, 99);
      } else {
        this.Mines.Clear();
      }
      /************************************************/
      this.MineControl.AdjustSize();
      this.MineControl.Refresh();
      this.StopTimer();
      this.RemainMineCount = this.mines.MineRemainCount;
      this.Duration = 0;
      this.ChangeFace(1);
      
      base.ClientSize = new Size(this.MineControl.Width + 9 + 3 + 3 + 6, this.MineControl.Height + 9 + 2 + 36 + 2 + 6 + 3 + 4);
    }
  }
}