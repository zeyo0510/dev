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
      this.nameLabel         = new Label();
      this.iconPictureBox    = new PictureBox();
      this.defaultCheckBox   = new CheckBox();
      this.leftVLedBar        = new VLedBar();
      this.volumeMACTrackBar = new MACTrackBar();
      this.rightVLedBar       = new VLedBar();
      this.volumeLabel       = new Label();
      this.muteCheckBox      = new CheckBox();
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
      // volumeMACTrackBar
      this.volumeMACTrackBar.Name = "volumeMACTrackBar";
      this.volumeMACTrackBar.BackColor = System.Drawing.Color.Transparent;
      this.volumeMACTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
      this.volumeMACTrackBar.Cursor = Cursors.Hand;
      this.volumeMACTrackBar.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeMACTrackBar.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
      this.volumeMACTrackBar.IndentHeight = 6;
      this.volumeMACTrackBar.LargeChange = 1;
      this.volumeMACTrackBar.Location = new System.Drawing.Point(23, 121);
      this.volumeMACTrackBar.Maximum = 100;
      this.volumeMACTrackBar.Minimum = 0;
      this.volumeMACTrackBar.Orientation = Orientation.Vertical;
      this.volumeMACTrackBar.Size = new System.Drawing.Size(79, 247);
      this.volumeMACTrackBar.TabIndex = 10;
      this.volumeMACTrackBar.TextTickStyle = TickStyle.Both;
      this.volumeMACTrackBar.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
      this.volumeMACTrackBar.TickFrequency = 10;
      this.volumeMACTrackBar.TickHeight = 4;
      this.volumeMACTrackBar.TickStyle = TickStyle.Both;
      this.volumeMACTrackBar.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
      this.volumeMACTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
      this.volumeMACTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.VolumeLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.TrackLineHeight = 3;
      this.volumeMACTrackBar.Value = 0;
      this.volumeMACTrackBar.ValueChanged += this.volumeMACTrackBar_OnValueChanged;
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
      base.Controls.Add(this.volumeMACTrackBar);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.iconPictureBox);
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private Label       nameLabel         = null;
    private PictureBox  iconPictureBox    = null;
    private CheckBox    defaultCheckBox   = null;
    private VLedBar     leftVLedBar       = null;
    private MACTrackBar volumeMACTrackBar = null;
    private VLedBar     rightVLedBar      = null;
    private Label       volumeLabel       = null;
    private CheckBox    muteCheckBox      = null;
  }
}