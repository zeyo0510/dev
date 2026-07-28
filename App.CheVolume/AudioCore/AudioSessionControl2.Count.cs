using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetMeteringChannelCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}