using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioMeterInformation
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

    public AudioMeterInformationChannels PeakValues
    {
      get
      {
        return this._AudioMeterInformationChannels_;
      }
    }

    public EndpointHardwareSupport HardwareSupport
    {
      get
      {
        return this._HardwareSupport;
      }
    }

    public float MasterPeakValue
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetPeakValue(out float retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}