using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume : IDisposable
  {
    public float MasterVolumeLevelScalar
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.GetMasterVolumeLevelScalar(out float retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.SetMasterVolumeLevelScalar(value, Guid.Empty));
      }
    }
  }
}