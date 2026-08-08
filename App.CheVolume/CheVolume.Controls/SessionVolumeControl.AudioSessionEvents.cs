using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class SessionVolumeControl
{
  public void AudioSession_DisplayNameChanged(object sender, string displayName)
  {
    Console.WriteLine($"OnDisplayNameChanged : {displayName}");
  }
  /************************************************/
  public void AudioSession_IconPathChanged(object sender, string iconPath)
  {
    Console.WriteLine($"OnIconPathChanged : {iconPath}");
  }
  /************************************************/
  public void AudioSession_VolumeChanged(object sender, EventArgs e)
  {
    Console.WriteLine($"AudioSession_VolumeChanged");
    /************************************************/
    this.SetVolumeText(this.Volume.ToString());
    if (!macTrackBar1.Dragging)
    {
      this.SetTrackBar(this.Volume);
    }
  }
  public void AudioSession_MuteChanged(object sender, EventArgs e)
  {
    Console.WriteLine($"AudioSession_MuteChanged");
    /************************************************/
    this.SetMute(this.Mute);
  }
  /************************************************/
  public void AudioSession_ChannelVolumeChanged(object sender, EventArgs e)
  {
    Console.WriteLine($"OnChannelVolumeChanged");
  }
  /************************************************/
  public void AudioSession_GroupingParamChanged(object sender, Guid guid)
  {
    Console.WriteLine($"OnGroupingParamChanged");
  }
  /************************************************/
  public void AudioSession_StateChanged(object sender, AudioSessionState state)
  {
    Console.WriteLine($"OnStateChanged : {state}");
    /************************************************/
    this.process1.Refresh();
    /************************************************/
    OnStateChanged2(state);
  }
  /************************************************/
  public void AudioSession_SessionDisconnected(object sender, AudioSessionDisconnectReason disconnectReason)
  {
    Console.WriteLine($"OnSessionDisconnected : {disconnectReason}");
    /************************************************/
    RemoveSessionNotif();
  }
}