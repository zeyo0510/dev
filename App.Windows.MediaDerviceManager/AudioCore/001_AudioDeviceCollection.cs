using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioDeviceCollection
  {
    private readonly IMMDeviceCollection _MMDeviceCollection_ = null;
    /************************************************/
    internal AudioDeviceCollection(IMMDeviceCollection obj)
    {
      this._MMDeviceCollection_ = obj;
    }
    /************************************************/
    public AudioDevice this[int index]
    {
      get
      {
        IMMDevice retValue = null;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._MMDeviceCollection_.Item((uint)index, out retValue));
        /************************************************/
        return new AudioDevice(retValue);
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