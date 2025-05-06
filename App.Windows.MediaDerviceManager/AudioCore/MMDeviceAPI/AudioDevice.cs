using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioDevice
  {
    private readonly IMMDevice _MMDevice_ = null;
    /************************************************/
    internal AudioDevice(IMMDevice obj)
    {
      this._MMDevice_ = obj;
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
    private AudioClient _AudioClient = null;
    public AudioClient AudioClient
    {
      get
      {
        if (this._AudioClient == null)
        {
          object retValue = null;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioClient, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioClient = new AudioClient(retValue as IAudioClient);
        }
        /************************************************/
        return this._AudioClient;
      }
    }
    /************************************************/
    private AudioMeterInformation _AudioMeterInformation = null;
    public AudioMeterInformation AudioMeterInformation
    {
      get
      {
        if (this._AudioMeterInformation == null)
        {
          object retValue = null;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioMeterInformation = new AudioMeterInformation(retValue as IAudioMeterInformation);
        }
        return this._AudioMeterInformation;
      }
    }
    /************************************************/
    private AudioSessionManager2 _AudioSessionManager = null;
    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        if (this._AudioSessionManager == null)
        {
          object retValue = null;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioSessionManager = new AudioSessionManager2(retValue as IAudioSessionManager2);
        }
        /************************************************/
        return this._AudioSessionManager;
      }
    }
    /************************************************/
    private AudioEndpointVolume _AudioEndpointVolume = null;
    public AudioEndpointVolume AudioEndpointVolume
    {
      get
      {
        if (this._AudioEndpointVolume == null)
        {
          object retValue = null;
          /************************************************/
          Marshal.ThrowExceptionForHR(this._MMDevice_.Activate(ref Guids.IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out retValue));
          /************************************************/
          this._AudioEndpointVolume = new AudioEndpointVolume(retValue as IAudioEndpointVolume);
        }
        /************************************************/
        return this._AudioEndpointVolume;
      }
    }
    /************************************************/
    public override string ToString()
    {
      return FriendlyName;
    }
  }
}