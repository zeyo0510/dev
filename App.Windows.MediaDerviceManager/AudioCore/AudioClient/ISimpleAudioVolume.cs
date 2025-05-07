// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/nn-audioclient-isimpleaudiovolume
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface ISimpleAudioVolume
  {
    int SetMasterVolume(
      float _F_LEVEL,
      ref Guid _EVENT_CONTEXT_);
    /************************************************/
    int GetMasterVolume(
      out float _PF_LEVEL_);
    /************************************************/
    int SetMute(
      bool _B_MUTE_,
      ref Guid _EVENT_CONTEXT_);
    /************************************************/
    int GetMute(
      out bool _PB_MUTE_);
  }
}