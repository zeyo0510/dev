using AudioCore;
using AudioCore.Interfaces;
using System.Runtime.InteropServices;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class SessionVolumeControl : IAudioSessionEvents
{
  public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string displayName, Guid context)
  {
    Console.WriteLine($"OnDisplayNameChanged : {displayName}");
    /************************************************/
    return 0;
  }
  /************************************************/
  public int OnIconPathChanged(string iconPath, Guid context)
  {
    Console.WriteLine($"OnIconPathChanged : {iconPath}");
    /************************************************/
    return 0;
  }
  /************************************************/
  public int OnSimpleVolumeChanged(float volume, bool mute, Guid context)
  {
    Console.WriteLine($"OnSimpleVolumeChanged : {volume}, {mute}");
    /************************************************/
    this.SetMute(mute);
    this.SetVolumeText(Math.Ceiling(volume * 100f).ToString());
    if (!macTrackBar1.Dragging)
    {
      this.SetTrackBar((decimal)Math.Ceiling(volume * 100f));
    }
    /************************************************/
    return 0;
  }
  /************************************************/
  public int OnChannelVolumeChanged(uint P_0, IntPtr P_1, uint P_2, Guid context)
  {
    Console.WriteLine($"OnChannelVolumeChanged : {P_0}");
    /************************************************/
    return 0;
  }
  /************************************************/
  public int OnGroupingParamChanged(Guid groupingParam, Guid context)
  {
    return 0;
  }
  /************************************************/
  public int OnStateChanged(AudioSessionState state)
  {
    this.process1.Refresh();
    /************************************************/
    OnStateChanged2(state);
    /************************************************/
    return 0;
  }
  /************************************************/
  public int OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason)
  {
    RemoveSessionNotif();
    /************************************************/
    return 0;
  }
}