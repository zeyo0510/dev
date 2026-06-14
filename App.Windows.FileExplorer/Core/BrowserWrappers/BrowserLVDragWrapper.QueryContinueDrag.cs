using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
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
