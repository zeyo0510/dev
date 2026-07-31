// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/nn-audioclient-iaudiorenderclient
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioRenderClient
  {
    int GetBuffer(
      int _NUM_FRAMES_REQUESTED_,
      out IntPtr _PP_DATA_
    );
    /************************************************/
    int ReleaseBuffer(
      int _NUM_FRAMES_WRITTEN_,
      AudioClientBufferFlags _DW_FLAGS_
    );
  }
}