using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class MMDevice : IDisposable
  {
    private readonly IMMDevice _MMDevice_;
    private readonly IMMEndpoint _MMEndpoint_;

    private PropertyStore _PropertyStore_;

    internal MMDevice(IMMDevice _MM_DEVICE_)
    {
      this._MMDevice_ = _MM_DEVICE_;
      /************************************************/
      this._MMEndpoint_ = (IMMEndpoint)this._MMDevice_;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._AudioEndpointVolume_.RegisterControlChangeNotify(this));
    }
    
    ~MMDevice()
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
        if (this._PropertyStore_.Contains(PKEY.PKEY_Device_FriendlyName))
        {
          return (string)this._PropertyStore_[PKEY.PKEY_Device_FriendlyName].PropVariant.GetValue();
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

    public DeviceState State
    {
      get
      {
        Marshal.ThrowExceptionForHR(_MMDevice_.GetState(out DeviceState retValue));
        /************************************************/
        return retValue;
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
          return (string)this._PropertyStore_[PKEY.PKEY_DeviceClass_IconPath].PropVariant.GetValue();
        }
        /************************************************/
        return "Unknown";
      }
    }

    private PropertyStore GetPropertyInformation()
    {
      Marshal.ThrowExceptionForHR(this._MMDevice_.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore retValue));
      /************************************************/
      return new PropertyStore(retValue);
    }

    public override string ToString()
    {
      return FriendlyName;
    }
  }
}