using System;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper : IDisposable
  {
    private bool disposed = false;
    
    public void Dispose()
    {
      if (!disposed)
      {
        DisposeDropWrapper();
        GC.SuppressFinalize(this);

          disposed = true;
      }
    }
  }
}