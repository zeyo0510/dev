using JC.CS.Lib.Extensions;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  public void UpdateUI()
  {
    this.leftLedBar.Enabled = !this.audioSessionControl21.GetMute();
  }
}