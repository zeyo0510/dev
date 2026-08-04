using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public string DisplayName
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetDisplayName(out nint ptr));
        string retValue = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return retValue;
      }
    }
  }
}