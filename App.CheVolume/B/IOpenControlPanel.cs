using System.Runtime.InteropServices;

namespace B
{
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D11AD862-66DE-4DF4-BF6C-1F5621996AF1")]
	public interface IOpenControlPanel
	{
		[PreserveSig]
		int A(string P_0, string P_1, object P_2);
	}
}
