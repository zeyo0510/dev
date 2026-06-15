using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVContextMenuWrapper
  {
    private void CreateNormalMenu(Point ptInvoke, ListViewHitTestInfo hitTest, MouseEventArgs e)
    {
        hitTest.Item.Selected = true;

        ShellItem item = (ShellItem)hitTest.Item.Tag;

        int offset = browser.FileView.SelectedOrder.Contains(hitTest.Item) ? 0 : 1;
        IntPtr[] pidls = new IntPtr[browser.FileView.SelectedOrder.Count + offset];

        if (offset == 1)
            pidls[0] = item.PIDLRel.Ptr;

        for (int i = offset; i < pidls.Length; i++)
        {
            pidls[i] = ((ShellItem)((ListViewItem)browser.FileView.SelectedOrder[i - offset]).Tag).PIDLRel.Ptr;
        }

        
        IntPtr contextMenu = IntPtr.Zero,
            iContextMenuPtr = IntPtr.Zero,
            iContextMenuPtr2 = IntPtr.Zero,
            iContextMenuPtr3 = IntPtr.Zero;
        IShellFolder parentShellFolder =
            (item.ParentItem != null) ? item.ParentItem.ShellFolder : item.ShellFolder;

        try
        {
            if (ContextMenuHelper.GetIContextMenu(parentShellFolder, pidls, out iContextMenuPtr, out iContextMenu))
            {
                contextMenu = WinAPI.CreatePopupMenu();
                iContextMenu.QueryContextMenu(
                    contextMenu,
                    0,
                    WinAPI.CMD_FIRST,
                    WinAPI.CMD_LAST,
                    WinAPI.CMF.EXPLORE |
                    WinAPI.CMF.CANRENAME |
                    ((Control.ModifierKeys & Keys.Shift) != 0 ? WinAPI.CMF.EXTENDEDVERBS : 0));

                Marshal.QueryInterface(iContextMenuPtr, ref WinAPI.IID_IContextMenu2, out iContextMenuPtr2);
                Marshal.QueryInterface(iContextMenuPtr, ref WinAPI.IID_IContextMenu3, out iContextMenuPtr3);

                try
                {
                    iContextMenu2 =
                        (IContextMenu2)Marshal.GetTypedObjectForIUnknown(iContextMenuPtr2, typeof(IContextMenu2));

                    iContextMenu3 =
                        (IContextMenu3)Marshal.GetTypedObjectForIUnknown(iContextMenuPtr3, typeof(IContextMenu3));
                }
                catch (Exception) { }

                uint selected = WinAPI.TrackPopupMenuEx(
                                    contextMenu,
                                    WinAPI.TPM.RETURNCMD,
                                    ptInvoke.X,
                                    ptInvoke.Y,
                                    this.Handle,
                                    IntPtr.Zero);

                browser.OnContextMenuMouseHover(new ContextMenuMouseHoverEventArgs(string.Empty));

                if (selected >= WinAPI.CMD_FIRST)
                {
                    string command = ContextMenuHelper.GetCommandString(iContextMenu, selected - WinAPI.CMD_FIRST, true);

                    if (command == "Explore" && browser.FolderView.SelectedNode != null)
                    {
                        if (!browser.FolderView.SelectedNode.IsExpanded)
                            browser.FolderView.SelectedNode.Expand();

                        browser.FolderView.SelectedNode = browser.FolderView.SelectedNode.Nodes[hitTest.Item.Text];
                    }
                    else if (command == "rename")
                    {
                        hitTest.Item.BeginEdit();
                    }
                    else
                    {
                        ContextMenuHelper.InvokeCommand(
                            iContextMenu,
                            selected - WinAPI.CMD_FIRST,
                            ShellItem.GetRealPath(browser.SelectedItem),
                            ptInvoke);
                    }
                }
            }
        }
        catch (Exception) { }
        finally
        {
            if (iContextMenu != null)
            {
                Marshal.ReleaseComObject(iContextMenu);
                iContextMenu = null;
            }

            if (iContextMenu2 != null)
            {
                Marshal.ReleaseComObject(iContextMenu2);
                iContextMenu2 = null;
            }

            if (iContextMenu3 != null)
            {
                Marshal.ReleaseComObject(iContextMenu3);
                iContextMenu3 = null;
            }

            if (contextMenu != null)
                WinAPI.DestroyMenu(contextMenu);

            if (iContextMenuPtr != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr);

            if (iContextMenuPtr2 != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr2);

            if (iContextMenuPtr3 != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr3);
        }
    }
  }
}