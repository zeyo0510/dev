using System;

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
