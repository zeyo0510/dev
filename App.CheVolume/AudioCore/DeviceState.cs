// https://learn.microsoft.com/en-us/windows/win32/coreaudio/device-state-xxx-constants
/************************************************/
namespace AudioCore
{
  [Flags]
  public enum DeviceState
  {
    Active = JC.CS.Lib.CoreAudio.DeviceState._DEVICE_STATE_ACTIVE_,
    Disabled = JC.CS.Lib.CoreAudio.DeviceState._DEVICE_STATE_DISABLED_,
    NotPresent = JC.CS.Lib.CoreAudio.DeviceState._DEVICE_STATE_NOTPRESENT_,
    Unplugged = JC.CS.Lib.CoreAudio.DeviceState._DEVICE_STATE_UNPLUGGED_,
    All = JC.CS.Lib.CoreAudio.DeviceState._DEVICE_STATEMASK_ALL_,
  }
}