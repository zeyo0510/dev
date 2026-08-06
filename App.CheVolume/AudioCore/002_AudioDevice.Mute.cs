using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.SetMute(value, Guid.Empty));
      }
    }
  }
}