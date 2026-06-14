using System;
using System.Runtime.InteropServices;
using ShellDll;

namespace FileBrowser
{
  partial class ContextMenuHelper
  {
    public static bool GetIContextMenu(IShellFolder parent, IntPtr[] pidls, out IntPtr iContextMenuPtr, out IContextMenu iContextMenu)
    {
      if (parent.GetUIObjectOf(IntPtr.Zero, (uint)pidls.Length, pidls, ref WinAPI.IID_IContextMenu, IntPtr.Zero, out iContextMenuPtr) == WinAPI.S_OK)
      {
        iContextMenu =(IContextMenu)Marshal.GetTypedObjectForIUnknown(iContextMenuPtr, typeof(IContextMenu));

        return true;
      }
      else
      {
        iContextMenuPtr = IntPtr.Zero;
        iContextMenu = null;

        return false;
      }
    }
  }
}