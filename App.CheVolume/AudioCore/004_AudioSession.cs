using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSession
  {
    internal AudioSession(IAudioSessionControl2 _AUDIO_SESSION_CONTROL_)
    {
      this.IAudioSessionControl = _AUDIO_SESSION_CONTROL_;
    }
    /************************************************/
    public void RegisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this.IAudioSessionControl.RegisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this.IAudioSessionControl.UnregisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }
  }
}