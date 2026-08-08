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
    public IAudioSessionManager2 IAudioSessionManager
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDevice.Activate(ref IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out object retValue));
        /************************************************/
        return (IAudioSessionManager2)retValue;
      }
    }
    /************************************************/
    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        return new(this.IAudioSessionManager);
      }
    }
  }
}