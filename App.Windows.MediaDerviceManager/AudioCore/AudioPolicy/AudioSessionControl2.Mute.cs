using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public bool Mute
    {
      get
      {
        bool retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._ISimpleAudioVolume.GetMute(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._ISimpleAudioVolume.SetMute(value, ref empty));
      }
    }
  }
}