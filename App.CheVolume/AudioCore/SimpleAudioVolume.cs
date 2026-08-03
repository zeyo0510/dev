using System.Runtime.InteropServices;
/************************************************/
using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  public class SimpleAudioVolume
  {
    private ISimpleAudioVolume _SimpleAudioVolume_;
    /************************************************/
    internal SimpleAudioVolume(ISimpleAudioVolume _SIMPLE_AUDIO_VOLUME_)
    {
      this._SimpleAudioVolume_ = _SIMPLE_AUDIO_VOLUME_;
    }
    /************************************************/
    public float MasterVolume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.GetMasterVolume(out float retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.SetMasterVolume(value, ref empty));
      }
    }
    /************************************************/
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.SetMute(value, Guid.Empty));
      }
    }
  }
}