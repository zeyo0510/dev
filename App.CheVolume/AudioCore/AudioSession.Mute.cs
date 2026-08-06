using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.SetMute(value, Guid.Empty));
      }
    }
  }
}