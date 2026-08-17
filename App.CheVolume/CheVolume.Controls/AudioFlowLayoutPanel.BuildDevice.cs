using a;
using AudioCore;

namespace CheVolume.Controls;
/************************************************/
partial class AudioFlowLayoutPanel
{
  public void BuildDevice()
  {
    DeviceVolumeControl? deviceVolumeControl = this.Controls
    . OfType<DeviceVolumeControl>()
    . FirstOrDefault(x => {
      return string.Equals(x.Tag?.ToString(), this.AudioDevice.ID, StringComparison.OrdinalIgnoreCase);
    });
    /************************************************/
    if (deviceVolumeControl == null)
    {
      deviceVolumeControl = new(this.AudioDevice);
      {
        deviceVolumeControl.Tag  = this.AudioDevice.ID;
      }
      /************************************************/
      base.Controls.Add(deviceVolumeControl);
    }
    /************************************************/
    deviceVolumeControl?.SetDefault(Audio._MMDeviceEnumerator_.EnumerateAudioEndPoints(DataFlow.Render, ERole.eMultimedia).ID.Equals(this.AudioDevice.ID));
  }
}