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
        return pnlRight.Number;
      }
      set
      {
        pnlRight.Number = value;
      }
    }
  }
}