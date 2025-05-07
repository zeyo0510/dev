// https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nn-mmdeviceapi-immdevice
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface IMMDevice
  {
    int Activate(
      ref Guid _IID_,
      CLSCTX _DW_CLS_CTX_,
      IntPtr _P_ACTIVATION_PARAMS_,
      [MarshalAs(UnmanagedType.IUnknown)] out object _PP_INTERFACE_
    );
    /************************************************/
    [PreserveSig]
    int OpenPropertyStore(
      EStgmAccess _STGM_ACCESS_,
      out IPropertyStore _PP_PROPERTIES_
    );
    /************************************************/
    int GetId(
      [MarshalAs(UnmanagedType.LPWStr)] out string _PPSTR_ID_
    );
    /************************************************/
    int GetState(
      out DeviceState _PDW_STATE_
    );
  }
}