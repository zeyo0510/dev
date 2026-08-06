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
      this.lastNute = this.Mute;
      /************************************************/
      Marshal.ThrowExceptionForHR(this.IAudioEndpointVolume.RegisterControlChangeNotify(this));
    }
    /************************************************/
    ~AudioDevice()
    {
      this.Dispose();
    }

    public PropertyStore Properties
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        return this._PropertyStore_;
      }
    }

    public string FriendlyName
    {
      get
      {
        this._PropertyStore_ ??= GetPropertyInformation();
        /************************************************/
        if (this._PropertyStore_.Contains(PropertyKeys.PKEY_Device_FriendlyName))
        {
          return (string)this._PropertyStore_[PropertyKeys.PKEY_Device_FriendlyName].PropVariant.GetValue();
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
        if (this._PropertyStore_.Contains(PropertyKeys.PKEY_Device_DeviceDesc))
        {
          return (string)this._PropertyStore_[PropertyKeys.PKEY_Device_DeviceDesc].PropVariant.GetValue();
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
        if (this._PropertyStore_.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
        {
          return (string)this._PropertyStore_[PropertyKeys.PKEY_DeviceInterface_FriendlyName].PropVariant.GetValue();
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
        if (this._PropertyStore_.Contains(PropertyKeys.PKEY_DeviceClass_IconPath))
        {
          return (string)this._PropertyStore_[PropertyKeys.PKEY_DeviceClass_IconPath].PropVariant.GetValue();
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