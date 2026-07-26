using System.ComponentModel;
using System.Runtime.InteropServices;
using WinForm = System.Windows.Forms;
using a;
using AudioCore;
using CheVolume.Properties;
using EConTech.Windows.MACUI;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl : UserControl
{
  private delegate void SYS_STRING_INVOKE(string A_0);

  private delegate void SYS_BOOL_INVOKE(bool A_0);

  private delegate void SYS_INT_INVOKE(int A_0);

  private WinForm::Timer timer1;

  public MMDevice mmDevice1;

  private AudioEndpointVolume EndPointVolume
  {
    get
    {
      return mmDevice1.AudioEndpointVolume;
    }
  }

  private int VolumeValue
  {
    get
    {
      return macTrackBar1.Value;
    }
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
  private static extern bool _DestroyIcon(IntPtr P_0);

  public static Image ResizeImage(Image P_0, Size P_1)
  {
    return new Bitmap(P_0, P_1);
  }

  public DeviceVolumeControl(MMDevice P_0)
  {
    InitializeComponent();
    mmDevice1 = P_0;
    timer1 = new WinForm::Timer();
    timer1.Tick += timer1_Tick;
    timer1.Interval = 10;
    timer1.Start();
    base.Tag = mmDevice1.ID;
    nameLabel.Text = P_0.FriendlyName;
    muteCheckBox.Checked = EndPointVolume.Mute;
    macTrackBar1.Value = EndPointVolume.Volume;
    macTrackBar1.audioEndpointVolume1 = EndPointVolume;
    volumeLabel.Text = EndPointVolume.Volume.ToString();
    Icon icon = ImageHelper.Method1(mmDevice1.IconPath);
    if (icon.Height > 0)
    {
      this.devicePictureBox.Image = ResizeImage(icon.ToBitmap(), new Size(32, 32));
    }
    _DestroyIcon(icon.Handle);
    EndPointVolume.OnVolumeNotification += SetData;
  }

  public void SetVolumeTextV2(string newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_STRING_INVOKE(SetVolumeTextV2), newValue);
    }
    else
    {
      volumeLabel.Text = newValue;
    }
  }

  public void SetTrackBarV2(int newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_INT_INVOKE(SetTrackBarV2), newValue);
    }
    else if (!macTrackBar1.bool1)
    {
      macTrackBar1.Value = newValue;
    }
  }

  public void SetMuteV2(bool newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_BOOL_INVOKE(SetMuteV2), newValue);
    }
    else
    {
      muteCheckBox.Checked = newValue;
    }
  }

  public void SetDefaultV2(bool newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_BOOL_INVOKE(SetDefaultV2), newValue);
    }
    else
    {
      defaultCheckBox.Checked = newValue;
    }
  }

  private void SetData(AudioVolumeNotificationData P_0)
  {
    SetTrackBarV2(int.Parse(Math.Ceiling(P_0.MasterVolume * 100f).ToString()));
    SetMuteV2(P_0.Muted);
    SetVolumeTextV2(Math.Ceiling(P_0.MasterVolume * 100f).ToString());
  }

  private void timer1_Tick(object sender, EventArgs e)
  {
    float[] array = new float[0];
    try
    {
      array = mmDevice1.AudioMeterInformation.PeakValues.ToFloatArray;
    }
    catch (Exception)
    {
      leftLedBar.SetValue(0f);
      rightLedBar.SetValue(0f);
    }
    if (Enumerable.Count(array) > 0)
    {
      if (Enumerable.Count(array) == 1)
      {
        leftLedBar.SetValue(array[0]);
        rightLedBar.SetValue(array[0]);
        macTrackBar1.SetActualVolume(array[0]);
      }
      else
      {
        leftLedBar.SetValue(array[0]);
        rightLedBar.SetValue(array[1]);
        macTrackBar1.SetActualVolume((array[0] + array[1]) / 2f);
      }
    }
    macTrackBar1.Refresh();
  }

  private void muteCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (muteCheckBox.Checked)
    {
      leftLedBar.IsMuted = true;
      rightLedBar.IsMuted = true;
      macTrackBar1.TrackerColor = Color.Gray;
      muteCheckBox.Image = Resources.muteon;
      muteCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
    }
    else
    {
      leftLedBar.IsMuted = false;
      rightLedBar.IsMuted = false;
      macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
      muteCheckBox.Image = Resources.mute;
      muteCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
    }
    EndPointVolume.Mute = muteCheckBox.Checked;
    volumeLabel.Focus();
  }

  private void macTrackBar1_OnValueChanged(object sender, decimal e)
  {
    macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
    if (macTrackBar1.bool1)
    {
      macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
      EndPointVolume.Mute = false;
    }
  }

  public void SetDefault(bool newValue)
  {
    defaultCheckBox.Checked = newValue;
  }

  private void muteCheckBox_MouseUp(object sender, MouseEventArgs e)
  {
  }

  private void defaultCheckBox_CheckedChanged(object sender, EventArgs e)
  {
  }

  private void defaultCheckBox_MouseClick(object sender, MouseEventArgs e)
  {
    if (defaultCheckBox.Checked)
    {
      Class4.Method3(mmDevice1.ID);
    }
    else
    {
      defaultCheckBox.Checked = true;
    }
  }

  private void muteCheckBox_MouseEnter(object sender, EventArgs e)
  {
    muteCheckBox.Image = Resources.muteon;
    muteCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
  }

  private void muteCheckBox_MouseLeave(object sender, EventArgs e)
  {
    if (!muteCheckBox.Checked)
    {
      muteCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
      muteCheckBox.Image = Resources.mute;
    }
  }
}