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
        Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.GetMasterVolumeLevelScalar(out float retValue));
        /************************************************/
        retValue = (int)Math.Ceiling(retValue * 100f);
        /************************************************/
        return (int)retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.SetMasterVolumeLevelScalar(value / 100f, Guid.Empty));
      }
    }
  }
}