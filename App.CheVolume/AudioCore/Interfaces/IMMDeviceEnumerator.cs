// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immdeviceenumerator
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMDeviceEnumerator
  {
    [PreserveSig]
    int GetDefaultAudioEndpoint([In][MarshalAs(UnmanagedType.I4)] EDataFlow P_0, [In][MarshalAs(UnmanagedType.U4)] EDeviceState P_1, [MarshalAs(UnmanagedType.Interface)] out IMMDeviceCollection P_2);

    [PreserveSig]
    int EnumAudioEndpoints([In][MarshalAs(UnmanagedType.I4)] EDataFlow P_0, [In][MarshalAs(UnmanagedType.I4)] ERole P_1, [MarshalAs(UnmanagedType.Interface)] out IMMDevice P_2);

    [PreserveSig]
    int GetDevice([In][MarshalAs(UnmanagedType.LPWStr)] string P_0, [MarshalAs(UnmanagedType.Interface)] out IMMDevice P_1);

    [PreserveSig]
    int RegisterEndpointNotificationCallback([In][MarshalAs(UnmanagedType.Interface)] IMMNotificationClient P_0);

    [PreserveSig]
    int UnregisterEndpointNotificationCallback([In][MarshalAs(UnmanagedType.Interface)] IMMNotificationClient P_0);
  }
}