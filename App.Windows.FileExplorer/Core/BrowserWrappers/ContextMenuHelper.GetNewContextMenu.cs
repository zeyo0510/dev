using System;
using System.Runtime.InteropServices;
using ShellDll;

namespace FileBrowser
{
  partial class ContextMenuHelper
  {
    public static bool GetNewContextMenu(ShellItem item, out IntPtr iContextMenuPtr, out IContextMenu iContextMenu)
    {
        if (WinAPI.CoCreateInstance(ref WinAPI.CLSID_NewMenu, IntPtr.Zero, WinAPI.CLSCTX.INPROC_SERVER, ref WinAPI.IID_IContextMenu, out iContextMenuPtr) == WinAPI.S_OK)
        {
            iContextMenu = Marshal.GetTypedObjectForIUnknown(iContextMenuPtr, typeof(IContextMenu)) as IContextMenu;
            
            IntPtr iShellExtInitPtr;
            if (Marshal.QueryInterface(iContextMenuPtr, ref WinAPI.IID_IShellExtInit, out iShellExtInitPtr) == WinAPI.S_OK)
            {
                IShellExtInit iShellExtInit = Marshal.GetTypedObjectForIUnknown(
                    iShellExtInitPtr, typeof(IShellExtInit)) as IShellExtInit;

                PIDL pidlFull = item.PIDLFull;
                iShellExtInit.Initialize(pidlFull.Ptr, IntPtr.Zero, 0);

                Marshal.ReleaseComObject(iShellExtInit);
                Marshal.Release(iShellExtInitPtr);
                pidlFull.Free();
                
                return true;
            }
            else
            {
                if (iContextMenu != null)
                {
                    Marshal.ReleaseComObject(iContextMenu);
                    iContextMenu = null;
                }

                if (iContextMenuPtr != IntPtr.Zero)
                {
                    Marshal.Release(iContextMenuPtr);
                    iContextMenuPtr = IntPtr.Zero;
                }

                return false;
            }
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