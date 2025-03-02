using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      Rectangle rect = base.ClientRectangle;
      /************************************************/
      string num = this.value.ToString().PadLeft(3, '0').Replace("0-", "-0");
      /************************************************/
      for (int i = 0; i < 3; i++)
      {
        int j;
        if (num[i] == '-') {
          j = 10;
        } else {
          j = Convert.ToInt32(num[i]) - 48;
        }
        /************************************************/
        this.leds.Draw(g, rect.Left + 13 * i, rect.Top, j);
      }
    }
  }
}