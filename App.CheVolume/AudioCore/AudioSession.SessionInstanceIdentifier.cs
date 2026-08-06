using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public string SessionInstanceIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionInstanceIdentifier(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}