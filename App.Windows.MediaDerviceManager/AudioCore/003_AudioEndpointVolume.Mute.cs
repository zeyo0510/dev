using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    public bool Mute
    {
      get
      {
        bool retValue = false;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetMute(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.SetMute(value, Guid.Empty));
      }
    }
  }
}