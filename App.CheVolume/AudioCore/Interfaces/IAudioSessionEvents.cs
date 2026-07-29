// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessionevents
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionEvents
  {
    [PreserveSig]
    int OnDisplayNameChanged(
      [MarshalAs(UnmanagedType.LPWStr)] string _NEW_DISPLAY_NAME_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int OnIconPathChanged(
      [MarshalAs(UnmanagedType.LPWStr)] string _NEW_ICON_PATH_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int OnSimpleVolumeChanged(
      float _NEW_VOLUME_,
      bool _NEW_MUTE_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int OnChannelVolumeChanged(
      uint _CHANNEL_COUNT_,
      IntPtr _NEW_CHANNEL_VOLUME_ARRAY_,
      uint ChangedChannel,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int OnGroupingParamChanged(
      Guid _NEW_GROUPING_PARAM_,
      Guid _EVENT_CONTEXT_
    );
    /************************************************/
    [PreserveSig]
    int OnStateChanged(
      AudioSessionState _NEW_STATE_
    );
    /************************************************/
    [PreserveSig]
    int OnSessionDisconnected(
      AudioSessionDisconnectReason _DISCONNECT_REASON_
    );
  }
}