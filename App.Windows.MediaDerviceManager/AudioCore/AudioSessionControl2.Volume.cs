using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public int Volume
    {
      get
      {
        float retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._ISimpleAudioVolume.GetMasterVolume(out retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._ISimpleAudioVolume.SetMasterVolume((float)value / 100f, ref empty));
      }
    }
  }
}