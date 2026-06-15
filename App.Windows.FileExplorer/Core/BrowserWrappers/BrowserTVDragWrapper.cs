using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper : IDropSource
  {
    private Browser browser;

    private IntPtr dataObjectPtr;

    private MouseButtons startButton;

    public BrowserTVDragWrapper(Browser browser)
    {
      this.browser = browser;
      browser.FolderView.ItemDrag += new ItemDragEventHandler(ItemDrag);
    }

    ~BrowserTVDragWrapper()
    {
      ((IDisposable)this).Dispose();
    }
  }
}