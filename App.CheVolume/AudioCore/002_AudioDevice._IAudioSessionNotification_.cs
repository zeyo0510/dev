using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioDevice : IAudioSessionNotification
  {
    public event EventHandler<AudioSession>? SessionCreated = null;
    public int OnSessionCreated([In] IAudioSessionControl _NEW_SESSION_)
    {
      this.SessionCreated?.Invoke(this, new AudioSession((IAudioSessionControl2)_NEW_SESSION_));
      /************************************************/
      return 0;
    }
  }
}