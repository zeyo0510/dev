using System;
/************************************************/
namespace AudioCore
{
  public partial class AudioVolumeNotificationDataEventArgs : EventArgs
  {
    public AudioVolumeNotificationDataEventArgs(bool muted, float masterVolume)
    {
      this.Muted = muted;
      /************************************************/
      this.MasterVolume = masterVolume;
    }
  }
}