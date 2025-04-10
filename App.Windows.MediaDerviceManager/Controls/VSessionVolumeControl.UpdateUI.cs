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
//      System.Diagnostics.Debug.WriteLine(_AudioSessionControl1.Method1());
      this.leftVLedBar.Color = !this._AudioSessionControl1.Mute;
      this.volumeVTrackBar.Value = this._AudioSessionControl1.Volume;
      this.volumeLabel.Text = this._AudioSessionControl1.Volume.ToString();
      this.muteCheckBox.Checked = this._AudioSessionControl1.Mute;
    }
  }
}