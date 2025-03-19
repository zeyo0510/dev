using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public int Duration
    {
      get
      {
        int retValue = this.durationMineLED.Value;
        /************************************************/
        return retValue;
      }
      private set
      {
        this.durationMineLED.Value = value;
      }
    }
  }
}