// https://learn.microsoft.com/en-us/windows/win32/coreaudio/endpoint-hardware-support-xxx-constants
/************************************************/
namespace AudioCore
{
  public class EndpointHardwareSupport
  {
    public const int _ENDPOINT_HARDWARE_SUPPORT_VOLUME_= 0x00000001;
    public const int _ENDPOINT_HARDWARE_SUPPORT_MUTE_  = 0x00000002;
    public const int _ENDPOINT_HARDWARE_SUPPORT_METER_ = 0x00000004;
  }
}