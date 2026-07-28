using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioCaptureClient : IDisposable
  {
    private IAudioCaptureClient _AudioCaptureClient_;
		/************************************************/
    internal AudioCaptureClient(IAudioCaptureClient _AUDIO_CAPTURE_CLIENT_)
    {
      this._AudioCaptureClient_ = _AUDIO_CAPTURE_CLIENT_;
    }
		/************************************************/
    public IntPtr GetBuffer(out int P_0, out AudioClientBufferFlags P_1, out long P_2, out long P_3)
    {
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.GetBuffer(out nint retValue, out P_0, out P_1, out P_2, out P_3));
			/************************************************/
      return retValue;
    }

    public IntPtr GetBuffer(out int P_0, out AudioClientBufferFlags P_1)
    {
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.GetBuffer(out nint retValue, out P_0, out P_1, out long num, out long num2));
			/************************************************/
      return retValue;
    }

    public int GetNextPacketSize()
    {
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.GetNextPacketSize(out int retValue));
			/************************************************/
      return retValue;
    }

    public void ReleaseBuffer(int P_0)
    {
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.ReleaseBuffer(P_0));
    }

    public void Dispose()
    {
      if (_AudioCaptureClient_ != null)
      {
        Marshal.ReleaseComObject(_AudioCaptureClient_);
        _AudioCaptureClient_ = null;
        GC.SuppressFinalize(this);
      }
    }
  }
}