// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/ne-audioclient-_audclnt_bufferflags
/************************************************/
namespace AudioCore
{
  [Flags]
  public enum AudioClientBufferFlags
  {
    None = 0,
    DataDiscontinuity = 1,
    Silent = 2,
    TimestampError = 4
  }
}