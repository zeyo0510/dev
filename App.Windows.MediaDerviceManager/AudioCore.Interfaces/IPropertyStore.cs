using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IPropertyStore
	{
		int GetCount(out int P_0);

		int GetAt(int P_0, out PropertyKey P_1);

		int GetValue(ref PropertyKey P_0, out PropVariant P_1);

		int SetValue(ref PropertyKey P_0, ref PropVariant P_1);

		int Commit();
	}
}
