using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int Volume
  {
    get
    {
      return this.SessionControl.Volume;
    }
    set
    {
      this.SessionControl.Volume = value;
    }
  }
}