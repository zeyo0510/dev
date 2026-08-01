using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out object retValue));
        /************************************************/
        return new(retValue as IAudioSessionManager2);
      }
    }
  }
}