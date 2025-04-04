using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
using CheVolume.Properties;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class VDeviceVolumeControl : UserControl
  {
    private delegate void SYS_STRING_INVOKE(string A_0);

    private delegate void SYS_BOOL_INVOKE(bool A_0);

    private delegate void SYS_INT_INVOKE(int A_0);

    private Timer timer1;

    public MMDevice mmDevice1;

    private int num1;

    private bool bool1;

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
        return volumeMACTrackBar.Value;
      }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
    private static extern bool _DestroyIcon(IntPtr P_0);

    public static Image ResizeImage(Image P_0, Size P_1)
    {
      return new Bitmap(P_0, P_1);
    }

    public VDeviceVolumeControl(MMDevice P_0)
    {
      InitializeComponent();
      mmDevice1 = P_0;
      bool1 = false;
      timer1 = new Timer();
      timer1.Tick += timer1_Tick;
      timer1.Interval = 10;
      timer1.Start();
      base.Tag = mmDevice1.ID;
      nameLabel.Text = P_0.FriendlyName;
      muteCheCheckBox.Checked = EndPointVolume.Mute;
      volumeMACTrackBar.Value = EndPointVolume.Volume;
      volumeMACTrackBar.audioEndpointVolume1 = EndPointVolume;
      volumeLabel.Text = EndPointVolume.Volume.ToString();
      Icon icon = ImageHelper.Method1(mmDevice1.IconPath);
      if (icon.Height > 0)
      {
        iconPictureBox.Image = ResizeImage(icon.ToBitmap(), new Size(32, 32));
      }
      _DestroyIcon(icon.Handle);
      EndPointVolume.OnVolumeNotification += SetData;
    }

    public void SetVolumeTextV2(string P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new SYS_STRING_INVOKE(SetVolumeTextV2), P_0);
      }
      else
      {
        volumeLabel.Text = P_0;
      }
    }

    public void SetTrackBarV2(int P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new SYS_INT_INVOKE(SetTrackBarV2), P_0);
      }
      else if (!volumeMACTrackBar.bool1)
      {
        volumeMACTrackBar.Value = P_0;
      }
    }

    public void SetMuteV2(bool P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new SYS_BOOL_INVOKE(SetMuteV2), P_0);
      }
      else
      {
        muteCheCheckBox.Checked = P_0;
      }
    }

    public void SetDefaultV2(bool P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new SYS_BOOL_INVOKE(SetDefaultV2), P_0);
      }
      else
      {
        defaultCheckBox.Checked = P_0;
      }
    }

    private void SetData(AudioVolumeNotificationData P_0)
    {
      SetTrackBarV2(int.Parse(Math.Ceiling(P_0.MasterVolume * 100f).ToString()));
      SetMuteV2(P_0.Muted);
      SetVolumeTextV2(Math.Ceiling(P_0.MasterVolume * 100f).ToString());
    }

    private void timer1_Tick(object P_0, EventArgs P_1)
    {
      float[] array = new float[0];
      try
      {
        array = mmDevice1.AudioMeterInformation.PeakValues.ToFloatArray;
      }
      catch (Exception)
      {
        leftVLedBar.SetValue(0f);
        rightVLedBar.SetValue(0f);
      }
      if (Enumerable.Count(array) > 0)
      {
        if (Enumerable.Count(array) == 1)
        {
          leftVLedBar.SetValue(array[0]);
          rightVLedBar.SetValue(array[0]);
          volumeMACTrackBar.SetActualVolume(array[0]);
        }
        else
        {
          leftVLedBar.SetValue(array[0]);
          rightVLedBar.SetValue(array[1]);
          volumeMACTrackBar.SetActualVolume((array[0] + array[1]) / 2f);
        }
      }
      volumeMACTrackBar.Refresh();
    }

    private void lblProcessName_Click(object P_0, EventArgs P_1)
    {
    }

    private void btnMute_CheckedChanged(object P_0, EventArgs P_1)
    {
      if (muteCheCheckBox.Checked)
      {
        leftVLedBar.IsMuted = true;
        rightVLedBar.IsMuted = true;
        volumeMACTrackBar.TrackerColor = Color.Gray;
        muteCheCheckBox.Image = Resources.muteon;
        muteCheCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
      }
      else
      {
        leftVLedBar.IsMuted = false;
        rightVLedBar.IsMuted = false;
        volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
        muteCheCheckBox.Image = Resources.mute;
        muteCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
      }
      EndPointVolume.Mute = muteCheCheckBox.Checked;
      volumeLabel.Focus();
    }

    private void macTrackBar1_OnValueChanged(object P_0, decimal P_1)
    {
      volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
      if (volumeMACTrackBar.bool1)
      {
        volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
        EndPointVolume.Mute = false;
      }
    }

    public void SetDefault(bool P_0)
    {
      defaultCheckBox.Checked = P_0;
    }

    private void chkSetDefault_MouseClick(object P_0, MouseEventArgs P_1)
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

    private void _OnPaint(object P_0, PaintEventArgs P_1)
    {
    }

    private void btnMute_MouseEnter(object P_0, EventArgs P_1)
    {
      muteCheCheckBox.Image = Resources.muteon;
      muteCheCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
    }

    private void btnMute_MouseLeave(object P_0, EventArgs P_1)
    {
      if (!muteCheCheckBox.Checked)
      {
        muteCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
        muteCheCheckBox.Image = Resources.mute;
      }
    }
  }
}