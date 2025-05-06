// OK
// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immnotificationclient
/************************************************/
using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMMNotificationClient
  {
    void OnDeviceStateChanged(
      [MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_DEVICE_ID_,
      [MarshalAs(UnmanagedType.U4)] EDeviceState _DW_NEW_STATE_
    );
    /************************************************/
    void OnDeviceAdded(
      [MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_DEVICE_ID_
    );
    /************************************************/
    void OnDeviceRemoved(
      [MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_DEVICE_ID_
    );
    /************************************************/
    void OnDefaultDeviceChanged(
      [MarshalAs(UnmanagedType.I4)] EDataFlow _FLOW_,
      [MarshalAs(UnmanagedType.I4)] ERole _ROLE_,
      [MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_DEFAULT_DEVICE_ID_
    );
    /************************************************/
    void OnPropertyValueChanged(
      [MarshalAs(UnmanagedType.LPWStr)] string _PWSTR_DEVICE_ID_,
      PROPERTYKEY _KEY_
    );
  }
}