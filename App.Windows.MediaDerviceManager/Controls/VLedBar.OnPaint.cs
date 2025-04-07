using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VLedBar
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      /************************************************/
      Graphics g = e.Graphics;
      /************************************************/
      for (int i = 0; i < 15; i++)
      {
        int x = i *  0;
        int y = i * 15;
        int w =      5;
        int h =     14;
        /************************************************/
        Color color = SystemColors.ScrollBar;
        /************************************************/
        if (this.Direction == VDirection.TopBottom)
        {
          if (i ==  0 && this.Value >=  1              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  0 && this.Value >=  1 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  1 && this.Value >=  2              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  1 && this.Value >=  2 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  2 && this.Value >=  3              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  2 && this.Value >=  3 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  3 && this.Value >=  4              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  3 && this.Value >=  4 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  4 && this.Value >=  5              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  4 && this.Value >=  5 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  5 && this.Value >=  6              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  5 && this.Value >=  6 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  6 && this.Value >=  7              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  6 && this.Value >=  7 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  7 && this.Value >=  8              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  7 && this.Value >=  8 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  8 && this.Value >=  9              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  8 && this.Value >=  9 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  9 && this.Value >= 10              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  9 && this.Value >= 10 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i == 10 && this.Value >= 11              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 10 && this.Value >= 11 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i == 11 && this.Value >= 12              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 11 && this.Value >= 12 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i == 12 && this.Value >= 13              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 12 && this.Value >= 13 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
          if (i == 13 && this.Value >= 14              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 13 && this.Value >= 14 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
          if (i == 14 && this.Value >= 15              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 14 && this.Value >= 15 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
        }
        /************************************************/
        if (this.Direction == VDirection.BottomTop)
        {
          if (i == 14 && this.Value >=  1              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 14 && this.Value >=  1 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i == 13 && this.Value >=  2              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 13 && this.Value >=  2 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i == 12 && this.Value >=  3              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 12 && this.Value >=  3 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i == 11 && this.Value >=  4              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 11 && this.Value >=  4 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i == 10 && this.Value >=  5              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i == 10 && this.Value >=  5 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  9 && this.Value >=  6              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  9 && this.Value >=  6 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  8 && this.Value >=  7              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  8 && this.Value >=  7 && this.Color) color = System.Drawing.Color.FromArgb(085, 219, 025);
          if (i ==  7 && this.Value >=  8              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  7 && this.Value >=  8 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  6 && this.Value >=  9              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  6 && this.Value >=  9 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  5 && this.Value >= 10              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  5 && this.Value >= 10 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  4 && this.Value >= 11              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  4 && this.Value >= 11 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  3 && this.Value >= 12              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  3 && this.Value >= 12 && this.Color) color = System.Drawing.Color.FromArgb(251, 151, 000);
          if (i ==  2 && this.Value >= 13              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  2 && this.Value >= 13 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
          if (i ==  1 && this.Value >= 14              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  1 && this.Value >= 14 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
          if (i ==  0 && this.Value >= 15              ) color = System.Drawing.Color.FromArgb(128, 128, 128);
          if (i ==  0 && this.Value >= 15 && this.Color) color = System.Drawing.Color.FromArgb(255, 020, 020);
        }
        /************************************************/
        using (Brush brush = new SolidBrush(color))
        {
          g.FillRectangle(brush, x, y, w , h);
        }
      }
    }
  }
}