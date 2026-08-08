using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public AudioSessionCollection AudioSessions
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioSessionManager.GetSessionEnumerator(out IAudioSessionEnumerator retValue));
        /************************************************/
        return new AudioSessionCollection(retValue);
      }
    }
  }
}