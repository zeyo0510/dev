// OK
// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immdeviceenumerator
/************************************************/
using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMDeviceEnumerator
  {
    int EnumAudioEndpoints(
      [In][MarshalAs(UnmanagedType.I4)] EDataFlow _DATA_FLOW_,
      [In][MarshalAs(UnmanagedType.U4)] EDeviceState _DW_STATE_MASK_,
      [Out][MarshalAs(UnmanagedType.Interface)] out IMMDeviceCollection _PP_DEVICES_);
    /************************************************/
    int GetDefaultAudioEndpoint(
      [In][MarshalAs(UnmanagedType.I4)] EDataFlow _DATA_FLOW_,
      [In][MarshalAs(UnmanagedType.I4)] ERole _ROLE_,
      [Out][MarshalAs(UnmanagedType.Interface)] out IMMDevice _PP_ENDPOINT_);
    /************************************************/
    int GetDevice(
      [In][MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_ID_,
      [Out][MarshalAs(UnmanagedType.Interface)] out IMMDevice _PP_DEVICE_);
    /************************************************/
    int RegisterEndpointNotificationCallback(
      [In][MarshalAs(UnmanagedType.Interface)] IMMNotificationClient _P_CLIENT_);
    /************************************************/
    int UnregisterEndpointNotificationCallback(
      [In][MarshalAs(UnmanagedType.Interface)] IMMNotificationClient _P_CLIENT_);
  }
}