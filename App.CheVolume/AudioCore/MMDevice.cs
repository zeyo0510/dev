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