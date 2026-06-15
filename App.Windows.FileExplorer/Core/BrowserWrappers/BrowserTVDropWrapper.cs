using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal partial class BrowserTVDropWrapper : ShellDll.IDropTarget
  {
    private Browser browser;

    private IntPtr treeViewHandle;

    private ShellDll.IDropTarget dropTarget;
    private IntPtr dropTargetPtr;
    private IntPtr dropDataObject;

    private IDropTargetHelper dropHelper;
    private IntPtr dropHelperPtr;

    private TreeNode dropNode;

    private TreeNode lastSelectedNode;

    private ShellItem parentDropItem, parentDragItem;

    private WinAPI.MK mouseButtons;

    public BrowserTVDropWrapper(Browser browser)
    {
        this.browser = browser;

        treeViewHandle = browser.FolderView.Handle;
        WinAPI.RegisterDragDrop(treeViewHandle, this);

        browser.FolderView.HandleCreated += new EventHandler(FolderView_HandleCreated);
        browser.FolderView.HandleDestroyed += new EventHandler(FolderView_HandleDestroyed);

        ShellHelper.GetIDropTargetHelper(out dropHelperPtr, out dropHelper);
    }

    ~BrowserTVDropWrapper()
    {
        ((IDisposable)this).Dispose();
    }

    void FolderView_HandleCreated(object sender, EventArgs e)
    {
        treeViewHandle = browser.FolderView.Handle;
        WinAPI.RegisterDragDrop(treeViewHandle, this);
    }

    void FolderView_HandleDestroyed(object sender, EventArgs e)
    {
        WinAPI.RevokeDragDrop(browser.FolderView.Handle);
    }
  }
}