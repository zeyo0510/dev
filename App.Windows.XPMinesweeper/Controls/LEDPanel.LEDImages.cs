using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class LEDPanel
  {
    private ImageList leds;
    /************************************************/
    public ImageList LEDImages
    {
      get
      {
        return leds;
      }
      set
      {
        leds = value;
      }
    }
  }
}