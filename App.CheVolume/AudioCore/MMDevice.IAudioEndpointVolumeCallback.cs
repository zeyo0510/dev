using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class MMDevice : IAudioEndpointVolumeCallback
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
      this.OnNotifyChanged();
      /************************************************/
      return 0;
    }
  }
}