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
        Marshal.ThrowExceptionForHR(this.AudioEndpointVolume.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.AudioEndpointVolume.SetMute(value, Guid.Empty));
      }
    }
  }
}