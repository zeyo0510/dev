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