using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
	{
		private readonly AudioEndpointVolume audioEndpointVolume1;

		internal AudioEndpointVolumeCallback(AudioEndpointVolume P_0)
		{
			audioEndpointVolume1 = P_0;
		}

		[PreserveSig]
		public int OnNotify(IntPtr P_0)
		{
			AUDIO_VOLUME_NOTIFICATION_DATA aUDIO_VOLUME_NOTIFICATION_DATA = (AUDIO_VOLUME_NOTIFICATION_DATA)Marshal.PtrToStructure(P_0, typeof(AUDIO_VOLUME_NOTIFICATION_DATA));
			IntPtr intPtr = Marshal.OffsetOf(typeof(AUDIO_VOLUME_NOTIFICATION_DATA), "ChannelVolume");
			IntPtr ptr = (IntPtr)((long)P_0 + (long)intPtr);
			float[] array = new float[aUDIO_VOLUME_NOTIFICATION_DATA.nChannels];
			for (int i = 0; i < aUDIO_VOLUME_NOTIFICATION_DATA.nChannels; i++)
			{
				array[i] = (float)Marshal.PtrToStructure(ptr, typeof(float));
			}
			AudioVolumeNotificationData audioVolumeNotificationData = new AudioVolumeNotificationData(aUDIO_VOLUME_NOTIFICATION_DATA.bMuted, aUDIO_VOLUME_NOTIFICATION_DATA.fMasterVolume);
			audioEndpointVolume1.FireNotification(audioVolumeNotificationData);
			return 0;
		}
	}
}
