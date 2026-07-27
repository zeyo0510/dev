using EConTech.Windows.MACUI;
using JC.CS.Lib.Controls;
using System.ComponentModel;
/************************************************/
using ComModel = System.ComponentModel;
using WinForm = System.Windows.Forms;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  private ComModel::IContainer components = null;
  /************************************************/
  private WinForm::Timer guiTimer = null;
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
    this.components = new ComModel::Container();
    /************************************************/
    this.devicePictureBox = new();
    this.nameLabel        = new();
    this.muteCheckBox     = new();
    this.defaultCheckBox  = new();
    this.rightLedBar      = new();
    this.volumeVTrackBar  = new();
    this.leftLedBar       = new();
    this.volumeLabel      = new();
    /************************************************/
    this.guiTimer = new(this.components);
    /************************************************/
    // guiTimer
    {
      this.guiTimer.Enabled = true;
      this.guiTimer.Interval = 100;
      /************************************************/
      this.guiTimer.Tick += this.guiTimer_Tick;
    }
    // nameLabel
    {
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.nameLabel.Location = new System.Drawing.Point(3, 10);
      this.nameLabel.Size = new System.Drawing.Size(122, 48);
      this.nameLabel.Text = this.MMDevice.FriendlyName;
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    }
    // devicePictureBox
    {
      this.devicePictureBox.Name = "devicePictureBox";
      this.devicePictureBox.BackColor = System.Drawing.Color.Transparent;
      this.devicePictureBox.Location = new System.Drawing.Point(47, 58);
      this.devicePictureBox.Margin = new System.Windows.Forms.Padding(0);
      this.devicePictureBox.Size = new System.Drawing.Size(32, 32);
      this.devicePictureBox.TabStop = false;
      this.devicePictureBox.Image = this.ProcessIcon(this.MMDevice.IconPath);
    }
    // defaultCheckBox
    {
      this.defaultCheckBox.Name = "defaultCheckBox";
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
      this.defaultCheckBox.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.defaultCheckBox.Location = new System.Drawing.Point(13, 98);
      this.defaultCheckBox.Size = new System.Drawing.Size(105, 16);
      this.defaultCheckBox.Text = "Set as Default";
      this.defaultCheckBox.UseVisualStyleBackColor = true;
      /************************************************/
      this.defaultCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(defaultCheckBox_MouseClick);
    }
    // leftLedBar
    {
      this.leftLedBar.Name = "leftLedBar";
      this.leftLedBar.Enabled = true;
      this.leftLedBar.Location = new System.Drawing.Point(13, 132);
      this.leftLedBar.Size = new System.Drawing.Size(5, 225);
    }
    // volumeVTrackBar
    {
      this.volumeVTrackBar.Name = "volumeVTrackBar";
      this.volumeVTrackBar.Location = new System.Drawing.Point(50, 121);
      this.volumeVTrackBar.Size = new System.Drawing.Size(79, 247);
      /************************************************/
      this.volumeVTrackBar.ValueChanged += this.volumeVTrackBar_ValueChanged;
    }
    // rightLedBar
    {
      this.rightLedBar.Name = "rightLedBar";
      this.rightLedBar.Enabled = true;
      this.rightLedBar.Location = new System.Drawing.Point(108, 132);
      this.rightLedBar.Size = new System.Drawing.Size(5, 225);
    }
    // volumeLabel
    {
      this.volumeLabel.Name = "volumeLabel";
      this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
      this.volumeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.volumeLabel.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location = new System.Drawing.Point(44, 370);
      this.volumeLabel.Size = new System.Drawing.Size(38, 19);
      this.volumeLabel.Text = this.Volume.ToString();
      this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.volumeLabel.UseCompatibleTextRendering = true;
      this.volumeLabel.UseMnemonic = false;
    }
    // muteCheckBox
    {
      this.muteCheckBox.Name = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new System.Drawing.Point(46, 400);
      this.muteCheckBox.Text = "Mute";
      /************************************************/
      this.muteCheckBox.Click += this.muteCheckBox_Click;
    }
    // DeviceVolumeControl
    {
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
      base.Controls.Add(this.volumeVTrackBar);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.leftLedBar);
      base.Controls.Add(this.devicePictureBox);
    }
  }
  /************************************************/
  public Label nameLabel;
  public PictureBox devicePictureBox;
  private CheckBox defaultCheckBox;
  public VLevelMeter leftLedBar;
  private VTrackBar volumeVTrackBar;
  public VLevelMeter rightLedBar;
  private Label volumeLabel;
  public CheckBox muteCheckBox;
}