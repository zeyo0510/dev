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
      /************************************************/
      this.lastVolume = this.Volume;
      this.lastMute = this.Mute;
      /************************************************/
      Marshal.ThrowExceptionForHR(this.IAudioSessionControl.RegisterAudioSessionNotification(this));
    }
    /************************************************/
    ~AudioSession()
    {
      Marshal.ThrowExceptionForHR(this.IAudioSessionControl.UnregisterAudioSessionNotification(this));
    }
  }
}