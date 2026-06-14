using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShellDll
{
  [ComImport]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000122-0000-0000-C000-000000000046")]
  public interface IDropTarget
  {
    [PreserveSig]
    Int32 DragEnter(IntPtr pDataObj, WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect);

    [PreserveSig]
    Int32 DragOver(WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect);

    [PreserveSig]
    Int32 DragLeave();

    [PreserveSig]
    Int32 DragDrop(IntPtr pDataObj, WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect);
  }
}
