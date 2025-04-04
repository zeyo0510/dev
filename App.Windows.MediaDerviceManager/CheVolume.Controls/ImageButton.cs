using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CheVolume.Controls
{
  public class ImageButton : UserControl
  {
    private IContainer components;

    private RadioButton radioButton1;

    public Image Icon
    {
      get
      {
        return radioButton1.Image;
      }
      set
      {
        radioButton1.Image = value;
      }
    }

    public new string Text
    {
      get
      {
        return radioButton1.Text;
      }
      set
      {
        radioButton1.Text = value.Substring(0, 20);
      }
    }

    public ImageButton()
    {
      InitializeComponent();
    }

    private void Method1(object P_0, EventArgs P_1)
    {
    }

    protected override void Dispose(bool P_0)
    {
      if (P_0 && components != null)
      {
        components.Dispose();
      }
      base.Dispose(P_0);
    }

    private void InitializeComponent()
    {
      this.radioButton1 = new RadioButton();
      base.SuspendLayout();
      this.radioButton1.Appearance = Appearance.Button;
      this.radioButton1.Location = new System.Drawing.Point(0, 0);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(80, 80);
      this.radioButton1.TabIndex = 1;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "radioButton1";
      this.radioButton1.UseVisualStyleBackColor = true;
      base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.Controls.Add(this.radioButton1);
      base.Margin = new Padding(0);
      base.Name = "ImageButton";
      base.Size = new System.Drawing.Size(80, 80);
      base.ResumeLayout(false);
    }
  }
}