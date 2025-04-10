using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionControl2
  {
    private IAudioSessionControl2  _IAudioSessionControl2  = null;
    private ISimpleAudioVolume     _ISimpleAudioVolume     = null;
    private IAudioMeterInformation _IAudioMeterInformation = null;
    /************************************************/
    internal AudioSessionControl2(IAudioSessionControl2 _)
    {
      this._IAudioSessionControl2 = _;
      /************************************************/
      this._ISimpleAudioVolume     = (ISimpleAudioVolume)this._IAudioSessionControl2;
      this._IAudioMeterInformation = (IAudioMeterInformation)this._IAudioSessionControl2;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._IAudioSessionControl2.RegisterAudioSessionNotification(this));
    }
    /************************************************/
    ~AudioSessionControl2()
    {
      this.Dispose();
    }
  }
}