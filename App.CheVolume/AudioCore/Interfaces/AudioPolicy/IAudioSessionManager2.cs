// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessionmanager
// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessionmanager2
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionManager2
  {
    [PreserveSig]
    int GetAudioSessionControl(
      ref Guid _AUDIO_SESSION_GUID_,
      uint _STREAM_FLAGS_,
      IntPtr _SESSION_CONTROL_
    );
    /************************************************/
    [PreserveSig]
    int GetSimpleAudioVolume(
      ref Guid _AUDIO_SESSION_GUID_,
      uint _STREAM_FLAGS_,
      out ISimpleAudioVolume _AUDIO_VOLUME_
    );
    /************************************************/
    [PreserveSig]
    int GetSessionEnumerator(
      out IAudioSessionEnumerator _SESSION_ENUM_
    );
    /************************************************/
    [PreserveSig]
    int RegisterSessionNotification(
      IAudioSessionNotification _SESSION_NOTIFICATION_
    );
    /************************************************/
    [PreserveSig]
    int UnregisterSessionNotification(
      IAudioSessionNotification _SESSION_NOTIFICATION_
    );
    /************************************************/
    [PreserveSig]
    int RegisterDuckNotification(
      string _SESSION_ID_,
      IntPtr _DUCK_NOTIFICATION_
    );
    /************************************************/
    [PreserveSig]
    int UnregisterDuckNotification(
      IntPtr _DUCK_NOTIFICATION_
    );
  }
}