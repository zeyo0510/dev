using System.Collections;
using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionEnumerator: IEnumerable<AudioSession>
  {
    private readonly IAudioSessionEnumerator _AudioSessionEnumerator_;
    /************************************************/
    internal AudioSessionEnumerator(IAudioSessionEnumerator _AUDIO_SESSION_ENUMERATOR_)
    {
      this._AudioSessionEnumerator_ = _AUDIO_SESSION_ENUMERATOR_;
    }
    /************************************************/
    public AudioSession this[int index]
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionEnumerator_.GetSession(index, out IAudioSessionControl2 retValue));
        /************************************************/
        return new AudioSession(retValue);
      }
    }
    /************************************************/
    public IEnumerator<AudioSession> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }
    /************************************************/
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}