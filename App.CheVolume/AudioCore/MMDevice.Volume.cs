using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    public int Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.GetMasterVolumeLevelScalar(out float retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;
      }
      set
      {
        // value = (int)Math.Ceiling(value / 100f);
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.SetMasterVolumeLevelScalar(value / 100f, Guid.Empty));
      }
    }
  }
}