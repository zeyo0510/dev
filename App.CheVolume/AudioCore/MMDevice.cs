using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
  public class MMDevice
  {
    private readonly IMMDevice _MMDevice_;

    private PropertyStore propertyStore1;

    private AudioMeterInformation audioMeterInformation1;

    private AudioEndpointVolume audioEndpointVolume1;

    private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");

    private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");

    private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");

    private static Guid IID_IAudioSessionManager2 = new Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F");

    public AudioClient AudioClient
    {
      get
      {
        return GetAudioClient();
      }
    }

    public AudioSessionManager2 AudioSessionManager
    {
      get
      {
        return GetAudioSessionManager();
      }
    }

    public AudioMeterInformation AudioMeterInformation
    {
      get
      {
        if (audioMeterInformation1 == null)
        {
          GetAudioMeterInformation();
        }
        return audioMeterInformation1;
      }
    }

    public AudioEndpointVolume AudioEndpointVolume
    {
      get
      {
        if (audioEndpointVolume1 == null)
        {
          GetAudioEndpointVolume();
        }
        return audioEndpointVolume1;
      }
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

    public string ID
    {
      get
      {
        string result;
        Marshal.ThrowExceptionForHR(_MMDevice_.GetId(out result));
        return result;
      }
    }

    public DataFlow DataFlow
    {
      get
      {
        DataFlow result;
        (_MMDevice_ as IMMEndpoint).GetDataFlow(out result);
        return result;
      }
    }

    public DeviceState State
    {
      get
      {
        DeviceState result;
        Marshal.ThrowExceptionForHR(_MMDevice_.GetState(out result));
        return result;
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

    private AudioClient GetAudioClient()
    {
      object obj;
      Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioClient, CLSCTX.ALL, IntPtr.Zero, out obj));
      return new AudioClient(obj as IAudioClient);
    }

    private void GetAudioMeterInformation()
    {
      object obj;
      Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out obj));
      audioMeterInformation1 = new AudioMeterInformation(obj as IAudioMeterInformation);
    }

    private AudioSessionManager2 GetAudioSessionManager()
    {
      object obj;
      Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out obj));
      return new AudioSessionManager2(obj as IAudioSessionManager2);
    }

    private void GetAudioEndpointVolume()
    {
      object obj;
      Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out obj));
      audioEndpointVolume1 = new AudioEndpointVolume(obj as IAudioEndpointVolume);
    }

    internal MMDevice(IMMDevice P_0)
    {
      _MMDevice_ = P_0;
    }

    public override string ToString()
    {
      return FriendlyName;
    }
  }
}
