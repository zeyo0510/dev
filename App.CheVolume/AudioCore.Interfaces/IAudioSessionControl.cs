using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
	[Guid("f4b1a599-7266-4319-a8ca-e70acb11e8cd")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAudioSessionControl
	{
		[PreserveSig]
		int GetState(out AudioSessionState P_0);

		[PreserveSig]
		int GetDisplayName(out IntPtr P_0);

		[PreserveSig]
		int SetDisplayName(string P_0, Guid P_1);

		[PreserveSig]
		int GetIconPath(out IntPtr P_0);

		[PreserveSig]
		int SetIconPath(string P_0, Guid P_1);

		[PreserveSig]
		int GetGroupingParam(out Guid P_0);

		[PreserveSig]
		int SetGroupingParam(Guid P_0, Guid P_1);

		[PreserveSig]
		int RegisterAudioSessionNotification(IAudioSessionEvents P_0);

		[PreserveSig]
		int UnregisterAudioSessionNotification(IAudioSessionEvents P_0);
	}
}
