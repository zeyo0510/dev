using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  public partial class MineStatusChangeEventArgs: EventArgs
  {
    public MineStatusChangeEventArgs(Mine m)
    {
      this.Mine = m;
    }
  }
}