using System;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    internal void OnNotify(AudioVolumeNotificationDataEventArgs e)
    {
      if (this.AudioEndpointVolumeNotification == null) return;
      /************************************************/
      this.AudioEndpointVolumeNotification(e);
    }
  }
}