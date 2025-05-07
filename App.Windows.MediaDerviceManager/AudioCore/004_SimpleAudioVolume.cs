using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class SimpleAudioVolume
  {
    private ISimpleAudioVolume _SimpleAudioVolume_;
    /************************************************/
    internal SimpleAudioVolume(ISimpleAudioVolume obj)
    {
      this._SimpleAudioVolume_ = obj;
    }
    /************************************************/
    public float MasterVolume
    {
      get
      {
        float retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.GetMasterVolume(out retValue));
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
        bool retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.GetMute(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.SetMute(value, ref empty));
      }
    }
  }
}