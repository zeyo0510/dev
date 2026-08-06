using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public AudioSessionState State
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetState(out AudioSessionState retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}