using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioMeterInformation
  {
    [PreserveSig]
    int GetPeakValue(out float P_0);

    [PreserveSig]
    int GetMeteringChannelCount(out int P_0);

    [PreserveSig]
    int GetChannelsPeakValues(int P_0, [In] IntPtr P_1);

    [PreserveSig]
    int QueryHardwareSupport(out int P_0);
  }
}