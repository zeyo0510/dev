using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  partial class Mines
  {
    private GameState gameState = GameState.NotStarted;
    /************************************************/
    public GameState GameState
    {
      get
      {
        return gameState;
      }
    }
  }
}