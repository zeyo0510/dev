using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDeviceCollection
  {
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDeviceCollection.GetCount(out uint retValue));
        /************************************************/
        return (int)retValue;
      }
    }
  }
}