using System;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper : IDisposable
  {
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