using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class MMDeviceCollection
  {
    private readonly IMMDeviceCollection _MMDeviceCollection_ = null;
    /************************************************/
    internal MMDeviceCollection(IMMDeviceCollection obj)
    {
      this._MMDeviceCollection_ = obj;
    }
    /************************************************/
    public MMDevice this[int index]
    {
      get
      {
        IMMDevice retValue = null;
        /************************************************/
        this._MMDeviceCollection_.Item((uint)index, out retValue);
        /************************************************/
        return new MMDevice(retValue);
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        uint retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._MMDeviceCollection_.GetCount(out retValue));
        /************************************************/
        return (int)retValue;
      }
    }
  }
}