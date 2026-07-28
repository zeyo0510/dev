using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume : IDisposable
  {
    public int Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.GetMasterVolumeLevelScalar(out float retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.SetMasterVolumeLevelScalar(value / 100f, Guid.Empty));
      }
    }
  }
}