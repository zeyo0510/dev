using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    private AudioMeterInformation _AudioMeterInformation_;
    /************************************************/
    private static Guid IID_IAudioMeterInformation = new("C02216F6-8C67-4B5B-9D00-D008E73E0064");
    /************************************************/
    public AudioMeterInformation AudioMeterInformation
    {
      get
      {
        if (this._AudioMeterInformation_ == null)
        {
          Marshal.ThrowExceptionForHR(_MMDevice_.Activate(ref IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out object retValue));
          /************************************************/
          this._AudioMeterInformation_ = new(retValue as IAudioMeterInformation);
        }
        /************************************************/
        return this._AudioMeterInformation_;
      }
    }
  }
}