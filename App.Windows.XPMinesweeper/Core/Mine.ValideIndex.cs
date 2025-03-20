using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Core
{
  // 驗證索引是否有效
  partial class Mine
  {
    private bool valideIndex(int index)
    {
      int count = this.mines.Width * this.mines.Height;
      /************************************************/
      return index >= 0 && index < count;
    }
  }
}