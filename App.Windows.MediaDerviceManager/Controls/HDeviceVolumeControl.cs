using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class HDeviceVolumeControl : UserControl
  {
    private MMDevice            _MMDevice1           = null;
    private AudioEndpointVolume _AudioEndpointVolume = null;
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
    private static extern bool _DestroyIcon(IntPtr P_0);

    public static Image ResizeImage(Image P_0, Size P_1)
    {
      return new Bitmap(P_0, P_1);
    }

    public HDeviceVolumeControl(MMDevice device)
    {
      InitializeComponent();
      /************************************************/
      _MMDevice1           = device;
      _AudioEndpointVolume = _MMDevice1.AudioEndpointVolume;
      _AudioEndpointVolume.OnVolumeNotification += _AudioEndpointVolume_OnVolumeNotification;
      /************************************************/
      guiTimer.Start();
      /************************************************/
      base.Tag = _MMDevice1.ID;
      /************************************************/
      volumeMACTrackBar.audioEndpointVolume1 = _AudioEndpointVolume;
      /************************************************/
      Icon icon = ImageHelper.Method1(_MMDevice1.IconPath);
      if (icon.Height > 0)
      {
        iconPictureBox.Image = ResizeImage(icon.ToBitmap(), new Size(32, 32));
      }
      _DestroyIcon(icon.Handle);
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void _AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData P_0)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void timer1_Tick(object P_0, EventArgs P_1)
    {
      float[] array = new float[0];
      try
      {
        array = _MMDevice1.AudioMeterInformation.PeakValues.ToFloatArray;
      }
      catch (Exception)
      {
        volumeMACTrackBar.SetActualVolume(0f);
      }
      if (Enumerable.Count(array) > 0)
      {
        if (Enumerable.Count(array) == 1)
        {
          volumeMACTrackBar.SetActualVolume(array[0]);
        }
        else
        {
          volumeMACTrackBar.SetActualVolume((array[0] + array[1]) / 2f);
        }
      }
      volumeMACTrackBar.Refresh();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void defaultCheckBox_MouseClick(object sender, MouseEventArgs e)
    {
      if (defaultCheckBox.Checked)
      {
        Class4.SetDefaultEndpoint(_MMDevice1.ID);
      }
      else
      {
        defaultCheckBox.Checked = true;
      }
    }
    /************************************************/
    private void muteCheckBox_Click(object sender, EventArgs e)
    {
      this._AudioEndpointVolume.Mute = !this._AudioEndpointVolume.Mute;
    }
    /************************************************/
    private void volumeMACTrackBar_ValueChanged(object sender, decimal n)
    {
      volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
      if (volumeMACTrackBar.bool1)
      {
        volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
        _AudioEndpointVolume.Mute = false;
      }
    }
  }
}