using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  [ComImport]
  [GuidAttribute("DE5BF786-477A-11d2-839D-00C04FD918D0")]
  [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IDragSourceHelper
  {
    [PreserveSig]
    Int32 InitializeFromBitmap(ref WinAPI.SHDRAGIMAGE pshdi, IntPtr pDataObject);

    [PreserveSig]
    Int32 InitializeFromWindow(IntPtr hwnd, ref WinAPI.POINT ppt, IntPtr pDataObject);
  }
}