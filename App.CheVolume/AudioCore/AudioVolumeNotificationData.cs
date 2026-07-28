namespace AudioCore
{
  public class AudioVolumeNotificationData
  {
    public readonly bool Muted;

    public readonly float MasterVolume;

    public AudioVolumeNotificationData(bool muted, float masterVolume)
    {
      this.Muted = muted;
      this.MasterVolume = masterVolume;
    }
  }
}