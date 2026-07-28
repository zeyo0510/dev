using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public partial class AudioMeterInformation
  {
    private IAudioMeterInformation _AudioMeterInformation_;

    private EndpointHardwareSupport _HardwareSupport;

    private AudioMeterInformationChannels _AudioMeterInformationChannels_;

    internal AudioMeterInformation(IAudioMeterInformation _AUDIO_METER_INFORMATION_)
    {
      this._AudioMeterInformation_ = _AUDIO_METER_INFORMATION_;
      /************************************************/
      Marshal.ThrowExceptionForHR(_AudioMeterInformation_.QueryHardwareSupport(out int hardwareSupport));
      _HardwareSupport = (EndpointHardwareSupport)hardwareSupport;
      /************************************************/
      this._AudioMeterInformationChannels_ = new AudioMeterInformationChannels(_AudioMeterInformation_);
    }

    public EndpointHardwareSupport HardwareSupport
    {
      get
      {
        return this._HardwareSupport;
      }
    }
  }
}