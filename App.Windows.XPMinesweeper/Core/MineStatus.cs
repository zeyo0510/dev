using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  public enum MineStatus
  {
    NoMine      = 0,
    HasMine     = 1,
    MarkedWrong = 2,
    MarkedRight = 3,
    Clear       = 4,
    Exploded    = 5,
  }
}