using System;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    public int DragOver(WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect)
    {
        bool reset = false;

        Point point = br.FileView.PointToClient(new Point(pt.x, pt.y));
        ListViewHitTestInfo hitTest = br.FileView.HitTest(point);
        if (hitTest.Item != null && (br.FileView.View != View.Details || hitTest.SubItem == null || hitTest.Item.Name == hitTest.SubItem.Name) && (hitTest.Location == ListViewHitTestLocations.Image || hitTest.Location == ListViewHitTestLocations.Label || hitTest.Location == ListViewHitTestLocations.StateImage))
        {                
            if (!hitTest.Item.Equals(dropListItem))
            {
                if (dropTarget != null)
                    dropTarget.DragLeave();

                ReleaseCom();

                if (dropListItem != null)
                    dropListItem.Selected = wasSelected;

                dropListItem = hitTest.Item;
                wasSelected = dropListItem.Selected;
                dropListItem.Selected = true;

                ShellItem item = (ShellItem)dropListItem.Tag;
                parentDropItem = item;

                ShellHelper.GetIDropTarget(item, out dropTargetPtr, out dropTarget);
                reset = true;
            }
        }
        else
        {
            if (dropListItem != null)
            {
                if (dropTarget != null)
                    dropTarget.DragLeave();

                ReleaseCom();

                dropListItem.Selected = wasSelected;

                dropListItem = null;
                parentDropItem = br.SelectedItem;

                ShellHelper.GetIDropTarget(br.SelectedItem, out dropTargetPtr, out dropTarget);
                reset = true;
            }
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