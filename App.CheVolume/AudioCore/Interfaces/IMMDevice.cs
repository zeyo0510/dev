using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IMMDevice
	{
		int Activate(ref Guid P_0, CLSCTX P_1, IntPtr P_2, [MarshalAs(UnmanagedType.IUnknown)] out object P_3);

		[PreserveSig]
		int OpenPropertyStore(EStgmAccess P_0, out IPropertyStore P_1);

		int GetId([MarshalAs(UnmanagedType.LPWStr)] out string P_0);

		int GetState(out DeviceState P_0);
	}
}
