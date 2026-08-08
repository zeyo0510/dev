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
      return this.AudioSession.Mute;
    }
    set
    {
      this.AudioSession.Mute = value;
    }
  }
}