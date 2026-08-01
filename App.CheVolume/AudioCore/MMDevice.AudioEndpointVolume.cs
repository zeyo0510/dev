using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    private AudioEndpointVolume _AudioEndpointVolume_;
    /************************************************/
    private static Guid IID_IAudioEndpointVolume = new("5CDF2C82-841E-4546-9722-0CF74078229A");
    /************************************************/
    public AudioEndpointVolume AudioEndpointVolume
    {
      get
      {
        if (this._AudioEndpointVolume_ == null)
        {
          Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out object retValue));
          /************************************************/
          this._AudioEndpointVolume_ = new(retValue as IAudioEndpointVolume);
        }
        /************************************************/
        return this._AudioEndpointVolume_;
      }
    }
  }
}