using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    public int GiveFeedback(DragDropEffects dwEffect)
    {
      return WinAPI.DRAGDROP_S_USEDEFAULTCURSORS;
    }
  }
}