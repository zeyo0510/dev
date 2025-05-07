using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioEndpointVolume
  {
    private readonly IAudioEndpointVolume _IAudioEndpointVolume;
    /************************************************/
    private AudioEndpointVolumeCallback _AudioEndpointVolumeCallback;
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
    public int ChannelCount
    {
      get
      {
        int retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetChannelCount(out retValue));
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public float MasterVolumeLevelScalar
    {
      get
      {
        float retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetMasterVolumeLevelScalar(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
      }
    }
    /************************************************/
    public bool Mute
    {
      get
      {
        bool retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.GetMute(out retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this._IAudioEndpointVolume.SetMute(value, Guid.Empty));
      }
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