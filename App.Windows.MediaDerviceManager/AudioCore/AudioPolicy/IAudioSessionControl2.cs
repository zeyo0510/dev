// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessioncontrol2

using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioSessionControl2
  {
    [PreserveSig]
    int GetState(out AudioSessionState P_0);

    [PreserveSig]
    int GetDisplayName(out IntPtr P_0);

    [PreserveSig]
    int SetDisplayName(string P_0, Guid P_1);

    [PreserveSig]
    int GetIconPath(out IntPtr P_0);

    [PreserveSig]
    int SetIconPath(string P_0, Guid P_1);

    [PreserveSig]
    int GetGroupingParam(out Guid P_0);

    [PreserveSig]
    int SetGroupingParam(Guid P_0, Guid P_1);

    [PreserveSig]
    int RegisterAudioSessionNotification(IAudioSessionEvents P_0);

    [PreserveSig]
    int UnregisterAudioSessionNotification(IAudioSessionEvents P_0);

    [PreserveSig]
    int GetSessionIdentifier(out IntPtr P_0);

    [PreserveSig]
    int GetSessionInstanceIdentifier(out IntPtr P_0);

    [PreserveSig]
    int GetProcessId(out uint P_0);

    [PreserveSig]
    int IsSystemSoundsSession();

    [PreserveSig]
    int SetDuckingPreference(bool P_0);
  }
}