using System;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    public int DragOver(WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect)
    {
        bool reset = false;

        Point point = browser.FolderView.PointToClient(new Point(pt.x, pt.y));
        TreeViewHitTestInfo hitTest = browser.FolderView.HitTest(point);
        if (!TreeNode.Equals(dropNode, hitTest.Node))
        {
            if (dropTarget != null)
                dropTarget.DragLeave();

            ReleaseCom();

            dropNode = hitTest.Node;
            browser.FolderView.SelectedNode = dropNode;
            
            if (dropNode == null)
            {
                pdwEffect = DragDropEffects.None;

                if (dropHelper != null)
                    dropHelper.DragOver(ref pt, pdwEffect);

                return WinAPI.S_OK;
            }
            else
            {
                ShellItem item = (ShellItem)dropNode.Tag;
                parentDropItem = item;

                ShellHelper.GetIDropTarget(item, out dropTargetPtr, out dropTarget);
                reset = true;
            }
        }
        else if (dropNode == null)
        {
            if (dropTarget != null)
                dropTarget.DragLeave();

            ReleaseCom();

            dropNode = null;
            browser.SelectedNode = null;

            pdwEffect = DragDropEffects.None;

            if (dropHelper != null)
                dropHelper.DragOver(ref pt, pdwEffect);

            return WinAPI.S_OK;
        }

        if (dropTarget != null)
        {
            if (reset)
                dropTarget.DragEnter(dropDataObject, grfKeyState, pt, ref pdwEffect);
            else
                dropTarget.DragOver(grfKeyState, pt, ref pdwEffect);
        }
        else
            pdwEffect = DragDropEffects.None;

        if (dropHelper != null)
            dropHelper.DragOver(ref pt, pdwEffect);

        return WinAPI.S_OK;
    }
  }
}