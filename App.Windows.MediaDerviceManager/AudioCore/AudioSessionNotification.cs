using System;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class AudioSessionNotification : IAudioSessionNotification
  {
    private IAudioSessionNotificationCollection _AudioSessionNotificationCollection_ = null;
    /************************************************/
    public AudioSessionNotification(IAudioSessionNotificationCollection obj)
    {
      this._AudioSessionNotificationCollection_ = obj;
    }
    /************************************************/
    public int OnSessionCreated(IAudioSessionControl P_0)
    {
      return _AudioSessionNotificationCollection_.AlwaysZero(new AudioSessionControl2((IAudioSessionControl2)P_0));
    }
  }
}