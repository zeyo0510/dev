using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private AudioEndpointVolume _AudioEndpointVolume;
    /************************************************/
    public AudioEndpointVolume AudioEndpointVolume
    {
      get
      {
        if (this._AudioEndpointVolume == null)
        {
          object retValue;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioEndpointVolume = new AudioEndpointVolume(retValue as IAudioEndpointVolume);
        }
        /************************************************/
        return this._AudioEndpointVolume;
      }
    }
  }
}