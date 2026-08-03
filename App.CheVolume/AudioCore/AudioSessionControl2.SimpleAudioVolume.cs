using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class AudioSessionControl2
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