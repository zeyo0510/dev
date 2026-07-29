// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessioncontrol
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("f4b1a599-7266-4319-a8ca-e70acb11e8cd")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionControl
  {
    [PreserveSig]
    int GetState(
      out AudioSessionState _P_RET_VAL_
    );
    /************************************************/
    [PreserveSig]
    int GetDisplayName(
      out IntPtr _P_RET_VAL_
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
      out IntPtr _P_RET_VAL_
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
  }
}