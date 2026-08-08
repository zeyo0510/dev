using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    // TODO: JC TEST
    public float[] ChannelMeters
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.GetMeteringChannelCount(out int count));
        /************************************************/
        float[] retValue = new float[count];
        /************************************************/
        GCHandle gcHandle = GCHandle.Alloc(retValue, GCHandleType.Pinned);
        /************************************************/
        Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.GetChannelsPeakValues(retValue.Length, gcHandle.AddrOfPinnedObject()));
        /************************************************/
        gcHandle.Free();
        /************************************************/
        return retValue;
      }
    }
  }
}