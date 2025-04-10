using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public uint ProcessID
    {
      get
      {
        uint retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioSessionControl2.GetProcessId(out retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}