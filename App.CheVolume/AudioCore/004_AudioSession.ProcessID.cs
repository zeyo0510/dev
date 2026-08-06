using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public uint ProcessID
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioSessionControl.GetProcessId(out uint retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}