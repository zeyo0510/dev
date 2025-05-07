using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioRenderClient : IDisposable
  {
    private IAudioRenderClient _AudioRenderClient_;
    /************************************************/
    internal AudioRenderClient(IAudioRenderClient obj)
    {
      this._AudioRenderClient_ = obj;
    }
    /************************************************/
    public IntPtr GetBuffer(int P_0)
    {
      IntPtr result;
      Marshal.ThrowExceptionForHR(this._AudioRenderClient_.GetBuffer(P_0, out result));
      return result;
    }
    /************************************************/
    public void ReleaseBuffer(int P_0, AudioClientBufferFlags P_1)
    {
      Marshal.ThrowExceptionForHR(this._AudioRenderClient_.ReleaseBuffer(P_0, P_1));
    }
    /************************************************/
    public void Dispose()
    {
      if (this._AudioRenderClient_ != null)
      {
        Marshal.ReleaseComObject(this._AudioRenderClient_);
        this._AudioRenderClient_ = null;
        GC.SuppressFinalize(this);
      }
    }
  }
}