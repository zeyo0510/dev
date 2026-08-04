using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  private AudioMeterInformation MeterInformation
  {
    get
    {
      return this.AudioDevice.AudioMeterInformation;
    }
  }
}