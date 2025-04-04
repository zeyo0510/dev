using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class LedBar : UserControl
  {
    private readonly Label[] leds;

    private int _Value;

    [CompilerGenerated]
    private bool _IsMuted;

    private IContainer component;

    private Label led1;

    private Label led2;

    private Label led3;

    private Label led4;

    private Label led5;

    private Label led6;

    private Label led7;

    private Label led8;

    private Label led9;

    private Label led10;

    private Label led15;

    private Label led14;

    private Label led13;

    private Label led12;

    private Label led11;

    public bool IsMuted
    {
      [CompilerGenerated]
      get
      {
        return _IsMuted;
      }
      [CompilerGenerated]
      set
      {
        _IsMuted = value;
      }
    }

    public LedBar()
    {
      InitializeComponent();
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

    internal void SetValue(float P_0)
    {
      int num = (int)Math.Ceiling(P_0 * 15f);
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

    protected override void Dispose(bool P_0)
    {
      if (P_0 && component != null)
      {
        component.Dispose();
      }
      base.Dispose(P_0);
    }

    private void InitializeComponent()
    {
      this.led1 = new Label();
      this.led2 = new Label();
      this.led3 = new Label();
      this.led4 = new Label();
      this.led5 = new Label();
      this.led6 = new Label();
      this.led7 = new Label();
      this.led8 = new Label();
      this.led9 = new Label();
      this.led10 = new Label();
      this.led15 = new Label();
      this.led14 = new Label();
      this.led13 = new Label();
      this.led12 = new Label();
      this.led11 = new Label();
      base.SuspendLayout();
      this.led1.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led1.Location = new System.Drawing.Point(0, 0);
      this.led1.Name = "led1";
      this.led1.Size = new System.Drawing.Size(5, 14);
      this.led1.TabIndex = 39;
      this.led2.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led2.Location = new System.Drawing.Point(0, 15);
      this.led2.Name = "led2";
      this.led2.Size = new System.Drawing.Size(5, 14);
      this.led2.TabIndex = 40;
      this.led3.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led3.Location = new System.Drawing.Point(0, 30);
      this.led3.Name = "led3";
      this.led3.Size = new System.Drawing.Size(5, 14);
      this.led3.TabIndex = 41;
      this.led4.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led4.Location = new System.Drawing.Point(0, 45);
      this.led4.Name = "led4";
      this.led4.Size = new System.Drawing.Size(5, 14);
      this.led4.TabIndex = 42;
      this.led5.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led5.Location = new System.Drawing.Point(0, 60);
      this.led5.Name = "led5";
      this.led5.Size = new System.Drawing.Size(5, 14);
      this.led5.TabIndex = 43;
      this.led6.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led6.Location = new System.Drawing.Point(0, 75);
      this.led6.Name = "led6";
      this.led6.Size = new System.Drawing.Size(5, 14);
      this.led6.TabIndex = 44;
      this.led7.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led7.Location = new System.Drawing.Point(0, 90);
      this.led7.Name = "led7";
      this.led7.Size = new System.Drawing.Size(5, 14);
      this.led7.TabIndex = 45;
      this.led8.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led8.Location = new System.Drawing.Point(0, 105);
      this.led8.Name = "led8";
      this.led8.Size = new System.Drawing.Size(5, 14);
      this.led8.TabIndex = 46;
      this.led9.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led9.Location = new System.Drawing.Point(0, 120);
      this.led9.Name = "led9";
      this.led9.Size = new System.Drawing.Size(5, 14);
      this.led9.TabIndex = 47;
      this.led10.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led10.Location = new System.Drawing.Point(0, 135);
      this.led10.Name = "led10";
      this.led10.Size = new System.Drawing.Size(5, 14);
      this.led10.TabIndex = 48;
      this.led15.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led15.Location = new System.Drawing.Point(0, 210);
      this.led15.Name = "led15";
      this.led15.Size = new System.Drawing.Size(5, 14);
      this.led15.TabIndex = 53;
      this.led14.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led14.Location = new System.Drawing.Point(0, 195);
      this.led14.Name = "led14";
      this.led14.Size = new System.Drawing.Size(5, 14);
      this.led14.TabIndex = 52;
      this.led13.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led13.Location = new System.Drawing.Point(0, 180);
      this.led13.Name = "led13";
      this.led13.Size = new System.Drawing.Size(5, 14);
      this.led13.TabIndex = 51;
      this.led12.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led12.Location = new System.Drawing.Point(0, 165);
      this.led12.Name = "led12";
      this.led12.Size = new System.Drawing.Size(5, 14);
      this.led12.TabIndex = 50;
      this.led11.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led11.Location = new System.Drawing.Point(0, 150);
      this.led11.Name = "led11";
      this.led11.Size = new System.Drawing.Size(5, 14);
      this.led11.TabIndex = 49;
      base.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = System.Drawing.SystemColors.Control;
      base.Controls.Add(this.led15);
      base.Controls.Add(this.led14);
      base.Controls.Add(this.led13);
      base.Controls.Add(this.led12);
      base.Controls.Add(this.led11);
      base.Controls.Add(this.led10);
      base.Controls.Add(this.led9);
      base.Controls.Add(this.led8);
      base.Controls.Add(this.led7);
      base.Controls.Add(this.led6);
      base.Controls.Add(this.led5);
      base.Controls.Add(this.led4);
      base.Controls.Add(this.led3);
      base.Controls.Add(this.led2);
      base.Controls.Add(this.led1);
      base.Name = "LedBar";
      base.Size = new System.Drawing.Size(4, 225);
      base.ResumeLayout(false);
    }
  }
}