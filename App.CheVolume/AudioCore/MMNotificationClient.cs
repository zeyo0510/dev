using AudioCore.Interfaces;

namespace AudioCore
{
  public class MMNotificationClient : IMMNotificationClient
  {
    public MMNotificationClientDeviceDelegate DefaultChanged = null;

    public MMNotificationClientDeviceDelegate DeviceAdded;

    public MMNotificationClientDeviceDelegate DeviceRemoved;

    public MMNotificationClientPropertyValueDelegate PropertyValueChanged;

    public void OnDefaultDeviceChanged(EDataFlow dataFlow, ERole role, string P_2)
    {
      DefaultChanged?.Invoke(P_2);
    }

    public void OnDeviceStateChanged(string P_0, EDeviceState P_1)
    {
      switch (P_1)
      {
        case EDeviceState.Disabled:
        case EDeviceState.NotPresent:
        case EDeviceState.Unplugged:
            DeviceRemoved?.Invoke(P_0);
            break;
        case EDeviceState.Active:
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