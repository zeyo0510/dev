using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  partial class Mines
  {
    public void Clear()
    {
      this.init();
    }
    /************************************************/
    public void Clear(int width, int height, int count)
    {
      if (width > 30) width = 30;
      if (width < 09) width = 09;
      this.m_Width = width;
      /************************************************/
      if (height > 24) height = 24;
      if (height < 09) height = 09;
      this.m_Height = height;
      /************************************************/
      int tCount = width * height / 2;
      if (count < 10) count = 10;
      if (count > tCount) count = tCount;
      this.m_Count = count;
      /************************************************/
      this.Clear();
    }
  }
}