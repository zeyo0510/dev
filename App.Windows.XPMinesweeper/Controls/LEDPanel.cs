using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class LEDPanel : Panel
  {
    private const int charWidth = 13;
    private const int charHeight = 23;
    /************************************************/
    public LEDPanel()
    {
      this.InitializeComponent();

      SetStyle(ControlStyles.Selectable, false);
      BorderStyle = BorderStyle.Fixed3D;
      ClientSize = new Size(charWidth * charCount, charHeight);
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
  }
}
