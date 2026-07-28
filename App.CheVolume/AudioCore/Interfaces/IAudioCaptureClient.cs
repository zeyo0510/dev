using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("C8ADBD64-E71E-48a0-A4DE-185C395CD317")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioCaptureClient
	{
		int GetBuffer(out IntPtr P_0, out int P_1, out AudioClientBufferFlags P_2, out long P_3, out long P_4);

		int ReleaseBuffer(int P_0);

		int GetNextPacketSize(out int P_0);
	}
}
