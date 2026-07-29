// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/nn-audioclient-isimpleaudiovolume
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface ISimpleAudioVolume
  {
    [PreserveSig]
    int SetMasterVolume(
      float _F_LEVEL_,
      ref Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetMasterVolume(
      out float _PF_LEVEL_
    );
    /************************************************/
    [PreserveSig]
    int SetMute(
      bool _B_MUTE_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetMute(
      out bool _PB_MUTE_
    );
  }
}