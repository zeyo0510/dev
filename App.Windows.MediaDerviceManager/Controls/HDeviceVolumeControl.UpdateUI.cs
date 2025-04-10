using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HDeviceVolumeControl
  {
    public void UpdateUI()
    {
      this.nameLabel.Text = this._MMDevice1.NameDesc;
      this.volumeMACTrackBar.Value = this._AudioEndpointVolume.Volume;
      this.muteCheckBox.Checked = this._AudioEndpointVolume.Mute;;
    }
  }
}