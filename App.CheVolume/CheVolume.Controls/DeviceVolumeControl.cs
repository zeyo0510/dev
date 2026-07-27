using a;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl : UserControl
{
  public DeviceVolumeControl(MMDevice _MM_DEVICE_)
  {
    this.MMDevice = _MM_DEVICE_;
    /****************************************************/
    this.InitializeComponent();
    /****************************************************/
    base.Tag = this.MMDevice.ID;
    /****************************************************/
    this.UpdateUI();
    /****************************************************/
    this.EndPointVolume.OnVolumeNotification += SetData;
  }

  public void SetVolume(int newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<int>(SetVolume), newValue);
      return;
    }

    // this.volumeVTrackBar.Value = newValue;
    // this.volumeLabel.Text = newValue.ToString();
  }

  public void SetMute(bool newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<bool>(SetMute), newValue);
      return;
    }

    // muteCheckBox.Checked = newValue;
  }

  private void SetData(AudioVolumeNotificationData data)
  {
    SetVolume(int.Parse(Math.Ceiling(data.MasterVolume * 100f).ToString()));
    SetMute(data.Muted);
  }

  public void SetDefault(bool newValue)
  {
    defaultCheckBox.Checked = newValue;
  }

  private void guiTimer_Tick(object sender, EventArgs e)
  {
    this.UpdateUI();
  }

  private void defaultCheckBox_MouseClick(object sender, MouseEventArgs e)
  {
    if (defaultCheckBox.Checked)
    {
      Class4.Method3(this.MMDevice.ID);
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