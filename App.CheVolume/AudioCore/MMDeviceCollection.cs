using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class MMDeviceCollection
  {
    private readonly IMMDeviceCollection _MMDeviceCollection_;
    /************************************************/
    internal MMDeviceCollection(IMMDeviceCollection _MM_DEVICE_COLLECTION_)
    {
      this._MMDeviceCollection_ = _MM_DEVICE_COLLECTION_;
    }
    /************************************************/
    public AudioDevice this[int index]
    {
      get
      {
        this._MMDeviceCollection_.Item((uint)index, out IMMDevice retValue);
        /************************************************/
        return new AudioDevice(retValue);
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._MMDeviceCollection_.GetCount(out uint retValue));
        /************************************************/
        return (int)retValue;
      }
    }
  }
}