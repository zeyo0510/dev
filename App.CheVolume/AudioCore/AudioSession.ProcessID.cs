using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    public uint ProcessID
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetProcessId(out uint retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}