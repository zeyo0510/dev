using System;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VSessionVolumeControl
  {
    public void UpdateUI()
    {
      if (base.InvokeRequired)
      {
        base.Invoke(new Action(UpdateUI));
      }
      /************************************************/
      this.leftVLedBar.Color = !this._AudioSessionControl1.GetMute();
      this.volumeVTrackBar.Value = int.Parse(Math.Ceiling(this._AudioSessionControl1.GetVolume() * 100f).ToString());
      this.volumeLabel.Text = Math.Ceiling(this._AudioSessionControl1.GetVolume() * 100f).ToString();
      this.muteCheckBox.Checked = this._AudioSessionControl1.GetMute();
    }
  }
}