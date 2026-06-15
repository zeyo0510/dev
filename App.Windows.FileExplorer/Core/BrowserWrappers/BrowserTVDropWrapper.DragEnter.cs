using System;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    public int DragEnter(IntPtr pDataObj, WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect)
    {
        mouseButtons = grfKeyState;

        browser.FolderView.Focus();
        browser.SelectionChange = false;
        lastSelectedNode = browser.FolderView.SelectedNode;

        ReleaseCom();

        dropDataObject = pDataObj;

        Point point = browser.FolderView.PointToClient(new Point(pt.x, pt.y));
        TreeViewHitTestInfo hitTest = browser.FolderView.HitTest(point);

        dropNode = hitTest.Node;
        browser.FolderView.SelectedNode = dropNode;

        if (dropNode != null)
        {
            ShellItem item = (ShellItem)dropNode.Tag;
            parentDropItem = item;

            if (ShellHelper.GetIDropTarget(item, out dropTargetPtr, out dropTarget))
            {
                dropTarget.DragEnter(pDataObj, grfKeyState, pt, ref pdwEffect);
            }
        }

        if (dropHelper != null) dropHelper.DragEnter(browser.Handle, pDataObj, ref pt, pdwEffect);

        return WinAPI.S_OK;
    }
  }
}