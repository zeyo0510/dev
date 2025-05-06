// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immendpoint
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMEndpoint
  {
    int GetDataFlow(
      out DataFlow _P_DATA_FLOW_);
  }
}