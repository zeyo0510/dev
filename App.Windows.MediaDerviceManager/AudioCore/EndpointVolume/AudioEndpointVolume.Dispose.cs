using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume : IDisposable
  {
    public void Dispose()
    {
      if (this._AudioEndpointVolumeCallback == null) return;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.UnregisterControlChangeNotify(this._AudioEndpointVolumeCallback));
      /************************************************/
      this._AudioEndpointVolumeCallback = null;
    }
  }
}