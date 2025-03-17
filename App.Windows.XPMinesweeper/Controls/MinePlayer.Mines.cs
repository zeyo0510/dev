using System;
using App.Windows.XPMinesweeper.Core;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
  {
    private Mines mines;
    /************************************************/
    public Mines Mines
    {
      get
      {
        return this.mines;
      }
      set
      {
        this.mines = value;
      }
    }
  }
}