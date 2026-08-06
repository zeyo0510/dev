using System.Collections;
/************************************************/
namespace AudioCore
{
  partial class AudioDeviceCollection : IEnumerable<AudioDevice>
  {
    /************************************************/
    public IEnumerator<AudioDevice> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }
    /************************************************/
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}