using System.Collections;
using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionEnumerator: IEnumerable<AudioSessionControl2>
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
        Marshal.ThrowExceptionForHR(this._AudioSessionEnumerator_.GetSession(index, out IAudioSessionControl2 retValue));
        /************************************************/
        return new AudioSessionControl2(retValue);
      }
    }
    /************************************************/
    public IEnumerator<AudioSessionControl2> GetEnumerator()
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