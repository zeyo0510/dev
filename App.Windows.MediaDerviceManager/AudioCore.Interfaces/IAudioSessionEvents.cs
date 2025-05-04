using System;
using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionEvents
  {
    [PreserveSig]
    int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string P_0, Guid P_1);

    [PreserveSig]
    int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string P_0, Guid P_1);

    [PreserveSig]
    int OnSimpleVolumeChanged(float P_0, bool P_1, Guid P_2);

    [PreserveSig]
    int OnChannelVolumeChanged(uint P_0, IntPtr P_1, uint P_2, Guid P_3);

    [PreserveSig]
    int OnGroupingParamChanged(Guid P_0, Guid P_1);

    [PreserveSig]
    int OnStateChanged(AudioSessionState P_0);

    [PreserveSig]
    int OnSessionDisconnected(AudioSessionDisconnectReason P_0);
  }
}