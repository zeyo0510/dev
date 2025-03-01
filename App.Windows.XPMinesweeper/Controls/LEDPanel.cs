using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public class LEDPanel: Panel
  {
    public LEDPanel()
    {
      InitializeComponent();

      SetStyle(ControlStyles.Selectable, false);
      BorderStyle = BorderStyle.Fixed3D;
      ClientSize = new Size(charWidth * charCount, charHeight);
    }
    /************************************************/
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
      }
      base.Dispose( disposing );
    }
    /************************************************/
    private void InitializeComponent()
    {
      Name = "LEDPanel";
    }
    /************************************************/
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      Rectangle rect = ClientRectangle;
      Graphics g = e.Graphics;

      string num = number.ToString().PadLeft(charCount, '0').Replace("0-", "-0");
      for (int i = 0; i < charCount; i++)
      {
        int j;
        if (num[i] == '-')
          j = 10;
        else
          j = Convert.ToInt32(num[i]) - 48;
        leds.Draw(g, rect.Left + charWidth * i, rect.Top, j);
      }
    }

    private const int charWidth = 13;
    private const int charHeight = 23;
    private int charCount = 3;

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

    private ImageList leds;

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

    private int number;

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
