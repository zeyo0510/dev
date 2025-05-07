using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioSessionManager2
	{
		[PreserveSig]
		int GetAudioSessionControl(ref Guid P_0, uint P_1, IntPtr P_2);

		[PreserveSig]
		int GetSimpleAudioVolume(ref Guid P_0, uint P_1, out ISimpleAudioVolume P_2);

		[PreserveSig]
		int GetSessionEnumerator(out IAudioSessionEnumerator P_0);

		[PreserveSig]
		int RegisterSessionNotification(IAudioSessionNotification P_0);

		[PreserveSig]
		int UnregisterSessionNotification(IAudioSessionNotification P_0);

		[PreserveSig]
		int RegisterDuckNotification(string P_0, IntPtr P_1);

		[PreserveSig]
		int UnregisterDuckNotification(IntPtr P_0);
	}
}
