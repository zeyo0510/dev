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
        return pnlRight.Value;
      }
      set
      {
        pnlRight.Value = value;
      }
    }
  }
}