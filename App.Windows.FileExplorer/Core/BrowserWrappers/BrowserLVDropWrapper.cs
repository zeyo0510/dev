using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal partial class BrowserLVDropWrapper : ShellDll.IDropTarget
  {
    private Browser br;
    
    private IntPtr listViewHandle;

    private ShellDll.IDropTarget dropTarget;
    private IntPtr dropTargetPtr;
    private IntPtr dropDataObject;

    private IDropTargetHelper dropHelper;
    private IntPtr dropHelperPtr;

    private ListViewItem dropListItem;

    private bool wasSelected;

    private WinAPI.MK mouseButtons;
    private DragDropEffects startEffects;

    public BrowserLVDropWrapper(Browser br)
    {
        this.br = br;

        listViewHandle = br.FileView.Handle;
        WinAPI.RegisterDragDrop(listViewHandle, this);

        br.FileView.HandleCreated += new EventHandler(FileView_HandleCreated);
        br.FileView.HandleDestroyed += new EventHandler(FileView_HandleDestroyed);

        ShellHelper.GetIDropTargetHelper(out dropHelperPtr, out dropHelper);
    }

    ~BrowserLVDropWrapper()
    {
        ((IDisposable)this).Dispose();
    }

    void FileView_HandleCreated(object sender, EventArgs e)
    {
        listViewHandle = br.FileView.Handle;
        WinAPI.RegisterDragDrop(listViewHandle, this);
    }

    void FileView_HandleDestroyed(object sender, EventArgs e)
    {
        WinAPI.RevokeDragDrop(listViewHandle);
    }
  }
}