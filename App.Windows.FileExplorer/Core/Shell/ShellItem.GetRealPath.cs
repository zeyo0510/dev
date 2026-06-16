using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    public static string GetRealPath(ShellItem item)
    {
      if (item.Equals(item.Browser.DesktopItem))
      {
        return "::{450d8fba-ad25-11d0-98a8-0800361b1103}";
      }
      else if (item.Type == item.Browser.SystemFolderName)
      {
          IntPtr strr = Marshal.AllocCoTaskMem(WinAPI.MAX_PATH * 2 + 4);
          Marshal.WriteInt32(strr, 0, 0);
          StringBuilder buf = new StringBuilder(WinAPI.MAX_PATH);

          if (item.ParentItem.ShellFolder.GetDisplayNameOf(
                          item.PIDLRel.Ptr,
                          WinAPI.SHGNO.FORPARSING,
                          strr) == WinAPI.S_OK)
          {
              WinAPI.StrRetToBuf(strr, item.PIDLRel.Ptr, buf, WinAPI.MAX_PATH);
          }

          Marshal.FreeCoTaskMem(strr);

          return buf.ToString();
      }
      else
          return item.Path;
    }
  }
}