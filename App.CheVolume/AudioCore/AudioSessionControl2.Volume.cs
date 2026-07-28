using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
  {
    public float Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.GetMasterVolume(out float retValue));
        /************************************************/
        return retValue * 100f;
      }
      set
      {
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._SimpleAudioVolume_.SetMasterVolume(value / 100f, ref empty));
      }
    }
  }
}