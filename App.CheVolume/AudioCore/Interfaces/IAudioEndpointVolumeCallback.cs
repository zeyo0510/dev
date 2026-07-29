// https://learn.microsoft.com/en-us/windows/win32/api/endpointvolume/nn-endpointvolume-iaudioendpointvolumecallback
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore.Interfaces
{
  [Guid("657804FA-D6AD-4496-8A60-352752AF4F89")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioEndpointVolumeCallback
  {
    [PreserveSig]
    int OnNotify(
      IntPtr _P_NOTIFY_
    );
  }
}