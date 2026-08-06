using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
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