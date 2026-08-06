using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public string IconPath
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioSessionControl.GetIconPath(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}