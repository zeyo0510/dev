using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    /************************************************/
    private static Guid IID_IAudioEndpointVolume = new("5CDF2C82-841E-4546-9722-0CF74078229A");
    /************************************************/
    public IAudioEndpointVolume IAudioEndpointVolume
    {
      get
      {
        if (field == null)
        {
          Marshal.ThrowExceptionForHR(this.IMMDevice.Activate(ref IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out object retValue));
          /************************************************/
          field = (IAudioEndpointVolume)retValue;
        }
        /************************************************/
        return field;
      }
    }
  }
}