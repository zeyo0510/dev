using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EConTech.Windows.MACUI;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VDeviceVolumeControl
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
      this.nameLabel       = new Label();
      this.iconPictureBox  = new PictureBox();
      this.defaultCheckBox = new CheckBox();
      this.leftVLedBar     = new VLedBar();
      this.volumeVTrackBar = new VTrackBar();
      this.rightVLedBar    = new VLedBar();
      this.volumeLabel     = new Label();
      this.muteCheckBox    = new CheckBox();
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
      base.SuspendLayout();
      /************************************************/
      // guiTimer
      this.guiTimer.Interval = 10;
      this.guiTimer.Tick += this.guiTimer_Tick;
      // nameLabel
      this.nameLabel.Name      = "nameLabel";
      this.nameLabel.Location  = new Point(3, 10);
      this.nameLabel.Size      = new Size(122, 48);
      this.nameLabel.Text      = "Temp Text";
      this.nameLabel.TextAlign = ContentAlignment.MiddleCenter;
      // iconPictureBox
      this.iconPictureBox.Name     = "iconPictureBox";
      this.iconPictureBox.Location = new Point(47, 58);
      this.iconPictureBox.Margin   = new Padding(0);
      this.iconPictureBox.Size     = new Size(32, 32);
      // defaultCheckBox
      this.defaultCheckBox.Name     = "defaultCheckBox";
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Location = new Point(13, 98);
      this.defaultCheckBox.Size     = new Size(105, 16);
      this.defaultCheckBox.Text     = "Set as Default";
      this.defaultCheckBox.Click += this.defaultCheckBox_Click;
      // leftVLedBar
      this.leftVLedBar.Name     = "leftLedBar";
      this.leftVLedBar.Location = new Point(13, 132);
      // volumeVTrackBar
      this.volumeVTrackBar.Name = "volumeVTrackBar";
      this.volumeVTrackBar.Location = new Point(50, 132);
      this.volumeVTrackBar.Size = new Size(030, 225);
      this.volumeVTrackBar.Direction = VDirection.BottomTop;
      this.volumeVTrackBar.ValueChanged += this.volumeVTrackBar_ValueChanged;
      // rightVLedBar
      this.rightVLedBar.Name     = "rightVLedBar";
      this.rightVLedBar.Location = new Point(108, 132);
      // volumeLabel
      this.volumeLabel.Name        = "volumeLabel";
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Location    = new Point(44, 370);
      this.volumeLabel.Size        = new Size(38, 19);
      this.volumeLabel.Text        = "100";
      this.volumeLabel.TextAlign   = ContentAlignment.MiddleCenter;
      // muteCheckBox
      this.muteCheckBox.Name     = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new Point(46, 400);
      this.muteCheckBox.Text     = "Mute";
      this.muteCheckBox.Click += this.muteCheckBox_Click;
      // VDeviceVolumeControl
      base.Name = "VDeviceVolumeControl";
      base.AutoScaleDimensions = new SizeF(6f, 13f);
      base.AutoScaleMode       = AutoScaleMode.Font;
      base.AutoSizeMode        = AutoSizeMode.GrowAndShrink;
      base.Margin              = new Padding(0);
      base.Size                = new Size(128, 485);
      this.Font                = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      base.Controls.Add(this.leftVLedBar);
      base.Controls.Add(this.defaultCheckBox);
      base.Controls.Add(this.rightVLedBar);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.volumeVTrackBar);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.iconPictureBox);
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private Label      nameLabel       = null;
    private PictureBox iconPictureBox  = null;
    private CheckBox   defaultCheckBox = null;
    private VLedBar    leftVLedBar     = null;
    private VTrackBar  volumeVTrackBar = null;
    private VLedBar    rightVLedBar    = null;
    private Label      volumeLabel     = null;
    private CheckBox   muteCheckBox    = null;
  }
}