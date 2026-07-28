using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume : IDisposable
  {
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.SetMute(value, Guid.Empty));
      }
    }
  }
}