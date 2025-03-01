using System;
using App.Windows.XPMinesweeper.Controls;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  public sealed partial class Mines
  {
    internal MineStatus[,] MineArray;
    internal Mine[] mines;

    public Mines()
    {
      init();
    }

    private void init()
    {
      MineArray = new MineStatus[Width, Height];
      mines = new Mine[Width * Height];
      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {
          MineArray[i, j] = MineStatus.NoMine;
          int index = j * Width + i;
          mines[index] = new Mine(index, this);
        }
      }
      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {
          int index = j * Width + i;
          mines[index].Init();
        }
      }

      gameState = GameState.Complete;
      #region 呴儂票濘1 (虴薹詢)
      int[] temp = new int[mines.Length];
      for (int i = 0; i < temp.Length; i++)
      {
        temp[i] = i;
      }
      int d = 0, e;
      int cc = Width * Height;
      for (int c = 0; c < Count; c++)
      {
        unchecked {
          Random random = new Random(System.Environment.TickCount / (c + 1) + System.Environment.TickCount - d);
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

      #region 呴儂票濘2(虴薹腴)
//      int c = 0;
//      while (true)
//      {
//        Random random = new Random(System.Environment.TickCount / (c + 1) + System.Environment.TickCount - d);
//        int d = random.Next(Width * Height);
//        if (mines[d].MineStatus == MineStatus.NoMine)
//        {
//          mines[d].MineStatus = MineStatus.HasMine;
//          c++;
//        }
//        if (c >= Count)
//          break;
//      }
      #endregion
      gameState = GameState.NotStarted;
    }

    /// <summary>
    /// 笭陔扢离濘
    /// </summary>
    public void Clear()
    {
      init();
    }

    public void Clear(int width, int height, int count)
    {
      if (width > 30)
        width = 30;
      if (width < 9)
        width = 9;
      if (height > 24)
        height = 24;
      if (height < 9)
        height = 9;
      m_Width = width;
      m_Height = height;

      int tCount = width * height / 2;
      if (count < 10)
        count = 10;
      if (count > tCount)
        count = tCount;
      m_Count = count;
      init();
    }

    /// <summary>
    /// 呁豻帤楷珋麼梓祩腔濘杅
    /// </summary>
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