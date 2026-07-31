// https://learn.microsoft.com/en-us/windows/win32/api/audioclient/nn-audioclient-iaudiocaptureclient
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("C8ADBD64-E71E-48a0-A4DE-185C395CD317")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioCaptureClient
  {
    int GetBuffer(
      out IntPtr _PP_DATA_,
      out int _P_NUM_FRAMES_TO_READ_,
      out AudioClientBufferFlags _PDW_FLAGS_,
      out long _PU64_DEVICE_POSITION_,
      out long _PU64_QPC_POSITION_
    );
    /************************************************/
    int ReleaseBuffer(
      int _NUM_FRAMES_READ_
    );
    /************************************************/
    int GetNextPacketSize(
      out int _P_NUM_FRAMES_IN_NEXT_PACKET_
    );
  }
}