using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioMeterInformation
  {
    public float MasterPeakValue
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetPeakValue(out float retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}