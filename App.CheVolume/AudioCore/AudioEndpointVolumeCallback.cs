using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
  {
    private readonly AudioEndpointVolume _AudioEndpointVolume_;

    internal AudioEndpointVolumeCallback(AudioEndpointVolume _AUDIO_ENDPOINT_VOLUME_)
    {
      this._AudioEndpointVolume_ = _AUDIO_ENDPOINT_VOLUME_;
    }

    [PreserveSig]
    public int OnNotify(IntPtr P_0)
    {
      AUDIO_VOLUME_NOTIFICATION_DATA aUDIO_VOLUME_NOTIFICATION_DATA = (AUDIO_VOLUME_NOTIFICATION_DATA)Marshal.PtrToStructure(P_0, typeof(AUDIO_VOLUME_NOTIFICATION_DATA));
			/************************************************/
      IntPtr intPtr = Marshal.OffsetOf(typeof(AUDIO_VOLUME_NOTIFICATION_DATA), "ChannelVolume");
      IntPtr ptr = (IntPtr)((long)P_0 + (long)intPtr);
      float[] array = new float[aUDIO_VOLUME_NOTIFICATION_DATA.nChannels];
      for (int i = 0; i < aUDIO_VOLUME_NOTIFICATION_DATA.nChannels; i++)
      {
        array[i] = (float)Marshal.PtrToStructure(ptr, typeof(float));
      }
			/************************************************/
      AudioVolumeNotificationData audioVolumeNotificationData = new AudioVolumeNotificationData(aUDIO_VOLUME_NOTIFICATION_DATA.bMuted, aUDIO_VOLUME_NOTIFICATION_DATA.fMasterVolume);
			/************************************************/
      this._AudioEndpointVolume_.FireNotification(audioVolumeNotificationData);
			/************************************************/
      return 0;
    }
  }
}