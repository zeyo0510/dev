using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  internal class MMDeviceEnumerator
  {
    private readonly IMMDeviceEnumerator _MMDeviceEnumerator_ = new MMDeviceEnumerator_() as IMMDeviceEnumerator;
    /************************************************/
    public MMDeviceCollection EnumAudioEndpoints(EDataFlow dataflow, EDeviceState stateMask)
    {
      IMMDeviceCollection retValue = null;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.EnumAudioEndpoints(dataflow, stateMask, out retValue));
      /************************************************/
      return new MMDeviceCollection(retValue);
    }
    /************************************************/
    public MMDevice GetDefaultAudioEndpoint(EDataFlow dataflow, ERole role)
    {
      IMMDevice retValue = null;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.GetDefaultAudioEndpoint(dataflow, role, out retValue));
      /************************************************/
      return new MMDevice(retValue);
    }
    /************************************************/
    public MMDevice GetDevice(string id)
    {
      IMMDevice retValue = null;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._MMDeviceEnumerator_.GetDevice(id, out retValue));
      /************************************************/
      return new MMDevice(retValue);
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