namespace AudioCore
{
	public enum AudioSessionDisconnectReason
	{
		DisconnectReasonDeviceRemoval,
		DisconnectReasonServerShutdown,
		DisconnectReasonFormatChanged,
		DisconnectReasonSessionLogoff,
		DisconnectReasonSessionDisconnected,
		DisconnectReasonExclusiveModeOverride
	}
}
