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
  public partial class VDeviceVolumeControl : UserControl
  {
    private MMDevice              _MMDevice1             = null;
    private AudioEndpointVolume   _AudioEndpointVolume1  = null;
    private AudioMeterInformation _AudioMeterInformation = null;
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
    private static extern bool _DestroyIcon(IntPtr hIcon);

    public static Image ResizeImage(Image img, Size size)
    {
      return new Bitmap(img, size);
    }

    public VDeviceVolumeControl(MMDevice device)
    {
      this.InitializeComponent();
      /************************************************/
      this._MMDevice1             = device;
      this._AudioEndpointVolume1  = this._MMDevice1.AudioEndpointVolume;
      this._AudioMeterInformation = this._MMDevice1.AudioMeterInformation;
      this._AudioEndpointVolume1.AudioEndpointVolumeNotification += this._AudioEndpointVolume1_OnVolumeNotification;
      /************************************************/
      guiTimer.Start();
      /************************************************/
      base.Tag = _MMDevice1.ID;
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
    private void _AudioEndpointVolume1_OnVolumeNotification(AudioVolumeNotificationDataEventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      float[] array = new float[0];
      try
      {
        array = _MMDevice1.AudioMeterInformation.PeakValues.ToFloatArray;
      }
      catch (Exception)
      {
        leftVLedBar.Value = 0f;
        rightVLedBar.Value = 0f;
      }
      if (Enumerable.Count(array) > 0)
      {
        if (Enumerable.Count(array) == 1)
        {
          leftVLedBar.Value = array[0];
          rightVLedBar.Value = array[0];
        }
        else
        {
          leftVLedBar.Value = array[0];
          rightVLedBar.Value = array[1];
        }
      }
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void defaultCheckBox_Click(object sender, EventArgs e)
    {
      if (defaultCheckBox.Checked)
      {
        AudioManager.SetDefaultEndpoint(_MMDevice1.ID);
      }
      else
      {
        defaultCheckBox.Checked = true;
      }
    }
    /************************************************/
    private void volumeVTrackBar_ValueChanged(object sender, EventArgs e)
    {
      this._AudioEndpointVolume1.Volume = this.volumeVTrackBar.Value;
    }
    /************************************************/
    private void muteCheckBox_Click(object sender, EventArgs e)
    {
      this._AudioEndpointVolume1.Mute = !this._AudioEndpointVolume1.Mute;
    }

    public void SetDefault(bool P_0)
    {
      defaultCheckBox.Checked = P_0;
    }
  }
}