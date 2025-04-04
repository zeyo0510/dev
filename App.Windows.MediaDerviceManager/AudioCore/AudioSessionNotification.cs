using AudioCore.Interfaces;

namespace AudioCore
{
	internal class AudioSessionNotification : IAudioSessionNotification
	{
		private IAudioSessionNotificationCollection _AudioSessionNotificationCollection_;

		public AudioSessionNotification(IAudioSessionNotificationCollection P_0)
		{
			_AudioSessionNotificationCollection_ = P_0;
		}

		public int OnSessionCreated(IAudioSessionControl P_0)
		{
			return _AudioSessionNotificationCollection_.AlwaysZero(new AudioSessionControl2((IAudioSessionControl2)P_0));
		}
	}
}
