using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private static Guid IID_IAudioClient = new("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");
    /************************************************/
    public AudioClient AudioClient
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDevice.Activate(ref IID_IAudioClient, CLSCTX.ALL, IntPtr.Zero, out object retValue));
        /************************************************/
        return new((IAudioClient)retValue);
      }
    }
  }
}