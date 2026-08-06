using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioSession
  {
    private IAudioMeterInformation IAudioMeterInformation
    {
      get
      {
        IAudioMeterInformation retValue = (IAudioMeterInformation)this.IAudioSessionControl;
        /************************************************/
        return retValue;
      }
    }
  }
}