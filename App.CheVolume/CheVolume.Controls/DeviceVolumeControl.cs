using a;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl : UserControl
{
  public DeviceVolumeControl(MMDevice _MM_DEVICE_)
  {
    this.InitializeComponent();
    /****************************************************/
    this.MMDevice = _MM_DEVICE_;
    /****************************************************/
    this.guiTimer.Start();
    /****************************************************/
    base.Tag = this.MMDevice.ID;
    /****************************************************/
    this.UpdateUI();
    /****************************************************/
    this.EndPointVolume.OnVolumeNotification += SetData;
  }

  public void SetVolumeTextV2(string newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<string>(SetVolumeTextV2), newValue);
      return;
    }
    volumeLabel.Text = newValue;
  }

  public void SetTrackBarV2(int newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<int>(SetTrackBarV2), newValue);
      return;
    }

    this.volumeVTrackBar.Value = newValue;
  }

  public void SetMuteV2(bool newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<bool>(SetMuteV2), newValue);
      return;
    }

    muteCheckBox.Checked = newValue;
  }

  public void SetDefaultV2(bool newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<bool>(SetDefaultV2), newValue);
      return;
    }

    defaultCheckBox.Checked = newValue;
  }

  private void SetData(AudioVolumeNotificationData data)
  {
    SetTrackBarV2(int.Parse(Math.Ceiling(data.MasterVolume * 100f).ToString()));
    SetMuteV2(data.Muted);
    SetVolumeTextV2(Math.Ceiling(data.MasterVolume * 100f).ToString());
  }

  public void SetDefault(bool newValue)
  {
    defaultCheckBox.Checked = newValue;
  }

  private void guiTimer_Tick(object sender, EventArgs e)
  {
    this.UpdateUI();
  }

  private void defaultCheckBox_CheckedChanged(object sender, EventArgs e)
  {
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