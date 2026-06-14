using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class ContextMenuHelper
  {
    public static void InvokeCommand(IContextMenu iContextMenu, uint cmd, string parentDir, Point ptInvoke)
    {
      WinAPI.CMINVOKECOMMANDINFOEX invoke = new WinAPI.CMINVOKECOMMANDINFOEX();
      invoke.cbSize = WinAPI.cbInvokeCommand;
      invoke.lpVerb = (IntPtr)cmd;
      invoke.lpDirectory = parentDir;
      invoke.lpVerbW = (IntPtr)cmd;
      invoke.lpDirectoryW = parentDir;
      invoke.fMask = WinAPI.CMIC.UNICODE | WinAPI.CMIC.PTINVOKE | ((Control.ModifierKeys & Keys.Control) != 0 ? WinAPI.CMIC.CONTROL_DOWN : 0) | ((Control.ModifierKeys & Keys.Shift) != 0 ? WinAPI.CMIC.SHIFT_DOWN : 0);
      invoke.ptInvoke = new WinAPI.POINT(ptInvoke.X, ptInvoke.Y);
      invoke.nShow = WinAPI.SW.SHOWNORMAL;
      iContextMenu.InvokeCommand(ref invoke);
    }

    public static void InvokeCommand(IContextMenu iContextMenu, string cmd, string parentDir, Point ptInvoke)
    {
      WinAPI.CMINVOKECOMMANDINFOEX invoke = new WinAPI.CMINVOKECOMMANDINFOEX();
      invoke.cbSize = WinAPI.cbInvokeCommand;
      invoke.lpVerb = Marshal.StringToHGlobalAnsi(cmd);
      invoke.lpDirectory = parentDir;
      invoke.lpVerbW = Marshal.StringToHGlobalUni(cmd);
      invoke.lpDirectoryW = parentDir;
      invoke.fMask = WinAPI.CMIC.UNICODE | WinAPI.CMIC.PTINVOKE | ((Control.ModifierKeys & Keys.Control) != 0 ? WinAPI.CMIC.CONTROL_DOWN : 0) | ((Control.ModifierKeys & Keys.Shift) != 0 ? WinAPI.CMIC.SHIFT_DOWN : 0);
      invoke.ptInvoke = new WinAPI.POINT(ptInvoke.X, ptInvoke.Y);
      invoke.nShow = WinAPI.SW.SHOWNORMAL;

      iContextMenu.InvokeCommand(ref invoke);
    }

    public static void InvokeCommand(ShellItem parent, IntPtr[] pidls, string cmd, Point ptInvoke)
    {
      IntPtr icontextMenuPtr;
      IContextMenu iContextMenu;

      if (GetIContextMenu(parent.ShellFolder, pidls, out icontextMenuPtr, out iContextMenu))
      {
        try
        {
          InvokeCommand(iContextMenu, cmd, ShellItem.GetRealPath(parent), ptInvoke);
        } catch (Exception) {
          
        } finally {
          if (iContextMenu != null) Marshal.ReleaseComObject(iContextMenu);
          if (icontextMenuPtr != IntPtr.Zero) Marshal.Release(icontextMenuPtr);
        }
      }
    }
  }
}