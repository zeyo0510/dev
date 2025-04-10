using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HDeviceVolumeControl
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
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
      this.components = new Container();
      /************************************************/
      this.guiTimer = new Timer(this.components);
      /************************************************/
      this.defaultCheckBox = new CheckBox();
      this.iconPictureBox = new PictureBox();
      this.nameLabel = new Label();
      this.leftHLedBar = new HLedBar();
      this.volumeHTrackBar = new HTrackBar();
      this.rightHLedBar = new HLedBar();
      this.muteCheckBox = new CheckBox();
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
      base.SuspendLayout();
      /************************************************/
      // guiTimer
      this.guiTimer.Interval = 10;
      this.guiTimer.Tick += timer1_Tick;
      // defaultCheckBox
      this.defaultCheckBox.Name = "defaultCheckBox";
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Font = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World);
      this.defaultCheckBox.ForeColor = Color.FromArgb(64, 64, 64);
      this.defaultCheckBox.Location = new Point(34, 23);
      this.defaultCheckBox.Size = new Size(18, 17);
      this.defaultCheckBox.MouseClick += defaultCheckBox_MouseClick;
      // iconPictureBox
      this.iconPictureBox.Name = "IconBox";
      this.iconPictureBox.BackColor = Color.Transparent;
      this.iconPictureBox.Location = new Point(71, 12);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Size = new Size(43, 39);
      // nameLabel
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Font = new Font("Verdana", 11f, FontStyle.Bold, GraphicsUnit.World);
      this.nameLabel.ForeColor = Color.FromArgb(64, 64, 64);
      this.nameLabel.Location = new Point(119, 4);
      this.nameLabel.Size = new Size(157, 59);
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = ContentAlignment.MiddleLeft;
      // leftHLedBar
      this.leftHLedBar.Name = "leftHLedBar";
      this.leftHLedBar.Location = new Point(15, 40);
      // volumeHTrackBar
      this.volumeHTrackBar.Name = "volumeHTrackBar";
      this.volumeHTrackBar.Location = new Point(15, 45);
      this.volumeHTrackBar.Size = new Size(225, 4);
      this.volumeHTrackBar.ValueChanged += this.volumeHTrackBar_ValueChanged;
      // rightHLedBar
      this.rightHLedBar.Name = "rightHLedBar";
      this.rightHLedBar.Location = new Point(15, 72);
      // muteCheckBox
      this.muteCheckBox.Name = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new Point(287, 12);
      this.muteCheckBox.Size = new Size(34, 34);
      this.muteCheckBox.Text = "Mute";
      this.muteCheckBox.Click += this.muteCheckBox_Click;
      // HDeviceVolumeControl
      base.Name = "HDeviceVolumeControl";
      base.AutoScaleDimensions = new SizeF(8f, 16f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = Color.Transparent;
      base.Controls.Add(this.leftHLedBar);
      base.Controls.Add(this.rightHLedBar);
      base.Controls.Add(this.defaultCheckBox);
      base.Controls.Add(this.volumeHTrackBar);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.iconPictureBox);
      base.Margin = new Padding(0);
      base.Size = new Size(364, 100);
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private CheckBox defaultCheckBox = null;
    private PictureBox iconPictureBox = null;
    private Label nameLabel = null;
    private HLedBar leftHLedBar = null;
    private HTrackBar volumeHTrackBar = null;
    private HLedBar rightHLedBar = null;
    private CheckBox muteCheckBox = null;
  }
}