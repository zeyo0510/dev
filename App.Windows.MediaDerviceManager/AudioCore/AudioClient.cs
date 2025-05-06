using System;
using System.Runtime.InteropServices;
using System.Threading;
using AudioCore.Interfaces;

namespace AudioCore
{
  public class AudioClient : IDisposable
  {
    private IAudioClient _AudioClient_;

    private WaveFormat _MixFormat;

    private AudioRenderClient audioRenderClient1;

    private AudioCaptureClient audioCaptureClient1;

    public WaveFormat MixFormat
    {
      get
      {
        if (_MixFormat == null)
        {
          IntPtr intPtr;
          Marshal.ThrowExceptionForHR(_AudioClient_.GetMixFormat(out intPtr));
          WaveFormat waveFormat = WaveFormat.MarshalFromPtr(intPtr);
          Marshal.FreeCoTaskMem(intPtr);
          _MixFormat = waveFormat;
          return waveFormat;
        }
        return _MixFormat;
      }
    }

    public int BufferSize
    {
      get
      {
        uint result;
        Marshal.ThrowExceptionForHR(_AudioClient_.GetBufferSize(out result));
        return (int)result;
      }
    }

    public long StreamLatency
    {
      get
      {
        return _AudioClient_.GetStreamLatency();
      }
    }

    public int CurrentPadding
    {
      get
      {
        int result;
        Marshal.ThrowExceptionForHR(_AudioClient_.GetCurrentPadding(out result));
        return result;
      }
    }

    public long DefaultDevicePeriod
    {
      get
      {
        long result;
        long num;
        Marshal.ThrowExceptionForHR(_AudioClient_.GetDevicePeriod(out result, out num));
        return result;
      }
    }

    public long MinimumDevicePeriod
    {
      get
      {
        long num;
        long result;
        Marshal.ThrowExceptionForHR(_AudioClient_.GetDevicePeriod(out num, out result));
        return result;
      }
    }

    public AudioRenderClient AudioRenderClient
    {
      get
      {
        if (audioRenderClient1 == null)
        {
          Guid guid = new Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2");
          object obj;
          Marshal.ThrowExceptionForHR(_AudioClient_.GetService(ref guid, out obj));
          audioRenderClient1 = new AudioRenderClient((IAudioRenderClient)obj);
        }
        return audioRenderClient1;
      }
    }

    public AudioCaptureClient AudioCaptureClient
    {
      get
      {
        if (audioCaptureClient1 == null)
        {
          Guid guid = new Guid("c8adbd64-e71e-48a0-a4de-185c395cd317");
          object obj;
          Marshal.ThrowExceptionForHR(_AudioClient_.GetService(ref guid, out obj));
          audioCaptureClient1 = new AudioCaptureClient((IAudioCaptureClient)obj);
        }
        return audioCaptureClient1;
      }
    }

    internal AudioClient(IAudioClient P_0)
    {
      _AudioClient_ = P_0;
    }

    public void Initialize(AudioClientShareMode P_0, AudioClientStreamFlags P_1, long P_2, long P_3, WaveFormat P_4, Guid P_5)
    {
      Marshal.ThrowExceptionForHR(_AudioClient_.Initialize(P_0, P_1, P_2, P_3, P_4, ref P_5));
      _MixFormat = null;
    }

    public bool IsFormatSupported(AudioClientShareMode P_0, WaveFormat P_1)
    {
      WaveFormatExtensible waveFormatExtensible;
      return IsFormatSupported(P_0, P_1, out waveFormatExtensible);
    }

    public bool IsFormatSupported(AudioClientShareMode P_0, WaveFormat P_1, out WaveFormatExtensible P_2)
    {
      int num = _AudioClient_.IsFormatSupported(P_0, P_1, out P_2);
      switch (num)
      {
      case 0:
        return true;
      case 1:
        return false;
      case -2004287480:
        return false;
      default:
        Marshal.ThrowExceptionForHR(num);
        throw new NotSupportedException("Unknown hresult " + num);
      }
    }

    public void Start()
    {
      _AudioClient_.Start();
    }

    public void Stop()
    {
      _AudioClient_.Stop();
    }

    public void SetEventHandle(EventWaitHandle P_0)
    {
      _AudioClient_.SetEventHandle(P_0.SafeWaitHandle.DangerousGetHandle());
    }

    public void Reset()
    {
      _AudioClient_.Reset();
    }

    public void Dispose()
    {
      if (_AudioClient_ != null)
      {
        if (audioRenderClient1 != null)
        {
          audioRenderClient1.Dispose();
          audioRenderClient1 = null;
        }
        if (audioCaptureClient1 != null)
        {
          audioCaptureClient1.Dispose();
          audioCaptureClient1 = null;
        }
        Marshal.ReleaseComObject(_AudioClient_);
        _AudioClient_ = null;
        GC.SuppressFinalize(this);
      }
    }
  }
}