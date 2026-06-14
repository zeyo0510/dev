using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper : IDropSource
  {
    private Browser br;

    private IntPtr dataObjectPtr;

    private MouseButtons startButton;

    private bool disposed = false;

    public BrowserTVDragWrapper(Browser br)
    {
      this.br = br;
      br.FolderView.ItemDrag += new ItemDragEventHandler(ItemDrag);
    }

    ~BrowserTVDragWrapper()
    {
      ((IDisposable)this).Dispose();
    }
  }
}