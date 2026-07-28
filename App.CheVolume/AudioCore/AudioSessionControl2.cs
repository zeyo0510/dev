using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioSessionControl2
  {
    private readonly IAudioSessionControl2 _AudioSessionControl_;
    /************************************************/
    internal AudioSessionControl2(IAudioSessionControl2 _AUDIO_SESSION_CONTROL_)
    {
      this._AudioSessionControl_ = _AUDIO_SESSION_CONTROL_;
    }
    /************************************************/
    public uint ProcessID
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetProcessId(out uint retValue));
        /************************************************/
        return retValue;
      }
    }

    public string DisplayName
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetDisplayName(out nint ptr));
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return result;
      }
    }

    public string IconPath
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetIconPath(out nint ptr));
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return result;
      }
    }

    public string SessionIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionIdentifier(out nint ptr));
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return result;
      }
    }

    public string SessionInstanceIdentifier
    {
      get
      {
        Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetSessionInstanceIdentifier(out nint ptr));
        string result = Marshal.PtrToStringAuto(ptr);
        Marshal.FreeCoTaskMem(ptr);
        return result;
      }
    }

    public void RegisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.RegisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents _NEW_NOTIFICATIONS_)
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.UnregisterAudioSessionNotification(_NEW_NOTIFICATIONS_));
    }

    public AudioSessionState GetState()
    {
      Marshal.ThrowExceptionForHR(this._AudioSessionControl_.GetState(out AudioSessionState retValue));
      /************************************************/
      return retValue;
    }

    public int GetCount()
    {
      Marshal.ThrowExceptionForHR(((IAudioMeterInformation)this._AudioSessionControl_).GetMeteringChannelCount(out int retValue));
      /************************************************/
      return retValue;
    }

    public float Volume
    {
      get
      {
        Marshal.ThrowExceptionForHR(((ISimpleAudioVolume)this._AudioSessionControl_).GetMasterVolume(out float retValue));
        /************************************************/
        retValue *= 100f;
        /************************************************/
        return retValue;
      }
      set
      {
        ISimpleAudioVolume obj = (ISimpleAudioVolume)this._AudioSessionControl_;
        Guid empty = Guid.Empty;
        float num = (float)value / 100f;
        Marshal.ThrowExceptionForHR(obj.SetMasterVolume(num, ref empty));
      }
    }

    public bool Mute
    {
      get
      {
        Marshal.ThrowExceptionForHR((_AudioSessionControl_ as ISimpleAudioVolume).GetMute(out bool retValue));
        /************************************************/
        return retValue;
      }
      set
      {
        Marshal.ThrowExceptionForHR((_AudioSessionControl_ as ISimpleAudioVolume).SetMute(value, Guid.Empty));
      }
    }

    public float[] GetChannelsPeakValues()
    {
      IAudioMeterInformation obj = (IAudioMeterInformation)_AudioSessionControl_;
      float[] array = new float[GetCount()];
      GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
      Marshal.ThrowExceptionForHR(obj.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
      gCHandle.Free();
      return array;
    }

    public float Method1()
    {
      IAudioMeterInformation obj = (IAudioMeterInformation)_AudioSessionControl_;
      float result;
      obj.GetPeakValue(out result);
      int num;
      obj.GetMeteringChannelCount(out num);
      return result;
    }
  }
}
