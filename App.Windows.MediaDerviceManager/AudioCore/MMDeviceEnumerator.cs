using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	internal class MMDeviceEnumerator
	{
		private readonly IMMDeviceEnumerator _MMDeviceEnumerator_ = new MMDeviceEnumerator_() as IMMDeviceEnumerator;

		public MMDeviceCollection GetDefaultAudioEndpoint(EDataFlow P_0, EDeviceState P_1)
		{
			IMMDeviceCollection iMMDeviceCollection;
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDefaultAudioEndpoint(P_0, P_1, out iMMDeviceCollection));
			return new MMDeviceCollection(iMMDeviceCollection);
		}

		public MMDevice EnumerateAudioEndPoints(EDataFlow P_0, ERole P_1)
		{
			IMMDevice iMMDevice;
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.EnumAudioEndpoints(P_0, P_1, out iMMDevice));
			return new MMDevice(iMMDevice);
		}

		public MMDevice GetDevice(string P_0)
		{
			IMMDevice iMMDevice;
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDevice(P_0, out iMMDevice));
			return new MMDevice(iMMDevice);
		}

		public void RegisterEndpointNotificationCallback(IMMNotificationClient P_0)
		{
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.RegisterEndpointNotificationCallback(P_0));
		}

		public void UnregisterEndpointNotificationCallback(IMMNotificationClient P_0)
		{
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.UnregisterEndpointNotificationCallback(P_0));
		}

		public MMDevice GetDeviceV2(string P_0)
		{
			IMMDevice iMMDevice;
			Marshal.ThrowExceptionForHR(_MMDeviceEnumerator_.GetDevice(P_0, out iMMDevice));
			return new MMDevice(iMMDevice);
		}
	}
}
