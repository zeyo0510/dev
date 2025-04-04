using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class MMDeviceCollection
	{
		private readonly IMMDeviceCollection _MMDeviceCollection_;

		public int Count
		{
			get
			{
				uint result;
				Marshal.ThrowExceptionForHR(_MMDeviceCollection_.GetCount(out result));
				return (int)result;
			}
		}

		public MMDevice this[int P_0]
		{
			get
			{
				IMMDevice iMMDevice;
				_MMDeviceCollection_.Item((uint)P_0, out iMMDevice);
				return new MMDevice(iMMDevice);
			}
		}

		internal MMDeviceCollection(IMMDeviceCollection P_0)
		{
			_MMDeviceCollection_ = P_0;
		}
	}
}
