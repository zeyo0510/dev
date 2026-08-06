using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioSessionCollection
  {
    private IAudioSessionEnumerator IAudioSessionEnumerator
    {
      get; set;
    }
  }
}