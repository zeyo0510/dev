using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioRenderClient : IDisposable
  {
    private IAudioRenderClient _AudioRenderClient_;
    /************************************************/
    internal AudioRenderClient(IAudioRenderClient _)
    {
      this._AudioRenderClient_ = _;
    }
    /************************************************/
    public IntPtr GetBuffer(int value)
    {
      Marshal.ThrowExceptionForHR(_AudioRenderClient_.GetBuffer(value, out nint retValue));
      return retValue;
    }

    public void ReleaseBuffer(int P_0, AudioClientBufferFlags flags)
    {
      Marshal.ThrowExceptionForHR(_AudioRenderClient_.ReleaseBuffer(P_0, flags));
    }

    public void Dispose()
    {
      if (_AudioRenderClient_ != null)
      {
        Marshal.ReleaseComObject(_AudioRenderClient_);
        _AudioRenderClient_ = null;
        GC.SuppressFinalize(this);
      }
    }
  }
}