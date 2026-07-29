// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/nn-audioclient-iaudioclient
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioClient
  {
    [PreserveSig]
    int Initialize(
      AudioClientShareMode _SHARE_MODE_,
      AudioClientStreamFlags _STREAM_FLAGS_,
      long _HNS_BUFFER_DURATION_,
      long _HNS_PERIODICITY_,
      [In] WaveFormat _P_FORMAT_,
      [In] ref Guid _AUDIO_SESSION_GUID_
    );
    /************************************************/
    int GetBufferSize(
      out uint _P_NUM_BUFFER_FRAMES_
    );
    /************************************************/
    [return: MarshalAs(UnmanagedType.I8)]
    long GetStreamLatency();
    /************************************************/
    int GetCurrentPadding(
      out int _P_NUM_PADDING_FRAMES_
    );
    /************************************************/
    [PreserveSig]
    int IsFormatSupported(
      AudioClientShareMode _SHARE_MODE_,
      [In] WaveFormat _P_FORMAT_,
      [MarshalAs(UnmanagedType.LPStruct)] out WaveFormatExtensible _PP_CLOSEST_MATCH_
    );
    /************************************************/
    int GetMixFormat(
      out IntPtr _PP_DEVICE_FORMAT_
    );
    /************************************************/
    int GetDevicePeriod(
      out long _PHNS_DEFAULT_DEVICE_PERIOD_,
      out long _PHNS_MINIMUM_DEVICE_PERIOD_
    );
    /************************************************/
    int Start();
    /************************************************/
    int Stop();
    /************************************************/
    int Reset();
    /************************************************/
    int SetEventHandle(
      IntPtr _EVENT_HANDLE_
    );
    /************************************************/
    int GetService(
      ref Guid _RI_ID_,
      [MarshalAs(UnmanagedType.IUnknown)] out object _PPV_
    );
  }
}