using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class VLedBar : UserControl
  {
    private readonly Label[] leds;
    /************************************************/
    public VLedBar()
    {
      this.InitializeComponent();
      /************************************************/
      leds = new Label[16]
      {
        new Label(),
        led15,
        led14,
        led13,
        led12,
        led11,
        led10,
        led9,
        led8,
        led7,
        led6,
        led5,
        led4,
        led3,
        led2,
        led1
      };
    }
    /************************************************/
    public bool IsMuted
    {
      get; set;
    }
    /************************************************/
    private int _Value;
    /************************************************/
    internal void SetValue(float n)
    {
      int num = (int)Math.Ceiling(n * 15f);
      /************************************************/
      if (_Value != num)
      {
        _Value = num;
        for (int i = 0; i < 15; i++)
        {
          Color color = ((i <= 11) ? ((i <= 7) ? ((!IsMuted) ? Color.FromArgb(85, 219, 25) : Color.FromArgb(131, 131, 131)) : ((!IsMuted) ? Color.FromArgb(251, 151, 0) : Color.FromArgb(176, 176, 176))) : ((!IsMuted) ? Color.FromArgb(255, 20, 20) : Color.FromArgb(182, 182, 182)));
          leds[i].BackColor = ((num >= i) ? color : SystemColors.ScrollBar);
        }
      }
    }
  }
}