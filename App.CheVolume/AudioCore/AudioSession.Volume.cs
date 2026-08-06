using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public float Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.GetMasterVolume(out float retValue));
        /************************************************/
        return retValue * 100f;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.SetMasterVolume(value / 100f, ref empty));
      }
    }
  }
}