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
    this.macTrackBar1.audioEndpointVolume1 = this.EndPointVolume;
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

    if (!macTrackBar1.bool1)
    {
      macTrackBar1.Value = newValue;
    }
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
    float[] array = this.MeterInformation.PeakValues.ToFloatArray;
    /****************************************************/
    if (Enumerable.Count(array) > 0)
    {
      if (Enumerable.Count(array) == 1)
      {
        this.macTrackBar1.SetActualVolume(array[0]);
      }
      else
      {
        this.macTrackBar1.SetActualVolume((array[0] + array[1]) / 2f);
      }
    }
    /****************************************************/
    this.UpdateUI();
    /****************************************************/
    this.macTrackBar1.Refresh();
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

  private void macTrackBar1_OnValueChanged(object sender, decimal e)
  {
    if (macTrackBar1.bool1) // down?
    {
      this.Mute = false;
    }
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