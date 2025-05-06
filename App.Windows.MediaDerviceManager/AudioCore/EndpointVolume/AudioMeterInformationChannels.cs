using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public class AudioMeterInformationChannels
  {
    private IAudioMeterInformation _AudioMeterInformation_ = null;
    /************************************************/
    internal AudioMeterInformationChannels(IAudioMeterInformation obj)
    {
      this._AudioMeterInformation_ = obj;
    }
    /************************************************/
    public float this[int P_0]
    {
      get
      {
        float[] array = new float[Count];
        GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array[P_0];
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        int retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetMeteringChannelCount(out retValue));
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public float[] ToFloatArray
    {
      get
      {
        float[] array = new float[Count];
        GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array;
      }
    }
  }
}