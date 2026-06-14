using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
  {
    public int GiveFeedback(DragDropEffects dwEffect)
    {
      return WinAPI.DRAGDROP_S_USEDEFAULTCURSORS;
    }
  }
}