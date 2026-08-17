using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  public class AudioClient : IDisposable
  {
    private IAudioClient _AudioClient_;

    private WaveFormat _MixFormat;

    private AudioRenderClient _AudioRenderClient_;

    private AudioCaptureClient _AudioCaptureClient_;

    internal AudioClient(IAudioClient _AUDIO_CLIENT_)
    {
      _AudioClient_ = _AUDIO_CLIENT_;
    }

    public WaveFormat MixFormat
    {
      get
      {
        if (_MixFormat == null)
        {
          Marshal.ThrowExceptionForHR(_AudioClient_.GetMixFormat(out nint intPtr));
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
        Marshal.ThrowExceptionForHR(_AudioClient_.GetBufferSize(out uint retValue));
        /************************************************/
        return (int)retValue;
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
        Marshal.ThrowExceptionForHR(_AudioClient_.GetCurrentPadding(out int retValue));
        /************************************************/
        return retValue;
      }
    }

    public long DefaultDevicePeriod
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioClient_.GetDevicePeriod(out long retValue, out long num));
        /************************************************/
        return retValue;
      }
    }

    public long MinimumDevicePeriod
    {
      get
      {
        Marshal.ThrowExceptionForHR(_AudioClient_.GetDevicePeriod(out long num, out long retValue));
        /************************************************/
        return retValue;
      }
    }

    public AudioRenderClient AudioRenderClient
    {
      get
      {
        if (this._AudioRenderClient_ == null)
        {
          Guid guid = new("F294ACFC-3146-4483-A7BF-ADDCA7C260E2");
          /************************************************/
          Marshal.ThrowExceptionForHR(_AudioClient_.GetService(ref guid, out object retValue));
          /************************************************/
          this._AudioRenderClient_ = new AudioRenderClient((IAudioRenderClient)retValue);
        }
        /************************************************/
        return this._AudioRenderClient_;
      }
    }

    public AudioCaptureClient AudioCaptureClient
    {
      get
      {
        if (this._AudioCaptureClient_ == null)
        {
          Guid guid = new("c8adbd64-e71e-48a0-a4de-185c395cd317");
          /************************************************/
          Marshal.ThrowExceptionForHR(_AudioClient_.GetService(ref guid, out object retValue));
          /************************************************/
          this._AudioCaptureClient_ = new AudioCaptureClient((IAudioCaptureClient)retValue);
        }
        /************************************************/
        return this._AudioCaptureClient_;
      }
    }

    public void Initialize(AudioClientShareMode P_0, AudioClientStreamFlags P_1, long P_2, long P_3, WaveFormat P_4, Guid P_5)
    {
      Marshal.ThrowExceptionForHR(_AudioClient_.Initialize(P_0, P_1, P_2, P_3, P_4, ref P_5));
      /************************************************/
      _MixFormat = null;
    }

    public bool IsFormatSupported(AudioClientShareMode shareMode, WaveFormat format)
    {
      return IsFormatSupported(shareMode, format, out WaveFormatExtensible waveFormatExtensible);
    }

    public bool IsFormatSupported(AudioClientShareMode shareMode, WaveFormat format, out WaveFormatExtensible extensible)
    {
      int num = _AudioClient_.IsFormatSupported(shareMode, format, out extensible);
      /************************************************/
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
        if (_AudioRenderClient_ != null)
        {
          _AudioRenderClient_.Dispose();
          _AudioRenderClient_ = null;
        }
        if (_AudioCaptureClient_ != null)
        {
          _AudioCaptureClient_.Dispose();
          _AudioCaptureClient_ = null;
        }
        Marshal.ReleaseComObject(_AudioClient_);
        _AudioClient_ = null;
        GC.SuppressFinalize(this);
      }
    }
  }
}