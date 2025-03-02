using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
  {
    public int RemainMineCount
    {
      get
      {
        return flagMineLED.Value;
      }
      set
      {
        flagMineLED.Value = value;
      }
    }
  }
}