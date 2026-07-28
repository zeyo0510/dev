namespace AudioCore
{
  [Flags]
  public enum EDeviceState
  {
    Active = 1,
    Disabled = 2,
    NotPresent = 4,
    Unplugged = 8,
    All = 0xF
  }
}