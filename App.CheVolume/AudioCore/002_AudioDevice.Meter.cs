using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public float Meter
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.GetPeakValue(out float retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}