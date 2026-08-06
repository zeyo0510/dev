using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public float[] ChannelsPeakValues
    {
      get
      {
        float[] array = new float[this.Count];
        GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array;
      }
    }
  }
}