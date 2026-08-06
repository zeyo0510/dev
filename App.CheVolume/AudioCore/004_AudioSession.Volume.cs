using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public int Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.GetMasterVolume(out float retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;

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