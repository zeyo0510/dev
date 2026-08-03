using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public partial class MMDevice : IDisposable
  {
    public void Dispose()
    {
      Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.UnregisterControlChangeNotify(this));
    }
  }
}