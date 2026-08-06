using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public DeviceState State
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDevice.GetState(out DeviceState retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}