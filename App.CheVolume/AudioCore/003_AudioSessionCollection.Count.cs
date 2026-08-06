using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionCollection
  {
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.IAudioSessionEnumerator.GetCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}