using EConTech.Windows.MACUI;
using System.ComponentModel;
using WinForm = System.Windows.Forms;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  private IContainer components;
  /************************************************/
  private WinForm::Timer timer1;
  /************************************************/
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
    }
    /************************************************/
    base.Dispose(disposing);
  }
  /************************************************/
  private void InitializeComponent()
  {
    this.devicePictureBox = new();
    this.nameLabel        = new();
    this.muteCheckBox  = new();
    this.defaultCheckBox  = new();
    this.rightLedBar      = new();
    this.macTrackBar1     = new();
    this.leftLedBar       = new();
    this.volumeLabel      = new();
    /************************************************/
    // nameLabel
    this.nameLabel.Name = "nameLabel";
    this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.nameLabel.Location = new System.Drawing.Point(3, 10);
    this.nameLabel.Size = new System.Drawing.Size(122, 48);
    this.nameLabel.Text = "Temp Text";
    this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    // devicePictureBox
    this.devicePictureBox.Name = "devicePictureBox";
    this.devicePictureBox.BackColor = System.Drawing.Color.Transparent;
    this.devicePictureBox.Location = new System.Drawing.Point(47, 58);
    this.devicePictureBox.Margin = new System.Windows.Forms.Padding(0);
    this.devicePictureBox.Size = new System.Drawing.Size(32, 32);
    this.devicePictureBox.TabStop = false;
    // defaultCheckBox
    this.defaultCheckBox.Name = "defaultCheckBox";
    this.defaultCheckBox.AutoSize = true;
    this.defaultCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.defaultCheckBox.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
    this.defaultCheckBox.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.defaultCheckBox.Location = new System.Drawing.Point(13, 98);
    this.defaultCheckBox.Size = new System.Drawing.Size(105, 16);
    this.defaultCheckBox.Text = "Set as Default";
    this.defaultCheckBox.UseVisualStyleBackColor = true;
    this.defaultCheckBox.CheckedChanged += new System.EventHandler(defaultCheckBox_CheckedChanged);
    this.defaultCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(defaultCheckBox_MouseClick);
    // leftLedBar
    this.leftLedBar.Name = "leftLedBar";
    this.leftLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.leftLedBar.IsMuted = false;
    this.leftLedBar.Location = new System.Drawing.Point(13, 132);
    this.leftLedBar.Size = new System.Drawing.Size(5, 225);
    // macTrackBar1
    this.macTrackBar1.Name = "macTrackBar1";
    this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
    this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
    this.macTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
    this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
    this.macTrackBar1.IndentHeight = 6;
    this.macTrackBar1.LargeChange = 1;
    this.macTrackBar1.Location = new System.Drawing.Point(23, 121);
    this.macTrackBar1.Maximum = 100;
    this.macTrackBar1.Minimum = 0;
    this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
    this.macTrackBar1.Size = new System.Drawing.Size(79, 247);
    this.macTrackBar1.TextTickStyle = System.Windows.Forms.TickStyle.Both;
    this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
    this.macTrackBar1.TickFrequency = 10;
    this.macTrackBar1.TickHeight = 4;
    this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
    this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
    this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
    this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
    this.macTrackBar1.VolumeLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
    this.macTrackBar1.TrackLineHeight = 3;
    this.macTrackBar1.Value = 0;
    this.macTrackBar1.ValueChanged += macTrackBar1_OnValueChanged;
    // rightLedBar
    this.rightLedBar.Name = "rightLedBar";
    this.rightLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.rightLedBar.IsMuted = false;
    this.rightLedBar.Location = new System.Drawing.Point(108, 132);
    this.rightLedBar.Size = new System.Drawing.Size(5, 225);
    // volumeLabel
    this.volumeLabel.Name = "volumeLabel";
    this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
    this.volumeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.volumeLabel.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.volumeLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.volumeLabel.Location = new System.Drawing.Point(44, 370);
    this.volumeLabel.Size = new System.Drawing.Size(38, 19);
    this.volumeLabel.Text = "100";
    this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.volumeLabel.UseCompatibleTextRendering = true;
    this.volumeLabel.UseMnemonic = false;
    // muteCheckBox
    this.muteCheckBox.Name = "muteCheckBox";
    this.muteCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
    this.muteCheckBox.AutoSize = true;
    this.muteCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.muteCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.muteCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.muteCheckBox.Image = CheVolume.Properties.Resources.mute;
    this.muteCheckBox.Location = new System.Drawing.Point(46, 400);
    this.muteCheckBox.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.muteCheckBox.Size = new System.Drawing.Size(34, 34);
    this.muteCheckBox.UseVisualStyleBackColor = false;
    this.muteCheckBox.CheckedChanged += new System.EventHandler(muteCheckBox_CheckedChanged);
    this.muteCheckBox.MouseEnter += new System.EventHandler(muteCheckBox_MouseEnter);
    this.muteCheckBox.MouseLeave += new System.EventHandler(muteCheckBox_MouseLeave);
    this.muteCheckBox.MouseUp += new System.Windows.Forms.MouseEventHandler(muteCheckBox_MouseUp);
    // DeviceVolumeControl
    base.Name = "DeviceVolumeControl";
    base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.BackColor = System.Drawing.Color.Transparent;
    base.Margin = new System.Windows.Forms.Padding(0);
    base.Size = new System.Drawing.Size(128, 485);
    base.Controls.Add(this.defaultCheckBox);
    base.Controls.Add(this.rightLedBar);
    base.Controls.Add(this.volumeLabel);
    base.Controls.Add(this.macTrackBar1);
    base.Controls.Add(this.muteCheckBox);
    base.Controls.Add(this.nameLabel);
    base.Controls.Add(this.leftLedBar);
    base.Controls.Add(this.devicePictureBox);
  }
  /************************************************/
  public Label nameLabel;
  public PictureBox devicePictureBox;
  private CheckBox defaultCheckBox;
  public LedBar leftLedBar;
  public MACTrackBar macTrackBar1;
  public LedBar rightLedBar;
  private Label volumeLabel;
  public CheckBox muteCheckBox;
}