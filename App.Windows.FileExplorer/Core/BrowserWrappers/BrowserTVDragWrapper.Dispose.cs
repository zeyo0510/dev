using System;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper : IDisposable
  {
    private bool disposed = false;
    /************************************************/
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