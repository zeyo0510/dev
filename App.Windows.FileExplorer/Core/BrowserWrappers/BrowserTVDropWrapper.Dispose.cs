using System;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper : IDisposable
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