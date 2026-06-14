using System;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    public int DragEnter(IntPtr pDataObj, WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect)
    {
        mouseButtons = grfKeyState;
        startEffects = pdwEffect;

        br.FileView.Focus();
        br.SelectionChange = false;
        ReleaseCom();

        dropDataObject = pDataObj;

        Point point = br.FileView.PointToClient(new Point(pt.x, pt.y));
        ListViewHitTestInfo hitTest = br.FileView.HitTest(point);
        if (hitTest.Item != null && (br.FileView.View != View.Details || hitTest.SubItem == null || hitTest.Item.Name == hitTest.SubItem.Name) && (hitTest.Location == ListViewHitTestLocations.Image || hitTest.Location == ListViewHitTestLocations.Label || hitTest.Location == ListViewHitTestLocations.StateImage))
        {
            dropListItem = hitTest.Item;

            wasSelected = dropListItem.Selected;
            dropListItem.Selected = true;

            ShellItem item = (ShellItem)dropListItem.Tag;
            parentDropItem = item;

            ShellHelper.GetIDropTarget(item, out dropTargetPtr, out dropTarget);
        }
        else
        {
            dropListItem = null;
            parentDropItem = br.SelectedItem;
            ShellHelper.GetIDropTarget(br.SelectedItem, out dropTargetPtr, out dropTarget);
        }

        if (dropTarget != null)
            dropTarget.DragEnter(pDataObj, grfKeyState, pt, ref pdwEffect);

        if (dropHelper != null)
            dropHelper.DragEnter(br.Handle, pDataObj, ref pt, pdwEffect);

        return WinAPI.S_OK;
    }
  }
}