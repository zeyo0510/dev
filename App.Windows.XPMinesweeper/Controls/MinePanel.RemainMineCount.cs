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
        return pnlLeft.Value;
      }
      set
      {
        pnlLeft.Value = value;
      }
    }
  }
}