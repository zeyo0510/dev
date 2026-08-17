using AudioCore.Interfaces;

namespace AudioCore
{
  public class MMNotificationClient : IMMNotificationClient
  {
    public MMNotificationClientDeviceDelegate? DefaultChanged = null;

    public MMNotificationClientDeviceDelegate? DeviceAdded = null;

    public MMNotificationClientDeviceDelegate? DeviceRemoved = null;

    public MMNotificationClientPropertyValueDelegate? PropertyValueChanged = null;

    public void OnDefaultDeviceChanged(DataFlow dataFlow, ERole role, string P_2)
    {
      DefaultChanged?.Invoke(P_2);
    }

    public void OnDeviceStateChanged(string deviceID, DeviceState newState)
    {
      switch (newState)
      {
        case DeviceState.Disabled:
        case DeviceState.NotPresent:
        case DeviceState.Unplugged:
            DeviceRemoved?.Invoke(deviceID);
            break;
        case DeviceState.Active:
            DeviceAdded?.Invoke(deviceID);
            break;
      }
    }

    public void OnDeviceAdded(string deviceID)
    {
      DeviceAdded?.Invoke(deviceID);
    }

    public void OnDeviceRemoved(string deviceID)
    {
      DeviceRemoved?.Invoke(deviceID);
    }

    public void OnPropertyValueChanged(string deviceID, PROPERTYKEY key)
    {
      PropertyValueChanged?.Invoke(deviceID, key);
    }
  }
}