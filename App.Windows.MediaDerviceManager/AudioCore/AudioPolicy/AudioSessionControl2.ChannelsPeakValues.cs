using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public float[] ChannelsPeakValues
    {
      get
      {
        float[] retValue = new float[this.MeteringChannelCount];
        /************************************************/
        GCHandle gc = GCHandle.Alloc(retValue, GCHandleType.Pinned);
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioMeterInformation.GetChannelsPeakValues(retValue.Length, gc.AddrOfPinnedObject()));
        /************************************************/
        gc.Free();
        /************************************************/
        return retValue;
      }
    }
  }
}