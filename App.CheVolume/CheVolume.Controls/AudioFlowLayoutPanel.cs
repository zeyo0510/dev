using a;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class AudioFlowLayoutPanel : FlowLayoutPanel
{
  public AudioFlowLayoutPanel(AudioDeviceCollection deviceCollection, AudioDevice device)
  {
    this.AudioDeviceCollection = deviceCollection;
    this.AudioDevice           = device;
    /************************************************/
    this.InitializeComponent();
    /************************************************/
    this.BuildDevice();
  }
}