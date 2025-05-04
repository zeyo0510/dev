using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioSessionEnumerator
  {
    private readonly IAudioSessionEnumerator _AudioSessionEnumerator_ = null;
    /************************************************/
    internal AudioSessionEnumerator(IAudioSessionEnumerator obj)
    {
      this._AudioSessionEnumerator_ = obj;
    }
    /************************************************/
    public AudioSessionControl2 this[int index]
    {
      get
      {
        IAudioSessionControl2 retValue = null;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioSessionEnumerator_.GetSession(index, out retValue));
        /************************************************/
        return new AudioSessionControl2(retValue);
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        int retVlaue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioSessionEnumerator_.GetCount(out retVlaue));
        /************************************************/
        return retVlaue;
      }
    }
  }
}