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
        Marshal.ThrowExceptionForHR(this.ISimpleAudioVolume.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this.ISimpleAudioVolume.SetMute(value, ref empty));
      }
    }
  }
}