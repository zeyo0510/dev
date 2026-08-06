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
        Marshal.ThrowExceptionForHR(this.AudioMeterInformation.GetMeteringChannelCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}