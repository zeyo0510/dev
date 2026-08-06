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
        Console.WriteLine("SimpleAudioVolume :: GetMasterVolume");
        /************************************************/
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.GetMasterVolume(out float retValue));
        /************************************************/
        return retValue * 100f;
      }
      set
      {
        Console.WriteLine("SimpleAudioVolume :: SetMasterVolume");
        /************************************************/
        Guid empty = Guid.Empty;
        /************************************************/
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.SetMasterVolume(value / 100f, ref empty));
      }
    }
  }
}