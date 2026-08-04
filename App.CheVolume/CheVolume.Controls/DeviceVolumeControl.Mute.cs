using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Mute
  {
    get
    {
      return this.AudioDevice.Mute;
    }
    set
    {
      this.AudioDevice.Mute = value;
    }
  }
}