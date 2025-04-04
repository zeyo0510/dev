using System;

namespace AudioCore
{
	[Flags]
	public enum EndpointHardwareSupport
	{
		Volume = 1,
		Mute = 2,
		Meter = 4
	}
}
