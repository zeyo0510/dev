using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.SetMute(value, Guid.Empty));
      }
    }
  }
}