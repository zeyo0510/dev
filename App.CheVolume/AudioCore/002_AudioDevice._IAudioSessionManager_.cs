using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private static Guid IID_IAudioSessionManager2 = new("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F");
    /************************************************/
    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.MMDevice.Activate(ref IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out object retValue));
        /************************************************/
        return new((IAudioSessionManager2)retValue);
      }
    }
  }
}