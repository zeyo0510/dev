using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public string IconPath
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetIconPath(out nint ptr));
        string retValue = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return retValue;
      }
    }
  }
}