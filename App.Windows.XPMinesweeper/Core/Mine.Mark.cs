using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  // 炸彈：標記
  partial class Mine
  {
    public void Mark()
    {
      switch (MineStatus)
      {
        case MineStatus.NoMine:
          if (Doubt)
            Doubt = false;
          else
            MineStatus = MineStatus.MarkedWrong;
          break;
        case MineStatus.HasMine:
          if (Doubt)
            Doubt = false;
          else
            MineStatus = MineStatus.MarkedRight;
          break;
        case MineStatus.MarkedWrong:
          if (mines.AllowMarkDoubt)
            Doubt = true;
          MineStatus = MineStatus.NoMine;
          break;
        case MineStatus.MarkedRight:
          if (mines.AllowMarkDoubt)
            Doubt = true;
          MineStatus = MineStatus.HasMine;
          break;
        default:
          return;
      }
      /************************************************/
      this.mines.mineStatusChange(this);
    }
  }
}