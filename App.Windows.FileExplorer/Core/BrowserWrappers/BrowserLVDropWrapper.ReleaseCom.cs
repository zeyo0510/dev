using System;
using System.Runtime.InteropServices;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    private void ReleaseCom()
    {
      if (dropTarget != null)
      {
        Marshal.ReleaseComObject(dropTarget);

        dropTarget = null;
        dropHelperPtr = IntPtr.Zero;
      }
    }
  }
}