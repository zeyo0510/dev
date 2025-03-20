using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  // 本遊戲：地雷數
  partial class Mines
  {
    private int m_Count = 10;
    /************************************************/
    public int Count
    {
      get
      {
        return this.m_Count;
      }
    }
  }
}