using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetPath(ShellItem item)
    {
        IntPtr strr = Marshal.AllocCoTaskMem(WinAPI.MAX_PATH * 2 + 4);
        Marshal.WriteInt32(strr, 0, 0);
        StringBuilder buf = new StringBuilder(WinAPI.MAX_PATH);

        if (item.ParentItem.ShellFolder.GetDisplayNameOf(item.PIDLRel.Ptr, WinAPI.SHGNO.FORADDRESSBAR | WinAPI.SHGNO.FORPARSING, strr) == WinAPI.S_OK)
        {
            WinAPI.StrRetToBuf(strr, item.PIDLRel.Ptr, buf, WinAPI.MAX_PATH);
            item.path = buf.ToString();
        }

        Marshal.FreeCoTaskMem(strr);
    }
  }
}