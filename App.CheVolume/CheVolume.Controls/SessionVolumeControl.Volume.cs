using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public float Volume
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