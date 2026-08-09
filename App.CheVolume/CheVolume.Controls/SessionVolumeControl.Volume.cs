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
      return this.AudioSession.Volume;
    }
    set
    {
      this.AudioSession.Volume = value;
      /************************************************/
      this.UpdateUI();
    }
  }
}