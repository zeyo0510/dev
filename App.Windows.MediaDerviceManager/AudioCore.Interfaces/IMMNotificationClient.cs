using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMMNotificationClient
  {
    [PreserveSig]
    void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string P_0, [MarshalAs(UnmanagedType.U4)] EDeviceState P_1);

    [PreserveSig]
    void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string P_0);

    [PreserveSig]
    void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string P_0);

    [PreserveSig]
    void OnDefaultDeviceChanged([MarshalAs(UnmanagedType.I4)] EDataFlow P_0, [MarshalAs(UnmanagedType.I4)] ERole P_1, [MarshalAs(UnmanagedType.LPWStr)] string P_2);

    [PreserveSig]
    void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string P_0, PROPERTYKEY P_1);
  }
}