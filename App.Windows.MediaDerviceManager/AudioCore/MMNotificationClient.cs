using AudioCore.Interfaces;

namespace AudioCore
{
	public class MMNotificationClient : IMMNotificationClient
	{
		public MMNotificationClientDeviceDelegate DefaultChanged;

		public MMNotificationClientDeviceDelegate DeviceAdded;

		public MMNotificationClientDeviceDelegate DeviceRemoved;

		public MMNotificationClientPropertyValueDelegate PropertyValueChanged;

		public void OnDefaultDeviceChanged(EDataFlow P_0, ERole P_1, string P_2)
		{
			if (DefaultChanged != null)
			{
				DefaultChanged(P_2);
			}
		}

		public void OnDeviceStateChanged(string P_0, EDeviceState P_1)
		{
			switch (P_1)
			{
			case EDeviceState.Disabled:
			case EDeviceState.NotPresent:
			case EDeviceState.Unplugged:
				if (DeviceRemoved != null)
				{
					DeviceRemoved(P_0);
				}
				break;
			case EDeviceState.Active:
				if (DeviceAdded != null)
				{
					DeviceAdded(P_0);
				}
				break;
			}
		}

		public void OnDeviceAdded(string P_0)
		{
			if (DeviceAdded != null)
			{
				DeviceAdded(P_0);
			}
		}

		public void OnDeviceRemoved(string P_0)
		{
			if (DeviceRemoved != null)
			{
				DeviceRemoved(P_0);
			}
		}

		public void OnPropertyValueChanged(string P_0, PROPERTYKEY P_1)
		{
			if (PropertyValueChanged != null)
			{
				PropertyValueChanged(P_0, P_1);
			}
		}
	}
}
