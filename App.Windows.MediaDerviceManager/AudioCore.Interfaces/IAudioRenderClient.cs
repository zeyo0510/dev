using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioRenderClient
	{
		int GetBuffer(int P_0, out IntPtr P_1);

		int ReleaseBuffer(int P_0, AudioClientBufferFlags P_1);
	}
}
