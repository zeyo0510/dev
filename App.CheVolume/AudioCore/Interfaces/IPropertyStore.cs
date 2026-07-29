// https://learn.microsoft.com/en-us/windows/win32/api/propsys/nn-propsys-ipropertystore
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IPropertyStore
  {
    int GetCount(
      out int _C_PROPS_
    );
    /************************************************/
    int GetAt(
      int iProp,
      out PropertyKey _P_KEY_
    );
    /************************************************/
    int GetValue(
      ref PropertyKey _KEY_,
      out PropVariant _PV_
    );
    /************************************************/
    int SetValue(
      ref PropertyKey _KEY_,
      ref PropVariant _PROP_VAR_
    );
    /************************************************/
    int Commit();
  }
}