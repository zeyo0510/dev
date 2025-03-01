using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class LEDPanel
  {
    private int number;
    /************************************************/
    public int Number
    {
      get
      {
        return number;
      }
      set
      {
        if (value >= 0)
        {
          int maxValue = Convert.ToInt32(String.Empty.PadRight(charCount, '9'));
          if (value > maxValue)
            value = maxValue;
        }
        else
        {
          int minValue = -Convert.ToInt32(String.Empty.PadRight(charCount - 1, '9'));
          if (value < minValue)
            value = minValue;
        }
        if (number != value)
        {
          number = value;
          Invalidate();
        }
      }
    }
  }
}