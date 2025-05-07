// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immdevicecollection
/************************************************/
using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMMDeviceCollection
  {
    int GetCount(
      out uint _PC_DEVICES_);
    /************************************************/
    int Item(
      uint _N_DEVICE_,
      out IMMDevice _PP_DEVICE_);
  }
}