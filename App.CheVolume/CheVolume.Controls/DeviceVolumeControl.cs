using a;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl : UserControl
{
  public DeviceVolumeControl(AudioDevice _MM_DEVICE_)
  {
    this.AudioDevice = _MM_DEVICE_;
    this.AudioDevice.VolumeChanged += this.AudioDevice_VolumeChanged;
    this.AudioDevice.MuteChanged += this.AudioDevice_MuteChanged;
    this.AudioDevice.SessionCreated += this.AudioDevice_SessionCreated;
    /****************************************************/
    this.InitializeComponent();
    /****************************************************/
    base.Tag = this.AudioDevice.ID;
    /****************************************************/
    this.UpdateUI();
  }
  /****************************************************/
  private void AudioDevice_VolumeChanged(object? sender, EventArgs e)
  {
    Console.WriteLine("AudioDevice_VolumeChanged");
  }
  /****************************************************/
  private void AudioDevice_MuteChanged(object? sender, EventArgs e)
  {
    Console.WriteLine("AudioDevice_MuteChanged");
  }
  /****************************************************/
  private void AudioDevice_SessionCreated(object? sender, AudioSession e)
  {
    Console.WriteLine("AudioDevice_SessionCreated");
  }
  /****************************************************/
  public void SetDefault(bool newValue)
  {
    defaultCheckBox.Checked = newValue;
  }

  // object event...

  private void guiTimer_Tick(object sender, EventArgs e)
  {
    this.UpdateUI();
  }

  private void defaultCheckBox_Click(object sender, EventArgs e)
  {
    if (defaultCheckBox.Checked)
    {
      Audio.SetDefault(this.AudioDevice.ID);
    }
    else
    {
      defaultCheckBox.Checked = true;
    }
  }

  private void volumeVTrackBar_ValueChanged(object sender, EventArgs e)
  {
    this.Volume =  this.volumeVTrackBar.Value;
    /****************************************************/
    this.UpdateUI();
  }

  private void muteCheckBox_Click(object sender, EventArgs e)
  {
    this.Mute = !this.Mute;
    /****************************************************/
    this.UpdateUI();
  }
}