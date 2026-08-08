using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
using JC.CS.Lib.Extensions;
/************************************************/
namespace AudioCore
{
  partial class AudioSession : IAudioSessionEvents
  {
    private int lastVolume = 0;
    private bool lastMute = false;

    public event EventHandler<EventArgs>? ChannelVolumeChanged = null;
    public int OnChannelVolumeChanged(uint _CHANNEL_COUNT_, nint _NEW_CHANNEL_VOLUME_ARRAY_, uint ChangedChannel, Guid _EVENT_CONTEXT_)
    {
      this.ChannelVolumeChanged?.Invoke(this, EventArgs.Empty);
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<string>? DisplayNameChanged = null;
    public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string _NEW_DISPLAY_NAME_, Guid _EVENT_CONTEXT_)
    {
      this.DisplayNameChanged?.Invoke(this, _NEW_DISPLAY_NAME_);
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<Guid>? GroupingParamChanged = null;
    public int OnGroupingParamChanged(Guid _NEW_GROUPING_PARAM_, Guid _EVENT_CONTEXT_)
    {
      this.GroupingParamChanged?.Invoke(this, _NEW_GROUPING_PARAM_);
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<string>? IconPathChanged = null;
    public int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string _NEW_ICON_PATH_, Guid _EVENT_CONTEXT_)
    {
      this.IconPathChanged?.Invoke(this, _NEW_ICON_PATH_);
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<AudioSessionDisconnectReason>? SessionDisconnected = null;
    public int OnSessionDisconnected(AudioSessionDisconnectReason _DISCONNECT_REASON_)
    {
      this.SessionDisconnected?.Invoke(this, _DISCONNECT_REASON_);
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<EventArgs>? VolumeChanged = null;
    public event EventHandler<EventArgs> MuteChanged = null;
    public int OnSimpleVolumeChanged(float _NEW_VOLUME_, bool _NEW_MUTE_, Guid _EVENT_CONTEXT_)
    {
      int volume = (int)Math.Ceiling(_NEW_VOLUME_ * 100f);
      /************************************************/
      if (volume.IsNotEqualTo(this.lastVolume))
      {
        this.lastVolume = volume;
        this.VolumeChanged?.Invoke(this, EventArgs.Empty);
      }
      /************************************************/
      bool mute = _NEW_MUTE_;
      if (mute != this.lastMute)
      {
        this.lastMute = mute;
        this.MuteChanged.Invoke(this, EventArgs.Empty);
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public event EventHandler<AudioSessionState>? StateChanged = null;
    public int OnStateChanged(AudioSessionState _NEW_STATE_)
    {
      this.StateChanged?.Invoke(this, _NEW_STATE_);
      /************************************************/
      return 0;
    }
  }
}