using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
  [StructLayout(LayoutKind.Explicit)]
  public struct PropVariant
  {
    [FieldOffset(0)]
    private short vt;

    [FieldOffset(2)]
    private short wReserved1;

    [FieldOffset(4)]
    private short wReserved2;

    [FieldOffset(6)]
    private short wReserved3;

    [FieldOffset(8)]
    private sbyte cVal;

    [FieldOffset(8)]
    private byte bVal;

    [FieldOffset(8)]
    private short iVal;

    [FieldOffset(8)]
    private ushort uiVal;

    [FieldOffset(8)]
    private int lVal;

    [FieldOffset(8)]
    private uint ulVal;

    [FieldOffset(8)]
    private long hVal;

    [FieldOffset(8)]
    private ulong uhVal;

    [FieldOffset(8)]
    private float fltVal;

    [FieldOffset(8)]
    private double dblVal;

    [FieldOffset(8)]
    private Blob blobVal;

    [FieldOffset(8)]
    private DateTime date;

    [FieldOffset(8)]
    private bool boolVal;

    [FieldOffset(8)]
    private int scode;

    [FieldOffset(8)]
    private System.Runtime.InteropServices.ComTypes.FILETIME filetime;

    [FieldOffset(8)]
    private IntPtr everything_else;

    private byte[] GetBlob()
    {
      byte[] array = new byte[blobVal.Length];
      for (int i = 0; i < blobVal.Length; i++)
      {
        array[i] = Marshal.ReadByte((IntPtr)((long)blobVal.Data + i));
      }
      return array;
    }

    public object GetValue()
    {
      VarEnum varEnum = (VarEnum)vt;
      switch (varEnum)
      {
      case VarEnum.VT_I1:
        return bVal;
      case VarEnum.VT_I2:
        return iVal;
      case VarEnum.VT_I4:
        return lVal;
      case VarEnum.VT_I8:
        return hVal;
      case VarEnum.VT_INT:
        return iVal;
      case VarEnum.VT_UI4:
        return ulVal;
      case VarEnum.VT_LPWSTR:
        return Marshal.PtrToStringUni(everything_else);
      case VarEnum.VT_BLOB:
        return GetBlob();
      default:
        return "FIXME Type = " + varEnum;
      }
    }
  }
}