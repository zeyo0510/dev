// https://github.com/naudio/NAudio/blob/main/src/NAudio.Wasapi/CoreAudioApi/PropertyKey.cs
/************************************************/
namespace AudioCore
{
  public struct PropertyKey
  {
    public Guid fmtid;

    public int pid;

    public PropertyKey(Guid formatId, int propertyId)
    {
      fmtid = formatId;
      pid = propertyId;
    }
  }
}