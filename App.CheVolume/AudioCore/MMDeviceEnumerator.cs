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
    public MMDeviceCollection GetDefaultAudioEndpoint(EDataFlow P_0, EDeviceState P_1)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDefaultAudioEndpoint(P_0, P_1, out IMMDeviceCollection retValue));
			/************************************************/
      return new MMDeviceCollection(retValue);
    }
    /************************************************/
    public MMDevice EnumerateAudioEndPoints(EDataFlow P_0, ERole P_1)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.EnumAudioEndpoints(P_0, P_1, out IMMDevice retValue));
			/************************************************/
      return new MMDevice(retValue);
    }
    /************************************************/
    public MMDevice GetDevice(string P_0)
    {
      Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDevice(P_0, out IMMDevice retValue));
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