using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public float[] ChannelMeters
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.GetMeteringChannelCount(out int retValue));

        float[] array = new float[retValue];
        GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array;
      }
    }
  }
}