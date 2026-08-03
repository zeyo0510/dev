using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionControl2
  {
    private readonly IAudioSessionControl2 _AudioSessionControl_;
    private readonly IAudioMeterInformation _AudioMeterInformation_;
    private readonly ISimpleAudioVolume _SimpleAudioVolume_;
    /************************************************/
    internal AudioSessionControl2(IAudioSessionControl2 _AUDIO_SESSION_CONTROL_)
    {
      this._AudioSessionControl_   = _AUDIO_SESSION_CONTROL_;
      this._AudioMeterInformation_ = (IAudioMeterInformation)this._AudioSessionControl_;
      this._SimpleAudioVolume_     = (ISimpleAudioVolume)this._AudioSessionControl_;
    }
    /************************************************/
    public void RegisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.RegisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.UnregisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public float Method1()
    {
      this._AudioMeterInformation_.GetPeakValue(out float result);
      this._AudioMeterInformation_.GetMeteringChannelCount(out int num);
      return result;
    }
  }
}