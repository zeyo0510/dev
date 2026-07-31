// https://learn.microsoft.com/en-us/windows/win32/api/audiosessiontypes/ne-audiosessiontypes-audiosessionstate
namespace AudioCore
{
  public enum AudioSessionState
  {
    AudioSessionStateInactive,
    AudioSessionStateActive,
    AudioSessionStateExpired
  }
}