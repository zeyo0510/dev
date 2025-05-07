using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MyAudioManager
  {
    private readonly IMMDeviceEnumerator _MMDeviceEnumerator_;
    /************************************************/
    private AudioDeviceCollection EnumAudioEndpoints(EDataFlow dataflow, EDeviceState stateMask)
    {
      IMMDeviceCollection retValue;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.EnumAudioEndpoints(dataflow, stateMask, out retValue));
      /************************************************/
      return new AudioDeviceCollection(retValue);
    }
    /************************************************/
    private AudioDevice GetDefaultAudioEndpoint(EDataFlow dataflow, ERole role)
    {
      IMMDevice retValue;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.GetDefaultAudioEndpoint(dataflow, role, out retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public AudioDevice GetDevice(string id)
    {
      IMMDevice retValue;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.GetDevice(id, out retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public void RegisterEndpointNotificationCallback(IMMNotificationClient client)
    {
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.RegisterEndpointNotificationCallback(client));
    }
    /************************************************/
    public void UnregisterEndpointNotificationCallback(IMMNotificationClient client)
    {
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.UnregisterEndpointNotificationCallback(client));
    }
  }
}