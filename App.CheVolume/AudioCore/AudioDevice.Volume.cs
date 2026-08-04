using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public int Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(this.AudioEndpointVolume.GetMasterVolumeLevelScalar(out float retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.AudioEndpointVolume.SetMasterVolumeLevelScalar(value / 100f, Guid.Empty));
      }
    }
  }
}