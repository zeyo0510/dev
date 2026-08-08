using AudioCore.Interfaces;
/************************************************/
using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  public class AudioSessionManager2
  {
    public readonly AudioSessionCollection _AudioSessionEnumerator_;

    private readonly IAudioSessionManager2 _AudioSessionManager_;

    private Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification> audioSessionNotificationCollection1 = new Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification>();

    internal AudioSessionManager2(IAudioSessionManager2 _AUDIO_SESSION_MANAGER_)
    {
      this._AudioSessionManager_ = _AUDIO_SESSION_MANAGER_;
    }

    public void RegisterSessionNotification(IAudioSessionNotificationCollection _AUDIO_SESSION_NOTIFICATION_COLLECTION_)
    {
      AudioSessionNotification audioSessionNotification = new(_AUDIO_SESSION_NOTIFICATION_COLLECTION_);
      /************************************************/
      audioSessionNotificationCollection1.Add(_AUDIO_SESSION_NOTIFICATION_COLLECTION_, audioSessionNotification);
      /************************************************/
      this._AudioSessionManager_.RegisterSessionNotification(audioSessionNotification);
    }

    public void UnregisterSessionNotification(IAudioSessionNotificationCollection _AUDIO_SESSION_NOTIFICATION_COLLECTION_)
    {
      this._AudioSessionManager_.UnregisterSessionNotification(audioSessionNotificationCollection1[_AUDIO_SESSION_NOTIFICATION_COLLECTION_]);
      /************************************************/
      audioSessionNotificationCollection1.Remove(_AUDIO_SESSION_NOTIFICATION_COLLECTION_);
    }
  }
}