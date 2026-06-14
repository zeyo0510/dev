using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  [ComImport]
  [Guid("0000000d-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IEnumSTATSTG
  {
    [PreserveSig]
    uint Next(uint celt, [MarshalAs(UnmanagedType.LPArray)]out WinAPI.STATSTG[] rgelt, out uint pceltFetched);

    [PreserveSig]
    void Skip(uint celt);

    [PreserveSig]
    void Reset();

    [PreserveSig]
    IEnumSTATSTG Clone();
  }
}