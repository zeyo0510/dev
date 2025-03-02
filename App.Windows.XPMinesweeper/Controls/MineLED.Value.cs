using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    private int value = 0;
    /************************************************/
    public int Value
    {
      get
      {
        int retValue = this.value;
        /************************************************/
        return retValue;
      }
      set
      {
        int maxValue = 999;
        int minValue = -99;
        /************************************************/
        if (value > maxValue) value = maxValue;
        if (value < minValue) value = minValue;
        /************************************************/
        if (this.value != value)
        {
          this.value = value;
          /************************************************/
          base.Invalidate();
        }
      }
    }
  }
}