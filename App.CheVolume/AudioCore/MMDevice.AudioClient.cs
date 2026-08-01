using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    public AudioClient AudioClient
    {
      get
      {
        Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioClient, CLSCTX.ALL, IntPtr.Zero, out object retValue));
        /************************************************/
        return new(retValue as IAudioClient);
      }
    }
  }
}