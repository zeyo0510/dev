using System.Diagnostics;
using System.Runtime.InteropServices;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  public enum WindowShowState : int
  {
    HIDE = 0,
    SHOWNORMAL = 1,
    SHOWMINIMIZED = 2,
    SHOWMAXIMIZED = 3,
    SHOWNOACTIVATE = 4,
    SHOW = 5,
    MINIMIZE = 6,
    SHOWMINNOACTIVE = 7,
    SHOWNA = 8,
    RESTORE = 9,
    SHOWDEFAULT = 10,
    FORCEMINIMIZE = 11
  }

  [Serializable]
  internal struct WINDOWPLACEMENT
  {
    public int LENGTH;

    public int FLAGS;

    public WindowShowState SHOWCMD;

    public Point MIN_POS;

    public Point MAX_POS;

    public Rectangle NormalPosition;
  }

  [DllImport("user32.dll", EntryPoint = "ShowWindowAsync")]
  public static extern bool _SHOW_WINDOW_ASYNC_(HandleRef P_0, int P_1);

  [DllImport("user32.dll", EntryPoint = "GetWindowPlacement", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool _GET_WINDOW_PLACEMENT_(IntPtr P_0, ref WINDOWPLACEMENT P_1);

  [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
  private static extern bool _SET_FOREGROUND_WINDOW_(IntPtr P_0);

  private static WINDOWPLACEMENT BuildWindowPlacement(IntPtr P_0)
  {
    WINDOWPLACEMENT _WINDOW_PLACEMENT_ = default(WINDOWPLACEMENT);
    {
      _WINDOW_PLACEMENT_.LENGTH = Marshal.SizeOf(_WINDOW_PLACEMENT_);
    }
    /************************************************/
    _GET_WINDOW_PLACEMENT_(P_0, ref _WINDOW_PLACEMENT_);
    /************************************************/
    return _WINDOW_PLACEMENT_;
  }
  private void ShowProcess(Process p)
  {
    if (BuildWindowPlacement(p.MainWindowHandle).SHOWCMD == WindowShowState.SHOWMINIMIZED)
    {
      _SHOW_WINDOW_ASYNC_(new HandleRef(null, p.MainWindowHandle), 10);
    }
    else
    {
      _SHOW_WINDOW_ASYNC_(new HandleRef(null, p.MainWindowHandle), 5);
    }
    _SET_FOREGROUND_WINDOW_(p.MainWindowHandle);
  }
}