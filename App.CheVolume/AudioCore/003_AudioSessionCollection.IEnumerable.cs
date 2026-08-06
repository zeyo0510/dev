using System.Collections;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionCollection: IEnumerable<AudioSession>
  {
    public IEnumerator<AudioSession> GetEnumerator()
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