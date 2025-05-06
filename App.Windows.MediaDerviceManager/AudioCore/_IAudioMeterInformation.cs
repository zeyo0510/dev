// OK
// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/nn-endpointvolume-iaudiometerinformation
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  [Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioMeterInformation
  {
    int GetPeakValue(
      out float _PF_PEAK_);
    /************************************************/
    int GetMeteringChannelCount(
      out int _PN_CHANNEL_COUNT_);
    /************************************************/
    int GetChannelsPeakValues(
      int _U32_CHANNEL_COUNT_,
      [In] IntPtr _AF_PEAK_VALUES_);
    /************************************************/
    int QueryHardwareSupport(
      out int _PDW_HARDWARE_SUPPORT_MASK_);
  }
}