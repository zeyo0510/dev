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
        return this.allowMarkDoubt;
      }
      set
      {
        this.allowMarkDoubt = value;
      }
    }
  }
}