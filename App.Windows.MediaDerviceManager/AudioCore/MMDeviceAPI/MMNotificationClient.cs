using System;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class MMNotificationClient : IMMNotificationClient
  {
    public MMNotificationClientDeviceDelegate        DeviceAdded          = null;
    public MMNotificationClientDeviceDelegate        DeviceRemoved        = null;
    public MMNotificationClientDeviceDelegate        DefaultChanged       = null;
    public MMNotificationClientPropertyValueDelegate PropertyValueChanged = null;
    /************************************************/
    public void OnDeviceStateChanged(string deviceID, EDeviceState newState)
    {
      switch (newState)
      {
        case EDeviceState.Disabled:
        case EDeviceState.NotPresent:
        case EDeviceState.Unplugged:
          if (this.DeviceRemoved != null)
          {
            this.DeviceRemoved(deviceID);
          }
          break;
        case EDeviceState.Active:
          if (this.DeviceAdded != null)
          {
            this.DeviceAdded(deviceID);
          }
          break;
      }
    }
    /************************************************/
    public void OnDeviceAdded(string deviceID)
    {
      if (this.DeviceAdded != null)
      {
        this.DeviceAdded(deviceID);
      }
    }
    /************************************************/
    public void OnDeviceRemoved(string deviceID)
    {
      if (this.DeviceRemoved != null)
      {
        this.DeviceRemoved(deviceID);
      }
    }
    /************************************************/
    public void OnDefaultDeviceChanged(EDataFlow flow, ERole role, string defaultDeviceID)
    {
      if (this.DefaultChanged != null)
      {
        this.DefaultChanged(defaultDeviceID);
      }
    }
    /************************************************/
    public void OnPropertyValueChanged(string deviceID, PROPERTYKEY key)
    {
      if (this.PropertyValueChanged != null)
      {
        this.PropertyValueChanged(deviceID, key);
      }
    }
  }
}