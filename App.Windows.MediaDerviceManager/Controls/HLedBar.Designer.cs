using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HLedBar
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.led1  = new Label();
      this.led2  = new Label();
      this.led3  = new Label();
      this.led4  = new Label();
      this.led5  = new Label();
      this.led6  = new Label();
      this.led7  = new Label();
      this.led8  = new Label();
      this.led9  = new Label();
      this.led10 = new Label();
      this.led11 = new Label();
      this.led12 = new Label();
      this.led13 = new Label();
      this.led14 = new Label();
      this.led15 = new Label();
      /************************************************/
      // led1
      this.led1.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led1.Location = new System.Drawing.Point(0, 0);
      this.led1.Name = "led1";
      this.led1.Size = new System.Drawing.Size(14, 5);
      this.led1.TabIndex = 39;
      // led2
      this.led2.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led2.Location = new System.Drawing.Point(15, 0);
      this.led2.Name = "led2";
      this.led2.Size = new System.Drawing.Size(14, 5);
      this.led2.TabIndex = 40;
      // led3
      this.led3.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led3.Location = new System.Drawing.Point(30, 0);
      this.led3.Name = "led3";
      this.led3.Size = new System.Drawing.Size(14, 5);
      this.led3.TabIndex = 41;
      // led4
      this.led4.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led4.Location = new System.Drawing.Point(45, 0);
      this.led4.Name = "led4";
      this.led4.Size = new System.Drawing.Size(14, 5);
      this.led4.TabIndex = 42;
      // led5
      this.led5.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led5.Location = new System.Drawing.Point(60, 0);
      this.led5.Name = "led5";
      this.led5.Size = new System.Drawing.Size(14, 5);
      this.led5.TabIndex = 43;
      // led6
      this.led6.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led6.Location = new System.Drawing.Point(75, 0);
      this.led6.Name = "led6";
      this.led6.Size = new System.Drawing.Size(14, 5);
      this.led6.TabIndex = 44;
      // led7
      this.led7.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led7.Location = new System.Drawing.Point(90, 0);
      this.led7.Name = "led7";
      this.led7.Size = new System.Drawing.Size(14, 5);
      this.led7.TabIndex = 45;
      // led8
      this.led8.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led8.Location = new System.Drawing.Point(105, 0);
      this.led8.Name = "led8";
      this.led8.Size = new System.Drawing.Size(14, 5);
      this.led8.TabIndex = 46;
      // led9
      this.led9.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led9.Location = new System.Drawing.Point(120, 0);
      this.led9.Name = "led9";
      this.led9.Size = new System.Drawing.Size(14, 5);
      this.led9.TabIndex = 47;
      // led10
      this.led10.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led10.Location = new System.Drawing.Point(135, 0);
      this.led10.Name = "led10";
      this.led10.Size = new System.Drawing.Size(14, 5);
      this.led10.TabIndex = 48;
      // led11
      this.led11.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led11.Location = new System.Drawing.Point(150, 0);
      this.led11.Name = "led11";
      this.led11.Size = new System.Drawing.Size(14, 5);
      this.led11.TabIndex = 49;
      // led12
      this.led12.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led12.Location = new System.Drawing.Point(165, 0);
      this.led12.Name = "led12";
      this.led12.Size = new System.Drawing.Size(14, 5);
      this.led12.TabIndex = 50;
      // led13
      this.led13.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led13.Location = new System.Drawing.Point(180, 0);
      this.led13.Name = "led13";
      this.led13.Size = new System.Drawing.Size(14, 5);
      this.led13.TabIndex = 51;
      // led14
      this.led14.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led14.Location = new System.Drawing.Point(195, 0);
      this.led14.Name = "led14";
      this.led14.Size = new System.Drawing.Size(14, 5);
      this.led14.TabIndex = 52;
      // led15
      this.led15.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.led15.Location = new System.Drawing.Point(210, 0);
      this.led15.Name = "led15";
      this.led15.Size = new System.Drawing.Size(14, 5);
      this.led15.TabIndex = 53;
      // HLedBar
      base.Name = "HLedBar";
      base.Size = new System.Drawing.Size(225, 4);
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
    }
    /************************************************/
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
    private Label led11;
    private Label led12;
    private Label led13;
    private Label led14;
    private Label led15;
  }
}