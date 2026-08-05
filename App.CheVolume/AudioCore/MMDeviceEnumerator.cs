using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class MMDeviceEnumerator
  {
    private readonly IMMDeviceEnumerator obj = (IMMDeviceEnumerator)new MMDeviceEnumeratorComObject();
    /************************************************/
    public AudioDeviceCollection EnumAudioEndpoints(DataFlow dataFlow, EDeviceState state)
    {
      Marshal.ThrowExceptionForHR(obj.EnumAudioEndpoints(dataFlow, state, out IMMDeviceCollection retValue));
      /************************************************/
      return new AudioDeviceCollection(retValue);
    }
    /************************************************/
    public AudioDevice EnumerateAudioEndPoints(DataFlow dataFlow, ERole role)
    {
      Marshal.ThrowExceptionForHR(obj.GetDefaultAudioEndpoint(dataFlow, role, out IMMDevice retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public AudioDevice GetDevice(string deviceID)
    {
      Marshal.ThrowExceptionForHR(obj.GetDevice(deviceID, out IMMDevice retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public void RegisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(obj.RegisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
    /************************************************/
    public void UnregisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(obj.UnregisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
  }
}