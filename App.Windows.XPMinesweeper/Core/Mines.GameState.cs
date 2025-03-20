using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  // 本遊戲：狀態
  partial class Mines
  {
    private GameState gameState = GameState.NotStarted;
    /************************************************/
    public GameState GameState
    {
      get
      {
        return this.gameState;
      }
    }
  }
}