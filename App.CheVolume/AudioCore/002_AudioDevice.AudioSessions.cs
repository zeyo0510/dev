namespace AudioCore
{
  partial class AudioDevice
  {
    public AudioSessionCollection AudioSessions
    {
      get
      {
        return this.AudioSessionManager._AudioSessionEnumerator_;
      }
    }
  }
}