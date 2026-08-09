using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public partial class AudioSession : IDisposable
  {
    public void Dispose()
    {
      Marshal.ThrowExceptionForHR(this.IAudioSessionControl.UnregisterAudioSessionNotification(this));
    }
  }
}