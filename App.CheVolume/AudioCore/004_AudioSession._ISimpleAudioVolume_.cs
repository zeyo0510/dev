using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    private ISimpleAudioVolume ISimpleAudioVolume
    {
      get
      {
        ISimpleAudioVolume retValue = (ISimpleAudioVolume)this.IAudioSessionControl;
        /************************************************/
        return retValue;
      }
    }
  }
}