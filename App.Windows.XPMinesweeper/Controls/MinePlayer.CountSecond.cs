using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    public int CountSecond
    {
      get
      {
        return durationMineLED.Value;
      }
      set
      {
        durationMineLED.Value = value;
      }
    }
  }
}