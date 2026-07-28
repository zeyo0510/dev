using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioMeterInformationChannels
  {
    private IAudioMeterInformation _AudioMeterInformation_;
    /************************************************/
    internal AudioMeterInformationChannels(IAudioMeterInformation _AUDIO_METER_INFORMATION_)
    {
      this._AudioMeterInformation_ = _AUDIO_METER_INFORMATION_;
    }
    /************************************************/
    public float this[int index]
    {
      get
      {
        float[] array = new float[Count];
        GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array[index];
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetMeteringChannelCount(out int retValue));
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
        Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
        gCHandle.Free();
        return array;
      }
    }
  }
}
