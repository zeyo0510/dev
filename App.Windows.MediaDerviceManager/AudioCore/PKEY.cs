using System;

namespace AudioCore
{
	public static class PKEY
	{
		public static readonly PropertyKey PKEY_Device_FriendlyName;

		public static readonly PropertyKey PKEY_DeviceClass_IconPath;

		static PKEY()
		{
			PropertyKey pKEY_Device_FriendlyName = default(PropertyKey);
			pKEY_Device_FriendlyName.fmtid = new Guid(2757502286u, 57116, 20221, 128, 32, 103, 209, 70, 168, 80, 224);
			pKEY_Device_FriendlyName.pid = 14;
			PKEY_Device_FriendlyName = pKEY_Device_FriendlyName;
			pKEY_Device_FriendlyName = default(PropertyKey);
			pKEY_Device_FriendlyName.fmtid = new Guid(630898684, 20647, 18382, 175, 8, 104, 201, 167, 215, 51, 102);
			pKEY_Device_FriendlyName.pid = 12;
			PKEY_DeviceClass_IconPath = pKEY_Device_FriendlyName;
		}
	}
}
