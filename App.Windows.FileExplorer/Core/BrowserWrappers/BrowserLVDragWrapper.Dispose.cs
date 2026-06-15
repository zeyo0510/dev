using System;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper : IDisposable
  {
    private bool disposed = false;
    
    public void Dispose()
    {
      if (!this.disposed)
      {
        ReleaseCom();
        GC.SuppressFinalize(this);

        this.disposed = true;
      }
    }
  }
}