using System;
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
        IntPtr ptr = IntPtr.Zero;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioSessionControl2.GetIconPath(out ptr));
        /************************************************/
        string retValue = Marshal.PtrToStringAuto(ptr);
        /************************************************/
        Marshal.FreeCoTaskMem(ptr);
        /************************************************/
        return retValue;
      }
    }
  }
}