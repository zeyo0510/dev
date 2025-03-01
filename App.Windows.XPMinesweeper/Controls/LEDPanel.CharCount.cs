using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class LEDPanel
  {
    private int charCount = 3;
    /************************************************/
    public int CharCount
    {
      get
      {
        return charCount;
      }
      set
      {
        if (value != charCount)
        {
          charCount = value;
          ClientSize = new Size(charWidth * charCount, charHeight);
        }
      }
    }
  }
}