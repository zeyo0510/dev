using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioSessionManager2
  {
    public readonly AudioSessionEnumerator _AudioSessionEnumerator_;

    private readonly IAudioSessionManager2 _AudioSessionManager_;

    private AudioSessionManager2 audioSessionManager21;

    private Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification> audioSessionNotificationCollection1 = new Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification>();

    internal AudioSessionManager2(IAudioSessionManager2 _AUDIO_SESSION_MANAGER_)
    {
      this._AudioSessionManager_ = _AUDIO_SESSION_MANAGER_;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._AudioSessionManager_.GetSessionEnumerator(out IAudioSessionEnumerator audioSessionEnumerator));
      /************************************************/
      _AudioSessionEnumerator_ = new AudioSessionEnumerator(audioSessionEnumerator);
    }

    public int Count
    {
      get
      {
        return this._AudioSessionEnumerator_.Count;
      }
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

    public SimpleAudioVolume GetSimpleAudioVolume(Guid guid)
    {
      _AudioSessionManager_.GetSimpleAudioVolume(ref guid, 0u, out ISimpleAudioVolume simpleAudioVolume);
      /************************************************/
      return new SimpleAudioVolume(simpleAudioVolume);
    }
  }
}