using System.ComponentModel;
/************************************************/
using JC.CS.Lib.Controls;
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
      this.nameLabel.Name      = "nameLabel";
      this.nameLabel.Font      = new("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.nameLabel.Location  = new(3, 10);
      this.nameLabel.Size      = new(122, 48);
      this.nameLabel.Text      = this.AudioDevice.FriendlyName;
      this.nameLabel.TextAlign = ContentAlignment.MiddleCenter;
    }
    // devicePictureBox
    {
      this.devicePictureBox.Name      = "devicePictureBox";
      this.devicePictureBox.BackColor = Color.Transparent;
      this.devicePictureBox.Location  = new(47, 58);
      this.devicePictureBox.Margin    = new(0);
      this.devicePictureBox.Size      = new(32, 32);
      this.devicePictureBox.TabStop   = false;
      this.devicePictureBox.Image     = this.ProcessIcon(this.AudioDevice.IconPath);
    }
    // defaultCheckBox
    {
      this.defaultCheckBox.Name     = "defaultCheckBox";
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Cursor   = Cursors.Hand;
      this.defaultCheckBox.Font     = new("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World);
      this.defaultCheckBox.Location = new(13, 98);
      this.defaultCheckBox.Size     = new(105, 16);
      this.defaultCheckBox.Text     = "Set as Default";
      /************************************************/
      this.defaultCheckBox.MouseClick += this.defaultCheckBox_Click;
    }
    // leftLedBar
    {
      this.leftLedBar.Name     = "leftLedBar";
      this.leftLedBar.Enabled  = true;
      this.leftLedBar.Location = new(13, 132);
      this.leftLedBar.Size     = new(5, 225);
    }
    // volumeVTrackBar
    {
      this.volumeVTrackBar.Name     = "volumeVTrackBar";
      this.volumeVTrackBar.Location = new(50, 121);
      this.volumeVTrackBar.Size     = new(79, 247);
      /************************************************/
      this.volumeVTrackBar.ValueChanged += this.volumeVTrackBar_ValueChanged;
    }
    // rightLedBar
    {
      this.rightLedBar.Name     = "rightLedBar";
      this.rightLedBar.Enabled  = true;
      this.rightLedBar.Location = new(108, 132);
      this.rightLedBar.Size     = new(5, 225);
    }
    // volumeLabel
    {
      this.volumeLabel.Name        = "volumeLabel";
      this.volumeLabel.BackColor   = Color.Transparent;
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Font        = new("Verdana", 8f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.volumeLabel.ForeColor   = Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location    = new(44, 370);
      this.volumeLabel.Size        = new(38, 19);
      this.volumeLabel.Text        = this.Volume.ToString();
      this.volumeLabel.TextAlign   = ContentAlignment.MiddleCenter;
      this.volumeLabel.UseMnemonic = false;
    }
    // muteCheckBox
    {
      this.muteCheckBox.Name     = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new(46, 400);
      this.muteCheckBox.Text     = "Mute";
      /************************************************/
      this.muteCheckBox.Click += this.muteCheckBox_Click;
    }
    // DeviceVolumeControl
    {
      base.Name                = "DeviceVolumeControl";
      base.AutoScaleDimensions = new(6f, 13f);
      base.AutoScaleMode       = AutoScaleMode.Font;
      base.AutoSizeMode        = AutoSizeMode.GrowAndShrink;
      base.BackColor           = Color.FromArgb(242, 242, 242);
      base.Margin              = new(0);
      base.Size                = new(128, 485);
      /************************************************/
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
  private Label nameLabel = null;
  private PictureBox devicePictureBox = null;
  private CheckBox defaultCheckBox = null;
  private VLevelMeter leftLedBar = null;
  private VTrackBar volumeVTrackBar = null;
  private VLevelMeter rightLedBar = null;
  private Label volumeLabel = null;
  private CheckBox muteCheckBox = null;
}