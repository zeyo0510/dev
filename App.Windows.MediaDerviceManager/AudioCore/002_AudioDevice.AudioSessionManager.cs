using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private AudioSessionManager2 _AudioSessionManager;
    /************************************************/
    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        if (this._AudioSessionManager == null)
        {
          object retValue;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioSessionManager = new AudioSessionManager2(retValue as IAudioSessionManager2);
        }
        /************************************************/
        return this._AudioSessionManager;
      }
    }
  }
}