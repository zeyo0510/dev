using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MyAudioManager
  {
    private readonly IMMDeviceEnumerator _MMDeviceEnumerator_ = new MMDeviceEnumerator_() as IMMDeviceEnumerator;
    /************************************************/
    public AudioDeviceCollection EnumAudioEndpoints(EDataFlow dataflow, EDeviceState stateMask)
    {
      IMMDeviceCollection retValue = null;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.EnumAudioEndpoints(dataflow, stateMask, out retValue));
      /************************************************/
      return new AudioDeviceCollection(retValue);
    }
    /************************************************/
    public AudioDevice GetDefaultAudioEndpoint(EDataFlow dataflow, ERole role)
    {
      IMMDevice retValue = null;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.GetDefaultAudioEndpoint(dataflow, role, out retValue));
      /************************************************/
      return new AudioDevice(retValue);
    }
    /************************************************/
    public AudioDevice GetDevice(string id)
    {
      IMMDevice retValue = null;
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