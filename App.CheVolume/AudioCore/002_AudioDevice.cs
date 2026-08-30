// TODO: JC TEST

using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioDevice
  {
    private PropertyStore _PropertyStore_;

    internal AudioDevice(IMMDevice _MM_DEVICE_)
    {
      this.IMMDevice = _MM_DEVICE_;
      /************************************************/
      this.lastVolume = this.Volume;
      this.lastMute = this.Mute;
      /************************************************/
      Marshal.ThrowExceptionForHR(this.IAudioMeterInformation.QueryHardwareSupport(out int retValue));
      this.IsHardwareVolumeSupported = (retValue & EndpointHardwareSupport._ENDPOINT_HARDWARE_SUPPORT_VOLUME_) != 0;
      this.IsHardwareMuteSupported   = (retValue & EndpointHardwareSupport._ENDPOINT_HARDWARE_SUPPORT_MUTE_  ) != 0;
      this.IsHardwareMeterSupported  = (retValue & EndpointHardwareSupport._ENDPOINT_HARDWARE_SUPPORT_METER_ ) != 0;
      /************************************************/
      Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.RegisterControlChangeNotify(this));
      Marshal.ThrowExceptionForHR(this.IAudioSessionManager.RegisterSessionNotification(this));
    }
    /************************************************/
    ~AudioDevice()
    {
      this.Dispose();
    }

    public string FriendlyName
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        if (this._PropertyStore_.Contains(PKEY.PKEY_Device_FriendlyName))
        {
          return (string)this._PropertyStore_[PKEY.PKEY_Device_FriendlyName]?.GetValue();
        }
        /************************************************/
        return "Unknown";
      }
    }

    public string NameDesc
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        if (this._PropertyStore_.Contains(PKEY.PKEY_Device_DeviceDesc))
        {
          return (string)this._PropertyStore_[PKEY.PKEY_Device_DeviceDesc]?.GetValue();
        }
        /************************************************/
        return "Unknown";
      }
    }

    public string DeviceFriendlyName
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        if (this._PropertyStore_.Contains(PKEY.PKEY_DeviceInterface_FriendlyName))
        {
          return (string)this._PropertyStore_[PKEY.PKEY_DeviceInterface_FriendlyName]?.GetValue();
        }
        return "Unknown";
      }
    }

    public string IconPath
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        if (this._PropertyStore_.Contains(PKEY.PKEY_DeviceClass_IconPath))
        {
          return (string)this._PropertyStore_[PKEY.PKEY_DeviceClass_IconPath]?.GetValue();
        }
        /************************************************/
        return "Unknown";
      }
    }

    private PropertyStore GetPropertyInformation()
    {
      Marshal.ThrowExceptionForHR(this.IMMDevice.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore retValue));
      /************************************************/
      return new PropertyStore(retValue);
    }

    public override string ToString()
    {
      return FriendlyName;
    }
  }
}