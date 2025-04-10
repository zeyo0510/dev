using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HDeviceVolumeControl
  {
    public void UpdateUI()
    {
      this.nameLabel.Text = this._MMDevice1.NameDesc;
      this.leftHLedBar.Color = !this._AudioEndpointVolume.Mute;
      this.volumeHTrackBar.Value = this._AudioEndpointVolume.Volume;
      this.rightHLedBar.Color = !this._AudioEndpointVolume.Mute;
      this.muteCheckBox.Checked = this._AudioEndpointVolume.Mute;;
    }
  }
}