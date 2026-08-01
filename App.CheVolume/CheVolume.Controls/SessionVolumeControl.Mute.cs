using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Mute
  {
    get
    {
      return this.SessionControl.Mute;
    }
    set
    {
      this.SessionControl.Mute = value;
    }
  }
}