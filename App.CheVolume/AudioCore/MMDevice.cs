using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class MMDevice
  {
    private readonly IMMDevice _MMDevice_;
    private readonly IMMEndpoint _MMEndpoint_;

    private PropertyStore propertyStore1;

    private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");

    private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");

    private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");

    private static Guid IID_IAudioSessionManager2 = new Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F");

    internal MMDevice(IMMDevice _MM_DEVICE_)
    {
      this._MMDevice_ = _MM_DEVICE_;
      this._MMEndpoint_ = (IMMEndpoint)this._MMDevice_;
    }

    public PropertyStore Properties
    {
      get
      {
        if (propertyStore1 == null)
        {
          propertyStore1 = GetPropertyInformation();
        }
        return propertyStore1;
      }
    }

    public string FriendlyName
    {
      get
      {
        if (propertyStore1 == null)
        {
          propertyStore1 = GetPropertyInformation();
        }
        if (propertyStore1.Contains(PKEY.PKEY_Device_FriendlyName))
        {
          return (string)propertyStore1[PKEY.PKEY_Device_FriendlyName].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }

    public string NameDesc
    {
      get
      {
        if (propertyStore1 == null)
        {
          propertyStore1 = GetPropertyInformation();
        }
        if (propertyStore1.Contains(PropertyKeys.PKEY_Device_DeviceDesc))
        {
          return (string)propertyStore1[PropertyKeys.PKEY_Device_DeviceDesc].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }

    public string DeviceFriendlyName
    {
      get
      {
        if (propertyStore1 == null)
        {
          propertyStore1 = GetPropertyInformation();
        }
        if (propertyStore1.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
        {
          return (string)propertyStore1[PropertyKeys.PKEY_DeviceInterface_FriendlyName].PropVariant.GetValue();
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
        if (propertyStore1 == null)
        {
          propertyStore1 = GetPropertyInformation();
        }
        if (propertyStore1.Contains(PKEY.PKEY_DeviceClass_IconPath))
        {
          return (string)propertyStore1[PKEY.PKEY_DeviceClass_IconPath].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }

    private PropertyStore GetPropertyInformation()
    {
      IPropertyStore propertyStore;
      Marshal.ThrowExceptionForHR(_MMDevice_.OpenPropertyStore(EStgmAccess.STGM_READ, out propertyStore));
      return new PropertyStore(propertyStore);
    }

    public override string ToString()
    {
      return FriendlyName;
    }
  }
}