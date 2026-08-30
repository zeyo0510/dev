using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice : IDisposable
  {
    public void Dispose()
    {
      Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.UnregisterControlChangeNotify(this));
      Marshal.ThrowExceptionForHR(this.IAudioSessionManager.UnregisterSessionNotification(this));
    }
  }
}