using System;
using ShellDll;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  internal partial class BrowserLVDragWrapper : IDropSource
  {
    private Browser br;

    private IntPtr dataObjectPtr;

    private MouseButtons startButton;

    public BrowserLVDragWrapper(Browser br)
    {
      this.br = br;
      br.FileView.ItemDrag += new ItemDragEventHandler(ItemDrag);
    }
    /************************************************/
    ~BrowserLVDragWrapper()
    {
      ((IDisposable)this).Dispose();
    }
  }
}