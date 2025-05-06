using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
  {
    private readonly AudioEndpointVolume _AudioEndpointVolume = null;
    /************************************************/
    internal AudioEndpointVolumeCallback(AudioEndpointVolume obj)
    {
      this._AudioEndpointVolume = obj;
    }
    /************************************************/
    [PreserveSig]
    public int OnNotify(IntPtr P_0)
    {
      AUDIO_VOLUME_NOTIFICATION_DATA _AUDIO_VOLUME_NOTIFICATION_DATA_ = (AUDIO_VOLUME_NOTIFICATION_DATA)Marshal.PtrToStructure(P_0, typeof(AUDIO_VOLUME_NOTIFICATION_DATA));
      /************************************************/
      IntPtr intPtr = Marshal.OffsetOf(typeof(AUDIO_VOLUME_NOTIFICATION_DATA), "ChannelVolume");
      IntPtr ptr = (IntPtr)((long)P_0 + (long)intPtr);
      float[] array = new float[_AUDIO_VOLUME_NOTIFICATION_DATA_.nChannels];
      for (int i = 0; i < _AUDIO_VOLUME_NOTIFICATION_DATA_.nChannels; i++)
      {
        array[i] = (float)Marshal.PtrToStructure(ptr, typeof(float));
      }
      /************************************************/
      bool  muted        = _AUDIO_VOLUME_NOTIFICATION_DATA_.bMuted;
      float masterVolume = _AUDIO_VOLUME_NOTIFICATION_DATA_.fMasterVolume;
      /************************************************/
      this._AudioEndpointVolume.OnNotify(new AudioVolumeNotificationDataEventArgs(muted, masterVolume));
      /************************************************/
      return 0;
    }
  }
}