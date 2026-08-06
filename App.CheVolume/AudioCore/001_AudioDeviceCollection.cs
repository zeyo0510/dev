using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioDeviceCollection
  {
    internal AudioDeviceCollection(IMMDeviceCollection _MM_DEVICE_COLLECTION_)
    {
      this.IMMDeviceCollection = _MM_DEVICE_COLLECTION_;
    }
    /************************************************/
    public AudioDevice this[int index]
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDeviceCollection.Item((uint)index, out IMMDevice retValue));
        /************************************************/
        return new AudioDevice(retValue);
      }
    }
  }
}