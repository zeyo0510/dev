using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    private int _Value = 0;
    /************************************************/
    public int Value
    {
      get
      {
        int retValue = this._Value;
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
        if (this._Value != value)
        {
          this._Value = value;
          /************************************************/
          base.Invalidate();
        }
      }
    }
  }
}