using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ShellDll
{
  [ComImport()]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [GuidAttribute("000214e4-0000-0000-c000-000000000046")]
  public interface IContextMenu
  {
    [PreserveSig()]
    Int32 QueryContextMenu(IntPtr hmenu, uint iMenu, uint idCmdFirst, uint idCmdLast, WinAPI.CMF uFlags);

    [PreserveSig()]
    Int32 InvokeCommand(ref WinAPI.CMINVOKECOMMANDINFOEX info);

    [PreserveSig()]
    Int32 GetCommandString(uint idcmd, WinAPI.GCS uflags, uint reserved, [MarshalAs(UnmanagedType.LPArray)] byte[] commandstring, int cch);
  }
  
  [ComImport, Guid("000214f4-0000-0000-c000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IContextMenu2
  {
    [PreserveSig()]
    Int32 QueryContextMenu(IntPtr hmenu, uint iMenu, uint idCmdFirst, uint idCmdLast, WinAPI.CMF uFlags);

    [PreserveSig()]
    Int32 InvokeCommand(ref WinAPI.CMINVOKECOMMANDINFOEX info);

    [PreserveSig()]
    Int32 GetCommandString(uint idcmd, WinAPI.GCS uflags, uint reserved, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder commandstring, int cch);

    [PreserveSig]
    Int32 HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam);
  }

  [ComImport, Guid("bcfce0a0-ec17-11d0-8d10-00a0c90f2719")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IContextMenu3
  {
    [PreserveSig()]
    Int32 QueryContextMenu(IntPtr hmenu,uint iMenu, uint idCmdFirst, uint idCmdLast, WinAPI.CMF uFlags);

    [PreserveSig()]
    Int32 InvokeCommand(
        ref WinAPI.CMINVOKECOMMANDINFOEX info);

    [PreserveSig()]
    Int32 GetCommandString(uint idcmd, WinAPI.GCS uflags, uint reserved, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder commandstring, int cch);

    [PreserveSig]
    Int32 HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam);

    [PreserveSig]
    Int32 HandleMenuMsg2(uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr plResult);
  }
}