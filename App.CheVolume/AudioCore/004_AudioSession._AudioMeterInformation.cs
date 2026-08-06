using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    private IAudioMeterInformation AudioMeterInformation
    {
      get
      {
        IAudioMeterInformation retValue = (IAudioMeterInformation)this._AudioSessionControl_;
        /************************************************/
        return retValue;
      }
    }
  }
}