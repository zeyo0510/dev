using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    /************************************************/
    private static Guid IID_IAudioMeterInformation = new("C02216F6-8C67-4B5B-9D00-D008E73E0064");
    /************************************************/
    public IAudioMeterInformation IAudioMeterInformation
    {
      get
      {
        if (field == null)
        {
          Marshal.ThrowExceptionForHR(this.IMMDevice.Activate(ref IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out object retValue));
          /************************************************/
          field = (IAudioMeterInformation)retValue;
        }
        /************************************************/
        return field;
      }
    }
  }
}