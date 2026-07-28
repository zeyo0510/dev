using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ISimpleAudioVolume
	{
		[PreserveSig]
		int SetMasterVolume(float P_0, ref Guid P_1);

		[PreserveSig]
		int GetMasterVolume(out float P_0);

		[PreserveSig]
		int SetMute(bool P_0, Guid P_1);

		[PreserveSig]
		int GetMute(out bool P_0);
	}
}
