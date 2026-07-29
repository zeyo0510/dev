// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/ns-endpointvolume-audio_volume_notification_data
/************************************************/
namespace AudioCore.Interfaces
{
	internal struct AUDIO_VOLUME_NOTIFICATION_DATA
	{
		public Guid guidEventContext;

		public bool bMuted;

		public float fMasterVolume;

		public uint nChannels;

		public float ChannelVolume;
	}
}
