using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMMDeviceCollection
	{
		[PreserveSig]
		int GetCount(out uint P_0);

		[PreserveSig]
		int Item(uint P_0, out IMMDevice P_1);
	}
}
