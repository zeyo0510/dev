namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  public void UpdateUI()
  {
    if (base.InvokeRequired)
    {
      this.BeginInvoke(new Action(this.UpdateUI));
      return;
    }
    /************************************************/
    this.leftLedBar.Enabled = !this.Mute;
    // volumeVTrackBar
    this.volumeVTrackBar.Value = this.Volume;
    // volumeLabel
    this.volumeLabel.Text = this.Volume.ToString();
  }
}