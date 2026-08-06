using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public string SessionIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionIdentifier(out nint ptr));
        string retValue = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return retValue;
      }
    }
  }
}