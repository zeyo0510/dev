using System;
using App.Windows.XPMinesweeper.Core;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControl
  {
    private Mines mines;
    /************************************************/
    public Mines Mines
    {
      get
      {
        return mines;
      }
      set
      {
        mines = value;
        if (mines != null)
          mines.OnMineStatusChange += new MineStatusChangeEventHandler(mineStatusChange);
      }
    }
  }
}