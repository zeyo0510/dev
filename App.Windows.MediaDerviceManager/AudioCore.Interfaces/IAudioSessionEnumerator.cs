using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioSessionEnumerator
  {
    int GetCount(out int P_0);

    int GetSession(int P_0, out IAudioSessionControl2 P_1);
  }
}