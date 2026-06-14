using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal partial class BrowserTVDropWrapper : ShellDll.IDropTarget
  {
    private Browser br;

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


    public event DropEventHandler Drop;

    public BrowserTVDropWrapper(Browser br)
    {
        this.br = br;

        treeViewHandle = br.FolderView.Handle;
        WinAPI.RegisterDragDrop(treeViewHandle, this);

        br.FolderView.HandleCreated += new EventHandler(FolderView_HandleCreated);
        br.FolderView.HandleDestroyed += new EventHandler(FolderView_HandleDestroyed);

        ShellHelper.GetIDropTargetHelper(out dropHelperPtr, out dropHelper);
    }

    ~BrowserTVDropWrapper()
    {
        ((IDisposable)this).Dispose();
    }

    void FolderView_HandleCreated(object sender, EventArgs e)
    {
        treeViewHandle = br.FolderView.Handle;
        WinAPI.RegisterDragDrop(treeViewHandle, this);
    }

    void FolderView_HandleDestroyed(object sender, EventArgs e)
    {
        WinAPI.RevokeDragDrop(br.FolderView.Handle);
    }
  }
}