using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioEndpointVolume : IDisposable
  {
    private readonly IAudioEndpointVolume _AudioEndPointVolume_;

    private AudioEndpointVolumeCallback _CallBack;

    public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

    internal AudioEndpointVolume(IAudioEndpointVolume _AUDIO_ENDPOINT_VOLUME_)
    {
      this._AudioEndPointVolume_ = _AUDIO_ENDPOINT_VOLUME_;
      _CallBack = new AudioEndpointVolumeCallback(this);
      Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.RegisterControlChangeNotify(_CallBack));
    }

    ~AudioEndpointVolume()
    {
      this.Dispose();
    }

    internal void FireNotification(AudioVolumeNotificationData _AUDIO_VOLUME_NOTIFICATION_DATA_)
    {
      this.OnVolumeNotification?.Invoke(_AUDIO_VOLUME_NOTIFICATION_DATA_);
    }

    public void Dispose()
    {
      if (_CallBack != null)
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.UnregisterControlChangeNotify(_CallBack));
        _CallBack = null;
      }
    }

    public float Method1()
    {
      Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.GetChannelCount(out int count));
      /************************************************/
      float maxVolume = 0f;
      /************************************************/
      for (uint i = 0u; i < count; i++)
      {
        Marshal.ThrowExceptionForHR(this._AudioEndPointVolume_.GetChannelVolumeLevelScalar(i, out float volume));
        /************************************************/
        if (volume > maxVolume)
        {
          maxVolume = volume;
        }
      }
      /************************************************/
      return maxVolume;
    }
  }
}