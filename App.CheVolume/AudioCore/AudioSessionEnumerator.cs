using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioSessionEnumerator
  {
    private readonly IAudioSessionEnumerator _AudioSessionEnumerator_;
    /************************************************/
    internal AudioSessionEnumerator(IAudioSessionEnumerator _AUDIO_SESSION_ENUMERATOR_)
    {
      this._AudioSessionEnumerator_ = _AUDIO_SESSION_ENUMERATOR_;
    }
    /************************************************/
    public AudioSessionControl2 this[int index]
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioSessionEnumerator_.GetSession(index, out IAudioSessionControl2 audioSessionControl));
        /************************************************/
        return new AudioSessionControl2(audioSessionControl);
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioSessionEnumerator_.GetCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}