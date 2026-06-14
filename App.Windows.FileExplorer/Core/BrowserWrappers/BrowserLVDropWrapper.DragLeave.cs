using System;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    public int DragLeave()
    {
        ResetDrop();
        if (dropTarget != null)
        {
            dropTarget.DragLeave();

            ReleaseCom();
            dropDataObject = IntPtr.Zero;
        }

        if (dropHelper != null)
            dropHelper.DragLeave();

        return WinAPI.S_OK;
    }
  }
}