using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionControl2
  {
    private readonly IAudioSessionControl2  _IAudioSessionControl2  = null;
    private readonly ISimpleAudioVolume     _ISimpleAudioVolume     = null;
    private readonly IAudioMeterInformation _IAudioMeterInformation = null;
    /************************************************/
    internal AudioSessionControl2(IAudioSessionControl2 _)
    {
      this._IAudioSessionControl2 = _;
      /************************************************/
      this._ISimpleAudioVolume     = (ISimpleAudioVolume)this._IAudioSessionControl2;
      this._IAudioMeterInformation = (IAudioMeterInformation)this._IAudioSessionControl2;
    }
    
    public void RegisterAudioSessionNotification(IAudioSessionEvents P_0)
    {
      Marshal.ThrowExceptionForHR(_IAudioSessionControl2.RegisterAudioSessionNotification(P_0));
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents P_0)
    {
      Marshal.ThrowExceptionForHR(_IAudioSessionControl2.UnregisterAudioSessionNotification(P_0));
    }
  }
}