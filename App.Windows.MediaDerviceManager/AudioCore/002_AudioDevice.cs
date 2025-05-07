using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioDevice
  {
    private readonly IMMDevice _MMDevice_;
    /************************************************/
    internal AudioDevice(IMMDevice obj)
    {
      this._MMDevice_ = obj;
    }
    /************************************************/
    public string ID
    {
      get
      {
        string retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._MMDevice_.GetId(out retValue));
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public DeviceState State
    {
      get
      {
        DeviceState retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._MMDevice_.GetState(out retValue));
        /************************************************/
        return retValue;
      }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    private PropertyStore _PropertyStore = null;
    private PropertyStore PropertyStore
    {
      get
      {
        if (this._PropertyStore == null)
        {
          IPropertyStore retValue = null;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.OpenPropertyStore(EStgmAccess.STGM_READ, out retValue));
          /************************************************/
          this._PropertyStore = new PropertyStore(retValue);
        }
        /************************************************/
        return this._PropertyStore;
      }
    }
    /************************************************/
    public string FriendlyName
    {
      get
      {
        if (this.PropertyStore.Contains(PKEY.PKEY_Device_FriendlyName))
        {
          return (string)this.PropertyStore[PKEY.PKEY_Device_FriendlyName].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }
    /************************************************/
    public string NameDesc
    {
      get
      {
        if (this.PropertyStore.Contains(PropertyKeys.PKEY_Device_DeviceDesc))
        {
          return (string)this.PropertyStore[PropertyKeys.PKEY_Device_DeviceDesc].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }
    /************************************************/
    public string DeviceFriendlyName
    {
      get
      {
        if (this.PropertyStore.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
        {
          return (string)this.PropertyStore[PropertyKeys.PKEY_DeviceInterface_FriendlyName].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }
    /************************************************/
    public string IconPath
    {
      get
      {
        if (this.PropertyStore.Contains(PKEY.PKEY_DeviceClass_IconPath))
        {
          return (string)this.PropertyStore[PKEY.PKEY_DeviceClass_IconPath].PropVariant.GetValue();
        }
        return "Unknown";
      }
    }
    /************************************************/
    public DataFlow DataFlow
    {
      get
      {
        DataFlow retValue;
        /************************************************/
        (this._MMDevice_ as IMMEndpoint).GetDataFlow(out retValue);
        /************************************************/
        return retValue;
      }
    }
  }
}