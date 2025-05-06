// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/nf-endpointvolume-iaudioendpointvolume-getchannelcount
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioEndpointVolume
  {
    [PreserveSig]
    int RegisterControlChangeNotify(IAudioEndpointVolumeCallback P_0);

    [PreserveSig]
    int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback P_0);

    [PreserveSig]
    int GetChannelCount(out int P_0);

    [PreserveSig]
    int SetMasterVolumeLevel(float P_0, Guid P_1);

    [PreserveSig]
    int SetMasterVolumeLevelScalar(float P_0, Guid P_1);

    [PreserveSig]
    int GetMasterVolumeLevel(out float P_0);

    [PreserveSig]
    int GetMasterVolumeLevelScalar(out float P_0);

    [PreserveSig]
    int SetChannelVolumeLevel(uint P_0, float P_1, Guid P_2);

    [PreserveSig]
    int SetChannelVolumeLevelScalar(uint P_0, float P_1, Guid P_2);

    [PreserveSig]
    int GetChannelVolumeLevel(uint P_0, out float P_1);

    [PreserveSig]
    int GetChannelVolumeLevelScalar(uint P_0, out float P_1);

    [PreserveSig]
    int SetMute([MarshalAs(UnmanagedType.Bool)] bool P_0, Guid P_1);

    [PreserveSig]
    int GetMute(out bool P_0);

    [PreserveSig]
    int GetVolumeStepInfo(out uint P_0, out uint P_1);

    [PreserveSig]
    int VolumeStepUp(Guid P_0);

    [PreserveSig]
    int VolumeStepDown(Guid P_0);

    [PreserveSig]
    int QueryHardwareSupport(out uint P_0);

    [PreserveSig]
    int GetVolumeRange(out float P_0, out float P_1, out float P_2);
  }
}