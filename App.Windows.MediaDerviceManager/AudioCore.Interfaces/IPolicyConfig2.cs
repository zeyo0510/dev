using System.Runtime.InteropServices;

namespace AudioCore.Interfaces
{
  [ComImport]
  [Guid("F8679F50-850A-41CF-9C72-430F290290C8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IPolicyConfig2
  {
    void Reserved1();

    void Reserved2();

    void Reserved3();

    void Reserved4();

    void Reserved5();

    void Reserved6();

    void Reserved7();

    void Reserved8();

    void Reserved9();

    void Reserved10();

    [PreserveSig]
    int SetDefaultEndpoint(string P_0, ERole P_1);

    void Reserved11();
  }
}