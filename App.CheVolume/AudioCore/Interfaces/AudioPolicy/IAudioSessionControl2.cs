// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessioncontrol2
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionControl2
  {
    [PreserveSig]
    int GetState(
      out AudioSessionState _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int GetDisplayName(
      [MarshalAs(UnmanagedType.LPWStr)] out string _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int SetDisplayName(
      string _VALUE_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetIconPath(
      [MarshalAs(UnmanagedType.LPWStr)] out string _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int SetIconPath(
      string _VALUE_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int GetGroupingParam(
      out Guid _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int SetGroupingParam(
      Guid _OVERRIDE_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int RegisterAudioSessionNotification(
      IAudioSessionEvents _NEW_NOTIFICATIONS_
    );
    /************************************************/
    [PreserveSig]
    int UnregisterAudioSessionNotification(
      IAudioSessionEvents _NEW_NOTIFICATIONS_
    );
    /************************************************/
    [PreserveSig]
    int GetSessionIdentifier(
      [MarshalAs(UnmanagedType.LPWStr)] out string _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int GetSessionInstanceIdentifier(
      [MarshalAs(UnmanagedType.LPWStr)] out string _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int GetProcessId(
      out uint _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int IsSystemSoundsSession();
    /************************************************/
    [PreserveSig]
    int SetDuckingPreference(
      bool _OPT_OUT_
    );
  }
}