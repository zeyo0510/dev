using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public string ID
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IMMDevice.GetId(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}