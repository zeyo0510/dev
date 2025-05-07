using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private AudioMeterInformation _AudioMeterInformation;
    /************************************************/
    public AudioMeterInformation AudioMeterInformation
    {
      get
      {
        if (this._AudioMeterInformation == null)
        {
          object retValue;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioMeterInformation = new AudioMeterInformation(retValue as IAudioMeterInformation);
        }
        return this._AudioMeterInformation;
      }
    }
  }
}