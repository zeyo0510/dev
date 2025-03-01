using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  partial class Mines
  {
    private bool allowMarkDoubt = true;
    /************************************************/
    public bool AllowMarkDoubt
    {
      get
      {
        return allowMarkDoubt;
      }
      set
      {
        allowMarkDoubt = value;
      }
    }
  }
}