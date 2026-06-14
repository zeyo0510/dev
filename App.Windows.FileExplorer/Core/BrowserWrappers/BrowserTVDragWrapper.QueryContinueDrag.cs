using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
  {
    public int QueryContinueDrag(bool fEscapePressed, WinAPI.MK grfKeyState)
    {
      if (fEscapePressed)
        return WinAPI.DRAGDROP_S_CANCEL;
      else
      {
        if ((startButton & MouseButtons.Left) != 0 && (grfKeyState & WinAPI.MK.LBUTTON) == 0)
          return WinAPI.DRAGDROP_S_DROP;
        else if ((startButton & MouseButtons.Right) != 0 && (grfKeyState & WinAPI.MK.RBUTTON) == 0)
          return WinAPI.DRAGDROP_S_DROP;
        else
          return WinAPI.S_OK;
      }
    }
  }
}