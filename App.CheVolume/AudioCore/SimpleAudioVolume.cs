using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
  public class SimpleAudioVolume
  {
    private ISimpleAudioVolume _SimpleAudioVolume_;

    public float MasterVolume
    {
      get
      {
        float result;
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.GetMasterVolume(out result));
        return result;
      }
      set
      {
        Guid empty = Guid.Empty;
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.SetMasterVolume(value, ref empty));
      }
    }

    public bool Mute
    {
      get
      {
        bool result;
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.GetMute(out result));
        return result;
      }
      set
      {
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.SetMute(value, Guid.Empty));
      }
    }

    internal SimpleAudioVolume(ISimpleAudioVolume P_0)
    {
      _SimpleAudioVolume_ = P_0;
    }
  }
}
