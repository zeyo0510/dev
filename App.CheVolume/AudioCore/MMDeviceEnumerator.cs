using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class MMDeviceEnumerator
  {
    private readonly IMMDeviceEnumerator _MMDeviceEnumerator_ = new MMDeviceEnumerator_() as IMMDeviceEnumerator;
    /************************************************/
    public MMDeviceCollection EnumAudioEndpoints(EDataFlow dataFlow, EDeviceState state)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.EnumAudioEndpoints(dataFlow, state, out IMMDeviceCollection retValue));
			/************************************************/
      return new MMDeviceCollection(retValue);
    }
    /************************************************/
    public MMDevice EnumerateAudioEndPoints(EDataFlow dataFlow, ERole role)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDefaultAudioEndpoint(dataFlow, role, out IMMDevice retValue));
			/************************************************/
      return new MMDevice(retValue);
    }
    /************************************************/
    public MMDevice GetDevice(string deviceID)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDevice(deviceID, out IMMDevice retValue));
			/************************************************/
      return new MMDevice(retValue);
    }
    /************************************************/
    public void RegisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.RegisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
    /************************************************/
    public void UnregisterEndpointNotificationCallback(IMMNotificationClient _MM_NOTIFICATION_CLIENT_)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.UnregisterEndpointNotificationCallback(_MM_NOTIFICATION_CLIENT_));
    }
  }
}