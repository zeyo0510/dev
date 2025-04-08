using System;
using System.Drawing;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VDeviceVolumeControl
  {
    public void UpdateUI()
    {
      if (base.InvokeRequired)
      {
        base.Invoke(new Action(UpdateUI));
      }
      /************************************************/
      this.nameLabel.Text        = this._MMDevice1.FriendlyName;
      this.leftVLedBar.Color     = !this._AudioEndpointVolume1.Mute;
      this.volumeVTrackBar.Value = this._AudioEndpointVolume1.Volume;
      this.rightVLedBar.Color    = !this._AudioEndpointVolume1.Mute;
      this.volumeLabel.Text      = this._AudioEndpointVolume1.Volume.ToString();
      this.muteCheckBox.Checked  = this._AudioEndpointVolume1.Mute;
    }
  }
}