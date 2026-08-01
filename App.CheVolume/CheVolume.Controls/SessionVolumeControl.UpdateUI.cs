namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  public void UpdateUI()
  {
    this.leftLedBar.Enabled = !this.Mute;

    macTrackBar1.TrackerColor = this.muteCheckBox.Checked ? Color.DarkGray : Color.FromArgb(255, 128, 0);
  }
}