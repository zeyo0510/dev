using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
using JC.CS.Lib.CoreAudio;
using JC.CS.Lib.Extensions;
/************************************************/
namespace AudioCore
{
  public partial class AudioDevice : IAudioEndpointVolumeCallback
  {
    public int OnNotify(nint nodify)
    {
      AUDIO_VOLUME_NOTIFICATION_DATA data = Marshal.PtrToStructure<AUDIO_VOLUME_NOTIFICATION_DATA>(nodify);
			/************************************************/
      // IntPtr intPtr = Marshal.OffsetOf(typeof(AUDIO_VOLUME_NOTIFICATION_DATA), "ChannelVolume");
      // IntPtr ptr = (IntPtr)((long)nodify + (long)intPtr);
      // float[] array = new float[data.nChannels];
      // for (int i = 0; i < data.nChannels; i++)
      // {
      //   array[i] = (float)Marshal.PtrToStructure(ptr, typeof(float));
      // }
			/************************************************/
      AudioVolumeNotificationData audioVolumeNotificationData = new(data.bMuted, data.fMasterVolume);
			/************************************************/
      int volume = (int)Math.Ceiling(data.fMasterVolume * 100f);
      /************************************************/
      if (volume.IsNotEqualTo(this.lastVolume))
      {
        this.OnVolumeChanged();
      }
      /************************************************/
      bool mute = data.bMuted;
      if (mute != this.lastMute)
      {
        this.OnMuteChanged();
      }
      /************************************************/
      return 0;
    }
  }
}