using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public class AudioMeterInformation
  {
    private IAudioMeterInformation _AudioMeterInformation_ = null;
    /************************************************/
    internal AudioMeterInformation(IAudioMeterInformation obj)
    {
      this._AudioMeterInformation_ = obj;
    }
    /************************************************/
    private AudioMeterInformationChannels _AudioMeterInformationChannels;
    public AudioMeterInformationChannels AudioMeterInformationChannels
    {
      get
      {
        if (this._AudioMeterInformationChannels == null)
        {
          this._AudioMeterInformationChannels = new AudioMeterInformationChannels(this._AudioMeterInformation_);
        }
        /************************************************/
        return this._AudioMeterInformationChannels;
      }
    }
    /************************************************/
    public EndpointHardwareSupport HardwareSupport
    {
      get
      {
        int retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.QueryHardwareSupport(out retValue));
        /************************************************/
        return (EndpointHardwareSupport)retValue;
      }
    }
    /************************************************/
    public float PeakValue
    {
      get
      {
        float retValue;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._AudioMeterInformation_.GetPeakValue(out retValue));
        /************************************************/
        return retValue;
      }
    }
  }
}