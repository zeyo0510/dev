using System;
using System.Runtime.InteropServices;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
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