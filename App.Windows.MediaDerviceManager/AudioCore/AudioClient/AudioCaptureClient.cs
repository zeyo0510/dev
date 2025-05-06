using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioCaptureClient : IDisposable
  {
    private IAudioCaptureClient _AudioCaptureClient_ = null;
    /************************************************/
    internal AudioCaptureClient(IAudioCaptureClient obj)
    {
      _AudioCaptureClient_ = obj;
    }
    /************************************************/
    public IntPtr GetBuffer(out int P_0, out AudioClientBufferFlags P_1, out long P_2, out long P_3)
    {
      IntPtr result;
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.GetBuffer(out result, out P_0, out P_1, out P_2, out P_3));
      return result;
    }
    /************************************************/
    public IntPtr GetBuffer(out int P_0, out AudioClientBufferFlags P_1)
    {
      IntPtr result;
      long num;
      long num2;
      Marshal.ThrowExceptionForHR(_AudioCaptureClient_.GetBuffer(out result, out P_0, out P_1, out num, out num2));
      return result;
    }
    /************************************************/
    public int GetNextPacketSize()
    {
      int retValue;
      /************************************************/
      Marshal.ThrowExceptionForHR(this._AudioCaptureClient_.GetNextPacketSize(out retValue));
      /************************************************/
      return retValue;
    }
    /************************************************/
    public void ReleaseBuffer(int P_0)
    {
      Marshal.ThrowExceptionForHR(this._AudioCaptureClient_.ReleaseBuffer(P_0));
    }
    /************************************************/
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