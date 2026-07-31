// https://learn.microsoft.com/en-us/windows/win32/api/wtypes/ns-wtypes-propertykey
/************************************************/
namespace AudioCore.Interfaces
{
  public struct PROPERTYKEY
  {
    public Guid fmtid;

    public int pid;
  }
}