// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/nn-endpointvolume-iaudiometerinformation
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioMeterInformation
  {
    [PreserveSig]
    int GetPeakValue(
      out float _PF_PEAK_
    );
    /************************************************/
    [PreserveSig]
    int GetMeteringChannelCount(
      out int _PN_CHANNEL_COUNT_
    );
    /************************************************/
    [PreserveSig]
    int GetChannelsPeakValues(
      int _U32_CHANNEL_COUNT_,
      [In] IntPtr _AF_PEAK_VALUES_
    );
    /************************************************/
    [PreserveSig]
    int QueryHardwareSupport(
      out int _PDW_HARDWARE_SUPPORT_MASK_
    );
  }
}