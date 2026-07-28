using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionEnumerator
  {
    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionEnumerator_.GetCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}