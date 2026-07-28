using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(_SimpleAudioVolume_.SetMute(value, Guid.Empty));
      }
    }
  }
}