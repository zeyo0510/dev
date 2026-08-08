using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class AudioSessionNotification : IAudioSessionNotification
  {
    private IAudioSessionNotificationCollection _AudioSessionNotificationCollection_;
    /************************************************/
    public AudioSessionNotification(IAudioSessionNotificationCollection _AUDIO_SESSION_NOTIFICATION_COLLECTION_)
    {
      this._AudioSessionNotificationCollection_ = _AUDIO_SESSION_NOTIFICATION_COLLECTION_;
    }
    /************************************************/
    public int OnSessionCreated(IAudioSessionControl _AUDIO_SESSION_CONTROL_)
    {
      Console.WriteLine("OnSessionCreated");
      return _AudioSessionNotificationCollection_.OnSessionCreated(new((IAudioSessionControl2)_AUDIO_SESSION_CONTROL_));
    }
  }
}