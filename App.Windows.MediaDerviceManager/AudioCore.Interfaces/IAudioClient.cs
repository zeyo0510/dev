using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioClient
  {
    [PreserveSig]
    int Initialize(AudioClientShareMode P_0, AudioClientStreamFlags P_1, long P_2, long P_3, [In] WaveFormat P_4, [In] ref Guid P_5);

    int GetBufferSize(out uint P_0);

    [return: MarshalAs(UnmanagedType.I8)]
    long GetStreamLatency();

    int GetCurrentPadding(out int P_0);

    [PreserveSig]
    int IsFormatSupported(AudioClientShareMode P_0, [In] WaveFormat P_1, [MarshalAs(UnmanagedType.LPStruct)] out WaveFormatExtensible P_2);

    int GetMixFormat(out IntPtr P_0);

    int GetDevicePeriod(out long P_0, out long P_1);

    int Start();

    int Stop();

    int Reset();

    int SetEventHandle(IntPtr P_0);

    int GetService(ref Guid P_0, [MarshalAs(UnmanagedType.IUnknown)] out object P_1);
  }
}