using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSession
  {
    private readonly IAudioSessionControl2 _AudioSessionControl_;
    private readonly IAudioMeterInformation _AudioMeterInformation_;
    /************************************************/
    internal AudioSession(IAudioSessionControl2 _AUDIO_SESSION_CONTROL_)
    {
      this._AudioSessionControl_   = _AUDIO_SESSION_CONTROL_;
      this._AudioMeterInformation_ = (IAudioMeterInformation)this._AudioSessionControl_;
    }
    /************************************************/
    public void RegisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.RegisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.UnregisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }
  }
}