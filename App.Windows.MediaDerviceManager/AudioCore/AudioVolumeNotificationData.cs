namespace AudioCore
{
	public class AudioVolumeNotificationData
	{
		public readonly bool Muted;

		public readonly float MasterVolume;

		public AudioVolumeNotificationData(bool P_0, float P_1)
		{
			Muted = P_0;
			MasterVolume = P_1;
		}
	}
}
