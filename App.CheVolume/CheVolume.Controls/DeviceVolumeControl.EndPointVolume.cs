using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  private AudioEndpointVolume EndPointVolume
  {
    get
    {
      return this._MM_DEVICE_.AudioEndpointVolume;
    }
  }
}