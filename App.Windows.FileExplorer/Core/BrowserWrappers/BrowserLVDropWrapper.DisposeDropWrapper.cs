using System;
using System.Runtime.InteropServices;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    private void DisposeDropWrapper()
    {
      ReleaseCom();

      if (dropHelper != null)
      {
        Marshal.ReleaseComObject(dropHelper);
      }
    }
  }
}