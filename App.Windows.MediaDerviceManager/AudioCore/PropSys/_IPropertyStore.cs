// https://learn.microsoft.com/en-us/windows/win32/api/propsys/nn-propsys-ipropertystore
/************************************************/
using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  [Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IPropertyStore
  {
    int GetCount(
      out int _C_PROPS_);
    /************************************************/
    int GetAt(
      int _I_PROP_,
      out PropertyKey _P_KEY_);
    /************************************************/
    int GetValue(
      ref PropertyKey _KEY_,
      out PropVariant _P_V_);
    /************************************************/
    int SetValue(
      ref PropertyKey _KEY_,
      ref PropVariant _PROP_VAR_);
    /************************************************/
    int Commit();
  }
}