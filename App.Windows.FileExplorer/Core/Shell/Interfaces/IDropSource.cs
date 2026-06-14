using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShellDll
{
  [ComImport]
  [GuidAttribute("00000121-0000-0000-C000-000000000046")]
  [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IDropSource
  {
    [PreserveSig]
    Int32 QueryContinueDrag(bool fEscapePressed, WinAPI.MK grfKeyState);

    [PreserveSig]
    Int32 GiveFeedback(DragDropEffects dwEffect);
  }
}