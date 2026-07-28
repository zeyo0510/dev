using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public string SessionIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionIdentifier(out nint ptr));
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return result;
      }
    }
  }
}