using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public AudioSessionState State
    {
      get
      {
        AudioSessionState retValue = AudioSessionState.AudioSessionStateInactive;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioSessionControl2.GetState(out retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}