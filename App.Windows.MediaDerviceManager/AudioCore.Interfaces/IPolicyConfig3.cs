using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("00000000-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPolicyConfig3
	{
		[PreserveSig]
		int GetMixFormat(string P_0, IntPtr P_1);

		[PreserveSig]
		int GetDeviceFormat(string P_0, bool P_1, IntPtr P_2);

		[PreserveSig]
		int ResetDeviceFormat(string P_0);

		[PreserveSig]
		int SetDeviceFormat(string P_0, IntPtr P_1, IntPtr P_2);

		[PreserveSig]
		int GetProcessingPeriod(string P_0, bool P_1, IntPtr P_2, IntPtr P_3);

		[PreserveSig]
		int SetProcessingPeriod(string P_0, IntPtr P_1);

		[PreserveSig]
		int GetShareMode(string P_0, IntPtr P_1);

		[PreserveSig]
		int SetShareMode(string P_0, IntPtr P_1);

		[PreserveSig]
		int GetPropertyValue(string P_0, bool P_1, IntPtr P_2, IntPtr P_3);

		[PreserveSig]
		int SetPropertyValue(string P_0, bool P_1, IntPtr P_2, IntPtr P_3);

		[PreserveSig]
		int SetDefaultEndpoint(string P_0, ERole P_1);

		[PreserveSig]
		int SetEndpointVisibility(string P_0, bool P_1);
	}
}
