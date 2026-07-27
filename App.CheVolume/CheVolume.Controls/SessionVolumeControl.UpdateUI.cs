using JC.CS.Lib.Extensions;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  public void UpdateUI()
  {
    this.leftLedBar.Enabled = !this.SessionControl.GetMute();

    macTrackBar1.TrackerColor = muteCheCheckBox.Checked ? Color.DarkGray : Color.FromArgb(255, 128, 0);
  }
}