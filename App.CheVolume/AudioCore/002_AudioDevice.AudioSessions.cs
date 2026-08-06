namespace AudioCore
{
  partial class AudioDevice
  {
    public AudioSessionEnumerator AudioSessions
    {
      get
      {
        return this.AudioSessionManager._AudioSessionEnumerator_;
      }
    }
  }
}