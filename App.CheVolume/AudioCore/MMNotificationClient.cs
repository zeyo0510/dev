using AudioCore.Interfaces;

namespace AudioCore
{
  public class MMNotificationClient : IMMNotificationClient
  {
    public MMNotificationClientDeviceDelegate DefaultChanged = null;

    public MMNotificationClientDeviceDelegate DeviceAdded;

    public MMNotificationClientDeviceDelegate DeviceRemoved;

    public MMNotificationClientPropertyValueDelegate PropertyValueChanged;

    public void OnDefaultDeviceChanged(DataFlow dataFlow, ERole role, string P_2)
    {
      DefaultChanged?.Invoke(P_2);
    }

    public void OnDeviceStateChanged(string P_0, DeviceState state)
    {
      switch (state)
      {
        case DeviceState.Disabled:
        case DeviceState.NotPresent:
        case DeviceState.Unplugged:
            DeviceRemoved?.Invoke(P_0);
            break;
        case DeviceState.Active:
            DeviceAdded?.Invoke(P_0);
            break;
      }
    }

    public void OnDeviceAdded(string P_0)
    {
      DeviceAdded?.Invoke(P_0);
    }

    public void OnDeviceRemoved(string P_0)
    {
      DeviceRemoved?.Invoke(P_0);
    }

    public void OnPropertyValueChanged(string P_0, PROPERTYKEY P_1)
    {
      PropertyValueChanged?.Invoke(P_0, P_1);
    }
  }
}