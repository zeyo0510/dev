using System.Collections;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioDeviceCollection : IEnumerable<AudioDevice>
  {
    private readonly IMMDeviceCollection _MMDeviceCollection_;
    /************************************************/
    internal AudioDeviceCollection(IMMDeviceCollection _MM_DEVICE_COLLECTION_)
    {
      this._MMDeviceCollection_ = _MM_DEVICE_COLLECTION_;
    }
    /************************************************/
    public AudioDevice this[int index]
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._MMDeviceCollection_.Item((uint)index, out IMMDevice retValue));
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
    /************************************************/
    public IEnumerator<AudioDevice> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }
    /************************************************/
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}