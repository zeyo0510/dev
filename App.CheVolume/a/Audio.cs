using System.Runtime.InteropServices;
using AudioCore;
using AudioCore.Interfaces;
/************************************************/
namespace a
{
  internal static class Audio
  {
    private static readonly PolicyConfigClient? policyConfigClient1 = null;
    /************************************************/
    static Audio()
    {
      // do nothing....
    }
    /************************************************/
    public static AudioDevice DefaultRenderDevice
    {
      get
      {
        return Audio.EnumerateAudioEndPoints(DataFlow.Render, ERole.eMultimedia);
      }
    }
    /************************************************/
    internal static void SetDefault(string s)
    {
      policyConfigClient1?.SetDefaultEndpoint(s, ERole.eMultimedia);
    }
    /************************************************/
    private static readonly IMMDeviceEnumerator obj = (IMMDeviceEnumerator)new MMDeviceEnumeratorComObject();
    /************************************************/
    private static AudioDeviceCollection EnumAudioEndpoints(DataFlow dataFlow, DeviceState state)
    {
      Marshal.ThrowExceptionForHR(obj.EnumAudioEndpoints(dataFlow, state, out IMMDeviceCollection retValue));
      /************************************************/
      return new AudioDeviceCollection(retValue);
    }
    /************************************************/
    public static AudioDevice EnumerateAudioEndPoints(DataFlow dataFlow, ERole role)
    {
      Marshal.ThrowExceptionForHR(obj.GetDefaultAudioEndpoint(dataFlow, role, out IMMDevice retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public static AudioDevice GetDevice(string deviceID)
    {
      Marshal.ThrowExceptionForHR(obj.GetDevice(deviceID, out IMMDevice retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public static void RegisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(obj.RegisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
    /************************************************/
    public static void UnregisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(obj.UnregisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
    /************************************************/
    public static AudioDeviceCollection ActiveRenderDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
      }
    }
    /************************************************/
    public static AudioDeviceCollection DisabledRenderDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Render, DeviceState.Disabled);
      }
    }
    /************************************************/
    public static AudioDeviceCollection NotPresentRenderDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Render, DeviceState.NotPresent);
      }
    }
    /************************************************/
    public static AudioDeviceCollection UnpluggedRenderDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Render, DeviceState.Unplugged);
      }
    }
    /************************************************/
    public static AudioDeviceCollection ActiveCaptureDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active);
      }
    }
    /************************************************/
    public static AudioDeviceCollection DisabledCaptureDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Disabled);
      }
    }
    /************************************************/
    public static AudioDeviceCollection NotPresentCaptureDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Capture, DeviceState.NotPresent);
      }
    }
    /************************************************/
    public static AudioDeviceCollection UnpluggedCaptureDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Unplugged);
      }
    }
    /************************************************/
    public static AudioDeviceCollection ActiveAllDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.All, DeviceState.Active);
      }
    }
    /************************************************/
    public static AudioDeviceCollection DisabledAllDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.All, DeviceState.Disabled);
      }
    }
    /************************************************/
    public static AudioDeviceCollection NotPresentAllDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.All, DeviceState.NotPresent);
      }
    }
    /************************************************/
    public static AudioDeviceCollection UnpluggedAllDevices
    {
      get
      {
        return Audio.EnumAudioEndpoints(DataFlow.All, DeviceState.Unplugged);
      }
    }
  }
}