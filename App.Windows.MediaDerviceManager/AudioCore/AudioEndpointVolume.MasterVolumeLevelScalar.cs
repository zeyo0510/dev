using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    public float MasterVolumeLevelScalar
    {
      get
      {
        float retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetMasterVolumeLevelScalar(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
      }
    }
  }
}