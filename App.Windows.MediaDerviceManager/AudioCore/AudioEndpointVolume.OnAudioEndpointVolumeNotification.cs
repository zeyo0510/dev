using System;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    internal void OnAudioEndpointVolumeNotification(AudioVolumeNotificationDataEventArgs e)
    {
      if (this.AudioEndpointVolumeNotification == null) return;
      /************************************************/
      this.AudioEndpointVolumeNotification(e);
    }
  }
}