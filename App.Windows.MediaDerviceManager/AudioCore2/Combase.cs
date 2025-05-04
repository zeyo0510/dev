using System;
using System.Runtime.InteropServices;

namespace AudioCore2
{
  internal static class Combase
  {
    [DllImport("combase.dll", EntryPoint = "RoGetActivationFactory", PreserveSig = false)]
    public static extern void _RoGetActivationFactory([MarshalAs(UnmanagedType.HString)] string P_0, [In] ref Guid P_1, [MarshalAs(UnmanagedType.IInspectable)] out object P_2);

    [DllImport("combase.dll", EntryPoint = "WindowsCreateString", PreserveSig = false)]
    public static extern void _WindowsCreateString([MarshalAs(UnmanagedType.LPWStr)] string P_0, [In] uint P_1, out IntPtr P_2);
  }
}