using System;

namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetInfo(ShellItem item)
    {
        PIDL pidlFull = item.PIDLFull;

        WinAPI.SHFILEINFO info = new WinAPI.SHFILEINFO();
        WinAPI.SHGetFileInfo(pidlFull.Ptr, 0, ref info, WinAPI.cbFileInfo, WinAPI.SHGFI.PIDL | WinAPI.SHGFI.TYPENAME | WinAPI.SHGFI.SYSICONINDEX);

        pidlFull.Free();

        ShellImageList.SetIconIndex(item, info.iIcon, false);
        ShellImageList.SetIconIndex(item, info.iIcon, true);

        item.Type = info.szTypeName;
    }
  }
}