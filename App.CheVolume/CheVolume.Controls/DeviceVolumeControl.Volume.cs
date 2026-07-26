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
      return this.EndPointVolume.Volume;
    }
    set
    {
      this.EndPointVolume.Volume = value;
    }
  }
}