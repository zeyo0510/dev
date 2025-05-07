// https://learn.microsoft.com/en-us/windows/win32/coreaudio/device-state-xxx-constants
/************************************************/
using System;
/************************************************/
namespace AudioCore
{
  [Flags]
  public enum DeviceState
  {
    Active     = 0x1,
    Disabled   = 0x2,
    NotPresent = 0x4,
    Unplugged  = 0x8,
    All        = 0xf
  }
}