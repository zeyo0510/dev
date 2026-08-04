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
        Marshal.ThrowExceptionForHR(this._MMDevice_.GetId(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}