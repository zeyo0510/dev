using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2 : IDisposable
  {
    public void Dispose()
    {
      if (_IAudioSessionControl2 == null) return;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._IAudioSessionControl2.UnregisterAudioSessionNotification(this));
      /************************************************/
      this._IAudioSessionControl2  = null;
      this._IAudioMeterInformation = null;
      this._ISimpleAudioVolume     = null;
    }
  }
}