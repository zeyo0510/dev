using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioEndpointVolume
  {
    private readonly IAudioEndpointVolume _IAudioEndpointVolume = null;
    /************************************************/
    private AudioEndpointVolumeCallback _AudioEndpointVolumeCallback = null;
    /************************************************/
    internal AudioEndpointVolume(IAudioEndpointVolume obj)
    {
      this._IAudioEndpointVolume = obj;
      /************************************************/
      this._AudioEndpointVolumeCallback = new AudioEndpointVolumeCallback(this);
      /************************************************/
      Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.RegisterControlChangeNotify(this._AudioEndpointVolumeCallback));
    }
    /************************************************/
    ~AudioEndpointVolume()
    {
      this.Dispose();
    }
    /************************************************/
    public float Method1()
    {
      int count = this.ChannelCount;
      /************************************************/
      float retValue = 0f;
      /************************************************/
      for (uint i = 0u; i < count; i++)
      {
        float maxValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetChannelVolumeLevelScalar(i, out maxValue));
        /************************************************/
        if (maxValue > retValue)
        {
          retValue = maxValue;
        }
      }
      /************************************************/
      return retValue;
    }
  }
}