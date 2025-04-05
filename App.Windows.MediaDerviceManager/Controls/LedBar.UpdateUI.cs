using System;
using System.Drawing;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class LedBar
  {
    public void UpdateUI()
    {
      if (this.LayoutDirection == LayoutDirection.BottomToTop)
      {
        base.Size           = new Size(4, 225);
        /************************************************/
        this.led1 .Location = new Point(000, 000);
        this.led1 .Size     = new Size(5, 14);
        this.led2 .Location = new Point(000, 015);
        this.led2 .Size     = new Size(5, 14);
        this.led3 .Location = new Point(000, 030);
        this.led3 .Size     = new Size(5, 14);
        this.led4 .Location = new Point(000, 045);
        this.led4 .Size     = new Size(5, 14);
        this.led5 .Location = new Point(000, 060);
        this.led5 .Size     = new Size(5, 14);
        this.led6 .Location = new Point(000, 075);
        this.led6 .Size     = new Size(5, 14);
        this.led7 .Location = new Point(000, 090);
        this.led7 .Size     = new Size(5, 14);
        this.led8 .Location = new Point(000, 105);
        this.led8 .Size     = new Size(5, 14);
        this.led9 .Location = new Point(000, 120);
        this.led9 .Size     = new Size(5, 14);
        this.led10.Location = new Point(000, 135);
        this.led10.Size     = new Size(5, 14);
        this.led11.Location = new Point(000, 150);
        this.led11.Size     = new Size(5, 14);
        this.led12.Location = new Point(000, 165);
        this.led12.Size     = new Size(5, 14);
        this.led13.Location = new Point(000, 180);
        this.led13.Size     = new Size(5, 14);
        this.led14.Location = new Point(000, 195);
        this.led14.Size     = new Size(5, 14);
        this.led15.Location = new Point(000, 210);
        this.led15.Size     = new Size(5, 14);
      }
      if (this.LayoutDirection == LayoutDirection.LeftToRight)
      {
        base.Size = new Size(225, 4);
        this.led15.Location = new Point(000, 000);
        this.led15.Size     = new Size(14, 5);
        this.led14.Location = new Point(015, 000);
        this.led14.Size     = new Size(14, 5);
        this.led13.Location = new Point(030, 000);
        this.led13.Size     = new Size(14, 5);
        this.led12.Location = new Point(045, 000);
        this.led12.Size     = new Size(14, 5);
        this.led11.Location = new Point(060, 000);
        this.led11.Size     = new Size(14, 5);
        this.led10.Location = new Point(075, 000);
        this.led10.Size     = new Size(14, 5);
        this.led9 .Location = new Point(090, 000);
        this.led9 .Size     = new Size(14, 5);
        this.led8 .Location = new Point(105, 000);
        this.led8 .Size     = new Size(14, 5);
        this.led7 .Location = new Point(120, 000);
        this.led7 .Size     = new Size(14, 5);
        this.led6 .Location = new Point(135, 000);
        this.led6 .Size     = new Size(14, 5);
        this.led5 .Location = new Point(150, 000);
        this.led5 .Size     = new Size(14, 5);
        this.led4 .Location = new Point(165, 000);
        this.led4 .Size     = new Size(14, 5);
        this.led3 .Location = new Point(180, 000);
        this.led3 .Size     = new Size(14, 5);
        this.led2 .Location = new Point(195, 000);
        this.led2 .Size     = new Size(14, 5);
        this.led1 .Location = new Point(210, 000);
        this.led1 .Size     = new Size(14, 5);
      }
      if (this.LayoutDirection == LayoutDirection.TopToBottom)
      {
        base.Size = new Size(4, 225);
        this.led15.Location = new Point(000, 000);
        this.led15.Size     = new Size(5, 14);
        this.led14.Location = new Point(000, 015);
        this.led14.Size     = new Size(5, 14);
        this.led13.Location = new Point(000, 030);
        this.led13.Size     = new Size(5, 14);
        this.led12.Location = new Point(000, 045);
        this.led12.Size     = new Size(5, 14);
        this.led11.Location = new Point(000, 060);
        this.led11.Size     = new Size(5, 14);
        this.led10.Location = new Point(000, 075);
        this.led10.Size     = new Size(5, 14);
        this.led9 .Location = new Point(000, 090);
        this.led9 .Size     = new Size(5, 14);
        this.led8 .Location = new Point(000, 105);
        this.led8 .Size     = new Size(5, 14);
        this.led7 .Location = new Point(000, 120);
        this.led7 .Size     = new Size(5, 14);
        this.led6 .Location = new Point(000, 135);
        this.led6 .Size     = new Size(5, 14);
        this.led5 .Location = new Point(000, 150);
        this.led5 .Size     = new Size(5, 14);
        this.led4 .Location = new Point(000, 165);
        this.led4 .Size     = new Size(5, 14);
        this.led3 .Location = new Point(000, 180);
        this.led3 .Size     = new Size(5, 14);
        this.led2 .Location = new Point(000, 195);
        this.led2 .Size     = new Size(5, 14);
        this.led1 .Location = new Point(000, 210);
        this.led1 .Size     = new Size(5, 14);
      }
      if (this.LayoutDirection == LayoutDirection.RightToLeft)
      {
        base.Size = new Size(225, 4);
        this.led1 .Location = new Point(000, 000);
        this.led1 .Size     = new Size(14, 5);
        this.led2 .Location = new Point(015, 000);
        this.led2 .Size     = new Size(14, 5);
        this.led3 .Location = new Point(030, 000);
        this.led3 .Size     = new Size(14, 5);
        this.led4 .Location = new Point(045, 000);
        this.led4 .Size     = new Size(14, 5);
        this.led5 .Location = new Point(060, 000);
        this.led5 .Size     = new Size(14, 5);
        this.led6 .Location = new Point(075, 000);
        this.led6 .Size     = new Size(14, 5);
        this.led7 .Location = new Point(090, 000);
        this.led7 .Size     = new Size(14, 5);
        this.led8 .Location = new Point(105, 000);
        this.led8 .Size     = new Size(14, 5);
        this.led9 .Location = new Point(120, 000);
        this.led9 .Size     = new Size(14, 5);
        this.led10.Location = new Point(135, 000);
        this.led10.Size     = new Size(14, 5);
        this.led11.Location = new Point(150, 000);
        this.led11.Size     = new Size(14, 5);
        this.led12.Location = new Point(165, 000);
        this.led12.Size     = new Size(14, 5);
        this.led13.Location = new Point(180, 000);
        this.led13.Size     = new Size(14, 5);
        this.led14.Location = new Point(195, 000);
        this.led14.Size     = new Size(14, 5);
        this.led15.Location = new Point(210, 000);
        this.led15.Size     = new Size(14, 5);
      }
      /************************************************/
      this.led15.BackColor = (this.Value >=  1              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led15.BackColor = (this.Value >=  1 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led15.BackColor;
      this.led14.BackColor = (this.Value >=  2              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led14.BackColor = (this.Value >=  2 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led14.BackColor;
      this.led13.BackColor = (this.Value >=  3              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led13.BackColor = (this.Value >=  3 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led13.BackColor;
      this.led12.BackColor = (this.Value >=  4              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led12.BackColor = (this.Value >=  4 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led12.BackColor;
      this.led11.BackColor = (this.Value >=  5              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led11.BackColor = (this.Value >=  5 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led11.BackColor;
      this.led10.BackColor = (this.Value >=  6              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led10.BackColor = (this.Value >=  6 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led10.BackColor;
      this.led9 .BackColor = (this.Value >=  7              ) ? System.Drawing.Color.FromArgb(131, 131, 131) : SystemColors.ScrollBar;
      this.led9 .BackColor = (this.Value >=  7 && this.Color) ? System.Drawing.Color.FromArgb(085, 219, 025) : this.led9 .BackColor;
      /************************************************/
      this.led8 .BackColor = (this.Value >=  8              ) ? System.Drawing.Color.FromArgb(176, 176, 176) : SystemColors.ScrollBar;
      this.led8 .BackColor = (this.Value >=  8 && this.Color) ? System.Drawing.Color.FromArgb(251, 151, 000) : this.led8 .BackColor;
      this.led7 .BackColor = (this.Value >=  9              ) ? System.Drawing.Color.FromArgb(176, 176, 176) : SystemColors.ScrollBar;
      this.led7 .BackColor = (this.Value >=  9 && this.Color) ? System.Drawing.Color.FromArgb(251, 151, 000) : this.led7 .BackColor;
      this.led6 .BackColor = (this.Value >= 10              ) ? System.Drawing.Color.FromArgb(176, 176, 176) : SystemColors.ScrollBar;
      this.led6 .BackColor = (this.Value >= 10 && this.Color) ? System.Drawing.Color.FromArgb(251, 151, 000) : this.led6 .BackColor;
      this.led5 .BackColor = (this.Value >= 11              ) ? System.Drawing.Color.FromArgb(176, 176, 176) : SystemColors.ScrollBar;
      this.led5 .BackColor = (this.Value >= 11 && this.Color) ? System.Drawing.Color.FromArgb(251, 151, 000) : this.led5 .BackColor;
      this.led4 .BackColor = (this.Value >= 12              ) ? System.Drawing.Color.FromArgb(176, 176, 176) : SystemColors.ScrollBar;
      this.led4 .BackColor = (this.Value >= 12 && this.Color) ? System.Drawing.Color.FromArgb(251, 151, 000) : this.led4 .BackColor;
      /************************************************/
      this.led3 .BackColor = (this.Value >= 13              ) ? System.Drawing.Color.FromArgb(182, 182, 182) : SystemColors.ScrollBar;
      this.led3 .BackColor = (this.Value >= 13 && this.Color) ? System.Drawing.Color.FromArgb(255, 020, 020) : this.led3 .BackColor;
      this.led2 .BackColor = (this.Value >= 14              ) ? System.Drawing.Color.FromArgb(182, 182, 182) : SystemColors.ScrollBar;
      this.led2 .BackColor = (this.Value >= 14 && this.Color) ? System.Drawing.Color.FromArgb(255, 020, 020) : this.led2 .BackColor;
      this.led1 .BackColor = (this.Value >= 15              ) ? System.Drawing.Color.FromArgb(182, 182, 182) : SystemColors.ScrollBar;
      this.led1 .BackColor = (this.Value >= 15 && this.Color) ? System.Drawing.Color.FromArgb(255, 020, 020) : this.led1 .BackColor;
    }
  }
}