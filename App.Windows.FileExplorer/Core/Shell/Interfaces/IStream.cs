using System;
using System.Runtime.InteropServices;
using System.IO;

namespace ShellDll
{
  [ComImport]
  [Guid("0000000c-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IStream
  {
    [PreserveSig]
    Int32 Read([MarshalAs(UnmanagedType.LPArray)]byte[] pv, int cb, IntPtr pcbRead);

    [PreserveSig]
    Int32 Write([MarshalAs(UnmanagedType.LPArray)]byte[] pv, int cb, IntPtr pcbWritten);

    [PreserveSig]
    Int32 Seek(long dlibMove, SeekOrigin dwOrigin, IntPtr plibNewPosition);

    [PreserveSig]
    Int32 SetSize(long libNewSize);

    [PreserveSig]
    Int32 CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

    [PreserveSig]
    Int32 Commit(WinAPI.STGC grfCommitFlags);

    [PreserveSig]
    Int32 Revert();

    [PreserveSig]
    Int32 LockRegion(long libOffset, long cb, WinAPI.LOCKTYPE dwLockType);

    [PreserveSig]
    Int32 UnlockRegion(long libOffset, long cb, WinAPI.LOCKTYPE dwLockType);

    [PreserveSig]
    Int32 Stat(out WinAPI.STATSTG pstatstg, WinAPI.STATFLAG grfStatFlag);

    [PreserveSig]
    Int32 Clone(out IntPtr ppstm);
  }
}