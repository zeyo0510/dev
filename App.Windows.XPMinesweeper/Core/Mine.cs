using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  public sealed partial class Mine
  {
    private Mines mines;
    private Mine Mine00;
    private Mine Mine01;
    private Mine Mine02;
    private Mine Mine10;
    private Mine Mine12;
    private Mine Mine20;
    private Mine Mine21;
    private Mine Mine22;

    public Mine(int i, Mines m)
    {
      Index = i;
      mines = m;
    }

    public void Init()
    {
      int i;
      i = Index - mines.Width - 1;
      if (valideIndex(i) && Index % mines.Width != 0)
        Mine00 = mines.mines[i];
      else
        Mine00 = null;

      i = Index - mines.Width;
      if (valideIndex(i))
        Mine01 = mines.mines[i];
      else
        Mine01 = null;

      i = Index - mines.Width + 1;
      if (valideIndex(i) && Index % mines.Width != mines.Width - 1)
        Mine02 = mines.mines[i];
      else
        Mine02 = null;

      i = Index - 1;
      if (valideIndex(i) && Index % mines.Width != 0)
        Mine10 = mines.mines[i];
      else
        Mine10 = null;

      i = Index + 1;
      if (valideIndex(i) && Index % mines.Width != mines.Width - 1)
        Mine12 = mines.mines[i];
      else
        Mine12 = null;

      i = Index + mines.Width - 1;
      if (valideIndex(i) && Index % mines.Width != 0)
        Mine20 = mines.mines[i];
      else
        Mine20 = null;

      i = Index + mines.Width;
      if (valideIndex(i))
        Mine21 = mines.mines[i];
      else
        Mine21 = null;

      i = Index + mines.Width + 1;
      if (valideIndex(i) && Index % mines.Width != mines.Width - 1)
        Mine22 = mines.mines[i];
      else
        Mine22 = null;
    }


    private bool valideIndex(int i)
    {
      int Count = mines.Width * mines.Height;
      return i >= 0 && i < Count;
    }

    /// <summary>
    /// 袨怓
    /// </summary>
    public MineStatus MineStatus
    {
      get
      {
        return mines.MineArray[Index % mines.Width, Index / mines.Width];
      }
      set
      {
        mines.MineArray[Index % mines.Width, Index / mines.Width] =  value;
        if (mines.GameState != GameState.Complete)
          mines.updateGameState();
      }
    }

    /// <summary>
    /// 眈邁郖濘杅
    /// </summary>
    public int MineCount
    {
      get
      {
        if (MineStatus == MineStatus.Clear || MineStatus == MineStatus.NoMine)
        {
          int i = 0;
          if (hasMine(Mine00))
            i++;
          if (hasMine(Mine01))
            i++;
          if (hasMine(Mine02))
            i++;
          if (hasMine(Mine10))
            i++;
          if (hasMine(Mine12))
            i++;
          if (hasMine(Mine20))
            i++;
          if (hasMine(Mine21))
            i++;
          if (hasMine(Mine22))
            i++;
          return i;
        }
        else
          return -1;
      }
    }

    private bool hasMine(Mine mine)
    {
      return  (mine != null && (mine.MineStatus == MineStatus.HasMine ||
        mine.MineStatus == MineStatus.MarkedRight || mine.MineStatus == MineStatus.Exploded));
    }

    /// <summary>
    /// 梓祩涴跺濘
    /// </summary>
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
      mines.mineStatusChange(this);
    }

    /// <summary>
    /// 阼羲涴跺濘
    /// </summary>
    public void Dig(bool update)
    {
      #region 忑棒阼濘剒悵阼祥善濘
      if (mines.GameState == GameState.NotStarted)
        mines.moveMineToOtherPlace(this);
      #endregion

      Doubt = false;
      if (MineStatus == MineStatus.HasMine)
        MineStatus = MineStatus.Exploded;
      else if (MineStatus == MineStatus.NoMine)
      {
        MineStatus = MineStatus.Clear;
        if (MineCount == 0)
        {
          digMine(Mine00);
          digMine(Mine01);
          digMine(Mine02);
          digMine(Mine10);
          digMine(Mine12);
          digMine(Mine20);
          digMine(Mine21);
          digMine(Mine22);
        }
      }
      else
        return;
      if (update)
      {
        if (MineCount == 0)
          mines.mineStatusChange(null);
        else
          mines.mineStatusChange(this);
      }
    }

    private void digMine(Mine m)
    {
      if (m != null)
        m.Dig(false);
    }

    /// <summary>
    /// 辦厒阼羲笚峓腔濘
    /// </summary>
    public void QuickDig()
    {
      if (MineStatus == MineStatus.Clear)
      {
        if (markedMineCount == MineCount)
        {
          autoDig(Mine00);
          autoDig(Mine01);
          autoDig(Mine02);
          autoDig(Mine10);
          autoDig(Mine12);
          autoDig(Mine20);
          autoDig(Mine21);
          autoDig(Mine22);
        }
      }
    }

    private void autoDig(Mine mine)
    {
      if (mine == null)
        return;
      if (mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.NoMine)
        mine.Dig(true);
    }

    private bool markedMine(Mine mine)
    {
      return  (mine != null && (mine.MineStatus == MineStatus.MarkedRight ||
        mine.MineStatus == MineStatus.MarkedWrong || mine.MineStatus == MineStatus.Exploded));
    }

    private int markedMineCount
    {
      get
      {
        int i = 0;
        if (markedMine(Mine00))
          i++;
        if (markedMine(Mine01))
          i++;
        if (markedMine(Mine02))
          i++;
        if (markedMine(Mine10))
          i++;
        if (markedMine(Mine12))
          i++;
        if (markedMine(Mine20))
          i++;
        if (markedMine(Mine21))
          i++;
        if (markedMine(Mine22))
          i++;
        return i;
      }
    }
  }
}