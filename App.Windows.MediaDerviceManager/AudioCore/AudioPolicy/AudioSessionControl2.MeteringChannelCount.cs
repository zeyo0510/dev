using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public int MeteringChannelCount
    {
      get
      {
        int retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioMeterInformation.GetMeteringChannelCount(out retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}