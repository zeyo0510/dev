using System;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2 : IAudioSessionEvents
  {
    public event EventHandler DisplayNameChanged   = null;
    public event EventHandler IconPathChanged      = null;
    public event EventHandler SimpleVolumeChanged  = null;
    public event EventHandler ChannelVolumeChanged = null;
    public event EventHandler GroupingParamChanged = null;
    public event EventHandler StateChanged         = null;
    public event EventHandler SessionDisconnected  = null;
    /************************************************/
    public int OnDisplayNameChanged(string P_0, Guid P_1)
    {
      if (this.DisplayNameChanged != null)
      {
        this.DisplayNameChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnIconPathChanged(string P_0, Guid P_1)
    {
      if (this.IconPathChanged != null)
      {
        this.IconPathChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnSimpleVolumeChanged(float P_0, bool P_1, Guid P_2)
    {
      if (this.SimpleVolumeChanged != null)
      {
        this.SimpleVolumeChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnChannelVolumeChanged(uint P_0, IntPtr P_1, uint P_2, Guid P_3)
    {
      if (this.ChannelVolumeChanged != null)
      {
        this.ChannelVolumeChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnGroupingParamChanged(Guid P_0, Guid P_1)
    {
      if (this.GroupingParamChanged != null)
      {
        this.GroupingParamChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnStateChanged(AudioSessionState P_0)
    {
      if (this.StateChanged != null)
      {
        this.StateChanged(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
    /************************************************/
    public int OnSessionDisconnected(AudioSessionDisconnectReason P_0)
    {
      if (this.SessionDisconnected != null)
      {
        this.SessionDisconnected(this, new EventArgs());
      }
      /************************************************/
      return 0;
    }
  }
}