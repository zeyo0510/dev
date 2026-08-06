using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public string DisplayName
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetDisplayName(out string retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}