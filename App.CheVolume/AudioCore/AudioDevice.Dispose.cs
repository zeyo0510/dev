using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public partial class AudioDevice : IDisposable
  {
    public void Dispose()
    {
      Marshal.ThrowExceptionForHR(this.AudioEndpointVolume.UnregisterControlChangeNotify(this));
    }
  }
}