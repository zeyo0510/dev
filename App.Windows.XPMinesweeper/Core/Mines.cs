using System;
using App.Windows.XPMinesweeper.Controls;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  public sealed partial class Mines
  {
    internal MineStatus[,] MineArray;
    internal Mine[] mines;
    /************************************************/
    public Mines()
    {
      this.init();
    }
    /************************************************/
    private void init()
    {
      this.MineArray = new MineStatus[this.Width, this.Height];
      this.mines     = new Mine[this.Width * this.Height];
      /************************************************/
      for (int i = 0; i < this.Width; i++)
      {
        for (int j = 0; j < this.Height; j++)
        {
          this.MineArray[i, j] = MineStatus.NoMine;
          /************************************************/
          int index = (j * this.Width) + i;
          /************************************************/
          this.mines[index] = new Mine(index, this);
        }
      }
      /************************************************/
      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {
          int index = j * Width + i;
          /************************************************/
          this.mines[index].Init();
        }
      }
      /************************************************/
      this.gameState = GameState.Complete;
      /************************************************/
      #region 呴儂票濘1 (虴薹詢)
      int[] temp = new int[this.mines.Length];
      for (int i = 0; i < temp.Length; i++)
      {
        temp[i] = i;
      }
      int d = 0, e;
      int cc = Width * Height;
      for (int c = 0; c < Count; c++)
      {
        unchecked {
          Random random = new Random(Environment.TickCount / (c + 1) + Environment.TickCount - d);
          d = random.Next(cc - c);
          e = temp[d];
          if (mines[e].MineStatus != MineStatus.HasMine)
            mines[e].MineStatus = MineStatus.HasMine;
          else
            throw new MineException("呴儂票濘呾楊衄渣昫﹝");
          temp[d] = temp[cc - 1 - c];
        }
      }
      #endregion

      gameState = GameState.NotStarted;
    }

    public int MineRemainCount
    {
      get
      {
        if (GameState == GameState.Complete)
          return 0;

        int result = Count;
        for (int i = 0; i < Width; i++)
        {
          for (int j = 0; j < Height; j++)
          {
            Mine m = mines[j * Width + i];
            if (m.MineStatus == MineStatus.MarkedRight || m.MineStatus == MineStatus.MarkedWrong)
              result--;
          }
        }
        return result;
      }
    }

    internal void updateGameState()
    {
      int clearMine = 0;
      int cell = 0; //帤阼羲麼帤梓暮腔杅講
      foreach(Mine mine in mines)
      {
        if (mine.MineStatus == MineStatus.Exploded)
        {
          gameState = GameState.Fail;
          return;
        }
        if (mine.MineStatus == MineStatus.Clear || mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.MarkedRight)
          clearMine++;
        else if ((mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.NoMine) && !mine.Doubt)
          cell++;
      }
      if (clearMine == Width * Height)
        gameState = GameState.Complete;
      else if (cell != mines.Length)
        gameState = GameState.Processing;

      if (gameState == GameState.Complete)
      {
        foreach(Mine m in mines)
        {
          if (m.MineStatus == MineStatus.HasMine)
            m.MineStatus = MineStatus.MarkedRight;
        }
      }
    }

    public event MineStatusChangeEventHandler OnMineStatusChange;

    internal void mineStatusChange(Mine m)
    {
      if (OnMineStatusChange != null)
        OnMineStatusChange(this, new MineStatusChangeEventArgs(m));
    }

    internal void moveMineToOtherPlace(Mine m)
    {
      if (GameState == GameState.NotStarted && m.MineStatus == MineStatus.HasMine)
      {
        int[] temp = new int[Width * Height - Count];
        int j = 0;
        for (int i = 0; i < mines.Length; i++)
        {
          if (mines[i].MineStatus == MineStatus.NoMine)
          {
            temp[j] = i;
            j++;
          }
        }
        Random random = new Random(System.Environment.TickCount / (m.Index + 1) + System.Environment.TickCount);
        mines[temp[random.Next(temp.Length)]].MineStatus = MineStatus.HasMine;
        m.MineStatus = MineStatus.NoMine;
      }
    }
  }
}