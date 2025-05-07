using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private AudioClient _AudioClient;
    /************************************************/
    public AudioClient AudioClient
    {
      get
      {
        if (this._AudioClient == null)
        {
          object retValue;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioClient, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioClient = new AudioClient(retValue as IAudioClient);
        }
        /************************************************/
        return this._AudioClient;
      }
    }
  }
}