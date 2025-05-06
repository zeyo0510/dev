// OK
// https://learn.microsoft.com/zh-tw/windows/win32/api/audiopolicy/nf-audiopolicy-iaudiosessionevents-onsessiondisconnected
/************************************************/
using System;
/************************************************/
namespace AudioCore
{
  public enum AudioSessionDisconnectReason
  {
    DisconnectReasonDeviceRemoval,
    DisconnectReasonServerShutdown,
    DisconnectReasonFormatChanged,
    DisconnectReasonSessionLogoff,
    DisconnectReasonSessionDisconnected,
    DisconnectReasonExclusiveModeOverride
  }
}