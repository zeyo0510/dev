using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellBrowser
  {
    private void InitVars()
    {
        IntPtr tempPidl;
        WinAPI.SHFILEINFO info;

        //My Computer
        info = new WinAPI.SHFILEINFO();
        tempPidl = IntPtr.Zero;
        WinAPI.SHGetSpecialFolderLocation(IntPtr.Zero, WinAPI.CSIDL.DRIVES, out tempPidl);

        WinAPI.SHGetFileInfo(tempPidl, 0, ref info, WinAPI.cbFileInfo,
            WinAPI.SHGFI.PIDL | WinAPI.SHGFI.DISPLAYNAME | WinAPI.SHGFI.TYPENAME);

        sysfolderName = info.szTypeName;
        mycompName = info.szDisplayName;
        Marshal.FreeCoTaskMem(tempPidl);
        //

        //Dekstop
        tempPidl = IntPtr.Zero;
        WinAPI.SHGetSpecialFolderLocation(IntPtr.Zero, WinAPI.CSIDL.DESKTOP, out tempPidl);
        IntPtr desktopFolderPtr;
        WinAPI.SHGetDesktopFolder(out desktopFolderPtr);
        desktopItem = new ShellItem(this, tempPidl, desktopFolderPtr);
        //

        //My Documents
        uint pchEaten = 0;
        WinAPI.SFGAO pdwAttributes = 0;
        desktopItem.ShellFolder.ParseDisplayName(
            IntPtr.Zero,
            IntPtr.Zero,
            "::{450d8fba-ad25-11d0-98a8-0800361b1103}",
            ref pchEaten,
            out tempPidl,
            ref pdwAttributes);

        info = new WinAPI.SHFILEINFO();
        WinAPI.SHGetFileInfo(tempPidl, 0, ref info, WinAPI.cbFileInfo,
            WinAPI.SHGFI.PIDL | WinAPI.SHGFI.DISPLAYNAME);

        mydocsName = info.szDisplayName;
        Marshal.FreeCoTaskMem(tempPidl);

        StringBuilder path = new StringBuilder(WinAPI.MAX_PATH);
        WinAPI.SHGetFolderPath(
                IntPtr.Zero, WinAPI.CSIDL.PERSONAL,
                IntPtr.Zero, WinAPI.SHGFP.TYPE_CURRENT, path);
        mydocsPath = path.ToString();
        //
    }
  }
}