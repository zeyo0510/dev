using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class DeviceVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int Volume
  {
    get
    {
      return this.AudioDevice.Volume;
    }
    set
    {
      this.AudioDevice.Volume = value;
    }
  }
}