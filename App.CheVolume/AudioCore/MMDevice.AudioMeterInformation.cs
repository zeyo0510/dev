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