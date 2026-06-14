using System;
using System.Runtime.InteropServices;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    private void ReleaseCom()
    {
      if (dataObjectPtr != IntPtr.Zero)
      {
        Marshal.Release(dataObjectPtr);
        dataObjectPtr = IntPtr.Zero;
      }
    }
  }
}