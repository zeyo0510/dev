using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionCollection
  {
    internal AudioSessionCollection(IAudioSessionEnumerator _AUDIO_SESSION_ENUMERATOR_)
    {
      this.IAudioSessionEnumerator = _AUDIO_SESSION_ENUMERATOR_;
    }
    /************************************************/
    public AudioSession this[int index]
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioSessionEnumerator.GetSession(index, out IAudioSessionControl2 retValue));
        /************************************************/
        return new AudioSession(retValue);
      }
    }
  }
}