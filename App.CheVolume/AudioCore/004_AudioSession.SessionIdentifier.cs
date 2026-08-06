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
        Marshal.ThrowExceptionForHR(this.IAudioSessionControl.GetSessionIdentifier(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}