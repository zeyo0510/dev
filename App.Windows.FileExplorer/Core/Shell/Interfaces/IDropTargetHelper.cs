using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShellDll
{
  [ComImport]
  [GuidAttribute("4657278B-411B-11d2-839A-00C04FD918D0")]
  [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IDropTargetHelper
  {
    [PreserveSig]
    Int32 DragEnter(IntPtr hwndTarget, IntPtr pDataObject, ref WinAPI.POINT ppt, DragDropEffects dwEffect);

    [PreserveSig]
    Int32 DragLeave();

    [PreserveSig]
    Int32 DragOver(ref WinAPI.POINT ppt, DragDropEffects dwEffect);

    [PreserveSig]
    Int32 Drop(IntPtr pDataObject, ref WinAPI.POINT ppt, DragDropEffects dwEffect);

    [PreserveSig]
    Int32 Show(bool fShow);
  }
}
