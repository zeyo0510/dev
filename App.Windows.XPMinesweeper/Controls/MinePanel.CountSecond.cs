using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
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