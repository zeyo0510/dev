using AudioCore;
using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AudioDeviceCollection AudioDeviceCollection
  {
    get; set;
  }
}