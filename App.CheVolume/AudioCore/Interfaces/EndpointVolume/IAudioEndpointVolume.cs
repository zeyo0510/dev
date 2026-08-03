// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/nn-endpointvolume-iaudioendpointvolume
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioEndpointVolume
  {
    [PreserveSig]
    int RegisterControlChangeNotify(
      IAudioEndpointVolumeCallback _P_NOTIFY_
    );
    /************************************************/
    [PreserveSig]
    int UnregisterControlChangeNotify(
      IAudioEndpointVolumeCallback _P_NOTIFY_
    );
    /************************************************/
    [PreserveSig]
    int GetChannelCount(
      out int _PN_CHANNEL_COUNT_
    );
    /************************************************/
    [PreserveSig]
    int SetMasterVolumeLevel(
      float _F_LEVEL_DB_,
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int SetMasterVolumeLevelScalar(
      float _F_LEVEL_,
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetMasterVolumeLevel(
      out float _PF_LEVEL_DB_
    );
    /************************************************/
    [PreserveSig]
    int GetMasterVolumeLevelScalar(
      out float _PF_LEVEL_
    );
    /************************************************/
    [PreserveSig]
    int SetChannelVolumeLevel(
      uint _N_CHANNEL_,
      float _F_LEVEL_DB_,
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int SetChannelVolumeLevelScalar(
      uint _N_CHANNEL_,
      float _F_LEVEL_,
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetChannelVolumeLevel(
      uint _N_CHANNEL_,
      out float _PF_LEVEL_DB_
    );
    /************************************************/
    [PreserveSig]
    int GetChannelVolumeLevelScalar(
      uint _N_CHANNEL_,
      out float _PF_LEVEL_
    );
    /************************************************/
    [PreserveSig]
    int SetMute(
      [MarshalAs(UnmanagedType.Bool)] bool _B_MUTE_,
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetMute(
      out bool _PB_MUTE_
    );
    /************************************************/
    [PreserveSig]
    int GetVolumeStepInfo(
      out uint _PN_STEP_,
      out uint _PN_STEP_COUNT_
    );
    /************************************************/
    [PreserveSig]
    int VolumeStepUp(
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int VolumeStepDown(
      Guid _PGUID_EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int QueryHardwareSupport(
      out uint _PDW_HARDWARE_SUPPORT_MASK_
    );
    /************************************************/
    [PreserveSig]
    int GetVolumeRange(
      out float _PF_LVOLUME_MIND_B_,
      out float _PF_LVOLUME_MAXD_B_,
      out float _PF_LVOLUME_INCREMENTD_B_
    );
  }
}