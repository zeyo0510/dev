// https://learn.microsoft.com/en-us/windows/win32/api/audiopolicy/nn-audiopolicy-iaudiosessionenumerator
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionEnumerator
  {
    int GetCount(
			out int _SESSION_COUNT_
		);
    /************************************************/
    int GetSession(
			int _SESSION_COUNT_,
			out IAudioSessionControl2 _SESSION_
		);
  }
}