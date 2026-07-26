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
      return this.EndPointVolume.Mute;
    }
    set
    {
      this.EndPointVolume.Mute = value;
    }
  }
}