using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    public int ChannelCount
    {
      get
      {
        int retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetChannelCount(out retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}