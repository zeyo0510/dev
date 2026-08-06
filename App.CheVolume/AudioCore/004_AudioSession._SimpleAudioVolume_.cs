using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    private ISimpleAudioVolume SimpleAudioVolume
    {
      get
      {
        ISimpleAudioVolume retValue = (ISimpleAudioVolume)this._AudioSessionControl_;
        /************************************************/
        return retValue;
      }
    }
  }
}