using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public string SessionIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionIdentifier(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}