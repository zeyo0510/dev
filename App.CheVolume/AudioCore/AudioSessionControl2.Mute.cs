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
        Console.WriteLine("SimpleAudioVolume :: GetMute");
        /************************************************/
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Console.WriteLine("SimpleAudioVolume :: SetMute");
        /************************************************/
        Marshal.ThrowExceptionForHR(this.SimpleAudioVolume.SetMute(value, Guid.Empty));
      }
    }
  }
}