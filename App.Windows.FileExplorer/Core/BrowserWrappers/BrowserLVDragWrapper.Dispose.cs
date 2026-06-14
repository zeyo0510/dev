using System;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper : IDisposable
  {
    private bool disposed = false;
    
    public void Dispose()
    {
      if (!disposed)
      {
        ReleaseCom();
        GC.SuppressFinalize(this);

        disposed = true;
      }
    }
  }
}